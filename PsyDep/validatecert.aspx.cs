using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using log4net;
using System.Data;

public partial class validatecert : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (fuCerti.HasFile)
        {
            string id = Guid.NewGuid().ToString();
            Stream fs = fuCerti.FileContent;
            string hash = fl.SHA256CheckSum(fs);
            string res = fl.GetCertiByHash(hash);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "<div class='alert alert-success' role='alert'>"
                        + "This evidence is verified by <b>"
                                        + Session["inst_code"].ToString() + "</b></div>";
                            lblError.Text = "";
                }
                else
                {
                    lblMsg.Text = "";
                    lblError.Text = "<div class='alert alert-danger' role='alert'>*This evidence is not verified.</div>";
                }

            }
        }
        else
        {
            lblMsg.Text = "";
            lblError.Text = "<div class='alert alert-danger' role='alert'>*Please select file to verify.</div>";
        }
    }
    
}