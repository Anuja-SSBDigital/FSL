using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PsyDep_AddReqDoc : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataTable dttemp = TempTable();
                ViewState["dt"] = dttemp;
            }
            catch (Exception ex) { }
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

        return dt;
    }


    [WebMethod]
    public static string[] BindAutoCompleteList(string prefix)
    {
        List<string> c_id = new List<string>();
        try
        {
            FlureeCS fl = new FlureeCS();
            //string res = fl.GetAssignedCase(HttpContext.Current.Session["userid"].ToString());
            string res = fl.GetActiveCase(HttpContext.Current.Session["div_code"].ToString());
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
                else
                {
                    c_id.Add("No Case Found.");
                }
            }
            else
            {
                c_id.Add("No Case Found.");
            }
        }
        catch (Exception ex) {
            c_id.Add("No Case Found.");
        }
        return c_id.ToArray();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divAttach.Visible = true;
        BindData();
    }

    public void BindData()
    {
        string res = fl.GetCasewiseReqDoc(txtCaseNo.Text.Replace("\\", "\\\\"));
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divcd.Visible = true; try
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
            string res = fl.InserReqDoc(DateTime.Now.ToString(), txtCaseNo.Text.Replace("\\", "\\\\"),
                dr["filename"].ToString(), dr["path"].ToString().Replace("\\", "\\\\").Replace("/", "\\\\"),
                dr["type"].ToString());



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
            HttpPostedFile file = fuReq.PostedFile;
            string caseno = txtCaseNo.Text.Replace("/", "_").Replace("\\", "_");
            string type = ddlReq.SelectedValue, note = "", folder = "Required Document";

            long d = fl.ConvertToTimestamp(DateTime.Now);

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

            //DriveInfo[] drive = DriveInfo.GetDrives();
            //string d = "";
            //foreach (DriveInfo di in drive)
            //{
            //    if (di.VolumeLabel == ddlHD.SelectedItem.Text)
            //        d = di.Name + "\\";
            //}


            string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                "\\\\" + Session["dept_code"].ToString() + "\\\\" + Session["div_code"].ToString()
                + "\\\\" + caseno + "\\\\" + folder;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string path = dir +
                "\\\\" + fn;

            //string path = ddlHD.SelectedValue + "\\\\" + Session["inst_code"].ToString() +
            //    "\\\\" + Session["dept_code"].ToString() + "\\\\" + Session["div_code"].ToString() +
            //    "\\\\" + fn;

            if (!File.Exists(path))
                file.SaveAs(path);
            //File.Copy( fname, true);
            //string hash = Copy(Path.Combine(pathFrom + filename), Path.Combine(pathTo, fn));
            FileStream fs = File.OpenRead(path);

            string hash = fl.SHA256CheckSum(fs);
            DataRow dr = temp.NewRow();

            dr["filename"] = fn;// filename;
            dr["path"] = path;
            dr["hash"] = hash;
            dr["type"] = type;

            //string userid = "";
            //if (Session["role"].ToString() == "Admin" ||
            //            Session["role"].ToString() == "SuperAdmin")
            //{
            //    userid = ddlUser.SelectedValue;
            //}
            //else
            //{
            //    userid = Session["userid"].ToString();
            ////}

            //dr["userid"] = userid;// ddlUser.SelectedValue;
            temp.Rows.Add(dr);
            temp.AcceptChanges();


            ViewState["dt"] = temp;

            grdFile.DataSource = temp;
            grdFile.DataBind();
            btnInsert.Visible = true;
        }
        catch (Exception ex) { }
    }

    protected void grdFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}