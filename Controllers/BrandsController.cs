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

namespace FIT5120Project.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Brands
        /*public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }*/




        public ActionResult Index(string search, int? page, string sortOrder)
        {
            // Maintain the current search filter
            ViewBag.CurrentFilter = search;

            // Set up sorting parameters
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.ScoreSortParm = sortOrder == "score_asc" ? "score_desc" : "score_asc";

            var brands = db.Brands.AsQueryable();

            // Apply search filter if there is one
            if (!String.IsNullOrEmpty(search))
            {
                brands = brands.Where(b => b.BrandName.ToLower().Contains(search.ToLower()));
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "name_asc":
                    brands = brands.OrderBy(b => b.BrandName);
                    break;
                case "name_desc":
                    brands = brands.OrderByDescending(b => b.BrandName);
                    break;
                case "score_asc":
                    brands = brands.OrderBy(b => b.Scores);
                    break;
                case "score_desc":
                    brands = brands.OrderByDescending(b => b.Scores);
                    break;
                default:
                    // Default sort
                    brands = brands.OrderByDescending(b => b.Scores);
                    break;
            }

            int pageSize = 10; // Set the number of items per page
            int pageNumber = page ?? 1; // Set the default page number

            // Use ToPagedList to create the page
            return View(brands.ToPagedList(pageNumber, pageSize));
        }




        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            var brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }

            BrandService brandService = new BrandService(db);
            double percentile = brandService.CalculatePercentileForScore(brand.Scores);

            ViewBag.Percentile = percentile;

            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "brandId,brandName,year,scores,percentage,changeBetween")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // CSV Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCsv(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var csvData = new DataTable();

                using (var reader = new StreamReader(file.InputStream))
                {
                    string firstLine = reader.ReadLine();
                    var headers = firstLine.Split(',');
                    foreach (var header in headers)
                    {
                        csvData.Columns.Add(header.Trim()); // Trim header to remove potential whitespace
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        csvData.Rows.Add(values);
                    }
                }

                InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
            }

            return RedirectToAction("Create");
        }
        private void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            var connectionString = @"Server=tcp:fit5120projectdbserver.database.windows.net,1433;Initial Catalog=wardrobeforearthdb;Persist Security Info=False;User ID=tp17;Password=FIT5120tp;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; // Update your connection string accordingly
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Brands"; // Update the destination table name
                    // Update column mappings to match your table schema
                    s.ColumnMappings.Add("BrandName", "BrandName");
                    s.ColumnMappings.Add("Year", "Year");
                    s.ColumnMappings.Add("Scores", "Scores");
                    s.ColumnMappings.Add("Percentage", "Percentage");
                    s.ColumnMappings.Add("ChangeBetween", "ChangeBetween");

                    s.WriteToServer(csvFileData);
                }
            }
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "brandId,brandName,year,scores,percentage,changeBetween")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
