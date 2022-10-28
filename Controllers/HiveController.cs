using HiveManager.Interface;
using HiveManager.Models;
using HiveManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HiveManager.Controllers
{
    public class HiveController : Controller
    {
        public readonly IHiveService _hiveService;
        public HiveController(IHiveService hiveService)
        {
            _hiveService = hiveService;
        }
        public IActionResult hive()
        {
            return View();
        }
        [HttpPost]
        public void Post(int qntBeeIn, int specieId)
        {
            _hiveService.Insert(qntBeeIn, specieId);

        }
        [HttpDelete]
        public void Delete([FromQuery] int[] ids)
        {
            _hiveService.Remove(ids);
        }
        [HttpPost("edit")]
        public void Edit([FromQuery] int hiveId, int qntBeeIn, int specieId)
        {
            _hiveService.Update(hiveId, qntBeeIn, specieId);
        }
        /*[HttpGet]
        public List<Hive> Get(int[] ids, bool? status, int specieId, string specieName = null)
        {
            List<Hive> hives = _hiveService.Search(ids, status, specieId, specieName);
            return hives;
        }*/
        [HttpGet]
        public List<Hive> Get()
        {
            List<Hive> hives = _hiveService.Search();
            return hives;
        }
    }
}
