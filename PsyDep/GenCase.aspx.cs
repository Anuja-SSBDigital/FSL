using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PsyDep_GenCase : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["inst_code"] != null)
        {
            if (!IsPostBack)
            {
                Load();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    public void Load()
    {
        try
        {
            string num = fl.GetLastCaseNo();
            int n = Convert.ToInt32(num) + 1;
            txtCaseNo.Text = n.ToString();
            string caseId = Session["inst_code"] + "\\"
                + Session["dept_code"] + "\\"
                + DateTime.Now.Year + "\\"
                + Session["div_code"] + "\\"
                + n.ToString("0000");
            txtCaseId.Text = caseId;
            txtFIR.Text = "";
        }
        catch(Exception ex) { }
    }

    protected void btnGen_Click(object sender, EventArgs e)
    {
        try
        {
            txtCaseNo.ReadOnly = false;
            string no = txtCaseNo.Text;
            txtCaseNo.ReadOnly = true;
            string res = fl.InsertCaseAssignPsy(no, txtCaseId.Text.Replace("\\", "\\\\"), 
                txtFIR.Text,txtNote.Text,Session["div_code"].ToString());
            if (!res.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate("[" + res + "]");
                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["status"].ToString() == "200")
                    {
                        //count++;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Case Generated.');</script>");
                        //Response.Redirect("AssignCase.aspx");
                        txtNote.Text = "";
                        Load();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Case Not Generated. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Case Not Generated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Case Not Generated. Please Try Again Later..!!');</script>");
            }
        }
        catch (Exception ex) { }
    }
}