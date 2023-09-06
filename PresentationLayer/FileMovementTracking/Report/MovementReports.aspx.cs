//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 01-DEC-2015                                                        
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

public partial class FileMovementTracking_Report_MovementReports : System.Web.UI.Page
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
                    objCommon.FillDropDownList(ddlSection, "FILE_SECTIONMASTER", "SECTION_ID", "SECTION_NAME", "POST_TO_DATE IS NULL AND SECTION_ID <>0", "SECTION_ID");
                    objCommon.FillDropDownList(ddluser, "user_acc U INNER JOIN  FILE_SECTIONMASTER SM  ON (SM.RECEIVER_ID =U.UA_NO)", "UA_NO", "UA_FULLNAME", "", "UA_FULLNAME");   //GAYATRI RODE 07-04-2022 
                     
                    //GAYATRI RODE 07-04-2022 
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Report_MovementReports.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
    // this method is used to clear the controls.
    private void Clear()
    {
        ddlReport.SelectedIndex = 0;
        ddluser.SelectedIndex = 0;
        ddlSection.SelectedIndex = -1;
        txtFDate.Text = string.Empty;
        txtTDate.Text = string.Empty;
        lvFile.DataSource = null;
        lvFile.DataBind();
        lvFile.Visible = false;

     //--------------21/06/2022
        DIVUSER.Visible = false; //19-08-2022
        ddluser.Visible = false;//19-08-2022
        ddlSection.Visible = true; 
        DIVSEC.Visible = true;
        Fdate.Visible = true;
        Tdate.Visible = true;
        btnShow.Visible = true;
    //-------end 21/06/2022----
    }

    // this method is used to display the list of Files which are used for movement.
    private void BindListViewFile()
    {
        try
        {
            DataSet ds = null;

            ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO) LEFT OUTER JOIN FILE_MOVEMENTPATH MP ON (F.FILE_ID = MP.FILE_ID) inner join FILE_DOCUMENT_TYPE DT on (DT.DOCUMENT_TYPE_ID=F.DOCUMENT_TYPE)", "F.FILE_ID, FILE_CODE,FILE_NAME, MP.FILEPATH, F.STATUS", "DESCRIPTION, CREATION_DATE, U.UA_FULLNAME, DT.DOCUMENT_TYPE AS DOCUMENT_TYPE, (CASE F.STATUS WHEN 0 THEN 'ACTIVE' ELSE 'INACTIVE' END) AS STATUS, (CASE MOVEMENT_STATUS WHEN 'N' THEN 'NOT MOVE' WHEN 'M' THEN 'MOVE' WHEN 'C' THEN 'COMPLETE' END) AS MOVEMENT_STATUS, MP.FILE_MOVID ", "U.UA_TYPE  IN (3,5)", "F.FILE_ID");
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvFile.DataSource = ds;
                lvFile.DataBind();
                lvFile.Visible = true;
            }
            else
            {
                lvFile.DataSource = null;
                lvFile.DataBind();
                lvFile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Transaction_FileMovement.BindListViewFile-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
    #endregion
    #region Page Actions
    // this button is used to insert and update the section name.
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlSection.SelectedIndex >= 0 && ddlReport.SelectedIndex > 0)
            //{

                if (!txtFDate.Text.Equals(string.Empty))
                {
                    if (DateTime.Compare(Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text)) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('From Date Can Not Be Greater Than to Date.');", true);
                        txtFDate.Focus();
                        txtFDate.Text = string.Empty;
                        txtTDate.Text = string.Empty;
                        return;
                    }
                }

                if (ddlReport.SelectedValue != "4")
                {                   
                    ShowReport("File Movement Details", "rptFileStatus.rpt");
                }
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Please Select Section And Report.", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Report_MovementReports.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnDetails = sender as Button;
            ViewState["FILE_ID"] = int.Parse(btnDetails.CommandName);     
      
            ShowReportMovementDetails("File Movement Details", "rptFileMovement.rpt");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FileMovementTracking_Master_FileMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReportMovementDetails(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_IDNO=" + Session["userno"] + "," + "@P_FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]);

            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
            }
            

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


    // this button is used to cancel your selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }     

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FileMovementTracking")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FileMovementTracking," + rptFileName;
           // url += "&param=@P_SECTION_ID=" + ddlSection.SelectedValue + "," + "@P_REPORT_VALUE=" + ddlReport.SelectedValue + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
              url += "&param=@P_SECTION_ID=" + ddlSection.SelectedValue + "," + "@P_REPORT_VALUE=" + ddlReport.SelectedValue + "," + "@P_IDNO=" + ddluser.SelectedValue + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();     //GAYATRI RODE 07-04-2022 


            if (txtFDate.Text.Trim() != string.Empty && txtTDate.Text.Trim() != string.Empty)
            {
                url += ",@P_FDATE=" + txtFDate.Text + ",@P_TDATE=" + txtTDate.Text;
            }
            else
            {
                url += ",@P_FDATE=null,@P_TDATE=null";
            }
            

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

    protected void ClearSection()    //
    {
        ddluser.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
    }

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ClearSection();
            //if (ddlReport.SelectedValue == "4")
            //{
            //    BindListViewFile();
            //}

            //if (ddlReport.SelectedValue == "5" || ddlReport.SelectedValue == "6" || ddlReport.SelectedValue == "0" || ddlReport.SelectedValue == "1" || ddlReport.SelectedValue == "2" || ddlReport.SelectedValue == "4")          //gayatri rode07-03-2022
            //{
            //    ddlSection.Visible = true;
            //    DIVSEC.Visible = true;
            //}
            //else
            //{
            //    ddlSection.Visible = false;
            //    DIVSEC.Visible = false;
            //}

            //if ( ddlReport.SelectedValue == "3")   //gayatri rode07-03-2022
            //{
            //    DIVUSER.Visible = true;
            //    ddluser.Visible = true;
            //}
            //else
            //{
            //    DIVUSER.Visible = false;
            //    ddluser.Visible = false;
            //}

            ClearSection();
            if (ddlReport.SelectedValue == "4")
            {
                BindListViewFile();
                DIVSEC.Visible = false;
                Fdate.Visible = false;
                Tdate.Visible = false;
                pnlList.Visible = true;
                btnShow.Visible = false;  //21/06/2022
            }
            else
            {
                pnlList.Visible = false;
                DIVSEC.Visible = true;
                Fdate.Visible = true;
                Tdate.Visible = true;
                btnShow.Visible = true; //21/06/2022
            }

            //-----------------------------------------------------------------------------------


            if (ddlReport.SelectedValue == "5" || ddlReport.SelectedValue == "6" || ddlReport.SelectedValue == "0" || ddlReport.SelectedValue == "1" || ddlReport.SelectedValue == "2")          //gayatri rode07-03-2022
            {
                ddlSection.Visible = true;
                DIVSEC.Visible = true;
            }
            else
            {
                ddlSection.Visible = false;
                DIVSEC.Visible = false;
            }

            if (ddlReport.SelectedValue == "3")   //gayatri rode07-03-2022
            {
                DIVUSER.Visible = true;
                ddluser.Visible = true;
               
            }
            else
            {
                DIVUSER.Visible = false;
                ddluser.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    #endregion
   
}