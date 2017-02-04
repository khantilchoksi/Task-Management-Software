using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        String pd;
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id from users where email_id = '" + TextBox1.Text + "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt);
        bool flag=false;
        if (dt.Rows.Count > 0)
        {
            //send email part

            GenerateCode(Convert.ToInt32(dt.Rows[0][0]));

            cmd.CommandText = "select verification_code from user_verification where user_id = '" +dt.Rows[0][0] + "'";
            da.SelectCommand = cmd;
            da.Fill(dt2);
            pd = dt2.Rows[0][0].ToString();
            try
            {
                SendEmail(TextBox1.Text.ToString(), pd);
                Session["passwordSent"] = 1;
                Session["emailId"] = TextBox1.Text;
                flag = true;
            }
            catch (Exception e1)
            {
                SendingFailed.Style["display"] = "inherit";
                DeleteCode(Convert.ToInt32(dt.Rows[0][0]));
            }
            if(flag==true)
                Response.Redirect("PasswordRecovery.aspx?q=" + TextBox1.Text);
        }
        else
        {
            //Invalid email id
            error1.Style["display"] = "";
            error1.Style["color"] = "#dd4b39";
            error1.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
    }

    protected void GenerateCode(int id)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        String userCode = RandomString();
        con.Open();
        string s2;
        s2 = "insert into user_verification(user_id,verification_code,created_at) values(" + id + ",'" + userCode + "',GETDATE())";
        SqlCommand cmd3 = new SqlCommand(s2, con);

        cmd3.ExecuteNonQuery();
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

    protected void SendEmail(String emailId,String pd)
    {
        SmtpClient smtpClient = new SmtpClient();

        MailMessage msg = new MailMessage();
        msg.To.Add(emailId);
        msg.From = new MailAddress("bhavik.prerna15@gmail.com");
        msg.Subject = "Password Recovery :: Task Management Software";
        msg.Body = "Hello!\n\nYour verification code is: "+pd+"\n\n\nRegards,\nTeam Task Mangement";

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential("bhavik.prerna15@gmail.com", "5252444452");
        smtpClient.EnableSsl = true;
        smtpClient.Send(msg);
        Response.Write("Email Sent");

    }

    protected string RandomString()
    {
        Random rand = new Random();

        string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        int size = 8;

        char[] chars = new char[size];
        for (int i = 0; i < size; i++)
        {
            chars[i] = Alphabet[rand.Next(Alphabet.Length)];
        }

        return new string(chars);
    }
}