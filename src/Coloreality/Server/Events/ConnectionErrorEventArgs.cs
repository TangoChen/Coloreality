namespace Coloreality.Server
{
    public delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);

    public class ConnectionErrorEventArgs : ErrorEventArgs
    {
        public string ConnectionName { get; private set; }

        public ConnectionErrorEventArgs(string connectionName, string message) : base(message)
        {
            ConnectionName = connectionName;
        }
    }
}
