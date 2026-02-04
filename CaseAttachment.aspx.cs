using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CaseAttachment : System.Web.UI.Page
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
                //if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Sample Warden"
                //     || Session["role"].ToString() == "SuperAdmin" ||
                //        Session["role"].ToString() == "Assistant Director" ||
                //        Session["role"].ToString() == "Additional Director" ||
                //        Session["role"].ToString() == "Deputy Director")
                //{
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataTextField = "dept_name";
                    ddlDepartment.DataValueField = "dept_code";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                string deptid = Session["dept_code"].ToString();
                ddlDepartment.SelectedValue = deptid;
                //}
                //else
                //{
                //    ddlDepartment.DataSource = dt;
                //    ddlDepartment.DataTextField = "dept_name";
                //    ddlDepartment.DataValueField = "dept_code";
                //    ddlDepartment.DataBind();
                //    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                //    string deptid = Session["dept_code"].ToString();
                //    ddlDepartment.SelectedValue = deptid;
                //    ddlDepartment.Attributes.Add("disabled", "disabled");

                //    //string res_dtt = fl.GetCaseNoBydeptcode(ddlDepartment.SelectedValue);
                //    //if (!res_dtt.StartsWith("Error"))
                //    //{
                //    //    DataTable dta = fl.Tabulate(res_dtt);
                //    //    if (dta.Rows.Count > 0)
                //    //    {
                //    //        ddlcaseno.DataSource = dta;
                //    //        ddlcaseno.DataTextField = "caseno";
                //    //        ddlcaseno.DataValueField = "caseno";
                //    //        ddlcaseno.DataBind();
                //    //        ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));
                //    //    }
                //    //}
                //    }

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
                filldepartment();
                if (ddlDepartment.SelectedValue != "-1")
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
                        div_normal.Visible = true;
                        txt_div.Visible = true;
                        lbl_div.Visible = true;
                        div_fp.Visible = false;
                    }

                    string ff = ddlDepartment.SelectedValue;
                    if (ff == "PSY")
                    {
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
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string DeptCode = "";
        if (Session["role"].ToString() == "Admin" ||
            Session["role"].ToString() == "Assistant Director" ||
            Session["role"].ToString() == "Additional Director" ||
            Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
        {
            DeptCode = "";
        }
        else if(Session["role"].ToString() == "Department Head")
        {
            DeptCode = Session["dept_code"].ToString();
        }
        else
        {
            DeptCode = "";
        }
        string userid = "";
        if(Session["role"].ToString()=="Officer")
        {
            userid = Session["userid"].ToString();
        }
        else 
        {
            userid = "";
        }
        string txtcaseno = "";
       
            if (ddlDepartment.SelectedValue == "FP")
            {
                txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (ddlDepartment.SelectedValue == "BA")
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }
       
        string res = fl.GetCaseNoforcheck(txtcaseno, DeptCode, userid);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                if (txtcaseno != "-1")
                {

                    Response.Redirect("ViewDetails.aspx?caseno=" + txtcaseno + "");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                        "<script>alert('Please Enter Case No');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                       "<script>alert('This Case No is not Exists or not assigned to you.');</script>");
            }
        }
                
       
        //string timelineurl = "";
        //timelineurl += "Click Here to view Case number timeline :  <b><a href ='Timeline.aspx?caseno=" + ddlcaseno.SelectedValue 
        //    + "' class='alert-link'>" + ddlcaseno.SelectedValue + "</a></b>";
        //timeline.InnerHtml = timelineurl;
        //timeline.Visible = true;
        //string res = fl.GetCasewiseAttachment(ddlcaseno.SelectedValue);
        //if (!res.StartsWith("Error"))
        //{
        //    DataTable dt = fl.Tabulate(res);
         
        //    if (dt.Rows.Count > 0)
        //    {
        //        if(!dt.Columns.Contains("hd_name"))
        //        {
        //            DataColumn col = new DataColumn();
        //            col.ColumnName = "hd_name";
        //            col.DataType = typeof(string);
        //            dt.Columns.Add(col);
        //            dt.AcceptChanges();
        //        }
                //grdAttach.DataSource = dt;
                //grdAttach.DataBind();
        //    }
        //    else
        //    {
        //        //grdAttach.DataSource = null;
        //        //grdAttach.DataBind();
               
        //    }
        //}
    }

    protected void grdAttach_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HiddenField hdnFile = e.Row.FindControl("hdnFile") as HiddenField;
        //    HiddenField hdnHD_name = e.Row.FindControl("hdnHD_name") as HiddenField;
        //    string path = hdnFile.Value;
        //    int from = path.IndexOf("Upload");
        //    string cpath = "";
        //    if (from < 0)
        //    {
        //        from = 0;
        //        DriveInfo[] drive = DriveInfo.GetDrives();
        //        string d = "";
        //        foreach (DriveInfo di in drive)
        //        {
        //            if (di.VolumeLabel == hdnHD_name.Value)
        //                d = di.Name + "\\";
        //        }
        //        if (d != "")
        //        {
        //            from = 0;// path.IndexOf(":\\");
        //            string rd = path.Split(':')[0];
        //            cpath = path.Replace(rd, d);
        //            //cpath = d + ":";
        //            //int to = path.Length - from;
        //            //path = path.Substring(from, to);
        //            //cpath = path;
        //        }
        //    }
        //    else
        //    {
        //        cpath = Server.MapPath("~/");
        //        int to = path.Length - from;
        //        path = path.Substring(from, to);
        //        cpath += path;
        //    }
            
        //    bool exist = true;
        //    if (!File.Exists(cpath))
        //        exist = false;
        //    Label lblfn = e.Row.FindControl("lblfn") as Label;
        //    lblfn.Attributes.Add("onclick", "ShowFile('" + path.Replace("\\","\\\\").Replace("\\\\","\\\\\\\\") + "','"+exist+"');return false;");


        //}
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
        //    if (ddlDepartment.SelectedIndex != 0)
        //    {
        //        string res = fl.GetCaseNoBydeptcode(ddlDepartment.SelectedValue);
        //        if (!res.StartsWith("Error"))
        //        {
        //            DataTable dt = fl.Tabulate(res);
        //            if (dt.Rows.Count > 0)
        //            {
        //                ddlcaseno.DataSource = dt;
        //                ddlcaseno.DataTextField = "caseno";
        //                ddlcaseno.DataValueField = "caseno";
        //                ddlcaseno.DataBind();
        //                ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));
        //            }
        //            else
        //            {
        //                ddlcaseno.ClearSelection();
        //                ddlcaseno.Items.Clear();
        //                ddlcaseno.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));
        //            }
        //        }
        //    }
    }
}