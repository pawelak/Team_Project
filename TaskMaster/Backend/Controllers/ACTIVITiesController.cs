using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Backend.Models;

namespace Backend.Controllers
{
    public class ACTIVITiesController : Controller
    {
        private CONNECTION db = new CONNECTION();

        // GET: ACTIVITies
        public ActionResult Index()
        {
            return View(db.ACTIVITY.ToList());
        }

        // GET: ACTIVITies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTIVITY aCTIVITY = db.ACTIVITY.Find(id);
            if (aCTIVITY == null)
            {
                return HttpNotFound();
            }
            return View(aCTIVITY);
        }

        // GET: ACTIVITies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACTIVITies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,START,END")] ACTIVITY aCTIVITY)
        {
            if (ModelState.IsValid)
            {
                db.ACTIVITY.Add(aCTIVITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCTIVITY);
        }

        // GET: ACTIVITies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTIVITY aCTIVITY = db.ACTIVITY.Find(id);
            if (aCTIVITY == null)
            {
                return HttpNotFound();
            }
            return View(aCTIVITY);
        }

        // POST: ACTIVITies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,START,END")] ACTIVITY aCTIVITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCTIVITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCTIVITY);
        }

        // GET: ACTIVITies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTIVITY aCTIVITY = db.ACTIVITY.Find(id);
            if (aCTIVITY == null)
            {
                return HttpNotFound();
            }
            return View(aCTIVITY);
        }

        // POST: ACTIVITies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACTIVITY aCTIVITY = db.ACTIVITY.Find(id);
            db.ACTIVITY.Remove(aCTIVITY);
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
