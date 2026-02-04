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

public partial class Timeline : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    public void gettimelinedetails()
    {
        try
        {
            string CaseNo = Request.QueryString["caseno"];
            string DeptCode = "";
            if (Session["role"].ToString() == "Admin" ||
                Session["role"].ToString() == "Assistant Director" ||
                Session["role"].ToString() == "Additional Director" ||
                Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
            {
                DeptCode = "";
            }
            else
            {
                DeptCode = Session["dept_code"].ToString();
            }
            string userid = "";
            if (Session["role"].ToString() == "Officer")
            {
                userid = Session["userid"].ToString();
            }
            else
            {
                userid = "";
            }
            string res = fl.GetCaseuserwise(CaseNo,userid);
            if (!res.StartsWith("Error"))
            {
                DataTable dtcheck = fl.Tabulate(res);
                if (dtcheck.Rows.Count > 0)
                {

                    timeline_title.InnerHtml = "Timeline for Case No:" + CaseNo;
                    string resAttachment = fl.GetCasewiseAttachment(CaseNo);
                    string resUser = fl.GetUserAcceptanceDetails(CaseNo);
                    string resTrack = fl.GetTrackDetails(CaseNo);
                    if (!resUser.StartsWith("Error") && !resTrack.StartsWith("Error") && !resAttachment.StartsWith("Error"))
                    {
                        string pathAttachment = "";

                        DataTable dtAttachment = fl.Tabulate(resAttachment);
                        DataTable dt = fl.Tabulate(resUser);
                        DataTable dt_data = fl.Tabulate(resTrack);
                        if (dtAttachment.Rows.Count > 0)
                        {
                            pathAttachment = dtAttachment.Rows[0]["path"].ToString();
                        }
                        if (dt.Rows.Count > 0)
                        {
                            string agencyreferanceno = dt.Rows[0]["agencyreferanceno"].ToString();
                            string agencyname = dt.Rows[0]["agencyname"].ToString();
                            string recieptfile = dt.Rows[0]["receiptfilepath"].ToString();

                            if (dt_data.Rows.Count > 0)
                            {

                                for (int i = 0; i < dt_data.Rows.Count; i++)
                                {
                                    string status = dt_data.Rows[i]["status"].ToString();
                                    string caseno = dt_data.Rows[i]["caseno"].ToString();
                                    string caseassignby = dt_data.Rows[i]["caseassignby"].ToString();
                                    string notes = dt_data.Rows[i]["notes"].ToString();
                                    string createddate = dt_data.Rows[i]["createddate"].ToString();
                                    string statuschangedby = dt_data.Rows[i]["statuschangedby"].ToString();
                                    string coverteddate = FlureeCS.Epoch.AddMilliseconds(
                                            Convert.ToInt64(createddate)).ToString("dd-MMM-yyyy HH:mm:ss");
                                    string assignto = dt_data.Rows[i]["assignto"].ToString();

                                    string firstname = "";
                                    string lastname = "";
                                    string username = "";
                                    string firstname_assign = "";
                                    string lastname_assign = "";
                                    string username_assign = "";

                                    string userdata = fl.GetUserDetails(assignto);
                                    if (!userdata.StartsWith("Error"))
                                    {
                                        DataTable dtuser = fl.Tabulate(userdata);

                                        if (dtuser.Rows.Count > 0)
                                        {
                                            firstname = dtuser.Rows[0]["firstname"].ToString();
                                            lastname = dtuser.Rows[0]["lastname"].ToString();
                                            username = dtuser.Rows[0]["username"].ToString();

                                        }
                                    }
                                    string caseassigndata = fl.GetUserDetailsforCaseAssignBY(caseassignby);
                                    if (!caseassigndata.StartsWith("Error"))
                                    {
                                        DataTable dtuser = fl.Tabulate(caseassigndata);

                                        if (dtuser.Rows.Count > 0)
                                        {
                                            firstname_assign = dtuser.Rows[0]["firstname"].ToString();
                                            lastname_assign = dtuser.Rows[0]["lastname"].ToString();
                                            username_assign = dtuser.Rows[0]["username"].ToString();

                                        }
                                    }
                                    string backgroundcolour = "";
                                    string icon = "";

                                    if (status == "Assigned")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-picture";
                                        icon = "<i class='fas fa-bars iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Pending for Assign")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-Pendingassign";
                                        icon = "<i class='fas fa-clock iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Inprogress")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-movie";
                                        icon = "<i class='fas fa-spinner iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Mismatch Found")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-Signature";
                                        icon = "<i class='fas fa-times iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Report Preparation")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-location";
                                        icon = "<i class='fas fa-copy iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Pending for Director/HOD Signature")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-Signature";
                                        icon = "<i class='fas fa-file iconcss fa-2x'></i>";
                                    }
                                    else if (status == "Report Submission")
                                    {
                                        backgroundcolour = "cd-timeline-img cd-Completed";
                                        icon = "<i class='fas fa-clipboard iconcss fa-2x'></i>";
                                    }



                                    this.genDIV.InnerHtml += "<div class='cd-timeline-block'>";

                                    this.genDIV.InnerHtml += "<div class='" + backgroundcolour + "'>";

                                    this.genDIV.InnerHtml += " <div class='overview - box icon i'>" + icon + "</div>";
                                    this.genDIV.InnerHtml += "</div>";

                                    this.genDIV.InnerHtml += "<div class='cd-timeline-content'>";

                                    if (status == "Pending for Assign")
                                    {
                                        this.genDIV.InnerHtml += "<h2>Case Recieved: " + status + "</h2>";

                                        this.genDIV.InnerHtml += "<p><b>Police Sation Name: </b>" + agencyname + "</p>";
                                        this.genDIV.InnerHtml += "<p><b>Reference No: </b>" + agencyreferanceno + "</p>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";

                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        if (recieptfile != "")
                                        {
                                            this.genDIV.InnerHtml += "<b>View Reciept: </b><a href='" + recieptfile + "' target='_blank' class='alert-link'>Click Here</a>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<b>View Reciept:</b> No file Uploaded";
                                        }

                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }

                                    if (status == "Assigned")
                                    {
                                        //if (Session["role"].ToString() == "Officer")
                                        //{
                                        this.genDIV.InnerHtml += "<h2>Assigned: Pending For Investigation</h2>";
                                        this.genDIV.InnerHtml += "<p><b>Police Sation Name: </b>" + agencyname + "</p>";
                                        this.genDIV.InnerHtml += "<p><b>Reference No: </b>" + agencyreferanceno + "</p>";
                                        this.genDIV.InnerHtml += "<p><b>Case AssignBy: </b>" + firstname_assign + " " + lastname_assign + " (" + username_assign + ") </p>";
                                        this.genDIV.InnerHtml += "<p><b>Assign To: </b>" + firstname + " " + lastname + " (" + username + ") </p>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";



                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        if (recieptfile != "")
                                        {
                                            this.genDIV.InnerHtml += "<b>View Reciept: </b><a href='" + recieptfile + "' target='_blank' class='alert-link'>Click Here</a>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<b>View Reciept:</b> No file Uploaded";
                                        }
                                       
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";
                                        //}
                                        //else
                                        //{

                                        //    this.genDIV.InnerHtml += "<h2>Assigned: Pending For Investigation</h2>";
                                        //    this.genDIV.InnerHtml += "<p><b>Case AssignBy: </b>" + firstname_assign + " " + lastname_assign + " (" + username_assign + ") </p>";
                                        //    this.genDIV.InnerHtml += "<p><b>Assign To: </b>" + firstname + " " + lastname + " (" + username + ") </p>";
                                        //    this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";



                                        //    if (notes != "")
                                        //    {
                                        //        this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        //    }
                                        //    else
                                        //    {
                                        //        this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        //    }
                                        //    this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";
                                        //}
                                    }
                                    if (status == "Inprogress")
                                    {

                                        this.genDIV.InnerHtml += "<h2>Inprogress: Investigation going On</h2>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</b></p>";

                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }

                                    if (status == "Report Preparation")
                                    {

                                        this.genDIV.InnerHtml += "<h2>Investigation Completed: Report Preparartion going On</h2>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";

                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }

                                    if (status == "Pending for Director/HOD Signature")
                                    {

                                        this.genDIV.InnerHtml += "<h2>Pending for Signature</h2>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";

                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }

                                    if (status == "Report Submission")
                                    {

                                        this.genDIV.InnerHtml += "<h2>Final Report Submitted</h2>";
                                        this.genDIV.InnerHtml += "<p><b>Status Changed By: </b>" + statuschangedby + "</p>";

                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        this.genDIV.InnerHtml += "<b>View Files: </b><a href ='ViewDetails.aspx?caseno=" + CaseNo + "' class='alert-link'>Click Here</a>";
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }


                                    if (status == "Mismatch Found")
                                    {

                                        this.genDIV.InnerHtml += "<h2>Mismatch Found in Evidence</h2>";
                                        if (notes != "")
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks: </b>" + notes + "</p>";
                                        }
                                        else
                                        {
                                            this.genDIV.InnerHtml += "<p><b>Remarks:</b>---------</p>";


                                        }
                                        this.genDIV.InnerHtml += "<p style='float:right;'><b>" + coverteddate + "</b></p>";

                                    }




                                    this.genDIV.InnerHtml += "</div>";
                                    this.genDIV.InnerHtml += "</div> ";

                                }
                                //this.genDIV.InnerHtml = "</section>";
                            }
                        }
                    }
                }
                else
                {
                    if (Session["role"].ToString() == "Admin" ||
              Session["role"].ToString() == "Assistant Director" ||
              Session["role"].ToString() == "Additional Director" ||
              Session["role"].ToString() == "Deputy Director")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('This Case No Does Not Exist in the System');window.location.href='ViewTimeline.aspx'</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
              "<script>alert('This Case No Does Not Exist in the System');window.location.href='CaseAttachment.aspx'</script>");

                    }
                }
            }








        }
        catch (Exception ex) { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                gettimelinedetails();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }


}