using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class TRAININGANDPLACEMENT_Reports_TPStudInfoList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
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
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                // Check User Authority 
                this.CheckPageAuthorization(); 
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                this.FillDropdownList();
               // Fill_Branch();
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO NOT IN(0)", "DEGREENO");
                //ViewState["IDNO"] = string.Empty;
            }
        }
        divMsg.InnerHtml = string.Empty;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TpStudInfoList.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TpStudInfoList.aspx");
        }
    }
    private void FillDropdownList()
    {
        //Fill Dropdown session 
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc"); //--AND FLOCK = 1
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
    }

    #region Other Events
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                ClearAllDropDowns();
                ddlDegree.SelectedIndex = 0;
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAllDropDowns();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue, "BRANCHNO");
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAllDropDowns();
            if (ddlBranch.SelectedIndex > 0)
            {
                //if (ddlBranch.SelectedValue == "99")
                //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
                //else
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        

        if (ddlScheme.SelectedIndex > 0)
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + ddlScheme.SelectedValue + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            //Bindlist();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    private void ClearAllDropDowns()
    {
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //chk report TPStudInfoMech
            //if (ddlBranch.SelectedValue == "5")
            //{
            //    ShowReport("StudList_M", "TPStudInfoMech.rpt");
            //}
            //else 
            //{
                ShowReport("StudList_O", "TPStudInfoOther.rpt");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_TPStudInfoList.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            //ds = objTP.GetStudentInfoForCompNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt16(chkTpReg.Checked));
            ds = GetStudentInfoForCompNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt16(chkTpReg.Checked));
            ds.Tables[0].Columns.Remove("RES_DECL_STATUS_CNT");
            ds.Tables[0].Columns.Remove("OBTD_MARKS");
            ds.Tables[0].Columns.Remove("OUTOFMARKS");
            ds.Tables[0].Columns.Remove("IDNO");
            ShowReportExcel("xls", "Student_Database_Report_For_"+ddlBranch.SelectedItem.Text.Replace(" ","_"), ds);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_TPStudInfoList.btnExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnCan_Click(object sender, EventArgs e)
    {
        if (ddlSession.Items.Count>0)
            ddlSession.SelectedIndex = 0;
        if (ddlDegree.Items.Count > 0)
            ddlDegree.SelectedIndex = 0;
        if (ddlBranch.Items.Count > 0)
            ddlBranch.SelectedIndex = 0;
        if (ddlScheme.Items.Count > 0)
            ddlScheme.SelectedIndex = 0;
        if (ddlSemester.Items.Count > 0)
            ddlSemester.SelectedIndex = 0;
    }

    public DataSet GetStudentInfoForCompNew(int sessionNo, int schemeNo, int semesterno, int tpReg)
    {
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        DataSet ds = null;
        //try
        //{
        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        SqlParameter[] objParams = null;
        objParams = new SqlParameter[4];
        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
        objParams[3] = new SqlParameter("@P_ALL_OR_TPREGISTERED", tpReg);
        // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDACADINFO_FOR_COMPANY", objParams);

        SqlConnection conn = new SqlConnection(_nitprm_constr);
        SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STUDINFO_FOR_COMPANY", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 2000;
        int i;
        for (i = 0; i < objParams.Length; i++)
            cmd.Parameters.Add(objParams[i]);
        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
        }

        //}
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentInfoForCompNew-> " + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            if (ddlBranch.SelectedValue == "5")
            {
                //url += "&param=@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_STUDENT_TYPE=" + Convert.ToChar(ddlStudType.SelectedValue) + ",@P_BRANCHNAME=" + Convert.ToString(ddlBranch.SelectedItem.Text) + ",Username=" + Convert.ToString(Session["userfullname"]) ; 
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_BRANCHNAME=" + Convert.ToString(ddlBranch.SelectedItem.Text) + ",Username=" + Convert.ToString(Session["userfullname"]); 
            }
            else 
            {
                //url += "&param=@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_STUDENT_TYPE=" + Convert.ToChar(ddlStudType.SelectedValue) + ",@P_BRANCHNAME=" + Convert.ToString(ddlBranch.SelectedItem.Text) + ",Username=" + Convert.ToString(Session["userfullname"]);   
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_BRANCHNAME=" + Convert.ToString(ddlBranch.SelectedItem.Text) + ",Username=" + Convert.ToString(Session["userfullname"]);   
            }
            url = url.Replace(" & ", " and ");

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_TPStudInfoList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportExcel(string exporttype, string reportTitle, DataSet ds)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + reportTitle + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
