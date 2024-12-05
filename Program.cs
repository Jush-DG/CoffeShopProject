using System;
using System.Windows.Forms;

namespace Inventory
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (LoginForm loginForm = new LoginForm())
            {
                
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    string userRole = loginForm.UserRole;  
                    string username = loginForm.Username; 

                    if (userRole == "Admin")
                    {
                        
                        Application.Run(new AdminForm(username));
                    }
                    else if (userRole == "Employee")
                    {
                        // Open EmployeeForm
                      //  Application.Run(new EmployeeForm(username));
                    }
                    else
                    {
                        MessageBox.Show("Unknown role. Please contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
