//==========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE APPRAISAL               
// CREATION DATE :  10-06-2021
// CREATED BY    : TANU BALGOTE
// DESCRIPTION   :                            
// MODIFIED DATE :
// MODIFIED DESC :
//==========================================================================

using System;
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
using System.Data.SqlClient;

public partial class EMP_APPRAISAL_AppraisalPassingAuthorityPath : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AppraisalPassingAuthorityCon objPAuthority = new AppraisalPassingAuthorityCon();
    AppraisalPassingAuthorityEnt objobjEmpAuthority = new AppraisalPassingAuthorityEnt();

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
    //Page load
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
                Page.Title = Session["coll_name"].ToString();
               // CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                PnlAdd2.Visible = false;
                DivEmployeeList.Visible = false;
                pnlList.Visible = true;
                FillCollege();
                //FillStaffType();
                FillDepartment();
                FillPAuthority();
                BindListViewPAPath();
             
                objCommon.FillDropDownList(ddlsession, "APPRAISAL_SESSION_MASTER", "SESSION_ID", "SESSION_NAME", "IS_ACTIVE = 1", "SESSION_ID DESC");
            }
        }
    }

    //check authorization
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

    //this method used to fill dropdown value
    private void FillCollege()
    {


        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlCollegeGrid, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

            removeItem = ddlCollegeGrid.Items.FindByValue("0");
            ddlCollegeGrid.Items.Remove(removeItem);
        }


    }

    //this method is  used to fill department
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "D.SUBDEPT", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "D.SUBDEPT");
           // objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }   

    //this method is used to fill 1st Authority
    private void FillPAuthority()
    {
        try
        {

            if (ddlCollege.SelectedValue != "0")
            {
                objCommon.FillDropDownList(ddlPA01, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STATUS = 0", "PANAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        
        PnlAddNew.Visible = true;
        PnlAdd2.Visible = false; 
        DivEmployeeList.Visible = false;
        pnlList.Visible = true;
        trEmp.Visible = false;
        ddlDept.Enabled = true;
        ddlAppraisalType.Enabled = true;
        ddlCollege.Enabled = true;
       // BtnUpdate.Visible = false;
       // btnSave.Visible = true;

    }
    ////this method is used to clear controlls
    private void Clear()
    {
        ddlDept.SelectedIndex = ddlCollege.SelectedIndex = ddlStaffType.SelectedIndex = ddlCollegeGrid.SelectedIndex = 0;
        ddlPA01.SelectedIndex = 0;
        ddlPA02.SelectedIndex = 0;
        ddlPA03.SelectedIndex = 0;
        ddlPA04.SelectedIndex = 0;
        ddlPA05.SelectedIndex = 0;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        txtPAPath.Text = string.Empty;
        btnSave.Text = "Submit";
        btnCancel.Visible = true;
        //ddlCollege.Focus();
        //lvEmployees.DataSource = null;
        //lvEmployees.DataBind();
        //DivEmployeeList.Visible = false;
        //lvEmployees.Visible = false;
        trEmp.Visible = true;
        ViewState["action"] = "add";
        ViewState["PAPNO"] = null;
        ViewState["IDNO"] = null;
        trEmp.Visible = false;
        ddlAppraisalType.SelectedIndex = 0;

    }

    //this method is used to add new  path
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        PnlAdd2.Visible = true;
        pnlList.Visible = false;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        ViewState["action"] = "add";
        trEmp.Visible = false;
        PnlAddNew.Visible = false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            objobjEmpAuthority.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            objobjEmpAuthority.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            objobjEmpAuthority.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            objobjEmpAuthority.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            objobjEmpAuthority.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            objobjEmpAuthority.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            objobjEmpAuthority.PAPATH = Convert.ToString(txtPAPath.Text);
            objobjEmpAuthority.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objobjEmpAuthority.COLLEGE_CODE = Session["colcode"].ToString();
            objobjEmpAuthority.STNO = Convert.ToInt32(ddlStaffType.SelectedValue);
            objobjEmpAuthority.ATID = 0;// Convert.ToInt32(ddlAppraisalType.SelectedValue);
            objobjEmpAuthority.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objobjEmpAuthority.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            objobjEmpAuthority.SESSION_NO = Convert.ToInt32(ddlsession.SelectedValue);

            int count = 0; int count_record = 0;

            if (trEmp.Visible == true)
            {
                DataRow dr = dtEmpRecord.NewRow();
                int idno = Convert.ToInt32(ViewState["IDNO"]);
                dr["IDNO"] = idno;
                dtEmpRecord.Rows.Add(dr);
                dtEmpRecord.AcceptChanges();
                objobjEmpAuthority.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

            }
            else
            {
                foreach (ListViewItem lvitem in lvEmployees.Items)
                {
                    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;
                    if (chkIdNo.Checked == true)
                    {
                        count = count + 1;
                        objobjEmpAuthority.EMPNO = Convert.ToInt32(chkIdNo.ToolTip);
                        if (ViewState["action"].ToString().Equals("add"))
                        {

                            DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "PAPNO", "IDNO", "IDNO=" + objobjEmpAuthority.EMPNO + "AND STATUS<>1", "");

                            if (ds.Tables[0].Rows.Count <= 0)
                            {
                                count_record = count_record + 1;
                                DataRow dr = dtEmpRecord.NewRow();
                                dr["IDNO"] = objobjEmpAuthority.EMPNO;
                                dtEmpRecord.Rows.Add(dr);
                                dtEmpRecord.AcceptChanges();
                            }

                        }
                        else
                        {

                            objobjEmpAuthority.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

                            DataSet ds = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "PAPNO", "IDNO", "IDNO=" + objobjEmpAuthority.EMPNO + " AND PAPNO!=" + objobjEmpAuthority.PAPNO + "AND SESSION_NO=" + Convert.ToInt32(ddlsession.SelectedValue) + " ", "");

                            if (ds.Tables[0].Rows.Count <= 0)
                            {
                                count_record = count_record + 1;
                                DataRow dr = dtEmpRecord.NewRow();
                                dr["IDNO"] = objobjEmpAuthority.EMPNO;
                                dtEmpRecord.Rows.Add(dr);
                                dtEmpRecord.AcceptChanges();
                            }
                        }

                    }
                }
                if (count == 0)
                {
                    MessageBox("Please Select Atleast one employee");
                    return;
                }
                else if (count > 0 && count_record == 0)
                {
                    MessageBox("Sorry! Record Already exist");
                    return;
                }
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    objobjEmpAuthority.PAPNO = 0;
                    CustomStatus cs = (CustomStatus)objPAuthority.AddPAPath(objobjEmpAuthority, dtEmpRecord);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlList.Visible = true;
                        PnlAddNew.Visible = true;
                        PnlAdd2.Visible = false;
                        DivEmployeeList.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["PAPNO"] != null)
                    {

                        string ID = objCommon.LookUp("APPRAISAL_PASSING_AUTHORITY_PATH", "PAPNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSION_NO=" + Convert.ToInt32(ddlsession.SelectedValue));
                        if (ID != "")
                        {
                            objobjEmpAuthority.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                            string appraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ViewState["IDNO"]) + "AND SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue));
                            DataSet ds = objCommon.FillDropDown("APPRAISAL_EMPLOYEE", "*", "APPRAISAL_EMPLOYEE_ID", "PAPNO = " + objobjEmpAuthority.PAPNO, "");

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Modifying. It is in use.');", true);
                                return;
                            }
                            else
                                if (appraisalEmp_Id != "")
                                {
                                    //DataSet dsInfo = objPAuthority.GetEmployeeInformation(Convert.ToInt32(appraisalEmp_Id));
                                    //if (dsInfo.Tables[0].Rows.Count > 0)
                                    //{
                                    //int result = objPAuthority.DeleteEmployeeInfo(Convert.ToInt32(appraisalEmp_Id));
                                    //if (result > 0)
                                    //{
                                    //    CustomStatus CS = (CustomStatus)objPAuthority.AddPAPath(objobjEmpAuthority, dtEmpRecord);
                                    //    if (CS.Equals(CustomStatus.RecordSaved))
                                    //    {
                                    //        MessageBox("Record Updated Successfully");
                                    //        PnlAdd2.Visible = false;
                                    //        DivEmployeeList.Visible = false;
                                    //        pnlList.Visible = true;
                                    //        ViewState["action"] = null;
                                    //        ddlCollege.Enabled = true;
                                    //        ddlDept.Enabled = true;
                                    //        ddlAppraisalType.Enabled = true;
                                    //        // BtnUpdate.Visible = false;
                                    //        btnSave.Visible = true;
                                    //        Clear();
                                    //    }
                                    //}
                                }
                                else
                                {
                                    CustomStatus CS = (CustomStatus)objPAuthority.AddPAPath(objobjEmpAuthority, dtEmpRecord);
                                    if (CS.Equals(CustomStatus.RecordSaved))
                                    {
                                        MessageBox("Record Updated Successfully");

                                        pnlList.Visible = true;
                                        PnlAddNew.Visible = true;
                                        PnlAdd2.Visible = false;
                                        DivEmployeeList.Visible = false;
                                        pnlList.Visible = true;
                                        ViewState["action"] = null;
                                        ddlCollege.Enabled = true;
                                        ddlDept.Enabled = true;



                                        //  PnlAdd2.Visible = false;
                                        //  DivEmployeeList.Visible = false;
                                        //  pnlList.Visible = true;
                                        //  ViewState["action"] = null;
                                        //  ddlCollege.Enabled = true;
                                        //  ddlDept.Enabled = true;
                                        ////  ddlAppraisalType.Enabled = true;
                                        ////  BtnUpdate.Visible = false;
                                        btnSave.Visible = true;
                                        Clear();
                                    }
                                }
                        }

                    }

                }
                BindListViewPAPath();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method is used to Show msg
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }

    //this method is used to enable disable controlls
    private void EnableDisable(int index)
    {

        switch (index)
        {
            case 1:
                if (ddlPA01.SelectedIndex == 0)
                {
                    ddlPA02.SelectedIndex = 0;
                    ddlPA02.Enabled = false;
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");


                }
                else
                {

                    ddlPA02.Enabled = true;

                    string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")" + " AND STATUS = 0";

                    objCommon.FillDropDownList(ddlPA02, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }

                break;

            case 2:
                if (ddlPA02.SelectedIndex == 0)
                {
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }
                else
                {
                    ddlPA03.Enabled = true;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")" + " AND STATUS = 0";
                    objCommon.FillDropDownList(ddlPA03, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                break;
            case 3:


                if (ddlPA03.SelectedIndex == 0)
                {
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                else
                {
                    ddlPA04.Enabled = true;
                    //string swhere = " PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    //objCommon.FillDropDownList(ddlPA04, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                    string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")" + " AND STATUS = 0";
                    objCommon.FillDropDownList(ddlPA04, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }

                break;
            case 4:

                if (ddlPA04.SelectedIndex == 0)
                {
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }
                else
                {
                    ddlPA05.Enabled = true;
                    string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")" + " AND STATUS = 0";
                    objCommon.FillDropDownList(ddlPA05, "APPRAISAL_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString();
                }

                break;
            case 5:
                if (!(ddlPA04.SelectedIndex == 0))
                {
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString() + "->" + ddlPA05.SelectedItem.ToString();
                }
                break;

        }

    }


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment();
        DivEmployeeList.Visible = true;
        FillEmployee();
        ddlAppraisalType.SelectedIndex = 0;
    }

    //code not in use (future in use)
    protected void ddlAppraisalType_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  FillEmployee();
        //objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "", "");
        DivEmployeeList.Visible = true;
        lvEmployees.Visible = true;
        trEmp.Visible = false;
    }
    // first authority
    protected void ddlPA01_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " EmpAppraisal_AppraisalPassingAuthorityPath.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //2nd authority
    protected void ddlPA02_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " EmpAppraisal_AppraisalPassingAuthorityPath.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //3rd authority
    protected void ddlPA03_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " EmpAppraisal_AppraisalPassingAuthorityPath.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    //4th authority
    protected void ddlPA04_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " EmpAppraisal_AppraisalPassingAuthorityPath.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //5th authority
    protected void ddlPA05_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, " EmpAppraisal_AppraisalPassingAuthorityPath.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method use for fill employee
    private void FillEmployee()
    {
        //DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO,ISNULL(PFILENO,'') AS PFILENO", "isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND WORKING_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO=" + Convert.ToInt32(ddlStaffType.SelectedValue) + "", "");
        DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO,ISNULL(PFILENO,'') AS PFILENO", "isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "FNAME");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            lvEmployees.DataSource = dsEmp;
            lvEmployees.DataBind();
            DivEmployeeList.Visible = true;
            lvEmployees.Visible = true;
        }
        else
        {
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();

        }

        //objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + " ", "");

    }


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDepartment();
        FillPAuthority();
        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        DivEmployeeList.Visible = false;
    }


    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain  
    //    BindListViewPAPath();
    //}

    protected void ddlCollegeGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewPAPath();
    }

    //this method used to bind record
    protected void BindListViewPAPath()
    {
        try
        {

            DataSet ds = objPAuthority.GetAllPAPath(Convert.ToInt32(ddlCollegeGrid.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                rptPathList.DataSource = null;
                rptPathList.DataBind();
                rptPathList.Visible = false;
                //dpPager.Visible = false;
            }
            else
            {
                rptPathList.DataSource = ds;
                rptPathList.DataBind();
                rptPathList.Visible = true;
                //dpPager.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PAPNO = int.Parse(btnEdit.CommandArgument);

            DataSet ds = objCommon.FillDropDown("APPRAISAL_EMPLOYEE", "PAPNO", "APPRAISAL_EMPLOYEE_ID", "PAPNO = " + PAPNO, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Update. It is in use.');", true);
                return;
            }
            else
            {

                ShowDetails(PAPNO);
                PnlAddNew.Visible = false;
                ViewState["action"] = "edit";
                PnlAdd2.Visible = true;
                pnlList.Visible = false;
                //btnSave.Text = "Update";
                //btnSave.ToolTip = "Update the Record";
                btnSave.Visible = true;
                // BtnUpdate.Visible = true;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //this method show data for modification
    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAuthority.GetSinglePAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;

                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlCollege.Enabled = false;
                ddlStaffType.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                FillDepartment();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                ddlDept.Enabled = false;
              //  ddlAppraisalType.SelectedValue = ds.Tables[0].Rows[0]["ATID"].ToString();
               // ddlAppraisalType.Enabled = false;
                FillPAuthority();
                //FillEmployee();
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();

                trEmp.Visible = true;
                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                FillPAuthority();
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["PAN02"].ToString().Trim().Equals("0")))
                {
                    ddlPA02.SelectedValue = ds.Tables[0].Rows[0]["PAN02"].ToString();
                    this.EnableDisable(2);
                    ddlPA02.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN03"].ToString().Trim().Equals("0")))
                {
                    ddlPA03.SelectedValue = ds.Tables[0].Rows[0]["PAN03"].ToString();
                    this.EnableDisable(3);
                    ddlPA03.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN04"].ToString().Trim().Equals("0")))
                {
                    ddlPA04.SelectedValue = ds.Tables[0].Rows[0]["PAN04"].ToString();
                    this.EnableDisable(4);
                    ddlPA04.Enabled = true;
                }
                if (!(ds.Tables[0].Rows[0]["PAN05"].ToString().Trim().Equals("0")))
                {
                    ddlPA05.SelectedValue = ds.Tables[0].Rows[0]["PAN05"].ToString();
                    this.EnableDisable(5);
                    ddlPA05.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int PAPNO = int.Parse(btnDelete.CommandArgument);
         //   DataSet ds = objCommon.FillDropDown("APPRAISAL_EMPLOYEE", "PAPNO", "APPRAISAL_EMPLOYEE_ID", "PAPNO=" +PAPNO + " ", "");
            //DataSet ds = objCommon.FillDropDown("APPRAISAL_EMPLOYEE", "*", "", "( PAN01 = " + PANO + " OR PAN02 = " + PANO + " OR PAN03 = " + PANO + " OR PAN04 = " + PANO + " OR PAN05 = " + PANO + ")", "");
            DataSet ds = objCommon.FillDropDown("APPRAISAL_EMPLOYEE", "PAPNO", "APPRAISAL_EMPLOYEE_ID", "PAPNO = " + PAPNO, "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Authority Can Not Be Deleted. It is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objPAuthority.DeletePAPath(PAPNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = null;
                    BindListViewPAPath();
                }
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EmpAppraisal_AppraisalPassingAuthorityPath.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void FillStaffType()
    //{
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlStaffType, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "stno <> 0", "STAFFTYPE");


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, ".FillStaffType ->" + ex.Message + "  " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
}