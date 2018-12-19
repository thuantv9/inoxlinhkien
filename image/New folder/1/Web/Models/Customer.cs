using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string CustomerDescription { get; set; }
        public string CustomerRemark { get; set; }
    }
}