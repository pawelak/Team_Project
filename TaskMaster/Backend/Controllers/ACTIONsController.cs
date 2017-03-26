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
    public class ACTIONsController : Controller
    {
        private CONNECTION db = new CONNECTION();

        // GET: ACTIONs
        public ActionResult Index()
        {
            return View(db.ACTION.ToList());
        }

        // GET: ACTIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTION aCTION = db.ACTION.Find(id);
            if (aCTION == null)
            {
                return HttpNotFound();
            }
            return View(aCTION);
        }

        // GET: ACTIONs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACTIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,DESCRIPTION")] ACTION aCTION)
        {
            if (ModelState.IsValid)
            {
                db.ACTION.Add(aCTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCTION);
        }

        // GET: ACTIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTION aCTION = db.ACTION.Find(id);
            if (aCTION == null)
            {
                return HttpNotFound();
            }
            return View(aCTION);
        }

        // POST: ACTIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,DESCRIPTION")] ACTION aCTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCTION);
        }

        // GET: ACTIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACTION aCTION = db.ACTION.Find(id);
            if (aCTION == null)
            {
                return HttpNotFound();
            }
            return View(aCTION);
        }

        // POST: ACTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACTION aCTION = db.ACTION.Find(id);
            db.ACTION.Remove(aCTION);
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
