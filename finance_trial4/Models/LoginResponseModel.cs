using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace finance_trial4.Models
{
    public class LoginResponseModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public int CustomerId { get; set; }
    }
}