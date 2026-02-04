using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                DataTable dttemp = TempTable();
                ViewState["dt"] = dttemp;

                divcd.Visible = false;
            }
        }
    }

    public static DataTable TempTable()
    {
        DataTable dt = new DataTable();

        DataRow dr = dt.NewRow();

        DataColumn col = new DataColumn();
        col.ColumnName = "filename";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "path";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "hash";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "type";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "upload";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "note";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        col = new DataColumn();
        col.ColumnName = "date";
        col.DataType = typeof(string);
        dt.Columns.Add(col);

        return dt;
    }

    public void GetCasebyID()
    {
        try
        {
            string res = fl.GetUserCaseById(txtCaseNo.Text.Replace("\\", "\\\\"),
                Session["userid"].ToString(), Session["div_code"].ToString());

            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    divcd.Visible = true;
                    grdView.DataSource = dt;
                    grdView.DataBind();


                    DataTable dtst = FlureeCS.dtStatus;
                    var results = from myRow in dtst.AsEnumerable()
                                  where myRow.Field<string>("Name") == dt.Rows[0]["status"].ToString()
                                  select myRow;
                    DataTable view = results.CopyToDataTable();
                    int st = Convert.ToInt32(view.Rows[0]["Key"].ToString());

                    results = from myRow in dtst.AsEnumerable()
                              where myRow.Field<int>("Key") > st
                              select myRow;

                    //DataTable dtRes = results.CopyToDataTable();

                    ddlStatus.DataSource = view;
                    ddlStatus.DataTextField = "Name";
                    ddlStatus.DataValueField = "Value";
                    ddlStatus.DataBind();

                    string url = HttpContext.Current.Request.Url.Authority;
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        url += HttpContext.Current.Request.ApplicationPath;
                    url += "/PsyDep/GetCaseDetails.aspx?caseno=" +
                        txtCaseNo.Text.Replace("\\", "\\\\") + "&div=" + Session["div_code"].ToString();

                    string navurl = "";
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        navurl += HttpContext.Current.Request.ApplicationPath;
                    navurl += "/GetCaseDetails.aspx?caseno=" +
                        txtCaseNo.Text.Replace("\\", "\\\\") + "&div=" + Session["div_code"].ToString();

                    lblURL.Text = " " + url;
                    lblURL.NavigateUrl = navurl;

                    lblMsg.Text = "";
                }
                else
                {
                    divcd.Visible = false;
                    lblURL.Text = "";
                    lblMsg.Text = "<div class='alert alert-danger'>*No Record Found</div>";
                }
            }
            else
            {
                divcd.Visible = false;
                lblURL.Text = "";
                lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";

            }
        }
        catch (Exception ex)
        {
            divcd.Visible = false;
            lblURL.Text = "";
            lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";
        }
    }

    [WebMethod]
    public static string[] BindAutoCompleteList(string prefix)
    {
        FlureeCS fl = new FlureeCS();
        List<string> c_id = new List<string>();
        string res = fl.GetUserCaseById("-1", HttpContext.Current.Session["userid"].ToString(),
            HttpContext.Current.Session["div_code"].ToString());
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["case_id"].ToString().Contains(prefix))
                        c_id.Add(dr["case_id"].ToString());
                }
            }
        }
        return c_id.ToArray();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindData()
    {
        GetCasebyID();
        string res = fl.GetCasewiseAttachment(txtCaseNo.Text.Replace("\\", "\\\\"));
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                grdAttach.DataSource = dt;
                grdAttach.DataBind();
            }
            else
            {
                grdAttach.DataSource = null;
                grdAttach.DataBind();
            }
        }

    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string date = e.Row.Cells[1].Text;
                if (date.Trim() != "")
                {
                    e.Row.Cells[1].Text = FlureeCS.Epoch.AddMilliseconds(
                        Convert.ToInt64(date)).ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception ex) { }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            AddFiles();
        }
        catch (Exception ex) { }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DataTable temp = (DataTable)ViewState["dt"];
        int count = 0;
        foreach (DataRow dr in temp.Rows)
        {

            string res = fl.InsertAttachment("",
                txtCaseNo.Text.Replace("\\", "\\\\"), dr["note"].ToString(), dr["type"].ToString(),
                dr["hash"].ToString(), dr["path"].ToString().Replace("\\", "\\\\").Replace("/", "\\\\"),
                dr["filename"].ToString(), dr["date"].ToString(), dr["upload"].ToString());
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate("[" + res + "]");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["status"].ToString() == "200")
                    {
                        count++;
                    }
                    //else
                    //{
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //                    "<script>alert('Files Not Uploaded. Please Try Again Later..!!');</script>");
                    //}
                }
            }
        }
        string msg = count + " out of " + temp.Rows.Count + " files uploaded.";
        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('" + msg + "');</script>");
        BindData();
        DataTable dtv = TempTable();
        ViewState["dt"] = dtv;
        grdFile.DataSource = null;
        grdFile.DataBind();

    }

    public void AddFiles()
    {
        try
        {
            DataTable temp = (DataTable)ViewState["dt"];
            HttpPostedFile file = null;
            string caseno = txtCaseNo.Text.Replace("/", "_").Replace("\\", "_");
            string type = "", note = "", folder = "";

            long d = fl.ConvertToTimestamp(DateTime.Now);



            file = fuRep.PostedFile;
            type = txtRepType.Text;
            note = txtNoteRep.Text;
            txtNoteRep.Text = "";
            folder = "Reports";


            string filename = file.FileName;
            string[] fnarray = filename.Split('.');
            string fn = "";
            if (fnarray.Length > 1)
                fn = caseno + "_"
                    + type + "_" + DateTime.Now.ToString("ddMMyyyy")
                    + "_" + fnarray[0] + "." + fnarray[1];
            else
                fn = caseno + "_"
                    + type + "_" + DateTime.Now.ToString("ddMMyyyy")
                    + "_" + fnarray[0];




            string dir = Server.MapPath("~").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                "\\\\" + Session["dept_code"].ToString() + "\\\\" + Session["div_code"].ToString()
                + "\\\\" + caseno + "\\\\" + folder;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string path = dir +
                "\\\\" + fn;

            if (!File.Exists(path))
                file.SaveAs(path);

            FileStream fs = File.OpenRead(path);

            string hash = fl.SHA256CheckSum(fs);
            DataRow dr = temp.NewRow();

            dr["filename"] = fn;// filename;
            dr["path"] = path;
            dr["hash"] = hash;
            dr["type"] = type;
            dr["note"] = note;
            dr["date"] = d.ToString();
            dr["upload"] = "Yes";

            temp.Rows.Add(dr);
            temp.AcceptChanges();



            ViewState["dt"] = temp;

            grdFile.DataSource = temp;
            grdFile.DataBind();
            btnInsert.Visible = true;
        }
        catch (Exception ex) { }
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            string res = fl.UpdateStatus(txtCaseNo.Text.Replace("\\", "\\\\"),
                ddlStatus.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate("[" + res + "]");
                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["status"].ToString() == "200")
                    {
                        //count++;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Status Updated.');</script>");
                        //Response.Redirect("AssignCase.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Status Not Updated. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Status Not Updated. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Status Not Updated. Please Try Again Later..!!');</script>");
            }
        }
        catch (Exception ex) { }
    }
}