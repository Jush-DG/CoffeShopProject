using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    internal class AdminAddUserClass
    {
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");
        public int Id { get; set; }  
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; } 
        public string DateRegistered {  get; set; }

        public string Image {  get; set; }
        public List<AdminAddUserClass> UserListData()
        {
            List<AdminAddUserClass> listData = new List<AdminAddUserClass>();

            if (Connection.State != ConnectionState.Open)
            {
                try
                {
                    Connection.Open();

                    string selectData = "SELECT * FROM UserAccounts";

                    using (SqlCommand cmd = new SqlCommand(selectData, Connection))
                    {
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            AdminAddUserClass userData = new AdminAddUserClass();
                            userData.Id = (int)sqlDataReader["userID"];
                            userData.Username = sqlDataReader["userName"].ToString();
                            userData.Password = sqlDataReader["userPassword"].ToString();
                            userData.Role = sqlDataReader["userRole"].ToString();
                            userData.Status = sqlDataReader["userStatus"].ToString();
                            userData.DateRegistered = sqlDataReader["dateRegistered"].ToString();
                            userData.Image = sqlDataReader["userPhoto"].ToString();  

                            listData.Add(userData);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Failed", ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }

            return listData;

        }

    }
}
