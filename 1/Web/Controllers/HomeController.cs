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
        public ActionResult Index2()
        {
            ViewBag.Product = dbcontext.GetAllProduct();
            ViewBag.Customer = dbcontext.GetAllCustomer();
            ViewBag.News = dbcontext.GetAllNews().Take(3);
            return View(dbcontext.GetAllCategory());
        }
        public ActionResult Category()
        {
           
            var id = Url.RequestContext.RouteData.Values["id"];
            // truyền categoryid
            if (id == null)
            {
                ViewBag.CategoryId = "";
            }
            else
            {
                ViewBag.CategoryId = id;
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
        public ActionResult Customer()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null)
            {
                ViewBag.CustomerId = "";
            }
            else
            {
                ViewBag.CustomerId = id;
            }
            return View(dbcontext.GetAllCustomer());
        }
        public ActionResult Product(int id)
        {
            return View(dbcontext.GetProductById(id));
        }
      
    }
}