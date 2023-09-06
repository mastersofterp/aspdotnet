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


public partial class PAYROLL_TRANSACTIONS_Pay_EmployeeResignation : System.Web.UI.Page
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
                
                string CollegeNo = objCommon.LookUp("PAYROLL_EMPMAS", "COLLEGE_NO", "IDNO=" + Convert.ToInt32(Session["idno"]));

                ViewState["usertype"] = ua_type;
                txtresigntiondate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (ViewState["usertype"].ToString() != "1")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    
                    objCommon.FillDropDownList(ddlAuthoUser, "[dbo].[PAYROLL_RESIGNATION_PASSING_AUTHORITY] A", "A.REGPASSID", "A.PANAME", "A.COLLEGE_NO=" + CollegeNo + " AND PASSTYPE=2", "");
                    DivSerach.Visible = false;
                    pnlId.Visible = true;
                    ShowEmpDetails(Convert.ToInt32(Session["idno"]));
                    txtresigntiondate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtresigntiondate.Enabled = false;
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //pnlId.Visible = true;
                   
                    txtresigntiondate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtresigntiondate.Enabled = false;
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeResignation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeResignation.aspx");
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


            if (txtresignationremark.Text == "" && txtresigntiondate.Text  == "")
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
                    
                    //objEM.IdNo = Convert.ToInt32(lblIDNo.Text.Trim());
                    objEM.IdNo = Convert.ToInt32(ViewState["IDNO"]);
                    objEM.COLLEGE_NO = Convert.ToInt32(txtcollegeno.Text.Trim());
                    objEM.EXITTYPEID = Convert.ToInt32(ddlExitType.SelectedValue);
                    objEM.PNOTICEPERIOD = Convert.ToInt32(txtNoticePrirod.Text.Trim());
                    objEM.PASSTYPE = Convert.ToInt32(ddlAuthoUser.SelectedValue);
                    //HERE UPDATE THE EMPLOYEE ASSET DETAILE
                    if (ViewState["action"].ToString().Equals("edit"))
                    {                       
                        objEM.RESIGNATIONEMPID = EmpREsignationId;
                        cs = Convert.ToInt32(objECC.UpdateEmployeeResignation(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Updated Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeResignationDetails();
                            txtresignationremark.Text = "";
                            txtresigntiondate.Text = "";
                            ddlExitType.SelectedIndex = 0;
                            ddlAuthoUser.SelectedIndex = 0;
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                    else
                    {
                        foreach (ListViewDataItem items in lvEmp.Items)
                        {
                            Label lblrestatus = items.FindControl("lblresignation_STATUS") as Label;
                            if (lblrestatus.Text == "")
                            {
                                string MSG = "Resignation already Pending";
                                MessageBox(MSG);
                                return;
                            }
                        }

                        cs = Convert.ToInt32(objECC.SaveEmployeeResignation(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Saved Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeResignationDetails();
                            txtresignationremark.Text = "";
                            txtresigntiondate.Text = "";
                            ddlExitType.SelectedIndex = 0;
                            ddlAuthoUser.SelectedIndex = 0;
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

        DataTable dt = objECC.RetrieveEmpDetailsForaSsetAllotment(searchtext, category);
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
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

        DataTableReader dtr = objECC.ShowEmpDetails(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {

                pnlId.Visible = true;
                ViewState["IDNO"]  = dtr["idno"].ToString();
                lblIDNo.Text = dtr["EmployeeId"].ToString();
                txtcollegeno.Text = dtr["COLLEGE_NO"].ToString();
               // lblEmpcode.Text = dtr["seq_no"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                objCommon.FillDropDownList(ddlAuthoUser, "[dbo].[PAYROLL_RESIGNATION_PASSING_AUTHORITY] A", "A.REGPASSID", "A.PANAME", "A.COLLEGE_NO=" + txtcollegeno.Text + "", "");
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();
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
                txtNoticePrirod.Text = dtr["Notice_Period"].ToString();


            }
            BindEmployeeResignationDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }
    public void BindEmployeeResignationDetails()
    {
        bool status;
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
                //return;
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
               //status= Convert.ToBoolean(ds.Tables[0].Rows[0]["REG_STATUS"]);
               //if (status == true)
               //{
               //    string MSG = "Resignation is already Approved";
               //    MessageBox(MSG);
               //}
            foreach (ListViewDataItem items in lvEmp.Items)
            {
                Label lblrestatus = items.FindControl("lblresignation_STATUS") as Label;
                HiddenField hdnRegDate = items.FindControl("hdnRegDate") as HiddenField;
                HiddenField hdnNotice = items.FindControl("hdnNotice") as HiddenField;
                HiddenField HdnRegRemark = items.FindControl("HdnRegRemark") as HiddenField;
                HiddenField Hdnexittypeid=items.FindControl("hdnexittypeid") as HiddenField;
                HiddenField hdnREGPASSID = items.FindControl("hdnREGPASSID") as HiddenField;
                
                if (lblrestatus.Text != "")
                {
                    if (Convert.ToBoolean(lblrestatus.Text) == true)
                    {
                        txtresigntiondate.Text = hdnRegDate.Value.ToString();
                        txtresignationremark.Text = HdnRegRemark.Value.ToString();
                        txtNoticePrirod.Text = hdnNotice.Value.ToString();
                        ddlExitType.SelectedValue = Hdnexittypeid.Value;
                        ddlAuthoUser.SelectedValue = hdnREGPASSID.Value;
                        Btnsubmit.Enabled = false;
                    }

                }
                else if(lblrestatus.Text == "")
                {
                        txtresigntiondate.Text = hdnRegDate.Value.ToString();
                        txtresignationremark.Text = HdnRegRemark.Value.ToString();
                        txtNoticePrirod.Text = hdnNotice.Value.ToString();
                        ddlExitType.SelectedValue = Hdnexittypeid.Value;
                        ddlAuthoUser.SelectedValue = hdnREGPASSID.Value;
                        Btnsubmit.Enabled = true;
                }
            }
            foreach (ListViewDataItem items in lvEmp.Items)
            {
                ImageButton imgbtn = items.FindControl("btneditasset") as ImageButton;
                HiddenField hdnisappStatus = items.FindControl("hdnisappStatus") as HiddenField;

                if (Convert.ToString(hdnisappStatus.Value) == "<SPAN STYLE=\"COLOR:Blue;FONT-WEIGHT:BOLD\">PENDING</SPAN>")
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
                
                txtresignationremark.Text = dsdues.Tables[0].Rows[0]["RESIGNATIONREMARK"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["RESIGNATIONREMARK"].ToString();
                txtresigntiondate.Text = dsdues.Tables[0].Rows[0]["RESIGNATIONDATE"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["RESIGNATIONDATE"].ToString();
                ddlExitType.SelectedValue = dsdues.Tables[0].Rows[0]["ExitTypeId"].ToString();
                ddlAuthoUser.SelectedValue = dsdues.Tables[0].Rows[0]["REGPASSID"].ToString();
                txtNoticePrirod.Text = dsdues.Tables[0].Rows[0]["RESIGNATIONREMARK"] == DBNull.Value? " ":dsdues.Tables[0].Rows[0]["REG_NOTICE_PERIOD"].ToString();
                txtresigntiondate.Enabled = false;
               
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
       
    }