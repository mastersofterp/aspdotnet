using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_TRANSACTIONS_Pay_EmployeeResignationAccept : System.Web.UI.Page
{
   
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();
    int collegeno;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //   ShowEmpDetails();   
                ViewState["action"] = "add";

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() != "1")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION

                    // DivSerach.Visible = true;
                    // pnlId.Visible = true;
                    //ShowEmpDetails(Convert.ToInt32(Session["idno"]));
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    pnlId.Visible = false;
                    BindGridData();
                }
            }
        }
        else
        {

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_AssestAlottment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_AssestAlottment.aspx");
        }
    }
    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
            DataSet ds = null;
            ds = objECC.GetAllEmpResignationDetail(Convert.ToInt32(ViewState["IDNO"]));

            if (ds.Tables[1].Rows.Count > 0)
            {
                objCommon.DisplayMessage("Resignation is Accepted !!", this.Page);
                return;
            }


            if (txtresignationremark.Text == "" && txtresigntiondate.Text == "")
            {
                string MSG = "Please Select or Enter Proper Details!!";
                MessageBox(MSG);
                return;
            }
            else
            {
                if (ViewState["action"] != null)
                {
                    if (txtresigntiondate.Text != "") //yes
                    {

                        objEM.RESIGNATIONDATE = Convert.ToDateTime(txtresigntiondate.Text);

                    }
                    else //no
                    {

                        //objEM.RESIGNATIONDATE = DBNull.Value;
                    }
                    if (txtresignationremark.Text != "")
                    {
                        objEM.RESIGNATIONREMARK = txtresignationremark.Text.Trim();
                    }
                    else
                    {
                        objEM.RESIGNATIONREMARK = "";
                    }

                    objEM.IdNo = Convert.ToInt32(lblIDNo.Text.Trim());
                    objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text.Trim());
                    //HERE UPDATE THE EMPLOYEE ASSET DETAILE
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objEM.REGSTATUS = status;
                        objEM.RESIGNATIONEMPID = EmpREsignationId;
                        cs = Convert.ToInt32(objECC.UpdateEmployeeResignation(objEM));

                        if (cs == 1)
                        {
                            string MSG = "Records Updated Sucessfully";
                            MessageBox(MSG);
                            BindAssetAllotmentDetails();
                            txtresignationremark.Text = "";
                            txtresigntiondate.Text = "";
                        }
                        else
                        {

                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                    else
                    {
                        cs = Convert.ToInt32(objECC.SaveEmployeeResignation(objEM));

                        if (cs == 1)
                        {
                            string MSG = "Records Saved Sucessfully";
                            MessageBox(MSG);
                            BindAssetAllotmentDetails();
                            txtresignationremark.Text = "";
                            txtresigntiondate.Text = "";
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void BtnAccept_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
                objEM.IdNo = Convert.ToInt32(lblIDNo.Text);
                objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text);
                objEM.RESIGNATIONDATE = Convert.ToDateTime(txtresigntiondate.Text);

                if (!txtCurrRelevingDate.Text.Trim().Equals(string.Empty)) 
                    objEM.REGRELEVINGDATE = Convert.ToDateTime(txtCurrRelevingDate.Text);
           
               
                objEM.RESIGNATIONREMARK = txtresignationremark.Text.Trim();
                objEM.REGSTATUS = true;
                objEM.RESIGNATIONEMPID = Convert.ToInt32(hdnempregid.Value);
                objEM.PHRRESIGNATIONREMARK = txtHrRemark.Text.Trim();

                if (txtNoticePeriod.Text == "" || txtNoticePeriod.Text == "0")
                {
                    objEM.PNOTICEPERIOD = 0;
                }
                else
                {
                    objEM.PNOTICEPERIOD = Convert.ToInt32(txtNoticePeriod.Text);
                }

                objEM.ISNODUES = true;
                objEM.COMMANDTYPE = "ACCEPT";
                objEM.EXITTYPEID = Convert.ToInt32(ddlExitType.SelectedValue);
                if(txtfinalAmount.Text != "")
                {
                    objEM.FINALAMOUNT = Convert.ToDecimal(txtfinalAmount.Text.Trim());
                }
               else
                {
                objEM.FINALAMOUNT = 0;
                }
                cs = Convert.ToInt32(objECC.UpdateEmployeeResignationUpdated(objEM));
                if (cs == 1)
                {
                    string MSG = "Resignation Accepted Sucessfully";
                    MessageBox(MSG);
                    DivSerach.Visible = true;
                    pnlId.Visible = false;
                    BindGridData();
                }
                else
                {
                    string MSG = "Records Saved Failed";
                    MessageBox(MSG);
                }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
                objEM.IdNo = Convert.ToInt32(lblIDNo.Text);
                objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text);
                
            objEM.RESIGNATIONDATE = Convert.ToDateTime(txtresigntiondate.Text);
            if (!txtCurrRelevingDate.Text.Trim().Equals(string.Empty)) objEM.REGRELEVINGDATE = Convert.ToDateTime(txtCurrRelevingDate.Text);
           
               
                objEM.RESIGNATIONREMARK = txtresignationremark.Text.Trim();
                objEM.REGSTATUS = false;
                objEM.RESIGNATIONEMPID = Convert.ToInt32(hdnempregid.Value);
                objEM.PHRRESIGNATIONREMARK = txtHrRemark.Text.Trim();
                objEM.PNOTICEPERIOD = 0;
                objEM.ISNODUES = false;
                objEM.EXITTYPEID = Convert.ToInt32(ddlExitType.SelectedValue);
                objEM.COMMANDTYPE = "REJECT";

                cs = Convert.ToInt32(objECC.UpdateEmployeeResignationUpdated(objEM));
                if (cs == 1)
                {
                    string MSG = "Resignation Rejected Sucessfully";
                    MessageBox(MSG);
                    DivSerach.Visible = true;
                    pnlId.Visible = false;
                    BindGridData();
                }
                else
                {
                    string MSG = "Records Saved Failed";
                    MessageBox(MSG);
                }

               
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void BtnReleving_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
            objEM.IdNo = Convert.ToInt32(lblIDNo.Text);
            objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text);
            objEM.RESIGNATIONDATE = Convert.ToDateTime(txtresigntiondate.Text);
           
            if (!txtCurrRelevingDate.Text.Trim().Equals(string.Empty)) objEM.REGRELEVINGDATE = Convert.ToDateTime(txtCurrRelevingDate.Text);
           
            objEM.RESIGNATIONREMARK = txtresignationremark.Text.Trim();
            objEM.REGSTATUS = false;
            objEM.RESIGNATIONEMPID = Convert.ToInt32(hdnempregid.Value);
            objEM.PHRRESIGNATIONREMARK = txtHrRemark.Text.Trim();
            objEM.PNOTICEPERIOD = 0;
            objEM.ISNODUES = false;
            objEM.COMMANDTYPE = "RELEVING";
            objEM.EXITTYPEID = Convert.ToInt32(ddlExitType.SelectedValue);

            cs = Convert.ToInt32(objECC.UpdateEmployeeResignationUpdated(objEM));
            if (cs == 1)
            {
                string MSG = "Employee Releved Sucessfully !!";
                MessageBox(MSG);
                DivSerach.Visible = true;
                pnlId.Visible = false;
                BindGridData();
            }
            else
            {
                string MSG = "Records Saved Failed";
                MessageBox(MSG);
            }


            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.Btnsubmit_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void Search_Click(object sender, System.EventArgs e)
    {
        string searchtext = string.Empty;
        string category = string.Empty;

        if (rdoSelection.SelectedValue == "IDNO")
        {
            category = "IDNO";
        }
        else if (rdoSelection.SelectedValue == "NAME")
        {
            category = "NAME";
        }
        else if (rdoSelection.SelectedValue == "PFILENO")
        {
            category = "PFILENO";
        }
        else if (rdoSelection.SelectedValue == "EMPLOYEEID")
        {
            category = "EMPLOYEEID";
        }

        searchtext = txtSearch.Text.ToString();

        DataSet ds = objECC.GetAllEmpResignationDetailALL();
        if (ds.Tables[0].Rows.Count > 0)
        {
            rpt_RegEmp.DataSource = ds.Tables[0];
            rpt_RegEmp.DataBind();
        }
    }

    private void BindGridData()
    {
        DataSet ds = objECC.GetAllEmpResignationDetailALL();
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ListView1.DataSource = ds.Tables[0];
            //ListView1.DataBind();

            rpt_RegEmp.DataSource = ds.Tables[0];
            rpt_RegEmp.DataBind();
        }
    }


    protected void btnCanceModal_Click(object sender, System.EventArgs e)
    {

    }
    protected void lnkId_Click(object sender, System.EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();

        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void ShowEmpDetails(int idno)
    {
        EmpCreateController objECC = new EmpCreateController();

        DataTableReader dtr = objECC.ShowEmpDetailsForResignation(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {

                pnlId.Visible = true;
                ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
                txtcollegeno.Text = dtr["COLLEGE_NO"].ToString();
              //  lblEmpcode.Text = dtr["seq_no"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();
                lblRegStatus.Text = dtr["REG_STATUS_DETAILS"].ToString();
                lblrelievedstatus.Text = dtr["REG_STATUS_RELIEVED"].ToString();
                lblEmployeeNo.Text = dtr["EmployeeId"].ToString();
                txtresigntiondate.Text = Convert.ToDateTime(dtr["RESIGNATIONDATE"]).ToString("dd/MM/yyyy");
                txtcollegeno.Text = dtr["COLLEGE_NO"].ToString();
                hdnempregid.Value = dtr["EMPRESIGNATIONID"].ToString();
                txtNoticePeriod.Text = dtr["REG_NOTICE_PERIOD"].ToString(); 
                txtresignationremark.Text = dtr["RESIGNATIONREMARK"].ToString();
                txtHrRemark.Text = dtr["HR_RESIGNATION_REMARK"].ToString();
                txtNoDayAbs.Text = dtr["AbsentDays"].ToString();
                ddlExitType.SelectedValue = dtr["ExitTypeId"].ToString();
             //   txtCurrRelevingDate.Text = Convert.ToDateTime(dtr["CurrentReleingDate"]).ToString("dd/MM/yyyy"); //dtr["CurrentReleingDate"].ToString();
                if (Convert.ToInt32(dtr["REG_STATUS"]) == 1)
                {
                    lblfinalamount.Visible = true;
                    txtfinalAmount.Visible = true;
                    
                }
                else if(Convert.ToInt32(dtr["REG_STATUS"]) == 0)
                {
                    lblfinalamount.Visible = false;
                    txtfinalAmount.Visible = false;
                }
                if (dtr["CurrentReleingDate"] != DBNull.Value)
                {
                    txtCurrRelevingDate.Text = Convert.ToDateTime(dtr["CurrentReleingDate"]).ToString("dd/MM/yyyy");
                }
                else
                {
                   // txtCurrRelevingDate.Text = "";
                    DateTime Resigndate = Convert.ToDateTime(txtresigntiondate.Text);
                    int month = Convert.ToInt32(txtNoticePeriod.Text);
                    DateTime date = Resigndate.AddMonths(month);
                    txtCurrRelevingDate.Text = date.ToString("dd/MM/yyyy");
                }

                if (dtr["REG_RELEVING_DATE"] != DBNull.Value)
                {
                    txtRelevingDate.Text = Convert.ToDateTime(dtr["REG_RELEVING_DATE"]).ToString("dd/MM/yyyy");
                }
                else
                {
                   // txtRelevingDate.Text = "";
                    DateTime Resigndate = Convert.ToDateTime(txtresigntiondate.Text);
                    int month = Convert.ToInt32(txtNoticePeriod.Text);
                    DateTime date = Resigndate.AddMonths(month);
                    txtRelevingDate.Text = date.ToString("dd/MM/yyyy");
                }

                if (dtr["DOJ"] != "")
                {
                    lblDOJ.Text = Convert.ToDateTime(dtr["DOJ"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    lblDOJ.Text = "";
                }
                
                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=emp";
                imgPhoto.Visible = true;
            }
            
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }
    public void BindAssetAllotmentDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpResignationDetail(Convert.ToInt32(ViewState["IDNO"]));
            }
            else
            {
                ds = objECC.GetAllEmpResignationDetail(Convert.ToInt32(ViewState["IDNO"]));
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                objCommon.DisplayMessage("Resignation is Accepted !!", this.Page);
                return;
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmp.DataSource = ds.Tables[0];
                lvEmp.DataBind();
            }
            else
            {
                lvEmp.DataSource = null;
                lvEmp.DataBind();
            }


            foreach (ListViewDataItem items in lvEmp.Items)
            {
                ImageButton imgbtn = items.FindControl("btneditasset") as ImageButton;
                HiddenField hdnisappStatus = items.FindControl("hdnisappStatus") as HiddenField;

                if (Convert.ToString(hdnisappStatus.Value) == "<SPAN STYLE=\"COLOR:RED;FONT-WEIGHT:BOLD\">PENDING</SPAN>")
                {
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
        else
        {

            return;
        }
    }
    static int EmpREsignationId = 0;
    bool status;
    string strstatus;
    protected void btneditasset_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            EmpREsignationId = int.Parse(btnEdit.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetAllEmpResignationIDWISE(EmpREsignationId);
            if (dsdues.Tables[0].Rows.Count > 0)
            {
                if (dsdues.Tables[0].Rows[0]["REG_STATUS"].ToString() == "")
                {
                    // status = null; //No
                    strstatus = dsdues.Tables[0].Rows[0]["REG_STATUS_DETAILS"].ToString();
                }
                else
                {
                    if (dsdues.Tables[0].Rows[0]["REG_STATUS"].ToString() == "0")
                    {
                        status = false; //No
                        strstatus = dsdues.Tables[0].Rows[0]["REG_STATUS_DETAILS"].ToString();
                    }
                    else
                    {
                        status = true; //No
                        strstatus = dsdues.Tables[0].Rows[0]["REG_STATUS_DETAILS"].ToString();
                    }

                }
                txtresignationremark.Text = dsdues.Tables[0].Rows[0]["RESIGNATIONREMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
                txtresigntiondate.Text = dsdues.Tables[0].Rows[0]["RESIGNATIONDATE"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["RESIGNATIONDATE"].ToString();

            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_EmployeeResignation.aspx.btneditasset_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }




    }
    protected void txtNoticePeriod_TextChanged(object sender, System.EventArgs e)
    {
        DateTime Resigndate = Convert.ToDateTime(txtresigntiondate.Text);
        int month = Convert.ToInt32(txtNoticePeriod.Text);
        DateTime date = Resigndate.AddMonths(month);
        txtRelevingDate.Text = date.ToString("dd/MM/yyyy");
    }
}