using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class TP_Category_Updation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
    StudentController objSC = new StudentController(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ////SetDataColumn();
            //if (Session["userno"] == null || Session["username"] == null ||
            //    Session["usertype"] == null || Session["userfullname"] == null)
            //{
            //    Response.Redirect("~/default.aspx");
            //}
            //else
            //{
            //    // Check User Authority 
            //   // this.CheckPageAuthorization();
            //    Page.Title = Session["coll_name"].ToString();

            //    if (Request.QueryString["pageno"] != null)
            //    {
            //        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            //    }
               
            //  //  divMsg.InnerHtml = string.Empty;
            //}


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

                //Check for Activity On/Off
                // CheckActivity();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

               
                //FOR ONLY STUDENT LOGIN 
                if (Session["usertype"].ToString().Equals("2")) //Student login
                {
                    // TP CATEGORY DROPDOWN WILL AVALAIBLE FROM THIRD SEMESTER STUDENT ON 02-MARCH-2020 .
                     string semesterno = objCommon.LookUp("ACD_STUDENT", "semesterno", "IDNO = '" + Session["IDNO"] + "'");

                    //COMMENTED BY SUMIT FOR ALLOW TO REGISTRATION FOR 1ST YEAR STUDENT ON 14-JULY-2020
                     //if (Convert.ToInt32(semesterno) <= 2)
                     //{
                     //    ddlApplicationType.Enabled = false;
                     //    btnTPSubmitType.Enabled = false;
                     //    btnTpchangeRequest.Enabled = false;
                     //    btnTPCancel.Enabled = false;
                     //}
                     string idno1 = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO = '" + Session["IDNO"] + "'");// 17-03-2023
                     //string idno1 = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "CATEGORY_STATUS", "A.IDNO = '" + Session["IDNO"] + "'");
                     ViewState["idno"] = idno1;
                     string CategoryStatus = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.CATEGORY_STATUS", "A.IDNO = '" + ViewState["idno"] + "'");

                     //STATUS FOR CATEGORY CHNAGE REQUEST
                     if (CategoryStatus == Convert.ToString('R'))
                     {
                         //STATUS FOR REJECT
                         string StudentRejectStatus = objCommon.LookUp("ACD_STUDENT", "REJECTSTUDSTATUS", "IDNO = '" + ViewState["idno"] + "'");
                         int StudentRejectStatus1 = Convert.ToInt32(StudentRejectStatus);
                         if (StudentRejectStatus1 == 1)
                         {
                             ddlApplicationType.Enabled = false;
                             btnTPSubmitType.Enabled = false;
                             btnTpchangeRequest.Enabled = false;
                             objCommon.DisplayMessage("Your Request For Modify TP Category Are Rejected !", this.Page);
                            
                         }
                         else
                         {
                             ddlApplicationType.Enabled = false;
                             btnTPSubmitType.Enabled = false;
                             btnTpchangeRequest.Enabled = true;
                         }
                     }

                    //added by sumit on 17032020

                     //if (ddlApplicationType.Enabled ==false)
                     //{
                     //  //  btnTPSubmitType.Enabled = false;
                     //    btnTpchangeRequest.Enabled = true;
                     //}
                    // if (ddlApplicationType.Enabled == true)
                    //{
                    //    btnTpchangeRequest.Enabled = false;
                    //}


                    // string CategoryStatusOnLoad = objCommon.LookUp("ACD_STUDENT", "CATEGORY_STATUS", "IDNO = '" + ViewState["idno"] + "'");  //17-03-2023
                     string CategoryStatusOnLoad = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.CATEGORY_STATUS", "A.IDNO = '" + ViewState["idno"] + "'");
                     //STATUS FOR SUBMIT AGAIN AFTER TP_CATEGORY CHANGE REQUEST APPROVE BY TPO
                     if (CategoryStatus == Convert.ToString('C'))
                     {
                         ddlApplicationType.Enabled = false;
                         btnTPSubmitType.Enabled = false;
                         btnTpchangeRequest.Enabled = true;
                     }

                     //STATUS FOR APPROVED
                     if (CategoryStatus == Convert.ToString('A'))
                     {
                        // ddlApplicationType.Enabled = false;
                         btnTPSubmitType.Enabled = true;
                         btnTpchangeRequest.Enabled = false;
                     }

                     //string CategoryStatusOnLoad1 = objCommon.LookUp("ACD_STUDENT", "CATEGORY_STATUS", "IDNO = '" + ViewState["idno"] + "'");   //17-03-2023
                     string CategoryStatusOnLoad1 = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.CATEGORY_STATUS", "A.IDNO = '" + ViewState["idno"] + "'");
                     //STATUS FOR SUBMIT FIRSTIME
                     if (CategoryStatus == Convert.ToString('F'))
                     {
                         ddlApplicationType.Enabled = false;
                         btnTPSubmitType.Enabled = false;
                         btnTpchangeRequest.Enabled = true;
                     }
                    

                    // string TpstatusNo =objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'");

                    //if (TpstatusNo == "")
                    //{
                    // TpstatusNo = Convert.ToString(0);
                    //}
                    // int TPStatus = Convert.ToInt32(TpstatusNo);

                    // int StatusCategory = Convert.ToInt32(ViewState["TP_CATEGORY"]);
                    // if ( StatusCategory== TPStatus)
                    // {
                    //     ddlApplicationType.Enabled = false;
                    // }
             
                    if (Session["IDNO"] != string.Empty)
                    {

                        //  string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtSearchText.Text.Trim() + "'");

                        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO = '" + Session["IDNO"] + "'");
                         ViewState["idno"]=idno;

                        //if (idno != "")
                        //{
                        //    string semester = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO = '" + Session["IDNO"] + "'");

                        int studentId = Convert.ToInt32(idno.ToString());

                        ViewState["idno"] = studentId;
                        //    int semesterno = Convert.ToInt32(semester.ToString());
                        //    //ddlSemester.SelectedItem.Text = objCommon.LookUp("ACD_SEMESTER", "SEMFULLNAME", "SEMESTERNO = " + semesterno + "");
                        if (idno == string.Empty)
                        {
                            ViewState["idno"] = null;
                            objCommon.DisplayMessage("Student Name Not Found", this.Page);

                            return;
                        }
                        //else
                        //{
                         
                        //    string Session1 = objCommon.LookUp("ACD_SESSION_MASTER", "max(SESSIONNO) AS sessionno", "");
                 
                        //}

                    }
                    else
                    {
                        // ShowMessage("Please Enter Correct Student Name..");
                        //objCommon.DisplayMessage("Please Enter Correct Student Name..",this.Page);
                    }
                }
                else
                {
                    updcategory.Visible = false;
                    ShowMessage("You are Not Authorized to View this Page!!");

                }
                BindTpCategory();
                getconfsemester();

            }
         //BIND THE TP CATEGORY ON DROPDOWN 18-FRB-2020
             
            
        }
      //  string TPCategoryNo = objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO=" + idno);
      

    }

    private void BindTpCategory()
    {
       // string Category = objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'"); 17-03-2023
        string Category = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.TP_CATEGORY", "A.IDNO = '" + ViewState["idno"] + "'");
        if (Category == "")
        {
            Category = Convert.ToString(0);
            ddlApplicationType.SelectedValue = Category;
        }
        else
        {
            ddlApplicationType.SelectedValue = Category;
           
        }
    }

    public void getconfsemester()
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "semesterno", "DEGREENO", "IDNO = '" + Session["IDNO"] + "'", "");
        int semesterno = Convert.ToInt32(ds.Tables[0].Rows[0]["semesterno"].ToString());
        int degreeno = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());

        DataSet ds1 = objCommon.FillDropDown("TP_CATEGORY_CONFIGURATION_STATUS", "SEMESTERNO", "DEGREENO", "DEGREENO = '" + degreeno + "'and IS_ACTIVE=1", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            int semesternocof = Convert.ToInt32(ds1.Tables[0].Rows[0]["SEMESTERNO"].ToString());
            if (semesternocof == 1)
            {
                lblsemester.Text = "First";
            }
            if (semesternocof == 2)
            {
                lblsemester.Text = "Second";
            }
            if (semesternocof == 3)
            {
                lblsemester.Text = "Third";
            }
            if (semesternocof == 4)
            {
                lblsemester.Text = "Fourth";
            }
            if (semesternocof == 5)
            {
                lblsemester.Text = "Fifth";
            }
            if (semesternocof == 6)
            {
                lblsemester.Text = "Sixth";
            }
            if (semesternocof == 7)
            {
                lblsemester.Text = "Seventh";
            }
            if (semesternocof == 8)
            {
                lblsemester.Text = "Eighth";
            }
            if (semesternocof == 9)
            {
                lblsemester.Text = "Ninth";
            }
            if (semesternocof == 10)
            {
                lblsemester.Text = "Tenth";
            }
            if (semesternocof == 11)
            {
                lblsemester.Text = "Eleventh";
            }
            if (semesternocof == 12)
            {
                lblsemester.Text = "Twelve";
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
           // Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TP_Category_Updation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TP_Category_Updation.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
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

    protected void btnTPSubmitType_Click(object sender, EventArgs e)
    {
       // string semesterno = objCommon.LookUp("ACD_STUDENT", "semesterno,BRANCHNO", "IDNO = '" + Session["IDNO"] + "'");
       // string semesterno = string.Empty;
        int semesternocof=0;
        int semesterno=0;
        int degreeno =0;
        int status = 0;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "semesterno", "DEGREENO", "IDNO = '" + Session["IDNO"] + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
         semesterno = Convert.ToInt32(ds.Tables[0].Rows[0]["semesterno"].ToString());
         degreeno = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
        }

        DataSet ds1 = objCommon.FillDropDown("TP_CATEGORY_CONFIGURATION_STATUS", "SEMESTERNO", "DEGREENO,Cast(IS_ACTIVE as int) IS_ACTIVE", "DEGREENO = '" + degreeno + "'", "");
       if (ds1.Tables[0].Rows.Count > 0)
       {
         semesternocof = Convert.ToInt32(ds1.Tables[0].Rows[0]["SEMESTERNO"].ToString());
         status = Convert.ToInt32(ds1.Tables[0].Rows[0]["IS_ACTIVE"].ToString());
       }

      
        //if (ddlApplicationType.SelectedValue == "0" && Convert.ToInt32(semesterno) <= 6 )
       if (Convert.ToInt32(semesterno) < semesternocof && status==1)
        {
            objCommon.DisplayMessage(updcategory, "You Are Not Eligible For Application Category Registration !", this.Page);
            return;
        }
        else if (ds1.Tables[0].Rows.Count == 0)
        {

            objCommon.DisplayMessage(updcategory, "Category activity configuration is not deffined. Please contact TP administrator!", this.Page);
            return;
        }
       else if (Convert.ToInt32(semesterno) < semesternocof && status == 0)
       {

           objCommon.DisplayMessage(updcategory, "Category activity configuration is Inactive. Please contact TP administrator!", this.Page);
           return;
       }
        if(ddlApplicationType.SelectedValue=="0")
        {
            objCommon.DisplayMessage(updcategory, "Please select Application Category Type !", this.Page);
          //  string IDNO = objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'");
          //  if (IDNO == "")
           // {
          //      IDNO = Convert.ToString(0);
          //      ddlApplicationType.SelectedValue = IDNO;
          //  }
          //  else
          //  {
           //     ddlApplicationType.SelectedValue = IDNO;
           // }
            return;
           
        }
        else     
        {
            // string TpstatusNo1 =objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'");

            //if (TpstatusNo1 == "")
            //{
            //    TpstatusNo1 = Convert.ToString(0);
            //}
            // int TPStatus1 = Convert.ToInt32(TpstatusNo1);

            // if (TPStatus1 == 1 || TPStatus1 == 2 || TPStatus1 == 3)
            // {
            //     objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));
            //     objCommon.DisplayMessage(updcategory, "Please wait To Get Approval from TPO for Change the Category !", this.Page);
            //     ddlApplicationType.Enabled = false;
            //     return;
            // }

           // string TpstatusNo =objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'");  //17-03-2023
            string TpstatusNo = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.CATEGORY_STATUS", "A.IDNO = '" + ViewState["idno"] + "'");
            if (TpstatusNo == "")
            {
                TpstatusNo = Convert.ToString(0);
            }
             int TPStatus = Convert.ToInt32(TpstatusNo);
            
             if (TPStatus == 1 || TPStatus == 2 || TPStatus == 3)
             {
                 string idno1 = objCommon.LookUp("ACD_STUDENT", "IDNO", "IDNO = '" + Session["IDNO"] + "'");
                 ViewState["idno"] = idno1;

                 string CategoryStatus = objCommon.LookUp("ACD_STUDENT", "CATEGORY_STATUS", "IDNO = '" + ViewState["idno"] + "'");
                 if (CategoryStatus == Convert.ToString("R"))
                 {

                     objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));
                     objCommon.DisplayMessage(updcategory, "Please wait To Get Approval from TPO for Change the Category !", this.Page);
                     ddlApplicationType.Enabled = false;
                     return;
                 }
                 else
                 {
                     if (ddlApplicationType.Enabled == true)
                     {
                         CustomStatus cs = (CustomStatus)objTP.TP_Category(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlApplicationType.SelectedValue));
                         if (cs.Equals(CustomStatus.RecordSaved))
                         {
                             //objCommon.DisplayMessage(updcategory, "Application Category Type Save Successfully !", this.Page);
                             // ddlApplicationType.SelectedIndex = 0;
                             BindTpCategory();

                            // objTP.Change_TP_Category_On_Load(Convert.ToInt32(ViewState["idno"]));

                            // objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));

                             //string TpstatusNo1 = objCommon.LookUp("ACD_STUDENT", "TP_CATEGORY", "IDNO = '" + ViewState["idno"] + "'"); //17-03-2023
                             string TpstatusNo1 = objCommon.LookUp("ACD_STUDENT A Inner Join [dbo].[ACD_TP_APPLICATION_CATEGORY] B ON (A.IDNO = B.IDNO)", "B.CATEGORY_STATUS", "A.IDNO = '" + ViewState["idno"] + "'");
                             if (TpstatusNo1 == "")
                             {
                                 TpstatusNo1 = Convert.ToString(0);
                             }
                             int TPStatuss = Convert.ToInt32(TpstatusNo1);
                             ViewState["TP_CATEGORY"] = TPStatuss;
                             ddlApplicationType.Enabled = false;
                             btnTPSubmitType.Enabled = false;
                             btnTpchangeRequest.Enabled = true;

                         }

                     }
                     //else
                     //{
                     //    objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));
                     //    objCommon.DisplayMessage(updcategory, "Please wait To Get Approval from TPO for Change the Category !", this.Page);
                     //}
                 }
             }
             else
             {
                 CustomStatus cs = (CustomStatus)objTP.TP_Category(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlApplicationType.SelectedValue));
                 if (cs.Equals(CustomStatus.RecordSaved))
                 {
                     objCommon.DisplayMessage(updcategory, "Application Category Type Save Successfully !", this.Page);
                     // ddlApplicationType.SelectedIndex = 0;
                     BindTpCategory();
                     //objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));
                     ddlApplicationType.Enabled = false;
                     btnTPSubmitType.Enabled = false;
                     btnTpchangeRequest.Enabled = true;

                 }
             }
            
     
        
        }

    }
    protected void btnTPCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnTpchangeRequest_Click(object sender, EventArgs e)
    {
        string semesterno = objCommon.LookUp("ACD_STUDENT", "semesterno", "IDNO = '" + Session["IDNO"] + "'");
        if (Convert.ToInt32(semesterno) == 5 || Convert.ToInt32(semesterno) == 6 || Convert.ToInt32(semesterno) == 7 || Convert.ToInt32(semesterno) == 8)
        {
           
        if (ddlApplicationType.Enabled == false)
        {
           
            objTP.Change_TP_Category(Convert.ToInt32(ViewState["idno"]));
            objCommon.DisplayMessage(updcategory, "Please wait To Get Approval from TPO for Change the Category !", this.Page);
                   
        }
        //else
        //{
        //    btnTPSubmitType.Enabled = false;
        //}
      }
        else
        {
            objCommon.DisplayMessage(updcategory, "You Can Not Modify Application Category !", this.Page);
            return;    
        }
       
    }
}