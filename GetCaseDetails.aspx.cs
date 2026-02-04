using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class GetCaseDetails : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["caseno"] != null)
        {
            txtCaseNo.Text = Request.QueryString["caseno"].ToString();
            txtCaseNo.ReadOnly = true;
            BindData();
        }
    }

    protected void grdAttach_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                if (e.Row.Cells[1].Text.Trim() != "")
                {
                    DateTime dt = FlureeCS.Epoch.AddMilliseconds(
                       Convert.ToInt64(e.Row.Cells[1].Text));

                    e.Row.Cells[1].Text = dt.ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception ex) { }
        }
    }

    public void BindData()
    {
        int num = GetCasebyID();
        if (num > 0)
        {
            string res = fl.GetCasewiseAttachment(txtCaseNo.Text);
            if (!res.StartsWith("Error"))
            {
                divAttach.Visible = true;
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    grdAttach.DataSource = dt;
                    grdAttach.DataBind();
                }
                else
                {
                    divAttach.Visible = false;
                    grdAttach.DataSource = null;
                    grdAttach.DataBind();
                }
            }
            else
            {
                divAttach.Visible = false;
                grdAttach.DataSource = null;
                grdAttach.DataBind();
            }
        }
        else
        {
            divAttach.Visible = false;
            grdAttach.DataSource = null;
            grdAttach.DataBind();
        }


    }
    public int GetCasebyID()
    {
        DataTable dt = new DataTable();
        try
        {
            string res = fl.GetAssignedCaseById(txtCaseNo.Text,
                "-1", Request.QueryString["div"],"");

            if (!res.StartsWith("Error"))
            {
                dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    divcd.Visible = true;
                    grdView.DataSource = dt;
                    grdView.DataBind();
                    //if (dt.Rows.Count > 0)
                    //{
                    //}
                    lblMsg.Text = "";
                }
                else
                {
                    divcd.Visible = false;
                    lblMsg.Text = "<div class='alert alert-danger'>*No Record Found</div>";
                }
            }
            else
            {
                divcd.Visible = false;
                lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";

            }
        }
        catch (Exception ex)
        {
            divcd.Visible = false;
            lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";
        }
        return dt.Rows.Count;
    }
}