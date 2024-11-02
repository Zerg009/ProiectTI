using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["OracleDbConnection"].ConnectionString;
        private int PageSize => int.Parse(PageSizeDropdown.SelectedValue);
        private int CurrentPage
        {
            get
            {
                return ViewState["CurrentPage"] != null ? Convert.ToInt32(ViewState["CurrentPage"]) : 1;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        private int TotalEntries
        {
            get
            {
                return ViewState["TotalEntries"] != null ? Convert.ToInt32(ViewState["TotalEntries"]) : 1;
            }
            set
            {
                ViewState["TotalEntries"] = value;
            }
        }
        private int TotalPages => (int)Math.Ceiling((double)TotalEntries / PageSize);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                UpdatePaginationControls();
            }
        }

        private void BindGrid(string searchTerm = "", string sortExpression = "")
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();

                // Get total entries count
                string countQuery = "SELECT COUNT(*) FROM Salarii_Angajati WHERE NUME LIKE :searchTerm";
                using (OracleCommand countCommand = new OracleCommand(countQuery, connection))
                {
                    countCommand.Parameters.Add(":searchTerm", $"%{searchTerm}%");
                    TotalEntries = Convert.ToInt32(countCommand.ExecuteScalar());
                }

                // Calculate the starting row and the number of rows to fetch
                int startRow = (CurrentPage - 1) * PageSize + 1;
                int endRow = CurrentPage * PageSize;

                // Build the query with pagination
                string query = @"
                    SELECT * FROM (
                        SELECT 
                            Salarii_Angajati.*, 
                            ROW_NUMBER() OVER (ORDER BY NR_CRT) AS row_num 
                        FROM Salarii_Angajati 
                        WHERE UPPER(NUME) LIKE UPPER(:searchTerm)
                    ) 
                    WHERE row_num BETWEEN :startRow AND :endRow";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(":searchTerm", $"%{searchTerm}%");
                    command.Parameters.Add(":startRow", startRow);
                    command.Parameters.Add(":endRow", endRow);
                    
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gvEmployees.DataSource = dt;
                        gvEmployees.DataBind();
                        
                    }
                }

            }
        }
        private void UpdatePaginationControls()
        {
            
            PageNumberTextbox.Text = CurrentPage.ToString();
            PreviousPageButton.Enabled = CurrentPage > 1;
            NextPageButton.Enabled = CurrentPage < TotalPages;
        }
        protected void PageSizeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            BindGrid();
            UpdatePaginationControls();
        }

        protected void PageNumberTextbox_TextChanged(object sender, EventArgs e)
        {
            int page;
            if (int.TryParse(PageNumberTextbox.Text, out page) && page > 0 && page <= TotalPages)
            {
                CurrentPage = page;
            }
            else
            {
                CurrentPage = 1;
            }
            BindGrid();
            UpdatePaginationControls();
        }

        protected void PreviousPageButton_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                BindGrid();
                UpdatePaginationControls();
            }
        }

        protected void NextPageButton_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                BindGrid();
                UpdatePaginationControls();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            BindGrid(); // Method to re-bind the data
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int nrCrt = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
            string nume = ((TextBox)gvEmployees.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            decimal salarBaza = Convert.ToDecimal(((TextBox)gvEmployees.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            decimal spor = Convert.ToDecimal(((TextBox)gvEmployees.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
            decimal premiiBrute = Convert.ToDecimal(((TextBox)gvEmployees.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
            decimal retin = Convert.ToDecimal(((TextBox)gvEmployees.Rows[e.RowIndex].Cells[5].Controls[0]).Text);

            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("UPDATE Salarii_Angajati SET NUME = :nume, SALAR_BAZA = :salarBaza, SPOR = :spor, PREMII_BRUTE = :premiiBrute, RETINERI = :retin WHERE NR_CRT = :nrCrt", connection))
                {
                    command.Parameters.Add(":nume", nume);
                    command.Parameters.Add(":salarBaza", salarBaza);
                    command.Parameters.Add(":spor", spor);
                    command.Parameters.Add(":premiiBrute", premiiBrute);
                    command.Parameters.Add(":retin", retin);
                    command.Parameters.Add(":nrCrt", nrCrt);
                    command.ExecuteNonQuery();
                }
            }

            gvEmployees.EditIndex = -1;
            BindGrid(txtSearch.Text.Trim());
        }
        protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = ViewState["SortDirection"] != null && ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
            ViewState["SortDirection"] = sortDirection;

            BindGrid(txtSearch.Text.Trim(), e.SortExpression + " " + sortDirection);
        }


        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmployees.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtSalariuBaza = (TextBox)row.FindControl("txtSALAR_BAZA");
                    TextBox txtSpor = (TextBox)row.FindControl("txtSPOR");
                    TextBox txtPremiiBrute = (TextBox)row.FindControl("txtPREMII_BRUTE");
                    TextBox txtRetineri = (TextBox)row.FindControl("txtRETINERI");

                    int salariuBaza = int.Parse(txtSalariuBaza.Text);
                    int spor = int.Parse(txtSpor.Text);
                    int premiiBrute = int.Parse(txtPremiiBrute.Text);
                    int retineri = int.Parse(txtRetineri.Text);
                    int employeeId = Convert.ToInt32(gvEmployees.DataKeys[row.RowIndex].Value);

                    // Perform your database update logic here
                    using (var connection = new OracleConnection(connString))
                    {
                        connection.Open();
                        using (var command = new OracleCommand(
                            @"UPDATE Salarii_Angajati SET SALAR_BAZA = :salariuBaza, 
                            SPOR = :spor, PREMII_BRUTE = :premiiBrute, 
                            RETINERI = :retineri WHERE NR_CRT = :employeeId",
                            connection
                        ))
                        {
                            command.Parameters.Add(new OracleParameter("salariuBaza", salariuBaza));
                            command.Parameters.Add(new OracleParameter("spor", spor));
                            command.Parameters.Add(new OracleParameter("premiiBrute", premiiBrute));
                            command.Parameters.Add(new OracleParameter("retineri", retineri));
                            command.Parameters.Add(new OracleParameter("employeeId", employeeId));

                            command.ExecuteNonQuery();
                        }
                        // Call the recalculation procedure immediately after the update
                        using (var commandRecalc = new OracleCommand(
                            "BEGIN Initiate_Recalculations(:employeeId); END;",
                            connection))
                        {
                            commandRecalc.Parameters.Add(new OracleParameter("employeeId", employeeId));
                            commandRecalc.ExecuteNonQuery();
                        }
                    }
                }
            }


            BindGrid(); // Refresh the GridView after saving changes
        }
    }
}