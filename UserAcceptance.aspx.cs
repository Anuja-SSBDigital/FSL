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
using System.Web.UI.HtmlControls;
using System.IO;

public partial class UserAcceptance : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    public void filldepartment()
    {
        string res = fl.GetDeptById(Session["inst_id"].ToString());
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                if (Session["role"].ToString() == "SuperAdmin")
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataTextField = "dept_name";
                    ddlDepartment.DataValueField = "dept_code";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                }

            }
        }
    }

    public void fillevidencedetails()
    {

        string evidenceid = Request.QueryString["evidenceid"];
        string res = fl.GetEvidencebyId(evidenceid);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate(res);
            if (dtdata.Rows.Count > 0)
            {
                string[] Splitdeptcode = dtdata.Rows[0]["caseno"].ToString().Split('/');


                if (Session["dept_code"].ToString() == "BA")
                {
                    txt_dfsee.Text = Splitdeptcode[0] + "/" + Splitdeptcode[1];
                    txt_year.Text = Splitdeptcode[2];
                    txt_no.Text = Splitdeptcode[3];
                    txt_dfsee.Attributes.Add("readonly", "readonly");
                    txt_year.Attributes.Add("readonly", "readonly");

                    txt_no.Attributes.Add("readonly", "readonly");

                }
                //else if (Session["dept_code"].ToString() == "FP")
                //{
                //    txt_fp.Text = Splitdeptcode[0] + "/" + Splitdeptcode[1] + "/" + Splitdeptcode[2];
                //    txt_shortname.Text = Splitdeptcode[3];
                //    txt_fpnumber.Text = Splitdeptcode[4];
                //    txt_fpyear.Text = Splitdeptcode[5];
                //    txt_fpdate.Text = Splitdeptcode[6];
                //    txt_shortname.Attributes.Add("readonly", "readonly");
                //    txt_fpnumber.Attributes.Add("readonly", "readonly");
                //    txt_fpnumber.Attributes.Add("readonly", "readonly");
                //    txt_fpyear.Attributes.Add("readonly", "readonly");
                //    txt_fpdate.Attributes.Add("readonly", "readonly");
                //}
                //else if (Session["dept_code"].ToString() == "HPB")
                //{
                //    txt_dfsee.Text = Splitdeptcode[0] + "/" + Splitdeptcode[1];
                //    txt_year.Text = Splitdeptcode[2];
                //    txt_div.Text = Splitdeptcode[3] + "/" + Splitdeptcode[4];
                //    txt_no.Text = Splitdeptcode[5];
                //    txt_dfsee.Attributes.Add("readonly", "readonly");
                //    txt_year.Attributes.Add("readonly", "readonly");
                //    txt_div.Attributes.Add("readonly", "readonly");
                //    txt_no.Attributes.Add("readonly", "readonly");
                //}
                else
                {
                    txt_dfsee.Text = Splitdeptcode[0] + "/" + Splitdeptcode[1];
                    txt_year.Text = Splitdeptcode[2];
                    txt_div.Text = Splitdeptcode[3];
                    txt_no.Text = Splitdeptcode[4];
                    txt_dfsee.Attributes.Add("readonly", "readonly");
                    txt_year.Attributes.Add("readonly", "readonly");
                    txt_div.Attributes.Add("readonly", "readonly");
                    txt_no.Attributes.Add("readonly", "readonly");

                }

                hdnUserID.Value = dtdata.Rows[0]["evidenceid"].ToString();
                //txtCaseNo.Text = dtdata.Rows[0]["caseno"].ToString();
                txtReferanceNo.Text = dtdata.Rows[0]["agencyreferanceno"].ToString();
                txtPoliceStation.Text = dtdata.Rows[0]["agencyname"].ToString();
                txtNotes.Text = dtdata.Rows[0]["notes"].ToString();
                txtNoOfExhibits.Text = dtdata.Rows[0]["noof_exhibits"].ToString();
                BtnUpdate.Visible = true;
                BtnSave.Visible = false;
                // txtCaseNo.Attributes.Add("readonly", "readonly");cc

            }

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                txt_year.Text = DateTime.Today.ToString("yyyy");
                txt_fpyear.Text = DateTime.Today.ToString("yyyy");
                HdnDivision.Value = Session["dept_code"].ToString();

                if (Request.QueryString["evidenceid"] != null && Request.QueryString["evidenceid"].ToString() != "")
                {

                    fillevidencedetails();
                }
                else
                {
                    if (Session["role"].ToString() == "SuperAdmin")
                    {
                        div_dept.Visible = true;
                        filldepartment();
                        div_normal.Visible = false;
                        //txt_fp.Visible = false;
                    }


                    if (Session["dept_code"].ToString() == "BA")
                    {

                        txt_dfsee.Text = "RFSL/BA";
                        txt_div.Visible = false;
                        lbl_div.Visible = false;
                        div_fp.Visible = false;
                        div_normal.Visible = true;


                    }
                    //else if (Session["dept_code"].ToString() == "FP")
                    //{
                    //    div_fp.Visible = true;
                    //    div_normal.Visible = false;
                    //    txt_dfsee.Text = "";
                    //}

                    string ff = Session["dept_code"].ToString();
                    if (ff == "PSY")
                    {
                        PSY_ToolTip.Visible = true;
                        txt_div.Text = Session["dept_code"].ToString();
                        txt_div.Attributes.Remove("readonly");

                    }
                    //else if (ff == "HPB")
                    //{
                    //    txt_div.Text = "HPB/AB";
                    //    div_fp.Visible = false;
                    //    div_normal.Visible = true;
                    //    txt_div.Attributes.Add("readonly", "readonly");
                    //}

                    else
                    {
                        txt_div.Text = Session["dept_code"].ToString();
                        txt_div.Attributes.Add("readonly", "readonly");
                    }
                }


                //string role1 = Session["role"].ToString();



            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            FileStream fs = null;
            string result = "";
            string res = "";
            string reshash = "";
            string resTrack = "";
            string folder = "";
            string pathdb = "";
            string deptcode = "";
            string txtcaseno = "";
            string dir = "";
            if (Session["dept_code"].ToString() == "FP")
            {
                txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }

            HttpPostedFile file = null;
            string uploadcasename = txtcaseno.Replace("/", "_").Replace("\\", "_");

            if (txtPDF.HasFiles)
            {
                file = txtPDF.PostedFile;
                folder = "EvidenceAccept";


            }

            string path = "";// fil
            string fn = "";

            string hash = "";
            string divcode = "";



            string[] Splitdeptcode = txtcaseno.Split('/');
            deptcode = Splitdeptcode[3];
            string var = Session["dept_code"].ToString();

            if (Session["dept_code"].ToString() == "PSY")
            {
                PSY_ToolTip.Visible = true;
                deptcode = "PSY";
                divcode = Splitdeptcode[3];
                //  deptcode = divcode;
            }
            else
            {
                divcode = Session["div_code"].ToString();
            }


            if (Splitdeptcode[1] == "BA")
            {
                deptcode = "BA";
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;


            }

            if (Splitdeptcode[0] == "FP")
            {
                deptcode = "FP";

            }


            if (file != null)
            {

                string filename = file.FileName;
                long size = file.ContentLength;


                if (size <= 26214400)
                {
                    Random rndm = new Random();
                    int random = rndm.Next();
                    string[] fnarray = filename.Split('.');
                    if (fnarray.Length > 1)
                        fn = uploadcasename + "_"
                            + DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random + "." + fnarray[1];
                    else
                        fn = uploadcasename + "_"
                            + DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random;


                    dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                                "\\\\" + deptcode
                                + "\\\\" + folder;


                    string dirdb = "Uploads/" + Session["inst_code"].ToString() +
                                "/" + deptcode
                                + "/" + folder;

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


                    pathdb = url + dirdb + "/" + fn;

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    path = dir +
                               "\\\\" + fn;

                    if (!File.Exists(path))
                        file.SaveAs(path);
                }
                fs = File.OpenRead(path);



                hash = fl.SHA256CheckSum(fs);


            }


            res = fl.GetUserAcceptanceDetails(txtcaseno);
            DataTable dt = fl.Tabulate(res);

            reshash = fl.GetHashFromUserAcceptance(hash);
            DataTable dthash = fl.Tabulate(reshash);

            if ((dt.Rows.Count > 0) || (dthash.Rows.Count > 0))
            {
                if (dthash.Rows.Count > 0)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                   "<script>alert('This Hash is Already Exists in the System.Please choose another File.');</script>");

                }
                if (dt.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('This CaseNo is Already Exists');</script>");
                }
            }
            else
            {
                Guid id = Guid.NewGuid();

                string[] caseno_txt = txtcaseno.Split('/');
                string spliteddivcode = caseno_txt[1];
                string statuschangeby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();
                if (Session["role"].ToString() == "Officer")
                {

                    res = fl.InsertUserAcceptance(id.ToString(), txtcaseno.ToUpper(), pathdb, txtReferanceNo.Text,
                        txtPoliceStation.Text, txtNotes.Text, Status.Value, deptcode,
                        Session["inst_code"].ToString(), divcode,
                        Session["username"].ToString(), hash, txtNoOfExhibits.Text, Session["userid"].ToString());

                    resTrack = fl.InsertTrack(txtcaseno.ToUpper(), Status.Value, Session["username"].ToString(), Session["userid"].ToString(), txtNotes.Text, statuschangeby);

                    string InsertCaseAssi = fl.InsertCaseAssign(txtcaseno, statuschangeby, Session["userid"].ToString(),
                        txtNotes.Text, divcode, deptcode);
                }
                else
                {
                    string SessionDivCode = "";
                    Status.Value = "Pending for Assign";
                    if (Session["role"].ToString() == "Admin" ||
                       Session["role"].ToString() == "Assistant Director" ||
                       Session["role"].ToString() == "Additional Director" ||
                       Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Department Head")
                    {
                        if (txt_div.Text == "LVA" || txt_div.Text == "BEOS" || txt_div.Text == "NARCO" || txt_div.Text == "SDS" || txt_div.Text == "PSY" || txt_div.Text == "P.Assessment")
                        {
                            PSY_ToolTip.Visible = true;
                            deptcode = "PSY";

                        }
                        if (Splitdeptcode[0] == "FP")
                        {
                            SessionDivCode = Splitdeptcode[0];
                        }
                        else if (Splitdeptcode[1] == "BA")
                        {
                            SessionDivCode = Splitdeptcode[1];
                        }
                        else
                        {
                            SessionDivCode = Splitdeptcode[3];
                        }


                    }
                    else
                    {
                        SessionDivCode = Session["div_code"].ToString();
                    }


                    res = fl.InsertUserAcceptance(id.ToString(), txtcaseno.ToUpper(), pathdb, txtReferanceNo.Text,
                        txtPoliceStation.Text, txtNotes.Text, Status.Value, deptcode,
                        Session["inst_code"].ToString(), SessionDivCode,
                        Session["username"].ToString(), hash, txtNoOfExhibits.Text, "");
                    resTrack = fl.InsertTrack(txtcaseno, Status.Value, "", "", txtNotes.Text, statuschangeby);

                }

                if (!res.StartsWith("Error") && !resTrack.StartsWith("Error"))
                {
                    DataTable dtdata = fl.Tabulate("[" + res + "]");
                    if (dtdata.Rows.Count > 0)
                    {
                        if (dtdata.Rows[0]["status"].ToString() == "200")
                        {
                            if (Session["role"].ToString() == "Officer")
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                       "<script>alert('Details Added Successfully');window.location.href='AddCaseDetails.aspx?caseno=" + txtcaseno + "'</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                       "<script>alert('Details Added Successfully');window.location.href='AssignCase.aspx?caseno=" + txtcaseno + "'</script>");
                            }

                            string finaldir = path;
                            txt_year.Text = "";
                            txt_no.Text = "";
                            txt_div.Text = "";
                            txtReferanceNo.Text = "";
                            txtPoliceStation.Text = "";
                            txtNotes.Text = "";
                            txtNoOfExhibits.Text = "";
                            txt_shortname.Text = "";
                            txt_fpnumber.Text = "";
                            txt_fpyear.Text = "";
                            txt_fpdate.Text = "";

                        }
                        else
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Details not Added sucessfully. Please Try Again Later..!!');</script>");

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
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Details not Added sucessfully. Please Try Again Later..!!');</script>");
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
                        //Directory.Delete(finaldir);
                        //Directory.CreateDirectory(finaldir);

                    }
                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Details not Added sucessfully. Please Try Again Later..!!');</script>");

                }

            }

        }
        catch (Exception ex) { }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string hash = "";
            string res = "";
            string reshash = "";
            string resTrack = "";
            string folder = "";
            string fn = "";
            string path = "";
            string filename = "";
            string pathdb = "";
            string dir = "";
            FileStream fs = null;


            string txtcaseno = "";
            txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            HttpPostedFile file = null;
            string uploadcasename = txtcaseno.Replace("/", "_").Replace("\\", "_");

            if (txtPDF.HasFiles)
            {
                file = txtPDF.PostedFile;
                folder = "EvidenceAccept";


            }
            if (file != null)
            {

                filename = file.FileName;
                long size = file.ContentLength;



                if (size <= 5000000)
                {
                    Random rndm = new Random();
                    int random = rndm.Next();
                    string[] fnarray = filename.Split('.');
                    if (fnarray.Length > 1)
                        fn = uploadcasename + "_"
                            + DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random + "." + fnarray[1];
                    else
                        fn = uploadcasename + "_"
                            + DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random;


                    string[] Splitdeptcode = txtcaseno.Split('/');
                    string deptcode = Splitdeptcode[3];
                    if (Splitdeptcode[0] == "FP")
                    {
                        deptcode = "FP";
                    }

                    if (Splitdeptcode[1] == "BA")
                    {
                        deptcode = "BA";
                        txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;

                    }


                    dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                                "\\\\" + deptcode
                                + "\\\\" + folder;

                    string dirdb = "Uploads/" + Session["inst_code"].ToString() +
                                "/" + deptcode
                                + "/" + folder;

                    string url = HttpContext.Current.Request.Url.Authority;
                    pathdb = url + "/" + dirdb + "/" + fn;

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    path = dir +
                               "\\\\" + fn;

                    if (!File.Exists(path))
                        file.SaveAs(path);
                }

                fs = File.OpenRead(path);
                fs.Close();
                fs.Dispose();

                hash = fl.SHA256CheckSum(fs);

            }

            reshash = fl.GetHashFromUserAcceptance(hash);
            DataTable dthash = fl.Tabulate(reshash);

            if (dthash.Rows.Count > 0)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('This Hash is Already Exists in the System.Please choose another File.');</script>");
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
                res = fl.UpdateEvidencefile(hdnUserID.Value, txtPoliceStation.Text, pathdb, txtReferanceNo.Text, txtNotes.Text, txtNoOfExhibits.Text);
                if (!res.StartsWith("Error"))
                {
                    DataTable dtdata = fl.Tabulate("[" + res + "]");
                    if (dtdata.Rows.Count > 0)
                    {
                        if (dtdata.Rows[0]["status"].ToString() == "200")
                        {


                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Details Updated Successfully');window.location.href='EditEvidence.aspx'</script>");

                            txt_year.Text = "";
                            txt_no.Text = "";
                            txt_div.Text = "";
                            txtReferanceNo.Text = "";
                            txtPoliceStation.Text = "";
                            txtNotes.Text = "";
                            txtNoOfExhibits.Text = "";


                        }
                        else
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Details not Updated Successfully');</script>");

                        }
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Details not Updated Successfully');</script>");

                    }
                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('Details not Updated Successfully');</script>");

                }

            }


        }
        catch (Exception ex) { }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue == "BA")
        {

            txt_dfsee.Text = "RFSL/BA";
            txt_div.Visible = false;
            lbl_div.Visible = false;
            div_fp.Visible = false;
            div_normal.Visible = true;


        }
        else if (ddlDepartment.SelectedValue == "FP")
        {
            div_fp.Visible = true;
            div_normal.Visible = false;
            txt_dfsee.Text = "";
        }
        else
        {
            txt_dfsee.Text = "RFSL/EE";
            div_normal.Visible = true;
            txt_div.Visible = true;
            lbl_div.Visible = true;
            div_fp.Visible = false;
        }

        string ff = ddlDepartment.SelectedValue;
        if (ff == "PSY")
        {
            PSY_ToolTip.Visible = true;
            txt_div.Text = ddlDepartment.SelectedValue;
            txt_div.Attributes.Remove("readonly");

        }
        //else if (ff == "HPB")
        //{
        //    txt_div.Text = "HPB/AB";
        //    div_fp.Visible = false;
        //    div_normal.Visible = true;
        //    txt_div.Attributes.Add("readonly", "readonly");
        //}

        else
        {
            txt_div.Text = ddlDepartment.SelectedValue;
            txt_div.Attributes.Add("readonly", "readonly");
        }
    }
}