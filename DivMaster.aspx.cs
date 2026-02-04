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
                    string DeptID = "";
                    if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "Additional Director" ||
                        Session["role"].ToString() == "Assistant Director" ||
                        Session["role"].ToString() == "Deputy Director")
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

                        string res = fl.GetDeptByIdForAdmin(ddlInst.SelectedValue);
                        if (!res.StartsWith("Error"))
                        {
                            DataTable dt = fl.Tabulate(res);
                            if (dt.Rows.Count > 0)
                            {
                                ddlDept.DataSource = dt;
                                ddlDept.DataTextField = "dept_name";
                                ddlDept.DataValueField = "dept_id";
                                ddlDept.DataBind();
                                ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                            }
                        }
                        //    string rr = Session["role"].ToString();
                        //if (rr != "Admin" || rr != "Additional Director" || rr != "Assistant Director" || rr != "Deputy Director")
                        //{
                        //    DeptID = Session["dept_id"].ToString();
                        //    ddlDept.SelectedValue = Session["dept_id"].ToString();
                        //    ddlDept.Attributes.Add("disabled", "disabled");
                        //}

                    }
                    else if (Session["role"].ToString() == "SuperAdmin")
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
                                }
                            }

                        }
                        ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

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
                        string res = fl.GetDeptByIdForAdmin(ddlInst.SelectedValue);
                        if (!res.StartsWith("Error"))
                        {
                            DataTable dt = fl.Tabulate(res);
                            if (dt.Rows.Count > 0)
                            {
                                ddlDept.DataSource = dt;
                                ddlDept.DataTextField = "dept_name";
                                ddlDept.DataValueField = "dept_id";
                                ddlDept.DataBind();
                                ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                            }
                        }
                        DeptID = Session["dept_id"].ToString();
                        ddlDept.SelectedValue = Session["dept_id"].ToString();
                        ddlDept.Attributes.Add("disabled", "disabled");
                    }

                    if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "Additional Director" ||
                        Session["role"].ToString() == "Assistant Director" ||
                        Session["role"].ToString() == "Deputy Director")
                    {
                        string tabDiv = fl.GetDivisionforAdmin(Session["inst_id"].ToString());
                        if (!tabDiv.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDiv);
                            GrdDiv.DataSource = dtHd;
                            GrdDiv.DataBind();
                        }
                    }
                    else if (Session["role"].ToString() == "SuperAdmin")
                    {
                        string tabDiv = fl.GetDivision("-1");
                        if (!tabDiv.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDiv);
                            GrdDiv.DataSource = dtHd;
                            GrdDiv.DataBind();
                        }
                    }
                    else
                    {
                        string tabDiv = fl.GetDivisionforDepartment(DeptID);
                        if (!tabDiv.StartsWith("Error"))
                        {
                            DataTable dtHd = fl.Tabulate(tabDiv);
                            GrdDiv.DataSource = dtHd;
                            GrdDiv.DataBind();
                        }
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
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnAddDiv_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = "";
        string InstID = "";
        if (Session["role"].ToString() == "Department Head")
        {
             InstID = Session["inst_id"].ToString();
            ddlInst.SelectedValue = Session["inst_id"].ToString();
            ddlInst.Attributes.Add("disabled", "disabled");
            string resdata = fl.GetDeptById(ddlInst.SelectedValue);
            if (!resdata.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(resdata);
                if (dt.Rows.Count > 0)
                {
                    ddlDept.DataSource = dt;
                    ddlDept.DataTextField = "dept_name";
                    ddlDept.DataValueField = "dept_id";
                    ddlDept.DataBind();
                    ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                }
            }
            ddlDept.SelectedValue = Session["dept_id"].ToString();
            ddlDept.Attributes.Add("disabled", "disabled");
        }
        
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
                        "<script>alert('Division created.');window.location='DivMaster.aspx';</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division updated');window.location='DivMaster.aspx';</script>");
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
                        "<script>alert('Division not created. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Division not updated. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
                }
            }
            else
            {
                if (hdnDivId.Value == "")
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Division not created. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Division not updated. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
            }
        }
        else
        {
            if (hdnDivId.Value == "")
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Division not created. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
            else
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Division not updated. Please Try Again Later..!!');window.location='DivMaster.aspx';</script>");
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
                    ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
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