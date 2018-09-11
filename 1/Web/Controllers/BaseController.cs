using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        DbContext dbcontext = new DbContext();  
        #region Admin Product
        // lấy tất cả product
        public JsonResult GetAllProduct()
        {
            return Json(dbcontext.GetAllProduct(), JsonRequestBehavior.AllowGet);
        }
        // lấy by id
        public JsonResult GetProductByCategoryId(int CategoryId)
        {
            return Json(dbcontext.GetProductByCategoryId(CategoryId), JsonRequestBehavior.AllowGet);
        }
        // Thêm mới sản phẩm
        public JsonResult InsertProduct(Product product)
        {
            return Json(dbcontext.InsertProduct(product), JsonRequestBehavior.AllowGet);
        }
        // Cập nhật sản phẩm
        public JsonResult UpdateProduct(Product product)
        {
            return Json(dbcontext.UpdateProduct(product), JsonRequestBehavior.AllowGet);
        }
        // Xóa sản phẩm 
        public JsonResult Delete(int id)
        {
            return Json(dbcontext.Delete(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Admin Category
        // lấy tất cả category
        public JsonResult GetAllCategory()
        {
            return Json(dbcontext.GetAllCategory(), JsonRequestBehavior.AllowGet);
        }
        // lấy chủng loại theo Id
        public JsonResult GetCategoryById(int categoryid)
        {
            return Json(dbcontext.GetCategoryById(categoryid), JsonRequestBehavior.AllowGet);
        }
        // thêm mới category
        public JsonResult InsertCategory(Category category)
        {
            return Json(dbcontext.InsertCategory(category), JsonRequestBehavior.AllowGet);
        }
        // Cập nhật chủng loại
        public JsonResult UpdateCategory(Category category)
        {
            return Json(dbcontext.UpdateCategory(category), JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Admin Customer
        // lấy tất cả kh
        public JsonResult GetAllCustomer()
        {
            return Json(dbcontext.GetAllCustomer(), JsonRequestBehavior.AllowGet);
        }
        // thêm mới khách hàng
        public JsonResult InsertCustomer(Customer customer)
        {
            return Json(dbcontext.InsertCustomer(customer), JsonRequestBehavior.AllowGet);
        }
        // cập nhật khách hàng
         public JsonResult UpdateCustomer(Customer customer)
        {
            return Json(dbcontext.UpdateCustomer(customer), JsonRequestBehavior.AllowGet);
        }
     
        #endregion
        #region Admin SlideImage
        // lấy tất cả slide
        public JsonResult GetAllSlideImage()
         {
             return Json(dbcontext.GetAllSlideImage(), JsonRequestBehavior.AllowGet);
         }
        // thêm mới slide
        public JsonResult InsertSlideImage(SlideImage slide)
         {
             return Json(dbcontext.InsertSlideImage(slide), JsonRequestBehavior.AllowGet);
         }        
        #endregion
        #region Admin Order
        // lấy tất cả đơn hàng
        public JsonResult GetAllOrder()
        {
            return Json(dbcontext.GetAllOrder(), JsonRequestBehavior.AllowGet);
        }
        // cập nhật đơn hàng
        public JsonResult InsertOrder(Order order)
        {
            return Json(dbcontext.InsertOrder(order), JsonRequestBehavior.AllowGet);
        }
        // update đơn hàng
        public JsonResult UpdateOrder(Order order)
        {
            return Json(dbcontext.UpdateOrder(order), JsonRequestBehavior.AllowGet);
        }
        
        #endregion
        #region Admin OrderItem
        // lấy tất cả chi tiết
        public JsonResult GetAllOrderItem()
        {
            return Json(dbcontext.GetAllOrderItem(), JsonRequestBehavior.AllowGet);
        }
        // lấy chi tiết đơn hàng theo mã đơn hàng
        public JsonResult GetOrderItemByOrderId(int OrderId)
        {
            return Json(dbcontext.GetOrderItemByOrderId(OrderId), JsonRequestBehavior.AllowGet);
        }
        // thêm mới chi tiết đơn hàng
        public JsonResult InsertOrderItem(OrderItem  orderitem)
        {
            return Json(dbcontext.InsertOrderItem(orderitem), JsonRequestBehavior.AllowGet);
        }
        // Update chi tiết đơn hàng
        public JsonResult UpdateOrderItem(OrderItem orderitem)
        {
            return Json(dbcontext.UpdateOrderItem(orderitem), JsonRequestBehavior.AllowGet);
        }
        
        #endregion
	}
}