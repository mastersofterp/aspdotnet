using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.UI.WebControls.WebParts;
using ClosedXML.Excel;

public partial class ACADEMIC_OnlinePaymentLog : System.Web.UI.Page
    {
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();


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
                    // CheckPageAuthorization();
                    PopulateDropDownList();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        }
                    }
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        }

    private void CheckPageAuthorization()
        {
        if (Request.QueryString["pageno"] != null)
            {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentLog.aspx");
                }
            }
        else
            {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlinePaymentLog.aspx");
            }
        }

    protected void PopulateDropDownList()
        {
        try
            {
            objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
            objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE RT INNER JOIN ACD_DEMAND D ON(RT.RECIEPT_CODE = D.RECIEPT_CODE)", "DISTINCT RT.RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "RECIEPT_TITLE");

            }
        catch (Exception ex)
            {
            throw;
            }
        }

    protected void btnCancel_Click(object sender, EventArgs e)
        {
        try
            {
            Response.Redirect(Request.Url.ToString());
            }
        catch (Exception)
            {
            throw;
            }
        }

    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlClg.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlClg.SelectedValue), "A.DEGREENO");
            }
        }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
        if (ddlDegree.SelectedIndex > 0)
            {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON A.BRANCHNO=B.BRANCHNO", "B.BRANCHNO", "A.LONGNAME", "B.DEGREENO=" + ddlDegree.SelectedValue, "A.SHORTNAME");
            }
        }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static List<Student.StudentInfo> BindList(int CollegeID, int DegreeNo, int BranchNo, int Semesterno, String ReceiptType)
    //    {
    //    StudentController objStud = new StudentController();
    //    List<Student.StudentInfo> ObjList = new List<Student.StudentInfo>();
    //    Student.StudentInfo objStudE = new Student.StudentInfo();
    //    DataSet ds = new DataSet();
    //    objStudE.College_ID = CollegeID;
    //    objStudE.Degree_no = DegreeNo;
    //    objStudE.Branch_no = BranchNo;
    //    objStudE.SemesterNo = Semesterno;
    //    objStudE.ReceiptType = ReceiptType;

    //    try
    //        {
    //        ds = objStud.GetStudentOnlinePaymentLog(CollegeID, DegreeNo, BranchNo, Semesterno, ReceiptType);

    //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
    //            {
    //            ObjList = (from DataRow dr in ds.Tables[0].Rows
    //                          select new Student.StudentInfo
    //                          {
    //                              Registrationno = dr[0].ToString(),
    //                              StudentName = dr[0].ToString(),
    //                              Degreeno = dr[0].ToString(),
    //                              Branchno = dr[0].ToString(),
    //                              SemesterName = dr[0].ToString(),
    //                              OrderID = dr[0].ToString(),
    //                              transactionID = dr[0].ToString(),
    //                              PaymentDate = dr[0].ToString(),
    //                              Amount = dr[0].ToString(),
    //                              PaymentStatus = dr[0].ToString(),
    //                          }).ToList();
    //            }
    //        }
    //    catch (Exception ex)
    //        {

    //        }
    //    finally
    //        {
    //        ds.Dispose();
    //        }
    //    return ObjList;
    //    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Student.StudentInfo> BindList(int CollegeID, int DegreeNo, int BranchNo, int Semesterno, string ReceiptType)
        {
        StudentController objStud = new StudentController();
        List<Student.StudentInfo> ObjList = new List<Student.StudentInfo>();
        Common objcommon = new Common();
        DataSet ds = new DataSet();

        try
            {            
            ds = objStud.GetStudentOnlinePaymentLog(CollegeID, DegreeNo, BranchNo, Semesterno, ReceiptType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                    Student.StudentInfo studentInfo = new Student.StudentInfo();

                    studentInfo.Registrationno = dr["REGNO"] != DBNull.Value ? dr["REGNO"].ToString() : string.Empty;
                    studentInfo.StudentName = dr["STUDNAME"] != DBNull.Value ? dr["STUDNAME"].ToString() : string.Empty;
                    studentInfo.CollegeID = dr["COLLEGE_NAME"] != DBNull.Value ? dr["COLLEGE_NAME"].ToString() : string.Empty;
                    studentInfo.Degreeno = dr["DEGREENAME"] != DBNull.Value ? dr["DEGREENAME"].ToString() : string.Empty;
                    studentInfo.Branchno = dr["BRANCHNAME"] != DBNull.Value ? dr["BRANCHNAME"].ToString() : string.Empty;
                    studentInfo.SemesterName = dr["SEMESTERNO"] != DBNull.Value ? dr["SEMESTERNO"].ToString() : string.Empty;
                    studentInfo.OrderID = dr["ORDER_ID"] != DBNull.Value ? dr["ORDER_ID"].ToString() : string.Empty;
                    studentInfo.transactionID = dr["TRANSACTIONID"] != DBNull.Value ? dr["TRANSACTIONID"].ToString() : string.Empty;
                    studentInfo.PaymentDate = dr["PAYMENTDATE"] != DBNull.Value ? dr["PAYMENTDATE"].ToString() : string.Empty;
                    studentInfo.Amount = Convert.ToDecimal(dr["TOTAL_AMT"] != DBNull.Value ? dr["TOTAL_AMT"].ToString() : string.Empty);
                    studentInfo.PaymentStatus = dr["PAYMENT_STATUS"] != DBNull.Value ? dr["PAYMENT_STATUS"].ToString() : string.Empty;

                    ObjList.Add(studentInfo);
                    }
                }
            else
                {
               //// objcommon.DisplayMessage(this.Page, "Data Not Found..!",this);
               // objcommon.DisplayMessage(Page, "Data Not Found..!!", Page);              
                }
            }
        catch (Exception ex)
            {
            throw;
            }
        finally
            {
            ds.Dispose(); // Dispose the DataSet
            }

        return ObjList;
        }

    protected void btnExcelReport_Click(object sender, EventArgs e)
        {
        StudentController objSC = new StudentController();
        Student.StudentInfo objStudE = new Student.StudentInfo();
        int Collegeid = Convert.ToInt32(ddlClg.SelectedValue.ToString());
        int DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue.ToString());
        int BranchNO = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
        int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue.ToString());
        string Receipt_code = (ddlReceiptType.SelectedValue.ToString());
        DataSet ds = objSC.GetStudentOnlinePaymentLog(Collegeid, DegreeNo, BranchNO, SemesterNo, Receipt_code);

        ds.Tables[0].TableName = "Student Details";

        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count <= 0)
            ds.Tables[0].Rows.Add("No Record Found");

        using (XLWorkbook wb = new XLWorkbook())
            {
            foreach (System.Data.DataTable dt in ds.Tables)
                wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BulkSectionAllotment.xlsx");
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
