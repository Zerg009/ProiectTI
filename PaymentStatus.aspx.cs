using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument report = new ReportDocument();
            string path = Server.MapPath("CrystalReport1.rpt");
            report.Load(path);
            //raport.SetDataSource(ds.Tables["tabela"]);
            CrystalReportViewer1.ReportSource = report;
        }
    }
}