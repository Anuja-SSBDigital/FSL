using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();


    //public void fillstatus()
    //{
    //    string reschkstatus = fl.GetTrackDetails(txtCaseNo.Text);
    //    DataTable dt = fl.Tabulate(reschkstatus);
    //    if (dt.Rows.Count > 0)
    //    {
    //        string status = dt.Rows[0]["status"].ToString();
    //        string Notes = dt.Rows[0]["notes"].ToString()

    //        if (status != "Assigned")
    //        {
    //            ddlStatus.SelectedValue = status;
    //            DivRemarks.Visible = true;
    //            txtRemarks.Text = Notes;
    //        }
    //        if (status == "Report Submission")
    //        {
    //            divTab.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        ddlStatus.Items.Insert(0, new ListItem("Change Status", "-1"));
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));

        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txt_year.Text = DateTime.Today.ToString("yyyy");
                txt_fpyear.Text = DateTime.Today.ToString("yyyy");
                HdnDivision.Value = Session["dept_code"].ToString();

                DataTable dttemp = TempTable();

                ViewState["dt"] = dttemp;

                divsearch.Visible = true;
                string caseno = Request.QueryString["caseno"];


                if (caseno != null)
                {
                    string[] splitcase = caseno.Split('/');
                    if (Session["dept_code"].ToString() == "BA")
                    {
                        txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                        txt_year.Text = splitcase[2];
                        txt_no.Text = splitcase[3];
                        lbl_div.Visible = false;
                    }
                    else if (Session["dept_code"].ToString() == "FP")
                    {
                        txt_fp.Text = splitcase[0] + "/" + splitcase[1] + "/" + splitcase[2];
                        txt_shortname.Text = splitcase[3];
                        txt_fpnumber.Text = splitcase[4];
                        txt_fpyear.Text = splitcase[5];
                        txt_fpdate.Text = splitcase[6];

                        div_fp.Visible = true;
                        div_normal.Visible = false;
                    }
                    //else if (Session["dept_code"].ToString() == "HPB")
                    //{
                    //    txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                    //    txt_year.Text = splitcase[2];
                    //    txt_div.Text = splitcase[3] + "/" + splitcase[4];
                    //    txt_no.Text = splitcase[5];
                    //    txt_div.Attributes.Add("readonly", "readonly");
                    //    txt_dfsee.Attributes.Add("readonly", "readonly");

                    //}

                    else
                    {
                        txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                        txt_year.Text = splitcase[2];
                        txt_div.Text = splitcase[3];
                        txt_no.Text = splitcase[4];
                        txt_div.Attributes.Add("readonly", "readonly");
                    }


                    divTab.Visible = true;
                    divcd.Visible = true;
                    string timelineurl = "";
                    timelineurl += "Click Here to view timeline Case number :  <b><a href ='Timeline.aspx?caseno=" + caseno + "' class='alert-link'>" + caseno + "</a></b>";
                    timeline.InnerHtml = timelineurl;

                }
                else
                {
                    if (Session["dept_code"].ToString() == "BA")
                    {
                        txt_dfsee.Text = "RFSL/BA";

                        lbl_div.Visible = false;
                    }
                    else if (Session["dept_code"].ToString() == "FP")
                    {

                        div_fp.Visible = true;
                        div_normal.Visible = false;
                    }
                    //else if (Session["dept_code"].ToString() == "HPB")
                    //{
                    //    txt_div.Text = "HPB/AB";
                    //    txt_div.Attributes.Add("readonly", "readonly");

                    //}
                    else if (Session["dept_code"].ToString() == "PSY")
                    {
                        txt_div.Text = Session["dept_code"].ToString();
                        txt_div.Attributes.Remove("readonly");
                    }
                    else
                    {
                        txt_div.Text = Session["dept_code"].ToString();
                        txt_div.Attributes.Add("readonly", "readonly");
                    }
                }

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
            //string rescase = fl.GetCaseNoforcheck(txtCaseNo.Text, Session["dept_code"].ToString());
            //if (!rescase.StartsWith("Error"))
            //{
            //    DataTable dtcase = fl.Tabulate(rescase);
            //    if (dtcase.Rows.Count > 0)
            //    {
            string timelineurl = "";

            string txtCaseNo = "";

            if (Session["dept_code"].ToString() == "FP")
            {
                txtCaseNo = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (Session["dept_code"].ToString() == "BA")
            {
                txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }

            timelineurl += "Click Here to view timeline Case number :  <b><a href ='Timeline.aspx?caseno=" + txtCaseNo + "' class='alert-link'>" + txtCaseNo + "</a></b>";
            timeline.InnerHtml = timelineurl;


            string[] caseno = txtCaseNo.Split('/');
            string deptcode = caseno[3];
            string divcode = "";
            if (caseno[3] == "LVA" || caseno[3] == "NARCO" || caseno[3] == "SDS" ||  caseno[3] == "BEOS" || caseno[3] == "P.ASSESSMENT" || caseno[3] == "PSY")
            {
                deptcode = "PSY";
                divcode = "";

            }
            else
            {
                divcode = Session["div_code"].ToString();
            }
            if (caseno[1] == "BA")
            {
                deptcode = caseno[1];
            }
            if (caseno[0] == "FP")
            {
                deptcode = caseno[0];
            }
            string res = fl.GetAssignedCaseById(txtCaseNo, Session["userid"].ToString(), divcode, deptcode);

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
                        txtCaseNo + "&div=" + Session["div_code"].ToString();

                    string navurl = "";
                    if (HttpContext.Current.Request.ApplicationPath != "/")
                        navurl += HttpContext.Current.Request.ApplicationPath;
                    navurl += "/GetCaseDetails.aspx?caseno=" +
                        txtCaseNo + "&div=" + Session["div_code"].ToString();


                }
                else
                {
                    divcd.Visible = false;
                    //lblURL.Text = "";
                    lblMsg.Text = "<div class='alert alert-danger'>*No Record Found</div>";
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
        //}
        //else
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //                           "<script>alert('This Case No Does Not Exist in the System');window.location.href='AddCaseDetails.aspx';</script>");
        //}

    }

    //[WebMethod]
    //public static string[] BindAutoCompleteList(string prefix)
    //{
    //    FlureeCS fl = new FlureeCS();
    //    List<string> c_id = new List<string>();
    //    string divcode = "";
    //    string dept_code = "";
    //    if (HttpContext.Current.Session["dept_code"].ToString() == "PSY")
    //    {
    //        divcode = "";
    //        dept_code = "PSY";
    //    }
    //    else
    //    {
    //        divcode = HttpContext.Current.Session["dept_code"].ToString();
    //        dept_code = HttpContext.Current.Session["dept_code"].ToString();
    //    }
    //    string res = fl.GetAssignedCaseById("-1", HttpContext.Current.Session["userid"].ToString(), divcode, dept_code);
    //    if (!res.StartsWith("Error"))
    //    {
    //        DataTable dt = fl.Tabulate(res);
    //        if (dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                if (dr["case_id"].ToString().Contains(prefix))
    //                    c_id.Add(dr["case_id"].ToString());
    //            }
    //        }
    //    }
    //    return c_id.ToArray();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txt_no.Text != "" || txt_fpdate.Text != "")
        {

            BindData();
            divTab.Visible = true;
            divcd.Visible = true;
            GetCasebyID();
        }
        else
        {
            Response.Write("<script>alert('Please Fill All the details')</script>");
        }



    }

    public void BindData()
    {

        string txtCaseNo = "";
        if (Session["dept_code"].ToString() == "FP")
        {
            txtCaseNo = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
        }
        else if (Session["dept_code"].ToString() == "BA")
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
        }
        else
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
        }
        string rescase = fl.GetCaseNoforcheck(txtCaseNo, Session["dept_code"].ToString(), Session["userid"].ToString());
        if (!rescase.StartsWith("Error"))
        {
            DataTable dtcase = fl.Tabulate(rescase);
            if (dtcase.Rows.Count > 0)
            {
                this.div_grd.InnerHtml = "";
                string res = fl.GetCasewiseAttachment(txtCaseNo);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        //grdAttach.DataSource = dt;
                        //grdAttach.DataBind();
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
                            string file = dt.Rows[i]["filename"].ToString();
                            string path = dt.Rows[i]["path"].ToString();
                            string[] filepath = path.Split('/');
                            string type1 = "";
                            string type = dt.Rows[i]["type"].ToString();

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
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                       "<script>alert('This Case No Does Not Exist in the System');window.location.href='AddCaseDetails.aspx';</script>");
            }
        }


    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HtmlButton btnApprove = e.Row.FindControl("btnApprove") as HtmlButton;
            //HiddenField hdnID = e.Row.FindControl("hdnUserId") as HiddenField;
            //HtmlButton btnDelete = e.Row.FindControl("btnDelete") as HtmlButton;
            ////window.location.href='createuser.aspx?a=<%= Eval("userid") %>'
            ////btnEdit.Attributes.Add("onclick", "window.location.href=\'createuser.aspx?a=" + hdnID.Value + "\'");

            //btnDelete.Attributes.Add("onclick", "CaseDetails('"+ e.Row.Cells[0].Text + "','" + e.Row.Cells[1].Text + "')");
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

        string txtCaseNo = "";
        string resUser = "";
        string resTrack = "";
        if (Session["dept_code"].ToString() == "FP")
        {
            txtCaseNo = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
        }
        else if (Session["dept_code"].ToString() == "BA")
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
        }
        else
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
        }
        DataTable temp = (DataTable)ViewState["dt"];
        int count = 0;
        string statuschangeby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();

        string reschkstatus = fl.GetTrackDetails(txtCaseNo);
        DataTable dtstaatus = fl.Tabulate(reschkstatus);
        if (dtstaatus.Rows.Count > 0)
        {
            string status = dtstaatus.Rows[0]["status"].ToString();
            if (status != "Report Submission")
            {
                resUser = fl.UpdateUserAcceptanceStatus(txtCaseNo, "Report Submission", "", "");

                resTrack = fl.InsertTrack(txtCaseNo, "Report Submission", "", "", "", statuschangeby);
            }

        }

        if(!resUser.StartsWith("Error") && !resTrack.StartsWith("Error"))
        {
            foreach (DataRow dr in temp.Rows)
            {
                string hd = "";
                if (dr["upload"].ToString() == "No")
                    hd = "";
                string res = fl.InsertAttachment(hd,
                   txtCaseNo.Replace("\\", "\\\\"), dr["note"].ToString(), dr["type"].ToString(),
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
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Files Not Uploaded. Please Try Again Later..!!');</script>");


                        }
                    }
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
            string dir = "";
            string txtCaseNo = "";
            if (Session["dept_code"].ToString() == "FP")
            {
                txtCaseNo = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (Session["dept_code"].ToString() == "BA")
            {
                txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }
            DataTable temp = (DataTable)ViewState["dt"];
            HttpPostedFile file = null;
            string caseno = txtCaseNo.Replace("/", "_").Replace("\\", "_");
            string type = "", note = "", folder = "";

            long d = fl.ConvertToTimestamp(DateTime.Now);


            //if (!btnStatus_Change.Click)
            //{
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
            if (size <= 26214400)
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


                 dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
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
                txtAnnType.Text = "Annexures";
                txtRepType.Text = "Reports";

                string finaldir = dir;
                string finalfile = fn;

                if (File.Exists(Path.Combine(finaldir, finalfile)))
                {
                    // If file found, delete it    

                    try
                    {

                        fs.Close();
                        //System.IO.File.Delete(Server.MapPath(path));
                        File.Delete(Path.Combine(finaldir, finalfile));
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine("The deletion failed: {0}", e1.Message);
                    }

                }
            }
            else
            {

                DataRow dr = temp.NewRow();

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

                string dirdb = "Uploads" + "/" + Session["inst_code"].ToString() +
                       "/" + Session["dept_code"].ToString() + "/" + Session["div_code"].ToString()
                       + "/" + caseno + "/" + folder;



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

                txtAnnType.Text = "Annexures";
                txtRepType.Text = "Reports";
            }
        }
        catch (Exception ex) { }
    }


    protected void chkHD_CheckedChanged(object sender, EventArgs e)
    {

    }


    protected void btnStatus_Change_Click(object sender, EventArgs e)
    {
        divcd.Visible = true;

        //if (ddlStatus.SelectedValue == "Report Submission")
        //{

        //    divTab.Visible = true;

        //    btnInsert.Visible = true;
        //}
        //else
        //{
        //    //divHD.Visible = false;
        //    divTab.Visible = false;
        //    btnInsert.Visible = false;
        //    //divcd.Visible = false;


        //}
        //DivStatus.Visible = true;

        BindData();
        GetCasebyID();
        string txtCaseNo = "";
        if (Session["dept_code"].ToString() == "FP")
        {
            txtCaseNo = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
        }
        else if (Session["dept_code"].ToString() == "BA")
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
        }
        else
        {
            txtCaseNo = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
        }
        string checkstatus = fl.GetUserAcceptanceDetails(txtCaseNo);
        DataTable dtstatus = fl.Tabulate(checkstatus);
        if (dtstatus.Rows.Count > 0)
        {
            string statuscheck = dtstatus.Rows[0]["status"].ToString();
            if (statuscheck == "Report Submission")
            {
                string reschkstatus = fl.GetTrackDetails(txtCaseNo);
                DataTable dt = fl.Tabulate(reschkstatus);
                if (dt.Rows.Count > 0)
                {

                    string Remarks_DB = dt.Rows[0]["notes"].ToString();
                    string TrackID = dt.Rows[0]["trackid"].ToString();
                    string status = dt.Rows[0]["status"].ToString();
                    //if ((status == ddlStatus.SelectedValue) && (Remarks_DB != txtRemarks.Text))
                    //{
                    //    string resTrack = fl.UpdateTrack(TrackID, txtRemarks.Text);

                    //    if (!resTrack.StartsWith("Error"))
                    //    {
                    //        DataTable dtdata = fl.Tabulate("[" + resTrack + "]");
                    //        if (dtdata.Rows.Count > 0)
                    //        {
                    //        }
                    //        else
                    //        {
                    //            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //              "<script>alert('Remarks not updated.');</script>");
                    //        }
                    //    }
                    //}
                }
                Response.Write("<script>alert('Status is in Report Submission You cannot change other status now.')</script>");
            }
            else
            {
                string reschkstatus = fl.GetTrackDetails(txtCaseNo);
                DataTable dt = fl.Tabulate(reschkstatus);
                if (dt.Rows.Count > 0)
                {

                    string Remarks_DB = dt.Rows[0]["notes"].ToString();
                    string TrackID = dt.Rows[0]["trackid"].ToString();
                    string status = dt.Rows[0]["status"].ToString();
                    //if ((status == ddlStatus.SelectedValue) && (Remarks_DB != txtRemarks.Text))
                    //{
                    //    string resTrack = fl.UpdateTrack(TrackID, txtRemarks.Text);

                    //    if (!resTrack.StartsWith("Error"))
                    //    {
                    //        DataTable dtdata = fl.Tabulate("[" + resTrack + "]");
                    //        if (dtdata.Rows.Count > 0)
                    //        {
                    //        }
                    //        else
                    //        {
                    //            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //              "<script>alert('Remarks not updated.');</script>");
                    //        }
                    //    }
                    //}
                    //if (status != ddlStatus.SelectedValue)
                    //{
                    //    string statuschangeby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();

                    //    string res = fl.UpdateUserAcceptanceStatus(txtCaseNo.Text, ddlStatus.SelectedValue, txtRemarks.Text,"");

                    //    string resTrack = fl.InsertTrack(txtCaseNo.Text, ddlStatus.SelectedValue, "", "", txtRemarks.Text, statuschangeby);

                    //    if (!res.StartsWith("Error"))
                    //    {
                    //        DataTable dtdata = fl.Tabulate("[" + res + "]");
                    //        if (dtdata.Rows.Count > 0)
                    //        {
                    //            if (dtdata.Rows[0]["status"].ToString() == "200")
                    //            {


                    //                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //               "<script>alert('Status updated');</script>");
                    //                //Response.Redirect("InstMaster.aspx");
                    //                txtRemarks.Text = "";
                    //            }
                    //            else
                    //            {

                    //                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //                "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                    //            }
                    //        }
                    //        else
                    //        {

                    //            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //            "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                    //        }
                    //    }
                    //    else
                    //    {

                    //        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //        "<script>alert('Status not updated. Please Try Again Later..!!');</script>");
                    //    }


                    //}
                    //else
                    //{

                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //                "<script>alert('This CaseNo is already in " + ddlStatus.SelectedValue + "');</script>");
                    //}
                }
            }
        }



    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        // DivRemarks.Visible = true;
        string Notes = "";
        //if (ddlStatus.SelectedValue == "Mismatch Found")
        //{
        //    divcd.Visible = false;

        //}
        //string reschkstatus = fl.GetStatusRemakrs(txtCaseNo.Text, ddlStatus.SelectedValue);
        //DataTable dt = fl.Tabulate(reschkstatus);
        //if (dt.Rows.Count > 0)
        //{
        //     Notes = dt.Rows[0]["notes"].ToString();

        //if (ddlStatus.SelectedValue == "Assigned")
        //{
        //    txtRemarks.Text = Notes;
        //}else if (ddlStatus.SelectedValue == "Inprogress")
        //{
        //    txtRemarks.Text = Notes;
        //}
        //else if (ddlStatus.SelectedValue == "Mismatch Found")
        //{
        //    txtRemarks.Text = Notes;
        //}
        //else if (ddlStatus.SelectedValue == "Report Preparation")
        //{
        //    txtRemarks.Text = Notes;
        //}
        //else if (ddlStatus.SelectedValue == "Pending for Director/HOD Signature")
        //{
        //    txtRemarks.Text = Notes;
        //}
        //else if (ddlStatus.SelectedValue == "Report Submission")
        //{
        //txtRemarks.Text = Notes;
        //}

        //}
        //else
        //{
        //    txtRemarks.Text = "";
        //}
    }


    protected void ddlcaseno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCasebyID();
    }
}