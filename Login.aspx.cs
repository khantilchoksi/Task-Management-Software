using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TextBox1.Attributes.Add("type", "email");
        
        

        if (!IsPostBack)
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                TextBox1.Text = Request.Cookies["username"].Value.ToString();
                TextBox2.Attributes.Add("value", Request.Cookies["password"].Value.ToString());
                //Response.Write("This is password: " + Request.Cookies["password"].Value);
            }
            if (Convert.ToInt32(Session["pwdReset"]) == 1)
            {
                Session["pwdReset"] = 0;
                PasswordChanged.Style["display"] = "inherit";
            }
            //Response.Write("This is password: " + Request.Cookies["password"].Value);
        }
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        //cmd.CommandText = "select user_id,first_name,last_name from users where email_id = '" + TextBox1.Text + "' and password = '" + TextBox2.Text + "'";
        cmd.CommandText = "select * from users where email_id = '" + TextBox1.Text+ "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][5].ToString() == TextBox2.Text)
            {
                Session["user"] = dt.Rows[0][0].ToString();
                Session["userName"] = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                Session["pid"] = null;
                Session["projRole"] = null;
                Session["canViewFlag"] = 1;
                Session["canEditFlag"] = 1;
                Session["canDeleteFlag"] = 1;
                Session["successFlag"] = 0;
                Session["infoEditedFlag"] = 0;
                Session["pwdChangedFlag"] = 0;
                Session["ConAddedFlag"] = 0;
                Session["ConCancelledFlag"] = 0;
                //Response.Write("Welcome " + Session["userName"] + "!");
                string s1 = "update users set online_status = 1 where user_id = " + Convert.ToInt32(Session["user"]);
                SqlCommand cmd2 = new SqlCommand(s1, con);
                con.Open();
                cmd2.ExecuteNonQuery();
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                //Invalid Password
                error2.Style["display"] = "";
                error2.Style["color"] = "#dd4b39";
                error1.Style["display"] = "none";
                TextBox2.Style["border-color"] = "#dd4b39";
            }
        }
        else
        {
            //Invalid email id
            error1.Style["display"] = "";
            error1.Style["color"] = "#dd4b39";
            error2.Style["display"] = "none";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
        con.Close();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            Response.Cookies["userName"].Expires = DateTime.Now.AddDays(60);
            Response.Cookies["password"].Expires = DateTime.Now.AddDays(60);
        }
        else
        {
            Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
        }
        Response.Cookies["username"].Value = TextBox1.Text;
        Response.Cookies["password"].Value = TextBox2.Text;
    }

}