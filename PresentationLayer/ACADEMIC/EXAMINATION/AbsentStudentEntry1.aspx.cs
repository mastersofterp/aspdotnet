//======================================================================================
// PROJECT NAME  : RFC SVCE                                                          
// MODULE NAME   : EXAMINATION                                                             
// PAGE NAME     : Absent Student Entry1                                                    
// CREATION DATE :                                                        
// CREATED BY    :                                                      
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls.Adapters;
using DynamicAL_v2;

public partial class ACADEMIC_EXAMINATION_AbsentStudentEntry1 : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    Exam objExam = new Exam();
    DynamicControllerAL AL = new DynamicControllerAL();

    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default.aspx  PAGE
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
                    // this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //this.FillDropdown(1);
                    //this.FillDropdown(2);
                    this.FillDropdown(0);
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[2]) + " )", "SESSIONNO DESC");

                    //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT DISTINCT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='ABS')", "SESSIONNO DESC");


                    DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT DISTINCT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDs IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='ABS')", "SESSIONNO DESC");



                    if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    {
                        //btnShow.Enabled = false;
                        //objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                        //divAbs.Visible = false;
                        //return;

                        if (Convert.ToInt32(Session["usertype"]) == 1)
                        {
                            btnShow.Enabled = true;
                            divAbs.Visible = true;
                            divexamtype.Visible = true;
                        }
                        else
                        {
                            btnShow.Enabled = false;
                            objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                            divAbs.Visible = false;
                            divexamtype.Visible = false;
                            return;
                        }

                    }
                    else
                    {
                        divAbs.Visible = true;
                    }
                }

                btnLock.Enabled = btnSubmit.Enabled = btnAbsentReport1.Enabled = false;
            }
            divMsg.InnerHtml = string.Empty;
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }
    /// <summary>
    /// This FillDropdown method is Use for Binding the Session Name remark by pankaj 12/09/2019
    /// </summary>
    private void FillDropdown(int callnum)
    {
        try
        {
            //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");


            //commented by prafull on dt 25082022

            //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");






            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }




            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNAME", "EXAMNAME IS NOT NULL AND EXAMNAME!='' AND FLDNAME='EXTERMARK'", "FLDNAME"); //--AND FLOCK = 1
            btnLock.Enabled = false;
            //if (callnum == 1)
            //{
            //    objCommon.FillDropDownList(ddlsessionforabsent, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO desc"); //--AND FLOCK = 1
            //    btnLock.Enabled = false;
            //}
            //else if (callnum == 2)
            //{
            //    objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", "DISTINCT FLDNAME", "EXAMNAME", "EXAMNAME IS NOT NULL AND EXAMNAME!='' AND FLDNAME='EXTERMARK'", "FLDNAME"); //--AND FLOCK = 1
            //    btnLock.Enabled = false;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillStudentList()
    {
        try
        {
            DataSet ds;
            int sessionNo = 0;
            int semesterNo = 0;
            int collegeNo = 0;
            int courseNo = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.FillStudentList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    /// <summary>
    /// this Cancel button is used for canceling the page  remark by pankaj 12/09/2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // ddlcollege.SelectedIndex = ddlsessionforabsent.SelectedIndex = ddlExamDate.SelectedIndex = ddlexamnameabsentstudent.SelectedIndex = ddlcourseforabset.SelectedIndex = ddlSubexamnameabsentstudent.SelectedIndex = ddlexam_type.SelectedIndex = 0;
        div_Result.Visible = false;
        btnLock.Enabled = false;
        btnSubmit.Enabled = false;
        Response.Redirect(Request.Url.ToString());

    }

    /// <summary>
    /// This Submit Button is used for to Submitting the Absent student record  remark by pankaj nakhale 12/09/2019 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    private string SaveAndLock(string Lock)
    {

        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            string idno = "";
            string val = "";
            string hfd_IDNO = "";
            string hfd_SEM = "";
            string hfd_SEC = "";
            string hdf_SUBID = "";
            int examtype = Convert.ToInt32(ddlexam_type.SelectedValue);
            string Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
            string Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

            foreach (RepeaterItem item in rpt_Success.Items)
            {
                CheckBox chk_ab = (CheckBox)item.FindControl("chk_Absent");
                //CheckBox chk_uf = (CheckBox)item.FindControl("chk_Ufm");

                hfd_IDNO = ((HiddenField)item.FindControl("hdf_IDNO")).Value;
                hfd_SEM += "#" + ((HiddenField)item.FindControl("hdf_SEM")).Value;
                hfd_SEC += "#" + ((HiddenField)item.FindControl("hdf_SEC")).Value;
                hdf_SUBID += "#" + ((HiddenField)item.FindControl("hdf_SUBID")).Value;

                if (chk_ab.Checked == true)
                {
                    idno += "#" + hfd_IDNO;
                    val += "#902";
                }
                //else if (chk_uf.Checked == true)
                //{
                //    idno += "#" + hfd_IDNO;
                //    val += "#903";
                //}
                else
                {
                    idno += "#" + hfd_IDNO;
                    val += "#-1";
                }
            }


            //int ret = Convert.ToInt32(objExamController.InsertAbsentStudEntry(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[0]), ddlcourseforabset.SelectedItem.Text.Split('-')[0], idno.Substring(1, idno.Length - 1), val.Substring(1, val.Length - 1), Lock, Examname, Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[1]), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Subexamname, hfd_SEM.Substring(1, hfd_SEM.Length - 1), hfd_SEC.Substring(1, hfd_SEC.Length - 1)));
            //if (ret > 0)
            //{
            //}
            string SP_Name = "PKG_ABSENT_STUDENT_ENTRY_INSERT";
            string SP_Parameters = "@P_SESSIONNO,@P_COURSENO,@P_CCODE,@P_STUDIDS,@P_MARKS,@P_LOCK,@P_EXAM,@P_SUBID,@P_UA_NO,@P_IPADDRESS,@P_SUB_EXAM,@P_SEMESTERNO,@P_SECTIONNO,@P_EXAMTYPE,@P_OP";
            string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + "," + idno.Substring(1, idno.Length - 1) + "," + val.Substring(1, val.Length - 1) + "," + Lock + "," + Examname + "," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + Convert.ToInt32(Session["usertype"]) + "," + ViewState["ipAddress"].ToString() + "," + Subexamname + "," + hfd_SEM.Substring(1, hfd_SEM.Length - 1) + "," + hfd_SEC.Substring(1, hfd_SEC.Length - 1) + "," + examtype + ",1";
            //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",'" + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + "','" + idno.Substring(1, idno.Length - 1) + "','" + val.Substring(1, val.Length - 1) + "'," + Lock + ",'" + Examname + "'," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + Convert.ToInt32(Session["userno"]) + ",'" + ViewState["ipAddress"].ToString() + "','" + Subexamname + "','" + hfd_SEM.Substring(1, hfd_SEM.Length - 1) + "','" + hfd_SEC.Substring(1, hfd_SEC.Length - 1) + "'";

            //return;

            string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 1);

            //  string cs = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true,1);

            return (que_out);

        }
        else
        {
            string idno = "";
            string val = "";
            string hfd_IDNO = "";
            string hfd_SEM = "";
            string hfd_SEC = "";
            string hdf_SUBID = "";
            int examtype = 0;
            string Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
            string Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

            foreach (RepeaterItem item in rpt_Success.Items)
            {
                CheckBox chk_ab = (CheckBox)item.FindControl("chk_Absent");
                //CheckBox chk_uf = (CheckBox)item.FindControl("chk_Ufm");

                hfd_IDNO = ((HiddenField)item.FindControl("hdf_IDNO")).Value;
                hfd_SEM += "#" + ((HiddenField)item.FindControl("hdf_SEM")).Value;
                hfd_SEC += "#" + ((HiddenField)item.FindControl("hdf_SEC")).Value;
                hdf_SUBID += "#" + ((HiddenField)item.FindControl("hdf_SUBID")).Value;

                if (chk_ab.Checked == true)
                {
                    idno += "#" + hfd_IDNO;
                    val += "#902";
                }
                //else if (chk_uf.Checked == true)
                //{
                //    idno += "#" + hfd_IDNO;
                //    val += "#903";
                //}
                else
                {
                    idno += "#" + hfd_IDNO;
                    val += "#-1";
                }
            }


            //int ret = Convert.ToInt32(objExamController.InsertAbsentStudEntry(Convert.ToInt32(ddlsessionforabsent.SelectedValue), Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[0]), ddlcourseforabset.SelectedItem.Text.Split('-')[0], idno.Substring(1, idno.Length - 1), val.Substring(1, val.Length - 1), Lock, Examname, Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[1]), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Subexamname, hfd_SEM.Substring(1, hfd_SEM.Length - 1), hfd_SEC.Substring(1, hfd_SEC.Length - 1)));
            //if (ret > 0)
            //{
            //}
            string SP_Name = "PKG_ABSENT_STUDENT_ENTRY_INSERT";
            string SP_Parameters = "@P_SESSIONNO,@P_COURSENO,@P_CCODE,@P_STUDIDS,@P_MARKS,@P_LOCK,@P_EXAM,@P_SUBID,@P_UA_NO,@P_IPADDRESS,@P_SUB_EXAM,@P_SEMESTERNO,@P_SECTIONNO,@P_EXAMTYPE,@P_OP";
            string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + "," + idno.Substring(1, idno.Length - 1) + "," + val.Substring(1, val.Length - 1) + "," + Lock + "," + Examname + "," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + Convert.ToInt32(Session["userno"]) + "," + ViewState["ipAddress"].ToString() + "," + Subexamname + "," + hfd_SEM.Substring(1, hfd_SEM.Length - 1) + "," + hfd_SEC.Substring(1, hfd_SEC.Length - 1) + "," + examtype + ",1";
            //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",'" + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + "','" + idno.Substring(1, idno.Length - 1) + "','" + val.Substring(1, val.Length - 1) + "'," + Lock + ",'" + Examname + "'," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + Convert.ToInt32(Session["userno"]) + ",'" + ViewState["ipAddress"].ToString() + "','" + Subexamname + "','" + hfd_SEM.Substring(1, hfd_SEM.Length - 1) + "','" + hfd_SEC.Substring(1, hfd_SEC.Length - 1) + "'";

            //return;

            string que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 1);

            //  string cs = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true,1);

            return (que_out);
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //string que_out = SaveAndLock("0");
            string que_out = SaveAndLock("1");
            if (que_out == "2")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", "alert('Absent Student Added Successfully ...');", true);
            }

            btnShow_Click(sender, e);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            string que_out = SaveAndLock("1");
            if (que_out == "2")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", "alert('Absent Student Locked Successfully ...');", true);
            }
            else if (que_out == "-2")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", "alert('Can not Proceed !! Dummy Number is not generated for " + ddlcourseforabset.SelectedItem.Text + ".');", true);
                return;
            }

            btnShow_Click(sender, e);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            try
            {
                int UnLock = 0;
                int examtype = Convert.ToInt32(ddlexam_type.SelectedValue);
                string Examname, Subexamname = string.Empty;
                Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
                Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

                string SP_Name = "PKG_ABSENT_STUDENT_ENTRY";
                string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SUBID,@P_CCODE, @P_SECTIONNO, @P_EXAM, @P_SUB_EXAM,@P_UA_NO,@P_EXAMTYPE";
                string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["usertype"]) + "," + examtype + "";
                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    rpt_Success.DataSource = ds.Tables[0];
                    rpt_Success.DataBind();
                    div_Result.Visible = true;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                btnSubmit.Enabled = btnLock.Enabled = false;
                                btnAbsentReport1.Enabled = true;
                            }
                            else
                            {
                                UnLock++;
                                btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('No Students Found for " + ddlcourseforabset.SelectedItem.Text.Split('-')[1] + " !!');", true);
                        btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = false;
                    }
                }
                else
                {
                    div_Result.Visible = false;
                    btnSubmit.Enabled = btnLock.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('No Record Found !!');", true);
                }
                if (UnLock > 0)
                {
                    btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = true;
                    btnAbsentReport1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        else
        {
            try
            {
                int UnLock = 0;
                int examtype = 0;
                string Examname, Subexamname = string.Empty;
                Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
                Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

                string SP_Name = "PKG_ABSENT_STUDENT_ENTRY";
                string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SUBID,@P_CCODE, @P_SECTIONNO, @P_EXAM, @P_SUB_EXAM,@P_UA_NO,@P_EXAMTYPE";
                string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["userno"]) + "," + examtype + "";
                DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    rpt_Success.DataSource = ds.Tables[0];
                    rpt_Success.DataBind();
                    div_Result.Visible = true;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                btnSubmit.Enabled = btnLock.Enabled = false;
                                btnAbsentReport1.Enabled = true;
                            }
                            else
                            {
                                UnLock++;
                                btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('No Students Found for " + ddlcourseforabset.SelectedItem.Text.Split('-')[1] + " !!');", true);
                        btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = false;
                    }
                }
                else
                {
                    div_Result.Visible = false;
                    btnSubmit.Enabled = btnLock.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('No Record Found !!');", true);
                }
                if (UnLock > 0)
                {
                    btnSubmit.Enabled = btnLock.Enabled = btnAbsentReport1.Enabled = true;
                    btnAbsentReport1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
            }
        }
    }

    protected void btnBlankDocket_Click(object sender, EventArgs e)
    {
    }

    protected void btnDocket_Click(object sender, EventArgs e)
    {

    }

    #region Commented
    /// <summary>
    /// This ShowAbsentReport method is used for to binding the Report record remark by pankaj nakhale 12/09/2019
    /// </summary>
    /// <param name="reportTitle"></param>
    /// <param name="rptFileName"></param>
    //private void ShowAbsentReport(string reportTitle_x, string rptFileName_x)
    //{

    //    //try
    //    //{

    //    //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //    //    url += "Reports/Commonreport.aspx?";
    //    //    url += "pagetitle=" + reportTitle;
    //    //    url += "&path=~,Reports,Academic," + rptFileName;
    //    //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ",@P_EXAMNO=1,@P_COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue) + "";
    //    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //    //    sb.Append(@"window.open('" + url + "','','" + features + "');");
    //    //    ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    if (Convert.ToBoolean(Session["error"]) == true)
    //    //        objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
    //    //    else
    //    //        objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    //}

    //}
    #endregion
    /// <summary>
    /// This FillDropDownList method is used for to binding the Exam Date remark by pankaj nakhale 12/09/2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlsessionforabsent_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlsessionforabsent.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");

            if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
            {
                objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0", "");
                ddlcourseforabset.Focus();

            }
            else
            {
                objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0    AND (UA_NO =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_PRAC =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_TUTR=" + Convert.ToString(Session["userno"].ToString()) + ")", "");
                ddlcourseforabset.Focus();


            }

            objCommon.FillDropDownList(ddlExamDate, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_EXAM_NAME EN ON AED.EXAM_TT_TYPE=EN.EXAMNO INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = EN.EXAMNO) INNER JOIN ACD_EXAM_TT_SLOT TT ON (TT.SLOTNO = AED.SLOTNO)", "DISTINCT CONVERT(NVARCHAR(30),AED.EXAMDATE,103)", "CONVERT(NVARCHAR(30),AED.EXAMDATE,103) ED", "AED.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND AED.COLLEGE_ID =" + ddlcollege.SelectedValue, "");
        }
        else
        {
            ddlsessionforabsent.SelectedIndex = 0;
            ddlExamDate.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlexamnameabsentstudent.SelectedIndex = 0;
            ddlSubexamnameabsentstudent.SelectedIndex = 0;
            ddlcourseforabset.SelectedIndex = 0;
            ddlexam_type.SelectedIndex = 0;
            ddlsessionforabsent.Focus();
        }

        Clearlistview();

    }

    /// <summary>
    /// This FillDropDownList method is used for to binding the COURSE NAME with course code remark by pankaj nakhale 12/09/2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlExamDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamDate.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_EXAM_NAME EN ON AED.EXAM_TT_TYPE=EN.EXAMNO INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = EN.EXAMNO) INNER JOIN ACD_EXAM_TT_SLOT TT ON (TT.SLOTNO = AED.SLOTNO) INNER JOIN ACD_SEMESTER SEM ON (AED.SEMESTERNO = SEM.SEMESTERNO)", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "AED.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND AED.COLLEGE_ID =" + ddlcollege.SelectedValue + " AND CONVERT(VARCHAR,AED.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");
            //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND ACD.EXAM_TT_TYPE='" + Convert.ToInt32(ddlExamDate.SelectedValue) + "'", "");
            objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");
        }
        else
        {
            if (ddlsessionforabsent.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");

                if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
                {
                    objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0", "");
                    ddlcourseforabset.Focus();

                }
                else
                {
                    objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0    AND (UA_NO =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_PRAC =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_TUTR=" + Convert.ToString(Session["userno"].ToString()) + ")", "");
                    ddlcourseforabset.Focus();


                }

                objCommon.FillDropDownList(ddlExamDate, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_EXAM_NAME EN ON AED.EXAM_TT_TYPE=EN.EXAMNO INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = EN.EXAMNO) INNER JOIN ACD_EXAM_TT_SLOT TT ON (TT.SLOTNO = AED.SLOTNO)", "DISTINCT CONVERT(NVARCHAR(30),AED.EXAMDATE,103)", "CONVERT(NVARCHAR(30),AED.EXAMDATE,103) ED", "AED.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND AED.COLLEGE_ID =" + ddlcollege.SelectedValue, "");
                ddlSem.SelectedIndex = 0;
                ddlcourseforabset.SelectedIndex = 0;
            }
            ddlExamDate.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlexamnameabsentstudent.SelectedIndex = 0;
            ddlSubexamnameabsentstudent.SelectedIndex = 0;
            ddlcourseforabset.SelectedIndex = 0;
            ddlexam_type.SelectedIndex = 0;
        }
        Clearlistview();


    }

    /// <summary>
    /// This FillDropDownList method is used for to binding the ROOM NO remark by pankaj nakhale 12/09/2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlcourseforabset_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcourseforabset.SelectedIndex > 0)
        {
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[0]) + ""));
            int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "PATTERNNO", "SCHEMENO=" + schemeno + ""));
            //ADDED BY SHUBHAM
            string degreeno = objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + schemeno + "");
            string Branchno = objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + schemeno + "");
            //END

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                if (ddlcourseforabset.SelectedIndex > 0)
                {
                   
                    if (patternno > 0)
                    {
                        if (Convert.ToInt32(Session["OrgId"]) == 2) // Added By Sagar Mankar On Date 26062023 For Crescent
                        {
                            objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", "DISTINCT EXAMNO", "EXAMNAME", "EXAMNO>0 AND PATTERNNO=" + patternno + " AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO ASC");
                        }
                        else
                        {
                            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", "DISTINCT EXAMNO", "EXAMNAME", "EXAMNO>0 AND PATTERNNO=" + patternno + " AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO ASC");
                            objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", "DISTINCT EXAMNO", "EXAMNAME", "EXAMNO>0 AND PATTERNNO=" + patternno + " AND FLDNAME='EXTERMARK' AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO ASC");
                        }
                        ddlexamnameabsentstudent.Focus();
                    }
                }
                else
                {
                    div_Result.Visible = false;
                    ddlcourseforabset.Focus();
                    ddlexamnameabsentstudent.SelectedIndex = 0;
                    ddlSubexamnameabsentstudent.SelectedIndex = 0;
                    ddlexam_type.SelectedIndex = 0;
                    ddlcourseforabset.SelectedIndex = 0;

                }
            }
            else
            {

                //DataSet ds = objCommon.FillDropDown(" SESSION_ACTIVITY SA INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=SA.SESSION_NO) INNER JOIN  ACTIVITY_MASTER AM ON(AM.ACTIVITY_NO=SA.ACTIVITY_NO)", "DISTINCT SA.ACTIVITY_NO", "SESSION_NAME", "SA.STARTED=1 AND SA.SESSION_NO=  (" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ") AND ACTIVITY_CODE='ABS' AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + " AND UA_TYPE LIKE '%" + Session["usertype"] + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "ACTIVITY_NO DESC");
                DataSet ds = objCommon.FillDropDown(" SESSION_ACTIVITY SA INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=SA.SESSION_NO) INNER JOIN  ACTIVITY_MASTER AM ON(AM.ACTIVITY_NO=SA.ACTIVITY_NO)", "DISTINCT SA.ACTIVITY_NO", "SESSION_NAME", "SA.STARTED=1 AND SA.SESSION_NO=  (" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ") AND ACTIVITY_CODE='ABS' AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + " AND UA_TYPE LIKE '%" + Session["usertype"] + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND SA.DEGREENO LIKE '%" + degreeno + "%' AND SA.BRANCH LIKE '%" + Branchno + "%'", "ACTIVITY_NO DESC");
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["ACTIVITY_NO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVITY_NO"]).ToString();
                    //            SELECT DISTINCT SA.ACTIVITY_NO,SESSION_NAME  FROM SESSION_ACTIVITY SA
                    //INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=SA.SESSION_NO)  
                    //INNER JOIN  ACTIVITY_MASTER AM ON(AM.ACTIVITY_NO=SA.ACTIVITY_NO)
                    //WHERE SA.STARTED=1 AND SA.SESSION_NO=59 AND ACTIVITY_CODE='ABS' AND PAGE_LINK='1054' AND UA_TYPE LIKE '%2%'

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");          


                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                    return;
                }

                if (ddlcourseforabset.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (E.EXAMNO=AM.EXAMNO) INNER JOIN SESSION_ACTIVITY SA ON (SA.ACTIVITY_NO=AM.ACTIVITY_NO)", "DISTINCT E.EXAMNO", "CAST(E.EXAMNAME AS VARCHAR)", "E.EXAMNO > 0 AND ISNULL(E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1 and AM.ACTIVITY_NO IN (" + Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString()) + ") and SA.SESSION_NO= " + Convert.ToInt32(ddlsessionforabsent.SelectedValue), "E.EXAMNO");
                    ddlexamnameabsentstudent.Focus();
                }
                else
                {
                    div_Result.Visible = false;
                    ddlcourseforabset.Focus();
                    ddlexamnameabsentstudent.SelectedIndex = ddlSubexamnameabsentstudent.SelectedIndex = ddlcourseforabset.SelectedIndex = 0;
                }
                //if (ddlcourseforabset.SelectedIndex > 0)
                //{
                //    objCommon.FillDropDownList(ddlblock, "ACD_ROOM AR INNER JOIN ACD_SEATING_ARRANGEMENT ACS ON ACS.ROOMNO=AR.ROOMNO", "DISTINCT AR.ROOMNO", "(AR.ROOMNAME)", "ACS.COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[1], "");
                //}

                //div_Result.Visible = false;

                //ddlcourseforabset.Focus();
                Clearlistview();
            }
        }
        else
        {

            DataSet ds = objCommon.FillDropDown(" SESSION_ACTIVITY SA INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=SA.SESSION_NO) INNER JOIN  ACTIVITY_MASTER AM ON(AM.ACTIVITY_NO=SA.ACTIVITY_NO)", "DISTINCT SA.ACTIVITY_NO", "SESSION_NAME", "SA.STARTED=1 AND SA.SESSION_NO=  (" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + ") AND ACTIVITY_CODE='ABS' AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + " AND UA_TYPE LIKE '%" + Session["usertype"] + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "ACTIVITY_NO DESC");

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["ACTIVITY_NO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVITY_NO"]).ToString();
                //            SELECT DISTINCT SA.ACTIVITY_NO,SESSION_NAME  FROM SESSION_ACTIVITY SA
                //INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=SA.SESSION_NO)  
                //INNER JOIN  ACTIVITY_MASTER AM ON(AM.ACTIVITY_NO=SA.ACTIVITY_NO)
                //WHERE SA.STARTED=1 AND SA.SESSION_NO=59 AND ACTIVITY_CODE='ABS' AND PAGE_LINK='1054' AND UA_TYPE LIKE '%2%'

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");          


            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                return;
            }

            if (ddlcourseforabset.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (E.EXAMNO=AM.EXAMNO) INNER JOIN SESSION_ACTIVITY SA ON (SA.ACTIVITY_NO=AM.ACTIVITY_NO)", "DISTINCT E.EXAMNO", "CAST(E.EXAMNAME AS VARCHAR)", "E.EXAMNO > 0 AND ISNULL(E.EXAMNAME,'')<>'' AND ISNULL(E.ACTIVESTATUS,0)=1 and AM.ACTIVITY_NO IN (" + Convert.ToInt32(ViewState["ACTIVITY_NO"].ToString()) + ") and SA.SESSION_NO= " + Convert.ToInt32(ddlsessionforabsent.SelectedValue), "E.EXAMNO");
                ddlexamnameabsentstudent.Focus();
            }
            else
            {
                div_Result.Visible = false;
                ddlcourseforabset.Focus();
                ddlexamnameabsentstudent.SelectedIndex = ddlSubexamnameabsentstudent.SelectedIndex = ddlcourseforabset.SelectedIndex = 0;
            }
            //if (ddlcourseforabset.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlblock, "ACD_ROOM AR INNER JOIN ACD_SEATING_ARRANGEMENT ACS ON ACS.ROOMNO=AR.ROOMNO", "DISTINCT AR.ROOMNO", "(AR.ROOMNAME)", "ACS.COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[1], "");
            //}

            //div_Result.Visible = false;

            //ddlcourseforabset.Focus();
            Clearlistview();
        }
    }

    //aayushi gupta
    /// <summary>
    /// This BindSeatPlan method is used for to binding the ROOM NO,SESSION,COURSE AND display in list view. remark by pankaj nakhale 12/09/2019
    /// </summary>
    public void BindSeatPlan()
    {
        try
        {
            // DataSet ds;
            int session = 0;
            int course = 0;
            int roomno = 0;
            session = Convert.ToInt32(ddlsessionforabsent.SelectedValue);
            course = Convert.ToInt32(ddlcourseforabset.SelectedValue);
            roomno = Convert.ToInt32(ddlblock.SelectedValue);
            DataSet ds = objExamController.GetAllSeatPlan(roomno, session, course);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //lvdetails.DataSource = ds;
                    //lvdetails.DataBind();
                    //pnldetails.Visible = true;
                }
                else
                {
                    //lvdetails.DataSource = null;
                    //lvdetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.BindSeatPlan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlblock_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSeatPlan();
    }
    /// <summary>
    /// This AbsentReport1 Button is used for to display Absent Student record on AbsentStudentEntryFormReport report . remark by pankaj nakhale 12/09/2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAbsentReport1_Click(object sender, EventArgs e)
    {
        try
        {
            // changes done by the Shubham as per enchancement
            String Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
            String Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

            string headerexam = ddlexamnameabsentstudent.SelectedItem.Text.Trim() + '-' + ddlSubexamnameabsentstudent.SelectedItem.Text.Trim();
            //ShowAbsentReport("Absent_Student_Report", "AbsentStudentEntryFormReport.rpt");

            string proc_name = "PKG_ABSENT_STUDENT_ENTRY_REPORT";
            string param = "@P_SESSIONNO,@P_COURSENO,@P_SEMESTERNO,@P_SUBID,@P_CCODE,@P_SECTIONNO,@P_EXAM,@P_SUB_EXAM,@P_UA_NO";
            string call_values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + "," + 0 + "," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + "," + 0 + "," + Examname + "," + Subexamname + "," + Convert.ToInt32(Session["userno"]) + "";

            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string reportTitle = "Absent_UFM_Student_Report";
                string rptFileName = "AbsentStudentReport.rpt";

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/commonreport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=";
                url += "@P_SESSIONNO=" + ddlsessionforabsent.SelectedValue + ",";
                url += "@P_CCODE=" + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",";
                url += "@P_COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[0] + ",";
                url += "@P_SEMESTERNO=0,";
                url += "@P_SECTIONNO=0,";
                url += "@P_SUBID=" + ddlcourseforabset.SelectedValue.Split('-')[1] + ",";
                url += "@P_EXAM=" + Examname + ",";
                url += "@P_SUB_EXAM=" + Subexamname + ",";
                //url += "@P_EXAM_HEADER=" + headerexam + ",";
                url += "@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",";
                url += "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);

            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "ShowReportResultAnalysis_Examwise() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlexamnameabsentstudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ExamName = ddlexamnameabsentstudent.SelectedItem.ToString();
        string SUBID = objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[0]);
        if (ddlexamnameabsentstudent.SelectedIndex > 0)
        {

            DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT DISTINCT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='ABS' AND AM.EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + ")", "SESSIONNO DESC");
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT DISTINCT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='ABS' AND EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + ")", "SESSIONNO DESC");
            if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            {
                //btnShow.Enabled = false;
                //objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                //divAbs.Visible = false;
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    btnShow.Enabled = true;
                    divAbs.Visible = true;
                }
                else
                {

                    btnShow.Enabled = false;
                    objCommon.DisplayMessage(this.updpnlExam, "This activity may not be Started!!!, Please contact Admin", this.Page);
                    divAbs.Visible = false;
                    return;
                }

                //return;
            }
            else
            {
                divAbs.Visible = true;
            }



            objCommon.FillDropDownList(ddlSubexamnameabsentstudent, "ACD_SUBEXAM_NAME", "DISTINCT SUBEXAMNO", "SUBEXAMNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(SUBID) + " AND ISNULL(ACTIVESTATUS,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "");
            ddlSubexamnameabsentstudent.Focus();
        }
        else
        {


            ddlexamnameabsentstudent.SelectedIndex = 0;
            ddlSubexamnameabsentstudent.SelectedIndex = 0;
            ddlexam_type.SelectedIndex = 0;
            ddlexamnameabsentstudent.Focus();
        }
        Clearlistview();
        //if (ddlsessionforabsent.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlExamDate, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO inner join ACD_EXAM_NAME EN on AED.EXAM_TT_TYPE=EN.EXAMNO", "DISTINCT CONVERT(NVARCHAR(30),EXAMDATE,103)", "CONVERT(NVARCHAR(30),AED.EXAMDATE,103) ED", "ASM.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND en.EXAMNO in (SELECT EXAMNO FROM ACD_EXAM_NAME WHERE dbo.ACD_EXAM_NAME.EXAMNAME='" + ExamName + "')", "");
        //}

        //div_Result.Visible = false;

        //ddlSubexamnameabsentstudent.Focus();

        //ddlExamDate.SelectedIndex = 0; //ddlcourseforabset.SelectedIndex =
    }

    protected void ddlSubexamnameabsentstudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlexam_type.SelectedIndex = 0;

        //if (ddlSubexamnameabsentstudent.SelectedIndex > 0)
        //{
        //    //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");

        //    if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        //    {
        //        objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0", "");
        //        ddlcourseforabset.Focus();

        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0    AND (UA_NO =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_PRAC =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_TUTR=" + Convert.ToString(Session["userno"].ToString()) + ")", "");
        //        ddlcourseforabset.Focus();

        //    }
        //}
        //else
        //{
        //    ddlSubexamnameabsentstudent.SelectedIndex = 0;
        //    ddlcourseforabset.SelectedIndex = 0;
        //}
        Clearlistview();
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlsessionforabsent, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "SESSIONNO DESC"); //--AND FLOCK = 1
            ddlsessionforabsent.Focus();
        }
        else
        {
            ddlcollege.SelectedIndex = 0;
            ddlsessionforabsent.SelectedIndex = 0;
            ddlExamDate.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlexamnameabsentstudent.SelectedIndex = 0;
            ddlSubexamnameabsentstudent.SelectedIndex = 0;
            ddlcourseforabset.SelectedIndex = 0;
            ddlexam_type.SelectedIndex = 0;

        }
        Clearlistview();

    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "' AND ACD.SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue), "");

            }
            else
            {
                if (ddlExamDate.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSem, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_EXAM_NAME EN ON AED.EXAM_TT_TYPE=EN.EXAMNO INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = EN.EXAMNO) INNER JOIN ACD_EXAM_TT_SLOT TT ON (TT.SLOTNO = AED.SLOTNO) INNER JOIN ACD_SEMESTER SEM ON (AED.SEMESTERNO = SEM.SEMESTERNO)", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "AED.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND AED.COLLEGE_ID =" + ddlcollege.SelectedValue + " AND CONVERT(VARCHAR,AED.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");
                    //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND ACD.EXAM_TT_TYPE='" + Convert.ToInt32(ddlExamDate.SelectedValue) + "'", "");
                    objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");
                }
                else
                {
                    if (ddlsessionforabsent.SelectedIndex > 0)
                    {
                        //objCommon.FillDropDownList(ddlcourseforabset, "ACD_EXAM_DATE ACD INNER JOIN ACD_COURSE ACO ON ACD.COURSENO=ACO.COURSENO", "DISTINCT CAST(ACO.COURSENO AS VARCHAR)+'-'+CAST(ACO.SUBID AS VARCHAR)", "ACO.CCODE+'-'+COURSE_NAME", "ACD.SESSIONNO=" + ddlsessionforabsent.SelectedValue + "AND CONVERT(VARCHAR,ACD.EXAMDATE,103)='" + Convert.ToString(ddlExamDate.SelectedItem.Text) + "'", "");

                        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
                        {
                            objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0", "");
                            ddlcourseforabset.Focus();

                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlcourseforabset, "ACD_STUDENT_RESULT R WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (R.CCODE = C.CCODE AND R.SCHEMENO = C.SCHEMENO AND R.COURSENO=C.COURSENO)", "DISTINCT CAST(C.COURSENO AS VARCHAR)+'-'+CAST(C.SUBID AS VARCHAR)", "C.CCODE+'-'+COURSE_NAME", "R.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(CANCEL,0)= 0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1 AND ISNULL(R.DETAIND,0)=0    AND (UA_NO =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_PRAC =" + Convert.ToString(Session["userno"].ToString()) + " OR UA_NO_TUTR=" + Convert.ToString(Session["userno"].ToString()) + ")", "");
                            ddlcourseforabset.Focus();


                        }

                        objCommon.FillDropDownList(ddlExamDate, "ACD_EXAM_DATE AED INNER JOIN ACD_SESSION_MASTER ASM ON ASM.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_EXAM_NAME EN ON AED.EXAM_TT_TYPE=EN.EXAMNO INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = EN.EXAMNO) INNER JOIN ACD_EXAM_TT_SLOT TT ON (TT.SLOTNO = AED.SLOTNO)", "DISTINCT CONVERT(NVARCHAR(30),AED.EXAMDATE,103)", "CONVERT(NVARCHAR(30),AED.EXAMDATE,103) ED", "AED.SESSIONNO=" + ddlsessionforabsent.SelectedValue + " AND AED.COLLEGE_ID =" + ddlcollege.SelectedValue, "");
                        div_Result.Visible = false;

                        ddlExamDate.Focus();

                        ddlcourseforabset.SelectedIndex = 0;
                    }
                }
                ddlSem.SelectedIndex = 0;
                ddlexamnameabsentstudent.SelectedIndex = 0;
                ddlSubexamnameabsentstudent.SelectedIndex = 0;
                ddlcourseforabset.SelectedIndex = 0;
                ddlexam_type.SelectedIndex = 0;
            }
            Clearlistview();

        }

        catch (Exception ex)
        {
        }
    }

    private void Clearlistview()
    {
        try
        {
            DataSet ds = null;
            rpt_Success.DataSource = ds;
            rpt_Success.DataBind();

        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlexam_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clearlistview();
        }
        catch (Exception ex)
        {
        }
    }

    //private void ShowAbsentReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        String Examname = objCommon.LookUp("ACD_EXAM_NAME", "FLDNAME", "EXAMNO=" + Convert.ToInt32(ddlexamnameabsentstudent.SelectedValue));
    //        String Subexamname = objCommon.LookUp("ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+ '-' + CAST(SUBEXAMNO AS VARCHAR)", "SUBEXAMNO=" + Convert.ToInt32(ddlSubexamnameabsentstudent.SelectedValue));

    //        string headerexam = ddlexamnameabsentstudent.SelectedItem.Text.Trim() + '-' + ddlSubexamnameabsentstudent.SelectedItem.Text.Trim();


    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/commonreport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=";
    //        url += "@P_SESSIONNO=" + ddlsessionforabsent.SelectedValue + ",";
    //        url += "@P_CCODE=" + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",";
    //        url += "@P_COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[0] + ",";
    //        url += "@P_SEMESTERNO=0,";
    //        url += "@P_SECTIONNO=0,";
    //        url += "@P_SUBID=" + ddlcourseforabset.SelectedValue.Split('-')[1] + ",";
    //        url += "@P_EXAM=" + Examname + ",";
    //        url += "@P_SUB_EXAM=" + Subexamname + ",";
    //        //url += "@P_EXAM_HEADER=" + headerexam + ",";
    //        url += "@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",";
    //        url += "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

    //        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
}