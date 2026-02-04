using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Load();
            }
            catch(Exception ex) { }
        }

    }

    public void Load()
    {

       

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
        ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        txtFromDate.ReadOnly = false;
        txtFromDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtFromDate.ReadOnly = true;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            txtCaseNo.ReadOnly = false;
            string no = txtCaseNo.Text;
            txtCaseNo.ReadOnly = true;

            txtFromDate.ReadOnly = false;
            string date = txtFromDate.Text;
            txtFromDate.ReadOnly = true;

            string res = fl.InsertUserCase(txtCaseNo.Text.Replace("\\","\\\\"), ddlUser.SelectedValue,
                txtNote.Text, Session["div_code"].ToString(),ddlType.SelectedValue,date);
            if (!res.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate("[" + res + "]");
                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["status"].ToString() == "200")
                    {
                        //count++;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", 
                            "<script>alert('Case Assigned.');</script>");
                        //Response.Redirect("AssignCase.aspx");
                        txtNote.Text = "";
                        Load();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
            }
        }
        catch(Exception ex) { }
    }

    public void BindData()
    {
        string res = fl.GetCasewiseReqDoc(txtCaseNo.Text.Replace("\\", "\\\\"));
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                grdAttach.DataSource = dt;
                grdAttach.DataBind();

                if (dt.Rows.Count == 3)
                {
                    divDetails.Visible = true;
                    lblMsg.Text = "";
                }
                else
                {
                    divDetails.Visible = false;
                    lblMsg.Text = "<div class='alert alert-danger' role='alert'>*All Required Documents Are Not Yet Submitted. Please Submit Before Assigning.</div>";
                }
            }
            else
            {
                grdAttach.DataSource = null;
                grdAttach.DataBind();
            }
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            divAttach.Visible = true;
            BindData();
        }
        catch(Exception ex) { }
    }
}