//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : THIS IS A Hostel Seat Quota Master                                     
// CREATION DATE : 19 March 2013
// CREATED BY    : ASHISH MOTGHARE                               
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 21 March 2013
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 22 March 2013
// MODIFIED BY   : ASHISH MOTGHARE
// MODIFIED DATE : 23 March 2013
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class HOSTEL_REPORT_Student_Quota : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRM = new RoomAllotmentController();
    SeatQuotaController objSQ = new SeatQuotaController();

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
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                //CheckPageAuthorization();
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
            trTotStud.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineApply.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_Quota.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_Quota.aspx");
        }
    }
   

    private void FillListView()
    {
        
        try
        {
            DataSet ds = objSQ.GetDegreewise(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue));

            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvBranchList.Visible = true;
                lvBranchList.DataSource = ds;
                lvBranchList.DataBind();

                if (Convert.ToInt32(objCommon.LookUp("acd_hostel_seatquota", "Count(*)", "ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue))) > 0)
                {

                    DataSet dsGetBranchwisePer = objSQ.GetDegreewisePercentage(Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue));
                    if (dsGetBranchwisePer != null & dsGetBranchwisePer.Tables.Count > 0 && dsGetBranchwisePer.Tables[0].Rows.Count > 0)
                    {
                        foreach (ListViewDataItem dataItem in lvBranchList.Items)
                        {
                            Label degree = dataItem.FindControl("lblBranch") as Label;
                            Label totstud = dataItem.FindControl("lblTotStud") as Label;
                            TextBox allindiaper = dataItem.FindControl("txtSeatQuota") as TextBox;
                            TextBox stateper = dataItem.FindControl("txtSeatQuota1") as TextBox;
                            TextBox cat1ind_per = dataItem.FindControl("txtGeneral") as TextBox;
                            TextBox cat1st_per = dataItem.FindControl("txtGeneral1") as TextBox;
                            TextBox cat2ind_per = dataItem.FindControl("txtObc") as TextBox;
                            TextBox cat2st_per = dataItem.FindControl("txtObc1") as TextBox;
                            TextBox cat3ind_per = dataItem.FindControl("txtSc") as TextBox;
                            TextBox cat3st_per = dataItem.FindControl("txtSc1") as TextBox;
                            TextBox cat4ind_per = dataItem.FindControl("txtSt") as TextBox;
                            TextBox cat4st_per = dataItem.FindControl("txtSt1") as TextBox;
                            TextBox cat5ind_per = dataItem.FindControl("txtNt") as TextBox;
                            TextBox cat5st_per = dataItem.FindControl("txtNt1") as TextBox;
                            for (int i = 0; i < dsGetBranchwisePer.Tables[0].Rows.Count; i++)
                            {
                                if (degree.ToolTip.ToString() == dsGetBranchwisePer.Tables[0].Rows[i]["DEGREENO"].ToString())
                                {

                                    if (dsGetBranchwisePer.Tables[0].Rows[i]["SEAT_QUOTA"].ToString() == "2")
                                    {
                                        cat1st_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY1_PER"].ToString();
                                        cat2st_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY2_PER"].ToString();
                                        cat3st_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY3_PER"].ToString();
                                        cat4st_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY4_PER"].ToString();
                                        cat5st_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY5_PER"].ToString();
                                    }
                                    if (dsGetBranchwisePer.Tables[0].Rows[i]["SEAT_QUOTA"].ToString() == "1")
                                    {
                                        cat1ind_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY1_PER"].ToString();
                                        cat2ind_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY2_PER"].ToString();
                                        cat3ind_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY3_PER"].ToString();
                                        cat4ind_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY4_PER"].ToString();
                                        cat5ind_per.Text = dsGetBranchwisePer.Tables[0].Rows[i]["CATEGORY5_PER"].ToString();
                                    }
                                    if (dsGetBranchwisePer.Tables[0].Rows[i]["SEAT_QUOTA"].ToString() == string.Empty)
                                    {
                                        cat1ind_per.Text = string.Empty;
                                        cat2ind_per.Text = string.Empty;
                                        cat3ind_per.Text = string.Empty;
                                        cat4ind_per.Text = string.Empty;
                                        cat5ind_per.Text = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataSet ds1 = objCommon.FillDropDown("(select BATCH_NO,QUOTA_NO,QUOTA_PER,GEN_PER,OBC_PER,SC_PER,ST_PER,NT_PER from ACD_HOSTEL_QUOTA  WHERE QUOTA_NO = 1 union	select BATCH_NO,QUOTA_NO,QUOTA_PER,GEN_PER,OBC_PER,SC_PER,ST_PER,NT_PER from ACD_HOSTEL_QUOTA  WHERE QUOTA_NO = 2 )x ", "x.QUOTA_NO", "x.*", "x.QUOTA_NO>0", "x.QUOTA_NO");
                    if (ds1 != null & ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (ListViewDataItem dataItem in lvBranchList.Items)
                        {
                            Label ALLINDIASQNO = dataItem.FindControl("lblSeatQuotaAllIndia") as Label;
                            Label STATELEVELSQNO = dataItem.FindControl("lblSeatQuotaStateLevel") as Label;
                            TextBox cat1ind_per = dataItem.FindControl("txtGeneral") as TextBox;
                            TextBox cat1st_per = dataItem.FindControl("txtGeneral1") as TextBox;
                            TextBox cat2ind_per = dataItem.FindControl("txtObc") as TextBox;
                            TextBox cat2st_per = dataItem.FindControl("txtObc1") as TextBox;
                            TextBox cat3ind_per = dataItem.FindControl("txtSc") as TextBox;
                            TextBox cat3st_per = dataItem.FindControl("txtSc1") as TextBox;
                            TextBox cat4ind_per = dataItem.FindControl("txtSt") as TextBox;
                            TextBox cat4st_per = dataItem.FindControl("txtSt1") as TextBox;
                            TextBox cat5ind_per = dataItem.FindControl("txtNt") as TextBox;
                            TextBox cat5st_per = dataItem.FindControl("txtNt1") as TextBox;
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                if (ALLINDIASQNO.ToolTip.ToString() == ds1.Tables[0].Rows[i]["QUOTA_NO"].ToString())
                                {
                                    cat1ind_per.Text = ds1.Tables[0].Rows[i]["GEN_PER"].ToString();
                                    cat2ind_per.Text = ds1.Tables[0].Rows[i]["OBC_PER"].ToString();
                                    cat3ind_per.Text = ds1.Tables[0].Rows[i]["Sc_PER"].ToString();
                                    cat4ind_per.Text = ds1.Tables[0].Rows[i]["ST_PER"].ToString();
                                    cat5ind_per.Text = ds1.Tables[0].Rows[i]["NT_PER"].ToString();

                                }
                                if (STATELEVELSQNO.ToolTip.ToString() == ds1.Tables[0].Rows[i]["QUOTA_NO"].ToString())
                                {
                                    cat1st_per.Text = ds1.Tables[0].Rows[i]["GEN_PER"].ToString();
                                    cat2st_per.Text = ds1.Tables[0].Rows[i]["OBC_PER"].ToString();
                                    cat3st_per.Text = ds1.Tables[0].Rows[i]["Sc_PER"].ToString();
                                    cat4st_per.Text = ds1.Tables[0].Rows[i]["ST_PER"].ToString();
                                    cat5st_per.Text = ds1.Tables[0].Rows[i]["NT_PER"].ToString();

                                }
                            }
                        }
                    }
                }
            }


            else
            {
                lvBranchList.Visible = false;
                lvBranchList.DataSource = null;
                lvBranchList.DataBind();
                objCommon.DisplayMessage("Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string degreeno = string.Empty;
            string seatquotaind = string.Empty;
            string seatquotast = string.Empty;
            string seatquotaind_per = string.Empty;
            string seatquotast_per = string.Empty;
            string categoryno1 = string.Empty;
            string categoryno1ind_per = string.Empty;
            string categoryno1st_per = string.Empty;
            string categoryno2 = string.Empty;
            string categoryno2ind_per = string.Empty;
            string categoryno2st_per = string.Empty;
            string categoryno3 = string.Empty;
            string categoryno3ind_per = string.Empty;
            string categoryno3st_per = string.Empty;
            string categoryno4 = string.Empty;
            string categoryno4ind_per = string.Empty;
            string categoryno4st_per = string.Empty;
            string categoryno5 = string.Empty;
            string categoryno5ind_per = string.Empty;
            string categoryno5st_per = string.Empty;
            string total_stud = string.Empty;
            int count = 0;
            foreach (ListViewDataItem dataItem in lvBranchList.Items)
            {
                
                Label degree = dataItem.FindControl("lblBranch") as Label;
                Label countStudent = dataItem.FindControl("lblCOUNT") as Label;
                Label sqind = dataItem.FindControl("lblSeatQuotaAllIndia") as Label;
                Label sqst = dataItem.FindControl("lblSeatQuotaStateLevel") as Label;

                TextBox sqind_per = dataItem.FindControl("txtSeatQuota") as TextBox;
                TextBox sqst_per = dataItem.FindControl("txtSeatQuota1") as TextBox;

                TextBox cat1 = dataItem.FindControl("txtGeneral") as TextBox;
                TextBox cat1ind_per = dataItem.FindControl("txtGeneral") as TextBox;
                TextBox cat1st_per = dataItem.FindControl("txtGeneral1") as TextBox;

                TextBox cat2 = dataItem.FindControl("txtObc") as TextBox;
                TextBox cat2ind_per = dataItem.FindControl("txtObc") as TextBox;
                TextBox cat2st_per = dataItem.FindControl("txtObc1") as TextBox;

                TextBox cat3 = dataItem.FindControl("txtSc") as TextBox;
                TextBox cat3ind_per = dataItem.FindControl("txtSc") as TextBox;
                TextBox cat3st_per = dataItem.FindControl("txtSc1") as TextBox;

                TextBox cat4 = dataItem.FindControl("txtSt") as TextBox;
                TextBox cat4ind_per = dataItem.FindControl("txtSt") as TextBox;
                TextBox cat4st_per = dataItem.FindControl("txtSt1") as TextBox;

                TextBox cat5 = dataItem.FindControl("txtNt") as TextBox;
                TextBox cat5ind_per = dataItem.FindControl("txtNt") as TextBox;
                TextBox cat5st_per = dataItem.FindControl("txtNt1") as TextBox;

                Label totstud = dataItem.FindControl("lblTotStud") as Label;
                if (sqind_per.Text != string.Empty || sqst_per.Text != string.Empty)
                {
                    degreeno += degree.ToolTip + ",";

                    seatquotaind += sqind.ToolTip + ",";
                    seatquotast += sqst.ToolTip + ",";

                    if (sqind_per.Text != string.Empty)
                    {
                        seatquotaind_per += sqind_per.Text + ",";
                    }
                    else
                    {
                        seatquotaind_per += "00.00" + ",";
                    }
                    if (sqind_per.Text != string.Empty)
                    {
                        seatquotast_per += sqst_per.Text + ",";
                    }
                    else
                    {
                        seatquotast_per += "00.00" + ",";
                    }
                    categoryno1 += cat1.ToolTip + ",";


                    if (cat1ind_per.Text != string.Empty)
                    {
                        categoryno1ind_per += cat1ind_per.Text + ",";
                    }
                    else
                    {
                        categoryno1ind_per += "00.00" + ",";
                    }
                    if (cat1st_per.Text != string.Empty)
                    {
                        categoryno1st_per += cat1st_per.Text + ",";
                    }
                    else
                    {
                        categoryno1st_per += "00.00" + ",";
                    }

                    categoryno2 += cat2.ToolTip + ",";
                    if (cat2ind_per.Text != string.Empty)
                    {
                        categoryno2ind_per += cat2ind_per.Text + ",";
                    }
                    else
                    {
                        categoryno2ind_per += "00.00" + ",";
                    }
                    if (cat2st_per.Text != string.Empty)
                    {
                        categoryno2st_per += cat2st_per.Text + ",";
                    }
                    else
                    {
                        categoryno2st_per += "00.00" + ",";
                    }

                    categoryno3 += cat3.ToolTip + ",";
                    if (cat3ind_per.Text != string.Empty)
                    {
                        categoryno3ind_per += cat3ind_per.Text + ",";
                    }
                    else
                    {
                        categoryno3ind_per += "00.00" + ",";
                    }
                    if (cat3st_per.Text != string.Empty)
                    {
                        categoryno3st_per += cat3st_per.Text + ",";
                    }
                    else
                    {
                        categoryno3st_per += "00.00" + ",";
                    }

                    categoryno4 += cat4.ToolTip + ",";
                    if (cat4ind_per.Text != string.Empty)
                    {
                        categoryno4ind_per += cat4ind_per.Text + ",";
                    }
                    else
                    {
                        categoryno4ind_per += "00.00" + ",";
                    }
                    if (cat4st_per.Text != string.Empty)
                    {
                        categoryno4st_per += cat4st_per.Text + ",";
                    }
                    else
                    {
                        categoryno4st_per += "00.00" + ",";
                    }

                    categoryno5 += cat5.ToolTip + ",";
                    if (cat5ind_per.Text != string.Empty)
                    {
                        categoryno5ind_per += cat5ind_per.Text + ",";
                    }
                    else
                    {
                        categoryno5ind_per += "00.00" + ","; 
                    }
                    if (cat5st_per.Text != string.Empty)
                    {
                        categoryno5st_per += cat5st_per.Text + ",";
                    }
                    else
                    {
                        categoryno5st_per += "00.00" + ",";
                    }

                    total_stud += countStudent.ToolTip + ",";


                    count++;
                }
                
            }
            if (degreeno.Trim().Length > '0')
            {
                degreeno = degreeno.Remove(degreeno.Length - 1);
            }
            if (count < 0)
            {
                objCommon.DisplayMessage("Please Enter Atleast one Record for Seat Quota..!!", this.Page);
                return;
            }
            CustomStatus cs = (CustomStatus)objSQ.AddUpdateSeatQuota(seatquotaind, seatquotast, seatquotaind_per, seatquotast_per, categoryno1, categoryno1ind_per, categoryno1st_per, categoryno2, categoryno2ind_per, categoryno2st_per, categoryno3, categoryno3ind_per, categoryno3st_per, categoryno4, categoryno4ind_per, categoryno4st_per, categoryno5, categoryno5ind_per, categoryno5st_per, total_stud, degreeno, Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage ("Seat Quota Alloted Successfuly..!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CopyCase.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if(ddlSemester.SelectedValue=="1" || ddlSemester.SelectedValue=="2")
            this.ShowReport("Seat_Quota_Report", "SeatQuotaForHostel.rpt");
        else
            this.ShowReport("Seat_Quota_Report", "rptSeatQuotaBranchwise.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));            

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmbatch.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "REPORTS_ACADEMIC_ResultReport.ShowCourseGraphReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    protected void btnShow_Click(object sender, EventArgs e)
    {
        int studCount = int.Parse(objCommon.LookUp("ACD_STUDENT", "COUNT(IDNO)", "ADMBATCH = " + ddlAdmbatch.SelectedValue + "  AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND CAN = '0' AND ADMCAN = '0'"));
        txtTotStud.Text = Convert.ToString(studCount);
        FillListView();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "1" && ddlSemester.SelectedValue != "2")
        {
            btnShow.Visible = false;
            btnSubmit.Visible = false;
            lvBranchList.Visible = false;
        }
        else
        {
            btnShow.Visible = true;
            btnSubmit.Visible = true;
        }
    }
}
