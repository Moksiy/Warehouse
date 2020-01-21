using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_DB
{
    public class Element
    {
        public string ProductName { get; set; }
        public byte Status { get; set; }
        public double Cost { get; set; }
        public int Quantity { get; set; }
        public double Result { get; set; }
        public DateTime StatusDate { get; set; }

        public Element(string name, byte status, double cost, int quan, double res, DateTime datest)
        {
            ProductName = name;
            Status = status;
            Cost = cost;
            Quantity = quan;
            Result = res;
            StatusDate = datest;
        }
    }
}
