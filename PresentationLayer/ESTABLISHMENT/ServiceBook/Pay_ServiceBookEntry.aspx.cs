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
using System.Web.UI.WebControls;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = 0;

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
                //if (IscheckAuth == true)
                //{
                //    CheckPageAuthorization();
                //}
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
                //Response.Redirect("~/Establishment/ServiceBook/Pay_ServiceBookEntry.aspx?refno=" + Session["refno"].ToString() + "&id=" + ddlEmployee.SelectedValue);
                if (Request.QueryString["id"] != null)
                {
                    ViewState["action"] = "edit";
                    ShowEmpDetails();

                }

                //==============
                int user_type = 0;
                user_type = Convert.ToInt32(Session["usertype"].ToString());
                if (user_type != 1)
                {
                   
                    ddlEmployee.Enabled = false;
                    //=============Menu hide====================
                    var menu = Page.Master.FindControl("mainMenu") as Menu;
                    mainMenu.Items.Remove(mainMenu.FindItem("Financial"));
                    mainMenu.Items.Remove(mainMenu.FindItem("Other"));
                }
                else
                {
                    
                    ddlEmployee.Enabled = true;

                }
                //============
                if (Request.QueryString["refno"] != null)
                {
                    if (Request.QueryString["orderby"] != null)
                    {

                        ddlorderby.SelectedValue = (Request.QueryString["orderby"].ToString());
                    }                   
                    string refno_url = (Request.QueryString["refno"].ToString());//2?id=1
                    Session["refno"] = refno_url;
                    //========================15-DEC-2015
                    // if (refno_url.ToString().IndexOf("?id=") > 0)
                    Session["serviceIdNo"] = Session["serviceIdNo"].ToString();
                    if (Request.QueryString["id"] != null)
                    {
                        Session["serviceIdNo"] = (Request.QueryString["id"].ToString());
                        //Request.QueryString["refno"]=2?id=1
                        //here i have to update  Session["serviceIdNo"] = refno_url.IndexOf("?id=")
                        //line number=83
                    }
                    else
                    {
                        // Session["refno"] = refno_url;
                    }
                    //===========================
                    ToDoUserControlVisibleTrueAndfalse(Convert.ToInt32(Session["refno"].ToString()));
                }
                else
                {
                    if (Request.QueryString["id"] != null)
                    {

                    }
                    else
                    {
                        Session["serviceIdNo"] = null;

                        Session["refno"] = 1;
                    }
                }

                if (Session["serviceIdNo"] != null)
                {
                    id = Convert.ToInt32(Session["serviceIdNo"].ToString());

                }
            }



            ddlEmployee.Enabled = true;
            ddlorderby.Enabled = true;

            if (Request.QueryString["id"] == null)//15-12-2015
            {


                // FillEmployeeByCollegeNo();//129
                FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
                int user_type = Convert.ToInt32(Session["usertype"].ToString());
                //ddlEmployee.SelectedValue = id.ToString();//15-12-2015//activated on 15-12-2017
                if (user_type == 1)//13012018
                {
                    ddlEmployee.SelectedValue = id.ToString();//15-12-2015//activated on 15-12-2017
                }
                if (Session["serviceIdNo"] == null && ddlEmployee.SelectedValue.ToString() != "0")
                {
                    Session["serviceIdNo"] = ddlEmployee.SelectedValue.ToString();
                }

                //15-12-2015
            }

        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    // bindlist(arg[0], arg[1]);
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancemodal"))
                {
                    //UnBindList();
                }
            }
            if (Request.Params["__EVENTTARGET"] != null &&
            Request.Params["__EVENTTARGET"].ToString() != string.Empty &&
            Request.Params["__EVENTTARGET"].ToString() == "div")
            {
                for (int i = 1; i <= 21; i++)
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



    }
    private void ShowEmpDetails()
    {
        try
        {
            string idno = Request.QueryString["id"].ToString();


            // FillEmployeeByCollegeNo();
            FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
            ddlEmployee.SelectedValue = Request.QueryString["id"].ToString();
            Session["serviceIdNo"] = Request.QueryString["id"].ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowEmpDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
        //Response.Redirect("~/Establishment/ServiceBook/Pay_ServiceBookEntry.aspx?refno="+Session["refno"].ToString());
        Response.Redirect("~/Establishment/ServiceBook/Pay_ServiceBookEntry.aspx?refno=" + Session["refno"].ToString() + "&id=" + ddlEmployee.SelectedValue);
        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
    }

    private void ToDoUserControlVisibleTrueAndfalse(int number)
    {
        if (number == 1)
        {
            UserControl1.Visible = true;
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

            int user_type = 0;
            user_type = Convert.ToInt32(Session["usertype"].ToString());
            if (user_type != 1)
            {
                //username
                //int ua_idno = Convert.ToInt32(Session["username"].ToString());
                int ua_idno = Convert.ToInt32(Session["idno"].ToString());

                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + "  and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + " AND EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");



                ddlEmployee.SelectedValue = ua_idno.ToString();
                ddlEmployee.Enabled = false;

                //=============Menu hide====================
               // MenuItem item = NavigationMenu.FindItem(valuePath);
                //if (mainMenu.MenuItems.Contains(Financial))
                //{
                //   // MessageBox.Show("The File menu contains 'Open' ", fileMenu.Text);
                //}
                //var menu = Page.Master.FindControl("mainMenu") as Menu;
                //mainMenu.Items.Remove(mainMenu.FindItem("Financial"));
                //mainMenu.Items.Remove(mainMenu.FindItem("Other"));
               
            }
            else
            {

                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "  CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
                ddlEmployee.Enabled = true;


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

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}
