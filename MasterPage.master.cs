using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPage : System.Web.UI.MasterPage
{


    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] != null)
            {
                lblDesignation.Text = Session["designation"].ToString();
                lblUNMaster.Text = Session["username"].ToString();
                lblUserMaster.Text = Session["firstname"].ToString() + " "
                    + Session["lastname"].ToString();
                string Role = Session["role"].ToString();

                if (Role != "SuperAdmin")
                {

                    DFS_Title.InnerHtml = "Forensic Science Laboratory";
                    //Sub_Title.InnerHtml = "<br>(Home Department  Government of Gujarat)";



                }
                else
                {
                    //Sub_Title.InnerHtml = "<br>(Home Department  Government of Gujarat)";
                    DFS_Title.InnerHtml = "Forensic Science Laboratory";
                }
                string NotificationDiv = "";
                li1.Visible = false;
                liCUL.Visible = false;
                if (Role == "Sample Warden")
                {
                    liMaster.Visible = false;
                    liMasterM.Visible = false;
                    liCaseDM.Visible = false;
                    liCaseD.Visible = false;
                    //liViewReq.Visible = false;
                    //liViewReqM.Visible = false;
                    //liManageReqM.Visible = false;
                    //liManageReq.Visible = false;
                    liVeriM.Visible = false;
                    liVeri.Visible = false;
                    //liManageRequestM.Visible = false;
                    //liManageRequest.Visible = false;
                }
                else if (Role == "Officer")
                {
                    BellIcon.Visible = true;
                    liMaster.Visible = false;
                    liMasterM.Visible = false;
                    // lichangestatus.Visible = true;
                    // lichangestatusd.Visible = true;
                    liCaseD.InnerHtml = "<a href ='AddCaseDetails.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    liCaseDM.InnerHtml = "<a href ='AddCaseDetails.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    AllNoti.Visible = false;
                    string RequestedbyName = Session["firstname"].ToString() + " " + Session["lastname"].ToString() + "("
                        + Session["username"].ToString() + ")";
                    string res = fl.GetReportRequestDetail(Session["dept_code"].ToString(),
                        Session["inst_code"].ToString(), RequestedbyName);

                    int CountNotification = 0;
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dtdata = fl.Tabulate(res);
                        if (dtdata.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtdata.Rows.Count; i++)
                            {
                                string caseno = dtdata.Rows[i]["caseno"].ToString();
                                string hodstatus = dtdata.Rows[i]["hodstatus"].ToString();
                                string requestedby = dtdata.Rows[i]["requestedby"].ToString();
                                string requestedon = dtdata.Rows[i]["requestedon"].ToString();
                                string hodremarks = dtdata.Rows[i]["hodremarks"].ToString();
                                string officerstatus = dtdata.Rows[i]["officerstatus"].ToString();
                                string RequestedOn = FlureeCS.Epoch.AddMilliseconds(
                                          Convert.ToInt64(requestedon)).ToString("dd-MMM-yyyy HH:mm:ss");


                                if (hodstatus == "Approve")
                                {
                                    CountNotification++;

                                    NotificationDiv += "<div class='notifi__item'>"
                                                    + "<div class='bg-c1 img-cir img-40'><i class='zmdi zmdi-comment-alt-text'></i></div>"
                                                    + "<div class='content'>"
                                                    + "<h5>" + caseno + "</h5>"
                                                    //+ "<p>" + requestedby + "</p>"
                                                    + "<span class='date'>" + RequestedOn + "</span></div></div>";
                                }
                                if (hodstatus == "Reject")
                                {
                                    CountNotification++;

                                    NotificationDiv += "<div class='notifi__item'>"
                                    + "<div class='bg-c2 img-cir img-40'>"
                                    + "<i class='zmdi zmdi-alert-triangle'></i></div>"
                                    + "<div class='content'>"
                                      + "<h5>" + caseno + "</h5>"
                                    + "<p>" + hodremarks + "</p>"
                                    + "<span class='date'>" + RequestedOn + "</span>"
                                    + "</div></div>";
                                }
                            }
                            CountNoti.InnerText = Convert.ToString(CountNotification);
                            MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                            Notification.InnerHtml = NotificationDiv;
                        }
                        else
                        {
                            CountNoti.InnerText = Convert.ToString(CountNotification);
                            MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                        }
                    }
                    else
                    {
                        CountNoti.InnerText = Convert.ToString(CountNotification);
                        MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                    }

                }
                else if (Role == "Department Head")
                {
                    BellIcon.Visible = true;
                    liDeptM.Visible = false;
                    liDept.Visible = false;
                    liRequestM.Visible = true;
                    liRequest.Visible = true;
                    liResetPasswordM.Visible = true;
                    liResetPassword.Visible = true;
                    //  lichangestatus.Visible = true;
                    // lichangestatusd.Visible = true;
                    liCaseD.InnerHtml = "<a href ='AddCaseDetails.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    liCaseDM.InnerHtml = "<a href ='AddCaseDetails.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    AllNoti.Visible = true;
                    string res = fl.GetReportrequestdetails(Session["dept_code"].ToString(), Session["inst_code"].ToString());

                    int CountNotification = 0;
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dtdata = fl.Tabulate(res);
                        if (dtdata.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtdata.Rows.Count; i++)
                            {
                                string caseno = dtdata.Rows[i]["caseno"].ToString();
                                string hodstatus = dtdata.Rows[i]["hodstatus"].ToString();
                                string requestedby = dtdata.Rows[i]["requestedby"].ToString();
                                string requestedon = dtdata.Rows[i]["requestedon"].ToString();
                                string officerstatus = dtdata.Rows[i]["officerstatus"].ToString();
                                string RequestedOn = FlureeCS.Epoch.AddMilliseconds(
                                          Convert.ToInt64(requestedon)).ToString("dd-MMM-yyyy HH:mm:ss");

                                if (officerstatus == "Requested" && hodstatus == "")
                                {
                                    CountNotification++;
                                    NotificationDiv += "<div class='notifi__item'>"
                                                                                       + "<div class='bg-c1 img-cir img-40'><i class='zmdi zmdi-comment-alt-text'></i></div>"
                                                                                       + "<div class='content'>"
                                                                                       + "<h5>" + caseno + "</h5>"
                                                                                       + "<p>" + requestedby + "</p>"
                                                                                       + "<span class='date'>" + RequestedOn + "</span></div></div>";
                                }
                            }
                            CountNoti.InnerText = Convert.ToString(CountNotification);
                            MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                            Notification.InnerHtml = NotificationDiv;
                        }
                        else
                        {
                            CountNoti.InnerText = Convert.ToString(CountNotification);
                            MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                        }
                    }
                    else
                    {
                        CountNoti.InnerText = Convert.ToString(CountNotification);
                        MessageNoti.InnerText = "You have " + CountNotification + " Notifications";

                    }
                }
                else if (Role == "Admin" || Role == "SuperAdmin" ||
                    Role == "Assistant Director" || Role == "Additional Director" ||
                    Role == "Deputy Director")
                {
                    //liCaseD.InnerHtml = "<a href ='AddCaseDetails_DI.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    //liCaseDM.InnerHtml = "<a href ='AddCaseDetails_DI.aspx'><i class='fas  fa-file-alt'></i>Update Case Details</a>";
                    AllNoti.Visible = true;
                    li1.Visible = true;
                    liCUL.Visible = true;

                }
                else
                {
                    liMaster.Visible = true;
                    liMasterM.Visible = true;
                }
                if (Role == "Admin" || Role == "Deputy Director")
                {
                    liCaseD.Visible = false;
                    liCaseDM.Visible = false;
                }
                if (Role == "Additional Director")
                {
                    liCaseD.Visible = false;
                    liCaseDM.Visible = false;
                }
                if (Role == "SuperAdmin")
                {
                    liResetPasswordM.Visible = true;
                    liResetPassword.Visible = true;

                    //liRequestM.Visible = true;
                    //liRequest.Visible = true;
                }
                string rights = Session["rights"].ToString();
                if (rights.Contains("Dashboard"))
                {
                    liDash.Visible = true;
                    liDashM.Visible = true;
                }
                else
                {
                    liDash.Visible = false;
                    liDashM.Visible = false;
                }
                if (rights.Contains("DivMas"))
                {
                    liDiv.Visible = true;
                    liDivM.Visible = true;
                }
                else
                {
                    liDiv.Visible = false;
                    liDivM.Visible = false;
                }

                if (rights.Contains("AssignCase"))
                {
                    liAssign.Visible = true;
                    liAssignM.Visible = true;
                }
                else
                {
                    liAssign.Visible = false;
                    liAssignM.Visible = false;
                }

                if (rights.Contains("CreateUser"))
                {
                    liCU.Visible = true;
                    liCUM.Visible = true;
                }
                else
                {
                    liCU.Visible = false;
                    liCUM.Visible = false;
                }

                if (rights.Contains("RoleRights"))
                {
                    liRR.Visible = true;
                    liRRM.Visible = true;
                }
                else
                {
                    liRR.Visible = false;
                    liRRM.Visible = false;
                }

                if (rights.Contains("HDDetails"))
                {
                    liHD.Visible = true;
                    liHDM.Visible = true;
                }
                else
                {
                    liHD.Visible = false;
                    liHDM.Visible = false;
                }

                if (rights.Contains("CreateRole"))
                {
                    liRole.Visible = true;
                    liRoleM.Visible = true;
                }
                else
                {
                    liRole.Visible = false;
                    liRoleM.Visible = false;
                }

                if (rights.Contains("DepMas"))
                {
                    liDeptM.Visible = false;
                }

                if (rights.Contains("InstMas"))
                {
                    liInst.Visible = true;
                    liInstM.Visible = true;
                }
                else
                {
                    liInst.Visible = false;
                    liInstM.Visible = false;
                }


                if (rights.Contains("Report"))
                {
                    liReport.Visible = true;
                    liReportM.Visible = true;
                }
                else
                {
                    liReport.Visible = false;
                    liReportM.Visible = false;
                }

                if (rights.Contains("EvidenceAcceptance"))
                {
                    liEA.Visible = true;
                    liEAM.Visible = true;
                }
                else
                {
                    liEA.Visible = false;
                    liEAM.Visible = false;
                }

                if (rights.Contains("EditEvidence"))
                {
                    liEditEvidenceM.Visible = true;
                    liEditEvidence.Visible = true;

                }
                else
                {
                    liEditEvidence.Visible = false;
                    liEditEvidenceM.Visible = false;
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string res = fl.Updatereportrequest(hf_requestid.Value, ddl_status.SelectedValue, txt_remarks.Text, Session["firstname"].ToString() + " " + Session["lastname"].ToString() + "(" + Session["username"].ToString() + ")");

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                Response.Write("<script>alert('Status Has been Changed');window.location='request.aspx'</script>");

            }
            else
            {

                Response.Write("<script>alert('Something went wrong..!!');</script>");

            }
        }
    }
}
