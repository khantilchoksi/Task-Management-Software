using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class MyNetwork : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["inviteSent"]) == 1)
        {
            Session["inviteSent"] = 0;
            SuccessBox.Style["display"] = "inherit";
        }
        loadUsers();
    }

    protected void loadUsers()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        int id = Convert.ToInt32(Session["user"]);
        List<String> list = new List<String>();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        //  cmd.CommandText = "select first_name+' '+last_name as Member from users where user_id in (select member_id from project_network where project_id = " + id + ")";
        String[] names;
        //cmd.CommandText = "select distinct project_id from project_network where member_id =" + id ;
        cmd.CommandText = "select distinct user_id,first_name+' '+last_name,img_path from users where user_id in ((select member_1 from network_members where member_2=" + id + " and invite_status=2) union (select member_2 from network_members where member_1=" + id + " and invite_status=2))";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        int t = 0;
        da.SelectCommand = cmd;
        //da.Fill(dt2);

        //int p;
        //for (int i = 0; i < dt2.Rows.Count; i++)
        //{
        //    Response.Write(dt2.Rows[i][0]);

        //    p = Convert.ToInt32(dt2.Rows[i][0]);

        //    cmd.CommandText = "select distinct first_name+' '+last_name from users where user_id in(select member_id from project_network where project_id =" + p + ")";
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;

        //    da.SelectCommand = cmd;
        da.Fill(dt);

        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {
        //        if(!list.Contains(dt.Rows[j][0].ToString()))
        //        {
        //            list.Add(dt.Rows[j][0].ToString());
        //        }
        //    }  

        ////}
        //names=new String[list.Count];

        //t = 0;
        //    foreach (String y in list)
        //    {
        //        Response.Write(y);
        //        names[t++]=y;
        //    }


        if (dt.Rows.Count == 0)
        {
            NoMembers.InnerHtml = "You don't have any members in your network!";
        }
        else
        {
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                users_list.Controls.Add(li);

                HtmlGenericControl image = new HtmlGenericControl("img");
                image.Attributes.Add("src", "uploads/users/" + dt.Rows[k][2].ToString());
                image.Attributes.Add("alt", "User Image");

                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("class", "users-list-name");
                anchor.Attributes.Add("href", "NetworkProfile.aspx?q=" + dt.Rows[k][0]);

                //anchor.InnerText = dt.Rows[j][0].ToString();
                anchor.InnerText = dt.Rows[k][1].ToString();

                //HtmlGenericControl span = new HtmlGenericControl("span");
                //span.Attributes.Add("class", "users-list-date");
                // span.InnerText = "Date";

                //   string username = Session["userName"].ToString();

                //if (username.Equals(dt.Rows[i][0].ToString()))
                //{
                //    span.InnerText = "Admin";
                //}
                //else
                //{
                //    span.InnerText = "Member";
                //}
                //span.InnerText = "Member";
                li.Controls.Add(image);
                li.Controls.Add(anchor);
                //li.Controls.Add(span);

            }
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
        cmd.CommandText = "select user_id from users where email_id = '" + TextBox1.Text + "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //check invitations part
            //Response.Write(dt.Rows[0][0].ToString());
            checkInvite(Convert.ToInt32(dt.Rows[0][0]));
            //SuccessBox.Style["display"] = "inherit";
        }
        else
        {
            //Invalid email id
            error3.Style["display"] = "none";
            error4.Style["display"] = "none";
            error5.Style["display"] = "none";
            error2.Style["display"] = "none";
            SuccessBox.Style["display"] = "none";
            error1.Style["display"] = "";
            error1.Style["color"] = "#dd4b39";
            error1.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
    }

    protected void checkInvite(int user2)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        int memFlag=0;
        int user1 = Convert.ToInt32(Session["user"]);
        
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select * from network_members";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    Response.Write(dt.Rows[i][1].ToString() + "  " + dt.Rows[i][2].ToString()+"\n");
        //}
        int u1, u2;


        if (user1 == user2)
        {
            memFlag = 4;
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                u1 = Convert.ToInt32(dt.Rows[i][1]);
                u2 = Convert.ToInt32(dt.Rows[i][2]);
                //Response.Write(u1.ToString()+" and"+u2.ToString()+"$$"+user1.ToString()+"and"+user2.ToString()+"**");

                if ((u1 == user1 && u2 == user2) || (u1 == user2 && u2 == user1))
                {
                    // memFlag = 1;
                    if (Convert.ToInt32(dt.Rows[i][3]) == 1)
                    {
                        //request sent
                        //   Response.Write("YP");
                        if (u1 == user1 && u2 == user2)
                        {
                            memFlag = 1;
                        }
                        else
                        {
                            memFlag = 3;
                        }
                        break;
                    }
                    else if (Convert.ToInt32(dt.Rows[i][3]) == 2)
                    {
                        //already in network
                        memFlag = 2;
                        // Response.Write("YS");
                        break;
                    }
                }
            }
        }

        if (memFlag == 1)
        {
            SuccessBox.Style["display"] = "none";
            error1.Style["display"] = "none";
            error3.Style["display"] = "none";
            error4.Style["display"] = "none";
            error5.Style["display"] = "none";
            error2.Style["display"] = "";
            error2.Style["color"] = "#dd4b39";
            error2.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
        else if (memFlag == 2)
        {
            SuccessBox.Style["display"] = "none";
            error1.Style["display"] = "none";
            error2.Style["display"] = "none";
            error4.Style["display"] = "none";
            error5.Style["display"] = "none";
            error3.Style["display"] = "";
            error3.Style["color"] = "#dd4b39";
            error3.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
        else if (memFlag == 3)
        {
            SuccessBox.Style["display"] = "none";
            error1.Style["display"] = "none";
            error2.Style["display"] = "none";
            error3.Style["display"] = "none";
            error5.Style["display"] = "none";
            error4.Style["display"] = "";
            error4.Style["color"] = "#dd4b39";
            error4.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
        else if (memFlag == 4)
        {
            SuccessBox.Style["display"] = "none";
            error1.Style["display"] = "none";
            error2.Style["display"] = "none";
            error3.Style["display"] = "none";
            error4.Style["display"] = "none";
            error5.Style["display"] = "";
            error5.Style["color"] = "#dd4b39";
            error5.Style["margin-top"] = "5px";
            TextBox1.Style["border-color"] = "#dd4b39";
        }
        else if (memFlag == 0)
        {
            //SuccessBox.Style["display"] = "inherit";
            sendInvite(user1, user2);
            Session["inviteSent"] = 1;
            Response.Redirect("MyNetwork.aspx");
        }

    }

    protected void sendInvite(int u1, int u2)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        string s2;
        con.Open();
        s2 = "insert into network_members(member_1,member_2,invite_status,created_at) values("+u1+","+u2+",1,GETDATE())";
        SqlCommand cmd3 = new SqlCommand(s2, con);

        cmd3.ExecuteNonQuery();
        con.Close();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("NetworkCrystalReportPDF.aspx");
    }
}