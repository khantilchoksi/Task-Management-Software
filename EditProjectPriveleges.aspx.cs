using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class EditProjectPriveleges : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            int pid = Convert.ToInt32(Session["pid"]);

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
           
            cmd.CommandText = "select project_title from projects where project_id = " + pid;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);

            ptitle.Controls.Add(new LiteralControl(dt.Rows[0][0].ToString()));

            cmd.CommandText = "select member_id from project_network where project_id = " + pid;
            da.SelectCommand = cmd;
            da.Fill(dt1);

            if (dt1.Rows.Count == 1)
            {
                boxBody.InnerHtml = "No Members in Project";
            }

            if (Convert.ToInt32(Session["successFlag"]) == 1)
            {
                Session["successFlag"] = 0;
                SuccessBox.Style["display"] = "inherit";
            }
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["pid"] == null)
        {
            Response.Redirect("Projects.aspx");
        }
        if (Convert.ToInt32(Session["projRole"]) != 1)
        {
            Response.Redirect("ProjectDetails.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int pid = Convert.ToInt32(Session["pid"]);

        string s1 = "update project_network set read_right = @read, write_right=@write where project_id=" + pid + " and member_id=@member";

        SqlCommand cmd = new SqlCommand(s1, con);
        con.Open();

        foreach (GridViewRow row in GridView1.Rows)
        {
            //Get the Member Id from the DataKey property.
            int member_id = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);

            //Get the checked value of the CheckBox.
            bool read_right = (row.FindControl("read") as CheckBox).Checked;
            bool write_right = (row.FindControl("write") as CheckBox).Checked;

            //Save to database
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@read", read_right);
            cmd.Parameters.AddWithValue("@write", write_right);
            cmd.Parameters.AddWithValue("@member", member_id);
            cmd.ExecuteNonQuery();
        }
        Session["successFlag"] = 1;
        Response.Redirect("EditProjectPriveleges.aspx");
        con.Close();
    }
}