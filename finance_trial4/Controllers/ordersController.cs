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
    public class ordersController : ApiController
    {

        private financedbEntities6 db = new financedbEntities6();

        public IHttpActionResult PostOrders(orderattributes orderattributes)
        {
            productsMaster product = db.productsMasters.Where(x => x.product_id == orderattributes.product_id).FirstOrDefault();
            order order = new order();
            EMItypeMaster EMItype = db.EMItypeMasters.Where(x => x.EMItype_id == orderattributes.EMItype_id).FirstOrDefault();
            EMIcard emicard = db.EMIcards.Where(x => x.customer_id == orderattributes.customer_id).FirstOrDefault();
            if(emicard==null)
            {
                return Ok(-1);
            }
            
            if(emicard.remaining_credit<product.product_price)
            {
                return Ok(0);
            }
            else
            {
                order.product_id = orderattributes.product_id;
                order.customer_id = orderattributes.customer_id;
                order.EMItype_id = orderattributes.EMItype_id;
                order.EMI_amount = product.product_price / EMItype.EMI_tenure;
                order.order_date = DateTime.Now;
                order.order_status = false;
                db.orders.Add(order);
                db.SaveChanges();
                emicard.remaining_credit = emicard.remaining_credit - product.product_price;
                emicard.used_credit = emicard.used_credit + product.product_price;
                db.Entry(emicard).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(1);
               
            }
           
        }
        // GET: api/orders
        public IQueryable<order> Getorders()
        {
            return db.orders;
        }

        // GET: api/orders/5
        [ResponseType(typeof(order))]
        public IHttpActionResult Getorder(int id)
        {
            order order = db.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putorder(int id, order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.order_id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderExists(id))
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

        // POST: api/orders
        [ResponseType(typeof(order))]
        //public IHttpActionResult Postorder(order order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.orders.Add(order);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = order.order_id }, order);
        //}


        // DELETE: api/orders/5
        [ResponseType(typeof(order))]
        public IHttpActionResult Deleteorder(int id)
        {
            order order = db.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool orderExists(int id)
        {
            return db.orders.Count(e => e.order_id == id) > 0;
        }
    }
}