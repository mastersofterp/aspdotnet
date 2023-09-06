//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 29-OCT-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class FileMovementTracking_Transaction_FileMovementStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FileMovement objFMov = new FileMovement();
    FileMovementController objFController = new FileMovementController();
    #region PageLoad Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    BindListOfFileMovement();                 
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovementStatus.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region User-Define Methods
    // This method is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // this method is used to display the file movement entry list.
    private void BindListOfFileMovement()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["userno"]) == 1)
            {
                //ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON ( FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN USER_ACC E ON(FT.RECEIVER_ID = E.UA_NO)", "FT.FMTRAN_ID, FM.FILE_CODE, FP.FILEPATH, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' END) AS STATUS, E.FNAME, FT.REMARK ", "", "", "FMTRAN_ID DESC");
                ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON ( FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN USER_ACC E ON(FT.RECEIVER_ID = E.UA_NO)", "FT.FMTRAN_ID, FM.FILE_CODE, FP.FILEPATH, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' END) AS STATUS ", "", "", "FMTRAN_ID DESC");
            }
            else
            {
                //ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON ( FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN USER_ACC E ON(FT.RECEIVER_ID = E.UA_NO)", "FT.FMTRAN_ID, FM.FILE_CODE, FP.FILEPATH, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' END) AS STATUS, E.FNAME , FT.REMARK ", "", "FT.RECEVING_HEAD_ID=" + Convert.ToInt32(Session["userno"]), "FMTRAN_ID DESC");
                ds = objCommon.FillDropDown("FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON ( FT.FILE_MOVID = FP.FILE_MOVID) INNER JOIN FILE_FILEMASTER FM ON (FP.FILE_ID = FM.FILE_ID) INNER JOIN FILE_SECTIONMASTER FS ON(FT.SECTION_ID = FS.SECTION_ID) INNER JOIN USER_ACC E ON(FT.RECEIVER_ID = E.UA_NO)", "FT.FMTRAN_ID, FM.FILE_CODE, FP.FILEPATH, FM.FILE_NAME, FS.SECTION_NAME, FT.MOVEMENT_DATE, (CASE FT.[STATUS] WHEN 'P' THEN 'PENDING' WHEN 'F' THEN 'FORWARD' WHEN 'R' THEN 'RETURN' END) AS STATUS ", "", "FT.RECEVING_HEAD_ID=" + Convert.ToInt32(Session["userno"]), "FMTRAN_ID DESC");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFileMovement.DataSource = ds;
                lvFileMovement.DataBind();
            }
            else
            {
                lvFileMovement.DataSource = null;
                lvFileMovement.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovementStatus.BindListOfFileMovement-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
   
    #endregion
    #region Page Actions  
    protected void btnReport_Click(object sender, EventArgs e)
    {

    }
   
    #endregion

}