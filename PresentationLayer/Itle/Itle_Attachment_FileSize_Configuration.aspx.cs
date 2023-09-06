//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE SIZE CONFIGURATION FOR ATTATCHEMENT FILES                              
// CREATION DATE : 24/04/2014                                                          
// CREATED BY    : Zubair Ahmad                                                 
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Collections.Generic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using IITMS.NITPRM;
using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;

public partial class Itle_Itle_Attachment_FileSize_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new IITMS.UAIMS_Common();

    IFileSizeConfigurationController objFZC = new IFileSizeConfigurationController();
    IFileSizeConfiguration objFileSize = new IFileSizeConfiguration();

    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {

                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                }

                //Fill_TreeLinks(tvLinks, string.Empty);
                
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Itle_Student_Roll_List.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_Attachment_FileSize_Configuration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Attachment_FileSize_Configuration.aspx");
        }
    }

    private void BindListView()
    {

        DataSet ds = null;
        try
        {
            //int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //int UA_NO = Convert.ToInt32(Session["userno"]);
            ds = objFZC.GetAllPagesByUserNo(Convert.ToInt32(Session["User_Type"]), Convert.ToInt32(Session["OrgId"]));
            lvPageList.DataSource = ds;
            lvPageList.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TestMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Dispose();
        }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["User_Type"] = Convert.ToInt32(ddlUserType.SelectedValue.ToString());

        BindListView();
        pnlPageList.Visible = true;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserType.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(updFileConfig, "Please Select User Type.", this.Page);
            }
            else
            {
                ImageButton btnEdit = sender as ImageButton;
                int page_no = int.Parse(btnEdit.CommandArgument);
                ViewState["page_no"] = page_no;

                ShowDetail(page_no, Convert.ToInt32(Session["User_Type"]));

                ViewState["action"] = "edit";
                trPageName.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int page_no, int user_type)
    {
        DataTableReader dtr = null;
        try
        {

            string[] words = null;
            dtr = objFZC.GetFileSizeByUserType(Convert.ToInt32(ViewState["page_no"]), user_type, Convert.ToInt32(Session["OrgId"]));

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblPageName.Text = dtr["AL_Link"] == null ? "" : dtr["AL_Link"].ToString();

                    if (user_type == 1)
                    {

                        words = dtr["FILE_SIZE_ADMIN"].ToString().Split(' ');

                        txtFileSize.Text = words[0];
                        if (words[1] == "KB")
                        {
                            ddlSizeUnit.SelectedValue = "0";
                        }
                        else
                            if (words[1] == "MB")
                            {
                                ddlSizeUnit.SelectedValue = "1";
                            }
                            else
                                if (words[1] == "GB")
                                {
                                    ddlSizeUnit.SelectedValue = "2";
                                }
                                else
                                {
                                    ddlSizeUnit.SelectedValue = "0";
                                }

                    }
                    else if (user_type == 2)
                    {


                        words = dtr["FILE_SIZE_STUDENT"].ToString().Split(' ');

                        txtFileSize.Text = words[0];

                        if (words[1] == "KB")
                        {
                            ddlSizeUnit.SelectedValue = "0";
                        }
                        else
                            if (words[1] == "MB")
                            {
                                ddlSizeUnit.SelectedValue = "1";
                            }
                            else
                                if (words[1] == "GB")
                                {
                                    ddlSizeUnit.SelectedValue = "2";
                                }
                                else
                                {
                                    ddlSizeUnit.SelectedValue = "0";
                                }


                    }
                    else if (user_type == 3)
                    {


                        words = dtr["FILE_SIZE_FACULTY"].ToString().Split(' ');

                        txtFileSize.Text = words[0];

                        if (words[1] == "KB")
                        {
                            ddlSizeUnit.SelectedValue = "0";
                        }
                        else
                            if (words[1] == "MB")
                            {
                                ddlSizeUnit.SelectedValue = "1";
                            }
                            else
                                if (words[1] == "GB")
                                {
                                    ddlSizeUnit.SelectedValue = "2";
                                }
                                else
                                {
                                    ddlSizeUnit.SelectedValue = "0";
                                }
                    }



                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            dtr.Dispose();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (lblPageName.Text != "")
            {

            objFileSize.PAGE_ID = Convert.ToInt32(ViewState["page_no"]);
            objFileSize.UA_TYPE = Convert.ToInt32(ddlUserType.SelectedValue);
            objFileSize.UNIT = ddlSizeUnit.SelectedItem.ToString();
            double fileSize = Convert.ToDouble(txtFileSize.Text.Trim());

            if (objFileSize.UNIT == "KB")
            {

                objFileSize.FILE_SIZE = Convert.ToDecimal(fileSize) * 1024;
            }

            else
            {
                if (objFileSize.UNIT == "MB")
                {

                    objFileSize.FILE_SIZE = Convert.ToDecimal(fileSize) * 1048576;

                }

                else
                {
                    if (objFileSize.UNIT == "GB")
                    {

                        objFileSize.FILE_SIZE = Convert.ToDecimal(fileSize) * 1073741824;

                    }
                }
            }



            CustomStatus cs = (CustomStatus)objFZC.InsertUpdateFileSize(objFileSize, Convert.ToInt32(Session["OrgId"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updFileConfig, "Record Modified Sucessfully", this.Page);
                    BindListView();
                }
            }
            else
            {
                objCommon.DisplayMessage(updFileConfig, "Please select Page to set Size", this.Page);
            }


            ClearControls();

        }

        catch (Exception ex)
        {

        }

    }

    //protected void ContactsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    Label EmailAddressLabel;
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        // Display the e-mail address in italics.
    //        EmailAddressLabel = (Label)e.Item.FindControl("EmailAddressLabel");
    //        EmailAddressLabel.Font.Italic = true;

    //        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
    //        string currentEmailAddress = rowView["EmailAddress"].ToString();
    //        if (currentEmailAddress == "orlando0@adventure-works.com")
    //        {
    //            EmailAddressLabel.Font.Bold = true;
    //        }
    //    }
    //}
    //protected void lvPageList_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    Label lblfaculty;
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        // Display the e-mail address in italics.
    //        lblfaculty = (Label)e.Item.FindControl("lblfaculty");


    //        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

    //        string currentEmailAddress = rowView["EmailAddress"].ToString();
    //        if (currentEmailAddress == "orlando0@adventure-works.com")
    //        {
    //            EmailAddressLabel.Font.Bold = true;
    //        }
    //    }
    //}

    private void ClearControls()
    {
        ddlSizeUnit.SelectedValue = "0";
        ddlUserType.SelectedValue = "0";
        txtFileSize.Text = string.Empty;
        trPageName.Visible = false;
        lblPageName.Text = string.Empty;

        dvAddPages.Visible = false;
        pnlPageList.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearControls();

    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommon.FillDropDownList(ddlPages, "ACCESS_LINK", "AL_No", "AL_Link", "AL_ASNO=" + ddlModule.SelectedValue + "AND MastNo IN (1444,1470)", "AL_Link");
        ddlPages.Focus();
        
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnOpenWindow_Click(object sender, EventArgs e)
    {
        try
        {
            dvAddPages.Visible = true;
            objCommon.FillDropDownList(ddlModule, "ACC_SECTION", "AS_No", "AS_Title", "AS_SrNo=6", "AS_Title");
            //ddlModule.SelectedItem.Text= "ITLE";

            ddlModule.Focus();
        }
        catch (Exception ex)
        { 
        
        }
      
    }

    protected void btnAddPage_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = (CustomStatus)objFZC.AddPageID(Convert.ToInt32(ddlPages.SelectedValue), Convert.ToInt32(Session["OrgId"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updFileConfig, "Record Saved Sucessfully", this.Page);

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updFileConfig, "Page already exist.", this.Page);

            }
            BindListView();

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        dvAddPages.Visible = false;
    }
}