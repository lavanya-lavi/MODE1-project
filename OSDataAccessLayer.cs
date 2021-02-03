using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using OSEntitiesLib;
using System.Configuration;
using OSExceptionLib;

 namespace OSDataAccessLib
{
    public class OSDataAccessLayer : IOSDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;
        public OSDataAccessLayer()
        {
            con = new SqlConnection();
           // object Configurationmanager = null;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
        }          
        /// <summary>
        /// This method to add items to cart
        /// </summary>
        /// <param name="lst">list as a parameter</param>
        public void AddToCart(List<CartItems> lst)
        {
            int itemsEffected;
            try
            {
                foreach (var item in lst)
                {
                    //to insert details of product into cart
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into cart(productid,productname,image,price) values(@pid,@pn,@im,@pr)";
                    cmd.Parameters.Clear();
                    //configure parameters
                    cmd.Parameters.AddWithValue("@pid", item.ProductId);
                    cmd.Parameters.AddWithValue("@pn", item.ProductName);
                    cmd.Parameters.AddWithValue("@im", item.Image);
                    cmd.Parameters.AddWithValue("@pr", item.Price);
                    cmd.CommandType = CommandType.Text;
                    //atach the connection with the command
                    cmd.Connection = con;
                    //open the connection
                    con.Open();
                    itemsEffected = cmd.ExecuteNonQuery();
                    if (itemsEffected == 0)
                    {
                        throw new Exception("no item in the cart");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
        }
        /// <summary>
        /// this method is used to delete the product from cart based on productid
        /// </summary>
        /// <param name="id"> productid as parameter</param>
        public void DeleteFromCartById(int id)
        {
            try
            {
                //delete the product based on product id
                cmd = new SqlCommand();
                cmd.CommandText = "delete from cart where productid=@id";
                //configure the commands
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                //open the connection
                con.Open();
                int recordsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if(recordsAffected==0)
                {
                    throw new Exception("item does not exist in cart");
                }
            }
            catch(SqlException ex)
            {
                throw new OSException("some database error occured :" + ex.Message);
            }
            catch(Exception ex)
            {
                throw new OSException("some database error occured :" + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
        }

        /// <summary>
        /// This method retrieve all the productcategory records from database
        /// </summary>
        /// <returns>productcategory record from database</returns>
        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> lstc = new List<ProductCategory>();
            try
            {
                //To do select all
                cmd = new SqlCommand();
                cmd.CommandText = "select * from prodcategory";
                cmd.CommandType = CommandType.Text;
                //attach the connection with the command
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data reader & add them to the collection
                while (sdr.Read())
                {
                    ProductCategory pct = new ProductCategory
                    {
                        CategoryId = (int)sdr[0],
                        CategoryName = sdr[1].ToString()
                    };
                    lstc.Add(pct);
                }
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close connection
                con.Close();
            }
            return lstc;
        }
        /// <summary>
        /// this method retrieves all the items in cart from database
        /// </summary>
        /// <returns>cart items record from data base</returns>
        public List<CartItems> GetCartItems()
        {
            List<CartItems> lstcart = new List<CartItems>();
            try
            {
                //to do select product details from cart
                cmd = new SqlCommand();
                cmd.CommandText = "select * from cart";
                //configure the command
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                //open the connection
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    //read the all parameters
                    CartItems cart = new CartItems
                    {
                        ProductId = (int)sdr[0],
                        ProductName = sdr[1].ToString(),
                        Image = sdr[2].ToString(),
                        Price = (decimal)sdr[3]

                    };
                    lstcart.Add(cart);
                }
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return the list of record
            return lstcart;

        }
            
        
        /// <summary>
        /// This method retrieve the usercredentials record from database based on password
        /// </summary>
        /// <param name="name"> It is used to pass the password whose record is to be selected</param>
        /// <returns> usercredentials record found based on password passed</returns>
        public UserCredentials GetPassword(string name)
        {
            UserCredentials user = new UserCredentials();
            try
            {
                //to select password
                cmd = new SqlCommand();
                cmd.CommandText = "select password from usercredentials where password=@name";
                //configure command parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.CommandType = CommandType.Text;
                //attach the connection
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    //read the record
                    user.Password = sdr[0].ToString();
                }
                sdr.Close();
            
            }
            catch(SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
             }
            catch(Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
             }
            finally
            {
                //close connection
              con.Close();
             }
            //return the record
            return user;
           
              }
        /// <summary>
        /// This method retrieve the product record from database based on product id
        /// </summary>
        /// <param name="id"> It is used to pass the product id whose record is to be selected  </param>
        /// <returns> product record found based on product id passed</returns>
        public Products GetProductDetailsById(int id)
        {
            Products pro = new Products();
            try
            {
                //select products by id
                cmd = new SqlCommand();
                cmd.CommandText = "select*from product where productid=@id";
                //configure command parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;
                //attach the command
                cmd.Connection = con;
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    //read the record
                    pro.ProductId = (int)sdr[0];
                    pro.CategoryId = (int)sdr[1];
                    pro.ProductName = sdr[2].ToString();
                    pro.Picture = sdr[3].ToString();
                    pro.Price = (decimal)sdr[4];
                    pro.Content = sdr[5].ToString();

                }
                sdr.Close();
                   
            }
            catch(SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch(Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return the record
            return pro;
           
        }
        /// <summary>
        /// This method retrieve the product records from database based on category name
        /// </summary>
        /// <param name="cname">It is used to pass the category name whose record is to be selected </param>
        /// <returns> product records found based on category name passed</returns>
        public List<Products> GetProductsByCategoryName(string cname)
        {
            List<Products> lstpro = new List<Products>();
            try
            {
                //select products by categoryname
                cmd = new SqlCommand();

                cmd.CommandText = "select p.productid,p.productname,p.picture,p.price from product as p join " +
                    " prodcategory as c on p.categoryid=c.categoryid  where category like @cn";
                // "select productid,productname,picture,price from product where productname like @pn";
                //configure the command parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cn", "%" + cname + "%");
                cmd.CommandType = CommandType.Text;
                //attach the connection
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Products pro = new Products
                    {
                        //read the record
                        ProductId = (int)sdr[0],

                        ProductName = sdr[1].ToString(),
                        Picture = sdr[2].ToString(),
                        Price = (decimal)sdr[3]
                    };
                    lstpro.Add(pro);
                }

                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return the list
            return lstpro;

        }
        /// <summary>
        /// This method retrieve the products record from database based on product name
        /// </summary>
        /// <param name="productname"> It is used to pass the product name whose record is to be selected</param>
        /// <returns> product records found based on productname passed</returns>
        public List<Products> GetProductsByName(string productname)
        {
            List<Products> lstpro = new List<Products>();
            try
            {
                 //select product by productname
                cmd = new SqlCommand();
                cmd.CommandText = "select p.productid,p.productname,p.picture,p.price from product as p join " +
                    " prodcategory as c on p.categoryid=c.categoryid  where productname like @pn";
               // "select productid,productname,picture,price from product where productname like @pn";
               //configure command parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pn", "%" + productname + "%");
                cmd.CommandType = CommandType.Text;
                //attach the connection
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Products pro = new Products
                    {
                        //read the records
                        ProductId = (int)sdr[0],

                        ProductName = sdr[1].ToString(),
                        Picture = sdr[2].ToString(),
                       Price=(decimal)sdr[3]



                    };
                    lstpro.Add(pro);
                }

                sdr.Close();   
            }
            catch(SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch(Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close connection
                con.Close();
            }
            //return the list 
            return lstpro;
            
        }
        /// <summary>
        /// This Method retrieve the usercredentials record from database based on username
        /// </summary>
        /// <param name="name"> It is used to pass the username whose record is to be selected</param>
        /// <returns> usercredentials record found based on username passed</returns>
        public UserCredentials GetUserName(string name)
        {
            UserCredentials user = new UserCredentials();
            try
            {
                //to select username
                cmd = new SqlCommand();
                cmd.CommandText = "select username from usercredentials where username=@name";
                //configure the command parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.CommandType = CommandType.Text;
                //attach the connection
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    //read the record
                    user.UserName = sdr[0].ToString();
                }
                sdr.Close();
            }
            catch(SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
             }
            catch(Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close connection
              con.Close();
            }
            //return the record
            return user;           
        }
        /// <summary>
        /// This method displays the products which are in cart list
        /// </summary>
        /// <param name="list">it is used to pass the cartlist whose record is to be selected</param>
        /// <returns>products are found</returns>

        public List<Products> ViewCart(string list)
        {
            List<Products> lstpro = new List<Products>();
            cmd = new SqlCommand();
            try
            {
               // select product by name
                cmd.CommandText = "select productid,productname,picture,price from product where productid in ("+list+"0)";
                cmd.CommandType = CommandType.Text;
                //attach connection
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    Products product = new Products
                    {
                        ProductId = (int)sdr[0],
                        ProductName = sdr[1].ToString(),
                        Picture = sdr[2].ToString(),
                        Price = Convert.ToDecimal(sdr[3]),
                    };
                    lstpro.Add(product);
                }
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new OSException("some data base error happened:" + ex.Message);
            }
            finally
            {
                //close connection
                con.Close();
            }
            //return the list 
            return lstpro;


        }
    }
}
