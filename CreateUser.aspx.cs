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



public partial class Home : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);




    public void fillddl()
    {
        try
        {
            if (Session["role"].ToString() == "Admin")
            {
                string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "", "", "", "");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }

            else if (Session["role"].ToString() == "Additional Director")
            {
                string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "", "", "");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }
            else if (Session["role"].ToString() == "Assistant Director")
            {
                string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "", "");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }

                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }
            else if (Session["role"].ToString() == "Deputy Director")
            {
                string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "Deputy Director", "");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }
            else if (Session["role"].ToString() == "Department Head")
            {
                string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "Deputy Director", "Department Head");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }
            else
            {
                string user = fl.GetRoleByID("-1");
                if (!user.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(user);
                    if (dt.Rows.Count > 0)
                    {
                        ddlRole.DataSource = dt;
                        ddlRole.DataTextField = "role";
                        ddlRole.DataValueField = "role_id";
                        ddlRole.DataBind();
                    }
                }
                ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

            }


            if (Session["role"].ToString() == "Admin" ||
                Session["role"].ToString() == "Assistant Director" ||
                Session["role"].ToString() == "Additional Director" ||
                Session["role"].ToString() == "Deputy Director")
            {
                string inst = fl.GetInst();
                if (!inst.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(inst);
                    if (dt.Rows.Count > 0)
                    {
                        ddlInst.DataSource = dt;
                        ddlInst.DataTextField = "inst_name";
                        ddlInst.DataValueField = "inst_id";
                        ddlInst.DataBind();
                    }
                }
                string InstID = Session["inst_id"].ToString();
                ddlInst.SelectedValue = Session["inst_id"].ToString();
                ddlInst.Attributes.Add("disabled", "disabled");

                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                    }
                }
            }
            else if (Session["role"].ToString() == "SuperAdmin")
            {
                string inst = fl.GetInst();
                if (!inst.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(inst);
                    if (dt.Rows.Count > 0)
                    {
                        ddlInst.DataSource = dt;
                        ddlInst.DataTextField = "inst_name";
                        ddlInst.DataValueField = "inst_id";
                        ddlInst.DataBind();
                    }
                }
                ddlInst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));

                if (ddlInst.SelectedIndex != 0)
                {
                    string res = fl.GetDeptById(ddlInst.SelectedValue);
                    if (!res.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(res);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDep.DataSource = dt;
                            ddlDep.DataTextField = "dept_name";
                            ddlDep.DataValueField = "dept_id";
                            ddlDep.DataBind();
                        }
                    }
                }
                //ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));

            }
            else
            {
                string inst = fl.GetInst();
                if (!inst.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(inst);
                    if (dt.Rows.Count > 0)
                    {
                        ddlInst.DataSource = dt;
                        ddlInst.DataTextField = "inst_name";
                        ddlInst.DataValueField = "inst_id";
                        ddlInst.DataBind();
                    }
                }
                ddlInst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                string InstID = Session["inst_id"].ToString();
                ddlInst.SelectedValue = Session["inst_id"].ToString();
                ddlInst.Attributes.Add("disabled", "disabled");

                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                    }
                }
                string DeptID = Session["dept_id"].ToString();
                ddlDep.SelectedValue = Session["dept_id"].ToString();
                ddlDep.Attributes.Add("disabled", "disabled");

                string resdata = fl.GetDivById(ddlDep.SelectedValue);
                if (!resdata.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(resdata);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDiv.DataSource = dt;
                        ddlDiv.DataTextField = "div_name";
                        ddlDiv.DataValueField = "div_id";
                        ddlDiv.DataBind();
                    }
                }

            }
            ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));


        }
        catch (Exception ex) { }
    }

   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                fillddl();
               

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string Username = "";
        string resdata = fl.CheckUsername(txtUN.Text, Session["inst_code"].ToString(), Session["div_code"].ToString());
        DataTable dt = fl.Tabulate(resdata);
        if (dt.Rows.Count > 0)
        {
            Username = dt.Rows[0]["username"].ToString();
        }

        if (Username != txtUN.Text)
        {


            string pass = fl.EncryptString(txtPass.Text);
            string conpass = fl.EncryptString(txtConPass.Text);
            string userid = hdnUserID.Value;
            string res = "";

            string Appoi_folder = "";
            string Promo_folder = "";
            string Profile_folder = "";
            string Appoi_pathdb = "";
            string Promo_pathdb = "";
            string Profile_pathdb = "";
            string Appoi_hash = "";
            string Promo_hash = "";
            string reshash = "";
            string Appoipath = "";
            string reshashpromo = "";
            string Promopath = "";
            string Profilepath = "";
            string Profileimage = "";
            HttpPostedFile Appoi_file = null;
            HttpPostedFile Promo_file = null;
            HttpPostedFile Profile_file = null;

            if (txt_appointmentletter.HasFiles)
            {
                Appoi_file = txt_appointmentletter.PostedFile;
                Appoi_folder = "AppoitmentLetter";
            }
            if (txt_promotionletter.HasFiles)
            {
                Promo_file = txt_promotionletter.PostedFile;
                Promo_folder = "PromotionLetter";
            }
            if (fl_profile.HasFiles)
            {
                Profile_file = fl_profile.PostedFile;
                Profile_folder = "UserProfile";
            }

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

            if (Appoi_file != null)
            {
                string Appoi_filename = Appoi_file.FileName;
                long Appoi_size = Appoi_file.ContentLength;
                string Appoi_path = "";// fil
                string Appoi_fn = "";
                if (Appoi_size <= 26214400)
                {
                    Random rndm = new Random();
                    int random = rndm.Next();
                    string[] fnarray = Appoi_filename.Split('.');
                    if (fnarray.Length > 1)
                        Appoi_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random + "." + fnarray[1];
                    else
                        Appoi_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random;




                    string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                                "\\\\" + Appoi_folder;

                    string dirdb = "Uploads" + "/" + Session["inst_code"].ToString() +
                                "/" + Appoi_folder;

                    Appoi_pathdb = dirdb +
                                "/" + Appoi_fn;

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    Appoi_path = dir +
                               "\\\\" + Appoi_fn;



                    Appoipath = url + Appoi_pathdb;

                    if (!File.Exists(Appoi_path))
                        Appoi_file.SaveAs(Appoi_path);



                }
                FileStream fs = File.OpenRead(Appoi_path);

                Appoi_hash = fl.SHA256CheckSum(fs);
            }
            if (Promo_file != null)
            {
                string Promo_filename = Promo_file.FileName;
                long Promo_size = Promo_file.ContentLength;
                string Promo_path = "";// fil
                string Promo_fn = "";
                if (Promo_size <= 26214400)
                {
                    Random rndm = new Random();
                    int random = rndm.Next();
                    string[] fnarray = Promo_filename.Split('.');
                    if (fnarray.Length > 1)
                        Promo_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random + "." + fnarray[1];
                    else
                        Promo_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random;


                    string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                                "\\\\" + Promo_folder;

                    string dirdb = "Uploads" + "/" + Session["inst_code"].ToString() +
                                "/" + Promo_folder;

                    Promo_pathdb = dirdb +
                                "/" + Promo_fn;

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    Promo_path = dir +
                               "\\\\" + Promo_fn;


                    Promopath = url + Promo_pathdb;

                    if (!File.Exists(Promo_path))
                        Promo_file.SaveAs(Promo_path);

                }

                FileStream fs_promo = File.OpenRead(Promo_path);

                Promo_hash = fl.SHA256CheckSum(fs_promo);
            }

            if (Profile_file != null)
            {
                string Profile_filename = Profile_file.FileName;
                long Profile_size = Profile_file.ContentLength;
                string Profile_path = "";// fil
                string Profile_fn = "";
                if (Profile_size <= 26214400)
                {
                    Random rndm = new Random();
                    int random = rndm.Next();
                    string[] fnarray = Profile_filename.Split('.');
                    if (fnarray.Length > 1)
                        Profile_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random + "." + fnarray[1];
                    else
                        Profile_fn = DateTime.Now.ToString("yyyyMMddHHmmss")
                            + "_" + random;


                    string dir = Server.MapPath("~/").Replace("\\", "\\\\") + "Uploads" + "\\\\" + Session["inst_code"].ToString() +
                                "\\\\" + Profile_folder;

                    string dirprofile = "Uploads" + "/" + Session["inst_code"].ToString() +
                                "/" + Profile_folder;

                    Profile_pathdb = dirprofile +
                                "/" + Profile_fn;

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    Profile_path = dir +
                               "\\\\" + Profile_fn;


                    Profilepath = url + Profile_pathdb;

                    if (!File.Exists(Profile_path))
                        Profile_file.SaveAs(Profile_path);

                }

                FileStream fs_profile = File.OpenRead(Profile_path);


            }

            reshash = fl.GetHashFromCreateUserForAppoi(Appoi_hash);
            DataTable dthash = fl.Tabulate(reshash);

            reshashpromo = fl.GetHashFromCreateUserForPromo(Promo_hash);
            DataTable dthashpromo = fl.Tabulate(reshashpromo);

            if ((dthash.Rows.Count > 0) || (dthashpromo.Rows.Count > 0))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "<script>alert('This Hash is Already Exists in the System.Please choose another File.');</script>");

            }
            else
            {
                if (Appoi_hash == Promo_hash)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                  "<script>alert('Appoitment and Promotion Letters Hash are same.Please Choose Different file.');</script>");

                }
                else
                {
                    if (hdnUserID.Value == "")
                    {
                        res = fl.CreateUser(userid, txtFN.Text, txtLN.Text,
                    txtUN.Text, txtDes.Text, pass, ddlInst.SelectedValue, ddlDep.SelectedValue,
                    ddlDiv.SelectedValue, ddlRole.SelectedValue, Appoipath, Promopath, Appoi_hash, Promo_hash, txtEmail.Text, Profilepath, txt_mob.Text);
                    }
                    else
                    {
                        //    res = fl.UpdateDiv(userid, txtFN.Text, txtLN.Text,
                        //txtUN.Text, txtDes.Text, pass, ddlInst.SelectedValue, ddlDep.SelectedValue,
                        //ddlDiv.SelectedValue, ddlRole.SelectedValue, Appoipath, Promopath, Appoi_hash, Promo_hash, txtEmail.Text);
                    }

                    if (!res.StartsWith("Error"))
                    {
                        DataTable dtdata = fl.Tabulate("[" + res + "]");
                        if (dtdata.Rows.Count > 0)
                        {
                            if (dtdata.Rows[0]["status"].ToString() == "200")
                            {
                                if (hdnUserID.Value == "")
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "<script>alert('User Successfully Created.');window.location='CreateUser.aspx';</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(),
                             "alert", "<script>alert('User Updated Successfully.');window.location='CreateUser.aspx';</script>");
                                }

                            }
                            else
                            {
                                if (hdnUserID.Value == "")
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                           "<script>alert('User Does not Created. Please Try Again Later.!!');</script>");
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(),
                           "alert", "<script>alert('User Does not Updated Successfully.');window.location='CreateUser.aspx';</script>");
                                }
                            }
                        }
                        else
                        {
                            if (hdnUserID.Value == "")
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                          "<script>alert('User Does not Created.Please Try Again Later.!!');</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(),
                           "alert", "<script>alert('User Does not Updated Successfully.');window.location='CreateUser.aspx';</script>");
                            }
                        }
                    }
                    else
                    {
                        if (hdnUserID.Value == "")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                      "<script>alert('User Does not Created.Please Try Again Later.!!');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(),
                       "alert", "<script>alert('User Does not Updated Successfully.');window.location='CreateUser.aspx';</script>");
                        }
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(),
                       "alert", "<script>alert('Username is already taken. Please choose another Username.');</script>");
        }
    }

    protected void ddlInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInst.SelectedIndex != 0)
        {
            string res = fl.GetDeptById(ddlInst.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDep.DataSource = dt;
                    ddlDep.DataTextField = "dept_name";
                    ddlDep.DataValueField = "dept_id";
                    ddlDep.DataBind();
                    ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                }
            }
        }


        if (Session["role"].ToString() == "Admin")
        {
            string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "", "", "", "");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }
            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }

        else if (Session["role"].ToString() == "Additional Director")
        {
            string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "", "", "");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }
            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }
        else if (Session["role"].ToString() == "Assistant Director")
        {
            string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "", "");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }

            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }
        else if (Session["role"].ToString() == "Deputy Director")
        {
            string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "Deputy Director", "");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }
            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }
        else if (Session["role"].ToString() == "Department Head")
        {
            string user = fl.GetRoleByRole(Session["role"].ToString(), "Admin", "Additional Director", "Assistant Director", "Deputy Director", "Department Head");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }
            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }
        else
        {
            string user = fl.GetRoleByID("-1");
            if (!user.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(user);
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "role";
                    ddlRole.DataValueField = "role_id";
                    ddlRole.DataBind();
                }
            }
            ddlRole.Items.Insert(0, new ListItem("-- Select Role --", "-1"));

        }
    }

    protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDep.SelectedIndex != 0)
        {
            string res = fl.GetDivById(ddlDep.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddlDiv.DataSource = dt;
                    ddlDiv.DataTextField = "div_name";
                    ddlDiv.DataValueField = "div_id";
                    ddlDiv.DataBind();
                    ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                }
            }
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Ahmedabad" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;
                string t = "ALL Department - Ahmedabad";
                ddlDep.ClearSelection();
                ddlDep.Items.FindByText(t).Selected = true;

                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division - Ahmedabad";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {

                DivDep.Visible = true;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }
        else if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Baroda" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;
                string t = "ALL Department - Baroda";
                ddlDep.ClearSelection();
                ddlDep.Items.FindByText(t).Selected = true;

                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division - Baroda";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {
                DivDep.Visible = true;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }
        else if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Gandhinagar" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                string t = "ALL Department";
                ddlDep.ClearSelection();
                var item = ddlDep.Items.FindByText(t);
                //if (item != null)
                //{
                //    item.Selected = true;
                ddlDep.Items.FindByText(t).Selected = true;
                //}
                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {
                DivDep.Visible = true;
                string res = fl.GetDeptByIdForAdmin(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }
        else if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Junagadh" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;

                string t = "ALL Department - Junagadh";
                ddlDep.ClearSelection();
                ddlDep.Items.FindByText(t).Selected = true;
                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division - Junagadh";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {
                DivDep.Visible = true;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }

        else if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Rajkot" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;

                string t = "ALL Department - Rajkot";
                ddlDep.ClearSelection();
                ddlDep.Items.FindByText(t).Selected = true;
                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division - Rajkot";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {
                DivDep.Visible = true;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }
        else if (ddlInst.SelectedItem.Text == "Directorate Of Forensic Science - Surat" &&
            Session["role"].ToString() != "Department Head")
        {
            if (ddlRole.SelectedItem.ToString() == "Admin" ||
            ddlRole.SelectedItem.ToString() == "Assistant Director" ||
            ddlRole.SelectedItem.ToString() == "Additional Director" ||
            ddlRole.SelectedItem.ToString() == "Deputy Director" ||
            ddlRole.SelectedItem.ToString() == "SuperAdmin")
            {
                DivDep.Visible = false;

                string t = "ALL Department - Surat";
                ddlDep.ClearSelection();
                ddlDep.Items.FindByText(t).Selected = true;
                ddlDep.Attributes.Add("disabled", "disabled");
                if (ddlDep.SelectedIndex != 0)
                {
                    string resdep = fl.GetDivById(ddlDep.SelectedValue);
                    if (!resdep.StartsWith("Error"))
                    {
                        DataTable dt = fl.Tabulate(resdep);
                        if (dt.Rows.Count > 0)
                        {
                            ddlDiv.DataSource = dt;
                            ddlDiv.DataTextField = "div_name";
                            ddlDiv.DataValueField = "div_id";
                            ddlDiv.DataBind();
                            ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                            string d = "ALL Division - Surat";
                            ddlDiv.ClearSelection();
                            ddlDiv.Items.FindByText(d).Selected = true;
                            ddlDiv.Attributes.Add("disabled", "disabled");


                        }
                    }
                }
            }
            else
            {
                DivDep.Visible = true;
                string res = fl.GetDeptById(ddlInst.SelectedValue);
                if (!res.StartsWith("Error"))
                {
                    DataTable dt = fl.Tabulate(res);
                    if (dt.Rows.Count > 0)
                    {
                        ddlDep.DataSource = dt;
                        ddlDep.DataTextField = "dept_name";
                        ddlDep.DataValueField = "dept_id";
                        ddlDep.DataBind();
                        ddlDep.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
                    }
                }
                ddlDiv.Items.Clear();
                ddlDiv.Items.Insert(0, new ListItem("-- Select Division --", "-1"));
                ddlDep.Attributes.Remove("disabled");
                ddlDiv.Attributes.Remove("disabled");

            }
        }
    }
}