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


public partial class request : System.Web.UI.Page
{

    FlureeCS fl = new FlureeCS();


    public void getreportrequestdetails()
    {
        string res = fl.GetReportrequestdetails(Session["dept_code"].ToString(), Session["inst_code"].ToString());

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate(res);
            if (dtdata.Rows.Count > 0)
            {
                rpt_details.DataSource = dtdata;
                rpt_details.DataBind();
                Header.Visible = true;
                title.Visible = false;

            }
            else
            {
                rpt_details.DataSource = null;
                title.Visible = true;
                title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
                rpt_details.DataBind();
                Header.Visible = false;


            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                //getreportrequestdetails();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    //protected void rpt_details_PreRender(object sender, EventArgs e)
    //{
    //    foreach (RepeaterItem item in rpt_details.Items)
    //    {
    //        LinkButton lnk_edit = item.FindControl("lnk_edit") as LinkButton;
    //        Label lblapprove = item.FindControl("lblapprove") as Label;
    //        Label lblreject = item.FindControl("lblreject") as Label;

    //        string res = fl.GetReportrequestdetails();

    //        if (!res.StartsWith("Error"))
    //        {
    //            DataTable dtdata = fl.Tabulate(res);
    //            if (dtdata.Rows.Count > 0)
    //            {
    //                rpt_details.DataSource = dtdata;
    //                rpt_details.DataBind();
    //                string hodstatus = dtdata.Rows[0]["hodstatus"].ToString();
    //                if (hodstatus=="Approve")
    //                {
    //                    lblapprove.Visible = true;
    //                    lnk_edit.Visible = false;
    //                }
    //                else if(hodstatus == "Reject")
    //                {
    //                    lblreject.Visible = true;
    //                    lnk_edit.Visible = false;
    //                }
    //            }
    //        }




    //    }
    //}



    protected void rpt_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        LinkButton lnk_edit = (LinkButton)e.Item.FindControl("lnk_edit");
        Label lblapprove = (Label)e.Item.FindControl("lblapprove");
        Label lblreject = (Label)e.Item.FindControl("lblreject");
        HiddenField hdn_hodstatus = (HiddenField)e.Item.FindControl("hdn_hodstatus");
        HiddenField hdf_fileupload = (HiddenField)e.Item.FindControl("hdf_fileupload");
        HyperLink hpl_fileupload = (HyperLink)e.Item.FindControl("hpl_fileupload");
        if (hdn_hodstatus.Value.ToString() == "Approve")
        {
            lblapprove.Visible = true;
            lnk_edit.Visible = false;
        }
        else if (hdn_hodstatus.Value.ToString() == "Reject")
        {
            lblreject.Visible = true;
            lnk_edit.Visible = false;
        }

        if (hdf_fileupload.Value != null && hdf_fileupload.Value.ToString() != "")
        {
            hpl_fileupload.Visible = true;
        }
        else
        {
            hpl_fileupload.Text = "No File";
        }

    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue != "-1")
        {
            string res = fl.GetRequeststatuswise(Session["dept_code"].ToString(), ddlStatus.SelectedValue, Session["inst_code"].ToString());

            if (!res.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate(res);
                if (dtdata.Rows.Count > 0)
                {
                    rpt_details.DataSource = dtdata;
                    rpt_details.DataBind();
                    //grdFile.Visible = false;
                    //rpt_details.Visible = true;
                    Header.Visible = true;
                    title.Visible = false;

                }
                else
                {
                    rpt_details.DataSource = null;
                    Header.Visible = false;
                    title.Visible = true;
                    title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
                    rpt_details.DataBind();
                    //grdFile.Visible = true;
                    //rpt_details.Visible = false;

                }
            }
        }
        else
        {
            getreportrequestdetails();
        }
    }

    //protected void rpt_details_PreRender(object sender, EventArgs e)
    //{
    //    foreach (RepeaterItem item in rpt_details.Items)
    //    {

    //        Label hf_date = item.FindControl("hf_date") as Label;
    //        string converteddate = FlureeCS.Epoch.AddMilliseconds(Convert.ToDouble(hf_date)).ToString("dd-MMM-yyyy HH:mm:ss");
    //        Label lbl_date = item.FindControl("lbl_date") as Label;
    //        lbl_date.Text = converteddate;
    //    }
    //}
}