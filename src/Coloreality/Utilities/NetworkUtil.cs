using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Coloreality
{
    public static class NetworkUtil
    {
        public const int PORT_MIN = 0;
        public const int PORT_MAX = 65535;

        public static IPAddress GetIp()
        {
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress ip in addressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return null;
        }

        public static int GetOpenPort(int start = 10000, int length = 500)
        {
            int searchingEnd = System.Math.Min(PORT_MAX, start + length);
            List<int> usingPorts = GetUsingPorts();
            for (int checkPort = start; checkPort <= searchingEnd; checkPort++)
            {
                if (!usingPorts.Contains(checkPort))
                {
                    return checkPort;
                }
            }
            return 0;
        }

        public static bool IsPortAvailable(int value)
        {
            return value >= PORT_MIN && value <= PORT_MAX && !GetUsingPorts().Contains(value);
        }

        private static List<int> GetUsingPorts()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            return tcpEndPoints.Select(p => p.Port).ToList<int>();
        }


    }
}
