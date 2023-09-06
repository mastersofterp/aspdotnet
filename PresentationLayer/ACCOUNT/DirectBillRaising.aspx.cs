//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :DIRECT BILL RAISING                                                  
// CREATION DATE :22-FEB-2019                                            
// CREATED BY    :Nokhlal Kumar                                       
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Configuration;
using System.Web;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using SendGrid;


public partial class ACCOUNT_DirectBillRaising : System.Web.UI.Page
{
    Common objCommon = new Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();
    AccountTransactionController objPC1 = new AccountTransactionController();

    //public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public string Docpath = HttpContext.Current.Server.MapPath("~/FILEUPLOAD/upload_files/");
    public string path = string.Empty;
    DataTable dt = new DataTable("DocTbl");
    string IsSponsorProject = "";
    public static string StrVno = "abc/111";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        // Added By Akshay Dixit On 12-04-2022
        if (Session["comp_code"] == null)
        {
            Session["comp_set"] = "NotSelected";

            Response.Redirect("~/Account/selectCompany.aspx");
        }

        if (!Page.IsPostBack)
        {
            Clear();
            //Check Page Authoriztion
            CheckPageAuthorization();

            Session["BillComp_Code"] = null;
            ViewState["VoucherSqn"] = null;

            int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
            ViewState["RaisePayNo"] = serialno.ToString();
            lblSerialNo.Text = ViewState["RaisePayNo"].ToString();


            //added by tanu 07/04/2022
            int bill_Id = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(bill_id),0) + 1", "COMPANY_CODE='" +(Session["Comp_Code"]).ToString()+"'"));
            ViewState["bill_Id"] = bill_Id.ToString();
            lblBillId.Text = ViewState["bill_Id"].ToString();

            SetDeptPath();

            //Binding Raising Payment Bill Into Repeater
            BindCompany();

            Page.Title = Session["coll_name"].ToString();
            ddlSelectCompany.Focus();

            ViewState["Edit"] = "N";


            Panel1.Visible = false;

            btnSubmit.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = false;

            filldropdown();
            ViewState["DOCS"] = null;
            DeleteDirecPath(Docpath + "TEMPUPLOAD_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]));
            PendingRadiobtn();// show default pending items

            //BindBillList();
            //if (!IsPostBack)
            // txtGSTAmount.Attributes.Add("readonly", "readonly");
            //txtTDSonSGSTAmount.Attributes.Add("readonly", "readonly");

        }
        GetGstValue();
        //GetTdsAmt();
        //GetTdsOnIgstAmt();

        //GetTdsOnSGstAmt();
        //GetTdsOnCGstAmt();
        //GetTdsAmt();
        //GetTdsOnIGstAmt();
        GetNetAmount();
    }


    private void filldropdown()
    {
        objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");
           
    }

    private void GetTdsAmt()
    {

        if (ddlSection.SelectedValue == "0" || ddlSection.SelectedValue == "")
        {
            txtTDSPer.Text = "0";
            //txtTdsOnAmt.Text = "0";
            txtTDSAmt.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSPer.Text != "")
            {
                if (hdntxtTDSAmt.Value != "")
                {
                    txtTDSAmt.Text = hdntxtTDSAmt.Value;
                }
                else
                {
                    txtTDSAmt.Text = (Convert.ToDouble(txtTdsOnAmt.Text) * Convert.ToDouble(txtTDSPer.Text) / 100).ToString();
                }
            }
        }
    }
    private void GetTdsOnIGstAmt()
    {

        if (ddlTDSonGSTSection.SelectedValue == "0" || ddlTDSonGSTSection.SelectedValue == "")
        {
            txtTDSonGSTPer.Text = "0";
            //txtTDSGSTonAmount.Text = "0";
            txtTDSonGSTAmount.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonGSTPer.Text != "")
            {
                if (hdntxtTDSonGSTAmount.Value != "")
                {
                    txtTDSonGSTAmount.Text = hdntxtTDSonGSTAmount.Value;
                }
                else
                {
                    txtTDSonGSTAmount.Text = (Convert.ToDouble(txtTDSGSTonAmount.Text) * Convert.ToDouble(txtTDSonGSTPer.Text) / 100).ToString();
                }
            }
        }
    }

    private void GetTdsOnSGstAmt()
    {

        if (ddlTDSonSGSTSection.SelectedValue == "0" || ddlTDSonSGSTSection.SelectedValue == "")
        {
            txtTDSonSGSTPer.Text = "0";
            //txtTDSonSGSTAmount.Text = "0";
            txtTDSonSGSTAmount.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonSGSTPer.Text != "")
            {
                if (hdntxtTDSonSGSTAmount.Value != "")
                {
                    txtTDSonSGSTAmount.Text = hdntxtTDSonSGSTAmount.Value;
                }
                else
                {
                    txtTDSonSGSTAmount.Text = (Convert.ToDouble(txtTDSSGSTonAmount.Text) * Convert.ToDouble(txtTDSonSGSTPer.Text) / 100).ToString();
                }
            }
        }
    }

    private void GetTdsOnCGstAmt()
    {
        if (ddlTDSonCGSTSection.SelectedValue == "0" || ddlTDSonCGSTSection.SelectedValue == "")
        {
            txtTDSonCGSTPer.Text = "0";
            txtTDSonCGSTAmount.Text = "0";
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            if (txtTDSonCGSTPer.Text != "")
            {
                if (hdntxtTDSonCGSTAmount.Value != "")
                {
                    txtTDSonCGSTAmount.Text = hdntxtTDSonCGSTAmount.Value;
                }
                else
                {
                    txtTDSonCGSTAmount.Text = (Convert.ToDouble(txtTDSCGSTonAmount.Text) * Convert.ToDouble(txtTDSonCGSTPer.Text) / 100).ToString();
                }
            }
        }
    }

    private void GetTdsOnIgstAmt()
    {
        if (ddlTDSonGSTSection.SelectedValue == "0" || ddlTDSonGSTSection.SelectedValue == "")
        {
            txtTDSonGSTPer.Text = "";
            txtTDSonGSTAmount.Text = string.Empty;
            //txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSPer.Text = "";
            }
        }
    }

    private void GetNetAmount()
    {
        if (!chkGST.Checked && !chkIGST.Checked)
        {
            txtGSTAmount.Text = "0";
        }

        if (txtBillAmt.Text != "")
        {
            Double NetAmount = Convert.ToDouble(txtBillAmt.Text);
            Double TotalDedution = 0;
            Double GrossAmount = Convert.ToDouble(txtBillAmt.Text);

            if (chkTDSApplicable.Checked)
            {
                if (txtTDSAmt.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSAmt.Text);
                    TotalDedution = Convert.ToDouble(txtTDSAmt.Text);
                }
            }
            if (chkTdsOnCGSTSGST.Checked)
            {
                if (txtTDSonCGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonCGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonCGSTAmount.Text);
                }
                if (txtTDSonSGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonSGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonSGSTAmount.Text);
                }

            }
            if (chkTDSOnGst.Checked)
            {
                if (txtTDSonGSTAmount.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtTDSonGSTAmount.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtTDSonGSTAmount.Text);
                }
            }
            if (chkSecurity.Checked)
            {
                if (txtSecurityAmt.Text != "")
                {
                    NetAmount = NetAmount - Convert.ToDouble(txtSecurityAmt.Text);
                    TotalDedution = TotalDedution + Convert.ToDouble(txtSecurityAmt.Text);
                }
            }
            if (chkIGST.Checked)
            {
                if (txtIgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtIgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtIgstAmount.Text);
                }
            }
            if (chkGST.Checked)
            {
                if (txtCgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtCgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtCgstAmount.Text);
                }
                if (txtSgstAmount.Text != "")
                {
                    NetAmount = NetAmount + Convert.ToDouble(txtSgstAmount.Text);
                    GrossAmount = GrossAmount + Convert.ToDouble(txtSgstAmount.Text);
                }
            }

            txtNetAmt.Text = NetAmount.ToString();
            txtTotalTDSAmt.Text = TotalDedution.ToString();
            txtTotalBillAmt.Text = GrossAmount.ToString();
            //txtTotalTDSAmt.Text = 
            //txtNetAmt.Text = (Convert.ToDouble(txtTotalBillAmt.Text) - Convert.ToDouble(txtTotalTDSAmt.Text)).ToString();
        }

        
    }

    //private void GetTdsAmt()
    //{
    //    if (ddlSection.SelectedValue == "0" || ddlSection.SelectedValue == "")
    //    {
    //        txtTDSPer.Text = "";
    //        txtTDSAmt.Text = string.Empty;
    //        //txtNetAmt.Text = txtBillAmt.Text;
    //    }
    //    else
    //    {
    //        decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSection.SelectedValue)));
    //        if (per > 0)
    //        {
    //            txtTDSPer.Text = per.ToString();
    //            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 1 + ");</script>", false);
    //        }
    //        else
    //        {
    //            txtTDSPer.Text = "";
    //        }
    //    }
    //}
    #endregion

    #region Private Methods


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

    private void BindBillList()
    {
        string EMPNO = "";
        DataSet ds = null;
        if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
        {
            EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
            ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "D");  //,Session["Comp_Code"].ToString()
        }
        else
        {
            EMPNO = objCommon.LookUp("USER_ACC UA INNER JOIN PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "EMP.IDNO", "UA.UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));

            ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "D"); // , Session["Comp_Code"].ToString()

            //if (EMPNO.ToString() == null || EMPNO.ToString() == "" || EMPNO.ToString() == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Sorry! you Cannot request the bill.", this.Page);
            //    Response.Redirect("~/notauthorized.aspx?page=RaisingPaymentBill.aspx");
            //    return;
            //}
            //else
            //{
            //    ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO),"D");
            //}
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvBillList.DataSource = ds.Tables[0];
            lvBillList.DataBind();
        }
        else
        {
            lvBillList.DataSource = null;
            lvBillList.DataBind();
        }
    }

    protected void rblApprovePending_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblApprovePending.SelectedValue == "approve")
        {
           
            string EMPNO = "";
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
            {
                EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
                ds = objRPBController.GetSelectedDirectBillDetails(Convert.ToInt32(EMPNO), "D", "A");
            }
            else
            {
                EMPNO = objCommon.LookUp("USER_ACC UA INNER JOIN PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "EMP.IDNO", "UA.UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));

                ds = objRPBController.GetSelectedDirectBillDetails(Convert.ToInt32(EMPNO), "D", "A");

                //if (EMPNO.ToString() == null || EMPNO.ToString() == "" || EMPNO.ToString() == string.Empty)
                //{
                //    objCommon.DisplayMessage(UPDLedger, "Sorry! you Cannot request the bill.", this.Page);
                //    Response.Redirect("~/notauthorized.aspx?page=RaisingPaymentBill.aspx");
                //    return;
                //}
                //else
                //{
                //    ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO),"D");
                //}
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBillList.DataSource = ds.Tables[0];
                lvBillList.DataBind();
            }
            else
            {
                lvBillList.DataSource = null;
                lvBillList.DataBind();
            }
            lvBillList.Visible = true;
        }
        if (rblApprovePending.SelectedValue == "pending")
        {
       
            PendingRadiobtn();

        }
    }
    public void PendingRadiobtn()
    {
        if (rblApprovePending.SelectedValue == "pending")
        {

            string EMPNO = "";
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
            {
                EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
                ds = objRPBController.GetSelectedDirectBillDetails(Convert.ToInt32(EMPNO), "D", "P");
            }
            else
            {
                if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
                {
                    EMPNO = objCommon.LookUp("USER_ACC UA INNER JOIN PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "EMP.IDNO", "UA.UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));

                    ds = objRPBController.GetSelectedDirectBillDetails(Convert.ToInt32(EMPNO), "D", "P");
                }
                else
                {
                    EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
                    ds = objRPBController.GetSelectedDirectBillDetails(Convert.ToInt32(EMPNO), "D", "P");
                }
                //if (EMPNO.ToString() == null || EMPNO.ToString() == "" || EMPNO.ToString() == string.Empty)
                //{
                //    objCommon.DisplayMessage(UPDLedger, "Sorry! you Cannot request the bill.", this.Page);
                //    Response.Redirect("~/notauthorized.aspx?page=RaisingPaymentBill.aspx");
                //    return;
                //}
                //else
                //{
                //    ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO),"D");
                //}
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvBillList.DataSource = ds.Tables[0];
                lvBillList.DataBind();
            }
            else
            {
                lvBillList.DataSource = null;
                lvBillList.DataBind();
            }
            lvBillList.Visible = true;
        }
    }

    private void SetDeptPath()
    {
        objCommon.FillDropDownList(ddlDeptBranch, "Payroll_subdept", "SubDeptNo", "SubDept", "SubDeptNo<>0", "SubDept");
        txtApprovalDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

        string Path = "AO->FO";

        lblAuthorityPath.Text = Path.ToString();
        lblAuthorityPath.Font.Bold = true;

        dvApproval.Visible = true;
        dvAuthorityPath.Visible = true;
    }

    private void Clear()
    {
        txtApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtApprovalNo.Text = string.Empty;
        txtBillAmt.Text = string.Empty;
        txtGSTAmount.Text = string.Empty;
        txtTotalTDSAmt.Text = string.Empty;
        txtGSTINNo.Text = string.Empty;
        txtNatureOfService.Text = string.Empty;
        txtPanNo.Text = string.Empty;
        txtPayeeNameAddress.Text = string.Empty;
        txtRemark.Text = string.Empty;
       // txtServiceName.Text = string.Empty;
        ddlEmployee.SelectedValue = "0";   

        txtTDSAmt.Text = string.Empty;
        txtTDSPer.Text = string.Empty;
        txtTotalBillAmt.Text = string.Empty;
        txtNetAmt.Text = string.Empty;
        txtInvoiceDate.Text = string.Empty;
        txtInvoiceNo.Text = string.Empty;
        ddlDeptBranch.SelectedValue = "0";
        ddlAccount.SelectedValue = "0";
        ddlApprovedBy.SelectedValue = "0";
        chkTDSApplicable.Checked = false;
        ddlAccount.Focus();
        dvSection.Visible = false;
        int serialno = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0) + 1", ""));
        ViewState["RaisePayNo"] = serialno.ToString();
        lblSerialNo.Text = ViewState["RaisePayNo"].ToString();
        ViewState["Edit"] = "N";
        ddlBudgethead.SelectedValue = "0";
        txtLedgerHead.Text = string.Empty;
        dvApproval.Visible = false;
        dvAuthorityPath.Visible = false;

        txtTransDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtTDSLedger.Text = string.Empty;
        txtBankLedger.Text = string.Empty;
        txtNarration.Text = string.Empty;
        dvIGST.Visible = false;
        dvIgstledger.Visible = false;
        dvGSTLedger.Visible = false;
        dvCgst.Visible = false;
        dvSgst.Visible = false;
        txtIGSTLedger.Text = txtCGSTLedger.Text = txtSgstLedger.Text = string.Empty;
        txtSgstPer.Text = txtCGSTPER.Text = txtCgstAmount.Text = txtGSTAmount.Text = txtIgstAmount.Text = txtIgstPer.Text = txtTotalTDSAmt.Text = string.Empty;
        chkGST.Checked = false;
        chkIGST.Checked = false;

        tdstextbox.Visible = false;
        tdslabel.Visible = false;

        chkTDSOnGst.Checked = false;
        divTdsOnGst.Visible = false;
        txtTDSGSTonAmount.Text = string.Empty;
        ///ddlTDSonGSTSection.SelectedIndex = 0;
        txtTDSonGSTPer.Text = string.Empty;
        txtTDSonGSTAmount.Text = string.Empty;

        chkSecurity.Checked = false;
        chkTdsOnCGSTSGST.Checked = false;
        txtTDSCGSTonAmount.Text = string.Empty;
        txtTDSonCGSTAmount.Text = string.Empty;
        //ddlTDSonCGSTSection.SelectedIndex = 0;
        txtTDSonCGSTPer.Text = string.Empty;
        txtSecurityPer.Text = string.Empty;
        txtSecurityAmt.Text = string.Empty;

        txtTDSSGSTonAmount.Text = string.Empty;
        txtTDSonSGSTAmount.Text = string.Empty;
        //ddlTDSonSGSTSection.SelectedIndex = 0;
        txtTDSonSGSTPer.Text = string.Empty;

        txtTDSonGSTLedger.Text = string.Empty;
        txtTDSonCGSTLedger.Text = string.Empty;
        txtTDSonSGSTLedger.Text = string.Empty;
        txtSecurityLedger.Text = string.Empty;

        divTdsOnCGST.Visible = false;
        divTDSOnSGST.Visible = false;
        divSecurity.Visible = false;
        divTdsonCGstLedger.Visible = false;
        divTdsonSGstLedger.Visible = false;

        lvNewBills.DataSource = null;
        lvNewBills.DataBind();
        pnlNewBills.Visible = false;
        txtBillName.Text = string.Empty;

        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        DeleteDirecPath(Docpath + "TEMPUPLOAD_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]));
        rblApprovePending.SelectedValue = "pending";

        txtExpenseLedger.Text = string.Empty;
        ddlSponsor.SelectedIndex = 0;
        ddlProjSubHead.SelectedIndex = 0;
        lblExpenseLedger.Text = string.Empty;
        hdnGSTAmount.Value = string.Empty;
        hdnTDSonCGSTAmount.Value = string.Empty;
        txtSgstAmount.Text = string.Empty;


        ddlPayeeNature.SelectedValue = "0";
        ddlPayee.SelectedValue = "0";
        ddlEmpType.SelectedValue = "0";
        divPayeeNature1.Visible = false;


        //added by tanu 07/04/2022

        int bill_Id = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(bill_id),0) + 1", "COMPANY_CODE='" + (Session["Comp_Code"]).ToString() + "'"));
        ViewState["bill_Id"] = bill_Id.ToString();
        lblBillId.Text = ViewState["bill_Id"].ToString();

        
    }

    private void BindCompany()
    {
       // ddlSelectCompany.SelectedValue = Session["comp_code"].ToString();

        int IsExist = Convert.ToInt32(objCommon.LookUp("acc_usercashbook", "ISNULL(COUNT(*),0)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
        if (IsExist > 0)
        {
            objCommon.FillDropDownList(ddlSelectCompany, "ACC_COMPANY a inner join Split((select cashbookid from acc_usercashbook where ua_no=" + Session["userno"].ToString() + "),',') b on (a.COMPANY_NO=b.Value)", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'AND COMPANY_CODE = '" + Session["comp_code"].ToString() + "'", "COMPANY_NAME");
          
        }
        else
        {
            objCommon.FillDropDownList(ddlSelectCompany, "Acc_Company", "Company_no", "Company_Name + ' - ' + Cast(Year(COMPANY_FINDATE_FROM) as nvarchar(10))+ ' - ' + Cast(Year(COMPANY_FINDATE_TO) As nvarchar(10))", "Drop_Flag='N' AND COMPANY_CODE = '" + Session["comp_code"].ToString() + "'", "Company_Name");

        }
      

    }

    //private void BindBillList()
    //{
    //    string EMPNO = "";
    //    DataSet ds = null;
    //    if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
    //    {
    //        EMPNO = objCommon.LookUp("USER_ACC", "ISNULL(UA_IDNO,0) UA_IDNO", "UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));
    //        ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "D");
    //    }
    //    else
    //    {
    //        EMPNO = objCommon.LookUp("USER_ACC UA INNER JOIN PAYROLL_EMPMAS EMP ON UA.UA_IDNO = EMP.IDNO", "EMP.IDNO", "UA.UA_NO =" + Convert.ToInt32(Session["userno"].ToString()));

    //        ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO), "D");

    //        //if (EMPNO.ToString() == null || EMPNO.ToString() == "" || EMPNO.ToString() == string.Empty)
    //        //{
    //        //    objCommon.DisplayMessage(UPDLedger, "Sorry! you Cannot request the bill.", this.Page);
    //        //    Response.Redirect("~/notauthorized.aspx?page=RaisingPaymentBill.aspx");
    //        //    return;
    //        //}
    //        //else
    //        //{
    //        //    ds = objRPBController.GetSelectedBillDetails(Convert.ToInt32(EMPNO),"D");
    //        //}
    //    }

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvBillList.DataSource = ds.Tables[0];
    //        lvBillList.DataBind();
    //    }
    //    else
    //    {
    //        lvBillList.DataSource = null;
    //        lvBillList.DataBind();
    //    }
    //}

    private void BindSection()
    {
        objCommon.FillDropDownList(ddlSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonCGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
        objCommon.FillDropDownList(ddlTDSonSGSTSection, "ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_NO", "SECTION_NAME", "", "SECTION_NO");
    }

    private void BindBudget()
    {
        if (Convert.ToInt32(ddlSelectCompany.SelectedValue) > 0)
        {
            CostCenterController objbud = new CostCenterController();
            objbud.BindBudgetHead(ddlBudgethead);
            //objCommon.FillDropDownList(ddlBudgethead, "ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD a", "isnull(budg_no,0) budg_no", "BUDG_NAME", "not exists (select BUDG_PRNO from ACC_" + Session["BillComp_Code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO)", "BUDG_NAME");
        }
        else
        {

        }
    }

    #endregion

    #region Events Click
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Panel1.Enabled = true;
        btnSubmit.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;

        btnSubmit.Enabled = true;
        btnCancel.Enabled = true;
        btnBack.Enabled = true;

        UPDLedger.Visible = true;
        UpdatePanel1.Visible = true;
        div7.Visible = true;


        Session["BillComp_Code"] = null;
        Panel1.Visible = true;
        pnlBillList.Visible = false;
        SetDeptPath();
        BindCompany();
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["VoucherSqn"] = null;
        ViewState["Edit"] = "Y";
        try
        {

            btnSubmit.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = true;

            Panel1.Visible = true;
            Panel1.Enabled = true;


            UPDLedger.Visible = true;
            UpdatePanel1.Visible = true;

            btnSubmit.Visible = true;
            btnCancel.Visible = true;
            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;


            ImageButton btnEdit = sender as ImageButton;
            int RaisePayno = Convert.ToInt32(btnEdit.CommandArgument.ToString());
            string statuscode = btnEdit.CommandName.ToString();

            string status = objCommon.LookUp("ACC_RAISING_PAYMENT_BILL_APP_PASS", "ISNULL([STATUS],'P')", "BIIL_SRNO =" + RaisePayno + " AND SEQUENCE = 2");
            if (status == "" || status == string.Empty)
            {
                status = "P";
            }
            if (status != "P")
            {
                objCommon.DisplayMessage(UPDLedger, "Sorry! You cannot edit this record, Because it is forwarded/approved by authority", this.Page);

                btnSubmit.Visible = false;
                btnBack.Visible = false;
                btnCancel.Visible = false;

                Panel1.Visible = false;
                Panel1.Enabled = false;


                UPDLedger.Visible = false;
                UpdatePanel1.Visible = false;

                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                btnSubmit.Enabled = false;
                btnCancel.Enabled = false;
                return;
            }
            else
            {
                DataSet ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO a", "*", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    //added by tanu 07/04/2022
                    ViewState["bill_Id"] = ds.Tables[0].Rows[0]["bill_id"].ToString();
                    lblBillId.Text = ds.Tables[0].Rows[0]["bill_id"].ToString();
                    int BILL_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["bill_id"]);

                    ViewState["BillNo"] = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["RAISE_PAY_NO"].ToString();
                    lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();

                    Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();

                    string VoucherSqn = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + BILL_NO + " AND TRANSACTION_TYPE = 'J'");
                    ViewState["VoucherSqn"] = VoucherSqn;
                    objCommon.FillDropDownList(ddlSponsor, "Acc_" + Session["BillComp_Code"].ToString() + "_Project", "ProjectId", "ProjectName", "", "");


                    //FILL Account Dropdown....
                    ddlAccount.Items.Clear();
                    int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Count(*)", ""));
                    if (count > 0)
                    {
                        objCommon.FillDropDownList(ddlAccount, "ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Acc_Id", "Account_Name", "", "Acc_Id");

                    }
                    else
                    {
                        ddlAccount.Items.Clear();
                    }
                    BindBudget();
                    BindSection();
                    //lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    //ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                    ddlAccount.SelectedValue = ds.Tables[0].Rows[0]["ACCOUNT"].ToString();
                    ddlDeptBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();

                    string Path = "AO->FO";

                    if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
                    {
                        dvApproval.Visible = true;
                        dvAuthorityPath.Visible = true;
                        lblAuthorityPath.Text = Path.ToString();
                    }
                    else
                    {
                        lblAuthorityPath.Text = "";
                        lblAuthorityPath.Font.Bold = true;
                        dvApproval.Visible = false;
                        dvAuthorityPath.Visible = false;
                    }

                    txtApprovalNo.Text = ds.Tables[0].Rows[0]["APPROVAL_NO"].ToString();
                    txtApprovalDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPROVAL_DATE"]).ToString("dd/MM/yyyy");
                    lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["BUDGET_NO"].ToString()) > 0)
                    {
                        ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                        ddlBudgethead_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["ProjectId"].ToString() != "")
                    {
                        ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
                    }
                    else
                    {
                        //ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
                    }
                    objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["BillComp_Code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["BillComp_Code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "A.ProjectId=" + ddlSponsor.SelectedValue, "");
                    if (ds.Tables[0].Rows[0]["ProjectSubId"].ToString() != "" && ds.Tables[0].Rows[0]["ProjectSubId"].ToString() != "0")
                    {
                        ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
                        ddlProjSubHead_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        //ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
                    }

                    string PartyNo = ds.Tables[0].Rows[0]["LEDGER_NO"].ToString();
                    if (Convert.ToInt32(PartyNo) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + PartyNo);
                        txtLedgerHead.Text = partyname.ToString();
                        txtLedgerHead_TextChanged(sender, e);
                    }
                    else
                    {
                        string partyname = "";
                        txtLedgerHead.Text = partyname.ToString();
                    }

                    string ExpenseLedgerNo = ds.Tables[0].Rows[0]["EXPENSE_LEDGER_NO"].ToString();
                    if (ExpenseLedgerNo != "0" && ExpenseLedgerNo != "")
                    {
                        string ExpenseLedgerName = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + ExpenseLedgerNo);
                        txtExpenseLedger.Text = ExpenseLedgerName.ToString();
                        txtExpenseLedger_TextChanged(sender, e);
                    }
                    else
                    {
                        string ExpenseLedgerName = "";
                        txtExpenseLedger.Text = ExpenseLedgerName.ToString();
                    }

                    string CGST = ds.Tables[0].Rows[0]["TRANS_CGST_ID"].ToString();
                    if (Convert.ToInt32(CGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + CGST);
                        txtCGSTLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtCGSTLedger.Text = partyname.ToString();
                    }

                    string SGST = ds.Tables[0].Rows[0]["TRANS_SGST_ID"].ToString();
                    if (Convert.ToInt32(SGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + SGST);
                        txtSgstLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtSgstLedger.Text = partyname.ToString();
                    }

                    string IGST = ds.Tables[0].Rows[0]["TRANS_IGST_ID"].ToString();
                    if (Convert.ToInt32(IGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + IGST);
                        txtIGSTLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtIGSTLedger.Text = partyname.ToString();
                    }


                    rdbBillList.SelectedValue = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                    txtInvoiceNo.Text = ds.Tables[0].Rows[0]["BILL_INVOICE_NO"].ToString();
                    string date = ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"].ToString();
                    if (date != "" || date != string.Empty)
                    {
                        txtInvoiceDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtInvoiceDate.Text = "";
                    }
                    txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                    txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                    // txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();

                    //ADDED BY TANU 15/12/2021
                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        Diveservices.Visible = true;
                        DivServicesText.Visible = true;
                        txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                    }
                    else
                    {
                        Diveservices.Visible = false;
                        DivServicesText.Visible = false;

                    }
                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divProviderType.Visible = false;
                        DivddlEmptype.Visible = false;
                        ddlEmpType.SelectedValue = "0";
                    }
                    else
                    {
                        divProviderType.Visible = true;
                        DivddlEmptype.Visible = true;
                        ddlEmpType.SelectedValue = ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString();
                    }

                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divEmployee1.Visible = false;
                        divEmployee2.Visible = false;
                        divPayeeNature1.Visible = false;
                        divPayeeNature2.Visible = false;
                        divPayee1.Visible = false;
                        divPayee2.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            divEmployee1.Visible = true;
                            divEmployee2.Visible = true;
                        }
                        else
                        {
                            divEmployee1.Visible = false;
                            divEmployee2.Visible = false;
                        }
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {
                            divPayeeNature1.Visible = true;
                            divPayeeNature2.Visible = true;
                            divPayee1.Visible = true;
                            divPayee2.Visible = true;
                        }
                        else
                        {
                            divPayeeNature1.Visible = false;
                            divPayeeNature2.Visible = false;
                            divPayee1.Visible = false;
                            divPayee2.Visible = false;
                        }
                    }

                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divEmployee1.Visible = false;
                        divEmployee2.Visible = false;
                        divPayeeNature1.Visible = false;
                        divPayeeNature2.Visible = false;
                        divPayee1.Visible = false;
                        divPayee2.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            ddlPayeeNature.SelectedValue = "0";
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {
                            ddlPayeeNature.SelectedValue = ds.Tables[0].Rows[0]["PAYEE_NATURE"].ToString();
                        }
                        else
                        {
                            ddlPayeeNature.SelectedValue = "0";

                        }
                        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["comp_code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");

                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {
                            ddlPayee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                        }
                        else
                        {
                            ddlEmployee.SelectedValue = "0";
                            ddlPayee.SelectedValue = "0";
                        }



                    }

                    txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();

                    txtBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_AMT"]).ToString();
                    int IsTDS = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDS"].ToString());
                    int IsTDSonGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONGST"].ToString());
                    int IsTDSonCGSTSGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONCGSTSGST"].ToString());
                    int ISSECURITY = Convert.ToInt32(ds.Tables[0].Rows[0]["ISSECURITY"].ToString());
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                    if (IsTDS == 1)
                    {
                        //BindSection();
                        chkTDSApplicable.Checked = true;
                        dvSection.Visible = true;
                        txtTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_AMT"]).ToString();
                        txtTdsOnAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_ON_AMT"]).ToString();
                        ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                        txtTDSPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_PERCENT"]).ToString();
                        //txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();

                        string TDS_Ledger_Id = ds.Tables[0].Rows[0]["TRANS_TDSID"].ToString();
                        if (Convert.ToInt32(TDS_Ledger_Id) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDS_Ledger_Id);
                            txtTDSLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSLedger.Text = TDSpartyname.ToString();
                        }

                        tdslabel.Visible = true;
                        tdstextbox.Visible = true;
                    }

                    else
                    {
                        chkTDSApplicable.Checked = false;
                        dvSection.Visible = false; ;
                        txtTDSAmt.Text = "";
                        txtTdsOnAmt.Text = "";
                        ddlSection.SelectedValue = "0";
                        txtTDSPer.Text = "";
                        // txtPanNo.Text = "";
                        string TDSpartyname = "";
                        //  txtLedgerHead.Text = TDSpartyname.ToString();
                        tdslabel.Visible = false;
                        tdstextbox.Visible = false;
                    }

                    //Added by Gopal Anthati
                    if (IsTDSonGST == 1)
                    {
                        //BindSection();
                        chkTDSOnGst.Checked = true;
                        divTdsOnGst.Visible = true;
                        txtTDSonGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONGST_AMOUNT"]).ToString();
                        txtTDSGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSGST_ON_AMT"]).ToString();
                        ddlTDSonGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONGST_SECTION"].ToString();
                        txtTDSonGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONGSTPER"]).ToString();

                        string TDSonGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONGSTID"].ToString();
                        if (Convert.ToInt32(TDSonGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonGSTLedgerId);
                            txtTDSonGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonGSTLedger.Text = TDSpartyname.ToString();
                        }
                        divTdsonGstLedger.Visible = true;
                    }

                    else
                    {
                        chkTDSOnGst.Checked = false;
                        divTdsOnGst.Visible = false; ;
                        txtTDSonGSTAmount.Text = "";
                        txtTDSGSTonAmount.Text = "";
                        ddlTDSonGSTSection.SelectedValue = "0";
                        txtTDSonGSTPer.Text = "";
                        string TDSpartyname = "";
                        divTdsonGstLedger.Visible = false;

                    }

                    if (IsTDSonCGSTSGST == 1)
                    {
                        //BindSection();
                        chkTdsOnCGSTSGST.Checked = true;
                        divTdsOnCGST.Visible = true;
                        divTDSOnSGST.Visible = true;
                        divTdsonSGstLedger.Visible = true;
                        divTdsonCGstLedger.Visible = true;
                        txtTDSonCGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONCGST_AMOUNT"]).ToString();
                        txtTDSCGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSCGST_ON_AMT"]).ToString();
                        ddlTDSonCGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONCGST_SECTION"].ToString();
                        txtTDSonCGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONCGSTPER"]).ToString();

                        string TDSonCGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONCGSTID"].ToString();
                        if (Convert.ToInt32(TDSonCGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonCGSTLedgerId);
                            txtTDSonCGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonCGSTLedger.Text = TDSpartyname.ToString();
                        }

                      

                        txtTDSonSGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONSGST_AMOUNT"]).ToString();
                        txtTDSSGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSSGST_ON_AMT"]).ToString();
                        ddlTDSonSGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONSGST_SECTION"].ToString();
                        txtTDSonSGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONSGSTPER"]).ToString();

                        string TDSonSGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONSGSTID"].ToString();
                        if (Convert.ToInt32(TDSonSGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonSGSTLedgerId);
                            txtTDSonSGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonSGSTLedger.Text = TDSpartyname.ToString();
                        }
                    }

                    else
                    {
                        chkTdsOnCGSTSGST.Checked = false;
                        divTdsOnCGST.Visible = false;
                        divTDSOnSGST.Visible = false;
                        divTdsonSGstLedger.Visible = false;
                        divTdsonCGstLedger.Visible = false;
                        txtTDSonCGSTAmount.Text = "";
                        txtTDSCGSTonAmount.Text = "";
                        ddlTDSonCGSTSection.SelectedValue = "0";
                        txtTDSonCGSTPer.Text = "";

                        txtTDSonSGSTAmount.Text = "";
                        txtTDSSGSTonAmount.Text = "";
                        ddlTDSonSGSTSection.SelectedValue = "0";
                        txtTDSonSGSTPer.Text = "";


                    }

                    if (ISSECURITY == 1)
                    {
                        chkSecurity.Checked = true;
                        txtSecurityPer.Text = ds.Tables[0].Rows[0]["SECURITY_PER"].ToString();
                        txtSecurityAmt.Text = ds.Tables[0].Rows[0]["SECURITY_AMOUNT"].ToString();
                        string SecurityLedgerId = ds.Tables[0].Rows[0]["TRANS_SECURITYID"].ToString();
                        if (Convert.ToInt32(SecurityLedgerId) > 0)
                        {
                            string Securitypartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + SecurityLedgerId);
                            txtSecurityLedger.Text = Securitypartyname.ToString();
                        }
                        else
                        {
                            string Securitypartyname = "";
                            txtSecurityLedger.Text = Securitypartyname.ToString();
                        }
                        divSecurity.Visible = true;
                        divSecurityLedger.Visible = true;

                    }
                    else
                    {
                        chkSecurity.Checked = false;
                        divSecurity.Visible = false;
                        divSecurityLedger.Visible = false;
                    }

                    //Added by vijay andoju
                    int isigst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISIGST"].ToString());
                    int isgst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISGST"].ToString());
                    if (isgst == 1)
                    {
                        chkGST.Checked = true;
                        txtCgstAmount.Text = ds.Tables[0].Rows[0]["CGST_AMOUNT"].ToString();
                        txtCGSTPER.Text = ds.Tables[0].Rows[0]["CGST_PER"].ToString();
                        ddlCgstSection.SelectedValue = ds.Tables[0].Rows[0]["CGST_SECTIONNO"].ToString();

                        txtSgstAmount.Text = ds.Tables[0].Rows[0]["SGST_AMOUNT"].ToString();
                        txtSgstPer.Text = ds.Tables[0].Rows[0]["SGST_PER"].ToString();
                        ddlSgstSection.SelectedValue = ds.Tables[0].Rows[0]["SGST_SECTIONNO"].ToString();
                        dvCgst.Visible = true;
                        dvSgst.Visible = true;
                        dvGSTLedger.Visible = true;
                    }
                    else
                    {
                        chkGST.Checked = false;
                        dvCgst.Visible = false;
                        dvSgst.Visible = false;
                        dvGSTLedger.Visible = false;
                    }
                    if (isigst == 1)
                    {
                        chkIGST.Checked = true;
                        txtIgstPer.Text = ds.Tables[0].Rows[0]["IGST_PER"].ToString();
                        txtIgstAmount.Text = ds.Tables[0].Rows[0]["IGST_AMOUNT"].ToString();
                        ddlIgstSection.SelectedValue = ds.Tables[0].Rows[0]["IGST_SECTIONNO"].ToString();
                        dvIGST.Visible = true;
                        dvIgstledger.Visible = true;
                    }
                    else
                    {
                        chkIGST.Checked = false;
                        dvIGST.Visible = false;
                        dvIgstledger.Visible = false;
                    }

                    txtTotalBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"]).ToString();
                    txtTotalTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TDS_AMT"]).ToString();
                    txtGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GST_AMT"]).ToString();
                    txtNetAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_AMT"]).ToString();

                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

                    string BankLedgerId = ds.Tables[0].Rows[0]["TRANS_BANKID"].ToString();
                    if (Convert.ToInt32(BankLedgerId) > 0)
                    {
                        string BankLedgerName = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + BankLedgerId);
                        txtBankLedger.Text = BankLedgerName.ToString();
                    }
                    else
                    {
                        string BankLedgerName = "";
                        txtBankLedger.Text = BankLedgerName.ToString();
                    }
                    string TransDate = ds.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                    if (TransDate.Length > 0)
                    {
                        txtTransDate.Text = Convert.ToDateTime(TransDate).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtTransDate.Text = "";
                    }

                    txtNarration.Text = ds.Tables[0].Rows[0]["TRANS_NARRATION"].ToString();

                    Panel1.Visible = true;
                    pnlBillList.Visible = false;
                    //Added by Vidisha on 13-05-2021 for multiple bill upload
                    DataSet dsDoc = objCommon.FillDropDown("ACC_BILL_RAISED_UPLOAD_DOCUMENT", "*", "", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                    if (Convert.ToInt32(dsDoc.Tables[0].Rows.Count) > 0)
                    {
                        int rowCount = dsDoc.Tables[0].Rows.Count;
                        CreateTable();
                        DataTable dtM = (DataTable)ViewState["DOCS"];
                        for (int i = 0; i < rowCount; i++)
                        {
                            DataRow dr = dtM.NewRow();
                            dr["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                            dr["Filepath"] = dsDoc.Tables[0].Rows[i]["Filepath"].ToString();
                            dr["DisplayFileName"] = dsDoc.Tables[0].Rows[i]["DisplayFileName"].ToString();
                            dr["DocumentName"] = dsDoc.Tables[0].Rows[i]["DocumentName"].ToString();
                            dtM.Rows.Add(dr);
                            dtM.AcceptChanges();
                            ViewState["DOCS"] = dtM;
                            ViewState["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                        }
                        lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
                        lvNewBills.DataBind();
                        pnlNewBills.Visible = true;
                    }
                    else
                    {
                        pnlNewBills.Visible = false;
                        lvNewBills.DataSource = null;
                        lvNewBills.DataBind();
                    }
                    //end
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lvBillList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
            ImageButton btnview = (ImageButton)e.Item.FindControl("btnView");
            ImageButton btnprint = (ImageButton)e.Item.FindControl("btnPrint");
            ImageButton btnAddBill = (ImageButton)e.Item.FindControl("btnAddBill");         
            ImageButton btnEdit = (ImageButton)e.Item.FindControl("btnEdit");
            ImageButton btnPayVoucher = (ImageButton)e.Item.FindControl("btnPayVoucher");

            //Added By Akshay Dixit On 12-07-2022 To Visible true and false the view,print and AddBill column.
            HtmlControl tdview = (HtmlControl)e.Item.FindControl("idView");
            HtmlControl idPrint = (HtmlControl)e.Item.FindControl("idPrint");
            HtmlControl idAddBill = (HtmlControl)e.Item.FindControl("idAddBill");

                    
            if (rblApprovePending.SelectedValue == "pending")
            {
              //  thAction.Visible = true;
             //   btnEdit.Visible = true;

                 thView.Visible = false;
                // thprint.Visible = false;
                 thAddBill.Visible = false;
                 btnview.Visible = false;
              //   btnprint.Visible = false;
                 btnAddBill.Visible = false;
                 btnPayVoucher.Visible = false;

                 //Added By Akshay On 12-09-2022      
                 thApprove.Visible = false;
                 thReturn.Visible = true;
              
                                 
            }
            else
            {
              //  thAction.Visible = false;
              //  btnEdit.Visible = false;

                thView.Visible = true;
                thprint.Visible = true;
              //  thAddBill.Visible = true;
                btnview.Visible = true;
                btnprint.Visible = true;
              //  btnAddBill.Visible = true;
                btnPayVoucher.Visible = true;

                tdview.Visible = true;
                idPrint.Visible = true;
               // idAddBill.Visible = true;

                //Added By Akshay On 12-09-2022      
                thApprove.Visible = true;
                thReturn.Visible = false;
               
            }
        }

        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Visible = true;
            btnBack.Visible = true;
            btnCancel.Visible = true;

            //Added by gopal anthati on 20-08-2021 To catch the cleared values after postback
            //if (hdnTDSonCGSTAmount.Value != "" || hdnTDSonCGSTAmount.Value != "0")
            //    txtTDSonSGSTAmount.Text = hdnTDSonCGSTAmount.Value;
            //if (hdnGSTAmount.Value != "" || hdnGSTAmount.Value != "0")
            //    txtGSTAmount.Text = hdnGSTAmount.Value;

            if (ddlSelectCompany.SelectedValue == "0")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Select Company", this.Page);  // Added by Akshay Dixit on 02-09-2022 to validate select Company.
                ddlSelectCompany.Focus();
                return;
            }


            //Added by Vidisha on 13-05-2021 for TDS and GST check box validation 
            if ((chkTDSApplicable.Checked || chkTdsOnCGSTSGST.Checked || chkTDSOnGst.Checked) && (txtPanNo.Text == "" || txtPanNo.Text == string.Empty))
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter PAN Number", this.Page);
                txtPanNo.Focus();
                return;
            }
            if ((chkGST.Checked || chkIGST.Checked) && (txtGSTINNo.Text == "" || txtGSTINNo.Text == string.Empty))
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter GSTIN Number", this.Page);
                txtGSTINNo.Focus();
                return;
            }
            //end
            if (lblSerialNo.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Serial is not available, Cannot able to save..!", this.Page);
                return;
            }
            //if (ddlAccount.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Account..!", this.Page);
            //    return;
            //}
            //if (ddlDeptBranch.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Department", this.Page);
            //    return;
            //}
            if (lblAuthorityPath.Text == "" || lblAuthorityPath.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Passing Path cannot be blank!", this.Page);
                return;
            }
            //if (txtServiceName.Text == "" || txtServiceName.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter Supplier's/Service Provider Name", this.Page);
            //    return;
            //}
            //if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter Nature of Service /Goods etc", this.Page);
            //    return;
            //}
            //if (txtBillAmt.Text == "" || txtBillAmt.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
            //    return;
            //}
            //if (txtLedgerHead.Text == "" || txtLedgerHead.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Party Name.", this.Page);
            //    return;
            //}
            if (chkTDSApplicable.Checked)
            {
                if (txtTDSAmt.Text == "" || txtTDSAmt.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Amount", this.Page);
                    return;
                }
                if (txtTdsOnAmt.Text == "" || txtTdsOnAmt.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS On Amount", this.Page);
                    return;
                }
                if (txtTDSPer.Text == "" || txtTDSPer.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Percentage", this.Page);
                    return;
                }
                if (ddlSection.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Tds Section.", this.Page);
                    return;
                }
                if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter PAN Number", this.Page);
                    return;
                }
                if (txtPanNo.Text.Length != 10)
                {
                    objCommon.DisplayMessage(UPDLedger, "PAN No. should be 10 character!!", this.Page);
                    return;
                }
            }

            //ADDED BY TANU 27_12_2021 FOR VALIDATION 

            if (chkGST.Checked)
            {
                if (txtCGSTPER.Text == "" || txtCGSTPER.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter CGST Percentage", this.Page);
                    return;
                }
                if (txtCgstAmount.Text == "" || txtCgstAmount.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter CGST  Amount", this.Page);
                    return;
                }

                if (txtSgstPer.Text == "" || txtSgstPer.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter SGST Percentage", this.Page);
                    return;
                }
                if (txtSgstAmount.Text == "" || txtSgstAmount.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter SGST  Amount", this.Page);
                    return;
                }

            }

            if (chkIGST.Checked)
            {
                if (txtIgstPer.Text == "" || txtIgstPer.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter IGST Percentage", this.Page);
                    return;
                }
                if (txtIgstAmount.Text == "" || txtIgstAmount.Text == string.Empty)
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Enter IGST  Amount", this.Page);
                    return;
                }

            }
            if (ddlEmpType.SelectedValue == "1")
            {
              //  if (ddlEmployee.SelectedValue=="0")
                if (ddlEmployee.SelectedIndex > 0)
                {
                   
                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Employee", this.Page);
                    return;
                }
            }
            if (ddlEmpType.SelectedValue == "2")
            {
               // if (ddlPayeeNature.SelectedValue == "0")
                if (ddlPayeeNature.SelectedIndex > 0)
                {
                   
                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Payee Nature", this.Page);
                    return;
                }
                //if (ddlPayee.SelectedValue=="0")
                if (ddlPayee.SelectedIndex > 0)
                {

                }
                else
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Payee", this.Page);
                    return;
                }
            }



            if (txtGSTAmount.Text == "" || txtGSTAmount.Text == string.Empty)
            {
                //objCommon.DisplayMessage(UPDLedger, "Please Enter GST Amount", this.Page);
                //return;
                txtGSTAmount.Text = "0";
            }
            if (txtTotalTDSAmt.Text == "" || txtTotalTDSAmt.Text == string.Empty)
            {
                //objCommon.DisplayMessage(UPDLedger, "Please Enter GST Amount", this.Page);
                //return;
                txtTotalTDSAmt.Text = "0";
            }
            if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
            {
                if (txtGSTINNo.Text.Length != 15)
                {
                    objCommon.DisplayMessage(UPDLedger, "GST No. should be 15 character!!", this.Page);
                    return;
                }
            }
            if (txtNetAmt.Text == "" || txtNetAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Net Amount is Empty", this.Page);
            }
            if (txtTotalBillAmt.Text == "" || txtTotalBillAmt.Text == string.Empty)
            {
                objCommon.DisplayMessage(UPDLedger, "Total Bill Amount is Empty", this.Page);
                return;
            }
            //if (ddlSelectCompany.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select any Company", this.Page);
            //    return;
            //}

            if (txtTransDate.Text != string.Empty)
            {
                DataSet dsFinancilaYear = objCommon.FillDropDown("ACC_COMPANY", "CAST(COMPANY_FINDATE_FROM AS DATE) FINFROM", "CAST(COMPANY_FINDATE_TO AS DATE) FINTO", "DROP_FLAG = 'N' AND COMPANY_CODE = '" + Session["BillComp_Code"].ToString() + "'", "");
                string FinDateFrom = dsFinancilaYear.Tables[0].Rows[0]["FINFROM"].ToString();
                string FinDateTo = dsFinancilaYear.Tables[0].Rows[0]["FINTO"].ToString();
                if (DateTime.Compare(Convert.ToDateTime(txtTransDate.Text), Convert.ToDateTime(FinDateTo.ToString())) == 1)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
                    txtTransDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtTransDate.Focus();
                    return;
                }

                if (DateTime.Compare(Convert.ToDateTime(FinDateFrom.ToString()), Convert.ToDateTime(txtTransDate.Text)) == 1)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Transaction Should Be In The Financial Year Range. ", this);
                    txtTransDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtTransDate.Focus();
                    return;
                }
            }

            ObjRPB.SERIAL_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());
            ObjRPB.ACCOUNT = ddlAccount.SelectedValue;
            ObjRPB.DEPT_ID = Convert.ToInt32(ddlDeptBranch.SelectedValue);
            ObjRPB.APPROVAL_NO = txtApprovalNo.Text.ToString();
            ObjRPB.APPROVAL_DATE = Convert.ToDateTime(txtApprovalDate.Text);
            ObjRPB.APPROVED_BY = lblApprovedBy.Text;


         //   ObjRPB.SUPPLIER_NAME = txtServiceName.Text.ToString();

            //ADDED BY TANU  FOR TAG USER 15/12/2021


            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.SUPPLIER_NAME = ddlEmployee.SelectedItem.Text;
            }
            else if (ddlEmpType.SelectedValue == "2")
            {
                ObjRPB.SUPPLIER_NAME = ddlPayee.SelectedItem.Text;
            }
            else
            {
                ObjRPB.SUPPLIER_NAME = "0";
            }

            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.SUPPLIER_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                ObjRPB.SUPPLIER_ID = Convert.ToInt32(ddlPayee.SelectedValue);
            }
            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.PROVIDER_TYPE = Convert.ToInt32(ddlEmpType.SelectedValue);
            }
            else
            {
                ObjRPB.PROVIDER_TYPE = Convert.ToInt32(ddlEmpType.SelectedValue);
            }
            if (ddlEmpType.SelectedValue == "1")
            {
                ObjRPB.PAYEE_NATURE = 0;
            }
            else
            {
                ObjRPB.PAYEE_NATURE = Convert.ToInt32(ddlPayeeNature.SelectedValue);
            }



            ObjRPB.PAYEE_NAME_ADDRESS = txtPayeeNameAddress.Text.ToString();
            ObjRPB.NATURE_SERVICE = txtNatureOfService.Text.ToString();
            ObjRPB.GSTIN_NO = txtGSTINNo.Text.ToString();
            ObjRPB.BILL_AMT = Convert.ToDouble(txtBillAmt.Text);
            ObjRPB.GST_AMT = Convert.ToDouble(txtGSTAmount.Text);
            ObjRPB.TOTAL_BILL_AMT = Convert.ToDouble(txtTotalBillAmt.Text);
            ObjRPB.TOTAL_TDS_AMT = Convert.ToDouble(txtTotalTDSAmt.Text);
            ObjRPB.NET_AMT = Convert.ToDouble(txtNetAmt.Text);
            ObjRPB.INVOICEDATE = txtInvoiceDate.Text;
            ObjRPB.INVOICENO = txtInvoiceNo.Text;
            ObjRPB.COMPANY_CODE = Session["BillComp_Code"].ToString();
            ObjRPB.BUDGET_NO = Convert.ToInt32(ddlBudgethead.SelectedValue);

            //Added by gopal anthati on 19-08-2021
            ObjRPB.ProjectId = Convert.ToInt32(ddlSponsor.SelectedValue);
            ObjRPB.ProjectSubId = Convert.ToInt32(ddlProjSubHead.SelectedValue);
            if (txtExpenseLedger.Text == "" || txtExpenseLedger.Text == string.Empty)
            {
                ObjRPB.EXPENSE_LEDGER_NO = 0;
            }
            else
            {
                ObjRPB.EXPENSE_LEDGER_NO = Convert.ToInt32(txtExpenseLedger.Text.Trim().ToString().Split('*')[1].ToString());
            }

            if (txtLedgerHead.Text == "" || txtLedgerHead.Text == string.Empty)
            {
                ObjRPB.LEDGER_NO = 0;
            }
            else
            {
                ObjRPB.LEDGER_NO = Convert.ToInt32(txtLedgerHead.Text.Trim().ToString().Split('*')[1].ToString());
            }

            ObjRPB.BILL_TYPE = Convert.ToInt32(rdbBillList.SelectedValue);
            ObjRPB.PAN_NO = txtPanNo.Text.ToString();
            if (chkTDSApplicable.Checked)
            {
                ObjRPB.ISTDS = 1;
                ObjRPB.TDS_AMT = Convert.ToDouble(txtTDSAmt.Text);
                ObjRPB.TDS_ON_AMT = Convert.ToDouble(txtTdsOnAmt.Text);
                ObjRPB.TDS_PERCENT = Convert.ToDouble(txtTDSPer.Text);
                ObjRPB.SECTION_NO = Convert.ToInt32(ddlSection.SelectedValue);
                //ObjRPB.PAN_NO = txtPanNo.Text.ToString();

                if (txtTDSLedger.Text == "" || txtTDSLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDS Ledger", this.Page);
                    return;
                    //ObjRPB.TDSLedgerId = 0;
                }
                else
                {
                       DataSet ds = new DataSet();     
                       RaisingPaymentBillController objPayBill = new RaisingPaymentBillController();

                       ds = objPayBill.GetAccountEntryLedger(txtTDSLedger.Text.Trim().ToString().Split('*')[0].ToString());


                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Please select Proper TDS Ledger", this.Page);
                        return;

                    }
                    else
                    {

                        ObjRPB.TDSLedgerId = Convert.ToInt32(txtTDSLedger.Text.Trim().ToString().Split('*')[1].ToString());
                    }
                }
            }
            else
            {
                ObjRPB.ISTDS = 0;
                ObjRPB.TDS_AMT = 0;
                ObjRPB.TDS_PERCENT = 0;
                ObjRPB.SECTION_NO = 0;
                // ObjRPB.PAN_NO = string.Empty;
                ObjRPB.TDSLedgerId = 0;
            }

            //Added by Gopal Anthati on 21/04/2021
            if (chkTDSOnGst.Checked)
            {
                ObjRPB.ISTDSONGST = 1;

                
               // ObjRPB.TDS_ON_GST_AMT = Convert.ToDouble(txtTDSonGSTAmount.Text);
                ObjRPB.TDS_ON_GST_AMT = Convert.ToDouble(txtTDSGSTonAmount.Text) * Convert.ToDouble(txtTDSonGSTPer.Text) / 100;
                ObjRPB.TDSGST_ON_AMT = Convert.ToDouble(txtTDSGSTonAmount.Text);
                ObjRPB.TDSGST_PERCENT = Convert.ToDouble(txtTDSonGSTPer.Text);
                ObjRPB.TDSGST_SECTION_NO = Convert.ToInt32(ddlTDSonGSTSection.SelectedValue);

                if (txtTDSonGSTLedger.Text == "" || txtTDSonGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonGST Ledger", this.Page);
                    return;
                    //ObjRPB.TDSonGSTLedgerId = 0;
                }
                else
                {
                    ObjRPB.TDSonGSTLedgerId = Convert.ToInt32(txtTDSonGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                }
            }
            else
            {
                ObjRPB.ISTDSONGST = 0;
                ObjRPB.TDS_ON_GST_AMT = 0;
                ObjRPB.TDSGST_PERCENT = 0;
                ObjRPB.TDSGST_SECTION_NO = 0;
                ObjRPB.TDSonGSTLedgerId = 0;
            }

            if (chkTdsOnCGSTSGST.Checked)
            {
                ObjRPB.ISTDSONCGSTSGST = 1;
                ObjRPB.TDS_ON_CGST_AMT = Convert.ToDouble(txtTDSonCGSTAmount.Text);
                ObjRPB.TDS_ON_SGST_AMT = Convert.ToDouble(txtTDSonSGSTAmount.Text);
                ObjRPB.TDSCGST_ON_AMT = Convert.ToDouble(txtTDSCGSTonAmount.Text);
                ObjRPB.TDSSGST_ON_AMT = Convert.ToDouble(txtTDSSGSTonAmount.Text);
                ObjRPB.TDSCGST_PERCENT = Convert.ToDouble(txtTDSonCGSTPer.Text);
                ObjRPB.TDSSGST_PERCENT = Convert.ToDouble(txtTDSonSGSTPer.Text);
                ObjRPB.TDSCGST_SECTION_NO = Convert.ToInt32(ddlTDSonCGSTSection.SelectedValue);
                ObjRPB.TDSSGST_SECTION_NO = Convert.ToInt32(ddlTDSonSGSTSection.SelectedValue);

                if (txtTDSonCGSTLedger.Text == "" || txtTDSonCGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonCGST Ledger", this.Page);
                    return;
                   // ObjRPB.TDSonCGSTLedgerId = 0;
                }
                else
                {
                    ObjRPB.TDSonCGSTLedgerId = Convert.ToInt32(txtTDSonCGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                }
                if (txtTDSonSGSTLedger.Text == "" || txtTDSonSGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter TDSonSGST Ledger", this.Page);
                    return;
                   // ObjRPB.TDSonSGSTLedgerId = 0;
                }
                else
                {
                    ObjRPB.TDSonSGSTLedgerId = Convert.ToInt32(txtTDSonSGSTLedger.Text.Trim().ToString().Split('*')[1].ToString());
                }
            }
            else
            {
                ObjRPB.ISTDSONCGSTSGST = 0;
                ObjRPB.TDS_ON_CGST_AMT = 0;
                ObjRPB.TDSCGST_PERCENT = 0;
                ObjRPB.TDSCGST_SECTION_NO = 0;
                ObjRPB.TDSonCGSTLedgerId = 0;


                ObjRPB.TDS_ON_SGST_AMT = 0;
                ObjRPB.TDSSGST_PERCENT = 0;
                ObjRPB.TDSSGST_SECTION_NO = 0;
                ObjRPB.TDSonSGSTLedgerId = 0;
            }


            //Added by vijay andoju on 08092020
            if (chkIGST.Checked)
            {
                ObjRPB.ISIGST = 1;
                ObjRPB.IGST_AMT = Convert.ToDouble(txtIgstAmount.Text);
                ObjRPB.IGST_PER = Convert.ToDouble(txtIgstPer.Text);
                ObjRPB.IGST_SECTION = 0;
                if (txtIGSTLedger.Text == "" || txtIGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter IGST Ledger", this.Page);
                    return;

                }

            }
            else
            {
                ObjRPB.ISIGST = 0;
                ObjRPB.IGST_AMT = 0;
                ObjRPB.IGST_PER = 0;
                ObjRPB.IGST_SECTION = 0;
            }
            if (chkGST.Checked)
            {
                ObjRPB.ISGST = 1;
                ObjRPB.CGST_AMT = Convert.ToDouble(txtCgstAmount.Text);
                ObjRPB.CGST_PER = Convert.ToDouble(txtCGSTPER.Text);
                ObjRPB.CGST_SECTION = 0;

                ObjRPB.SGST_AMT = Convert.ToDouble(txtSgstAmount.Text);
                ObjRPB.SGST_PER = Convert.ToDouble(txtSgstPer.Text);
                ObjRPB.SGST_SECTION = 0;

                if (txtCGSTLedger.Text == "" || txtCGSTLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter CGST Ledger", this.Page);
                    return;

                }
                if (txtSgstLedger.Text == "" || txtSgstLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter SGST Ledger", this.Page);
                    return;

                }
            }
            else
            {
                ObjRPB.ISGST = 0;
                ObjRPB.CGST_AMT = 0;
                ObjRPB.CGST_PER = 0;
                ObjRPB.CGST_PER = 0;

                ObjRPB.SGST_AMT = 0;
                ObjRPB.SGST_PER = 0;
                ObjRPB.SGST_SECTION = 0;
            }

            if (chkSecurity.Checked)
            {
                ObjRPB.ISSECURITY = 1;
                ObjRPB.SECURITY_AMT = Convert.ToDouble(txtSecurityAmt.Text);
                //ObjRPB.SECURITY_PER = Convert.ToDouble(txtSecurityPer.Text); 
                ObjRPB.SECURITY_PER = 0;

                if (txtSecurityLedger.Text == "" || txtSecurityLedger.Text == string.Empty)
                {
                    //added by tanu 24/01/2022
                    objCommon.DisplayMessage(UPDLedger, "Please Enter Security Ledger", this.Page);
                    return;
                   // ObjRPB.SecurityLedgerId = 0;
                }
                else
                {
                    ObjRPB.SecurityLedgerId = Convert.ToInt32(txtSecurityLedger.Text.Trim().ToString().Split('*')[1].ToString());
                }
            }
            else
            {
                ObjRPB.ISSECURITY = 0;
                ObjRPB.SECURITY_AMT = 0;
                ObjRPB.SECURITY_PER = 0;
                ObjRPB.SecurityLedgerId = 0;
            }


            if (txtTransDate.Text != string.Empty)
            {
                ObjRPB.TransDate = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");
            }
            else
            {
                ObjRPB.TransDate = string.Empty;
            }

         
            if (txtBankLedger.Text != string.Empty)
            {
                ObjRPB.BankLedgerId = Convert.ToInt32(txtBankLedger.Text.Trim().ToString().Split('*')[1].ToString());
            }
            else
            {
                ObjRPB.BankLedgerId = 0;
            }

            //ADDED BY VIJAY ANDOJU ON 19092020

            //Added by vijay on18092020
            int TDSId = 0, bankid = 0, Cgst = 0, Sgst = 0, Igst = 0;
            if (txtCGSTLedger.Text == "" || txtBankLedger.Text == string.Empty)
            {
                Cgst = 0;
            }
            else
            {
                Cgst = Convert.ToInt32(txtCGSTLedger.Text.ToString().Split('*')[1].ToString());
            }
            if (txtSgstLedger.Text == "" || txtBankLedger.Text == string.Empty)
            {
                Sgst = 0;
            }
            else
            {
                Sgst = Convert.ToInt32(txtSgstLedger.Text.ToString().Split('*')[1].ToString());
            }
            if (txtIGSTLedger.Text == "" || txtBankLedger.Text == string.Empty)
            {
                Igst = 0;
            }
            else
            {
                Igst = Convert.ToInt32(txtIGSTLedger.Text.ToString().Split('*')[1].ToString());
            }

            ObjRPB.Narration = txtNarration.Text;
            ObjRPB.REMARK = txtRemark.Text;

            //Added by Vidisha on 13-05-2021 for multiple bill upload
            DataTable dtD = null;
            if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
            {
                dtD = (DataTable)ViewState["DOCS"];
            }
            else
            {
                CreateTable();
                dtD = (DataTable)ViewState["DOCS"];
            }

            if (ViewState["Edit"].ToString() == "Y")
            {
                ObjRPB.RAISE_PAY_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());



                if (dtD.Rows.Count > 0)
                {
                    ObjRPB.FILEPATH = Docpath + "DIRECT_BILL_RAISED\\EMPID_" + Convert.ToInt32(Session["userno"]) + "\\BillNo_" + ObjRPB.RAISE_PAY_NO;
                    if (ViewState["DESTINATION_PATH"] != null)
                    {
                        AddDocuments(ObjRPB.RAISE_PAY_NO);
                    }
                }
                else
                {
                    ObjRPB.FILEPATH = "";
                }
            }
            else
            {
                ObjRPB.RAISE_PAY_NO = 0;

                // To avoid resend action/duplicate entry added by gopal anthati on 19-08-2021
                //int Bill_Serial_No = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(SERIAL_NO),0)", ""));
                //int Count = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "Count(*)", "RAISE_PAY_NO=" + Convert.ToInt32(ViewState["MaxRaiseId"])));
                //if (Count > 0)
                //{
                //    Response.Redirect(Request.Url.ToString());
                //}

                if (ViewState["DESTINATION_PATH"] != null)
                {
                    ObjRPB.FILEPATH = ViewState["DESTINATION_PATH"].ToString() + "\\BillNo_";
                }
            }
            //end

            int ret = objRPBController.AddDirectRaisingBillpayment(ObjRPB, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Session["colcode"].ToString()), Cgst, Sgst, Igst, dtD);
            //int ret = 1;
            if (ret == 1)
            {
                //Added by Vidisha on 13-05-2021 for multiple bill upload
                if (ViewState["DESTINATION_PATH"] != null)
                {
                    AddDocuments(ObjRPB.RAISE_PAY_NO);
                }
                //end
                objCommon.DisplayMessage(UPDLedger, "Record Inserted Successfully!", this.Page);
                JVCreationForBill();
                //BindBillList();
                PendingRadiobtn();
                Panel1.Visible = false;
                pnlBillList.Visible = true;
                Clear();
                //ViewState["MaxRaiseId"] = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(RAISE_PAY_NO),0)", ""));
               

            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully!", this.Page);
                if (ViewState["VoucherSqn"] != "")
                {
                    JvVoucherDelete();
                }
                JVCreationForBill();
                //BindBillList();
                PendingRadiobtn();
                Panel1.Visible = false;
                pnlBillList.Visible = true;
                Clear();

            }
            //Added by Vidisha on 13-05-2021 for clear multiple bill upload
            pnlNewBills.Visible = false;
            ViewState["DESTINATION_PATH"] = null;
            ViewState["DOCS"] = null;
            clearDoc();
            ViewState["letrno"] = null;
            //end

            //Added By Akshay Dixit on 02-09-2022 To hide after save
            btnSubmit.Visible = false;
            btnBack.Visible = false;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void JvVoucherDelete()
    {
        string FinancialDate = objCommon.LookUp("ACC_COMPANY", "cast(COMPANY_FINDATE_FROM as nvarchar(20))" + "+ ''' and ''' +" + "cast(COMPANY_FINDATE_TO as nvarchar(20))", "COMPANY_CODE='" + Session["BillComp_Code"].ToString() + "'");
        DataSet TF_ENTRY = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_TRANS", "TRANSFER_ENTRY", "", "TRANSACTION_DATE BETWEEN '" + FinancialDate + "' and  VOUCHER_SQN=" + ViewState["VoucherSqn"].ToString().Trim(), string.Empty);
        if (Convert.ToInt32(TF_ENTRY.Tables[0].Rows[0][0].ToString()) == 0)
        {
            CustomStatus cs = (CustomStatus)objRPBController.DeleteJournalVoucher(ViewState["VoucherSqn"].ToString(), Session["BillComp_Code"].ToString(), "J");
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayUserMessage(UPDLedger, "Voucher Deleted successfully.", this);               
            }
        }
    }

    private void JVCreationForBill()
    {
        int rows = 0;
        XmlDocument objXMLDoc = new XmlDocument();

        ViewState["VoucherNo"] = string.Empty;

        FinanceCashBookController objCBC = new FinanceCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Convert.ToInt32(ddlSelectCompany.SelectedValue).ToString().Trim());
        if (dtr.Read())
        {
            Session["BillComp_Code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
        }
        dtr.Close();
        XmlDeclaration xmlDeclaration = objXMLDoc.CreateXmlDeclaration("1.0", null, null);

        // Create the root element
        XmlElement rootNode = objXMLDoc.CreateElement("tables");
        objXMLDoc.InsertBefore(xmlDeclaration, objXMLDoc.DocumentElement);
        objXMLDoc.AppendChild(rootNode);
        try
        {
            rows = 1;

            //if (chkTDSApplicable.Checked)
            //{
            //    rows = 2;

            //}


            HiddenField hdnparty = new HiddenField();

            for (int i = 0; i < rows; i++)
            {
                XmlElement objElement = objXMLDoc.CreateElement("Table");
                XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
                XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");
                XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
                XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");
                XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
                XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
                XmlElement Section = objXMLDoc.CreateElement("SECTION");
                XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
                XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
                XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");

                XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
                XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
                XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");
                XmlElement IsTDSApplicable = objXMLDoc.CreateElement("IsTDSApplicable");

                XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
                XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
                XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
                XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

                XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");

                XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
                XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
                XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

                XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
                XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
                XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

                XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");
                XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");


                IsGSTApplicable.InnerText = "0";
                TDSSection.InnerText = "0";
                TDSAMOUNT.InnerText = "0";
                TDPersentage.InnerText = "0";
                IsTDSonGSTApplicable.InnerText = "0";
                IsTDSonCGSTSGSTApplicable.InnerText = "0";
                IsSecurityApplicable.InnerText = "0";

                ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                ViewState["TDSAmount"] = txtTDSAmt.Text.Trim().ToString();
                ViewState["TDSonCGSTAmount"] = txtTDSonCGSTAmount.Text.Trim().ToString();
                ViewState["TDSonSGSTAmount"] = txtTDSonSGSTAmount.Text.Trim().ToString();
                if (i == 0)
                {
                    Section.InnerText = "0";
                    IsTDSApplicable.InnerText = "0";
                    ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                    ViewState["TDSAmount"] = 0;

                    TDSSection.InnerText = "0";
                    TDSAMOUNT.InnerText = "0.00";
                    TDPersentage.InnerText = "0.00";

                    AmtWithoutGST.InnerText = "0";
                }
                else if (chkTDSApplicable.Checked)
                {
                    Section.InnerText = Convert.ToInt32(ddlSection.SelectedValue).ToString();
                    IsTDSApplicable.InnerText = "0";
                    ViewState["Amount"] = txtNetAmt.Text.Trim().ToString();
                    ViewState["TDSAmount"] = txtTDSAmt.Text.Trim().ToString();
                    AmtWithoutGST.InnerText = "0";

                    TDSSection.InnerText = "0";
                    TDSAMOUNT.InnerText = "0.00";
                    TDPersentage.InnerText = "0.00";
                }
                GSTPercent.InnerText = "0";
                SUBTR_NO.InnerText = "0";
                if (txtTransDate.Text.ToString().Trim() != "")
                {
                    DateTime TranDate = Convert.ToDateTime(txtTransDate.Text);
                    TRANSACTION_DATE.InnerText = TranDate.ToString("dd-MMM-yyyy");
                    ViewState["TRANDATE"] = Convert.ToDateTime(txtTransDate.Text);
                }
                else
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Enter The Transaction Date.", this);
                    txtTransDate.Focus();
                    return;
                }

                XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
                TRANSACTION_TYPE.InnerText = "J";
                //HiddenField hdnOpParty = new HiddenField();
                //if (i == 0)
                //{
                //    hdnOpParty.Value = Convert.ToInt32(txtBankLedger.Text.Split('*')[1]).ToString();
                //}
                //else
                //{
                //    hdnOpParty.Value = Convert.ToInt32(txtLedgerHead.Text.Split('*')[1]).ToString();
                //}
                //if (hdnOpParty != null)
                //{
                //    OPARTY.InnerText = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                //    ViewState["OPartyNo"] = Convert.ToInt32(hdnOpParty.Value.ToString().Trim()).ToString();
                //}
                OPARTY.InnerText = Convert.ToInt32(txtLedgerHead.Text.Split('*')[1] == "" ? "0" : txtLedgerHead.Text.Split('*')[1]).ToString();
                ViewState["OPartyNo"] = OPARTY.InnerText;


                //if (i == 0)
                //{
                //    hdnparty.Value = Convert.ToInt32(txtExpenseLedger.Text.Split('*')[1]).ToString();
                //}
                //else if (chkTDSApplicable.Checked)
                //{

                //    hdnparty.Value = Convert.ToInt32(txtTDSLedger.Text.Split('*')[1] == "" ? "0" : txtTDSLedger.Text.Split('*')[1]).ToString();
                //    ViewState["OPartyNo"] = ViewState["OPartyNo"].ToString() + "," + hdnparty.Value;
                //}

                //if (hdnparty != null)
                //{
                //    PARTY_NO.InnerText = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                //    ViewState["PartyNo"] = Convert.ToInt32(hdnparty.Value.ToString().Trim()).ToString();
                //}
                //else { return; }
                PARTY_NO.InnerText = Convert.ToInt32(txtExpenseLedger.Text.Split('*')[1] == "" ? "0" : txtExpenseLedger.Text.Split('*')[1]).ToString();
                ViewState["PartyNo"] = PARTY_NO.InnerText;

                if (i == 0)
                {
                    TRAN.InnerText = "Dr";
                    AMOUNT.InnerText = txtBillAmt.Text.Trim().ToString();

                }
                else
                {
                    if (chkTDSApplicable.Checked)
                    {
                        TRAN.InnerText = "Cr";

                        AMOUNT.InnerText = txtBillAmt.Text.ToString();//txtTDSAmt.Text.Trim().ToString();

                    }
                }

                XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
                DEGREE_NO.InnerText = "0";

                XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");
                TRANSFER_ENTRY.InnerText = "0";
                XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
                CBTYPE_STATUS.InnerText = "0";
                XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
                CBTYPE.InnerText = "0";
                XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");
                RECIEPT_PAYMENT_FEES.InnerText = "0";
                XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
                REC_NO.InnerText = "0";
                XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
                XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");

                CHQ_NO.InnerText = "0";
                ViewState["CHQ_NO"] = "0";
                CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");
                ViewState["CHQ_DATE"] = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

                XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
                CHALLAN.InnerText = "false";
                XmlElement CAN = objXMLDoc.CreateElement("CAN");
                CAN.InnerText = "false";
                XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
                DCR_NO.InnerText = "0";
                XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
                CC_ID.InnerText = "0";

                XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
                if (ddlBudgethead.SelectedValue == "0")
                {
                    BudgetNo.InnerText = "0";
                }
                else
                {
                    BudgetNo.InnerText = ddlBudgethead.SelectedValue;
                }


                XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
                if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
                {
                    PARTY_NAME.InnerText = "-";
                }
                else
                {
                    PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
                }

                XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
                if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
                {
                    PAN_NO.InnerText = "-";
                }
                else
                {
                    PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
                }

                XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
                if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
                {
                    NATURE_SERVICE.InnerText = "-";
                }
                else
                {
                    NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
                }

                XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
                CASH_BANK_NO.InnerText = "0";
                XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
                ADVANCE_REFUND_NONE.InnerText = "N";
                XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
                PAGENO.InnerText = "0";
                XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");

                string narration = txtNarration.Text.Replace("'", "''");
                //PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Party <" + txtServiceName.Text + ">";    //THIS LINE COMMENT BY TANU

                PARTICULARS.InnerText = "JV Against Transaction_No<" + lblBillId.Text + "> Party <" + ddlEmployee.SelectedItem.Text + ">";    //THIS LINE COMMENT BY TANU



                //if (txtServiceName.Text != string.Empty)
                //{
                //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Party <" + txtServiceName.Text + ">";
                //}
                //else
                //{
                //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + ">";
                //}
                //else 
                //if (txtServiceName.Text != string.Empty)
                //{
                //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Party <" + txtServiceName.Text + ">";
                //}
                //else
                //{
                //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + ">";
                //}
                //else if (txtInvoiceDate.Text != string.Empty)
                //{
                //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Dated <" + txtInvoiceDate.Text + ">";
                //}
               

                XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
                COLLEGE_CODE.InnerText = Session["colcode"].ToString();

                XmlElement USER = objXMLDoc.CreateElement("USER");
                USER.InnerText = Session["userno"].ToString().Trim();

                XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
                CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");
                XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
                XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");

                XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
                XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

                XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
                XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
                DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();


                XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
                IsIGSTApplicable.InnerText = "0";
                XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
                XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
                XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

                IGSTPER.InnerText = "0";
                IGSTAMOUNT.InnerText = "0";
                IGSTonAmount.InnerText = "0";
                XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
                XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
                XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
                XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
                XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
                XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

                XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
                SGSTApplicable.InnerText = "0";
                CGSTper.InnerText = "0";
                CGSTamount.InnerText = "0";
                CGSTonamount.InnerText = "0";


                SGSTper.InnerText = "0";
                SGSTamount.InnerText = "0";
                SGSTonamount.InnerText = "0";

                TDSonGSTSection.InnerText = "0";
                TDSGSTonAMOUNT.InnerText = "0";
                TDSonGSTPersentage.InnerText = "0";

                TDSonCGSTSection.InnerText = "0";
                TDSCGSTonAMOUNT.InnerText = "0";
                TDSonCGSTPercentage.InnerText = "0";

                TDSonSGSTSection.InnerText = "0";
                TDSSGSTonAMOUNT.InnerText = "0";
                TDSonSGSTPercentage.InnerText = "0";

                SecurityAmt.InnerText = "0";


                XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");

                if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
                {
                    GSTIN_NO.InnerText = txtGSTINNo.Text;
                }
                GSTIN_NO.InnerText = "-";


                string voucherNo1 = string.Empty;

                if (objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'") == "Y")
                {
                    voucherNo1 = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString().Trim() + "_TRANS", "ISNULL(MAX(cast(voucher_no as int)),0)+1", "TRANSACTION_DATE<=convert(datetime,'" + Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy") + "',112) and TRANSACTION_TYPE='J'");

                    VOUCHER_NO.InnerText = voucherNo1;
                    ViewState["VoucherNo"] = voucherNo1;

                    STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherNo1;//  txtVoucherNo.Text.ToString().Trim();
                }

                STR_CB_VOUCHER_NO.InnerText = StrVno;

                //ProjectId.InnerText = "0";
                ProjectId.InnerText = ddlSponsor.SelectedValue;

                HiddenField hdnSubproject = new HiddenField();
                hdnSubproject.Value = "";

                ProjectSubId.InnerText = hdnSubproject.Value == "" ? "0" : hdnSubproject.Value;
                ProjectSubId.InnerText = ddlProjSubHead.SelectedValue;
                BILL_ID.InnerText = lblBillId.Text.ToString();

                objElement.AppendChild(SUBTR_NO);
                objElement.AppendChild(TRANSACTION_DATE);
                objElement.AppendChild(TRANSACTION_TYPE);
                objElement.AppendChild(OPARTY);
                objElement.AppendChild(PARTY_NO);
                objElement.AppendChild(TRAN);
                objElement.AppendChild(AMOUNT);
                objElement.AppendChild(DEGREE_NO);
                objElement.AppendChild(TRANSFER_ENTRY);
                objElement.AppendChild(CBTYPE_STATUS);
                objElement.AppendChild(CBTYPE);
                objElement.AppendChild(RECIEPT_PAYMENT_FEES);
                objElement.AppendChild(REC_NO);
                objElement.AppendChild(CHQ_NO);
                objElement.AppendChild(CHQ_DATE);
                objElement.AppendChild(CHALLAN);
                objElement.AppendChild(CAN);
                objElement.AppendChild(DCR_NO);
                objElement.AppendChild(CC_ID);
                objElement.AppendChild(BudgetNo);
                objElement.AppendChild(IsTDSApplicable);
                objElement.AppendChild(IsTDSonGSTApplicable);
                objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
                objElement.AppendChild(IsSecurityApplicable);
                objElement.AppendChild(AmtWithoutGST);
                objElement.AppendChild(GSTPercent);
                objElement.AppendChild(IsGSTApplicable);
                objElement.AppendChild(Section);
                objElement.AppendChild(PARTY_NAME);
                objElement.AppendChild(PAN_NO);
                objElement.AppendChild(NATURE_SERVICE);
                objElement.AppendChild(CASH_BANK_NO);
                objElement.AppendChild(ADVANCE_REFUND_NONE);
                objElement.AppendChild(PAGENO);
                objElement.AppendChild(PARTICULARS);
                objElement.AppendChild(COLLEGE_CODE);
                objElement.AppendChild(USER);
                objElement.AppendChild(CREATED_MODIFIED_DATE);
                objElement.AppendChild(VOUCHER_NO);
                objElement.AppendChild(STR_VOUCHER_NO);
                objElement.AppendChild(STR_CB_VOUCHER_NO);
                objElement.AppendChild(ProjectId);
                objElement.AppendChild(ProjectSubId);
                objElement.AppendChild(BILL_ID);

                objElement.AppendChild(TDSSection);
                objElement.AppendChild(TDSAMOUNT);
                objElement.AppendChild(TDPersentage);
                objElement.AppendChild(TDSonGSTSection);
                objElement.AppendChild(TDSGSTonAMOUNT);
                objElement.AppendChild(TDSonGSTPersentage);

                objElement.AppendChild(TDSonCGSTSection);
                objElement.AppendChild(TDSCGSTonAMOUNT);
                objElement.AppendChild(TDSonCGSTPercentage);

                objElement.AppendChild(TDSonSGSTSection);
                objElement.AppendChild(TDSSGSTonAMOUNT);
                objElement.AppendChild(TDSonSGSTPercentage);

                objElement.AppendChild(SecurityAmt);


                objElement.AppendChild(DepartmentId);

                objElement.AppendChild(IsIGSTApplicable);
                objElement.AppendChild(IGSTAMOUNT);
                objElement.AppendChild(IGSTPER);
                objElement.AppendChild(IGSTonAmount);
                objElement.AppendChild(CGSTamount);
                objElement.AppendChild(CGSTper);
                objElement.AppendChild(CGSTonamount);
                objElement.AppendChild(SGSTamount);
                objElement.AppendChild(SGSTper);
                objElement.AppendChild(SGSTonamount);
                objElement.AppendChild(SGSTApplicable);
                objElement.AppendChild(GSTIN_NO);


                objXMLDoc.DocumentElement.AppendChild(objElement);
            }
            //if (chkIGST.Checked)
            //{
            //    objXMLDoc = AddIgsTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //}
            //if (chkGST.Checked)
            //{
            //    objXMLDoc = AddSGsttable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //    objXMLDoc = AddCGsttable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //}
            //if (chkTDSOnGst.Checked)
            //{
            //    objXMLDoc = AddTDSonGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //}
            //if (chkTdsOnCGSTSGST.Checked)
            //{
            //    objXMLDoc = AddTDSonCGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //    objXMLDoc = AddTDSonSGSTTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //}
            //if (chkSecurity.Checked)
            //{
            //    objXMLDoc = AddSecurityTable(objXMLDoc, ViewState["VoucherNo"].ToString());
            //}

            objXMLDoc = ConsolidateTransactionEntry1(objXMLDoc, ViewState["VoucherNo"].ToString());

            string IsModify = string.Empty;
            int VoucherSqn = 0;
            int voucherno;
            IsModify = "N";
            VoucherSqn = 0;
            voucherno = objPC1.AddTransactionWithXML(objXMLDoc, Session["BillComp_Code"].ToString().Trim(), IsModify, VoucherSqn, Session["fin_yr"].ToString().Trim(), "J");

            Session["vchno"] = voucherno.ToString();

            if (ddlBudgethead.SelectedIndex > 0)
            {
                objPC1.AddBudgetTransaction(Session["BillComp_Code"].ToString().Trim(), Convert.ToString(voucherno), "J");
            }



            //DataSet dsResult = objPC1.GetTransactionResult(voucherno, Session["BillComp_Code"].ToString(), "J");
            //if (objCommon.LookUp("acc_" + Session["BillComp_Code"].ToString() + "_config", "PARAMETER", "CONFIGDESC='ENABLE CHEQUE PRINTING'") == "N")
            //{
            //    //btnchequePrint.Visible = false;
            //}
            //else
            //{
            //    string tranno = dsResult.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
            //    string partyName = dsResult.Tables[0].Rows[0]["LEDGER"].ToString();
            //    partyName = ViewState["BankName"].ToString();
            //    string chqno2 = "0";
            //    string amount = string.Empty;
            //    if (Convert.ToDecimal(dsResult.Tables[0].Rows[0]["DEBIT"].ToString()) > 0)
            //        amount = dsResult.Tables[0].Rows[0]["DEBIT"].ToString();
            //    else
            //        amount = dsResult.Tables[0].Rows[0]["CREDIT"].ToString();

            //    string CHQ_NO = dsResult.Tables[0].Rows[0]["CHQ_NO"].ToString();

            //    //btnchequePrint.Attributes.Add("onclick", "ShowChequePrintingTran('" + chqno2.ToString() + "','" + tranno + "','" + partyName + "','" + amount + "','0','" + CHQ_NO + "')");
            //}

            //if (dsResult != null)
            //{
            //    if (dsResult.Tables[0].Rows.Count != 0)
            //    {
            //        //lvGrp.DataSource = dsResult.Tables[0];
            //        //lvGrp.DataBind();                   
            //        //if (isvoucher_Cheque_Print == "Y")
            //            //upd_ModalPopupExtender1.Show();
            //        objPC1.UpdateBalanceAllLedger();                  
            //    }
            //    else
            //    {
            //        objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
            //    }

            //}
            ////txtApproveRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UPDLedger, "Transaction Not Performed Successfully", this.Page);
        }

    }

    private XmlDocument ConsolidateTransactionEntry1(XmlDocument objXMLDoc, string voucherno)
    {
        AccountTransaction objPC = new AccountTransaction();

        string opartystring = string.Empty;
        XmlElement objElement = objXMLDoc.CreateElement("Table");
        XmlElement SUBTR_NO = objXMLDoc.CreateElement("SUBTR_NO");
        SUBTR_NO.InnerText = "1";
        XmlElement PARTICULARS = objXMLDoc.CreateElement("PARTICULARS");
        XmlElement OPARTY = objXMLDoc.CreateElement("OPARTY");
        XmlElement TRANSACTION_DATE = objXMLDoc.CreateElement("TRANSACTION_DATE");

        XmlElement TRANSACTION_TYPE = objXMLDoc.CreateElement("TRANSACTION_TYPE");
        XmlElement TRAN = objXMLDoc.CreateElement("TRAN");
        XmlElement PARTY_NO = objXMLDoc.CreateElement("PARTY_NO");

        XmlElement AMOUNT = objXMLDoc.CreateElement("AMOUNT");
        XmlElement DEGREE_NO = objXMLDoc.CreateElement("DEGREE_NO");
        XmlElement VOUCHER_NO = objXMLDoc.CreateElement("VOUCHER_NO");

        XmlElement STR_VOUCHER_NO = objXMLDoc.CreateElement("STR_VOUCHER_NO");
        XmlElement STR_CB_VOUCHER_NO = objXMLDoc.CreateElement("STR_CB_VOUCHER_NO");
        XmlElement TRANSFER_ENTRY = objXMLDoc.CreateElement("TRANSFER_ENTRY");

        XmlElement CBTYPE_STATUS = objXMLDoc.CreateElement("CBTYPE_STATUS");
        XmlElement CBTYPE = objXMLDoc.CreateElement("CBTYPE");
        XmlElement RECIEPT_PAYMENT_FEES = objXMLDoc.CreateElement("RECIEPT_PAYMENT_FEES");


        XmlElement REC_NO = objXMLDoc.CreateElement("REC_NO");
        XmlElement CHQ_NO = objXMLDoc.CreateElement("CHQ_NO");
        XmlElement CHQ_DATE = objXMLDoc.CreateElement("CHQ_DATE");


        XmlElement CHALLAN = objXMLDoc.CreateElement("CHALLAN");
        XmlElement CAN = objXMLDoc.CreateElement("CAN");

        XmlElement ProjectId = objXMLDoc.CreateElement("ProjectId");
        XmlElement ProjectSubId = objXMLDoc.CreateElement("ProjectSubId");

        XmlElement BILL_ID = objXMLDoc.CreateElement("BILL_ID");
        XmlElement AmtWithoutGST = objXMLDoc.CreateElement("AmtWithoutGST");
        XmlElement GSTPercent = objXMLDoc.CreateElement("GSTPercent");
        XmlElement IsGSTApplicable = objXMLDoc.CreateElement("IsGSTApplicable");
        XmlElement Section = objXMLDoc.CreateElement("SECTION");

        XmlElement TDSonGSTSection = objXMLDoc.CreateElement("TDSonGSTSection");
        XmlElement TDSGSTonAMOUNT = objXMLDoc.CreateElement("TDSGSTonAMOUNT");
        XmlElement TDSonGSTPersentage = objXMLDoc.CreateElement("TDSonGSTPersentage");
        XmlElement IsTDSonGSTApplicable = objXMLDoc.CreateElement("IsTDSonGSTApplicable");

        XmlElement IsTDSonCGSTSGSTApplicable = objXMLDoc.CreateElement("IsTDSonCGSTSGSTApplicable");
        XmlElement IsSecurityApplicable = objXMLDoc.CreateElement("IsSecurityApplicable");

        XmlElement TDSonCGSTSection = objXMLDoc.CreateElement("TDSonCGSTSection");
        XmlElement TDSCGSTonAMOUNT = objXMLDoc.CreateElement("TDSCGSTonAMOUNT");
        XmlElement TDSonCGSTPercentage = objXMLDoc.CreateElement("TDSonCGSTPercentage");

        XmlElement TDSonSGSTSection = objXMLDoc.CreateElement("TDSonSGSTSection");
        XmlElement TDSSGSTonAMOUNT = objXMLDoc.CreateElement("TDSSGSTonAMOUNT");
        XmlElement TDSonSGSTPercentage = objXMLDoc.CreateElement("TDSonSGSTPercentage");

        XmlElement SecurityAmt = objXMLDoc.CreateElement("SecurityAmt");

        IsTDSonCGSTSGSTApplicable.InnerText = "0";
        IsSecurityApplicable.InnerText = "0";

        TDSonCGSTSection.InnerText = "0";
        TDSCGSTonAMOUNT.InnerText = "0";
        TDSonCGSTPercentage.InnerText = "0";
        TDSonSGSTSection.InnerText = "0";
        TDSSGSTonAMOUNT.InnerText = "0";
        TDSonSGSTPercentage.InnerText = "0";
        SecurityAmt.InnerText = "0";

        IsTDSonGSTApplicable.InnerText = "0";
        TDSonGSTPersentage.InnerText = "0";
        TDSGSTonAMOUNT.InnerText = "0";
        TDSonGSTSection.InnerText = "0";

        IsGSTApplicable.InnerText = "0";
        Section.InnerText = "0";

        XmlElement PARTY_NAME = objXMLDoc.CreateElement("PARTY_NAME");
        if (txtPayeeNameAddress.Text == "" || txtPayeeNameAddress.Text == string.Empty)
        {
            PARTY_NAME.InnerText = "-";
        }
        else
        {
            PARTY_NAME.InnerText = txtPayeeNameAddress.Text.Trim().ToString();
        }

        XmlElement PAN_NO = objXMLDoc.CreateElement("PAN_NO");
        if (txtPanNo.Text == "" || txtPanNo.Text == string.Empty)
        {
            PAN_NO.InnerText = "-";
        }
        else
        {
            PAN_NO.InnerText = txtPanNo.Text.Trim().ToString();
        }

        XmlElement NATURE_SERVICE = objXMLDoc.CreateElement("NATURE_SERVICE");
        if (txtNatureOfService.Text == "" || txtNatureOfService.Text == string.Empty)
        {
            NATURE_SERVICE.InnerText = "-";
        }
        else
        {
            NATURE_SERVICE.InnerText = txtNatureOfService.Text.Trim().ToString().Replace("'", "''");
        }

        ViewState["BankName"] = txtLedgerHead.Text.ToString().Split('*')[0].ToString();
        int i = 0;
        string narration = txtNarration.Text.Trim().Replace("'", "''");
    //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Party <" + txtServiceName.Text + ">";  TANU
        PARTICULARS.InnerText = "JV Against Transaction_No<" + lblBillId.Text + "> Party <" + ddlEmployee.SelectedItem.Text + ">"; 

        //if (txtServiceName.Text != string.Empty)
        //{
        //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + "> Party <" + txtServiceName.Text + ">";
        //}
        //else
        //{
        //    PARTICULARS.InnerText = "JV Against Transaction_No<" + lblSerialNo.Text + ">";
        //}
        OPARTY.InnerText = Convert.ToInt32(txtExpenseLedger.Text.Split('*')[1] == "" ? "0" : txtExpenseLedger.Text.Split('*')[1]).ToString();
        //OPARTY.InnerText = ViewState["OPartyNo"].ToString();
        TRANSACTION_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy").Trim();
        TRANSACTION_TYPE.InnerText = "J";

        TRAN.InnerText = "Cr";

        XmlElement TDSSection = objXMLDoc.CreateElement("TDSSection");
        XmlElement TDSAMOUNT = objXMLDoc.CreateElement("TDSAMOUNT");
        XmlElement TDPersentage = objXMLDoc.CreateElement("TDPersentage");


        TDSSection.InnerText = "0";
        TDSAMOUNT.InnerText = "0";
        TDPersentage.InnerText = "0";
        PARTY_NO.InnerText = Convert.ToInt32(txtLedgerHead.Text.Split('*')[1] == "" ? "0" : txtLedgerHead.Text.Split('*')[1]).ToString();
        if (chkTDSApplicable.Checked)
        {
            AMOUNT.InnerText = txtBillAmt.Text.ToString();//Convert.ToDouble(ViewState["Amount"].ToString()).ToString();
            AmtWithoutGST.InnerText = "0";
            GSTPercent.InnerText = "0";
            TDSSection.InnerText = "0";
            TDSAMOUNT.InnerText = ViewState["TDSAmount"].ToString();
            TDPersentage.InnerText = "0";
        }
        else
        {
            AMOUNT.InnerText = txtBillAmt.Text.ToString();//Convert.ToDouble(ViewState["Amount"].ToString()).ToString();
            AmtWithoutGST.InnerText = "0";
            GSTPercent.InnerText = "0";
        }
        DEGREE_NO.InnerText = "0";

        VOUCHER_NO.InnerText = voucherno;
        STR_CB_VOUCHER_NO.InnerText = StrVno;

        STR_VOUCHER_NO.InnerText = Session["BillComp_Code"].ToString().Trim() + "/P" + voucherno.ToString();// txtVoucherNo.Text.ToString().Trim();

        TRANSFER_ENTRY.InnerText = "0";
        CBTYPE_STATUS.InnerText = "0";
        CBTYPE.InnerText = "0";
        RECIEPT_PAYMENT_FEES.InnerText = "0";
        REC_NO.InnerText = "0";

        CHQ_NO.InnerText = "0";

        CHQ_DATE.InnerText = Convert.ToDateTime(txtTransDate.Text).ToString("dd-MMM-yyyy");

        CHALLAN.InnerText = "false";
        CAN.InnerText = "false";

        XmlElement DCR_NO = objXMLDoc.CreateElement("DCR_NO");
        DCR_NO.InnerText = "0";

        XmlElement CC_ID = objXMLDoc.CreateElement("CC_ID");
        CC_ID.InnerText = "0";

        XmlElement BudgetNo = objXMLDoc.CreateElement("BudgetNo");
        BudgetNo.InnerText = "0";
        XmlElement IsTDSApllicalbe = objXMLDoc.CreateElement("IsTDSApplicable");
        IsTDSApllicalbe.InnerText = "0";
        XmlElement CASH_BANK_NO = objXMLDoc.CreateElement("CASH_BANK_NO");
        CASH_BANK_NO.InnerText = "0";

        XmlElement ADVANCE_REFUND_NONE = objXMLDoc.CreateElement("ADVANCE_REFUND_NONE");
        ADVANCE_REFUND_NONE.InnerText = "N";
        XmlElement PAGENO = objXMLDoc.CreateElement("PAGENO");
        PAGENO.InnerText = "0";

        XmlElement COLLEGE_CODE = objXMLDoc.CreateElement("COLLEGE_CODE");
        COLLEGE_CODE.InnerText = Session["colcode"].ToString();
        XmlElement USER = objXMLDoc.CreateElement("USER");
        USER.InnerText = Session["userno"].ToString().Trim();
        XmlElement CREATED_MODIFIED_DATE = objXMLDoc.CreateElement("CREATED_MODIFIED_DATE");
        CREATED_MODIFIED_DATE.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        XmlElement DepartmentId = objXMLDoc.CreateElement("DepartmentId");
        DepartmentId.InnerText = ddlDeptBranch.SelectedValue.ToString();

        //ADDEDE BY VIJAY ON 02092020 FOR GST&IGST    
        XmlElement IsIGSTApplicable = objXMLDoc.CreateElement("IsIGSTApplicable");
        IsIGSTApplicable.InnerText = "0";
        XmlElement IGSTAMOUNT = objXMLDoc.CreateElement("IGSTAMOUNT");
        XmlElement IGSTPER = objXMLDoc.CreateElement("IGSTPER");
        XmlElement IGSTonAmount = objXMLDoc.CreateElement("IGSTonAmount");

        IGSTPER.InnerText = "0";
        IGSTAMOUNT.InnerText = "0";
        IGSTonAmount.InnerText = "0";
        XmlElement CGSTamount = objXMLDoc.CreateElement("CGSTamount");
        XmlElement CGSTper = objXMLDoc.CreateElement("CGSTper");
        XmlElement CGSTonamount = objXMLDoc.CreateElement("CGSTonamount");
        XmlElement SGSTamount = objXMLDoc.CreateElement("SGSTamount");
        XmlElement SGSTper = objXMLDoc.CreateElement("SGSTper");
        XmlElement SGSTonamount = objXMLDoc.CreateElement("SGSTonamount");

        XmlElement SGSTApplicable = objXMLDoc.CreateElement("CGSTApplicable");
        SGSTApplicable.InnerText = "0";
        CGSTper.InnerText = "0";
        CGSTamount.InnerText = "0";
        CGSTonamount.InnerText = "0";


        SGSTper.InnerText = "0";
        SGSTamount.InnerText = "0";
        SGSTonamount.InnerText = "0";




        XmlElement GSTIN_NO = objXMLDoc.CreateElement("GSTIN_NO");


        if (txtGSTINNo.Text != "" || txtGSTINNo.Text != string.Empty)
        {
            GSTIN_NO.InnerText = txtGSTINNo.Text;
        }
        GSTIN_NO.InnerText = "-";

        //ProjectId.InnerText = "0";
        //ProjectSubId.InnerText = "0";
        ProjectId.InnerText = ddlSponsor.SelectedValue;
        ProjectSubId.InnerText = ddlProjSubHead.SelectedValue;

        BILL_ID.InnerText = lblBillId.Text;

        objElement.AppendChild(SUBTR_NO);
        objElement.AppendChild(TRANSACTION_DATE);
        objElement.AppendChild(TRANSACTION_TYPE);
        objElement.AppendChild(OPARTY);
        objElement.AppendChild(PARTY_NO);

        objElement.AppendChild(TRAN);
        objElement.AppendChild(AMOUNT);
        objElement.AppendChild(DEGREE_NO);
        objElement.AppendChild(TRANSFER_ENTRY);
        objElement.AppendChild(CBTYPE_STATUS);

        objElement.AppendChild(CBTYPE);
        objElement.AppendChild(RECIEPT_PAYMENT_FEES);
        objElement.AppendChild(REC_NO);
        objElement.AppendChild(CHQ_NO);
        objElement.AppendChild(CHQ_DATE);

        objElement.AppendChild(CHALLAN);
        objElement.AppendChild(CAN);
        objElement.AppendChild(DCR_NO);
        objElement.AppendChild(CC_ID);
        objElement.AppendChild(BudgetNo);
        objElement.AppendChild(IsTDSApllicalbe);
        objElement.AppendChild(IsTDSonGSTApplicable);
        objElement.AppendChild(IsTDSonCGSTSGSTApplicable);
        objElement.AppendChild(IsSecurityApplicable);
        objElement.AppendChild(AmtWithoutGST);
        objElement.AppendChild(GSTPercent);
        objElement.AppendChild(IsGSTApplicable);
        objElement.AppendChild(Section);
        objElement.AppendChild(PARTY_NAME);
        objElement.AppendChild(PAN_NO);
        objElement.AppendChild(NATURE_SERVICE);
        objElement.AppendChild(CASH_BANK_NO);

        objElement.AppendChild(ADVANCE_REFUND_NONE);
        objElement.AppendChild(PAGENO);
        objElement.AppendChild(PARTICULARS);
        objElement.AppendChild(COLLEGE_CODE);
        objElement.AppendChild(USER);

        objElement.AppendChild(CREATED_MODIFIED_DATE);
        objElement.AppendChild(VOUCHER_NO);
        objElement.AppendChild(STR_VOUCHER_NO);
        objElement.AppendChild(STR_CB_VOUCHER_NO);

        objElement.AppendChild(ProjectId);
        objElement.AppendChild(ProjectSubId);
        objElement.AppendChild(BILL_ID);

        objElement.AppendChild(TDSSection);
        objElement.AppendChild(TDSAMOUNT);
        objElement.AppendChild(TDPersentage);
        objElement.AppendChild(TDSonGSTSection);
        objElement.AppendChild(TDSGSTonAMOUNT);
        objElement.AppendChild(TDSonGSTPersentage);
        objElement.AppendChild(TDSonCGSTSection);
        objElement.AppendChild(TDSCGSTonAMOUNT);
        objElement.AppendChild(TDSonCGSTPercentage);

        objElement.AppendChild(TDSonSGSTSection);
        objElement.AppendChild(TDSSGSTonAMOUNT);
        objElement.AppendChild(TDSonSGSTPercentage);

        objElement.AppendChild(SecurityAmt);
        objElement.AppendChild(DepartmentId);

        objElement.AppendChild(IsIGSTApplicable);
        objElement.AppendChild(IGSTAMOUNT);
        objElement.AppendChild(IGSTPER);
        objElement.AppendChild(IGSTonAmount);
        objElement.AppendChild(CGSTamount);
        objElement.AppendChild(CGSTper);
        objElement.AppendChild(CGSTonamount);
        objElement.AppendChild(SGSTamount);
        objElement.AppendChild(SGSTper);
        objElement.AppendChild(SGSTonamount);
        objElement.AppendChild(SGSTApplicable);
        objElement.AppendChild(GSTIN_NO);

        objXMLDoc.DocumentElement.AppendChild(objElement);
        return objXMLDoc;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        dvSection.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        pnlBillList.Visible = true;

        lvNewBills.DataSource = null;
        lvNewBills.DataBind();
        pnlNewBills.Visible = false;
        DeleteDirecPath(Docpath + "TEMPUPLOAD_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]));
        ViewState["letrno"] = null;
        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        ViewState["RaisePayNo"] = null;


        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        btnfillSave.Visible = false;
        btnBack.Visible = false;


         Diveservices.Visible = false;
         DivServicesText.Visible = false;
       
        
        //Clear();
    }
    //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    double per = Convert.ToDouble(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSection.SelectedValue)));
    //    if (per > 0)
    //    {
    //        txtTDSPer.Text = per.ToString();
    //    }
    //    else
    //    {
    //        txtTDSPer.Text = "";
    //    }
    //}
    //protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    //{
    //    double CalcTDS = 0;
    //    if (chkTDSApplicable.Checked)
    //    {
    //        dvSection.Visible = true;
    //        if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
    //        {
    //            if (ddlSection.SelectedValue != "0")
    //            {
    //                CalcTDS = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
    //                txtTDSAmt.Text = CalcTDS.ToString();
    //            }
    //            if (txtGSTAmount.Text != "")
    //            {
    //                txtNetAmt.Text = Math.Round((Convert.ToDouble(txtBillAmt.Text) + Convert.ToDouble(txtGSTAmount.Text)) - Convert.ToDouble(txtTDSAmt.Text)).ToString();
    //            }
    //        }
    //    }
    //    else
    //    {

    //        txtNetAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNetAmt.Text == "" ? "0" : txtNetAmt.Text)) + (Convert.ToDouble(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text))).ToString();
    //        dvSection.Visible = false;
    //        txtTDSAmt.Text = txtTDSPer.Text = txtPanNo.Text = string.Empty;
    //        ddlSection.SelectedValue = "0";



    //    }
    //}
    protected void rdbTDS_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtLedgerHead_TextChanged(object sender, EventArgs e)
    {

        //Added by vijay andoju on 27102020 for validation for ledger head selection
        if (CheckValidLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Ledger From List..!", this.Page);
            txtLedgerHead.Text = lblLedgerClBal.Text = string.Empty;
            return;
        }
        string partyNo = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtLedgerHead.Text.ToString().Trim().Split('*')[1] + "'");
        PartyController objPC = new PartyController();
        DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["BillComp_Code"].ToString());
        if (dtr.Read())
        {

            if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
            }
            else
            {
                lblLedgerClBal.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";
            }
        }
        txtInvoiceNo.Focus();
    }
    //Addede by vijay andoju on 27102020
    protected int CheckValidLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtLedgerHead.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected int CheckValidExpenseLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtExpenseLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void ddlBudgethead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBudgethead.SelectedValue == "0")
            {
                return;
            }
            else
            {

                string Amount = string.Empty;
                DataSet ds = null;

                string company_code = objCommon.LookUp("acc_company", "COMPANY_CODE", "COMPANY_NO=" + ddlSelectCompany.SelectedValue);
                ds = objRPBController.GetBudegetBalanceNEW(Convert.ToInt32(ddlDeptBranch.SelectedValue), Convert.ToInt32(ddlBudgethead.SelectedValue), DateTime.Today);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblBudgetClBal.Text = String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[0]["BALANCE"].ToString()));
                    }
                    else
                    {
                        lblBudgetClBal.Text = "0.00";
                    }
                }
                txtLedgerHead.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    
    }
    protected void ddlDeptBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSelectCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        string IsCompanySelected = ddlSelectCompany.SelectedValue;
        if (IsCompanySelected == "0")
        {

        }
        else
        {
            txtGSTAmount.Text = string.Empty;
            txtLedgerHead.Text = string.Empty;
            txtTDSLedger.Text = string.Empty;
            txtBankLedger.Text = string.Empty;
            lblLedgerClBal.Text = "0";
            lblBudgetClBal.Text = "0";
            //string Company_Code = Session["Comp_Code"].ToString();

            string Comp_code = objCommon.LookUp("Acc_Company", "Company_Code", "Company_No = " + Convert.ToInt32(ddlSelectCompany.SelectedValue));
            Session["BillComp_Code"] = Comp_code;
            BindSection();
            ddlAccount.Items.Clear();
            int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Comp_code + "_Comp_Account_Master", "Count(*)", ""));
            if (count > 0)
            {
                objCommon.FillDropDownList(ddlAccount, "ACC_" + Comp_code + "_Comp_Account_Master", "Acc_Id", "Account_Name", "", "Acc_Id");
                BindBudget();
            }
            else
            {
                ddlAccount.Items.Clear();
            }
            SetParameters(Comp_code);
            if (IsSponsorProject == "Y")
            {
                trSponsor.Visible = true;
                objCommon.FillDropDownList(ddlSponsor, "Acc_" + Comp_code + "_Project", "ProjectId", "ProjectName", "", "");
            }
            else
            {
                //trSponsor.Visible = false;
                trSponsor.Attributes.Add("style", "display:none");
                trSubHead.Attributes.Add("style", "display:none");
            }

            //added by tanu 07/04/2022
            int bill_Id = Convert.ToInt32(objCommon.LookUp("ACC_RAISING_PAYMENT_BILL", "ISNULL(MAX(bill_id),0) + 1", "COMPANY_CODE='" + (Session["BillComp_Code"]).ToString() + "'"));
            ViewState["bill_Id"] = bill_Id.ToString();
            lblBillId.Text = ViewState["bill_Id"].ToString();

        }
    }
    #endregion

    #region Web Service

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            //AutoCompleteController objAutocomplete = new AutoCompleteController();
            RaisingPaymentBillController objPayBill = new RaisingPaymentBillController();
            ds = objPayBill.GetAccountEntryLedger(prefixText);

            if (ds.Tables[0].Rows.Count == 0)
            {
               
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
                }
            }
            
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            //AutoCompleteController objAutocomplete = new AutoCompleteController();
            RaisingPaymentBillController objPayBill = new RaisingPaymentBillController();
            ds = objPayBill.GetAccountEntryCashBank(prefixText);
            if ((ds.Tables[0].Rows.Count == 0) || (ds == null))
            {
                
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
                }

            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    #endregion
    private void SetParameters(string comp_code)
    {
        objCommon = new Common();
        DataSet ds = new DataSet();
        // ds = objCommon.FillDropDown("ACC_"+Session["BillComp_Code"]+"_CONFIG", "PARAMETER", "CONFIGDESC", string.Empty, "CONFIGID");
        ds = objCommon.FillDropDown("ACC_" + comp_code + "_CONFIG", "PARAMETER", "CONFIGDESC", "", "CONFIGID");
        int i = 0;
        if (ds != null)
        {
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                if (ds.Tables[0].Rows[i]["CONFIGDESC"].ToString().Trim() == "ALLOW SPONSOR PROJECT")
                {
                    IsSponsorProject = ds.Tables[0].Rows[i]["PARAMETER"].ToString().Trim();
                }

            }
        }
    }
    protected void chkGST_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGST.Checked == true)
        {

            if ((txtBillAmt.Text == "") || (txtBillAmt.Text == "NaN"))
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
                txtBillAmt.Focus();
                chkGST.Checked = false;
                return;
            }
        }

        txtIgstAmount.Text = txtIgstPer.Text = "0";
        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = "0";
        txtTDSGSTonAmount.Text = txtTDSonGSTPer.Text = txtTDSonGSTAmount.Text = "0";
        ddlTDSonGSTSection.SelectedIndex = 0;
        txtGSTAmount.Text = "0";

        if (chkGST.Checked == true)
        {        
            chkIGST.Checked = false;
            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            dvSgst.Visible = true;
            dvCgst.Visible = true;
            dvIGST.Visible = false;
            dvGSTLedger.Visible = true;
            dvIgstledger.Visible = false;
            double gstAmount = Convert.ToDouble(Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text));
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();
            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        else
        {
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            dvGSTLedger.Visible = false;
            chkTdsOnCGSTSGST.Checked = false;
            divTDSOnSGST.Visible = false;
            divTdsOnCGST.Visible = false;
            txtTDSCGSTonAmount.Text = txtTDSonCGSTPer.Text = txtTDSonCGSTAmount.Text = "0";
            txtTDSSGSTonAmount.Text = txtTDSonSGSTPer.Text = txtTDSonSGSTAmount.Text = "0";
            ddlTDSonCGSTSection.SelectedIndex = 0;
            ddlTDSonSGSTSection.SelectedIndex = 0;

            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }
    protected void chkIGST_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIGST.Checked)
        {
            if (txtBillAmt.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
                txtBillAmt.Focus();
                chkIGST.Checked = false;
                return;
            }
        }

        txtCgstAmount.Text = txtCGSTPER.Text = txtSgstAmount.Text = txtSgstPer.Text = txtGSTAmount.Text = "0";
        txtTDSSGSTonAmount.Text = txtTDSonSGSTPer.Text = txtTDSonSGSTAmount.Text = "0";
        txtIgstPer.Text = txtIgstAmount.Text = "0";
        ddlTDSonSGSTSection.SelectedIndex = 0;
        txtGSTAmount.Text = "0";
        if (chkIGST.Checked == true)
        {
            chkGST.Checked = false;
            chkTdsOnCGSTSGST.Checked = false;
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            //added by  tanu 24/01/2022
            divTdsonSGstLedger.Visible = false;
            dvSgst.Visible = false;
            dvCgst.Visible = false;
            dvIGST.Visible = true;
            dvIgstledger.Visible = true;
            dvGSTLedger.Visible = false;
            double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text);
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        else
        {
            dvIGST.Visible = false;
            dvIgstledger.Visible = false;
            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            txtTDSGSTonAmount.Text = txtTDSonGSTPer.Text = txtTDSonGSTAmount.Text = "0";
            ddlTDSonGSTSection.SelectedIndex = 0;

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }

    protected void GetGstValue()
    {
        double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text) + Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text);

        txtGSTAmount.Text = gstAmount.ToString();
        if (txtBillAmt.Text != "")
            txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();

    }
    protected void AddGstAmount()
    {
        if (chkSecurity.Checked)
        {
            if (txtBillAmt.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
                txtBillAmt.Focus();
                chkSecurity.Checked = false;
                return;
            }
        }

        double gstAmount = Convert.ToDouble(txtIgstAmount.Text == "" ? "0" : txtIgstAmount.Text) + Convert.ToDouble(txtCgstAmount.Text == "" ? "0" : txtCgstAmount.Text) + Convert.ToDouble(txtSgstAmount.Text == "" ? "0" : txtSgstAmount.Text);
        double Tdsamount = Convert.ToDouble(txtTDSAmt.Text == "" ? "0" : txtTDSAmt.Text);
        double amount = Convert.ToDouble(txtBillAmt.Text == "" ? "0" : txtBillAmt.Text);

        double TdsonGstamount = Convert.ToDouble(txtTDSonGSTAmount.Text == "" ? "0" : txtTDSonGSTAmount.Text);
        double TdsonCGSTamount = Convert.ToDouble(txtTDSonCGSTAmount.Text == "" ? "0" : txtTDSonCGSTAmount.Text);
        double TdsonSGSTamount = Convert.ToDouble(txtTDSonSGSTAmount.Text == "" ? "0" : txtTDSonSGSTAmount.Text);
        double Securityamount = Convert.ToDouble(txtSecurityAmt.Text == "" ? "0" : txtSecurityAmt.Text);

        txtTotalBillAmt.Text = (Convert.ToDouble(txtBillAmt.Text) + gstAmount).ToString();
        txtNetAmt.Text = Convert.ToDouble(amount - (Tdsamount + TdsonGstamount + TdsonCGSTamount + TdsonSGSTamount + Securityamount) + gstAmount).ToString();
        txtTotalTDSAmt.Text = Convert.ToDouble(Tdsamount + TdsonGstamount + TdsonCGSTamount + TdsonSGSTamount + Securityamount).ToString();
        txtGSTAmount.Text = gstAmount.ToString();

    }

    protected void chkTDSApplicable_CheckedChanged(object sender, EventArgs e)
    {
        txtTDSAmt.Text = txtTDSPer.Text = "0";
        double CalcTDS = 0;
        if (chkTDSApplicable.Checked)
        {
            if (txtBillAmt.Text == "")
            {
                objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
                txtBillAmt.Focus();
                ddlSection.SelectedValue = "0";
                chkTDSApplicable.Checked = false;
                return;
            }

            txtTdsOnAmt.Text = txtBillAmt.Text;
            dvSection.Visible = true;
            tdslabel.Visible = true;
            tdstextbox.Visible = true;
            txtPanNo.Enabled = true;

            if (txtTDSPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlSection.SelectedValue != "0")
                {
                    CalcTDS = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSPer.Text)) * 0.01);
                    txtTDSAmt.Text = CalcTDS.ToString();
                    // txtTdsOnAmt.Text = CalcTDS.ToString();



                }
                //if (txtGSTAmount.Text != "")
                //{
                //    txtNetAmt.Text = Math.Round((Convert.ToDouble(txtBillAmt.Text) + Convert.ToDouble(txtGSTAmount.Text)) - Convert.ToDouble(txtTDSAmt.Text)).ToString();
                //}
            }
        }
        else
        {
            dvSection.Visible = false;
            tdslabel.Visible = false;
            tdstextbox.Visible = false;
            ddlSection.SelectedValue = "0";
        }

        AddGstAmount();
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSection.SelectedValue == "0")
        {
            txtTDSPer.Text = "0";
            txtTDSAmt.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>Per(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSPer.Text = "0";
            }
        }

    }
    protected void ddlIgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSgstSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chkTDSOnGst_CheckedChanged(object sender, EventArgs e)
    {
        //GetTdsAmt();
        //GetTdsOnIgstAmt();
        //if (chkTDSApplicable.Checked == false)
        //{
        //    MessageBox("Please Check TDS (Y/N)");
        //    return;
        //}
        if (chkTDSOnGst.Checked)
        {
            if (chkIGST.Checked == false)
            {
                MessageBox("Please Check IGST (Y/N)");
                chkTDSOnGst.Checked = false;
                return;
            }
        }

        double CalcTDSOnGST = 0;
        txtTDSGSTonAmount.Text = txtTDSonGSTAmount.Text = txtTDSonGSTPer.Text = "0";
        txtTDSonGSTLedger.Text = string.Empty;

        if (chkTDSOnGst.Checked)
        {
            txtTDSGSTonAmount.Text = txtBillAmt.Text;
            divTdsOnGst.Visible = true;
            divTdsonGstLedger.Visible = true;

            chkTdsOnCGSTSGST.Checked = false;
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            divTdsonCGstLedger.Visible = false;
            divTdsonSGstLedger.Visible = false;
            txtTDSonCGSTAmount.Text = txtTDSonSGSTAmount.Text = txtTDSonCGSTPer.Text = txtTDSonSGSTPer.Text = "0";
            txtTDSonCGSTLedger.Text = txtTDSonSGSTLedger.Text = string.Empty;

            //if (txtTDSonGSTPer.Text != "" && txtBillAmt.Text != "")
            //{
            //    if (ddlSection.SelectedValue != "0")
            //    {
            //        CalcTDSOnGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonGSTPer.Text)) * 0.01);
            //        txtTDSAmt.Text = CalcTDSOnGST.ToString();
            //        // txtTdsOnAmt.Text = CalcTDS.ToString();
            //    }

            //}
        }
        else
        {
            divTdsOnGst.Visible = false;
            ddlTDSonGSTSection.SelectedValue = "0";
            divTdsonGstLedger.Visible = false;
        }

        AddGstAmount();
    }
    protected void ddlTDSonGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       // if (txtTDSonGSTAmount.Text == "0" || txtTDSonGSTAmount.Text == "" || txtTDSonGSTAmount.Text == "0.00" || txtTDSonGSTAmount.Text == "0.0")
       // {
            if (ddlTDSonGSTSection.SelectedValue == "0")
            {
                txtTDSonGSTPer.Text = "0";
                txtTDSonGSTAmount.Text = "0";
                txtNetAmt.Text = txtBillAmt.Text;
            }
            else
            {
                decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonGSTSection.SelectedValue)));
                if (per > 0)
                {
                    txtTDSonGSTPer.Text = per.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonGst(" + per.ToString() + "," + 1 + ");</script>", false);
                }
                else
                {
                    txtTDSPer.Text = "0";
                }
            }
       // }
    }


    protected void txtBillAmt_TextChanged(object sender, EventArgs e)
    {

        if ((txtBillAmt.Text == "") || (txtBillAmt.Text == "NaN"))
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter Bill Amount", this.Page);
            txtBillAmt.Focus();           
            return;
        }

        txtTDSPer.Text = string.Empty;
        txtTDSAmt.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        txtTotalTDSAmt.Text = txtTotalTDSAmt.Text != "" ? txtTotalTDSAmt.Text : "0";
        txtGSTAmount.Text = txtGSTAmount.Text != "" ? txtGSTAmount.Text : "0";

        txtTDSonGSTPer.Text = string.Empty;
        txtTDSonGSTAmount.Text = string.Empty;
        ddlTDSonGSTSection.SelectedIndex = 0;

        txtTDSonCGSTPer.Text = string.Empty;
        txtTDSonCGSTAmount.Text = string.Empty;
        ddlTDSonCGSTSection.SelectedIndex = 0;

        txtTDSonSGSTPer.Text = string.Empty;
        txtTDSonSGSTAmount.Text = string.Empty;
        ddlTDSonSGSTSection.SelectedIndex = 0;

        txtGSTAmount.Text = string.Empty;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void chkTdsOnCGSTSGST_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkTDSApplicable.Checked == false)
        //{
        //    MessageBox("Please Check TDS (Y/N)");
        //    return;
        //}
        if (chkTdsOnCGSTSGST.Checked)
        {
            if (chkGST.Checked == false)
            {
                MessageBox("Please Check GST (Y/N)");
                chkTdsOnCGSTSGST.Checked = false;
                return;
            }
        }
        txtTDSonGSTLedger.Text = txtTDSonCGSTLedger.Text = txtTDSonSGSTLedger.Text = string.Empty;
        txtTDSonCGSTAmount.Text = txtTDSonSGSTAmount.Text = txtTDSonCGSTPer.Text = txtTDSonSGSTPer.Text = txtTDSonGSTAmount.Text = txtTDSonGSTPer.Text = "0";


        double CalcTDSOnCGSTSGST = 0;
        if (chkTdsOnCGSTSGST.Checked)
        {
            txtTDSCGSTonAmount.Text = txtTDSSGSTonAmount.Text = txtBillAmt.Text;
            divTDSOnSGST.Visible = true;
            divTdsOnCGST.Visible = true;

            divTdsonCGstLedger.Visible = true;
            divTdsonSGstLedger.Visible = true;

            chkTDSOnGst.Checked = false;
            divTdsOnGst.Visible = false;
            divTdsonGstLedger.Visible = false;

            if (txtTDSonCGSTPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlTDSonCGSTSection.SelectedValue != "0")
                {
                    CalcTDSOnCGSTSGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonCGSTPer.Text)) * 0.01);
                    txtTDSonCGSTAmount.Text = CalcTDSOnCGSTSGST.ToString();
                    // txtTdsOnAmt.Text = CalcTDS.ToString();
                }

            }
            if (txtTDSonSGSTPer.Text != "" && txtBillAmt.Text != "")
            {
                if (ddlTDSonSGSTSection.SelectedValue != "0")
                {
                    CalcTDSOnCGSTSGST = Math.Round(Convert.ToDouble(txtBillAmt.Text) * (Convert.ToDouble(txtTDSonSGSTPer.Text)) * 0.01);
                    txtTDSonSGSTAmount.Text = CalcTDSOnCGSTSGST.ToString();
                }

            }
        }
        else
        {
            ddlTDSonCGSTSection.SelectedValue = "0";
            ddlTDSonSGSTSection.SelectedValue = "0";
            divTdsOnCGST.Visible = false;
            divTDSOnSGST.Visible = false;
            divTdsonCGstLedger.Visible = false;
            divTdsonSGstLedger.Visible = false;
        }
        AddGstAmount();
    }
    protected void chkSecurity_CheckedChanged(object sender, EventArgs e)
    {
        txtSecurityAmt.Text = txtSecurityPer.Text = "0";

        if (chkSecurity.Checked == true)
        {
            divSecurity.Visible = true;
            divSecurityLedger.Visible = true;
        }
        else
        {
            divSecurity.Visible = false;
            divSecurityLedger.Visible = false;
            //  ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>CalNetAmountNew();</script>", false);
        }
        AddGstAmount();
    }
    protected void ddlTDSonCGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonCGSTSection.SelectedValue == "0")
        {
            txtTDSonCGSTPer.Text = "0";
            txtTDSonCGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonCGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonCGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonCGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSonCGSTPer.Text = "0";
            }
        }
    }
    protected void ddlTDSonSGSTSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTDSonSGSTSection.SelectedValue == "0")
        {
            txtTDSonSGSTPer.Text = "0";
            txtTDSonSGSTAmount.Text = "0";
            txtNetAmt.Text = txtBillAmt.Text;
        }
        else
        {
            decimal per = Convert.ToDecimal(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_SECTION", "SECTION_PERCENT", "SECTION_NO =" + Convert.ToInt32(ddlTDSonSGSTSection.SelectedValue)));
            if (per > 0)
            {
                txtTDSonSGSTPer.Text = per.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Key", "<script>PerTdsonSGst(" + per.ToString() + "," + 1 + ");</script>", false);
            }
            else
            {
                txtTDSonSGSTPer.Text = "0";
            }
        }
    }

    // Added by Vidisha on 13-05-2021 for multiple bill upload & download
    #region Upload Bills
    //Added By Vidisha on 12-05-2021 for multiple bill upload
    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG", ".jpeg", ".JPEG", ".pdf", ".PDF", ".Pdf", ".png", ".docx", ".PNG", ".XLS", ".xls", ".DOC", ".doc", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }
    public void AddDocuments(int RAISE_PAY_NO)
    {
        try
        {
            string sourcePath = string.Empty;
            string targetPath = string.Empty;
            DeletePath();
            int userno = Convert.ToInt32(Session["userno"]);

            string PATH = ViewState["DESTINATION_PATH"].ToString();

            sourcePath = ViewState["SOURCE_FILE_PATH"].ToString();
            targetPath = PATH + "\\BillNo_" + RAISE_PAY_NO;

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            foreach (var srcPath in Directory.GetFiles(sourcePath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
            }
            DeleteDirectory(sourcePath);
        }
        catch (Exception ex)
        {

        }
    }
    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            //Delete all files from the Directory
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            //Delete all child Directories
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }
            //Delete a Directory
            Directory.Delete(path);
        }
    }
    //This is used to add new Bills if any.
    protected void btnAddBill_Click(object sender, EventArgs e)
    {
        try
        {
            string file = string.Empty;
            int userno = Convert.ToInt32(Session["userno"]);

            if (FileUploadBill.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUploadBill.FileName)))
                {
                    if (FileUploadBill.FileContent.Length >= 5 * 1024 * 1024)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Bill Size Should Not Be Greater Than 5 Mb!", this.Page);
                        FileUploadBill.Dispose();
                        FileUploadBill.Focus();
                        return;
                    }

                    if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["DOCS"];
                        foreach (DataRow drow in dtM.Rows)
                        {
                            if (drow["DisplayFileName"].ToString() == FileUploadBill.FileName.Trim())
                            {
                                objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.", this.Page);
                                return;
                            }
                        }
                    }

                    //if (ViewState["action"] != null)
                    //{
                    file = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "DIRECT_BILL_RAISED\\EMPID_" + userno;
                    ViewState["DESTINATION_PATH"] = PATH;
                    //}

                    if (!System.IO.Directory.Exists(file))
                    {
                        System.IO.Directory.CreateDirectory(file);
                    }

                    path = file + "\\" + FileUploadBill.FileName.Trim();
                    //CHECKING FOLDER EXISTS OR NOT file
                    if (!System.IO.Directory.Exists(path))
                    {
                        if (!File.Exists(path))
                        {
                            Addfieldstotbl();
                            FileUploadBill.PostedFile.SaveAs(path);
                        }
                        else
                        {
                            if (ViewState["DELETE_DOCS"] != null)
                            {
                                int d = 0;
                                DataTable DtDel = (DataTable)ViewState["DELETE_DOCS"];
                                foreach (DataRow row in DtDel.Rows)
                                {
                                    if (FileUploadBill.FileName.ToString() == row["FILENAME"].ToString())
                                    {
                                        Addfieldstotbl();
                                        d++;
                                    }
                                }
                                if (d == 0)
                                {

                                    objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.!", this.Page);
                                    return;
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                                objCommon.DisplayMessage(UPDLedger, "This Bill Already Exists.!", this.Page);
                                return;
                            }
                        }
                    }
                }
                else
                {

                    objCommon.DisplayMessage(UPDLedger, "Please Upload Valid Bill[.jpg,.pdf,.txt,.doc,.png,.xls]", this.Page);
                    FileUploadBill.Focus();
                }
            }
            else
            {

                objCommon.DisplayMessage(UPDLedger, "Please Select File!", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void Addfieldstotbl()
    {
        int userno = Convert.ToInt32(Session["userno"]);
        if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
        {
            DataTable dt = (DataTable)ViewState["DOCS"];
            DataRow dr = dt.NewRow();
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["Filepath"] = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
            dr["DisplayFileName"] = FileUploadBill.FileName.Trim();
            dr["DocumentName"] = txtBillName.Text.Trim();

            dt.Rows.Add(dr);
            ViewState["DOCS"] = dt;
            lvNewBills.DataSource = ViewState["DOCS"];
            lvNewBills.DataBind();
            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            pnlNewBills.Visible = true;
            txtBillName.Text = string.Empty;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["DOCS"];
            DataRow dr = dt.NewRow();
            dr["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["Filepath"] = Docpath + "TEMPUPLOAD_BILL\\EMPID_" + userno;
            dr["DisplayFileName"] = FileUploadBill.FileName.Trim();
            dr["DocumentName"] = txtBillName.Text.Trim();

            ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["DOCS"] = dt;
            lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
            lvNewBills.DataBind();
            pnlNewBills.Visible = true;
            txtBillName.Text = string.Empty;
        }
    }
    public void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        dc = new DataColumn("FUID", typeof(int));
        dt.Columns.Add(dc);
        dc = new DataColumn("Filepath", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("DocumentName", typeof(string));
        dt.Columns.Add(dc);
        ViewState["DOCS"] = dt;
    }
    protected void imgBill_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;
        DownloadFile(btn.CommandArgument, btn.ToolTip);
    }
    public void DownloadFile(string path, string fileName)
    {
        try
        {
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }
    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
    protected void btnDeleteBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string filepath = btnDelete.AlternateText;
            string filename = btnDelete.ToolTip;

            if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
            {
                DataTable dt = (DataTable)ViewState["DOCS"];
                dt.Rows.Remove(this.GetEditableDatarowBill(dt, filename));
                ViewState["DOCS"] = dt;
                lvNewBills.DataSource = dt;
                lvNewBills.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Deleted Successfully.');", true);

                if (ViewState["DELETE_DOCS"] != null && ((DataTable)ViewState["DELETE_DOCS"]) != null)
                {
                    DataTable dtD = (DataTable)ViewState["DELETE_DOCS"];
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_DOCS"] = dtD;
                }
                else
                {
                    DataTable dtD = this.CreateTableBill();
                    DataRow dr = dtD.NewRow();
                    dr["FILEPATH"] = filepath;
                    dr["FILENAME"] = filename;
                    dtD.Rows.Add(dr);
                    ViewState["DELETE_DOCS"] = dtD;
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable CreateTableBill()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("FILENAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("FILEPATH", typeof(string)));
        return dtRe;
    }
    private DataRow GetEditableDatarowBill(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
            {
                if (dr["DisplayFileName"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return datRow;
    }
    private void DeleteDirecPath(string FilePath)
    {
        if (System.IO.Directory.Exists(FilePath))
        {
            try
            {
                System.IO.Directory.Delete(FilePath, true);
            }

            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    private void DeletePath()
    {
        if (ViewState["DELETE_DOCS"] != null && ((DataTable)ViewState["DELETE_DOCS"]) != null)
        {
            int i = 0;
            DataTable DtDel = (DataTable)ViewState["DELETE_DOCS"];
            foreach (DataRow Dr in DtDel.Rows)
            {
                string filename = DtDel.Rows[i]["FILENAME"].ToString();
                string filepath = DtDel.Rows[i]["FILEPATH"].ToString();

                if (File.Exists(filepath + "\\" + filename))
                {
                    //DELETING THE FILE
                    File.Delete(filepath + "\\" + filename);
                }
                i++;
            }
            ViewState["DELETE_DOCS"] = null;
        }
    }
    private void clearDoc()
    {
        lvNewBills.DataSource = null;
        lvNewBills.DataBind();
        pnlNewBills.Visible = false;

        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        DeleteDirecPath(Docpath + "TEMPUPLOAD_BILL\\EMPID_" + Convert.ToInt32(Session["userno"]));
    }

    protected void ddlSponsor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["BillComp_Code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["BillComp_Code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "A.ProjectId=" + ddlSponsor.SelectedValue, "");
    }
    protected void ddlProjSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRemainAmt.Text = objRPBController.GetSponsorProjectBalance(Convert.ToInt32(ddlSponsor.SelectedValue), Convert.ToInt32(ddlProjSubHead.SelectedValue), Session["BillComp_Code"].ToString());
    }
    //end
    #endregion

    //Fill AutoComplete Against name Textbox
    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //public static List<string> GetMergeLedger(string prefixText)
    //{
    //    List<string> Ledger = new List<string>();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        AutoCompleteController objAutocomplete = new AutoCompleteController();
    //        ds = objAutocomplete.GetName(prefixText);
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            Ledger.Add(ds.Tables[0].Rows[i]["SUPPLIER_NAME"].ToString());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ds.Dispose();
    //    }
    //    return Ledger;
    //}
    //protected void txtSearch_TextChanged1(object sender, EventArgs e)
    //{
    //    //var input = sender as TextBox;
    //    //var searchResult = Ledger.Where(i => i.Title.Contains(input.Text));
    //    //lvBillList.DataSource = searchResult;
    //    //lvBillList.DataBind();

    //}
    protected void txtExpenseLedger_TextChanged(object sender, EventArgs e)
    {
        //Added by gopal anthati on 19-08-2021 for validation for Expense ledger head selection
        if (CheckValidExpenseLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Expense Ledger From List..!", this.Page);
            txtExpenseLedger.Text = lblExpenseLedger.Text = string.Empty;
            return;
        }
        string partyNo = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + txtExpenseLedger.Text.ToString().Trim().Split('*')[1] + "'");
        PartyController objPC = new PartyController();
        DataTableReader dtr = objPC.GetPartyByPartyNo(Convert.ToInt32(partyNo), Session["BillComp_Code"].ToString());
        if (dtr.Read())
        {

            if (Convert.ToDouble(dtr["BALANCE"].ToString()) >= 0)
            {
                lblExpenseLedger.Text = String.Format("{0:0.00}", dtr["BALANCE"].ToString().Trim()) + " Dr";
            }
            else
            {
                lblExpenseLedger.Text = String.Format("{0:0.00}", (-1) * Convert.ToDouble(dtr["BALANCE"].ToString().Trim())) + " Cr";
            }
        }
        txtInvoiceNo.Focus();
    }
    //added by tanu 07/12/2021 for add/download bill after approval
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
          
            UPDLedger.Visible = true;
            UpdatePanel1.Visible = true;
            pnlupload.Visible = true;
            Panel1.Enabled = false;

            btnfillSave.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;

            btnBack.Visible = true;

            objCommon.FillDropDownList(ddlPayeeNature, "ACC_PAYEE_NATURE_MASTER", "NATURE_ID", "NATURE_NAME", "", "NATURE_NAME");
            objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') + '['+ convert(nvarchar(150),EmployeeId) + ']' AS NAME", "IDNO > 0", "FNAME");
           


            ImageButton btnView = sender as ImageButton;
            int RaisePayno = Convert.ToInt32(btnView.CommandArgument.ToString());
            {
               DataSet ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO a", "*", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Panel1.Enabled = false;                
                 
                  //  ViewState["BillNo"] = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                   ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["RAISE_PAY_NO"].ToString();
                    lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                    lblBillId.Text = ds.Tables[0].Rows[0]["bill_id"].ToString();
                    Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();

                //    string VoucherSqn = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + RaisePayno + " AND TRANSACTION_TYPE = 'J'");
                 //   ViewState["VoucherSqn"] = VoucherSqn;
                    objCommon.FillDropDownList(ddlSponsor, "Acc_" + Session["BillComp_Code"].ToString() + "_Project", "ProjectId", "ProjectName", "", "");


                    //FILL Account Dropdown....
                    ddlAccount.Items.Clear();
                    int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Count(*)", ""));
                    if (count > 0)
                    {
                        objCommon.FillDropDownList(ddlAccount, "ACC_" + Session["BillComp_Code"].ToString() + "_Comp_Account_Master", "Acc_Id", "Account_Name", "", "Acc_Id");

                    }
                    else
                    {
                        ddlAccount.Items.Clear();
                    }
                    BindBudget();
                    BindSection();
                    //lblSerialNo.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    //ddlSelectCompany.SelectedValue = ds.Tables[0].Rows[0]["COMPANY_NO"].ToString();
                    ddlAccount.SelectedValue = ds.Tables[0].Rows[0]["ACCOUNT"].ToString();
                    ddlDeptBranch.SelectedValue = ds.Tables[0].Rows[0]["DEPT_ID"].ToString();

                    string Path = "AO->FO";

                    if (Convert.ToInt32(ddlDeptBranch.SelectedValue) > 0)
                    {
                        dvApproval.Visible = true;
                        dvAuthorityPath.Visible = true;
                        lblAuthorityPath.Text = Path.ToString();
                    }
                    else
                    {
                        lblAuthorityPath.Text = "";
                        lblAuthorityPath.Font.Bold = true;
                        dvApproval.Visible = false;
                        dvAuthorityPath.Visible = false;
                    }

                    txtApprovalNo.Text = ds.Tables[0].Rows[0]["APPROVAL_NO"].ToString();
                    txtApprovalDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPROVAL_DATE"]).ToString("dd/MM/yyyy");
                    lblApprovedBy.Text = ds.Tables[0].Rows[0]["APPROVED_BY"].ToString();
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["BUDGET_NO"].ToString()) > 0)
                    {
                        ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                        ddlBudgethead_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        ddlBudgethead.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_NO"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["ProjectId"].ToString() != "")
                    {
                        ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
                    }
                    else
                    {
                        //ddlSponsor.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
                    }
                    objCommon.FillDropDownList(ddlProjSubHead, "Acc_" + Session["BillComp_Code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["BillComp_Code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId)", "b.ProjectSubId", "ProjectSubHeadName", "A.ProjectId=" + ddlSponsor.SelectedValue, "");
                    if (ds.Tables[0].Rows[0]["ProjectSubId"].ToString() != "" && ds.Tables[0].Rows[0]["ProjectSubId"].ToString() != "0")
                    {
                        ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
                        ddlProjSubHead_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        //ddlProjSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
                    }

                    string PartyNo = ds.Tables[0].Rows[0]["LEDGER_NO"].ToString();
                    if (Convert.ToInt32(PartyNo) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + PartyNo);
                        txtLedgerHead.Text = partyname.ToString();
                        txtLedgerHead_TextChanged(sender, e);
                    }
                    else
                    {
                        string partyname = "";
                        txtLedgerHead.Text = partyname.ToString();
                    }

                    string ExpenseLedgerNo = ds.Tables[0].Rows[0]["EXPENSE_LEDGER_NO"].ToString();
                    if (ExpenseLedgerNo != "0" && ExpenseLedgerNo != "")
                    {
                        string ExpenseLedgerName = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + ExpenseLedgerNo);
                        txtExpenseLedger.Text = ExpenseLedgerName.ToString();
                        txtExpenseLedger_TextChanged(sender, e);
                    }
                    else
                    {
                        string ExpenseLedgerName = "";
                        txtExpenseLedger.Text = ExpenseLedgerName.ToString();
                    }

                    string CGST = ds.Tables[0].Rows[0]["TRANS_CGST_ID"].ToString();
                    if (Convert.ToInt32(CGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + CGST);
                        txtCGSTLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtCGSTLedger.Text = partyname.ToString();
                    }

                    string SGST = ds.Tables[0].Rows[0]["TRANS_SGST_ID"].ToString();
                    if (Convert.ToInt32(SGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + SGST);
                        txtSgstLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtSgstLedger.Text = partyname.ToString();
                    }

                    string IGST = ds.Tables[0].Rows[0]["TRANS_IGST_ID"].ToString();
                    if (Convert.ToInt32(IGST) > 0)
                    {
                        string partyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + IGST);
                        txtIGSTLedger.Text = partyname.ToString();
                    }
                    else
                    {
                        string partyname = "";
                        txtIGSTLedger.Text = partyname.ToString();
                    }


                    rdbBillList.SelectedValue = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                    txtInvoiceNo.Text = ds.Tables[0].Rows[0]["BILL_INVOICE_NO"].ToString();
                    string date = ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"].ToString();
                    if (date != "" || date != string.Empty)
                    {
                        txtInvoiceDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BILL_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtInvoiceDate.Text = "";
                    }
                    txtNatureOfService.Text = ds.Tables[0].Rows[0]["NATURE_SERVICE"].ToString();
                    txtPayeeNameAddress.Text = ds.Tables[0].Rows[0]["PAYEE_NAME_ADDRESS"].ToString();
                  //  txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();

                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString())=="")
                    {
                        Diveservices.Visible = true;
                        DivServicesText.Visible = true;
                        txtServiceName.Text = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                    }
                    else
                    {
                        Diveservices.Visible = false;
                        DivServicesText.Visible = false;

                    }
                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divProviderType.Visible = false;
                        DivddlEmptype.Visible = false;
                        ddlEmpType.SelectedValue = "0";
                    }
                    else
                    {
                        divProviderType.Visible = true;
                        DivddlEmptype.Visible = true;
                        ddlEmpType.SelectedValue = ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString();
                    }

                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divEmployee1.Visible = false;
                        divEmployee2.Visible = false;
                        divPayeeNature1.Visible = false;
                        divPayeeNature2.Visible = false;
                        divPayee1.Visible = false;
                        divPayee2.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            divEmployee1.Visible = true;
                            divEmployee2.Visible = true;
                        }
                        else
                        {
                            divEmployee1.Visible = false;
                            divEmployee2.Visible = false;
                        }
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {
                            divPayeeNature1.Visible = true;
                            divPayeeNature2.Visible = true;
                            divPayee1.Visible = true;
                            divPayee2.Visible = true;
                        }
                        else
                        {
                            divPayeeNature1.Visible = false;
                            divPayeeNature2.Visible = false;
                            divPayee1.Visible = false;
                            divPayee2.Visible = false;
                        }
                    }

                    if ((ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == "")
                    {
                        divEmployee1.Visible = false;
                        divEmployee2.Visible = false;
                        divPayeeNature1.Visible = false;
                        divPayeeNature2.Visible = false;
                        divPayee1.Visible = false;
                        divPayee2.Visible = false;
                    }
                    else
                    {
                       
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            ddlPayeeNature.SelectedValue = "0";
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {
                            ddlPayeeNature.SelectedValue = ds.Tables[0].Rows[0]["PAYEE_NATURE"].ToString();
                        }
                        else
                        {
                            ddlPayeeNature.SelectedValue = "0";

                        }
                        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["BillComp_Code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");

                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 1)
                        {
                            ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["PROVIDER_TYPE"].ToString()) == 2)
                        {

                            ddlPayee.SelectedValue = ds.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                        }
                        else
                        {
                            ddlEmployee.SelectedValue = "0";
                            ddlPayee.SelectedValue = "0";
                        }

                       

                    }

                    txtGSTINNo.Text = ds.Tables[0].Rows[0]["GSTIN_NO"].ToString();

                    txtBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_AMT"]).ToString();
                    int IsTDS = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDS"].ToString());
                    int IsTDSonGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONGST"].ToString());
                    int IsTDSonCGSTSGST = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTDSONCGSTSGST"].ToString());
                    int ISSECURITY = Convert.ToInt32(ds.Tables[0].Rows[0]["ISSECURITY"].ToString());
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();
                    if (IsTDS == 1)
                    {
                        //BindSection();
                        chkTDSApplicable.Checked = true;
                        dvSection.Visible = true;
                        txtTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_AMT"]).ToString();
                        txtTdsOnAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_ON_AMT"]).ToString();
                        ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTION_NO"].ToString();
                        txtTDSPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS_PERCENT"]).ToString();
                        //txtPanNo.Text = ds.Tables[0].Rows[0]["PAN_NO"].ToString();

                        string TDS_Ledger_Id = ds.Tables[0].Rows[0]["TRANS_TDSID"].ToString();
                        if (Convert.ToInt32(TDS_Ledger_Id) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDS_Ledger_Id);
                            txtTDSLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSLedger.Text = TDSpartyname.ToString();
                        }

                        tdslabel.Visible = true;
                        tdstextbox.Visible = true;
                    }

                    else
                    {
                        chkTDSApplicable.Checked = false;
                        dvSection.Visible = false; ;
                        txtTDSAmt.Text = "";
                        txtTdsOnAmt.Text = "";
                        ddlSection.SelectedValue = "0";
                        txtTDSPer.Text = "";
                        // txtPanNo.Text = "";
                        string TDSpartyname = "";
                        //  txtLedgerHead.Text = TDSpartyname.ToString();
                        tdslabel.Visible = false;
                        tdstextbox.Visible = false;
                    }

                    //Added by Gopal Anthati
                    if (IsTDSonGST == 1)
                    {
                        //BindSection();
                        chkTDSOnGst.Checked = true;
                        divTdsOnGst.Visible = true;
                        txtTDSonGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONGST_AMOUNT"]).ToString();
                        txtTDSGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSGST_ON_AMT"]).ToString();
                        ddlTDSonGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONGST_SECTION"].ToString();
                        txtTDSonGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONGSTPER"]).ToString();

                        string TDSonGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONGSTID"].ToString();
                        if (Convert.ToInt32(TDSonGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonGSTLedgerId);
                            txtTDSonGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonGSTLedger.Text = TDSpartyname.ToString();
                        }
                        divTdsonGstLedger.Visible = true;
                    }

                    else
                    {
                        chkTDSOnGst.Checked = false;
                        divTdsOnGst.Visible = false; ;
                        txtTDSonGSTAmount.Text = "";
                        txtTDSGSTonAmount.Text = "";
                        ddlTDSonGSTSection.SelectedValue = "0";
                        txtTDSonGSTPer.Text = "";
                        string TDSpartyname = "";
                        divTdsonGstLedger.Visible = false;

                    }

                    if (IsTDSonCGSTSGST == 1)
                    {
                        //BindSection();
                        chkTdsOnCGSTSGST.Checked = true;
                        divTdsOnCGST.Visible = true;
                        divTDSOnSGST.Visible = true;
                        divTdsonSGstLedger.Visible = true;
                        divTdsonCGstLedger.Visible = true;
                        txtTDSonCGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONCGST_AMOUNT"]).ToString();
                        txtTDSCGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSCGST_ON_AMT"]).ToString();
                        ddlTDSonCGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONCGST_SECTION"].ToString();
                        txtTDSonCGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONCGSTPER"]).ToString();

                        string TDSonCGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONCGSTID"].ToString();
                        if (Convert.ToInt32(TDSonCGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonCGSTLedgerId);
                            txtTDSonCGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonCGSTLedger.Text = TDSpartyname.ToString();
                        }

                        string SecurityLedgerId = ds.Tables[0].Rows[0]["TRANS_SECURITYID"].ToString();
                        if (Convert.ToInt32(SecurityLedgerId) > 0)
                        {
                            string Securitypartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + SecurityLedgerId);
                            txtSecurityLedger.Text = Securitypartyname.ToString();
                        }
                        else
                        {
                            string Securitypartyname = "";
                            txtSecurityLedger.Text = Securitypartyname.ToString();
                        }

                        txtTDSonSGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONSGST_AMOUNT"]).ToString();
                        txtTDSSGSTonAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSSGST_ON_AMT"]).ToString();
                        ddlTDSonSGSTSection.SelectedValue = ds.Tables[0].Rows[0]["TDSONSGST_SECTION"].ToString();
                        txtTDSonSGSTPer.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSONSGSTPER"]).ToString();

                        string TDSonSGSTLedgerId = ds.Tables[0].Rows[0]["TRANS_TDSONSGSTID"].ToString();
                        if (Convert.ToInt32(TDSonSGSTLedgerId) > 0)
                        {
                            string TDSpartyname = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + TDSonSGSTLedgerId);
                            txtTDSonSGSTLedger.Text = TDSpartyname.ToString();
                        }
                        else
                        {
                            string TDSpartyname = "";
                            txtTDSonSGSTLedger.Text = TDSpartyname.ToString();
                        }
                    }

                    else
                    {
                        chkTdsOnCGSTSGST.Checked = false;
                        divTdsOnCGST.Visible = false;
                        divTDSOnSGST.Visible = false;
                        divTdsonSGstLedger.Visible = false;
                        divTdsonCGstLedger.Visible = false;
                        txtTDSonCGSTAmount.Text = "";
                        txtTDSCGSTonAmount.Text = "";
                        ddlTDSonCGSTSection.SelectedValue = "0";
                        txtTDSonCGSTPer.Text = "";

                        txtTDSonSGSTAmount.Text = "";
                        txtTDSSGSTonAmount.Text = "";
                        ddlTDSonSGSTSection.SelectedValue = "0";
                        txtTDSonSGSTPer.Text = "";


                    }

                    if (ISSECURITY == 1)
                    {
                        chkSecurity.Checked = true;
                        txtSecurityPer.Text = ds.Tables[0].Rows[0]["SECURITY_PER"].ToString();
                        txtSecurityAmt.Text = ds.Tables[0].Rows[0]["SECURITY_AMOUNT"].ToString();

                        divSecurity.Visible = true;
                        divSecurityLedger.Visible = true;

                    }
                    else
                    {
                        chkSecurity.Checked = false;
                        divSecurity.Visible = false;
                        divSecurityLedger.Visible = false;
                    }

                    //Added by vijay andoju
                    int isigst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISIGST"].ToString());
                    int isgst = Convert.ToInt32(ds.Tables[0].Rows[0]["ISGST"].ToString());
                    if (isgst == 1)
                    {
                        chkGST.Checked = true;
                        txtCgstAmount.Text = ds.Tables[0].Rows[0]["CGST_AMOUNT"].ToString();
                        txtCGSTPER.Text = ds.Tables[0].Rows[0]["CGST_PER"].ToString();
                        ddlCgstSection.SelectedValue = ds.Tables[0].Rows[0]["CGST_SECTIONNO"].ToString();

                        txtSgstAmount.Text = ds.Tables[0].Rows[0]["SGST_AMOUNT"].ToString();
                        txtSgstPer.Text = ds.Tables[0].Rows[0]["SGST_PER"].ToString();
                        ddlSgstSection.SelectedValue = ds.Tables[0].Rows[0]["SGST_SECTIONNO"].ToString();
                        dvCgst.Visible = true;
                        dvSgst.Visible = true;
                        dvGSTLedger.Visible = true;
                    }
                    else
                    {
                        chkGST.Checked = false;
                        dvCgst.Visible = false;
                        dvSgst.Visible = false;
                        dvGSTLedger.Visible = false;
                    }
                    if (isigst == 1)
                    {
                        chkIGST.Checked = true;
                        txtIgstPer.Text = ds.Tables[0].Rows[0]["IGST_PER"].ToString();
                        txtIgstAmount.Text = ds.Tables[0].Rows[0]["IGST_AMOUNT"].ToString();
                        ddlIgstSection.SelectedValue = ds.Tables[0].Rows[0]["IGST_SECTIONNO"].ToString();
                        dvIGST.Visible = true;
                        dvIgstledger.Visible = true;
                    }
                    else
                    {
                        chkIGST.Checked = false;
                        dvIGST.Visible = false;
                        dvIgstledger.Visible = false;
                    }

                    txtTotalBillAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_BILL_AMT"]).ToString();
                    txtTotalTDSAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_TDS_AMT"]).ToString();
                    txtGSTAmount.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GST_AMT"]).ToString();
                    txtNetAmt.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_AMT"]).ToString();

                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

                    string BankLedgerId = ds.Tables[0].Rows[0]["TRANS_BANKID"].ToString();
                    if (Convert.ToInt32(BankLedgerId) > 0)
                    {
                        string BankLedgerName = objCommon.LookUp("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "PARTY_NO = " + BankLedgerId);
                        txtBankLedger.Text = BankLedgerName.ToString();
                    }
                    else
                    {
                        string BankLedgerName = "";
                        txtBankLedger.Text = BankLedgerName.ToString();
                    }
                    string TransDate = ds.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                    if (TransDate.Length > 0)
                    {
                        txtTransDate.Text = Convert.ToDateTime(TransDate).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtTransDate.Text = "";
                    }

                    txtNarration.Text = ds.Tables[0].Rows[0]["TRANS_NARRATION"].ToString();

                    Panel1.Visible = true;
                    pnlBillList.Visible = false;
                    //Added by Vidisha on 13-05-2021 for multiple bill upload
                    DataSet dsDoc = objCommon.FillDropDown("ACC_BILL_RAISED_UPLOAD_DOCUMENT", "*", "", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                    if (Convert.ToInt32(dsDoc.Tables[0].Rows.Count) > 0)
                    {
                        int rowCount = dsDoc.Tables[0].Rows.Count;
                        CreateTable();
                        DataTable dtM = (DataTable)ViewState["DOCS"];
                        for (int i = 0; i < rowCount; i++)
                        {
                            DataRow dr = dtM.NewRow();
                            dr["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                            dr["Filepath"] = dsDoc.Tables[0].Rows[i]["Filepath"].ToString();
                            dr["DisplayFileName"] = dsDoc.Tables[0].Rows[i]["DisplayFileName"].ToString();
                            dr["DocumentName"] = dsDoc.Tables[0].Rows[i]["DocumentName"].ToString();
                            dtM.Rows.Add(dr);
                            dtM.AcceptChanges();
                            ViewState["DOCS"] = dtM;
                            ViewState["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                        }
                        lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
                        lvNewBills.DataBind();
                        pnlNewBills.Visible = true;
                    }
                    else
                    {
                        pnlNewBills.Visible = false;
                        lvNewBills.DataSource = null;
                        lvNewBills.DataBind();
                    }
                    //end
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    } 
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

      //  ImageButton btnPrint = sender as ImageButton;
       // int RaisePayno = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        //{
        //    DataSet ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO a", "*", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        Panel1.Enabled = false;
        //        ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["RAISE_PAY_NO"].ToString();
        //        Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();

        //        string VoucherSqn = objCommon.LookUp("ACC_" + Session["BillComp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + RaisePayno + " AND TRANSACTION_TYPE = 'P'");
        //        ViewState["VoucherSqn"] = VoucherSqn;
                   
        //    }
        //}

       // ShowReport("Bulk Voucher Print", "PmtBillVoucherDetails.rpt");

        string isFourSign = string.Empty;
        string isBankCash = string.Empty;
        ImageButton btnPrint = sender as ImageButton;
        int BILL_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        //if (rblApprovePending.SelectedValue == "approve")
        //{
        //    string voucherNo = "0";
        //    string VoucherSqn = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + BILL_NO + " AND TRANSACTION_TYPE = 'P'");


        //    voucherNo = VoucherSqn.ToString();


        //    isFourSign = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
        //    isBankCash = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

        //    if (isFourSign == "N")
        //    {
        //        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", "Payment", voucherNo, isBankCash);
        //    }
        //    else if (isFourSign == "Y")
        //    {
        //        ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", "Payment", voucherNo, isBankCash);
        //    }
        //}
       // else if (rblApprovePending.SelectedValue == "pending")
        {

            string voucherNo = "0";
            string VoucherSqn = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + BILL_NO + " AND TRANSACTION_TYPE = 'J'");


            voucherNo = VoucherSqn.ToString();


            isFourSign = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
            isBankCash = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

            if (isFourSign == "N")
            {
                ShowVoucherPrintReport("Journal Voucher", "JvContraVoucherReport.rpt", "Journal", voucherNo);
            }
            else if (isFourSign == "Y")
            {
                ShowVoucherPrintReport("Journal Voucher", "JvContraVoucherReport_Format2_Pending.rpt", "Journal", voucherNo);
            }
        }


       
    }

    private void ShowVoucherCashBankReport(string reportTitle, string rptFileName, String TransactionType, string VchNo, string isBankCash)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            string Comp_Name = objCommon.LookUp("ACC_Company", "COMPANY_NAME", "COMPANY_CODE ='" + Session["Comp_Code"].ToString() + "'");

            Session["comp_name"] = Comp_Name.ToString();

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["Comp_Code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["Comp_Code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim() + ",BankORCashName=" + isBankCash;

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccountingVouchersModifications.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
           
        }
    }

    private void ShowVoucherPrintReport(string reportTitle, string rptFileName, String TransactionType, string VchNo)
    {
        try
        {
            string VCH_TYPE = string.Empty;

            if (TransactionType == "Payment")
                VCH_TYPE = "P";
            else if (TransactionType == "Receipt")
                VCH_TYPE = "R";
            else if (TransactionType == "Contra")
                VCH_TYPE = "C";
            else if (TransactionType == "Journal")
                VCH_TYPE = "J";
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            string ClMode;
            string VoucherType = TransactionType.ToString().Trim() + " Voucher";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_CODE_YEAR=" + Session["comp_code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + ",@P_VCH_NO=" + VchNo + "," + "@P_VOUCHER_TYPE=" + VoucherType.ToString().Trim() + "," + "@UserName=" + Session["userfullname"].ToString().Trim() + "," + "@P_STR_VCH_NO=" + Session["comp_code"].ToString().Trim() + "/" + VCH_TYPE.ToString().Trim() + "/" + VchNo + "," + "@P_VCH_TYPE=" + VCH_TYPE.ToString().Trim();

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AccVoucherModify.ShowVoucherPrintReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string VCH_TYPE = string.Empty;
            VCH_TYPE = "P";        

            objCommon = new Common();

            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));

            string LedgerName = string.Empty;

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;

            //Commented by Akshay Dixit on 05-09-2022
            url += "&param=@P_CODE_YEAR=" + Session["BillComp_Code"].ToString() + "," + "@P_COMPANY_NAME=" + Session["comp_name"].ToString().Trim().ToUpper() + "," + "@P_VOUCHER_TYPE=" + VCH_TYPE.ToString().Trim() + "," + "@P_VCH_TYPE=" + VCH_TYPE + "," + "@P_VCH_NO=" + ViewState["VoucherSqn"].ToString();
            //url += "&param=@P_CODE_YEAR=" + Session["BillComp_Code"].ToString() + "," + "@P_VCH_NO=" + ViewState["VoucherSqn"].ToString() + "," + "@P_VCH_TYPE=" + VCH_TYPE;


            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this.UPDLedger, UPDLedger.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnfillSave_Click(object sender, EventArgs e)
    {
          
        //Added by tanu on 09-12-2021 for multiple bill upload after approval
        DataTable dtD = null;
        if (ViewState["DOCS"] != null && ((DataTable)ViewState["DOCS"]) != null)
        {
            dtD = (DataTable)ViewState["DOCS"];
        }
        else
        {
            CreateTable();
            dtD = (DataTable)ViewState["DOCS"];
        }

            ObjRPB.RAISE_PAY_NO = Convert.ToInt32(ViewState["RaisePayNo"].ToString());



            if (dtD.Rows.Count > 0)
            {
                ObjRPB.FILEPATH = Docpath + "DIRECT_BILL_RAISED\\EMPID_" + Convert.ToInt32(Session["userno"]) + "\\BillNo_" + ObjRPB.RAISE_PAY_NO;
                if (ViewState["DESTINATION_PATH"] != null)
                {
                    AddDocuments(ObjRPB.RAISE_PAY_NO);
                }
            }
            else
            {
                ObjRPB.FILEPATH = "";
            }
        
        int ret = objRPBController.AddBillAfterApproval(ObjRPB, Convert.ToInt32(Session["userno"].ToString()),dtD);
       
        if (ret == 1)
        {
          
          //  AddDocuments(ObjRPB.RAISE_PAY_NO);

            objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully!", this.Page);
            Panel1.Visible = false;
            pnlBillList.Visible = true;
            btnfillSave.Visible = false;
            Clear();
          

        }
        else if (ret == 2)
        {
            AddDocuments(ObjRPB.RAISE_PAY_NO);

            objCommon.DisplayMessage(UPDLedger, "Record Inserted Successfully!", this.Page);
            Panel1.Visible = false;
            pnlBillList.Visible = true;
            btnfillSave.Visible = false;
            Clear();

        }
     
        pnlNewBills.Visible = false;
        ViewState["DESTINATION_PATH"] = null;
        ViewState["DOCS"] = null;
        clearDoc();
        ViewState["letrno"] = null;
    }
    protected void btnAddBill_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            ImageButton btnAddBill = sender as ImageButton;
            int RaisePayno = Convert.ToInt32(btnAddBill.CommandArgument.ToString());
            {
                DataSet ds = objCommon.FillDropDown("ACC_RAISING_PAYMENT_BILL", "RAISE_PAY_NO a", "*", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Panel1.Visible = true;
                    Panel1.Enabled = true;
                    btnBack.Visible = true;
                    pnlBillList.Visible = false;
                    UPDLedger.Visible = false;
                    UpdatePanel1.Visible = false;

                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    btnSubmit.Enabled = false;
                    btnCancel.Enabled = false;

                    btnfillSave.Visible = true;

                    ViewState["RaisePayNo"] = ds.Tables[0].Rows[0]["RAISE_PAY_NO"].ToString();

                    Session["BillComp_Code"] = ds.Tables[0].Rows[0]["COMPANY_CODE"].ToString();

                    DataSet dsDoc = objCommon.FillDropDown("ACC_BILL_RAISED_UPLOAD_DOCUMENT", "*", "", "RAISE_PAY_NO =" + RaisePayno, "Raise_Pay_No");
                    if (Convert.ToInt32(dsDoc.Tables[0].Rows.Count) > 0)
                    {
                        int rowCount = dsDoc.Tables[0].Rows.Count;
                        CreateTable();
                        DataTable dtM = (DataTable)ViewState["DOCS"];
                        for (int i = 0; i < rowCount; i++)
                        {
                            DataRow dr = dtM.NewRow();
                            dr["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                            dr["Filepath"] = dsDoc.Tables[0].Rows[i]["Filepath"].ToString();
                            dr["DisplayFileName"] = dsDoc.Tables[0].Rows[i]["DisplayFileName"].ToString();
                            dr["DocumentName"] = dsDoc.Tables[0].Rows[i]["DocumentName"].ToString();
                            dtM.Rows.Add(dr);
                            dtM.AcceptChanges();
                            ViewState["DOCS"] = dtM;
                            ViewState["FUID"] = dsDoc.Tables[0].Rows[i]["DOCID"].ToString();
                        }
                        lvNewBills.DataSource = (DataTable)ViewState["DOCS"];
                        lvNewBills.DataBind();
                        pnlNewBills.Visible = true;
                        
                    }
                    else
                    {
                        pnlNewBills.Visible = false;
                        lvNewBills.DataSource = null;
                        lvNewBills.DataBind();
                    }
                    //end
                }
               
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //added by 15/12/2021  for Provide payee & employee tagging functionality 
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlPayeeNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlPayee, "ACC_" + Session["BillComp_Code"] + "_PAYEE", "IDNO", "PARTYNAME", "NATURE_ID=" + ddlPayeeNature.SelectedValue, "PARTYNAME");
    }
    protected void ddlPayee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        divEmployee1.Visible = false;
        divEmployee2.Visible = false;
        divPayee1.Visible = false;
        divPayee2.Visible = false;
        divPayeeNature1.Visible = false;
        divPayeeNature2.Visible = false;

        ddlEmployee.SelectedIndex = 0;
        ddlPayeeNature.SelectedIndex = 0;
        ddlPayee.SelectedIndex = 0;
     
        if (ddlEmpType.SelectedValue == "1")
        {
            divEmployee1.Visible = true;
            divEmployee2.Visible = true;

            divPayee1.Visible = false;
            divPayee2.Visible = false;
            divPayeeNature1.Visible = false;
            divPayeeNature2.Visible = false;
        }
        else if (ddlEmpType.SelectedValue == "2")
        {
            divPayee1.Visible = true;
            divPayee2.Visible = true;
            divPayeeNature1.Visible = true;
            divPayeeNature2.Visible = true;
            divEmployee1.Visible = false;
            divEmployee2.Visible = false;
        }

    }
    protected void txtBankLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidBankLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper Bank/Cash Ledger From List..!", this.Page);
            txtBankLedger.Text = string.Empty;
            return;
        }
    }

    protected int CheckValidBankLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtBankLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void txtSgstLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidSGSTLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper SGST Ledger From List..!", this.Page);
            txtSgstLedger.Text = string.Empty;
            return;
        }

    }

    protected int CheckValidSGSTLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtSgstLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }


    protected void txtCGSTLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidCGSTLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper CGST Ledger From List..!", this.Page);
            txtCGSTLedger.Text = string.Empty;
            return;
        }
    }

    protected int CheckValidCGSTLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtCGSTLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }
    protected void txtTDSLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidTDSLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper TDS Ledger From List..!", this.Page);
            txtTDSLedger.Text = string.Empty;
            return;
        }

    }

    protected int CheckValidTDSLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtTDSLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void txtIGSTLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidIGSTLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper IGST Ledger From List..!", this.Page);
            txtIGSTLedger.Text = string.Empty;
            return;
        }

    }

    protected int CheckValidIGSTLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtIGSTLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void txtTDSonGSTLedger_TextChanged(object sender, EventArgs e)
    {
        if (CheckValidTDSonGSTLedger() == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Please Select Proper TDS On GST Ledger From List..!", this.Page);
            txtTDSonGSTLedger.Text = string.Empty;
            return;
        }

    }

    protected int CheckValidTDSonGSTLedger()
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        int RetVal = 1;
        try
        {


            ds = objCommon.FillDropDown("ACC_" + Session["BillComp_Code"].ToString() + "_PARTY ", "UPPER(PARTY_NAME) +'*'+CAST(ACC_CODE AS VARCHAR(12)) AS PARTY_NAME", "", "PAYMENT_TYPE_NO  NOT IN ('1','2') ", "ACC_CODE");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
            if (!Ledger.Contains(txtTDSonGSTLedger.Text))
            {
                RetVal = 0;
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return RetVal;
    }

    protected void btnPayVoucher_Click(object sender, ImageClickEventArgs e)
    {
        string isFourSign = string.Empty;
        string isBankCash = string.Empty;
        ImageButton btnPrint = sender as ImageButton;
        int BILL_NO = Convert.ToInt32(btnPrint.CommandArgument.ToString());
        if (rblApprovePending.SelectedValue == "approve")
        {
            string voucherNo = "0";
            string VoucherSqn = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_TRANS", "VOUCHER_SQN", "BILL_ID =" + BILL_NO + " AND TRANSACTION_TYPE = 'P'");


            voucherNo = VoucherSqn.ToString();


            isFourSign = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='VOUCHER WITH FOUR SIGN'");
            isBankCash = objCommon.LookUp("ACC_" + Session["Comp_Code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='LOGO AND BANK OR CASH IS DISPLAY ON VOUCHER PRINT'");

            if (isFourSign == "N")
            {
                ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt.rpt", "Payment", voucherNo, isBankCash);
            }
            else if (isFourSign == "Y")
            {
                ShowVoucherCashBankReport("Voucher", "PmtRcptCashVoucherRpt_Format2.rpt", "Payment", voucherNo, isBankCash);
            }
        }
    }
}