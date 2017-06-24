using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Coloreality
{
    public static class NetworkUtil
    {
        public const int PortMin = 0;
        public const int PortMax = 65535;

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
            int searchingEnd = System.Math.Min(PortMax, start + length);
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
            return IsPortInRange(value) && !GetUsingPorts().Contains(value);
        }

        public static bool IsPortInRange(int value)
        {
            return value >= PortMin && value <= PortMax;
        }

        private static List<int> GetUsingPorts()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            return tcpEndPoints.Select(p => p.Port).ToList<int>();
        }


    }
}
