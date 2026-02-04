using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class GetViewRequest : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    string res = fl.GetViewReq(ddlStatus.SelectedValue);
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(res);
                        if (dt.Rows.Count > 0)
                        {
                            grdAttach.DataSource = dt;
                            grdAttach.DataBind();
                        }
                        else
                        {
                            grdAttach.DataSource = null;
                            grdAttach.DataBind();
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string res = fl.GetViewReq(ddlStatus.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    grdAttach.DataSource = dt;
                    grdAttach.DataBind();
                }
                else
                {
                    grdAttach.DataSource = null;
                    grdAttach.DataBind();
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void grdAttach_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnID = e.Row.FindControl("hdnId") as HiddenField;
            HiddenField hdnStatus = e.Row.FindControl("hdnStatus") as HiddenField;
            HiddenField hdnReqID = e.Row.FindControl("hdnReqID") as HiddenField;
            HtmlButton btnApprove = e.Row.FindControl("btnApprove") as HtmlButton;
            HtmlButton btnRej = e.Row.FindControl("btnRej") as HtmlButton;
            //window.location.href='createuser.aspx?a=<%= Eval("userid") %>'
            //btnEdit.Attributes.Add("onclick", "window.location.href=\'createuser.aspx?a=" + hdnID.Value + "\'");
            if (hdnStatus.Value != "P")
            {
                btnRej.Disabled = true;
                btnApprove.Disabled = true;
            }
            btnRej.Attributes.Add("onclick", "ViewRequestReject('" + hdnID.Value
                + "','R','" + e.Row.Cells[3].Text + "','" + Session["div_code"].ToString() + "','" + hdnReqID.Value + "')");
            btnApprove.Attributes.Add("onclick", "ViewRequest('" + hdnID.Value
                + "','A','','" + e.Row.Cells[3].Text + "','" + Session["div_code"].ToString() + "','" + hdnReqID.Value + "')");
        }
    }

    [WebMethod]
    public static string SendMail(string Id, string tomailid, string code, string reqid, string status, string notes)
    {
        string data, htmlString, Link, subject = "";
        FlureeCS fl = new FlureeCS();
        try
        {
            data = fl.UpdateRequestStatus(reqid, status, notes);
            if (!data.StartsWith("Error"))
            {
                if (status == "A")
                {
                    Link = HttpContext.Current.Request.Url.Authority;
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        Link += HttpContext.Current.Request.ApplicationPath;
                    Link += "/GetCaseDetails.aspx?caseno=" +
                    Id.Replace(@"\", "\\\\") + "&div=" + code;
                    htmlString = getHtmlAccept(Link, "");
                    subject = "Link to view Case Report";
                }
                else
                {
                    Link = "";
                    htmlString = getHtmlAccept(Link, notes);
                    subject = "Your Request has been Rejected";
                }
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("skill4sale308@gmail.com");
                message.To.Add(new MailAddress(tomailid));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                //smtp.Port = 587;
                //smtp.Host = "smtp.gmail.com"; //for gmail host  
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("saas.iware@gmail.com", "1nt3RACT");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return "Mail has been sent successfully!";
            }
            else
            {
                return "Something went wrong please try again later.";
            }
        }
        catch (Exception ex)
        {
            return "Something went wrong please try again later.";
        }
    }

    public static string getHtmlAccept(string Link, string notes)
    {
        try
        {
            string html = "";
            if (Link != "")
            {
                html = "<div style='width: 80%;height: auto;bottom: 0;left: 0;right: "
                     + "0;margin: auto;margin-bottom: 30px;margin-top:20px'><div style='border: "
                     + "3px solid #f1f1f1;font-family: Arial;'><div style='padding: 20px;background-color:"
                     + " #f1f1f1;'><h4>Link to view Case Report</h4><p>The following is the link to access the reports and other"
                     + " documents related to the case you have requested for:</p></div>"
                     + "<div style='padding: 20px;background-color:white;font-size: 14px;'>"
                     + "<label><b>Link To Access:</b> " + Link + "</label><br />"
                     + "</div> "
                     + "</div></div><h3>Regards,<br/><b>S.S.B Forensic Tool</b></h3>";
            }
            else
            {
                html = "<div style='width: 80%;height: auto;bottom: 0;left: 0;right: "
                  + "0;margin: auto;margin-bottom: 30px;margin-top:20px'><div style='border: "
                  + "3px solid #f1f1f1;font-family: Arial;'><div style='padding: 20px;background-color:"
                  + " #f1f1f1;'><h4>Your Request has been Rejected</h4></div>"
                  + "<div style='padding: 20px;background-color:white;font-size: 14px;'>"
                  + "<label><b>Reason Given By the authority is:</b> " + notes + "</label><br />"
                  + "</div> "
                  + "</div></div><h3>Regards,<br/><b>S.S.B Forensic Tool</b></h3>";
            }
            return html;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}