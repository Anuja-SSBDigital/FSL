using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CaseAttachment : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string res = fl.GetCasewiseAttachment(txtCaseNo.Text.Replace("\\", "\\\\"));
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                grdAttach.DataSource = dt;
                grdAttach.DataBind();
            }
            else
            {
                grdAttach.DataSource = null;
                grdAttach.DataBind();
            }
        }
    }

    protected void grdAttach_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFile = e.Row.FindControl("hdnFile") as HiddenField;
            string path = hdnFile.Value;
            int from = path.IndexOf("Upload");
            if (from < 0)
                from = 0;
            int to = path.Length - from;
            path = path.Substring(from, to);
            bool exist = true;
            if (!File.Exists(hdnFile.Value))
                exist = false;
            Label lblfn = e.Row.FindControl("lblfn") as Label;
            lblfn.Attributes.Add("onclick", "ShowFile('" + path.Replace("\\","\\\\").Replace("\\\\","\\\\\\\\") + "','"+exist+"');return false;");

        }
    }
}