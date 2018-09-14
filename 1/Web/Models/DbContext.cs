using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Web.Common;
namespace Web.Models
{
    public class DbContext
    {
        #region Table Product
        // lấy tất cả sản phẩm
        public List<Product> GetAllProduct()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new Product()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            MadeFrom = reader["MadeFrom"].ToString(),
                            CategoryId = Int32.Parse(reader["CategoryId"].ToString()),
                            Dimenson = reader["Dimenson"].ToString(),
                            Image = reader["Image"].ToString(),
                            Remark = reader["Remark"].ToString(),
                            Status = Boolean.Parse(reader["Status"].ToString())
                        });
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // lấy sản phẩm theo chủng loại sản phẩm
        public List<Product> GetProductByCategoryId(int CategoryId)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GeProductByCategoryId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product()
                    {
                        Id = Int32.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString(),
                        MadeFrom = reader["MadeFrom"].ToString(),
                        CategoryId = Int32.Parse(reader["CategoryId"].ToString()),
                        Dimenson = reader["Dimenson"].ToString(),
                        Image = reader["Image"].ToString(),
                        Remark = reader["Remark"].ToString(),
                        Status = Boolean.Parse(reader["Status"].ToString())
                    });
                }
                return products;
            }
        }
        // lấy sản phẩm theo Id sản phẩm
        public Product GetProductById(int Id)
        {
            Product c = new Product();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetProductById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.Id = Int32.Parse(reader["Id"].ToString());
                    c.Name = reader["Name"].ToString();
                    c.MadeFrom = reader["MadeFrom"].ToString();
                    c.CategoryId = Int32.Parse(reader["CategoryId"].ToString());
                    c.Dimenson = reader["Dimenson"].ToString();
                    c.Image = reader["Image"].ToString();
                    c.Remark = reader["Remark"].ToString();
                    c.Status = Boolean.Parse(reader["Status"].ToString());
                }
                return c;
            }
        }
        // lấy sản phẩm theo tên
        public Product GetProductById(string Name)
        {
            Product c = new Product();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetProductByName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", Name);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.Id = Int32.Parse(reader["Id"].ToString());
                    c.Name = reader["Name"].ToString();
                    c.MadeFrom = reader["MadeFrom"].ToString();
                    c.CategoryId = Int32.Parse(reader["CategoryId"].ToString());
                    c.Dimenson = reader["Dimenson"].ToString();
                    c.Image = reader["Image"].ToString();
                    c.Remark = reader["Remark"].ToString();
                    c.Status = Boolean.Parse(reader["Status"].ToString());
                }
                return c;
            }
        }
        //  Thêm mới sản phẩm
        public int InsertProduct(Product product)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Int32.Parse(product.Id.ToString()));
                cmd.Parameters.AddWithValue("@Name", product.Name.ToString());
                cmd.Parameters.AddWithValue("@MadeFrom", product.MadeFrom);
                cmd.Parameters.AddWithValue("@CategoryId", Int32.Parse(product.CategoryId.ToString()));
                cmd.Parameters.AddWithValue("@Dimenson", product.Dimenson);
                cmd.Parameters.AddWithValue("@Image", product.Image);
                // Check remarks là null thì truyền null
                if (product.Remark == null)
                {
                    cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Remark", product.Remark);
                }
                cmd.Parameters.AddWithValue("@Status", Boolean.Parse(product.Status.ToString()));
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Cập nhật sản phẩm
        public int UpdateProduct(Product product)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Int32.Parse(product.Id.ToString()));
                cmd.Parameters.AddWithValue("@Name", product.Name.ToString());
                cmd.Parameters.AddWithValue("@MadeFrom", product.MadeFrom);
                cmd.Parameters.AddWithValue("@CategoryId", Int32.Parse(product.CategoryId.ToString()));
                cmd.Parameters.AddWithValue("@Dimenson", product.Dimenson);
                cmd.Parameters.AddWithValue("@Image", product.Image);
                // Check remarks là null thì truyền null
                if (product.Remark == null)
                {
                    cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Remark", product.Remark);
                }
                cmd.Parameters.AddWithValue("@Status", Boolean.Parse(product.Status.ToString()));
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Xóa sản phẩm 
        public int DeleteProduct(int Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        #endregion
        #region Table Category
        // lấy tất cả loại sản phẩm
        public List<Category> GetAllCategory()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            CategoryId = Int32.Parse(reader["CategoryId"].ToString()),
                            CategoryName = reader["CategoryName"].ToString(),

                        });
                    }
                    return categories;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // lấy chủng loại theo Id
        public Category GetCategoryById(int CategoryId)
        {
            Category c = new Category();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCategoryById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.CategoryId = Int32.Parse(reader["CategoryId"].ToString());
                    c.CategoryName = reader["CategoryName"].ToString();
                }
                return c;
            }
        }
        // Thêm chủng loại
        public int InsertCategory(Category category)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", Int32.Parse(category.CategoryId.ToString()));
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Cập nhật chủng loại
        public int UpdateCategory(Category category)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", Int32.Parse(category.CategoryId.ToString()));
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName.ToString());
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        #endregion
        #region Table Customer
        // lấy tất cả khách hàng
        public List<Customer> GetAllCustomer()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerId = Int32.Parse(reader["CustomerId"].ToString()),
                            CustomerName = reader["CustomerName"].ToString(),
                            CustomerImage = reader["CustomerImage"].ToString(),
                            CustomerDescription = reader["CustomerDescription"].ToString(),
                            CustomerRemark = reader["CustomerRemark"].ToString()
                            
                        });
                    }
                    return customers;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // thêm mới khách hàng
        public int InsertCustomer(Customer customer)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Int32.Parse(customer.CustomerId.ToString()));
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName.ToString());
                // Check CustomerImage là null thì truyền null
                if (customer.CustomerImage == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerImage", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerImage", customer.CustomerImage);
                }
                // Check CustomerDescription là null thì truyền null
                if (customer.CustomerDescription == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerDescription", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerDescription", customer.CustomerDescription);
                }
                //  // Check CustomerRemark là null thì truyền null
                if (customer.CustomerRemark == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerRemark", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerRemark", customer.CustomerRemark);
                }
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Cập nhật khách hàng
        public int UpdateCustomer(Customer customer)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Int32.Parse(customer.CustomerId.ToString()));
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName.ToString());
                // Check CustomerImage là null thì truyền null
                if (customer.CustomerImage == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerImage", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerImage", customer.CustomerImage);
                }
                // Check CustomerDescription là null thì truyền null
                if (customer.CustomerDescription == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerDescription", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerDescription", customer.CustomerDescription);
                }
                //  // Check CustomerRemark là null thì truyền null
                if (customer.CustomerRemark == null)
                {
                    cmd.Parameters.AddWithValue("@CustomerRemark", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerRemark", customer.CustomerRemark);
                }
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Xóa khách hàng
        public int DeleteCustomerById(int CustomerId)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteCustomerById", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", CustomerId);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        #endregion
        #region Table User
        
        #endregion
        #region Table SlideImage
        // lấy tất cả slide anh
        public List<SlideImage> GetAllSlideImage()
        {
            List<SlideImage> slides = new List<SlideImage>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllSlideImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        slides.Add(new SlideImage()
                        {
                            SlideId = Int32.Parse(reader["SlideId"].ToString()),
                            SlideImageName = reader["SlideImageName"].ToString(),
                            
                        });
                    }
                    return slides;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //  Thêm mới Slide
        public int InsertSlideImage(SlideImage slideimage)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertSlideImage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SlideId", Int32.Parse(slideimage.SlideId.ToString()));
                cmd.Parameters.AddWithValue("@SlideImageName", slideimage.SlideImageName.ToString());
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
       
        #endregion
        #region Table Order
        // lấy tất cả đơn hàng
        public List<Order> GetAllOrder()
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllOrder", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(new Order()
                        {
                            OrderId = Int32.Parse(reader["OrderId"].ToString()),
                            CustomerName = reader["CustomerName"].ToString(),
                            Creator = reader["Creator"].ToString(),
                            CreateDate =Convert.ToDateTime(reader["CreateDate"].ToString())
                        });
                    }
                    return orders;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // thêm mới đơn hàng
        public int InsertOrder(Order order)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", Int32.Parse(order.OrderId.ToString()));
                cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                cmd.Parameters.AddWithValue("@Creator", order.Creator);
                cmd.Parameters.AddWithValue("@CreateDate", order.CreateDate);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Update đơn hàng
        public int UpdateOrder(Order order)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateOrder", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", Int32.Parse(order.OrderId.ToString()));
                cmd.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                cmd.Parameters.AddWithValue("@Creator", order.Creator);
                cmd.Parameters.AddWithValue("@CreateDate", order.CreateDate);
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        #endregion
        #region Table OrderItem
        // lấy tất cả chi tiết hóa đơn
        public List<OrderItem> GetAllOrderItem()
        {
            List<OrderItem> orderitems = new List<OrderItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(Const.Connectring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetAllOrderItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orderitems.Add(new OrderItem()
                        {
                            OrderItemId = Int32.Parse(reader["OrderItemId"].ToString()),
                            OrderId = Int32.Parse(reader["OrderId"].ToString()),
                            ProductId = Int32.Parse(reader["ProductId"].ToString()),
                            Quantity = Int32.Parse(reader["Quantity"].ToString())
                        });
                    }
                    return orderitems;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // lấy chi tiết đơn hàng theo mã đơn hàng
        public List<OrderItem> GetOrderItemByOrderId(int OrderId)
        {
            List<OrderItem> orderitems = new List<OrderItem>();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetOrderItemByOrderId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderitems.Add(new OrderItem()
                    {
                        OrderItemId = Int32.Parse(reader["OrderItemId"].ToString()),
                        OrderId = Int32.Parse(reader["OrderId"].ToString()),
                        ProductId = Int32.Parse(reader["ProductId"].ToString()),
                        Quantity = Int32.Parse(reader["Quantity"].ToString())
                    });
                }
                return orderitems;
            }
        }
        // Thêm mới chi tiết đơn hàng
        public int InsertOrderItem(OrderItem orderitem)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertOrderItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderItemId", Int32.Parse(orderitem.OrderItemId.ToString()));
                cmd.Parameters.AddWithValue("@OrderId", Int32.Parse(orderitem.OrderId.ToString()));
                cmd.Parameters.AddWithValue("@ProductId", Int32.Parse(orderitem.ProductId.ToString()));
                cmd.Parameters.AddWithValue("@Quantity", Int32.Parse(orderitem.Quantity.ToString()));                
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        // Update chi tiết đơn hàng
        public int UpdateOrderItem(OrderItem orderitem)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateOrderItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderItemId", Int32.Parse(orderitem.OrderItemId.ToString()));
                cmd.Parameters.AddWithValue("@OrderId", Int32.Parse(orderitem.OrderId.ToString()));
                cmd.Parameters.AddWithValue("@ProductId", Int32.Parse(orderitem.ProductId.ToString()));
                cmd.Parameters.AddWithValue("@Quantity", Int32.Parse(orderitem.Quantity.ToString()));
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }
        #endregion
    }
}