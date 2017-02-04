using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class Dashboard : System.Web.UI.Page
{
    bool flag = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        if (!IsPostBack)
        {
            loadProjects();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataTable network_dt = new DataTable();

            int id = Convert.ToInt32(Session["user"]);

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
            cmd.CommandText = "select count(*) from project_network where member_id = " + id +" and project_id in (select project_id from projects where deleted_at is null)";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);

            int projects = Convert.ToInt32(dt.Rows[0][0].ToString());
            projNum.InnerHtml = projects.ToString();

            if (projects == 0)
            {
                moreInfoT.InnerHtml = "Add New    ";
                moreInfoP.Attributes["href"] = "AddProject.aspx ";
            }

            //for tasks
            cmd.CommandText = "select count(*) from task_network where member_id = " + id + " and task_id in (select task_id from tasks where deleted_at is null)";
            DataTable task_dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(task_dt);

            int tasks = Convert.ToInt32(task_dt.Rows[0][0].ToString());
            taskNum.InnerHtml = tasks.ToString();

            if (tasks == 0)
            {
                moreInfoTaskT.InnerHtml = "Add New    ";
                moreInfoTask.Attributes["href"] = "CreateTask.aspx ";
            }

            cmd.CommandText = "select count(distinct user_id) from users where user_id in ((select member_1 from network_members where member_2=" + id + " and invite_status=2) union (select member_2 from network_members where member_1=" + id + " and invite_status=2))";
            da.SelectCommand = cmd;
            da.Fill(network_dt);
            
            int net = Convert.ToInt32(network_dt.Rows[0][0].ToString());
            networkNum.InnerHtml = net.ToString();
            if (flag == true)
            {
                loadMessages();
            }
            else
            {
                chatbox.InnerText = "No Messages To Show";
                chatFooter.Style["display"] = "none";
            }
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (Convert.ToInt32(Session["ConAddedFlag"]) == 1)
        {
            Session["ConAddedFlag"] = 0;
            AddedSuccessBox.Style["display"] = "inherit";
        }
        if (Convert.ToInt32(Session["ConCancelledFlag"]) == 1)
        {
            Session["ConCancelledFlag"] = 0;
            CancelledSuccessBox.Style["display"] = "inherit";
        }
        Session["pid"] = null;
        Session["projRole"] = null;
    }

    private void loadProjects()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        if (isMemberAny() == true)
        {
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
            cmd.CommandText = "select * from projects where project_id in (select project_id from project_network where member_id = " + id + ") and deleted_at is null";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownList1.DataSource = dt;
                DropDownList1.DataValueField = "project_id";
                DropDownList1.DataTextField = "project_title";
                DropDownList1.DataBind();
            }
        }
        else
        {
            DropDownList1.Items.Add(new ListItem("No Projects To Show", "0",true));
            //DropDownList1.DataBind();
        }
    }

    private bool isMemberAny()
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select project_id from project_network where member_id = " + id + " and project_id in (select project_id from projects where deleted_at is null)"; 
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            flag = true;
            return true;
        }
        else
        {
            flag = false;
            return false;
        }
    }

    protected void loadMessages(object sender = null, EventArgs e = null)
    {
        chatbox.InnerText = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select user_id,first_name+' '+last_name, img_path, online_status, message_body, sent_at, message_id from users u, messages m where sender_id = user_id and project_id = @pid order by message_id desc";

        cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(DropDownList1.SelectedValue));
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
                onlineStatus = Convert.ToInt32(dt.Rows[i][3]) == 1 ? "online" : "offline";
                loadMessage(Convert.ToInt32(dt.Rows[i][0]), Convert.ToInt32(dt.Rows[i][6]), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][4].ToString(), sent_time, onlineStatus);
            }
        }
        else
        {
            chatbox.InnerText = "No messages to show";
        }
    }

    private void loadMessage(int id, int msg_id, string name,string image,string msg,string sent_time,string onlineStatus)
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
         
                <div class="attachment">
                  <h4>Attachments:</h4>
                  <p class="filename">
                    Theme-thumbnail-image.jpg
                  </p>
                  <div class="pull-right">
                    <button type="button" class="btn btn-primary btn-sm btn-flat">Open</button>
                  </div>
                </div>
                <!-- /.attachment -->
              </div>
          <!-- /.item -->*/
        if (id == Convert.ToInt32(Session["user"].ToString()))
            name = "You";
        HtmlGenericControl div = new HtmlGenericControl("div");
        chatbox.Controls.Add(div);
        div.Attributes.Add("class", "item");

        HtmlGenericControl img = new HtmlGenericControl("img");
        img.Attributes.Add("src", "uploads/users/"+image);
        img.Attributes.Add("alt", "user image");
        img.Attributes.Add("class", onlineStatus);

        HtmlGenericControl p = new HtmlGenericControl("p");
        p.Attributes.Add("class", "message");

        HtmlGenericControl a = new HtmlGenericControl("a");
        if (id.ToString() == Session["user"].ToString())
            a.Attributes.Add("href", "javascript:void(0)");
        else
            a.Attributes.Add("href", "NetworkProfile.aspx?q=" + id);
        a.Attributes.Add("class", "name");
        a.Controls.Add(new LiteralControl(name));

        HtmlGenericControl small = new HtmlGenericControl("small");
        small.Attributes.Add("class", "text-muted pull-right");

        HtmlGenericControl i = new HtmlGenericControl("i");
        i.Attributes.Add("class", "fa fa-clock-o");

        small.Controls.Add(i);
        small.Controls.Add(new LiteralControl(" "+sent_time));
        a.Controls.Add(small);
        p.Controls.Add(a);
        p.Controls.Add(new LiteralControl(msg));

        div.Controls.Add(img);
        div.Controls.Add(p);
        if (isAttachment(msg_id) == true)
        {
            string fname = getFileName(msg_id);
            HtmlGenericControl attach_div = new HtmlGenericControl("div");
            attach_div.Attributes.Add("class", "attachment");

            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.Controls.Add(new LiteralControl("Attachments:"));

            HtmlGenericControl p1 = new HtmlGenericControl("p");
            p1.Attributes.Add("class", "filename");
            p1.Controls.Add(new LiteralControl(fname));

            HtmlGenericControl button_div = new HtmlGenericControl("div");
            button_div.Attributes.Add("class", "pull-right");

            HtmlGenericControl button = new HtmlGenericControl("a");
            button.Attributes.Add("href", "uploads/files/" + fname);
            button.Attributes.Add("type", "button");
            button.Attributes.Add("class", "btn btn-primary btn-sm btn-flat");
            button.Attributes.Add("download", fname);
            button.Controls.Add(new LiteralControl("Download"));

            button_div.Controls.Add(button);
            attach_div.Controls.Add(h4);
            attach_div.Controls.Add(p1);
            attach_div.Controls.Add(button_div);
            div.Controls.Add(attach_div);
        }
    }

    private bool isAttachment(int msg_id)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);
        bool aflag = false;

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select * from message_attachments where message_id = " + msg_id;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        if (dt.Rows.Count == 1)
            aflag = true;

        return aflag;
    }

    private string getFileName(int msg_id)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select file_path from message_attachments where message_id = " + msg_id;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        return dt.Rows[0][0].ToString();
    }

    protected void sendMsg(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int id = Convert.ToInt32(Session["user"]);
        string s1 = "insert into messages(sender_id,message_body,sent_at,project_id) values(" + id + ",@msg,GETDATE(),@pid)";

        SqlCommand cmd = new SqlCommand(s1, con);
        cmd.Parameters.AddWithValue("@msg", TextBox1.Text);
        cmd.Parameters.AddWithValue("@pid",Convert.ToInt32(DropDownList1.SelectedValue));
        con.Open();
        cmd.ExecuteNonQuery();
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath(" ")+"/uploads/files/" + FileUpload1.FileName);
            int maxid = getMsgID(id, Convert.ToInt32(DropDownList1.SelectedValue));
            string s2 = "insert into message_attachments values (" + maxid + ",'" + FileUpload1.FileName + "')";
            SqlCommand cmd1 = new SqlCommand(s2, con);
            cmd1.ExecuteNonQuery();
        }
        con.Close();
        loadMessages();
        TextBox1.Text = string.Empty;
        //Response.Redirect("Dashboard.aspx");
    }

    private int getMsgID(int sender, int pid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select max(message_id) from messages where sender_id = " + sender + " and project_id = " + pid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        return Convert.ToInt32(dt.Rows[0][0]);
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

}