using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int Total { get; set; } // <----- PERGUNTAR SOBRE
        public int RequestQuantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Cpf { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Sale()
        {
        }
        public Sale(int quantity, int totalQuantity, double totalPrice, string cpf, Product product)
        {
            RequestQuantity = quantity;
            Total = totalQuantity;
            TotalPrice = totalPrice;
            Date = DateTime.UtcNow;
            Product = product;
            Cpf = cpf;
        }

    }
}
