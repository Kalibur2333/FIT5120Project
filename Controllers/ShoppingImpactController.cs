using FIT5120Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5120Project.Controllers
{
    public class ShoppingImpactController : Controller
    {
        // GET: ShoppingImpactController
        public ActionResult Index()
        {
            return View(new ShoppingImpactViewModel());
        }

        [HttpPost]
        public ActionResult CalculateImpact(ShoppingImpactViewModel model)
        {
            double avgCo2 = 20.6;
            double onlineCo2 = 0.44;
            double instoreCo2 = 1.34;
            double landfillCo2 = 1.78;
            double donateCo2 = 19.7;

            double co2Month = model.ShopQuantityMonth * avgCo2;
            co2Month += model.OnlineQuantity * onlineCo2;

            int onlineReturned = (int)Math.Round(model.OnlineQuantity * model.ReturnPercOnline * 0.01);
            double onlineReturnedCo2 = 0.29 * onlineReturned;
            double onlineReturnedItemCo2 = 6.18 * onlineReturned;
            co2Month += onlineReturnedCo2 + onlineReturnedItemCo2;

            co2Month += model.InstoreQuantity * instoreCo2;

            int instoreReturned = (int)Math.Round(model.InstoreQuantity * model.ReturnPercInstore * 0.01);
            double instoreReturnedCo2 = 1.24 * instoreReturned;
            double instoreReturnedItemCo2 = 6.18 * instoreReturned;
            co2Month += instoreReturnedCo2 + instoreReturnedItemCo2;

            double landfillEmission = landfillCo2 * model.LandfillPerc * 0.01;
            co2Month += landfillEmission;

            double donateEmission = donateCo2 * (1 - model.LandfillPerc * 0.01);
            co2Month -= donateEmission;

            model.Co2Total = co2Month * 12;

            const int melbSydCo2 = 193;
            model.DrivingQuantity = (int)Math.Floor(model.Co2Total / melbSydCo2);

            const double co2PerKm = 0.222;
            const int moonKm = 382500;
            double co2Moon = moonKm * co2PerKm;
            double co2_20yrs = model.Co2Total * 50;
            model.TimesToMoon = (int)Math.Floor(co2_20yrs / co2Moon);

            return View("Results", model);
        }
    }
}