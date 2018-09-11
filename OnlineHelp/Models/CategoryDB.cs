using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineHelp.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace OnlineHelp.Models
{
    public class CategoryDB
    {

        // Phương thức List danh sách Category
        public List<Category> ListAll()
        {
            List<Category> categories = new List<Category>();
            try
            { 
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ShowAllCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category()
                    {
                        CategoryID = Int32.Parse(reader["CategoryID"].ToString()),
                        Level = Int32.Parse(reader["Level"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        ParentCategoryID = Int32.Parse(reader["ParentCategoryID"].ToString()),
                        EditDate = Convert.ToDateTime(reader["EditDate"].ToString()),
                        Editor = reader["Editor"].ToString(),
                        Description = reader["Description"].ToString(),
                        Remarks = reader["Remarks"].ToString()
                    });
                }
                return categories;
            }
                }
            catch(Exception ex)
            {
                return null;
            }
        }
        // lấy category by ID
        public Category GetCategoryByID(int categoryid)
        {
            Category c = new Category();
            string query = string.Format("select * from Categories where CategoryID={0}", categoryid);
            SqlConnection connection = new SqlConnection(Const.Connectring);
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        c.CategoryID = Int32.Parse(reader["CategoryID"].ToString());
                        c.Level = Int32.Parse(reader["Level"].ToString());
                        c.CategoryName = reader["CategoryName"].ToString();
                        c.ParentCategoryID = Int32.Parse(reader["ParentCategoryID"].ToString());
                        c.EditDate = Convert.ToDateTime(reader["EditDate"].ToString());
                        c.Editor = reader["Editor"].ToString();
                        c.Description = reader["Description"].ToString();
                        c.Remarks = reader["Remarks"].ToString();
                    }
                    return c;
                }
            }
        }

        // 
        public List<Category> GetListByLevel(int level)
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCategoryByLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Level", level);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category()
                    {
                        CategoryID = Int32.Parse(reader["CategoryID"].ToString()),
                        Level = Int32.Parse(reader["Level"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        ParentCategoryID = Int32.Parse(reader["ParentCategoryID"].ToString()),
                        EditDate = Convert.ToDateTime(reader["EditDate"].ToString()),
                        Editor = reader["Editor"].ToString(),
                        Description = reader["Description"].ToString(),
                        Remarks = reader["Remarks"].ToString()
                    });
                }
                return categories;
            }
        }


        // Phương thức lấy list level
        public List<int> GetListLevel()
        {
            List<int> lstLevel = new List<int>();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetListLevel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstLevel.Add(Int32.Parse(reader["Level"].ToString()));
                }
                return lstLevel;
            }
        }

        // Phương thức lấy Category cha truyền vào là level con.
       public DataTable GetListCategoryByLevelParent(int level)
        {
            DataTable dt = new DataTable() ;
            SqlDataAdapter da= new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
               
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetCategoryByLevelParent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Level", level);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
            }
            return dt;
        }

        // Phương thức thêm mới Insert Category
        public int Create(Category category)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertUpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", Int32.Parse(category.CategoryID.ToString()));
                cmd.Parameters.AddWithValue("@Level", Int32.Parse(category.Level.ToString()));
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@ParentCategoryID", Int32.Parse(category.ParentCategoryID.ToString()));
                cmd.Parameters.AddWithValue("@EditDate", category.EditDate);
                cmd.Parameters.AddWithValue("@Editor", category.Editor);
                cmd.Parameters.AddWithValue("@Description", category.Description);
                // Check remarks là null thì truyền null
                if (category.Remarks == null)
                {
                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                }
                else
                { 
                cmd.Parameters.AddWithValue("@Remarks", category.Remarks);
                }
                cmd.Parameters.AddWithValue("@Action", "Insert");
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        // Phương thức Update Category
        public int Update(Category category)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertUpdateCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", Int32.Parse(category.CategoryID.ToString()));
                cmd.Parameters.AddWithValue("@Level", Int32.Parse(category.Level.ToString()));
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@ParentCategoryID", Int32.Parse(category.ParentCategoryID.ToString()));
                cmd.Parameters.AddWithValue("@EditDate", category.EditDate);
                cmd.Parameters.AddWithValue("@Editor", category.Editor);
                cmd.Parameters.AddWithValue("@Description", category.Description);
                cmd.Parameters.AddWithValue("@Remarks", category.Remarks);
                cmd.Parameters.AddWithValue("@Action", "Update");
                i = cmd.ExecuteNonQuery();
            }
            return i;
        }

        // Phương thức Delete
        public int Delete(int categoryid)
        {
            int i;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryID", categoryid);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        // lấy số CategoryID tiếp theo 
        public int GetNextCategoryID()
        {
            int i = 1;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("GetNextCategoryID", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = com.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        i = Int32.Parse(reader[0].ToString());
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return i;
        }

        // lấy số level mới
        public int GetNextLevel()
        {

            int i = 1;
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("GetNextLevel", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = com.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        i = Int32.Parse(reader[0].ToString());
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }
            return i;
        }
        // lấy nhưng Category có level trước level truyền vào
    public List<Category> Getparentcategorybylevelofchild(int level)
        {
            List<Category> categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(Const.Connectring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Getparentcategorybylevelofchild", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Level", level);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category()
                    {
                        CategoryID = Int32.Parse(reader["CategoryID"].ToString()),
                        Level = Int32.Parse(reader["Level"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        ParentCategoryID = Int32.Parse(reader["ParentCategoryID"].ToString()),
                        EditDate = Convert.ToDateTime(reader["EditDate"].ToString()),
                        Editor = reader["Editor"].ToString(),
                        Description = reader["Description"].ToString(),
                        Remarks = reader["Remarks"].ToString()
                    });
                }
                return categories;
            }

        }
    }
    
}