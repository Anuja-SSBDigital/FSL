using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null) {
            lblDesignation.Text = Session["designation"].ToString();
            lblUNMaster.Text = Session["username"].ToString();
            lblUserMaster.Text = Session["firstname"].ToString() + " "
                + Session["lastname"].ToString();

            if (Session["role"].ToString() == "Officer")
            {
                liMaster.Visible = false;
                liMasterM.Visible = false;
            }
            else
            {
                liMaster.Visible = true;
                liMasterM.Visible = true;
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

            if (rights.Contains("GenCase"))
            {
                liGenCase.Visible = true;
                liGenCaseM.Visible = true;
            }
            else
            {
                liGenCase.Visible = false;
                liGenCaseM.Visible = false;
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
                liDept.Visible = true;
                liDeptM.Visible = true;
            }
            else
            {
                liDept.Visible = false;
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


            //if (rights.Contains("Report"))
            //{
            //    liReport.Visible = true;
            //    liReportM.Visible = true;
            //}
            //else
            //{
            //    liReport.Visible = false;
            //    liReportM.Visible = false;
            //}
        }
        else
            Response.Redirect("../Login.aspx");
    }
}
