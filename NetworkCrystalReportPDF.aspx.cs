using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class NetworkCrystalReportPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument cryRpt = new ReportDocument();
        cryRpt.Load(Server.MapPath("~/NetworkCrystalReport.rpt"));
        cryRpt.SetDataSource(GetData());

        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "NetworkContacts");
    }

    private NetworkDataSet GetData()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        NetworkDataSet ds = new NetworkDataSet();

        cmd.CommandText = "select first_name+' '+last_name as Name, email_id as Email, mobile as Mobile from users where user_id in ((select member_1 from network_members where member_2= @userid and invite_status=2) union (select member_2 from network_members where member_1= @userid and invite_status=2))";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@userid", Convert.ToInt32(Session["user"]));
        da.SelectCommand = cmd;
        da.Fill(ds, "DataTable1");

        return ds;

    }
}