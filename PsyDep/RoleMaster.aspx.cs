using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using System.ComponentModel.DataAnnotations;
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
                string ddlRole = fl.GetRoleByID("-1");
                if (!ddlRole.StartsWith("Error"))
                {
                    DataTable dtHd = fl.Tabulate(ddlRole);
                    GrdRole.DataSource = dtHd;
                    GrdRole.DataBind();
                }
                else
                {
                    string Role = fl.GetRoleByID(Session["role_id"].ToString());
                    if (!Role.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(Role);
                        GrdRole.DataSource = dtHd;
                        GrdRole.DataBind();
                    }
                }
                string id = Request.QueryString["a"];
                if (!String.IsNullOrEmpty(id))
                {

                    string res = fl.GetRoleByID(id);
                    if (!res.StartsWith("Error"))
                    {
                        lblRoleTitle.Text = "Edit User";
                        DataTable dt = fl.Tabulate(res);
                        if (dt.Rows.Count > 0)
                        {
                            txtRole.Text = dt.Rows[0]["role_id"].ToString();
                        }
                        else
                        {
                            lblRoleTitle.Text = "Add User";
                        }
                    }
                    else
                    {
                        lblRoleTitle.Text = "Add User";
                    }

                }
                else
                {
                    lblRoleTitle.Text = "Add User";
                }

            }
            catch (Exception ex) { }


           

        }

    }
    
    protected void btnRole_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        if (hdnRoleID.Value == "")
            res = fl.InsertRolePsy(id.ToString(), txtRole.Text);
        else
            res = fl.UpdateRolePsy(hdnRoleID.Value, txtRole.Text);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    string ddlRole = fl.GetRoleByID("-1");
                    if (!ddlRole.StartsWith("Error"))
                    {
                        DataTable dtHd = fl.Tabulate(ddlRole);
                        GrdRole.DataSource = dtHd;
                        GrdRole.DataBind();
                    }
                    txtRole.Text = "";
                    if (hdnRoleID.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Role is successfully created.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Role updated.');</script>");
                    //Response.Redirect("InstMaster.aspx");
                }
                else
                {
                    if (hdnRoleID.Value == "")
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Role not created. Please Try Again Later..!!');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Role not updated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                if (hdnRoleID.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Role not created. Please Try Again Later..!!');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Role not updated. Please Try Again Later..!!');</script>");
            }
        }
        else
        {
            if (hdnRoleID.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Role not created. Please Try Again Later..!!');</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Role not updated. Please Try Again Later..!!');</script>");
        }

    }


    protected void GrdRole_RowDataBound(object sender, GridViewRowEventArgs e)
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
    public static string GetRoleData(string id)
    {
        string res = "";

        FlureeCS fl = new FlureeCS();

        res = fl.GetRoleByID(id);
        return res;
    }

    [WebMethod]
    public static string DeleteData(string id)
    {
        string res = "";
        FlureeCS fl = new FlureeCS();
        res = fl.DeleteRole(id);
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