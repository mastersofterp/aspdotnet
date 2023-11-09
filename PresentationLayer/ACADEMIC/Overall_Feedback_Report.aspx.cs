//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Overall Feedback Report                                
// CREATION DATE : 23-10-2023                                                      
// CREATED BY    : Vipul R Tichakule                                                        
// MODIFIED DATE : 
// MODIFIED BY                                                     
// MODIFIED DESC :                                                                  
//======================================================================================

using BusinessLogicLayer.BusinessLogic.Academic;
using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Overall_Feedback_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   
    StudentFeedBackController feedb = new StudentFeedBackController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                FillDropDownList();
            }
        }
    }

    private void FillDropDownList()
    {
        if (Session["usertype"].ToString() != "1")
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
        else       
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId="+ Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO>0 AND ISNULL(PREV_STATUS,0)=0 and R.SESSIONNO=" + ddlSession.SelectedValue, "R.SEMESTERNO");
            ddlSemester.Focus();
            objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
            
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME", "FEEDBACK_NO>0", "FEEDBACK_NO");
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ExcelOverallFeedbackReport();  
    }

    private void ExcelOverallFeedbackReport()
    {
        int scheme = Convert.ToInt32(ViewState["schemeno"].ToString());
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int feedbacktype = Convert.ToInt32(ddlFeedbackTyp.SelectedValue);




        string SP_Name2 = "PKG_ACD_STUDENT_FEEDBACK_REPORT_DAIICT";
        string SP_Parameters2 = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_FEEDBACK_TYPENO";

        //string Call_Values2 = "'" + session + "','" + scheme + "','" + semester + "','" + feedbacktype + "'";

        string Call_Values2 = "" + session + "," + scheme + "," + semester + "," + feedbacktype + "";


        DataSet dsFeedBack = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);



       //DataSet dsFeedBack = feedb.Get_OverallFeedBackData(scheme, session, semester, feedbacktype);

        if (dsFeedBack != null && dsFeedBack.Tables.Count > 0)
        {
            dsFeedBack.Tables[0].TableName = "Overall FeedBack Report";
            dsFeedBack.Tables[1].TableName = "Comments";

                //if (dsFeedBack.Tables[0] != null && dsFeedBack.Tables[0].Rows.Count <= 0)
                //    dsFeedBack.Tables[0].Rows.Add("No Record Found");

                //if (dsFeedBack.Tables[1] != null && dsFeedBack.Tables[1].Rows.Count <= 0)
                //    dsFeedBack.Tables[1].Rows.Add("No Record Found");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsFeedBack.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= OverAllFeedBackReport.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
                ClearControls();            
        }
        else
        {
            objCommon.DisplayUserMessage(updFeed, "No Record Found", this.Page);
            return;
        }

    }


    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void ClearControls()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlFeedbackTyp.SelectedIndex = 0;
    }


    
}