using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project_mtrs
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
