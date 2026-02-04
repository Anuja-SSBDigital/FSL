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
        if (Session["userid"] != null) {
            if (!IsPostBack)
            {
                string userid = "";
                if (Session["role"] != null && (Session["role"].ToString() == "Admin" ||
                    Session["role"].ToString() == "SuperAdmin"))
                    userid = "-1";
                else
                    userid = Session["userid"].ToString();
                string res = fl.GetIndexCountPsy(userid);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate("[" + res + "]");
                    if (dt.Rows.Count > 0)
                    {
                        //if (dt.Columns.Contains("UserE"))
                        //    c = Convert.ToInt32(dt.Rows[0]["UserE"].ToString());
                        //lblE.Text = c.ToString();
                        int c = 0;

                        if (dt.Columns.Contains("Case"))
                            c = Convert.ToInt32(dt.Rows[0]["Case"].ToString());
                        lblCases.Text = c.ToString();
                        c = 0;

                        if (dt.Columns.Contains("HD"))
                            c = Convert.ToInt32(dt.Rows[0]["HD"].ToString());
                        lblHD.Text = c.ToString();
                        c = 0;

                        //if (dt.Columns.Contains("User"))
                        //    c = Convert.ToInt32(dt.Rows[0]["User"].ToString());
                        //lblUsers.Text = c.ToString();
                        //c = 0;

                    }
                }
            }
        }
    }
}