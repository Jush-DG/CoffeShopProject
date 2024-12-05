using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Inventory
{

    internal class LoginLogoutClass
    {
        private string connectionString = "Data Source=LAPTOP-DVKO92VB\\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;"
;
        public void RecordLoginTime(int userID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Insert login time into user_logins table
                string query = "INSERT INTO logHistory (userID, loginTime) VALUES (@userId, @loginTime)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userID);
                    cmd.Parameters.AddWithValue("@loginTime", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UserLogout(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT TOP 1 userID FROM LogHistory WHERE userID = @userId AND Status = 'Active' ORDER BY loginTime DESC;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add the @userId parameter for the SELECT query
                    cmd.Parameters.AddWithValue("@userId", userId);

                    var userStatus = cmd.ExecuteScalar();

                    if (userStatus != null)
                    {
                        // Correct the UPDATE query
                        string updateQuery = "UPDATE LogHistory SET logoutTime = @logoutTime, Status = 'Inactive' WHERE userID = @userId AND Status = 'Active';";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                        {
                            // Add both parameters for the UPDATE query
                            updateCmd.Parameters.AddWithValue("@logoutTime", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@userId", userId);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No active status found for user {userId}");
                    }
                }
            }
        }

        public int geUserId(string username)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT userID FROM userAccounts WHERE userName = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

