using HiveManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Hive
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int QuantityBeesInside { get; set; }
        public DateTime StartDate { get; set; }
        public double TotalHoney { get; set; }
        public DateTime DateLastUpdate { get; set; }
        public BeeSpecie Specie { get; set; }
        public int SpecieId { get; set; }
        [NotMapped]
        public int WithdrawndHoneyId { get; set; }
        public Hive()
        {
        }
        public Hive(int quantityBeesInside, BeeSpecie specie)
        {
            QuantityBeesInside = quantityBeesInside;
            Status = specie.Status;
            Specie = specie;
            TotalHoney = 0;
            StartDate = DateTime.UtcNow;
            DateLastUpdate = DateTime.UtcNow;

        }
      
    }
}
