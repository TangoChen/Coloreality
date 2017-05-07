using System;

namespace Coloreality.Server
{
    public delegate void ConnectEventHandler(object sender, ConnectionEventArgs e);
    public delegate void DisconnectEventHandler(object sender, ConnectionEventArgs e);

    public class ConnectionEventArgs : EventArgs
    {
        public Connection Connection { get; private set; }
        public ConnectionEventArgs(Connection connection)
        {
            Connection = connection;
        }
    }
}
