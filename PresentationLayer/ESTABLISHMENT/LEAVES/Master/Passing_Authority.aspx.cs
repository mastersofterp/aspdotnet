using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ESTABLISHMENT_LEAVES_Master_Passing_Authority : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objPAuthority = new LeavesController();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

              
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillCollege();
                FillDepartment();
                FillUser();
                BindListViewPAuthority();

                btnAdd.Visible = true;
                btnShowReport.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
        divMsg.InnerHtml = string.Empty;
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

    private void FillDepartment()
    {
        try
        {
            if (Convert.ToInt32(ddlCollege.SelectedValue) > 0)
            {
                //objCommon.FillDropDownList(ddlDepartment, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "D.SUBDEPT", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "D.SUBDEPT");
                objCommon.FillDropDownList(ddlDepartment, "Payroll_subdept", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
               // objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "DISTINCT SUBDEPTNO", "SUBDEPT", "SUBDEPTNO > 0", "SUBDEPT");

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
       
    }
    protected void BindListViewPAuthority()
    {
        try
        {
           DataSet ds = objPAuthority.GetAllPassAuthority(Convert.ToInt32(ddlCollege.SelectedValue));
           if (ds.Tables[0].Rows.Count <= 0)
           {
               btnShowReport.Visible = false;
              // dpPager.Visible = false;
           }
           else
           {
               btnShowReport.Visible = true;
             //  dpPager.Visible = true;
           }
           lvPAuthority.DataSource = ds;
           lvPAuthority.DataBind();
           pnlList.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindListViewPAuthority();
    //}
    private void Clear()
    {
        txtPAuthority.Text = string.Empty;
        ddlUser.SelectedIndex =ddlCollege.SelectedIndex= 0;
        ddlDepartment.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["PANO"] = null;
      
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnShowReport.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }
    private void FillUser()
    {
        try
        {
            //select ua_college_nos,* from USER_ACC where ua_college_nos   like('%2%')
            if (ddlCollege.SelectedValue!="0")
            {
               // objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", " UA_Type IN (3,4) and ua_college_nos like('%"+Convert.ToInt32(ddlCollege.SelectedValue) +"%')", "UA_FULLNAME");
                
               //select U.UA_NO,U.UA_FULLNAME from PAYROLL_EMPMAS E INNER JOIN USER_ACC U ON(U.UA_IDNO=E.IDNO) WHERE E.COLLEGE_NO=2 AND U.UA_TYPE IN(3,4)
                //objCommon.FillDropDownList(ddlUser, "PAYROLL_EMPMAS E INNER JOIN USER_ACC U ON(U.UA_IDNO=E.IDNO)", "U.UA_NO", "U.UA_FULLNAME ", "U.UA_TYPE IN(3,4) AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "U.UA_FULLNAME");
                //objCommon.FillDropDownList(ddlUser, "PAYROLL_EMPMAS E INNER JOIN USER_ACC U ON(U.UA_IDNO=E.IDNO)", "U.UA_NO", "U.UA_FULLNAME ", "U.UA_TYPE IN(1,2,3,6,7) AND E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "U.UA_FULLNAME"); //Modified on 09-December-2016 By Saket Singh
              //  objCommon.FillDropDownList(ddlUser, "PAYROLL_EMPMAS E INNER JOIN USER_ACC U ON(U.UA_IDNO=E.IDNO)", "U.UA_NO", "U.UA_FULLNAME ", "U.UA_TYPE IN(1,3,4,5,8) AND (E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " OR UA_COLLEGE_NOS like ('%" + Convert.ToInt32(ddlCollege.SelectedValue) + "%'))", "U.UA_FULLNAME");
                
                  //SELECT U.UA_NO,UA_FULLNAME AS UA_FULLNAME   FROM USER_acc U    WHERE    U.UA_Type in(1,3,4,5,8)   ORDER BY U.UA_FULLNAME
                objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO="+Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
                //1=ADMIN
                //3=FACULTY
                //4=DEAN/REGISTRAR
                //5=NON TEACHING 
                //8=HOD
              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Leaves objLeaves = new Leaves();
            objLeaves.PANAME = Convert.ToString(txtPAuthority.Text);
            objLeaves.UANO = Convert.ToInt32(ddlUser.SelectedValue);
            objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //SELECT * FROM PAYROLL_LEAVE_PASSING_AUTHORITY WHERE PANAME ='JNMC-HOD(DEAN)' AND UA_NO=969 AND PANO !=1
                    //DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO", "PANAME='" + txtPAuthority.Text + "' and UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO="+Convert.ToInt32(ddlCollege.SelectedValue) +"", "");
                    DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO", " UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       // objCommon.DisplayMessage("Sorry! Record Already Exists", this.Page);
                        MessageBox("Sorry! Record Already Exists");
                        Clear();
                        return;                        
                    }
                    CustomStatus cs = (CustomStatus)objPAuthority.AddPassAuthority(objLeaves);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       
                        Clear();
                        BindListViewPAuthority();
                      
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;

                        ViewState["action"] = null;
                        MessageBox("Record Saved Successfully");

                        btnAdd.Visible = true;
                        btnShowReport.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        btnBack.Visible = false;
                       // objCommon.DisplayMessage("Record Saved Sucessfully", this.Page);
                       
                    }
                }
                else
                {
                    if (ViewState["PANO"] != null)
                    {
                        objLeaves.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
                        //SELECT * FROM PAYROLL_LEAVE_PASSING_AUTHORITY WHERE PANAME ='JNMC-HOD(DEAN)' AND UA_NO=969 AND PANO !=1
                        DataSet ds = objCommon.FillDropDown("PAYROLL_LEAVE_PASSING_AUTHORITY", "UA_NO", "PANO", "PANAME='" + txtPAuthority.Text + "' and UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND PANO!=" + objLeaves.PANO + "", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //objCommon.DisplayMessage("Sorry! Record Already Exists", this.Page);
                            MessageBox("Sorry! Record Already Exists");
                            Clear();
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objPAuthority.UpdatePassAuthority(objLeaves);
                        if(cs.Equals(CustomStatus.RecordUpdated))
                        {
                           
                            Clear();
                            BindListViewPAuthority();
                           
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;

                            ViewState["action"] = null;
                            MessageBox("Record Updated Successfully");

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;
                            //objCommon.DisplayMessage("Record Updated Sucessfully", this.Page);
                           
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        //ViewState["action"] = "add";
        //ViewState["PANO"] = null;
        txtPAuthority.Text = string.Empty;
        ddlUser.SelectedIndex = ddlCollege.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;
        btnShowReport.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PANO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PANO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            //updAdd.Visible = true;ddlDepartment txtPAuthority ddlUser
            btnAdd.Visible = false;
            btnShowReport.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
    //protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        int PANO = int.Parse(btnDelete.CommandArgument);
    //        CustomStatus cs = (CustomStatus)objPAuthority.DeletePassAuthority(PANO);
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //        {
    //            ViewState["action"] = null;
    //            BindListViewPAuthority();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }

    //}
    private void ShowDetails(Int32 PANO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAuthority.GetSingPassAuthority(PANO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PANO"] = PANO;
                string colno = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                if (colno != string.Empty)
                {
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                }

                FillDepartment();
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString()) > 0)
                {
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["UA_EMPDEPTNO"].ToString();
                }
                else
                {
                    ddlDepartment.SelectedValue = "0";
                }

                FillUser();
                string userno = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                if (userno != string.Empty)
                {
                    ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                }
                
                txtPAuthority.Text = ds.Tables[0].Rows[0]["PANAME"].ToString();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("Authority", "ESTB_PassAuthority.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
          //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@username=" + Session["username"].ToString();
           // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_CODE=0," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AttendanceReport.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillUser();
        FillDepartment();

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillUser();
    }
}
