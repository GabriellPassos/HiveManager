using HiveManager.Interface;
using HiveManager.Models;
using HiveManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace HiveManager.Controllers
{
    public class HomeController : Controller
    {
        public readonly IHiveService _hiveService;
        public readonly ProductService _productService;
        public readonly DerivativeService _derivativeService;
        public readonly PackageService _packageService;
        public readonly SaleService _saleService;
        public readonly WithdrawnHoneyService _withdrawnHoneyService;
        public readonly PriceService _priceSerive;
        public readonly BeeSpecieService _beeSpecieService;

        public HomeController(IHiveService hiveService, ProductService productService,
            DerivativeService derivativeService, PackageService packageService, SaleService saleService,
            WithdrawnHoneyService withdrawnHoneyService, PriceService priceSerive, BeeSpecieService beeSpecieService)
        {
            _hiveService = hiveService;
            _productService = productService;
            _derivativeService = derivativeService;
            _packageService = packageService;
            _saleService = saleService;
            _withdrawnHoneyService = withdrawnHoneyService;
            _priceSerive = priceSerive;
            _beeSpecieService = beeSpecieService;
        }
        public IActionResult Index()
        {
            RefreshInfo();
            return View();
        }
        public void RefreshInterfaceInfo()
        {

        }
        [HttpGet]
        public string Privacy()
        {
            return "tuim";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void RefreshInfo()
        {
            List<Hive> hiveList = _hiveService.Search();
            List<Hive> hve = hiveList.OrderByDescending(x => x.TotalHoney).ToList();

            List<Product> productList = _productService.Search();
            List<Product> pdct = productList.OrderByDescending(x => x.Id).ToList();

            List<Derivative> derivativeList = _derivativeService.Search();
            List<Derivative> dvt = derivativeList.OrderByDescending(x => x.Id).ToList();

            List<Package> packageList = _packageService.Search();
            List<Package> pkg = packageList.OrderByDescending(x => x.Id).ToList();

            List<Sale> saleList = _saleService.Search();
            List<Sale> sle = saleList.OrderBy(x => x.Date).ToList();

            List<Price> priceList = _priceSerive.Search();

            List<BeeSpecie> beeList = _beeSpecieService.Search();
            List<BeeSpecie> bees = beeList.OrderByDescending(x => x.Id).ToList();

            List<WithdrawndHoney> withdrawsList = _withdrawnHoneyService.Search();
            List<WithdrawndHoney> withdraws = withdrawsList.OrderByDescending(x => x.HoneyRemainingMl).ToList();

            string hivesInfo = null;
            string beesInfo = null;
            string pricesInfo = null;
            string packagesInfo = null;
            string derivativeInfo = null;
            string productInfo = null;
            string saleInfo = null;
            string withdrawInfo = null;

            for (int i = 0; i < bees.Count(); i++)
            {
                beesInfo = beesInfo + bees[i].Id.ToString() + ";" + bees[i].Name + ";" + bees[i].Status + ("-");
            }

            for (int i = 0; i < hve.Count(); i++)
            {
                hivesInfo = hivesInfo + hve[i].Id.ToString() + ";" + hve[i].Specie.Name + ";" + hve[i].TotalHoney.ToString("F", CultureInfo.InvariantCulture) + "-";
            }
            

            for (int i = 0; i < priceList.Count(); i++)
            {
                pricesInfo = pricesInfo + priceList[i].Id.ToString() + ";" + priceList[i].Name + ";" + priceList[i].Unit.ToString("F", CultureInfo.InvariantCulture) + ";" + priceList[i].ProfitPercentage.ToString("F", CultureInfo.InvariantCulture) + "-";
            }

            for (int i = 0; i < pkg.Count(); i++)
            {
                packagesInfo = packagesInfo + pkg[i].Id + ";" + pkg[i].Name + ";" + pkg[i].Quantity.ToString() + "-";
            }

            for (int i = 0; i < dvt.Count(); i++)
            {
                derivativeInfo = derivativeInfo + dvt[i].Id + ";" + dvt[i].Name + ";" + dvt[i].WithdrawndHoney.Specie.Name + "-";
            }

            for (int i = 0; i < pdct.Count(); i++)
            {
                productInfo = productInfo + pdct[i].Id + ";" + pdct[i].Name + ";" + pdct[i].Package.Name + ";" + pdct[i].Derivative.Name + ";" + pdct[i].Price.ToString("F", CultureInfo.InvariantCulture) + "-";
            }

            for (int i = 0; i < sle.Count(); i++)
            {
                saleInfo = saleInfo + sle[i].Product.Id + ";" + sle[i].Product.Name +";" + sle[i].RequestQuantity.ToString() + ";" + sle[i].TotalPrice.ToString("F", CultureInfo.InvariantCulture) + "-";
            }
            for (int i = 0; i < withdraws.Count(); i++)
            {
                withdrawInfo = withdrawInfo + withdraws[i].Specie.Name + ";" + withdraws[i].HoneyRemainingMl.ToString("F", CultureInfo.InvariantCulture) + "-";
            }

            TempData["hives"] = hivesInfo;
            TempData["bees"] = beesInfo;
            TempData["prices"] = pricesInfo;
            TempData["packages"] = packagesInfo;
            TempData["derivatives"] = derivativeInfo;
            TempData["products"] = productInfo;
            TempData["sales"] = saleInfo;
            TempData["withdraws"] = withdrawInfo;

        }
    }
}
