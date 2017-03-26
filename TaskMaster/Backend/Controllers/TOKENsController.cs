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
    public class TOKENsController : Controller
    {
        private CONNECTION db = new CONNECTION();

        // GET: TOKENs
        public ActionResult Index()
        {
            return View(db.TOKEN.ToList());
        }

        // GET: TOKENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOKEN tOKEN = db.TOKEN.Find(id);
            if (tOKEN == null)
            {
                return HttpNotFound();
            }
            return View(tOKEN);
        }

        // GET: TOKENs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TOKENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PASSWORD")] TOKEN tOKEN)
        {
            if (ModelState.IsValid)
            {
                db.TOKEN.Add(tOKEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tOKEN);
        }

        // GET: TOKENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOKEN tOKEN = db.TOKEN.Find(id);
            if (tOKEN == null)
            {
                return HttpNotFound();
            }
            return View(tOKEN);
        }

        // POST: TOKENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PASSWORD")] TOKEN tOKEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOKEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tOKEN);
        }

        // GET: TOKENs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOKEN tOKEN = db.TOKEN.Find(id);
            if (tOKEN == null)
            {
                return HttpNotFound();
            }
            return View(tOKEN);
        }

        // POST: TOKENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOKEN tOKEN = db.TOKEN.Find(id);
            db.TOKEN.Remove(tOKEN);
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
