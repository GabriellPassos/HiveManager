using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class BeeSpecie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double HoneyMod { get; set; }
        public bool Status { get; set; } 
        public virtual List<Hive> Hives { get; set; }
        public BeeSpecie()
        {
        }

        public BeeSpecie(string name, double honeyMod)
        {
            Name = name;
            HoneyMod = honeyMod;
            Status = true;
        }
    }
}
