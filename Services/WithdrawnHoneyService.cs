using HiveManager.Data;
using HiveManager.Interface;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class WithdrawnHoneyService : WithdrawndHoney
    {
        public readonly HiveContext _context;
        public readonly IHiveService _hiveService;

        public WithdrawnHoneyService(HiveContext context, IHiveService hiveService)
        {
            _context = context;
            _hiveService = hiveService;
        }
        public void Insert(double quantityMl, int specieId)
        {
            List<Hive> hives = _context.Hives.Include(y => y.Specie).Where(x => x.SpecieId == specieId).OrderBy(z => z.TotalHoney).ToList();
            WithdrawndHoney withdrawndHoney = _context.WithdrawndsHoney.OrderBy(x => x.WithdrawDate).LastOrDefault(z => z.SpecieId == specieId);
            if (hives.Count > 0)
            {
                _hiveService.UpdateTotalHoney(hives);
                if (hives.Sum(y => y.TotalHoney) >= quantityMl && quantityMl > 0)
                {
                    if (withdrawndHoney == null)
                    {
                        withdrawndHoney = new WithdrawndHoney(quantityMl, quantityMl, _context.BeeSpecies.
                            FirstOrDefault(z => z.Id == specieId));
                    }
                    else
                    {
                        withdrawndHoney = new WithdrawndHoney(quantityMl, quantityMl + withdrawndHoney.HoneyRemainingMl, _context.BeeSpecies.
                            FirstOrDefault(z => z.Id == specieId));
                    }
                    foreach (var item in hives)
                    {
                        if (item.TotalHoney - quantityMl >= 0)
                        {
                            item.TotalHoney -= quantityMl;
                            quantityMl = 0;
                        }
                        else
                        {
                            quantityMl = quantityMl - item.TotalHoney;
                            item.TotalHoney = 0;
                        }
                    }
                    _context.WithdrawndsHoney.Add(withdrawndHoney);
                    _context.SaveChanges();
                }
            }
        }
        public void Remove(int[] withdrawnId)
        {
            if (withdrawnId.Any())
            {
                _context.WithdrawndsHoney.RemoveRange(_context.WithdrawndsHoney.Where(x => withdrawnId.Contains(x.Id)));
                _context.SaveChanges();
            }
        }
        public List<WithdrawndHoney> Search(int[] ids)
        {
            if (ids.Any())
            {
                return _context.WithdrawndsHoney.Where(x => ids.Contains(x.Id)).ToList();
            }
            return _context.WithdrawndsHoney.ToList();
        }
        public List<WithdrawndHoney> Search()
        {
            return _context.WithdrawndsHoney.ToList();
        }
    }
}
