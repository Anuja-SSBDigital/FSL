using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class searchreport : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

    public void fill_user()
    {
        string rescode = "";
        string deptcode = "";

        if (Session["role"].ToString() == "Department Head" || Session["role"].ToString() == "Assistant Director")
        {
            deptcode = Session["dept_code"].ToString();
            div_user.Visible = true;
        }
        else if (Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Additional Director" || Session["role"].ToString() == "Deputy Director")
        {
            deptcode = ddlDepartment.SelectedValue;
            div_user.Visible = true;
        }
        rescode = fl.GetUsersDeptcodewise("", deptcode);
        if (!rescode.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(rescode);
            if (dt.Rows.Count > 0)
            {

                ddl_user.DataSource = dt;
                ddl_user.DataTextField = "Firstname";
                ddl_user.DataValueField = "userid";
                ddl_user.DataBind();
                ddl_user.Items.Insert(0, new ListItem("-- Select User --", "-1"));
            }
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
                txt_year.Text = DateTime.Today.ToString("yyyy");
                txt_fpyear.Text = DateTime.Today.ToString("yyyy");
                fill_user();
                if (Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Additional Director" || Session["role"].ToString() == "Deputy Director")
                {
                    div_dept.Visible = true;
                    fill_department();
                    div_normal.Visible = false;
                    //txt_fp.Visible = false;
                }
                else
                {
                    if (Session["dept_code"].ToString() == "BA")
                    {

                        txt_dfsee.Text = "RFSL/BA";
                        txt_div.Visible = false;
                        lbl_div.Visible = false;
                        div_fp.Visible = false;
                        div_normal.Visible = true;

                    }
                    else if (Session["dept_code"].ToString() == "FP")
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

                    string ff = Session["dept_code"].ToString();
                    if (ff == "PSY")
                    {
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

                txt_agencyname.Visible = true;
                txt_refernceno.Visible = true;

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void pdf_Details()
    {
        double fd = 0;
        double td = 0;
        string txtcaseno = "";
        if (txt_no.Text != "" || txt_fpnumber.Text != "")
        {
            if (ddlDepartment.SelectedValue == "FP" || Session["dept_code"].ToString() == "FP")
            {
                txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (ddlDepartment.SelectedValue == "BA" || Session["dept_code"].ToString() == "BA")
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }
        }
        //if (txt_no.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "" || txt_fromdate.Text != ""
        //    || txt_todate.Text != "" || txt_shortname.Text != "" || txt_fpnumber.Text != ""
        //    || txt_fpdate.Text != "" || ddl_status.SelectedValue != "-1" || ddl_user.SelectedValue != "-1" || ddlDepartment.SelectedValue != " - 1" )
        //{
        if (txt_fromdate.Text != "" && txt_todate.Text != "")
        {
            fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
            td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));

        }        //if (txt_caseno.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "")
                 //{
        div_rpt.Visible = true;
        string Division = "";
        if (Session["role"].ToString() == "Admin" ||
            Session["role"].ToString() == "Assistant Director" ||
            Session["role"].ToString() == "Additional Director" ||
            Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
        {
            if (ddlDepartment.SelectedValue != "-1")
            {
                Division = ddlDepartment.SelectedValue;
            }
            else
            {
                Division = "";
            }

        }
        else if (Session["role"].ToString() == "Department Head")
        {
            Division = Session["dept_code"].ToString();
        }
        else
        {
            Division = "";
        }
        string user = "";
        if (Session["role"].ToString() == "Officer")
        {
            user = Session["userid"].ToString();
        }
        else
        {
            user = ddl_user.SelectedValue;
        }

        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();

            try
            {
                string imageURL = HttpContext.Current.Server.MapPath("~/").Replace("\\", "/") + "images/dfs_logo.png";

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(50f, 50f);

                jpg.SpacingBefore = 100f;

                jpg.SpacingAfter = 100f;

                jpg.Alignment = Element.ALIGN_CENTER;

                document.Add(jpg);

            }

            catch (Exception ex) { }

            string institutename = Session["inst_name"].ToString();

            string logotext1 = institutename;
            Paragraph logo = new Paragraph();
            logo.SpacingBefore = 20;
            logo.SpacingAfter = 15;
            logo.Alignment = Element.ALIGN_CENTER;
            logo.Font = new Font(FontFactory.GetFont("Arial", 13, Font.BOLD));
            logo.Add(logotext1);
            document.Add(logo);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.ITALIC);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);



            //string logotext2 = "(Home Department  Government of Gujarat)";
            Paragraph logo2 = new Paragraph();
            //logo2.SpacingBefore = 20;
            logo2.SpacingAfter = 20;
            logo2.Alignment = Element.ALIGN_CENTER;
            logo2.Font = new Font(FontFactory.GetFont("Arial", 13, Font.BOLD));
            //logo2.Add(logotext2);
            //document.Add(logo2);
            var normalFont2 = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.ITALIC);
            var boldFont2 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            //string text = "Case Details of:- " + Casenozip;
            //Paragraph paragraph = new Paragraph();
            //paragraph.SpacingBefore = 20;
            //paragraph.SpacingAfter = 20;
            //paragraph.Alignment = Element.ALIGN_CENTER;
            //paragraph.Font = new Font(FontFactory.GetFont("Arial", 13, Font.BOLD));
            //paragraph.Add(text);
            //document.Add(paragraph);


            //string text002 = "Report Details";
            //Paragraph paragraph001 = new Paragraph();
            //paragraph001.SpacingBefore = 20;
            //paragraph001.SpacingAfter = 20;
            //paragraph001.Alignment = Element.ALIGN_CENTER;
            //paragraph001.Font = new Font(FontFactory.GetFont("Arial", 15, Font.BOLD));
            //paragraph001.Add(text002);
            //document.Add(paragraph001);



            PdfPTable table2 = new PdfPTable(6);

            PdfPCell cellsrno = new PdfPCell(new Phrase("Srno", boldFont));
            PdfPCell cell = new PdfPCell(new Phrase("CaseNo", boldFont));
            PdfPCell cell1 = new PdfPCell(new Phrase("Agency No", boldFont));
            PdfPCell cell2 = new PdfPCell(new Phrase("Reference No", boldFont));
            PdfPCell cell3 = new PdfPCell(new Phrase("Status", boldFont));
            PdfPCell cell4 = new PdfPCell(new Phrase("Notes", boldFont));
            cellsrno.HorizontalAlignment = 1;
            cell.HorizontalAlignment = 1;
            cell1.HorizontalAlignment = 1;
            cell2.HorizontalAlignment = 1;
            cell3.HorizontalAlignment = 1;
            cell4.HorizontalAlignment = 1;

            //cell.Rowspan = 5;
            //cell1.Rowspan = 5;
            //cell2.Rowspan = 5;
            //cell3.Rowspan = 5;
            //cell4.Rowspan = 5;
            table2.AddCell(cellsrno);
            table2.AddCell(cell);
            table2.AddCell(cell1);
            table2.AddCell(cell2);
            table2.AddCell(cell3);
            table2.AddCell(cell4);

            document.Add(table2);
            //document.Add(table2);

            //foreach (string MainData in arraydata)
            //{

            //    string[] separatewithcomma = MainData.Split(',');

            //    hashf = separatewithcomma[0];
            //    notesf = separatewithcomma[1];
            //    filenamef = separatewithcomma[2];
            //    datef = separatewithcomma[3];
            //    string originalfile = separatewithcomma[4];

            //    PdfPTable table1 = new PdfPTable(4);
            //    table1.AddCell(filenamef + "(" + originalfile + ")");
            //    table1.AddCell(hashf);
            //    table1.AddCell(notesf);
            //    table1.AddCell(datef);
            //    document.Add(table1);



            //Paragraph para100 = new Paragraph();
            //Chunk chunk100 = new Chunk(new VerticalPositionMark());
            //Phrase ph100 = new Phrase();
            //ph100.Add(new Chunk(Environment.NewLine));
            //Paragraph para102 = new Paragraph();
            //ph100.Add(new Chunk("File Name: " + filenamef));
            //ph100.Add(chunk100); // Here I add special chunk to the same phrase.    

            //para102.Add(ph100);
            //document.Add(para102);


            //Paragraph para105 = new Paragraph();
            //Chunk chunk105 = new Chunk(new VerticalPositionMark());
            //Phrase ph105 = new Phrase();
            //ph105.Add(new Chunk(Environment.NewLine));
            //Paragraph main105 = new Paragraph();
            //ph105.Add(new Chunk("Remarks: " + notesf));
            //ph105.Add(chunk105); // Here I add special chunk to the same phrase.    
            //main105.Add(ph105);
            //para105.Add(main105);
            //document.Add(para105);


            //Paragraph para103 = new Paragraph();
            //Chunk chunk103 = new Chunk(new VerticalPositionMark());
            //Phrase ph103 = new Phrase();
            //ph103.Add(new Chunk(Environment.NewLine));
            //Paragraph main103 = new Paragraph();
            //ph103.Add(new Chunk("Hash of the File: " + hashf));
            //ph103.Add(chunk103); // Here I add special chunk to the same phrase.    
            //main103.Add(ph103);
            //para103.Add(main103);
            //document.Add(para103);

            //Paragraph para104 = new Paragraph();
            //Chunk chunk104 = new Chunk(new VerticalPositionMark());
            //Phrase ph104 = new Phrase();
            //ph104.Add(new Chunk(Environment.NewLine));
            //Paragraph main104 = new Paragraph();
            //ph104.Add(new Chunk("File Uploaded Date: " + datef));
            //ph104.Add(chunk104); // Here I add special chunk to the same phrase.    
            //main103.Add(ph104);
            //para104.Add(main104);
            //document.Add(para104);

            //}

            string res = fl.GetEvidencereport(txt_agencyname.Text, txtcaseno, txt_refernceno.Text, fd.ToString(), td.ToString(), Division, user, ddl_status.SelectedValue, Session["inst_code"].ToString());

            if (!res.StartsWith("Error"))
            {

                DataTable dt_Request = fl.Tabulate(res);
                if (dt_Request.Rows.Count > 0)
                {


                    //string text2 = "Case List";
                    //Paragraph paragraph02 = new Paragraph();
                    //paragraph02.SpacingBefore = 20;
                    //paragraph02.SpacingAfter = 20;
                    //paragraph02.Alignment = Element.ALIGN_CENTER;
                    //paragraph02.Font = new Font(FontFactory.GetFont("Arial", 15, Font.BOLD));
                    //paragraph02.Add(text2);
                    //document.Add(paragraph02);
                    int SrNo = 1;
                    //string filedownloadedon = "";

                    for (int i = 0; i < dt_Request.Rows.Count; i++)
                    {
                        //string downloadedstatus = dt_Request.Rows[i]["status"].ToString();
                        //string hodremarks = dt_Request.Rows[i]["hodremarks"].ToString();
                        //string officerremarks = dt_Request.Rows[i]["remarksbyofficer"].ToString();
                        //string requestedby = dt_Request.Rows[i]["requestedby"].ToString();
                        //filedownloadedon = dt_Request.Rows[i]["filedownloadedon"].ToString();
                        //string requestedon = dt_Request.Rows[i]["requestedon"].ToString();
                        //string hodstatus = dt_Request.Rows[i]["hodstatus"].ToString();
                        //string approvedby = dt_Request.Rows[i]["approvedby"].ToString();
                        //string requestedondate = FlureeCS.Epoch.AddMilliseconds(
                        //Convert.ToInt64(requestedon)).ToString("dd-MMM-yyyy HH:mm:ss");

                        string caseno = dt_Request.Rows[i]["caseno"].ToString();
                        string agencyname = dt_Request.Rows[i]["agencyname"].ToString();
                        string agencyreferanceno = dt_Request.Rows[i]["agencyreferanceno"].ToString();
                        string status = dt_Request.Rows[i]["status"].ToString();
                        string notes = dt_Request.Rows[i]["notes"].ToString();

                        PdfPTable table3 = new PdfPTable(6);
                        PdfPCell cellfsrno = new PdfPCell(new Phrase(Convert.ToString(SrNo)));
                        PdfPCell cellf = new PdfPCell(new Phrase(caseno));
                        PdfPCell cellf1 = new PdfPCell(new Phrase(agencyname));
                        PdfPCell cellf2 = new PdfPCell(new Phrase(agencyreferanceno));
                        PdfPCell cellf3 = new PdfPCell(new Phrase(status));
                        PdfPCell cellf4 = new PdfPCell(new Phrase(notes));
                        cellfsrno.HorizontalAlignment = 1;
                        cellf.HorizontalAlignment = 1;
                        cellf1.HorizontalAlignment = 1;
                        cellf2.HorizontalAlignment = 1;
                        cellf3.HorizontalAlignment = 1;
                        cellf4.HorizontalAlignment = 1;
                        table3.AddCell(cellfsrno);
                        table3.AddCell(cellf);
                        table3.AddCell(cellf1);
                        table3.AddCell(cellf2);
                        table3.AddCell(cellf3);
                        table3.AddCell(cellf4);



                        //PdfPCell cell5 = new PdfPCell(new Phrase(caseno));
                        //PdfPCell cell6 = new PdfPCell(new Phrase(agencyname));
                        //PdfPCell cell7 = new PdfPCell(new Phrase(agencyreferanceno));
                        //PdfPCell cell8 = new PdfPCell(new Phrase(status));
                        //PdfPCell cell9 = new PdfPCell(new Phrase(notes));
                        //cell5.Rowspan = 4;
                        //cell6.Rowspan = 4;
                        //cell7.Rowspan = 4;
                        //cell8.Rowspan = 4;
                        //cell9.Rowspan = 4;
                        //table3.AddCell(cell5);
                        //table3.AddCell(cell6);
                        //table3.AddCell(cell7);
                        //table3.AddCell(cell8);
                        //table3.AddCell(cell9);
                        document.Add(table3);




                        //table3.AddCell("");

                        //document.Add(table2);
                        SrNo++;

                    }


                    //Paragraph para06 = new Paragraph();
                    //Chunk chunk06 = new Chunk(new VerticalPositionMark());
                    //Phrase ph06 = new Phrase();
                    //ph06.Add(new Chunk(Environment.NewLine));
                    //Paragraph para07 = new Paragraph();
                    //ph06.Add(new Chunk("Report Requestedon: " + requestedondate));
                    //ph06.Add(chunk06); // Here I add special chunk to the same phrase.    
                    //ph06.Add(new Chunk("Officer Remarks: " + officerremarks));
                    //para07.Add(ph06);
                    //document.Add(para07);

                    //Paragraph para08 = new Paragraph();
                    //Chunk chunk07 = new Chunk(new VerticalPositionMark());
                    //Phrase ph07 = new Phrase();
                    //ph07.Add(new Chunk(Environment.NewLine));
                    //Paragraph main0 = new Paragraph();
                    //ph07.Add(new Chunk("HOD Status: " + hodstatus)); // Here I add projectname as a chunk into Phrase.    
                    //ph07.Add(chunk07); // Here I add special chunk to the same phrase.    
                    //ph07.Add(new Chunk("Hod Remarks: " + hodremarks));
                    //main0.Add(ph07);
                    //para08.Add(main0);
                    //document.Add(para08);


                    //Paragraph para09 = new Paragraph();
                    //Chunk chunk08 = new Chunk(new VerticalPositionMark());
                    //Phrase ph08 = new Phrase();
                    //ph08.Add(new Chunk(Environment.NewLine));
                    //Paragraph para10 = new Paragraph();
                    //ph08.Add(new Chunk("File Downloaded On: " + System.DateTime.Now));
                    //para10.Add(ph08);
                    //document.Add(para10);


                }


                //Paragraph para006 = new Paragraph();
                //Chunk chunk006 = new Chunk(new VerticalPositionMark());

                //Phrase ph006 = new Phrase();
                //ph006.Add(new Chunk(Environment.NewLine));
                //Paragraph para007 = new Paragraph();
                //para007.SpacingBefore = 40;
                //para007.SpacingAfter = 20;
                //ph006.Add(new Chunk("Requested Person Signature", boldFont));
                //ph006.Add(chunk006); // Here I add special chunk to the same phrase.    
                //ph006.Add(new Chunk("Authority Signature", boldFont));
                //para007.Add(ph006);
                //document.Add(para007);


                //Paragraph para008 = new Paragraph();
                //Chunk chunk008 = new Chunk(new VerticalPositionMark());
                //Phrase ph008 = new Phrase();
                //ph008.Add(new Chunk(Environment.NewLine));
                //Paragraph para009 = new Paragraph();
                //ph008.Add(new Chunk("________________________"));
                //ph008.Add(chunk008); // Here I add special chunk to the same phrase.    
                //ph008.Add(new Chunk("________________________"));
                //para009.Add(ph008);
                //document.Add(para009);


            }

            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Caselist.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
            //FileStream fs = File.OpenRead(dirdb);
            //fs.Close();

        }


        //if (!res.StartsWith("Error"))
        //{
        //    DataTable dtdata = fl.Tabulate(res);
        //    if (dtdata.Rows.Count > 0)
        //    {

        //        string text2 = "Download Report Details";
        //        Paragraph paragraph02 = new Paragraph();
        //        paragraph02.SpacingBefore = 20;
        //        paragraph02.SpacingAfter = 20;
        //        paragraph02.Alignment = Element.ALIGN_CENTER;
        //        paragraph02.Font = new Font(FontFactory.GetFont("Arial", 15, Font.BOLD));
        //        paragraph02.Add(text2);
        //        document.Add(paragraph02);
        //        int SrNo = 1;
        //        string filedownloadedon = "";

        //        for (int i = 0; i < dt_Request.Rows.Count; i++)
        //        {
        //            string downloadedstatus = dt_Request.Rows[i]["status"].ToString();
        //            string hodremarks = dt_Request.Rows[i]["hodremarks"].ToString();
        //            string officerremarks = dt_Request.Rows[i]["remarksbyofficer"].ToString();
        //            string requestedby = dt_Request.Rows[i]["requestedby"].ToString();
        //            filedownloadedon = dt_Request.Rows[i]["filedownloadedon"].ToString();
        //            string requestedon = dt_Request.Rows[i]["requestedon"].ToString();
        //            string hodstatus = dt_Request.Rows[i]["hodstatus"].ToString();
        //            string approvedby = dt_Request.Rows[i]["approvedby"].ToString();
        //            string requestedondate = FlureeCS.Epoch.AddMilliseconds(
        //            Convert.ToInt64(requestedon)).ToString("dd-MMM-yyyy HH:mm:ss");



        //            PdfPTable table3 = new PdfPTable(2);
        //            PdfPCell cell4 = new PdfPCell(new Phrase(Convert.ToString(SrNo), boldFont));
        //            cell4.Colspan = 2;
        //            cell4.HorizontalAlignment = Element.ALIGN_LEFT;
        //            table3.AddCell(cell4);
        //            //table3.AddCell(Convert.ToString(SrNo));
        //            table3.AddCell("Report Requested On:");
        //            table3.AddCell(requestedondate);
        //            table3.AddCell("Requested By:");
        //            table3.AddCell(requestedby);
        //            table3.AddCell("Officer Remarks:");
        //            table3.AddCell(officerremarks);
        //            table3.AddCell("Approved By:");
        //            table3.AddCell(approvedby);
        //            table3.AddCell("Hod Remarks:");
        //            table3.AddCell(hodremarks);
        //            table3.AddCell("File Downloaded On:");
        //            if (filedownloadedon != "")
        //            {

        //                string filedownloadate = FlureeCS.Epoch.AddMilliseconds(
        //      Convert.ToInt64(filedownloadedon)).ToString("dd-MMM-yyyy HH:mm:ss");

        //                table3.AddCell(filedownloadate);

        //            }
        //            else 
        //            {
        //                table3.AddCell(Convert.ToString(System.DateTime.Now));

        //            }

        //            //table3.AddCell("");

        //            document.Add(table3);
        //            SrNo++;

        //        }


        //    }
        //    else
        //    {
        //        Header.Visible = false;
        //        title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

        //        rpt_details.DataBind();
        //    }
        //}
        //}

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        double fd = 0;
        double td = 0;
        string txtcaseno = "";
        if (txt_no.Text != "" || txt_fpnumber.Text != "")
        {
            if (ddlDepartment.SelectedValue == "FP" || Session["dept_code"].ToString() == "FP")
            {
                txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
            }
            else if (ddlDepartment.SelectedValue == "BA" || Session["dept_code"].ToString() == "BA")
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
            }
            else
            {
                txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
            }
        }
        //if (txt_no.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "" || txt_fromdate.Text != ""
        //    || txt_todate.Text != "" || txt_shortname.Text != "" || txt_fpnumber.Text != ""
        //    || txt_fpdate.Text != "" || ddl_status.SelectedValue != "-1" || ddl_user.SelectedValue != "-1" || ddlDepartment.SelectedValue != "-1")
        //{
        if (txt_fromdate.Text != "" && txt_todate.Text != "")
        {
            fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_fromdate.Text + " 00:00:00"));
            td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_todate.Text + " 23:59:59"));

        }        //if (txt_caseno.Text != "" || txt_agencyname.Text != "" || txt_refernceno.Text != "")
                 //{
        div_rpt.Visible = true;
        string Division = "";
        if (Session["role"].ToString() == "Admin" ||
            Session["role"].ToString() == "Assistant Director" ||
            Session["role"].ToString() == "Additional Director" ||
            Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
        {
            if (ddlDepartment.SelectedValue != "-1")
            {
                Division = ddlDepartment.SelectedValue;
            }
            else
            {
                Division = "";
            }

        }
        else if (Session["role"].ToString() == "Department Head")
        {
            Division = Session["dept_code"].ToString();
        }
        else
        {
            Division = "";
        }
        string user = "";
        if (Session["role"].ToString() == "Officer")
        {
            user = Session["userid"].ToString();
        }
        else
        {
            user = ddl_user.SelectedValue;
        }


        string res = fl.GetEvidencereport(txt_agencyname.Text, txtcaseno, txt_refernceno.Text, fd.ToString(), td.ToString(), Division, user, ddl_status.SelectedValue, Session["inst_code"].ToString());

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate(res);
            if (dtdata.Rows.Count > 0)
            {
                title.InnerHtml = "";
                Header.Visible = true;
                if (txt_fromdate.Text != "" && txt_todate.Text != "")
                {
                    title.InnerText = "Evidence data between " + txt_fromdate.Text + " to " + txt_todate.Text;
                }
                //else
                //{
                //    title.InnerText = "Evidence data of " + ddlDep.SelectedItem + " Division ";

                //}
                rpt_details.DataSource = dtdata;
                rpt_details.DataBind();
            }
            else
            {
                Header.Visible = false;
                title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

                rpt_details.DataBind();
            }
        }
        //}
        //else
        //{
        //    Response.Write("<script>alert('Please fill at least one category.')</script>");
        //}
    }


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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDepartment.SelectedValue != "-1")
        {

            fill_user_departmetwise(ddlDepartment.SelectedValue);
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
        }
    }

    public void fill_user_departmetwise( string Depcode)
    {
        string rescode = "";
        rescode = fl.GetUsersDeptcodewiseafterIndexchanges(Depcode);
        DataTable dt = fl.Tabulate(rescode);
        ddl_user.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddl_user.DataSource = dt;
            ddl_user.DataTextField = "Firstname";
            ddl_user.DataValueField = "userid";
            ddl_user.DataBind();

            ddl_user.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        }
        else
        {
            ddl_user.Items.Add(new ListItem("No user found", "-1"));
        }
    }

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

    protected void btn_generatepdf_Click(object sender, EventArgs e)
    {
        pdf_Details();
    }
}