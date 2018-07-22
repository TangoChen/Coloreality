using System;

namespace Coloreality.Server
{
    public class ConnectionEventArgs : EventArgs
    {
        public Connection Connection { get; private set; }
        public ConnectionEventArgs(Connection connection)
        {
            Connection = connection;
        }
    }

    public class ConnectionErrorEventArgs : ErrorEventArgs
    {
        public string ConnectionName { get; private set; }

        public ConnectionErrorEventArgs(string connectionName, string message) : base(message)
        {
            ConnectionName = connectionName;
        }
    }

    public class ConnectionReceiveEventArgs : ReceiveEventArgs
    {
        public string ClientName { get; private set; }

        public ConnectionReceiveEventArgs(string clientName, byte[] rawMessage) : base(rawMessage)
        {
            ClientName = clientName;
        }
    }
}
