//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ServiceBookEntry.ASPX                                                    
// CREATION DATE : 19-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PayRoll_Pay_ServiceBookEntry : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();   
    PayController objpay = new PayController();
    static bool IscheckAuth = true;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
       
    }
    //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
    //  this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
          
    protected void Page_Load(object sender, EventArgs e)
    {
        int id=0;
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
                //CheckPageAuthorization();
                if (Session["IscheckAuth"] == null)
                {
                    CheckPageAuthorization();
                }

                Session["IscheckAuth"] = "1";
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if (Request.QueryString["refno"] != null)
                {
                   
                    Session["refno"] = (Request.QueryString["refno"].ToString());
                    ToDoUserControlVisibleTrueAndfalse(Convert.ToInt32(Session["refno"].ToString()));
                   
                        
                }
                else
                {
                    Session["serviceIdNo"] = null;
                    Session["refno"] = 1;
                }

                //string url = Request.QueryString["refno"].ToString();
                if (Session["serviceIdNo"] != null)
                {
                    id = Convert.ToInt32(Session["serviceIdNo"].ToString());
                    //ddlEmployee.SelectedValue = Session["serviceIdNo"].ToString();
                }
                
            }

            
            //if (Convert.ToInt32(Session["userno"].ToString()) > 1)
            //{
            //    this.FillDropDownByIdno(Convert.ToInt32(Session["idno"].ToString()));
            //    ddlEmployee.Enabled = false;
            //    ddlorderby.Enabled = false;
            //    HideTr.Visible = false;
            //    ddlorderby.SelectedValue = "2";                
            //}
            //else
            //{
                ddlEmployee.Enabled = true;
                ddlorderby.Enabled = true;
                //HideTr.Visible = true;
                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
               // this.FillEmployeeByStaffno();

                ddlEmployee.SelectedValue = id.ToString();
                
              
               // this.FillRFIDNO();
                
            //}
        }
        else
        {

            if (Request.Params["__EVENTTARGET"] != null &&
            Request.Params["__EVENTTARGET"].ToString() != string.Empty &&
            Request.Params["__EVENTTARGET"].ToString() == "div")
            {
                for (int i = 1; i <= 17; i++)
                {
                    if (Request.Params["__EVENTARGUMENT"] != null &&
                        Request.Params["__EVENTARGUMENT"].ToString() != string.Empty &&
                        Convert.ToInt32(Request.Params["__EVENTARGUMENT"].ToString()) == i)
                    {
                        this.ToDoUserControlVisibleTrueAndfalse(i);
                    }
                }
            }

        }
       
        //if (ddlEmployee.SelectedValue != null && ddlEmployee.SelectedValue != string.Empty && ddlEmployee.SelectedValue != "0" && ddlEmployee.SelectedValue != "-1" && ddlEmployee.SelectedValue != "")
        //{
        //    //UserControl1.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl2.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl3.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);            
        //    //UserControl4.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl5.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl6.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl7.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl8.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl9.IdnoEmp  = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl10.IdnoEmp = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl11.IdnoEmp = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl12.IdnoEmp = Convert.ToInt32(ddlEmployee.SelectedValue);
        //   // UserControl13.IdnoEmp = Convert.ToInt32(ddlEmployee.SelectedValue);
        //    //UserControl14.IdnoEmp = Convert.ToInt32(ddlEmployee.SelectedValue);
        //}
        //else
        //{
        //    //UserControl1.IdnoEmp  = 0;
        //    //UserControl2.IdnoEmp  = 0;
        //    //UserControl3.IdnoEmp  = 0;
        //    //UserControl4.IdnoEmp  = 0;
        //    //UserControl5.IdnoEmp  = 0;          
        //    //UserControl6.IdnoEmp  = 0;
        //    //UserControl7.IdnoEmp  = 0;
        //    //UserControl8.IdnoEmp  = 0;
        //    //UserControl9.IdnoEmp  = 0;
        //    //UserControl10.IdnoEmp = 0;
        //    //UserControl11.IdnoEmp = 0;
        //    //UserControl12.IdnoEmp = 0;
        //    //UserControl13.IdnoEmp = 0;
        //    //UserControl14.IdnoEmp = 0;
        //}       

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["serviceIdNo"] = ddlEmployee.SelectedValue;
        IscheckAuth = false;
        Response.Redirect("~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno="+Session["refno"].ToString());
        
    }

    private void  ToDoUserControlVisibleTrueAndfalse(int number)
    {
        if (number == 1)
        {
            UserControl1.Visible = true;
            UserControl2.Visible= false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
            

        }
        else if (number == 2)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = true;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 3)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = true;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 4)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = true;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 5)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = true;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 6)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = true;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 7)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = true;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 8)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = true;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 9)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = true;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 10)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = true;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 11)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = true;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 12)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = true;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 13)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = true;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 14)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = true;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 15)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = true;
            UserControl16.Visible = false;
            UserControl17.Visible = false;
        }
        else if (number == 16)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = true;
            UserControl17.Visible = false;
        }
        else if (number == 17)
        {
            UserControl1.Visible = false;
            UserControl2.Visible = false;
            UserControl3.Visible = false;
            UserControl4.Visible = false;
            UserControl5.Visible = false;
            UserControl6.Visible = false;
            UserControl7.Visible = false;
            UserControl8.Visible = false;
            UserControl9.Visible = false;
            UserControl10.Visible = false;
            UserControl11.Visible = false;
            UserControl12.Visible = false;
            UserControl13.Visible = false;
            UserControl14.Visible = false;
            UserControl15.Visible = false;
            UserControl16.Visible = false;
            UserControl17.Visible = true;
        }
    }
    private void FillDropDown(int val)
    {
        try
        {
            //if (val == 1)
            //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
            //if (val == 2)
            //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");

            int user_type = 0;
            user_type = Convert.ToInt32(Session["usertype"].ToString());
            if (user_type == 3 || user_type == 4 || user_type==5)
            {
                //username//Session["idno"]
                int ua_idno = Convert.ToInt32(Session["idno"].ToString());//Session["username"].ToString());
                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + " AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + " AND EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");

            }
            else
            {
                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");

            }

        }
        catch (Exception ex)
        {

            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
    }
  

    private void FillDropDownByIdno(int idno)
    {
        try
        {
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO=" + idno + " and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
            ddlEmployee.SelectedValue = idno.ToString();
        }
        catch (Exception ex)
        {

            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
    }



    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
       this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
    }



    //protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillEmployeeByStaffno();

    //}
    //private void FillRFIDNO()
    //{
    //    objCommon.FillDropDownList(ddlRFID, "PAYROLL_EMPMAS", "RFIDNO", "RFIDNO AS RFID", "RFIDNO>0 AND ACTIVE ='Y'", "RFIDNO");
    //}

    //private void FillEmployeeByRFIDno()
    //{
    //    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.RFIDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.ACTIVE='Y' AND EM.RFIDNO ="+ddlRFID.SelectedValue, "");
    //}
    //protected void ddlRFID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   FillEmployeeByRFIDno();
    //   ddlEmployee.Enabled = true;
    //}


    //protected void txtrfid_TextChanged(object sender, EventArgs e)
    //{
    //    string rfidno = string.Empty;
    //    rfidno = txtrfid.Text;
    //    string status = objCommon.LookUp("PAYROLL_EMPMAS","ACTIVE","RFIDNO="+ rfidno);
    //    if (status == "N")
    //    {
    //        MessageBox("Employee is Inactive");
    //        ddlEmployee.Enabled = false;
    //    }
    //    else
    //    {
    //        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.RFIDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.RFIDNO =" + txtrfid.Text, "");
    //        ddlEmployee.Enabled = true;
    //    }
    //}

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}
