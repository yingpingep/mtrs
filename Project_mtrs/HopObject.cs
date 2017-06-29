using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Project_mtrs
{
    public class HopObject
    {
        public string Host { get; set; }
        public double Last { get; set; }
        public double Average { get; set; }
        public double Best { get; set; }
        public double Worst { get; set; }
        public double Sum { get; set; }
        public double Loss { get; set; }
        public int Count { get; set; }
        public int LostCount { get; set; }

        public HopObject()
        {
            Last = 0;
            Best = 0;
            Worst = 0;
            Sum = 0;
            Count = 0;
            Loss = 0;
            LostCount = 0;
        }

        public void SetAverage()
        {
            Average = Sum / Count;
        }
        public void SetLoss()
        {
            Loss = LostCount / Count * 100;
        }

        public override string ToString()
        {
            return String.Format("{0, -30}\t{1, 5}%\t{2, 5}\t{3, 5}\t{4, 5}\t{5, 5}\t{6, 5}", Host.ToString(), Loss, Count, Last, Average.ToString("0.0"), Best, Worst);
        }
    }
}
