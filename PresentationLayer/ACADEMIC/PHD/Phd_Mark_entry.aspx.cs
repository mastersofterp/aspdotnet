using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using Mastersoft.Security.IITMS;


public partial class ACADEMIC_PHD_Phd_Mark_entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Load
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
                //   this.CheckPageAuthorization();   

                Page.Title = Session["coll_name"].ToString();

                PopulateDropDownList();
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Mark_entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page  
            Response.Redirect("~/notauthorized.aspx?page=Phd_Mark_entry.aspx");
        }
    }
    protected void PopulateDropDownList()
    {
        BindListMarkCrieria();
        //objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION_ACTIVITY", "ADMBATCH BATCHNO", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH", "ACTIVITY_STATUS=1", "ADMBATCH");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        objCommon.FillDropDownList(ddlAdmBatch1, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
        //  objCommon.FillDropDownList(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3", "CDB.BRANCHNO");
    }
    #endregion

    #region Ph.D. Mark Entry
    private void SHOW()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_PHD_STUDENTS_FOR_ENTRY";
            string SP_Parameters2 = "@P_ADMBATCH,@P_DEPARTMENT_NO,@P_PHD_MODE,@P_COLLEGE_ID";
            string Call_Values2 = "" + Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()) + "," +
                                 Convert.ToInt32(ddlprogram.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlPhDMode.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlSchool.SelectedValue.ToString()) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                Panel3.Visible = true;
                LvPhdMark.DataSource = dsStudList;
                LvPhdMark.DataBind();
                ((HtmlTableCell)LvPhdMark.FindControl("header1")).InnerText = "Test Mark (Out of " + hdftestmark.Value + ")";
                ((HtmlTableCell)LvPhdMark.FindControl("header2")).InnerText = "Interview Mark (Out of " + hdfinterview.Value + ")";
            }
            else
            {
                btnSubmit.Visible = false;
                objCommon.DisplayMessage(this.updmarkEntry, "No Record Found", this.Page);
                ddlAdmBatch.SelectedIndex = 0;
                ddlPhDMode.SelectedIndex = 0;
                ddlSchool.SelectedIndex = 0;
                ddlprogram.SelectedIndex = 0;
                Panel3.Visible = false;
                LvPhdMark.DataSource = null;
                LvPhdMark.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        SHOW();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlPhDMode.SelectedIndex = 0;
        ddlprogram.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        Panel3.Visible = false;
        btnSubmit.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            PhdController marks = new PhdController();
            int count = 0;
            CustomStatus cs = 0;
            string phdmode = "";
            string username = ""; string name = "";
            string testmark = ""; string interviewmarks = ""; string total = "";
            string USERNO = ""; string Text_Marks = string.Empty; string INTER_Marks = string.Empty;
            foreach (ListViewDataItem dataitem in LvPhdMark.Items)
            {
                CheckBox chkBoxX = dataitem.FindControl("chkallotment") as CheckBox;
                Label lblusername = dataitem.FindControl("lblusername") as Label;
                Label lblname = dataitem.FindControl("lblname") as Label;
                Label lblbranch = dataitem.FindControl("lblbranch") as Label;
                Label lblphdmode = dataitem.FindControl("lblmode") as Label;
                TextBox TxtTestmark = dataitem.FindControl("TxtTestmark") as TextBox;
                TextBox TxtInterMarks = dataitem.FindControl("TxtInterMarks") as TextBox;
                TextBox TxtTotal = dataitem.FindControl("TxtTotal") as TextBox;
                count++;
                string mark = string.Empty;
                string interview = string.Empty;
                if (TxtTestmark.Text.ToUpper() == "AB")
                {
                    mark = "0";

                }
                else
                {
                    if (TxtTestmark.Text.ToUpper() == "")
                    {
                        mark = "0";

                    }
                    else
                    {
                        mark = TxtTestmark.Text;
                    }
                   
                }
                if (TxtInterMarks.Text.ToUpper() == "AB")
                {

                    interview = "0";
                }
                else
                {
                    if (TxtInterMarks.Text.ToUpper() == "")
                    {

                        interview = "0";
                    }
                    else
                    {
                        interview = TxtInterMarks.Text;
                    }
                }
                //int num1 = Convert.ToInt32(mark);
                //int num2 = Convert.ToInt32(interview);
                decimal TOTAL = Convert.ToDecimal(mark) + Convert.ToDecimal(interview);
                if (TOTAL == Convert.ToDecimal(TxtTotal.Text))
                {
                    if (TxtTestmark.Text != "")
                    {
                        USERNO += lblusername.ToolTip + ',';
                        username += lblusername.Text.ToString() + ',';
                        name += lblname.Text.ToString() + ',';
                        total += TxtTotal.Text + ',';
                        phdmode += lblphdmode.Text + ',';
                        if (TxtTestmark.Text.ToUpper() == "AB")
                        {
                            testmark += "-1" + ',';
                        }
                        else
                        {
                            testmark += TxtTestmark.Text + ',';
                        }

                        if (TxtInterMarks.Text.ToUpper() == "AB")
                        {
                            interviewmarks += "-1" + ',';
                        }
                        else if (TxtInterMarks.Text.ToUpper() == "")
                        {
                            interviewmarks += "-0" + ',';
                        }
                        else
                        {
                            interviewmarks += TxtInterMarks.Text + ',';
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Test marks and Interview marks total are not correct", this.Page);
                    return;
                }
            }

            USERNO = USERNO.TrimEnd(',');
            username = username.TrimEnd(',');
            name = name.TrimEnd(',');
            testmark = testmark.TrimEnd(',');
            interviewmarks = interviewmarks.TrimEnd(',');
            total = total.TrimEnd(',');
            phdmode = phdmode.TrimEnd(',');
            cs = (CustomStatus)marks.PhdApplicationMarkjEntry(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])), USERNO, Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()), Convert.ToInt32(ddlprogram.SelectedValue.ToString()), phdmode, Convert.ToString(testmark), Convert.ToString(interviewmarks), Convert.ToString(total), Convert.ToString(ViewState["ipAddress"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSchool.SelectedValue.ToString()));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Marks Saved Successfully !!", this.Page);
                SHOW();
            }
            else
            {
                objCommon.DisplayMessage(this, "Error in Saving", this.Page);
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            PhdController marks = new PhdController();
            DataSet ds = marks.GetPhdMarkExcelReport(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlPhDMode.SelectedValue), Convert.ToInt32(ddlprogram.SelectedValue), Convert.ToInt32(ddlSchool.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView gvStudData = new GridView();
                gvStudData.DataSource = ds;
                gvStudData.DataBind();
                string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
                string attachment = "attachment; filename=Phd_Mark_Entry.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Response.Write(FinalHead);
                gvStudData.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updmarkEntry, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            string SP_Name2 = "PKG_PHD_GET_MARK_ENTRY_CRITERIA";
            string SP_Parameters2 = "@P_MODE,@P_ADMBATCH";
            string Call_Values2 = "2," + Convert.ToInt32(ddlAdmBatch.SelectedValue);
            DataSet dsMark = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsMark.Tables[0].Rows.Count > 0)
            {
                hdftestmark.Value = dsMark.Tables[0].Rows[0]["TEST_MARK"].ToString();
                hdfinterview.Value = dsMark.Tables[0].Rows[0]["INTERVIEW_MARK"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this, "Marks Criteria not set for this admission batch !!", this.Page);
                return;
            }
        }
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
    }

    protected void ddlPhDMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
    }

    protected void ddlprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdMark.DataSource = null;
        LvPhdMark.DataBind();
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Visible = false;
            Panel3.Visible = false;
            LvPhdMark.DataSource = null;
            LvPhdMark.DataBind();
            if (ddlSchool.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "CDB.BRANCHNO");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Something went wrong.`)", true);
            return;
        }
    }
    #endregion

    #region Ph.D. Mark Entry Criteria
    protected void ddlAdmBatch1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chkMark_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkMark.Checked == true)
        //{
        //    divTestMark.Visible = true;
        //}
        //else
        //{
        //    divTestMark.Visible = false;
        //}
    }
    protected void chkInterview_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkInterview.Checked == true)
        //{
        //    divInterviewMark.Visible = true;
        //}
        //else
        //{
        //    divInterviewMark.Visible = false;
        //}
    }

    protected void btnEntryCancel_Click(object sender, EventArgs e)
    {
        cancel();
    }
    protected void btnMarkSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtTestMark.Text != "" && txtInterviewMark.Text != "")
            {
                PhdController marks = new PhdController();
                CustomStatus cs = 0;
                int USERNO = Convert.ToInt32(Session["userno"]);
                string IPADDRESS = Convert.ToString(ViewState["ipAddress"]);
                string TestMark = Convert.ToString(txtTestMark.Text);
                string InterviewMark = Convert.ToString(txtInterviewMark.Text);
                int OrgId = Convert.ToInt32(Session["OrgId"]);
                int admbatch = Convert.ToInt32(ddlAdmBatch1.SelectedValue);
                cs = (CustomStatus)marks.PhdApplicationMarkjEntryCriteria(OrgId, admbatch, TestMark, InterviewMark, IPADDRESS, USERNO);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Marks Criteria Saved Successfully !!", this.Page);
                    BindListMarkCrieria();
                    cancel();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this, "Record Already Exist !!", this.Page);
                    BindListMarkCrieria();

                }
                else
                {
                    objCommon.DisplayMessage(this, "Error in Saving", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void cancel()
    {
        ddlAdmBatch1.SelectedIndex = 0;
        chkMark.Checked = false;
        chkInterview.Checked = false;
        txtInterviewMark.Text = string.Empty;
        txtTestMark.Text = string.Empty;
        // divInterviewMark.Visible = false;
        //  divTestMark.Visible = false;
    }
    protected void BindListMarkCrieria()
    {
        try
        {
            string SP_Name2 = "PKG_PHD_GET_MARK_ENTRY_CRITERIA";
            string SP_Parameters2 = "@P_MODE,@P_ADMBATCH";
            string Call_Values2 = "1," + Convert.ToInt32(ddlAdmBatch1.SelectedValue);
            DataSet dsMark = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsMark.Tables[0].Rows.Count > 0)
            {
                PnlCriteria.Visible = true;
                lvCriteria.DataSource = dsMark;
                lvCriteria.DataBind();
            }
            else
            {
                PnlCriteria.Visible = false;
                lvCriteria.DataSource = null;
                lvCriteria.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Phd_Mark_entry.BindListMarkCrieria-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion
}

