using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace WebApplication1
{
    public partial class WebForm3 : TableBaseClass
    {
        protected override DropDownList ddlPageSize => PageSizeDropdown;
        protected override GridView gridViewEmployees => gvEmployees;
        protected override TextBox txtPageNumber => PageNumberTextbox;
        protected override TextBox textSearch => txtSearch;
        protected override Button btnPreviousPage => PreviousPageButton;
        protected override Button btnNextPage => NextPageButton;

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int employeeId;
            if (int.TryParse(hdnEmployeeId.Value, out employeeId))
            {
                // Delete the employee directly from the database
                DeleteEmployee(employeeId);

                string searchTerm = txtSearch.Text;
                // Rebind the grid to reflect the changes
                BindGrid(searchTerm);
            }

            // Clear the hidden field value
            hdnEmployeeId.Value = string.Empty;
        }

        private void DeleteEmployee(int employeeId)
        {
            using (var connection = new OracleConnection(connString))
            {
                connection.Open();
                using (var command = new OracleCommand("DELETE FROM Salarii_Angajati WHERE NR_CRT = :employeeId", connection))
                {
                    command.Parameters.Add(new OracleParameter("employeeId", employeeId));
                    command.ExecuteNonQuery();
                }
            }
        }
        protected void btnCancelDelete_Click(object sender, EventArgs e)
        {
            // Optionally, you can handle any logic needed when canceling
        }
    }
}