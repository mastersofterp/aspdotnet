//=================================================================================
// PROJECT NAME  : COMMON CODE                                                          
// MODULE NAME   : ACADEMIC-EXAMINATION - MARK ENTRY BY ADMIN EXTERNAL                                         
// CREATION DATE :                                   
// CREATED BY    : PRAFULL MUKE                                       
// MODIFIED BY   :                                                  
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
//using ClosedXML.Excel;
using System.Data.OleDb;
using ClosedXML.Excel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Threading.Tasks;
   

public partial class Academic_MarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

     string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();



    // string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_MEContainerName"].ToString();
    //  string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
        if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                this.PopulateDropDown();
                

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

             
                if (Request.QueryString["pageno"] != null)
                {
                    int ADMIN_LEVEL_MARKS_ENTRY_USER = Convert.ToInt32(objCommon.LookUp("REFF", "isnull(ADMIN_LEVEL_MARKS_ENTRY,0)", ""));
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CheckPageAuthorization();
                }
            }
        }
     
        divMsg.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //PopulateDropDown();
    }
    private void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            objCommon.FillDropDownList(ddlcollege, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //0 - means - unlock
    //    SaveAndLock(0);
    //}

    //#region Private/Public Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
        }
    }
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

   
    private bool CheckMarks(int lock_status)
    {
        
            bool flag = true;
            try
            {
                Label lbl;
                TextBox txt;
                string marks = string.Empty;
                string maxMarks = string.Empty;
                string examtype = string.Empty;
               
                    for (int j = 3; j < gvStudent.Columns.Count; j++)    //columns
                    {
                        for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                        {
                            if (gvStudent.Columns[j].Visible == true)
                            {
                                if (j == 3) //TA MARKS
                                {
                                    lbl = gvStudent.Rows[i].Cells[j].FindControl("lblMarks") as Label;      //Max Marks 
                                    txt = gvStudent.Rows[i].Cells[j].FindControl("txtMarks") as TextBox;    //Marks Entered 
                                    maxMarks = lbl.Text.Trim();
                                    marks = txt.Text.Trim();

                                    if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                                    {
                                        if (txt.Text == "")
                                        {
                                            if (lock_status == 1)
                                            {
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                                //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            //Check for Marks entered greater than Max Marks
                                            if (Convert.ToDouble(txt.Text) > Convert.ToDouble(lbl.Text))
                                            {
                                                if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                                {
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                                    txt.Focus();
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                            else if (Convert.ToDouble(txt.Text) < 0)
                                            {
                                                //Note : 401 for Absent and Not Eligible
                                                if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                                {
                                                }
                                                else
                                                {
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.");
                                                    txt.Focus();
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (txt.Enabled == true)
                                        {
                                            if (lock_status == 1)
                                            {
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                                //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            #region Not Needed Commented by Manish

                            #endregion

                            #region comment
                            //}

                            #endregion

                            if (flag == false) break;
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
            return flag;
       
    }
 
    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        //SaveAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }

    private void Clear()
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
        //btnReport.Visible = false;
    }



    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
          //  ShowStudents();
     
    }

    //private void ShowStudents()
    //{
        
    //        try
    //        {
    //            string[] course = ddlCourse.SelectedItem.Text.Split('-');
    //            string[] Exam = ddlExam.SelectedValue.Split('-');
    //            string SubExam = string.Empty;
    //            string SubExamName = string.Empty;
    //            DataSet ds = null;
    //            Boolean LOCK1 = false;
    //            Boolean LOCK2 = false;
    //            int Subexamno = 0;
    //            string examtype = string.Empty;

    //            if (Exam[0].StartsWith("S"))
    //                examtype = "S";
    //            else if (Exam[0].StartsWith("E"))
    //                examtype = "E";


    //            if (divSubExamName.Visible == true)
    //            {
    //                SubExam = ddlSubExamName.SelectedValue;
    //                SubExamName = ddlSubExamName.SelectedItem.Text;
    //            }
    //            else
    //            {
    //                if (Convert.ToInt32(Session["OrgId"]) == 6)
    //                {
    //                    if (ddlStudenttype.SelectedValue == "1")
    //                    {
    //                        SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
    //                    }
    //                    else
    //                    {
    //                        SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
    //                    }

    //                }
    //                else
    //                {
    //                    SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
    //                }
                    
    //            }

    //                Subexamno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1]));

    //                if (Convert.ToInt32(Session["OrgId"]) == 6)
    //                {
    //                    if (ddlStudenttype.SelectedValue == "1")
    //                    {
    //                        ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

    //                    }
    //                    else
    //                    {
    //                        ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

    //                    }
    //                }
    //                else
    //                {
    //                    ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

    //                }
                
    //            string extermark = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
    //            if (extermark != "0.00")
    //            {
    //                if (ds != null && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
    //                    {
    //                        objCommon.DisplayMessage(this, "STOP !!! Rule 1 for End Sem Exam is not Defined", this.Page);
    //                        return;
    //                    }
    //                    else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
    //                    {
    //                        objCommon.DisplayMessage(this, "STOP !!! Rule 2 for End Sem Exam is not Defined", this.Page);
    //                        return;
    //                    }
    //                }
    //                else
    //                {

    //                    objCommon.DisplayMessage(this, "STOP !!! Exam Rule is not Defined", this.Page);
    //                    return;
    //                }
    //            }

    //            DataSet dsStudent = null;
    //            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

    //            if (Convert.ToInt32(Session["OrgId"]) == 6)
    //            {
    //                if (ddlStudenttype.SelectedValue == "1")
    //                {
    //                    if (examtype == "S")
    //                    {
    //                        string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC";
    //                        string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAMNO,@P_SEMESTERNO";
    //                        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + (ddlSubExamName.SelectedValue).Split('-')[0] + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";
    //                        dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


    //                      //  dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]), Convert.ToInt32(ddlsemester.SelectedValue));
    //                    }
    //                    else
    //                    {

    //                        string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC";
    //                        string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_SEMESTERNO";
    //                        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + SubExam + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlsemester.SelectedValue) +  "";
    //                        dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


    //                      //  dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]),Convert.ToInt32(ddlsemester.SelectedValue));
    //                    }
    //                }
    //                else 
    //                {
    //                    if (examtype == "S")
    //                    {
    //                        string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC_BACKLOG";
    //                        string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAMNO";
    //                        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + (ddlSubExamName.SelectedValue).Split('-')[0] + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]) + "";
    //                        dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                      
    //                    }
    //                    else
    //                    {

                           
    //                        string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC_Backlog";
    //                        string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID";
    //                        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + SubExam + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "";
    //                        dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                       
    //                    }

    //                }
                   
    //            }
    //            else
    //            {
    //                if (examtype == "S")
    //                {

    //                    string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC";
    //                    string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAMNO,@P_SEMESTERNO";
    //                    string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + (ddlSubExamName.SelectedValue).Split('-')[0] + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";
    //                    dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


    //                 //   dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]),Convert.ToInt32(ddlsemester.SelectedValue));

    //                    //dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]));
    //                }
    //                else
    //                {

    //                    string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC";
    //                    string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_SEMESTERNO";
    //                    string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + SubExam + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "";
    //                    dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


    //                 //   dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlsemester.SelectedValue));

    //                   // dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]));
    //                }
    //            } 
    //            if (dsStudent != null && dsStudent.Tables.Count > 0)
    //            {
    //                if (dsStudent.Tables[0].Rows.Count > 0)
    //                {

                       
    //                    string excelStatus = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ExcelMarkEntry", "");
    //                    if (excelStatus == "1")
    //                    {
    //                        lnkExcekImport.Visible = true;

    //                    }
    //                    else
    //                    {
    //                        lnkExcekImport.Visible = false;
    //                    }
    //                    if (Convert.ToInt32(Session["OrgId"]) == 6)
    //                    {
    //                        btnEndSemReport.Visible = true;
    //                    }
    //                    else
    //                    {
    //                        btnEndSemReport.Visible = false;
    //                    }


    //                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
    //                    if (Convert.ToString(ddlExam.SelectedValue).Split('-')[0] == "EXTERMARK")
    //                    {
    //                        gvStudent.Columns[2].Visible = true;
    //                    }
    //                    else
    //                    {
    //                        gvStudent.Columns[2].Visible = true;
    //                    }
    //                    if (examtype == "S")
    //                    {
    //                        if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
    //                        {

    //                            if (divSubExamName.Visible == false)
    //                            {
    //                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();


    //                                gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                            }
    //                            else
    //                            {
    //                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();
    //                                gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                            }


    //                            gvStudent.Columns[4].Visible = true;
    //                            gvStudent.Columns[5].Visible = false;
    //                            gvStudent.Columns[6].Visible = false;
    //                            gvStudent.Columns[3].Visible = false;
    //                            ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
    //                            ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
    //                            ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];


    //                        }
    //                        else
    //                        {
    //                            gvStudent.Columns[4].Visible = false;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string extermarkNEW = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
    //                        if (extermarkNEW != "0.00")
    //                        {
    //                            if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
    //                            {

    //                                if (divSubExamName.Visible == false)
    //                                {
    //                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

    //                                    gvStudent.Columns[5].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                                }
    //                                else
    //                                {
    //                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();
    //                                    //gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]" + " - " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                                    gvStudent.Columns[5].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                                }


    //                                gvStudent.Columns[5].Visible = true;
    //                                gvStudent.Columns[3].Visible = true;
    //                                gvStudent.Columns[4].Visible = false;
    //                                ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
    //                                ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
    //                                ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
    //                            }
    //                            else
    //                            {
    //                                gvStudent.Columns[5].Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (divSubExamName.Visible == false)
    //                            {
    //                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

    //                                //gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + "  " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]" + " - " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                                gvStudent.Columns[5].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                            }
    //                            else
    //                            {
    //                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
    //                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();
    //                                //gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]" + " - " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                                gvStudent.Columns[5].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
    //                            }
    //                            gvStudent.Columns[5].Visible = false;
    //                            gvStudent.Columns[3].Visible = true;
    //                            gvStudent.Columns[4].Visible = false;
    //                            //btnSave.Visible = false;
    //                            //btnLock.Visible = false;
    //                            ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
    //                            ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
    //                            ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
    //                        }

    //                    }
    //                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

    //                    //Bind the Student List
    //                    gvStudent.DataSource = dsStudent;
    //                    gvStudent.DataBind();



    //                    //added by prafull on dt 13042023   

                        
    //                    int lockcount = 0;
    //                    int lockcount_test = 0;
    //                    for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
    //                    {
    //                        if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
    //                        {
    //                            lockcount++;
    //                        }
    //                    }

    //                    for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
    //                    {
    //                        if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["TESTLOCK"]) == true)
    //                        {
    //                            lockcount_test++;
    //                        }
    //                    }


    //                    // added for absent student by prafull on dated 23072022
    //                    int z = 0;

    //                    //commented by prafull on dt 10022023

    //                    //foreach (GridViewRow rw in gvStudent.Rows)
    //                    //{
    //                    //    TextBox txtmark = (TextBox)rw.FindControl("txtmarks");
    //                    //    string regno = (dsStudent.Tables[0].Rows[z]["REGNO"]).ToString();

    //                    //    if ((dsStudent.Tables[0].Rows[z]["SMARK"]) is DBNull)
    //                    //    {
    //                    //        txtmark.Enabled = true;
    //                    //    }
    //                    //    else if ((Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 902.00) || (Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 903.00))
    //                    //    {
    //                    //        txtmark.Enabled = false;
    //                    //    }
    //                    //    else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == true)
    //                    //    {
    //                    //        txtmark.Enabled = false;
    //                    //    }
    //                    //    else
    //                    //    {
    //                    //        txtmark.Enabled = true;
    //                    //    }


    //                    //    z++;
    //                    //}
    //                    // prafull comment end 
    //                    #region Comment Code



    //                    #endregion Comment Code

    //                    btnSave.Enabled = true;
    //                    btnLock.Enabled = true;
    //                    btnSave.Visible = true;
    //                    btnLock.Visible = true;

    //                    btnExcelReport.Enabled = true;
    //                    btnExcelReport.Visible = true;
    //                    //lnkExcekImport.Visible = true;


    //                    int TestMark = 0;
    //                    TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
    //                    string extermarmew = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));


                       
    //                    btnGrade.Visible = false;

    //                    if (examtype == "S")
    //                    {
    //                        int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
    //                           if (SESSION_TYPE == 1)
    //                           {
                              
    //                                if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
    //                                {
    //                                    #region
    //                                      if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                         {

    //                                    btnSave.Enabled = false;
    //                                    btnLock.Enabled = false;

    //                                    btnSave.Visible = false;
    //                                    btnLock.Visible = false;


    //                                    //TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
    //                                    //extermarmew = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
    //                                    if (TestMark > 0)
    //                                    {

    //                                        if (extermarmew == "0.00")
    //                                        {
    //                                            string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                            string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                            string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                            DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                            if (dschk.Tables.Count > 0)
    //                                            {
    //                                                if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                {
    //                                                    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                    if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                    {
    //                                                        // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

    //                                                        btnGrade.Enabled = false;
    //                                                        btnGrade.Visible = false;

    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                        {
    //                                                            btnReGrade.Enabled = true;
    //                                                            btnReGrade.Visible = true;
    //                                                            btnGrade.Enabled = false;
    //                                                            btnGrade.Visible = false;
    //                                                        }
    //                                                        else
    //                                                        {
    //                                                            btnReGrade.Enabled = false;
    //                                                            btnReGrade.Visible = false;
    //                                                            btnGrade.Enabled = true;
    //                                                            btnGrade.Visible = true;
    //                                                        }

    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            btnGrade.Enabled = false;
    //                                            btnGrade.Visible = false;
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                    }
    //                                    lnkExcekImport.Enabled = false;
    //                                    //btnReGrade.Enabled = false;
    //                                    //btnReGrade.Visible = false;
    //                                }
    //                                     else
    //                                      {

    //                                    btnSave.Enabled = true;
    //                                    btnLock.Enabled = true;

    //                                    btnSave.Visible = true;
    //                                    btnLock.Visible = true;
    //                                   if (TestMark > 0)
    //                                    {

    //                                        if (extermarmew == "0.00")
    //                                        {
    //                                            string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                            string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                            string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                            DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                            if (dschk.Tables.Count > 0)
    //                                            {
    //                                                if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                {
    //                                                    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                    if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                    {
    //                                                        // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

    //                                                        btnGrade.Enabled = false;
    //                                                        btnGrade.Visible = false;

    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                        {
    //                                                            btnReGrade.Enabled = true;
    //                                                            btnReGrade.Visible = true;
    //                                                            btnGrade.Enabled = false;
    //                                                            btnGrade.Visible = false;
    //                                                        }
    //                                                        else
    //                                                        {
    //                                                            btnReGrade.Enabled = false;
    //                                                            btnReGrade.Visible = false;
    //                                                            btnGrade.Enabled = true;
    //                                                            btnGrade.Visible = true;
    //                                                        }

    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                        else
    //                                        {
    //                                            btnGrade.Enabled = false;
    //                                            btnGrade.Visible = false;
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                    }
    //                                    lnkExcekImport.Enabled = false;

    //                                    //btnReGrade.Enabled = false;
    //                                    //btnReGrade.Visible = false;
    //                            }
    //                           #endregion
    //                                }
    //                             else
    //                            {
    //                                gvStudent.Columns[5].Visible = false;
    //                                gvStudent.Columns[6].Visible = false;
    //                                btnSave.Enabled = true;
    //                                btnLock.Enabled = true;
    //                                btnSave.Visible = true;
    //                                btnLock.Visible = true;
    //                                lnkExcekImport.Enabled = true;
    //                                if (TestMark > 0)
    //                                {
    //                                    if (extermarmew == "0.00")
    //                                    {
    //                                        string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                        string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                        string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                        DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                        if (dschk.Tables.Count > 0)
    //                                        {
    //                                            if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                            {
    //                                                string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                {
    //                                                    btnGrade.Enabled = false;
    //                                                    btnGrade.Visible = false;
    //                                                }
    //                                                else
    //                                                {
    //                                                    if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                    {
    //                                                        btnReGrade.Enabled = true;
    //                                                        btnReGrade.Visible = true;
    //                                                        btnGrade.Enabled = false;
    //                                                        btnGrade.Visible = false;
    //                                                    }
    //                                                    else
    //                                                    {
    //                                                        btnReGrade.Enabled = false;
    //                                                        btnReGrade.Visible = false;
    //                                                        btnGrade.Enabled = true;
    //                                                        btnGrade.Visible = true;
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    btnReGrade.Enabled = false;
    //                                    btnReGrade.Visible = false;
    //                                }
    //                            }
    //                        }
    //                        else if (SESSION_TYPE == 2)
    //                           {

    //                               if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
    //                               {
    //                                   #region
    //                                   if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                   {

    //                                       btnSave.Enabled = false;
    //                                       btnLock.Enabled = false;

    //                                       btnSave.Visible = false;
    //                                       btnLock.Visible = false;
    //                                       if (TestMark > 0)
    //                                       {

    //                                           if (extermarmew == "0.00")
    //                                           {
    //                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                               if (dschk.Tables.Count > 0)
    //                                               {
    //                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                   {
    //                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                       {
    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;

    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                           {
    //                                                               btnReGrade.Enabled = true;
    //                                                               btnReGrade.Visible = true;
    //                                                               btnGrade.Enabled = false;
    //                                                               btnGrade.Visible = false;
    //                                                           }
    //                                                           else
    //                                                           {
    //                                                               btnReGrade.Enabled = false;
    //                                                               btnReGrade.Visible = false;
    //                                                               btnGrade.Enabled = true;
    //                                                               btnGrade.Visible = true;
    //                                                           }

    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                           else
    //                                           {
    //                                               btnGrade.Enabled = false;
    //                                               btnGrade.Visible = false;
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                       lnkExcekImport.Enabled = false;

    //                                   }
    //                                   else
    //                                   {

    //                                       btnSave.Enabled = true;
    //                                       btnLock.Enabled = true;

    //                                       btnSave.Visible = true;
    //                                       btnLock.Visible = true;
    //                                       if (TestMark > 0)
    //                                       {

    //                                           if (extermarmew == "0.00")
    //                                           {
    //                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                               if (dschk.Tables.Count > 0)
    //                                               {
    //                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                   {
    //                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                       {
                                                               

    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;

    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                           {
    //                                                               btnReGrade.Enabled = true;
    //                                                               btnReGrade.Visible = true;
    //                                                               btnGrade.Enabled = false;
    //                                                               btnGrade.Visible = false;
    //                                                           }
    //                                                           else
    //                                                           {
    //                                                               btnReGrade.Enabled = false;
    //                                                               btnReGrade.Visible = false;
    //                                                               btnGrade.Enabled = true;
    //                                                               btnGrade.Visible = true;
    //                                                           }

    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                           else
    //                                           {
    //                                               btnGrade.Enabled = false;
    //                                               btnGrade.Visible = false;
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                       lnkExcekImport.Enabled = false;
    //                                  }
    //                                   #endregion
    //                               }
    //                               else
    //                               {
    //                                   gvStudent.Columns[5].Visible = false;
    //                                   gvStudent.Columns[6].Visible = false;

    //                                   btnSave.Enabled = true;
    //                                   btnLock.Enabled = true;

    //                                   btnSave.Visible = true;
    //                                   btnLock.Visible = true;
    //                                   lnkExcekImport.Enabled = true;
                                
    //                                   if (TestMark > 0)
    //                                   {
    //                                       if (extermarmew == "0.00")
    //                                       {
    //                                           string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                           string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                           string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                           DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                           if (dschk.Tables.Count > 0)
    //                                           {
    //                                               if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                               {
    //                                                   string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                   if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                   {
    //                                                       btnGrade.Enabled = false;
    //                                                       btnGrade.Visible = false;
    //                                                   }
    //                                                   else
    //                                                   {
    //                                                       if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                       {
    //                                                           btnReGrade.Enabled = true;
    //                                                           btnReGrade.Visible = true;
    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;
    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           btnReGrade.Enabled = false;
    //                                                           btnReGrade.Visible = false;
    //                                                           btnGrade.Enabled = true;
    //                                                           btnGrade.Visible = true;
    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                   }
    //                                   else
    //                                   {
    //                                       btnReGrade.Enabled = false;
    //                                       btnReGrade.Visible = false;
    //                                   }
    //                               }

    //                           }

    //                           #region Session_type3

    //                           ///******************added by prafull on dt:12092023 For Remedial ExamType 2******************************
    //                           else if(SESSION_TYPE==3)
    //                           {

    //                               if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
    //                               {
    //                                   #region
    //                                   if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                   {

    //                                       btnSave.Enabled = false;
    //                                       btnLock.Enabled = false;

    //                                       btnSave.Visible = false;
    //                                       btnLock.Visible = false;
    //                                       if (TestMark > 0)
    //                                       {

    //                                           if (extermarmew == "0.00")
    //                                           {
    //                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                               if (dschk.Tables.Count > 0)
    //                                               {
    //                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                   {
    //                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                       {
    //                                                           // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;

    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                           {
    //                                                               btnReGrade.Enabled = true;
    //                                                               btnReGrade.Visible = true;
    //                                                               btnGrade.Enabled = false;
    //                                                               btnGrade.Visible = false;
    //                                                           }
    //                                                           else
    //                                                           {
    //                                                               btnReGrade.Enabled = false;
    //                                                               btnReGrade.Visible = false;
    //                                                               btnGrade.Enabled = true;
    //                                                               btnGrade.Visible = true;
    //                                                           }

    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                           else
    //                                           {
    //                                               btnGrade.Enabled = false;
    //                                               btnGrade.Visible = false;
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                       lnkExcekImport.Enabled = false;

    //                                   }
    //                                   else
    //                                   {

    //                                       btnSave.Enabled = true;
    //                                       btnLock.Enabled = true;

    //                                       btnSave.Visible = true;
    //                                       btnLock.Visible = true;
    //                                       if (TestMark > 0)
    //                                       {

    //                                           if (extermarmew == "0.00")
    //                                           {
    //                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                               if (dschk.Tables.Count > 0)
    //                                               {
    //                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                                   {
    //                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                       {


    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;

    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                           {
    //                                                               btnReGrade.Enabled = true;
    //                                                               btnReGrade.Visible = true;
    //                                                               btnGrade.Enabled = false;
    //                                                               btnGrade.Visible = false;
    //                                                           }
    //                                                           else
    //                                                           {
    //                                                               btnReGrade.Enabled = false;
    //                                                               btnReGrade.Visible = false;
    //                                                               btnGrade.Enabled = true;
    //                                                               btnGrade.Visible = true;
    //                                                           }

    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                           else
    //                                           {
    //                                               btnGrade.Enabled = false;
    //                                               btnGrade.Visible = false;
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                       lnkExcekImport.Enabled = false;
    //                                   }
    //                                   #endregion
    //                               }
    //                               else
    //                               {
    //                                   gvStudent.Columns[5].Visible = false;
    //                                   gvStudent.Columns[6].Visible = false;

    //                                   btnSave.Enabled = true;
    //                                   btnLock.Enabled = true;

    //                                   btnSave.Visible = true;
    //                                   btnLock.Visible = true;
    //                                   lnkExcekImport.Enabled = true;
                              
    //                                   if (TestMark > 0)
    //                                   {
    //                                       if (extermarmew == "0.00")
    //                                       {
    //                                           string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                           string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                           string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                           DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                           if (dschk.Tables.Count > 0)
    //                                           {
    //                                               if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                               {
    //                                                   string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                                   if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                                   {
    //                                                       btnGrade.Enabled = false;
    //                                                       btnGrade.Visible = false;

    //                                                   }
    //                                                   else
    //                                                   {
    //                                                       if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                                       {
    //                                                           btnReGrade.Enabled = true;
    //                                                           btnReGrade.Visible = true;
    //                                                           btnGrade.Enabled = false;
    //                                                           btnGrade.Visible = false;
    //                                                       }
    //                                                       else
    //                                                       {
    //                                                           btnReGrade.Enabled = false;
    //                                                           btnReGrade.Visible = false;
    //                                                           btnGrade.Enabled = true;
    //                                                           btnGrade.Visible = true;
    //                                                       }
    //                                                   }
    //                                               }
    //                                           }
    //                                       }
    //                                       else
    //                                       {
    //                                           btnGrade.Enabled = false;
    //                                           btnGrade.Visible = false;
    //                                       }
    //                                   }
    //                                   else
    //                                   {
    //                                       btnReGrade.Enabled = false;
    //                                       btnReGrade.Visible = false;
    //                                   }
                                  
    //                               }
    //                           }
                           
    //                        #endregion
    //                        pnlStudGrid.Visible = true;                         
    //                        lblStudents.Visible = true;

                          
    //                        if (TestMark > 0)
    //                        {
    //                            if (extermarmew == "0.00")
    //                            {
    //                                string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
    //                                string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
    //                                string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
    //                                DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
    //                                if (dschk.Tables.Count > 0)
    //                                {
    //                                    if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
    //                                    {
    //                                        string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

    //                                        if (islocked == "0" || islocked == string.Empty || islocked == null)
    //                                        {
    //                                            gvStudent.Columns[7].Visible = false;
    //                                            btnReGrade.Enabled = false;
    //                                            btnReGrade.Visible = false;

    //                                        }
    //                                        else
    //                                        {
    //                                            if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                                            {
    //                                                gvStudent.Columns[7].Visible = true;
    //                                                btnReGrade.Enabled = true;
    //                                                btnReGrade.Visible = true;
    //                                            }
    //                                            else
    //                                            {
    //                                                gvStudent.Columns[7].Visible = false;
    //                                                btnReGrade.Enabled = false;
    //                                                btnReGrade.Visible = false;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                gvStudent.Columns[7].Visible = false;
    //                                btnReGrade.Enabled = false;
    //                                btnReGrade.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            gvStudent.Columns[7].Visible = false;
    //                            btnReGrade.Enabled = false;
    //                            btnReGrade.Visible = false;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
    //                        if (SESSION_TYPE == 1)
    //                        {
    //                            if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                            {

    //                                if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = false;
    //                                    btnLock.Enabled = false;

    //                                    btnSave.Visible = false;
    //                                    btnLock.Visible = false;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = true;
    //                                    btnLock.Enabled = true;

    //                                    btnSave.Visible = true;
    //                                    btnLock.Visible = true;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }

    //                                }

    //                                //end 

    //                            }
    //                            else
    //                            {
    //                                gvStudent.Columns[3].Visible = true;
    //                                gvStudent.Columns[6].Visible = false;
    //                                //lnkExcekImport.Visible = true;
    //                            }
    //                        }

    //                        else if (SESSION_TYPE == 2)
    //                        {
    //                            if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                            {

    //                                if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = false;
    //                                    btnLock.Enabled = false;

    //                                    btnSave.Visible = false;
    //                                    btnLock.Visible = false;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = true;
    //                                    btnLock.Enabled = true;

    //                                    btnSave.Visible = true;
    //                                    btnLock.Visible = true;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }

    //                                }

    //                                //end 

    //                            }
    //                            else
    //                            {
    //                                gvStudent.Columns[3].Visible = true;
    //                                gvStudent.Columns[6].Visible = false;
    //                                //lnkExcekImport.Visible = true;
    //                            }
    //                        }
    //                        else if (SESSION_TYPE == 3)
    //                        {
    //                            if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                            {

    //                                if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = false;
    //                                    btnLock.Enabled = false;

    //                                    btnSave.Visible = false;
    //                                    btnLock.Visible = false;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {

    //                                    gvStudent.Columns[3].Visible = true;
    //                                    gvStudent.Columns[6].Visible = true;
    //                                    //gvStudent.Columns[4].Enabled = false;
    //                                    btnSave.Enabled = true;
    //                                    btnLock.Enabled = true;

    //                                    btnSave.Visible = true;
    //                                    btnLock.Visible = true;
    //                                    btnExcelReport.Enabled = true;
    //                                    btnExcelReport.Visible = true;
    //                                    //lnkExcekImport.Visible = false;

    //                                    int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

    //                                    //added by prafull on dt 30032023  for grade button only for admin login

    //                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;

    //                                        btnEndSemReport.Visible = false;
    //                                    }
    //                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                    {
    //                                        btnGrade.Enabled = true;
    //                                        btnGrade.Visible = true;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnGrade.Enabled = false;
    //                                        btnGrade.Visible = false;
    //                                        btnEndSemReport.Visible = true;
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                gvStudent.Columns[3].Visible = true;
    //                                gvStudent.Columns[6].Visible = false;
    //                                //lnkExcekImport.Visible = true;
    //                            }
    //                        }
    //                        pnlStudGrid.Visible = true;                          
    //                        lblStudents.Visible = true;



    //                        if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
    //                        {
    //                            if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
    //                            {
    //                                gvStudent.Columns[7].Visible = true;
    //                                //gvStudent.Columns[5].Visible = true;
    //                                btnSave.Enabled = false;
    //                                btnLock.Enabled = false;

    //                                btnSave.Visible = false;
    //                                btnLock.Visible = false;
    //                                btnGrade.Enabled = false;
    //                                btnGrade.Visible = false;



    //                                //added by prafull on dt 30032023  for grade button only for admin login

    //                                if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                {
    //                                    btnReGrade.Enabled = true;
    //                                    btnReGrade.Visible = true;
    //                                }
    //                                else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                {
    //                                    btnReGrade.Enabled = true;
    //                                    btnReGrade.Visible = true;
    //                                }
    //                                else
    //                                {
    //                                    btnReGrade.Enabled = false;
    //                                    btnReGrade.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {

    //                                gvStudent.Columns[7].Visible = false;
    //                                gvStudent.Columns[6].Visible = false;
    //                                //gvStudent.Columns[5].Visible = true;
    //                                btnSave.Enabled = true;
    //                                btnLock.Enabled = true;

    //                                btnSave.Visible = true;
    //                                btnLock.Visible = true;
    //                                btnGrade.Enabled = false;
    //                                btnGrade.Visible = false;



    //                                //added by prafull on dt 30032023  for grade button only for admin login

    //                                if (Convert.ToInt32(Session["OrgId"]) != 6)
    //                                {
    //                                    btnReGrade.Enabled = false;
    //                                    btnReGrade.Visible = false;
    //                                }
    //                                else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
    //                                {
    //                                    btnReGrade.Enabled = false;
    //                                    btnReGrade.Visible = false;
    //                                }
    //                                else
    //                                {
    //                                    btnReGrade.Enabled = false;
    //                                    btnReGrade.Visible = false;
    //                                }

    //                            }
                             
    //                        }
    //                        else
    //                        {
    //                            gvStudent.Columns[7].Visible = false;
    //                            // btnSave.Enabled = true;
    //                            // btnLock.Enabled = true;
    //                            //   btnSave.Visible = true;
    //                            //  btnLock.Visible = true;
    //                            // btnGrade.Enabled = false;
    //                            //btnGrade.Visible = false;
    //                            btnReGrade.Enabled = false;
    //                            btnReGrade.Visible = false;

    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
    //                }
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "Academic_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server Unavailable.");
    //            objCommon.DisplayMessage(ex.ToString(), this.Page);
    //        }
        
    //}

   
   
    //protected void btnGrade_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //check for if any exams on
    //        if (ddlExam.SelectedIndex > 0)
    //        {
    //            string studids = string.Empty;

    //            MarksEntryController objMarksEntry = new MarksEntryController();
    //            Label lbl;
    //            CheckBox chk;
    //            for (int i = 0; i < gvStudent.Rows.Count; i++)
    //            {
    //                chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;

    //                //Gather Student IDs 
    //                lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
    //                studids += lbl.ToolTip + ",";

    //            }
    //            if (studids == string.Empty)
    //            {
    //                objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
    //                return;
    //            }

    //            CustomStatus cs;
    //            if (Convert.ToInt32(Session["OrgId"]) == 8)
    //            {
    //                cs = (CustomStatus)objMarksEntry.GradeGenaerationNew_MIT(studids, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
    //            }
    //            else
    //            {
    //                cs = (CustomStatus)objMarksEntry.GradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
    //            }
    //            if (cs.Equals(CustomStatus.RecordSaved))
    //            {
    //                objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
    //                //btnReport.Enabled = true;
    //                ShowStudents();
    //            }
    //        }
    //        else
    //        {
    //            int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));

    //            if (ddlSubjectType.SelectedValue == "10" || Is_Specialcase == 1)
    //            {
    //                string examtype = string.Empty;
    //                string Subexam = string.Empty;
    //                int lock_status = 1;
    //                string studids = string.Empty;

    //                MarksEntryController objMarksEntry = new MarksEntryController();
    //                Label lbl;
    //                CheckBox chk;


    //                string marks = string.Empty;
    //                TextBox txtMarks;

    //                for (int i = 0; i < gvStudent.Rows.Count; i++)
    //                {
    //                    chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;
    //                    if (lock_status == 0)
    //                    {
    //                        //Gather Student IDs 
    //                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
    //                        studids += lbl.ToolTip + ",";

    //                        //Gather Exam Marks 
    //                        txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
    //                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                    }
    //                    else if (lock_status == 1 || lock_status == 2)
    //                    {
    //                        //Gather Student IDs 
    //                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
    //                        studids += lbl.ToolTip + ",";

    //                        //Gather Exam Marks 
    //                        txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
    //                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                    }
    //                }


    //                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

    //                string Exam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));


    //                string examname = string.Empty;


    //                string SubExamName = string.Empty;
    //                string SubExamComponentName = string.Empty;

    //                if (divSubExamName.Visible == true)
    //                {
    //                    SubExamName = ddlSubExamName.SelectedValue;
    //                    SubExamComponentName = ddlSubExamName.SelectedItem.Text;
    //                }

    //                CustomStatus cs1 = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[0]), SubExamComponentName);
    //                if (cs1.Equals(CustomStatus.RecordSaved))
    //                {
    //                    if (lock_status == 1)
    //                    {
    //                        // objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
    //                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
    //                    }
    //                    else if (lock_status == 2)
    //                    {
    //                        objCommon.DisplayMessage(updpnl, "Marks Unlocked Successfully!!!", this.Page);
    //                    }
    //                    else
    //                        objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
    //                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);


    //                    CustomStatus cs = (CustomStatus)objMarksEntry.GradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
    //                    if (cs.Equals(CustomStatus.RecordSaved))
    //                    {
    //                        objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
    //                        //btnReport.Enabled = true;
    //                        //ShowStudentsSpecialSubject();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updpnl, "Please Select Exam Name.", this.Page);
    //                return;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}




    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {
        try
        {

            //DataSet dsStudent = null;
            //string excelname = string.Empty;
            //string sp_procedure = "PKG_COURSE_SP_GET_STUD_FOR_COURSES_MARKS_ENTRY_FOR_EXTERNAL_BULK_UPLAOD_ADMIN_CC";
            //string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_UA_NO";
            //string sp_callValues = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + ","+Convert.ToInt32(Session["userno"])+ "";
            //dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            //if (dsStudent != null && dsStudent.Tables.Count > 0)
            //{
            //    if (dsStudent.Tables[0].Rows.Count > 0)
            //    {

            //        excelname = Session["username"].ToString() + '_' + ddlcollege.SelectedItem.Text  + '_' +  DateTime.Now.ToString("dd-MM-yyyy");
            //        ViewState["StudCount"] = dsStudent.Tables[0].Rows.Count;
            //        //Bind the Student List
            //        DataTable dst = dsStudent.Tables[0];
            //        DataGrid dg = new DataGrid();
            //        if (dsStudent != null && dsStudent.Tables.Count > 0)
            //        {
            //            if (dsStudent.Tables[0].Rows.Count > 0)
            //            {
            //                string[] selectedColumns = new[] { "IDNO", "STUDNAME", "REGNO1", "CCODE", "COURSENO", "COURSENAME", "DEGREENAME", "BRANCHNAME", "SCHEMENAME", "SEMESTERNAME", "SEMESTERNO", "SESSIONNAME", "EXAMNAME", "SUBEXAMNAME", "SECTIONNAME", "MAXMARK", "MARK" };

            //                DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
            //                dt.Columns["REGNO1"].ColumnName = "REGNO"; // change column names
                     
            //                using (XLWorkbook wb = new XLWorkbook())
            //                {
                               
            //                    wb.Worksheets.Add(dt);
                           
            //                    Response.Clear();
            //                    Response.Buffer = true;
            //                    Response.Charset = "";
            //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //                    Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");
            //                    using (MemoryStream MyMemoryStream = new MemoryStream())
            //                    {
            //                        wb.SaveAs(MyMemoryStream);
            //                        MyMemoryStream.WriteTo(Response.OutputStream);
            //                        Response.Flush();
            //                        //Response.End();
            //                    }
            //                }
            //            }
            //        }
            //        //   pnlSelection.Visible = false;
            //        pnlMarkEntry.Visible = true;
            //        pnlStudGrid.Visible = true;
            //        lblStudents.Visible = true;
            //        //btnBack.Visible = true;

            //    }
            //    else
            //    {
            //        //objCommon.DisplayMessage(updpanle1, "Students Not Found..!!", this.Page);
            //    }
            //}



           // DataSet ds = null;// Subject.RetrieveCourseMasterDataForExcel();
           // DataTable dt;
           // //ataSet dsStudent = null;
           // string excelname = string.Empty;
           // string sp_procedure = "PKG_COURSE_SP_GET_STUD_FOR_COURSES_MARKS_ENTRY_FOR_EXTERNAL_BULK_UPLAOD_ADMIN_CC";
           // string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_UA_NO";
           // string sp_callValues = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "";
           // ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

           // ds.Tables[0].TableName = "TableMark";
           //// ds.Tables[1].TableName = "Subject Type";
           //// ds.Tables[2].TableName = "Departments";


           // if (ds.Tables[0].Rows.Count > 0)
           // {
           //     using (XLWorkbook wb = new XLWorkbook())
           //     {
           //         foreach (System.Data.DataTable dt in ds.Tables)
           //         {
           //             //Add System.Data.DataTable as Worksheet.


           //             //wb.Worksheets.Add(dt);
           //             var ws = wb.Worksheets.Add(dt, dt.TableName.ToString());
           //           //  var ws = wb.Worksheets.Add(dt);
           //             for (int j = 1; j <= ds.Tables[0].Columns.Count; j++)
           //             {

           //                 if (j != ds.Tables[0].Columns.Count)
           //                 {
           //                     ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 64, 0); //All columns of second row
           //                 }
           //                 else
           //                 {
           //                     ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 150, 255); //All columns of second row
           //                 }
           //             }
           //         }

           //         //Export the Excel file.
           //         Response.Clear();
           //         Response.Buffer = true;
           //         Response.Charset = "";
           //         Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
           //         Response.AddHeader("content-disposition", "attachment;filename=Mark_Entry_Blank_Tempalte.xlsx");
           //         using (MemoryStream MyMemoryStream = new MemoryStream())
           //         {
           //             wb.SaveAs(MyMemoryStream);
           //             MyMemoryStream.WriteTo(Response.OutputStream);
           //             Response.Flush();
           //             Response.End();
           //         }
           //     }
           // }

            DataSet ds = null;// Subject.RetrieveCourseMasterDataForExcel();
            string excelname = string.Empty;
            excelname = Session["username"].ToString() + '_' + ddlcollege.SelectedItem.Text + '_' + DateTime.Now.ToString("dd-MM-yyyy");
           // DataTable dt;
            //ataSet dsStudent = null;
           
            string sp_procedure = "PKG_COURSE_SP_GET_STUD_FOR_COURSES_MARKS_ENTRY_FOR_EXTERNAL_BULK_UPLAOD_ADMIN_CC";
            string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_UA_NO";
            string sp_callValues = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "";
            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            DataTable dt = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                      //  Response.End();
                    }
                }
                ddlcollege.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "No Data Found..!!!", this.Page);
                return;
            }
         



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }
    private bool CheckExcelMarks(int lock_status, DataTable dt)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;
            DataColumnCollection columns = dt.Columns;
            if (columns.Contains("MARK"))
            {
                for (int j = 8; j < dt.Columns.Count; j++)    //columns
                {
                    for (int i = 0; i < dt.Rows.Count; i++)   //rows 
                    {

                        if (j == 8) // MARKS
                        {


                            // maxMarks = 
                            string sessionno = dt.Rows[i]["SESSIONNO"].ToString();

                            if(sessionno != ddlcollege.SelectedValue.ToString())
                            {
                               objCommon.DisplayMessage(updpnl, "Wrong File is Selected or Wrong Session is selected..please select proper file and session!!", this);
                                 return false;
                                 break;
                            }

                            maxMarks = dt.Rows[i]["MAXMARK"].ToString();
                            marks = dt.Rows[i]["MARK"].ToString();
                            if (!marks.Equals(string.Empty) && !maxMarks.Equals(string.Empty))
                            {
                                if (marks == "")
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                        //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");

                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToDouble(marks) > Convert.ToDouble(maxMarks))
                                    {

                                        if (Convert.ToDouble(marks) != 902 && Convert.ToDouble(marks) != 903 && Convert.ToDouble(marks) != 904 && Convert.ToDouble(marks) != 905 && Convert.ToDouble(marks) != 906)
                                        {
                                            ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");

                                            flag = false;
                                            break;
                                        }
                                    }
                                    else if (Convert.ToDouble(marks) < 0)
                                    {
                                       
                                        ShowMessage("Marks Entered [" + marks + "] cannot be Less than 0 (zero).");
                                            flag = false;
                                            break;
                                       
                                    }
                                }
                            }
                            else
                            {
                                ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                flag = false;
                                break;
                            }
                        }

                        if (flag == false)
                            break;
                    }
                }
            }
            else
            {
                //ShowMessage("Invalid Excel File !!");
                flag = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    private void SaveExcelMarks(int lock_status, DataTable dt, int semno)
    {
        try
        {
            
            CustomStatus cs = CustomStatus.Error;
            CustomStatus log = CustomStatus.Error;
            string file_name = ViewState["FileName"].ToString();
            int FlagReval = 0;
     
            string studids = string.Empty;
            string marks = string.Empty;
            string courseno = string.Empty;
            string ccode = string.Empty;
            string semesterno = string.Empty;

            MarksEntryController objMarksEntry = new MarksEntryController();
            Label lbl;
            TextBox txtMarks;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Note : -100 for Marks will be converted as NULL           
            //NULL means mark entry not done.                           
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                studids += dt.Rows[i]["IDNO"].ToString() + ",";
                marks += dt.Rows[i]["MARKS"].ToString() == string.Empty ? "-100," : dt.Rows[i]["MARKS"].ToString() + ",";
            }
            //return;
            if (!string.IsNullOrEmpty(studids))
            {

              
                   // cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
               
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    //ShowStudents();
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Marks Saved Successfully.", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                   // ShowStudents();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    private bool CheckFormatAndImport(string Extension, string FilePath, string isHDR)  //bool
    {
        string filename = ViewState["FileName"].ToString();
        Exam objExam = new Exam();
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 Excel07ConString
                //   conStr = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }

       
        conStr = String.Format(conStr, FilePath);

        string Message = string.Empty;
        int count = 0;
        OleDbConnection connExcel = new OleDbConnection(conStr);
       
            try
            {

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

            //string SheetName = "TableMark$";//dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

           /// Check for lock and null marks
            if (CheckExcelMarks(0, dt) == false)
            {
                return false;
            }
            else
            {

                 string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_BULK_UPLOAD_CC";
                 string sp_parameters = "@P_SEMESTERNO,@P_UA_NO,@P_SESSIONNO";
                 string sp_callValues = "" + (ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "," + ddlcollege.SelectedValue + "";
               
                 DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                 if (dschk.Tables.Count > 0)
                 {
                     if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                     {
                         string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                         if (islocked == "0" || islocked == string.Empty || islocked == null)
                         {
                             objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Upload the External marks..!!!", this.Page);
                             return false;
                         }
                     }
                 }



                DataTable dtExcelData = new DataTable();
                DataTable dtmarkrange = new DataTable();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                OleDbDataReader dr = cmdExcel.ExecuteReader();
                if (dr.HasRows)
                {
                    //ContentTable = new DataTable();
                    dtmarkrange.Columns.Add("REGNO", typeof(string));
                    dtmarkrange.Columns.Add("CCODE", typeof(string));
                    dtmarkrange.Columns.Add("COURSENO", typeof(Int32));
                    dtmarkrange.Columns.Add("SESSIONNO", typeof(Int32));
                    dtmarkrange.Columns.Add("SEMESTERNO", typeof(Int32));
                    dtmarkrange.Columns.Add("SECTIONNO", typeof(Int32));
                    dtmarkrange.Columns.Add("EXAMNAME", typeof(string));
                    dtmarkrange.Columns.Add("SUBEXAMNAME", typeof(string));
                    dtmarkrange.Columns.Add("MAXMARK", typeof(decimal));
                    dtmarkrange.Columns.Add("MARK", typeof(decimal));
                    using (OleDbDataAdapter oda1 = new OleDbDataAdapter("SELECT * FROM [" + SheetName + "]", connExcel))
                    {
                        oda1.Fill(dtmarkrange);
                    }

                   

                }

                    string SP_Name = "PKG_ACD_BULK_UPLOAD_EXTERNAL_MARKS_CC";
                    string SP_Parameters = "@P_TBL_GRADE_DATA, @P_SESSIONNO, @P_SEMESTERNO, @P_UA_NO,@P_OUT";
                    string Call_Values = " 0," + ddlcollege.SelectedValue + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + ",0";

                    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, dtmarkrange, true, 1);
                   // DataSet ds1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, dtmarkrange);

                    if (que_out == "1")
                {
                    objCommon.DisplayMessage(updpnl, "Mark Uploaded Sucessfully !!!", this.Page);
                    return true;

                }
                else{
                    objCommon.DisplayMessage(updpnl, "Error in Importing File !!", this.Page);
                    return false;
                }
          
            }
            }

            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(this.Page, "Server UnAvailable");
                return false;
            }
            finally
            {
                connExcel.Close();
                connExcel.Dispose();
            }

            //return true;
        }
    
   
    //protected void CreateBlobContainer(string Name)
    //{

    //    //Get the reference of the Storage Account
    //    CloudStorageAccount storageaccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString());
    //    //Get the reference of the Storage Blob

    //    CloudBlobClient client = storageaccount.CreateCloudBlobClient();

    //    //Get the reference of the Container. The GetConainerReference doesn't make a request to the Blob Storage but the Create() &CreateIfNotExists() method does. The method CreateIfNotExists() could be use whether the Container exists or not
    //    CloudBlobContainer container = client.GetContainerReference(Name);
    //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //    container.CreateIfNotExists();
    //}

    //private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    //{
    //    CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
    //    CloudBlobClient client = account.CreateCloudBlobClient();
    //    CloudBlobContainer container = client.GetContainerReference(ContainerName);
    //    return container;
    //}

    //public void DeleteIFExits(string FileName)
    //{
    //    CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
    //    string FN = Path.GetFileNameWithoutExtension(FileName);
    //    try
    //    {
    //        Parallel.ForEach(container.ListBlobs(FN, true), y =>
    //        {
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            ((CloudBlockBlob)y).DeleteIfExists();
    //        });
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    //public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    //{
    //    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
    //    int retval = 1;
    //    string Ext = Path.GetExtension(FU.FileName);
    //    string FileName = DocName;
    //    try
    //    {
    //        DeleteIFExits(FileName);
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        container.CreateIfNotExists();
    //        container.SetPermissions(new BlobContainerPermissions
    //        {
    //            PublicAccess = BlobContainerPublicAccessType.Blob
    //        });

    //        CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
    //        cblob.UploadFromStream(FU.PostedFile.InputStream);
    //    }
    //    catch
    //    {
    //        retval = 0;
    //        return retval;
    //    }
    //    return retval;
    //}


   
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER SM ON SM.SEMESTERNO=SR.SEMESTERNO","DISTINCT SM.SEMESTERNO","SEMESTERNAME","SESSIONNO="+ddlcollege.SelectedValue+" AND ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED=1 AND ISNULL(REGISTERED,0)=1","SEMESTERNO");
            ddlSemester.Focus();
        }
    }
    protected void btnprocess_Click(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "key", "alert('Please Select College & Session.');", true);
            return;
        }


        if (FuBrowse.HasFile)           //  if (FuBrowse.HasFile) //(FuBrowse.PostedFile != null)
        {
                       


            string FileName = Path.GetFileName(FuBrowse.PostedFile.FileName);
            string Extension = Path.GetExtension(FuBrowse.PostedFile.FileName);
            if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
            {
                string path = MapPath("~/ExcelData/");
              
                if (!(Directory.Exists(MapPath("~/ExcelData"))))
                    Directory.CreateDirectory(path);
                string fileName1 = FuBrowse.PostedFile.FileName.Trim();
                //string fileName = "" + Session["userno"] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName1 + "";
                if (File.Exists((path + fileName1).ToString()))
                    File.Delete((path + fileName1).ToString());
                FuBrowse.SaveAs(path + fileName1);
                string Filepath = Server.MapPath("~/ExcelData/" + fileName1);
                ViewState["FileName"] = fileName1;


               // string contentType = contentType = FuBrowse.PostedFile.ContentType;
               // string filename = Path.GetFileName(FuBrowse.PostedFile.FileName);
                //int retval = Blob_Upload(blob_ConStr, blob_ContainerName, filename, FuBrowse);
                //if (retval == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                //    return;
                //}



                if (!CheckFormatAndImport(Extension, Filepath, "yes"))
                {
                    File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                }

                else
                {
                    //int retval = Blob_Upload(blob_ConStr, blob_ContainerName, filename, FuBrowse);
                    //if (retval == 0)
                    //{
                    //}
                    //else
                    //{
                    //    File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                    //}
                    //  ShowStudents();
                    //  pnlUP.Visible = false;
                    objCommon.DisplayMessage(updpnl, "Mark Entry Uploaded Successfully !", this);
                }

                //string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                //string FilePath = Server.MapPath(FolderPath + FileName);
                //fu.SaveAs(FilePath);

                // if required you add in blob later




                ///added by prafull on dt-16-06-2023  to download the file from blob to get the data

                //return;


                //string folderName = "~/DownloadImg" + "/";

                //string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
                //string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
                //string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

                //StorageCredentials creden = new StorageCredentials(accountname, accesskey);
                //CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
                //CloudBlobClient client = acc.CreateCloudBlobClient();
                //CloudBlobContainer container = client.GetContainerReference(containerName);

                //CloudBlob blob = container.GetBlobReference("top/" + fileName1);




                //string directoryPath = Server.MapPath(folderName).ToString();
                //string filePath = directoryPath + "\\" + fileName1;
                //if (!Directory.Exists(directoryPath))
                //{
                //    Directory.CreateDirectory(directoryPath);
                //}

                //blob.DownloadToFile(filePath, FileMode.OpenOrCreate);

                //  string FilePath =
              //  ExcelToDatabase(filePath, Extension, "yes", CCode);
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Only .xls or .xlsx extention is allowed", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(updpnl, "Please Select File to Upload!!!", this);
            return;
        }
    }
         //added by prafull

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    //For Top file upload
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            CloudBlobDirectory directory = container.GetDirectoryReference("top");
            CloudBlockBlob cblob = directory.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }   

    //*********************************

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //SubjectMasterController Subject = new SubjectMasterController();
        DataSet ds = null;// Subject.RetrieveCourseMasterDataForExcel();

        //ataSet dsStudent = null;
        string excelname = string.Empty;
        string sp_procedure = "PKG_COURSE_SP_GET_STUD_FOR_COURSES_MARKS_ENTRY_FOR_EXTERNAL_BULK_UPLAOD_ADMIN_CC";
        string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_UA_NO";
        string sp_callValues = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "";
        ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

        ds.Tables[0].TableName = "Subject Data Template";
        ds.Tables[1].TableName = "Subject Type";
        ds.Tables[2].TableName = "Departments";


        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.


                    //wb.Worksheets.Add(dt);
                    //var ws = wb.Worksheets.Add(dt, dt.TableName.ToString());
                    var ws = wb.Worksheets.Add(dt);
                    for (int j = 1; j <= ds.Tables[0].Columns.Count; j++)
                    {

                        if (j != ds.Tables[0].Columns.Count)
                        {
                            ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(255, 64, 0); //All columns of second row
                        }
                        else
                        {
                            ws.Cell(1, j).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 150, 255); //All columns of second row
                        }
                    }
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadCourseData.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    //**************************************************

    protected void btnreportbeforemark_Click(object sender, EventArgs e)
    {
        GridView GVStudData = new GridView();



        string SP_Name = "PKG_ACD_EXTERNAL_MARK_ENTRY_REPORT_BULK_CC";
        string SP_Parameters = "@P_SESSIONNO, @P_SEMESTERNO";
      
        string Call_Values = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStudData.DataSource = ds;
            GVStudData.DataBind();

            string attachment = "attachment;filename=InternalExternalMarkentryExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
        }

    }
    protected void btnreportaftermark_Click(object sender, EventArgs e)
    {

        GridView GVStudData = new GridView();



        string SP_Name = "PKG_ACD_EXTERNAL_MARK_ENTRY_REPORT_BULK_CC";
        string SP_Parameters = "@P_SESSIONNO, @P_SEMESTERNO";

        string Call_Values = "" + Convert.ToInt32(ddlcollege.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStudData.DataSource = ds;
            GVStudData.DataBind();

            string attachment = "attachment;filename=InternalExternalMarkentryExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
        }

    }
}

