// ========================================================
// MODIFY BY    : MRUNAL SINGH
// MODIFY DATE  : 02/10/2014
// DESCRIPTION  : USED TO ALLOCATE MEMBERS FOR THE COMMITEE
// ========================================================

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

public partial class MEETING_MANAGEMENT_Master_CommiteeMembers : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMC = new MeetingController();
    public static int PK_CM_ID, desig_id;
    CheckBox cbhead = null;
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
                    ViewState["action"] = "add";
                }
               
                BindlistView();
                if (Convert.ToInt32(Session["usertype"]) == 1)
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");                    
                else
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["userEmpDeptno"]) + "", "NAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
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

    

    
    protected void lvmember_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chk = e.Item.FindControl("chkRow") as CheckBox;

        if (chk.ToolTip == "True")
        {
            chk.Checked = true;
        }
        else
        {
            chk.Checked = false;
        }

        for (int i = 0; i <= lvmember.Items.Count; i++)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList ddlDesignation = (DropDownList)e.Item.FindControl("ddlDesignation");
                objCommon.FillDropDownList(ddlDesignation, "TBL_MM_COMMITEEDESIG", "PK_COMMITEEDES", "DESIGNATION", "STATUS=0", "PK_COMMITEEDES");
            }
        }

    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_MENBERDETAILS MD", "TITLE+' '+FNAME+' '+MNAME+' '+LNAME as NAME,USERID", "PK_CMEMBER", "", "PK_CMEMBER");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvmember.DataSource = ds;
                lvmember.DataBind();
                lvmember.Visible = true;
            }
            else
            {
                lvmember.DataSource = null;
                lvmember.DataBind();
                lvmember.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDesignation = sender as DropDownList;
        desig_id = Convert.ToInt32(ddlDesignation.SelectedValue);

    }

    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER", "*", "", "FK_COMMITEE=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["action"] = "edit";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (lvmember.Items.Count > 0)
                {
                    for (int i = 0; i < lvmember.Items.Count; i++)
                    {
                        int member_no = Convert.ToInt32(dr["FK_MEMBER"]);
                        CheckBox chkRow = lvmember.Items[i].FindControl("chkRow") as CheckBox;
                        HiddenField hdnmember = lvmember.Items[i].FindControl("hdnmember") as HiddenField;//member no
                        TextBox txtstartdate = lvmember.Items[i].FindControl("txtstartdate") as TextBox;//start date
                        TextBox txtenddate = lvmember.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                        DropDownList ddlDesignation = lvmember.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 

                        if (member_no == Convert.ToInt32(hdnmember.Value))
                        {
                            chkRow.Checked = true;
                            txtstartdate.Text = Convert.ToString(dr["startdate"]);
                            txtenddate.Text = Convert.ToString(dr["enddate"]);
                            ddlDesignation.SelectedValue = Convert.ToString(dr["FK_COMMITEE_DESIG"]);
                            break;
                        }

                    }
                }
            }
        }
        else
        {
            if (lvmember.Items.Count > 0)
            {
                for (int i = 0; i < lvmember.Items.Count; i++)
                {
                    CheckBox chkRow = lvmember.Items[i].FindControl("chkRow") as CheckBox;
                    TextBox txtstartdate = lvmember.Items[i].FindControl("txtstartdate") as TextBox;//start date
                    TextBox txtenddate = lvmember.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                    DropDownList ddlDesignation = lvmember.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 
                    chkRow.Checked = false;
                    txtstartdate.Text = string.Empty;
                    txtenddate.Text = string.Empty;
                    ddlDesignation.SelectedIndex = 0;
                }
            }
        }
    }

    public void Clear()
    {
        if (lvmember.Items.Count > 0)
        {
            for (int i = 0; i < lvmember.Items.Count; i++)
            {
                CheckBox chkRow = lvmember.Items[i].FindControl("chkRow") as CheckBox;
                TextBox txtstartdate = lvmember.Items[i].FindControl("txtstartdate") as TextBox;//start date
                TextBox txtenddate = lvmember.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                DropDownList ddlDesignation = lvmember.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 
                chkRow.Checked = false;
                txtstartdate.Text = string.Empty;
                txtenddate.Text = string.Empty;
                ddlDesignation.SelectedIndex = 0;
            }
        }
    }

    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        if (lvmember.Items.Count > 0)
        {
            for (int i = 0; i < lvmember.Items.Count; i++)
            {
                CheckBox chkRow = lvmember.Items[i].FindControl("chkRow") as CheckBox;
                if (chkRow.Checked == false)
                {
                    TextBox txtstartdate = lvmember.Items[i].FindControl("txtstartdate") as TextBox;//start date
                    TextBox txtenddate = lvmember.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                    DropDownList ddlDesignation = lvmember.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 
                    txtenddate.Text = string.Empty;
                    txtstartdate.Text = string.Empty;
                    ddlDesignation.SelectedIndex = 0;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            objMM.COMMITEE_NO = Convert.ToInt32(ddlCommitee.SelectedValue);
            objMM.ACTIVE = 'Y';
            objMM.AUDIT_DATE = Convert.ToDateTime(System.DateTime.Now.ToString().Trim());
            string IdNo = string.Empty;
            foreach (ListViewDataItem dti in lvmember.Items)
            {
                CheckBox chkRow = dti.FindControl("chkRow") as CheckBox;
                TextBox txtstartdate = dti.FindControl("txtstartdate") as TextBox;
                TextBox txtenddate = dti.FindControl("txtenddate") as TextBox;
                DropDownList ddldesignation = dti.FindControl("ddlDesignation") as DropDownList;
                HiddenField Memberid = dti.FindControl("hdnmember") as HiddenField;

                if (chkRow.Checked)
                {
                    if (txtstartdate.Text == "" || txtenddate.Text=="")
                    {
                        objCommon.DisplayMessage(this.updActivity, "Please Select Date.", this.Page); //18/11/2021
                        return;
                    }
                    else if (ddldesignation.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage(this.updActivity, "Please Select Designation.", this.Page); //18/11/2021
                        return;
                    }
                    //---------------Shaikh Juned 08-09-2022---start---
                   // string startdate = Convert.ToDateTime(txtstartdate.Text.ToString("yyyy-MM-dd"));

                    //DataSet ds = objCommon.FillDropDown("TBL_MM_RELETIONMASTER MR INNER JOIN TBL_MM_MENBERDETAILS MM ON (MR.FK_MEMBER=MM.PK_CMEMBER)", "MR.*", " MR.startdate,MR.enddate", " MR.startdate >= '" + Convert.ToDateTime(txtstartdate.Text).ToString("yyyy-MM-dd") + "' and MR.enddate <= '" + Convert.ToDateTime(txtenddate.Text).ToString("yyyy-MM-dd") + "'AND FK_MEMBER='" + Convert.ToInt32(Memberid.Value) + "'", "");
                    //if (ds.Tables[0].Rows.Count != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //    //{
                    //    //    int COMMITEE =Convert.ToInt32( dr["FK_COMMITEE"]);
                    //    //    int MEMBER = Convert.ToInt32(dr["FK_MEMBER"]);
                    //    //    // string ORDNO = dr["ORDNO"].ToString();
                    //    //    if (COMMITEE == Convert.ToInt32(ddlCommitee.SelectedValue) && MEMBER == Convert.ToInt32(Memberid.Value))
                    //    //    {
                    //    objCommon.DisplayMessage(this.Page, "Member Is Already Exist Another Committee.", this.Page);
                    //    return;
                    //    //    }


                    //    //}
                    //}
                   


                    //----------------end------------
                    if (IdNo.Equals(string.Empty))
                    {
                        IdNo = chkRow.ToolTip;
                    }
                    else
                    {
                        IdNo = IdNo + "," + chkRow.ToolTip;
                    }
                }
            }
            if (IdNo.Equals(string.Empty))
            {
                //objCommon.DisplayMessage(this, "Please Select Member.", this.Page);

                objCommon.DisplayMessage(this.updActivity, "Please Select Member.", this.Page); //18/11/2021
                return;
            }
            DataSet dsid = null;
            string ua_no = string.Empty;
            string UANO = string.Empty;
            dsid = objCommon.FillDropDown("TBL_MM_MENBERDETAILS M INNER JOIN USER_ACC U ON (M.USERID=U.UA_IDNO)", "USERID", "UA_NO", "PK_CMEMBER IN( " + IdNo + ") AND UA_TYPE  IN (3,5)", "");
            for (int i = 0; i < dsid.Tables[0].Rows.Count; i++)
            {
                ua_no = dsid.Tables[0].Rows[i]["UA_NO"].ToString();
                if (UANO.Equals(string.Empty))
                {
                    UANO = ua_no;
                }
                else
                {
                    UANO = UANO + ',' + ua_no;
                }

            }
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objMM.COMMITEE_NO = Convert.ToInt32(ddlCommitee.SelectedValue);
                        cs = (CustomStatus)objMC.Delete_Commitee_Member(objMM);
                        if (cs.Equals(CustomStatus.RecordDeleted))
                        {
                            ViewState["action"] = "edit";
                        }
                    }
                    if (lvmember.Items.Count > 0)
                    {
                        for (int i = 0; i < lvmember.Items.Count; i++)
                        {
                            CheckBox chkRow = lvmember.Items[i].FindControl("chkRow") as CheckBox;
                            HiddenField hdnmember = lvmember.Items[i].FindControl("hdnmember") as HiddenField;//member no
                            TextBox txtstartdate = lvmember.Items[i].FindControl("txtstartdate") as TextBox;//start date
                            TextBox txtenddate = lvmember.Items[i].FindControl("txtenddate") as TextBox;//txtenddate 
                            DropDownList ddlDesignation = lvmember.Items[i].FindControl("ddlDesignation") as DropDownList;//txtenddate 
                            HiddenField hdnUserid = lvmember.Items[i].FindControl("hdnUserid") as HiddenField; // useid

                            objMM.MEMBER_NO = Convert.ToInt32(hdnmember.Value);
                            //   objMM.USERID = Convert.ToInt32(hdnUserid.Value);
                            if (chkRow.Checked == true)
                            {
                                int j = Convert.ToInt32(chkRow.ToolTip);
                                j = j - 1;
                                if (txtstartdate.Text == string.Empty || txtenddate.Text == string.Empty || ddlDesignation.SelectedIndex <= 0)
                                {
                                    //Modified by Saahil Trivedi 20-01-2022
                                    //objCommon.DisplayMessage(this.updActivity, "Please Enter Dates.", this.Page);
                                    //objCommon.DisplayMessage(this, "Please Enter Dates And Select Designation.", this.Page);
                                   // return;
                                }
                                objMM.STARTDATE = txtstartdate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtstartdate.Text.Trim());
                                objMM.ENDDATE = txtenddate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtenddate.Text.Trim());
                                //objMM.DESIGNATION_ID = Convert.ToInt32(desig_id);
                                objMM.DESIGNATION_ID = Convert.ToInt32(ddlDesignation.SelectedValue);
                                objMM.COMMITEE_NO = Convert.ToInt32(ddlCommitee.SelectedValue);
                                objMM.DEPTNO = Convert.ToInt32(Session["UA_EmpDeptNo"]);// Convert.ToInt32(Session["userEmpDeptno"]);                          
                                cs = (CustomStatus)objMC.AddUpdate_Comittee_Member(objMM);
                            }
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            ViewState["action"] = "add";



                           //UANO = objCommon.LookUp("USER_ACC", "UA_NO", "UA_TYPE=8 AND UA_EMPDEPTNO="+Convert.ToInt32(ddlCommitee.SelectedValue)+"");
                            if (UANO != "")
                            {
                                CustomStatus css = (CustomStatus)objMC.UpdateMemberRightsOfEmp(UANO);
                                if (css.Equals(CustomStatus.RecordSaved))
                                    objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                            }
                            if (UANO == "")  //shaikh juned 27-06-2022 --start
                            {
                                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                            }   //shaikh juned 27-06-2022 --end
                            ddlCommitee.SelectedIndex = 0;
                            Clear();
                        }
                    }
                }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCommitee.SelectedIndex = 0;
        Clear();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCommitee.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Committee.", this.Page);
                //objCommon.DisplayMessage(this, "Please Select Committee.", this.Page);
            }
            else
            {
                ShowReport("pdf", "CommitteeMemberList.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.btnPrint_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=Member List" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_COMMITTEE_TYPE=" + ddlCommitee.SelectedValue;

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (rdbCommitteeType.SelectedValue == "U")
            //{
            //    trCollegeName.Visible = false;
            //    ddlCollege.SelectedIndex = 0;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =0", "NAME");
            //}
            //else
            //{
            //    trCollegeName.Visible = true;
            //    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommiteeMembers.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue), "NAME");
    }


}
