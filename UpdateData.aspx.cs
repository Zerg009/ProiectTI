using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Paging event
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();  // Rebind data to reflect the new page index
        }
        // Sorting event
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataView dataView = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
        private string GetSortDirection(string column)
        {
            // Default sort direction is ascending
            string sortDirection = "ASC";
            string previousSortExpression = ViewState["SortExpression"] as string;

            if (previousSortExpression != null && previousSortExpression == column)
            {
                // Reverse sort direction if the same column is sorted again
                sortDirection = ViewState["SortDirection"] as string == "ASC" ? "DESC" : "ASC";
            }

            // Save new sort expression and direction in ViewState
            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;

            return sortDirection;
        }
        // Edit event
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();  // Rebind data to enter edit mode
        }
        // Update event
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int NR_CRT = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["NR_CRT"]);
            string NUME = (GridView1.Rows[e.RowIndex].FindControl("NUME") as TextBox).Text;
            string PRENUME = (GridView1.Rows[e.RowIndex].FindControl("PRENUME") as TextBox).Text;
            string VIRAT_CARD = (GridView1.Rows[e.RowIndex].FindControl("VIRAT_CARD") as TextBox).Text;

            SqlDataSource1.UpdateParameters["NR_CRT"].DefaultValue = NR_CRT.ToString();
            SqlDataSource1.UpdateParameters["NUME"].DefaultValue = NUME;
            SqlDataSource1.UpdateParameters["PRENUME"].DefaultValue = PRENUME;
            SqlDataSource1.UpdateParameters["VIRAT_CARD"].DefaultValue = VIRAT_CARD;

            SqlDataSource1.Update();  // Trigger the update command

            GridView1.EditIndex = -1;  // Exit edit mode
            GridView1.DataBind();  // Rebind data to reflect updates
        }
        // Cancel Edit event
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();  // Rebind data to exit edit mode
        }
    }
}