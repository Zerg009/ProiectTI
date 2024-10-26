using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
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
        private void BindGrid()
        {
            string connString = ConfigurationManager.ConnectionStrings["OracleDbConnection"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = new OracleCommand("SELECT nume, prenume, salar_baza FROM salarii_angajati", conn))
                {
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRowIndex = GridView1.SelectedIndex;
            string column1Value = GridView1.SelectedRow.Cells[1].Text;  // Adjust column index as needed

            // Example: Perform actions based on selected row data
            // Manipulate the data in the selected row here
        }
        protected void btnProcessSelection_Click(object sender, EventArgs e)
        {
            List<string> selectedRows = new List<string>();

            foreach (GridViewRow row in GridView1.Rows)
            {
                // Find the CheckBox control
                CheckBox chkSelect = (CheckBox)row.FindControl("CheckBoxSelect");

                // Check if the CheckBox is checked
                if (chkSelect != null && chkSelect.Checked)
                {
                    // Assuming you want to collect the 'nume' column value as an example
                    string nume = row.Cells[1].Text; // Adjust the index based on your columns
                    selectedRows.Add(nume);
                }
            }

            // Now you can process the selectedRows list as needed
            // For example, display the selected names in a label or process them further
        }
    }
}