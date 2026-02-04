using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using System.ComponentModel.DataAnnotations;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string user = fl.GetRoleByID("-1");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                        BindChk();
                    }
                }
            }
            catch (Exception ex) { }
        }
    }

    public void BindChk()
    {
        string res = fl.GetRights(ddlRole.SelectedValue);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                string rights = dt.Rows[0]["rolerights"].ToString();

                if (rights.Contains(chkInstMaster.Value))
                    chkInstMaster.Checked = true;
                else
                    chkInstMaster.Checked = false;

                if (rights.Contains(chkDepatMaster.Value))
                    chkDepatMaster.Checked = true;
                else
                    chkDepatMaster.Checked = false;

                if (rights.Contains(chkCreateUser.Value))
                    chkCreateUser.Checked = true;
                else
                    chkCreateUser.Checked = false;

                if (rights.Contains(chkDivMaster.Value))
                    chkDivMaster.Checked = true;
                else
                    chkDivMaster.Checked = false;

                if (rights.Contains(chkRoleRights.Value))
                    chkRoleRights.Checked = true;
                else
                    chkRoleRights.Checked = false;

                if (rights.Contains(chkCreateRole.Value))
                    chkCreateRole.Checked = true;
                else
                    chkCreateRole.Checked = false;

                if (rights.Contains(chkHDDetails.Value))
                    chkHDDetails.Checked = true;
                else
                    chkHDDetails.Checked = false;

                if (rights.Contains(chkAssignCase.Value))
                    chkAssignCase.Checked = true;
                else
                    chkAssignCase.Checked = false;

                if (rights.Contains(chkDashboard.Value))
                    chkDashboard.Checked = true;
                else
                    chkDashboard.Checked = false;

                if (rights.Contains(chkAddCaseDetails.Value))
                    chkAddCaseDetails.Checked = true;
                else
                    chkAddCaseDetails.Checked = false;

                //if (rights.Contains(chkReport.Value))
                //    chkReport.Checked = true;
                //else
                //    chkReport.Checked = false;

                if (rights.Contains(chkReqDoc.Value))
                    chkReqDoc.Checked = true;
                else
                    chkReqDoc.Checked = false;


            }
            else
            {
                chkDivMaster.Checked = false;
                chkAssignCase.Checked = false;
                chkHDDetails.Checked = false;
                chkCreateRole.Checked = false;
                chkRoleRights.Checked = false;
                chkCreateUser.Checked = false;
                chkDepatMaster.Checked = false;
                chkInstMaster.Checked = false;
                chkDashboard.Checked = false;
                chkAddCaseDetails.Checked = false;
                //chkReport.Checked = false;
                chkReqDoc.Checked = false;
            }
        }
        else
        {
            chkDivMaster.Checked = false;
            chkAssignCase.Checked = false;
            chkHDDetails.Checked = false;
            chkCreateRole.Checked = false;
            chkRoleRights.Checked = false;
            chkCreateUser.Checked = false;
            chkDepatMaster.Checked = false;
            chkInstMaster.Checked = false;
            chkDashboard.Checked = false;
            chkAddCaseDetails.Checked = false;
            //chkReport.Checked = false;
            chkReqDoc.Checked = false;
        }

        if (ddlRole.SelectedItem.Text == "SuperAdmin")
        {
            chkDivMaster.Disabled = false;
            chkAssignCase.Disabled = false;
            chkHDDetails.Disabled = false;
            chkCreateRole.Disabled = false;
            chkRoleRights.Disabled = false;
            chkCreateUser.Disabled = false;
            chkDepatMaster.Disabled = false;
            chkInstMaster.Disabled = false;
            chkDashboard.Disabled = false;
            chkAddCaseDetails.Disabled = false;
            //chkReport.Disabled = false;
            chkReqDoc.Disabled = false;
        }
        else if (ddlRole.SelectedItem.Text == "Admin")
        {
            chkDivMaster.Disabled = false;
            chkAssignCase.Disabled = false;
            chkHDDetails.Disabled = false;
            chkCreateRole.Disabled = false;
            chkRoleRights.Disabled = false;
            chkCreateUser.Disabled = false;
            chkDepatMaster.Disabled = false;
            chkInstMaster.Disabled = true;
            chkDashboard.Disabled = false;
            chkAddCaseDetails.Disabled = false;
            //chkReport.Disabled = false;
            chkReqDoc.Disabled = false;
        }
        else
        {
            chkDivMaster.Disabled = true;
            chkAssignCase.Disabled = false;
            chkHDDetails.Disabled = false;
            chkCreateRole.Disabled = true;
            chkRoleRights.Disabled = true;
            chkCreateUser.Disabled = true;
            chkDepatMaster.Disabled = true;
            chkInstMaster.Disabled = true;
            chkDashboard.Disabled = false;
            chkAddCaseDetails.Disabled = false;
            //chkReport.Disabled = false;
            chkReqDoc.Disabled = false;
        }
    }
    protected void btnRights_Click(object sender, EventArgs e)
    {

        string rights = "";

        if (chkDivMaster.Checked)
            rights += chkDivMaster.Value + ",";

        if (chkAssignCase.Checked)
            rights += chkAssignCase.Value + ",";

        if (chkCreateUser.Checked)
            rights += chkCreateUser.Value + ",";

        if (chkRoleRights.Checked)
            rights += chkRoleRights.Value + ",";

        if (chkHDDetails.Checked)
            rights += chkHDDetails.Value + ",";

        if (chkCreateRole.Checked)
            rights += chkCreateRole.Value + ",";

        if (chkDepatMaster.Checked)
            rights += chkDepatMaster.Value + ",";

        if (chkInstMaster.Checked)
            rights += chkInstMaster.Value + ",";

        if (chkDashboard.Checked)
            rights += chkDashboard.Value + ",";

        if (chkAddCaseDetails.Checked)
            rights += chkAddCaseDetails.Value + ",";

        //if (chkReport.Checked)
        //    rights += chkReport.Value + ",";

        if (chkReqDoc.Checked)
            rights += chkReqDoc.Value + ",";

        rights = rights.Substring(0, rights.Length - 1);

        string res = fl.UpdateRights(rights, ddlRole.SelectedValue);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate("[" + res + "]");
            string status = dt.Rows[0]["status"].ToString();
            if (status == "200")
            {
                if (ddlRole.SelectedValue == Session["role"].ToString())
                {
                    Session["rolerights"] = rights;
                    Response.Redirect("RoleRights.aspx");
                }
            }
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindChk();
    }
}