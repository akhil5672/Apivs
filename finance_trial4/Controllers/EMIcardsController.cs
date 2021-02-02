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

    public class EMIcardsController : ApiController
    {
        private financedbEntities6 db = new financedbEntities6();
        Customer tempcustomer = new Customer();
       // [Route("api/EMIinsert")]
        public IHttpActionResult PostEMIinsertion(Customer tempcustomer)
        {
            
            EMIcard temp = db.EMIcards.Where(x => x.customer_id == tempcustomer.customer_id).FirstOrDefault();
            if (temp != null)
            {
                return Ok(0);
            }
            else
            {
                System.DateTime date = DateTime.Now;
                EMIcard emicard = new EMIcard();
                var tempCustomerDetails = db.Customers.ToList();
                CustomerCard a = new CustomerCard();
                a = (from ab in tempCustomerDetails
                     join ct in db.CardTypeMasters on ab.cardtype_id equals ct.EMIcardtype_id
                     select new CustomerCard
                     {
                         customer_id = ab.customer_id,
                         total_limit = ct.total_limit,
                         EMIcard_validity = ct.EMIcard_validity
                     }
                       ).Where(x => x.customer_id == tempcustomer.customer_id).SingleOrDefault();
                emicard.customer_id = a.customer_id;
                emicard.EMIcard_expiry = date.AddYears(a.EMIcard_validity);
                emicard.used_credit = 0;
                emicard.remaining_credit = a.total_limit;
                //int num = random.Next(1000);
                //Math.Floor((Math.random() * 100) + 1);

                Random random = new Random();
                emicard.EMIcard_number = random.Next(1000000000, int.MaxValue);
                db.EMIcards.Add(emicard);
                db.SaveChanges();

            }
            return Ok(1);
        }
        // GET: api/EMIcards
        public IQueryable<EMIcard> GetEMIcards()
        {
            return db.EMIcards;
        }

        // GET: api/EMIcards/5
        [ResponseType(typeof(EMIcard))]
        public IHttpActionResult GetEMIcard(int id)
        {
            EMIcard eMIcard = db.EMIcards.Find(id);
            if (eMIcard == null)
            {
                return NotFound();
            }

            return Ok(eMIcard);
        }

        // PUT: api/EMIcards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMIcard(int id, EMIcard eMIcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMIcard.EMIcard_id)
            {
                return BadRequest();
            }

            db.Entry(eMIcard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMIcardExists(id))
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

        // POST: api/EMIcards
        [ResponseType(typeof(EMIcard))]
        //public IHttpActionResult PostEMIcard(EMIcard eMIcard)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.EMIcards.Add(eMIcard);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = eMIcard.EMIcard_id }, eMIcard);
        //}

        // DELETE: api/EMIcards/5
        [ResponseType(typeof(EMIcard))]
        public IHttpActionResult DeleteEMIcard(int id)
        {
            EMIcard eMIcard = db.EMIcards.Find(id);
            if (eMIcard == null)
            {
                return NotFound();
            }

            db.EMIcards.Remove(eMIcard);
            db.SaveChanges();

            return Ok(eMIcard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMIcardExists(int id)
        {
            return db.EMIcards.Count(e => e.EMIcard_id == id) > 0;
        }
    }
}