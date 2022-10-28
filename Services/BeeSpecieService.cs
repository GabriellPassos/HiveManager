using HiveManager.Data;
using HiveManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class BeeSpecieService : BeeSpecie
    {

        public readonly HiveContext _context;

        public BeeSpecieService(HiveContext context)
        {
            _context = context;
        }
        public void Insert(string name, double polemPerHour)
        {
            if (string.IsNullOrEmpty(name) == false && polemPerHour > 0) {
                BeeSpecie beeSpecie = _context.BeeSpecies.FirstOrDefault(z => z.Name.ToLower() == name.ToLower());
                if (beeSpecie == null)
                {
                    _context.BeeSpecies.Add(new BeeSpecie(name, polemPerHour));
                    _context.SaveChanges();
                }
            }
        }
        public void Remove(int[] specieId)
        {
            if (specieId.Any())
            {
                //_context.BeeSpecies.RemoveRange(_context.BeeSpecies.Where(x => specieId.Contains(x.Id)));
                foreach (var item in _context.BeeSpecies.Where(y => specieId.Contains(y.Id)))
                {
                    item.Status = false;
                }
                _context.SaveChanges();
            }
        }
        public void Update(int specieId, double polemPerHour)
        {
            if (polemPerHour > 0 && specieId > 0)
            {
                _context.BeeSpecies.Find(specieId).HoneyMod = polemPerHour;
            }
            _context.SaveChanges();
        }
        public List<BeeSpecie> Search(int[] ids, string specieName, bool? status)
        {
            if (ids.Any())
            {
                return _context.BeeSpecies.Where(item => ids.Contains(item.Id)).ToList();
            }
            if (specieName != null)
            {
                return _context.BeeSpecies.Where(item => specieName == item.Name).ToList();
            }
            if (status != null)
            {
                return _context.BeeSpecies.Where(item => item.Status == status).ToList();
            }
            return null;
        }
        public List<BeeSpecie> Search()
        {
            return _context.BeeSpecies.ToList();
        }
    }

}