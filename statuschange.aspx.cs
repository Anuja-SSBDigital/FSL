using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class statuschange : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }



    protected void btn_search_Click(object sender, EventArgs e)
    {

        string res = fl.GetCasenofor_Bulk(Session["div_code"].ToString(),ddl_status.SelectedValue,Session["userid"].ToString());
        
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                Header.Visible = true;
                title.Visible = false;
                ddl_bulkstatus.Visible=true;
                btn_submit.Visible = true;
                rpt_details.DataSource = dt;
                rpt_details.DataBind();
            }
            else
            {
               
                Header.Visible = false;
                btn_submit.Visible = false;
                ddl_bulkstatus.Visible = false;
                rpt_details.DataSource = null;
                rpt_details.DataBind();
                title.Visible = true;

                title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
            }


        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string falseDesicion = "";
        string trueDesicion = "";
        for (int i = 0; i < rpt_details.Items.Count; i++)
        {
            CheckBox chk_caseno = (CheckBox)rpt_details.Items[i].FindControl("chk_caseno");
       
           Label lbl_caseno = (Label)rpt_details.Items[i].FindControl("lbl_caseno");
            if (chk_caseno.Checked)
            {
                string checkstatus = fl.GetTrackDetails(lbl_caseno.Text);
                DataTable dtstatus = fl.Tabulate(checkstatus);
                if (dtstatus.Rows.Count > 0)
                {

                    string getstatus = "";
                    for (int j = 0; j < dtstatus.Rows.Count; j++)
                    {
                        getstatus += dtstatus.Rows[j]["status"].ToString() + ",";

                    }
                    string[] dbstatus = getstatus.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    string status = ddl_bulkstatus.SelectedValue + ",";
                    string[] ddlstatus = status.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    var intersect = dbstatus.Intersect(ddlstatus).Count() > 0;
                     if (intersect == false)
                    {
                        string res = fl.UpdateUserAcceptanceStatus(lbl_caseno.Text, ddl_bulkstatus.SelectedValue, "","");
                        string resTrack = fl.InsertTrack(lbl_caseno.Text, ddl_bulkstatus.SelectedValue, "", "", "", Session["username"].ToString());
                        
                        falseDesicion = falseDesicion + lbl_caseno.Text +",";
                   }
                    else
                    {

                        //Response.Write("<script>alert('This CaseNo is already in " + ddl_status.SelectedValue + "')</script>");
                        trueDesicion = trueDesicion + lbl_caseno.Text + ",";
                    }

                }
                else
                {
                   
                }
            }
        }
        string mess = "";
        if (falseDesicion != "")
        {
            mess = "Status Changed Successfully";
        }
        else if (trueDesicion != "")
        {
            mess = "Status Not Changed Successfully";
        }
        else if (falseDesicion != "" && trueDesicion != "")
        {
            mess = "Status Changed Successfully of case no" + falseDesicion + " Status Not Changed of Case No " + trueDesicion + "";
        }
        Response.Write("<script>alert('" + mess + "')</script>");
        

    }
}