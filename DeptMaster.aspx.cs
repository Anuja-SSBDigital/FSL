using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using log4net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

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
                    if (Session["role"].ToString() == "SuperAdmin")
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
                    }
                    else
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
                        string InstID = Session["inst_id"].ToString();
                        ddlInst.SelectedValue = Session["inst_id"].ToString();
                        ddlInst.Attributes.Add("disabled", "disabled");

                        string tabDept = fl.GetDeptByIdForAdmin(InstID);
                        if (!tabDept.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDept);
                            if (dtHd.Rows.Count > 0)
                            {
                                GrdDept.DataSource = dtHd;
                                GrdDept.DataBind();
                            }
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

    protected void btnAddDept_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        string InstID = "";
        if (Session["role"].ToString() == "Admin" ||
            Session["role"].ToString() == "Assistant Director" ||
            Session["role"].ToString() == "Additional Director" ||
            Session["role"].ToString() == "Deputy Director")
        {
            InstID = Session["inst_id"].ToString();
            ddlInst.SelectedValue = Session["inst_id"].ToString();
            ddlInst.Attributes.Add("disabled", "disabled");
        }

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
                    GrdDept.DataSource = null;
                    GrdDept.DataBind();
                    if (hdnDepId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department created.');window.location='DeptMaster.aspx';</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department updated.');window.location='DeptMaster.aspx';</script>");
                    //Response.Redirect("InstMaster.aspx");
                    ddlInst.SelectedIndex = 0;
                    //ddlInst.SelectedIndex = 0;
                    txtDeptCode.Text = "";
                    txtDeptName.Text = "";
                    //txtNote.Text = "";
                    btnAddDept.Text = "Add";

                    if (Session["role"].ToString() == "SuperAdmin")
                    {
                        string tabDept = fl.GetDept();
                        if (!tabDept.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDept);
                            GrdDept.DataSource = dtHd;
                            GrdDept.DataBind();
                            //}
                        }
                    }
                    else
                    {
                        string tabDept = fl.GetDeptById(InstID);
                        if (!tabDept.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDept);
                            GrdDept.DataSource = dtHd;
                            GrdDept.DataBind();
                        }
                    }
                }
                else
                {
                    if (hdnDepId.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not created. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not updated. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
                }
            }
            else
            {
                if (hdnDepId.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not created. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Department not updated. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
            }
        }
        else
        {
            if (hdnDepId.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Department not created. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Department not updated. Please Try Again Later..!!');window.location='DeptMaster.aspx';</script>");
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