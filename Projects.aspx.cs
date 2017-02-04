using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Projects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            int id = Convert.ToInt32(Session["user"]);

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

            cmd.CommandText = "select count(*) from project_network where member_id = " + id + " and project_id in (select project_id from projects where deleted_at is null)";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            da.SelectCommand = cmd;
            da.Fill(dt);

            int c = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (c == 0)
            {
                tab1_msg.InnerHtml = "No Projects To Show";
                tab2_msg.InnerHtml = "No Projects To Show";
                tab3_msg.InnerHtml = "No Projects To Show";
            }
            else
            {
                cmd.CommandText = "select count(*) from projects where project_status = 1 and project_id in (select project_id from project_network where member_id = " + id + ") and deleted_at is null";
                da.SelectCommand = cmd;
                da.Fill(dt1);
                if (Convert.ToInt32(dt1.Rows[0][0].ToString()) != 0)
                {
                    //adds <thead> and <tbody> elements
                    GridView1.UseAccessibleHeader = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    tab1_msg.InnerHtml = "No Projects To Show";
                }

                cmd.CommandText = "select count(*) from projects where project_status = 3 and project_id in (select project_id from project_network where member_id = " + id + ") and deleted_at is null";
                da.SelectCommand = cmd;
                da.Fill(dt2);
                if (Convert.ToInt32(dt2.Rows[0][0].ToString()) != 0)
                {
                    //adds <thead> and <tbody> elements
                    GridView2.UseAccessibleHeader = true;
                    GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    tab2_msg.InnerHtml = "No Projects To Show";
                }

                cmd.CommandText = "select count(*) from projects where project_status = 2 and project_id in (select project_id from project_network where member_id = " + id + ") and deleted_at is null";
                da.SelectCommand = cmd;
                da.Fill(dt3);
                if (Convert.ToInt32(dt3.Rows[0][0].ToString()) != 0)
                {
                    //adds <thead> and <tbody> elements
                    GridView3.UseAccessibleHeader = true;
                    GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    tab3_msg.InnerHtml = "No Projects To Show";
                }
            }
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Convert.ToInt32(Session["canViewFlag"]) == 0)
        {
            Session["canViewFlag"] = 1;
            AccessBox.Style["display"] = "inherit";
        }
        if (Convert.ToInt32(Session["canEditFlag"]) == 0)
        {
            Session["canEditFlag"] = 1;
            AccessBox.Style["display"] = "inherit";
        }
        if (Convert.ToInt32(Session["canDeleteFlag"]) == 0)
        {
            Session["canDeleteFlag"] = 1;
            AccessBox.Style["display"] = "inherit";
        }
        Session["pid"] = null;
        Session["projRole"] = null;
    }

    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[2];
            if (statusCell.Text == "Active")
            {
                statusCell.Text = "<span class='label label-warning' style='line-height:3'>Active</span>";
            }
            if (statusCell.Text == "Completed")
            {
                statusCell.Text = "<span class='label label-success' style='line-height:3'>Completed</span>";
            }
            if (statusCell.Text == "Inactive")
            {
                statusCell.Text = "<span class='label label-info' style='line-height:3'>Inactive</span>";
            }
        }*/
    }

    protected void viewProject(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string pid = btn.CommandArgument;
        Session["pid"] = pid;
        setRole(Convert.ToInt32(pid));
        Response.Redirect("ProjectDetails.aspx");
    }

    protected void editProject(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string pid = btn.CommandArgument;
        Session["pid"] = pid;
        setRole(Convert.ToInt32(pid));
        Response.Redirect("EditProject.aspx");
    }

    protected void deleteProject(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        int pid = Convert.ToInt32(btn.CommandArgument);
        setRole(pid);
        int prole = Convert.ToInt32(Session["projRole"]);
        if (prole == 2)
        {
            Session["canDeleteFlag"] = 0;
            Response.Redirect("Projects.aspx");
        }
        else
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

            int id = Convert.ToInt32(Session["user"]);

            string s1 = "update projects set deleted_at = GETDATE() where project_id = " + pid;
            string s2 = "update tasks set deleted_at = GETDATE() where project_id = " + pid;
            SqlCommand cmd = new SqlCommand(s1, con);
            SqlCommand cmd2 = new SqlCommand(s2, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            con.Close();
            Response.Redirect("Projects.aspx");
        }
    }

    protected void showTasks(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string pid = btn.CommandArgument;
        Session["selectedProjectForTask"] = pid;
        //setRole(Convert.ToInt32(pid));
        Response.Redirect("Tasks.aspx");
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