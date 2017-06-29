using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_mtrs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Clear();
            string welcome = "MTRs [v 0.0.1]";
            int x = (Console.WindowWidth - welcome.Length) / 2;
            Console.CursorLeft = x;
            Console.CursorVisible = false;
            Console.WriteLine(welcome);
            Console.CursorLeft = 0;

            Thread getHost = new Thread(Hyper.GetHostHelper);
            getHost.Start("8.8.8.8");

            Console.WriteLine("{0, -30}\t\t  Loss\tSent\tLast\tAvg\tBest\tWorst", "Address");
            
        }
    }


    [Serializable]
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException() { }
        public DestinationNotFoundException(string message) : base(message) { }
        public DestinationNotFoundException(string message, Exception inner) : base(message, inner) { }
        public override string ToString()
        {
            return "Make sure enter an IP address or a host name.\n";
        }
        protected DestinationNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
