using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {

                //txtYear.InnerText = DateTime.Today.ToString("yyyy");
                if (Session["role"] != null && (Session["role"].ToString() == "Admin" ||
                   Session["role"].ToString() == "SuperAdmin" ||
                   Session["role"].ToString() == "Assistant Director" ||
                    Session["role"].ToString() == "Additional Director" ||
                    Session["role"].ToString() == "Deputy Director"))
                {
                    DFSDirector_Title.InnerText = "All Departments Details - " + DateTime.Today.ToString("yyyy");
                    DivDirector.Visible = true;
                    DivTab.Visible = true;
                    DivHOD.Visible = false;

                }
                else if (Session["role"].ToString() == "Department Head")
                {
                    //DivUser.Visible = true;
                    DivHOD.Visible = true;
                    DivOfficer.Visible = false;
                    DivDirector.Visible = false;
                    DivTab.Visible = false;

                }
                else
                {
                    //DivUser.Visible = false;
                    DivHOD.Visible = false;
                    DivOfficer.Visible = true;
                    DivDirector.Visible = false;
                    DivTab.Visible = false;
                }
                string userid = "";

                if (Session["role"] != null && (Session["role"].ToString() == "Admin" ||
                    Session["role"].ToString() == "SuperAdmin" ||
                    Session["role"].ToString() == "Assistant Director" ||
                    Session["role"].ToString() == "Additional Director" ||
                    Session["role"].ToString() == "Deputy Director"))
                {
                    userid = "-1";

                }
                else
                {
                    userid = Session["userid"].ToString();
                }


                //if (Session["role"] != null && ( Session["role"].ToString() == "SuperAdmin"))
                //{
                //    DivDirector.Visible = true;
                //    DivTab.Visible = true;
                //    DivHOD.Visible = false;
                //}
                //else
                //{
                //    DivHOD.Visible = true;
                //    DivDirector.Visible = false;
                //    DivTab.Visible = false;
                //}

                //if (Session["role"] != null && (Session["role"].ToString() == "SuperAdmin"))
                //{
                //    userid = "-1";
                //}
                //else
                //{
                //    userid = Session["userid"].ToString();
                //}

                string SessUsername = "";
                string SessionUsername = "";
                if (Session["role"].ToString() == "Officer")
                {
                    SessUsername = Session["userid"].ToString();
                    //lblTotalCase_Officer.InnerText = "Total Case Assigned to Me";
                    string res = "";
                    res = fl.GetIndexCount(Session["dept_code"].ToString(), SessUsername);
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate("[" + res + "]");
                        if (dt.Rows.Count > 0)
                        {
                            int lblCase = 0;
                            int data = 0;
                            int var = 0;

                            int c = 0;
                            if (dt.Columns.Contains("TotalCases"))
                                c = Convert.ToInt32(dt.Rows[0]["TotalCases"].ToString());
                            //lblCases_Officer.Text = c.ToString();
                            //lblOpenCases.Text = "2";
                            c = 0;

                            //if (dt.Columns.Contains("PendingCase"))
                            //    c = Convert.ToInt32(dt.Rows[0]["PendingCase"].ToString());
                            //lblPendingCases.Text = c.ToString();
                            ////lblCases.Text = "5";
                            //c = 0;



                            if (dt.Columns.Contains("Preparation"))
                                c = Convert.ToInt32(dt.Rows[0]["Preparation"].ToString());
                            //lblPrepareCases_Officer.Text = c.ToString();
                            //lblHD.Text = "2";
                            c = 0;

                            if (dt.Columns.Contains("Completed"))
                                c = Convert.ToInt32(dt.Rows[0]["Completed"].ToString());
                            lblComplete_Officer.Text = c.ToString();
                            lblComplete_Officer1.Text = c.ToString();
                            //lblUsers.Text = "1";
                            c = 0;

                            //lblCase = Convert.ToInt32(lblCases_Officer.Text);
                            //data = Convert.ToInt32(lblPrepareCases_Officer.Text) + Convert.ToInt32(lblComplete_Officer.Text);
                            //var = lblCase - data;
                            //lblPendingCases_Officer.Text = Convert.ToString(var);
                        }
                    }
                }
                else if (Session["role"].ToString() == "Department Head")
                {
                    SessionUsername = Session["userid"].ToString();
                    //lblTotalCase_Officer.InnerText = "Total Case Assigned to Me";
                    //lblTotalCase.InnerText = "Total Cases";

                    string res = "";
                    res = fl.GetIndexCount(Session["dept_code"].ToString(), SessUsername);
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate("[" + res + "]");
                        if (dt.Rows.Count > 0)
                        {
                            int lblCase = 0;
                            int data = 0;
                            int var = 0;

                            int c = 0;
                            //if (dt.Columns.Contains("TotalCases"))
                            //    c = Convert.ToInt32(dt.Rows[0]["TotalCases"].ToString());
                            //lblCases.Text = c.ToString();
                            //lblOpenCases.Text = "2";
                            //c = 0;

                            //if (dt.Columns.Contains("PendingCase"))
                            //    c = Convert.ToInt32(dt.Rows[0]["PendingCase"].ToString());
                            //lblPendingCases.Text = c.ToString();
                            ////lblCases.Text = "5";
                            //c = 0;



                            //if (dt.Columns.Contains("Preparation"))
                            //    c = Convert.ToInt32(dt.Rows[0]["Preparation"].ToString());
                            //lblPrepareCases.Text = c.ToString();
                            //lblHD.Text = "2";
                            //c = 0;

                            if (dt.Columns.Contains("Completed"))
                                c = Convert.ToInt32(dt.Rows[0]["Completed"].ToString());
                            lblComplete.Text = c.ToString();
                            //lblUsers.Text = "1";
                            c = 0;

                            //lblCase = Convert.ToInt32(lblCases.Text);
                            //data = Convert.ToInt32(lblPrepareCases.Text) + Convert.ToInt32(lblComplete.Text);
                            //var = lblCase - data;
                            //lblPendingCases.Text = Convert.ToString(var);
                        }
                    }

                    string res_Officer = "";
                    res_Officer = fl.GetIndexCount(Session["dept_code"].ToString(), SessionUsername);
                    if (!res_Officer.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate("[" + res_Officer + "]");
                        if (dt.Rows.Count > 0)
                        {
                            int lblCase_Officer = 0;
                            int data_Officer = 0;
                            int var_Officer = 0;

                            int c = 0;
                            //if (dt.Columns.Contains("TotalCases"))
                            //    c = Convert.ToInt32(dt.Rows[0]["TotalCases"].ToString());
                            //lblCases_Officer.Text = c.ToString();
                            //lblOpenCases.Text = "2";
                            //c = 0;

                            //if (dt.Columns.Contains("PendingCase"))
                            //    c = Convert.ToInt32(dt.Rows[0]["PendingCase"].ToString());
                            //lblPendingCases.Text = c.ToString();
                            ////lblCases.Text = "5";
                            //c = 0;



                            //if (dt.Columns.Contains("Preparation"))
                            //    c = Convert.ToInt32(dt.Rows[0]["Preparation"].ToString());
                            //lblPrepareCases_Officer.Text = c.ToString();
                            //lblHD.Text = "2";
                            //c = 0;

                            if (dt.Columns.Contains("Completed"))
                                c = Convert.ToInt32(dt.Rows[0]["Completed"].ToString());
                            lblComplete_Officer.Text = c.ToString();
                            lblComplete_Officer1.Text = c.ToString();
                            //lblUsers.Text = "1";
                            c = 0;

                            //lblCase_Officer = Convert.ToInt32(lblCases_Officer.Text);
                            //data_Officer = Convert.ToInt32(lblPrepareCases_Officer.Text) + Convert.ToInt32(lblComplete_Officer.Text);
                            //var_Officer = lblCase_Officer - data_Officer;
                            //lblPendingCases_Officer.Text = Convert.ToString(var_Officer);
                        }
                    }
                }




                if (Session["role"].ToString() == "SuperAdmin")
                {
                    string resdi = fl.GetDashboardCountForSuperAdmin();
                    if (!resdi.StartsWith("Error"))
                    {
                        DataTable dt_direc = fl.Tabulate("[" + resdi + "]");
                        if (dt_direc.Rows.Count > 0)
                        {

                            int c = 0;
                            if (dt_direc.Columns.Contains("Departments"))
                                c = Convert.ToInt32(dt_direc.Rows[0]["Departments"].ToString());
                            lblDepartment.Text = c.ToString();
                            c = 0;

                            if (dt_direc.Columns.Contains("User"))
                                c = Convert.ToInt32(dt_direc.Rows[0]["User"].ToString());
                            lblTotalUsers.Text = c.ToString();
                            c = 0;


                        }
                    }
                }
                //string User = "";
                //if (Session["role"].ToString() == "Officer")
                //{
                //    User = Session["userid"].ToString();
                //}
                //else
                //{
                //    User = "";
                //}
                //string res_graph = fl.GetGraphData(Session["inst_code"].ToString(),
                //      Session["dept_code"].ToString(), User);
                //if (!res_graph.StartsWith("Error"))
                //{
                //    DataTable dt_graph = fl.Tabulate(res_graph);
                //    if (dt_graph.Rows.Count > 0)
                //    {

                //        string CaseNo = "";
                //        string Date = "";
                //        string UpdatedDate = "";
                //        int Jan = 0;
                //        int Feb = 0;
                //        int Mar = 0;
                //        int Apr = 0;
                //        int May = 0;
                //        int Jun = 0;
                //        int Jul = 0;
                //        int Aug = 0;
                //        int Sep = 0;
                //        int Oct = 0;
                //        int Nov = 0;
                //        int Dec = 0;
                //        int Count = 0;
                //        for (int j = 0; j < dt_graph.Rows.Count; j++)
                //        {
                //            CaseNo = dt_graph.Rows[j]["caseno"].ToString();

                //            Date = dt_graph.Rows[j]["updateddate"].ToString();
                //            UpdatedDate = FlureeCS.Epoch.AddMilliseconds(
                //                              Convert.ToInt64(Date)).ToString("dd-MM-yyyy HH:mm:ss");
                //            string[] split = UpdatedDate.Split('-');
                //            string Month = split[1];
                //            string Year = split[2];
                //            string Merge = Year + "-" + Month;
                //            if (Merge == Year + "-" + "01")
                //            {
                //                Jan = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "02")
                //            {
                //                Feb = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "03")
                //            {
                //                Mar = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "04")
                //            {
                //                Apr = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "05")
                //            {
                //                May = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "06")
                //            {
                //                Jun = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "07")
                //            {
                //                Jul = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "08")
                //            {
                //                Aug = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "09")
                //            {
                //                Sep = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "10")
                //            {
                //                Oct = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "11")
                //            {
                //                Nov = Count + 1;
                //            }
                //            if (Merge == Year + "-" + "12")
                //            {
                //                Dec = Count + 1;
                //            }


                //        }
                //this.graph_data.InnerText = "";
                //this.graph_data.InnerText += "<input type='hidden' id='Hdn_Jan' name='Hdn_Jan' value='" + Convert.ToString(Jan) + "'/>"
                //                       + "< input type='hidden' id='Hdn_Feb' name='Hdn_Feb' value='" + Convert.ToString(Feb) + "'/>"
                //                       + "<input type='hidden' id='Hdn_Mar' name='Hdn_Mar' value='" + Convert.ToString(Mar) + "'/>"
                //                       + "<input type='hidden' id='Hdn_Apr' name='Hdn_Apr' value='" + Convert.ToString(Apr) + "'/>"
                //                       + "<input type='hidden' id='Hdn_May' name='Hdn_May' value='" + Convert.ToString(May) + "'/>"
                //                       + "<input type='hidden' id='Hdn_Jun' name='Hdn_Jun' value='" + Convert.ToString(Jun) + "'/> "
                //                       + " <input type='hidden' id='Hdn_Jul' name='Hdn_Jul' value='" + Convert.ToString(Jul) + "'/> "
                //                       + " < input type='hidden' id='Hdn_Aug' name='Hdn_Aug' value='" + Convert.ToString(Aug) + "'/> "
                //                       + "<input type='hidden' id='Hdn_Sep' name='Hdn_Sep' value='" + Convert.ToString(Sep) + "'/> "
                //                       + "<input type='hidden' id='Hdn_Oct' name='Hdn_Oct' value='" + Convert.ToString(Oct) + "'/> "
                //                       + "<input type='hidden' id='Hdn_Nov' name='Hdn_Nov' value='" + Convert.ToString(Nov) + "'/> "
                //                       + "<input type='hidden' id='Hdn_Dec' name='Hdn_Dec' value='" + Convert.ToString(Dec) + "'/> ";
                //string Hdn_Jan = String.Format("{0}", Request.Form["Hdn_Jan"]);
                //string Hdn_Feb = String.Format("{0}", Request.Form["Hdn_Feb"]);
                //string Hdn_Mar = String.Format("{0}", Request.Form["Hdn_Mar"]);
                //string Hdn_Apr = String.Format("{0}", Request.Form["Hdn_Apr"]);
                //string Hdn_May = String.Format("{0}", Request.Form["Hdn_May"]);
                //string Hdn_Jun = String.Format("{0}", Request.Form["Hdn_Jun"]);
                //string Hdn_Jul = String.Format("{0}", Request.Form["Hdn_Jul"]);
                //string Hdn_Aug = String.Format("{0}", Request.Form["Hdn_Aug"]);
                //string Hdn_Sep = String.Format("{0}", Request.Form["Hdn_Sep"]);
                //string Hdn_Oct = String.Format("{0}", Request.Form["Hdn_Oct"]);
                //string Hdn_Nov = String.Format("{0}", Request.Form["Hdn_Nov"]);
                //string Hdn_Dec = String.Format("{0}", Request.Form["Hdn_Dec"]);

                //Hdn_Jan = Convert.ToString(Jan);
                //Hdn_Feb = Convert.ToString(Feb);
                //Hdn_Mar = Convert.ToString(Mar);
                //Hdn_Apr = Convert.ToString(Apr);
                //Hdn_May = Convert.ToString(May);
                //Hdn_Jun = Convert.ToString(Jun);
                //Hdn_Jul = Convert.ToString(Jul);
                //Hdn_Aug = Convert.ToString(Aug);
                //Hdn_Sep = Convert.ToString(Sep);
                //Hdn_Oct = Convert.ToString(Oct);
                //Hdn_Nov = Convert.ToString(Nov);
                //Hdn_Dec = Convert.ToString(Dec);

                //Hdn_Jan = "0";
                //Hdn_Feb = "0";
                //Hdn_Mar = "0";
                //Hdn_Apr = "0";
                //Hdn_May = "7";
                //Hdn_Jun = "0";
                //Hdn_Jul = "0";
                //Hdn_Aug = "0";
                //Hdn_Sep = "0";
                //Hdn_Oct = "0";
                //Hdn_Nov = "0";
                //Hdn_Dec = "0";
                //    }
                //}

                if (Session["role"].ToString() == "Admin" ||
                    Session["role"].ToString() == "Assistant Director" ||
                    Session["role"].ToString() == "Additional Director" ||
                    Session["role"].ToString() == "Deputy Director")
                {

                    string resdire = fl.GetDashboardCountForDirector(Session["inst_id"].ToString());
                    if (!resdire.StartsWith("Error"))
                    {
                        DataTable dt_direc = fl.Tabulate("[" + resdire + "]");
                        if (dt_direc.Rows.Count > 0)
                        {

                            int c = 0;
                            if (dt_direc.Columns.Contains("Departments"))
                                c = Convert.ToInt32(dt_direc.Rows[0]["Departments"].ToString());
                            lblDepartment.Text = c.ToString();
                            c = 0;

                            if (dt_direc.Columns.Contains("User"))
                                c = Convert.ToInt32(dt_direc.Rows[0]["User"].ToString());
                            lblTotalUsers.Text = c.ToString();
                            c = 0;


                        }
                    }
                }
                string resdetails = "";
                if (Session["role"].ToString() == "Admin" ||
                    Session["role"].ToString() == "Assistant Director" ||
                    Session["role"].ToString() == "Additional Director" ||
                    Session["role"].ToString() == "Deputy Director")
                {
                    resdetails = fl.GetDeptById(Session["inst_id"].ToString());

                }
                else
                {
                    resdetails = fl.GetDeptByIdForSuperadmin();
                }

                if (!resdetails.StartsWith("Error"))
                {
                    DataTable dt_data = fl.Tabulate(resdetails);
                    if (dt_data.Rows.Count > 0 || dt_data.Rows.Count > 1)
                    {
                        double CurrentDate = 0;
                        string Year = DateTime.Today.ToString("yyyy");
                        string Date = "01-01-" + Year;
                        DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);
                        string dateString = firstDayOfYear.ToString("yyyy-MM-dd");
                        txt_FromDate.Text = dateString;


                        CurrentDate = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(Date + " 00:00:00"));

                        DateTime currentDate_fortextbox = DateTime.Now;
                        string dateString_fortextbox = currentDate_fortextbox.ToString("yyyy-MM-dd");

                        txt_ToDate.Text = dateString_fortextbox;


                        string lblDeptName = "";
                        this.DivTable.InnerHtml = "";
                        int Assigned = 0;
                        int Pending = 0;
                        int Preparation = 0;
                        int Signature = 0;
                        int Submitted = 0;
                        int TotalCases = 0;
                        int AvgTotal = 0;
                        int AvgPending = 0;
                        int AvgSubmitted = 0;
                        string DeptCode = "";
                        string Deptname = "";
                        if (dt_data.Rows.Count == 1)
                        {
                            DeptCode = dt_data.Rows[0]["dept_code"].ToString();
                            Deptname = dt_data.Rows[0]["dept_name"].ToString();


                            //if (Deptname != "ALL Department - Junagadh")
                            if (Deptname != "ALL Department - Ahmedabad" || Deptname != "ALL Department - Baroda" ||
                        Deptname != "ALL Department" || Deptname != "ALL Department - Junagadh" ||
                        Deptname != "ALL Department - Rajkot" || Deptname != "ALL Department - Surat")
                            {
                                if (Deptname != "Other Sample Warden")
                                {

                                    string resdepcount = fl.GetAllDeptCodeWiseCount(DeptCode,
                                        Session["inst_code"].ToString(), CurrentDate.ToString(), "", "");
                                    if (!resdepcount.StartsWith("Error"))
                                    {
                                        DataTable dt_depcount = fl.Tabulate("[" + resdepcount + "]");
                                        if (dt_depcount.Rows.Count > 0)
                                        {
                                            lblDeptName = Deptname;

                                            if (dt_depcount.Columns.Contains("TotalCases"))
                                                TotalCases = Convert.ToInt32(dt_depcount.Rows[0]["TotalCases"].ToString());

                                            if (dt_depcount.Columns.Contains("Submitted"))
                                                Submitted = Convert.ToInt32(dt_depcount.Rows[0]["Submitted"].ToString());
                                        }
                                    }

                                    Pending = TotalCases - Submitted;
                                    this.DivTable.InnerHtml += "<tr>";
                                    this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                                    //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + TotalCases + "</td>";
                                    //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Pending + "</td>";
                                    this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Submitted + "</td>";
                                    this.DivTable.InnerHtml += "</tr>";
                                    AvgTotal += Convert.ToInt32(TotalCases);
                                    AvgPending += Convert.ToInt32(Pending);
                                    AvgSubmitted += Convert.ToInt32(Submitted);

                                }
                            }
                            else
                            {
                                this.DivTable.InnerHtml += "<tr style='text-align:center;'>";
                                this.DivTable.InnerHtml += "<td colspan='6' ><h4>No records Found.</h4></td>";
                                this.DivTable.InnerHtml += "</tr>";
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt_data.Rows.Count; i++)
                            {

                                DeptCode = dt_data.Rows[i]["dept_code"].ToString();
                                Deptname = dt_data.Rows[i]["dept_name"].ToString();

                                if ((Deptname != "ALL Department - Ahmedabad") || (Deptname != "ALL Department - Baroda") ||
                                    (Deptname != "ALL Department") || (Deptname != "ALL Department - Junagadh") ||
                                    (Deptname != "ALL Department - Rajkot") || (Deptname != "ALL Department - Surat"))
                                {
                                    if (Deptname != "Other Sample Warden")
                                    {
                                        string resdepcount = fl.GetAllDeptCodeWiseCount(DeptCode,
                                            Session["inst_code"].ToString(), CurrentDate.ToString(), "", "");
                                        if (!resdepcount.StartsWith("Error"))
                                        {
                                            DataTable dt_depcount = fl.Tabulate("[" + resdepcount + "]");
                                            if (dt_depcount.Rows.Count > 0)
                                            {
                                                lblDeptName = Deptname;

                                                //if (dt_depcount.Columns.Contains("Assigned"))
                                                //    Assigned = Convert.ToInt32(dt_depcount.Rows[0]["Assigned"].ToString());
                                                if (dt_depcount.Columns.Contains("TotalCases"))
                                                    TotalCases = Convert.ToInt32(dt_depcount.Rows[0]["TotalCases"].ToString());

                                                //if (dt_depcount.Columns.Contains("Pending"))
                                                //    Pending = Convert.ToInt32(dt_depcount.Rows[0]["Pending"].ToString());

                                                //if (dt_depcount.Columns.Contains("Preparation"))
                                                //    Preparation = Convert.ToInt32(dt_depcount.Rows[0]["Preparation"].ToString());

                                                //if (dt_depcount.Columns.Contains("Signature"))
                                                //    Signature = Convert.ToInt32(dt_depcount.Rows[0]["Signature"].ToString());

                                                if (dt_depcount.Columns.Contains("Submitted"))
                                                    Submitted = Convert.ToInt32(dt_depcount.Rows[0]["Submitted"].ToString());
                                            }
                                        }
                                        //this.DivTable.InnerHtml += "<tr>";
                                        //this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                                        //this.DivTable.InnerHtml += "<td>" + Assigned + "</td>";
                                        //this.DivTable.InnerHtml += "<td>" + Pending + "</td>";
                                        //this.DivTable.InnerHtml += "<td>" + Preparation + "</td>";
                                        //this.DivTable.InnerHtml += "<td>" + Signature + "</td>";
                                        //this.DivTable.InnerHtml += "<td>" + Submitted + "</td>";
                                        //this.DivTable.InnerHtml += "</tr>";
                                        Pending = TotalCases - Submitted;
                                        this.DivTable.InnerHtml += "<tr>";
                                        this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                                        //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + TotalCases + "</td>";
                                        //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Pending + "</td>";
                                        this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Submitted + "</td>";
                                        this.DivTable.InnerHtml += "</tr>";
                                        AvgTotal += Convert.ToInt32(TotalCases);
                                        AvgPending += Convert.ToInt32(Pending);
                                        AvgSubmitted += Convert.ToInt32(Submitted);

                                    }
                                }
                                else
                                {
                                    this.DivTable.InnerHtml += "<tr style='text-align:center;'>";
                                    this.DivTable.InnerHtml += "<td colspan='6' ><h4>No records Found.</h4></td>";
                                    this.DivTable.InnerHtml += "</tr>";
                                }
                            }
                        }
                        //lblAvgTotalCases.InnerText = Convert.ToString(AvgTotal);
                        //lblAvgPendingCases.InnerText = Convert.ToString(AvgPending);
                        lblAvgCompletedCases.InnerText = Convert.ToString(AvgSubmitted);

                    }
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
        //if (Session["userid"] != null)
        //{
        double fd = 0;
        double td = 0;

        string resdetails = "";
        if (Session["role"].ToString() == "Admin" ||
            Session["role"].ToString() == "Assistant Director" ||
            Session["role"].ToString() == "Additional Director" ||
            Session["role"].ToString() == "Deputy Director")
        {
            resdetails = fl.GetDeptById(Session["inst_id"].ToString());
        }
        else
        {
            resdetails = fl.GetDeptByIdForSuperadmin();
        }

        if (!resdetails.StartsWith("Error"))
        {
            DataTable dt_data = fl.Tabulate(resdetails);
            if (dt_data.Rows.Count > 0)
            {
                fd = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_FromDate.Text + " 00:00:00"));
                td = fl.ConvertDateTimeToTimestamp(Convert.ToDateTime(txt_ToDate.Text + " 23:59:59"));
                title.InnerText = "All Department data between " + txt_FromDate.Text + " to " + txt_ToDate.Text;

                string lblDeptName = "";
                this.DivTable.InnerHtml = "";
                int Assigned = 0;
                int Pending = 0;
                int Preparation = 0;
                int Signature = 0;
                int Submitted = 0;
                int TotalCases = 0;
                int AvgTotal = 0;
                int AvgPending = 0;
                int AvgSubmitted = 0;
                string DeptCode = "";
                string Deptname = "";
                if (dt_data.Rows.Count == 1)
                {
                    DeptCode = dt_data.Rows[0]["dept_code"].ToString();
                    Deptname = dt_data.Rows[0]["dept_name"].ToString();


                    //if (Deptname != "ALL Department - Junagadh")
                    if (Deptname != "ALL Department - Ahmedabad" || Deptname != "ALL Department - Baroda" ||
                Deptname != "ALL Department" || Deptname != "ALL Department - Junagadh" ||
                Deptname != "ALL Department - Rajkot" || Deptname != "ALL Department - Surat")
                    {
                        if (Deptname != "Other Sample Warden")
                        {

                            string resdepcount = fl.GetAllDeptCodeWiseCount(DeptCode,
                                Session["inst_code"].ToString(), "", fd.ToString(), td.ToString());
                            if (!resdepcount.StartsWith("Error"))
                            {
                                DataTable dt_depcount = fl.Tabulate("[" + resdepcount + "]");
                                if (dt_depcount.Rows.Count > 0)
                                {
                                    lblDeptName = Deptname;

                                    if (dt_depcount.Columns.Contains("TotalCases"))
                                        TotalCases = Convert.ToInt32(dt_depcount.Rows[0]["TotalCases"].ToString());

                                    if (dt_depcount.Columns.Contains("Submitted"))
                                        Submitted = Convert.ToInt32(dt_depcount.Rows[0]["Submitted"].ToString());
                                }
                            }

                            Pending = TotalCases - Submitted;
                            this.DivTable.InnerHtml += "<tr>";
                            this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                            this.DivTable.InnerHtml += "<td style='text-align:center;'>" + TotalCases + "</td>";
                            this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Pending + "</td>";
                            this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Submitted + "</td>";
                            this.DivTable.InnerHtml += "</tr>";
                            AvgTotal += Convert.ToInt32(TotalCases);
                            AvgPending += Convert.ToInt32(Pending);
                            AvgSubmitted += Convert.ToInt32(Submitted);

                        }
                    }
                    else
                    {
                        this.DivTable.InnerHtml += "<tr style='text-align:center;'>";
                        this.DivTable.InnerHtml += "<td colspan='6' ><h4>No records Found.</h4></td>";
                        this.DivTable.InnerHtml += "</tr>";
                    }
                }
                else
                {
                    for (int i = 1; i < dt_data.Rows.Count; i++)
                    {

                        DeptCode = dt_data.Rows[i]["dept_code"].ToString();
                        Deptname = dt_data.Rows[i]["dept_name"].ToString();

                        if ((Deptname != "ALL Department - Ahmedabad") || (Deptname != "ALL Department - Baroda") ||
                            (Deptname != "ALL Department") || (Deptname != "ALL Department - Junagadh") ||
                            (Deptname != "ALL Department - Rajkot") || (Deptname != "ALL Department - Surat"))
                        {
                            if (Deptname != "Other Sample Warden")
                            {
                                string resdepcount = fl.GetAllDeptCodeWiseCount(DeptCode,
                                    Session["inst_code"].ToString(), "", fd.ToString(), td.ToString());
                                if (!resdepcount.StartsWith("Error"))
                                {
                                    DataTable dt_depcount = fl.Tabulate("[" + resdepcount + "]");
                                    if (dt_depcount.Rows.Count > 0)
                                    {
                                        lblDeptName = Deptname;

                                        //if (dt_depcount.Columns.Contains("Assigned"))
                                        //    Assigned = Convert.ToInt32(dt_depcount.Rows[0]["Assigned"].ToString());
                                        if (dt_depcount.Columns.Contains("TotalCases"))
                                            TotalCases = Convert.ToInt32(dt_depcount.Rows[0]["TotalCases"].ToString());

                                        //if (dt_depcount.Columns.Contains("Pending"))
                                        //    Pending = Convert.ToInt32(dt_depcount.Rows[0]["Pending"].ToString());

                                        //if (dt_depcount.Columns.Contains("Preparation"))
                                        //    Preparation = Convert.ToInt32(dt_depcount.Rows[0]["Preparation"].ToString());

                                        //if (dt_depcount.Columns.Contains("Signature"))
                                        //    Signature = Convert.ToInt32(dt_depcount.Rows[0]["Signature"].ToString());

                                        if (dt_depcount.Columns.Contains("Submitted"))
                                            Submitted = Convert.ToInt32(dt_depcount.Rows[0]["Submitted"].ToString());
                                    }
                                }
                                //this.DivTable.InnerHtml += "<tr>";
                                //this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                                //this.DivTable.InnerHtml += "<td>" + Assigned + "</td>";
                                //this.DivTable.InnerHtml += "<td>" + Pending + "</td>";
                                //this.DivTable.InnerHtml += "<td>" + Preparation + "</td>";
                                //this.DivTable.InnerHtml += "<td>" + Signature + "</td>";
                                //this.DivTable.InnerHtml += "<td>" + Submitted + "</td>";
                                //this.DivTable.InnerHtml += "</tr>";
                                Pending = TotalCases - Submitted;
                                this.DivTable.InnerHtml += "<tr>";
                                this.DivTable.InnerHtml += "<td><div class='table - data__info'><h6>" + lblDeptName + "</h6></td>";
                                //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + TotalCases + "</td>";
                                //this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Pending + "</td>";
                                this.DivTable.InnerHtml += "<td style='text-align:center;'>" + Submitted + "</td>";
                                this.DivTable.InnerHtml += "</tr>";
                                AvgTotal += Convert.ToInt32(TotalCases);
                                AvgPending += Convert.ToInt32(Pending);
                                AvgSubmitted += Convert.ToInt32(Submitted);

                            }
                        }
                        else
                        {
                            this.DivTable.InnerHtml += "<tr style='text-align:center;'>";
                            this.DivTable.InnerHtml += "<td colspan='6' ><h4>No records Found.</h4></td>";
                            this.DivTable.InnerHtml += "</tr>";
                        }
                    }
                }
                //lblAvgTotalCases.InnerText = Convert.ToString(AvgTotal);
                //lblAvgPendingCases.InnerText = Convert.ToString(AvgPending);
                lblAvgCompletedCases.InnerText = Convert.ToString(AvgSubmitted);

            }
            else
            {
                title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";

            }
        }
    }
    //    }  else
    //        {
    //            Response.Redirect("Login.aspx");
    //        }
}