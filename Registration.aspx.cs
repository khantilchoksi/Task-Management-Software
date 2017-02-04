using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        error1.Style["display"] = "none";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


        cmd.CommandText = "select * from users where email_id = '" + TextBox2.Text+ "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            error1.Style["display"] = "";
            error1.Style["color"] = "#dd4b39";
            error1.Style["margin-top"] = "5px";
            TextBox2.Style["border-color"] = "#dd4b39";
        }
        else
        {
            error1.Style["display"] = "none";
            string s1 = "insert into users(first_name,last_name,email_id,mobile,password,description,img_path,is_validated,online_status,is_active,created_at,updated_at) values('" + TextBox1.Text + "','" + TextBox3.Text + "','" + TextBox2.Text + "','" + TextBox6.Text + "','" + TextBox5.Text + "','Nothing To show!','Default-Profile-160x160.jpg',1,1,1,GETDATE(),GETDATE())";
            SqlCommand cmd2 = new SqlCommand(s1, con);
            con.Open();
            //  cmd.Connection = con;
            // cmd.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            //da.InsertCommand = cmd;
            cmd2.CommandText = "select user_id,first_name,last_name from users where email_id = '" + TextBox2.Text + "' and password = '" + TextBox4.Text + "'";
            cmd2.Connection = con;
            cmd2.CommandType = CommandType.Text;

            da.SelectCommand = cmd2;
            
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                
                Session["user"] = dt.Rows[0][0].ToString();
                Session["userName"] = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                Response.Redirect("Dashboard.aspx");
                //Response.Write("Welcome " + Session["userName"] + "!");
            } 
        }
    }
    
}