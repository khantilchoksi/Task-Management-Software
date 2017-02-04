using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EditTask : System.Web.UI.Page
{
    int task_id;
    int project_id;
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        if (!IsPostBack)
        {
            CheckSessions();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            task_id = Convert.ToInt32(Session["task_id"]);

            
            cmd.CommandText = "select * from tasks where task_id = " + task_id;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);

            TaskTitleTextBox.Text = dt.Rows[0][1].ToString();
            DescriptionTextBox.Text = dt.Rows[0][10].ToString();

            DateTime oldDueDateTime = Convert.ToDateTime(dt.Rows[0][6].ToString());
            Session["oldDueDateTime"] = oldDueDateTime;
            //Response.Write(" Page Load old: old due date : "+oldOueDateTime.ToString());
            datepicker.Text = oldDueDateTime.ToString("MM/dd/yyyy");

            timePicker.Text = oldDueDateTime.ToString("HH:mm");

            project_id = Convert.ToInt32(dt.Rows[0][2]);

            //Response.Write(" Task id : "+task_id+"   Project id : " + project_id);

            
            TaskStatusDropDownList.SelectedValue = dt.Rows[0][4].ToString();
            TaskPriorityDropDownList.SelectedValue = dt.Rows[0][5].ToString();

            //ProjectDropDownList.Attributes("onChange") = "DisplayConfirmation();"
            //ProjectDropDownList.Attributes.Add("onChange", "DisplayConfirmation();");
            PopulateCheckboxList(Convert.ToInt32(dt.Rows[0][2].ToString()));

            cmd.CommandText = "select project_title from projects where project_id = @proj_id";
            cmd.Parameters.AddWithValue("@proj_id", project_id);

            da.SelectCommand = cmd;
            DataTable projectDt = new DataTable();
            da.Fill(projectDt);

            ProjectTextBox.Text = projectDt.Rows[0][0].ToString();
        }
        
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

        if (Convert.ToInt32(Session["task_id"]) == 0)
        {
            Response.Redirect("Tasks.aspx");
        }
    }

    protected void SaveTaskChangesButton_Click(object sender, EventArgs e)
    {
        //Response.Write("Task Id : "+task_id+"  Saved Project:" + ProjectDropDownList.SelectedItem.Value);
        //Response.Write("Status : " + TaskStatusDropDownList.SelectedItem.Value + "  Priority : " + TaskPriorityDropDownList.SelectedItem.Value);

        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        
        //Fetch the old data
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        cmd.CommandText = "select * from tasks where task_id = @tid " ;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
        da.SelectCommand = cmd;
        da.Fill(dt);





        //Check whether task status has changed or not
        CheckTaskStatusChanged(Convert.ToInt32(dt.Rows[0][4].ToString()));

        //Check whether task priority has changed or not
        CheckTaskPriorityChanged(Convert.ToInt32(dt.Rows[0][5].ToString()));
        
        //update all task details
        string updateQuery = "update tasks set task_title = @tasktitle, task_status = @tstatus, task_priority = @tpriority, due_date = @duedate, updated_at = GETDATE(), task_desc = @tdesc where task_id = @tid" ;
        //task_status = @tstatus, task_priority = @tpriority, 
        SqlCommand updatecmd = new SqlCommand(updateQuery, con);
        updatecmd.Parameters.AddWithValue("@tasktitle", TaskTitleTextBox.Text);
        updatecmd.Parameters.AddWithValue("@tstatus", Convert.ToInt32(TaskStatusDropDownList.SelectedItem.Value));
        updatecmd.Parameters.AddWithValue("@tpriority", Convert.ToInt32(TaskPriorityDropDownList.SelectedItem.Value));
        updatecmd.Parameters.AddWithValue("@duedate", datepicker.Text + " " + timePicker.Text);
        updatecmd.Parameters.AddWithValue("@tdesc", DescriptionTextBox.Text);
        updatecmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));

        con.Open();
        updatecmd.ExecuteNonQuery();

        //add new member to project network
        string insertQuery = "insert into task_network(task_id,member_id,role_id,joined_on,read_right,write_right) values(@tid,@member,2,GETDATE(),1,0)";

        //delete member from project network
        string deleteQuery = "delete from task_network where task_id = @tid and member_id = @delMember";

        SqlCommand insertCommand = new SqlCommand(insertQuery, con);
        SqlCommand deleteCommand = new SqlCommand(deleteQuery, con);

        

        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in AssigneeCheckboxList.Items)
        {
 

            if (item.Selected)
            {
                //user is selected
                if (!isMember(Convert.ToInt32(item.Value))){
                    //user is not member of task
                    //so assign the user a task
                    insertCommand.Parameters.Clear();
                    insertCommand.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
                    insertCommand.Parameters.AddWithValue("@member", item.Value);
                    insertCommand.ExecuteNonQuery();

                    string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
                    SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

                    timelineCmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
                    timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
                    timelineCmd.Parameters.AddWithValue("@msg", item.Text+" has been assigned task.");
                    timelineCmd.ExecuteNonQuery();
                }
                    
            }
            else
            {
                if (isMember(Convert.ToInt32(item.Value)))
                {
                    //item is not selected
                    deleteCommand.Parameters.Clear();
                    deleteCommand.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
                    deleteCommand.Parameters.AddWithValue("@delMember", item.Value);
                    deleteCommand.ExecuteNonQuery();

                    string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
                    SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

                    timelineCmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
                    timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
                    timelineCmd.Parameters.AddWithValue("@msg", item.Text + " has been removed from task.");
                    timelineCmd.ExecuteNonQuery();
                }
                    
            }
        }

        con.Close();

        //For new due date time
        SqlCommand newcmd = new SqlCommand();
        SqlDataAdapter newda = new SqlDataAdapter();
        DataTable newdt = new DataTable();


        newcmd.CommandText = "select * from tasks where task_id = @tid";
        newcmd.Connection = con;
        newcmd.CommandType = CommandType.Text;
        newcmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
        newda.SelectCommand = cmd;
        newda.Fill(newdt);

        //check whether Task Deadline has changed or not
        CheckTaskDeadlineChanged(Convert.ToDateTime(newdt.Rows[0][6].ToString()));



        Response.Redirect("Tasks.aspx");

    }

    protected void CheckTaskDeadlineChanged(DateTime newDueDateTime)
    {
        DateTime oldDueDateTime = Convert.ToDateTime(Session["oldDueDateTime"]);
        //Response.Write("Old : " + old.ToString() + "   New : " + newDueDateTime.ToString());
        if (!(oldDueDateTime).Equals(newDueDateTime))
        {
            string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
            SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

            timelineCmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
            timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
            timelineCmd.Parameters.AddWithValue("@msg", "updated task deadline to " + datepicker.Text + " " + timePicker.Text);
            con.Open();
            timelineCmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void CheckTaskStatusChanged(int oldStatusId)
    {
        if (Convert.ToInt32(TaskStatusDropDownList.SelectedItem.Value) != oldStatusId)
        {
            string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
            SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

            timelineCmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
            timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
            timelineCmd.Parameters.AddWithValue("@msg", "updated task status to "+ getStylishedStatus(TaskStatusDropDownList.SelectedItem.Value));
            con.Open();
            timelineCmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void CheckTaskPriorityChanged(int oldPriorityId)
    {
        if (Convert.ToInt32(TaskPriorityDropDownList.SelectedItem.Value) != oldPriorityId)
        {
            string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
            SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

            timelineCmd.Parameters.AddWithValue("@tid", Convert.ToInt32(Session["task_id"]));
            timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
            timelineCmd.Parameters.AddWithValue("@msg", "updated task priority to " + getStylishedPriority(TaskPriorityDropDownList.SelectedItem.Value));
            con.Open();
            timelineCmd.ExecuteNonQuery();
            con.Close();
        }
    }


    protected void AssigneeCheckboxList_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < AssigneeCheckboxList.Items.Count; i++)
        {
            if (isMember(Convert.ToInt32(AssigneeCheckboxList.Items[i].Value)))
                AssigneeCheckboxList.Items[i].Selected = true;
        }
    }

    //protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ShowConfirmationDialog() == DialogResult.Yes)
    //    //{
    //    //    //Do something
    //    //    PopulateCheckboxList();
    //    //}
    //    //else
    //    //{
    //    //    //Do something else
    //    //}

    //    PopulateCheckboxList(Convert.ToInt32(ProjectDropDownList.SelectedItem.Value));
        
    //}

    protected void PopulateCheckboxList(int projectId)
    {
        //Response.Write("Selected Project : " + projectId);
        SqlConnection con = new SqlConnection();
        int id = Convert.ToInt32(Session["user"]);
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        DataSet ds = new DataSet();
        string cmdstr = "select first_name+' '+last_name as FULLNAME,user_id from users where user_id in (select member_id from project_network where project_id = '" + projectId + "') and user_id != " + id;
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);
        adp.Fill(ds);
        AssigneeCheckboxList.DataSource = ds;
        AssigneeCheckboxList.DataTextField = "FULLNAME";
        AssigneeCheckboxList.DataValueField = "user_id";
        AssigneeCheckboxList.DataBind();

        //Check the already assigned members
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();

        int tid = Convert.ToInt32(Session["task_id"]);

        cmd.CommandText = "select * from task_network where task_id = " + tid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);


        //foreach (DataRow row in dt.Rows)
        //{
        //    String value = row["member_id"].ToString();
        //    Response.Write(" U: "+ value);

        //    for (int i = 0; i < AssigneeCheckboxList.Items.Count; i++)
        //    {
        //        if (AssigneeCheckboxList.Items[i].Value == value)
        //        {
        //            AssigneeCheckboxList.Items[i].Selected = true ;
        //        }
        //    }

        //}

    }

    protected bool isMember(int id)
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        int tid = Convert.ToInt32(Session["task_id"]);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        cmd.CommandText = "select member_id from task_network where task_id = " + tid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i][0]) == id)
                return true;
        }
        return false;
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
}