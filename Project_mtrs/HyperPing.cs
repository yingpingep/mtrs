using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Project_mtrs
{
    public class HyperPing
    {
        public IPAddress[] GetHostList(string destination)
        {
            return GetHostList(destination, 1).ToArray();
        }

        private IEnumerable<IPAddress> GetHostList(string destination, int ttl)
        {
            List<IPAddress> result = new List<IPAddress>();
            Ping ping = new Ping();

            PingOptions option = new PingOptions(ttl, true);
            byte[] buffer = new byte[1024];
            int timeout = 5000;

            PingReply reply = ping.Send(destination, timeout, buffer, option);

            if (reply.Status == IPStatus.Success)
            {
                // Arrive destination.
                result.Add(reply.Address);
            }
            else if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut)
            {
                if (reply.Status == IPStatus.TtlExpired)
                {
                    // Arrive a hop.
                    result.Add(reply.Address);
                }

                // Go to next hop.
                IEnumerable<IPAddress> temp = GetHostList(destination, ttl + 1);
                result.AddRange(temp);
            }

            return result;
        }
    }
}
