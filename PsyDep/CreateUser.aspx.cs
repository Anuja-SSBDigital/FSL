using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using System.ComponentModel.DataAnnotations;



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
                string user = fl.GetRoleByID("-1");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

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
                ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));


            }
            catch (Exception ex) { }
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {

        string pass = fl.EncryptString(txtPass.Text);
        string conpass = fl.EncryptString(txtConPass.Text);
        string userid = hdnUserID.Value;

        string res = fl.CreateUser(userid, txtFN.Text, txtLN.Text,
            txtUN.Text, txtDes.Text, pass, ddlInst.SelectedValue, ddlDep.SelectedValue,
            ddlDiv.SelectedValue, ddlRole.SelectedValue);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    ClientScript.RegisterStartupScript(this.GetType(),
                    "alert", "<script>alert('User Successfully Created.');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                   "<script>alert('User Does not Created. Please Try Again Later.!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                  "<script>alert('User Does not Created.Please Try Again Later.!!');</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('User Does not Created.Please Try Again Later.!!');</script>");
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
                    ddlDep.DataSource = dt;
                    ddlDep.DataTextField = "dept_name";
                    ddlDep.DataValueField = "dept_id";
                    ddlDep.DataBind();
                    ddlDep.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }
        }
    }

    protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDep.SelectedIndex != 0)
        {
            string res = fl.GetDivById(ddlDep.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDiv.DataSource = dt;
                    ddlDiv.DataTextField = "div_name";
                    ddlDiv.DataValueField = "div_id";
                    ddlDiv.DataBind();
                    ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                }
            }
        }
    }
}