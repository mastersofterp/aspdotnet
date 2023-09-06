using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_Pay_Transaction_Pay_Arrears : System.Web.UI.Page
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
        objCommon.FillDropDownList(ddlRule, "PAYROLL_RULE", "RULENO", "PAYRULE", "RULENO>0", "RULENO");
        objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE ='C'", "");
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        PayArrearsEntity objPayroll = new PayArrearsEntity();

        //string monyr = txtCPTFromDate.Text;
        //int count = Convert.ToInt32(objCommon.LookUp("PAYROLL_SALFILE", "COUNT(*)", "MONYEAR=" + "'" + monyr + "'" + "and collegeno =" + ddlCollegeType.SelectedValue));
        //if (count == 0)
        //{
        //    MessageBox("Salary is not Processed for" + monyr);
           
        //}

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyy-dd-MM")));
        //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
        Fdate = Fdate.Substring(0, 10);
        string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyy-dd-MM")));
        Tdate = Tdate.Substring(0, 10);

        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {

                objPayroll.STAFFNO = Convert.ToInt32 (ddlCollegeType.SelectedValue);
                objPayroll.AFRM = Convert.ToDateTime (txtCPTFromDate.Text);
                objPayroll.ATO = Convert.ToDateTime(txtCPTToDate.Text);
                objPayroll.PAYHEAD = ddlPayhead.SelectedValue;
                objPayroll.Per = Convert.ToDouble (txtCurRate.Text);
                objPayroll.Payrule = ddlRule.SelectedValue;
                objPayroll.GOVORDNO = txtGovOrdNo.Text;
                objPayroll.GOVORDDT = Convert.ToDateTime (txtGovDt.Text);
                objPayroll.OFFORDNO = txtOffOrdNo.Text;
                objPayroll.OFFORDDT = Convert.ToDateTime(txtOffDt.Text);
                objPayroll.REMARK = txtRemark.Text;
                objPayroll.COLLEGE_CODE = Session["colcode"].ToString();
                objPayroll.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);

                //add
                if (ViewState["action"].Equals("add"))
                {
                    //int chkarnorecord = Convert.ToInt32(objCommon.LookUp("PAY_ARREARS", "COUNT(ARNO)", "COLLEGENO=" + ddlCollegeType.SelectedValue + " AND PAYHEAD='"
                    //                    + ddlPayhead.SelectedValue + "' AND AFRM=CONVERT(DATETIME,'" +
                    //                    Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103) AND ATO=CONVERT(DATETIME,'"
                    //                    + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103)"));

                    int chkarnorecord = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "COUNT(ARNO)", "COLLEGE_NO=" + ddlCollege.SelectedValue + " AND COLLEGENO=" + ddlCollegeType.SelectedValue + " AND PAYHEAD='"
                                        + ddlPayhead.SelectedValue + "' AND (AFRM IN(CONVERT(DATETIME,'" +
                                        Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'"
                                        + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103)) OR ATO IN (CONVERT(DATETIME,'"
                                        + Convert.ToDateTime(txtCPTToDate.Text).ToString("yyyyMMdd") + "',103),CONVERT(DATETIME,'" +
                                        Convert.ToDateTime(txtCPTFromDate.Text).ToString("yyyyMMdd") + "',103))) AND RULENO="+ddlRule.SelectedValue+""));

                   
                                    
                    if (chkarnorecord == 0)
                    {

                        CustomStatus cs = (CustomStatus)objpay.AddPayArrears(objPayroll);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            int arno = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "ISNULL(MAX(ARNO),0)", ""));



                            //BindListViewSanctioned();
                            //Clear();
                            //objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);
                            MessageBox("Arrears Calculated Sucessfully");
                           
                            int i = 0;
                            int idno = 0;
                            string idno1 = string.Empty;
                            DataSet ds;
                            if (ddlEmployee.SelectedValue != "0")
                                ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "idno", "fname", "IDNO=" + ddlEmployee.SelectedValue, "");
                            else
                                ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "E.FNAME", "E.COLLEGE_NO="+ddlCollege.SelectedValue+" AND E.STAFFNO=" + ddlCollegeType.SelectedValue+" AND P.PAYRULE="+ddlRule.SelectedValue,"");
                                //ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "E.FNAME", "E.COLLEGE_NO=" + ddlCollege.SelectedValue + " AND E.STAFFNO=" + ddlCollegeType.SelectedValue, "");

                            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                idno = Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]);
                                idno1 = idno1 + idno.ToString() + ",";
                            }
                            idno1 = idno1.Substring(0, idno1.Length - 1);

                            //int arnocheck = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "ARNO", "COLLEGENO=" + ddlCollegeType.SelectedValue + " AND AFRM BETWEEN '" + txtCPTFromDate.Text + "' AND '" + txtCPTToDate.Text + "' AND RULENO="+ddlRule.SelectedValue));



                            CustomStatus cs1 = (CustomStatus)objpay.CalculateArrears(Convert.ToInt32(ddlCollegeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), ddlPayhead.SelectedValue, Fdate, Tdate, idno1, Convert.ToDouble(txtCurRate.Text), arno, Convert.ToInt32(ddlRule.SelectedValue));


                            //clear();

                        }
                        //else
                        //    objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                       
                    }
                    else
                    {
                        int arnocheck = Convert.ToInt32(objCommon.LookUp("PAYROLL_ARREARS", "ARNO", "COLLEGE_NO="+ddlCollege.SelectedValue+" AND COLLEGENO=" + ddlCollegeType.SelectedValue + " AND PAYHEAD='"
                                   + ddlPayhead.SelectedValue + "' AND (AFRM IN(CONVERT(DATETIME,'" +
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

                        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            idno = Convert.ToInt32(ds.Tables[0].Rows[i]["IDNO"]);
                            idno1 = idno1 + idno.ToString() + ",";

                        }
                        idno1 = idno1.Substring(0, idno1.Length - 1);

                        //CustomStatus cs = (CustomStatus)objpay.DeleteArrearsDiffRecord(arnocheck);
                        if (arnocheck > 0)
                        {
                            MessageBox("Arrears Already calculated, To recalculate Remove it from Arrear Diff!");
                        }
                        else
                        {
                            CustomStatus cs1 = (CustomStatus)objpay.CalculateArrears(Convert.ToInt32(ddlCollegeType.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), ddlPayhead.SelectedValue, Fdate, Tdate, idno1, Convert.ToDouble(txtCurRate.Text), arnocheck, Convert.ToInt32(ddlRule.SelectedValue));
                           
                           // clear();
                        }

                        //MessageBox("Arrears from " + Convert.ToDateTime(txtCPTFromDate.Text).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtCPTToDate.Text).ToString("dd/MM/yyyy") + " for " + ddlCollegeType.SelectedItem.Text +
                        //" on " + ddlPayhead.SelectedItem.Text + " head Alreay Calculated ");
                       
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
    protected void txtCurRate_TextChanged(object sender, EventArgs e)
    {
        //string monyr = txtCPTFromDate.Text;
        //int count = Convert.ToInt32 (objCommon.LookUp("PAYROLL_SALFILE","COUNT(*)","MONYEAR="+"'"+monyr+"'" +"and collegeno ="+ddlCollegeType.SelectedValue));
        //if (count == 0)
        //{
        //    MessageBox("Salary is not Processed for" + monyr);
        //}
        //else
        //{
        //    txtRemark.Text = ddlPayhead.SelectedItem.Text + "Arrears from month" + txtCPTFromDate.Text + "" + txtCPTToDate.Text;
        //}
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void ddlCollegeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+ ' ' + '['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND STAFFNO="+ ddlCollegeType.SelectedValue +" AND PM.PSTATUS='Y' AND EM.IDNO > 0 AND EM.STATUS IS NULL", "EM.IDNO");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void clear()
    {
        ddlCollegeType.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        ddlPayhead.SelectedIndex = 0;
        ddlRule.SelectedIndex = 0;
        txtCPTFromDate.Text = txtCPTToDate.Text = txtCurRate.Text = txtGovDt.Text = txtGovOrdNo.Text = txtOffDt.Text = txtOffOrdNo.Text = txtRemark.Text = string.Empty;
    }
   
}
