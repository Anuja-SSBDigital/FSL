using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Changecasenumber : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

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
        string res = fl.SearchEvidenceByCaseOrDepartment(caseNo, Division);
        if (!res.StartsWith("Error"))
        {
            JArray dataArray = JArray.Parse(res);

            if (dataArray.Count > 0)
            {
                title.InnerHtml = "";
                title.InnerText = "Case Details Found Successfully";
                casedata.Visible = true;
                lblMessage.Text = "";
                lblMessage.Text = "";
                div_rpt.Visible = true; // show the form or div
                // Take first record
                JObject obj = (JObject)dataArray[0];
                string fullCaseNo = obj["caseno"].ToString(); // FSL/EE/2025/FPB/20221
                string idofcase = obj["evidenceid"].ToString(); // FSL/EE/2025/FPB/20221
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
        }

    }


    protected void btnAjaxSearch_Click(object sender, EventArgs e)
    {
        string caseNo = hdncasenum.Value;
        string yearofnew = txtYear.Text;
        string insideid = hdn_idofcase.Value;



        string[] parts = caseNo.Split('/');

        if (parts.Length >= 3)
        {
            parts[2] = yearofnew;   // Replace year part
        }

        string newCaseNo = string.Join("/", parts);

        string residtrns = fl.chnageEvidenceByCase(newCaseNo, insideid);
        DataTable dtdata = fl.Tabulate("[" + residtrns + "]");
        string Division = "";
        Division = ddlDepartment.SelectedValue;
        if (dtdata.Rows.Count > 0)
        {
            if (dtdata.Rows[0]["status"].ToString() == "200")
            {
                string resforcasedate = fl.SearchEvidenceByCaseOrDepartmentincasemaster(caseNo, Division);
                if (!resforcasedate.StartsWith("Error"))
                {

                    JArray dataArray = JArray.Parse(resforcasedate);
                    JObject obj = (JObject)dataArray[0];

                    string idofcase = obj["_id"].ToString();
                    string residcase = fl.chnagecasemasterByCase(newCaseNo, idofcase);
                    DataTable dtcasedata = fl.Tabulate("[" + residcase + "]");
                    if (dtcasedata.Rows[0]["status"].ToString() == "200")
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
    }
}