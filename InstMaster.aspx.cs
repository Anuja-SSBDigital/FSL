using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using log4net;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.HtmlControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
         (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    string tabInst = fl.GetInstById("-1");
                    if (!tabInst.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(tabInst);
                        GrdInst.DataSource = dtHd;
                        GrdInst.DataBind();
                    }
                    else
                    {
                        string tabInst1 = fl.GetInstById(Session["inst_id"].ToString());
                        if (!tabInst1.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabInst1);
                            GrdInst.DataSource = dtHd;
                            GrdInst.DataBind();
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnAddInst_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        if (hdnInsId.Value == "")
            res = fl.InsertInst(id.ToString(), txtInsName.Text, txtInsCode.Text, txtInsLoc.Text);
        else
            res = fl.UpdateInst(hdnInsId.Value, txtInsName.Text, txtInsCode.Text, txtInsLoc.Text);
        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    txtInsCode.Text = "";
                    txtInsLoc.Text = "";
                    txtInsName.Text = "";
                    btnAddInst.Text = "Add";
                    string tabInst = fl.GetInstById("-1");
                    if (!tabInst.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(tabInst);
                        GrdInst.DataSource = dtHd;
                        GrdInst.DataBind();
                    }
                    if (hdnInsId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Institute created.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Institute updated');</script>");
                    //Response.Redirect("InstMaster.aspx");
                }
                else
                {
                    if (hdnInsId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Institute not updated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                if (hdnInsId.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Institute not updated. Please Try Again Later..!!');</script>");
            }
        }
        else
        {
            if (hdnInsId.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Institute not updated. Please Try Again Later..!!');</script>");
        }

    }

    protected void GrdInst_RowDataBound(object sender, GridViewRowEventArgs e)
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
    public static string GetDeptData(string id)
    {
        string res = "";

        FlureeCS fl = new FlureeCS();

        res = fl.GetInstById(id);
        return res;
    }

    [WebMethod]
    public static string DeleteData(string id)
    {
        string res = "";
        FlureeCS fl = new FlureeCS();
        res = fl.DeleteInst(id);
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