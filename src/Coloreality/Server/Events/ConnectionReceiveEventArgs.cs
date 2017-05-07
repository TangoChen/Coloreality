namespace Coloreality.Server
{
    public delegate void ConnectionReceiveEventHandler(object sender, ConnectionReceiveEventArgs e);

    public class ConnectionReceiveEventArgs : ReceiveEventArgs
    {
        public string ClientName { get; private set; }

        public ConnectionReceiveEventArgs(string clientName, byte[] rawMessage) : base(rawMessage)
        {
            ClientName = clientName;
        }
    }
}
