using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
namespace WebApplication1
{
    public partial class WebForm7 : TableBaseClass
    {
        protected override DropDownList ddlPageSize => PageSizeDropdown;
        protected override GridView gridViewEmployees => gvEmployees;
        protected override TextBox txtPageNumber => PageNumberTextbox;
        protected override TextBox textSearch => txtSearch;
        protected override Button btnPreviousPage => PreviousPageButton;
        protected override Button btnNextPage => NextPageButton;

        //protected override void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        GetEmployeeDetails(1);
        //    }
        //}

        [WebMethod]
        public static string GetEmployeeDetails(int employeeId)
        {
            string query = @"
        SELECT NUME, PRENUME, FUNCTIE, POZA 
        FROM Salarii_Angajati 
        WHERE NR_CRT = :employeeId";

            Employee employee = null;

            try
            {
                using (OracleConnection connection = new OracleConnection(connString))
                {
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.BindByName = true;
                        command.Parameters.Add(":employeeId", employeeId);

                        connection.Open();

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new Employee
                                {
                                    Nume = reader["NUME"].ToString(),
                                    Prenume = reader["PRENUME"].ToString(),
                                    Functie = reader["FUNCTIE"].ToString(),
                                    Poza = reader["POZA"] != DBNull.Value ? (byte[])reader["POZA"] : null
                                };
                            }
                            else
                            {
                                // Log when no data is found for the given employeeId
                                System.Diagnostics.Debug.WriteLine($"No employee found for ID: {employeeId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions to help diagnose the issue
                System.Diagnostics.Debug.WriteLine($"Error in GetEmployeeDetails: {ex.Message}");
            }


            return JsonConvert.SerializeObject(employee);
        }

        public class Employee
        {
            public string Nume { get; set; }
            public string Prenume { get; set; }
            public string Functie { get; set; }
            public byte[] Poza { get; set; }
        }
    }
}