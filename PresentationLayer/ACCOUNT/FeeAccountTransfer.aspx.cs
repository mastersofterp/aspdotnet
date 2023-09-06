//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 14-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
// MODIFIED BY   : KAPIL BUDHLANI
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class FeeAccountTransfer : System.Web.UI.Page
{
    //Under development:- Conform Message box when we transfer
    //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController objTrans = new AccountTransactionController();
    AccountTransaction objStrMst = new AccountTransaction();
    static string strManual = string.Empty;
    string paytype = string.Empty;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //btnTrans.Enabled = false;

        //if (txtFromDate.Text.ToString().Trim() != "")
        //{
        //    bool result = CheckEntry();
        //    if (result == true)
        //    {
        //        btnTrans.Attributes.Add("onClick", "return ShowConfirm();");
        //    }
        //}

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");

        if (!Page.IsPostBack)
        {
            //btnTrans.Attributes.Add("onclick", "return confirm('Record Allread Present Are U sure want to Replace records?')");
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayMessage("Select company/cash book.", this);
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Added By Nitin Meshram on 29-04-2014
                    string IsCCMS = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='IS CCMS'");
                    string AllowFullMapping = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='MAP ALL LEDGER FOR FEES TRANSFER'");
                    Session["AllowFullMapping"] = AllowFullMapping;
                    if (IsCCMS == "Y")
                    {
                        //row18.Visible = false;

                        Session["IsCCMS"] = IsCCMS;
                        PopulateReceptTypeDropdown();
                        PopulateDegreeDropdown();
                        Session["IsCCMS"] = IsCCMS;
                        //Filling Recept list
                        PopulateReceptTypeDropdown();
                        row18.Visible = true;
                        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='FEES TRANSFER USING PAYMENT TYPE'") == "Y")
                        {
                            ViewState["PaymentTypeWise"] = "Y";
                            TrPaytype.Visible = true;
                            objCommon.FillDropDownList(ddlGrpPayment, "acc_" + Session["comp_code"] + "_PAYMENT_GROUP", "PGROUP_NO", "GROUPNAME", "", "");
                        }
                        else
                        {
                            ViewState["PaymentTypeWise"] = "N";
                        }

                    }
                    else
                    {
                        // Filling Degrees
                        PopulateDegreeDropdown();
                        Session["IsCCMS"] = IsCCMS;
                        //Filling Recept list
                        PopulateReceptTypeDropdown();
                        row18.Visible = true;
                        if (objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='FEES TRANSFER USING PAYMENT TYPE'") == "Y")
                        {
                            ViewState["PaymentTypeWise"] = "Y";
                            TrPaytype.Visible = true;
                            objCommon.FillDropDownList(ddlGrpPayment, "acc_" + Session["comp_code"] + "_PAYMENT_GROUP", "PGROUP_NO", "GROUPNAME", "", "");
                        }
                        else
                        {
                            ViewState["PaymentTypeWise"] = "N";
                        }
                    }
                    rowgrid.Visible = false;
                    //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //txtTodate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    ViewState["Operation"] = "Submit";

                    SetFinancialYear();
                    strManual = objCommon.LookUp("ACC_" + Session["comp_code"] + "_CONFIG", "PARAMETER", "CONFIGDESC='AUTOGENERATED VOUCHER NO. REQUIRED'");
                }
            }
            rdoMiscFees_CheckedChanged(sender, e);
        }
        //if (txtFromDate.Text.ToString().Trim() != "")
        //{
        //    bool result = CheckEntry();
        //    if (result == true)
        //    {
        //        btnTrans.Attributes.Add("onClick", "return ShowConfirm();");
        //    }
        //    else
        //    {
        //        btnTrans.Attributes.Add("onClick", "return NotShowConfirm();");
        //    }
        //}
        divMsg.InnerHtml = string.Empty;
    }
    private void SetFinancialYear()
    {
        FinCashBookController objCBC = new FinCashBookController();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(Session["comp_no"].ToString().Trim());
        if (dtr.Read())
        {
            Session["comp_code"] = dtr["COMPANY_CODE"];
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            Session["fin_date_from"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]);
            Session["fin_date_to"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]);
            Session["FromYear"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString();
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy");
            txtTodate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy");
        }
        dtr.Close();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeAccountTransfer.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeAccountTransfer.aspx");
        }
    }

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        rowgrid.Visible = true;
        //SqlConnection sqlcon = new SqlConnection(_Fees);

        if (txtFromDate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Transfer Date Required... ", this);
            txtFromDate.Focus();
            return;
        }
        if (txtTodate.Text == string.Empty)
        {
            objCommon.DisplayMessage(UPDLedger, "Upto Date Required... ", this);
            txtFromDate.Focus();
            return;
        }

        if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtFromDate.Text)) > 0)
        {
            objCommon.DisplayMessage(UPDLedger, "From Date And Upto Date Is Not Valid ", this);
            txtFromDate.Focus();
            return;
        }

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
        Fdate = Fdate.Substring(0, 10);

        string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtTodate.Text)));
        Ldate = Ldate.Substring(0, 10);



        //SQLHelper objSqlhelper = new SQLHelper(_nitprm_constr);
        DataSet DsFeelegHd = new DataSet();
        //string SqlString = "SELECT LH.RECIEPT_TYPE,LH.FEE_HEAD_NO,LH.FEE_HEAD_NAME, PT.PARTY_NAME FROM ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH,ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT WHERE LH.LEDGERNO=PT.PARTY_NO AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND LH.COLLEGE_CODE='" + ddlDegree.SelectedValue.ToString() + "'";
        //DsFeelegHd = objSqlhelper.ExecuteDataSet(SqlString);

        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                //DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH,ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME, PT.PARTY_NAME", "LH.LEDGERNO=PT.PARTY_NO AND LH.DEGREENO=0 AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "'", "CONVERT(INT,REPLACE(LH.FEE_HEAD_NO,'F',''))");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"

                //DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT on (LH.LEDGERNO=PT.PARTY_NO)", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME , case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME, case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.LEDGERNO=PT.PARTY_NO AND LH.DEGREENO=0 AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "'", "Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"

                DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT on (LH.LEDGERNO=PT.PARTY_NO)", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME , case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME, case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.LEDGERNO=PT.PARTY_NO AND LH.DEGREENO=0 AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND LH.DEPTNO=" + ddlDept.SelectedValue, "Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"

            }
            else
            {
                //DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH,ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME, PT.PARTY_NAME", "LH.LEDGERNO=PT.PARTY_NO AND LH.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "'", "CONVERT(INT,REPLACE(LH.FEE_HEAD_NO,'F',''))");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"
                //  DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT on (LH.LEDGERNO=PT.PARTY_NO)", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME , case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME, case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "'", "Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"

                DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT on (LH.LEDGERNO=PT.PARTY_NO)", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME , case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME, case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND LH.RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND LH.BRANCHNO=" + ddlDept.SelectedValue, "Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"

            }
        }
        else
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT   on (LH.LEDGERNO=PT.PARTY_NO) ", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME,case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME,case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.RECIEPT_TYPE='MF'", "LH.Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"
                //DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH,ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME, PT.PARTY_NAME", "LH.LEDGERNO=PT.PARTY_NO AND LH.RECIEPT_TYPE='MF'", "LH.FEE_HEAD_NO");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"
            }
            else
            {
                //tanu aaj ch
                DsFeelegHd = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD LH left outer join ACC_" + Session["comp_code"].ToString().Trim() + "_PARTY PT  on (LH.LEDGERNO=PT.PARTY_NO) ", "LH.RECIEPT_TYPE,LH.FEE_HEAD_NO", "LH.FEE_HEAD_NAME, case when LH.LEDGERNO=0 then 'UNMAPPED LEDGER' else PT.PARTY_NAME end  PARTY_NAME,case when LH.LEDGERNO=0 then 'UnMapped' else 'Mapped' end  FeeHeadsStatus", "LH.RECIEPT_TYPE='MF'", "LH.Sequenceid");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"
                //DsFeelegHd = objCommon.FillDropDown("ACD_MISCDCR LH,ACD_MISCDCR_TRANS PT", "LH.RECIEPT_CODE AS RECIEPT_TYPE,PT.MISCHEADCODE AS FEE_HEAD_NO", "PT.MISCHEAD AS FEE_HEAD_NAME, LH.NAME AS PARTY_NAME", "LH.MISCDCRSRNO=PT.MISCDCRSRNO ", "");// AND LH.COLLEGE_CODE='" + Session["colcode"].ToString() + "'"
            }
        }

        if (DsFeelegHd != null && DsFeelegHd.Tables[0].Rows.Count > 0)
        {
            GridData.DataSource = DsFeelegHd;
            GridData.DataBind();
        }
        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "DATA NOT AVAILABLE", this);
            return;
        }
        for (int y = 0; y < GridData.Rows.Count; y++)
        {
            Label FeeHeadsStatus = GridData.Rows[y].FindControl("lblFeeHeadsStatus") as Label;
            if (FeeHeadsStatus != null)
            {
                if (FeeHeadsStatus.Text == "UnMapped")
                {
                    //GridViewRow firstRow = GridData.Rows[y];
                    //firstRow.CssClass = "highlightRow";
                    GridData.Rows[y].BackColor = System.Drawing.Color.Red;
                }
            }
        }
        //SELECT sum(f1),sum(f2) FROM DCR where REC_Dt between  '01-jan-2010' and '31-mar-2010' and can=0 and pay_ty='C' and cbtype='TF' and college=4

        //For cash amount
        DataSet dsCashAmt = new DataSet();
        double cashAmt = 0;
        double balAmt = 0;
        //string OrlString = "SELECT sum(f1),sum(f2),sum(f3),sum(f4),sum(f5),sum(f6),sum(f7),sum(f8),sum(f9),sum(f10),sum(f11),sum(f12),sum(f13),sum(f14),sum(f15),sum(f16),sum(f17),sum(f18),sum(f19),sum(f20),sum(f21),sum(f22),sum(f23),sum(f24),sum(f25),sum(f26),sum(f27),sum(f28),sum(f29),sum(f30) FROM ACD_DCR where REC_Dt between  '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_tyPE='C' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'";
        //SqlDataAdapter OrDa = new SqlDataAdapter(OrlString, sqlcon);
        //OrDa.Fill(dsCashAmt);
        string _CASH_BANK_TRANSFER = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='TRANSFER COUNTER TO CASH OR CHALLAN TO BANK'");
        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                dsCashAmt = objTrans.GetCashAmountForFeesTransferCCMS((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), "C", _CCMS);
            }
            else
            {
                if (objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='FEES TRANSFER USING PAYMENT TYPE'") == "Y")
                {
                    string paymenttypenos = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PAYTYPENO", "PGROUP_NO=" + ddlGrpPayment.SelectedValue.ToString());
                    //dsCashAmt = objCommon.FillDropDown("ACD_DCR", "sum(f1) as F1", "sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30", "REC_Dt BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_tyPE='C' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'", "");
                    dsCashAmt = objTrans.GetAmountForFeesTransferRF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos, ddlDept.SelectedValue);
                }
                else
                {
                    string paymenttypenos = string.Empty;
                    //dsCashAmt = objCommon.FillDropDown("ACD_DCR", "sum(f1) as F1", "sum(f2) as F2,sum(f3) as F3,sum(f4) as F4,sum(f5) as F5,sum(f6) as F6,sum(f7) as F7,sum(f8)as F8,sum(f9)as F9,sum(f10)as F10,sum(f11)as F11,sum(f12) as F12,sum(f13)as F13,sum(f14)as F14,sum(f15)as F15,sum(f16)as F16,sum(f17)as F17,sum(f18)as F18,sum(f19)as F19,sum(f20)as F20,sum(f21)as F21,sum(f22)as F22,sum(f23)as F23,sum(f24)as F24,sum(f25)as F25,sum(f26)as F26,sum(f27)as F27,sum(f28)as F28,sum(f29)as F29,sum(f30)as F30", "REC_Dt BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_tyPE='C' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'", "");
                    dsCashAmt = objTrans.GetAmountForFeesTransferRF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos, ddlDept.SelectedValue);
                }
            }
            if (dsCashAmt.Tables[0].Columns.Count > 0)
            {
                if (GridData.Rows.Count > 0)
                {
                    for (int x = 0; x < dsCashAmt.Tables[0].Columns.Count; x++)
                    {
                        string headwsCashAmt = string.IsNullOrEmpty(dsCashAmt.Tables[0].Rows[0][x].ToString().Trim()) ? "0.0" : dsCashAmt.Tables[0].Rows[0][x].ToString();
                        if (Convert.ToDouble(headwsCashAmt) > 0)
                        {
                            for (int y = 0; y < GridData.Rows.Count; y++)
                            {
                                Label lblFeeHeadNo = GridData.Rows[y].FindControl("lblFeeHeadsNo") as Label;
                                Label lblCash = GridData.Rows[y].FindControl("lblCashAmt") as Label;
                                if (lblCash != null)
                                {
                                    if (lblFeeHeadNo.Text.ToUpper() == dsCashAmt.Tables[0].Columns[x].ToString().ToUpper())
                                    {
                                        //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsCashAmt.Tables[0].Rows[0][x].ToString().Trim())));
                                        //lblCash.Text = dsCashAmt.Tables[0].Rows[0][x].ToString().Trim();

                                        lblCash.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(headwsCashAmt)));
                                        cashAmt = cashAmt + Convert.ToDouble(lblCash.Text);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        else
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                dsCashAmt = objTrans.GetCashAmountForFeesTransferMFCCMS((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), "MFD", "C", _CCMS);
            }
            else
            {
                if (objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='FEES TRANSFER USING PAYMENT TYPE'") == "Y")
                {
                    string paymenttypenos = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PAYTYPENO", "PGROUP_NO=" + ddlGrpPayment.SelectedValue.ToString());
                    //dsCashAmt = objCommon.FillDropDown("ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO)", "MISCHEADSRNO,	MISCHEADCODE", "MISCHEAD, sum(MIscamt)AMT", "RECPTDATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and CHDD='C' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,	MISCHEADCODE,	MISCHEAD ", "");
                    dsCashAmt = objTrans.GetCashAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS);
                }
                else
                {
                    string paymenttypenos = string.Empty;
                    //dsCashAmt = objCommon.FillDropDown("ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO)", "MISCHEADSRNO,	MISCHEADCODE", "MISCHEAD, sum(MIscamt)AMT", "RECPTDATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and CHDD='C' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,	MISCHEADCODE,	MISCHEAD ", "");

                    //orignal 
                    //  dsCashAmt = objTrans.GetCashAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS);

                    //added by tanu 01/11/2021

                    if (rdbReceipt.Checked == true)
                    {
                        paytype = "R";
                        {
                            dsCashAmt = objTrans.GetCashAndBankAmountForFeesTransfer(Fdate, Ldate, _CCMS, paytype);
                        }
                    }
                    else if (rdbPayment.Checked == true)
                    {
                        paytype = "P";
                        {
                            dsCashAmt = objTrans.GetCashAndBankAmountForFeesTransfer(Fdate, Ldate, _CCMS, paytype);
                        }
                    }
                    else
                    {
                        dsCashAmt = objTrans.GetCashAmountFormisFeesTransfer(Fdate, Ldate, _CCMS);
                    }


                }
            }

            //
            if (dsCashAmt.Tables[0].Rows.Count > 0)
            {
                if (GridData.Rows.Count > 0)
                {
                    for (int x = 0; x < dsCashAmt.Tables[0].Rows.Count; x++)
                    {
                        string headwsCashAmt = string.IsNullOrEmpty(dsCashAmt.Tables[0].Rows[x]["AMT"].ToString().Trim()) ? "0.0" : dsCashAmt.Tables[0].Rows[x]["AMT"].ToString();
                        if (Convert.ToDouble(headwsCashAmt) > 0)
                        {
                            for (int y = 0; y < GridData.Rows.Count; y++)
                            {
                                Label lblFeeHeadNo = GridData.Rows[y].FindControl("lblFeeHeadsNo") as Label;
                                Label lblCash = GridData.Rows[y].FindControl("lblCashAmt") as Label;
                                HiddenField hdnMISCHEADSRNO = GridData.Rows[y].FindControl("hdnMISCHEADSRNO") as HiddenField;


                                if (lblCash != null)
                                {

                                    if (lblFeeHeadNo.Text.ToUpper() == dsCashAmt.Tables[0].Rows[x]["MISCHEADCODE"].ToString().ToUpper())
                                    {
                                        //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsCashAmt.Tables[0].Rows[0][x].ToString().Trim())));
                                        //lblCash.Text = dsCashAmt.Tables[0].Rows[0][x].ToString().Trim();
                                        hdnMISCHEADSRNO.Value = dsCashAmt.Tables[0].Rows[x]["MISCHEADSRNO"].ToString().ToUpper();
                                        lblCash.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(headwsCashAmt)));
                                        // cashAmt = cashAmt + Convert.ToDouble(lblCash.Text);
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }

            //ADDED BY TANU for total cash amount   23/11/2021
            foreach (GridViewRow gvr in GridData.Rows)
            {
                Label lblcash = (Label)gvr.FindControl("lblCashAmt") as Label;
                Label FeeHeadsStatus = (Label)gvr.FindControl("lblFeeHeadsStatus") as Label;
                if (FeeHeadsStatus != null)
                {
                    if (FeeHeadsStatus.Text == "Mapped")
                    {

                        cashAmt = cashAmt + Convert.ToDouble(lblcash.Text);
                    }
                }

            }

        }


        lblCashTotal.ForeColor = System.Drawing.Color.Red;
        //String.Format("{0:0.00}", Math.Abs(cashAmt));
        lblCashTotal.Text = String.Format("{0:0.00}", Math.Abs(cashAmt));

        double BankAmt = 0;
        DataSet dsBankAmt = new DataSet();

        //string OrlString1 = "SELECT sum(f1),sum(f2),sum(f3),sum(f4),sum(f5),sum(f6),sum(f7),sum(f8),sum(f9),sum(f10),sum(f11),sum(f12),sum(f13),sum(f14),sum(f15),sum(f16),sum(f17),sum(f18),sum(f19),sum(f20),sum(f21),sum(f22),sum(f23),sum(f24),sum(f25),sum(f26),sum(f27),sum(f28),sum(f29),sum(f30) FROM acd_dcr where REC_Dt between  '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_type='D' and RECON='1' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'";
        //SqlDataAdapter OrDa1 = new SqlDataAdapter(OrlString1, sqlcon);
        //OrDa1.Fill(dsBankAmt);
        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                dsBankAmt = objTrans.GetCashAmountForFeesTransferCCMS((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), "Q", _CCMS);
            }
            else
            {
                if (objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='FEES TRANSFER USING PAYMENT TYPE'") == "Y")
                {
                    string paymenttypenos = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PAYTYPENO", "PGROUP_NO=" + ddlGrpPayment.SelectedValue.ToString());
                    //dsBankAmt = objCommon.FillDropDown("acd_dcr", "sum(f1)F1", "sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30", "REC_Dt between  '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_type='D' and RECON='1' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'", "");
                    //dsBankAmt = objTrans.GetBankAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos);
                    dsBankAmt = objTrans.GetBankAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos, ddlDept.SelectedValue);//ADDED BY VIJAY ANDOJU 05092020

                }
                else
                {
                    string paymenttypenos = string.Empty;
                    //dsBankAmt = objCommon.FillDropDown("acd_dcr", "sum(f1)F1", "sum(f2)F2,sum(f3)F3,sum(f4)F4,sum(f5)F5,sum(f6)F6,sum(f7)F7,sum(f8)F8,sum(f9)F9,sum(f10)F10,sum(f11)F11,sum(f12)f12,sum(f13)f13,sum(f14)f14,sum(f15)f15,sum(f16)f16,sum(f17)f17,sum(f18)f18,sum(f19)f19,sum(f20)f20,sum(f21)f21,sum(f22)f22,sum(f23)f23,sum(f24)f24,sum(f25)f25,sum(f26)f26,sum(f27)f27,sum(f28)f28,sum(f29)f29,sum(f30)f30", "REC_Dt between  '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' and '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and pay_type='D' and RECON='1' and RECIEPT_CODE='" + ddlRecept.SelectedValue.ToString() + "' and DEGREENO='" + ddlDegree.SelectedValue.ToString() + "'", "");
                    //dsBankAmt = objTrans.GetBankAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos);
                    dsBankAmt = objTrans.GetBankAmountForFeesTransfer((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), ddlRecept.SelectedValue.ToString(), ddlDegree.SelectedValue.ToString(), _CCMS, _CASH_BANK_TRANSFER, ViewState["PaymentTypeWise"].ToString(), paymenttypenos, ddlDept.SelectedValue);


                }
            }
            if (dsBankAmt.Tables[0].Rows.Count > 0)
            {
                if (GridData.Rows.Count > 0)
                {
                    for (int x = 0; x < dsBankAmt.Tables[0].Columns.Count; x++)
                    {
                        string headwsBankAmt = "0.00";
                        if (dsBankAmt.Tables[0].Rows[0][x].ToString().Trim() != "")
                        {
                            if (dsBankAmt.Tables[0].Rows[0][x].ToString().Trim() != null)
                            {
                                //string headwsBankAmt = string.IsNullOrEmpty(dsBankAmt.Tables[0].Rows[0][x].ToString().Trim()) ? "0.0" : dsBankAmt.Tables[0].Rows[0][x].ToString();
                                headwsBankAmt = dsBankAmt.Tables[0].Rows[0][x].ToString().Trim();
                            }
                        }

                        if (Convert.ToDouble(headwsBankAmt) != 0)
                        {
                            for (int y = 0; y < GridData.Rows.Count; y++)
                            {
                                Label lblFeeHeadNo = GridData.Rows[y].FindControl("lblFeeHeadsNo") as Label;
                                Label lblBAmt = GridData.Rows[y].FindControl("lblBankAmt") as Label;
                                if (lblBAmt != null)
                                {
                                    if (lblFeeHeadNo.Text.ToUpper() == dsBankAmt.Tables[0].Columns[x].ToString().ToUpper())
                                    {
                                        lblBAmt.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(headwsBankAmt)));
                                        BankAmt = BankAmt + Convert.ToDouble(lblBAmt.Text);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                dsCashAmt = objTrans.GetCashAmountForFeesTransferMFCCMS((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), "MFD", "Q", _CCMS);
            }
            else
            {
                //dsCashAmt = objCommon.FillDropDown("ACD_MISCDCR_TRANS MT inner join ACD_MISCDCR MD on(MT.MISCDCRSRNO = MD.MISCDCRSRNO)", "MISCHEADSRNO,	MISCHEADCODE", "MISCHEAD, sum(MIscamt)AMT", "RECPTDATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' and can=0 and CHDD='Q' and RECIEPT_CODE='MF' GROUP BY MISCHEADSRNO,	MISCHEADCODE,	MISCHEAD ", "");
                //orignal
                //  dsCashAmt = objTrans.GetBankAmountForMF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS);

                //added by tanu 01/11/2021

                if (rdbReceipt.Checked == true)
                {
                    paytype = "R";
                    {
                        dsCashAmt = objTrans.GetMisBankAmountForMF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS, paytype);

                    }
                }
                else if (rdbPayment.Checked == true)
                {
                    paytype = "P";
                    {
                        dsCashAmt = objTrans.GetMisBankAmountForMF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS, paytype);
                    }
                }
                else
                {
                    dsCashAmt = objTrans.GetBankAmountmissForMF((Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")), (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")), _CCMS);
                }
            }

            if (dsCashAmt.Tables[0].Rows.Count > 0)
            {
                if (GridData.Rows.Count > 0)
                {
                    for (int x = 0; x < dsCashAmt.Tables[0].Rows.Count; x++)
                    {
                        string headwsBankAmt = string.IsNullOrEmpty(dsCashAmt.Tables[0].Rows[x]["AMT"].ToString().Trim()) ? "0.0" : dsCashAmt.Tables[0].Rows[x]["AMT"].ToString();
                        if (Convert.ToDouble(headwsBankAmt) > 0)
                        {
                            for (int y = 0; y < GridData.Rows.Count; y++)
                            {
                                Label lblFeeHeadNo = GridData.Rows[y].FindControl("lblFeeHeadsNo") as Label;
                                Label lblBAmt = GridData.Rows[y].FindControl("lblBankAmt") as Label;
                                HiddenField hdnMISCHEADSRNO = GridData.Rows[y].FindControl("hdnMISCHEADSRNO") as HiddenField;
                                if (lblBAmt != null)
                                {
                                    if (lblFeeHeadNo.Text.ToUpper() == dsCashAmt.Tables[0].Rows[x]["MISCHEADCODE"].ToString().ToUpper())
                                    {
                                        //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsCashAmt.Tables[0].Rows[0][x].ToString().Trim())));
                                        //lblCash.Text = dsCashAmt.Tables[0].Rows[0][x].ToString().Trim();
                                        hdnMISCHEADSRNO.Value = dsCashAmt.Tables[0].Rows[x]["MISCHEADSRNO"].ToString().ToUpper();
                                        lblBAmt.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(headwsBankAmt)));
                                        //    BankAmt = BankAmt + Convert.ToDouble(lblBAmt.Text);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }



            //ADDED BY TANU for total bank  amount   23/11/2021
            foreach (GridViewRow gvr in GridData.Rows)
            {
                Label lblBAmt = (Label)gvr.FindControl("lblBankAmt") as Label;
                Label FeeHeadsStatus = (Label)gvr.FindControl("lblFeeHeadsStatus") as Label;
                if (FeeHeadsStatus != null)
                {
                    if (FeeHeadsStatus.Text == "Mapped")
                    {

                        BankAmt += Convert.ToDouble(lblBAmt.Text);
                    }
                }

            }

        }
        //if (dsBankAmt.Tables[0].Rows.Count > 0)
        //{
        //    if (GridData.Rows.Count > 0)
        //    {
        //        int x = 0;
        //        for (x = 0; x < GridData.Rows.Count; x++)
        //        {
        //            Label lblBAmt = GridData.Rows[x].FindControl("lblBankAmt") as Label;
        //            if (lblBAmt != null)
        //            {
        //                //String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(dsBankAmt.Tables[0].Rows[0][x].ToString().Trim())));
        //               // lblBAmt.Text = dsBankAmt.Tables[0].Rows[0][x].ToString().Trim();
        //                string Bamt = dsBankAmt.Tables[0].Rows[0][x].ToString().Trim();
        //                if (Bamt == "")
        //                    Bamt = "0.0";

        //                lblBAmt.Text = String.Format("{0:0.00}", Math.Abs(Convert.ToDouble(Bamt)));

        //                if (lblBAmt.Text == "")
        //                    lblBAmt.Text = "0.0";

        //                BankAmt = BankAmt + Convert.ToDouble(lblBAmt.Text);
        //            }
        //        }
        //    }
        //}
        // lblBankTotal.Style("color") = "#0066FF";

        lblBankTotal.ForeColor = System.Drawing.Color.Red;
        //String.Format("{0:0.00}", Math.Abs(cashAmt))
        //BankAmt.ToString();
        lblBankTotal.Text = String.Format("{0:0.00}", Math.Abs(BankAmt));

        if (cashAmt == 0)
            btnCash.Enabled = false;
        if (BankAmt == 0)
            btnTrans.Enabled = false;
        if (BankAmt != 0)
            btnTrans.Enabled = true;
        if (cashAmt != 0)
            btnCash.Enabled = true;

        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            Label lblBankAmount = GridData.Rows[i].FindControl("lblBankAmt") as Label;
            Label lblFeeHeadsNo = GridData.Rows[i].FindControl("lblFeeHeadsNo") as Label;
            string BankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "BANKNO", "FEE_HEAD_NO='" + lblFeeHeadsNo.Text + "' and RECIEPT_TYPE='" + ddlRecept.SelectedValue + "'");
            if (BankNo == "0" && lblBankAmount.Text != "0.0")
            {
                //objCommon.DisplayUserMessage(UPDLedger, "Some Ledger Having Amount But Bank is Not Map", this.Page);
                //return;
                btnTrans.Attributes.Add("onClick", "return AskSave();");

            }
        }

        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            Label lblCashAmt = GridData.Rows[i].FindControl("lblCashAmt") as Label;
            Label lblFeeHeadsNo = GridData.Rows[i].FindControl("lblFeeHeadsNo") as Label;
            string CASHNO = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "CASHNO", "FEE_HEAD_NO='" + lblFeeHeadsNo.Text + "' and RECIEPT_TYPE='" + ddlRecept.SelectedValue + "'");
            if (CASHNO == "0" && lblCashAmt.Text != "0.0")
            {
                //objCommon.DisplayUserMessage(UPDLedger, "Some Ledger Having Amount But Cash is Not Map", this.Page);
                //return;
                btnTrans.Attributes.Add("onClick", "return AskSave();");

            }
        }
        int ChkStatus = 0;
        for (int y = 0; y < GridData.Rows.Count; y++)
        {
            Label FeeHeadsStatus = GridData.Rows[y].FindControl("lblFeeHeadsStatus") as Label;
            if (FeeHeadsStatus != null)
            {
                if (FeeHeadsStatus.Text == "UnMapped")
                {
                    ChkStatus = ChkStatus + 1;

                }
            }
        }
        if (ChkStatus != 0)
        {
            btnTrans.Attributes.Add("onClick", "return askConfirm();");
            btnCash.Attributes.Add("onClick", "return askConfirm();");
        }

    }
    /// <summary>
    /// Get Fees title And Head no
    /// </summary>
    /// <param name="rpt_Type"></param>
    /// <returns></returns>
    public DataSet GetFeeHeadAndNo(string rpt_Type)
    {
        DataSet dtFeesheads = new DataSet();
        //dtFeesheads =null;
        try
        {
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            ////AND FEE_TITLE !='" + temp + "'
            //string temp = " ";
            //string SelectStr = "SELECT FEE_HEAD_NO,FEE_TITLE FROM FEE_TITLE WHERE RECIEPT_TYPE='" + rpt_Type + "' AND FEE_TITLE !='" + temp + "'";
            //SqlDataAdapter DAFeehead = new SqlDataAdapter(SelectStr, sqlcon);
            //DAFeehead.Fill(dtFeesheads);
            ////return dtFeesheads;

            string temp = " ";
            objCommon = new Common();
            dtFeesheads = objCommon.FillDropDown("FEE_TITLE", "FEE_HEAD_NO", "FEE_TITLE", "RECIEPT_TYPE='" + rpt_Type + "' AND FEE_TITLE !='" + temp + "'", "");

            if (dtFeesheads != null && dtFeesheads.Tables[0].Rows.Count > 0)
            {
                return dtFeesheads;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return dtFeesheads;
    }

    protected void btnSave0_Click(object sender, EventArgs e)
    {

    }
    protected void UpdateRecord()
    {
        FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
        AccountTransactionController objAccTran = new AccountTransactionController();

        int i = 0;

        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());

                FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();

                Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                FLHMobj.FEE_HEAD_NO = lblHNO.Text;

                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                FLHMobj.LEDGER_NO = Convert.ToInt32(ddlFH.SelectedValue.ToString());



                //Label lbl =GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                //string temp = lbl.Text;

                DropDownList ddlcas = GridData.Rows[Icount].FindControl("ddllCash") as DropDownList;
                FLHMobj.CASH_NO = Convert.ToInt32(ddlcas.SelectedValue.ToString());

                DropDownList ddlBank = GridData.Rows[Icount].FindControl("ddlBank") as DropDownList;
                FLHMobj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue.ToString());
                FLHMobj.LASTMODIFIER_DATE = DateTime.Now;
                //FLHMobj.CREATE_DATE = DateTime.Now;
                //FLHMobj.CREATER_NAME = Session["username"].ToString();
                FLHMobj.LASTMODIFIER = Session["username"].ToString();
                i = objAccTran.UpdateFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim(), 0, "0");
            }
        }
        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully", this);
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Record Not Updated ", this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    private void ClearAll()
    {
        if (Session["IsCCMS"].ToString() == "Y")
        {
            row18.Visible = false;
            Div3.Visible = false;

        }
        else
        {
            row18.Visible = true;
            row4.Visible = true;
            Div3.Visible = true;


        }
        GridData.DataSource = null;
        GridData.DataBind();
        btnTrans.Enabled = false;
        //txtFromDate.Text = string.Empty;
        //txtFromDate.Text = string.Empty;
        //txtFromDate.Focus();
        lblCashTotal.Text = string.Empty;
        lblBankTotal.Text = string.Empty;
        rowgrid.Visible = false;
        btnTrans.Attributes.Remove("onClick");
    }
    public void PopulateDegreeDropdown()
    {
        try
        {
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            //string temp = " ";
            //string OraSelectStr = "SELECT DEGREENO,DEGREENAME FROM ACD_DEGREE WHERE DEGREENAME !='" + temp + "'";
            //SqlDataAdapter ODADegree = new SqlDataAdapter(OraSelectStr, sqlcon);
            //DataTable DTDegree = new DataTable();
            //ODADegree.Fill(DTDegree);

            //if (DTDegree.Rows.Count > 0)
            //{
            //    ddlDegree.DataTextField = "DEGREENAME";
            //    ddlDegree.DataValueField = "DEGREENO";
            //    ddlDegree.DataSource = DTDegree;
            //    ddlDegree.DataBind();
            //}

            //string temp = " ";
            objCommon = new Common();
            //DataSet ds = objCommon.FillDropDown("acd_degree", "DEGREENO", "DEGREENAME", "DEGREENAME !='" + temp + "' AND DEGREENO>0", "");
            DataSet ds = objTrans.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0]; ;
                    ddlDegree.DataBind();
                }
            }

            //Added by vijay andoju on 03092020 for department filter
            //objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO IN( SELECT distinct deptno FROM ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD WHERE DEGREENO=" + ddlDegree.SelectedValue + " )", "DEPTNO");
            objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO IN( SELECT distinct BRANCHNO FROM ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD WHERE DEGREENO=" + ddlDegree.SelectedValue + " )", "BRANCHNO");
            //objCommon.FillDropDownList(ddlDept, "ACD_COLLEGE_DEGREE_BRANCH B INNER JOIN ACD_DEPARTMENT D ON(D.DEPTNO=B.DEPTNO)", " D.DEPTNO", "D.DEPTNAME", "B.DEGREENO="+ddlDegree.SelectedValue, "D.DEPTNAME");
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void PopulateReceptTypeDropdown()
    {

        try
        {
            //string OraSelectStr = "SELECT RECIEPT_CODE,RECIEPT_TITLE FROM ACD_RECIEPT_TYPE";
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            //SqlDataAdapter ODArcpt = new SqlDataAdapter(OraSelectStr, sqlcon);
            //DataTable DTrcpt = new DataTable();
            //ODArcpt.Fill(DTrcpt);
            //if (DTrcpt.Rows.Count > 0)
            //{
            //    ddlRecept.DataTextField = "RECIEPT_TITLE";
            //    ddlRecept.DataValueField = "RECIEPT_CODE";
            //    ddlRecept.DataSource = DTrcpt;
            //    ddlRecept.DataBind();
            //}

            objCommon = new Common();
            DataSet ds = new DataSet();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                ds = objTrans.GetReceiptTypeForCCMS(_CCMS);
            }
            else
            {
                //ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                ds = objTrans.PopulateReceiptType(_CCMS);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRecept.Items.Clear();
                ddlRecept.Items.Add("Please Select");
                ddlRecept.SelectedItem.Value = "0";
                ddlRecept.DataTextField = "RECIEPT_TITLE";
                ddlRecept.DataValueField = "RECIEPT_CODE";
                ddlRecept.DataSource = ds.Tables[0];
                ddlRecept.DataBind();
                ddlRecept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void GridData_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    //protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ClearAll();
    //}
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        // objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO IN( SELECT distinct deptno FROM ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD WHERE DEGREENO=" + ddlDegree.SelectedValue + " )", "DEPTNO");
        objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO IN( SELECT distinct BRANCHNO FROM ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD WHERE DEGREENO=" + ddlDegree.SelectedValue + " )", "BRANCHNO");
    }
    protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        row4.Visible = true;
    }
    protected void btnTrans_Click(object sender, EventArgs e)
    {

        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry.....No DATA... Transaction Not Posible", this);
            return;
        }
        int ChkStatus = 0;
        for (int y = 0; y < GridData.Rows.Count; y++)
        {
            Label FeeHeadsStatus = GridData.Rows[y].FindControl("lblFeeHeadsStatus") as Label;
            if (FeeHeadsStatus != null)
            {
                if (FeeHeadsStatus.Text == "UnMapped")
                {
                    ChkStatus += ChkStatus;

                }
            }
        }
        if (ChkStatus != 0)
        {
            btnCash.Attributes.Add("onClick", "return askConfirm();");
            btnTrans.Attributes.Add("onClick", "return askConfirm();");
        }
        //Code To Check Whole Head Is Map Or Not
        //Added By Nitin Meshram on Date 07-05-2014
        if (Session["AllowFullMapping"].ToString() == "Y")
        {
            if (rdoGenralFees.Checked == true)
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {

                    int FeeHead = objTrans.GetCountOfFeeHeadForCCMS(ddlRecept.SelectedValue, _CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' and FEE_HEAD_NO<>'EF'"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
                else
                {
                    //int FeeHead = Convert.ToInt32(objCommon.LookUp("ACD_FEE_TITLE", "COUNT(FEE_HEAD)", "RECIEPT_CODE='" + ddlRecept.SelectedValue + "' and FEE_HEAD !='' and FEE_LONGNAME<>''"));
                    int FeeHead = objTrans.GetCountOfFeeHeadForRFCampus(ddlRecept.SelectedValue.ToString(), _CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' and BRANCHNO=" + ddlDept.SelectedValue + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.ToString()) + " and FEE_HEAD_NO NOT IN ('EF','LF') "));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }

            }
            else
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    int FeeHead = objTrans.GetCountMiscFeeHaedForCCMS(_CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE=''"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
                else
                {
                    //int FeeHead = Convert.ToInt32(objCommon.LookUp("ACD_MISCELLANEOUS_HEAD", "count(MHEADCODE)", ""));
                    int FeeHead = objTrans.GetCountOfMiscFeeHeadForRFCampus(_CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='MF' AND DEGREENO=0"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
            }
        }


        if (hdnAskSave.Value.ToString() == "0")
        {
            btnTrans.Attributes.Remove("onClick");
            return;
        }
        DataSet DsEntrys = new DataSet();
        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=0 AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
            }
            else
            {
                DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
            }
        }
        else
        {
            DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=0 AND CBTYPE='MF' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
        }
        if (DsEntrys.Tables[0].Rows.Count > 0)
        {
            //objCommon.DisplayMessage(UPDLedger, "Fees already transfered on date " + Convert.ToDateTime(DsEntrys.Tables[0].Rows[0]["TRANSACTION_DATE"]).ToString("dd-MMM-yyyy"), this);
            //return;
            //DeleteTransfer();
        }
        Transfer();
        if (rdoGenralFees.Checked)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                row18.Visible = false;

            }
            else
            {
                row18.Visible = true;
                row4.Visible = true;
                Div3.Visible = true;
            }
        }
        else
        {
            row18.Visible = false;
            row18.Visible = false;
            row4.Visible = false;
            Div3.Visible = false;
        }
    }
    //not required 16052013
    public bool CheckEntry()
    {
        bool result = false;
        try
        {
            DataSet DsEntrys = new DataSet();
            //objCommon.FillDropDown("ACC_FEE_"+ Session["comp_code"].ToString().Trim() +"_LEDERHEAD", "*", "", "RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND COLLEGE_CODE='" + ddlDegree.SelectedValue.ToString() + "'", "");
            //DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSACTION_DATE='" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "' AND TRANSFER_ENTRY='1' AND DEGREE_NO="+Convert.ToInt32( ddlDegree.SelectedValue)+" AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "'", "");
            DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "'", "TRANSACTION_DATE");
            if (DsEntrys.Tables[0].Rows.Count > 0)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.CheckEntry-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return result;
    }

    protected void Transfer()
    {
        AccountTransactionController objAccTran = new AccountTransactionController();
        int degNo = 0;
        string retype = String.Empty;
        string DegreeName = String.Empty;
        string RecieptType = String.Empty;
        int i = 0;

        DataTable TransFieldsTbl = new DataTable("TransFieldsTbl");
        TransFieldsTbl.Columns.Add("MISCHEADSRNO", typeof(int));


        DataRow dr = null;
        for (int y = 0; y < GridData.Rows.Count; y++)
        {
            Label lblCash = GridData.Rows[y].FindControl("lblCashAmt") as Label;
            HiddenField hdnMISCHEADSRNO = GridData.Rows[y].FindControl("hdnMISCHEADSRNO") as HiddenField;

            if (Convert.ToDouble(lblCash.Text) != 0.00)
            {
                if (hdnMISCHEADSRNO != null)
                {
                    dr = TransFieldsTbl.NewRow();
                    dr["MISCHEADSRNO"] = Convert.ToInt32(hdnMISCHEADSRNO.Value);


                    TransFieldsTbl.Rows.Add(dr);
                }
            }
        }
        objStrMst.TransFieldsTbl_TRAN = TransFieldsTbl;





        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                degNo = 0;
                DegreeName = "";
            }
            else
            {
                degNo = Convert.ToInt32(ddlDegree.SelectedValue);
                DegreeName = ddlDegree.SelectedItem.ToString().Trim();
            }
            retype = ddlRecept.SelectedValue.ToString().Trim();

            RecieptType = ddlRecept.SelectedItem.ToString().Trim();
            string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            int Uno = Convert.ToInt32(Session["userno"].ToString().Trim());
            string compCode = Session["comp_code"].ToString().Trim();
            string collegeCode = ddlDegree.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                string[] database = _CCMS.Split('=');
                string databaseName = database[4].ToString();
                i = objAccTran.AddFeeAccountTransferFromCCMS_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), "", RecieptType, databaseName);
            }
            else
            {
                string[] database = _CCMS.Split('=');
                //string databaseName = database[4].ToString();//OLD
                //ADDED BY VIJAY ANDOJU ON 04-09-2020 FOR DATABASE NAME
                string databaseName = "[" + database[4].ToString() + "]";
                if (ViewState["PaymentTypeWise"].ToString() == "N")
                {
                    if (strManual == "Y")
                    {

                        i = objAccTran.AddFeeAccountTransfer_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, ddlDept.SelectedValue);
                        //i = objAccTran.AddFeeAccountTransfer_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                    }
                    else
                    {

                        i = objAccTran.AddFeeAccountTransfer_BANK_MANUAL(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                    }
                }
                else if (ViewState["PaymentTypeWise"].ToString() == "Y")
                {
                    string paymenttypenos = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PAYTYPENO", "PGROUP_NO=" + ddlGrpPayment.SelectedValue.ToString());
                    if (strManual == "Y")
                    {
                        i = objAccTran.AddFeeAccountTransfer_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paymenttypenos, Convert.ToInt32(ddlGrpPayment.SelectedValue));
                    }
                    else
                    {
                        i = objAccTran.AddFeeAccountTransfer_BANK_MANUAL(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paymenttypenos, Convert.ToInt32(ddlGrpPayment.SelectedValue));

                    }
                }
            }
        }
        else
        {
            DegreeName = "";
            retype = "MF";
            RecieptType = "Miscellaneous Fees";
            //   string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            //  string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");


            string DateFrom = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
            DateFrom = DateFrom.Substring(0, 10);

            string DateTo = (String.Format("{0:u}", Convert.ToDateTime(txtTodate.Text)));
            DateTo = DateTo.Substring(0, 10);



            int Uno = Convert.ToInt32(Session["userno"].ToString().Trim());
            string compCode = Session["comp_code"].ToString().Trim();
            string collegeCode = ddlDegree.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                string[] database = _CCMS.Split('=');
                string databaseName = "[" + database[4].ToString() + "]";
                i = objAccTran.AddMISCFeeAccountTransferFromCCMS_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
            }
            else
            {
                string[] database = _CCMS.Split('=');
                string databaseName = "[" + database[4].ToString() + "]";
                //i = objAccTran.AddMISCFeeAccountTransfer(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType);
                if (strManual == "Y")
                {
                    // i = objAccTran.AddMISCFeeAccountTransfer_BANK(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                    // added by tanu 02/11/2021
                    if (rdbReceipt.Checked == true)
                    {
                        paytype = "R";
                    }
                    if (rdbPayment.Checked == true)
                    {
                        paytype = "P";
                    }
                    if (rdbReceipt.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_FOR_RECEIPT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);
                    }
                    else if (rdbPayment.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_FOR_PAYMENT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);

                    }
                    else
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_NPAYT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, objStrMst);
                    }
                }
                else
                {
                    // i = objAccTran.AddMISCFeeAccountTransfer_BANK_MANUAL_VCH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);

                    // added by tanu 02/11/2021
                    if (rdbReceipt.Checked == true)
                    {
                        paytype = "R";
                    }
                    if (rdbPayment.Checked == true)
                    {
                        paytype = "P";
                    }
                    if (rdbReceipt.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_FOR_RECEIPT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);
                    }
                    else if (rdbPayment.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_FOR_PAYMENT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);

                    }
                    else
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_BANK_MANUAL_VCH_NPAYT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, objStrMst);
                    }
                }
            }

        }


        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Account Transfer Successfully", this);
            ClearAll();
            //btnShowData_Click(object sender, EventArgs e);
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry.... Account Transfer Fail ", this);
        }
    }

    protected void Transfer_Cash()
    {
        AccountTransactionController objAccTran = new AccountTransactionController();
        int degNo = 0;
        string retype = String.Empty;
        string DegreeName = String.Empty;
        string RecieptType = String.Empty;
        int i = 0;



        DataTable TransFieldsTbl = new DataTable("TransFieldsTbl");
        TransFieldsTbl.Columns.Add("MISCHEADSRNO", typeof(int));


        DataRow dr = null;
        for (int y = 0; y < GridData.Rows.Count; y++)
        {
            Label lblCash = GridData.Rows[y].FindControl("lblCashAmt") as Label;
            HiddenField hdnMISCHEADSRNO = GridData.Rows[y].FindControl("hdnMISCHEADSRNO") as HiddenField;

            if (Convert.ToDouble(lblCash.Text) != 0.00)
            {
                if (hdnMISCHEADSRNO != null)
                {
                    dr = TransFieldsTbl.NewRow();
                    dr["MISCHEADSRNO"] = Convert.ToInt32(hdnMISCHEADSRNO.Value);


                    TransFieldsTbl.Rows.Add(dr);
                }
            }
        }
        objStrMst.TransFieldsTbl_TRAN = TransFieldsTbl;


        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                degNo = 0;
                DegreeName = "";
            }
            else
            {
                degNo = Convert.ToInt32(ddlDegree.SelectedValue);
                DegreeName = ddlDegree.SelectedItem.ToString().Trim();
            }
            retype = ddlRecept.SelectedValue.ToString().Trim();

            RecieptType = ddlRecept.SelectedItem.ToString().Trim();
            string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            int Uno = Convert.ToInt32(Session["userno"].ToString().Trim());
            string compCode = Session["comp_code"].ToString().Trim();
            string collegeCode = ddlDegree.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                string[] database = _CCMS.Split('=');
                string databaseName = database[4].ToString();
                i = objAccTran.AddFeeAccountTransferFromCCMS_CASH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), "", RecieptType, databaseName);
            }
            else
            {
                string[] database = _CCMS.Split('=');
                string databaseName = database[4].ToString();
                if (ViewState["PaymentTypeWise"].ToString() == "N")
                {
                    if (strManual == "Y")
                        i = objAccTran.AddFeeAccountTransfer_CASH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                    else
                        i = objAccTran.AddFeeAccountTransfer_CASH_manual(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                }
                else if (ViewState["PaymentTypeWise"].ToString() == "Y")
                {
                    string paymenttypenos = objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PAYTYPENO", "PGROUP_NO=" + ddlGrpPayment.SelectedValue.ToString());
                    if (strManual == "Y")
                        i = objAccTran.AddFeeAccountTransfer_CASH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paymenttypenos, Convert.ToInt32(ddlGrpPayment.SelectedValue));
                    else
                        i = objAccTran.AddFeeAccountTransfer_CASH_manual(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paymenttypenos, Convert.ToInt32(ddlGrpPayment.SelectedValue));
                }
            }
        }
        else
        {
            DegreeName = "";
            retype = "MF";
            RecieptType = "Miscellaneous Fees";
            // string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            // string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");


            string DateFrom = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
            DateFrom = DateFrom.Substring(0, 10);

            string DateTo = (String.Format("{0:u}", Convert.ToDateTime(txtTodate.Text)));
            DateTo = DateTo.Substring(0, 10);




            //Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            int Uno = Convert.ToInt32(Session["userno"].ToString().Trim());
            string compCode = Session["comp_code"].ToString().Trim();
            string collegeCode = ddlDegree.SelectedValue.ToString().Trim();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                string[] database = _CCMS.Split('=');
                string databaseName = database[4].ToString();
                i = objAccTran.AddMISCFeeAccountTransferFromCCMS_CASH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
            }
            else
            {
                string[] database = _CCMS.Split('=');
                string databaseName = "[" + database[4].ToString() + "]";
                //i = objAccTran.AddMISCFeeAccountTransfer(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType);
                if (strManual == "Y")
                {
                    //  i = objAccTran.AddMISCFeeAccountTransfer_CASH(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);

                    if (rdbReceipt.Checked == true)
                    {
                        paytype = "R";
                    }
                    if (rdbPayment.Checked == true)
                    {
                        paytype = "P";
                    }
                    if (rdbReceipt.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_FOR_RECEIPT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);
                    }
                    else if (rdbPayment.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_FOR_PAYMENT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);

                    }
                    else
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_NPAYT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, objStrMst);
                    }
                }
                else
                {
                    //i = objAccTran.AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName);
                    // added by tanu 02/11/2021
                    if (rdbReceipt.Checked == true)
                    {
                        paytype = "R";
                    }
                    if (rdbPayment.Checked == true)
                    {
                        paytype = "P";
                    }
                    if (rdbReceipt.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_FOR_RECEIPT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);
                    }
                    else if (rdbPayment.Checked == true)
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_FOR_PAYMENT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, paytype, objStrMst);

                    }
                    else
                    {
                        i = objAccTran.AddMISCFeeAccountTransfer_CASH_MANUAL_VCHNO_NPAYT(DateFrom, DateTo, Uno, compCode, degNo, retype, Session["colcode"].ToString(), DegreeName, RecieptType, databaseName, objStrMst);
                    }

                }
            }
        }


        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Account Transfer Successfully", this);
            ClearAll();
            //btnShowData_Click(object sender, EventArgs e);
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry.... Account Transfer Fail ", this);
        }
    }
    public void DeleteTransfer()
    {
        try
        {
            AccountTransactionController objAccTran = new AccountTransactionController();
            string DateFrom = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            string DateTo = Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy");

            string compCode = Session["comp_code"].ToString().Trim();
            string CBtype = ddlRecept.SelectedValue.ToString().Trim();
            int degNo = Convert.ToInt32(ddlDegree.SelectedValue);

            //string strdelete = "Delete From ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS Where CBTYPE='" + CBtype + "' AND TRANSFER_ENTRY='1' AND TRANSACTION_DATE='"+DateFrom+"'";
            //string ConStr= System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            //SqlConnection sqlcon = new SqlConnection(ConStr);
            //SqlCommand cmd = new SqlCommand(strdelete, sqlcon);
            //sqlcon.Open();

            //int i=Convert.ToInt32(cmd.ExecuteNonQuery());
            int i = objAccTran.deleteTransactionForTransfer(DateFrom, DateTo, compCode, degNo, CBtype);

            //sqlcon.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeAccountTransfer.DeleteTransfer-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdoMiscFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row18.Visible = false;
        row4.Visible = false;
        Div3.Visible = false;
        TrPaytype.Visible = false;
    }
    protected void rdoGenralFees_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        row4.Visible = true;
        if (Session["IsCCMS"].ToString() != "Y")
        {
            row18.Visible = true;
            if (ViewState["PaymentTypeWise"].ToString() == "Y")
                TrPaytype.Visible = true;
            else
                TrPaytype.Visible = false;
        }
    }
    protected void btnCash_Click(object sender, EventArgs e)
    {
        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Sorry.....No DATA... Transaction Not Posible", this);
            return;
        }

        //Code To Check Whole Head Is Map Or Not
        //Added By Nitin Meshram on Date 07-05-2014
        if (Session["AllowFullMapping"].ToString() == "Y")
        {
            if (rdoGenralFees.Checked == true)
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    int FeeHead = objTrans.GetCountOfFeeHeadForCCMS(ddlRecept.SelectedValue, _CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' and FEE_HEAD_NO<>'EF'"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
                else
                {
                    int FeeHead = objTrans.GetCountOfFeeHeadForRFCampus(ddlRecept.SelectedValue.ToString(), _CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue.ToString()) + " and FEE_HEAD_NO NOT IN ('EF','LF')"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
            }
            else
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    int FeeHead = objTrans.GetCountMiscFeeHaedForCCMS(_CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE=''"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
                else
                {
                    int FeeHead = objTrans.GetCountOfMiscFeeHeadForRFCampus(_CCMS);
                    int LedgerHead = Convert.ToInt32(objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "count(FEE_HEAD_NO)", "RECIEPT_TYPE='MF' AND DEGREENO=0"));
                    if (FeeHead != LedgerHead)
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Head", this.Page);
                        return;
                    }
                }
            }
        }
        if (hdnAskSave.Value.ToString() == "0")
        {
            btnTrans.Attributes.Remove("onClick");
            return;
        }
        DataSet DsEntrys = new DataSet();
        if (rdoGenralFees.Checked == true)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=0 AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' AND CBTYPE_STATUS='C' ", "TRANSACTION_DATE");
            }
            else
            {
                DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CBTYPE='" + ddlRecept.SelectedValue.ToString() + "' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' AND CBTYPE_STATUS='C'", "TRANSACTION_DATE");
            }
        }
        else
        {
            DsEntrys = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_TRANS", "*", "TRANSACTION_TYPE", "TRANSFER_ENTRY='1' AND DEGREE_NO=0 AND CBTYPE='MF' AND TRANSACTION_DATE BETWEEN '" + (Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy")) + "' AND '" + (Convert.ToDateTime(txtTodate.Text).ToString("dd-MMM-yyyy")) + "' AND CBTYPE_STATUS='C'", "TRANSACTION_DATE");
        }
        if (DsEntrys.Tables[0].Rows.Count > 0)
        {
            //objCommon.DisplayMessage(UPDLedger, "Fees already transfered on date " + Convert.ToDateTime(DsEntrys.Tables[0].Rows[0]["TRANSACTION_DATE"]).ToString("dd-MMM-yyyy"), this);
            //return;
            //DeleteTransfer();
        }
        Transfer_Cash();
        if (rdoGenralFees.Checked)
        {
            if (Session["IsCCMS"].ToString() == "Y")
            {
                row18.Visible = false;
            }
            else
            {
                row18.Visible = true;
                row4.Visible = true;
            }
        }
        else
        {
            row18.Visible = false;
            row18.Visible = false;
            row4.Visible = false;
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}