using HiveManager.Data;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class ProductService : Product
    {
        public readonly HiveContext _context;

        public ProductService(HiveContext context)
        {
            _context = context;
        }
        public Product Buy(int productId, int quantity)
        {
            if (productId > 0 && quantity > 0)
            {
                Product product = _context.Products.Include(z => z.Derivative).ThenInclude(y => y.WithdrawndHoney).Include(x => x.Package).
                    FirstOrDefault(y => y.Id == productId);
                if (product != null)
                {
                    double qntDerivativeRq = product.Package.VolumeMl * quantity;
                    WithdrawndHoney withdrawndHoney = _context.WithdrawndsHoney.OrderBy(x => x.WithdrawDate).
                        LastOrDefault(z => product.Derivative.WithdrawndHoney.SpecieId == z.SpecieId);
                    if (withdrawndHoney.HoneyRemainingMl >= qntDerivativeRq &&
                        product.Package.Quantity >= quantity)
                    {
                        withdrawndHoney.HoneyRemainingMl -= qntDerivativeRq;
                        product.Package.Quantity -= quantity;
                        _context.SaveChanges();
                        return product;
                    }
                }
            }
            return null;
        }
        //Calculo preço do produto
        public double CalcPrice(Derivative derivative, Package package)
        {
            return derivative.Price.ProfitPercentage * derivative.Price.Unit  * package.VolumeMl +
                package.Price.ProfitPercentage * package.Price.Unit;
        }
        public void Insert(string name, int derivativeId, int packageId)
        {
            Derivative derivative = _context.Derivatives.Include(z => z.Price).FirstOrDefault(x => x.Id == derivativeId);
            Package package = _context.Packages.Include(z => z.Price).FirstOrDefault(x => x.Id == packageId);
            if (derivative != null && package != null)
            {
                _context.Products.Add(new Product(
                    string.IsNullOrEmpty(name) ? derivative.Name + " in " + package.Name : name, derivative, package,
                    CalcPrice(derivative, package)));
                _context.SaveChanges();
            }
        }
        public void Remove(int[] productId)
        {
            if (productId.Any())
            {
                foreach (var item in _context.BeeSpecies.Where(y => productId.Contains(y.Id)))
                {
                    item.Status = false;
                }
                _context.SaveChanges();
            }
        }
        public void Update(int productId, string name)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                if (name.Any())
                {
                    product.Name = name;
                }
            }
            _context.SaveChanges();
        }
        public List<Product> Search(int[] ids, int[] derivativeId, int[] packageId, bool? status)
        {

            if (derivativeId.Any())
            {

                return _context.Products.Include(x => x.Derivative).Where(x => derivativeId.Contains(x.DerivativeId)).ToList();
            }
            else if (packageId.Any())
            {
                return _context.Products.Include(x => x.Package).Where(x => packageId.Contains(x.PackageId)).ToList();
            }
            else if (ids.Any())
            {
                return _context.Products.Where(x => ids.Contains(x.Id)).ToList();
            }
            if (status != null)
            {
                return _context.Products.Where(item => item.Status == status).ToList();
            }
            return null;
        }
        public List<Product> Search()
        {
            return _context.Products.ToList();
        }
    }
}
