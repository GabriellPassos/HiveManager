using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiveManager.Data;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HiveManager.Interface
{
    public interface IHiveService
    {
        public void Insert(int quantityBeesInside, int specieId)
        {

        }
        public void Remove(int[] hiveId)
        {

        }
        public void Update(int hiveId, int quantityBeesInside, int specieId)
        {
        }
        public List<Hive> Search()
        {
            return new List<Hive>();
        }
        public List<Hive> Search(int[] ids, bool? status, int specieId, string specieName)
        {

            return new List<Hive>();
        }
        public void UpdateTotalHoney(List<Hive> hives)
        {
        }
        public void UpdateTotalHoney(Hive hive)
        {

        }
    }
}
