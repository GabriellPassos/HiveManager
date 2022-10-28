using HiveManager.Data;
using HiveManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class PriceService
    {
        public readonly HiveContext _context;

        public PriceService(HiveContext context)
        {
            _context = context;
        }
        public void Insert(double PricePerOne, double profitPercentage, string name)
        {
            if (PricePerOne > 0 && profitPercentage > 0)
            {
                _context.Prices.Add(new Price(PricePerOne, profitPercentage, name != null ?  name : "empty"));
                _context.SaveChanges();
            }
        }
        public void Remove(int[] priceId)
        {
            if (priceId.Any())
            {
                foreach (var item in _context.BeeSpecies.Where(y => priceId.Contains(y.Id)))
                {
                    item.Status = false;
                }
                _context.SaveChanges();
            }
        }
        public bool Update(int PriceId, double PricePerOne, double profitPercentage, string name, bool? status)
        {
            Price price = _context.Prices.FirstOrDefault(x => x.Id == PriceId);
            bool changeFlag = false;
            if (price != null)
            {
                if (PricePerOne > 0)
                {
                    price.Unit = PricePerOne;
                    changeFlag = true;
                }
                if (profitPercentage > 0)
                {
                    price.ProfitPercentage = profitPercentage;
                    changeFlag = true;
                }
                if (name.Length > 0)
                {
                    price.Name = name;
                    changeFlag = true;
                }
                if (status != null)
                {
                    price.Status = status;
                    changeFlag = true;
                }
            }
            _context.SaveChanges();
            return changeFlag;
        }
        public List<Price> Search(int[] ids, bool? status)
        {
            if (ids.Any())
            {
                return _context.Prices.Where(x => ids.Contains(x.Id)).ToList();
            }
            if (status != null)
            {
                return _context.Prices.Where(item => item.Status == status).ToList();
            }
            return _context.Prices.ToList();
        }
        public List<Price> Search()
        {
            return _context.Prices.ToList();
        }
    }
}

