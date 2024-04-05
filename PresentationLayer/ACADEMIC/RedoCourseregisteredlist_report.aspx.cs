using IITMS;
using IITMS.UAIMS;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
public partial class ACADEMIC_RedoCourseregisteredlist_report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    AcademinDashboardController AcadDash = new AcademinDashboardController();
    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //Page Authorization
                CheckPageAuthorization();
                PopulateDropDown();
                SessionBind();
                ViewState["edit"] = "add";

                objCommon.FillDropDownList(ddlActivityName, "ACD_COURSE_ACTIVITY_TYPE_MASTER WITH (NOLOCK)", "CRS_ACTIVITY_NO", "CRS_ACTIVITY_NAME", "ISNULL(ISACTIVE,0)=1", "CRS_ACTIVITY_NO");

               // ddlSessionCollege.Focus();
                // objCommon.FillDropDownList(ddlStudentIDType, "ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO", "IDTYPEDESCRIPTION", "ISNULL(ACTIVESTATUS,0)=1", "IDTYPENO");      
            }
            divMsg.InnerHtml = string.Empty;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 22/12/2021
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 22/12/2021
           
            


        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RedoCourseregisteredlist_report.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RedoCourseregisteredlist_report.aspx.aspx");
        }
    }


    private void SessionBind()
    {
        try
        {
            objCommon.FillDropDownList(ddlSessionCollege, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC"); // ADDED ORDER BY DESC CLAUSE BY SHAILENDRA K. ON DATED 20.06.2023
        }
        catch
        {
            throw;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            ////Fill Dropdown Session 
            //string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            //DataSet dsCollegeSession = objCC.GetCollegeSession(1, college_IDs);
            //ddlCollege.Items.Clear();
            ////ddlCollege.Items.Add("Please Select");
            //ddlCollege.DataSource = dsCollegeSession;
            //ddlCollege.DataValueField = "SESSIONNO";
            //ddlCollege.DataTextField = "COLLEGE_SESSION";
            //ddlCollege.DataBind();
            //  rdbReport.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Added by Suraj on 01032024
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        


        GridView GVCreditDef = new GridView();
        string sp_Name = string.Empty; string sp_Paramters = string.Empty; string sp_Values = string.Empty;
        int sessionvalue = Convert.ToInt32(ddlSessionCollege.SelectedValue);
        string CollegeID = ddlCollege.SelectedValue;
        int ActName = Convert.ToInt32(ddlActivityName.SelectedValue);
        //sp_Name = "PKG_GET_ACD_COURSE_REG_CONFIG_ACTIVITY_DATA_DETAILS_EXCEL";
        sp_Name = "PKG_ACD_GET_ALL_EXAM_REPORT";
        sp_Paramters = " @P_SESSIONID, @P_CollegeID,@P_Act_TYPE_No";//@P_out
        sp_Values = "" + sessionvalue + "," + CollegeID + "," + ActName + "";//0
        DataSet ds = objCommon.DynamicSPCall_Select(sp_Name, sp_Paramters, sp_Values);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GVCreditDef.DataSource = ds;
            GVCreditDef.DataBind();

            string attachment = "attachment; filename=" + "RegistrationReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVCreditDef.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!!", this.Page);
        }

        
        ddlSessionCollege.Focus();


    }
    #endregion

    protected void Clear()
    {
        ddlSessionCollege.ClearSelection();
        ddlCollege.ClearSelection();
        ddlActivityName.ClearSelection();
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect(Request.Url.ToString());
        this.Clear();
        ddlSessionCollege.Focus();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }

    //private void BindDegreeWithMultipleCollege(string CollegeIds)
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)", "DISTINCT CD.DEGREENO", "D.CODE", "CM.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "CD.DEGREENO");
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //        //    chkDegreeList.DataTextField = "CODE";
    //        //    chkDegreeList.DataValueField = "DEGREENO";
    //        //    chkDegreeList.ToolTip = "DEGREENO";
    //        //    chkDegreeList.DataSource = ds.Tables[0];
    //        //    chkDegreeList.DataBind();
    //        }
    //    }
    //}


    //private DataSet BindBranchWithMultipleCollege(string degNos)
    //{
    //    string CollegeIds = GetSelectedCollegeIds();
    //    DataSet ds = new DataSet();
    //    ds = objCommon.FillDropDown("ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON CD.BRANCHNO = B.BRANCHNO INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON D.DEGREENO = CD.DEGREENO", "DISTINCT B.BRANCHNO", "B.SHORTNAME +' ('+D.CODE+')' SHORTNAME", "B.BRANCHNO >0 AND (D.DEGREENO IN (" + degNos + ") or 0 IN(" + degNos + ")) AND CD.COLLEGE_ID IN (SELECT Value FROM DBO.SPLIT( '" + CollegeIds + "',','))", "SHORTNAME");
    //    return ds;
    //}

    protected void ddlSessionCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultipleCollegeBind();
    }

    private void MultipleCollegeBind()
    {
        try
        {
            // modified by nehal [22-03-2023]
            DataSet ds = null;
            ds = AcadDash.Get_CollegeID_BySession(Convert.ToInt32(ddlSessionCollege.SelectedValue));

            ddlCollege.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.DataSource = ds;
                ddlCollege.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlCollege.DataTextField = ds.Tables[0].Columns[3].ToString();
                ddlCollege.DataBind();
            }

            //// add by maithili [07-09-2022]
            //DataSet ds = null;
            //ds = AcadDash.Get_CollegeID_Sessionno(1, "");

            //ddlCollege.Items.Clear();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlCollege.DataSource = ds;
            //    ddlCollege.DataValueField = ds.Tables[0].Columns[0].ToString();
            //    ddlCollege.DataTextField = ds.Tables[0].Columns[4].ToString();
            //    ddlCollege.DataBind();
            //}
            ////end 

            ////objCommon.FillListBox(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch
        {
            throw;
        }
    }


    //private string GetSelectedCollegeIds()
    //{
    //    string CollegeIds = string.Empty;
    //    foreach (ListItem items in ddlCollege.Items)
    //    {
    //        if (items.Selected == true)
    //        {
    //            //CollegeIds += items.Value + ',';
    //            CollegeIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
    //        }
    //    }
    //    if (CollegeIds.Length > 1)
    //    {
    //        CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
    //    }
    //    return CollegeIds;
    //}

    protected void ddlActivityName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlActivityName.SelectedIndex > 0)
        //    dvNoOfPaperAllowed.Visible = (ddlActivityName.SelectedValue == "4") ? true : false;
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Please Select Activity.!!!", this.Page);
        //    return;
        //}
    }


}