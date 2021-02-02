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
    public class EMItypeMastersController : ApiController
    {
        private financedbEntities6 db = new financedbEntities6();

        // GET: api/EMItypeMasters
        public IHttpActionResult GetEMItypeMasters()
        {
            return Ok(db.EMItypeMasters);
        }

        // GET: api/EMItypeMasters/5
        [ResponseType(typeof(EMItypeMaster))]
        public IHttpActionResult GetEMItypeMaster(int id)
        {
            EMItypeMaster eMItypeMaster = db.EMItypeMasters.Find(id);
            if (eMItypeMaster == null)
            {
                return NotFound();
            }

            return Ok(eMItypeMaster);
        }

        // PUT: api/EMItypeMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMItypeMaster(int id, EMItypeMaster eMItypeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMItypeMaster.EMItype_id)
            {
                return BadRequest();
            }

            db.Entry(eMItypeMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMItypeMasterExists(id))
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

        // POST: api/EMItypeMasters
        [ResponseType(typeof(EMItypeMaster))]
        public IHttpActionResult PostEMItypeMaster(EMItypeMaster eMItypeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMItypeMasters.Add(eMItypeMaster);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EMItypeMasterExists(eMItypeMaster.EMItype_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eMItypeMaster.EMItype_id }, eMItypeMaster);
        }

        // DELETE: api/EMItypeMasters/5
        [ResponseType(typeof(EMItypeMaster))]
        public IHttpActionResult DeleteEMItypeMaster(int id)
        {
            EMItypeMaster eMItypeMaster = db.EMItypeMasters.Find(id);
            if (eMItypeMaster == null)
            {
                return NotFound();
            }

            db.EMItypeMasters.Remove(eMItypeMaster);
            db.SaveChanges();

            return Ok(eMItypeMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMItypeMasterExists(int id)
        {
            return db.EMItypeMasters.Count(e => e.EMItype_id == id) > 0;
        }
    }
}