using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class BeeSpecieController : Controller
    {
        public readonly BeeSpecieService _beeSpecieService;
        public BeeSpecieController(BeeSpecieService beeSpecieService)
        {
            _beeSpecieService = beeSpecieService;
        }
        [HttpPost]
        public void Post(string name, double polemPerHour)
        {
            _beeSpecieService.Insert(name, polemPerHour);
        }
        [HttpDelete]
        public void Delete([FromQuery] int[] ids)
        {
            _beeSpecieService.Remove(ids);
            
        }
        [HttpPost("edit")]
        public void Edit(int specieId, double polemPerHour)
        {
            _beeSpecieService.Update(specieId, polemPerHour);
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids,  string specieName, bool? status)
        {
            return Ok(_beeSpecieService.Search(ids, specieName, status));
        }
    }
}
