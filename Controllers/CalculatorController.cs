using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIT5120Project.Models;

namespace FIT5120Project.Controllers
{
    public class CalculatorController : Controller
    {

        public ActionResult Question1()
        {
            return View();
        }

        //Questions 1-6
        [HttpPost]
        public ActionResult Question1(CalculatorViewModels model)
        {
            TempData["Question1Data"] = model.ShopQuantityMonth;
            return RedirectToAction("Question2");
        }
        public ActionResult Question2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question2(CalculatorViewModels model)
        {
            TempData["Question2Data"] = model.OnlineQuantity;
            return RedirectToAction("Question3");
        }

        public ActionResult Question3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question3(CalculatorViewModels model)
        {
            TempData["Question3Data"] = model.ReturnPercOnline;
            return RedirectToAction("Question4");
        }

        public ActionResult Question4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question4(CalculatorViewModels model)
        {
            TempData["Question4Data"] = model.InstoreQuantity;
            return RedirectToAction("Question5");
        }

        public ActionResult Question5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question5(CalculatorViewModels model)
        {
            TempData["Question5Data"] = model.ReturnPercInstore;
            return RedirectToAction("Question6");
        }

        public ActionResult Question6()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Question6(CalculatorViewModels model)
        {
            TempData["Question6Data"] = model.LandfillPercentage;          
            return RedirectToAction("Result");
        }

        //Results

        [HttpPost]
        public ActionResult CalculateCarbonFootprint(int shopQuantityMonth, int onlineQuantity, int returnPercOnline, int instoreQuantity, int returnPercInstore, int landfillPerc, int donatePerc)
        {
            // Calculate CO2 emissions from clothing shopping
            double avgCo2 = 20.6;
            double co2Month = shopQuantityMonth * avgCo2;

            // Calculate CO2 emissions from online shopping
            double onlineCo2 = 0.44 * onlineQuantity;
            co2Month += onlineCo2;

            // Calculate CO2 emissions from returning online purchases
            int onlineReturned = (int)Math.Round(onlineQuantity * returnPercOnline / 100.0);
            double onlineReturnedCo2 = 0.29 * onlineReturned;
            double onlineReturnedItemCo2 = 6.18 * onlineReturned;
            double onlineTotalReturnCo2 = onlineReturnedCo2 + onlineReturnedItemCo2;
            co2Month += onlineTotalReturnCo2;

            // Calculate CO2 emissions from in-store shopping
            double instoreCo2 = 1.34 * instoreQuantity;
            co2Month += instoreCo2;

            // Calculate CO2 emissions from returning in-store purchases
            int instoreReturned = (int)Math.Round(instoreQuantity * returnPercInstore / 100.0);
            double instoreReturnedCo2 = 1.24 * instoreReturned;
            double instoreReturnedItemCo2 = 6.18 * instoreReturned;
            double instoreTotalReturnCo2 = instoreReturnedCo2 + instoreReturnedItemCo2;
            co2Month += instoreTotalReturnCo2;

            // Calculate CO2 emissions from disposal
            double landfillCo2 = 1.78 * landfillPerc / 100.0;
            co2Month += landfillCo2;
            donatePerc = 100 - landfillPerc;
            double donateCo2 = 19.7 * donatePerc / 100.0;
            co2Month -= donateCo2;

            // Calculate total CO2 emissions annually
            double co2Total = co2Month * 12;

            // Calculate other values
            double melbSyd = 193; // Amount of kg of CO2 to drive from Melbourne to Sydney
            double drivingQuantity = co2Total / melbSyd;

            double melbSydKm = 867; // km from Melbourne to Sydney
            double moonKm = 382500; // km from Earth to moon
            double co2Km = 0.222; // Average CO2 per km
            double co220Years = co2Total * 20;
            double co2Moon = moonKm * co2Km;
            double timesToMoon = co220Years / co2Moon;

            // Display results
            ViewBag.Co2Total = co2Total;
            ViewBag.DrivingQuantity = drivingQuantity;
            ViewBag.TimesToMoon = timesToMoon;

            return View("Results");
        }
        /*
        // GET: Calculator
        public ActionResult Index()
        {
            return View();
        }

        // GET: Calculator/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calculator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculator/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Calculator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calculator/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Calculator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calculator/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
