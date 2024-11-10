using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
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
            if (!IsPostBack)
            {
                ReportDocument report = new ReportDocument();
                string path = Server.MapPath("CrystalReport1.rpt");
                report.Load(path);

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                
            }
        }
    }
}