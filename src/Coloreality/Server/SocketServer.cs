using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Coloreality.Server
{
    public class SocketServer
    {
        public IPAddress Ip { get; private set; }

        public int Port { get; private set; }

        private const int OpenPortFlag = -1;
        /// <summary>
        /// Try setting a port for server. Returns true if the port is available.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="setOpenPortIfFailed">If true, will automatically set an open port when the input one is not available.</param>
        /// <returns>return true if succeeded.</returns>
        public bool TrySetPort(int port = OpenPortFlag, bool setOpenPortIfFailed = false)
        {
            if (isListening)
            {
                if (OnError != null) OnError.Invoke(this, new ConnectionErrorEventArgs("Server", "Cannot change the port when server is running."));
                return false;
            }
            if (port == OpenPortFlag)
            {
                Port = NetworkUtil.GetOpenPort();
                return true;
            }
            else
            {
                if (NetworkUtil.IsPortAvailable(port))
                {
                    Port = port;
                    return true;
                }
                else
                {
                    if (setOpenPortIfFailed)
                    {
                        TrySetPort();
                    }
                    return false;
                }
            }
        }

        public event ConnectEventHandler OnConnected;
        public event DisconnectEventHandler OnDisconnected;
        public event ConnectionErrorEventHandler OnError;
        //public event ReceiveEventHandler OnReceived;

        private Dictionary<string, Connection> connectionsByName = new Dictionary<string, Connection>();
        public Connection GetConnection(string name)
        {
            if (connectionsByName.ContainsKey(name)) { 
                return connectionsByName[name];
            }
            else
            {
                return null;
            }
        }

        private Connection[] connections = new Connection[0];
        public Connection[] Connections
        {
            get
            {
                return connections;
            }
        }

        private int sendInterval = Globals.DefaultSendInterval;
        public int SendInterval
        {
            get
            {
                return sendInterval;
            }
            set
            {
                if(value >= 0)
                {
                    sendInterval = value;
                }
            }
        }

        public void SetAllSendInterval(int value)
        {
            if (value < 0) return;
            for (int i = 0; i < connections.Length; i++)
            {
                connections[i].SendInterval = value;
            }
        }

        Thread listenThread;
        Socket listener;
        bool isListening = false;

        public bool IsListening
        {
            get
            {
                return isListening;
            }
        }

        public SocketServer(bool autoStartListen = false, int port = -1)
        {
            Ip = NetworkUtil.GetIp();
            TrySetPort(port, true);
            
            if (autoStartListen)
            {
                Listen();
            }
        }

        /// <summary>
        /// For manually setting ip address and port with simple checks on machine that does not support as some NetworkUtil class functions as on PC.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public SocketServer(string ip, int port = Globals.ServerDefaultPort)
        {
            if (!SetIp(ip))
            {
                if (OnError != null) OnError.Invoke(this, new ConnectionErrorEventArgs("Server", "Cannot set ip: " + ip + "."));
            }

            if (NetworkUtil.IsPortInRange(port))
            {
                Port = port;
            }
            else if (OnError != null) {
                OnError.Invoke(this, new ConnectionErrorEventArgs("Server", "Port number is out of range: " + port.ToString() + "."));
            }

        }

        public bool SetIp(string ip)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(ip, out ipAddress))
            {
                Ip = ipAddress;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Listen()
        {
            if(listener == null) listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenThread = new Thread(ListenThread)
            {
                IsBackground = true
            };
            listenThread.Start();
        }

        private void ListenThread()
        {
            IPEndPoint endPoint = new IPEndPoint(Ip, Port);
            listener.Bind(endPoint);
            listener.Listen(10);

            isListening = true;
            while (isListening)
            {
                try
                {
                    Socket socketConnection = listener.Accept();
                    Connection connection = new Connection(socketConnection, OnClosedConnection, Globals.DefaultBufferSize, sendInterval);
                    connection.OnError += OnError;
                    connectionsByName.Add(connection.Name, connection);
                    UpdateConnections();
                    if (OnConnected != null) OnConnected.Invoke(this, new ConnectionEventArgs(connection));
                }
                catch (Exception ex)
                {
                    if (isListening && OnError != null) OnError(this, new ConnectionErrorEventArgs("Server", ex.GetType().ToString() + ": " + ex.Message + ex.StackTrace));
                }
            }
        }

        private void UpdateConnections()
        {
            connections = connectionsByName.Values.ToArray();
        }

        public void SendAllRawBytes(byte[] value)
        {
            for (int i = 0; i < connections.Length; i++)
            {
                connections[i].SendRawBytes(value);
            }
        }

        private void OnClosedConnection(object sender, ConnectionEventArgs e)
        {
            connectionsByName.Remove(e.Connection.Name);
            UpdateConnections();
            if (OnDisconnected != null) OnDisconnected.Invoke(this, e);
        }

        public void CloseConnection(string name)
        {
            connectionsByName[name].Close(true);
        }

        public void CloseAllConnections()
        {
            for (int i = 0; i < connections.Length; i++)
            {
                connections[i].Close(true);
            }
        }
        
        public bool HasConnection
        {
            get
            {
                return connectionsByName.Count > 0;
            }
        }

        public int ConnectionCount
        {
            get
            {
                return connectionsByName.Count;
            }
        }

        /// <summary>
        /// Close Server.
        /// </summary>
        public void Close()
        {
            isListening = false;

            CloseAllConnections();

            if (listener != null)
            {
                if (listener.Connected)
                {
                    listener.Shutdown(SocketShutdown.Both);
                }
                listener.Close();
                listener = null;
            }
        }

    }
}
