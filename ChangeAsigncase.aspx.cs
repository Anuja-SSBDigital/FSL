using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
[assembly: log4net.Config.XmlConfigurator(ConfigFile =
                "log4net.config", Watch = true)]
public partial class ChangeAsigncase : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["inst_id"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            fill_department();
        }


    }
    public void fill_department()
    {


        string res = fl.GetDeptById(Session["inst_id"].ToString());
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "dept_name";
                ddlDepartment.DataValueField = "dept_code";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
            }
        }
    }
    protected void btnSearchCase_Click(object sender, EventArgs e)
    {
        string caseNo = txtCaseNo.Text.Trim();
        string Division = "";
        Division = ddlDepartment.SelectedValue;

        if (string.IsNullOrEmpty(caseNo))
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please enter case number.";
            //divCaseDetails.Visible = false;
            return;
        }
        if (string.IsNullOrEmpty(Division))
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Please Select Department.";
            //divCaseDetails.Visible = false;
            return;
        }
        string res = fl.SearchEvidenceByCaseOrDepartmentincaseasign(caseNo, Division);
        if (!res.StartsWith("Error"))
        {
            JArray dataArray = JArray.Parse(res);

            if (dataArray.Count > 0)
            {
                title.InnerHtml = "";
                title.InnerText = "Case Details Found Successfully";
                casedata.Visible = true;
                user_Div.Visible = true;
                ChangeUser_div.Visible = true;
                lblMessage.Text = "";
                lblMessage.Text = "";
                div_rpt.Visible = true; // show the form or div
                // Take first record
                JObject obj = (JObject)dataArray[0];
                string fullCaseNo = obj["caseno"].ToString(); // FSL/EE/2025/FPB/20221
                string idofcase = obj["evidenceid"].ToString();
                string Caseassign_userid = obj["caseassign_userid"].ToString();
                string Caseassign_inst_code = obj["inst_code"].ToString();
                string Caseassign_div_code = obj["div_code"].ToString();
                string resforuserfind = fl.GetUsersForUpdate(Caseassign_userid, Caseassign_inst_code, Caseassign_div_code);
                if (!resforuserfind.StartsWith("Error"))
                {
                    JArray dataArrayGetUsers = JArray.Parse(resforuserfind);

                    if (dataArrayGetUsers.Count > 0)
                    {
                        JObject objforuser = (JObject)dataArrayGetUsers[0];
                        string Usernameis = objforuser["firstname"].ToString() + " " + objforuser["lastname"].ToString() + " (" + objforuser["username"].ToString() + ") ";
                        Username.Text = Usernameis;
                    }

                    hdncasenum.Value = fullCaseNo;
                    hdn_idofcase.Value = idofcase;
                    // Split case number
                    // Assuming format: PREFIX/YEAR/SECTION/SERIAL
                    string[] parts = fullCaseNo.Split('/');

                    if (parts.Length >= 5)
                    {

                        txtPrefix.Text = parts[0] + "/" + parts[1] + "/";
                        txtYear.Text = parts[2];
                        txtSection.Text = parts[3];
                        txtSerial.Text = parts[4];
                    }
                    else
                    {
                        //error.InnerText = "No Data Found ";
                        casedata.Visible = false;
                        // fallback if unexpected format
                        txtPrefix.Text = "";
                        txtYear.Text = "";
                        txtSection.Text = "";
                        txtSerial.Text = fullCaseNo;
                    }



                }
                else
                {
                    div_rpt.Visible = false; // show the form or div
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "No Data Found ";
                    casedata.Visible = false;
                    // fallback if unexpected format
                    txtPrefix.Text = "";
                    txtYear.Text = "";
                    txtSection.Text = "";


                }
                fill_user_departmetwise(ddlDepartment.SelectedValue);

            }
            else
            {
                div_rpt.Visible = false; // show the form or div
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "No Data Found ";
                casedata.Visible = false;
                // fallback if unexpected format
                txtPrefix.Text = "";
                txtYear.Text = "";
                txtSection.Text = "";


            }



        }
        else
        {
            div_rpt.Visible = false; // show the form or div
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "No Data Found ";
            casedata.Visible = false;
            // fallback if unexpected format
            txtPrefix.Text = "";
            txtYear.Text = "";
            txtSection.Text = "";


        }
    }


    public void fill_user_departmetwise(string Depcode)
    {
        string rescode = "";
        rescode = fl.GetUsersDeptcodewiseafterIndexchanges(Depcode);
        DataTable dt = fl.Tabulate(rescode);
        ddl_chnageusername.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddl_chnageusername.DataSource = dt;
            ddl_chnageusername.DataTextField = "Firstname";
            ddl_chnageusername.DataValueField = "userid";
            ddl_chnageusername.DataBind();

            ddl_chnageusername.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        }
        else
        {
            ddl_chnageusername.Items.Add(new ListItem("No user found", "-1"));
        }
    }


    protected void btn_changeUsercaseasign_Click(object sender, EventArgs e)
    {

        string caseNo = hdncasenum.Value;
        string evdnid = hdn_idofcase.Value;
        string newasignuserid = ddl_chnageusername.SelectedValue;
        string residtrns = fl.chnageUserAsigneUser(caseNo, newasignuserid, evdnid);
        DataTable dtdata = fl.Tabulate("[" + residtrns + "]");
        if (dtdata.Rows.Count > 0)
        {
            if (dtdata.Rows[0]["status"].ToString() == "200")
            {
                string resfortrackdata = fl.Searchdataintrackmaster(caseNo);
                if (!string.IsNullOrEmpty(resfortrackdata) && !resfortrackdata.StartsWith("Error"))
                {
                    JArray trackArray = JArray.Parse(resfortrackdata);
                    bool allUpdated = true;

                    foreach (JObject trackObj in trackArray)
                    {
                        try
                        {
                            string trackRecordId = trackObj["_id"].ToString();
                            string trackId = trackObj["trackmaster/trackid"].ToString();

                            if (!string.IsNullOrEmpty(trackRecordId) && !string.IsNullOrEmpty(trackId))
                            {
                                // Call update function
                                string updateResult = fl.UpdateTrackMasterAsign(trackRecordId, trackId, newasignuserid);
                                DataTable dtUpdate = fl.Tabulate("[" + updateResult + "]");

                                if (dtUpdate.Rows.Count > 0 && dtUpdate.Rows[0]["status"].ToString() != "200")
                                {
                                    allUpdated = false;
                                    // Log failed update
                                    log.Info("Failed to update TrackMaster: TrackId={trackId}");
                                    continue;
                                }
                            }
                            else
                            {
                                allUpdated = false;
                                log.Info("Missing trackId or recordId for trackmaster record.");
                            }
                        }
                        catch (Exception ex)
                        {
                            allUpdated = false;
                            log.Info("Exception updating trackmaster record: " + ex.Message);
                            continue; // skip this record
                        }
                    }

                    if (allUpdated)
                    {
                        log.Info("allUpdated");

                        string selecteduserID = ddl_chnageusername.SelectedValue;
                        string residcase = fl.SearchIDinusermstr(selecteduserID);
                        if (!residcase.StartsWith("Error"))
                        {
                            try
                            {
                                JArray dataArray = JArray.Parse(residcase);
                                if (dataArray.Count > 0)
                                {
                                    JObject obj = (JObject)dataArray[0];
                                    string idofuser = obj["_id"].ToString();
                                    string resforuseridcase = fl.chnagecasemasterByUserid(caseNo, idofuser);
                                    DataTable dtcasedata = fl.Tabulate("[" + resforuseridcase + "]");

                                    if (dtcasedata.Rows.Count > 0 && dtcasedata.Rows[0]["status"].ToString() == "200")
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", "alert('All records updated successfully!'); window.location='searchreport.aspx';", true);

                                    }
                                    else
                                    {
                                        // Error updating case master
                                        log.Info("Error updating case master: " + residcase);
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update case master record.');", true);
                                    }
                                }
                                else
                                {
                                    // No data returned from case master search
                                    log.Info("No case master record found for caseNo: " + caseNo);
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No case master record found.');", true);
                                }
                            }
                            catch (Exception ex)
                            {
                                // JSON parsing or unexpected error
                                log.Info("Exception processing case master data: " + ex.Message);
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error processing case master data.');", true);
                            }
                        }
                        
                    }
                    else
                    {
                        // Some records failed
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Some trackmaster records failed to update. Check logs for details.');", true);
                    }
                }
                else
                {
                    // Error fetching trackmaster data
                    log.Info("Error fetching trackmaster data: " + resfortrackdata);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to fetch trackmaster data.');", true);
                }
            }
            else
            {
                // dtdata has no rows
                log.Info("No data found in dtdata for caseNo: " + caseNo);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No data found.');", true);
            }
        }
    }
}