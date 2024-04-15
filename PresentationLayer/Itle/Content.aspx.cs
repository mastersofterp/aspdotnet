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
using BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using IITMS.NITPRM;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;




public partial class MockUps_Content : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    BlobController objBlob = new BlobController();
  //  VimeoController icon = new VimeoController();


    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    static decimal File_size;
    string PageId;

    #region Page Load

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

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }
                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                // lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //Page Authorization
                //CheckPageAuthorization();
                if (Convert.ToInt32(Session["usertype"]) == 3)  // fuculty
                {
                    BlobDetails();
                }
                else if (Convert.ToInt32(Session["usertype"]) == 2) // student
                {
                    BindQuestionStude();
                }
                objCommon.FillDropDownList(ddlModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");

                lvCompAttach.DataSource = string.Empty;
                lvCompAttach.DataBind();
                GetAttachmentSize();
                if (Session["usertype"].Equals(2))
                {
                    BindQuestionStude();
                    trSession.Visible = false;
                }
                else
                {
                    BindQuestion();
                    trSession.Visible = true;
                }

                Session["Attachments"] = null;
                lvCompAttach.DataSource = null;
                lvCompAttach.DataBind();

                DataSet dsPURPOSE = new DataSet();
                dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");
                string Extension = "";
                for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
                {
                    if (Extension == "")
                        Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                    else
                        Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                }
                lblExtension.Text = Extension;

   
            }

        }

        // Session["Attachments"] = null;

    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Content.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Content.aspx");
        }
    }

    private void BindQuestion()
    {
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESTATUS", "MODULETITLE AS MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "MODULE_NO ASC");
        // DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESTATUS", "MODULETITLE AS MODULETITLE", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            RpModule.DataSource = ds;
            RpModule.DataBind();

            foreach (RepeaterItem lsvdata in RpModule.Items)
            {

                CheckBox chkModule = lsvdata.FindControl("chkModule") as CheckBox;
                HiddenField hdnMODULE_NO = lsvdata.FindControl("chkhdnModule") as HiddenField;

                if (hdnMODULE_NO.Value == "1")
                {

                    chkModule.Checked = true;

                }
                else
                {
                    chkModule.Checked = false;
                }
            }


        }
    }


    private void BindQuestionStude()
    {
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESTATUS", "MODULETITLE AS MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " and MODULESTATUS=" + (1), "");
        // DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESTATUS", "MODULETITLE AS MODULETITLE", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            RpModule.DataSource = ds;
            RpModule.DataBind();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                foreach (RepeaterItem lsvdata in RpModule.Items)
                {

                    CheckBox chkModule = lsvdata.FindControl("chkModule") as CheckBox;
                    LinkButton linkmoduleedit = lsvdata.FindControl("linkbtnback") as LinkButton;
                    chkModule.Visible = false;
                    linkmoduleedit.Visible = false;
                }

            }
        }
    }


    protected void RpModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int TITLE = 0;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            ListView lvTopic = e.Item.FindControl("lvTopic") as ListView;
            ListView lstMaintoipic = e.Item.FindControl("lstMaintoipic") as ListView;
            HiddenField hdnMODULE_NO = e.Item.FindControl("hdnMODULE_NO") as HiddenField;
            
            if (Convert.ToInt32(Session["usertype"]) == 3)  // fuculty
            {
               

                //For Subtopic
                DataSet ds1 = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO AS MAINTITLE_NO,isnull(MAINTOPICSTATUS,0) MAINTOPICSTATUS,MODULENO", "MAINTOPICTITLE AS MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(hdnMODULE_NO.Value), "");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lstMaintoipic.DataSource = ds1;
                    lstMaintoipic.DataBind();
                    
                    foreach (ListViewDataItem lstSubtoipic1 in lstMaintoipic.Items)
                    {
                        
                        CheckBox chkTopic = lstSubtoipic1.FindControl("chkSubTopic") as CheckBox;
                        HiddenField chkSubhdntopic = lstSubtoipic1.FindControl("chkSubhdntopic") as HiddenField;
                        HiddenField hdnMAINTITLE_NO = lstSubtoipic1.FindControl("hdnMAINTITLE_NO") as HiddenField;
                      
                       TITLE =  Convert.ToInt32( hdnMAINTITLE_NO.Value);
                       
                        //chkTopic.Enabled = false;
                        if (chkSubhdntopic.Value == "1")
                        {

                            chkTopic.Checked = true;
                            

                        }
                        else
                        {
                            chkTopic.Checked = false;
                        }

                       // Subtopice_ItemDataBound((object)TITLE, e);


                    }


                }

                
                //End 
            }
            else if (Convert.ToInt32(Session["usertype"]) == 2)  // student
            {
                DataSet ds2 = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO AS MAINTITLE_NO,isnull(MAINTOPICSTATUS,0) MAINTOPICSTATUS,MODULENO", "MAINTOPICTITLE AS MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(hdnMODULE_NO.Value), "");

                // DataSet ds2 = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(hdnMODULE_NO.Value) + " and STATUS=" + (1), "");
                //DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "MODULENO=" + Convert.ToInt32(hdnMODULE_NO.Value), "");

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lstMaintoipic.DataSource = ds2;
                    lstMaintoipic.DataBind();
                    
                    foreach (ListViewDataItem lstSubtoipic1 in lstMaintoipic.Items)
                    {

                        CheckBox chkTopic = lstSubtoipic1.FindControl("chkSubTopic") as CheckBox;
                        HiddenField chkSubhdntopic = lstSubtoipic1.FindControl("chkSubhdntopic") as HiddenField;
                        HiddenField hdnMAINTITLE_NO = lstSubtoipic1.FindControl("hdnMAINTITLE_NO") as HiddenField;
                        LinkButton linkbtnTopicback12 = lstSubtoipic1.FindControl("linkbtnTopicback") as LinkButton;
                        TITLE = Convert.ToInt32(hdnMAINTITLE_NO.Value);
                        
                        //chkTopic.Enabled = false;
                        if (chkSubhdntopic.Value == "1")
                        {

                            chkTopic.Checked = true;
                            chkTopic.Visible = false;
                            linkbtnTopicback12.Visible = false;
                         
                            //chkSubhdntopic.Visible = false;

                        }
                        else
                        {
                            chkTopic.Checked = false;
                            chkTopic.Visible = false;
                            linkbtnTopicback12.Visible = false;
                            //chkSubhdntopic.Visible = false;
                        }

                        // Subtopice_ItemDataBound((object)TITLE, e);
                    }


                }
            }
        }
    }
    //protected void Subtopice_ItemDataBound(object sender, RepeaterItemEventArgs s) { 
    
    //  int TITLE = 0;
    //  if (s.Item.ItemType == ListItemType.Item || s.Item.ItemType == ListItemType.AlternatingItem)
    //  {
    //      ListView lvTopic = s.Item.FindControl("lvTopic") as ListView;
    //      ListView lstMaintoipic = s.Item.FindControl("lstMaintoipic") as ListView;
    //      //HiddenField hdnMODULE_NO = s.Item.FindControl("hdnMODULE_NO") as HiddenField;
    //      //HiddenField hdnMODULE_NO = s.Item.FindControl("hdnMODULE_NO") as HiddenField;
    //      TITLE  = Convert.ToInt32(sender);
          
    //        if (Convert.ToInt32(Session["usertype"]) == 3)  // fuculty
    //        {
    //            DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"])  + " AND MAINTITLE_NO=" + Convert.ToInt32(TITLE), "");
    //            //DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "MODULENO=" + Convert.ToInt32(hdnMODULE_NO.Value), "");
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lvTopic.DataSource = ds;
    //                lvTopic.DataBind();

    //                foreach (ListViewDataItem lsvdata in lvTopic.Items)
    //                {

    //                    CheckBox chkTopic = lsvdata.FindControl("chkTopic") as CheckBox;
    //                    HiddenField chkhdntopic = lsvdata.FindControl("chkhdntopic") as HiddenField;

    //                    //chkTopic.Enabled = false;
    //                    if (chkhdntopic.Value == "1")
    //                    {

    //                        chkTopic.Checked = true;

    //                    }
    //                    else
    //                    {
    //                        chkTopic.Checked = false;
    //                    }
    //                }


    //            }
    //        }
    //  }
    //}
    protected void linkbtnback_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            btnModuleSubmit.Visible = false;
            btnModuleClose.Visible = false;
        }
        else
        {
            btnModuleSubmit.Visible = true;
            btnModuleClose.Visible = true;
        }
        LinkButton linkbtnback = sender as LinkButton;
        int ModuleNo = Convert.ToInt32(int.Parse(linkbtnback.CommandArgument));
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ModuleNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["ViewMODULE_NO"] = ds.Tables[0].Rows[0]["MODULE_NO"].ToString();
            txtUnitSequence.Text = ds.Tables[0].Rows[0]["MODULESEQUENCE"].ToString();
            txtUnitTitleName.Text = ds.Tables[0].Rows[0]["MODULETITLE"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["MODULEDESCRIPTION"].ToString();
            LblModuleName.Text = ds.Tables[0].Rows[0]["MODULETITLE"].ToString();
            LblModuleName.Visible = true;
            lblTopicName.Visible = false;
            Label1.Visible = false;
            Label2.Visible = false;

            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MODULESTATUS"].Equals(1)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
            }
            createNewTopicModal.Visible = false;
            pnlmain.Visible = false;
            addNewModuleModal.Visible = true;
            AddNewMainTopic.Visible = false;
        }
    }

    protected void linkbtnTopicback_Click(object sender, EventArgs e)
    {
       
        objCommon.FillDropDownList(ddlModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");

        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            btnTitleSubmit.Visible = false;
            btnTitleClose.Visible = false;
        }
        else
        {
            btnTitleSubmit.Visible = true;
            btnTitleClose.Visible = true;
        }



        LinkButton linkbtnTopicback = sender as LinkButton;
        int TopicNo = Convert.ToInt32(int.Parse(linkbtnTopicback.CommandArgument));
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,STATUS,TOPIC_TYPE", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK,isnull(MAINTITLE_NO,0)MAINTITLE_NO", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["ViewTopic_NO"] = ds.Tables[0].Rows[0]["TITLE_NO"].ToString();
            txtTitleSequence.Text = ds.Tables[0].Rows[0]["TITLESEQUENCE"].ToString();
            ddlModule.SelectedValue = ds.Tables[0].Rows[0]["MODULENO"].ToString();
          
            txtTitleName.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();
            txtAvailableDateTime.Text = ds.Tables[0].Rows[0]["AVAILABLE_FROM_DATE"].ToString();
            txtavailableTillDate.Text = ds.Tables[0].Rows[0]["AVAILABLE_TILL_DATE"].ToString();
            txtDescriptionTopic.Text = ds.Tables[0].Rows[0]["TITLEDESCRIPTION"].ToString();
            HiddenTopicType.Value = ds.Tables[0].Rows[0]["TOPIC_TYPE"].ToString();
            DataSet dt = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["MODULENO"].ToString()), "");
            if (dt.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;
                Label1.Visible = true;
                Label2.Visible = true;

            }
            LblModuleName.Text = dt.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();

            objCommon.FillDropDownList(ddlMainTopic, "ITLE_CONTENT_Main_Title", "MAINTITLE_NO", "MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue), "");

            ddlMainTopic.SelectedValue = ds.Tables[0].Rows[0]["MAINTITLE_NO"].ToString();
             DataSet dt2 = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO", "MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue), "");

             Label1.Text = dt2.Tables[0].Rows[0]["MAINTOPICTITLE"].ToString();
            if (Convert.ToInt32(HiddenTopicType.Value) == 2)
            {
                divlink.Visible = true;
            }
            else
            {
                divlink.Visible = false;
            }


            txtLinkURL.Text = ds.Tables[0].Rows[0]["LINK"].ToString();
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTopicStat(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTopicStat(false);", true);
            }

            Session["Attachments"] = null;
            ShowDetail(TopicNo);
            createNewTopicModal.Visible = true;
            pnlmain.Visible = false;
            addNewModuleModal.Visible = false;
            AddNewMainTopic.Visible = false;
        }
    }
    #endregion

    //#region Create New Module

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static int SaveContentModule(string MODULESEQUENCE, string MODULETITLE, int MODULESTATUS, string MODULEDESCRIPTION)
    //{
    //    int ret = 0;

    //    try
    //    {
    //        IContentCon ContentModuleCon = new IContentCon();

    //        IContentEnt.IContentModuleEnt ContentCon = new IContentEnt.IContentModuleEnt();
    //        ContentCon.MODULE_NO = 0;
    //        ContentCon.MODULESEQUENCE = Convert.ToInt32(MODULESEQUENCE);
    //        ContentCon.MODULETITLE = MODULETITLE;
    //        ContentCon.MODULESTATUS = MODULESTATUS;
    //        ContentCon.MODULEDESCRIPTION = MODULEDESCRIPTION;
    //        ContentCon.SESSIONNO = Convert.ToInt32(HttpContext.Current.Session["userno"]);
    //        ContentCon.COURSENO = Convert.ToInt32(HttpContext.Current.Session["userno"]);

    //        ret = ContentModuleCon.AddIContentModule(ContentCon);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the exception or log the error
    //    }

    //    return ret;
    //}

    //#endregion

    //#region Create New Title


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static int SaveContentTitle(string TITLESEQUENCE, string MODULENO, string TOPIC_TITLE, string AVAILABLE_FROM_DATE, string AVAILABLE_TILL_DATE, string TITLEDESCRIPTION, string TOPIC_TYPE, string LINK, int TITLE_STATUS)
    //{
    //    int ret = 0;

    //    try
    //    {
    //        IContentCon ContentTitleCon = new IContentCon();

    //        IContentEnt.IContentTitleEnt ContentTitleEnt = new IContentEnt.IContentTitleEnt();



    //        ContentTitleEnt.TITLE_NO = 0;
    //        ContentTitleEnt.TITLESEQUENCE = Convert.ToInt32(TITLESEQUENCE);
    //        ContentTitleEnt.MODULENO = Convert.ToInt32(MODULENO);
    //        ContentTitleEnt.TOPIC_TITLE = TOPIC_TITLE;
    //        ContentTitleEnt.AVAILABLE_FROM_DATE = Convert.ToDateTime(AVAILABLE_FROM_DATE);
    //        ContentTitleEnt.AVAILABLE_TILL_DATE = Convert.ToDateTime(AVAILABLE_TILL_DATE);
    //        ContentTitleEnt.TITLEDESCRIPTION = TITLEDESCRIPTION;
    //        ContentTitleEnt.TOPIC_TYPE = TOPIC_TYPE;
    //        ContentTitleEnt.LINK = LINK;
    //        ContentTitleEnt.TITLE_STATUS = Convert.ToInt32(TITLE_STATUS);
    //        ContentTitleEnt.SESSIONNO = Convert.ToInt32(HttpContext.Current.Session["userno"]);
    //        ContentTitleEnt.COURSENO = Convert.ToInt32(HttpContext.Current.Session["userno"]);
    //        ret = ContentTitleCon.AddIContentTitle(ContentTitleEnt);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the exception or log the error
    //    }

    //    return ret;
    //}



    //#endregion


    #region file upload
    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ATTACH_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("FILE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILE_PATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        return dt;
    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ATTACH_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));
                //lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_ADMIN,'Bytes')AS FILE_SIZE_ADMIN", "PAGE_ID=" + PageId);
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));
                    // lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_STUDENT,'Bytes')AS FILE_SIZE_STUDENT", "PAGE_ID=" + PageId);
                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));

                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);

                }



        }
        catch (Exception ex)
        {

        }

    }


    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/ContentTopic/Topic" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/ContentTopic/" + filename.ToString();
        else
            return "";
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNameitledoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                Control ctrhead2 = lvCompAttach.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {



                Control ctrHeader = lvCompAttach.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvCompAttach.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnAttachFile_Click(object sender, EventArgs e)
    {

        bool results;
        try
        {
            // lvCompAttach.Visible = true;
            mutex.WaitOne();
            IContentEnt.IContentTitleEnt ContentTitleEnt = new IContentEnt.IContentTitleEnt();


            GetAttachmentSize();

            string filename = string.Empty;
            string FilePath = string.Empty;
            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");


            string fileName1 = fileUploader.FileName;
            if (fileUploader.HasFile)
            {

                string DOCFOLDER = file_path + "ITLE\\upload_files\\ContentTopic";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }
                string fileName = System.Guid.NewGuid().ToString() + fileUploader.FileName.Substring(fileUploader.FileName.IndexOf('.'));

                string contentType = contentType = fileUploader.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fileUploader.PostedFile.FileName);


                int SRNO = Convert.ToInt32(objCommon.LookUp("ITLE_CONTENT_TITLE_ATTACHMENTS", "(ISNULL(MAX(SR_NO),0))+1 AS SR_NO", ""));

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt1;
                    dt1 = ((DataTable)Session["Attachments"]);
                    int attachid = dt1.Rows.Count;

                    filename = SRNO + "_Topic_" + Session["userno"] + "-" + time + attachid;
                }
                else
                {

                    filename = SRNO + "_Topic_" + Session["userno"] + time;

                }
                ContentTitleEnt.ATTACHMENT = filename;
                ContentTitleEnt.FILEPATH = fileUploader.FileName;


                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + ext.ToString() + "'"));

                DataSet dsPURPOSE = new DataSet();

                dsPURPOSE = objCommon.FillDropDown("ACD_IATTACHMENT_FILE_EXTENTIONS", "EXTENTION", "", "", "");

                if (count != 0)
                {
                    string filePath = file_path + "ITLE\\upload_files\\ContentTopic\\" + fileName;


                    if (fileUploader.PostedFile.ContentLength < File_size)
                    {


                        string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                        string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                        results = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);


                        //if (ext == ".MP4" || ext == ".mp4")
                        //{

                        //    string uploadPath = Server.MapPath("~/Video/");
                        //    Directory.CreateDirectory(uploadPath);

                        //    fileName = Path.GetFileName(fileUploader.FileName);
                        //    filePath = Path.Combine(uploadPath, fileName);

                        //    fileUploader.SaveAs(filePath);

                        //    string AccesToken = objCommon.LookUp("Itle_Config", "AccessToken", "");

                        //   // icon.UploadVideoAsync(filePath, fileName, fileName, AccesToken).Wait();

                        //    //fileName = icon.URL;


                        //}
                        //else
                        //{
                            if (results == true)
                            {


                                int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, filename, fileUploader);

                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }

                                ContentTitleEnt.FILEPATH = fileUploader.FileName;

                            }



                            else
                            {
                                fileUploader.SaveAs(filePath);
                                HttpPostedFile file = fileUploader.PostedFile;
                                ContentTitleEnt.FILEPATH = file_path + "ITLE\\upload_files\\ContentTopic\\" + fileName;

                            }
                        }


                   // }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;
                    int maxVal = 0;

                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {
                        dt = ((DataTable)Session["Attachments"]);
                        DataRow dr = dt.NewRow();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (dt != null)
                            {
                                maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
                            }
                            dr["ATTACH_ID"] = maxVal + 1;
                            if (results == true)
                            {
                                if (ext == ".MP4" || ext == ".mp4")
                                {
                                    dr["FILE_NAME"] = fileName;
                                }
                                else
                                {
                                    dr["FILE_NAME"] = filename + ext;
                                }

                            }
                            else
                            {
                                dr["FILE_NAME"] = fileUploader.FileName;
                            }

                            dr["FILE_PATH"] = ContentTitleEnt.FILEPATH;
                            dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session["Attachments"] = dt;
                            this.BindListView_Attachments(dt);

                        }
                        else
                        {
                            dt = this.GetAttachmentDataTable();
                            dr = dt.NewRow();
                            // dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            if (dt != null)
                            {
                                maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
                            }
                            dr["ATTACH_ID"] = maxVal + 1;
                            if (results == true)
                            {
                                if (ext == ".MP4" || ext == ".mp4")
                                {
                                    dr["FILE_NAME"] = fileName;
                                }
                                else
                                {
                                    dr["FILE_NAME"] = filename + ext;
                                }
                            }
                            else
                            {
                                dr["FILE_NAME"] = fileUploader.FileName;
                            }
                            dr["FILE_PATH"] = ContentTitleEnt.FILEPATH;
                            dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session.Add("Attachments", dt);
                            this.BindListView_Attachments(dt);
                        }
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        //dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        if (dt != null)
                        {
                            maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["ATTACH_ID"]));
                        }
                        dr["ATTACH_ID"] = maxVal + 1;
                        if (results == true)
                        {
                            if (ext == ".MP4" || ext == ".mp4")
                            {
                                dr["FILE_NAME"] = fileName;
                            }
                            else
                            {
                                dr["FILE_NAME"] = filename + ext;
                            }
                        }
                        else
                        {
                            dr["FILE_NAME"] = fileUploader.FileName;
                        }
                        dr["FILE_PATH"] = ContentTitleEnt.FILEPATH;
                        dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                    // ScriptManager.RegisterStartupScript(this, GetType(), "showProgress", "showProgressBar();", true);

                }
                else
                {
                    string Extension = "";
                    for (int i = 0; i < dsPURPOSE.Tables[0].Rows.Count; i++)
                    {
                        if (Extension == "")
                            Extension = dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                        else
                            Extension = Extension + ", " + dsPURPOSE.Tables[0].Rows[i]["EXTENTION"].ToString();
                    }
                    objCommon.DisplayMessage("Upload Supported File Format.Please Upload File In " + Extension, this);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }
            // ScriptManager.RegisterStartupScript(this, GetType(), "HideProgress", "hideProgressBar();", true);
            showhidedoclist();
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "assignmentMaster.btnAttachFile_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        finally
        {
            // Release the mutex
            mutex.ReleaseMutex();
        }
    }

    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            //to permanently delete from database in case of Edit
            if (ViewState["ViewTopic_NO"] != null)
            {
                string count = objCommon.LookUp("ITLE_CONTENT_TITLE_ATTACHMENTS", "ATTACHMENT_ID", "TOPIC_NO =" + Convert.ToInt32(ViewState["ViewTopic_NO"]) + "AND ATTACHMENT_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ITLE_CONTENT_TITLE_ATTACHMENTS", "TOPIC_NO =" + Convert.ToInt32(ViewState["ViewTopic_NO"]) + " AND ATTACHMENT_ID=" + fileId);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
        string ua_no = Session["userno"].ToString();
        string embed = string.Empty;
        string Url = string.Empty;
        string directoryPath = string.Empty;
        ltEmbed.Text = null;
        string fileExtension = string.Empty;
        try
        {
            string img1 = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            fileExtension = Path.GetExtension(img1);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                if (fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }

            }

            if (fileExtension == "")
            {

                ImageButton btnEdit = sender as ImageButton;
                string Link = btnEdit.CommandArgument;
                HiddenField hdnAttachId = btnEdit.FindControl("hdnAttachId") as HiddenField;

                int ID = 0;
                DateTime UploadDate;
                DateTime UploadDateStr;
                DateTime currentDateTime;


                DataSet dsattach = objCommon.FillDropDown("ITLE_CONTENT_TITLE_ATTACHMENTS", "DateOfSub", "ATTACHMENT_ID", "ATTACHMENT_ID=" + hdnAttachId.Value + " and TOPIC_NO =" + Convert.ToInt32(ViewState["ViewTopic_NO"]), "");
                if (dsattach.Tables[0].Rows.Count > 0)
                {
                    UploadDate = Convert.ToDateTime(dsattach.Tables[0].Rows[0]["DateOfSub"]);
                    ID = Convert.ToInt32(dsattach.Tables[0].Rows[0]["ATTACHMENT_ID"]);

                }
                if (ID == 0)
                {

                    // objCommon.DisplayUserMessage(this.Page, "Video will be available after 30 minutes of uploading.", this.Page);
                    PnlLbl.Visible = true;
                    lblalert.Visible = true;
                    VimeoIframe.Attributes["src"] = "";
                    pnliframe.Visible = false;
                    pnlliteral.Visible = false;
                    lblalert.Visible = true;
                }
                else
                {
                    currentDateTime = DateTime.Now;
                    UploadDateStr = Convert.ToDateTime(dsattach.Tables[0].Rows[0]["DateOfSub"]);

                    TimeSpan timeDifference = currentDateTime - UploadDateStr;
                    double thirtyMinutes = TimeSpan.FromMinutes(30).TotalMinutes;
                    DateTime thirtyMinutesAgo = DateTime.Now.AddMinutes(+30);
                    double minutesDifference = timeDifference.TotalMinutes;


                    if (minutesDifference > thirtyMinutes)
                    {
                        VimeoIframe.Attributes["src"] = Link + "?autoplay=1&loop=1&rel=0&wmode=transparent";
                        pnliframe.Visible = true;
                        pnlliteral.Visible = false;
                        PnlLbl.Visible = false;
                        lblalert.Visible = false;

                    }
                    else
                    {
                        // HtmlGenericControl previewDiv = FindControl("preview") as HtmlGenericControl;
                        // previewDiv.Attributes["style"] = "display: none;";
                        PnlLbl.Visible = true;
                        lblalert.Visible = true;
                        VimeoIframe.Attributes["src"] = "";
                        pnliframe.Visible = false;
                        pnlliteral.Visible = false;
                        lblalert.Visible = true;
                    }
                }

            }
            else
            {
                ImageButton btnEdit = sender as ImageButton;
                string Link = btnEdit.CommandArgument;


                BlobDetails();
                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/EmailUploadFile" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();

                var ImageName = img;
                if (img == null || img == "")
                {
                    embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = "Image Not Found....!";


                }
                else
                {
                    DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var blob = blobContainer.GetBlockBlobReference(ImageName);


                    string filePath = directoryPath + "\\" + ua_no + time + ImageName;
                    string filePath1 = directoryPath + ua_no + time + ImageName;
                    string newfilename = ua_no + time + ImageName;
                    hdnfilepath.Value = filePath1;

                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";

                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/EmailUploadFile/" + newfilename));

                    if (fileExtension.ToLower() == "jpg" || fileExtension.ToLower() == "jpeg" || fileExtension.ToLower() == "png" || fileExtension.ToLower() == "gif")
                    {
                        ltEmbed.Text = string.Format("<img src='{0}' alt='Image Preview' />", ResolveUrl("~/EmailUploadFile/" + newfilename));


                        ltEmbed.Text += "<br /><a href='" + ResolveUrl("~/EmailUploadFile/" + newfilename + Url) + "' download='downloadedImage'>Click Here To Download Image</a>";
                    }
                    pnlliteral.Visible = true;
                    pnliframe.Visible = false;
                    PnlLbl.Visible = false;
                    lblalert.Visible = false;

                }


            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowDetail(int TopicNo)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["asno"] = TopicNo;
            //used to access attachments
            DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE_ATTACHMENTS A", "distinct A.ATTACHMENT_ID,A.TOPIC_NO,a.FILE_NAME, a.FILE_PATH,A.SIZE", "", "TOPIC_NO=" + Convert.ToInt32(TopicNo), "");
            DataTable dt = new DataTable();

            dt = this.GetAttachmentDataTable();
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                //dt = this.GetAttachmentDataTable();
                DataRow dr = dt.NewRow();
                dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACHMENT_ID"];
                //string fileName = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                dt.Rows.Add(dr);
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }


            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


            DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,STATUS,TOPIC_TYPE,IsBlob", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");

            if (dtr != null)
            {
                if (dtr.Tables[0].Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string blob;
                        blob = dtr.Tables[0].Rows[0]["IsBlob"].ToString();  //dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                        if (blob == "1")
                        {
                            Control ctrHeader = lvCompAttach.FindControl("divBlobDownload");
                            Control ctrHead1 = lvCompAttach.FindControl("divattachblob");
                            Control ctrhead2 = lvCompAttach.FindControl("divattach");
                            ctrHeader.Visible = true;
                            ctrHead1.Visible = true;
                            ctrhead2.Visible = false;

                            foreach (ListViewItem lvRow in lvCompAttach.Items)
                            {
                                Control ckBox = (Control)lvRow.FindControl("tdBlob");
                                Control ckattach = (Control)lvRow.FindControl("attachfile");
                                Control attachblob = (Control)lvRow.FindControl("attachblob");
                                ckBox.Visible = true;
                                attachblob.Visible = true;
                                ckattach.Visible = false;

                            }
                        }
                        else
                        {



                            Control ctrHeader = lvCompAttach.FindControl("divDownload");

                            ctrHeader.Visible = false;


                            foreach (ListViewItem lvRow in lvCompAttach.Items)
                            {
                                Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");

                                ckBox.Visible = false;

                            }
                        }

                    }




                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void imgbtnPreviewtop_Click(object sender, ImageClickEventArgs e)
    {
        string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
        string ua_no = Session["userno"].ToString();
        string embed = string.Empty;
        string Url = string.Empty;
        string directoryPath = string.Empty;
        ltEmbed.Text = null;
        string fileExtension = string.Empty;
        try
        {
            string img1 = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            fileExtension = Path.GetExtension(img1);

            if (!string.IsNullOrEmpty(fileExtension))
            {
                if (fileExtension.StartsWith("."))
                {
                    fileExtension = fileExtension.Substring(1);
                }

            }

            if (fileExtension == "")
            {

                ImageButton btnEdit = sender as ImageButton;
                string Link = btnEdit.CommandArgument;
                HiddenField hdnAttachId1 = btnEdit.FindControl("hdnAttachId1") as HiddenField;

                int ID = 0;
                DateTime UploadDate;
                DateTime UploadDateStr;
                DateTime currentDateTime;



                DataSet dsattach = objCommon.FillDropDown("ITLE_CONTENT_TITLE_ATTACHMENTS", "DateOfSub", "ATTACHMENT_ID", "ATTACHMENT_ID=" + hdnAttachId1.Value + " and TOPIC_NO =" + Convert.ToInt32(ViewState["ViewTopic_NO"]), "");
                if (dsattach.Tables[0].Rows.Count > 0)
                {
                    UploadDate = Convert.ToDateTime(dsattach.Tables[0].Rows[0]["DateOfSub"]);
                    ID = Convert.ToInt32(dsattach.Tables[0].Rows[0]["ATTACHMENT_ID"]);

                }
                if (ID == 0)
                {

                    // objCommon.DisplayUserMessage(this.Page, "Video will be available after 30 minutes of uploading.", this.Page);
                    PnlLbl.Visible = true;
                    lblalert.Visible = true;
                    VimeoIframe.Attributes["src"] = "";
                    pnliframe.Visible = false;
                    pnlliteral.Visible = false;
                    lblalert.Visible = true;
                }
                else
                {
                    currentDateTime = DateTime.Now;
                    UploadDateStr = Convert.ToDateTime(dsattach.Tables[0].Rows[0]["DateOfSub"]);

                    TimeSpan timeDifference = currentDateTime - UploadDateStr;
                    double thirtyMinutes = TimeSpan.FromMinutes(30).TotalMinutes;
                    DateTime thirtyMinutesAgo = DateTime.Now.AddMinutes(+30);
                    double minutesDifference = timeDifference.TotalMinutes;


                    if (minutesDifference > thirtyMinutes)
                    {
                        VimeoIframe.Attributes["src"] = Link + "?autoplay=1&loop=1&rel=0&wmode=transparent";
                        pnliframe.Visible = true;
                        pnlliteral.Visible = false;
                        PnlLbl.Visible = false;
                        lblalert.Visible = false;

                    }
                    else
                    {
                        // HtmlGenericControl previewDiv = FindControl("preview") as HtmlGenericControl;
                        // previewDiv.Attributes["style"] = "display: none;";
                        PnlLbl.Visible = true;
                        lblalert.Visible = true;
                        VimeoIframe.Attributes["src"] = "";
                        pnliframe.Visible = false;
                        pnlliteral.Visible = false;
                        lblalert.Visible = true;
                    }
                }

            }
            else
            {
                BlobDetails();
                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/EmailUploadFile" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
                var ImageName = img;


                if (img == null || img == "")
                {
                    embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a target = \"_blank\"  href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = "Image Not Found....!";


                }
                else
                {
                    DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                    var blob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ua_no + time + ImageName;
                    string filePath1 = directoryPath + ua_no + time + ImageName;
                    string newfilename = ua_no + time + ImageName;
                    hdnfilepath.Value = filePath1;
                    if (dtBlobPic.Rows.Count > 0)
                    {
                        string valueToAssign = dtBlobPic.Rows[0]["Uri"].ToString();

                        hdnlink.Value = valueToAssign.ToString();
                    }
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                    embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a  target = \"_blank\" href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/EmailUploadFile/" + newfilename));
                    if (fileExtension.ToLower() == "jpg" || fileExtension.ToLower() == "jpeg" || fileExtension.ToLower() == "png" || fileExtension.ToLower() == "gif")
                    {
                        ltEmbed.Text = string.Format("<img src='{0}' alt='Image Preview' />", ResolveUrl("~/EmailUploadFile/" + newfilename));


                        ltEmbed.Text += "<br /><a href='" + ResolveUrl("~/EmailUploadFile/" + newfilename + Url) + "' download='downloadedImage'>Click Here To Download Image</a>";
                    }
                    pnlliteral.Visible = true;
                    pnliframe.Visible = false;
                    lblalert.Visible = false;


                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void showdetailaftersubmit(int TopicNo)
    {
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,ATTACHMENT", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataSet dt = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["MODULENO"].ToString()), "");
            if (dt.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;
            }
            LblModuleName.Text = dt.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;


            string textData = ds.Tables[0].Rows[0]["TITLEDESCRIPTION"].ToString();
            string encodedTextData = Server.HtmlEncode(textData);
            iframeDes.Attributes["srcdoc"] = textData;
            //hplink.NavigateUrl = ds.Tables[0].Rows[0]["LINK"].ToString();
            if (ds.Tables[0].Rows[0]["LINK"].ToString() != "")
            {
                Alink.Visible = true;
                Alink.HRef = ds.Tables[0].Rows[0]["LINK"].ToString();
            }
            else
            {
                Alink.Visible = false;
                Alink.HRef = null;
            }
            if (ds.Tables[0].Rows[0]["ATTACHMENT"].ToString() != "")
            {
                // imgbtnPreview.CommandArgument = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                //imgbtnPreview.Visible = true;
                // imgbtnPreview.ToolTip = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
            else
            {
                //imgbtnPreview.Visible = false;
            }
            ShowDetailontop(TopicNo);
        }
    }
    private void ShowDetailontop(int TopicNo)
    {
        try
        {
            Session["Attachments"] = null;
            ListViewtopic.DataSource = null;
            ListViewtopic.DataBind();

            AssignmentController objAM = new AssignmentController();

            //used to access attachments
            DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE_ATTACHMENTS A", "distinct A.ATTACHMENT_ID,A.TOPIC_NO,a.FILE_NAME, a.FILE_PATH,A.SIZE", "", "TOPIC_NO=" + Convert.ToInt32(TopicNo), "");
            DataTable dt = new DataTable();

            dt = this.GetAttachmentDataTable();
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                //dt = this.GetAttachmentDataTable();
                DataRow dr = dt.NewRow();
                dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACHMENT_ID"];
                //string fileName = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                dt.Rows.Add(dr);
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }


            ListViewtopic.DataSource = dt;
            ListViewtopic.DataBind();


            DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,STATUS,TOPIC_TYPE,IsBlob", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");

            if (dtr != null)
            {
                if (dtr.Tables[0].Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListViewtopic.Visible = true;
                        string blob;
                        blob = dtr.Tables[0].Rows[0]["IsBlob"].ToString();  //dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                        if (blob == "1")
                        {
                            Control ctrHeader = ListViewtopic.FindControl("divBlobDownloadtop");
                            Control ctrHead1 = ListViewtopic.FindControl("divattachblobtop");

                            ctrHeader.Visible = true;
                            ctrHead1.Visible = true;

                            foreach (ListViewItem lvRow in ListViewtopic.Items)
                            {
                                Control ckBox = (Control)lvRow.FindControl("tdBlobtop");
                                Control ckattach = (Control)lvRow.FindControl("attachfiletop");
                                Control attachblob = (Control)lvRow.FindControl("attachblobtop");
                                ckBox.Visible = true;
                                attachblob.Visible = true;
                                // ckattach.Visible = false;

                            }
                        }
                        else
                        {



                            Control ctrHeader = lvCompAttach.FindControl("divDownloadtop");

                            ctrHeader.Visible = false;


                            foreach (ListViewItem lvRow in ListViewtopic.Items)
                            {
                                Control ckBox = (Control)lvRow.FindControl("tdDownloadLinktop");

                                ckBox.Visible = false;

                            }
                        }

                    }
                    else
                    {
                        ListViewtopic.Visible = false;
                    }



                }

            }
            DataSet ds2 = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,STATUS,TOPIC_TYPE", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");
            DataSet dt1 = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds2.Tables[0].Rows[0]["MODULENO"].ToString()), "");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;

            }
            LblModuleName.Text = dt1.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds2.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    public bool CheckModule()
    {
        bool result = false;

        string MODULETITLE = txtUnitTitleName.Text;
        // DataSet ds = objCommon.FillDropDown("ACD_IASSIGNMASTER", "AS_NO", "SUBJECT", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND SUBJECT='" + SUBJECT + "'", "");

        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND MODULETITLE='" + MODULETITLE + "'", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public bool CheckMainTopic()
    {
        bool result = false;

        string MainTopic = txtMainTitle.Text;
        // DataSet ds = objCommon.FillDropDown("ACD_IASSIGNMASTER", "AS_NO", "SUBJECT", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND SUBJECT='" + SUBJECT + "'", "");

        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO", "MAINTOPICTITLE", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND MAINTOPICTITLE='" + MainTopic + "'", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }


    public bool CheckTopic()
    {
        bool result = false;

        string topic = txtTitleName.Text;
        // DataSet ds = objCommon.FillDropDown("ACD_IASSIGNMASTER", "AS_NO", "SUBJECT", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND SUBJECT='" + SUBJECT + "'", "");

        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO", "TOPIC_TITLE", "SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + "AND TOPIC_TITLE='" + topic + "'", "");

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    protected void btnModuleSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            mutex.WaitOne();

            int ret = 0;
            IContentCon ContentModuleCon = new IContentCon();

            IContentEnt.IContentModuleEnt ContentCon = new IContentEnt.IContentModuleEnt();

            if (ViewState["ViewMODULE_NO"] != null)
            {
                ContentCon.MODULE_NO = Convert.ToInt32(ViewState["ViewMODULE_NO"]);
            }
            else
            {
                ContentCon.MODULE_NO = 0;
                DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULESEQUENCE", "", "MODULESEQUENCE=" + Convert.ToInt32(txtUnitSequence.Text) + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");
                if (dtr.Tables[0].Rows.Count > 0)
                {
                    if (dtr.Tables[0].Rows[0]["MODULESEQUENCE"].ToString() != "")
                    {
                        objCommon.DisplayUserMessage(this.Page, "Sequence already exist... ", this.Page);
                        return;
                    }
                }
            }
            ContentCon.MODULESEQUENCE = Convert.ToInt32(txtUnitSequence.Text);
            ContentCon.MODULETITLE = txtUnitTitleName.Text;
            if (hfdActive.Value == "true")
            {
                ContentCon.MODULESTATUS = Convert.ToInt32(1);
            }
            else
            {
                ContentCon.MODULESTATUS = Convert.ToInt32(0);
            }
            ContentCon.MODULEDESCRIPTION = txtDescription.Text;
            ContentCon.SESSIONNO = Convert.ToInt32(HttpContext.Current.Session["SessionNo"]);
            ContentCon.COURSENO = Convert.ToInt32(HttpContext.Current.Session["ICourseNo"]);
            bool result = CheckModule();
            if (ViewState["ViewMODULE_NO"] == null)
            {
                if (result == true)
                {
                    objCommon.DisplayMessage(this.Page, "MODULE  Already Exist", this.Page);
                    return;
                }
            }
            ret = ContentModuleCon.AddIContentModule(ContentCon);
            if (ret == 1)
            {
                objCommon.DisplayUserMessage(this.Page, "MODULE Created Successfully... ", this.Page);
                BindQuestion();
                moduleclear();
                // Response.Redirect(Request.RawUrl);
            }
            else if (ret == 2)
            {
                objCommon.DisplayUserMessage(this.Page, "MODULE Update Successfully... ", this.Page);
                BindQuestion();
                showmoduledescaftersubmit(Convert.ToInt32(ViewState["ViewMODULE_NO"]));
                //moduleclear();
                // Response.Redirect(Request.RawUrl);
            }

            else
            {

                objCommon.DisplayUserMessage(this.Page, "MODULE Not Save... ", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            // Release the mutex
            mutex.ReleaseMutex();
        }
    }
    protected void btnModuleClose_Click(object sender, EventArgs e)
    {
        moduleclear();
        Response.Redirect(Request.RawUrl);
    }
    protected void moduleclear()
    {
        txtUnitSequence.Text = string.Empty;
        txtUnitTitleName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        createNewTopicModal.Visible = false;
        AddNewMainTopic.Visible = false;
        pnlmain.Visible = true;
        addNewModuleModal.Visible = false;
        ViewState["ViewMODULE_NO"] = null;
        Session["Attachments"] = null;
        LblModuleName.Visible = false;
        lblTopicName.Visible = false;
        lbltitlehipen.Visible = false;
        LblModuleName.Text = string.Empty;
        lblTopicName.Text = string.Empty;
        txtMainDescription.Text = string.Empty;
        txtMainSeq.Text = string.Empty;
        txtMainTitle.Text = string.Empty;
        ViewState["ViewMainTitle_NO"] = null;
   
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    public static bool ValidateDate(string dateStr)
    {
        string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/[0-9]{4}$";
        return Regex.IsMatch(dateStr, pattern);
    }

    protected void btnTitleSubmit_Click(object sender, EventArgs e)
    {
        int ret = 0;

        try
        {

            IContentCon ContentTitleCon = new IContentCon();

            IContentEnt.IContentTitleEnt ContentTitleEnt = new IContentEnt.IContentTitleEnt();

            if (txtTitleSequence.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Sequence... ", this.Page);
                return;
            }
            if (ddlModule.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Module... ", this.Page);
                return;
            }
            if (ddlMainTopic.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Topic... ", this.Page);
                return;
            }
            if (txtTitleName.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Topic Title... ", this.Page);
                return;
            }
            if (txtAvailableDateTime.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Available From Date... ", this.Page);
                return;
            }
            if (txtavailableTillDate.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Available Till Date... ", this.Page);
                return;
            }

            if (ValidateDate(txtAvailableDateTime.Text))
            {

            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Valid Available From Date... ", this.Page);
                return;
            }

            if (ValidateDate(txtavailableTillDate.Text))
            {

            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Valid Available Till Date... ", this.Page);
                return;
            }

            if (Convert.ToInt32(HiddenTopicType.Value) == 2)
            {
                if (txtLinkURL.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(this.Page, "Please Enter Link... ", this.Page);
                    return;
                }
            }

            if (txtAvailableDateTime.Text != string.Empty)
            {
                DateTime date = Convert.ToDateTime(txtavailableTillDate.Text.ToString());
                DateTime startDate = Convert.ToDateTime(txtAvailableDateTime.Text.ToString());


                if (startDate > date)
                {

                    MessageBox("Available from Date Should be greater than Available Till Date.");
                    txtavailableTillDate.Text = "";
                    txtAvailableDateTime.Text = "";
                    return;
                }
            }




            if (ViewState["ViewTopic_NO"] != null)
            {
                ContentTitleEnt.TITLE_NO = Convert.ToInt32(ViewState["ViewTopic_NO"]);

            }
            else
            {
                ContentTitleEnt.TITLE_NO = 0;
               // DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLESEQUENCE", "", "TITLESEQUENCE=" + Convert.ToInt32(txtTitleSequence.Text) + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue), "");


                DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLESEQUENCE", "", "TITLESEQUENCE=" + Convert.ToInt32(txtTitleSequence.Text) + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + " AND MAINTITLE_NO=" + Convert.ToInt32(ddlMainTopic.SelectedValue), "");
                if (dtr.Tables[0].Rows.Count > 0)
                {
                    if (dtr.Tables[0].Rows[0]["TITLESEQUENCE"].ToString() != "")
                    {
                        objCommon.DisplayUserMessage(this.Page, "Sequence already exist... ", this.Page);
                        return;
                    }
                }
            }


            string DOCFOLDER = file_path + "ITLE\\upload_files\\ContentTopic";

            if (!System.IO.Directory.Exists(DOCFOLDER))
            {
                System.IO.Directory.CreateDirectory(DOCFOLDER);

            }

            if (lblBlobConnectiontring.Text != "")
            {
                ContentTitleEnt.IsBlob = 1;
            }
            else
            {
                ContentTitleEnt.IsBlob = 0;
            }
            bool result1;
            string FilePath = string.Empty;
            string filename = string.Empty;
            List<IContentEnt.TopicAttachment> attachments = new List<IContentEnt.TopicAttachment>();

            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    IContentEnt.TopicAttachment attach = new IContentEnt.TopicAttachment();
                    attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            ContentTitleEnt.attachmentsTopic = attachments;

            ContentTitleEnt.ATTACHMENT = string.Empty;
            ContentTitleEnt.FILEPATH = string.Empty;

            ContentTitleEnt.TITLESEQUENCE = Convert.ToInt32(txtTitleSequence.Text);
            ContentTitleEnt.MODULENO = Convert.ToInt32(ddlModule.SelectedValue);
            ContentTitleEnt.TOPIC_TITLE = txtTitleName.Text;
            ContentTitleEnt.AVAILABLE_FROM_DATE = Convert.ToDateTime(txtAvailableDateTime.Text);
            ContentTitleEnt.AVAILABLE_TILL_DATE = Convert.ToDateTime(txtavailableTillDate.Text);
            ContentTitleEnt.TITLEDESCRIPTION = txtDescriptionTopic.Text;
            ContentTitleEnt.TOPIC_TYPE = HiddenTopicType.Value;
            ContentTitleEnt.MainTitle_NO = Convert.ToInt32(ddlMainTopic.SelectedValue);
            ContentTitleEnt.LINK = txtLinkURL.Text;

            if (hdnswitchPublish.Value == "true")
            {
                ContentTitleEnt.TITLE_STATUS = Convert.ToInt32(1);
            }
            else
            {
                ContentTitleEnt.TITLE_STATUS = Convert.ToInt32(0);
            }

            ContentTitleEnt.TITLE_STATUS = Convert.ToInt32(1);
            ContentTitleEnt.SESSIONNO = Convert.ToInt32(HttpContext.Current.Session["SessionNo"]);
            ContentTitleEnt.COURSENO = Convert.ToInt32(HttpContext.Current.Session["ICourseNo"]);


            bool result = CheckTopic();
            if (ViewState["ViewTopic_NO"] == null)
            {
                if (result == true)
                {
                    objCommon.DisplayMessage(this.Page, "Topic  Already Exist", this.Page);
                    return;
                }
            }
            ret = ContentTitleCon.AddIContentTitle(ContentTitleEnt);

            if (ret != -99 && (ViewState["ViewTopic_NO"] == null))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "TopicStat(1);", true);
                objCommon.DisplayUserMessage(this.Page, "Topic Created Successfully... ", this.Page);

                showdetailaftersubmit(ret);
                clearTopic();
                lbltitlehipen.Visible = true;
                BindQuestion();
                // Response.Redirect(Request.RawUrl);
            }
            else if (ret != -99 && ViewState["ViewTopic_NO"] != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "TopicStat(2);", true);
                objCommon.DisplayUserMessage(this.Page, "Topic Update Successfully... ", this.Page);
                //ShowDetailontop(Convert.ToInt32(ViewState["ViewTopic_NO"]));
                showdetailaftersubmit(Convert.ToInt32(ViewState["ViewTopic_NO"]));
                //clearTopic();
                clearAferupdTopic();
                BindQuestion();
                // Response.Redirect(Request.RawUrl);
            }
            else if (ret != -99)
            {
                clearTopic();
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "TopicStat(3);", true);
                objCommon.DisplayUserMessage(this.Page, "Topic Not Save... ", this.Page);

            }

        }
        catch (Exception ex)
        {
            // Handle the exception or log the error
        }


    }
    protected void btnTitleClose_Click(object sender, EventArgs e)
    {
        clearTopic();
        Response.Redirect(Request.RawUrl);
    }
    protected void clearTopic()
    {
        txtTitleSequence.Text = string.Empty;
        ddlModule.SelectedIndex = 0;
        txtTitleName.Text = string.Empty;
        txtAvailableDateTime.Text = string.Empty;
        txtavailableTillDate.Text = string.Empty;
        txtDescriptionTopic.Text = string.Empty;
        txtLinkURL.Text = string.Empty;
        createNewTopicModal.Visible = false;
        pnlmain.Visible = true;
        addNewModuleModal.Visible = false;
        ViewState["ViewTopic_NO"] = null;
        Session["Attachments"] = null;
        lvCompAttach.Items.Clear();
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        //lvCompAttach.Visible = false;


        //LblModuleName.Visible = false;
        //lblTopicName.Visible = false;
        lbltitlehipen.Visible = false;
        // LblModuleName.Text = string.Empty;
        //lblTopicName.Text = string.Empty;
    }
    protected void clearAferupdTopic()
    {
        txtTitleSequence.Text = string.Empty;
        ddlModule.SelectedIndex = 0;
        ddlMainTopic.SelectedIndex = 0;
        txtTitleName.Text = string.Empty;
        txtAvailableDateTime.Text = string.Empty;
        txtavailableTillDate.Text = string.Empty;
        txtDescriptionTopic.Text = string.Empty;
        txtLinkURL.Text = string.Empty;
        createNewTopicModal.Visible = false;
        pnlmain.Visible = true;
        addNewModuleModal.Visible = false;
        // ViewState["ViewTopic_NO"] = null;
        Session["Attachments"] = null;
        lvCompAttach.Items.Clear();
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        //lvCompAttach.Visible = false;


        //LblModuleName.Visible = false;
        //lblTopicName.Visible = false;
        lbltitlehipen.Visible = false;
        // LblModuleName.Text = string.Empty;
        //lblTopicName.Text = string.Empty;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        LinkButton LinkButton1 = sender as LinkButton;
        int TopicNo = Convert.ToInt32(int.Parse(LinkButton1.CommandArgument));
        ViewState["ViewTopic_NO"] = TopicNo;
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,TITLESEQUENCE,MODULENO,ATTACHMENT", "TOPIC_TITLE AS TOPIC_TITLE,AVAILABLE_FROM_DATE,AVAILABLE_TILL_DATE,TITLEDESCRIPTION,LINK", "TITLE_NO=" + Convert.ToInt32(TopicNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataSet dt = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["MODULENO"].ToString()), "");
            if (dt.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;
            }
            LblModuleName.Text = dt.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;


            string textData = ds.Tables[0].Rows[0]["TITLEDESCRIPTION"].ToString();
            string encodedTextData = Server.HtmlEncode(textData);
            iframeDes.Attributes["srcdoc"] = textData;
            //hplink.NavigateUrl = ds.Tables[0].Rows[0]["LINK"].ToString();
            if (ds.Tables[0].Rows[0]["LINK"].ToString() != "")
            {
                Alink.Visible = true;
                Alink.HRef = ds.Tables[0].Rows[0]["LINK"].ToString();
            }
            else
            {
                Alink.Visible = false;
                Alink.HRef = null;
            }
            if (ds.Tables[0].Rows[0]["ATTACHMENT"].ToString() != "")
            {
                // imgbtnPreview.CommandArgument = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                //imgbtnPreview.Visible = true;
                // imgbtnPreview.ToolTip = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
            else
            {
                //imgbtnPreview.Visible = false;
            }
            ShowDetailontop(TopicNo);
        }
    }

    protected void showmoduledescaftersubmit(int moduleno)
    {
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(moduleno), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            LblModuleName.Visible = true;
            LblModuleName.Text = ds.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lbltitlehipen.Visible = false;
            lblTopicName.Visible = false;

            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            ListViewtopic.DataSource = null;
            ListViewtopic.DataBind();
            ListViewtopic.Visible = false;

            string textData = ds.Tables[0].Rows[0]["MODULEDESCRIPTION"].ToString();
            string encodedTextData = Server.HtmlEncode(textData);
            iframeDes.Attributes["srcdoc"] = textData;

        }
    }
    protected void lnkmodule_Click(object sender, EventArgs e)
    {
        LinkButton module = sender as LinkButton;
        int moduleNo = Convert.ToInt32(int.Parse(module.CommandArgument));
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(moduleNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            LblModuleName.Visible = true;
            LblModuleName.Text = ds.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lbltitlehipen.Visible = false;
            lblTopicName.Visible = false;

            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            ListViewtopic.DataSource = null;
            ListViewtopic.DataBind();
            ListViewtopic.Visible = false;

            string textData = ds.Tables[0].Rows[0]["MODULEDESCRIPTION"].ToString();
            string encodedTextData = Server.HtmlEncode(textData);
            iframeDes.Attributes["srcdoc"] = textData;

        }
    }
    protected void chkModule_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox moduleStatus = sender as CheckBox;

        int moduleNo = Convert.ToInt32(moduleStatus.Text);
        IContentCon ContentModuleCon = new IContentCon();
        int ret = 0;
        int status = 0;
        string CourseName = string.Empty;
        if (moduleStatus.Checked)
        {
            status = 1;
            CourseName = "I";
        }
        else
        {
            status = 0;
            CourseName = "U";
        }
        ret = ContentModuleCon.UpdateIContentModule(moduleNo, status);
        if (ret == 1)
        {

            objCommon.DisplayUserMessage(this.Page, "Module published changed ... ", this.Page);
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            // Response.Redirect(Request.RawUrl);
        }



    }

    protected void chkTopic_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkTopic = sender as CheckBox;

        int TopicNo = Convert.ToInt32(chkTopic.Text);
        IContentCon ContentModuleCon = new IContentCon();
        int ret = 0;
        int status = 0;
        string CourseName = string.Empty;
        if (chkTopic.Checked)
        {
            status = 1;
            CourseName = "I";
        }
        else
        {
            status = 0;
            CourseName = "U";
        }
        ret = ContentModuleCon.UpdateIContenTitle(TopicNo, status);

        if (ret == 1)
        {

            objCommon.DisplayUserMessage(this.Page, "Topic published changed ... ", this.Page);
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            Response.Redirect(Request.RawUrl);
        }
    }

    protected void PageLoadView()
    {

        trSession.Visible = true;
    }
    protected void btnModule_Click(object sender, EventArgs e)
    {
        moduleclear();
        pnlmain.Visible = false;
        addNewModuleModal.Visible = true;
        AddNewMainTopic.Visible = false;
        createNewTopicModal.Visible = false;
        string maxtitle = objCommon.LookUp("ITLE_CONTENT_MODULE", "ISNULL(max(MODULESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]));
        txtUnitSequence.Text = maxtitle.ToString();

    }
    protected void btnTopic_Click(object sender, EventArgs e)
    {
        clearTopic();
        pnlmain.Visible = false;
        upd_ModalPopupExtender1.Show();
        addNewModuleModal.Visible = false;
        AddNewMainTopic.Visible = false;
    }

    protected void lnlLinkTopic_Click(object sender, EventArgs e)
    {
        LblModuleName.Visible = false;
        lblTopicName.Visible = false;
        lbltitlehipen.Visible = false;
        LblModuleName.Text = string.Empty;
        lblTopicName.Text = string.Empty;

        objCommon.FillDropDownList(ddlModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");
        clearTopic();
        divlink.Visible = false;
        HiddenTopicType.Value = "1";
        createNewTopicModal.Visible = true;
        pnlmain.Visible = false;
        addNewModuleModal.Visible = false;
        // string maxtitle = objCommon.LookUp("ITLE_CONTENT_TITLE", "ISNULL(max(TITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue));
        // txtTitleSequence.Text = maxtitle.ToString();
        Session["Attachments"] = null;
        lvCompAttach.Items.Clear();
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        DateTime date = DateTime.Now;
        txtAvailableDateTime.Text = date.ToString();
        DateTime endDateThreshold = date.AddYears(10);
        txtavailableTillDate.Text = endDateThreshold.ToString();
       
    }
    protected void lnlWebLinkTopic_Click(object sender, EventArgs e)
    {
        LblModuleName.Visible = false;
        lblTopicName.Visible = false;
        lbltitlehipen.Visible = false;
        LblModuleName.Text = string.Empty;
        lblTopicName.Text = string.Empty;
        objCommon.FillDropDownList(ddlModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");
        clearTopic();
        divlink.Visible = true;
        HiddenTopicType.Value = "2";
        createNewTopicModal.Visible = true;
        pnlmain.Visible = false;
        addNewModuleModal.Visible = false;
        //string maxtitle = objCommon.LookUp("ITLE_CONTENT_TITLE", "ISNULL(max(TITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue));
        // txtTitleSequence.Text = maxtitle.ToString();
        Session["Attachments"] = null;
        lvCompAttach.DataSource = null;
        lvCompAttach.DataBind();
        DateTime date = DateTime.Now;
        txtAvailableDateTime.Text = date.ToString();
        DateTime endDateThreshold = date.AddYears(10);
        txtavailableTillDate.Text = endDateThreshold.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string directoryPath = Server.MapPath("~/EmailUploadFile/");

        // Delete all files in the directory
        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                if (file == hdnfilepath.Value)
                {
                    File.Delete(file);
                }
            }
        }

        // Clear the embedded object
        ltEmbed.Text = "";

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        upd_ModalPopupExtender1.Hide();
        Response.Redirect(Request.RawUrl);
    }
    private static Mutex mutex = new Mutex();
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {

        //string maxtitle = objCommon.LookUp("ITLE_CONTENT_Main_Title", "ISNULL(max(MAINTITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + " AND MAINTITLE_NO=" + Convert.ToInt32(ddlMainTopic.SelectedValue));

        ////string maxtitle = objCommon.LookUp("ITLE_CONTENT_TITLE", "ISNULL(max(TITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue));
        //txtTitleSequence.Text = maxtitle.ToString();
        //Session["Attachments"] = null;
        objCommon.FillDropDownList(ddlMainTopic, "ITLE_CONTENT_Main_Title", "MAINTITLE_NO", "MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue), "");
        //lvCompAttach.DataSource = null;
        //lvCompAttach.DataBind();
        showhidedoclist();

    }

    protected void showhidedoclist()
    {
        if (Session["Attachments"] == null)
        {
            lvCompAttach.Visible = false;
        }
        else
        {
            lvCompAttach.Visible = true;
        }
    }




    protected void btnMainTopic1_Click(object sender, EventArgs e)
    {
        moduleclear();
        AddNewMainTopic.Visible = true;
       
        pnlmain.Visible = false;
        addNewModuleModal.Visible = false;
        createNewTopicModal.Visible = false;
        objCommon.FillDropDownList(ddlMainModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");
    }
    protected void ddlMainModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        string maxtitle = objCommon.LookUp("ITLE_CONTENT_Main_Title", "ISNULL(max(MainTITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlMainModule.SelectedValue));
        txtMainSeq.Text = maxtitle.ToString();
    }
    protected void btnMainSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            mutex.WaitOne();

            int ret = 0;
            IContentCon ContentMainTopicCon = new IContentCon();

            IContentEnt.IContentMainTitleEnt ContentCon = new IContentEnt.IContentMainTitleEnt();
            if (ddlMainModule.SelectedIndex == 0)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Module... ", this.Page);
                return;
            }
            if (txtMainSeq.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Sequence... ", this.Page);
                return;
            }
            
            if (txtMainTitle.Text == string.Empty)
            {
                objCommon.DisplayUserMessage(this.Page, "Please Enter Topic Title... ", this.Page);
                return;
            }
            if (ViewState["ViewMainTitle_NO"] != null)
            {
                ContentCon.MainTitle_NO = Convert.ToInt32(ViewState["ViewMainTitle_NO"]);
            }
            else
            {
                ContentCon.MainTitle_NO = 0;
                DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MainTITLESEQUENCE", "", "MainTitleSEQUENCE=" + Convert.ToInt32(txtMainSeq.Text) + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlMainModule.SelectedValue), "");
                if (dtr.Tables[0].Rows.Count > 0)
                {
                    if (dtr.Tables[0].Rows[0]["MainTITLESEQUENCE"].ToString() != "")
                    {
                        objCommon.DisplayUserMessage(this.Page, "Sequence already exist... ", this.Page);
                        return;
                    }
                }
            }
            ContentCon.MainModuleSequece = Convert.ToInt32(txtMainSeq.Text);
            ContentCon.MainTopicTitle = txtMainTitle.Text;
            if (MainSwitch.Value == "true")
            {
                ContentCon.MODULESTATUS = Convert.ToInt32(1);
            }
            else
            {
                ContentCon.MODULESTATUS = Convert.ToInt32(0);
            }
            ContentCon.Module_NO = Convert.ToInt32(ddlMainModule.SelectedValue);
            ContentCon.MainTopicDescription = txtMainDescription.Text;
            ContentCon.SESSIONNO = Convert.ToInt32(HttpContext.Current.Session["SessionNo"]);
            ContentCon.COURSENO = Convert.ToInt32(HttpContext.Current.Session["ICourseNo"]);
            bool result = CheckMainTopic();
            if (ViewState["ViewMainTitle_NO"] == null)
            {
                if (result == true)
                {
                    objCommon.DisplayMessage(this.Page, "Topic  Already Exist", this.Page);
                    return;
                }
            }
            ret = ContentMainTopicCon.AddIContentMainTitle(ContentCon);
            if (ret == 1)
            {
                objCommon.DisplayUserMessage(this.Page, "Topic Created Successfully... ", this.Page);
                BindQuestion();
                moduleclear();
            }
            else if (ret == 2)
            {
                objCommon.DisplayUserMessage(this.Page, "Topic Update Successfully... ", this.Page);
                BindQuestion();
                moduleclear();
               // showmoduledescaftersubmit(Convert.ToInt32(ViewState["ViewMainTitle_NO"]));
 
            }

            else
            {

                objCommon.DisplayUserMessage(this.Page, "Topic Not Save... ", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            // Release the mutex
            mutex.ReleaseMutex();
        }

    }
    protected void btnMainClose_Click(object sender, EventArgs e)
    {
        moduleclear();
        Response.Redirect(Request.RawUrl);
    }
    protected void ddlMainTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
       // DataSet dtr = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLESEQUENCE", "", "TITLESEQUENCE=" + Convert.ToInt32(txtTitleSequence.Text) + " AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + " AND MAINTITLE_NO=" + Convert.ToInt32(ddlMainTopic.SelectedValue), "");

        string maxtitle = objCommon.LookUp("ITLE_CONTENT_TITLE", "ISNULL(max(TITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + " AND MAINTITLE_NO=" + Convert.ToInt32(ddlMainTopic.SelectedValue));

     //   string maxtitle = objCommon.LookUp("ITLE_CONTENT_Main_Title", "ISNULL(max(MAINTITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue) + " AND MAINTITLE_NO=" + Convert.ToInt32(ddlMainTopic.SelectedValue));

        //string maxtitle = objCommon.LookUp("ITLE_CONTENT_TITLE", "ISNULL(max(TITLESEQUENCE),0)+1", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlModule.SelectedValue));
        txtTitleSequence.Text = maxtitle.ToString();
    }
    protected void LinkMainButton1_Click(object sender, EventArgs e)
    {
        LinkButton LinkMainButton1 = sender as LinkButton;
        int MainTItleNo = Convert.ToInt32(int.Parse(LinkMainButton1.CommandArgument));
        ViewState["ViewMainTitle_NO"] = MainTItleNo;
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO AS MAINTITLE_NO,MAINTITLESEQUENCE,MODULENO", "MAINTOPICTITLE AS MAINTOPICTITLE,MAINTITLEDESCRIPTION", "MAINTITLE_NO=" + Convert.ToInt32(MainTItleNo), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataSet dt = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["MODULENO"].ToString()), "");
            if (dt.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;
            }
            LblModuleName.Text = dt.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds.Tables[0].Rows[0]["MAINTOPICTITLE"].ToString();
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            AddNewMainTopic.Visible = false;


            string textData = ds.Tables[0].Rows[0]["MAINTITLEDESCRIPTION"].ToString();
            string encodedTextData = Server.HtmlEncode(textData);
            iframeDes.Attributes["srcdoc"] = textData;

        }
    }
    protected void linkbtnTopicback_Click1(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlMainModule, "ITLE_CONTENT_MODULE", "MODULE_NO", "MODULETITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]), "");

        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            btnMainSubmit.Visible = false;
            btnMainClose.Visible = false;
        }
        else
        {
            btnMainSubmit.Visible = true;
            btnMainClose.Visible = true;
        }



        LinkButton linkbtnTopicback1 = sender as LinkButton;
        int Main_Title_NO = Convert.ToInt32(int.Parse(linkbtnTopicback1.CommandArgument));
        DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_Main_Title", "MAINTITLE_NO,MAINTITLESEQUENCE,MODULENO,MAINTOPICSTATUS STATUS", "MAINTOPICTITLE AS TOPIC_TITLE,MAINTITLEDESCRIPTION", "MAINTITLE_NO=" + Convert.ToInt32(Main_Title_NO), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["ViewMainTitle_NO"] = ds.Tables[0].Rows[0]["MAINTITLE_NO"].ToString();
            txtMainSeq.Text = ds.Tables[0].Rows[0]["MAINTITLESEQUENCE"].ToString();
            ddlMainModule.SelectedValue = ds.Tables[0].Rows[0]["MODULENO"].ToString();

            txtMainTitle.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();

            txtMainDescription.Text = ds.Tables[0].Rows[0]["MAINTITLEDESCRIPTION"].ToString();
            DataSet dt = objCommon.FillDropDown("ITLE_CONTENT_MODULE", "MODULE_NO AS MODULE_NO,MODULESEQUENCE", "MODULETITLE AS MODULETITLE,MODULEDESCRIPTION,MODULESTATUS", "MODULE_NO=" + Convert.ToInt32(ds.Tables[0].Rows[0]["MODULENO"].ToString()), "");
            if (dt.Tables[0].Rows.Count > 0)
            {
                LblModuleName.Visible = true;
                lblTopicName.Visible = true;
                lbltitlehipen.Visible = true;
                Label1.Visible = false;
                Label2.Visible = false;

            }
            LblModuleName.Text = dt.Tables[0].Rows[0]["MODULETITLE"].ToString();
            lblTopicName.Text = ds.Tables[0].Rows[0]["TOPIC_TITLE"].ToString();
            ddlMainModule.SelectedValue = ds.Tables[0].Rows[0]["MODULENO"].ToString();
            objCommon.FillDropDownList(ddlMainTopic, "ITLE_CONTENT_Main_Title", "MAINTITLE_NO", "MAINTOPICTITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MODULENO=" + Convert.ToInt32(ddlMainModule.SelectedValue), "");

            ddlMainTopic.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["MAINTITLE_NO"]).ToString();

            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat1(false);", true);
            }

            Session["Attachments"] = null;
            createNewTopicModal.Visible = false; ;
            pnlmain.Visible = false;
            addNewModuleModal.Visible = false;
            addNewModuleModal.Visible = false;
            AddNewMainTopic.Visible = true;
            createNewTopicModal.Visible = false;
        }
    }
    protected void chkSubTopic_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox MainTpicStatus = sender as CheckBox;

        int Main_title_NO = Convert.ToInt32(MainTpicStatus.Text);
        IContentCon ContentModuleCon = new IContentCon();
        int ret = 0;
        int status = 0;
        string CourseName = string.Empty;
        if (MainTpicStatus.Checked)
        {
            status = 1;
            CourseName = "I";
        }
        else
        {
            status = 0;
            CourseName = "U";
        }
        ret = ContentModuleCon.UpdateIContentMainTopic(Main_title_NO, status);
        if (ret == 1)
        {

            objCommon.DisplayUserMessage(this.Page, "Main Topic published changed ... ", this.Page);
            createNewTopicModal.Visible = false;
            pnlmain.Visible = true;
            addNewModuleModal.Visible = false;
            // Response.Redirect(Request.RawUrl);
        }
    }


    protected void lstMaintoipic_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {

        ListView lstMaintoipic = e.Item.FindControl("lstMaintoipic") as ListView;
        ListView lvTopic = e.Item.FindControl("lvTopic") as ListView;
        HiddenField hdnMAINTITLE_NO = e.Item.FindControl("hdnMAINTITLE_NO") as HiddenField;
        HiddenField hdnMAINModule_NO = e.Item.FindControl("hdnMAINModule_NO") as HiddenField;
        //HiddenField hdnMODULE_NO = s.Item.FindControl("hdnMODULE_NO") as HiddenField;

        //HiddenField TITLE = lstMaintoipic.FindControl("hdnMODULE_NO") as HiddenField;

        //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        //Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        //TITLE = Convert.ToInt32(sender);

        if (Convert.ToInt32(Session["usertype"]) == 3)  // fuculty
        {
            DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MAINTITLE_NO=" + Convert.ToInt32(hdnMAINTITLE_NO.Value) + " AND MODULENO=" + Convert.ToInt32(hdnMAINModule_NO.Value), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTopic.DataSource = ds;
                lvTopic.DataBind();

                foreach (ListViewDataItem lsvdata in lvTopic.Items)
                {

                    CheckBox chkTopic = lsvdata.FindControl("chkTopic") as CheckBox;
                    HiddenField chkhdntopic = lsvdata.FindControl("chkhdntopic") as HiddenField;

                    //chkTopic.Enabled = false;
                    if (chkhdntopic.Value == "1")
                    {

                        chkTopic.Checked = true;

                    }
                    else
                    {
                        chkTopic.Checked = false;
                    }
                }


            }
        }
        if (Convert.ToInt32(Session["usertype"]) == 2)  // fuculty
        {
            DataSet ds = objCommon.FillDropDown("ITLE_CONTENT_TITLE", "TITLE_NO AS TITLE_NO,STATUS", "TOPIC_TITLE AS TOPIC_TITLE", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND MAINTITLE_NO=" + Convert.ToInt32(hdnMAINTITLE_NO.Value) + " AND MODULENO=" + Convert.ToInt32(hdnMAINModule_NO.Value), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTopic.DataSource = ds;
                lvTopic.DataBind();
                foreach (ListViewDataItem lsvdata in lvTopic.Items)
                {

                    CheckBox chkTopic = lsvdata.FindControl("chkTopic") as CheckBox;
                    HiddenField chkhdntopic = lsvdata.FindControl("chkhdntopic") as HiddenField;
                    LinkButton linktopicedit1 = lsvdata.FindControl("linkbtnTopicback") as LinkButton;

                    //chkTopic.Enabled = false;
                    if (chkhdntopic.Value == "1")
                    {

                        chkTopic.Checked = true;
                        chkTopic.Visible = false;
                        linktopicedit1.Visible = false;
                       

                    }
                    else
                    {
                        chkTopic.Checked = false;
                        linktopicedit1.Visible = false;
                        chkTopic.Visible = false;
                    }
                }


            }
        }
    }
}