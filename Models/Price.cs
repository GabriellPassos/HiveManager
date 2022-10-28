using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Price
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Unit { get; set; } //Preço por um (Ml, unidade, etc)
        public double ProfitPercentage { get; set; } //Porcentagem de lucro
        public bool? Status { get; set; }
        public Price()
        {
        }
        public Price(double unit, double profitPercentage, string name)
        {
            Name = name;
            Unit = unit;
            ProfitPercentage = profitPercentage / 100;
            Status = true;
        }
    }
}
