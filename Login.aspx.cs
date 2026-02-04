using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using log4net;
using System.Data;
using System.Net;

public partial class Login : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Session["username"] = null;
            Session["userid"] = null;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string res = fl.CheckLogin(txtUN.Text, txtPass.Text);
        if (res != "" || !res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    
                    Session["id"] = dt.Rows[0]["_id"].ToString();
                    Session["password"] = dt.Rows[0]["password"].ToString();
                    Session["userid"] = dt.Rows[0]["userid"].ToString();
                    Session["inst_id"] = dt.Rows[0]["inst_id"].ToString();
                    Session["div_code"] = dt.Rows[0]["div_code"].ToString();
                    Session["role"] = dt.Rows[0]["role"].ToString();
                    Session["inst_code"] = dt.Rows[0]["inst_code"].ToString();
                    Session["inst_name"] = dt.Rows[0]["inst_name"].ToString();
                    Session["dept_code"] = dt.Rows[0]["dept_code"].ToString();
                    Session["dept_id"] = dt.Rows[0]["dept_id"].ToString(); 
                    Session["username"] = dt.Rows[0]["username"].ToString();
                    Session["firstname"] = dt.Rows[0]["firstname"].ToString();
                    Session["lastname"] = dt.Rows[0]["lastname"].ToString();
                    Session["designation"] = dt.Rows[0]["designation"].ToString();
                    string right = fl.GetRightsByRole(dt.Rows[0]["role"].ToString());
                    Session["rights"] = "";
                    if (!right.StartsWith("Error"))
                    {
                        DataTable dtright = fl.Tabulate(right);
                        if (dtright.Rows.Count > 0)
                        {
                            Session["rights"] = dtright.Rows[0]["rolerights"].ToString();
                        }
                    }
                    lblMsg.Text = "";
                    if (dt.Rows[0]["dept_code"].ToString() == "CF" || 
                        dt.Rows[0]["dept_code"].ToString() == "OT" ||
                        dt.Rows[0]["dept_code"].ToString() == "HPB" || 
                        dt.Rows[0]["dept_code"].ToString() == "BL" ||
                        dt.Rows[0]["dept_code"].ToString() == "PSYD" ||
                        dt.Rows[0]["dept_code"].ToString() == "DNA" || 
                        dt.Rows[0]["dept_code"].ToString() == "BA" || 
                        dt.Rows[0]["dept_code"].ToString() == "PHY" || 
                        dt.Rows[0]["dept_code"].ToString() == "C" || 
                        dt.Rows[0]["dept_code"].ToString() == "NC" ||
                        dt.Rows[0]["dept_code"].ToString() == "PHOTO" ||
                        dt.Rows[0]["dept_code"].ToString() == "FP" ||
                        dt.Rows[0]["dept_code"].ToString() == "PSY" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept-A" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept-B" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept-J" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept-R" ||
                        dt.Rows[0]["dept_code"].ToString() == "ALLDept-S")
                    {
                        Response.Redirect("UserAcceptance.aspx");
                    }
                    //else if(dt.Rows[0]["dept_code"].ToString() == "PSY")
                    //{
                    //    Response.Redirect("PsyDep/Home.aspx");
                    //}
                    else
                    {
                        Response.Redirect("UserAcceptance.aspx");

                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "*" + ex.Message;
                    log.Error("************** Error : " + ex.Message);
                }
            }
            else
            {
                lblMsg.Text = "*Invalid Username or Password.";
            }

        }
        else
        {
            lblMsg.Text = "*" + res;

        }
    }
}