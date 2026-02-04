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

    protected void btnAddInst_Click(object sender, EventArgs e)
    {
        Guid id = Guid.NewGuid();
        string res = fl.InsertInst(id.ToString(), txtInsName.Text, txtInsCode.Text, txtInsLoc.Text);
        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    GrdInst.DataSource = null;
                    GrdInst.DataBind();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Institute crested.');</script>");
                    //Response.Redirect("InstMaster.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('Institute not created. Please Try Again Later..!!');</script>");
        }

    }
}