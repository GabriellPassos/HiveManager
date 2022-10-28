using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
        public double VolumeMl { get; set; }
        //public double TotalPrice { get; set; }
        public int PriceId { get; set; }
        public virtual Price Price { get; set; }
        public Package()
        {
        }
        public Package(string name, int quantity, double volumeMl, Price price)
        {
            Name = name;
            Quantity = quantity;
            //TotalPrice = quantity ;
            VolumeMl = volumeMl;
            Price = price;
            Status = true;
        }
    }
}
