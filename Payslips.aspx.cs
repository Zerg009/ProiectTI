using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public partial class WebForm6 : TableBaseClass
    {
        protected override DropDownList ddlPageSize => PageSizeDropdown;
        protected override GridView gridViewEmployees => gvEmployees;
        protected override TextBox txtPageNumber => PageNumberTextbox;
        protected override TextBox textSearch => txtSearch;
        protected override Button btnPreviousPage => PreviousPageButton;
        protected override Button btnNextPage => NextPageButton;
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        protected void btnPrintAll_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = @"SELECT 
                                    NR_CRT,
                                    NUME,
                                    PRENUME,
                                    FUNCTIE,
                                    SALAR_BAZA,
                                    SPOR,
                                    PREMII_BRUTE,
                                    TOTAL_BRUT,
                                    BRUT_IMPOZABIL,
                                    CAS,
                                    CASS,
                                    IMPOZIT,
                                    RETINERI,
                                    VIRAT_CARD
                                FROM
                                    Salarii_Angajati";

                OracleDataAdapter da = new OracleDataAdapter(sqlQuery, connString);
                DataSet ds = new DataSet();
                da.Fill(ds, "table");

                ReportDocument report = new ReportDocument();
                string path = Server.MapPath("CrystalReport2.rpt");
                report.Load(path);
                report.SetDataSource(ds.Tables["table"]);


                string currentMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
                string currentYear = DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture);
                string payslipsFolderPath = Server.MapPath($"~/Payslips_{currentYear}");
                string filePath = Path.Combine(payslipsFolderPath, $"Fluturasi_{currentMonth}_{currentYear}.pdf");


                if (!Directory.Exists(payslipsFolderPath))
                {
                    Directory.CreateDirectory(payslipsFolderPath);
                }
                DiskFileDestinationOptions fisier = new DiskFileDestinationOptions();
                report.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                report.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                fisier.DiskFileName = filePath;
                report.ExportOptions.DestinationOptions = fisier;
                report.Export();
                lblMessage.Text = "Payslips printed successfully";
                lblMessage.CssClass = "text-success me-3";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "error" + ex.ToString();
                lblMessage.CssClass = "text-danger me-3"; // Error - red
            }
        }
        protected void btnPrintPayslip_Click(object sender, CommandEventArgs e)
        {
            try
            {
                // Get the employee ID from CommandArgument
                int employeeId = Convert.ToInt32(e.CommandArgument);
                string sqlQuery = @"
                                    SELECT 
                                        NR_CRT,
                                        NUME,
                                        PRENUME,
                                        FUNCTIE,
                                        SALAR_BAZA,
                                        SPOR,
                                        PREMII_BRUTE,
                                        TOTAL_BRUT,
                                        BRUT_IMPOZABIL,
                                        CAS,
                                        CASS,
                                        IMPOZIT,
                                        RETINERI,
                                        VIRAT_CARD
                                    FROM
                                        Salarii_Angajati
                                    WHERE NR_CRT = :employeeId"; 

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    OracleDataAdapter da = new OracleDataAdapter(sqlQuery, conn);
                    da.SelectCommand.Parameters.Add(":employeeId", OracleDbType.Int32).Value = employeeId; // Add parameter to command
                    DataSet ds = new DataSet();
                    da.Fill(ds, "table");

                    if (ds.Tables["table"].Rows.Count > 0)
                    {
                        ReportDocument report = new ReportDocument();
                        string path = Server.MapPath("CrystalReport2.rpt");
                        report.Load(path);
                        report.SetDataSource(ds.Tables["table"]);

                        string nume = ds.Tables["table"].Rows[0]["NUME"].ToString();
                        string prenume = ds.Tables["table"].Rows[0]["PRENUME"].ToString(); // Corrected to use PRENUME
                        string currentMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
                        string currentYear = DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture);

                        string payslipsFolderPath = Server.MapPath("~/Payslips");
                        if (!Directory.Exists(payslipsFolderPath))
                        {
                            Directory.CreateDirectory(payslipsFolderPath);
                        }

                        DiskFileDestinationOptions fisier = new DiskFileDestinationOptions();
                        report.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        report.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

                        // Save the file with employee name and current month/year
                        fisier.DiskFileName = Server.MapPath($"~/Payslips_{currentYear}/{nume}_{prenume}_{currentMonth}_{currentYear}.pdf");
                        report.ExportOptions.DestinationOptions = fisier;
                        report.Export();

                        lblMessage.Text = "Payslip generated successfully.";
                        lblMessage.CssClass = "text-success me-3";
                    }
                    else
                    {
                        lblMessage.Text = "No employee found with the provided ID.";
                        lblMessage.CssClass = "text-danger me-3"; // Error - red
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger me-3"; // Error - red
            }
        }
    }
}