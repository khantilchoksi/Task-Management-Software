using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class TaskDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        CheckSessions();
        EditTaskButton.Style["float"] = "right";
        
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int taskId = Convert.ToInt32(Session["task_id"]);
        int userId = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select * from tasks where task_id = " + taskId;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        descBody.InnerHtml = dt.Rows[0][10].ToString();
        task_title.InnerHtml = dt.Rows[0][1].ToString();

        //descriptionTextBox.Text = dt.Rows[0][10].ToString();
        //descriptionTextBox.ReadOnly = true;

        loadUsers();

        loadTimeline();
    }

    protected void CheckSessions()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (Session["task_id"] == null)
        {
            Response.Redirect("Tasks.aspx");
        }
    }

    protected void loadUsers()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int taskId = Convert.ToInt32(Session["task_id"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select first_name+' '+last_name as Member, img_path from users where user_id in (select member_id from task_network where task_id = " + taskId + ")";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            users_list.Controls.Add(li);

            HtmlGenericControl image = new HtmlGenericControl("img");
            image.Attributes.Add("src", "uploads/users/" + dt.Rows[i][1].ToString());
            image.Attributes.Add("alt", "User Image");

            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("class", "users-list-name");
            anchor.Attributes.Add("href", "#");
            anchor.InnerText = dt.Rows[i][0].ToString();
            //anchor.InnerText = "User";

            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("class", "users-list-date");
            //span.InnerText = "Date";

            string username = Session["userName"].ToString();

            if (username.Equals(dt.Rows[i][0].ToString()))
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
    protected void TaskDetailsView_DataBound(object sender, EventArgs e)
    {
        DetailsView TaskDetailsView = (DetailsView)sender;
        //Response.Write("" + TaskDetailsView.Rows[0].Cells[0].Text);
        //Response.Write("  : " + TaskDetailsView.Rows[0].Cells[1].Text);

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        //Task Project Name
        DataTable proj_dt = new DataTable();
        cmd.CommandText = "select project_title from projects where project_id = " + TaskDetailsView.Rows[0].Cells[1].Text;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(proj_dt);

        TaskDetailsView.Rows[0].Cells[1].Text = proj_dt.Rows[0][0].ToString();

        //Task Admin
        DataTable admin_dt = new DataTable();
        cmd.CommandText = "select first_name+' '+last_name as AdminName from users where user_id = " + TaskDetailsView.Rows[1].Cells[1].Text;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(admin_dt);

        TaskDetailsView.Rows[1].Cells[1].Text = admin_dt.Rows[0][0].ToString();

        //Task Status
        DataTable status_dt = new DataTable();
        cmd.CommandText = "select status_name from task_status where status_id = " + TaskDetailsView.Rows[2].Cells[1].Text;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(status_dt);

        //TaskDetailsView.Rows[2].Cells[1].Text = status_dt.Rows[0][0].ToString();
        switch (TaskDetailsView.Rows[2].Cells[1].Text)
        {
            case "1":   //Status Active 
                TaskDetailsView.Rows[2].Cells[1].Text = "<span class='label label-info' style='line-height:3;font-size:85%'>" + status_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "2":   //Status In Progress
                TaskDetailsView.Rows[2].Cells[1].Text = "<span class='label label-primary' style='line-height:3;font-size:85%'>" + status_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "3":   //Status Paused
                TaskDetailsView.Rows[2].Cells[1].Text = "<span class='label label-warning' style='line-height:3;font-size:85%'>" + status_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "4":   //Status Completed
                TaskDetailsView.Rows[2].Cells[1].Text = "<span class='label label-success' style='line-height:3;font-size:85%'>" + status_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "5":   //Status Closed
                TaskDetailsView.Rows[2].Cells[1].Text = "<span class='label label-danger' style='line-height:3;font-size:85%'>" + status_dt.Rows[0][0].ToString() + "</span>";
                break;


        }

        //Task Priority
        DataTable prio_dt = new DataTable();
        cmd.CommandText = "select priority_name from task_priority where priority_id = " + TaskDetailsView.Rows[3].Cells[1].Text;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(prio_dt);

        //TaskDetailsView.Rows[3].Cells[1].Text = prio_dt.Rows[0][0].ToString();
        switch (TaskDetailsView.Rows[3].Cells[1].Text)
        {
            case "1":   //High 
                TaskDetailsView.Rows[3].Cells[1].Text = "<span class='label label-warning' style='line-height:3;font-size:85%'>" + prio_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "2":   //Very High
                TaskDetailsView.Rows[3].Cells[1].Text = "<span class='label label-danger' style='line-height:3;font-size:85%'>" + prio_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "3":   //Medium
                TaskDetailsView.Rows[3].Cells[1].Text = "<span class='label label-primary' style='line-height:3;font-size:85%'>" + prio_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "4":   //Low
                TaskDetailsView.Rows[3].Cells[1].Text = "<span class='label label-info' style='line-height:3;font-size:85%'>" + prio_dt.Rows[0][0].ToString() + "</span>";
                break;
            case "5":   //None
                TaskDetailsView.Rows[3].Cells[1].Text = "<span class='label label-default' style='line-height:3;font-size:85%'>" + prio_dt.Rows[0][0].ToString() + "</span>";
                break;
        }
    }

    protected void loadTimeline()
    {
        timelinebox.InnerText = "";

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();


        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id,first_name+' '+last_name, img_path, online_status, message_body, 	t.created_at from users u, task_timeline t where sender_id = user_id and task_id = @tid order by timeline_id desc";
        cmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        string sent_time, onlineStatus;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sent_time = getRelativeDate((DateTime)dt.Rows[i][5]);
                //sent_time = getRelativeDate(Convert.ToDateTime("28-09-2016 17:57:57 PM"));
                onlineStatus = Convert.ToInt32(dt.Rows[i][3]) == 1 ? "online" : "offline";
                loadMessage(Convert.ToInt32(dt.Rows[i][0]), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][4].ToString(), sent_time, onlineStatus);
            }
        }
        else
        {
            timelinebox.InnerText = "No messages to show";
        }
    }

    private void loadMessage(int user_id, string name, string image_path, string msg, string sent_time, string onlineStatus)
    {
        /*<!-- chat item -->
              <div class="item">
                <img src="dist/img/user4-128x128.jpg" alt="user image" class="online">

                <p class="message">
                  <a href="#" class="name">
                    <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> 2:15</small>
                    Mike Doe
                  </a>
                  I would like to meet you to discuss the latest news about
                  the arrival of the new theme. They say it is going to be one the
                  best themes on the market
                </p>
              </div>
          <!-- /.item -->*/
        if (user_id == Convert.ToInt32(Session["user"].ToString()))
            name = "You";

        HtmlGenericControl div = new HtmlGenericControl("div");
        timelinebox.Controls.Add(div);
        div.Attributes.Add("class", "item");

        HtmlGenericControl img = new HtmlGenericControl("img");
        img.Attributes.Add("src", "uploads/users/" + image_path);
        img.Attributes.Add("alt", "user image");
        img.Attributes.Add("class", onlineStatus);

        HtmlGenericControl p = new HtmlGenericControl("p");
        p.Attributes.Add("class", "message");

        HtmlGenericControl a = new HtmlGenericControl("a");
        a.Attributes.Add("href", "#");
        a.Attributes.Add("class", "name");
        a.Controls.Add(new LiteralControl(name));

        HtmlGenericControl small = new HtmlGenericControl("small");
        small.Attributes.Add("class", "text-muted pull-right");

        HtmlGenericControl i = new HtmlGenericControl("i");
        i.Attributes.Add("class", "fa fa-clock-o");

        small.Controls.Add(i);
        small.Controls.Add(new LiteralControl(" " + sent_time));
        a.Controls.Add(small);
        p.Controls.Add(a);
        p.Controls.Add(new LiteralControl(msg));

        div.Controls.Add(img);
        div.Controls.Add(p);
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

    protected void EditTaskButton_Click(object sender, EventArgs e)
    {
        int taskId = Convert.ToInt32(Session["task_id"]);
        Session["task_id"] = taskId.ToString();
        Response.Redirect("EditTask.aspx");
    }
}