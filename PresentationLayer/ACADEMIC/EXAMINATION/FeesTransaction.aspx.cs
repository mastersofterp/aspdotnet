using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_EXAMINATION_FeesTransaction : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    CourseController objCourse = new CourseController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //if ((Session["dec"].ToString() == "1") || (Session["usertype"].ToString() == "1"))
               // {
                    this.FillDropDownList();
                    this.GetFeesDetails();
               // }
               // else
                  //  Response.Redirect("~/notauthorized.aspx?page=LockUnlockMarksByStudent.aspx");
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int feeitem = Convert.ToInt32(ddlfeeitem.SelectedValue);     
        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int subjecttype = Convert.ToInt32(ddlsubjecttype.SelectedValue);
        string amount = txtAmount.Text.Trim();
      
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            //Edit 

            //CustomStatus cs = (CustomStatus)objCourse.UpdateFeesTransaction(Convert.ToInt32(ViewState["ID"]),amount);
            CustomStatus cs = (CustomStatus)objCourse.InsertTransaction(feeitem, Sessionno, amount, subjecttype, Convert.ToInt32(ViewState["ID"]));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                
                objCommon.DisplayMessage(this.updpnlExam, "Record Updated sucessfully", this.Page);
                GetFeesDetails();
                this.Clear();

            }                        
        }
        else
        {
            //Add New
            CustomStatus cs = (CustomStatus)objCourse.InsertTransaction(feeitem, Sessionno, amount, subjecttype, Convert.ToInt32(ViewState["ID"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {               
                objCommon.DisplayMessage(this.updpnlExam, "Record add sucessfully", this.Page);
               this.GetFeesDetails();
               this.Clear();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(this.updpnlExam, "Record Updated sucessfully", this.Page);
                GetFeesDetails();
                this.Clear();

            }

            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Updation failed", this.Page);
                GetFeesDetails();
                this.Clear();
            }
           

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
    }


    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc");        
        objCommon.FillDropDownList(ddlfeeitem, "ACD_Fees_Master", "FeeItemId", "FeeItemName", "FeeItemId > 0", "FeeItemId");      
        objCommon.FillDropDownList(ddlsubjecttype, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
    
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeesTransaction.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeesTransaction.aspx");
        }
    }
    private void Clear()
    {
        ddlfeeitem.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlsubjecttype.SelectedIndex = 0;
        txtAmount.Text = string.Empty;
        ViewState["action"] = null;
        ddlSession.Enabled = true;
        ddlfeeitem.Enabled = true;
        ddlsubjecttype.Enabled = true;


    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();   
           // url += "&param=@P_COLLEGE_CODE=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegreeReport.SelectedValue + ",@P_BRANCHNO=" + ddlBranchReport.SelectedValue + ",@P_SEMESTERNO=" + ddlSemReport.SelectedValue + ",@P_EXAM_TT_TYPE=" + ddlExmType.SelectedValue + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlregbac.SelectedValue);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("Fee Definition Report", "rptfeesdefinitionreport.rpt");
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int FeeItemTransId = int.Parse(btnEdit.CommandArgument);
           // ViewState["sessionno"] = int.Parse(btnEdit.CommandArgument);

            ViewState["edit"] = "edit";
            this.ShowDetails(FeeItemTransId);
            ddlSession.Enabled = false;
            ddlfeeitem.Enabled = false;
            ddlsubjecttype.Enabled = false;
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(int FeeItemTransId)
    {
        try
        {

            SqlDataReader dr = objCourse.GetFeesDefinitionDetails(FeeItemTransId);
            if (dr != null)
            {
                if (dr.Read())
                {


                    int id = Convert.ToInt32(dr["FeeItemTransId"]);
                      ViewState["ID"] = Convert.ToInt32(id);
                    string amount = dr["AMOUNT"] == null ? string.Empty : dr["AMOUNT"] .ToString();
                    if (amount.Contains('.'))
                    {
                        int index = amount.IndexOf('.');
                        string result = amount.Substring(0, index);
                        txtAmount.Text = result;
                    }
                    if (dr["SESSIONNO"] == null | dr["SESSIONNO"].ToString().Equals(""))
                        ddlSession.SelectedIndex = 0;
                        
                    else
                        ddlSession.Text = dr["SESSIONNO"].ToString();

                    if (dr["SUBJECTTYPE"] == null | dr["SUBJECTTYPE"].ToString().Equals(""))
                        ddlsubjecttype.SelectedIndex = 0;
                    else
                        ddlsubjecttype.Text = dr["SUBJECTTYPE"].ToString();

                    if (dr["FeeItemId"] == null | dr["FeeItemId"].ToString().Equals(""))
                        ddlfeeitem.SelectedIndex = 0;
                    else
                        ddlfeeitem.Text = dr["FeeItemId"].ToString();
                    

                }

            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }



    private void GetFeesDetails()
    {
        DataSet ds =objCourse.GetFeeDetails();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvFeeItem.DataSource = ds;
            lvFeeItem.DataBind();
            pnlfeeItem.Visible=true;
            lvFeeItem.Visible = true;

            }        
        
    }
}