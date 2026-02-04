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
    public void get_evideincedetails()
    {
        try
        {

            string txtcaseno = "";
            string caseno = "";
            caseno = Request.QueryString["caseno"];

            if ((caseno != "") || (caseno != null))
            {

                string[] splitcase = caseno.Split('/');
                if (txt_no.Text != "" || txt_fpnumber.Text != "")
                {
                    if (splitcase[0] == "FP" || Session["dept_code"].ToString() == "FP")
                    {
                        txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;
                    }
                    else if (splitcase[1] == "BA" || Session["dept_code"].ToString() == "BA")
                    {
                        txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;
                    }
                    else
                    {
                        txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;
                    }
                }
            }
            else
            {
                //if (txt_no.Text != "" || txt_fpnumber.Text != "")
                //{

                //}
            }


            string res = fl.GetUserAcceptanceDetails(caseno);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    string agencyreferanceno = dt.Rows[0]["agencyreferanceno"].ToString();
                    string receiptfilepath = dt.Rows[0]["receiptfilepath"].ToString();
                    string agencyname = dt.Rows[0]["agencyname"].ToString();
                    string view = "";
                    // string[] path = receiptfilepath.Split('\\');
                    // string Uploads = path[3];
                    // string DFS = path[4];
                    // string dept = path[5];
                    // string folder = path[6];
                    // string file = path[7];
                    //string viewpdf = Uploads + "\\" + DFS + "\\" + dept + "\\" + folder + "\\" + file;
                    //view += "<iframe src = '"+viewpdf+"' width = '800' height = '600'>";
                    if (receiptfilepath != "")
                    {
                        view = "Click here to <a href='" + receiptfilepath + "' target='_blank'>View PDF</a>";
                    }
                    else
                    {
                        view = "No File to View";
                    }

                    view_pdf.InnerHtml = view;
                    txt_Referenceno.Text = agencyreferanceno;
                    txt_agencyname.Text = agencyname;


                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('No records Found');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('No records Found');</script>");
            }
        }
        catch (Exception ex) { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                try
                {

                    Load();

                    string caseno = Request.QueryString["caseno"];

                    txt_year.Text = DateTime.Today.ToString("yyyy");
                    txt_fpyear.Text = DateTime.Today.ToString("yyyy");
                    if (caseno != null)
                    {
                        get_evideincedetails();
                        string[] splitcase = caseno.Split('/');


                        if (splitcase[1] == "BA" || Session["dept_code"].ToString() == "BA")
                        {
                            txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                            txt_year.Text = splitcase[2];
                            txt_no.Text = splitcase[3];
                            lbl_div.Visible = false;

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

                                    ddlDepartment.ClearSelection();

                                    ddlDepartment.Items.FindByValue(splitcase[1]).Selected = true;

                                }
                            }

                            string user = fl.GetUsers("-1", Session["inst_code"].ToString(), splitcase[1]);
                            if (!user.StartsWith("Error"))
                            {
                                DataTable dt = fl.Tabulate(user);
                                if (dt.Rows.Count > 0)
                                {
                                    ddlUser.DataSource = dt;
                                    ddlUser.DataTextField = "firstname" + "lastname";
                                    //+ " " + "lastname";
                                    ddlUser.DataValueField = "userid";
                                    ddlUser.DataBind();

                                }
                            }
                            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
                        }
                        else if (splitcase[0] == "FP" || Session["dept_code"].ToString() == "FP")
                        {
                            txt_fp.Text = splitcase[0] + "/" + splitcase[1] + "/" + splitcase[2];
                            txt_shortname.Text = splitcase[3];
                            txt_fpnumber.Text = splitcase[4];
                            txt_fpyear.Text = splitcase[5];
                            txt_fpdate.Text = splitcase[6];
                            txt_shortname.Attributes.Add("readonly", "readonly");
                            div_fp.Visible = true;
                            div_normal.Visible = false;

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

                                    ddlDepartment.ClearSelection();

                                    ddlDepartment.Items.FindByValue(splitcase[0]).Selected = true;

                                }
                            }

                            string user = fl.GetUsers("-1", Session["inst_code"].ToString(), splitcase[0]);
                            if (!user.StartsWith("Error"))
                            {
                                DataTable dt = fl.Tabulate(user);
                                if (dt.Rows.Count > 0)
                                {
                                    ddlUser.DataSource = dt;
                                    ddlUser.DataTextField = "firstname";
                                    //+ " " + "lastname";
                                    ddlUser.DataValueField = "userid";
                                    ddlUser.DataBind();

                                }



                                // Assuming you have a dropdown list control named "ddlCombinedValues" in your ASP.NET markup.

                                // Create a list of combined values
                                //List<string[]> combinedValues = new List<string[]>
                                //{
                                //  new string[] { "Value1", "Description1" },
                                //  new string[] { "Value2", "Description2" },
                                //   new string[] { "Value3", "Description3" }
                                //  };

                                //// Iterate through the combined values and add them to the dropdown list
                                //foreach (string[] combinedValue in combinedValues)
                                //{
                                //    string value = combinedValue[0]; // The first column value
                                //    string description = combinedValue[1]; // The second column value

                                //    // Create a ListItem with the combined value
                                //    ListItem listItem = new ListItem(value + description, value);

                                //    // Add the ListItem to the dropdown list
                                //    ddlUser.Items.Add(listItem);
                                //}




                            }
                            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
                        }
                        //else if (splitcase[3] == "HPB" || Session["dept_code"].ToString() == "HPB")
                        //{
                        //    txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                        //    txt_year.Text = splitcase[2];
                        //    txt_div.Text = splitcase[3] + "/" + splitcase[4];
                        //    txt_no.Text = splitcase[5];
                        //    txt_div.Attributes.Add("readonly", "readonly");
                        //    txt_dfsee.Attributes.Add("readonly", "readonly");

                        //    string res = fl.GetDeptById(Session["inst_id"].ToString());
                        //    if (!res.StartsWith("Error"))
                        //    {
                        //        DataTable dt = fl.Tabulate(res);
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            ddlDepartment.DataSource = dt;
                        //            ddlDepartment.DataTextField = "dept_name";
                        //            ddlDepartment.DataValueField = "dept_code";
                        //            ddlDepartment.DataBind();
                        //            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

                        //            ddlDepartment.ClearSelection();

                        //            ddlDepartment.Items.FindByValue(splitcase[3]).Selected = true;

                        //        }
                        //    }

                        //    string user = fl.GetUsers("-1", Session["inst_code"].ToString(), splitcase[3]);
                        //    if (!user.StartsWith("Error"))
                        //    {
                        //        DataTable dt = fl.Tabulate(user);
                        //        if (dt.Rows.Count > 0)
                        //        {
                        //            ddlUser.DataSource = dt;
                        //            ddlUser.DataTextField = "firstname";
                        //            //+ " " + "lastname";
                        //            ddlUser.DataValueField = "userid";
                        //            ddlUser.DataBind();

                        //        }
                        //    }
                        //    ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));

                        //}
                        else
                        {
                            txt_dfsee.Text = splitcase[0] + "/" + splitcase[1];
                            txt_year.Text = splitcase[2];
                            txt_div.Text = splitcase[3];
                            txt_no.Text = splitcase[4];
                            txt_div.Attributes.Add("readonly", "readonly");

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

                                    ddlDepartment.ClearSelection();
                                    if (splitcase[3] == "PSY" || splitcase[3] == "LVA" || splitcase[3] == "SDS" || splitcase[3] == "NARCO" || splitcase[3] == "BEOS" || splitcase[3] == "P.ASSESSMENT")
                                    {
                                        ddlDepartment.Items.FindByValue("PSY").Selected = true;
                                    }
                                    else
                                    {
                                        ddlDepartment.Items.FindByValue(splitcase[3]).Selected = true;
                                    }


                                }
                            }
                            string user = "";
                            if (splitcase[3] == "PSY" || splitcase[3] == "LVA" || splitcase[3] == "SDS" || splitcase[3] == "NARCO" || splitcase[3] == "BEOS" || splitcase[3] == "P.ASSESSMENT")
                            {
                                user = fl.GetUsers("-1", Session["inst_code"].ToString(), "PSY");
                            }
                            else
                            {
                                user = fl.GetUsers("-1", Session["inst_code"].ToString(), splitcase[3]);
                            }

                            if (!user.StartsWith("Error"))
                            {
                                DataTable dt = fl.Tabulate(user);
                                if (dt.Rows.Count > 0)
                                {
                                    ddlUser.DataSource = dt;
                                    ddlUser.DataTextField = "firstname";
                                    //+ " " + "lastname";
                                    ddlUser.DataValueField = "userid";
                                    ddlUser.DataBind();

                                }
                            }
                            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
                        }

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
                            // txt_div.Attributes.Add("readonly", "readonly");
                        }
                        else
                        {
                            txt_div.Text = Session["dept_code"].ToString();
                            txt_div.Attributes.Add("readonly", "readonly");
                        }
                    }

                }
                catch (Exception ex) { }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }


    public void filldepartment()
    {
        if (Session["role"].ToString() == "Admin" ||
                        Session["role"].ToString() == "Assistant Director" ||
                        Session["role"].ToString() == "Additional Director" ||
                        Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin")
        {
            div_department.Visible = true;
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
    }

    public void Load()
    {
        //string code = "";

        //code = Session["dept_code"].ToString();

        //string caseno = fl.GetCaseNo("-1", code);
        //if (!caseno.StartsWith("Error"))
        //{
        //    DataTable dtcaseno = fl.Tabulate(caseno);
        //    if (dtcaseno.Rows.Count > 0)
        //    {
        //        ddlCaseNo.DataSource = dtcaseno;
        //        ddlCaseNo.DataTextField = "caseno";
        //        ddlCaseNo.DataValueField = "caseno";
        //        ddlCaseNo.DataBind();

        //    }
        //}
        //ddlCaseNo.Items.Insert(0, new ListItem("-- Select Case No --", "-1"));


        string user = fl.GetUsers("-1", Session["inst_code"].ToString(), Session["div_code"].ToString());
        if (!user.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(user);

            if (dt.Rows.Count > 0)
            {
                ddlUser.DataSource = dt;
                ddlUser.DataTextField = "firstname";
                //+ " " + "lastname";
                ddlUser.DataValueField = "userid";
                ddlUser.DataBind();

            }
        }
        ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string SessionDivCode = "";
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
            string[] caseno = txtcaseno.Split('/');
            string spliteddivcode = caseno[3];
            string dept_code = "";
            if (Session["role"].ToString() == "Admin" ||
                      Session["role"].ToString() == "Assistant Director" ||
                      Session["role"].ToString() == "Additional Director" ||
                      Session["role"].ToString() == "Deputy Director" || Session["role"].ToString() == "SuperAdmin" || Session["role"].ToString() == "Department Head")
            {
                if (caseno[1] == "BA")
                {
                    SessionDivCode = "BA";
                }
                else if (caseno[0] == "FP")
                {
                    SessionDivCode = caseno[0];
                }
                else
                {
                    SessionDivCode = spliteddivcode;
                }

            }
            else
            {
                SessionDivCode = Session["div_code"].ToString();
            }
            if (caseno[3] == "BEOS" || caseno[3] == "LVA" || caseno[3] == "SDS" || caseno[3] == "NARCO" || caseno[3] == "PSY" || caseno[3] == "P.Assessment")
            {
                dept_code = "PSY";
            }
            else if (caseno[1] == "BA")
            {
                dept_code = "BA";
            }
            else if (caseno[0] == "FP")
            {
                dept_code = caseno[0];
            }
            else
            {
                dept_code = caseno[3];
            }
            //string _id = "";
            string statuschangeby = Session["firstname"].ToString() + " " + Session["lastname"].ToString();
            string res = fl.InsertCaseAssign(txtcaseno, txtRefBy.Text, ddlUser.SelectedValue,
                txtNote.Text, SessionDivCode, dept_code);
            string resTrack = fl.InsertTrack(txtcaseno, "Assigned", Session["username"].ToString(), ddlUser.SelectedValue, txtNote.Text, statuschangeby);
            //string rescased = fl.GetCaseuserwise(txtcaseno, "");
            //if (!rescased.StartsWith("Error"))
            //{
            //    DataTable dtcased = fl.Tabulate(rescased);
            //    if (dtcased.Rows.Count > 0)
            //    {
            //       _id = dtcased.Rows[0]["_id"].ToString();
            //    }
            //}
            string resupdate = fl.UpdateUserAcceptanceStatus(txtcaseno, "Assigned", "", ddlUser.SelectedValue);
            if (!res.StartsWith("Error") && !resTrack.StartsWith("Error") && !resupdate.StartsWith("Error"))
            {
                DataTable dtdata = fl.Tabulate("[" + res + "]");
                if (dtdata.Rows.Count > 0)
                {
                    if (dtdata.Rows[0]["status"].ToString() == "200")
                    {
                        //count++;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Case Assigned.');window.location='AssignCase.aspx';</script>");
                        //Response.Redirect("AssignCase.aspx");
                        txtRefBy.Text = "";
                        txtNote.Text = "";
                        txt_Referenceno.Text = "";
                        txt_agencyname.Text = "";


                        Load();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('Case Not Assigned. Please Try Again Later..!!');</script>");
            }
        }
        catch (Exception ex) { }
    }

    protected void ddlCaseNo_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

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

        string userdept = fl.GetUsersDeptcodewise("-1", ddlDepartment.SelectedValue);
        if (!userdept.StartsWith("Error"))
        {
            DataTable deptuser = fl.Tabulate(userdept);
            if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "SuperAdmin" ||
                        Session["role"].ToString() == "Assistant Director" ||
                        Session["role"].ToString() == "Additional Director" ||
                        Session["role"].ToString() == "Deputy Director")
            {
                if (deptuser.Rows.Count > 0)
                {
                    ddlUser.DataSource = deptuser;
                    ddlUser.DataTextField = "firstname";
                    //+ " " + "lastname";
                    ddlUser.DataValueField = "userid";
                    ddlUser.DataBind();


                }
            }

        }
        ddlUser.Items.Insert(0, new ListItem("-- Select Users --", "-1"));


        //if (ddlDepartment.SelectedIndex != 0)
        //{
        //    string res = fl.GetCaseNo("-1", ddlDepartment.SelectedValue);
        //    if (!res.StartsWith("Error"))
        //    {
        //        DataTable dt = fl.Tabulate(res);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlCaseNo.DataSource = dt;
        //            ddlCaseNo.DataTextField = "caseno";
        //            ddlCaseNo.DataValueField = "caseno";
        //            ddlCaseNo.DataBind();
        //            ddlCaseNo.Items.Insert(0, new ListItem("-- Select CaseNo --", "-1"));
        //        }
        //    }
        //}
    }

    protected void txt_no_TextChanged(object sender, EventArgs e)
    {
        string txtcaseno = "";
        string session = Session["dept_code"].ToString();
        Response.Write("<script>('" + session + "')</script>");

        if (txt_dfsee.Text == "BA" || Session["dept_code"].ToString() == "BA")
        {
            txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_no.Text;

            lbl_div.Visible = false;

            string res1 = fl.GetDeptById(Session["inst_id"].ToString());
            if (!res1.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res1);
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataTextField = "dept_name";
                    ddlDepartment.DataValueField = "dept_code";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

                    ddlDepartment.ClearSelection();

                    ddlDepartment.Items.FindByValue("BA").Selected = true;

                }
            }

            string user = fl.GetUsers("-1", Session["inst_code"].ToString(), "BA");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlUser.DataSource = dt;
                    ddlUser.DataTextField = "firstname";
                    //+ " " + "lastname";
                    ddlUser.DataValueField = "userid";
                    ddlUser.DataBind();

                }
            }
            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        }
        else if (Session["dept_code"].ToString() == "FP")
        {
            txtcaseno = txt_fp.Text + "/" + txt_shortname.Text + "/" + txt_fpnumber.Text + "/" + txt_fpyear.Text + "/" + txt_fpdate.Text;

            txt_shortname.Attributes.Add("readonly", "readonly");
            div_fp.Visible = true;
            div_normal.Visible = false;

            string res2 = fl.GetDeptById(Session["inst_id"].ToString());
            if (!res2.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res2);
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataTextField = "dept_name";
                    ddlDepartment.DataValueField = "dept_code";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

                    ddlDepartment.ClearSelection();

                    ddlDepartment.Items.FindByValue("FP").Selected = true;

                }
            }

            string user = fl.GetUsers("-1", Session["inst_code"].ToString(), "FP");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlUser.DataSource = dt;
                    ddlUser.DataTextField = "firstname";
                    //+ " " + "lastname";
                    ddlUser.DataValueField = "userid";
                    ddlUser.DataBind();

                }
            }
            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        }
        //else if (txt_div.Text == "HPB/AB" || Session["dept_code"].ToString() == "HPB")
        //{
        //    txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;

        //    txt_div.Attributes.Add("readonly", "readonly");
        //    txt_dfsee.Attributes.Add("readonly", "readonly");

        //    string res3 = fl.GetDeptById(Session["inst_id"].ToString());
        //    if (!res3.StartsWith("Error"))
        //    {
        //        DataTable dt = fl.Tabulate(res3);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlDepartment.DataSource = dt;
        //            ddlDepartment.DataTextField = "dept_name";
        //            ddlDepartment.DataValueField = "dept_code";
        //            ddlDepartment.DataBind();
        //            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

        //            ddlDepartment.ClearSelection();

        //            ddlDepartment.Items.FindByValue("HPB").Selected = true;

        //        }
        //    }

        //    string user = fl.GetUsers("-1", Session["inst_code"].ToString(), "HPB");
        //    if (!user.StartsWith("Error"))
        //    {
        //        DataTable dt = fl.Tabulate(user);
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlUser.DataSource = dt;
        //            ddlUser.DataTextField = "firstname";
        //            //+ " " + "lastname";
        //            ddlUser.DataValueField = "userid";
        //            ddlUser.DataBind();

        //        }
        //    }
        //    ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));

        //}
        else
        {
            txtcaseno = txt_dfsee.Text + "/" + txt_year.Text + "/" + txt_div.Text + "/" + txt_no.Text;


            // txt_div.Attributes.Add("readonly", "readonly");

            string res4 = fl.GetDeptById(Session["inst_id"].ToString());
            if (!res4.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res4);
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dt;
                    ddlDepartment.DataTextField = "dept_name";
                    ddlDepartment.DataValueField = "dept_code";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

                    ddlDepartment.ClearSelection();
                    if (txt_div.Text == "PSY" || txt_div.Text == "LVA" || txt_div.Text == "SDS" || txt_div.Text == "NARCO" || txt_div.Text == "BEOS" || txt_div.Text == "P.ASSESSMENT")
                    {
                        ddlDepartment.Items.FindByValue("PSY").Selected = true;
                    }
                    else
                    {
                        ddlDepartment.Items.FindByValue(txt_div.Text).Selected = true;
                    }



                }
            }
            string user = "";
            if (txt_div.Text == "PSY" || txt_div.Text == "LVA" || txt_div.Text == "SDS" || txt_div.Text == "NARCO" || txt_div.Text == "BEOS" || txt_div.Text == "P.ASSESSMENT")
            {
                user = fl.GetUsers("-1", Session["inst_code"].ToString(), "PSY");
            }
            else
            {
                user = fl.GetUsers("-1", Session["inst_code"].ToString(), txt_div.Text);
            }

            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlUser.DataSource = dt;
                    ddlUser.DataTextField = "firstname";
                    //+ " " + "lastname";
                    ddlUser.DataValueField = "userid";
                    ddlUser.DataBind();

                }
            }
            ddlUser.Items.Insert(0, new ListItem("-- Select User --", "-1"));
        }


        string res = fl.GetUserAcceptance_detailsforassign(txtcaseno);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                string agencyreferanceno = dt.Rows[0]["agencyreferanceno"].ToString();
                string receiptfilepath = dt.Rows[0]["receiptfilepath"].ToString();
                string agencyname = dt.Rows[0]["agencyname"].ToString();
                string view = "";
                // string[] path = receiptfilepath.Split('\\');
                // string Uploads = path[3];
                // string DFS = path[4];
                // string dept = path[5];
                // string folder = path[6];
                // string file = path[7];
                //string viewpdf = Uploads + "\\" + DFS + "\\" + dept + "\\" + folder + "\\" + file;
                //view += "<iframe src = '"+viewpdf+"' width = '800' height = '600'>";
                view = "Click here to <a href='" + receiptfilepath + "' target='_blank'>View PDF</a>";
                view_pdf.InnerHtml = view;
                txt_Referenceno.Text = agencyreferanceno;
                txt_agencyname.Text = agencyname;


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "<script>alert('This Case is Already Assigned or No records found.');</script>");
                txt_no.Text = "";
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "<script>alert('No records Found');</script>");
        }
        //get_evideincedetails();
    }

    protected void txt_fpdate_TextChanged(object sender, EventArgs e)
    {
        get_evideincedetails();
    }
}