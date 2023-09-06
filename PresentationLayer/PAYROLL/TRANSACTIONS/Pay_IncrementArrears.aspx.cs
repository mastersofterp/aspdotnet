using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_TRANSACTIONS_Pay_IncrementArrears : System.Web.UI.Page
{
    Common objCommon = new Common();
    PayArrearsController objpay = new PayArrearsController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //For displaying user friendly messages
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));


        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                ViewState["action"] = "add";
                //function to fill dropdownlists
                FillDropdown();
                //function to Bind Listview with Payheads

                DateTime fdate = System.DateTime.Now;
                txtCPTFromDate.Text = fdate.ToShortDateString();
                txtCPTToDate.Text = fdate.ToShortDateString();
                txtGovDt.Text = fdate.ToShortDateString();
                txtOffDt.Text = fdate.ToShortDateString();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Arrears.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Arrears.aspx");
        }
    }

    //Function to fill dropdownlist for college type
    protected void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlCollegeType, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        //objCommon.FillDropDownList(ddlRule, "PAYROLL_RULE", "RULENO", "PAYRULE", "RULENO>0", "RULENO");
        //objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE ='C'", "");
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        PayArrearsEntity objPayroll = new PayArrearsEntity();

        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {

                objPayroll.STAFFNO = Convert.ToInt32(ddlCollegeType.SelectedValue);
                objPayroll.AFRM = Convert.ToDateTime(txtCPTFromDate.Text);
                objPayroll.ATO = Convert.ToDateTime(txtCPTToDate.Text);
                objPayroll.PAYHEAD = "";
                objPayroll.Per = 0.00;
                objPayroll.Payrule = "";
                objPayroll.GOVORDNO = txtGovOrdNo.Text;
                objPayroll.GOVORDDT = Convert.ToDateTime(txtGovDt.Text);
                objPayroll.OFFORDNO = txtOffOrdNo.Text;
                objPayroll.OFFORDDT = Convert.ToDateTime(txtOffDt.Text);
                objPayroll.REMARK = txtRemark.Text;
                objPayroll.COLLEGE_CODE = Session["colcode"].ToString();
                objPayroll.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);

                //add
                if (ViewState["action"].Equals("add"))
                {
  
                    int chkarnorecord = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "COUNT(ARNO)", "COLLEGE_NO=" + ddlCollege.SelectedValue + " AND COLLEGENO=" + ddlCollegeType.SelectedValue + " AND PAYHEAD='"
                                        + "' AND (AFRM IN(CONVERT(DATETIME,'" +
                                        Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'"
                                        + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103)) OR ATO IN (CONVERT(DATETIME,'"
                                        + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'" +
                                        Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103)))"));

                    if (chkarnorecord == 0)
                    {

                        CustomStatus cs = (CustomStatus)objpay.AddPayArrears(objPayroll);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            int arno = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "ISNULL(MAX(ARNO),0)", ""));

                            MessageBox("Arrears Calculated Sucessfully");

                            int i = 0;
                            int idno = 0;
                            string idno1 = string.Empty;
                           
                            foreach (ListViewDataItem itm in lvEmployees.Items)
                            {
                                CheckBox chk = itm.FindControl("chkRow") as CheckBox;
                                HiddenField hdnf = itm.FindControl("hidStudentId") as HiddenField;
                                idno = Convert.ToInt32(hdnf.Value);
                                if (chk.Checked == true && (chk.Enabled == true))
                                {
                                    idno1 = idno1 + idno.ToString() + ",";
                                }
                            }

                            idno1 = idno1.Substring(0, idno1.Length - 1);

                            CustomStatus cs1 = (CustomStatus)objpay.IncrementCalculateArrears(txtCPTFromDate.Text, txtCPTToDate.Text, Convert.ToInt32(ddlCollegeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), arno, idno1);
                        }
                    }
                    else
                    {
                        int arnocheck = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "ARNO", "COLLEGE_NO=" + ddlCollege.SelectedValue + " AND COLLEGENO=" + ddlCollegeType.SelectedValue + " AND PAYHEAD='"
                                   + "' AND (AFRM IN(CONVERT(DATETIME,'" +
                                   Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'"
                                   + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103)) OR ATO IN (CONVERT(DATETIME,'"
                                   + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'" +
                                   Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103)))"));

                        int i = 0;
                        int idno = 0;
                        string idno1 = string.Empty;

                        DataSet ds;
                        if (ddlEmployee.SelectedValue != "0")
                            ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "idno", "fname", "IDNO=" + ddlEmployee.SelectedValue, "");
                        else
                            ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "idno", "fname", "STAFFNO=" + ddlCollegeType.SelectedValue, "");

                        foreach (ListViewDataItem itm in lvEmployees.Items)
                        {
                            CheckBox chk = itm.FindControl("chkRow") as CheckBox;
                            HiddenField hdnf = itm.FindControl("hidStudentId") as HiddenField;
                            idno = Convert.ToInt32(hdnf.Value);
                            if (chk.Checked == true && (chk.Enabled == true))
                            {
                                idno1 = idno1 + idno.ToString() + ",";
                            }
                        }
                        idno1 = idno1.Substring(0, idno1.Length - 1);

                        //CustomStatus cs = (CustomStatus)objpay.DeleteArrearsDiffRecord(arnocheck);
                        if (arnocheck > 0)
                        {
                            MessageBox("Arrears Already calculated, To recalculate Remove it from Arrear Diff!");
                        }
                        else
                        {
                            CustomStatus cs1 = (CustomStatus)objpay.IncrementCalculateArrears(txtCPTFromDate.Text, txtCPTToDate.Text, Convert.ToInt32(ddlCollegeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), arnocheck, idno1);
                           // clear();
                        }
                    }
                }


                //update
                else
                {

                    //objSanc.SPNO = Convert.ToInt32(ViewState["lblSPNO"]);

                    //CustomStatus cs = (CustomStatus)objSancCntrl.UpdateSanctionedPost(objSanc);
                    //if (cs.Equals(CustomStatus.RecordUpdated))
                    //{
                    //    BindListViewSanctioned();
                    //    Clear();
                    //    objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Updated, Common.MessageType.Success);

                    //}
                    //else
                    //    objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                }
            }
        }
        catch (Exception ex)
        {
            //objCommon = new Common();
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }
    
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void ddlCollegeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND STAFFNO=" + ddlCollegeType.SelectedValue + " AND PM.PSTATUS='Y' AND EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        ddlCollegeType.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        //ddlPayhead.SelectedIndex = 0;
        //ddlRule.SelectedIndex = 0;
        txtCPTFromDate.Text = txtCPTToDate.Text = txtGovDt.Text = txtGovOrdNo.Text = txtOffDt.Text = txtOffOrdNo.Text = txtRemark.Text = string.Empty;
    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "IDNO", "CASE WHEN PFILENO IS NULL THEN '-' ELSE PFILENO END PFILENO,FNAME+' '+MNAME+' '+LNAME AS NAME,ISNULL(LOGIN_STATUS,0) AS LOGIN_STATUS", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STAFFNO=" + Convert.ToInt32(ddlCollegeType.SelectedValue), "IDNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvEmployees.DataSource = ds.Tables[0];
            lvEmployees.DataBind();
            //pnllistview.Visible = true;
            lvEmployees.Visible = true;
            btnCalculate.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("No record found!", this.Page);
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();
            //pnllistview.Visible = false;
            lvEmployees.Visible = false;
            btnCalculate.Visible = false;
        }

        string s = "qwert100ft34";
        int sum = 0;
        var x = s.ToCharArray();
        Console.WriteLine(x);
        for (int I = 0; I <= x.Length - 1; I++)
        {
            if (x[I] > '0' && x[I] <= '9')
            {
                sum += x[I] - '0';
            }
        }
        Console.WriteLine(sum);
    }
}