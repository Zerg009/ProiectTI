using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // Get the employee details from the input fields
            string nume = txtNume.Text.Trim();
            string prenume = txtPrenume.Text.Trim();
            string salariu = txtSalariu.Text.Trim();

            // SQL Insert query
            string query = "INSERT INTO SALARII_ANGAJATI (NUME, PRENUME, SALARIU) VALUES (@NUME, @PRENUME, @SALARIU)";

            // Connect to the database and execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@NUME", nume);
                        cmd.Parameters.AddWithValue("@PRENUME", prenume);
                        cmd.Parameters.AddWithValue("@SALARIU", salariu);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the insert was successful
                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Employee added successfully!";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessage.Text = "Failed to add employee.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            // Optionally, clear the form fields after submission
            txtNume.Text = "";
            txtPrenume.Text = "";
            txtSalariu.Text = "";
        }
    }
}