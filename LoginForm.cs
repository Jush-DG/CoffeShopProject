using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory
{

    public partial class LoginForm : Form
    {
        private SqlConnection connectionString = new SqlConnection(@"Data Source=LAPTOP-DVKO92VB\SQLEXPRESS;Initial Catalog=CoffeeShopDB;Integrated Security=True;");
        private LoginLogoutClass LoginLogoutClass = new LoginLogoutClass();
        public LoginForm()
        {
            InitializeComponent();
        }
        public string UserRole { get; private set; }
        public string Username { get; private set; }
        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = showPass.Checked ? '\0' : '*';

        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;

            if (emptyFields())
            {
                MessageBox.Show("Please enter username and password.", "Error Message", MessageBoxButtons.OK);
                return;
            }

            try
            {
                if (connectionString.State != ConnectionState.Open)
                {
                    connectionString.Open();
                }

                string getUserAccount = "SELECT * FROM UserAccounts WHERE userName = @username AND userPassword = @password";

                using (SqlCommand cmd = new SqlCommand(getUserAccount, connectionString))
                {
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 1)
                    {
                        Username = dataTable.Rows[0]["userName"].ToString();
                        UserRole = dataTable.Rows[0]["userRole"].ToString();

                        MessageBox.Show("Login Successfully!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        int userID = LoginLogoutClass.geUserId(Username);
                        LoginLogoutClass.RecordLoginTime(userID);

                        DialogResult = DialogResult.OK; 
                        connectionString.Close();

                    }
                    else
                    {
                        MessageBox.Show("Login failed. Username or password is incorrect", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connectionString.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString.Close();
            }
            finally
            {
                connectionString.Close();
            }
        }
        private bool emptyFields()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}



