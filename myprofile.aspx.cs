using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myprofile : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();
    private static readonly ILog log = LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                string res = fl.GetUsersForUpdate(Session["userid"].ToString(), Session["inst_code"].ToString(), Session["div_code"].ToString());
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    txtUN.Text = dt.Rows[0]["username"].ToString();
                    txtFN.Text = dt.Rows[0]["firstname"].ToString();
                    txtLN.Text = dt.Rows[0]["lastname"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txtDes.Text = dt.Rows[0]["designation"].ToString();
                    txtInst.Text = dt.Rows[0]["inst_name"].ToString();
                    txtDep.Text = dt.Rows[0]["dept_name"].ToString();
                    txtDiv.Text = dt.Rows[0]["div_name"].ToString();
                    txtRole.Text = dt.Rows[0]["role"].ToString(); 
                    txt_mob.Text = dt.Rows[0]["mobileno"].ToString();
                    hdnUserID.Value = Session["userid"].ToString();
                    //string appointmentletter = dt.Rows[0]["appointmentletter"].ToString();
                    //string promotionletter =  dt.Rows[0]["promotionletter"].ToString();
                    //string profileimage = dt.Rows[0]["profileimage"].ToString();

                    //if (profileimage != "")
                    //{
                    //    this.imagepreview.InnerHtml = "<div class='span3'><a class='img lightbox_trigger' href='"
                    //        + profileimage + "' target='_blank'>View Profile</a ></div>";

                    //}
                    //if (appointmentletter != "")
                    //{
                    //    this.AppointmentLetter_Div.InnerHtml = "<div class='span3'><a class='img lightbox_trigger' href='"
                    //  + appointmentletter + "' target='_blank'>View Appoitment Letter</a ></div>";

                    //}
                    //if (promotionletter != "")
                    //{
                    //    this.PromotionLetter_Div.InnerHtml = "<div class='span3'><a class='img lightbox_trigger' href='"
                    //  + promotionletter + "' target='_blank'>View Promotion Letter</a ></div>";

                    //}
                }
                    
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       
        string userid = hdnUserID.Value;

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
            string hdnpath = hdnAppoitmentLetter.Value;
            if (hdnpath != "")
            {
                if (File.Exists(hdnAppoitmentLetter.Value))
                {
                    File.Delete(hdnAppoitmentLetter.Value);
                }
            }
            string Appoi_filename = Appoi_file.FileName;
            long Appoi_size = Appoi_file.ContentLength;
            string Appoi_path = "";// fil
            string Appoi_fn = "";
            if (Appoi_size <= 5000000)
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
            string hdnpath = hdnPromotionLetter.Value;
            if (hdnpath != "")
            {
                if (File.Exists(hdnPromotionLetter.Value))
                {
                    File.Delete(hdnPromotionLetter.Value);
                }
            }
            string Promo_filename = Promo_file.FileName;
            long Promo_size = Promo_file.ContentLength;
            string Promo_path = "";// fil
            string Promo_fn = "";
            if (Promo_size <= 5000000)
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
            string hdnpath = hdnProfileImg.Value;
            if (hdnpath != "imges/user.png")
            {
                if (File.Exists(hdnProfileImg.Value))
                {
                    File.Delete(hdnProfileImg.Value);
                }
            }
            string Profile_filename = Profile_file.FileName;
            long Profile_size = Profile_file.ContentLength;
            string Profile_path = "";// fil
            string Profile_fn = "";
            if (Profile_size <= 5000000)
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
        string res = "";

        //reshash = fl.GetHashFromCreateUserForAppoi(Appoi_hash);
        //DataTable dthash = fl.Tabulate(reshash);

        //reshashpromo = fl.GetHashFromCreateUserForPromo(Promo_hash);
        //DataTable dthashpromo = fl.Tabulate(reshashpromo);

        //if ((dthash.Rows.Count > 0) || (dthashpromo.Rows.Count > 0))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //"<script>alert('This Hash is Already Exists in the System.Please choose another File.');</script>");

        //}
        //else
        //{
        //    if (Appoi_hash == Promo_hash)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert",
        //      "<script>alert('Appoitment and Promotion Letters Hash are same.Please Choose Different file.');</script>");

        //    }
        //    else
        //    {
                    res = fl.UpdateBasicDetails(userid, txtFN.Text, txtLN.Text,txtDes.Text,Appoipath, Promopath, Appoi_hash, Promo_hash,txtEmail.Text, Profilepath, txt_mob.Text);
                

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
                        "alert", "<script>alert('User Successfully Created.');window.location='myprofile.aspx';</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(),
                         "alert", "<script>alert('User Updated Successfully.');window.location='myprofile.aspx';</script>");
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
                       "alert", "<script>alert('User Does not Updated Successfully.');window.location='myprofile.aspx';</script>");
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
                       "alert", "<script>alert('User Does not Updated Successfully.');window.location='myprofile.aspx';</script>");
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
                   "alert", "<script>alert('User Does not Updated Successfully.');window.location='myprofile.aspx';</script>");
                    }
                }
           // }
      //  }





    }
}