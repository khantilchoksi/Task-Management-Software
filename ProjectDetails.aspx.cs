using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class ProjectDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int pid = Convert.ToInt32(Session["pid"]), id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select * from projects where project_id = " + pid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        descBody.InnerHtml = dt.Rows[0][3].ToString();
        ptitle.InnerHtml = dt.Rows[0][1].ToString();

        loadUsers();

        if (Convert.ToInt32(Session["projRole"]) == 1)
        {
            priv.Style["display"] = "block";
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Session["pid"] == null || Session["projRole"] == null || Convert.ToInt32(Session["canViewFlag"]) == 0)
        {
            Response.Redirect("Projects.aspx");
        }
        Session["canEditFlag"] = 1;
    }



    protected void loadUsers()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["pid"]),role;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id, first_name+' '+last_name as Member, img_path from users where user_id in (select member_id from project_network where project_id = " + id + ")";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            users_list.Controls.Add(li);

            HtmlGenericControl image = new HtmlGenericControl("img");
            image.Attributes.Add("src", "uploads/users/"+dt.Rows[i][2].ToString());
            image.Attributes.Add("alt", "User Image");

            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("class", "users-list-name");
            if (dt.Rows[i][0].ToString() == Session["user"].ToString())
            {
                anchor.Attributes.Add("href", "javascript:void(0)");
                anchor.InnerText = "You";
            }
            else
            {
                anchor.Attributes.Add("href", "NetworkProfile.aspx?q=" + dt.Rows[i][0]);
                anchor.InnerText = dt.Rows[i][1].ToString();
            }

            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("class", "users-list-date");
            string username = Session["userName"].ToString();

            role = getProjectRole(Convert.ToInt32(dt.Rows[i][0]));

            if(role == 1) 
            {
                span.InnerText = "Admin";
            }
            else 
            {
                span.InnerText = "Member";
            }

            li.Controls.Add(image);
            li.Controls.Add(anchor);
            li.Controls.Add(span);

        }
    }

    private int getProjectRole(int user)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int pid = Convert.ToInt32(Session["pid"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select role_id from project_network where project_id = " + pid + " and member_id = " + user;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        return Convert.ToInt32(dt.Rows[0][0]);
    }
    protected void DetailsView1_DataBound(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select status_name from project_status where status_id = " + DetailsView1.Rows[1].Cells[1].Text;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        switch (DetailsView1.Rows[1].Cells[1].Text)
        {
            case "1":   //Status Active 
                DetailsView1.Rows[1].Cells[1].Text = "<span class='label label-info' style='line-height:3;'>" + dt.Rows[0][0].ToString() + "</span>";
                break;
            case "2":   //Status Completed
                DetailsView1.Rows[1].Cells[1].Text = "<span class='label label-success' style='line-height:3;'>" + dt.Rows[0][0].ToString() + "</span>";
                break;
            case "3":   //Status Inactive
                DetailsView1.Rows[1].Cells[1].Text = "<span class='label label-warning' style='line-height:3;'>" + dt.Rows[0][0].ToString() + "</span>";
                break;
        }
    }
}