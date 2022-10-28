using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiveManager.Data;
using HiveManager.Interface;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;

namespace HiveManager.Services
{
    public class HiveService : IHiveService
    {
        public readonly HiveContext _context;
        public HiveService(HiveContext context)
        {
            _context = context;
        }
        public void Insert(int quantityBeesInside, int specieId)
        {
            BeeSpecie specie = _context.BeeSpecies.FirstOrDefault(x => x.Id == specieId);
            if (specie != null && quantityBeesInside > 0)
            {
                if (specie.Status == true)
                {
                    _context.Hives.Add(new Hive(quantityBeesInside, specie));
                    _context.SaveChanges();
                }
            }
        }
        public void Remove(int[] hiveId)
        {
            if (hiveId.Any())
            {
                foreach (var item in _context.BeeSpecies.Where(y => hiveId.Contains(y.Id)))
                {
                    item.Status = false;
                }
                _context.SaveChanges();
            }
        }
        public void Update(int hiveId, int quantityBeesInside, int specieId)
        {
            Hive hive = _context.Hives.Find(hiveId);
            BeeSpecie beeSpecie = _context.BeeSpecies.Find(specieId);
            if (hive != null)
            {
                if (quantityBeesInside > 0)
                {
                    hive.QuantityBeesInside = quantityBeesInside;
                }
            }
            if (beeSpecie != null)
            {
                hive.Specie = beeSpecie;
            }
            _context.SaveChanges();
        }
        public List<Hive> Search()
        {
            return _context.Hives.Include(x => x.Specie).ToList();
        }
        public List<Hive> Search(int[] ids, bool? status, int specieId, string specieName)
        {
            List<Hive> selectedHives = new();
            if (ids.Any())
            {
                selectedHives = _context.Hives.Include(x => x.Specie).Where(item => ids.Contains(item.Id)).ToList();
            }
            else if (specieId > 0)
            {
                selectedHives = _context.Hives.Include(x => x.Specie).Where(item => specieId == item.SpecieId).ToList();
            }
            else if (specieName != null)
            {
                selectedHives = _context.Hives.Where(item => specieName.ToLower() == item.Specie.Name).ToList();
            }
            else
            {
                selectedHives = _context.Hives.Include(x => x.Specie).ToList();
            }
            if (status != null)
            {
                var listAux = selectedHives.Where(z => z.Status == status);
                selectedHives = listAux.ToList();
            }
            UpdateTotalHoney(selectedHives);
            return selectedHives;
        }
        public void UpdateTotalHoney(List<Hive> hives)
        {
            //Hive hive = _context.Hives.Include(z => z.Specie).FirstOrDefault(x => x.Id == hiveId);
            foreach (var item in hives)
            {
                if (item.Specie.Status == true && item.Status)
                {
                    item.TotalHoney += item.Specie.HoneyMod * DateTime.UtcNow.Subtract(item.DateLastUpdate).TotalMinutes * item.QuantityBeesInside;
                    item.DateLastUpdate = DateTime.UtcNow;
                }
            }
            _context.SaveChanges();
        }
        public void UpdateTotalHoney(Hive hive)
        {
            //Hive hive = _context.Hives.Include(z => z.Specie).FirstOrDefault(x => x.Id == hiveId);
            if (hive != null && hive.Specie.Status == true && hive.Status)
            {
                hive.TotalHoney += hive.Specie.HoneyMod * DateTime.UtcNow.Subtract(hive.DateLastUpdate).TotalHours * hive.QuantityBeesInside;
                hive.DateLastUpdate = DateTime.UtcNow;
            }
            _context.SaveChanges();
        }
    }
}
