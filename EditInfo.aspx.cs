using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;  

public partial class EditInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        session_ops();
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            int id = Convert.ToInt32(Session["user"]);

            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
            cmd.CommandText = "select * from users where user_id = " + id;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            da.SelectCommand = cmd;
            da.Fill(dt);
  
            TextBox1.Text = dt.Rows[0][1].ToString();
            TextBox2.Text = dt.Rows[0][2].ToString();
            TextBox3.Text = dt.Rows[0][4].ToString();
            TextBox6.Text = dt.Rows[0][6].ToString();
            image_upload_preview.Attributes["src"] = "uploads/users/" + dt.Rows[0][12].ToString();
        }
    }

    private void session_ops()
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Convert.ToInt32(Session["infoEditedFlag"]) == 1)
        {
            Session["infoEditedFlag"] = 0;
            SuccessBox.Style["display"] = "inherit";
        }
        if (Convert.ToInt32(Session["pwdChangedFlag"]) == 1)
        {
            Session["pwdChangedFlag"] = 0;
            Success1.Style["display"] = "inherit";
        }
        Session["pid"] = null;
        Session["projRole"] = null;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string fileName="",s1="";
        bool isFile = false;
        if (FileUpload1.HasFile)
        {
            isFile = true;
            fileName = saveFile();
        }
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int id = Convert.ToInt32(Session["user"]);

        if (isFile == false) 
           s1 = "update users set first_name = @fname, last_name = @lname, mobile = @mobile, description=@desc, updated_at = GETDATE() where user_id = " + id;
        else
           s1 = "update users set first_name = @fname, last_name = @lname, mobile = @mobile, description=@desc, updated_at = GETDATE(), img_path = '" + fileName + "' where user_id = " + id; 
        
        SqlCommand cmd = new SqlCommand(s1, con);
        cmd.Parameters.AddWithValue("@fname", TextBox1.Text);
        cmd.Parameters.AddWithValue("@lname", TextBox2.Text);
        cmd.Parameters.AddWithValue("@mobile", TextBox3.Text);
        cmd.Parameters.AddWithValue("@desc", TextBox6.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        Session["userName"] = TextBox1.Text + " " + TextBox2.Text;

        Session["infoEditedFlag"] = 1;
        Response.Redirect("EditInfo.aspx");
    }

    protected string saveFile()
    {
        if (FileUpload1.PostedFile != null)
        {
            // Check the extension of image  
            string extension = Path.GetExtension(FileUpload1.FileName);
            var uniqueFileName = string.Format(@"{0}.jpg", Guid.NewGuid());
            if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg")
            {
                Stream strm = FileUpload1.PostedFile.InputStream;
                using (var image = System.Drawing.Image.FromStream(strm))
                {
                    int newWidth = 160; // New Width of Image in Pixel  
                    int newHeight = 160; // New Height of Image in Pixel  
                    var thumbImg = new Bitmap(newWidth, newHeight);
                    var thumbGraph = Graphics.FromImage(thumbImg);
                    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(image, imgRectangle);
                    // Save the file  
                    string targetPath = Server.MapPath(" ") + "//uploads//users//" + uniqueFileName;
                    thumbImg.Save(targetPath, image.RawFormat);
                    return uniqueFileName;
                }
            }
            return null;
        }
        return null;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;

        int id = Convert.ToInt32(Session["user"]);

        string s1 = "update users set password = @pwd, updated_at = GETDATE() where user_id = " + id;

        SqlCommand cmd = new SqlCommand(s1, con);
        cmd.Parameters.AddWithValue("@pwd", TextBox4.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Session["pwdChangedFlag"] = 1;
        Response.Redirect("EditInfo.aspx");
    }
}