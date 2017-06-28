using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Project_mtrs
{
    public class PingObject
    {
        public string Host { get; set; }
        public double Last { get; set; }
        public double Average { get; set; }
        public double Best { get; set; }
        public double Worst { get; set; }
        public double SDevation { get; set; }
        public double Sum { get; set; }
        public double Loss { get; set; }
        public int Count { get; set; }
        public int LostCount { get; set; }
        public List<double> Values { get; set; }

        public PingObject() { }
        public PingObject(string host)
        {
            Host = host;
        }

        public void SetAverage()
        {
            Average = Sum / Count;
        }

        public async Task<double> GetSDevationAsync()
        {
            return await Task.Run(() => GetSDevation());
        }

        private double GetSDevation()
        {
            double sum = 0;
            foreach (var item in Values)
            {
                sum += Math.Pow(item, 2);
            }

            return sum - (Count * Math.Pow(Average, 2));
        }

        public void SetLoss()
        {
            Loss = LostCount / Sum * 100;            
        }

        public override string ToString()
        {
            return String.Format("{0, 46}\t{1, 5}%\t{2, 5}\t{3, 5}\t{4, 5}\t{5, 5}\t{6, 5}\t{7, 5}", Host, Loss.ToString("0.0"), Count, Last.ToString("0.0"), Average.ToString("0.0"), Best.ToString("0.0"), Worst.ToString("0.0"), SDevation.ToString("0.0"));
        }
    }
}
