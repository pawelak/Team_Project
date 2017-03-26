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
    public class EVENTsController : Controller
    {
        private CONNECTION db = new CONNECTION();

        // GET: EVENTs
        public ActionResult Index()
        {
            return View(db.EVENT.ToList());
        }

        // GET: EVENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // GET: EVENTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EVENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.EVENT.Add(eVENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eVENT);
        }

        // GET: EVENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: EVENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID")] EVENT eVENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eVENT);
        }

        // GET: EVENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENT eVENT = db.EVENT.Find(id);
            if (eVENT == null)
            {
                return HttpNotFound();
            }
            return View(eVENT);
        }

        // POST: EVENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENT eVENT = db.EVENT.Find(id);
            db.EVENT.Remove(eVENT);
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
