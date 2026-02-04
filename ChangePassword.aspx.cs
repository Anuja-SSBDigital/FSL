using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

        }

        protected void btnChangePass_Click(object sender, EventArgs e)
    {
        if (fl.EncryptString(txtCurrentPass.Text) == Session["password"].ToString())
        {
            if (txtNewPass.Text == txtConfNewPass.Text)
            {

                //lblMsg.Text = "";

                string res = fl.ChangePassword(Session["userid"].ToString(), fl.EncryptString(txtNewPass.Text));
                if (res.StartsWith("Error"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(),
                   "alert", "<script>alert('Password doesn't change. Please try again later.');</script>");
                    //lblMsg.Text = "Password doesn't change. Please try again later.";
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    Session["username"] = null;
                    Session["userid"] = null;
                    Response.Write("<script language='javascript'>window.alert('Password changed successfully.');window.location='../Login.aspx';</script>");

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(),
                 "alert", "<script>alert('*New Password and Confirm Password does not match.');</script>");

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(),
             "alert", "<script>alert('*Current Password Is Incorrect.');</script>");
        }
    }
}