using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class AddCaseDetails_DI : System.Web.UI.Page
{

    FlureeCS fl = new FlureeCS();


    public void filldepartment()
    {

        string res = fl.GetDeptById(Session["inst_id"].ToString());
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "dept_name";
                ddlDepartment.DataValueField = "dept_code";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
            }
        }

    }
    public void fillstatus()
    {
        string reschkstatus = fl.GetTrackDetails(ddlcaseno.SelectedValue);
        DataTable dt = fl.Tabulate(reschkstatus);
        if (dt.Rows.Count > 0)
        {
            string status = dt.Rows[0]["status"].ToString();
            string Notes = dt.Rows[0]["notes"].ToString();

            if (status != "Assigned")
            {
                ddlStatus.SelectedValue = status;
                DivRemarks.Visible = true;
                txtRemarks.Text = Notes;
            }
            if (status == "Report Submission")
            {
                divTab.Visible = true;
            }
        }
        else
        {
            ddlStatus.Items.Insert(0, new ListItem("Change Status", "-1"));
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));

        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));

                DataTable dttemp = TempTable();

                ViewState["dt"] = dttemp;

                filldepartment();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
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
            string timelineurl = "";
            string divcode = "";
            string userid = "";

            string[] code = ddlcaseno.SelectedValue.Split('/');
            divcode = code[1];
            userid = "-1";
            if (code[0] == "FP")
            {
                divcode = code[0];
            }

            //divcd.Visible = true;
            timelineurl += "Click Here to view timeline Case number :  <b><a href ='Timeline.aspx?caseno=" + ddlcaseno.SelectedValue + "' class='alert-link'>" + ddlcaseno.SelectedValue + "</a></b>";
            timeline.InnerHtml = timelineurl;


            string res = fl.GetAssignedCaseById(ddlcaseno.SelectedValue, userid, divcode);

            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    //string replace_caseno = dt.Rows[0]["case_id"].ToString().Replace("\\", "/");
                    string replace_caseno = dt.Rows[0]["case_id"].ToString();

                    lblMsg.Text = "";

                    string url = HttpContext.Current.Request.Url.Authority;
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        url += HttpContext.Current.Request.ApplicationPath;
                    url += "/GetCaseDetails.aspx?caseno=" +
                        ddlcaseno.SelectedValue + "&div=" + Session["div_code"].ToString();

                    string navurl = "";
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        navurl += HttpContext.Current.Request.ApplicationPath;
                    navurl += "/GetCaseDetails.aspx?caseno=" +
                        ddlcaseno.SelectedValue + "&div=" + Session["div_code"].ToString();

                    //lblURL.Text = " " + url;
                    //lblURL.NavigateUrl = navurl;
                }
                else
                {
                    divcd.Visible = false;
                    //lblURL.Text = "";
                    //lblMsg.Text = "<div class='alert alert-danger'>*No Record Found</div>";
                }
            }
            else
            {
                divcd.Visible = false;
                //lblURL.Text = "";
                lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";

            }
        }
        catch (Exception ex)
        {
            divcd.Visible = false;
            //lblURL.Text = "";
            lblMsg.Text = "<div class='alert alert-danger'>*Something went wrong.</div>";
        }
    }

    [WebMethod]
    public static string[] BindAutoCompleteList(string prefix)
    {
        FlureeCS fl = new FlureeCS();
        List<string> c_id = new List<string>();
        string res = fl.GetAssignedCaseById("-1", HttpContext.Current.Session["userid"].ToString(),
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



    public void BindData()
    {
        this.div_grd.InnerHtml = "";
        string res = fl.GetCasewiseAttachment(ddlcaseno.SelectedValue);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
               
                int Annexures = 0;
                int Reports = 0;
                int Exhibits = 0;

                this.div_grd.InnerHtml += "<table class='table table-borderless table-striped table-earning'>";
                this.div_grd.InnerHtml += "<thead>";
                this.div_grd.InnerHtml += "<tr>";
                this.div_grd.InnerHtml += "<th>FileName</th>";
                this.div_grd.InnerHtml += "<th>Type</th>";
                this.div_grd.InnerHtml += "<th>Notes</th>";
                this.div_grd.InnerHtml += " </tr>";
                this.div_grd.InnerHtml += " </thead>";
                this.div_grd.InnerHtml += "<tbody>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    string file = dt.Rows[i]["filename"].ToString();
                    string type = dt.Rows[i]["type"].ToString();
                    
                    string notes = dt.Rows[i]["notes"].ToString();
                    string path = dt.Rows[i]["path"].ToString();
                    string[] filepath = path.Split('/');
                     string type1 = "";
                    string localhost = filepath[2];
                    string[] split = localhost.Split(':');

                    string Host = Request.Url.Host;
                    
                    if (split[0] == "localhost")
                    {

                        type1 = filepath[8];
                    }
                    else
                    {
                        type1 = filepath[9];
                    }

                    string filename = "";
                    if (type == "Annexures")
                    {
                        Annexures = Annexures + 1;

                        filename = "Annexures" + Annexures;
                    }
                    if (type == "Reports")
                    {
                        Reports = Reports + 1;
                        filename = "Reports" + Reports;
                    }
                    if (type1 == "Exhibits")
                    {
                        Exhibits = Exhibits + 1;
                        filename = "Exhibits" + Exhibits;
                    }

                    //this.div_grd.InnerHtml += "</div>";

                    this.div_grd.InnerHtml += "<tr>";
                    this.div_grd.InnerHtml += " <td>" + filename + "</td>";
                    this.div_grd.InnerHtml += "<td>" + type + "</td>";
                    this.div_grd.InnerHtml += "<td>" + notes + "</td>";

                    this.div_grd.InnerHtml += "<tr>";


                }
                this.div_grd.InnerHtml += "</tbody>";

                this.div_grd.InnerHtml += "</table>";


            }

        }

    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HtmlButton btnApprove = e.Row.FindControl("btnApprove") as HtmlButton;
            //HiddenField hdnID = e.Row.FindControl("hdnUserId") as HiddenField;
            //HtmlButton btnEdit = e.Row.FindControl("btnEdit") as HtmlButton;
            ////window.location.href='createuser.aspx?a=<%= Eval("userid") %>'
            ////btnEdit.Attributes.Add("onclick", "window.location.href=\'createuser.aspx?a=" + hdnID.Value + "\'");

            //btnEdit.Attributes.Add("onclick", "CaseDetails('" + hdnID.Value + "','" 
            //    + e.Row.Cells[0].Text + "','"+ e.Row.Cells[1].Text + "')");
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
            string hd = "";
            if (dr["upload"].ToString() == "No")
                hd = "";
            string res = fl.InsertAttachment(hd,
               ddlcaseno.SelectedValue, dr["note"].ToString(), dr["type"].ToString(),
                dr["hash"].ToString(), dr["path"].ToString(),
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
            string caseno = ddlcaseno.SelectedValue.Replace("/", "_").Replace("\\", "_");
            string type = "", note = "", folder = "";

            long d = fl.ConvertToTimestamp(DateTime.Now);



            if (fuAnn.HasFiles)
            {
                file = fuAnn.PostedFile;
                type = txtAnnType.Text;
                note = txtNoteAnn.Text;
                txtAnnType.Text = "";
                txtNoteAnn.Text = "";
                folder = "Annexures";

            }
            else if (fuRep.HasFiles)
            {
                file = fuRep.PostedFile;
                type = txtRepType.Text;
                note = txtNoteRep.Text;
                txtRepType.Text = "";
                txtNoteRep.Text = "";
                folder = "Reports";
            }
            else if (fuExb.HasFiles)
            {
                file = fuExb.PostedFile;
                type = txtExhType.Text;
                note = txtNoteExh.Text;
                txtExhType.Text = "";
                txtNoteExh.Text = "";
                folder = "Exhibits";
            }


            string filename = file.FileName;
            long size = file.ContentLength;
            string path = "";// fil
            string fn = "";
            string upload = "Yes";
            if (size <= 5000000)
            {
                Random rndm = new Random();
                int random = rndm.Next();
                string[] fnarray = filename.Split('.');
                if (fnarray.Length > 1)
                    fn = caseno + "_"
                        + type + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")
                        + "_" + random + "." + fnarray[1];
                else
                    fn = caseno + "_"
                        + type + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")
                        + "_" + random;

                string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                     "\\\\" + Session["dept_code"].ToString() + "\\\\" + Session["div_code"].ToString()
                     + "\\\\" + caseno + "\\\\" + folder;


                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                path = dir +
                   "\\\\" + fn;

                if (!File.Exists(path))
                    file.SaveAs(path);
            }
            else
            {
                fn = filename;
                upload = "No";
            }

            FileStream fs = File.OpenRead(path);

            string hash = fl.SHA256CheckSum(fs);
            string reshash = fl.GetHashFromAttachment(hash);
            DataTable dthash = fl.Tabulate(reshash);

            if (dthash.Rows.Count > 0)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('This Hash is Already Exists in the System.Please choose another File.');</script>");


            }
            else
            {
                DataRow dr = temp.NewRow();

                string dirdb = "Uploads" + "/" + Session["inst_code"].ToString() +
                       "/" + Session["dept_code"].ToString() + "/" + Session["div_code"].ToString()
                       + "/" + caseno + "/" + folder;

                string absoluteurl = Request.Url.AbsoluteUri;
                string[] SplitedURL = absoluteurl.Split('/');
                string http = SplitedURL[0];
                string Host = Request.Url.Host;
                string url = "";
                if (Host == "localhost")
                {
                    url = http + "//" + Request.Url.Authority + "/";

                }
                else
                {
                    url = http + "//" + Host + HttpContext.Current.Request.ApplicationPath + "/";
                }

                string pathdb = url + dirdb +
                           "/" + fn;

                dr["filename"] = fn;// filename;
                dr["path"] = pathdb;
                dr["hash"] = hash;
                dr["type"] = type;
                dr["note"] = note;
                dr["date"] = d.ToString();
                dr["upload"] = upload;


                temp.Rows.Add(dr);
                temp.AcceptChanges();

                ViewState["dt"] = temp;

                grdFile.DataSource = temp;
                grdFile.DataBind();
                btnInsert.Visible = true;
            }
        }
        catch (Exception ex) { }
    }


    protected void chkHD_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkHD.Checked)
        //{
        //    divHD.Visible = true;
        //    divTab.Visible = false;
        //}
        //else
        //{
        //    divHD.Visible = false;
        //    divTab.Visible = true;
        //}
    }


    protected void btnStatus_Change_Click(object sender, EventArgs e)
    {

        if (ddlStatus.SelectedValue == "Report Submission")
        {

            divTab.Visible = true;
            divcd.Visible = true;
            btnInsert.Visible = true;
        }
        else
        {
            //divHD.Visible = false;
            divTab.Visible = false;
            divcd.Visible = false;
            btnInsert.Visible = false;


        }
        DivStatus.Visible = true;
        BindData();
        GetCasebyID();
        divcd.Visible = true;



        string reschkstatus = fl.GetTrackDetails(ddlcaseno.SelectedValue);
        DataTable dt = fl.Tabulate(reschkstatus);
        if (dt.Rows.Count > 0)
        {
            string status = dt.Rows[0]["status"].ToString();
            if (status != ddlStatus.SelectedValue)
            {

                string statuschangeby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();
                string res = fl.UpdateUserAcceptanceStatus(ddlcaseno.SelectedValue, ddlStatus.SelectedValue, txtRemarks.Text,"");

                string resTrack = fl.InsertTrack(ddlcaseno.SelectedValue, ddlStatus.SelectedValue, "", "", txtRemarks.Text, statuschangeby);

                if (!res.StartsWith("Error"))
                {
                    DataTable dtdata = fl.Tabulate("[" + res + "]");
                    if (dtdata.Rows.Count > 0)
                    {
                        if (dtdata.Rows[0]["status"].ToString() == "200")
                        {
                           

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                           "<script>alert('Status updated');</script>");
                            //Response.Redirect("InstMaster.aspx");
                            txtRemarks.Text = "";
                        }
                        else
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                        }
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                }


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('This CaseNo is already in " + ddlStatus.SelectedValue + "');</script>");
            }
        }

        
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DivRemarks.Visible = true;
       
        divcd.Visible = true;

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex != 0)
        {
            string res = fl.GetCaseNoBydeptcodeforDirector(ddlDepartment.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlcaseno.DataSource = dt;
                    ddlcaseno.DataTextField = "caseno";
                    ddlcaseno.DataValueField = "caseno";
                    ddlcaseno.DataBind();
                }
                else
                {
                    ddlcaseno.ClearSelection();
                    ddlcaseno.Items.Clear();
                }
            }
            ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));

        }
    }

    protected void lnkdir_search_Click(object sender, EventArgs e)
    {


        GetCasebyID();
        DivStatus.Visible = true;
        DivRemarks.Visible = true;
        fillstatus();

        //divTab.Visible = true;
        divcd.Visible = true;
        string res = fl.GetCasewiseAttachment(ddlcaseno.SelectedValue);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                
                int Annexures = 0;
                int Reports = 0;
                int Exhibits = 0;

                this.div_grd.InnerHtml += "<table class='table table-borderless table-striped table-earning'>";
                this.div_grd.InnerHtml += "<thead>";
                this.div_grd.InnerHtml += "<tr>";
                this.div_grd.InnerHtml += "<th>FileName</th>";
                this.div_grd.InnerHtml += "<th>Type</th>";
                this.div_grd.InnerHtml += "<th>Notes</th>";
                this.div_grd.InnerHtml += " </tr>";
                this.div_grd.InnerHtml += " </thead>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string path = dt.Rows[i]["path"].ToString();
                    string file = dt.Rows[i]["filename"].ToString();
                    string[] filepath = path.Split('\\');

                    string type1 = filepath[10];
                    string type = dt.Rows[i]["type"].ToString();
                    string notes = dt.Rows[i]["notes"].ToString();

                    string filename = "";
                    if (type == "Annexures")
                    {
                        Annexures = Annexures + 1;

                        filename = "Annexures" + " " + Annexures;
                    }
                    if (type == "Reports")
                    {
                        Reports = Reports + 1;
                        filename = "Reports" + " " + Reports;
                    }
                    if (type1 == "Exhibits")
                    {
                        Exhibits = Exhibits + 1;
                        filename = "Exhibits" + " " + Exhibits;
                    }

                    //this.div_grd.InnerHtml += "</div>";
                    this.div_grd.InnerHtml += "<tbody>";
                    this.div_grd.InnerHtml += "<tr>";
                    this.div_grd.InnerHtml += " <td>" + filename + "</td>";
                    this.div_grd.InnerHtml += "<td>" + type + "</td>";
                    this.div_grd.InnerHtml += "<td>" + notes + "</td>";

                    this.div_grd.InnerHtml += "<tr>";
                    this.div_grd.InnerHtml += "</tbody>";

                }


                this.div_grd.InnerHtml += "</table>";
            }

        }
    }

    protected void ddlcaseno_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCasebyID();
    }
}