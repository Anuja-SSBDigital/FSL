using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Userlist : System.Web.UI.Page
{
    FlureeCS fl = new FlureeCS();

    public void fillinst()
    {

        
        if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Additional Director" || Session["role"].ToString() == "Deputy Director")
        {
            string inst = fl.GetInst();
            if (!inst.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(inst);
                if (dt.Rows.Count > 0)
                {
                    ddl_inst.DataSource = dt;
                    ddl_inst.DataTextField = "inst_name";
                    ddl_inst.DataValueField = "inst_id";
                    ddl_inst.DataBind();
                    ddl_inst.SelectedValue = Session["inst_id"].ToString();
                    ddl_inst.Attributes.Add("disabled", "disabled");
                    //ddl_inst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }


            string res = fl.GetDeptById(ddl_inst.SelectedValue);
            if (!res.StartsWith("Error"))
            {
                DataTable dt = fl.Tabulate(res);
                if (dt.Rows.Count > 0)
                {
                    ddl_department.DataSource = dt;
                    ddl_department.DataTextField = "dept_name";
                    ddl_department.DataValueField = "dept_id";
                    ddl_department.DataBind();
                    ddl_department.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
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
                    ddl_inst.DataSource = dt;
                    ddl_inst.DataTextField = "inst_name";
                    ddl_inst.DataValueField = "inst_id";
                    ddl_inst.DataBind();
                  
                    ddl_inst.Items.Insert(0, new ListItem("-- Select Institute --", "-1"));
                }
            }



        }




       
       
    }

    public void getuserdetails()
    {
        //LinkButton linkbtn = (LinkButton)rpt_details.Items[0].FindControl("link_ActiveDeActive");

        if (Session["role"].ToString() == "Admin" ||
                      Session["role"].ToString() == "Additional Director" ||
                      Session["role"].ToString() == "Assistant Director" ||
                      Session["role"].ToString() == "Deputy Director")
        {

            string tabDiv = fl.GetUserforSuperAdmin(Session["inst_id"].ToString(), ddl_department.SelectedValue);
            if (!tabDiv.StartsWith("Error"))
            {
                DataTable dtHd = fl.Tabulate(tabDiv);
                if (dtHd.Rows.Count > 0)
                {
                    rpt_details.DataSource = dtHd;
                    rpt_details.DataBind();
                    Header.Visible = true;
                    title.Visible = false;
                    //string Status = dtHd.Rows[0]["isactive"].ToString();
                    //if (Status == "1")
                    //{
                    //    linkbtn.CssClass = "btn-outline-danger";
                    //    linkbtn.Text = "Active";
                    //}
                    //else
                    //{
                    //    linkbtn.CssClass = "btn-outline-success";
                    //    linkbtn.Text = "DeActive";
                    //}
                }
                else
                {
                    rpt_details.DataSource = null;
                    rpt_details.DataBind();
                    Header.Visible = false;
                    title.Visible = true;
                    title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
                }
            }
        }
        else if (Session["role"].ToString() == "SuperAdmin")
        {

            string tabDiv = fl.GetUserforSuperAdmin(ddl_inst.SelectedValue,ddl_department.SelectedValue);
            if (!tabDiv.StartsWith("Error"))
            {
                DataTable dtHd = fl.Tabulate(tabDiv);
                if (dtHd.Rows.Count > 0)
                {
                    //string Status = "";
                    rpt_details.DataSource = dtHd;
                    rpt_details.DataBind();
                    Header.Visible = true;
                    title.Visible = false;

                    //LinkButton linkbtn = (LinkButton)rpt_details.Items[0].FindControl("link_ActiveDeActive");

                    //for (int i = 0; i < dtHd.Rows.Count; i++)
                    //{
                     
                    //    Status = dtHd.Rows[i]["isactive"].ToString();
                    //    if (Status == "1")
                    //    {
                    //        linkbtn.CssClass = "btn-outline-danger";
                    //        linkbtn.Text = "Active";
                    //    }
                    //    else
                    //    {
                    //        linkbtn.CssClass = "btn-outline-success";
                    //        linkbtn.Text = "DeActive";
                    //    }
                    //}
                }
                else
                {
                    rpt_details.DataSource = null;
                    rpt_details.DataBind();
                    Header.Visible = false;
                    title.Visible = true;
                    title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
                }
            }
        }
        else
        {
            string tabDiv = fl.GetUserdeptwise(Session["dept_id"].ToString());
            if (!tabDiv.StartsWith("Error"))
            {
                DataTable dtHd = fl.Tabulate(tabDiv);
                if (dtHd.Rows.Count > 0)
                {
                    rpt_details.DataSource = dtHd;
                    rpt_details.DataBind();
                    Header.Visible = true;
                    title.Visible = false;
                    string Status = dtHd.Rows[0]["isactive"].ToString();
                    //if (Status == "1")
                    //{
                    //    linkbtn.CssClass = "btn-outline-danger";
                    //    linkbtn.Text = "Active";
                    //}
                    //else
                    //{
                    //    linkbtn.CssClass = "btn-outline-success";
                    //    linkbtn.Text = "DeActive";
                    //}
                }
                else
                {
                    rpt_details.DataSource = null;
                    rpt_details.DataBind();
                    Header.Visible = false;
                    title.Visible = true;
                    title.InnerHtml = "<div class='alert alert-danger' role='alert'>*No Records Found.</div>";
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["userid"] != null)
        {
            if(!IsPostBack)
            {
                getuserdetails();
                fillinst();
            }
            
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void rpt_details_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName == "lnk_edit")
        {
            Response.Redirect("Editprofile.aspx?userid="+e.CommandArgument.ToString()+"");
        }



        if (e.CommandName == "link_Active")
        {
            LinkButton link_Active = (LinkButton)e.Item.FindControl("link_Active");
            string ChangeStatus = "";
            string Message = "";
            //string tabactivedeavtive = fl.GetUseridwise(e.CommandArgument.ToString());
            //if (!tabactivedeavtive.StartsWith("Error"))
            //{
            //DataTable dtHd = fl.Tabulate(tabactivedeavtive);
            //if (dtHd.Rows.Count > 0)
            //{
                //string Status = dtHd.Rows[0]["isactive"].ToString();
                    //if (Status == "1")
                    //{
                    //    ChangeStatus = "0";
                    //    Message = "Account is DeActivated Successfully.";
                    //    link_Active.Visible = true;
                    //}
                   
                    string statuschange = fl.IsActiveUsers(e.CommandArgument.ToString(), "0");
                    if (!statuschange.StartsWith("Error"))
                    {
                        DataTable dtstatus = fl.Tabulate("[" + statuschange + "]");
                        if (dtstatus.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Account is Deactivated Successfully.');window.location.href='Userlist.aspx'</script>");

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('This Account status is not changed.Please try again.');window.location.href='Userlist.aspx'</script>");

                        }
                    }

               // }
            //}
        }


        else if (e.CommandName == "link_DeActive")
        {
           
            LinkButton link_DeActive = (LinkButton)e.Item.FindControl("link_DeActive");
            string ChangeStatus = "";
            string Message = "";
            //string tabactivedeavtive = fl.GetUseridwise(e.CommandArgument.ToString());
            //if (!tabactivedeavtive.StartsWith("Error"))
            //{
            //    DataTable dtHd = fl.Tabulate(tabactivedeavtive);
            //    if (dtHd.Rows.Count > 0)
            //    {
            //        string Status = dtHd.Rows[0]["isactive"].ToString();
            //        if (Status == "0")
            //        {
            //            ChangeStatus = "1";
            //            Message = "Account is Activated Successfully.";
            //            link_DeActive.Visible = true;
            //        }
                    string statuschange = fl.IsActiveUsers(e.CommandArgument.ToString(), "1");
                    if (!statuschange.StartsWith("Error"))
                    {
                        DataTable dtstatus = fl.Tabulate("[" + statuschange + "]");
                        if (dtstatus.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('Account is Activated Successfully.');window.location.href='Userlist.aspx'</script>");

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "<script>alert('This Account status is not changed.Please try again.');window.location.href='Userlist.aspx'</script>");

                        }
                    }

                //}
            //}
        }
    }

    protected void rpt_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField Hdn_IsActive = (HiddenField)e.Item.FindControl("Hdn_IsActive");
        LinkButton link_Active = (LinkButton)e.Item.FindControl("link_Active");
        LinkButton link_DeActive = (LinkButton)e.Item.FindControl("link_DeActive");
        if (Hdn_IsActive.Value.ToString() == "0")
        {
            link_DeActive.Visible = true;
            link_Active.Visible = false;
        }
        else if (Hdn_IsActive.Value.ToString() == "1")
        {
            link_Active.Visible = true;
            link_DeActive.Visible = false;
           
        }
    }

   

    protected void ddl_inst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddl_inst.SelectedValue != "-1")
        {

        string res = fl.GetDeptById(ddl_inst.SelectedValue);
        if (!res.StartsWith("Error"))
        {
            DataTable dt = fl.Tabulate(res);
            if (dt.Rows.Count > 0)
            {
                ddl_department.DataSource = dt;
                ddl_department.DataTextField = "dept_name";
                ddl_department.DataValueField = "dept_id";
                ddl_department.DataBind();
                ddl_department.Items.Insert(0, new ListItem("-- Select Department --", "-1"));
            }
        }
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        getuserdetails();
    }
}