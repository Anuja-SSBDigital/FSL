using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using log4net;
using System.Data;
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

public partial class validatecert : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (fuCerti.HasFile)
        {
            string id = Guid.NewGuid().ToString();
            Stream fs = fuCerti.FileContent;
            string hash = fl.SHA256CheckSum(fs);
            string res = fl.GetCertiByHash(hash);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    string filename = dt.Rows[0]["filename"].ToString();
                    string[] Splitfilename = filename.Split('_');
                    string instcode = Splitfilename[0];
                    string divcode = Splitfilename[1];
                    string year = Splitfilename[2];
                    string deptcode = Splitfilename[3];
                    string no = Splitfilename[4];
                    string random = Splitfilename[5];
                    string caseno = "";

                    if (Splitfilename[3] == "HP")
                    {
                       caseno = instcode + "/" + divcode + "/" + year + "/" + deptcode + "/" + no + "/" + random;

                    }
                    else
                    {
                        caseno = instcode + "/" + divcode + "/" + year + "/" + deptcode + "/" + no;

                    }



                    if (Splitfilename[0] == "FP")
                    {
                        instcode = Splitfilename[0];
                        string CHP = Splitfilename[1];
                        string OP = Splitfilename[2];
                        string shortname = Splitfilename[3];
                        no = Splitfilename[4];
                        year = Splitfilename[5];
                        string date = Splitfilename[6];


                        caseno = instcode + "/" + CHP + "/" + OP + "/" + shortname + "/" + no + "/" + year + "/" + date;

                    }
                    btn_pdf.Visible = true;
                    hdn_caseno.Value = caseno;
                    hdn_hash.Value = hash;

                    lblMsg.Text = "<div class='alert alert-success' role='alert'>"
                        + "This evidence is verified by <b>"
                                        + Session["inst_code"].ToString() + "</b>"
                                         + "<BR/>Case No(Click on to CaseNo): <b><a href ='Timeline.aspx?caseno=" + caseno + "' class='alert-link'>" + caseno + "</a></b>"
                                          + "<BR/> Calculated hash : <b>"
                                         + hash
                                        + "</b></div>";
                    lblError.Text = "";
                }
                else
                {
                    lblMsg.Text = "";
                    lblError.Text = "<div class='alert alert-danger' role='alert'>*This file is not match in our records.</div>";
                    btn_pdf.Visible = false;
                }

            }
        }
        else
        {
            lblMsg.Text = "";
            lblError.Text = "<div class='alert alert-danger' role='alert'>*Please select file to verify.</div>";
        }
    }


    protected void btn_pdf_Click(object sender, EventArgs e)
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

        string firstname_assignzip = "";
        string lastname_assignzip = "";
        string username_assignzip = "";


        string assigntozip = "";
        string assignbyzip = "";
        string dbstatuszip = "";
        string firstnamezip = "";
        string lastnamezip = "";
        string usernamezip = "";
        string refrenceno = "";
        string agencyname = "";
        string createddate = "";

        string converted = "";


        string res = fl.GetCertiByHash(hdn_hash.Value);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                createddate = dt.Rows[0]["createddate"].ToString();

                converted = FlureeCS.Epoch.AddMilliseconds(
                       Convert.ToInt64(createddate)).ToString("dd-MMM-yyyy HH:mm:ss");

            }
        }
        string resEvidence = fl.GetUserAcceptanceDetails(hdn_caseno.Value);
        if (!resEvidence.StartsWith("Error"))
        {

            DataTable dtevidence = fl.Tabulate(resEvidence);
            if (dtevidence.Rows.Count > 0)
            {
                agencyname = dtevidence.Rows[0]["agencyname"].ToString();
                refrenceno = dtevidence.Rows[0]["agencyreferanceno"].ToString();

            }
        }

        string resTrack = fl.GetPDFTrekDetails(hdn_caseno.Value);

        if (!resTrack.StartsWith("Error"))
        {

            DataTable dttrack = fl.Tabulate(resTrack);
            if (dttrack.Rows.Count > 0)
            {

                for (int i = 0; i < dttrack.Rows.Count; i++)
                {

                    assigntozip = dttrack.Rows[i]["assignto"].ToString();
                    dbstatuszip = dttrack.Rows[i]["status"].ToString();
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

                }

            }
        }


        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            //PdfWriter.GetInstance(document, new FileStream(document, FileMode.Create));

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
            document.Add(logo2);
            var normalFont2 = FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.ITALIC);
            var boldFont2 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);



            string text = "Case Details of:- " + hdn_caseno.Value;
            Paragraph paragraph = new Paragraph();
            paragraph.SpacingBefore = 20;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 13, Font.BOLD));
            paragraph.Add(text);
            document.Add(paragraph);



            Paragraph para0 = new Paragraph();
            Chunk chunk0 = new Chunk(new VerticalPositionMark());
            Phrase ph0 = new Phrase();
            ph0.Add(new Chunk(Environment.NewLine));
            Paragraph para01 = new Paragraph();
            ph0.Add(new Chunk("Agency Name: " + agencyname));
            ph0.Add(chunk0); // Here I add special chunk to the same phrase.    
            para01.Add(ph0);
            document.Add(para01);


            Paragraph para = new Paragraph();
            Chunk chunk2 = new Chunk(new VerticalPositionMark());
            Phrase ph1 = new Phrase();
            ph1.Add(new Chunk(Environment.NewLine));
            Paragraph main = new Paragraph();
            ph1.Add(new Chunk("Reference No: " + refrenceno)); // Here I add projectname as a chunk into Phrase.    
            ph1.Add(chunk2); // Here I add special chunk to the same phrase.
            main.Add(ph1);
            para.Add(main);
            document.Add(para);

            Paragraph para05 = new Paragraph();
            Chunk chunk05 = new Chunk(new VerticalPositionMark());
            Phrase ph05 = new Phrase();
            ph05.Add(new Chunk(Environment.NewLine));
            Paragraph main05 = new Paragraph();
            ph05.Add(new Chunk("Case Assign By: " + firstname_assignzip + " " + lastname_assignzip + " (" + username_assignzip + ")")); // Here I add projectname as a chunk into Phrase.    
            ph05.Add(chunk05); // Here I add special chunk to the same phrase.
            main05.Add(ph05);
            para05.Add(main05);
            document.Add(para05);


            Paragraph para06 = new Paragraph();
            Chunk chunk06 = new Chunk(new VerticalPositionMark());
            Phrase ph06 = new Phrase();
            ph06.Add(new Chunk(Environment.NewLine));
            Paragraph main06 = new Paragraph();
            ph06.Add(new Chunk("Report Prepared By: " + firstnamezip + " " + lastnamezip + "(" + usernamezip + ")"));
            ph06.Add(chunk06); // Here I add special chunk to the same phrase.
            main06.Add(ph06);
            para06.Add(main06);
            document.Add(para06);


            Paragraph para2 = new Paragraph();
            Chunk chunk02 = new Chunk(new VerticalPositionMark());
            Phrase ph01 = new Phrase();
            ph01.Add(new Chunk(Environment.NewLine));
            Paragraph main0 = new Paragraph();
            ph01.Add(new Chunk("File Hash is same at the time of Uploaded and Downloaded: ")); // Here I add projectname as a chunk into Phrase.    
            ph01.Add(chunk02); // Here I add special chunk to the same phrase.
            ph01.Add(new Chunk(hdn_hash.Value));
            main0.Add(ph01);
            para2.Add(main0);
            document.Add(para2);

            Paragraph para3 = new Paragraph();
            Chunk chunk03 = new Chunk(new VerticalPositionMark());
            Phrase ph03 = new Phrase();
            ph03.Add(new Chunk(Environment.NewLine));
            Paragraph main3 = new Paragraph();
            ph03.Add(new Chunk("File Uploaded On: " + converted)); // Here I add projectname as a chunk into Phrase.    
            ph03.Add(chunk03); // Here I add special chunk to the same phrase.
            main3.Add(ph03);
            para3.Add(main3);
            document.Add(para3);

           
            //Paragraph para04 = new Paragraph();
            //Chunk chunk04 = new Chunk(new VerticalPositionMark());
            //Phrase ph04 = new Phrase();
            //ph04.Add(new Chunk(Environment.NewLine));
            //Paragraph main04 = new Paragraph();
            //ph04.Add(new Chunk()); // Here I add projectname as a chunk into Phrase.    
            //ph04.Add(chunk04); // Here I add special chunk to the same phrase.
            //ph04.Add(new Chunk("dd.MM.yyyy HH: mm:ss"));
            //main04.Add(ph04);
            //para04.Add(main04);
            //document.Add(para04);
            document.Close();



            // FileStream fs = File.OpenRead(dirdb);
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Report.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
            //string hash = fl.SHA256CheckSum(fs);

            //fs.Close();

        }

    }
}