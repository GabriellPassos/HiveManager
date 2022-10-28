using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class ProductController : Controller
    {
        public readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public void Post(string name, int derivativeId, int packageId)
        {
            _productService.Insert(name, derivativeId, packageId);
        }
        [HttpDelete]
        public void Delete(int[] ids)
        {
            _productService.Remove(ids);
        }
        [HttpPost("edit")]
        public void Edit(int productId, string name)
        {
            _productService.Update(productId, name);
        }
        [HttpGet]
        public ActionResult Get(int[] ids, int[] derivativeId, int[] packageId, bool? status)
        {
            return Ok(_productService.Search(ids, derivativeId, packageId, status));
        }
    }
}
