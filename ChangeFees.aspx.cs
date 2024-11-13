using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["OracleDbConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string defaultPassword = "STUDENT";
                string hashedPassword = PasswordHelper.HashPassword(defaultPassword);

                using (var connection = new OracleConnection(connString))
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE Parametri_Financiari SET PAROLA_CRIPTATA = :hashedPassword WHERE ID = 1", connection))
                    {
                        command.Parameters.Add(new OracleParameter("hashedPassword", hashedPassword));
                        command.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal();", true);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredPassword = txtModalPassword.Text;

            string storedHashedPassword = GetStoredHashedPasswordFromDB(); 
            if (PasswordHelper.VerifyPassword(enteredPassword, storedHashedPassword))
            {
                pnlParameters.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "closeModal();", true);
            }
            else
            {
                lblErrorMessage.Text = "Incorrect password, please try again.";
                lblErrorMessage.Visible = true; // Show the error message label
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal();", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            decimal casPensii, cassSanatate, impozit;

            
            if (decimal.TryParse(txtCASPensii.Text, out casPensii) &&
                decimal.TryParse(txtCASSanatate.Text, out cassSanatate) &&
                decimal.TryParse(txtImpozit.Text, out impozit))
            {
                
                using (var connection = new OracleConnection(connString))
                {
                    connection.Open();
                    using (var command = new OracleCommand("UPDATE Parametri_Financiari SET CAS_PENSIE = :casPensii, CASS_SANTATE = :cassSanatate, IMPOZIT = :impozit WHERE ID = 1", connection))
                    {
                        
                        command.Parameters.Add(new OracleParameter("casPensii", casPensii));
                        command.Parameters.Add(new OracleParameter("cassSanatate", cassSanatate));
                        command.Parameters.Add(new OracleParameter("impozit", impozit));

                       
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateSuccess", "alert('Parameters updated successfully!');", true);
                        }
                        else
                        {
                            
                            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateFail", "alert('Update failed. Please try again.');", true);
                        }
                    }
                }
            }
            else
            {
                // Display error message for invalid input
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('Please enter valid numbers for all fields.');", true);
            }
        }
        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "closeModal();", true);
        }
        private string GetStoredHashedPasswordFromDB()
        {
            string hashedPassword = "";
            using (var connection = new OracleConnection(connString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT PAROLA_CRIPTATA FROM Parametri_Financiari WHERE ID = 1", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetString(0);
                        }
                    }
                }
            }
            return hashedPassword;
        }
    }
}