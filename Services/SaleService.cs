using HiveManager.Data;
using HiveManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Services
{
    public class SaleService : Sale
    {
        public readonly HiveContext _context;
        public readonly ProductService _productService;

        public SaleService(HiveContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public void Insert(int productId, int quantity, string cpf)
        {
            if (productId > 0 && quantity > 0 && string.IsNullOrEmpty(cpf) == false)
            {
                if (cpf.Length == 11)
                {
                    Product product = _productService.Buy(productId, quantity);
                    if (product != null)
                    {
                        Sale sale = _context.Sales.Include(z => z.Product).Where(x => x.ProductId == productId).
                            OrderBy(x => x.Date).Last();
                        if (sale != null)
                        {
                            double totalPrice = sale.TotalPrice + sale.Product.Price * quantity;
                            _context.Sales.Add(new Sale(quantity, quantity + sale.Total, totalPrice, cpf, sale.Product));
                            _context.SaveChanges();
                        }
                        else
                        {
                            _context.Sales.Add(new Sale(quantity, quantity, product.Price * quantity, cpf, product));
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
        public List<Sale> Search(int[] ids, int[] productId)
        {
            if (ids.Any())
            {
                return _context.Sales.Where(x => ids.Contains(x.Id)).ToList();
            }
            if (productId.Any())
            {
                return _context.Sales.Include(y => y.Product).Where(x => productId.Contains(x.ProductId)).ToList();
            }
            return _context.Sales.ToList();
        }
        public List<Sale> Search()
        {
            return _context.Sales.ToList();
        }

    }
}
