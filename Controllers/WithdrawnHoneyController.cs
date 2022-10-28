using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class WithdrawnHoneyController : Controller
    {
        public readonly WithdrawnHoneyService _withdrawnHoneyService;
        public WithdrawnHoneyController(WithdrawnHoneyService withdrawnHoneyService)
        {
            _withdrawnHoneyService = withdrawnHoneyService;
        }
        [HttpPost]
        public void Post(double quantity, int specieId)
        {
            _withdrawnHoneyService.Insert(quantity, specieId);
        }
        /*[HttpDelete]
        public void Delete([FromQuery] int[] ids)
        {
            _withdrawnHoneyService.Remove(ids);
        }*/
        [HttpGet]
        public ActionResult Get([FromQuery] int[] ids)
        {
            return Ok(_withdrawnHoneyService.Search(ids));
        }
    }
}
