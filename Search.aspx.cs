using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Search : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();


    public void fill_user()
    {
        string rescode = "";
        string deptcode = "";

        deptcode = ddlDepartment.SelectedValue;
        div_user.Visible = true;

        rescode = fl.GetUsersDeptcodewise("", deptcode);
        if (!rescode.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(rescode);
            if (dt.Rows.Count > 0)
            {

                ddl_user.DataSource = dt;
                ddl_user.DataTextField = "Firstname";
                ddl_user.DataValueField = "userid";
                ddl_user.DataBind();
                ddl_user.Items.Insert(0, new ListItem("-- Select User --", "-1"));
            }
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
    protected void Page_Load(object sender, EventArgs e)
    {

        if(!IsPostBack)
        {
            fill_user();
            fill_department();
            txt_year.Text = DateTime.Today.ToString("yyyy");
            txt_fpyear.Text = DateTime.Today.ToString("yyyy");
        }
          
       
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        double fd = 0;
        double td = 0;
        string txtcaseno = "";
        if (txt_no.Text != "" || txt_fpnumber.Text != "")
        {
            if (ddlDepartment.SelectedValue == "FP" )
            {
                txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (ddlDepartment.SelectedValue == "BA" )
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }
        }
        //if (txt_no.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "" || txt_fromdate.Text != ""
        //    || txt_todate.Text != "" || txt_shortname.Text != "" || txt_fpnumber.Text != ""
        //    || txt_fpdate.Text != "" || ddl_status.SelectedValue != "-1" || ddl_user.SelectedValue != "-1" || ddlDepartment.SelectedValue != "-1")
        //{
        if (txt_fromdate.Text != "" && txt_todate.Text != "")
        {
            fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
            td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));

        }        //if (txt_caseno.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "")
                 //{
        div_rpt.Visible = true;
        string Division = "";
       
            if (ddlDepartment.SelectedValue != "-1")
            {
                Division = ddlDepartment.SelectedValue;
            }
            else
            {
                Division = "";
            }

      
        string user = "";
        user = ddl_user.SelectedValue;
       


        string res = fl.GetEvidencereport(txt_agencyname.Text, txtcaseno, txt_refernceno.Text, fd.ToString(), td.ToString(), Division, user, ddl_status.SelectedValue,Session["inst_code"].ToString());

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate(res);
            if (dtdata.Rows.Count > 0)
            {
                title.InnerHtml = "";
                Header.Visible = true;
                if (txt_fromdate.Text != "" && txt_todate.Text != "")
                {
                    title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
                }
                //else
                //{
                //    title.InnerText = "Evidence data of " + ddlDep.SelectedItem + " Division ";

                //}
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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "-1")
        {
            if (ddlDepartment.SelectedValue == "BA")
            {

                txt_dfsee.Text = "RFSL/BA";
                txt_div.Visible = false;
                lbl_div.Visible = false;
                div_fp.Visible = false;
                div_normal.Visible = true;


            }
            else if (ddlDepartment.SelectedValue == "FP")
            {
                div_fp.Visible = true;
                div_normal.Visible = false;
                txt_dfsee.Text = "";
            }
            else
            {
                txt_dfsee.Text = "RFSL/EE";
                div_normal.Visible = true;
                txt_div.Visible = true;
                lbl_div.Visible = true;
                div_fp.Visible = false;
            }

            string ff = ddlDepartment.SelectedValue;
            if (ff == "PSY")
            {
                txt_div.Text = ddlDepartment.SelectedValue;
                txt_div.Attributes.Remove("readonly");

            }
            else if (ff == "HPB")
            {
                txt_div.Text = "HPB/AB";
                div_fp.Visible = false;
                div_normal.Visible = true;
                txt_div.Attributes.Add("readonly", "readonly");
            }

            else
            {
                txt_div.Text = ddlDepartment.SelectedValue;
                txt_div.Attributes.Add("readonly", "readonly");
            }
        }
        txt_year.Text = DateTime.Today.ToString("yyyy");
        txt_fpyear.Text = DateTime.Today.ToString("yyyy");
        fill_user();
    }


    protected void rpt_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hf_status = (HiddenField)e.Item.FindControl("hf_status");
        LinkButton lnk_pending = (LinkButton)e.Item.FindControl("lnk_pending");
        LinkButton lnk_completed = (LinkButton)e.Item.FindControl("lnk_completed");

        if (hf_status.Value == "Assigned" || hf_status.Value == "Pending for Assign")
        {
            lnk_pending.Visible = true;
            lnk_completed.Visible = false;
        }
        else
        {
            lnk_pending.Visible = false;
            lnk_completed.Visible = true;
        }
    }
}