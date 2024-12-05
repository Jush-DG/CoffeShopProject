using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory
{
    public partial class AdminForm : Form
    {
        private string _username;
        public AdminForm(string username)
        {
            InitializeComponent();
            _username = username;
        }
       

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginLogoutClass loginLogoutClass = new LoginLogoutClass();
            int userID = loginLogoutClass.geUserId(_username);
            loginLogoutClass.UserLogout(userID);

            MessageBox.Show("Logged out successfuly!", "Confirm", MessageBoxButtons.OK);
            
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            lblName.Text = _username;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Proceed to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                Application.Exit();
            }
           
            
        }
    }
}
