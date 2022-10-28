using HiveManager.Data;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class DerivativeService
    {
        public readonly HiveContext _context;

        public DerivativeService(HiveContext context)
        {
            _context = context;
        }
        public void Insert(string name, int PriceId, int SpecieId)
        {
            if (string.IsNullOrEmpty(name) == false && PriceId > 0 && SpecieId > 0)
            {
                Derivative derivative = _context.Derivatives.FirstOrDefault(z => z.Name.ToLower() == name.ToLower());
                Price price = _context.Prices.FirstOrDefault(x => x.Id == PriceId);
                WithdrawndHoney withdrawndHoney = _context.WithdrawndsHoney.OrderBy(z => z.WithdrawDate).
                    LastOrDefault(x => x.SpecieId == SpecieId);
                if (derivative != null)
                {
                    return;

                }
                else if (price != null && withdrawndHoney != null && derivative == null)
                {
                    if (price.Status == true)
                    {
                        _context.Derivatives.Add(new Derivative(name, price, withdrawndHoney));
                    }
                }
                _context.SaveChanges();
            }
        }
        public void Remove(int[] derivateId)
        {
            if (derivateId.Any())
            {
                foreach (var item in _context.BeeSpecies.Where(y => derivateId.Contains(y.Id)))
                {
                    item.Status = false;
                }
                _context.SaveChanges();
            }
        }
        public void Update(int derivateId, string name, bool? status, int priceId)
        {
            Derivative derivative = _context.Derivatives.FirstOrDefault(x => x.Id == derivateId);
            Price price = _context.Prices.FirstOrDefault(x => x.Id == priceId);
            if (derivative != null)
            {
                if (name.Length > 0)
                {
                    derivative.Name = name;
                }
                if (status.HasValue)
                {
                    derivative.Status = status.Value;
                }
                if (price != null)
                {
                    derivative.PriceId = price.Id;
                }
                _context.SaveChanges();
            }
        }
        public List<Derivative> Search(int[] ids, int[] productId, bool? status)
        {

            if (productId.Any())
            {
                return _context.Derivatives.Include(x => x.Products).Where(x => productId.Contains(x.Id)).ToList();
            }
            else if (ids.Any())
            {
                return _context.Derivatives.Where(x => ids.Contains(x.Id)).ToList();
            }
            if (status != null)
            {
                return _context.Derivatives.Where(item => item.Status == status).ToList();
            }
            return null;
        }
        public List<Derivative> Search() 
        { 
            return _context.Derivatives.ToList();
        }
    }
}

