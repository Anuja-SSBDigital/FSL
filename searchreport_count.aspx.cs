using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class searchreport_count : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

    public void fill_user()
    {
        string rescode = "";
        string deptcode = "";

        if (Session["role"].ToString() == "Department Head" || Session["role"].ToString() == "Assistant Director")
        {
            deptcode = Session["dept_code"].ToString();

        }
        else if (Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Additional Director" || Session["role"].ToString() == "Deputy Director")
        {
            deptcode = ddlDepartment.SelectedValue;

        }


    }


    public void fill_department()
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                HdnDivision.Value = Session["dept_code"].ToString();


                if (Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Additional Director" || Session["role"].ToString() == "Deputy Director")
                {
                    div_dept.Visible = true;
                    fill_department();

                    //txt_fp.Visible = false;
                }
                else
                {

                }



            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        double fd = 0;
        double td = 0;

        // Convert dates to timestamp
        if (!string.IsNullOrWhiteSpace(txt_fromdate.Text) && !string.IsNullOrWhiteSpace(txt_todate.Text))
        {
            fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
            td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));
        }

        string Division = GetDivisionBasedOnRole();
        string user = "";

        // Check if search criteria is empty
        bool isSearchEmpty = string.IsNullOrWhiteSpace(txt_fromdate.Text) &&
                             string.IsNullOrWhiteSpace(txt_todate.Text) &&
                             string.IsNullOrWhiteSpace(Division) &&
                             ddl_status.SelectedIndex == 0;

        if (isSearchEmpty)
        {
            title.InnerHtml = "<div class='alert alert-danger'>⚠ Please select at least one search criteria (From Date, To Date, Department or Status).</div>";
            Header.Visible = false;
            return;
        }

        // Fetch data
        string res = fl.GetEvidencereport("", "", "", fd.ToString(), td.ToString(),
                                          Division, user, ddl_status.SelectedValue,
                                          Session["inst_code"].ToString());

        if (res.StartsWith("Error"))
        {
            Response.Write("<script>alert('No data found or an error occurred.')</script>");
            return;
        }

        DataTable dtdata = fl.Tabulate(res);

        if (dtdata.Rows.Count == 0)
        {
            Header.Visible = false;
            title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
            div_userSummary.Visible = false;
            return;
        }

        // Set Title
        title.InnerHtml = "";
        Header.Visible = true;

        if (!string.IsNullOrWhiteSpace(txt_fromdate.Text) && !string.IsNullOrWhiteSpace(txt_todate.Text))
        {
            title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
        }

        // ====================== USER-WISE CASE SUMMARY ======================
        Dictionary<string, UserCaseSummary> userSummary = new Dictionary<string, UserCaseSummary>();

        foreach (DataRow row in dtdata.Rows)
        {
            string caseassign_userid = row["caseassign_userid"].ToString().Trim();
            string caseinst_code = row["inst_code"].ToString();
            string casediv_code = row["div_code"].ToString();
            string currentStatus = row["status"].ToString() ?? "";

            // Get user details
            string resforuserfind = fl.GetUsersForUpdate(caseassign_userid, caseinst_code, casediv_code);

            bool isUserMatched = false;
            string userFullName = caseassign_userid; // fallback
            string department = casediv_code;

            if (!resforuserfind.StartsWith("Error"))
            {
                JArray dataArrayGetUsers = JArray.Parse(resforuserfind);
                foreach (JObject objUser in dataArrayGetUsers)
                {
                    string username = objUser["userid"].ToString() ?? "";
                    if (username == caseassign_userid)
                    {
                        isUserMatched = true;

                        // Combine firstname and lastname
                        string firstName = (objUser["firstname"].ToString() ?? "").Trim();
                        string lastName = (objUser["lastname"].ToString() ?? "").Trim();

                        if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                        {
                            userFullName = firstName + " " + lastName;
                            userFullName = userFullName.Trim();
                        }
                        else
                        {
                            // Fallback to old "username" field
                            string fallbackName = (objUser["username"].ToString() ?? "").Trim();
                            if (!string.IsNullOrEmpty(fallbackName))
                                userFullName = fallbackName;
                        }

                        break;
                    }
                }
            }

            // Only process matched users
            if (isUserMatched)
            {
                if (!userSummary.ContainsKey(userFullName))
                {
                    userSummary[userFullName] = new UserCaseSummary
                    {
                        TotalCases = 0,
                        CompleteCases = 0,
                        PendingCases = 0,
                        Department = department
                    };
                }

                var summary = userSummary[userFullName];
                summary.TotalCases++;

                // Classify Complete vs Pending
                string statusLower = currentStatus.ToLower();
                if (statusLower.Contains("report submission") ||
                    statusLower.Contains("complete") ||
                    statusLower.Contains("submitted") ||
                    statusLower.Contains("closed"))
                {
                    summary.CompleteCases++;
                }
                else
                {
                    summary.PendingCases++;
                }
            }
        }

        // ====================== BIND ONLY USER SUMMARY TABLE ======================
        if (userSummary.Count > 0)
        {
            DataTable dtUserSummary = new DataTable();
            dtUserSummary.Columns.Add("UserName");
            dtUserSummary.Columns.Add("Department");
            dtUserSummary.Columns.Add("TotalCases", typeof(int));
            dtUserSummary.Columns.Add("CompleteCases", typeof(int));
            dtUserSummary.Columns.Add("PendingCases", typeof(int));

            // Sort by Total Cases descending
            var sortedSummary = userSummary.OrderByDescending(x => x.Value.TotalCases);

            foreach (var item in sortedSummary)
            {
                dtUserSummary.Rows.Add(
                    item.Key,
                    item.Value.Department,
                    item.Value.TotalCases,
                    item.Value.CompleteCases,
                    item.Value.PendingCases
                );
            }

            Repeater_userSummary.DataSource = dtUserSummary;
            Repeater_userSummary.DataBind();

            div_userSummary.Visible = true;
        }
        else
        {
            div_userSummary.Visible = false;
            title.InnerHtml += "<div class='alert alert-info'>No matched user records found.</div>";
        }
    }

    public class UserCaseSummary
    {
        public int TotalCases { get; set; }
        public int CompleteCases { get; set; }
        public int PendingCases { get; set; }
        public string Department { get; set; }
    }
    // Keep your existing helper method
    private string GetDivisionBasedOnRole()
    {
        string role = Session["role"].ToString() ?? "";

        if (role == "Admin" || role == "Assistant Director" ||
            role == "Additional Director" || role == "Deputy Director" ||
            role == "SuperAdmin")
        {
            return ddlDepartment.SelectedValue != "-1" ? ddlDepartment.SelectedValue : "";
        }
        else if (role == "Department Head")
        {
            return Session["dept_code"].ToString() ?? "";
        }

        return "";
    }

    //    string res = fl.GetEvidencereport(txt_agencyname.Text, txtcaseno, txt_refernceno.Text, fd.ToString(), td.ToString(), Division, user, ddl_status.SelectedValue, Session["inst_code"].ToString());
    //    if (!res.StartsWith("Error"))
    //    {
    //        DataTable dtdata = fl.Tabulate(res);
    //        JArray dataArray = JArray.Parse(res);

    //        if (dtdata.Rows.Count > 0)
    //        {
    //            title.InnerHtml = "";
    //            Header.Visible = true;
    //            if (txt_fromdate.Text != "" && txt_todate.Text != "")
    //            {
    //                title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
    //            }
    //            //else
    //            //{
    //            //    title.InnerText = "Evidence data of " + ddlDep.SelectedItem + " Division ";

    //            //}

    //            if (!string.IsNullOrWhiteSpace(Division) && string.IsNullOrWhiteSpace(user))
    //            {
    //                JObject obj = (JObject)dataArray[0];
    //                string fullCaseNo = obj["caseno"].ToString(); // FSL/EE/2025/FPB/20221
    //                string idofcase = obj["evidenceid"].ToString();
    //                string Caseassign_userid = obj["caseassign_userid"].ToString();
    //                string Caseassign_inst_code = obj["inst_code"].ToString();
    //                string Caseassign_div_code = obj["div_code"].ToString();
    //                string resforuserfind = fl.GetUsersForUpdate(Caseassign_userid, Caseassign_inst_code, Caseassign_div_code);
    //                if (!resforuserfind.StartsWith("Error"))
    //                {
    //                    JArray dataArrayGetUsers = JArray.Parse(resforuserfind);

    //                    if (dataArrayGetUsers.Count > 0)
    //                    {
    //                        JObject objforuser = (JObject)dataArrayGetUsers[0];


    //                    }
    //                }

    //            rpt_details.DataSource = dtdata;
    //            rpt_details.DataBind();
    //        }
    //        else
    //        {
    //            Header.Visible = false;
    //            title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

    //            rpt_details.DataBind();
    //        }
    //    }

    //    //if (!res.StartsWith("Error"))
    //    //{
    //    //    string divisionCode = "";

    //    //    if (Division == "TOX")
    //    //    {
    //    //        divisionCode = "TOX";
    //    //    }
    //    //    else if (Division == "CHEM")
    //    //    {
    //    //        divisionCode = "CHEM";
    //    //    }
    //    //    else if (Division == "BIO")
    //    //    {
    //    //        divisionCode = "BIO";
    //    //    }
    //    //    DataTable dtdata = fl.Tabulate(res);
    //    //    if (dtdata.Rows.Count > 0)
    //    //    {
    //    //        DataTable dtMatch = dtdata.Clone();   // correct department
    //    //        DataTable dtOther = dtdata.Clone();   // wrong department

    //    //        title.InnerHtml = "";
    //    //        Header.Visible = true;
    //    //        if (txt_fromdate.Text != "" && txt_todate.Text != "")
    //    //        {
    //    //            title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
    //    //        }
    //    //        //else
    //    //        //{o
    //    //        //    title.InnerText = "Evidence data of " + ddlDep.SelectedItem + " Division ";

    //    //        //}

    //    //        foreach (DataRow row in dtdata.Rows)
    //    //        {
    //    //            string dept = row["department_code"].ToString();

    //    //            if (dept == divisionCode)
    //    //            {
    //    //                dtMatch.ImportRow(row);
    //    //            }
    //    //            else
    //    //            {
    //    //                dtOther.ImportRow(row);
    //    //            }
    //    //        }

    //    //        // MAIN DATA
    //    //        if (dtMatch.Rows.Count > 0)
    //    //        {
    //    //            rpt_details.DataSource = dtMatch;
    //    //            rpt_details.DataBind();
    //    //        }

    //    //        // OTHER DEPARTMENT DATA
    //    //        if (dtOther.Rows.Count > 0)
    //    //        {
    //    //            rpt_otherdept.DataSource = dtOther;
    //    //            rpt_otherdept.DataBind();
    //    //            div_otherdept.Visible = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            div_otherdept.Visible = false;
    //    //        }



    //    //        //rpt_details.DataSource = dtdata;
    //    //        //rpt_details.DataBind();
    //    //    }
    //    //    else
    //    //    {
    //    //        Header.Visible = false;
    //    //        title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

    //    //        rpt_details.DataBind();
    //    //    }
    //    //}
    //    //}
    //    //else
    //    //{
    //    //    Response.Write("<script>alert('Please fill at least one category.')</script>");
    //    //}
    //}


    //protected void rdo_caseno_CheckedChanged(object sender, EventArgs e)
    //{
    //    txt_caseno.Visible = true;
    //    txt_agencyname.Visible = false;
    //    txt_refernceno.Visible = false;
    //}

    //protected void rdo_agencyname_CheckedChanged(object sender, EventArgs e)
    //{
    //    txt_agencyname.Visible = true;
    //    txt_caseno.Visible = false;
    //    txt_refernceno.Visible = false;
    //}

    //protected void rdo_referenceno_CheckedChanged(object sender, EventArgs e)
    //{
    //    txt_refernceno.Visible = true;
    //    txt_caseno.Visible = false;
    //    txt_agencyname.Visible = false;
    //}


    protected void rpt_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hf_status = (HiddenField)e.Item.FindControl("hf_status");
        LinkButton lnk_pending = (LinkButton)e.Item.FindControl("lnk_pending");
        LinkButton lnk_completed = (LinkButton)e.Item.FindControl("lnk_completed");

        if (hf_status.Value == "Assigned" || hf_status.Value == "Pending for Assign")
        {
            lnk_pending.Visible = true;
            lnk_completed.Visible = false;
        }
        else
        {
            lnk_pending.Visible = false;
            lnk_completed.Visible = true;
        }
    }



    protected void rpt_otherdept_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    //protected void btn_generatepdf_Click1(object sender, EventArgs e)
    //{
    //    if (Repeater_userSummary.Items.Count == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
    //            "alert('No data available to generate PDF. Please perform search first.');", true);
    //        return;
    //    }

    //    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
    //    {
    //        Document document = new Document(PageSize.A4, 30, 30, 40, 40);
    //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
    //        document.Open();

    //        try
    //        {
    //            // ====================== LOGO ======================
    //            string imageURL = HttpContext.Current.Server.MapPath("~/images/dfs_logo.png");
    //            if (System.IO.File.Exists(imageURL))
    //            {
    //                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
    //                jpg.ScaleToFit(80f, 80f);           // Slightly bigger than before
    //                jpg.SpacingBefore = 10f;
    //                jpg.SpacingAfter = 15f;
    //                jpg.Alignment = Element.ALIGN_CENTER;
    //                document.Add(jpg);
    //            }
    //        }
    //        catch { /* Logo not found - continue without error */ }

    //        // ====================== INSTITUTE NAME ======================
    //        string institutename = Session["inst_name"].ToString() ?? "Your Institute Name";
    //        Paragraph logoPara = new Paragraph(institutename,
    //            FontFactory.GetFont("Arial", 14, Font.BOLD));
    //        logoPara.Alignment = Element.ALIGN_CENTER;
    //        logoPara.SpacingAfter = 8f;
    //        document.Add(logoPara);

    //        // ====================== REPORT TITLE ======================
    //        string titleText = "User-wise Case Summary";
    //        if (!string.IsNullOrWhiteSpace(txt_fromdate.Text) && !string.IsNullOrWhiteSpace(txt_todate.Text))
    //        {
    //            titleText += " (" + txt_fromdate.Text + " to " + txt_todate.Text + ")";
    //        }

    //        Paragraph titlePara = new Paragraph(titleText,
    //            FontFactory.GetFont("Arial", 16, Font.BOLD));
    //        titlePara.Alignment = Element.ALIGN_CENTER;
    //        titlePara.SpacingAfter = 25f;
    //        document.Add(titlePara);

    //        // ====================== GENERATION DATE ======================
    //        Paragraph datePara = new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"),
    //            FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC));
    //        datePara.Alignment = Element.ALIGN_RIGHT;
    //        datePara.SpacingAfter = 20f;
    //        document.Add(datePara);

    //        // ====================== TABLE ======================
    //        PdfPTable table = new PdfPTable(new float[] { 1.2f, 4f, 3f, 2f }); // Column ratios
    //        table.WidthPercentage = 100;
    //        table.SpacingBefore = 10f;
    //        table.SpacingAfter = 10f;

    //        // Header Cells
    //        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.WHITE);
    //        BaseColor headerBg = new BaseColor(0, 102, 204); // Nice blue

    //        PdfPCell cellNo = new PdfPCell(new Phrase("No", headerFont));
    //        cellNo.BackgroundColor = headerBg;
    //        cellNo.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cellNo.Padding = 8;

    //        PdfPCell cellUser = new PdfPCell(new Phrase("User Name", headerFont));
    //        cellUser.BackgroundColor = headerBg;
    //        cellUser.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cellUser.Padding = 8;

    //        PdfPCell cellDept = new PdfPCell(new Phrase("Department", headerFont));
    //        cellDept.BackgroundColor = headerBg;
    //        cellDept.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cellDept.Padding = 8;

    //        PdfPCell cellCompleted = new PdfPCell(new Phrase("Completed", headerFont));
    //        cellCompleted.BackgroundColor = headerBg;
    //        cellCompleted.HorizontalAlignment = Element.ALIGN_CENTER;
    //        cellCompleted.Padding = 8;

    //        table.AddCell(cellNo);
    //        table.AddCell(cellUser);
    //        table.AddCell(cellDept);
    //        table.AddCell(cellCompleted);

    //        // Data Rows from Repeater
    //        int srNo = 1;
    //        Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
    //        Font completedFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, new BaseColor(0, 153, 0)); // Green

    //        foreach (RepeaterItem item in Repeater_userSummary.Items)
    //        {
    //            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
    //            {
    //                string userName = EvalData(item, "UserName");
    //                string department = EvalData(item, "Department");
    //                string completed = EvalData(item, "CompleteCases");

    //                // Row Cells
    //                PdfPCell c1 = new PdfPCell(new Phrase(srNo.ToString(), dataFont));
    //                c1.HorizontalAlignment = Element.ALIGN_CENTER;
    //                c1.Padding = 7;

    //                PdfPCell c2 = new PdfPCell(new Phrase(userName, dataFont));
    //                c2.Padding = 7;

    //                PdfPCell c3 = new PdfPCell(new Phrase(department, dataFont));
    //                c3.Padding = 7;

    //                PdfPCell c4 = new PdfPCell(new Phrase(completed, completedFont));
    //                c4.HorizontalAlignment = Element.ALIGN_CENTER;
    //                c4.Padding = 7;

    //                table.AddCell(c1);
    //                table.AddCell(c2);
    //                table.AddCell(c3);
    //                table.AddCell(c4);

    //                srNo++;
    //            }
    //        }

    //        document.Add(table);

    //        // ====================== CLOSE DOCUMENT ======================
    //        document.Close();

    //        // ====================== DOWNLOAD ======================
    //        byte[] bytes = memoryStream.ToArray();

    //        Response.Clear();
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("Content-Disposition", "attachment; filename=UserWise_Case_Summary.pdf");
    //        Response.BinaryWrite(bytes);
    //        Response.Flush();
    //        Response.End();
    //    }
    //}

    //// Helper method to safely get data from RepeaterItem
    //private string EvalData(RepeaterItem item, string columnName)
    //{
    //    if (item.DataItem == null) return "";
    //    object value = DataBinder.Eval(item.DataItem, columnName);
    //    return value.ToString() ?? "";
    //}
    protected void btn_generatepdf_Click1(object sender, EventArgs e)
    {
        // Get the original data (same as used in search)
        string Division = GetDivisionBasedOnRole();
        string user = "";

        double fd = 0;
        double td = 0;
        if (!string.IsNullOrWhiteSpace(txt_fromdate.Text) && !string.IsNullOrWhiteSpace(txt_todate.Text))
        {
            fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
            td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));
        }

        string res = fl.GetEvidencereport("", "", "", fd.ToString(), td.ToString(),
                                          Division, user, ddl_status.SelectedValue,
                                          Session["inst_code"].ToString());

        if (res.StartsWith("Error"))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('No data found or error occurred.');", true);
            return;
        }

        DataTable dtdata = fl.Tabulate(res);

        if (dtdata.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('No records found to generate PDF.');", true);
            return;
        }

        // ====================== REBUILD USER SUMMARY (Same logic as search) ======================
        Dictionary<string, UserCaseSummary> userSummary = new Dictionary<string, UserCaseSummary>();

        foreach (DataRow row in dtdata.Rows)
        {
            string caseassign_userid = row["caseassign_userid"].ToString().Trim();
            string caseinst_code = row["inst_code"].ToString();
            string casediv_code = row["div_code"].ToString();
            string currentStatus = row["status"].ToString() ?? "";

            // Get user details
            string resforuserfind = fl.GetUsersForUpdate(caseassign_userid, caseinst_code, casediv_code);
            string userFullName = caseassign_userid; // fallback
            string department = casediv_code;

            if (!resforuserfind.StartsWith("Error"))
            {
                JArray dataArrayGetUsers = JArray.Parse(resforuserfind);
                foreach (JObject objUser in dataArrayGetUsers)
                {
                    string username = objUser["userid"].ToString() ?? "";
                    if (username == caseassign_userid)
                    {
                        string firstName = (objUser["firstname"].ToString() ?? "").Trim();
                        string lastName = (objUser["lastname"].ToString() ?? "").Trim();

                        if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                        {
                            userFullName = (firstName + " " + lastName).Trim();
                        }
                        else
                        {
                            string fallbackName = (objUser["username"].ToString() ?? "").Trim();
                            if (!string.IsNullOrEmpty(fallbackName))
                                userFullName = fallbackName;
                        }
                        break;
                    }
                }
            }

            // Add to summary
            if (!userSummary.ContainsKey(userFullName))
            {
                userSummary[userFullName] = new UserCaseSummary
                {
                    TotalCases = 0,
                    CompleteCases = 0,
                    PendingCases = 0,
                    Department = department
                };
            }

            var summary = userSummary[userFullName];
            summary.TotalCases++;

            string statusLower = currentStatus.ToLower();
            if (statusLower.Contains("report submission") ||
                statusLower.Contains("complete") ||
                statusLower.Contains("submitted") ||
                statusLower.Contains("closed"))
            {
                summary.CompleteCases++;
            }
            else
            {
                summary.PendingCases++;
            }
        }

        if (userSummary.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('No user records found for PDF.');", true);
            return;
        }

        // ====================== CREATE PDF ======================
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            Document document = new Document(PageSize.A4, 30f, 30f, 40f, 40f);
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Logo
            try
            {
                string logoPath = Server.MapPath("~/images/dfs_logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(80f, 80f);
                    logo.SpacingBefore = 10f;
                    logo.SpacingAfter = 15f;
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(logo);
                }
            }
            catch { }

            // Institute Name
            string instName = Session["inst_name"].ToString() ?? "";
            if (!string.IsNullOrEmpty(instName))
            {
                Paragraph p = new Paragraph(instName, FontFactory.GetFont("Arial", 14, Font.BOLD));
                p.Alignment = Element.ALIGN_CENTER;
                p.SpacingAfter = 8f;
                document.Add(p);
            }

            // Title
            string titleText = "User-wise Case Summary";
            if (!string.IsNullOrWhiteSpace(txt_fromdate.Text) && !string.IsNullOrWhiteSpace(txt_todate.Text))
            {
                titleText += " (From " + txt_fromdate.Text + " to " + txt_todate.Text + ")";
            }
            Paragraph titlePara = new Paragraph(titleText, FontFactory.GetFont("Arial", 16, Font.BOLD));
            titlePara.Alignment = Element.ALIGN_CENTER;
            titlePara.SpacingAfter = 20f;
            document.Add(titlePara);

            // Date
            Paragraph datePara = new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"),
                FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC));
            datePara.Alignment = Element.ALIGN_RIGHT;
            datePara.SpacingAfter = 15f;
            document.Add(datePara);

            // Table
            PdfPTable table = new PdfPTable(new float[] { 1.2f, 4.5f, 3.8f, 2f });
            table.WidthPercentage = 100;
            table.SpacingBefore = 10f;

            // Header
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.WHITE);
            BaseColor headerBg = new BaseColor(0, 102, 204);

            table.AddCell(new PdfPCell(new Phrase("No", headerFont)) { BackgroundColor = headerBg, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 8 });
            table.AddCell(new PdfPCell(new Phrase("User Name", headerFont)) { BackgroundColor = headerBg, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 8 });
            table.AddCell(new PdfPCell(new Phrase("Department", headerFont)) { BackgroundColor = headerBg, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 8 });
            table.AddCell(new PdfPCell(new Phrase("Completed", headerFont)) { BackgroundColor = headerBg, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 8 });

            // Data Rows
            int srNo = 1;
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font greenFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, new BaseColor(0, 153, 0));

            // Sort by TotalCases descending (same as in search)
            var sortedSummary = userSummary.OrderByDescending(x => x.Value.TotalCases);

            foreach (var item in sortedSummary)
            {
                table.AddCell(new PdfPCell(new Phrase(srNo.ToString(), normalFont))
                { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7 });

                table.AddCell(new PdfPCell(new Phrase(item.Key, normalFont)) { Padding = 7 });
                table.AddCell(new PdfPCell(new Phrase(item.Value.Department, normalFont)) { Padding = 7 });
                table.AddCell(new PdfPCell(new Phrase(item.Value.CompleteCases.ToString(), greenFont))
                { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 7 });

                srNo++;
            }

            document.Add(table);
            document.Close();

            // Download
            byte[] bytes = memoryStream.ToArray();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=UserWise_Case_Summary.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }

}