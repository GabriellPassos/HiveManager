using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class PackageController : Controller
    {
        public readonly PackageService _packageService;
        public PackageController(PackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpPost]
        public void Post(string name, int quantity, double volumeMl, int priceId)
        {
            _packageService.Insert(name, quantity, volumeMl, priceId);
        }
        [HttpDelete]
        public void Delete([FromQuery] int[] ids)
        {
            _packageService.Remove(ids);
        }
        [HttpPost("edit")]
        public void Edit([FromQuery] int packageId, string name, int quantity, double volumeMl, int priceId)
        {
            _packageService.Update(packageId, name, quantity, volumeMl, priceId);
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids, bool? status)
        {
            return Ok(_packageService.Search(ids, status));
        }
        [HttpPost("add")]
        public void Add(int quantity, int packageId)
        {
            _packageService.Add(quantity, packageId);
        }
    }
}

