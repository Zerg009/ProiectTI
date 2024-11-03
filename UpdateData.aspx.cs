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
    public partial class WebForm1 : TableBaseClass
    {
        protected override DropDownList ddlPageSize => PageSizeDropdown; 
        protected override GridView gridViewEmployees => gvEmployees; 
        protected override TextBox txtPageNumber => PageNumberTextbox; 
        protected override TextBox textSearch => txtSearch; 
        protected override Button btnPreviousPage => PreviousPageButton; 
        protected override Button btnNextPage => NextPageButton;
       
    }
}