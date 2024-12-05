using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _productName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingSource showProductList;

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _productName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_productName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = {"Beverages", "Bread/Bakery", "Canned/Jarred Goods" +
                    "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other"};

            foreach (string item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }
        }
        public string Product_Name(string name)
        {

            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z\s'-]+$")) 
                {
                    throw new FormatException("Invalid product name. Only alphabetic characters are allowed.");
                }
                return name;
            }  
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; 
            }
        }

        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                {
                    throw new ArithmeticException("Invalid quantity. Only numeric values are allowed.");
                }
                return Convert.ToInt32(qty);
            }
            catch (ArithmeticException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; 
            }
        }

        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price, @"^(\d*\.)?\d+$"))
                {
                    throw new FormatException("Invalid price format.");
                }
                return Convert.ToDouble(price);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        }
}
