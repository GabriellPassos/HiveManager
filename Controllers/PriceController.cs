using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class PriceController : Controller
    {
        public readonly PriceService _priceService;
        public PriceController(PriceService priceService)
        {
            _priceService = priceService;
        }
        [HttpPost]
        public void Post(double unit, double profitPercentage, string name)
        {
            _priceService.Insert(unit, profitPercentage, name);
        }
        [HttpDelete]
        public void Delete([FromQuery] int[] ids, bool? status)
        {
            _priceService.Remove(ids);
        }
        [HttpPost("edit")]
        public ActionResult Edit([FromQuery] int priceId, string name, int PricePerOne, int profitPercentage, bool? status)
        {
            return Ok(_priceService.Update(priceId, PricePerOne, profitPercentage, name, status));
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids, bool? status)
        {
            return Ok(_priceService.Search(ids, status));
        }
    }
}

