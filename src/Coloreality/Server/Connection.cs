using System;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Coloreality.Server
{
    public class Connection
    {
        public string Name { get; private set; }
        public int BufferSize { get; set; }
        public int SendInterval { get; set; }

        public event ConnectionReceiveEventHandler OnReceivedMessage;
        public event DisconnectEventHandler OnDisconnected;
        public event ConnectionErrorEventHandler OnError;

        public bool closeForCommand = true;

        private Socket socket;
        private Thread threadReceive;

        private bool doReceive = true;

        Stopwatch sendIntervalWatch = new Stopwatch();

        public Connection(Socket socket, DisconnectEventHandler disconnectHandler, int bufferSize = Globals.DefaultBufferSize, int sendInterval = Globals.DefaultSendInterval)
        {
            this.socket = socket;
            BufferSize = bufferSize;
            SendInterval = sendInterval;

            OnDisconnected += disconnectHandler;
            Name = socket.RemoteEndPoint.ToString();

            threadReceive = new Thread(ThreadReceive)
            {
                IsBackground = true
            };
            threadReceive.Start();
        }

        void ThreadReceive()
        {
            while (doReceive)
            {
                try
                {
                    if (socket.Available <= 0) continue;

                    byte[] buffer = new byte[BufferSize];
                    int length = socket.Receive(buffer);
                    if (length > 0)
                    {
                        byte[] message = new byte[length];
                        Buffer.BlockCopy(buffer, 0, message, 0, length);
                        ConnectionReceiveEventArgs newClientMessage = new ConnectionReceiveEventArgs(Name, message);

                        if (OnReceivedMessage != null)
                        {
                            OnReceivedMessage.Invoke(this, newClientMessage);
                        }

                        if (closeForCommand && newClientMessage.MessageType == DataType.Close)
                        {
                            Close(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if(doReceive) HandleConnectionException(ex);
                }
            }
        }

        private void HandleConnectionException(Exception ex)
        {
            if (OnError != null) OnError.Invoke(this, new ConnectionErrorEventArgs(Name, ex.GetType().ToString() + ": " + ex.Message + ex.StackTrace));
            if (!socket.Connected)
            {
                Close();
            }
        }

        public void OnReaderReady(object sender, SerializationEventArgs e)
        {
            SendSerializedData(e.DataIndex, e.Data, e.CanSkip);
        }

        public void SendRawBytes(byte[] value, bool useSendInterval = false)
        {
            if (socket.Connected)
            {
                try
                {
                    if (CanSend(useSendInterval))
                    {
                        socket.Send(value);
                    }
                    //sendIntervalWatch.Restart();  //Restart() won't work for ColorealityUnity due to its different target framework that does not support it.
                    sendIntervalWatch.Reset();
                    sendIntervalWatch.Start();
                }
                catch (Exception ex)
                {
                    HandleConnectionException(ex);
                }
            }
        }

        public void SendBytesWithDefinedStart(byte first, byte[] value, bool useSendInterval = false)
        {
            SendRawBytes(ByteUtil.GetStartDefinedBytes(first, value), useSendInterval);
        }

        public void SendBytesWithDefinedStart(DataType type, byte[] value, bool useSendInterval = false)
        {
            SendBytesWithDefinedStart((byte)type, value, useSendInterval);
        }

        public void SendType(DataType type, bool useSendInterval = false)
        {
            SendRawBytes(new byte[]{(byte)type}, useSendInterval);
        }
        
        public void SendBytes(byte[] value, bool useSendInterval = false)
        {
            SendBytesWithDefinedStart(DataType.Byte, value, useSendInterval);
        }

        public void SendString(string value, bool useSendInterval = false)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            SendBytesWithDefinedStart(DataType.String, bytes, useSendInterval);
        }

        public void SendSerializedData(int dataIndex, byte[] data, bool useSendInterval = false)
        {
            if (CanSend(useSendInterval))
            {
                PreSerialization newPreSerial = new PreSerialization(dataIndex, data.Length + 1);
                SendBytesWithDefinedStart(DataType.PreSerialization, newPreSerial.ToSerialization());
                SendBytesWithDefinedStart(DataType.Serialization, data);
            }
        }

        private bool CanSend(bool useSendInterval = true)
        {
            return (!useSendInterval || !sendIntervalWatch.IsRunning || sendIntervalWatch.ElapsedMilliseconds > SendInterval);
        }

        /// <summary>
        /// Close connection.
        /// </summary>
        /// <param name="isInitiative">
        /// If true, will send a Close command to the client side. Like saying "Hey, I'm closing the connection! (Do something if you need.)"
        /// If false, just close without telling the client. False can be used when has been told to close(client sent the Close command to here) and no need to notify back.
        /// </param>
        public void Close(bool isInitiative = false)
        {
            doReceive = false;
            if (socket != null)
            {
                if (socket.Connected)
                {
                    if (isInitiative) SendType(DataType.Close);
                    Thread.Sleep(30);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                if (OnDisconnected != null) OnDisconnected.Invoke(this, new ConnectionEventArgs(this));
            }
        }

    }
}
