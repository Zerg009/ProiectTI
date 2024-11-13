using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            if (!IsPostBack)
            {
                //string defaultPassword = "STUDENT";
                //string hashedPassword = PasswordHelper.HashPassword(defaultPassword);

                //using (var connection = new OracleConnection(connString))
                //{
                //    connection.Open();
                //    using (var command = new OracleCommand("UPDATE Parametri_Financiari SET PAROLA_CRIPTATA = :hashedPassword WHERE ID = 1", connection))
                //    {
                //        command.Parameters.Add(new OracleParameter("hashedPassword", hashedPassword));
                //        command.ExecuteNonQuery();
                //    }
                //}

                LoadParameters();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal();", true);
            }
        }
        private void LoadParameters()
        {
            using (var connection = new OracleConnection(connString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT CAS_PENSIE, CASS_SANATATE, IMPOZIT FROM Parametri_Financiari WHERE ID = 1", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtCASPensii.Text = reader["CAS_PENSIE"].ToString();
                            txtCASSanatate.Text = reader["CASS_SANATATE"].ToString();
                            txtImpozit.Text = reader["IMPOZIT"].ToString();
                        }
                    }
                }
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
            decimal? casPensii = string.IsNullOrWhiteSpace(txtCASPensii.Text) ? (decimal?)null : Convert.ToDecimal(txtCASPensii.Text);
            decimal? cassSanatate = string.IsNullOrWhiteSpace(txtCASSanatate.Text) ? (decimal?)null : Convert.ToDecimal(txtCASSanatate.Text);
            decimal? impozit = string.IsNullOrWhiteSpace(txtImpozit.Text) ? (decimal?)null : Convert.ToDecimal(txtImpozit.Text);

            if (!casPensii.HasValue && !cassSanatate.HasValue && !impozit.HasValue)
            {
                lblMessage.Text = "Please enter at least one valid number.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
                return;
            }

            using (var connection = new OracleConnection(connString))
            {
                connection.Open();
                using (var command = new OracleCommand("UPDATE Parametri_Financiari SET CAS_PENSIE = COALESCE(:casPensii, CAS_PENSIE), CASS_SANATATE = COALESCE(:cassSanatate, CASS_SANATATE), IMPOZIT = COALESCE(:impozit, IMPOZIT) WHERE ID = 1", connection))
                {
                    // Explicitly set the parameter data types
                    command.Parameters.Add(new OracleParameter("casPensii", OracleDbType.Decimal, casPensii.HasValue ? (object)casPensii.Value : DBNull.Value, ParameterDirection.Input));
                    command.Parameters.Add(new OracleParameter("cassSanatate", OracleDbType.Decimal, cassSanatate.HasValue ? (object)cassSanatate.Value : DBNull.Value, ParameterDirection.Input));
                    command.Parameters.Add(new OracleParameter("impozit", OracleDbType.Decimal, impozit.HasValue ? (object)impozit.Value : DBNull.Value, ParameterDirection.Input));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lblMessage.Text = "Parameters updated successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "Update failed. Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Visible = true;
                    }
                }
            }
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "closeModal();", true);
        }
        protected void btnCloseChangePasswordModal_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeChangePasswordModal", "closeChangePasswordModal();", true);
        }

        protected void btnSubmitNewPassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Validate passwords
            if (newPassword != confirmPassword)
            {
                lblChangePasswordError.Text = "New password and confirmation do not match.";
                lblChangePasswordError.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showChangePasswordModal", "showChangePasswordModal();", true);
                return;
            }

            // Verify the current password
            string storedHashedPassword = GetStoredHashedPasswordFromDB(); // Retrieve current hashed password
            if (!PasswordHelper.VerifyPassword(currentPassword, storedHashedPassword))
            {
                lblChangePasswordError.Text = "Current password is incorrect.";
                lblChangePasswordError.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showChangePasswordModal", "showChangePasswordModal();", true);
                return;
            }

            // Update the password
            string newHashedPassword = PasswordHelper.HashPassword(newPassword);
            bool updateSuccess = UpdatePasswordInDB(newHashedPassword); // Save new hashed password to DB

            if (updateSuccess)
            {
                lblChangePasswordError.Text = "Password changed successfully!";
                lblChangePasswordError.ForeColor = System.Drawing.Color.Green;
                lblChangePasswordError.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeChangePasswordModal", "closeChangePasswordModal();", true);
            }
            else
            {
                lblChangePasswordError.Text = "An error occurred while updating the password. Please try again.";
                lblChangePasswordError.Visible = true;
            }
        }
        private bool UpdatePasswordInDB(string newHashedPassword)
        {
            bool updateSuccess = false;

            string query = "UPDATE Parametri_Financiari SET PAROLA_CRIPTATA = :newPassword WHERE ID = 1";

            using (OracleConnection connection = new OracleConnection(connString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("newPassword", newHashedPassword));

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        updateSuccess = rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            return updateSuccess;
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