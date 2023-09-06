//=========================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 29-AUG-2017                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//=========================================================
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

public partial class FileMovementTracking_Report_StatusReport : System.Web.UI.Page
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
                    //objCommon.FillDropDownList(ddlFile, "FILE_FILEMASTER", "FILE_ID", "FILE_NAME", "USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_NAME");

                    if (Convert.ToInt32(Session["userno"]) == 1)
                    {

                        objCommon.FillDropDownList(ddlFile, "FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID)INNER JOIN FILE_FILEMASTER F ON (FP.[FILE_ID] = F.[FILE_ID]) INNER JOIN User_Acc U ON (FT.RECEIVER_ID = U.UA_NO)", "distinct FT.File_Movid", "F.FILE_NAME", "U.UA_TYPE  IN (3,4) AND FT.[STATUS] != 'C'", "FILE_NAME");
                        //objCommon.FillDropDownList(ddlFile, "FILE_FILEMOVEMENT_TRANSACTION FT  INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID)INNER JOIN FILE_FILEMASTER F ON (FP.[FILE_ID] = F.[FILE_ID])INNER JOIN User_Acc U ON (FT.RECEIVER_ID = U.UA_NO)INNER JOIN USER_ACC FU ON (F.USERNO = FU.UA_NO) INNER JOIN ACD_DEPARTMENT D ON (U.UA_DEPTNO = D.DEPTNO) INNER JOIN FILE_DOCUMENT_TYPE FD ON (F.DOCUMENT_TYPE = FD.DOCUMENT_TYPE_ID)", "F.FILE_ID", "F.FILE_NAME", "U.UA_TYPE  IN (3,4) AND FT.[STATUS] != 'C'", "FILE_NAME");

                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlFile, "FILE_FILEMOVEMENT_TRANSACTION FT INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID)INNER JOIN FILE_FILEMASTER F ON (FP.[FILE_ID] = F.[FILE_ID]) INNER JOIN User_Acc U ON (FT.RECEIVER_ID = U.UA_NO)", "distinct FT.File_Movid", "F.FILE_NAME", "U.UA_TYPE  IN (3,4) AND FT.[STATUS] != 'C' AND F.USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_NAME");
                        //objCommon.FillDropDownList(ddlFile, "FILE_FILEMOVEMENT_TRANSACTION FT  INNER JOIN FILE_MOVEMENTPATH FP ON (FT.FILE_MOVID = FP.FILE_MOVID)INNER JOIN FILE_FILEMASTER F ON (FP.[FILE_ID] = F.[FILE_ID])INNER JOIN User_Acc U ON (FT.RECEIVER_ID = U.UA_NO)INNER JOIN USER_ACC FU ON (F.USERNO = FU.UA_NO) INNER JOIN ACD_DEPARTMENT D ON (U.UA_DEPTNO = D.DEPTNO) INNER JOIN FILE_DOCUMENT_TYPE FD ON (F.DOCUMENT_TYPE = FD.DOCUMENT_TYPE_ID)", "F.FILE_ID", "F.FILE_NAME", "U.UA_TYPE  IN (3,4) AND FT.[STATUS] != 'C' AND F.USERNO=" + Convert.ToInt32(Session["userno"]), "FILE_NAME");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Report_StatusReport.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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

    // this button is used to insert and update the section name.
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFile.SelectedIndex > 0)
            {
                ShowReport("File Status Report", "ConsolidateFileMovementDetails.rpt");
            }           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Report_StatusReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            url += "&param=@P_FILE_ID=" + ddlFile.SelectedValue + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    #endregion
}