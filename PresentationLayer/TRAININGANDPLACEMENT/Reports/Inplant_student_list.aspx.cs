using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Reports_Inplant_student_list_new : System.Web.UI.Page
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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["TPSession"] = objCommon.LookUp("ACD_REFE", "ISNULL(TP_CURRENT_SESSIONNO,0)AS TP_CURRENT_SESSIONNO", "");

                //objCommon.FillDropDownList(ddlStudent, "acd_student",
                  //                        "IDNO", "STUDNAME", "IDNO IN (SELECT DISTINCT IDNO FROM ACAD_TP_INPLANT_COMPANYS)", "STUDNAME");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREECODE", "DEGREENO>0", "DEGREENO");
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");


                //txtFromDt.Text = DateTime.Today.Date.ToString();
                //txtTodt .Text = DateTime.Today.Date.ToString();
  
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Forwarding_Letter", "tp_forwarding_letter.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        //ddlDegree.SelectedIndex=0;
        //ddlBranch.Items.Clear();
        //ddlStudent.Items.Clear();
        //ddlComp.Items.Clear();
        //ddlSem.SelectedIndex = 0;
    }
    
    protected void btnStudList_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex < 1)
            {
                objCommon.DisplayMessage("Please Select Degree.", this.Page);
                return;
            }
            if (ddlBranch .SelectedIndex < 1)
            {
                objCommon.DisplayMessage("Please Select Branch.", this.Page);
                return;
            }
            ShowStudListReport("Inplant_Student_List", "Ip_CompStudentList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.btnStudList_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string idnos = string.Empty;
        foreach (ListViewItem item in lvStudent.Items)
        {
            CheckBox c =(CheckBox) item.FindControl("chkStud");
            if (c.Checked)
            {
                idnos = idnos +Convert.ToInt32( c.ToolTip)+ ",";
            }
        }
        if (idnos != string.Empty)
        {
            CustomStatus cs = (CustomStatus)objTP.UpdateLetterGeneStud(idnos);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Records Updated Successfully..!", this);
                //clearLetterGeneList();
                clear();
                ddlDegree.SelectedIndex = 0;
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select Students For Which Letter Has Been Generated..!", this);
        }
        
    }

    private void ShowStudListReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            url += "&param=username=" + Session["userfullname"].ToString() + ",@P_BRANCHNO=" + Convert.ToInt16(ddlBranch.SelectedValue);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.ShowStudListReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clearLetterGeneList()
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnSubmit.Visible = false;
    }

    protected void clear()
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnSubmit.Visible = false;

        if (ddlDegree.Items.Count>0)
        ddlDegree.SelectedIndex = 0;
        if (ddlBranch.Items.Count > 0)
        ddlBranch.SelectedIndex = 0;
        if (ddlSem.Items.Count > 0)
        ddlSem.SelectedIndex = 0;
        if (ddlStudent.Items.Count > 0)
        ddlStudent.SelectedIndex=0;
        if (ddlComp.Items.Count > 0)
        ddlComp.SelectedIndex = 0;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //fromdt todt
            DataSet ds = objCommon.FillDropDown("ACAD_TP_INPLANT_COMPANYS", "CONVERT(NVARCHAR(40),FROMDT,106)FROMDT", "CONVERT(NVARCHAR(40),TODT,106)TODT", "COMPANYID=" + Convert.ToInt32(ddlComp.SelectedValue), "");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            url += "&param=@COMPID=" + Convert.ToInt32(ddlComp.SelectedValue) + ",@FileName=" + "" + ",@Date=" + ds.Tables[0].Rows[0]["FROMDT"].ToString() + " To " + ds.Tables[0].Rows[0]["TODT"].ToString() + ",@Today_date=" + DateTime.Today.ToShortDateString();
            
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            
            //url += "&param=@COMPID=" + Convert.ToInt32(ddlComp.SelectedValue) + ",@FileName=" + txtFileNm.Text.Trim().ToString() + ",@Date=" + txtFromDt.Text.ToString() + " To " + txtTodt.Text.ToString()+",@Today_date="+DateTime.Today.ToShortDateString();
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlComp.Items.Clear();
        if (ddlStudent.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlComp, "ACAD_TP_INPLANT_COMPANYS", "COMPANYID", "COMPNAME", "IDNO=" + Convert.ToInt32(ddlStudent.SelectedValue), "COMPNAME");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                clearLetterGeneList();

                ddlBranch.Items.Clear();
                ddlStudent.Items.Clear();
                ddlComp.Items.Clear();
                ddlSem.SelectedIndex = 0;

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt16(ddlDegree.SelectedValue), "LONGNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearLetterGeneList();
        ddlSem.SelectedIndex = 0;

        //ddlStudent.Items.Clear();
        //ddlComp.Items.Clear();

        //if (ddlBranch.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlStudent, "acd_student",
        //                        "IDNO", "STUDNAME", "IDNO IN (SELECT DISTINCT IDNO FROM ACAD_TP_INPLANT_COMPANYS)AND BRANCHNO=" + Convert.ToInt16(ddlBranch.SelectedValue), "STUDNAME");
        //    //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt16(ddlDegree.SelectedValue), "LONGNAME");
        //}
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                ddlStudent .Items.Clear();
                objCommon.FillDropDownList(ddlStudent, "acd_student",
                                    "IDNO", "STUDNAME", "IDNO IN (SELECT DISTINCT IDNO FROM ACAD_TP_INPLANT_COMPANYS WHERE LETTER_GENERATE<>1 OR LETTER_GENERATE IS NULL)AND BRANCHNO=" + Convert.ToInt16(ddlBranch.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt16(ddlSem.SelectedValue), "STUDNAME");
                
                DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACAD_TP_INPLANT_COMPANYS C ON (S.IDNO=C.IDNO)",
                                    "DISTINCT S.IDNO", "S.STUDNAME,ISNULL(LETTER_GENERATE,0)LETTER_GENERATE", "S.IDNO IN (SELECT DISTINCT IDNO FROM ACAD_TP_INPLANT_COMPANYS)AND BRANCHNO=" + Convert.ToInt16(ddlBranch.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt16(ddlSem.SelectedValue), "STUDNAME");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds.Tables[0];
                    lvStudent.DataBind();
                    btnSubmit.Visible = true;

                }
                else
                {
                    clearLetterGeneList();
                }
                ddlStudent.Focus();
            }
            else
            {
                ddlStudent.Items.Clear();
                ddlStudent.Items.Add("Please Select");
            }

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Inplant_Student_List.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
