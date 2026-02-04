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

                string tabDept = fl.GetDept();
                if (!tabDept.StartsWith("Error"))
                {
                    DataTable dtHd = fl.Tabulate(tabDept);
                    GrdDept.DataSource = dtHd;
                    GrdDept.DataBind();
                }
                //else
                //{
                //    string tabInst1 = fl.GetDeptId(Session["dept_id"].ToString());
                //    if (!tabInst1.StartsWith("Error"))
                //    {
                //        DataTable dtHd = fl.Tabulate(tabInst1);
                //        GrdDept.DataSource = dtHd;
                //        GrdDept.DataBind();
                //    }
                //}


            }
            catch (Exception ex) { }
        }
    }
    
    protected void btnAddDept_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        if (hdnDepId.Value == "")
            res = fl.InsertDept(id.ToString(), txtDeptName.Text, txtDeptCode.Text, ddlInst.SelectedValue);
        else
            res = fl.UpdateDept(hdnDepId.Value, txtDeptName.Text, txtDeptCode.Text,
                ddlInst.SelectedValue);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    if (hdnDepId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department created.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department updated.');</script>");
                    //Response.Redirect("InstMaster.aspx");
                    ddlInst.SelectedIndex = 0;
                    //ddlInst.SelectedIndex = 0;
                    txtDeptCode.Text = "";
                    txtDeptName.Text = "";
                    //txtNote.Text = "";
                    btnAddDept.Text = "Add";

                    string tabDept = fl.GetDept();
                    if (!tabDept.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(tabDept);
                        GrdDept.DataSource = dtHd;
                        GrdDept.DataBind();
                    }
                }
                else
                {
                    if (hdnDepId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not created. Please Try Again Later..!!');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not updated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                if (hdnDepId.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not created. Please Try Again Later..!!');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not updated. Please Try Again Later..!!');</script>");
            }
        }
        else
        {
            if (hdnDepId.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Department not created. Please Try Again Later..!!');</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Department not updated. Please Try Again Later..!!');</script>");
        }

    }

    protected void GrdDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnID = e.Row.FindControl("hdnDeptId") as HiddenField;
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

        res = fl.GetDeptId(id);
        return res;
    }

    [WebMethod]
    public static string DeleteData(string id)
    {
        string res = "";
        FlureeCS fl = new FlureeCS();
        res = fl.DeleteDept(id);
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