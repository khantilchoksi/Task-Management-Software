using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PasswordRecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["passwordSent"]) == 1)
        {
            Session["passwordSent"] = 0;
            SuccessBox.Style["display"] = "inherit";
        }
        Session["emailId"] = Request.QueryString["q"].ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id,verification_code from user_verification where user_id = (select user_id from users where email_id='" + Session["emailId"].ToString() + "')";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
  
        

        da.SelectCommand = cmd;

        da.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Response.Write(dt.Rows[i][0] + " & " + dt.Rows[i][1]);
        }
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][1].ToString() == TextBox1.Text)
            {
                resetPwd(Convert.ToInt32(dt.Rows[0][0]));
                DeleteCode(Convert.ToInt32(dt.Rows[0][0]));
                Session["pwdReset"] = 1;
                Response.Write("This:" + Session["pwdReset"].ToString());
                Response.Redirect("Login.aspx");
            }
            else
            {
                //Invalid verification code
                error1.Style["display"] = "";
                error1.Style["color"] = "#dd4b39";
                error1.Style["margin-top"] = "5px";
                TextBox1.Style["border-color"] = "#dd4b39";
            }
        }
        else
        {
            //Verification code does not exist
            error1.Style["display"] = "";
            error1.Style["color"] = "#000";
            error1.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
    }

    private void resetPwd(int user)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        string s1 = "update users set password = @pwd, updated_at = GETDATE() where user_id = " + user;

        SqlCommand cmd = new SqlCommand(s1, con);
        cmd.Parameters.AddWithValue("@pwd", TextBox2.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void DeleteCode(int id)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        string s2;
        con.Open();
        s2 = "delete from user_verification where user_id=" + id;
        SqlCommand cmd3 = new SqlCommand(s2, con);

        cmd3.ExecuteNonQuery();
        con.Close();
    }
}