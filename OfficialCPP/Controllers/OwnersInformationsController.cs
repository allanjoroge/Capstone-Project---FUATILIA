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
    public class OwnersInformationsController : Controller
    {
        private ValetLogEntities1 db = new ValetLogEntities1();

        // GET: OwnersInformations
        public ActionResult Index(string query)
        {
            var ownersInformations = db.OwnersInformations.Include(o => o.Driver).Where(y=>y.exp==null);
            if (!string.IsNullOrWhiteSpace(query))
            {
                if(int.TryParse(query, out var ticketnumberdontblow))
                {
                    ownersInformations = ownersInformations.Where(a => a.TicketNumber == ticketnumberdontblow);
                }
                else
                {
                    ownersInformations = ownersInformations.Where(a => a.LastName.StartsWith(query) || a.Make.StartsWith(query));
                }
            }
            return View(ownersInformations.OrderByDescending(a => a.TicketNumber).ToList());
        }

        public ActionResult Gone(string query)
        {
            var ownersInformations = db.OwnersInformations.Include(o => o.Driver).Where(y => y.exp != null);
            if (!string.IsNullOrWhiteSpace(query))
            {
                if (int.TryParse(query, out var ticketnumberdontblow))
                {
                    ownersInformations = ownersInformations.Where(a => a.TicketNumber == ticketnumberdontblow);
                }
                else
                {
                    ownersInformations = ownersInformations.Where(a => a.LastName.StartsWith(query) || a.Make.StartsWith(query));
                }
            }
            return View(ownersInformations.OrderByDescending(a => a.TicketNumber).ToList());
        }



        // GET: OwnersInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnersInformation ownersInformation = db.OwnersInformations.Find(id);
            if (ownersInformation == null)
            {
                return HttpNotFound();
            }
            return View(ownersInformation);
        }

        // GET: OwnersInformations/Create
        public ActionResult Create()
        {
            ViewBag.ParkedBy = new SelectList(db.Drivers, "DriversId", "DriversId");
            return View();
        }

        // POST: OwnersInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketNumber,FirstName,LastName,Make,Model,Color,LicensePlate,ParkedLocation,ParkedBy,exp")] OwnersInformation ownersInformation)
        {
            if (ModelState.IsValid)
            {
                ownersInformation.Arrival = DateTime.Now;
                db.OwnersInformations.Add(ownersInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParkedBy = new SelectList(db.Drivers, "DriversId", "Firstname", ownersInformation.ParkedBy);
            return View(ownersInformation);
        }

        // GET: OwnersInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnersInformation ownersInformation = db.OwnersInformations.Find(id);
            if (ownersInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParkedBy = new SelectList(db.Drivers, "DriversId", "Firstname", ownersInformation.ParkedBy);
            return View(ownersInformation);
        }

        // POST: OwnersInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketNumber,FirstName,LastName,Make,Model,Color,LicensePlate,ParkedLocation,ParkedBy,exp")] OwnersInformation ownersInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownersInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParkedBy = new SelectList(db.Drivers, "DriversId", "Firstname", ownersInformation.ParkedBy);
            return View(ownersInformation);
        }

        // GET: OwnersInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnersInformation ownersInformation = db.OwnersInformations.Find(id);
            if (ownersInformation == null)
            {
                return HttpNotFound();
            }
            return View(ownersInformation);
        }

        // POST: OwnersInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnersInformation ownersInformation = db.OwnersInformations.Find(id);
            db.OwnersInformations.Remove(ownersInformation);
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
