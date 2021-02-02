using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using finance_trial4.Models;

namespace finance_trial4.Controllers
{
    public class LogincustomerController : ApiController
    {
        private financedbEntities6 db = new financedbEntities6();

        public IHttpActionResult GetAdmin()
        {
            return Ok(db.adminMasters);
        }

        [Route("api/user")]
        public IHttpActionResult PostUserLogin(Logincredentials cred)
        {
            LoginResponseModel loginres = new LoginResponseModel();
            Customer temp = db.Customers.Where(x => x.user_name == cred.user_name && x.user_password == cred.user_password).FirstOrDefault();
            if (temp == null)
            {
                loginres.StatusCode = 0;
                loginres.Message = "Invalid Login";
                loginres.CustomerId = 0;
                return Ok(loginres);
            }
            else
            {
                loginres.StatusCode = 1;
                loginres.Message = "Login Successful";
                loginres.CustomerId = temp.customer_id;
                return Ok(loginres);
            }
        }

        [Route("api/admin")]
        public IHttpActionResult PostAdminLogin(Logincredentials cred)
        {
            LoginResponseModel1 loginres = new LoginResponseModel1();
            adminMaster temp = db.adminMasters.Where(x => x.admin_username == cred.user_name && x.admin_password == cred.user_password).FirstOrDefault();
            if (temp == null)
            {
                loginres.StatusCode = 0;
                loginres.Message = "Invalid Login";
                loginres.Admin_username= null;
                return Ok(loginres);
                
            }
            else
            {
                loginres.StatusCode = 1;
                loginres.Message = "Login Successful";
                loginres.Admin_username = temp.admin_username;
                return Ok(loginres);
            }

           
        }



    }
}
