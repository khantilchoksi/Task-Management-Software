using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class NetworkProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["user2"]=Request.QueryString["q"];

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable connect_dt = new DataTable();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
       // GridView1.UseAccessibleHeader = true;
       // GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

        DisplayGrid();

        int id = Convert.ToInt32(Session["user2"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select first_name+' '+last_name,img_path,email_id,mobile,description from users where user_id = " + id;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        
            string imagePath = dt.Rows[0][1].ToString();
            img1.Attributes["src"] = "uploads/users/" + imagePath;
            userName2.InnerHtml = dt.Rows[0][0].ToString();
            email1.InnerHtml = dt.Rows[0][2].ToString();
            mobile1.InnerHtml = dt.Rows[0][3].ToString();
            notes.InnerHtml = dt.Rows[0][4].ToString();
            Page.Title = dt.Rows[0][0].ToString();

            HtmlGenericControl current = new HtmlGenericControl("li");
            bread.Controls.Add(current);

            current.Attributes.Add("class", "active");
            current.InnerHtml = dt.Rows[0][0].ToString();

            cmd.CommandText = "select count(distinct user_id) from users where user_id in ((select member_1 from network_members where member_2=" + id + " and invite_status=2) union (select member_2 from network_members where member_1=" + id + " and invite_status=2))";
            da.SelectCommand = cmd;
            da.Fill(connect_dt);

            connectNum.InnerHtml = connect_dt.Rows[0][0].ToString();
       
    }

    protected void DisplayGrid()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
    
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
         //GridView1.UseAccessibleHeader = true;
         //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

        //DisplayGrid();

        int id2 = Convert.ToInt32(Session["user2"]);
        int id = Convert.ToInt32(Session["user"]);
        
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select count(*) from projects where project_id in ((select project_id from project_network where member_id=" + id2 + ") intersect (select project_id  from project_network where member_id=" + id + "))";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        if (Convert.ToInt32(dt.Rows[0][0]) != 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            noProj.InnerHtml = "No Projects Shared";           
        }
    }

    protected void viewProject(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string pid = btn.CommandArgument;
        Session["pid"] = pid;
        setRole(Convert.ToInt32(pid));
        Response.Redirect("ProjectDetails.aspx");
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
            TableCell projStatusCell = e.Row.Cells[2];

            cmd.CommandText = "select status_name from project_status where status_id = " + projStatusCell.Text;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            da.SelectCommand = cmd;
            da.Fill(dt);

            switch (projStatusCell.Text)
            {
                case "1":   //Status Active 
                    projStatusCell.Text = "<span class='label label-info' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
                    break;
                case "2":   //Status Completed
                    projStatusCell.Text = "<span class='label label-success' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
                    break;
                case "3":   //Inactive
                    projStatusCell.Text = "<span class='label label-warning' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
                    break;

            }
        }
    }

    protected void setRole(int pid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select role_id,read_right,write_right from project_network where member_id = " + id + " and project_id = " + pid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        Session["projRole"] = dt.Rows[0][0].ToString();
        Session["canViewFlag"] = dt.Rows[0][1].ToString();
        Session["canEditFlag"] = dt.Rows[0][2].ToString();
    }
}