using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CreateTaskWithMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateCheckboxList();
        }
    }
    protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateCheckboxList();
    }

    protected void PopulateCheckboxList()
    {
        int id = Convert.ToInt32(Session["user"]);
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        DataSet ds = new DataSet();
        string cmdstr = "select first_name+' '+last_name as FULLNAME,user_id from users where user_id in (select member_id from project_network where project_id = '" + ProjectDropDownList.SelectedValue + "') and user_id != "+id;
        SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);
        adp.Fill(ds);
        AssigneeCheckboxList.DataSource = ds;
        AssigneeCheckboxList.DataTextField = "FULLNAME";
        AssigneeCheckboxList.DataValueField = "user_id";
        AssigneeCheckboxList.DataBind();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        //DateTime dueDateTime = Convert.ToDateTime(datepicker.Text + " " + timePicker.Text);
        //Response.Write("Date&Time Picker:  " + dueDateTime);

        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        int userid = Convert.ToInt32(Session["user"]);

        string insertQuery = "insert into tasks(task_title,project_id,creator_id,task_status,task_priority,due_date,created_at,task_desc) values('" + TaskTitleTextBox.Text + "','" + ProjectDropDownList.SelectedValue + "','" + userid + "','" + TaskStatusDropDownList.SelectedValue + "','" + TaskPriorityDropDownList.SelectedValue + "','" + datepicker.Text + " " + timePicker.Text + "', GETDATE(),'" + DescriptionTextBox.Text + "')";

        SqlCommand cmd = new SqlCommand(insertQuery, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        SqlDataAdapter da = new SqlDataAdapter();

        DataTable dt = new DataTable();

        cmd.CommandText = "select MAX(task_id) from tasks where creator_id = " + userid;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;

        da.SelectCommand = cmd;
        da.Fill(dt);

        int taskId = (int)dt.Rows[0][0];

        //Inserting table entry for the task creator
        String taskAdminInsertQuery = "insert into task_network values('" + taskId + "','" + userid + "', 1 , GETDATE(),1, 1)";
        //Admin of the task has role id = 1 , read_right = 1 , write_right=1

        SqlCommand insertCmd = new SqlCommand(taskAdminInsertQuery, con);
        con.Open();
        insertCmd.ExecuteNonQuery();
        con.Close();

        //Inserting table entries for the task members
        string taskNetworkInsertQuery = null;

        List<ListItem> selected = new List<ListItem>();
        foreach (ListItem item in AssigneeCheckboxList.Items)
        {
            if (item.Selected)
            {
                taskNetworkInsertQuery = "insert into task_network values('" + taskId + "','" + item.Value + "', 2 , GETDATE(),1, 0)";
                //Memeber of the task has role id = 2 , read_right = 1 , write_right=0

                SqlCommand cmd1 = new SqlCommand(taskNetworkInsertQuery, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }

        }

        string insertTimelineQuery = "insert into task_timeline(task_id, sender_id, message_body,created_at) values(@tid,@uid,@msg, GETDATE())";
        SqlCommand timelineCmd = new SqlCommand(insertTimelineQuery, con);

        timelineCmd.Parameters.AddWithValue("@tid", taskId);
        timelineCmd.Parameters.AddWithValue("@uid", Convert.ToInt32(Session["user"]));
        timelineCmd.Parameters.AddWithValue("@msg", " created task.");
        con.Open();
        timelineCmd.ExecuteNonQuery();
        con.Close();


        //DateTime dateAndTime = Convert.ToDateTime(dateAndTimePicker.Text);
        //Response.Write("Date and time picker: " + dateAndTime);
        Response.Redirect("Tasks.aspx");
    }
}