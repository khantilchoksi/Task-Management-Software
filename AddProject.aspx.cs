using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class AddProject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        if (!IsPostBack)
        {
            populateMemberCheckBoxList();
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Session["pid"] = null;
        Session["projRole"] = null;
    }

    private void populateMemberCheckBoxList()
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id, first_name+' '+last_name as Member from users where user_id in ((select member_2 from network_members where member_1 = " + id + " and invite_status=2) union (select member_1 from network_members where member_2 = " + id + " and invite_status=2))";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            CheckBoxList1.DataSource = dt;
            CheckBoxList1.DataValueField = "user_id";
            CheckBoxList1.DataTextField = "Member";
            CheckBoxList1.DataBind();
        }
        else
        {
            memberList.InnerHtml = "No Members in Network, <a href='MyNetwork.aspx'>Invite Members</a> to add in your Network";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (checkProjectNameAvailable())
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

            int id = Convert.ToInt32(Session["user"]);

            int pr_id = 1 + getMaxProjID();

            //insert command for projects
            string s1 = "insert into projects(project_title,project_desc,project_status,created_by,created_at) values(@ptitle,@pdesc,1,"+id+",GETDATE())";
            
            //insert command for adding admin to project network
            string s2 = "insert into project_network(project_id,member_id,role_id,joined_on,read_right,write_right) values(" + pr_id + "," + id + ",1,GETDATE(),1,1)";

            //insert command for adding members to project network
            string s3 = "insert into project_network(project_id,member_id,role_id,joined_on,read_right,write_right) values(" + pr_id + ",@member,2,GETDATE(),1,0)";
            
            SqlCommand cmd1 = new SqlCommand(s1, con);
            cmd1.Parameters.AddWithValue("@ptitle", TextBox1.Text);
            cmd1.Parameters.AddWithValue("@pdesc", TextBox2.Text);

            SqlCommand cmd2 = new SqlCommand(s2, con);
            SqlCommand cmd3 = new SqlCommand(s3, con);
            
            con.Open();
            
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            List<ListItem> selected = new List<ListItem>();
            foreach (ListItem item in CheckBoxList1.Items)
            {
                cmd3.Parameters.Clear();
                cmd3.Parameters.AddWithValue("@member", item.Value);
                if (item.Selected)
                {
                    cmd3.ExecuteNonQuery();
                }
            }

            con.Close();
            Session["pid"] = pr_id;
            Session["projRole"] = 1;
            Session["canViewFlag"] = 1;
            Response.Redirect("ProjectDetails.aspx");
        }

    }

    private bool checkProjectNameAvailable()
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


        cmd.CommandText = "select * from projects where project_title = @ptitle";
        cmd.Parameters.AddWithValue("@ptitle", TextBox1.Text);
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            error1.Style["display"] = "";
            error1.Style["color"] = "#dd4b39";
            error1.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
            ptitle.Style["color"] = "#dd4b39";
            return false;
        }
        else
        {
            return true;
        }
    }

    private int getMaxProjID()
    {
 	    SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select max(project_id) from projects";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        int projNo = Convert.ToInt32(dt.Rows[0][0].ToString());
        return projNo;
    }

}