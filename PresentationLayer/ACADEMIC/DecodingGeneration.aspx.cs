//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EXAMINATION                                                             
// PAGE NAME     : CODING DECODING GENERATION                                           
// CREATION DATE : 16-APRIL-2019                                                          
// CREATED BY    : ROHIT KUMAR TIWARI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_DecodingGeneration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objStudent = new StudentController();
    //ConnectionString
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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

                //lblPreSession.Text = Session["currentsession"].ToString();
                string session;   //Added by Injamam On 18-4-23
                if(!string.IsNullOrEmpty(Session["currentsession"].ToString()))
                {
                    session=Session["currentsession"].ToString();
                }else
                {
                    session="0";
                }

                //DataTableReader dtr = objCommon.FillDropDown("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO = " + Session["currentsession"].ToString(), string.Empty).CreateDataReader();
                DataTableReader dtr = objCommon.FillDropDown("ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO = " + session, string.Empty).CreateDataReader();
                if (dtr.Read())
                {
                    lblPreSession.Text = dtr["SESSION_NAME"].ToString();
                    lblPreSession.ToolTip = dtr["SESSIONNO"].ToString();
                }
                dtr.Close();

                PopulateDropDown();
                btnGenNo.Enabled = false;
                btnLock.Enabled = false;
                btnReport.Enabled = false;

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            //}
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DecodingGeneration.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Fill Dropdown Session
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //Fill Dropdown Scheme
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnGenNo_Click(object sender, EventArgs e)
    {
        try
        {
            string recordCount = objCommon.LookUp("ACD_LOG_RANDOMNO WITH (NOLOCK)", "COUNT(*)", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue);
            if (recordCount == "0")
            {
                CustomStatus cs = (CustomStatus)objStudent.GenerateDecodeNumber(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlDigits.SelectedValue), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"].ToString()), Session["colcode"].ToString());
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Decode Number Generate Successfully", this);
                    BindListView();
                    //Clear();

                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Deocde Number Generation Process Not Done", this);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Decode Number Already Exits For Selected Course", this);
                Clear();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.btnGenNo_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void Clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlDigits.SelectedIndex = 0;
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex <= 0 || ddlBranch.SelectedIndex <= 0 || ddlCourse.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage("Please Select Session/Branch/Course for Locking", this.Page);
            return;
        }

        int lck = 1;    //lock = true
        CustomStatus cs = (CustomStatus)objStudent.UpdateLockDecodeNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), lck);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage("Decoded Numbers Locked for Selected Course!!", this);
            Clear();
        }
        else
        {
            objCommon.DisplayMessage("Error!!", this);
        }


        BindListView();
    }

    private void BindListView()
    {
        DataSet ds = null;
        if (ddlBranch.SelectedValue != "99")
            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK)	ON (SR.IDNO = S.IDNO) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO ", "SR.IDNO", "C.COURSE_NAME , SR.REGNO,SR.ROLL_NO,SR.SESSIONNO,SR.SEATNO,SR.DECODENO,SR.EXTERMARK", "ISNULL(CANCEL,0)=0 and EXAM_REGISTERED=1 AND ISNULL(DETAIND,0)=0  AND SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND BRANCHNO=" + ddlBranch.SelectedValue, "SR.REGNO");
        else
            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON SR.COURSENO = C.COURSENO", "SR.IDNO", "C.COURSE_NAME ,SR.REGNO,SR.SESSIONNO,SR.SEATNO,SR.DECODENO,SR.EXTERMARK", "isnull(cancel,0)=0 and EXAM_REGISTERED=1 AND ISNULL(DETAIND,0)=0  AND SESSIONNO = " + ddlSession.SelectedValue + " AND C.COURSENO = " + ddlCourse.SelectedValue + "AND BRANCHNO=" + ddlBranch.SelectedValue + "AND SR.SEATNO IS NOT NULL ", "right(sr.seatno,3)");

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDecodeNo.DataSource = ds;
                lvDecodeNo.DataBind();
                btnLock.Enabled = true;
            }
            else
                btnLock.Enabled = false;
        }
        else
            btnLock.Enabled = false;


        string cnt = string.Empty;
        if (ddlBranch.SelectedValue == "99")
            cnt = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SR.IDNO = S.IDNO)", "DISTINCT (CASE WHEN DECODENOLOCK IS NULL THEN 0 ELSE DECODENOLOCK END) AS DECODENOLOCK", "SR.EXAM_REGISTERED = 1 AND isnull(cancel,0)=0 and  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue);
        else
            cnt = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON (SR.IDNO = S.IDNO)", "DISTINCT (CASE WHEN DECODENOLOCK IS NULL THEN 0 ELSE DECODENOLOCK END) AS DECODENOLOCK", "SR.EXAM_REGISTERED = 1 AND isnull(cancel,0)=0 and SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue);

        if (cnt == "1")
            btnLock.Enabled = false;
        else
            btnLock.Enabled = true;

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListView();

        //Get Total & Absent Students..
        string tot = string.Empty;
        string abs = string.Empty;

        if (ddlBranch.SelectedValue == "99")    //No Branch..
        {
            abs = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK)", "COUNT(*)", "(EXTERMARK = -1 OR EXTERMARK = -2.00) AND EXAM_REGISTERED = 1 AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue);
            tot = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK)", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and  COURSENO = " + ddlCourse.SelectedValue);
        }
        else
        {
            abs = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "(EXTERMARK = -1 OR EXTERMARK = -2) AND isnull(cancel,0)=0  AND ISNULL(DETAIND,0)=0 and EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue);
            tot = objCommon.LookUp("ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.IDNO=SR.IDNO", "COUNT(*)", "EXAM_REGISTERED = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ISNULL(DETAIND,0)=0 AND  COURSENO = " + ddlCourse.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue);
        }

        lblTot.Text = "Total Students : " + tot;
        lblAb.Text = "<span style='color:Red'>Absent Students : " + abs + "</span>";
        btnGenNo.Enabled = true;
        btnLock.Enabled = true;
        btnReport.Enabled = true;
    }

    protected void lvDecodeNo_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //if ((e.Item.FindControl("hdfAB") as HiddenField).Value == "-1")
        //{
        //    (e.Item.FindControl("lblABP") as Label).Text = "<span style='color:Red;font-weight:bold'>ABSENT</span>";
        //}

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            var status = e.Item.FindControl("lblABP") as Label;

            string a = status.Text;
            if (a == "-1.00")
            {
                status.Text = "ABSENT";
                status.ForeColor = System.Drawing.Color.Red;
                //status.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Bold);
                //status.ForeColor = System.Drawing.FontStyle.Bold;
            }
            else
            {
                status.Text = "";
            }

        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO=" + ddlBranch.SelectedValue, "SCHEMENO");
            }
            else
            {
                ddlScheme.SelectedIndex = 0;
                ddlScheme.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, " ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON S.IDNO=SR.IDNO", "DISTINCT COURSENO", "CCODE +' - '+ COURSENAME", " SESSIONNO=" + ddlSession.SelectedValue + "AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue, "COURSENO");

            }
            else
            {
                ddlDegree.SelectedIndex = 0;
                ddlDegree.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            }

            else
            {
                ddlDegree.SelectedIndex = 0;
                ddlDegree.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "B.LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "A.BRANCHNO");

            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                ddlBranch.Items.Add("Please Select");
            }
            lvDecodeNo.DataSource = null;
            lvDecodeNo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FinalDetaintion.ASPX.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("DecodedNumber_Report", "rptDecodeNumber.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DecodingGeneration.btnReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        string ccode = objCommon.LookUp("ACD_COURSE WITH (NOLOCK)", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_CCODE=" + ccode.ToString() + ",@P_COURSENO=" + ddlCourse.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DecodingGeneration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}