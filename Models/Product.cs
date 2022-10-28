using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public int DerivativeId { get; set; }
        public virtual Derivative Derivative { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
        public Product()
        {
        }
        public Product(string name, Derivative derivative, Package package, double price)
        {
            Name = name;
            Derivative = derivative;
            Package = package;
            Price = price;
            Status = derivative.Status && package.Status;
        }
    }
}
