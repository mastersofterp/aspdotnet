using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_ACPC_DCR_CREATION : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController objFee = new FeeCollectionController();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                    lvStudent.Visible = false;
                    btnAd.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ACPC_DCR_CREATION.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ACPC_DCR_CREATION.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlcategory, "acd_srcategory", "srcategoryno", "srcategory", "srcategoryno > 0 and srcategoryno=5", "srcategoryno");
        this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        ddlcategory.Items.Insert(ddlcategory.Items.Count, new ListItem("CET", ddlcategory.Items.Count.ToString()));
        ddlcategory.Items.Insert(ddlcategory.Items.Count, new ListItem("Scholarship", ddlcategory.Items.Count.ToString()));
    }


    #endregion
    private void BindListView()
    {
        try
        {
            DataSet ds = null;

            ds = objFee.Get_ACPC_STUDENT_FOR_DCR_ENTRY(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),ddlReceipt.SelectedItem.Text,Convert.ToInt32(ddlcategory.SelectedValue),ddlcolcodejss.SelectedItem.Text);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnAd.Visible = true;
                lvStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                pnlCourse.Visible = true;
               
            }

            else
            {
                btnAd.Visible = false;
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //btnAd.Visible = false;
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            btnAd.Visible = false;
            if (ddlCollege.SelectedIndex > 0)
            {
                ddlDegree.Items.Clear();
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
                ddlDegree.Focus();
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage(this.updpnl, "Please select College/School Name.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
            btnAd.Visible = false;
            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranch.Items.Clear();
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
                ddlBranch.Focus();
                
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage(this.updpnl, "Please select Degree.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.ddlDegree_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0 && ddlAdmBatch.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0 && ddlReceipt.SelectedIndex>0 && ddlcategory.SelectedIndex>0 && ddlcolcodejss.SelectedIndex>0)

            {
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Please select all mandatory fields.", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                lvStudent.Visible = false;
                btnAd.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.ddlSemester_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private ACPC Bind_AcpcData()
    {
        ACPC acp = new ACPC();
        try
        {
            acp.SessionN0 = Convert.ToInt32(ddlSession.SelectedValue);
            acp.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            acp.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            acp.RecieptCode = ddlReceipt.SelectedItem.ToString();
            acp.Branch = ddlBranch.SelectedItem.ToString();
           
            acp.IsReconciled = true;
            
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ACPC_DCR_CREATION.Bind_AcpcData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return acp;
    }
    protected void btnAd_Click(object sender, EventArgs e)
    {
        ACPC acp = this.Bind_AcpcData();
        bool flag = false;
       
        try
        {
            foreach (ListViewDataItem dataitem in lvStudent.Items)          
            {
                CheckBox chkSelect = dataitem.FindControl("chkSelect") as CheckBox;
                if (chkSelect.Checked)
                {
                    HiddenField Dmno = dataitem.FindControl("hdfDmNo") as HiddenField;
                    Label Idno = dataitem.FindControl("lblEnrollNo") as Label;
                    TextBox Remark = dataitem.FindControl("txtRemark") as TextBox;
                    Label lbltotamt = dataitem.FindControl("lbltotamt") as Label;

                    
                    acp.IdNo = Idno.ToolTip;
                    acp.DemandNo = Dmno.Value;
                    acp.Remark = Remark.Text;
                    acp.Amount = lbltotamt.Text;
                    flag = true;
                    CustomStatus cs = (CustomStatus)objFee.InsertACPCStudentsDetailsIntoDCR(acp);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.pnlCourse, "Records saved Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.pnlCourse, "Records Failled to save", this.Page);
                    }
                }
            }

            if (flag == false)
            {
                objCommon.DisplayMessage(this.pnlCourse, "Please select atleast single student", this.Page);
                return;
            }
           
           
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.btnAd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {           
            if (ddlAdmBatch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO = 1", "SEMESTERNO");
                objCommon.FillDropDownList(ddlReceipt, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_CODE", "RCPTTYPENO > 0", "RCPTTYPENO");
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Please select admission batch.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.ddlAdmBatch_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            btnAd.Visible = false;

            if (ddlBranch.SelectedIndex > 0)
            {
                ddlAdmBatch.Items.Clear();
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            }
            else
            {
                ddlAdmBatch.Items.Clear();
                ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
                objCommon.DisplayMessage(this.updpnl, "Please select branch.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ACPC_DCR_CREATION.ddlBranch_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO = 1", "SEMESTERNO");
        btnAd.Visible = false;
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
        Button btnPrint = sender as Button;
        int DCR_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        int idno = Convert.ToInt32(btnPrint.ToolTip);
       
       
            this.ShowReport("FeeCollectionReceipt.rpt", DCR_NO, idno, "1");
        
        }
        catch (Exception ex)
        {

        }
    }
    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(dcrNo, studentNo, copyNo);
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            ////To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        /// This report requires nine parameters. 
        /// Main report takes three params and three subreport takes two
        /// params each. Each subreport takes a pair of DCR_NO and ID_NO as parameter.
        /// Main report takes one extra param i.e. copyNo. copyNo is used to specify whether
        /// the receipt is a original copy(value=1) OR duplicate copy(value=2)
        /// ADD THE PARAMETER COLLEGE CODE
        /// 

        ////string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-01" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-01";
        ////param += ",@P_DCRNO=" + dcrNo.ToString() + "*DemandDraftDetails.rpt-02" + ",@P_IDNO=" + studentNo.ToString() + "*DemandDraftDetails.rpt-02";
        ////return param;

        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
        return param;

    }

    protected void ddlcategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ddlcolcodejss.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        catch
        { 
        }
    }
    protected void ddlcolcodejss_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
        catch
        { 
        }
    }
}