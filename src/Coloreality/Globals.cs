using System.Threading;

namespace Coloreality
{
    public static class Globals
    {
        public const int SERVER_DEFAULT_PORT = 2333;
        public const string SERVER_DEFAULT_IP = "192.168.1.101";
        public const int DEFAULT_BUFFER_SIZE = 9000;
        
        /// <summary>
        /// Millisecond but integer type.
        /// </summary>
        public const int DEFAULT_SEND_INTERVAL = 30;

        public static void CloseThreadIfExists(ref Thread thread)
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

    }

}