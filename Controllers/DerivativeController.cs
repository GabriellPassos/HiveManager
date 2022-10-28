using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class DerivativeController : Controller
    {
        public readonly DerivativeService _derivativeService;
        public DerivativeController(DerivativeService derivativeService)
        {
            _derivativeService = derivativeService;
        }
        [HttpPost]
        public void Post(string name, int priceId, int specieId)
        {
            _derivativeService.Insert(name, priceId, specieId);
        }
        [HttpDelete]
        public void Delete([FromQuery] int[] ids)
        {
            _derivativeService.Remove(ids);
        }
        [HttpPost("edit")]
        public void Edit([FromQuery] int derivativeId, int priceId, string name, double quantityMl, bool? status)
        {
            _derivativeService.Update(derivativeId, name, status, priceId);
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids, [FromQuery] int[] productId, bool? status)
        {
            return Ok(_derivativeService.Search(ids, productId, status));
        }
    
}
}
