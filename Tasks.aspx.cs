using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Tasks : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    //1. All Tasks
    //2. Active Tasks
    //3. Completed Tasks
    //4. Due Today Tasks
    //5. Late Tasks

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSessions();
        NoTasksDiv.InnerHtml = "";
        ProjectDropDownList.Style["float"] = "right";
        
        //loading sections
        loadSectionBar();

        if (!IsPostBack)
        {
            //extract from session about the selected project from Projects.aspx page
            //and then set the dropdownlist value according to session

            if (Session["selectedProjectForTask"] != null)
            {
                int selectedProjectForTask = Convert.ToInt32(Session["selectedProjectForTask"]);
                //Response.Write(" Project ID :" + selectedProjectForTask);


                if (selectedProjectForTask > 0)
                {
                    ProjectDropDownList.ClearSelection();

                    ProjectDropDownList.SelectedItem.Value = selectedProjectForTask.ToString();

                    //Response.Write("   selectedProjectForTask : " + selectedProjectForTask);
                    //Response.Write("   Page Load : " + ProjectDropDownList.SelectedItem.Value);
                    //Response.Write("   Page Load Selected Text : " + ProjectDropDownList.SelectedItem.Text.ToString());

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    cmd.CommandText = "select project_title from projects where project_id = @pid ";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pid", selectedProjectForTask);
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    projectSubTitle.InnerText = dt.Rows[0][0].ToString();
                    ProjectDropDownList.SelectedItem.Text = dt.Rows[0][0].ToString();
                    //ProjectDropDownList_SelectedIndexChanged(null, null);
                }
                Session["selectedProjectForTask"] = null;
            }

            //loading sections
            loadSectionBar();

            //loading grid view for showing all tasks
            AllTaskLinkButton_Click(null, null);
        }


    }

    protected void CheckSessions()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void TaskGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TableCell taskTitleCell = e.Row.Cells[2];
            //taskTitleCell.Text = "<a class='users-list-name' href='TaskDetails.aspx'>" + taskTitleCell.Text + "</a>";

            //TableCell taskStatusCell = e.Row.Cells[1];

            string taskStatusId = (e.Row.FindControl("taskStatusLabel") as Label).Text;
            //Status Dropdown List
            var ddlTasks = e.Row.FindControl("statusDropdownList") as DropDownList;
            if (ddlTasks != null)
            {
                //fetch status from task_status table
                con.Open();
                string fetchStatus = "SELECT * FROM task_status ORDER BY status_id ";
                SqlCommand statusCmd = new SqlCommand(fetchStatus, con);
                DataTable table = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(statusCmd);

                adapter.Fill(table);

                ddlTasks.DataSource = table;
                ddlTasks.DataValueField = "status_id"; //The Value of the DropDownList, to get it, call ddlTasks.SelectedValue;
                ddlTasks.DataTextField = "status_name"; //The Name shown of the DropDownList.
                
                
                ddlTasks.DataBind();
                con.Close();
            }

            ddlTasks.SelectedValue = taskStatusId;



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();


            //cmd.CommandText = "select status_name from task_status where status_id = @statusid";
            ////cmd.CommandText = "select status_name from task_status where status_id = 1";
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@statusid", e.Row.Cells[1].Text);
            //da.SelectCommand = cmd;
            //da.Fill(dt);

            ////taskStatusCell.Text = getStylishedStatus(taskStatusCell.Text);

            //DataTable pdt = new DataTable();
            //TableCell taskPriorityCell = e.Row.Cells[4];

            ////cmd.CommandText = "select priority_name from task_priority where priority_id = " + e.Row.Cells[4];
            //cmd.CommandText = "select priority_name from task_priority where priority_id = 1";
            //da.SelectCommand = cmd;
            //da.Fill(pdt);

            string taskPriorityId = (e.Row.FindControl("taskPriorityLabel") as Label).Text;
            //Priority Dropdown List
            var ddlTaskPriority = e.Row.FindControl("priorityDropdownList") as DropDownList;
            if (ddlTaskPriority != null)
            {
                //fetch status from task_status table
                con.Open();
                string fetchStatus = "SELECT * FROM task_priority ORDER BY priority_id ";
                SqlCommand statusCmd = new SqlCommand(fetchStatus, con);
                DataTable table = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(statusCmd);

                adapter.Fill(table);

                ddlTaskPriority.DataSource = table;
                ddlTaskPriority.DataValueField = "priority_id"; //The Value of the DropDownList, to get it, call ddlTasks.SelectedValue;
                ddlTaskPriority.DataTextField = "priority_name"; //The Name shown of the DropDownList.

                //ddlTasks.DataTextFormatString=
                //ddlTasks.DataTextField = "<span class='label label-info' style='line-height:3;font-size:85%'>" + "status_name"+ "</span>";
                ddlTaskPriority.DataBind();
                con.Close();
            }

            ddlTaskPriority.SelectedValue = taskPriorityId;

            //taskPriorityCell.Text = getStylishedPriority(taskPriorityCell.Text);

            //To get the project name
            DataTable projdt = new DataTable();

            LinkButton viewProjectLinkButton = (LinkButton)e.Row.Cells[4].FindControl("viewProjectLinkButton");
            if (viewProjectLinkButton != null)
            {
                cmd.CommandText = "select project_title from projects where project_id = " + viewProjectLinkButton.Text;
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(projdt);

                viewProjectLinkButton.Text = projdt.Rows[0][0].ToString();
            }

            TableCell taskDueCell = e.Row.Cells[7];
            taskDueCell.Text = getRelativeDueDate(Convert.ToDateTime(taskDueCell.Text));

            TableCell taskCreatedCell = e.Row.Cells[8];
            taskCreatedCell.Text = getRelativeDate(Convert.ToDateTime(taskCreatedCell.Text));

        }
    }

    protected string getStylishedPriority(string priorityId)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        cmd.CommandText = "select priority_name from task_priority where priority_id = " + priorityId;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        switch (priorityId)
        {
            case "1":   //High 
                return "<span class='label label-warning' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";

            case "2":   //Very High
                return "<span class='label label-danger' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";

            case "3":   //Medium
                return "<span class='label label-primary' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";

            case "4":   //Low
                return "<span class='label label-info' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";

            case "5":   //None
                return "<span class='label label-default' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";

                
            default:
                return priorityId;
        }
    }

    protected string getStylishedStatus(string statusId)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        cmd.CommandText = "select status_name from task_status where status_id = " + statusId;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        switch (statusId)
        {
            case "1":   //Status Active 
                return "<span class='label label-info' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
           
            case "2":   //Status In Progress
                return "<span class='label label-primary' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
              
            case "3":   //Status Paused
                return "<span class='label label-warning' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
              
            case "4":   //Status Completed
                return "<span class='label label-success' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
               
            case "5":   //Status Closed
                return "<span class='label label-danger' style='line-height:3;font-size:85%'>" + dt.Rows[0][0].ToString() + "</span>";
                
            default:
                return statusId;

        }
    }

    protected void TaskGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
        //    //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    e.Row.ToolTip = "Click to view the task";
        //    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(TaskGridView, "Select$" + e.Row.RowIndex.ToString()));
        //}
    }
    //protected void TaskGridView_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridView grid = (GridView)sender;
    //    Response.Write("" + TaskGridView.SelectedRow.Cells[1].Text);
    //    Response.Write("" + grid.SelectedDataKey["task_id"]);

    //    Session["task_id"] = grid.SelectedDataKey["task_id"];
    //    Response.Redirect("TaskDetails.aspx");

    //}

    protected void viewTask(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string taskid = btn.CommandArgument;
        Session["task_id"] = taskid;
        Response.Redirect("TaskDetails.aspx");
    }

    protected void viewProject(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string pid = btn.CommandArgument;
        Session["pid"] = pid;

        setProjectRole(Convert.ToInt32(pid));
        Response.Redirect("ProjectDetails.aspx");
    }

    //protected void stausDropdownList_DataBound(object sender, EventArgs e)
    //{
    //    Response.Write(" hhooeell");
    //    GridViewRow gvr = ((DropDownList)sender).NamingContainer as GridViewRow;
    //    if (gvr != null)
    //    {

    //        //We can find all the controls in this row and do operations on them
    //        var ddlStatus = gvr.FindControl("statusDropdownList") as DropDownList;

    //        if (ddlStatus != null)
    //        {
    //            foreach (ListItem listItem in ddlStatus.Items)
    //            {
    //                //Do some things to determine the color of the item
    //                //Set the item background-color like so:
    //                listItem.Attributes.Add("style", "background-color:#100121");
    //                Response.Write(" hheell");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("fdjaslk");
    //    }
        
    //}

    protected void stausDropdownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = ((DropDownList)sender).NamingContainer as GridViewRow;
        if (gvr != null)
        {

            //We can find all the controls in this row and do operations on them
            var ddlStatus = gvr.FindControl("statusDropdownList") as DropDownList;

            if (ddlStatus != null)
            {
                int rowindex = gvr.RowIndex;
                int task_id = Convert.ToInt32(TaskGridView.DataKeys[rowindex].Value);
                //Response.Write("" + ddlStatus.SelectedItem.Value+"Task Id : "+task_id);

                //updating the task status
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE tasks set task_status=" + ddlStatus.SelectedItem.Value + " where task_id=" + task_id;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();



                string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
                SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

                timelineCmd.Parameters.AddWithValue("@tid", task_id);
                timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
                timelineCmd.Parameters.AddWithValue("@msg", "updated task status to "+ getStylishedStatus(ddlStatus.SelectedItem.Value));
                timelineCmd.ExecuteNonQuery();


                con.Close();

                //loading sections
                loadSectionBar();
                ////Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                //GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                //ShowData();  
            }
        }
    }

    protected void priorityDropdownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = ((DropDownList)sender).NamingContainer as GridViewRow;
        if (gvr != null)
        {

            //We can find all the controls in this row and do operations on them
            var ddlPriority = gvr.FindControl("priorityDropdownList") as DropDownList;

            if (ddlPriority != null)
            {
                int rowindex = gvr.RowIndex;
                int task_id = Convert.ToInt32(TaskGridView.DataKeys[rowindex].Value);
                //Response.Write("" + ddlStatus.SelectedItem.Value+"Task Id : "+task_id);

                //updating the task status
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE tasks set task_priority=" + ddlPriority.SelectedItem.Value + " where task_id=" + task_id;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
                SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

                timelineCmd.Parameters.AddWithValue("@tid", task_id);
                timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
                timelineCmd.Parameters.AddWithValue("@msg", "updated task priority to " + getStylishedPriority(ddlPriority.SelectedItem.Value));
                timelineCmd.ExecuteNonQuery();

                con.Close();

                ////Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                //GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                //ShowData();  
            }
        }
    }

    protected void editTask(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string task_id = btn.CommandArgument;
        Session["task_id"] = task_id;
        //setRole(Convert.ToInt32(pid));
        Response.Redirect("EditTask.aspx");
    }

    protected void deleteTask(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        int task_id = Convert.ToInt32(btn.CommandArgument);
        //setRole(pid);
        if (Convert.ToInt32(Session["canEditFlag"]) == 0)
        {
            //Session["canEditFlag"] = 1;
            //AccessBox.Style["display"] = "inherit";
        }
        else
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

            int id = Convert.ToInt32(Session["user"]);


            string s1 = "update tasks set deleted_at = GETDATE() where task_id = " + task_id;

            SqlCommand cmd = new SqlCommand(s1, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("Tasks.aspx");
        }
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

    //For due date
    protected string getRelativeDueDate(DateTime yourDate)
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        if (yourDate.Ticks > DateTime.Now.Ticks)
        {

            var ts = new TimeSpan(yourDate.Ticks - DateTime.Now.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return "less than a minute to go";
                //return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute to go";
                //return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes to go";

            if (delta < 90 * MINUTE)
                return "an hour to go";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours to go";

            if (delta < 48 * HOUR)
                return "tomorrow";

            if (delta < 30 * DAY)
                return ts.Days + " days to go";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month to go" : months + " months to go";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year to go" : years + " years to go";
            }
        }
        else
        {
            //Deadline has passed
            return getRelativeDate(yourDate);
        }
    }

    //Setting project role to view the project
    protected void setProjectRole(int pid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int id = Convert.ToInt32(Session["user"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        cmd.CommandText = "select role_id,read_right,write_right from project_network where member_id = " + id + " and project_id = " + pid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        da.SelectCommand = cmd;
        da.Fill(dt);

        Session["projRole"] = dt.Rows[0][0].ToString();
        Session["canViewFlag"] = dt.Rows[0][1].ToString();
        Session["canEditFlag"] = dt.Rows[0][2].ToString();
    }

    protected void AllTaskLinkButton_Click(object sender, EventArgs e)
    {
        //Response.Write("Hello");
        Session["sectionChoice"] = 1;

        dataTableTitle.InnerText = "  All Tasks";
        dataTableSymbol.Attributes["class"] = "fa fa-list-ul";

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


            if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }
            else
            {
                //Response.Write("Hello");
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }


            TaskGridView.DataSource = dt;
            TaskGridView.DataBind();
            TaskGridView.HeaderRow.TableSection = TableRowSection.TableHeader;


    }

    protected void ActiveTaskLinkButton_Click(object sender, EventArgs e)
    {
        Session["sectionChoice"] = 2;
        dataTableTitle.InnerText = "  Active Tasks";
        dataTableSymbol.Attributes["class"] = "fa fa-play";

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


            if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and task_status = 1 and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }
            else
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id and task_status = 1 and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }

            TaskGridView.DataSource = dt;
            TaskGridView.DataBind();
            TaskGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void CompletedTaskLinkButton_Click(object sender, EventArgs e)
    {
        Session["sectionChoice"] = 3;

        dataTableTitle.InnerText = "  Completed Tasks";
        dataTableSymbol.Attributes["class"] = "fa fa-check-square-o";

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


            if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and task_status = 4 and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }
            else
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id and task_status = 4 and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }


            TaskGridView.DataSource = dt;
            TaskGridView.DataBind();
            TaskGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void DueTodayLinkButton_Click(object sender, EventArgs e)
    {
        Session["sectionChoice"] = 4;
        dataTableTitle.InnerText = "  Due Today Tasks";
        dataTableSymbol.Attributes["class"] = "fa fa-calendar";

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;


            if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and CAST(due_date AS DATE) = CAST(GETDATE() AS DATE) and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }
            else
            {
                cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id and CAST(due_date AS DATE) = CAST(GETDATE() AS DATE) and deleted_at is NULL order by due_date";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
                cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    NoTasksDiv.InnerHtml = "You don't have any tasks!";
                    TaskGridView.DataSource = null;
                    TaskGridView.DataBind();
                    return;
                }
            }
        

            TaskGridView.DataSource = dt;
            TaskGridView.DataBind();
            TaskGridView.HeaderRow.TableSection = TableRowSection.TableHeader;


        
    }

    protected void  LateLinkButton_Click(object sender, EventArgs e)
    {
        Session["sectionChoice"] = 5;

        dataTableTitle.InnerText = "  Late Tasks";
        dataTableSymbol.Attributes["class"] = "fa fa-exclamation";

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
        {
            cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and CAST(due_date AS DATE) < CAST(GETDATE() AS DATE) and deleted_at is NULL order by due_date";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                NoTasksDiv.InnerHtml = "You don't have any tasks!";
                TaskGridView.DataSource = null;
                TaskGridView.DataBind();
                return;
            }
        }
        else
        {
            cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id and CAST(due_date AS DATE) < CAST(GETDATE() AS DATE) and deleted_at is NULL order by due_date";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
            cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                NoTasksDiv.InnerHtml = "You don't have any tasks!";
                TaskGridView.DataSource = null;
                TaskGridView.DataBind();
                return;
            }
        }
        

        TaskGridView.DataSource = dt;
        TaskGridView.DataBind();
        TaskGridView.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Write(" Project :: " + ProjectDropDownList.SelectedItem.Value);
        //ProjectDropDownList.Items.FindByValue(Convert.ToString(Session["selectedProjectForTask"])).Selected = true;
        projectSubTitle.InnerText = ProjectDropDownList.SelectedItem.Text.ToString();

        int sectionChoice = Convert.ToInt32(Session["sectionChoice"]);
        //Response.Write("SectionChoice : " + sectionChoice);

        loadSectionBar();

        switch (sectionChoice)
        {
            case 1: AllTaskLinkButton_Click(null, null);
                break;
            case 2: ActiveTaskLinkButton_Click(null, null);
                break;
            case 3: CompletedTaskLinkButton_Click(null, null);
                break;
            case 4: DueTodayLinkButton_Click(null, null);
                break;
            case 5: LateLinkButton_Click(null, null);
                break;

        }

        //if(ProjectDropDownList.SelectedItem.Value.Equals("-1")){
        //    //All projects selected
        //    cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id order by task_status,due_date";
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
        //    da.SelectCommand = cmd;
        //    da.Fill(allTasksDataTable);
        //}
        //else{
        //    cmd.CommandText = "select n.task_id, task_title,project_id,task_status,task_priority,due_date,created_at from task_network n, tasks t where n.task_id = t.task_id and n.member_id = @user_id and project_id = @proj_id order by task_status,due_date";
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
        //    cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
        //    da.SelectCommand = cmd;
        //    da.Fill(allTasksDataTable);
        //}
  

        //TaskGridView.DataSource = allTasksDataTable;
        //TaskGridView.DataBind();
    }

    protected void loadSectionBar()
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        if (ProjectDropDownList.SelectedItem.Value.Equals("-1"))
        {
            //Selected All Projects
            //counting all tasks
            cmd.CommandText = "select count(*) from task_network tn , tasks t where tn.task_id = t.task_id and member_id = @user_id and deleted_at is NULL";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
            da.SelectCommand = cmd;
            da.Fill(dt);
            int allTasksCount = Convert.ToInt32(dt.Rows[0][0].ToString());
            allTasks.InnerHtml = allTasksCount.ToString();

            //couting active tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and task_status = 1 and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable activeTaskDt = new DataTable();
            da.Fill(activeTaskDt);

            int activeTasksCount = Convert.ToInt32(activeTaskDt.Rows[0][0].ToString());
            activeTasks.InnerHtml = activeTasksCount.ToString();

            //couting completed tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and task_status = 4 and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable completedTaskDt = new DataTable();
            da.Fill(completedTaskDt);

            int completedTasksCount = Convert.ToInt32(completedTaskDt.Rows[0][0].ToString());
            completedTasks.InnerHtml = completedTasksCount.ToString();

            //couting dueToday tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and CAST(due_date AS DATE) = CAST(GETDATE() AS DATE) and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable dueTodayTaskDt = new DataTable();
            da.Fill(dueTodayTaskDt);

            int dueTodayTasksCount = Convert.ToInt32(dueTodayTaskDt.Rows[0][0].ToString());
            dueTodayTasks.InnerHtml = dueTodayTasksCount.ToString();

            //couting late tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and due_date < GETDATE() and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable lateTaskDt = new DataTable();
            da.Fill(lateTaskDt);

            int lateTasksCount = Convert.ToInt32(lateTaskDt.Rows[0][0].ToString());
            lateTasks.InnerHtml = lateTasksCount.ToString();

        }
        else
        {
            //Selected One particular project

            //counting all tasks
            cmd.CommandText = "select count(*) from task_network tn , tasks t where tn.task_id = t.task_id and member_id = @user_id and project_id = @proj_id and deleted_at is NULL";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user"]));
            cmd.Parameters.AddWithValue("@proj_id", Convert.ToInt32(ProjectDropDownList.SelectedItem.Value.ToString()));
            da.SelectCommand = cmd;
            da.Fill(dt);
            int allTasksCount = Convert.ToInt32(dt.Rows[0][0].ToString());
            allTasks.InnerHtml = allTasksCount.ToString();

            //couting active tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and project_id = @proj_id and task_status = 1 and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable activeTaskDt = new DataTable();
            da.Fill(activeTaskDt);

            int activeTasksCount = Convert.ToInt32(activeTaskDt.Rows[0][0].ToString());
            activeTasks.InnerHtml = activeTasksCount.ToString();

            //couting completed tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and project_id = @proj_id and task_status = 4 and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable completedTaskDt = new DataTable();
            da.Fill(completedTaskDt);

            int completedTasksCount = Convert.ToInt32(completedTaskDt.Rows[0][0].ToString());
            completedTasks.InnerHtml = completedTasksCount.ToString();

            //couting dueToday tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and project_id = @proj_id and CAST(due_date AS DATE) = CAST(GETDATE() AS DATE) and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable dueTodayTaskDt = new DataTable();
            da.Fill(dueTodayTaskDt);

            int dueTodayTasksCount = Convert.ToInt32(dueTodayTaskDt.Rows[0][0].ToString());
            dueTodayTasks.InnerHtml = dueTodayTasksCount.ToString();

            //couting late tasks
            cmd.CommandText = "select count(*) from task_network tn, tasks t where tn.task_id = t.task_id and member_id = @user_id and project_id = @proj_id and due_date < GETDATE() and deleted_at is NULL";
            da.SelectCommand = cmd;
            DataTable lateTaskDt = new DataTable();
            da.Fill(lateTaskDt);

            int lateTasksCount = Convert.ToInt32(lateTaskDt.Rows[0][0].ToString());
            lateTasks.InnerHtml = lateTasksCount.ToString();
        }

            
    }

}