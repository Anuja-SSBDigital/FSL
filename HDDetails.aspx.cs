using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

    public static DataTable TempTable()
    {
        DataTable dt = new DataTable();

        DataRow dr = dt.NewRow();

        DataColumn col = new DataColumn();
        col.ColumnName = "hd_name";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "sr_num";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "capacity";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "brand";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "userid";
        col.DataType = typeof(string);
        dt.Columns.Add(col);


        return dt;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                btnInsert.Visible = false;
                // Session["role"] = dt.Rows[0]["role"].ToString();
                if (Session["role"] != null)
                {
                    DataTable dttemp = TempTable();
                    ViewState["dt"] = dttemp;
                    if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "SuperAdmin")
                    {
                        divUser.Visible = true;
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

                        string HD = fl.GetAssignedHD("-1");
                        if (!HD.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(HD);
                            grdHD.DataSource = dtHd;
                            grdHD.DataBind();
                        }
                        ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
                    }
                    else
                    {
                        divUser.Visible = false;
                        hdnUId.Value = Session["userid"].ToString();
                        string HD = fl.GetAssignedHD(Session["userid"].ToString());
                        if (!HD.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(HD);
                            grdHD.DataSource = dtHd;
                            grdHD.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string userid = "";
            if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "SuperAdmin")
            {
                userid = ddlUser.SelectedValue;
            }
            else
            {
                userid = Session["userid"].ToString();
            }
            if (hdnHdid.Value == "")
            {
                DataTable temp = (DataTable)ViewState["dt"];
                DataRow dr = temp.NewRow();
                dr["hd_name"] = txtalias.Text;
                dr["sr_num"] = txtSrNo.Text;
                dr["capacity"] = txtCap.Text;
                dr["brand"] = txtBrand.Text;

                dr["userid"] = userid;// ddlUser.SelectedValue;
                temp.Rows.Add(dr);
                temp.AcceptChanges();
                ViewState["dt"] = temp;

                grdView.DataSource = temp;
                grdView.DataBind();
                btnInsert.Visible = true;
            }
            else
            {
                string res = fl.UpdateHD(hdnHdid.Value, txtalias.Text, txtSrNo.Text, txtCap.Text,
                    txtBrand.Text, userid);
                if (!res.StartsWith("Error"))
                {
                    DataTable dtdata = fl.Tabulate("[" + res + "]");
                    if (dtdata.Rows.Count > 0)
                    {
                        if (dtdata.Rows[0]["status"].ToString() == "200")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Hard Drive Updated.');</script>");
                            //Response.Redirect("HDDetails.aspx");
                            if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "SuperAdmin")
                            {
                                userid = "-1";
                            }
                            else
                                userid = Session["userid"].ToString();
                            string HD = fl.GetAssignedHD(userid);
                            if (!HD.StartsWith("Error"))
                            {
                                DataTable dtHd = fl.Tabulate(HD);
                                grdHD.DataSource = dtHd;
                                grdHD.DataBind();
                            }
                            btnInsert.Visible = false;
                            txtalias.Text = "";
                            txtBrand.Text = "";
                            txtCap.Text = "";
                            txtSrNo.Text = "";
                            ddlUser.SelectedIndex = 0;
                            hdnHdid.Value = "";
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "<script>alert('Hard Drive Not Updated. Please Try Again Later..!!');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Hard Drive Not Updated. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Hard Drive Not Updated. Please Try Again Later..!!');</script>");
                }
            }
        }
        catch (Exception ex) { }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable temp = (DataTable)ViewState["dt"];
            int count = 0;
            foreach (DataRow dr in temp.Rows)
            {
                string res = fl.InsertHDDetails(dr["hd_name"].ToString(),
                    dr["sr_num"].ToString(), dr["capacity"].ToString(),
                    dr["brand"].ToString(), dr["userid"].ToString());
                if (!res.StartsWith("Error"))
                {
                    DataTable dtdata = fl.Tabulate("[" + res + "]");
                    if (dtdata.Rows.Count > 0)
                    {
                        if (dtdata.Rows[0]["status"].ToString() == "200")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Hard Drive Assigned.');</script>");
                            //Response.Redirect("HDDetails.aspx");
                            grdView.DataSource = null;
                            grdView.DataBind();
                            btnInsert.Visible = false;
                            txtalias.Text = "";
                            txtBrand.Text = "";
                            txtCap.Text = "";
                            txtSrNo.Text = "";
                            ddlUser.SelectedIndex = 0;
                            hdnHdid.Value = "";
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "<script>alert('Hard Drive Not Assigned. Please Try Again Later..!!');</script>");
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Hard Drive Not Assigned. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Hard Drive Not Assigned. Please Try Again Later..!!');</script>");
                }
            }
        }
        catch (Exception ex) { }
    }


    [WebMethod]
    public static string GetHDData(string id)
    {
        string res = "";

        FlureeCS fl = new FlureeCS();

        res = fl.GetHDById(id);
        return res;
    }

    [WebMethod]
    public static string DeleteData(string id)
    {
        string res = "";
        FlureeCS fl = new FlureeCS();
        res = fl.DeleteHD(id);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate("[" + res + "]");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "200")
                    res = "1";
            }
            else
                res = "0";
        }
        else
            res = "0";
        return res;
    }


    protected void grdHD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnID = e.Row.FindControl("hdnId") as HiddenField;
            HtmlButton btnEdit = e.Row.FindControl("btnEdit") as HtmlButton;
            HtmlButton btnDelete = e.Row.FindControl("btnDelete") as HtmlButton;

            btnEdit.Attributes.Add("onclick", "EditData('" + hdnID.Value + "');return false;");
            btnDelete.Attributes.Add("onclick", "DeleteData('" + hdnID.Value + "');return false;");
        }
    }
}