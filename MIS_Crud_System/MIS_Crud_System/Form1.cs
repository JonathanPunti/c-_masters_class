using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MIS_Crud_System
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;
        public MainForm()
        {
            InitializeComponent();
            string connectionString = "Data Source = JONATHANPUNTI\\SQLEXPRESS; Initial Catalog = MyMISDatabase; Integrated Security = True;";
            sqlConnection = new SqlConnection(connectionString);
            
            TabControlMain.Enabled = false;
            AddProductButton.Enabled = false;
            UpdateProductButton.Enabled = false;
            DeleteProductButton.Enabled = false;
            ProductsDataGridView.Enabled = false;
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                sqlConnection.Open();
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                sqlConnection.Close();

                if (count == 1)
                {
                    MessageBox.Show("Login successful.");
                    TabControlMain.Enabled = true;
                    AddProductButton.Enabled = true;
                    UpdateProductButton.Enabled = true;
                    DeleteProductButton.Enabled = true;
                    ProductsDataGridView.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }

        }

        private void LoadProducts()
        {
            string query = "SELECT * FROM Products";
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection))
            {
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                ProductsDataGridView.DataSource = dataTable;
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            string name = ProductNameTextBox.Text;
            string description = ProductDescriptionTextBox.Text;
            decimal price = Convert.ToDecimal(ProductPriceTextBox.Text);

            string query = "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", name);
                sqlCommand.Parameters.AddWithValue("@Description", description);
                sqlCommand.Parameters.AddWithValue("@Price", price);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

            LoadProducts();

        }

        private void UpdateProductButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ProductIDTextBox.Text);
            string name = ProductNameTextBox.Text;
            string description = ProductDescriptionTextBox.Text;
            decimal price = Convert.ToDecimal(ProductPriceTextBox.Text);

            string query = "UPDATE Products SET Name=@Name, Description=@Description, Price=@Price WHERE Id=@Id";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@Name", name);
                sqlCommand.Parameters.AddWithValue("@Description", description);
                sqlCommand.Parameters.AddWithValue("@Price", price);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

            LoadProducts();

        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ProductIDTextBox.Text);

            string query = "DELETE FROM Products WHERE Id=@Id";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

            LoadProducts();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //LoadProducts();

        }

    }
    }
    
