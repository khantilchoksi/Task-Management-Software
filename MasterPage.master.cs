using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        userName.InnerHtml = Session["userName"].ToString();
        userName2.InnerHtml = Session["userName"].ToString();
        userName3.InnerHtml = Session["userName"].ToString();

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select img_path,email_id from users where user_id = " + id;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);
        string imagePath = dt.Rows[0][0].ToString();
        img1.Attributes["src"] = "uploads/users/" + imagePath;
        img2.Attributes["src"] = "uploads/users/" + imagePath;
        img3.Attributes["src"] = "uploads/users/" + imagePath;

        emailid.InnerHtml = dt.Rows[0][1].ToString();


        loadNotifications();
        loadTasksCount();

    }

    private void loadNotifications()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select count(*) from network_members where member_2 = " + id + " and invite_status = 1";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        InviteCount.InnerText = dt.Rows[0][0].ToString();

        cmd.CommandText = "select user_id, img_path, first_name+' '+last_name, i.created_at from users u,network_members i where user_id in (select member_1 from network_members where member_2 = " + id + " and invite_status = 1) and user_id = member_1 and member_2 = " + id + " order by i.invite_id desc";

        da.SelectCommand = cmd;
        da.Fill(dt1);

        string sent_time;
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                sent_time = getRelativeDate((DateTime)dt1.Rows[i][3]);
                loadInvite(Convert.ToInt32(dt1.Rows[i][0]), dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), sent_time);
            }
        }
        else
        {
            InviteList.InnerHtml = "<p style=\"text-align:center; padding-top: 75px; color:#999\">No Pending Member Invites</p>";  
        }
    }

    private void loadInvite(int id, string path, string name, string sent_time)
    {
        /*<li>
            <a href="#">
                <div class="pull-left">
                    <img src="dist/img/user3-128x128.jpg" class="img-circle" alt="User Image">
                </div>
                <h4>
                    AdminLTE Design Team
                    <small><i class="fa fa-clock-o"></i> 2 hours</small>
                </h4>
                <p>Why not buy a new awesome theme?</p>
            </a>
        </li>*/
        HtmlGenericControl li = new HtmlGenericControl("li");
        InviteList.Controls.Add(li);

        HtmlGenericControl a = new HtmlGenericControl("a");
        a.Attributes.Add("href", "javascript:void(0)");

        HtmlGenericControl div = new HtmlGenericControl("div");
        div.Attributes.Add("class", "pull-left");

        HtmlGenericControl img = new HtmlGenericControl("img");
        img.Attributes.Add("src", "uploads/users/" + path);
        img.Attributes.Add("class", "img-circle");
        img.Attributes.Add("alt", "Sender Image");

        HtmlGenericControl h4 = new HtmlGenericControl("h4");
        HtmlGenericControl small = new HtmlGenericControl("small");
        HtmlGenericControl i = new HtmlGenericControl("i");
        i.Attributes.Add("class", "fa fa-clock-o");
        small.Controls.Add(i);
        small.Controls.Add(new LiteralControl(" " + sent_time));

        h4.Controls.Add(new LiteralControl(name));
        h4.Controls.Add(small);
        h4.Attributes.Add("style", "padding-bottom:5px;");

        div.Controls.Add(img);
        a.Controls.Add(div);
        a.Controls.Add(h4);
        li.Controls.Add(a);
        
        Button accept = new Button();
        accept.CommandArgument = id.ToString();
        accept.CssClass = "btn btn-primary";
        accept.Style["padding"] = "2px 12px";
        accept.Text = "Accept";
        accept.Click += new EventHandler(AcceptRequest);
        a.Controls.Add(accept);

        a.Controls.Add(new LiteralControl("&nbsp"));

        Button cancel = new Button();
        cancel.CommandArgument = id.ToString();
        cancel.CssClass = "btn btn-flat";
        cancel.Style["padding"] = "2px 12px";
        cancel.Text = "Cancel";
        cancel.Click += new EventHandler(CancelRequest);
        a.Controls.Add(cancel);
    }

    protected void AcceptRequest(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int id = Convert.ToInt32(Session["user"]);

        Button btn = (Button)(sender);
        string sender_id = btn.CommandArgument;

        //string s1 = "insert into network_members values(" + Convert.ToInt32(sender_id) + "," + id + ")";
        string s2 = "update network_members set invite_status = 2 where member_1 = " + Convert.ToInt32(sender_id) + " and member_2 = " + id;

        //SqlCommand cmd1 = new SqlCommand(s1, con);
        SqlCommand cmd2 = new SqlCommand(s2, con);
        con.Open();
        //cmd1.ExecuteNonQuery();
        cmd2.ExecuteNonQuery();
        con.Close();
        Session["ConAddedFlag"] = 1;
        Response.Redirect("Dashboard.aspx");
    }

    protected void CancelRequest(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int id = Convert.ToInt32(Session["user"]);

        Button btn = (Button)(sender);
        string sender_id = btn.CommandArgument;

        string s1 = "delete from network_members where member_1 = " + Convert.ToInt32(sender_id) + " and member_2 = " + id;

        SqlCommand cmd = new SqlCommand(s1, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Session["ConCancelledFlag"] = 1;
        Response.Redirect("Dashboard.aspx");
    }
    protected string getRelativeDate(DateTime yourDate)
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        var ts = new TimeSpan(DateTime.Now.Ticks - yourDate.Ticks);
        double delta = Math.Abs(ts.TotalSeconds);

        if (delta < 1 * MINUTE)
            return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

        if (delta < 2 * MINUTE)
            return "a minute ago";

        if (delta < 45 * MINUTE)
            return ts.Minutes + " minutes ago";

        if (delta < 90 * MINUTE)
            return "an hour ago";

        if (delta < 24 * HOUR)
            return ts.Hours + " hours ago";

        if (delta < 48 * HOUR)
            return "yesterday";

        if (delta < 30 * DAY)
            return ts.Days + " days ago";

        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "one month ago" : months + " months ago";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }

    protected void loadTasksCount()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


        //couting active tasks
        cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and task_status = 1 and deleted_at is NULL";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
        da.SelectCommand = cmd;
        DataTable activeTaskDt = new DataTable();
        da.Fill(activeTaskDt);

        int activeTasksCount = Convert.ToInt32(activeTaskDt.Rows[0][0].ToString());
        activeTaskCount.InnerHtml = activeTasksCount.ToString();

        //couting completed tasks
        cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and task_status = 4 and deleted_at is NULL";
        da.SelectCommand = cmd;
        DataTable completedTaskDt = new DataTable();
        da.Fill(completedTaskDt);

        int completedTasksCount = Convert.ToInt32(completedTaskDt.Rows[0][0].ToString());
        completedTaskCount.InnerHtml = completedTasksCount.ToString();

        //couting dueToday tasks
        cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and CAST(due_date AS DATE) = CAST(GETDATE() AS DATE) and deleted_at is NULL";
        da.SelectCommand = cmd;
        DataTable dueTodayTaskDt = new DataTable();
        da.Fill(dueTodayTaskDt);

        int dueTodayTasksCount = Convert.ToInt32(dueTodayTaskDt.Rows[0][0].ToString());
        dueTodayTaskCount.InnerHtml = dueTodayTasksCount.ToString();

        //couting late tasks
        cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and due_date < GETDATE() and deleted_at is NULL";
        da.SelectCommand = cmd;
        DataTable lateTaskDt = new DataTable();
        da.Fill(lateTaskDt);

        int lateTasksCount = Convert.ToInt32(lateTaskDt.Rows[0][0].ToString());
        lateTaskCount.InnerHtml = lateTasksCount.ToString();

    }

    protected void Sign_Out_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        int id = Convert.ToInt32(Session["user"]);
        string s1 = "update users set online_status = 0 where user_id = " + id;
        SqlCommand cmd = new SqlCommand(s1, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Session["user"] = null;
        Session["userName"] = null;
        Session["pid"] = null;
        Session["projRole"] = null;
        Session["canViewFlag"] = null;
        Session["canEditFlag"] = null;
        Session["canDeleteFlag"] = null;
        Session["successFlag"] = null;
        Session["infoEditedFlag"] = null;
        Session["pwdChangedFlag"] = null;
        Session["ConAddedFlag"] = null;
        Session["ConCancelledFlag"] = null;
        Session["task_id"] = null;
        Response.Redirect("Login.aspx");
    }
}
