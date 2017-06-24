using System.Threading;

namespace Coloreality
{
    public static class Globals
    {
        public const int ServerDefaultPort = 2333;
        public const string ServerDefaultIp = "192.168.1.101";
        public const int DefaultBufferSize = 9000;
        
        /// <summary>
        /// Millisecond but integer type.
        /// </summary>
        public const int DefaultSendInterval = 30;

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