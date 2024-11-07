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
        string connString = ConfigurationManager.ConnectionStrings["OracleDbConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitFunctionsDropdown();
            }
        }
        private void InitFunctionsDropdown()
        {
            List<string> functions = new List<string>
            {
                "Manager",
                "Engineer",
                "HR Specialist",
                "Accountant",
                "Technician",
                "Sales Representative",
                "Marketing Specialist",
                "Product Designer",
                "Data Analyst",
                "Customer Support Agent"
            };

            ddlPosition.Items.Clear();
            foreach (var function in functions)
            {
                ddlPosition.Items.Add(new ListItem(function));
            }
        }
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // Get values from the form
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string position = ddlPosition.SelectedValue;
            int baseSalary = int.Parse(txtBaseSalary.Text.Trim());
            int spor = int.Parse(txtSpor.Text.Trim());
            int grossBonuses = int.Parse(txtGrossBonuses.Text.Trim());
            int retineri = int.Parse(txtRetineri.Text.Trim());

            // Prepare the SQL insert statement with an additional parameter for the photo BLOB
            string insertQuery = "INSERT INTO Salarii_Angajati (NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, RETINERI, POZA) " +
                                 "VALUES (:name, :surname, :position, :baseSalary, :spor, :grossBonuses, :retineri, :photo)";

            try
            {
                using (OracleConnection conn = new OracleConnection(connString))
                {
                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        // Add parameters for employee details
                        cmd.Parameters.Add(new OracleParameter(":name", name));
                        cmd.Parameters.Add(new OracleParameter(":surname", surname));
                        cmd.Parameters.Add(new OracleParameter(":position", position));
                        cmd.Parameters.Add(new OracleParameter(":baseSalary", baseSalary));
                        cmd.Parameters.Add(new OracleParameter(":spor", spor));
                        cmd.Parameters.Add(new OracleParameter(":grossBonuses", grossBonuses));
                        cmd.Parameters.Add(new OracleParameter(":retineri", retineri));

                        // Check if a file was uploaded
                        if (filePhoto.HasFile)
                        {
                            // Convert the uploaded file to a byte array for the BLOB
                            byte[] photoData = filePhoto.FileBytes;
                            cmd.Parameters.Add(new OracleParameter(":photo", OracleDbType.Blob)).Value = photoData;
                        }
                        else
                        {
                            // If no photo uploaded, set photo to null
                            cmd.Parameters.Add(new OracleParameter(":photo", DBNull.Value));
                        }

                        // Open the connection and execute the insert command
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Angajat adăugat cu succes!"; // Success message
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Eroare la adăugarea angajatului: " + ex.Message; // Error message
            }
            finally
            {
                ClearForm(); // Clear the form fields
            }

            //// Clear the form after adding the employee
            //ClearForm();
        }
        private void ClearForm()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            ddlPosition.SelectedIndex = 0;
            txtBaseSalary.Text = "";
            txtSpor.Text = "";
            txtGrossBonuses.Text = "";
            txtRetineri.Text = "";
        }
        protected void ValidateNameLength(object source, ServerValidateEventArgs args)
        {
            args.IsValid = txtName.Text.Length <= 25;
        }

        protected void ValidateSurnameLength(object source, ServerValidateEventArgs args)
        {
            args.IsValid = txtSurname.Text.Length <= 25;
        }
    }
}