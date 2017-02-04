using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EditProject : System.Web.UI.Page
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

            int pid = Convert.ToInt32(Session["pid"]);

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
            cmd.CommandText = "select * from projects where project_id = " + pid;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);

            TextBox1.Text = dt.Rows[0][1].ToString();
            TextBox2.Text = dt.Rows[0][3].ToString();

            loadStatusList();

            DropDownList1.SelectedValue = dt.Rows[0][4].ToString();
            populateMemberCheckBoxList();
            
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Convert.ToInt32(Session["canEditFlag"]) == 0)
        {
            Response.Redirect("Projects.aspx");
        }
        if (Convert.ToInt32(Session["pid"]) == null)
        {
            Response.Redirect("Projects.aspx");
        }
        Session["canViewFlag"] = 1;
    }

    private void populateMemberCheckBoxList()
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        int id = Convert.ToInt32(Session["user"]);
        int pid = Convert.ToInt32(Session["pid"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        if (Convert.ToInt32(Session["projRole"]) == 1)
        {
            cmd.CommandText = "select user_id, first_name+' '+last_name as Member from users where user_id in ((select member_2 from network_members where member_1 = " + id + " and invite_status=2) union (select member_1 from network_members where member_2 = " + id + " and invite_status=2))";
        }
        else
        {
            cmd.CommandText = "select user_id, first_name+' '+last_name as Member from users where user_id in (((select member_2 from network_members where member_1 = " + id + " and invite_status=2) union (select member_1 from network_members where member_2 = " + id + " and invite_status=2)) except (select member_id from project_network where project_id = " + pid + " and role_id = 1))";
        }
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        CheckBoxList1.DataSource = dt;
        CheckBoxList1.DataValueField = "user_id";
        CheckBoxList1.DataTextField = "Member";
        CheckBoxList1.DataBind();
    }

    protected void checkOrUncheck(object sender, EventArgs e)
    {
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if(isMember(Convert.ToInt32(CheckBoxList1.Items[i].Value)))
                CheckBoxList1.Items[i].Selected = true;
        }
    }

    protected bool isMember(int id)
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        int pid = Convert.ToInt32(Session["pid"]);
        bool flag = false;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select member_id from project_network where project_id = " + pid + " and member_id = " + id;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);
        /*for (int i = 0; i < dt.Rows.Count; i++)
        {
            if(Convert.ToInt32(dt.Rows[i][0]) == id) 
                flag = true;
        }*/
        if (dt.Rows.Count == 1)
            flag = true;
        
        return flag;
    }

    

    private void loadStatusList()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        //int pid = Convert.ToInt32(Session["pid"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select * from project_status";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        DropDownList1.DataSource = dt;
        DropDownList1.DataValueField = "status_id";
        DropDownList1.DataTextField = "status_name";
        DropDownList1.DataBind(); 
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int pid = Convert.ToInt32(Session["pid"]);

        //update project title, description or status
        string s1 = "update projects set project_title = @ptitle, project_desc = @pdesc, project_status = @pstatus, updated_at = GETDATE() where project_id = " + pid;

        //add new member to project network
        string s2 = "insert into project_network(project_id,member_id,role_id,joined_on,read_right,write_right) values(" + pid + ",@member,2,GETDATE(),1,0)";

        //delete member from project network
        string s3 = "delete from project_network where project_id = " + pid + " and member_id = @delMember";

        SqlCommand cmd1 = new SqlCommand(s1, con);
        cmd1.Parameters.AddWithValue("@ptitle", TextBox1.Text);
        cmd1.Parameters.AddWithValue("@pdesc", TextBox2.Text);
        cmd1.Parameters.AddWithValue("@pstatus", Convert.ToInt32(DropDownList1.SelectedValue));

        SqlCommand cmd2 = new SqlCommand(s2, con);
        SqlCommand cmd3 = new SqlCommand(s3, con);
        con.Open();
        cmd1.ExecuteNonQuery();

        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in CheckBoxList1.Items)
        {
            cmd2.Parameters.Clear();
            cmd3.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@member", item.Value);
            cmd3.Parameters.AddWithValue("@delMember", item.Value);
            if (item.Selected)
            {
                if(!isMember(Convert.ToInt32(item.Value)))
                    cmd2.ExecuteNonQuery();
            }
            else
            {
                if(isMember(Convert.ToInt32(item.Value)))
                    cmd3.ExecuteNonQuery();
            }
        }
        con.Close();
        Response.Redirect("ProjectDetails.aspx");

    }
}