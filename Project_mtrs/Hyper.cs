using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace Project_mtrs
{
    public class Hyper
    {
        public static void GetHostHelper(Object obj)
        {
            int top = Console.CursorTop;
            string des = (string)obj;
            var list = GetHostList(des);        
            
            while (true)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = PingWorker(list[i]);
                    Console.CursorTop = top + i;
                    Console.WriteLine("{0, -2} {1}", i + 1, list[i].ToString());
                }
            }
        }

        private static HopObject[] GetHostList(string destination)
        {
            return GetHostList(destination, 1, 1).ToArray();
        }

        private static IEnumerable<HopObject> GetHostList(string destination, int ttl, int index)
        {
            List<HopObject> result = new List<HopObject>();
            Ping ping = new Ping();

            PingOptions option = new PingOptions(ttl, true);
            byte[] buffer = new byte[1024];
            int timeout = 1000;

            PingReply reply = ping.Send(destination, timeout, buffer, option);
            HopObject hop = new HopObject();
            if (reply.Status == IPStatus.Success)
            {
                // Arrive destination.
                hop.Host = reply.Address.ToString();
                result.Add(hop);
                Console.WriteLine("{0, -2} {1}", index, hop.ToString());
            }
            else if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut)
            {
                if (reply.Status == IPStatus.TtlExpired)
                {
                    // Arrive a hop.
                    hop.Host = reply.Address.ToString();
                    result.Add(hop);
                    Console.WriteLine("{0, -2} {1}", index, hop.ToString());
                }
                else
                    index -= 1;

                // Go to next hop.
                IEnumerable<HopObject> temp = GetHostList(destination, ttl + 1, index + 1);
                result.AddRange(temp);
            }

            return result;
        }
        
        private static HopObject PingWorker(HopObject hop)
        {
            Ping ping = new Ping();        
            int timeout = 500;

            if (!hop.Host.Equals("???"))
            {            
                PingReply reply = ping.Send(hop.Host, timeout);
                double time = reply.RoundtripTime;
                hop.Count += 1;

                if (reply.Status == IPStatus.Success)
                {      
                    hop.Sum += time;

                    if (hop.Count == 1)
                    {
                        hop.Last = time;
                        hop.Best = time;
                        hop.Worst = time;
                    }
                    else
                    {
                        hop.Last = time;
                        if (time > hop.Worst)
                            hop.Worst = time;
                        else if (time < hop.Best)
                            hop.Best = time;
                    }

                    hop.SetAverage();
                }
                else
                {
                    hop.Last = time;
                    hop.LostCount += 1;
                    hop.SetLoss();
                }
            }

            ping.Dispose();
            return hop;
        }
    }
}
