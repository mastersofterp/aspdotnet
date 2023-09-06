using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ADMPBranchCounselling : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    ADMPMeritListController OBJMRL = new ADMPMeritListController(); 

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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PopulateDropdown();

            }
            txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

    }


    #region User Defined Methods

    protected void PopulateDropdown()
    {
        objCommon.FillDropDownList(ddlAcadyear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
        ddlAcadyear.Items.RemoveAt(0);
        objCommon.FillDropDownList(ddlDegree, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlEntrance, "ACD_ADMP_TESTSCORE_MASTER", "SCOREID", "TESTNAME", "IsNull(ACTIVE_STATUS,0)=1", "SCOREID ASC");
        ddlEntrance.SelectedIndex = 0;
        objCommon.FillDropDownList(ddlLetter, "ACD_ADMP_LETTER_TEMPLATE", "LETTER_TEMPLATE_ID", "LETTER_TEMPLATE_NAME", "", "LETTER_TEMPLATE_ID DESC");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ADMPBranchCounselling.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ADMPBranchCounselling.aspx");
        }
    }
    #endregion


    //public DataSet GetBranch_CounsellinList(int AcadYear, int DegreeNo, int Entrance, int BRANCHNO, int ROUNDNO, int LETTER_TYPE_ID, DateTime Fromdate, DateTime Enddate , DateTime GENERATEDON)
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[9];
    //        objParams[0] = new SqlParameter("@P_ADMBATCH", AcadYear);
    //        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
    //        objParams[2] = new SqlParameter("@P_EXAMNO", Entrance);
    //        objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
    //        objParams[4] = new SqlParameter("@P_ROUNDNO", ROUNDNO);
    //        objParams[5] = new SqlParameter("@P_LETTER_TYPE_ID", LETTER_TYPE_ID);
    //        objParams[6] = new SqlParameter("@P_OFFER_FROM_DATE", Fromdate);
    //        objParams[7] = new SqlParameter("@P_OFFER_END_DATE", Enddate);
    //        objParams[8] = new SqlParameter("@P_GENERATEDON", GENERATEDON);
    //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_GET_BRANCH_COUNSELING", objParams);

    //    }
    //    catch (Exception ex)
    //    {

    //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
    //    }
    //    return ds;
    //}


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "VW_ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
    }


    protected void BindCounselling(int AcadYear, int DegreeNo, int Entrance, int BRANCHNO, int ROUNDNO, int LETTER_TYPE_ID,DateTime GENERATEDON)
    {
        DataSet ds = OBJMRL.GetBranch_CounsellinList(AcadYear, DegreeNo, Entrance, BRANCHNO, ROUNDNO, LETTER_TYPE_ID, GENERATEDON);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // pnlStudents.Visible = true;
            lvBranch_Counselling.DataSource = ds.Tables[0];
            lvBranch_Counselling.DataBind();
            lvBranch_Counselling.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("Record not found", this.Page);
            lvBranch_Counselling.DataSource = null;
            lvBranch_Counselling.DataBind();
            lvBranch_Counselling.Visible = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            BindCounselling(Convert.ToInt32(ddlAcadyear.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlEntrance.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlRound.SelectedValue), Convert.ToInt32(ddlLetter.SelectedValue),Convert.ToDateTime(txtDate.Text));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_GenerateOfferLetter.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        
    }
    private void ClearControls()
    {
        ddlEntrance.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlAcadyear.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlLetter.SelectedIndex = 0;
        ddlRound.SelectedIndex = 0;
        //ddlAcadyear.Items.Clear();
        //ddlAcadyear.Items.Add(new ListItem("Please Select", "0"));
        lvBranch_Counselling.Visible = false;


    }
}
