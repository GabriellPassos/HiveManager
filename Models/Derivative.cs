using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Derivative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public double TotalMl { get; set; }
        public int PriceId { get; set; }
        public Price Price { get; set; }
        public int WithdrawndHoneyId { get; set; }
        public virtual WithdrawndHoney WithdrawndHoney { get; set; }
        public virtual List<Product> Products { get; set; }
        public Derivative()
        {
        }
        public Derivative(string name, Price price, WithdrawndHoney withdrawndHoney)
        {
            Name = name;
            Status = true;
            Price = price;
            WithdrawndHoney = withdrawndHoney;
        }
    }
}
