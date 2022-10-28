using HiveManager.Data;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HiveManager.Services
{
    public class PackageService
    {
        public readonly HiveContext _context;

        public PackageService(HiveContext context)
        {
            _context = context;
        }
        public void Insert(string name, int quantity, double volumeMl, int priceId)
        {
            if (string.IsNullOrEmpty(name) == false && quantity > 0 && volumeMl > 0 && priceId > 0)
            {
                Package package = _context.Packages.FirstOrDefault(z => z.Name.ToLower() == name.ToLower());
                Price price = _context.Prices.FirstOrDefault(z => z.Id == priceId);


                if (package != null)
                {
                    if (package.Status == true)
                    {
                        package.Quantity += quantity;
                    }
                }
                else if (price != null)
                {
                    _context.Packages.Add(new Package(name, quantity, volumeMl, price));
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
        public bool Update(int packageId, string name, int quantity, double volumeMl, int PriceId)
        {
            Package package = _context.Packages.FirstOrDefault(x => x.Id == packageId);
            Price price = _context.Prices.FirstOrDefault(x => x.Id == PriceId);
            bool changeFlag = false;
            if (package != null)
            {
                if (quantity > 0)
                {
                    int qntAux = package.Quantity - quantity;
                    package.Quantity = quantity;
                    package.Quantity += qntAux;
                    changeFlag = true;
                }
                if (volumeMl > 0)
                {
                    package.VolumeMl = volumeMl;
                    changeFlag = true;
                }
                if (name.Length > 0)
                {
                    package.Name = name;
                    changeFlag = true;
                }
                if (price != null)
                {
                    package.Price = price;
                    changeFlag = true;
                }
            }

            _context.SaveChanges();
            return changeFlag;
        }
        public List<Package> Search(int[] ids, bool? status)
        {
            if (ids.Any())
            {
                return _context.Packages.Where(x => ids.Contains(x.Id)).ToList();
            }
            if (status != null)
            {
                return _context.Packages.Where(item => item.Status == status).ToList();
            }
            return _context.Packages.ToList();
        }
        public List<Package> Search()
        {
            return _context.Packages.ToList();
        }
        public void Add(int quantity, int packageId)
        {
            if( packageId > 0)
            {
                Package package = _context.Packages.FirstOrDefault(x => x.Id == packageId);
                if (package != null)
                {
                    package.Quantity += quantity;
                }
            }
        }
    }
}

