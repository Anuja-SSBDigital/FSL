using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using log4net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.UI.HtmlControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
   (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string inst = fl.GetInst();
                if (!inst.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(inst);
                    if (dt.Rows.Count > 0)
                    {
                        ddlInst.DataSource = dt;
                        ddlInst.DataTextField = "inst_name";
                        ddlInst.DataValueField = "inst_id";
                        ddlInst.DataBind();
                    }
                }
                ddlInst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

                string tabDiv = fl.GetDivision("-1");
                if (!tabDiv.StartsWith("Error"))
                {
                    DataTable dtHd = fl.Tabulate(tabDiv);
                    GrdDiv.DataSource = dtHd;
                    GrdDiv.DataBind();
                }
                //else
                //{
                //    string tabInst1 = fl.GetDivision(Session["div_id"].ToString());
                //    if (!tabInst1.StartsWith("Error"))
                //    {
                //        DataTable dtHd = fl.Tabulate(tabInst1);
                //        GrdDiv.DataSource = dtHd;
                //        GrdDiv.DataBind();
                //    }
                //}

            }
            catch (Exception ex) { }
        }
    }

    protected void btnAddDiv_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        if (hdnDivId.Value == "")
            res = fl.InsertDiv(id.ToString(), txtDivName.Text, txtDivCode.Text,
                ddlInst.SelectedValue, ddlDept.SelectedValue);
        else
            res = fl.UpdateDiv(hdnDivId.Value, txtDivName.Text, txtDivCode.Text,
                    ddlInst.SelectedValue, hdnDeptId.Value);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    string tabDiv = fl.GetDivision("-1");
                    if (!tabDiv.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(tabDiv);
                        GrdDiv.DataSource = dtHd;
                        GrdDiv.DataBind();
                    }
                    if (hdnDivId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division created.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division updated');</script>");
                    //Response.Redirect("InstMaster.aspx");
                    ddlDept.SelectedIndex = 0;
                    ddlInst.SelectedIndex = 0;
                    txtDivCode.Text = "";
                    txtDivName.Text = "";
                }
                else
                {
                    if (hdnDivId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division not created. Please Try Again Later..!!');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division not updated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                if (hdnDivId.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Division not created. Please Try Again Later..!!');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Division not updated. Please Try Again Later..!!');</script>");
            }
        }
        else
        {
            if (hdnDivId.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Division not created. Please Try Again Later..!!');</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Division not updated. Please Try Again Later..!!');</script>");
        }

    }


    protected void ddlInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInst.SelectedIndex != 0)
        {
            string res = fl.GetDeptById(ddlInst.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDept.DataSource = dt;
                    ddlDept.DataTextField = "dept_name";
                    ddlDept.DataValueField = "dept_id";
                    ddlDept.DataBind();
                    ddlDept.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }
        }
    }


    protected void GrdDiv_RowDataBound(object sender, GridViewRowEventArgs e)
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

    [WebMethod]
    public static string GetDivData(string id)
    {
        string res = "";

        FlureeCS fl = new FlureeCS();

        res = fl.GetDivision(id);
        string div = "";
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);

            div = fl.GetDeptById(dt.Rows[0]["inst_id"].ToString());
            if (div.StartsWith("Error"))
                div = "";

        }
        return "[{\"res\":" + res + "},{\"dep\":" + div + "}]";
    }

    [WebMethod]
    public static string DeleteData(string id)
    {
        string res = "";
        FlureeCS fl = new FlureeCS();
        res = fl.DeleteDiv(id);
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
}