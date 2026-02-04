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

public partial class forgotpassword : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string resdata = fl.forgotpassword(txtUN.Text, txtEmailid.Text);
        if (!resdata.StartsWith("Error"))
        {
            DataTable dtHd = fl.Tabulate(resdata);
            if (dtHd.Rows.Count > 0)
            {
                string EmailID = dtHd.Rows[0]["email"].ToString();
                string Username = dtHd.Rows[0]["username"].ToString();
                string userid = dtHd.Rows[0]["userid"].ToString();


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

                string Password = System.Web.Security.Membership.GeneratePassword(8, 5);


                string html = "<div style='width: 80%;height: auto;bottom: 0;left: 0;right: 0;margin: auto;margin-bottom: 30px;margin-top:20px'>"
                            + "<div style = 'border: 3px solid #f1f1f1;font-family: Arial;'>"
                            + "<div style = 'padding: 20px;background-color: #f1f1f1;'>"
                            + "<h2> Password Details </h2>"
                            + "<p> Dear " + txtUN.Text + ",</p><p> You have requested a reset password for " +
                            "e-Sanrakhshan account. The password for your e-Sanrakhshan Account has been " +
                            "changed.To get started with e-Sanrakhshan application below is your Login Details." +
                            "</p></div>"
                          + "<div style ='padding: 20px;background-color:white;font-size: 14px;'>"
                          + "<label><b>Email Address:</></label> " + txtEmailid.Text + " <br/><label>"
                                + "<b>UserName:</b>" + txtUN.Text + "</label><br /><label>"
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
                    message.To.Add(new MailAddress(txtEmailid.Text));
                    message.Subject = "Forgot Password Details";
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
                    string res = fl.AddResetPasswordData(userid, pass);
                    Response.Write("<script>alert('Mail has been sent to your registered Email ID.');window.location.href='login.aspx'</script>");


                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Response.Write("<script>alert('Message could not be sent.')</script>");

                }

            }
            else
            {
                Response.Write("<script>alert('Invalid registered Email ID or Username.')</script>");
            }

        }
    }
}