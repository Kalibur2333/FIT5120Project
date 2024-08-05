using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5120Project.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using PagedList;
using FIT5120Project.Service;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FIT5120Project.Controllers
{
    public class FabricsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fabrics
        public ActionResult Index(string search, int? page, string sortOrder, string words)
        {
            ViewBag.CurrentFilter = search;
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.ScoreSortParm = sortOrder == "score_asc" ? "score_desc" : "score_asc";

            var fabrics = db.Fabrics.AsQueryable();
            List<string> allFabricNames = db.Fabrics.Select(f => f.FabricName).ToList();

            if (words != null)
            {

                List<string> wordsList = Regex.Matches(words, @"\w+")
                                .Cast<Match>()
                                .Select(m => m.Value)
                                .ToList();
                Debug.WriteLine("New keyword:");
                foreach (string keyword in wordsList)
                {
                    Debug.WriteLine(keyword);
                }
                List<string> wordsListWithVariants = new List<string>();
                foreach (string word in wordsList)
                {
                    wordsListWithVariants.Add(word);
                    wordsListWithVariants.Add(word.ToLower());
                    wordsListWithVariants.Add(word.ToUpper());
                }
                /*
                List<string> newFabrics = allFabricNames
                     .Where(fabricName => wordsListWithVariants.Any(word => fabricName.ToLower().Contains(word.ToLower())))
                    .ToList();*/
                List<string> newFabrics = new List<string>();
                foreach (string thefabric in allFabricNames)
                {
                    Debug.WriteLine("searchword" + words);
                    Debug.WriteLine("searchfabric" + thefabric);
                    int index = words.ToLower().IndexOf(thefabric.ToLower());
                    Debug.WriteLine("searchfabricindex" + index);
                    if (index != -1)
                    {
                        newFabrics.Add(thefabric);
                    }
                }

                Debug.WriteLine("New Fabrics:" + newFabrics.Count);
                Debug.WriteLine(newFabrics.Count);
                foreach (string fabric in newFabrics)
                {
                    Debug.WriteLine(fabric);
                }
                if (newFabrics.Any())
                {
                    fabrics = fabrics.Where(fabric => newFabrics.Contains(fabric.FabricName.ToLower()));
                }
                if (newFabrics.Count == 0)
                {
                    fabrics = fabrics.Where(b => b.FabricName.ToLower().Contains("nocontent"));
                }
            }
            if (!String.IsNullOrEmpty(search))
            {
                fabrics = fabrics.Where(b => b.FabricName.ToLower().Contains(search.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_asc":
                    fabrics = fabrics.OrderBy(b => b.FabricName);
                    break;
                case "name_desc":
                    fabrics = fabrics.OrderByDescending(b => b.FabricName);
                    break;
                case "score_asc":
                    fabrics = fabrics.OrderBy(b => b.Score);
                    break;
                case "score_desc":
                    fabrics = fabrics.OrderByDescending(b => b.Score);
                    break;
                default:
                    // Default sort
                    fabrics = fabrics.OrderByDescending(b => b.Score);
                    break;
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            return View(fabrics.ToPagedList(pageNumber, pageSize));
        }

        // GET: Fabrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = db.Fabrics.Find(id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // GET: Fabrics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fabrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FabricId,FabricName,GreenHouse,Pollutants,WaterConsum,EnergyConsum,WasteConsum,SalesReve,Score")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                db.Fabrics.Add(fabric);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fabric);
        }

        // GET: Fabrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = db.Fabrics.Find(id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // POST: Fabrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FabricId,FabricName,GreenHouse,Pollutants,WaterConsum,EnergyConsum,WasteConsum,SalesReve,Score")] Fabric fabric)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fabric).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabric);
        }

        // GET: Fabrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabric fabric = db.Fabrics.Find(id);
            if (fabric == null)
            {
                return HttpNotFound();
            }
            return View(fabric);
        }

        // POST: Fabrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fabric fabric = db.Fabrics.Find(id);
            db.Fabrics.Remove(fabric);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
