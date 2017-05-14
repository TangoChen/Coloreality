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

        public List<string> ClientNameList { get; private set; }
        Dictionary<string, Connection> connections = new Dictionary<string, Connection>();

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
                    Connection connection = new Connection(socketConnection, OnClosedConnection);
                    connection.OnError += OnError;
                    connections.Add(connection.Name, connection);
                    UpdateClientNames();
                    if (OnConnected != null) OnConnected.Invoke(this, new ConnectionEventArgs(connection));
                }
                catch (Exception ex)
                {
                    if (isListening && OnError != null) OnError(this, new ConnectionErrorEventArgs("Server", ex.GetType().ToString() + ": " + ex.Message + ex.StackTrace));
                }
            }
        }
        
        private void UpdateClientNames()
        {
            ClientNameList = connections.Keys.ToList();
        }

        public void SendAllRawBytes(byte[] value)
        {
            foreach (Connection connection in connections.Values)
            {
                connection.SendRawBytes(value);
            }
        }

        private void OnClosedConnection(object sender, ConnectionEventArgs e)
        {
            connections.Remove(e.Connection.Name);
            UpdateClientNames();
            if (OnDisconnected != null) OnDisconnected.Invoke(this, e);
        }

        public void CloseConnection(string name)
        {
            connections[name].Close(true);
        }

        public void CloseAllConnections()
        {
            List<Connection> connectionList = connections.Values.ToList();
            for (int i = 0; i < connectionList.Count; i++)
            {
                connectionList[i].Close(true);
            }
        }
        
        public bool HasConnection
        {
            get
            {
                return connections.Count > 0;
            }
        }

        public int ConnectionCount
        {
            get
            {
                return connections.Count;
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
