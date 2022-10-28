using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class SaleController : Controller
    {
        public readonly SaleService _saleService;
        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpPost]
        public void Post(int productId, int requestQuantity, string cpf)
        {
            _saleService.Insert(productId, requestQuantity, cpf);
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids, [FromQuery] int[] productsId)
        {
            return Ok(_saleService.Search(ids, productsId));
        }
    }
}
