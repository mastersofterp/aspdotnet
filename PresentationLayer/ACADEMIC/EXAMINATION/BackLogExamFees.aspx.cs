//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Back LoG Exam Fees 
// CREATION DATE : 25-OCT-2019
// CREATED BY    : SUMIT KARANGALE
// MODIFIED DATE : 
// MODIFIED DESC : ADD BACK LOG EXAM FEES FOR MORE THAN THREE PAPERS
//======================================================================================


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

public partial class BackLogExamFees : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeesHeadController objFee = new FeesHeadController();

    protected void Page_PreInit(object Sender, EventArgs e)
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    this.BindFees();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    // BindListViewWorkArea();

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPWorkArea.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BackLogExamFees.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BackLogExamFees.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string IPADDRESS = Session["ipAddress"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            int perPaperFees = Convert.ToInt32(txtPerPaperFees.Text.Trim());
            int noOfPapers = Convert.ToInt32(txtNoOfPapers.Text.Trim());
            int MaxPaperFees = Convert.ToInt32(txtMoreFees.Text.Trim());

            CustomStatus cs = (CustomStatus)objFee.BackLogExamFees(perPaperFees, noOfPapers, MaxPaperFees, IPADDRESS, UA_NO);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updBacklogExamFees, "Record Saved Successfully.", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updBacklogExamFees, "Record Updated Successfully.", this.Page);
            }


        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BackLogExamFees.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

   

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindFees();
    }

    public void BindFees()
    {
        DataSet ds;
        ds = objCommon.FillDropDown("ACD_BACKLOGFEES_CONFIG", "PER_PAPER_FEES", "NO_OF_PAPERS,MORE_THAN_PAPERS_FEES", "FEE_ID=1", "");
        txtPerPaperFees.Text = ds.Tables[0].Rows[0]["PER_PAPER_FEES"].ToString();
        txtNoOfPapers.Text = ds.Tables[0].Rows[0]["NO_OF_PAPERS"].ToString();
        txtMoreFees.Text = ds.Tables[0].Rows[0]["MORE_THAN_PAPERS_FEES"].ToString();

    }
}