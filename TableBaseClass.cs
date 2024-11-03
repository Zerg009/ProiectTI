using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public abstract class TableBaseClass : System.Web.UI.Page
    {
        protected string connString = ConfigurationManager.ConnectionStrings["OracleDbConnection"].ConnectionString;
        protected abstract DropDownList ddlPageSize { get; }
        protected abstract GridView gridViewEmployees { get; }
        protected abstract TextBox txtPageNumber { get; }
        protected abstract TextBox textSearch { get; }
        protected abstract Button btnPreviousPage { get; }
        protected abstract Button btnNextPage { get; }

        private int PageSize => int.Parse(ddlPageSize.SelectedValue);

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


        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                UpdatePaginationControls();
            }
        }

        protected virtual void BindGrid(string searchTerm = "", string sortExpression = "")
        {
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();

     
                string countQuery = "SELECT COUNT(*) FROM Salarii_Angajati WHERE UPPER(NUME) LIKE UPPER(:searchTerm) OR UPPER(PRENUME) LIKE UPPER(:searchTerm)";
                using (OracleCommand countCommand = new OracleCommand(countQuery, connection))
                {
                    countCommand.BindByName = true;
                    countCommand.Parameters.Add(":searchTerm", $"%{searchTerm}%");
                    TotalEntries = Convert.ToInt32(countCommand.ExecuteScalar());
                }


                if (!string.IsNullOrEmpty(searchTerm) || TotalEntries == 0)
                {
                    CurrentPage = 1;
                    txtPageNumber.Text = CurrentPage.ToString();
                    UpdatePaginationControls();
                }

                
                int startRow = (CurrentPage - 1) * PageSize + 1;
                int endRow = Math.Min(CurrentPage * PageSize, TotalEntries);

                
                string query = @"
                    SELECT * FROM (
                        SELECT 
                            Salarii_Angajati.*, 
                            ROW_NUMBER() OVER (ORDER BY NR_CRT) AS row_num 
                        FROM Salarii_Angajati 
                        WHERE UPPER(NUME) LIKE UPPER(:searchTerm) OR UPPER(PRENUME) LIKE UPPER(:searchTerm)
                    ) 
                    WHERE row_num BETWEEN :startRow AND :endRow";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add(":searchTerm", $"%{searchTerm}%");
                    command.Parameters.Add(":startRow", startRow);
                    command.Parameters.Add(":endRow", endRow);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        gridViewEmployees.DataSource = dt;
                        gridViewEmployees.DataBind();

                    }
                }
            }
            UpdatePaginationControls();
        }
        private void UpdatePaginationControls()
        {

            txtPageNumber.Text = CurrentPage.ToString();
            btnPreviousPage.Enabled = CurrentPage > 1;
            btnNextPage.Enabled = CurrentPage < TotalPages;
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            BindGrid();
            UpdatePaginationControls();
        }

        protected void txtPageNumber_TextChanged(object sender, EventArgs e)
        {
            int page;
            if (int.TryParse(txtPageNumber.Text, out page) && page > 0 && page <= TotalPages)
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

        protected void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                BindGrid();
                UpdatePaginationControls();
            }
        }

        protected void btnNextPage_Click(object sender, EventArgs e)
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
            BindGrid(textSearch.Text.Trim());
        }

        protected void gridViewEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewEmployees.PageIndex = e.NewPageIndex;
            BindGrid(textSearch.Text.Trim());
        }

        protected void gridViewEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewEmployees.EditIndex = e.NewEditIndex;
            BindGrid(); // Method to re-bind the data
        }

        protected void gridViewEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewEmployees.EditIndex = -1;
            BindGrid(textSearch.Text.Trim());
        }

        protected void gridViewEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int nrCrt = Convert.ToInt32(gridViewEmployees.DataKeys[e.RowIndex].Value);
            string nume = ((TextBox)gridViewEmployees.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            decimal salarBaza = Convert.ToDecimal(((TextBox)gridViewEmployees.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
            decimal spor = Convert.ToDecimal(((TextBox)gridViewEmployees.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
            decimal premiiBrute = Convert.ToDecimal(((TextBox)gridViewEmployees.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
            decimal retin = Convert.ToDecimal(((TextBox)gridViewEmployees.Rows[e.RowIndex].Cells[5].Controls[0]).Text);

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

            gridViewEmployees.EditIndex = -1;
            BindGrid(textSearch.Text.Trim());
        }
        protected void gridViewEmployees_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = ViewState["SortDirection"] != null && ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
            ViewState["SortDirection"] = sortDirection;

            BindGrid(textSearch.Text.Trim(), e.SortExpression + " " + sortDirection);
        }


        protected virtual void btnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridViewEmployees.Rows)
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
                    int employeeId = Convert.ToInt32(gridViewEmployees.DataKeys[row.RowIndex].Value);

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