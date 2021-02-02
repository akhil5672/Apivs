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
using finance_trial4.Models;

namespace finance_trial4.Controllers
{
    public class productsMastersController : ApiController
    {
        private financedbEntities6 db = new financedbEntities6();

        // GET: api/productsMasters
        public IHttpActionResult  GetproductsMasters()
        {
            return Ok(db.productsMasters);
        }

        // GET: api/productsMasters/5
        [ResponseType(typeof(productsMaster))]
        public IHttpActionResult GetproductsMaster(int id)
        {
            productsMaster productsMaster = db.productsMasters.Find(id);
            if (productsMaster == null)
            {
                return NotFound();
            }

            return Ok(productsMaster);
        }

        // PUT: api/productsMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutproductsMaster(int id, productsMaster productsMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsMaster.product_id)
            {
                return BadRequest();
            }

            db.Entry(productsMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productsMasterExists(id))
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

        // POST: api/productsMasters
        [ResponseType(typeof(productsMaster))]
        public IHttpActionResult PostproductsMaster(productsMaster productsMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.productsMasters.Add(productsMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productsMaster.product_id }, productsMaster);
        }

        // DELETE: api/productsMasters/5
        [ResponseType(typeof(productsMaster))]
        public IHttpActionResult DeleteproductsMaster(int id)
        {
            productsMaster productsMaster = db.productsMasters.Find(id);
            if (productsMaster == null)
            {
                return NotFound();
            }

            db.productsMasters.Remove(productsMaster);
            db.SaveChanges();

            return Ok(productsMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productsMasterExists(int id)
        {
            return db.productsMasters.Count(e => e.product_id == id) > 0;
        }
    }
}