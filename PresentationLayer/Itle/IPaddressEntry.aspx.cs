using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;


public partial class Itle_IPaddressEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ILibrary objLib = new ILibrary();
    CourseControlleritle objcourse = new CourseControlleritle();
    ILibraryController objILB = new ILibraryController();
    DataSet ds = new DataSet();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            ViewState["action"] = "add";

            if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                //if (Session["ICourseNo"] == null)
                //    Response.Redirect("selectCourse.aspx");

                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
                // temprary provision for current session using session variable [by defaullt value set 1 in db]

                //lblSession.Text = Session["SESSION_NAME"].ToString();
                //lblSession.ToolTip = Session["SessionNo"].ToString();
                //lblCourseName.Text = Session["ICourseName"].ToString();

                //lblSession.ForeColor = System.Drawing.Color.Green;
                //lblCourseName.ForeColor = System.Drawing.Color.Green;
                BindListView();
            }
        }
        BindListView();

    }

    private void BindListView()
    {
        try
        {
            ds = objCommon.FillDropDown("ITLE_Lib_MapIPADD", "*", "SRNO", "isdeleted=" + 0, "SRNO DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvIpAdd.DataSource = ds;
                lvIpAdd.DataBind();
                fldipadd.Visible = true;
            }
            else
            {
                fldipadd.Visible = false;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IPaddressEntry.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("IPaddressEntry.aspx");
        }

    }

    //this used for sumit Ipa address entry into database. 
    protected void btnsubmitentry_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            if (!string.IsNullOrEmpty(txtcompterName.Text) && !string.IsNullOrEmpty(txtipaddress.Text))
            {
                objLib.Computername = txtcompterName.Text.Trim();
                objLib.Ipaddress = txtipaddress.Text.Trim();

                if (ViewState["action"].Equals("add"))
                {
                    ViewState["id"] = 0;

                    int status = objcourse.IAaddressEntry(objLib);

                    if (status != 0)
                    {
                        BindListView();
                        objCommon.DisplayUserMessage(updIPAddress, "IP Address saved successfully!", this.Page);
                       //lblStatus.Text = "Data save successfully!";
                        clearvalue();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updIPAddress, "Record Already Exist", this.Page);
                        //lblStatus.Text = "sorry!Try again";
                    }
                }

                if (ViewState["action"].Equals("edit"))
                {
                    objLib.Id = Convert.ToInt32(ViewState["ip_no"]);

                    int status = objcourse.IAaddressEntry(objLib);

                    if (status != 0)
                    {
                        BindListView();
                        objCommon.DisplayUserMessage(updIPAddress, "IP Address Updated successfully!", this.Page);
                        //lblStatus.Text = "Data Updated successfully!";
                        clearvalue();
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updIPAddress, "Record Already Exist", this.Page);
                        //lblStatus.Text = "sorry!Try again";
                    }

                }


            }
            else
            {
                lblStatus.Text = "Please Enter the Computer Name and IP address";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IPaddressEntry.btnsubmitentry_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    //protected void repIPadd_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "edit")
    //    {
    //        ImageButton imgbtn = e.Item.FindControl("imgbtnedit") as ImageButton;
    //        Label lblcompname = e.Item.FindControl("lblcomputername") as Label;
    //        Label lblipadd = e.Item.FindControl("lblipaddress") as Label;
    //        txtcompterName.Text = lblcompname.Text;
    //        txtipaddress.Text = lblipadd.Text;

    //        ViewState["action"] = "edit";
    //        ViewState["id"] = imgbtn.ToolTip;

    //    }
    //    if (e.CommandName == "delete")
    //    {
    //        ImageButton imgbtn = e.Item.FindControl("imgdelete") as ImageButton;
    //        objLib.Id = Convert.ToInt16(imgbtn.ToolTip);
    //        int status = objcourse.deleteipaddressentry(objLib);
    //        if (status > 0)
    //        {
    //            BindListView();
    //             lblStatus.Text = "IP address deleted successfully!";
    //        }
    //        else
    //        {
    //             lblStatus.Text = "Please Try again!";
    //        }
    //    }

    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ip_no = int.Parse(btnEdit.CommandArgument);

            ShowDetail(ip_no);

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IPaddressEntry.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int ip_no)
    {
        try
        {
            
            ViewState["ip_no"] = ip_no;
            DataTableReader dtr = objcourse.GetSingleIP(ip_no);

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    txtcompterName.Text = dtr["COMPUTERNAME"].ToString();
                    txtipaddress.Text = dtr["IPADD"].ToString();
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IPaddressEntry.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int ip_no = int.Parse(btnDel.CommandArgument);
            //objLib.Id = Convert.ToInt16(ip_no);

            int status = objcourse.deleteipaddressentry(ip_no);
            if (status > 0)
            {
                BindListView();
                //lblStatus.Text = "IP address deleted successfully!";
                objCommon.DisplayUserMessage(updIPAddress, "IP Address Deleted successfully!", this.Page);
            }
            else
            {
                lblStatus.Text = "Please Try again!";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "IPaddressEntry.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clearvalue()
    {
        txtipaddress.Text = string.Empty;
        txtcompterName.Text = string.Empty;
        ViewState["action"] = "add";

    }

    protected void btnresetentry_Click(object sender, EventArgs e)
    {
        clearvalue();
        lblStatus.Text = string.Empty;
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReportIPaddress("IPADDRESS", "IPaddressreport.rpt");
        }
        catch (Exception ex)
        {

        }


    }

    private void ShowReportIPaddress(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("IPaddressEntry.aspx")));

            url += "../Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //Response.Redirect(url);
        }
        catch (Exception ex)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NoReport, Common.MessageType.Error);
        }
    }
  
}
