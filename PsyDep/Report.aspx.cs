using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string user = fl.GetUsers("-1", Session["inst_code"].ToString(), Session["div_code"].ToString());
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlUser.DataSource = dt;
                        ddlUser.DataTextField = "firstname";
                        //+ " " + "lastname";
                        ddlUser.DataValueField = "userid";
                        ddlUser.DataBind();

                    }
                }
                ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
            }
            catch (Exception ex) { }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string res = fl.GetReportData(ddlUser.SelectedValue, txtYear.Text,
    txtCaseNo.Text);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("_id");
                dt.AcceptChanges();
                //divReport.Visible = true;
                lblNoData.Visible = false;
               rptViewer.Visible = true;

                //set Processing Mode of Report as Local  
                rptViewer.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report  
                rptViewer.LocalReport.EnableExternalImages = true;
                rptViewer.LocalReport.ReportPath = Server.MapPath("~/Report.rdlc");
                //ControlCollection cntrl = rptViewer.Controls;

                ReportDataSource rds = new ReportDataSource("ds", dt);

                //rptViewer.AsyncRendering = false;
                //rptViewer.SizeToReportContent = true;
                //rptViewer.ZoomMode = ZoomMode.FullPage;

                rptViewer.LocalReport.DataSources.Clear();
                //Add ReportDataSource  
                rptViewer.LocalReport.EnableExternalImages = true;
                rptViewer.LocalReport.DataSources.Add(rds);
                rptViewer.LocalReport.Refresh();
            }

            else
            {
                //divReport.Visible = false;
                lblNoData.Visible = true;
                rptViewer.Visible = false;
            }

        }
        else
        {
            //divReport.Visible = false;
            lblNoData.Visible = true;
            rptViewer.Visible = false;

        }
    }
}
