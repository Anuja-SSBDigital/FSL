using log4net;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadExcel : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["inst_id"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            fill_department();
        }
    }

    public void fill_department()
    {
        string res = fl.GetDeptById(Session["inst_id"].ToString());
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "dept_name";
            ddlDepartment.DataValueField = "dept_code";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
        }
    }

    // ====================== 1. VALIDATE CASES ======================
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile)
        {
            ShowMessage("Please upload Excel or CSV file", "Red");
            return;
        }

        if (ddlDepartment.SelectedValue == "-1")
        {
            ShowMessage("Please select Division", "Red");
            return;
        }

        string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();
        string filePath = Server.MapPath("~/Uploads/") + Guid.NewGuid().ToString() + fileExt;

        if (!Directory.Exists(Server.MapPath("~/Uploads/")))
            Directory.CreateDirectory(Server.MapPath("~/Uploads/"));

        FileUpload1.SaveAs(filePath);

        try
        {
            DataTable dtCases = (fileExt == ".csv") ? ReadCSV(filePath) : ReadExcel(filePath);

            DataTable result = ValidateCases(dtCases, ddlDepartment.SelectedValue);

            gvValidation.DataSource = result;
            gvValidation.DataBind();

            // Fill User Dropdown using your function
            fill_user_departmetwise(ddlDepartment.SelectedValue);

            pnlAssign.Visible = true;
            ShowMessage("Validation completed. Select new user and assign.", "Green");
        }
        catch (Exception ex)
        {
            ShowMessage("Error: " + ex.Message, "Red");
        }
    }

    private DataTable ValidateCases(DataTable dtCases, string divisionCode)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CaseNo");
        dt.Columns.Add("CurrentUser");
        dt.Columns.Add("Status");

        foreach (DataRow row in dtCases.Rows)
        {
            string caseNo = row[0].ToString().Trim();
            DataRow dr = dt.NewRow();
            dr["CaseNo"] = caseNo;

            string currentUser = GetCurrentUser(caseNo, divisionCode);
            dr["CurrentUser"] = currentUser;
            dr["Status"] = currentUser == "Not Found" ? "Not Found" : "Found";

            dt.Rows.Add(dr);
        }
        return dt;
    }

    private string GetCurrentUser(string caseNo, string divisionCode)
    {
        try
        {
            // Step 1: Search Case
            string res = fl.SearchEvidenceByCaseOrDepartmentincaseasign(caseNo, divisionCode);

            if (res.StartsWith("Error") || string.IsNullOrEmpty(res))
                return "Not Found";

            JArray dataArray = JArray.Parse(res);
            if (dataArray.Count == 0)
                return "Not Found";

            JObject obj = (JObject)dataArray[0];

            string Caseassign_userid = obj["caseassign_userid"].ToString() ?? "";
            string Caseassign_inst_code = obj["inst_code"].ToString() ?? "";
            string Caseassign_div_code = obj["div_code"].ToString() ?? "";

            if (string.IsNullOrEmpty(Caseassign_userid))
                return "Not Assigned";

            // Step 2: Get User Details (Same as your single case logic)
            string resforuserfind = fl.GetUsersForUpdate(Caseassign_userid, Caseassign_inst_code, Caseassign_div_code);

            if (!resforuserfind.StartsWith("Error") && !string.IsNullOrEmpty(resforuserfind))
            {
                JArray userArray = JArray.Parse(resforuserfind);
                if (userArray.Count > 0)
                {
                    JObject userObj = (JObject)userArray[0];
                    string firstName = userObj["firstname"].ToString() ?? "";
                    string lastName = userObj["lastname"].ToString() ?? "";
                    string userName = userObj["username"].ToString() ?? "";

                    return firstName + " " + lastName + " (" + userName + ")";
                }
            }

            return "User Details Not Found";
        }
        catch (Exception ex)
        {
            log.Info("Error in GetCurrentUser for case " + caseNo + ": " + ex.Message);
            return "Error";
        }
    }
    // ====================== 2. ASSIGN ALL CASES ======================
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        if (ddlNewUser.SelectedValue == "-1" || string.IsNullOrEmpty(ddlNewUser.SelectedValue))
        {
            ShowMessage("Please select new user", "Red");
            return;
        }

        string newUserId = ddlNewUser.SelectedValue;
        int success = 0, failed = 0;

        foreach (GridViewRow row in gvValidation.Rows)
        {
            if (row.Cells[2].Text == "Found")   // Status column
            {
                string caseNo = row.Cells[0].Text.Trim();
                bool isSuccess = AssignSingleCase(caseNo, newUserId);

                if (isSuccess) success++;
                else failed++;
            }
        }

        ShowMessage("Assignment Completed! Success: " + success + " | Failed: " + failed, "Green");
        pnlAssign.Visible = false;
    }

    private bool AssignSingleCase(string caseNo, string newUserId)
    {
        try
        {
            // Get evidenceid first
            string resSearch = fl.SearchEvidenceByCaseOrDepartmentincaseasign(caseNo, ddlDepartment.SelectedValue);
            string evidenceId = "";

            if (!resSearch.StartsWith("Error"))
            {
                JArray arr = JArray.Parse(resSearch);
                if (arr.Count > 0)
                {
                    JObject obj = (JObject)arr[0];
                    evidenceId = obj["evidenceid"].ToString() ?? "";
                }
            }

            string result = fl.chnageUserAsigneUser(caseNo, newUserId, evidenceId);
            DataTable dt = fl.Tabulate("[" + result + "]");

            return (dt.Rows.Count > 0 && dt.Rows[0]["status"].ToString() == "200");
        }
        catch (Exception ex)
        {
            log.Info("Assign Failed - Case: " + caseNo + " | " + ex.Message);
            return false;
        }
    }

    // Your existing function
    public void fill_user_departmetwise(string Depcode)
    {
        string rescode = fl.GetUsersDeptcodewiseafterIndexchanges(Depcode);
        DataTable dt = fl.Tabulate(rescode);
        ddlNewUser.Items.Clear();

        if (dt.Rows.Count > 0)
        {
            ddlNewUser.DataSource = dt;
            ddlNewUser.DataTextField = "Firstname";
            ddlNewUser.DataValueField = "userid";
            ddlNewUser.DataBind();
            ddlNewUser.Items.Insert(0, new ListItem("-- Select New User --", "-1"));
        }
        else
        {
            ddlNewUser.Items.Add(new ListItem("No user found", "-1"));
        }
    }

    private void ShowMessage(string msg, string color)
    {
        lblMessage.ForeColor = (color == "Red") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        lblMessage.Text = msg;
    }

    // ====================== File Readers ======================
    private DataTable ReadExcel(string filePath)
    {
        ExcelPackage.License.SetNonCommercialPersonal("Your Organization");

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var ws = package.Workbook.Worksheets[0];
            DataTable dt = new DataTable();
            int cols = ws.Dimension.End.Column;

            for (int c = 1; c <= cols; c++)
                dt.Columns.Add("Col" + c);

            for (int r = 2; r <= ws.Dimension.End.Row; r++)
            {
                DataRow dr = dt.NewRow();
                for (int c = 1; c <= cols; c++)
                    dr[c - 1] = ws.Cells[r, c].Value.ToString().Trim() ?? "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

    private DataTable ReadCSV(string filePath)
    {
        DataTable dt = new DataTable();
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
        using (var dr = new CsvHelper.CsvDataReader(csv))
        {
            dt.Load(dr);
        }
        return dt;
    }
}