using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MadeFrom { get; set; }
        public int CategoryId { get; set; }
        public string Dimenson { get; set; }
        public string Image { get; set; }
        public string Remark { get; set; }
        public Boolean Status { get; set; }
    }
}