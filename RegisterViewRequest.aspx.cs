using System;
using System.Web.UI.WebControls;
using System.Data;
using log4net;

public partial class RegisterViewRequest : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            string res = fl.InsertViewRequest(Guid.NewGuid().ToString(), txtCaseDet.Text, txtDepartment.Text,
                txtDesignation.Text, txtName.Text, txtEmail.Text, txtMobNo.Text, txtNotes.Text);

            if (!res.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate("[" + res + "]");
                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["status"].ToString() == "200")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(),
                        "alert", "<script>alert('Request Added Successfully.');window.location='Login.aspx';</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                       "<script>alert('Request Not Added. Please Try Again Later.!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                      "<script>alert('Request Not Added.Please Try Again Later.!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Request Not Added.Please Try Again Later.!!');</script>");
            }
        }
        catch (Exception ex) { }
    }
}