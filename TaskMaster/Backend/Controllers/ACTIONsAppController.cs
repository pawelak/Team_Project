using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Backend.Models;

namespace Backend.Controllers
{
    public class ACTIONsAppController : ApiController
    {
        private CONNECTION db = new CONNECTION();

        // GET: api/ACTIONsApp
        public IQueryable<ACTION> GetACTION()
        {
            return db.ACTION;
        }

        // GET: api/ACTIONsApp/5
        [ResponseType(typeof(ACTION))]
        public IHttpActionResult GetACTION(int id)
        {
            ACTION aCTION = db.ACTION.Find(id);
            if (aCTION == null)
            {
                return NotFound();
            }

            return Ok(aCTION);
        }

        // PUT: api/ACTIONsApp/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutACTION(int id, ACTION aCTION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aCTION.ID)
            {
                return BadRequest();
            }

            db.Entry(aCTION).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ACTIONExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ACTIONsApp
        [ResponseType(typeof(ACTION))]
        public IHttpActionResult PostACTION(ACTION aCTION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ACTION.Add(aCTION);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aCTION.ID }, aCTION);
        }

        // DELETE: api/ACTIONsApp/5
        [ResponseType(typeof(ACTION))]
        public IHttpActionResult DeleteACTION(int id)
        {
            ACTION aCTION = db.ACTION.Find(id);
            if (aCTION == null)
            {
                return NotFound();
            }

            db.ACTION.Remove(aCTION);
            db.SaveChanges();

            return Ok(aCTION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACTIONExists(int id)
        {
            return db.ACTION.Count(e => e.ID == id) > 0;
        }
    }
}