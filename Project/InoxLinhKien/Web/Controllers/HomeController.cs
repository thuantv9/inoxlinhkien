using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        DbContext dbcontext = new DbContext();
        public ActionResult Index()
        {
            ViewBag.SlideImage = dbcontext.GetAllSlideImage();
            ViewBag.Category = dbcontext.GetAllCategory();
            ViewBag.Customer = dbcontext.GetAllCustomer();
            return View(dbcontext.GetAllProduct());
        }
        public ActionResult Category()
        {
            var categoryid = Request["CategoryId"];
            // truyền categoryid
            if (categoryid == null)
            {
                ViewBag.CategoryId = 1;
            }
            else
            {
                ViewBag.CategoryId = categoryid;
            }

            // lấy tất cả sản phẩm
            ViewBag.Product = dbcontext.GetAllProduct();
            // truyền model tất cả chủng loại sản phẩm
            return View(dbcontext.GetAllCategory());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Category = dbcontext.GetAllCategory();
            return View();
        }
    }
}