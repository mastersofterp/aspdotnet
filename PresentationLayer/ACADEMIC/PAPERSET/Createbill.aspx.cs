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
using System.IO;
public partial class ACADEMIC_Createbill : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    string dept = string.Empty;
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
                   // this.CheckPageAuthorization();
                  
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillDropdown();
                if (Session["usertype"].ToString() == "1")
                {
                    divbill.Visible = false;
                    unlock.Visible = true;
                    tradmin.Visible = true;
                }
                else
                {
                    divbill.Visible = true;
                    unlock.Visible = false;
                    tradmin.Visible = false;

                    //added by samad
                    pnlRem.Visible = false;
                    btnEdit.Visible = false;  //Added on 25/11/2015
                }

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaperSetFacultyAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Createbill.aspx");
        }
    }


    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=0", "SESSIONNO DESC");
            //update on 23/03/2015
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            }

            //objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION_MASTER", "TOP (5) SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP (5) SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
            ////*******objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN SESSION_ACTIVITY A ON A.SESSION_NO=S.SESSIONNO inner join activity_master AM on (am.activity_no = a.activity_no)", "S.SESSIONNO", "S.SESSION_NAME", "ACTIVITY_CODE='Createbill' AND A.STARTED=1", "SESSIONNO DESC");

            //objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=0", "SESSIONNO DESC");
            //update on 20/03/2015
            //objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION_MASTER", "TOP (3) SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
            ////objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION_MASTER S INNER JOIN SESSION_ACTIVITY A ON A.SESSION_NO=S.SESSIONNO inner join activity_master AM on (am.activity_no = a.activity_no)", "S.SESSIONNO", "S.SESSION_NAME", "AM.ACTIVITY_NO = '19' AND A.STARTED=1", "SESSIONNO DESC");
            
            //DEGREE DROPDOWN IS FILLED BY THIS CODE FROM ACD_DEGREE
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");
            //To Fill Section DropDown
            //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            string username = string.Empty;
            //String username = new String("m");
            //int a = new int();
            double a = new double();
          
            //if (Session["userno"].ToString() == "1")
            if (Session["usertype"].ToString() == "1")
            {
                username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue));

            }
            else
            {
                username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            }
            //string a = Session["userfac"].ToString();
            txtExminer.Text = username;
            //txtExminer.Enabled = false;

            //if (Session["userno"].ToString() == "1")
            ////if (Session["usertype"].ToString() == "1")
            ////{
            ////    dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue));
            ////}
            ////else
            ////{
            ////    dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            ////}

            if (Session["usertype"].ToString() == "1")
            {
                if (ddlFac.SelectedIndex > 0)
                {
                    dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue));
                }
                else
                {
                    dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                }
            }
            else
            {
                dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            }

            string deptname = objCommon.LookUp("ACD_DEPARTMENT", "DEPTNAME", "DEPTNO=" + Convert.ToInt32(dept));
            txtNod.Text = deptname;
            txtNod.Enabled = false;
             DataSet ds =new DataSet();
             DataSet dss = new DataSet();
             //if (Session["userno"].ToString() == "1")
             if (Session["usertype"].ToString() == "1")
             {
                 ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "EXMRMOBNO,EXMRACCNO", "EXMNRADDRESS,BLNAME,BANKBRANCH,IFSCCODE,EMAIL", "EXMRUANO=" + Convert.ToInt32(ddlFac.SelectedValue), "");
             }
             else
             {
                 ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "EXMRMOBNO,EXMRACCNO", "EXMNRADDRESS,BLNAME,BANKBRANCH,IFSCCODE,EMAIL", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()), "");
                 dss = objCommon.FillDropDown("USER_ACC", "UA_ACCNO", "UA_FULLNAME,UA_NO,UA_IFSC,UA_MOBILE", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "");
             }
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBill.Text = ds.Tables[0].Rows[0]["BLNAME"].ToString();
                txtmobile.Text = ds.Tables[0].Rows[0]["EXMRMOBNO"].ToString();
                //txtSbiacno.Text = ds.Tables[0].Rows[0]["EXMRACCNO"].ToString();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() == "")
                    {
                        txtSbiacno.Enabled = true;
                        txtIfsccode.Enabled = true;
                        //objUac.UA_AccNo = txtSbiacno.Text;
                        //objUac.UA_No = Convert.ToInt32(ddlFac.SelectedValue);
                        //objUC.UpdateUserACCOUNTNO(objUac);
                    }
                    else
                    {
                        txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                        txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();
                        txtSbiacno.Enabled = false;
                        txtIfsccode.Enabled = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["EXMNRADDRESS"].ToString() != "")
                {
                    txtPostaladdres.Text = ds.Tables[0].Rows[0]["EXMNRADDRESS"].ToString();
                    txtPostaladdres.Enabled = false;
                }
                if (ds.Tables[0].Rows[0]["BANKBRANCH"].ToString() != "")
                {
                    txtbankbranch.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                    txtbankbranch.Enabled = false;
                }
                if (ds.Tables[0].Rows[0]["EMAIL"].ToString() != "")
                {
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    txtEmail.Enabled = false;
                }
                if (dss.Tables[0].Rows[0]["UA_MOBILE"].ToString() != "")
                {
                    txtEmail.Text = ds.Tables[0].Rows[0]["UA_MOBILE"].ToString();
                    txtEmail.Enabled = false;
                }
                //txtbankbranch.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                //txtIfsccode.Text = ds.Tables[0].Rows[0]["IFSCCODE"].ToString();
                //txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtBill.Enabled = false;
                txtmobile.Enabled = false;
                txtSbiacno.Enabled = false;
                //txtbankbranch.Enabled = false;
                txtIfsccode.Enabled = false;
                //txtEmail.Enabled = false;
            }
            else
            {
                txtBill.Enabled = true;
                txtmobile.Enabled = true;
                txtSbiacno.Enabled = true;
                txtPostaladdres.Enabled = true;
                txtbankbranch.Enabled = true;
                txtIfsccode.Enabled = true;
                txtEmail.Enabled = true;
            }
            ////********BINDLIST();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           string chkstatus = string.Empty;
           string session = objCommon.LookUp("ACD_SESSION_MASTER","SESSION_NAME","SESSIONNO="+ Convert.ToInt32(ddlSession.SelectedValue));
           txtBill.Text = session;

           BINDLIST();
           //if (Session["userno"].ToString() == "1")
           if (Session["usertype"].ToString() == "1")
            {
                chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "ISNULL(MAX(LOCK),0)", "EXMRUANO=" + Convert.ToInt32(ddlFac.SelectedValue) + " AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue));
            }
            else
            {
                chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "ISNULL(MAX(LOCK),0)", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue));
            }
                if (chkstatus == "1")
            {
                btnLock.Enabled = false;
                lvCreatebill.Enabled = false;
                //btnReport.Enabled = true;
                btnCal.Enabled = false;
            }
            else
            {
                btnLock.Enabled = true;
                lvCreatebill.Enabled = true;
                //btnReport.Enabled = false;
                btnCal.Enabled = true;
            }
                
                int deptno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                txtNod.Text = objCommon.LookUp("ACD_DEPARTMENT", "DEPTNAME", "DEPTNO=" + Convert.ToInt32(deptno));
                txtmobile.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRMOBNO", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
                //txtBill.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 BLNAME", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtExminer.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRNAME", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtSbiacno.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRACCNO", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtSbiacno.Text = objCommon.LookUp("USER_ACC", "UA_ACCNO", "UANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtPostaladdres.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMNRADDRESS", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtbankbranch.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 BANKBRANCH", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtIfsccode.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 IFSCCODE", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
                //txtEmail.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EMAIL", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));

                txtPostaladdres.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMNRADDRESS", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
                txtbankbranch.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "BANKBRANCH", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
                txtEmail.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "EMAIL", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
                if (txtPostaladdres.Text != "")
                {
                    txtPostaladdres.Enabled = false;
                }
                if (txtbankbranch.Text != "")
                {
                    txtbankbranch.Enabled = false;
                }
                if (txtIfsccode.Text != "")
                {
                    txtIfsccode.Enabled = false;
                }
                else
                {
                    txtIfsccode.Text = objCommon.LookUp("ACD_REMUNERATION_BILL", "IFSCCODE", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
                }
                if (txtEmail.Text != "")
                {
                    txtEmail.Enabled = false;
                }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ddlSession_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_Course_Equivalance.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlCourse_auto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void BINDLIST()
    {
       
        //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL A LEFT OUTER JOIN ACD_REMUNERATION_MASTER B ON(A.RM_BLNO =B.RMNO) LEFT OUTER JOIN ACD_COURSE C ON(C.COURSENO = A.COURSENO) LEFT OUTER JOIN ACD_SESSION_MASTER D ON(A.SESSIONNO =D.SESSIONNO)", "RMNO", "REMUNERATION,SESSION_NAME SESSIONNO,COURSE_NAME COURSENO", "RMNO > 0 AND RMNO IN(1,3,4) AND EXMRUANO =" + Convert.ToInt32(Session["userno"].ToString()), "");
        //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_MASTER B LEFT OUTER JOIN ACD_STAFF S ON(S.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ") LEFT OUTER JOIN ACD_PAPERSET_DETAILS E ON(E.FACULTY1=S.STAFFNO) LEFT OUTER JOIN ACD_COURSE C ON( C.CCODE= E.CCODE) LEFT OUTER JOIN ACD_SESSION_MASTER D ON(D.SESSIONNO =E.SESSIONNO) ", "DISTINCT RMNO", "REMUNERATION,SESSION_NAME SESSIONNO,C.CCODE AS COURSE,COURSE_NAME COURSENO", "RMNO > 0 AND RMNO IN(1,3,4) AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + " AND E.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), ""); 
        DataSet ds = new DataSet();
       
        //if (Session["userno"].ToString() == "1")

        ////if (Session["usertype"].ToString() == "1")
        ////{
        ////    ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue));

        ////}
        ////else
        ////{
        ////    ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
        ////}

        if (Session["usertype"].ToString() == "1")
        {
            if (ddlFac.SelectedIndex > 0)
            {
                ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue));
            }
            else
            {
                ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue));
            }
            

        }
        else
        {
            ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
        }
            if (ds.Tables[0].Rows.Count > 0)
        {
            DataSet ds1 = ds.Clone();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
             {
                if (ds.Tables[0].Rows[i]["RMNO"].ToString() == "2" || ds.Tables[0].Rows[i]["RMNO"].ToString() == "10" || ds.Tables[0].Rows[i]["RMNO"].ToString() == "11")
                {
                    if (ds.Tables[0].Rows[i]["AMOUNT"].ToString() != string.Empty && ds.Tables[0].Rows[i]["AMOUNT"].ToString() != "" && ds.Tables[0].Rows[i]["AMOUNT"].ToString() != "0.00")
                    {
                        if (Session["usertype"].ToString() != "1")
                        {
                            if (ds.Tables[0].Rows[i]["COURSE"].ToString() != null && ds.Tables[0].Rows[i]["COURSE"].ToString() != "")
                            {
                                string amt = "";
                                amt = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND RM_BLNO=2 AND CCODE='" + ds.Tables[0].Rows[i]["COURSE"].ToString() + "' AND CCODE IS NOT NULL AND SESSIONNO=" + ddlSession.SelectedValue + " AND BRANCH='"+ds.Tables[0].Rows[i]["BRANCH"].ToString()+"'");
                                if (amt != "0.00" && amt != "")
                                {
                                    DataRow rw = ds.Tables[0].Rows[i];
                                    ds1.Tables[0].ImportRow(ds.Tables[0].Rows[i]);
                                }
                            }
                        }
                        else
                        {
                            DataRow rw = ds.Tables[0].Rows[i];
                            ds1.Tables[0].ImportRow(ds.Tables[0].Rows[i]);
                        }
                    }
                }
                    else
                    {
                        DataRow rw = ds.Tables[0].Rows[i];
                        ds1.Tables[0].ImportRow(ds.Tables[0].Rows[i]);
                    }
            }

            lvCreatebill.DataSource = ds1;
            lvCreatebill.DataBind();
            btnCal.Enabled = true;
        }
        else
       {
           lvCreatebill.DataSource = null;
           lvCreatebill.DataBind();
            //btnCal.Enabled = false;      
        }
    }
    bool flag = false;
   private void BINDLISTAdmin()
   {
       //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL A LEFT OUTER JOIN ACD_REMUNERATION_MASTER B ON(A.RM_BLNO =B.RMNO) LEFT OUTER JOIN ACD_COURSE C ON(C.COURSENO = A.COURSENO) LEFT OUTER JOIN ACD_SESSION_MASTER D ON(A.SESSIONNO =D.SESSIONNO)", "RMNO", "REMUNERATION,SESSION_NAME SESSIONNO,COURSE_NAME COURSENO", "RMNO > 0 AND RMNO IN(1,3,4) AND EXMRUANO =" + Convert.ToInt32(Session["userno"].ToString()), "");
       //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_MASTER B LEFT OUTER JOIN ACD_STAFF S ON(S.UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ") LEFT OUTER JOIN ACD_PAPERSET_DETAILS E ON(E.FACULTY1=S.STAFFNO) LEFT OUTER JOIN ACD_COURSE C ON( C.CCODE= E.CCODE) LEFT OUTER JOIN ACD_SESSION_MASTER D ON(D.SESSIONNO =E.SESSIONNO) ", "DISTINCT RMNO", "REMUNERATION,SESSION_NAME SESSIONNO,C.CCODE AS COURSE,COURSE_NAME COURSENO", "RMNO > 0 AND RMNO IN(1,3,4) AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + " AND E.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), ""); 
       DataSet ds = objCommon.getPapersetStudentcount(Convert.ToInt32(ddlSessionunlock.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue));
       if (ds.Tables[0].Rows.Count > 0)
       {
           lvCreatebill.DataSource = ds;
           lvCreatebill.DataBind();
           btnCal.Enabled = true;
           btnAdminshow.Visible = false;//******
       }
       else
       {
           flag = true;
           lvCreatebill.DataSource = null;
           lvCreatebill.DataBind();
           objCommon.DisplayMessage(updFacAllot,"Teacher Student allotment is not done for selected faculty!", this.Page);
           return;
           //btnCal.Enabled = false;
       }

       DataSet dss = new DataSet();
       dss = objCommon.FillDropDown("USER_ACC", "UA_ACCNO", "UA_FULLNAME,UA_NO,UA_IFSC,UA_DEPTNO", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue), "");
                if (dss.Tables[0].Rows.Count > 0)
                {
                    if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() == "")
                    {
                        txtSbiacno.Enabled = true;
                        txtIfsccode.Enabled = true;
                        //objUac.UA_AccNo = txtSbiacno.Text;
                        //objUac.UA_No = Convert.ToInt32(ddlFac.SelectedValue);
                        //objUC.UpdateUserACCOUNTNO(objUac);
                    }
                    else
                    {
                        txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                        txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();

                        txtSbiacno.Enabled = false;
                        txtIfsccode.Enabled = false;
                    }


                    //added by samad
                    string R_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "CONVERT(DECIMAL(10,0),AMOUNT) AMOUNT", "EXMRUANO=" + dss.Tables[0].Rows[0]["UA_NO"].ToString() + "and EXMDEPTNO=" + dss.Tables[0].Rows[0]["UA_DEPTNO"].ToString() + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 ");
                    txtRem.Text = R_amount.ToString();
                    string V_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "CONVERT(DECIMAL(10,0),AMOUNT) AMOUNT", "EXMRUANO=" + dss.Tables[0].Rows[0]["UA_NO"].ToString() + "and EXMDEPTNO=" + dss.Tables[0].Rows[0]["UA_DEPTNO"].ToString() + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=11 AND AMOUNT > 0 ");
                    txtVar.Text = V_amount.ToString();
                    
                    string chkRemuneration = "";
                    string chkVarificationOfficer = "";
                    chkRemuneration = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + dss.Tables[0].Rows[0]["UA_DEPTNO"].ToString() + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 ");
                    chkVarificationOfficer = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + dss.Tables[0].Rows[0]["UA_DEPTNO"].ToString() + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=11 AND AMOUNT > 0 ");

                    if (chkRemuneration.ToString() != "")
                    {
                        txtRem.Enabled = false;
                        txtVar.Enabled = true;
                        if (V_amount == "" || V_amount != "0.00" || V_amount != "0")
                        lblMsg.Text = "Varification Officer amount not submitted for this faculty";
                    }
                    else if (chkVarificationOfficer.ToString() != "")
                    {
                       txtRem.Enabled = true;
                       txtVar.Enabled = true;
                       lblMsg.Text = "HOD Remuneration amount is not submitted for this department";
                    }
                   else 
                    {
                       txtRem.Enabled = true;
                       txtVar.Enabled = true;
                       lblMsg.Text = "HOD Remuneration  and Varification Officer amount is not submitted for this department";
                    }

                    if (R_amount != "" || V_amount != "")
                    {
                        btnEdit.Visible = true;  //Added on 25/11/2015
                        txtRem.Enabled = false;
                        txtVar.Enabled = false;
                    }

                    else if (R_amount == "" || V_amount == "")
                    {
                        btnEdit.Visible = false;  //Added on 25/11/2015
                    }
                }

   }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL A LEFT OUTER JOIN ACD_REMUNERATION_MASTER B ON(A.RM_BLNO =B.RMNO) LEFT OUTER JOIN ACD_COURSE C ON(C.COURSENO = A.COURSENO) LEFT OUTER JOIN ACD_SESSION_MASTER D ON(A.SESSIONNO =D.SESSIONNO)", "RMNO", "REMUNERATION,SESSION_NAME SESSIONNO,COURSE_NAME COURSENO", "RMNO > 0 AND RMNO IN(1,3,4) AND EXMRUANO =" + Convert.ToInt32(Session["userno"].ToString()), "");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    lvCreatebill.DataSource = ds;
        //    lvCreatebill.DataBind();
        //    btnCal.Enabled = true;
        //}
        //else
        //{
        //    btnCal.Enabled = false;
        //}
        BINDLIST();
    }
    protected void txtNum_TextChanged(object sender, EventArgs e)
    {
        TextBox txtNum = (TextBox)sender;
        ListViewItem item = (ListViewItem)txtNum.NamingContainer;
        TextBox txtUnitQuantity = (TextBox)item.FindControl("txtAmount");
        CheckBox chk = (CheckBox)item.FindControl("chkSelect");
        if (chk.Checked == true)
        {
            if (chk.ToolTip.ToString() == "1")
            {
                txtUnitQuantity.Text = ((Convert.ToDecimal(txtNum.Text) * 250)).ToString();
            }
            if (chk.ToolTip.ToString() == "3")
            {
                txtUnitQuantity.Text = ((Convert.ToDecimal(txtNum.Text) * 25)).ToString();
            }
            if (chk.ToolTip.ToString() == "4")
            {
                txtUnitQuantity.Text = ((Convert.ToDecimal(txtNum.Text) * 8)).ToString();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string chkstatus = string.Empty;
        //if (Session["userno"].ToString() == "1")
        if (Session["usertype"].ToString() == "1")
        {
            //chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMRUANO", "EXMRUANO=" + Convert.ToInt32(ddlFac.SelectedValue) );
            chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMRUANO", "EXMRUANO=" + Convert.ToInt32(ddlFac.SelectedValue) + "AND SESSIONNO=" +ddlSessionunlock.SelectedValue);
        }
        else
        {
            chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMRUANO", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND SESSIONNO=" + ddlSession.SelectedValue);
        }
            if (chkstatus == Session["userno"].ToString())
            {
                objCommon.DisplayMessage(updFacAllot,"Bill Entry already Exists !", this.Page);
            }
            else
            {
                int uano = Convert.ToInt32(Session["userno"].ToString());
                int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
                CustomStatus result = CustomStatus.Others;
                //result = (CustomStatus)objExamController.AddPaperBill(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse_auto.SelectedValue), txtBill.Text, uano, txtExminer.Text, Convert.ToInt32(txtmobile.Text), deptno, txtSbiacno.Text, txtPostaladdres.Text);
                //if (result == CustomStatus.RecordSaved)
                //    objCommon.DisplayMessage("Record Saved successfully!", this.Page);
                //else
                //    objCommon.DisplayMessage("Record Saved failed!", this.Page);
            }
    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
        int res = 0; int a = 0; int uano = 0; int deptno = 0;
        User_AccController objUC = new User_AccController();
        UserAcc objUac = new UserAcc();

         //string chkstatus = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMRUANO", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
         //if (chkstatus == Session["userno"].ToString())
         //{
         //    objCommon.DisplayMessage("Bill Entry already Exists !", this.Page);
         //}
         //else
         //{
        //if (Session["userno"].ToString() == "1")
        if (Session["usertype"].ToString() == "1")
        {
            uano = Convert.ToInt32(ddlFac.SelectedValue);
            //deptno = Convert.ToInt32(dept);

            //---add on 04042015
            ddlSession.SelectedValue = ddlSessionunlock.SelectedValue;
            dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue));
            deptno = Convert.ToInt32(dept);
            string username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue));
            txtExminer.Text = username;

            DataSet ds = new DataSet();
            DataSet dss = new DataSet();
            //if (Session["userno"].ToString() == "1")
            if (Session["usertype"].ToString() == "1")
            {
                ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "EXMRMOBNO,EXMRACCNO", "EXMNRADDRESS,BLNAME,BANKBRANCH,IFSCCODE,EMAIL", "EXMRUANO=" + Convert.ToInt32(ddlFac.SelectedValue), "");
                dss = objCommon.FillDropDown("USER_ACC", "UA_ACCNO", "UA_FULLNAME,UA_NO,UA_IFSC", "UA_NO=" + Convert.ToInt32(ddlFac.SelectedValue), "");
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBill.Text = ds.Tables[0].Rows[0]["BLNAME"].ToString();
                txtmobile.Text = ds.Tables[0].Rows[0]["EXMRMOBNO"].ToString();
                //txtSbiacno.Text = ds.Tables[0].Rows[0]["EXMRACCNO"].ToString();
                //txtPostaladdres.Text = ds.Tables[0].Rows[0]["EXMNRADDRESS"].ToString();
                //txtbankbranch.Text = ds.Tables[0].Rows[0]["BANKBRANCH"].ToString();
                //txtIfsccode.Text = ds.Tables[0].Rows[0]["IFSCCODE"].ToString();
                //txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtBill.Enabled = false;
                txtmobile.Enabled = false;
                if (dss.Tables[0].Rows.Count > 0)
                {
                    if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() == "")
                    {
                            txtSbiacno.Enabled = true;
                            txtIfsccode.Enabled = true;
                            objUac.UA_AccNo = txtSbiacno.Text;
                            objUac.UA_IFSC = txtIfsccode.Text;
                            objUac.UA_No = Convert.ToInt32(ddlFac.SelectedValue);
                            objUC.UpdateUserACCOUNTNO(objUac);
                    }
                    else
                    {
                        txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                        txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();

                        txtSbiacno.Enabled = false;
                        txtIfsccode.Enabled = false;
                    }
                }
                else if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() != "" || dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() != null || dss.Tables[0].Rows[0]["UA_IFSC"].ToString() != "" || dss.Tables[0].Rows[0]["UA_IFSC"].ToString() != null)
                {
                    txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                    txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();

                    txtSbiacno.Enabled = false;
                    txtIfsccode.Enabled = false;
                }
                else
                {
                    objCommon.DisplayMessage(updFacAllot,"Please enter account number and mobile number !", this.Page);
                    return;
                }
                txtPostaladdres.Enabled = false;
                txtbankbranch.Enabled = false;
              
                txtEmail.Enabled = false;
            }
            else if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() != "" &&  dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() != null && dss.Tables[0].Rows[0]["UA_IFSC"].ToString() != "" && dss.Tables[0].Rows[0]["UA_IFSC"].ToString() != null)
            {
                txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();

                txtSbiacno.Enabled = false;
                txtIfsccode.Enabled = false;
            }
            else
            {
                objCommon.DisplayMessage(updFacAllot,"Record Calculate failed. Please check account number and other details in user account!", this.Page);
                return;
            }
            //if(ddlSession.SelectedValue == "0" || uano == 0 || deptno == 0 || username == "" || txtBill.Text == "" || txtmobile.Text == "" || txtSbiacno.Text == "" || txtPostaladdres.Text == "" || txtbankbranch.Text == "" )

        }
        else
        {
            uano = Convert.ToInt32(Session["userno"].ToString());
            deptno = Convert.ToInt32(Session["userdeptno"].ToString());
            DataSet dss = new DataSet();
            dss = objCommon.FillDropDown("USER_ACC", "UA_ACCNO", "UA_FULLNAME,UA_NO,UA_IFSC", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "");
            if (dss.Tables[0].Rows.Count > 0)
            {
                if (dss.Tables[0].Rows[0]["UA_ACCNO"].ToString() == "")
                {
                    txtSbiacno.Enabled = true;
                    txtIfsccode.Enabled = true;

                    objUac.UA_AccNo = txtSbiacno.Text;
                    objUac.UA_IFSC = txtIfsccode.Text;
                    objUac.UA_No = Convert.ToInt32(Session["userno"].ToString());
                    
                    objUC.UpdateUserACCOUNTNO(objUac);
                }
                else
                {
                    txtSbiacno.Text = dss.Tables[0].Rows[0]["UA_ACCNO"].ToString();
                    txtIfsccode.Text = dss.Tables[0].Rows[0]["UA_IFSC"].ToString();

                    txtSbiacno.Enabled = false;
                    txtIfsccode.Enabled = false;
                }
            }
        }
              
             foreach (ListViewDataItem lsv in lvCreatebill.Items)
             {
                 TextBox txtnum = lsv.FindControl("txtNum") as TextBox;
                 TextBox txtNex = lsv.FindControl("txtNex") as TextBox;
                 DropDownList ddlNum = lsv.FindControl("ddlNum") as DropDownList;
                 TextBox txtAmt = lsv.FindControl("txtAmount") as TextBox;
                 Label lblrm = lsv.FindControl("lblRmno") as Label;
                 Label lblcode = lsv.FindControl("lblcode") as Label;
                 Label lblcourse = lsv.FindControl("lblcourse") as Label;
                 CheckBox chk = lsv.FindControl("chkSelect") as CheckBox;
                 Label lblsem = lsv.FindControl("lblsemester") as Label;
                 Label lblscheme = lsv.FindControl("lblSchemetype") as Label;
                 Label lblbranch = lsv.FindControl("lblbranch") as Label;
                 int rmno = Convert.ToInt32(lblrm.ToolTip.ToString());
                 int noofexam = 0;
                 if (rmno == 1)
                 {
                         noofexam = 1;
                   
                 }
                 if (rmno == 2)
                 {
                     noofexam = 1;

                 }
                 if (rmno == 10 || rmno == 11)
                 {
                     noofexam = 1;
                 }

                 if (rmno == 4)
                 {
                     if (txtNex.Text == "")
                     {
                         noofexam = 0;
                     }
                     else
                     {
                         noofexam = Convert.ToInt32(txtNex.Text);
                     }
                 }
                 if (rmno == 3)
                 {
                     if (ddlNum.SelectedIndex == 0)
                     {
                         noofexam = 0;
                     }
                     else
                     {
                         noofexam = Convert.ToInt32(ddlNum.SelectedValue);
                     }
                 }
                 int amount = txtAmt.Text == "" ? 0 : Convert.ToInt32(txtAmt.Text);
                 int course = 0;
          //if (chk.Checked == true)//***************
          //       {
           if (rmno == 10)
                     {
                         // here need to check remuneration amount already given to any HOD
                         string R_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 ");
                         if (R_amount == "" || R_amount == "0.00")
                         {
                             string V_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO=11");
                             if (V_amount == "" || V_amount == "0.00")
                             {
                                // res = objExamController.AddPaperBillAmount(Convert.ToInt32(ddlFacUpd.SelectedValue), rmno, noofexam, amount, Convert.ToInt32(lblyear.ToolTip), billname, exmrname, mobile, deptno, accname, address, lblcode.Text, lblcourse.Text, brnchcode, ifsc, email, lblscheme.Text, lblsem.Text, lblbranch.Text);
                                 res = objExamController.AddPaperBillAmount(uano, rmno, noofexam, amount, Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                                 //res = objExamController.AddPaperBillAmount(uano, 10, noofexam, Convert.ToInt32(txtRem.Text), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                             }
                             else
                             {
                                 objCommon.DisplayMessage(updFacAllot,"Varification Officer Amount Already Given to Faculty", this.Page);
                                 return;
                             }
                         }
                         else
                         {
                             // if remuneration is given to login faculty
                             string R_amount1 = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue));
                             if (R_amount1 != "" )
                             {

                             }
                             else
                             {
                                 objCommon.DisplayMessage(updFacAllot,"Remuneration Amount Already Given to Faculty", this.Page);
                                 return;
                             }
                         }

                     }
                     // check varification amount 
               else if (rmno == 11)
                     {
                         string R_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=11 AND AMOUNT > 0 ");
                         if (R_amount == "" || R_amount == "0.00")
                         {
                             string V_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO=10");
                             if (V_amount == "" || V_amount == "0.00")
                             {
                                 res = objExamController.AddPaperBillAmount(uano, rmno, noofexam, amount, Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                                 //res = objExamController.AddPaperBillAmount(uano, 11, noofexam, Convert.ToInt32(txtVar.Text), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                             }
                             else
                             {
                                 objCommon.DisplayMessage(updFacAllot,"Remuneration Amount Amount Already Given to Faculty", this.Page);
                                 return;
                             }
                         }
                         else
                         {
                             string R_amount1 = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=11 AND AMOUNT > 0 AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue));
                             if (R_amount1 != "" )
                             {
                                 res = objExamController.AddPaperBillAmount(uano, rmno, noofexam, amount, Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                                 //res = objExamController.AddPaperBillAmount(uano, 11, noofexam,Convert.ToInt32(txtVar.Text), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                             }
                             else
                             {
                                 objCommon.DisplayMessage(updFacAllot,"Varification Officer Already Given to Faculty", this.Page);
                                 return;
                             }
                         }

                     }
                     else
                     {
                         a++;
                         res = objExamController.AddPaperBillAmount(uano, rmno, noofexam, amount, Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, lblcode.Text, lblcourse.Text, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, lblscheme.Text, lblsem.Text, lblbranch.Text);
                     }
           

                 //}//if******
   
                 //else
                 //{
                 //    objCommon.DisplayMessage("Please Select Remuneration for Calculate!", this.Page);
                 //}
             }

        ///added by samad
             if (Session["usertype"].ToString() == "1")
             {
                 if (txtRem.ReadOnly == false)
                 {
                     string rs_amount = "0";
                     if (txtRem.Text != "")
                         rs_amount = txtRem.Text;
                     string Rem_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO = 10");
                     if (rs_amount != "")
                     {
                         res = objExamController.AddPaperBillAmount(uano, 10, 1, Convert.ToInt32(rs_amount), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                     }
                     else if (Rem_amount != "")
                     {
                         res = objExamController.AddPaperBillAmount(uano, 10, 1, Convert.ToInt32(rs_amount), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                     }
                     else
                     {
                         res = objExamController.AddPaperBillAmount(uano, 10, 1, Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                     }
                 }
                 else
                 {
                     res = objExamController.AddPaperBillAmount(uano, 10, 1, Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                 }
                 //if (txtVar.ReadOnly == false)
                 //{
                     string vs_amount = "0";
                     if (txtVar.Text != "")
                         vs_amount = txtVar.Text;
                     //string Var_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO = 11");
                     string Var_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMRUANO=" + uano + "and EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 ");
                     if (Var_amount == "")
                         res = objExamController.AddPaperBillAmount(uano, 11, 1, Convert.ToInt32(vs_amount), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                     else
                         res = objExamController.AddPaperBillAmount(uano, 11, 1, Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                 //}
                 //else
                 //{
                 //    res = objExamController.AddPaperBillAmount(uano, 11, 1, Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue), txtBill.Text, txtExminer.Text, txtmobile.Text, deptno, txtSbiacno.Text, txtPostaladdres.Text, null, null, txtbankbranch.Text, txtIfsccode.Text, txtEmail.Text, null, null, null);
                 //}
             }
        /////end
             if (a == 0)
             {
                 objCommon.DisplayMessage(updFacAllot,"Please Select Remuneration for Calculate!", this.Page);
             }
             if (res != 0)
             {
                 objCommon.DisplayMessage(updFacAllot,"Record Calculate successfully!", this.Page);
                 txtRem.Enabled = false;
                 txtVar.Enabled = false;
             }
             else
             {
                 objCommon.DisplayMessage(updFacAllot,"Record Calculate failed!", this.Page);
             }
         //}
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("billReport", "CreateBill.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {  
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //if (Session["userno"].ToString() == "1")
        if (Session["usertype"].ToString() == "1")
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSessionunlock.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(ddlFac.SelectedValue);
        else
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" +Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString());
        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

    }
    protected void ddlNum_onselectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        ListViewItem item = (ListViewItem)ddl.NamingContainer;
        DropDownList ddlnum = (DropDownList)item.FindControl("ddlNum");
        TextBox txtamt = (TextBox)item.FindControl("txtAmount");
        TextBox txtNex = (TextBox)item.FindControl("txtNex");
        CheckBox chkselect = (CheckBox)item.FindControl("chkSelect");
        if (chkselect.ToolTip.ToString() == "3")
        {
            if (Convert.ToInt32(ddlnum.SelectedValue) == 1 || Convert.ToInt32(ddlnum.SelectedValue) == 2)
            {
                txtamt.Text = "50";
            }
            if (Convert.ToInt32(ddlnum.SelectedValue) == 3)
            {
                txtamt.Text = "75";
            }
            if (Convert.ToInt32(ddlnum.SelectedValue) == 4)
            {
                txtamt.Text = "100";
            }
            if (Convert.ToInt32(ddlnum.SelectedValue) == 5)
            {
                txtamt.Text = "125";
            }
            if (Convert.ToInt32(ddlnum.SelectedValue) == 6)
            {
                txtamt.Text = "150";
            }
        }
        else
        {
            ddlnum.Enabled = false;
        }

        if (chkselect.ToolTip.ToString() == "3" || chkselect.ToolTip.ToString() == "1")
        {
            txtNex.Text = "";
        }
        else
        {
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        int uano = 0;
        //if (Session["userno"].ToString() == "1")
        if (Session["usertype"].ToString() == "1")
        {
            uano = Convert.ToInt32(ddlFac.SelectedValue);
            ddlSession.SelectedValue = ddlSessionunlock.SelectedValue;
        }
        else
        {
            uano = Convert.ToInt32(Session["userno"].ToString());
        }
        int res = 0;
        res = objExamController.LockPaperBillAmount(uano, Convert.ToInt32(ddlSession.SelectedValue));
        if (res > 0)
        {
            objCommon.DisplayMessage(updFacAllot,"Bill Entry Locked Successfully!!", this.Page);
            btnLock.Enabled = false;
            btnCal.Enabled = false;
            lvCreatebill.Enabled = false;
        }
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        if (ddlFac.SelectedIndex > 0 && ddlSessionunlock.SelectedIndex > 0)
        {
            int a = objCommon.UpdateLockbill(Convert.ToInt32(ddlFac.SelectedValue), Convert.ToInt32(ddlSessionunlock.SelectedValue));
            if (a > 0)
            {
                objCommon.DisplayMessage(updFacAllot,"Unlock done successfully!!", this.Page);
                BindlistLock();
            }
        }
        else
        {
            objCommon.DisplayMessage(updFacAllot,"Please Select Session and faculty for Unlock!!", this.Page);
        }
    }
    protected void ddlSessionunlock_SelectedIndexChanged(object sender, EventArgs e)
    {   //objCommon.FillDropDownList(ddlFac, "ACD_REMUNERATION_BILL", "DISTINCT EXMRUANO", "EXMRNAME", "EXMRUANO > 0 AND SESSIONNO = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
        //objCommon.FillDropDownList(ddlFacUpd, "ACD_REMUNERATION_BILL", "DISTINCT EXMRUANO", "EXMRNAME", "EXMRUANO > 0 AND SESSIONNO = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");


        ////objCommon.FillDropDownList(ddlFac, "ACD_PAPERSET_DETAILS A INNER JOIN ACD_STAFF B ON(A.FACULTY1=B.STAFFNO OR A.FACULTY2=B.STAFFNO OR A.FACULTY3=B.STAFFNO) LEFT OUTER JOIN ACD_REMUNERATION_BILL C ON(C.EXMRUANO=B.UA_NO)", "DISTINCT B.UA_NO EXMRUANO", "STAFF_NAME EXMRNAME", "A.SESSIONNO = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");

        objCommon.FillDropDownList(ddlFac, "ACD_PAPERSET_DETAILS A INNER JOIN ACD_STAFF B ON(A.FACULTY1=B.STAFFNO OR A.FACULTY2=B.STAFFNO OR A.FACULTY3=B.STAFFNO) LEFT OUTER JOIN ACD_REMUNERATION_BILL C ON(C.EXMRUANO=B.UA_NO) INNER JOIN ACD_SESSION_MASTER SM ON (A.SESSIONNO = SM.SESSIONNO)", "DISTINCT B.UA_NO EXMRUANO", "STAFF_NAME EXMRNAME", "SM.SESSIONID = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
       // objCommon.FillDropDownList(ddlFacUpd, "ACD_PAPERSET_DETAILS A INNER JOIN ACD_STAFF B ON(A.FACULTY1=B.STAFFNO OR A.FACULTY2=B.STAFFNO OR A.FACULTY3=B.STAFFNO) LEFT OUTER JOIN ACD_REMUNERATION_BILL C ON(C.EXMRUANO=B.UA_NO) ", "DISTINCT B.UA_NO EXMRUANO", "STAFF_NAME EXMRNAME", "UA_NO > 0 AND A.SESSIONNO = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
        objCommon.FillDropDownList(ddlFacUpd, "ACD_PAPERSET_DETAILS A INNER JOIN ACD_STAFF B ON(A.FACULTY1=B.STAFFNO OR A.FACULTY2=B.STAFFNO OR A.FACULTY3=B.STAFFNO) LEFT OUTER JOIN ACD_REMUNERATION_BILL C ON(C.EXMRUANO=B.UA_NO) INNER JOIN ACD_SESSION_MASTER SM ON (A.SESSIONNO = SM.SESSIONNO)", "DISTINCT B.UA_NO EXMRUANO", "STAFF_NAME EXMRNAME", "SM.SESSIONID = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
        //ddlFacUpd.Enabled = false;
        ////objCommon.FillDropDownList(ddlFacUpd, "ACD_PAPERSET_DETAILS A INNER JOIN ACD_STAFF B ON(A.FACULTY1=B.STAFFNO OR A.FACULTY2=B.STAFFNO OR A.FACULTY3=B.STAFFNO) LEFT OUTER JOIN ACD_REMUNERATION_BILL C ON(C.EXMRUANO=B.UA_NO) ", "DISTINCT B.UA_NO EXMRUANO", "STAFF_NAME EXMRNAME", "A.SESSIONNO = " + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
    }
    protected void lvCreatebill_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblrem = e.Item.FindControl("lblRmno") as Label;
        if (lblrem.Text == "Paper Setting")
        {
            lblrem.ForeColor = System.Drawing.Color.Green;
        }
        if (lblrem.Text == "Typing & Printing")
        {
            lblrem.ForeColor = System.Drawing.Color.MediumBlue;
        }
        if (lblrem.Text == "Marking Answer Book")
        {
            lblrem.ForeColor = System.Drawing.Color.PaleVioletRed;
        }
        Label lblccode = e.Item.FindControl("lblcode") as Label;
        TextBox txtamount = e.Item.FindControl("txtAmount") as TextBox;
        CheckBox chkselect = e.Item.FindControl("chkSelect") as CheckBox;

        // update 27/03/2015  by Atul                 
        if (Session["usertype"].ToString() != "1")
        {
            if (lblrem.Text == "M.O.I" || lblrem.Text == "Remuneration" || lblrem.Text == "Varification Officer")
            {
                txtamount.Enabled = false;
                chkselect.Enabled = false;
                //txtamount.Enabled = true;
                //chkselect.Enabled = true;
            }

        }
        // =============================================    

        double amount=0;
        string amt = string.Empty;
        string checkcnt = "0";
        if (Session["usertype"].ToString() == "1")
        {
            checkcnt = objCommon.LookUp("ACD_REMUNERATION_BILL", "COUNT(EXMRUANO)", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND SESSIONNO=" + ddlSessionunlock.SelectedValue);
        }
        else
        {
            checkcnt = objCommon.LookUp("ACD_REMUNERATION_BILL", "COUNT(EXMRUANO)", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND SESSIONNO=" + ddlSession.SelectedValue);
        }
         
         if (checkcnt != "0")
         {
             //if (Session["userno"].ToString() == "1")
             if (Session["usertype"].ToString() == "1")
             {
                 amt = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue) + " AND RM_BLNO=" + Convert.ToInt32(lblrem.ToolTip.ToString()) + " AND CCODE='" + lblccode.Text + "' AND CCODE IS NOT NULL AND SESSIONNO=" + ddlSessionunlock.SelectedValue);
                 if (amt != "")
                 {
                     amount = Convert.ToDouble(amt);
                 }
             }
             else
             {
                 amt = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMRUANO=" + Session["userno"].ToString() + " AND RM_BLNO=" + Convert.ToInt32(lblrem.ToolTip.ToString()) + " AND CCODE='" + lblccode.Text + "' AND CCODE IS NOT NULL  AND SESSIONNO=" + ddlSession.SelectedValue);
                 if (amt != "")
                 {
                     amount = Convert.ToDouble(amt);
                 }
             }
             if (amount != 0)
             {
                 chkselect.Checked = true;
             }
             else
             {
                 chkselect.Checked = false;
             }
             txtamount.Text = amount.ToString();
            
         }
         else
         {
             txtamount.Text =string.Empty;
             
         }



         // update 27/03/2015  by Atul      
         //if (Session["userno"].ToString() == "1")
         if (Session["usertype"].ToString() == "1")
         {
             string checkcnt1 = objCommon.LookUp("ACD_REMUNERATION_BILL", "COUNT(EXMRUANO)", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()));
             if (checkcnt1 != "0")
             {
                 amt = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMRUANO=" + Session["userno"].ToString() + " AND RM_BLNO=" + Convert.ToInt32(lblrem.ToolTip.ToString()) + " AND CCODE='" + lblccode.Text + "' AND CCODE IS NOT NULL");
                 if (amt != "")
                 {
                     amount = Convert.ToDouble(amt);
                 }
             }
             if (amount != 0)
             {
                 chkselect.Checked = true;
             }
             else
             {
                 chkselect.Checked = false;
             }
             txtamount.Text = amount.ToString();
             
         }
        // =============================================


    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
       
            GridView GVDayWiseAtt = new GridView();
            ExamController objExam=new ExamController();
            //DataSet ds = objExam.BillExcelReport(Convert.ToInt32(ddlSession.SelectedValue));
            DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "DBO.FN_DESC('SESSIONNAME',SESSIONNO)SESSION,EXMRNAME NAME,DBO.FN_DESC('DEPARTMENT',EXMDEPTNO) BRANCH", "EXMRMOBNO MOBILENO,EXMRACCNO SBIACCOUNTNO,IFSCCODE,(CASE WHEN LOCK = 0 THEN 'INCOMPLETED' ELSE 'COMPLETED' END)STATUS,SUM(AMOUNT) AMOUNT", "SESSIONNO =" + Convert.ToInt32(ddlSessionunlock.SelectedValue) + " GROUP BY SESSIONNO,EXMRNAME,EXMDEPTNO,EXMRMOBNO,EXMRACCNO,IFSCCODE,LOCK", "");
            if (ds.Tables[0].Rows.Count > 0)
              {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=Bill.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
    }
    private void BindlistLock()
    {
        DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "DISTINCT EXMRUANO", "EXMRNAME,(CASE WHEN LOCK = 0 THEN 'UNLOCK' ELSE 'LOCK' END)STATUS", "SESSIONNO=" + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvstatus.DataSource = ds;
            lvstatus.DataBind();
            btnCal.Enabled = true;
            ddlSession.SelectedValue = ddlSessionunlock.SelectedValue;
        }
        else
        {
            lvstatus.DataSource = null;
            lvstatus.DataBind();
            //btnCal.Enabled = false;
        }
    }
    protected void btnFaculty_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "DISTINCT EXMRUANO", "EXMRNAME,(CASE WHEN LOCK = 0 THEN 'UNLOCK' ELSE 'LOCK' END)STATUS", "SESSIONNO=" + Convert.ToInt32(ddlSessionunlock.SelectedValue), "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvstatus.DataSource = ds;
            lvstatus.DataBind();
             btnCal.Enabled = true;
             ddlSession.SelectedValue = ddlSessionunlock.SelectedValue;
        }
        else
        {
            lvstatus.DataSource = null;
            lvstatus.DataBind();
            //btnCal.Enabled = false;
        }
    }
    protected void btnLockunlock_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Lock")
        {
            Button btnlockunlock = (Button)sender;
            ListViewDataItem item = (ListViewDataItem)btnlockunlock.NamingContainer;
            Button btnlocku = (Button)item.FindControl("btnLockunlock");
            Label lblUano = (Label)item.FindControl("lblStatusexmr");
            string a = btnlocku.CommandArgument;
            int res = 0;
            res = objExamController.LockPaperBillAmount(Convert.ToInt32(lblUano.ToolTip), Convert.ToInt32(ddlSessionunlock.SelectedValue));
            if (res > 0)
            {
                objCommon.DisplayMessage(updFacAllot,"Bill Entry Locked Successfully!!", this.Page);
                BindlistLock();
            }
        }
    }
    protected void btnCancellock_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnAdminshow_Click(object sender, EventArgs e)
    {
        if (ddlSessionunlock.SelectedIndex > 0 && ddlFac.SelectedIndex >= 0)
        {
            lblyear.ToolTip = ddlSessionunlock.SelectedValue;
            ddlFacUpd.SelectedValue = ddlFac.SelectedValue.ToString();
          
            BINDLISTAdmin();
            if (flag == false)
            {
                unlock.Visible = false;
                divbill.Visible = true;
                trsession.Visible = false;
                trdegree.Visible = false;
                trbank.Visible = false;
                trbutton.Visible = true;
                trcourse.Visible = false;
                tremail.Visible = false;
                //trifsc.Visible = false;
                trifsc.Visible = true;
                //txtSbiacno.Visible = true;
            }
        }
        else
        {
            objCommon.DisplayMessage(updFacAllot,"Please Select Session and Faculty!", this.Page);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int billupd = 0;
        int aa = 0;
        int deptno = 0;string  mob = string.Empty;
        deptno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(ddlFacUpd.SelectedValue)));
        mob = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRMOBNO", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        //int mobile = 0;
        //mobile = mob == "" ? 0 : Convert.ToInt32(mob);
        string mobile;
        mobile = mob == "" ? "" : mob;
        string  billname =objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 BLNAME", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        string exmrname = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRNAME", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        string accname = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMRACCNO", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        //string address = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EXMNRADDRESS", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        //string brnchcode = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 BANKBRANCH", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        string ifsc = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 IFSCCODE", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));
        //string email = objCommon.LookUp("ACD_REMUNERATION_BILL", "top 1 EMAIL", "EXMRUANO=" + Convert.ToInt32(ddlFacUpd.SelectedValue));

        string address = objCommon.LookUp("ACD_REMUNERATION_BILL", "EXMNRADDRESS", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
        string brnchcode = objCommon.LookUp("ACD_REMUNERATION_BILL", "BANKBRANCH", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");
        string email = objCommon.LookUp("ACD_REMUNERATION_BILL", "EMAIL", "EXMRUANO=" + Convert.ToInt32(Session["userno"].ToString()) + "AND RM_BLNO=1");



      foreach (ListViewDataItem lsv in lvCreatebill.Items)
      {
          TextBox txtnum = lsv.FindControl("txtNum") as TextBox;
          TextBox txtNex = lsv.FindControl("txtNex") as TextBox;
          DropDownList ddlNum = lsv.FindControl("ddlNum") as DropDownList;
          TextBox txtAmt = lsv.FindControl("txtAmount") as TextBox;
          Label lblrm = lsv.FindControl("lblRmno") as Label;
          Label lblcode = lsv.FindControl("lblcode") as Label;
          Label lblcourse = lsv.FindControl("lblcourse") as Label;
          CheckBox chk = lsv.FindControl("chkSelect") as CheckBox;
          Label lblsem = lsv.FindControl("lblsemester") as Label;
          Label lblscheme = lsv.FindControl("lblSchemetype") as Label;
          Label lblbranch = lsv.FindControl("lblbranch") as Label;
          int rmno = Convert.ToInt32(lblrm.ToolTip.ToString());
          int noofexam = 0;
          if (rmno == 1)
          {
              noofexam = 1;
          }
          if (rmno == 2)
          {
              noofexam = 1;

          }
          if (rmno == 4)
          {
              if (txtNex.Text == "")
              {
                  noofexam = 0;
              }
              else
              {
                  noofexam = Convert.ToInt32(txtNex.Text);
              }
          }
          if (rmno == 10 || rmno == 11)
          {
              noofexam = 1;
          }
          if (rmno == 3)
          {
              if (ddlNum.SelectedIndex == 0)
              {
                  noofexam = 0;
              }
              else
              {
                  noofexam = Convert.ToInt32(ddlNum.SelectedValue);
              }
          }
          int amount = txtAmt.Text == "" ? 0 : Convert.ToInt32(txtAmt.Text);
          int course = 0;
          if (chk.Checked == true)
          {
              if (rmno == 10)
              {
                  // here need to check remuneration amount already given to any HOD
                  string R_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=10 AND AMOUNT > 0 " );
                  if (R_amount == "" || R_amount == "0.00")
                  {
                      string V_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO=11");
                      if (V_amount == "" || V_amount == "0.00")
                      {
                          billupd = objExamController.AddPaperBillAmount(Convert.ToInt32(ddlFacUpd.SelectedValue), rmno, noofexam, amount, Convert.ToInt32(lblyear.ToolTip), billname, exmrname, mobile, deptno, accname, address, lblcode.Text, lblcourse.Text, brnchcode, ifsc, email, lblscheme.Text, lblsem.Text, lblbranch.Text);
                      }
                      else
                      {
                          objCommon.DisplayMessage(updFacAllot,"Varification Officer Amount Already Given to Faculty",this.Page);
                          return;
                      }
                  }
                  else
                  {
                      objCommon.DisplayMessage(updFacAllot,"Remuneration Amount Already Given to Faculty", this.Page);
                      return;
                  }
             
              }
                  // check varification amount 
              else if(rmno == 11)
              {
                  string R_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + " AND RM_BLNO=11 AND AMOUNT > 0 ");
                  if (R_amount == "" || R_amount == "0.00")
                  {
                      string V_amount = objCommon.LookUp("ACD_REMUNERATION_BILL", "AMOUNT", "EXMDEPTNO=" + deptno + " AND SESSIONNO= " + Convert.ToInt32(lblyear.ToolTip) + "AND EXMRUANO= " + Convert.ToInt32(ddlFacUpd.SelectedValue) + "AND RM_BLNO=10");
                      if (V_amount == "" || V_amount == "0.00")
                      {
                          billupd = objExamController.AddPaperBillAmount(Convert.ToInt32(ddlFacUpd.SelectedValue), rmno, noofexam, amount, Convert.ToInt32(lblyear.ToolTip), billname, exmrname, mobile, deptno, accname, address, lblcode.Text, lblcourse.Text, brnchcode, ifsc, email, lblscheme.Text, lblsem.Text, lblbranch.Text);
                      }
                      else
                      {
                          objCommon.DisplayMessage(updFacAllot,"Remuneration Amount Amount Already Given to Faculty", this.Page);
                          return;
                      }
                  }
                  else
                  {
                      objCommon.DisplayMessage(updFacAllot,"Varification Officer Already Given to Faculty", this.Page);
                      return;
                  }
             
              }
              else
              {
                  aa++;
                  billupd = objExamController.AddPaperBillAmount(Convert.ToInt32(ddlFacUpd.SelectedValue), rmno, noofexam, amount, Convert.ToInt32(lblyear.ToolTip), billname, exmrname, mobile, deptno, accname, address, lblcode.Text, lblcourse.Text, brnchcode, ifsc, email, lblscheme.Text, lblsem.Text, lblbranch.Text);
              }

          }
          }
            if (aa == 0)
            {
                objCommon.DisplayMessage(updFacAllot,"Please Select Remuneration for Calculate!", this.Page);
            }
            if (billupd != 0)
                objCommon.DisplayMessage(updFacAllot,"Record Calculate successfully!", this.Page);
            else
                objCommon.DisplayMessage(updFacAllot,"Record Calculate failed!", this.Page);
        
    }

    // Added on 25/11/2015 as per requirement
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtRem.Enabled = true;
        txtVar.Enabled = true;
    }
    protected void btnExcel2_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlSessionunlock.SelectedIndex > 0)
            {
                GridView GVDayWiseAtt = new GridView();
                ExamController objExam = new ExamController();
                DataSet ds = objExam.RemunerationBillExcelReport(Convert.ToInt32(ddlSessionunlock.SelectedValue));
                //DataSet ds = objCommon.FillDropDown("ACD_REMUNERATION_BILL", "DBO.FN_DESC('SESSIONNAME',SESSIONNO)SESSION,EXMRNAME NAME,DBO.FN_DESC('DEPARTMENT',EXMDEPTNO) BRANCH", "EXMRMOBNO MOBILENO,EXMRACCNO SBIACCOUNTNO,IFSCCODE,(CASE WHEN LOCK = 0 THEN 'INCOMPLETED' ELSE 'COMPLETED' END)STATUS,SUM(AMOUNT) AMOUNT", "SESSIONNO =" + Convert.ToInt32(ddlSessionunlock.SelectedValue) + " GROUP BY SESSIONNO,EXMRNAME,EXMDEPTNO,EXMRMOBNO,EXMRACCNO,IFSCCODE,LOCK", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();
                    string attachment = "attachment; filename=Bill.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(updFacAllot,"Please select session", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlcollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "", "SESSIONNO DESC"); //--AND FLOCK = 1
            objCommon.FillDropDownList(ddlSessionunlock, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue), "S.SESSIONID DESC"); //--AND FLOCK = 1
            ddlSession.Focus();
        }
        else
        {
            ddlSessionunlock.SelectedIndex = 0;
            ddlFac.SelectedIndex = 0;
        }
    }
}
