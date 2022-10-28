using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class WithdrawndHoney
    {
        public int Id { get; set; }
        public double WithdrawnHoneyMl { get; set; }
        public double HoneyRemainingMl{ get; set; }
        public DateTime WithdrawDate { get; set; }
        public int SpecieId { get; set; }
        public virtual BeeSpecie Specie { get; set; }
        public WithdrawndHoney()
        {
        }
        public WithdrawndHoney(double quantityWithdrawn, double quantityMl, BeeSpecie specie)
        {
            WithdrawnHoneyMl = quantityWithdrawn;
            HoneyRemainingMl = quantityMl;
            WithdrawDate = DateTime.UtcNow;
            Specie = specie;
        }
    }
}
