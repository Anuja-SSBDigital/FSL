using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Newtonsoft.Json.Linq;
using Microsoft.Reporting.WebForms;

public partial class ReportViewer : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["caseno"] != null)
            {
                string caseno = Request.QueryString["caseno"].ToString();
                DataSet ds = GetDatatable(caseno);
                //ds.Tables.Add(dt);
                DataTable dt = ds.Tables[0];
                rptViewer.Reset();
                if (dt.Rows.Count > 0)
                {

                    //divReport.Visible = true;
                    lblNoData.Visible = false;

                    //set Processing Mode of Report as Local  
                    rptViewer.ProcessingMode = ProcessingMode.Local;
                    //set path of the Local report  
                    rptViewer.LocalReport.EnableExternalImages = true;
                    rptViewer.LocalReport.ReportPath = Server.MapPath("rptCaseDetails.rdlc");
                    //ControlCollection cntrl = rptViewer.Controls;
                    string noevi = dt.Rows.Count.ToString();



                    ReportParameter par = new ReportParameter("caseno", caseno);

                    rptViewer.LocalReport.SetParameters(par);

                    par = new ReportParameter("noevi", noevi);

                    rptViewer.LocalReport.SetParameters(par);

                    ReportDataSource rds = new ReportDataSource("dsCase", ds.Tables[0]);

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
                }
            }
        }
    }

    public DataSet GetDatatable(string caseid)
    {
        DataTable dt = new DataTable();

        string res = fl.GetCasewiseAttachment(caseid.Replace("\\","\\\\"));

        dt = fl.Tabulate(res);

        DataSet ds = new DataSet();
        ds.Tables.Add(dt);

        return ds;
    }
}