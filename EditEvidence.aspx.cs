using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
public partial class EditEvidence : System.Web.UI.Page
{

    FlureeCS fl = new FlureeCS();

    public void filldepartment()
    {
        string InstID = Session["inst_id"].ToString();
        if (Session["role"].ToString() == "Officer" || Session["role"].ToString() == "Department Head")
        {
            string res = fl.GetDeptById(InstID);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDep.DataSource = dt;
                    ddlDep.DataTextField = "dept_name";
                    ddlDep.DataValueField = "dept_code";
                    ddlDep.DataBind();
                    ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    ddlDep.SelectedValue = Session["dept_code"].ToString();
                    ddlDep.Attributes.Add("disabled", "disabled");
                }
            }
        }
        else
        {

            string res = fl.GetDeptById(InstID);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDep.DataSource = dt;
                    ddlDep.DataTextField = "dept_name";
                    ddlDep.DataValueField = "dept_code";
                    ddlDep.DataBind();
                    ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
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
                filldepartment();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


   

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        filldepartment();
        div_rpt.Visible = true;
        double fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
        double td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));
        string user = "";
        if(Session["role"].ToString() == "Officer")
        {
            user = Session["userid"].ToString();
        }
        else
        {
            user = "";
        }
        string res = fl.GetEvidenceDetails(ddlDep.SelectedValue, fd.ToString(), td.ToString(), user,Session["inst_code"].ToString());

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate(res);
            if (dtdata.Rows.Count > 0)
            {
              
                Header.Visible = true;
                if (txt_fromdate.Text != "" && txt_todate.Text != "")
                {
                    title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
                }
                else
                {
                    title.InnerText = "Evidence data of " + ddlDep.SelectedItem + " Division ";

                }
                rpt_details.DataSource = dtdata;
                rpt_details.DataBind();
            }
            else
            {
                Header.Visible = false;
                title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

                 rpt_details.DataBind();
            }
        }
    }

    protected void rpt_details_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "lnk_edit")
        {
           
            Response.Redirect("UserAcceptance.aspx?evidenceid=" + e.CommandArgument.ToString() + "");
        }
    }
}
