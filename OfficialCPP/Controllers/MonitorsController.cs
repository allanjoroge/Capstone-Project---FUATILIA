using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficialCPP.Models;

namespace OfficialCPP.Controllers
{
   [Authorize]
    public class MonitorsController : Controller
    {
        private ValetLogEntities1 db = new ValetLogEntities1();

        // GET: Monitors
        public ActionResult Index()
        {
            var monitors = db.Activities.Include(m => m.OwnersInformation).OrderByDescending(a=>a.TimeRequested);
            return View(monitors.ToList());
        }

        // GET: Monitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity monitor = db.Activities.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            return View(monitor);
        }

        // GET: Monitors/Create
        public ActionResult Create()
        {
            ViewBag.TicketNumber = new SelectList(db.OwnersInformations, "TicketNumber", "TicketNumber");
            return View();
        }

        // POST: Monitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Activity monitor)
        {
            if (ModelState.IsValid)
            {

                var ticketnum = db.OwnersInformations.Find(monitor.TicketNumber);
                var missingticket = false;
                if (ticketnum == null)
                {
                    ModelState.AddModelError("TicketNumber", "THIS TICKET # DOESN'T EXIST..");
                    missingticket = true;
                }
                if (!missingticket)
                {
                    monitor.TimeRequested = DateTime.Now;
                    db.Activities.Add(monitor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.TicketNumber = new SelectList(db.OwnersInformations, "TicketNumber", "FirstName", monitor.TicketNumber);
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity monitor = db.Activities.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketNumber = new SelectList(db.OwnersInformations, "TicketNumber", "FirstName", monitor.TicketNumber);
            return View(monitor);

        }

        // POST: Monitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity monitor)
        {
            if (ModelState.IsValid)
            {
                var driver = db.Drivers.Find(monitor.Deliverer);
                var parker = db.Drivers.Find(monitor.Parkedby);
                var missingdrivers = false;
                if (driver == null)
                {
                    ModelState.AddModelError("Deliverer", "This Driver hasn't clocked in..");
                    missingdrivers = true;
                }
                if (parker == null)
                {
                    ModelState.AddModelError("Parkedby", "This Driver hasn't clocked in..");
                    missingdrivers = true;
                }
                if (!missingdrivers)
                {
                }

                db.Entry(monitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketNumber = new SelectList(db.OwnersInformations, "TicketNumber", "FirstName", monitor.TicketNumber);
            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity monitor = db.Activities.Find(id);
            if (monitor == null)
            {
                return HttpNotFound();
            }
            return View(monitor);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity monitor = db.Activities.Find(id);
            db.Activities.Remove(monitor);
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
