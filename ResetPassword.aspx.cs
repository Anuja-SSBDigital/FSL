using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public partial class ResetPassword : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public void fillinst()
    {


        if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Assitant Director" || Session["role"].ToString() == "Department Head")
        {
            string inst = fl.GetInst();
            if (!inst.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(inst);
                if (dt.Rows.Count > 0)
                {
                    ddl_inst.DataSource = dt;
                    ddl_inst.DataTextField = "inst_name";
                    ddl_inst.DataValueField = "inst_id";
                    ddl_inst.DataBind();
                    ddl_inst.SelectedValue = Session["inst_id"].ToString();
                    ddl_inst.Attributes.Add("disabled", "disabled");
                    //ddl_inst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }


            string res = fl.GetDeptById(ddl_inst.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddl_department.DataSource = dt;
                    ddl_department.DataTextField = "dept_name";
                    ddl_department.DataValueField = "dept_id";
                    ddl_department.DataBind();
                    ddl_department.SelectedValue = Session["dept_id"].ToString();
                    ddl_department.Attributes.Add("disabled", "disabled");
                    ddl_department.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                }
            }

        }
        else if (Session["role"].ToString() == "SuperAdmin")
        {
            string inst = fl.GetInst();
            if (!inst.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(inst);
                if (dt.Rows.Count > 0)
                {
                    ddl_inst.DataSource = dt;
                    ddl_inst.DataTextField = "inst_name";
                    ddl_inst.DataValueField = "inst_id";
                    ddl_inst.DataBind();

                    ddl_inst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }

        }


    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                fillinst();
                try
                {
                    string ddlRole = "";
                    if (Session["role"].ToString() == "Department Head")
                    {

                        ddlRole = fl.GetAllUsers("-1", Session["inst_id"].ToString(), Session["dept_code"].ToString(), Session["div_code"].ToString());
                    }
                    else
                    {

                        ddlRole = fl.GetAllUsers("-1", "", "", "");

                    }
                    if (!ddlRole.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(ddlRole);
                        if (dtHd.Rows.Count > 0)
                        {
                            GrdReset.DataSource = dtHd;
                            GrdReset.DataBind();
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        else
        {
            Response.Redirect("Login.aspx");

        }
    }

    protected void GrdReset_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnUserID = e.Row.FindControl("hdnUserID") as HiddenField;
            HtmlButton btnReset = e.Row.FindControl("btnReset") as HtmlButton;

            btnReset.Attributes.Add("onclick", "ResetPassword('" + hdnUserID.Value + "');return false;");

        }
    }

    [WebMethod]
    public static string AddResetPassword(string hdnUserID)
    {
        string res = "";
        string resdata = "";
        string EmailID = "";
        string Username = "";
        FlureeCS fl = new FlureeCS();

        resdata = fl.GetAllUsers(hdnUserID, "", "", "");
        if (!resdata.StartsWith("Error"))
        {
            DataTable dtHd = fl.Tabulate(resdata);
            if (dtHd.Rows.Count > 0)
            {
                EmailID = dtHd.Rows[0]["email"].ToString();
                Username = dtHd.Rows[0]["username"].ToString();

                //  String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                string absoluteurl = HttpContext.Current.Request.Url.AbsoluteUri;
                string[] SplitedURL = absoluteurl.Split('/');
                string http = SplitedURL[0];
                string Host = HttpContext.Current.Request.Url.Host;
                string strUrl = "";
                if (Host == "localhost")
                {
                    strUrl = http + "//" + HttpContext.Current.Request.Url.Authority + "/";

                }
                else
                {
                    strUrl = http + "//" + Host + HttpContext.Current.Request.ApplicationPath + "/";
                }


                //String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");


                string Password = System.Web.Security.Membership.GeneratePassword(8, 5);



                string html = "<div style='width: 80%;height: auto;bottom: 0;left: 0;right: 0;margin: auto;margin-bottom: 30px;margin-top:20px'>"
                            + "<div style = 'border: 3px solid #f1f1f1;font-family: Arial;'>"
                            + "<div style = 'padding: 20px;background-color: #f1f1f1;'>"
                            + "<h2> Password Details </h2>"
                            + "<p> Dear " + Username + ",</p><p> You have requested a reset password for " +
                            "e-Sanrakhshan account. The password for your e-Sanrakhshan Account has been " +
                            "changed.To get started with e-Sanrakhshan application below is your Login Details." +
                            "</p></div>"
                          + "<div style ='padding: 20px;background-color:white;font-size: 14px;'>"
                          + "<label><b>Email Address:</></label> " + EmailID + " <br/><label>"
                                + "<b>UserName:</b>" + Username + "</label><br /><label>"
                                + "<b>Password:</b> " + Password + "</label></div>"
                                + "</div>"
            + "<div style ='padding:20px;background-color:#f1f1f1'>"
            + "<a href = '" + strUrl + "' style = 'width:100%;padding:12px;margin:8px 0;text-align:center;" +
            "display:inline-block;box-sizing:border-box;background-color: #392C70;color:white;border:none;'"
                                + " type='submit' target='_blank'>Let's Get Started</a></div></div>" +
                                "<h3>Regards,<br/><b>e-Sanrakhshan Team</b></h3>";

                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("helpme@ssbdigital.in");
                    message.To.Add(new MailAddress(EmailID));
                    message.Subject = "Reset Password Details";
                    message.IsBodyHtml = true;
                    message.Body = html;
                    smtp.Port = 587;
                    smtp.Host = "sg2plzcpnl505639.prod.sin2.secureserver.net";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("helpme@ssbdigital.in", "5FG_W^nt.#TU");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.Send(message);

                    string pass = fl.EncryptString(Password);
                    res = fl.AddResetPasswordData(hdnUserID, pass);
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(res);
                        if (dtHd.Rows.Count > 0)
                        {
                            res = "1";
                        }
                        else
                        {
                            res = "0";
                        }
                    }
                    else
                    {
                        res = "0";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);

                }

            }
        }
        return res;

    }


    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            string ddlRole = "";
            if (Session["role"].ToString() == "Department Head")
            {

                ddlRole = fl.GetAllUsers("-1", Session["inst_id"].ToString(), Session["dept_code"].ToString(), Session["div_code"].ToString());
            }
            else
            {

                ddlRole = fl.GetAllUsers("-1", ddl_inst.SelectedValue, ddl_department.SelectedValue, "");

            }
            if (!ddlRole.StartsWith("Error"))
            {
                DataTable dtHd = fl.Tabulate(ddlRole);
                if (dtHd.Rows.Count > 0)
                {
                    GrdReset.DataSource = dtHd;
                    GrdReset.DataBind();
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void ddl_inst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_inst.SelectedValue != "-1")
        {

            string res = fl.GetDeptById(ddl_inst.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddl_department.DataSource = dt;
                    ddl_department.DataTextField = "dept_name";
                    ddl_department.DataValueField = "dept_id";
                    ddl_department.DataBind();
                    ddl_department.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                }
            }
        }
    }
}