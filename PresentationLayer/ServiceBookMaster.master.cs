using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using DocumentFormat.OpenXml.Drawing;



public partial class ServiceBookMaster : System.Web.UI.MasterPage
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();
    public static int _idnoEmp; public static int college_id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int idnoEmp = 0; int collegeid = 0; int stno = 0;
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
                //if (Session["IsCheckAuth"] == null)
                //{
                //    UserAuthorized = GetUserRight();
                //    if (UserAuthorized != "YAYMYR")
                //    {
                //        Response.Redirect("~/UnAuthorizedAccess.aspx");
                //    }

                //}
                //Session["IsCheckAuth"] = "1";
                Page.Title = Session["coll_name"].ToString();
                string Currenturl = HttpContext.Current.Request.Url.AbsoluteUri;
                string fileName = System.IO.Path.GetFileName(Currenturl);

                EmpCreateController objECC = new EmpCreateController();
                DataTable dt = objECC.MenuDetails(Convert.ToInt32(Session["usertype"].ToString()), 0);
                if (dt.Rows.Count > 0)
                {
                    PopulateMenu(dt, 0, null);
                }
                else
                {
                     //
                }

                if (Request.UrlReferrer != null)
                {
                     string url = Request.UrlReferrer.ToString();
                     int usertype = Convert.ToInt32(Session["usertype"]);
                     //if (fileName != "ServiceBookMain.aspx?pageno=842")
                     //{
                     //    if ((Convert.ToInt32(Session["usertype"]) != 1) && ((fileName == "Pay_Sb_PayRevision.aspx") || (fileName == "Pay_Sb_Increment_Termination.aspx") || (fileName == "Pay_Sb_LoansAndAdvance.aspx")))
                     //    {

                     //        MessageBox("Sorry! U have not privilege for this tab!");
                     //        Response.Redirect(url);
                     //        return;
                     //    }                        

                     //}
                 
                        int count_page = url.Length;
                        int index_page = url.IndexOf("Pay_Sb", 0);
                        if (index_page != -1)
                        {
                            if (Session["serviceIdNo"] != null && ddlEmployee.SelectedIndex <= 0)//to keep the employee name for all the ServiceBook Pages
                            {
                                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                               
                                idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString());
                                   
                                ddlEmployee.SelectedValue = idnoEmp.ToString(); //07-12-2016

                                _idnoEmp = Convert.ToInt32(Session["serviceIdNo"]);//17-10-2015
                                ddlEmployee.SelectedValue = "0";
                                ddlEmployee.SelectedValue = Session["serviceIdNo"].ToString();


                            }
                            else
                            {
                                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                               
                            }

                        }
                        else
                        {
                            if (Session["serviceIdNo"] != null)
                            {
                                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                                ddlEmployee.SelectedValue = Session["serviceIdNo"].ToString();
                            }
                            else
                            {
                                Session["serviceIdNo"] = null;
                                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                            }

                        }

                }
                else
                {
                    if (Session["serviceIdNo"] != null)
                    {
                        this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                        ddlEmployee.SelectedValue = Session["serviceIdNo"].ToString();
                    }
                    else
                    {
                        Session["serviceIdNo"] = null;
                        this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                    }
                    
                }

               // if (Request.QueryString["id"] != null)
                if(Session["serviceIdNo"] !=null)
                {
                    ViewState["action"] = "edit";
                    ShowEmpDetails();

                }
                //Session["serviceIdNo"].ToString();
                if (Session["serviceIdNo"] != null) 
              //  if (Request.QueryString["refno"] != null) //Added by Saket Singh on 07-12-2016
                {

                   // if (Request.QueryString["orderby"] != null)
                    //if(ddlorderby.SelectedIndex>0)
                    //{

                    //    ddlorderby.SelectedValue = (Request.QueryString["orderby"].ToString());
                    //}


                    // Session["refno"] = (Request.QueryString["refno"].ToString());//9?id=1
                   // string refno_url = (Request.QueryString["refno"].ToString());//2?id=1
                    //Session["refno"] = refno_url;
                    //===============================================================
                    //Code to Allow aaccess to EMployee login
                    //int ua_type = 0;
                    //ua_type = Convert.ToInt32(Session["usertype"].ToString());
                    //if (Convert.ToInt32(Session["refno"]) != 4 && Convert.ToInt32(Session["refno"]) != 6 && Convert.ToInt32(Session["refno"]) != 7 &&
                    //    Convert.ToInt32(Session["refno"]) != 15 && Convert.ToInt32(Session["refno"]) != 16 && Convert.ToInt32(Session["refno"]) != 17
                    //    && Convert.ToInt32(Session["refno"]) != 18 && Convert.ToInt32(Session["refno"]) != 19 && Convert.ToInt32(Session["refno"]) != 20)//20
                    //{
                    //    if (ua_type == 1)
                    //    {
                    //        //allowed
                    //    }
                    //    else
                    //    {
                    //        //Code to Deactivate all tabs to view for Individual Employee login except Professional Tab
                    //        MessageBox("Sorry! U have not privilege for this tab!");
                    //       // Response.Redirect("~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=6"); //07-12-2016
                    //        return;

                    //    }
                    //}
                    //===============================================================

                    //========================07-DEC-2016

                    if (Session["serviceIdNo"] != null) 
                   // if (Request.QueryString["id"] != null)
                    {
                        Session["serviceIdNo"] = Session["serviceIdNo"].ToString();// (Request.QueryString["id"].ToString());
                       
                    }
                    else
                    {
                       
                    }                  
                    
                }
                else
                {
                   // if (Request.QueryString["idnoEmp"] != null)
                    if (Session["serviceIdNo"] != null) 
                    {

                    }
                    else
                    {
                        Session["serviceIdNo"] = null;
                        Session["serviceCollegeNo"] = null;
                        Session["serviceSTNO"] = null;
                        Session["refno"] = 1;
                    }
                }
                if (Session["serviceIdNo"] != null)
                {
                    idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString());
                    // lblApprove.Text = ViewState["REMARK"].ToString();

                }
                if (Session["serviceCollegeNo"] != null)
                {
                    collegeid = Convert.ToInt32(Session["serviceCollegeNo"].ToString());

                }
                if (Session["serviceSTNO"] != null)
                {
                    stno = Convert.ToInt32(Session["serviceSTNO"].ToString());

                }

            }

            ddlEmployee.Enabled = true;
            ddlorderby.Enabled = true;
            //HideTr.Visible = true;

            if (Session["serviceIdNo"] == null) 
           // if (Request.QueryString["idnoEmp"] == null)//07-12-2016
            {

                this.FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue)); //07-12-2016
                //ddlCollege.SelectedValue = collegeid.ToString();  
                //FillDropDownByIdno();//129
                ddlEmployee.SelectedValue = idnoEmp.ToString(); //07-12-2016
                //lblApprove.Text = ViewState["REMARK"].ToString();

            }


        }   

    }


    private void PopulateMenu(DataTable dt, int parentMenuId, MenuItem parentMenuItem)
    {
        EmpCreateController objECC = new EmpCreateController();
        string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        foreach (DataRow row in dt.Rows)
        {
            MenuItem menuItem = new MenuItem
            {
                Value = row["MenuId"].ToString(),
                Text = row["Title"].ToString(),
                NavigateUrl = row["Url"].ToString(),
                Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
            };
            if (parentMenuId == 0)
            {
               Menu1.Items.Add(menuItem);
              //  mainMenu.Items.Add(menuItem);

                DataTable dtChild = objECC.MenuDetails(Convert.ToInt32(Session["usertype"].ToString()), int.Parse(menuItem.Value));
                PopulateMenu(dtChild, int.Parse(menuItem.Value), menuItem);
            }
            else
            {
                parentMenuItem.ChildItems.Add(menuItem);
            }
        }
    }






    private void ShowEmpDetails()
    {
        try
        {
            //string idno = Request.QueryString["idnoEmp"].ToString();
            string idno = Session["serviceIdNo"].ToString();
            //FillCollege();
            //select college_no,* FROM PAYROLL_EMPMAS WHERE IDNO=1
            //DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "college_no", "STNO", "IDNO=" + Convert.ToInt32(idno) + "", "");
            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "SEQ_NO", "STNO", "IDNO=" + Convert.ToInt32(idno) + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["college_no"].ToString();
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
            }
            //FillEmployeeByCollegeNo();
            ddlEmployee.SelectedValue = Session["serviceIdNo"].ToString(); //Request.QueryString["idnoEmp"].ToString();

            //Session["serviceIdNo"] = Request.QueryString["id"].ToString();
            //Session["serviceCollegeNo"] = ddlCollege.SelectedValue.ToString();
            //Session["serviceSTNO"] = ddlStaffType.SelectedValue.ToString();
            //lblApprove.Text = ViewState["REMARK"].ToString();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowEmpDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropDown(Convert.ToInt32(ddlorderby.SelectedValue));
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        //Session["serviceIdNo"] = ddlEmployee.SelectedValue; //07-12-2016  
        //Response.Redirect(Request.Url.ToString().Trim());


        //=========================================
       
        if (ddlEmployee.SelectedIndex != 0)
        {
            Session["serviceIdNo"] = ddlEmployee.SelectedValue;
           

            ViewState["idno"] = ddlEmployee.SelectedValue;
           

            ViewState["orderby"] = null;
           
            Response.Redirect(Request.Url.ToString().Trim());

        }
        //===========================================


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
           // if (user_type == 3 || user_type == 4)
            if (user_type!=1)
            {
                //username
                int ua_idno = Convert.ToInt32(Session["idno"].ToString());
                Session["serviceIdNo"] = ua_idno;
                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", " + CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + "  and EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO=" + ua_idno + " AND EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");



                ListItem removeItem = ddlEmployee.Items.FindByValue("0");
                ddlEmployee.Items.Remove(removeItem);
                
            }

            //else
            //{
            if (user_type == 1 || Session["username"].ToString() == "registrar".ToString().Trim())
            {
                if (val == 1)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "  CONVERT(NVARCHAR(20),EM.IDNO) +'-'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
                if (val == 2)
                    objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", " EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.FNAME,EM.MNAME,EM.LNAME");
            }
            //}

        }
        catch (Exception ex)
        {

            throw new IITMSException("IITMS.UAIMS.PayRoll_Pay_ServiceBookEntry.FillDropDown-> " + ex.ToString());
        }
    }

}
