using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult OrderItem()
        {
            return View();
        }
        public ActionResult SlideImage()
        {
            return View();
        }
	}
}