using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;
using System.Net;
using System.Net.Mail;
//using System.Drawing;
using System.Drawing.Imaging;
using ZXing;


public partial class ViewDetails : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();



    public void getdetails()
    {
        try
        {
            this.div_grd.InnerHtml = "";
            string CaseNo = Request.QueryString["caseno"];

            lbl_caseno.Text = CaseNo;
            string DeptCode = "";
            if (Session["role"].ToString() == "Admin" ||
                Session["role"].ToString() == "Assistant Director" ||
                Session["role"].ToString() == "Additional Director" ||
                Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
            {
                DeptCode = "";
            }
            else
            {
                DeptCode = Session["dept_code"].ToString();
            }

            string userid = "";
            if (Session["role"].ToString() == "Officer")
            {
                userid = Session["userid"].ToString();
            }
            else 
            {
                userid = "";
            }
           
            string res = fl.GetCaseuserwise(CaseNo,userid);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    string timelineurl = "";
                    timelineurl += "Click Here to view timeline for Casenumber :  <b><a href ='Timeline.aspx?caseno=" + CaseNo
                        + "' class='alert-link'>" + CaseNo + "</a></b>";
                    timeline.InnerHtml = timelineurl;
                    timeline.Visible = true;

                    string resEvidence = fl.GetUserAcceptanceDetails(CaseNo);
                    if (!resEvidence.StartsWith("Error"))
                    {

                        DataTable dtevidence = fl.Tabulate(resEvidence);
                        if (dtevidence.Rows.Count > 0)
                        {
                            lbl_agencyname.Text = dtevidence.Rows[0]["agencyname"].ToString();
                            lbl_referenceno.Text = dtevidence.Rows[0]["agencyreferanceno"].ToString();


                        }
                    }


                    string resTrack = fl.GetTrackDetails(CaseNo);

                    if (!resTrack.StartsWith("Error"))
                    {

                        DataTable dttrack = fl.Tabulate(resTrack);
                        if (dttrack.Rows.Count > 0)
                        {

                            string assignto = "";
                            string dbstatus = "";
                            string firstname = "";
                            string lastname = "";
                            string username = "";
                            string caseassignby = "";
                            string firstname_assign = "";
                            string lastname_assign = "";
                            string username_assign = "";
                            for (int i = 0; i < dttrack.Rows.Count; i++)
                            {

                                assignto = dttrack.Rows[i]["assignto"].ToString();
                                dbstatus = dttrack.Rows[i]["status"].ToString();
                                if (dbstatus == "Assigned")
                                {

                                    caseassignby = dttrack.Rows[i]["caseassignby"].ToString();

                                    string userdata = fl.GetUserDetails(assignto);
                                    if (!userdata.StartsWith("Error"))
                                    {
                                        DataTable dtuser = fl.Tabulate(userdata);

                                        if (dtuser.Rows.Count > 0)
                                        {

                                            firstname = dtuser.Rows[0]["firstname"].ToString();
                                            lastname = dtuser.Rows[0]["lastname"].ToString();
                                            username = dtuser.Rows[0]["username"].ToString();
                                            lbl_assignto.Text = firstname + " " + lastname + " (" + username + ")";

                                        }
                                    }
                                    string caseassigndata = fl.GetUserDetailsforCaseAssignBY(caseassignby);
                                    if (!caseassigndata.StartsWith("Error"))
                                    {
                                        DataTable dtuser = fl.Tabulate(caseassigndata);

                                        if (dtuser.Rows.Count > 0)
                                        {
                                            tbl_details.Visible = true;
                                            lblNoData.Visible = false;
                                            firstname_assign = dtuser.Rows[0]["firstname"].ToString();
                                            lastname_assign = dtuser.Rows[0]["lastname"].ToString();
                                            username_assign = dtuser.Rows[0]["username"].ToString();
                                            lbl_assignby.Text = firstname_assign + " " + lastname_assign + " (" + username_assign + ")";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string rescase = fl.GetCasewiseAttachment(CaseNo);

                    if (!res.StartsWith("Error"))
                    {
                        DataTable dtcase = fl.Tabulate(rescase);

                        if (dtcase.Rows.Count > 0)
                        {
                            btn_request.Visible = true;
                            int Annexures = 0;
                            int Reports = 0;
                            int Exhibits = 0;
                            int count = 0;

                            string html = "";
                            html += "<div class='table - responsive table - data'><table class='table table-borderless table-striped table-earning'>";
                            html += "<thead>";
                            html += "<tr>";
                            html += "<th>FileName</th>";
                            html += "<th>Type</th>";
                            html += "<th>Notes</th>";

                            html += "<th>Date</th>";
                            html += " </tr>";
                            html += " </thead>";
                            html += "<tbody>";
                            for (int i = 0; i < dtcase.Rows.Count; i++)
                            {

                                string file = dtcase.Rows[i]["filename"].ToString();
                                string path = dtcase.Rows[i]["path"].ToString();
                                string[] filepath = path.Split('/');
                                string localhost = filepath[2];
                                string[] split = localhost.Split(':');

                                string Host = Request.Url.Host;
                                string type1 = "";
                                if (split[0] == "localhost")
                                {

                                    type1 = filepath[8];
                                }
                                else
                                {
                                    type1 = filepath[9];
                                }


                                string type = dtcase.Rows[i]["type"].ToString();
                                string notes = dtcase.Rows[i]["notes"].ToString();
                                string caseid = dtcase.Rows[i]["case_id"].ToString();
                                string createddate = dtcase.Rows[i]["createddate"].ToString();

                                string converteddate = FlureeCS.Epoch.AddMilliseconds(
                                       Convert.ToInt64(createddate)).ToString("dd-MMM-yyyy HH:mm:ss");

                                if (CaseNo == caseid)
                                {

                                    count++;


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

                                    html += "<tr>";
                                    if (Session["role"].ToString() == "Officer")
                                    {
                                        html += " <td>" + filename + "</td>";

                                    }
                                    else
                                    {
                                        html += "<td><a href='" + path + "'  target='_blank'>" + filename + "</a></td>";

                                    }

                                    html += "<td>" + type + "</td>";
                                    html += "<td>" + notes + "</td>";

                                    html += "<td>" + converteddate + "</td>";

                                    html += "<tr>";
                                }

                            }

                            html += "</tbody>";
                            html += "</table></div></div></div>";


                            if (count != 0)
                            {
                                this.div_grd.InnerHtml = html;
                                title.Visible = true;
                                div_grd.Visible = true;
                               // lblNoData.Visible = false;
                            }

                        }

                    }

                }
                else
                {
                    lblNoData.Visible = true;

                    div_grd.Visible = false;
                    tbl_details.Visible = false;
                }
            }

           
        }
        catch (Exception ex) { }
    }

    public void getreportdetails()
    {

        string resRequest = fl.Getrequestidbycaseno(Request.QueryString["caseno"]);
        if (!resRequest.StartsWith("Error"))
        {

            DataTable dtRequest = fl.Tabulate(resRequest);
            if (dtRequest.Rows.Count > 0)
            {
                hdn_requestid.Value = dtRequest.Rows[0]["requestid"].ToString();

                string hodstatus = dtRequest.Rows[0]["hodstatus"].ToString();
                string repstatus = dtRequest.Rows[0]["status"].ToString();
                string officerstatus = dtRequest.Rows[0]["officerstatus"].ToString();
                string requestedby = dtRequest.Rows[0]["requestedby"].ToString();
                string[] Split_Username = requestedby.Split('(');
                string[] SplitUsername = Split_Username[1].Split(')');
                if (Session["role"].ToString() == "Officer")
                {
                    if (SplitUsername[0] == Session["username"].ToString())
                    {
                        if (hodstatus == "Approve")
                        {
                            bttnzip.Visible = true;
                            btn_request.Visible = false;

                            if (repstatus == "Yes")
                            {
                                bttnzip.Visible = false;
                                btn_request.Visible = true;
                            }
                        }
                        else if (hodstatus == "Reject")
                        {
                            bttnzip.Visible = false;
                            btn_request.Visible = true;
                        }
                        else if (officerstatus == "Requested")
                        {

                            btn_request.Visible = false;
                        }
                    }
                }
                else
                {
                    btn_request.Visible = false;
                    bttnzip.Visible = false;

                }

            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
           
                getdetails();
                getreportdetails();
            if (Session["role"].ToString() != "Officer")
            {
                btn_request.Visible = false;
                bttnzip.Visible = false;
            }


            }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void bttnzip_Click(object sender, EventArgs e)
    {

        string uploadszip = "";
        string dfszip = "";
        string departmentzip = "";
        string divzip = "";
        string folderzip = "";
        string typezip = "";
        //string file = "";
        string path = "";
        string extrafiledata = "";
        string DBFile = "";
        string filename = "";
        string converteddate = "";


        string assigntozip = "";
        string assignbyzip = "";
        string dbstatuszip = "";
        string firstnamezip = "";
        string lastnamezip = "";
        string usernamezip = "";
        string refrenceno = "";
        string agencyname = "";
        string noteszip = "";
        string statuszip = "";
        string createddateTrek = "";
        string firstname_assignzip = "";
        string lastname_assignzip = "";
        string username_assignzip = "";
        string html = "";

        string Casenozip = Request.QueryString["caseno"];

        string resEvidence = fl.GetUserAcceptanceDetails(Casenozip);
        if (!resEvidence.StartsWith("Error"))
        {

            DataTable dtevidence = fl.Tabulate(resEvidence);
            if (dtevidence.Rows.Count > 0)
            {
                agencyname = dtevidence.Rows[0]["agencyname"].ToString();
                refrenceno = dtevidence.Rows[0]["agencyreferanceno"].ToString();

            }
        }
        string trekdata = "";
        string resTrack = fl.GetPDFTrekDetails(Casenozip);

        if (!resTrack.StartsWith("Error"))
        {

            DataTable dttrack = fl.Tabulate(resTrack);
            if (dttrack.Rows.Count > 0)
            {

                for (int i = 0; i < dttrack.Rows.Count; i++)
                {

                    assigntozip = dttrack.Rows[i]["assignto"].ToString();
                    dbstatuszip = dttrack.Rows[i]["status"].ToString();
                    noteszip = dttrack.Rows[i]["notes"].ToString();
                    createddateTrek = dttrack.Rows[i]["createddate"].ToString();

                    string converteddateTrek = FlureeCS.Epoch.AddMilliseconds(
                             Convert.ToInt64(createddateTrek)).ToString("dd-MMM-yyyy HH:mm:ss");


                    if (dbstatuszip == "Assigned")
                    {

                        assignbyzip = dttrack.Rows[i]["caseassignby"].ToString();

                        string userdata = fl.GetUserDetails(assigntozip);
                        if (!userdata.StartsWith("Error"))
                        {
                            DataTable dtuser = fl.Tabulate(userdata);

                            if (dtuser.Rows.Count > 0)
                            {

                                firstnamezip = dtuser.Rows[0]["firstname"].ToString();
                                lastnamezip = dtuser.Rows[0]["lastname"].ToString();
                                usernamezip = dtuser.Rows[0]["username"].ToString();

                            }
                        }
                        string caseassigndata = fl.GetUserDetailsforCaseAssignBY(assignbyzip);
                        if (!caseassigndata.StartsWith("Error"))
                        {
                            DataTable dtuser = fl.Tabulate(caseassigndata);

                            if (dtuser.Rows.Count > 0)
                            {
                                firstname_assignzip = dtuser.Rows[0]["firstname"].ToString();
                                lastname_assignzip = dtuser.Rows[0]["lastname"].ToString();
                                username_assignzip = dtuser.Rows[0]["username"].ToString();
                               
                            }
                        }
                    }

                    trekdata += firstname_assignzip + " " + lastname_assignzip + " (" + username_assignzip + ")" + ',' + firstnamezip + " " + lastnamezip + "(" + usernamezip + ")" + ',' + dbstatuszip + ',' + noteszip + ',' + converteddateTrek + "^";

                }

            }
        }

        string reszip = fl.GetCasewiseAttachment(Casenozip);

        if (!reszip.StartsWith("Error"))
        {
            DataTable dtzip = fl.Tabulate(reszip);

            if (dtzip.Rows.Count > 0)
            {
                int Annexures = 0;
                int Reports = 0;
                int Exhibits = 0;
                int count = 0;

                //string html = "";

                for (int i = 0; i < dtzip.Rows.Count; i++)
                {
                    string pathzip = dtzip.Rows[i]["path"].ToString();
                    string filezip = dtzip.Rows[i]["filename"].ToString();
                    string hash = dtzip.Rows[i]["hash"].ToString();
                    string type = dtzip.Rows[i]["type"].ToString();
                    string notes = dtzip.Rows[i]["notes"].ToString();
                    string caseid = dtzip.Rows[i]["case_id"].ToString();
                    string createddate = dtzip.Rows[i]["createddate"].ToString();
                    string[] filepath = pathzip.Split('/');
                    string type1 = "";
                    string localhost = filepath[2];
                    string[] split = localhost.Split(':');

                    string Host = Request.Url.Host;
                  
                   if (split[0] == "localhost")
                    {

                        type1 = filepath[8];

                        uploadszip = filepath[3];
                        dfszip = filepath[4];
                        departmentzip = filepath[5];
                        divzip = filepath[6];
                        folderzip = filepath[7];
                        typezip = filepath[8];
                        DBFile = filepath[9];
                    }
                    else
                    {
                        type1 = filepath[9];

                        uploadszip = filepath[4];
                        dfszip = filepath[5];
                        departmentzip = filepath[6];
                        divzip = filepath[7];
                        folderzip = filepath[8];
                        typezip = filepath[9];
                        DBFile = filepath[10];
                    }


                    converteddate = FlureeCS.Epoch.AddMilliseconds(
                           Convert.ToInt64(createddate)).ToString("dd-MMM-yyyy HH:mm:ss");

                    if (Casenozip == caseid)
                    {
                        count++;

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

                    }



                    path += Server.MapPath("~/" + uploadszip + '\\' + dfszip + '\\' + departmentzip + '\\' + divzip + '\\' + folderzip + '\\' + typezip + '\\' + DBFile + '^');
                    extrafiledata += hash + ',' + notes + ',' + filename + ',' + converteddate + ',' + DBFile + '^';

                }

                string[] arraydata = extrafiledata.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);


                string hashf = "";
                string notesf = "";
                string filenamef = "";
                string datef = "";

                Random rndm = new Random();
                int random = rndm.Next();
                string pdfpath = "";
                string pdfcaseno = Casenozip.Replace("/", "_");
                string fn = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + random + ".pdf";
                string folderpdf = "ExtraFiles";


                string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                 "\\\\" + Session["dept_code"].ToString() + "\\\\" + Session["div_code"].ToString()
                 + "\\\\" + pdfcaseno + "\\\\" + folderpdf;


                pdfpath = dir +
                   "\\\\" + fn;


                string PDFLink = "";
                string dirdb = "";


                dirdb = "Uploads/" + Session["inst_code"].ToString() + "/" + Session["dept_code"].ToString() + "/" + Session["div_code"].ToString() + "/" + pdfcaseno + "/" + folderpdf + "/";

                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                PDFLink = strUrl + dirdb + fn;
                string fullpath = HttpContext.Current.Server.MapPath("~/").Replace("\\", "/") + dirdb;

                if (!Directory.Exists(fullpath))
                    Directory.CreateDirectory(fullpath);

                dirdb = fullpath + fn;


                string[] arraytrekdata = trekdata.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);
                string reportupdate = fl.Updatereportpath(hdn_requestid.Value, dirdb);

                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 30, 30, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    PdfWriter.GetInstance(document, new FileStream(dirdb, FileMode.Create));

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
                   // logo2.Add(logotext2);
                    document.Add(logo2);
                    var normalFont2 = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.ITALIC);
                    var boldFont2 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                    string text = "Case Details of:- " + Casenozip;
                    Paragraph paragraph = new Paragraph();
                    paragraph.SpacingBefore = 20;
                    paragraph.SpacingAfter = 20;
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Font = new Font(FontFactory.GetFont("Arial", 13, Font.BOLD));
                    paragraph.Add(text);
                    document.Add(paragraph);


                    //foreach (string MaintrekData in arraytrekdata)
                    //{
                    //    string[] treksplitted = MaintrekData.Split(',');
                    //    string assign_by = treksplitted[0];
                    //    string assign_to = treksplitted[1];
                    //    statuszip = treksplitted[2];
                    //    string treknotes = treksplitted[3];
                    //    string trekdate = treksplitted[4];
                        //string trekdatef = treksplitted[3];

                        //1//


                        //if (statuszip == "Assigned")
                        //{
                        //    Chunk glue = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus = new Paragraph("Status: Assigned ("+ trekdate + ")", boldFont);
                        //    pstatus.Alignment = Element.ALIGN_CENTER;
                        //    pstatus.Add(new Chunk(glue));
                        //    pstatus.SpacingBefore = 20;

                        //    document.Add(pstatus);

                        //    Paragraph para0 = new Paragraph();
                        //    Chunk chunk0 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph0 = new Phrase();
                        //    ph0.Add(new Chunk(Environment.NewLine));
                        //    Paragraph para01 = new Paragraph();
                        //    ph0.Add(new Chunk("Agency Name: " + agencyname));
                        //    ph0.Add(chunk0); // Here I add special chunk to the same phrase.    
                        //    ph0.Add(new Chunk("Reference No: " + refrenceno));
                        //    para01.Add(ph0);
                        //    document.Add(para01);

                        //    Paragraph para = new Paragraph();
                        //    Chunk chunk2 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph1 = new Phrase();
                        //    ph1.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main = new Paragraph();
                        //    ph1.Add(new Chunk("Case Assign By: " + assign_by)); // Here I add projectname as a chunk into Phrase.    
                        //    ph1.Add(chunk2); // Here I add special chunk to the same phrase.    
                        //    ph1.Add(new Chunk("Case Assign To: " + assign_to));
                        //    main.Add(ph1);
                        //    para.Add(main);
                        //    document.Add(para);


                        //    Paragraph para1 = new Paragraph();
                        //    Chunk chunk3 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph2 = new Phrase();
                        //    ph2.Add(new Chunk(Environment.NewLine));
                        //    Paragraph para2 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph2.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph2.Add(new Chunk("Remarks: -------- "));
                        //    }
                        //    ph2.Add(chunk3); // Here I add special chunk to the same phrase.    
                        //    //ph2.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    para2.Add(ph2);
                        //    document.Add(para2);
                        //    //PdfPTable table = new PdfPTable(3);
                        //    //PdfPCell cell = new PdfPCell(new Phrase("Status: Assigned", boldFont));
                        //    //table.AddCell("Agency Name: " + agencyname);
                        //    //cell.Colspan = 2;
                        //    //table.AddCell(cell);
                        //    //table.AddCell("Reference No: " + refrenceno);

                        //    //document.Add(table);

                        //}


                        //2//
                        //if (statuszip == "Inprogress")
                        //{
                        //    Chunk glue2 = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus1 = new Paragraph("Status: Inprogress (" + trekdate + ")", boldFont);
                        //    pstatus1.Alignment = Element.ALIGN_CENTER;
                        //    pstatus1.Add(new Chunk(glue2));
                        //    pstatus1.SpacingBefore = 20;

                        //    document.Add(pstatus1);

                        //    Paragraph para3 = new Paragraph();
                        //    Chunk chunk4 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph3 = new Phrase();
                        //    ph3.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main2 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph3.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph3.Add(new Chunk("Remarks: -------- "));
                        //    }
                        //    ph3.Add(chunk4); // Here I add special chunk to the same phrase.    
                        //    //ph3.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    main2.Add(ph3);
                        //    para3.Add(main2);
                        //    document.Add(para3);
                        //}

                        //3//

                        //if (statuszip == "Report Preparation")
                        //{
                        //    Chunk glue3 = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus2 = new Paragraph("Status: Report Preparartion (" + trekdate + ")", boldFont);
                        //    pstatus2.Alignment = Element.ALIGN_CENTER;
                        //    pstatus2.Add(new Chunk(glue3));
                        //    pstatus2.SpacingBefore = 20;

                        //    document.Add(pstatus2);

                        //    Paragraph para4 = new Paragraph();
                        //    Chunk chunk5 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph4 = new Phrase();
                        //    ph4.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main3 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph4.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph4.Add(new Chunk("Remarks: -------- "));
                        //    }
                        //    ph4.Add(chunk5); // Here I add special chunk to the same phrase.    
                        //    //ph4.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    main3.Add(ph4);
                        //    para4.Add(main3);
                        //    document.Add(para4);
                        //}

                        //if (statuszip == "Pending for Director/HOD Signature")
                        //{
                        //    Chunk glue4 = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus4 = new Paragraph("Status: Pending for Signature (" + trekdate + ")", boldFont);
                        //    pstatus4.Alignment = Element.ALIGN_CENTER;
                        //    pstatus4.Add(new Chunk(glue4));
                        //    pstatus4.SpacingBefore = 20;

                        //    document.Add(pstatus4);

                        //    Paragraph para5 = new Paragraph();
                        //    Chunk chunk6 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph5 = new Phrase();
                        //    ph5.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main4 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph5.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph5.Add(new Chunk("Remarks: -------- "));
                        //    }
                        //    ph5.Add(chunk6); // Here I add special chunk to the same phrase.    
                        //    //ph5.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    main4.Add(ph5);
                        //    para5.Add(main4);
                        //    document.Add(para5);
                        //}


                        //if (statuszip == "Report Submission")
                        //{
                        //    Chunk glue5 = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus5 = new Paragraph("Status: Report Submission (" + trekdate + ")", boldFont);
                        //    pstatus5.Alignment = Element.ALIGN_CENTER;
                        //    pstatus5.Add(new Chunk(glue5));
                        //    pstatus5.SpacingBefore = 20;

                        //    document.Add(pstatus5);

                        //    Paragraph para6 = new Paragraph();
                        //    Chunk chunk7 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph6 = new Phrase();
                        //    ph6.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main5 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph6.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph6.Add(new Chunk("Remarks: -------- "));
                        //    }  // Here I add projectname as a chunk into Phrase.    
                        //    ph6.Add(chunk7); // Here I add special chunk to the same phrase.    
                        //    //ph6.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    main5.Add(ph6);
                        //    para6.Add(main5);
                        //    document.Add(para6);
                        //}


                        //if (statuszip == "Mismatch Found")
                        //{
                        //    Chunk glue6 = new Chunk(new VerticalPositionMark());
                        //    Paragraph pstatus6 = new Paragraph("Status: Mismatch Found (" + trekdate + ")", boldFont);
                        //    pstatus6.Alignment = Element.ALIGN_CENTER;
                        //    pstatus6.Add(new Chunk(glue6));
                        //    pstatus6.SpacingBefore = 20;

                        //    document.Add(pstatus6);

                        //    Paragraph para7 = new Paragraph();
                        //    Chunk chunk8 = new Chunk(new VerticalPositionMark());
                        //    Phrase ph7 = new Phrase();
                        //    ph7.Add(new Chunk(Environment.NewLine));
                        //    Paragraph main6 = new Paragraph();
                        //    if (treknotes != "")
                        //    {
                        //        ph7.Add(new Chunk("Remarks: " + treknotes));
                        //    }
                        //    else
                        //    {
                        //        ph7.Add(new Chunk("Remarks: -------- "));
                        //    }
                        //    ph7.Add(chunk8); // Here I add special chunk to the same phrase.    
                        //    //ph7.Add(new Chunk("Status ChangedOn: " + trekdate));
                        //    main6.Add(ph7);
                        //    para7.Add(main6);
                        //    document.Add(para7);
                        //}

                    //}


                    string text002 = "Report Details";
                    Paragraph paragraph001 = new Paragraph();
                    paragraph001.SpacingBefore = 20;
                    paragraph001.SpacingAfter = 20;
                    paragraph001.Alignment = Element.ALIGN_CENTER;
                    paragraph001.Font = new Font(FontFactory.GetFont("Arial", 15, Font.BOLD));
                    paragraph001.Add(text002);
                    document.Add(paragraph001);



                    PdfPTable table2 = new PdfPTable(4);

                    PdfPCell cell = new PdfPCell(new Phrase("File Name", boldFont));
                    PdfPCell cell1 = new PdfPCell(new Phrase("Hash of the File", boldFont));
                    PdfPCell cell2 = new PdfPCell(new Phrase("Remarks", boldFont));
                    PdfPCell cell3 = new PdfPCell(new Phrase("File Uploaded On", boldFont));
                    cell.Rowspan = 4;
                    cell1.Rowspan = 4;
                    cell2.Rowspan = 4;
                    cell3.Rowspan = 4;
                    table2.AddCell(cell);
                    table2.AddCell(cell1);
                    table2.AddCell(cell2);
                    table2.AddCell(cell3);
                    document.Add(table2);

                    foreach (string MainData in arraydata)
                    {

                        string[] separatewithcomma = MainData.Split(',');

                        hashf = separatewithcomma[0];
                        notesf = separatewithcomma[1];
                        filenamef = separatewithcomma[2];
                        datef = separatewithcomma[3];
                     string  originalfile = separatewithcomma[4];

                        PdfPTable table1 = new PdfPTable(4);
                        table1.AddCell(filenamef + "(" + originalfile + ")" );
                        table1.AddCell(hashf);
                        table1.AddCell(notesf);
                        table1.AddCell(datef);
                        document.Add(table1);



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

                    }

                    string RequestDetails = fl.Getrequestidbycaseno(Request.QueryString["caseno"]);
                    if (!RequestDetails.StartsWith("Error"))
                    {

                        DataTable dt_Request = fl.Tabulate(RequestDetails);
                        if (dt_Request.Rows.Count > 0)
                        {
                           

                            string text2 = "Download Report Details";
                            Paragraph paragraph02 = new Paragraph();
                            paragraph02.SpacingBefore = 20;
                            paragraph02.SpacingAfter = 20;
                            paragraph02.Alignment = Element.ALIGN_CENTER;
                            paragraph02.Font = new Font(FontFactory.GetFont("Arial", 15, Font.BOLD));
                            paragraph02.Add(text2);
                            document.Add(paragraph02);
                            int SrNo = 1;
                            string filedownloadedon = "";

                            for (int i =0; i< dt_Request.Rows.Count; i++)
                            {
                                string downloadedstatus = dt_Request.Rows[i]["status"].ToString();
                                string hodremarks = dt_Request.Rows[i]["hodremarks"].ToString();
                                string officerremarks = dt_Request.Rows[i]["remarksbyofficer"].ToString();
                                string requestedby = dt_Request.Rows[i]["requestedby"].ToString();
                                filedownloadedon = dt_Request.Rows[i]["filedownloadedon"].ToString();
                                string requestedon = dt_Request.Rows[i]["requestedon"].ToString();
                                string hodstatus = dt_Request.Rows[i]["hodstatus"].ToString();
                                string approvedby = dt_Request.Rows[i]["approvedby"].ToString();
                                string requestedondate = FlureeCS.Epoch.AddMilliseconds(
                                Convert.ToInt64(requestedon)).ToString("dd-MMM-yyyy HH:mm:ss");

                            

                                PdfPTable table3 = new PdfPTable(2);
                                PdfPCell cell4 = new PdfPCell(new Phrase(Convert.ToString(SrNo), boldFont));
                                cell4.Colspan = 2;
                                cell4.HorizontalAlignment = Element.ALIGN_LEFT;
                                table3.AddCell(cell4);
                                //table3.AddCell(Convert.ToString(SrNo));
                                table3.AddCell("Report Requested On:");
                                table3.AddCell(requestedondate);
                                table3.AddCell("Requested By:");
                                table3.AddCell(requestedby);
                                table3.AddCell("Officer Remarks:");
                                table3.AddCell(officerremarks);
                                table3.AddCell("Approved By:");
                                table3.AddCell(approvedby);
                                table3.AddCell("Hod Remarks:");
                                table3.AddCell(hodremarks);
                                table3.AddCell("File Downloaded On:");
                                if(filedownloadedon !="")
                                {
                                  
                                    string filedownloadate = FlureeCS.Epoch.AddMilliseconds(
                          Convert.ToInt64(filedownloadedon)).ToString("dd-MMM-yyyy HH:mm:ss");

                                    table3.AddCell(filedownloadate);

                                }
                                else
                                {
                                    table3.AddCell(Convert.ToString(System.DateTime.Now));
                             
                                }
                               
                                //table3.AddCell("");

                                document.Add(table3);
                                SrNo ++;

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


                        Paragraph para006 = new Paragraph();
                        Chunk chunk006 = new Chunk(new VerticalPositionMark());

                        Phrase ph006 = new Phrase();
                        ph006.Add(new Chunk(Environment.NewLine));
                        Paragraph para007 = new Paragraph();
                        para007.SpacingBefore = 40;
                        para007.SpacingAfter = 20;
                        ph006.Add(new Chunk("Requested Person Signature", boldFont));
                        ph006.Add(chunk006); // Here I add special chunk to the same phrase.    
                        ph006.Add(new Chunk("Authority Signature", boldFont));
                        para007.Add(ph006);
                        document.Add(para007);


                        Paragraph para008 = new Paragraph();
                        Chunk chunk008 = new Chunk(new VerticalPositionMark());
                        Phrase ph008 = new Phrase();
                        ph008.Add(new Chunk(Environment.NewLine));
                        Paragraph para009 = new Paragraph();
                        ph008.Add(new Chunk("________________________"));
                        ph008.Add(chunk008); // Here I add special chunk to the same phrase.    
                        ph008.Add(new Chunk("________________________"));
                        para009.Add(ph008);
                        document.Add(para009);


                    }

                    document.Close();
                    FileStream fs = File.OpenRead(dirdb);
                    fs.Close();

                }
              
                bttnzip.Visible = false;
                btn_request.Visible = true;

                string reportpath = "";
                string resreportpath = fl.Getreportpathbycaseno(Casenozip);
                if (!resreportpath.StartsWith("Error"))
                {
                    DataTable dtreport = fl.Tabulate(resreportpath);
                    if (dtreport.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtreport.Rows.Count; i++)
                        {

                            reportpath += dtreport.Rows[i]["reportpath"].ToString() + "^";
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Reports Not Found')</script");
                    }


                }
                else
                {
                    Response.Write("<script>alert('Reports Not Found')</script");
                }
                Response.BufferOutput = false;
                string sddfsd = path + reportpath;
                string[] Filenames = sddfsd.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries);

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFiles(Filenames, "Reports");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/zip";
                    Response.AddHeader(name: "Content-Disposition", value: "attachment;filename=" + Casenozip + ".zip");
                    int Files = Convert.ToInt32(Filenames.Length);
                    zip.Save(Response.OutputStream);
                    Response.Flush();

                }


            }



        }

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string folder = "";
        string fn = "";
        string path = "";
        string filename = "";
        string pathdb = "";
        HttpPostedFile file = null;
        if (fl_request.HasFiles)
        {
            file = fl_request.PostedFile;
            folder = "ReportRequestFile";


        }

        if (file != null)
        {

            filename = file.FileName;
            long size = file.ContentLength;



            if (size <= 5000000)
            {
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


                string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() + "\\\\" + folder;

                string dirdb = "Uploads/" + Session["inst_code"].ToString() + "/" + folder;
                
                pathdb = url + "/" + dirdb + "/" + filename;

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                path = dir +
                           "\\\\" + filename;

                if (!File.Exists(path))
                    file.SaveAs(path);
            }

            FileStream fs = File.OpenRead(path);

            
        }


        Guid id = Guid.NewGuid();
        string res = "";
        string caseno = Request.QueryString["caseno"].ToString();
        string[] splitcaseno = caseno.Split('/');
        string deptba = splitcaseno[1];
        string deptfp = splitcaseno[0];
        string dept = splitcaseno[3];
        string finaldept = "";
        if (deptba.ToString() == "BA")
        {
            finaldept = "BA";
        }
        else if (deptfp.ToString() == "FP")
        {
            finaldept = "FP";
        }
        else
        {
            finaldept = splitcaseno[3]; 
        }
        res = fl.Insertreportrequest(id.ToString(), Request.QueryString["caseno"].ToString(), txt_remarks.Text, Session["firstname"].ToString() +" " + Session["lastname"].ToString() + "(" + Session["username"].ToString() + ")", finaldept, Session["inst_code"].ToString(),pathdb);

        if (!res.StartsWith("Error"))
        {
            DataTable dtdata = fl.Tabulate("[" + res + "]");
            if (dtdata.Rows.Count > 0)
            {
                if (dtdata.Rows[0]["status"].ToString() == "200")
                {
                    txt_remarks.Text = "";
                    Response.Write("<script>alert('Request send Sucessfully');window.location.href='ViewDetails.aspx?caseno=" + caseno + "';</script>");
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //   "<script>alert('Request send Sucessfully');window.location.href='ViewDetails.aspx?caseno='"+ caseno +"''</script>");

                }
                else
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('Request not send');</script>");

                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "<script>alert('Request not send');</script>");

            }
        }
        else
        {

            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('Request not send');</script>");

        }

    }
}