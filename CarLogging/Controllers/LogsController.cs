using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarLogging.Logic;
using CarLogging.Models;

namespace CarLogging.Controllers
{
    public class LogsController : Controller
    {
        private CarLogEntities db = new CarLogEntities();

        // GET: Logs
   
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var entries = from e in db.Logs
                select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                entries = entries.Where(e => e.Last_Name.Contains(searchString)
                                               || e.First_Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    entries = entries.OrderByDescending(s => s.Last_Name);
                    break;
                case "Date":
                    entries = entries.OrderBy(s => s.Fill_Up_DateTime);
                    break;
                case "date_desc":
                    entries = entries.OrderByDescending(s => s.Fill_Up_DateTime);
                    break;
                default:
                   entries = entries.OrderBy(s => s.Last_Name);
                    break;
            }
            return View(entries.ToList());
        }

        public ActionResult Calculate(LogViewModel searchModel)
        {
            var log = new LogLogic();
            var model = log.GetLog(searchModel);
            return View(model);
        }

        //public ActionResult Calculate(string search, DateTime? startdate, DateTime? enddate)
        //{
        //    ViewBag.Datetime = DateTime.UtcNow;
        //    ViewBag.startdate = startdate;
        //    ViewBag.enddate = enddate;

        //    IQueryable<Log> logs = db.Logs;

        //    if (search != null)
        //    {
        //        logs = logs.Where(x => x.Last_Name.Contains(search) || x.First_Name.Contains(search));
        //    }
        //    if (startdate.HasValue)
        //    {
        //        logs = logs.Where(x => x.Fill_Up_DateTime > startdate.Value);
        //    }
        //    if (enddate.HasValue)
        //    {
        //        logs = logs.Where(x => x.Fill_Up_DateTime< enddate.Value);
        //    }
        //    // At this point the query has generated a SQL statement based on the conditions above,
        //    // but it will not be executed until the until the next line - i.e. when calling .ToList()
        //    return View(logs.ToList());
        //}

        // GET: Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,First_Name,Last_Name,Car_Model,Miles_Driven,Gallons_Filled,Fill_Up_DateTime")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        // GET: Logs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,First_Name,Last_Name,Car_Model,Miles_Driven,Gallons_Filled,Fill_Up_DateTime")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(log);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
