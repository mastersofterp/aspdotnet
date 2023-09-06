//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACCOUNT                                                     
// CREATION DATE : 14-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
// MODIFIED BY   : 
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
public partial class FeeLedgerHeadMapping : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController objAccTran = new AccountTransactionController();
    //OracleConnection ocon = new OracleConnection("Data Source=VNITFEES;UID=WCEFEES;PWD=WCEFEES");
    //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string IsBatchWise = string.Empty;
    string IsSemWise = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnSave.Enabled = false;

        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        { }
        else
            Response.Redirect("~/Default.aspx");

        if (!Page.IsPostBack)
        {
            btnSave.Enabled = false;
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

                    //Fill dropdown list
                    // Filling Degrees

                    //Filling Recept list


                    ViewState["Operation"] = "Submit";
                }
            }

            string IsCCMS = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='IS CCMS'");
            string AllowFullMapping = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='MAP ALL LEDGER FOR FEES TRANSFER'");
            Session["AllowFullMapping"] = AllowFullMapping;
            if (IsCCMS == "Y")
            {
                row18.Visible = false;
                Session["IsCCMS"] = IsCCMS;
                PopulateReceptTypeDropdown();
            }
            else
            {
                PopulateDegreeDropdown();
                row18.Visible = true;
                Session["IsCCMS"] = IsCCMS;
                PopulateReceptTypeDropdown();
                PopulateBranchDropdown();
            }

             IsBatchWise = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='IS FEES TRANSFER BATCH WISE'");
            if (IsBatchWise == "Y")
            {
                Divbatch.Visible = true;
            }
            else
            {
                Divbatch.Visible = false;
            }
             IsSemWise = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC='IS FEES TRANSFER SEMESTER WISE'");
            if (IsSemWise == "Y")
            {
                DivSem.Visible = true;
            }
            else
            {
                DivSem.Visible = false;
            }
            objCommon.FillDropDownList(ddlbatch, "acd_admbatch", "BATCHNO", "BATCHNAME", "BATCHNO<>0", "BATCHNO");
            objCommon.FillDropDownList(ddsem, "acd_semester", "SEMESTERNO", "SEMFULLNAME", "SEMESTERNO<>0", "SEMESTERNO");
           
            rowgrid.Visible = false;
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }


    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Degree", this);
            //    ddlDegree.Focus();
            //    return;
            //}
            if (rdoGenralFees.Checked == true)
            {
                if (ddlDegree.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Degree", this);
                    ddlRecept.Focus();
                    return;
                }
                if (ddlBranch.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Branch", this);
                    ddlRecept.Focus();
                    return;
                }
                if (ddlbatch.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Admission Batch", this);
                    ddlRecept.Focus();
                    return;
                }
                if (ddsem.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(ddsem, "Please Select Semester", this);
                    ddlRecept.Focus();
                    return;
                }
                if (ddlRecept.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Receipt Type", this);
                    ddlRecept.Focus();
                    return;
                }
               
              
            }
            else
            {

            }

            rowgrid.Visible = true;
            ViewState["Operation"] = "Submit";
            btnSave.Enabled = true;


            //DataTable dtFeesheads = new DataTable();
            DataSet dtFeesheads = new DataSet();

            if (rdoGenralFees.Checked == true)
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    dtFeesheads = objAccTran.GetFeeHeadForCCMS(ddlRecept.SelectedValue.ToString(), _CCMS);
                }
                else
                {
                    dtFeesheads = GetFeeHeadAndNo(ddlRecept.SelectedValue.ToString());
                }

                if (dtFeesheads.Tables[0].Rows.Count == 0)
                {
                    // objCommon.DisplayMessage("DATA NOT AVAILABLE", this);
                    GridData.DataBind();
                    objCommon.DisplayMessage(UPDLedger, "DATA NOT AVAILABLE", this);
                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    dtFeesheads.Tables[0].Rows.Add("EF", "Excess Fees");
                    dtFeesheads.Tables[0].Rows.Add("LF", "Late Fees");
                    dtFeesheads.Tables[0].Rows.Add("CF", "CANCELLATION CHARGES");
                }
                GridData.DataSource = dtFeesheads;
                GridData.DataBind();
            }
            else
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    dtFeesheads = objAccTran.GetMiscFeeHeadForCCMS(_CCMS);
                }
                else
                {
                    //dtFeesheads = objCommon.FillDropDown("ACD_MISCELLANEOUS_HEAD", "MHEADCODE as FEE_HEAD", "MHEADNAME as FEE_LONGNAME", "", "");
                    dtFeesheads = objAccTran.GetFeeHeadForMF(_CCMS);
                }
                if (dtFeesheads.Tables[0].Rows.Count == 0)
                {
                    // objCommon.DisplayMessage("DATA NOT AVAILABLE", this);
                    GridData.DataBind();
                    objCommon.DisplayMessage(UPDLedger, "DATA NOT AVAILABLE", this);
                    btnSave.Enabled = false;
                    return;
                }
                GridData.DataSource = dtFeesheads;
                GridData.DataBind();
            }

            if (GridData.Rows.Count > 0)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;

            //Filling dropdown lists 
            if (GridData.Rows.Count > 0)
            {
                int x = 0;
                for (x = 0; x < GridData.Rows.Count; x++)
                {
                    // objCommon.FillDropDownList(ddlledger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "");

                    DropDownList ddlledger = GridData.Rows[x].FindControl("ddlleagerHead") as DropDownList;
                    if (ddlledger != null)
                    {
                        objCommon.FillDropDownList(ddlledger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO NOT IN (1,2)", "PARTY_NAME");
                        //DataSet dsLH = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO NOT IN (1,2)", "");
                        //ddlledger.DataSource = dsLH.Tables[0];
                        //ddlledger.DataTextField = "PARTY_NAME";
                        //ddlledger.DataValueField = "PARTY_NO";
                        //ddlledger.DataBind();
                    }

                    DropDownList ddllcash = GridData.Rows[x].FindControl("ddllCash") as DropDownList;
                    if (ddllcash != null)
                    {
                        objCommon.FillDropDownList(ddllcash, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=1", "");
                        DataSet dsCH = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO =1", "");
                        //ddllcash.DataSource = dsCH.Tables[0];
                        //ddllcash.DataTextField = "PARTY_NAME";
                        //ddllcash.DataValueField = "PARTY_NO";
                        //ddllcash.DataBind();

                        for (int i = 0; i < dsCH.Tables[0].Rows.Count; i++)
                        {
                            if (dsCH.Tables[0].Rows[i][0].ToString().Equals("") || dsCH.Tables[0].Rows[i][1].ToString().Equals(""))
                            {
                                btnSave.Enabled = false;
                                //return;
                            }
                        }
                    }

                    DropDownList ddllBank = GridData.Rows[x].FindControl("ddlBank") as DropDownList;
                    if (ddllBank != null)
                    {
                        objCommon.FillDropDownList(ddllBank, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=2", "");
                        DataSet dsBank = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NAME", "PARTY_NO", "PAYMENT_TYPE_NO =2", "");


                        for (int i = 0; i < dsBank.Tables[0].Rows.Count; i++)
                        {
                            if (dsBank.Tables[0].Rows[i][0].ToString().Equals("") || dsBank.Tables[0].Rows[i][1].ToString().Equals(""))
                            {
                                btnSave.Enabled = false;
                                //return;
                            }
                        }
                    }
                }
                //btnSave.Enabled = true;
            }







            // updated by jitendra
            //if Data aalready present in table


            //Remain For CCMS After INSERTION
            if (GridData.Rows.Count > 0)
            {
                for (int k = 0; k < GridData.Rows.Count; k++)
                {
                    Label lblfeehd = GridData.Rows[k].FindControl("lblFeeHeadsNo") as Label;


                    //LEDGER
                    string sledgerNo = string.Empty;
                    if (rdoGenralFees.Checked == true)
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + "    AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + "  AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND  LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + "  AND BATCHNO=" + ddlbatch.SelectedValue + "  AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + "  AND BATCHNO=" + ddlbatch.SelectedValue + "  AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                        }
                    }
                    else
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            sledgerNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "LEDGERNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                    }
                    if (!string.IsNullOrEmpty(sledgerNo))
                    {
                        ViewState["Operation"] = "Update";
                        DropDownList ddlledger = GridData.Rows[k].FindControl("ddlleagerHead") as DropDownList;
                        if (ddlledger != null) ddlledger.SelectedValue = sledgerNo;
                    }


                    //CASH
                    string sCashNo = string.Empty;
                    if (rdoGenralFees.Checked == true)
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + " AND LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                            //sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND LEDGERNO>0");
                        }
                    }
                    else
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            sCashNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "CASHNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                    }
                    if (!string.IsNullOrEmpty(sCashNo))
                    {
                        DropDownList ddlledger = GridData.Rows[k].FindControl("ddllCash") as DropDownList;
                        if (ddlledger != null) ddlledger.SelectedValue = sCashNo;
                    }

                    //BANK
                    string sbankNo = string.Empty;
                    if (rdoGenralFees.Checked == true)
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {

                            sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) > 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) == 0)
                            {
                                sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND (COLLEGE_JSS IS NULL OR COLLEGE_JSS = '0')");
                            }
                            else if (Convert.ToInt32(ddlBranch.SelectedValue) == 0 && Convert.ToInt32(ddlAided_NoAided.SelectedValue) > 0)
                            {
                                sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND BATCHNO=" + ddlbatch.SelectedValue + "   AND SEMESTERNO=" + ddsem.SelectedValue + "  AND LEDGERNO>0 AND (BRANCHNO IS NULL OR BRANCHNO = 0) AND COLLEGE_JSS ='" + ddlAided_NoAided.SelectedItem.Text + "'");
                            }
                            //sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='" + ddlRecept.SelectedValue + "' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=" + ddlDegree.SelectedValue + " AND LEDGERNO>0");
                        }
                    }
                    else
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                        else
                        {
                            sbankNo = objCommon.LookUp("ACC_FEE_" + Session["comp_code"].ToString() + "_LEDERHEAD", "BANKNO", "RECIEPT_TYPE='MF' AND UPPER(FEE_HEAD_NO)='" + lblfeehd.Text.ToUpper().Trim() + "' AND DEGREENO=0 AND LEDGERNO>0");
                        }
                    }
                    if (!string.IsNullOrEmpty(sbankNo))
                    {
                        DropDownList ddlledger = GridData.Rows[k].FindControl("ddlBank") as DropDownList;
                        if (ddlledger != null) ddlledger.SelectedValue = sbankNo;
                    }

                }
            }

            //DataSet dsFeeLedgerHead = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "*", "RECIEPT_TYPE", "RECIEPT_TYPE='" + ddlRecept.SelectedValue.ToString() + "' AND LEDGERNO>0 AND COLLEGE_CODE='" + Session["colcode"].ToString() + "'", "");
            //if (dsFeeLedgerHead.Tables[0].Rows.Count > 0)
            //{
            //    if (dsFeeLedgerHead.Tables[0].Rows.Count > 0)
            //    {
            //        ViewState["Operation"] = "Update";
            //        if (GridData.Rows.Count > 0)
            //        {
            //            int y = 0;
            //            //for (y = 0; y < GridData.Rows.Count; y++)
            //            //dsFeeLedgerHead.Tables[0].Rows.Count
            //            for (y = 0; y < dsFeeLedgerHead.Tables[0].Rows.Count; y++)
            //            {
            //                Label lblfeehd = GridData.Rows[y].FindControl("lblFeeHeadsNo") as Label;
            //                if (lblfeehd != null)
            //                {
            //                    if (lblfeehd.Text.ToString().Trim() == dsFeeLedgerHead.Tables[0].Rows[y]["FEE_HEAD_NO"].ToString().Trim())
            //                    {
            //                        DropDownList ddlledger = GridData.Rows[y].FindControl("ddlleagerHead") as DropDownList;
            //                        if (ddlledger != null)
            //                        {
            //                            ddlledger.SelectedValue = dsFeeLedgerHead.Tables[0].Rows[y]["LEDGERNO"].ToString().Trim();
            //                        }
            //                        DropDownList ddllCash = GridData.Rows[y].FindControl("ddllCash") as DropDownList;
            //                        if (ddllCash != null)
            //                        {
            //                            ddllCash.SelectedValue = dsFeeLedgerHead.Tables[0].Rows[y]["CASHNO"].ToString().Trim();
            //                        }
            //                        DropDownList ddlBank = GridData.Rows[y].FindControl("ddlBank") as DropDownList;
            //                        if (ddlBank != null)
            //                        {
            //                            ddlBank.SelectedValue = dsFeeLedgerHead.Tables[0].Rows[y]["BANKNO"].ToString().Trim();
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            if (rdoGenralFees.Checked == true)
            {
                int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and DEGREE_NO='" + ddlDegree.SelectedValue + "' and CBTYPE='" + ddlRecept.SelectedValue + "' and TRANSFER_ENTRY=1"));
                if (count > 0)
                    btnSave.Attributes.Add("onClick", "return askConfirm();");
            }
            else
            {
                int count = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and DEGREE_NO='0' and CBTYPE='MF' and TRANSFER_ENTRY=1"));
                if (count > 0)
                    btnSave.Attributes.Add("onClick", "return askConfirm();");
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.btnShow_Data_Click " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
        try
        {
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            //string temp = " ";
            //string SelectStr = "SELECT FEE_HEAD,FEE_LONGNAME FROM ACD_FEE_TITLE WHERE RECIEPT_CODE='" + rpt_Type + "' AND FEE_HEAD !='" + temp + "'";
            //SqlDataAdapter DAFeehead = new SqlDataAdapter(SelectStr, sqlcon);
            //DAFeehead.Fill(dtFeesheads);

            //string temp = " ";
            objCommon = new Common();



            //dtFeesheads = objCommon.FillDropDown("ACD_FEE_TITLE", "FEE_HEAD", "FEE_LONGNAME", "RECIEPT_CODE='" + rpt_Type + "' AND FEE_HEAD !='" + temp + "' AND FEE_LONGNAME <>''", "");
            dtFeesheads = objAccTran.GetFeeHeadAndNo(rpt_Type, _CCMS);
            if (dtFeesheads != null && dtFeesheads.Tables[0].Rows.Count > 0)
            {
                return dtFeesheads;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.GetFeeHeadAndNo " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return dtFeesheads;
    }

    protected void SaveRecord()
    {
        FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
        AccountTransactionController objAccTran = new AccountTransactionController();

        int i = 0;

        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;

            //delete Fee ledger Head if it is general Fees 
            if (rdoGenralFees.Checked == true)
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    objAccTran.UpdateFeeLedgerHeadMapSetBlank(Session["comp_code"].ToString().Trim(), 0, ddlRecept.SelectedValue.ToString(), Convert.ToInt32(ddlBranch.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text, Convert.ToInt32(ddlbatch.SelectedValue), Convert.ToInt32(ddsem.SelectedValue));
                }
                else
                {
                    objAccTran.UpdateFeeLedgerHeadMapSetBlank(Session["comp_code"].ToString().Trim(), Convert.ToInt32(ddlDegree.SelectedValue), ddlRecept.SelectedValue.ToString(), Convert.ToInt32(ddlBranch.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text, Convert.ToInt32(ddlbatch.SelectedValue), Convert.ToInt32(ddsem.SelectedValue));
                }
            }
            else
            {
                if (Session["IsCCMS"].ToString() == "Y")
                {
                    objAccTran.UpdateFeeLedgerHeadMapSetBlank(Session["comp_code"].ToString().Trim(), 0, "MF", 0, "0",0,0);
                }
                else
                {
                    objAccTran.UpdateFeeLedgerHeadMapSetBlank(Session["comp_code"].ToString().Trim(), 0, "MF", 0, "0",0,0);
                }
            }

            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {
                //FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());
                FLHMobj.COLLEGE = Session["colcode"] == null ? 0 : Convert.ToInt32(Session["colcode"].ToString());
                if (rdoGenralFees.Checked == true)
                {
                    if (Session["IsCCMS"].ToString() == "Y")
                    {
                        FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();
                        FLHMobj.DegreeNo = 0;
                        FLHMobj.BatchNo = 0;
                        FLHMobj.SemesterNo = 0;
                    }
                    else
                    {
                        FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();
                        FLHMobj.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                        FLHMobj.BatchNo = Convert.ToInt32(ddlbatch.SelectedValue);
                        FLHMobj.SemesterNo = Convert.ToInt32(ddsem.SelectedValue);
                    }
                }
                else
                {
                    if (Session["IsCCMS"].ToString() == "Y")
                    {
                        FLHMobj.RECIEPT_TYPE = "MF";
                        FLHMobj.DegreeNo = 0;
                        FLHMobj.BatchNo = 0;
                        FLHMobj.SemesterNo = 0;
                    }
                    else
                    {
                        FLHMobj.RECIEPT_TYPE = "MF";
                        FLHMobj.DegreeNo = 0;
                        FLHMobj.BatchNo = 0;
                        FLHMobj.SemesterNo = 0;
                    }
                }
                Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                FLHMobj.FEE_HEAD_NO = lblHNO.Text;
                Label lblHName = GridData.Rows[Icount].FindControl("lblFeeHeads") as Label;
                FLHMobj.FeeLedger_NAme = lblHName.Text;
                //lblFeeHeads
                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                FLHMobj.LEDGER_NO = Convert.ToInt32(ddlFH.SelectedValue.ToString());
                //ddllCash
                DropDownList ddlcas = GridData.Rows[Icount].FindControl("ddllCash") as DropDownList;
                FLHMobj.CASH_NO = Convert.ToInt32(ddlcas.SelectedValue.ToString());
                DropDownList ddlBank = GridData.Rows[Icount].FindControl("ddlBank") as DropDownList;
                FLHMobj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue.ToString());
                FLHMobj.LASTMODIFIER_DATE = DateTime.Now;
                FLHMobj.CREATE_DATE = DateTime.Now;
                FLHMobj.CREATER_NAME = Session["username"].ToString();
                FLHMobj.LASTMODIFIER = Session["username"].ToString();
                FLHMobj.SequenceId = Icount + 1;
                //if ((ddlFH.SelectedItem.ToString() != "Please Select") && (ddlcas.SelectedItem.ToString() != "Please Select") && (ddlBank.SelectedItem.ToString() != "Please Select"))
                //if ((Convert.ToInt32(ddlFH.SelectedValue) > 0) && (Convert.ToInt32(ddlcas.SelectedValue) > 0 || Convert.ToInt32(ddlBank.SelectedValue) > 0))
                //{
                i = objAccTran.AddFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim(), Convert.ToInt32(ddlBranch.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text,Convert.ToInt32(0));
                //}
            }
        }
        if (i == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);
        }
        else
        {
            objCommon.DisplayMessage(UPDLedger, "Record Not Saved ", this);
        }
    }

    protected void UpdateRecord()
    {
        try
        {
            FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
            AccountTransactionController objAccTran = new AccountTransactionController();

            int i = 0;
            int cntSetBlank = 0;
            if (GridData.Rows.Count > 0)
            {
                int Icount = 0;
                cntSetBlank = objAccTran.UpdateFeeLedgerHeadMapSetBlank(Session["comp_code"].ToString().Trim(), Convert.ToInt32(ddlDegree.SelectedValue), ddlRecept.SelectedValue.ToString(), Convert.ToInt32(ddlBranch.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text, Convert.ToInt32(ddlbatch.SelectedValue), Convert.ToInt32(ddsem.SelectedValue));
                for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
                {
                    //FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());
                    FLHMobj.COLLEGE = Session["colcode"] == null ? 0 : Convert.ToInt32(Session["colcode"].ToString());
                    if (rdoGenralFees.Checked == true)
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();
                            FLHMobj.DegreeNo = 0;
                            FLHMobj.BatchNo = 0;
                            FLHMobj.SemesterNo = 0;
                        }
                        else
                        {
                            FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();
                            FLHMobj.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                            FLHMobj.BatchNo = Convert.ToInt32(ddlbatch.SelectedValue);
                            FLHMobj.SemesterNo = Convert.ToInt32(ddsem.SelectedValue);
                        }

                    }
                    else
                    {
                        if (Session["IsCCMS"].ToString() == "Y")
                        {
                            FLHMobj.RECIEPT_TYPE = "MF";
                            FLHMobj.DegreeNo = 0;
                            FLHMobj.BatchNo = 0;
                            FLHMobj.SemesterNo = 0;
                        }
                        else
                        {
                            FLHMobj.RECIEPT_TYPE = "MF";
                            FLHMobj.DegreeNo = 0;
                            FLHMobj.BatchNo = 0;
                            FLHMobj.SemesterNo = 0;
                        }
                    }
                    Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                    FLHMobj.FEE_HEAD_NO = lblHNO.Text;
                    Label lblHName = GridData.Rows[Icount].FindControl("lblFeeHeads") as Label;
                    FLHMobj.FeeLedger_NAme = lblHName.Text;
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

                    if ((ddlFH.SelectedItem.ToString() != "Please Select") && (ddlcas.SelectedItem.ToString() != "Please Select") && (ddlBank.SelectedItem.ToString() != "Please Select"))
                    {
                        i = objAccTran.UpdateFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim(), Convert.ToInt32(ddlBank.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text);
                    }
                }
            }
            if (i == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Updated Successfully", this);
            }
            else if (cntSetBlank < 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Record Not Updated", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.UpdateRecord-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (GridData.DataSource == null)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Sorry...Data Not Present. Click on Show Data", this);
            //    return;
            //}

            //if (ViewState["Operation"].ToString() == "Submit")
            //{


            //if (ddlDegree.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Degree", this);
            //    ddlDegree.Focus();
            //    return;
            //}

            if (rdoGenralFees.Checked == true)
            {
                if (ddlRecept.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(UPDLedger, "Please Select Receipt Type", this);
                    ddlRecept.Focus();
                    return;
                }
            }




            //if (ddlRecept.SelectedValue == "0")
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Please Select Receipt Type", this);
            //    ddlRecept.Focus();
            //    return;
            //}


            if (Session["AllowFullMapping"].ToString() == "Y")
            {
                for (int i = 0; i < GridData.Rows.Count; i++)
                {
                    DropDownList ddlLedger = GridData.Rows[i].FindControl("ddlleagerHead") as DropDownList;
                    DropDownList ddlCash = GridData.Rows[i].FindControl("ddllCash") as DropDownList;
                    if (ddlLedger.SelectedValue == "0" || ddlCash.SelectedValue == "0")
                    {
                        objCommon.DisplayUserMessage(UPDLedger, "Please Map All Fee Heads", this.Page);
                        return;
                    }
                }
            }
            else
            {
                int count = 0;
                for (int i = 0; i < GridData.Rows.Count; i++)
                {
                    DropDownList ddlLedger = GridData.Rows[i].FindControl("ddlleagerHead") as DropDownList;
                    DropDownList ddlCash = GridData.Rows[i].FindControl("ddllCash") as DropDownList;
                    if (ddlLedger.SelectedValue != "0" && ddlCash.SelectedValue != "0")
                    {
                        count = count + 1;

                    }
                }

                if (count == 0)
                {
                    objCommon.DisplayUserMessage(UPDLedger, "Please Map At Least One Fee Heads", this.Page);
                    return;
                }
            }


            //int count1 = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and DEGREE_NO='" + ddlDegree.SelectedValue + "' and CBTYPE='" + ddlRecept.SelectedValue + "'"));
            //if (count1 > 0)
            //{
            //    divMsg1.InnerText = "<script type='text/javascript' language='javascript'>";
            //    divMsg1.InnerText += " if(confirm('Fees is already transfered Still want to change mapping?'))";
            //    divMsg1.InnerText += "{__doPostBack('SaveRecord', '');}</script>";
            //}
            //else
            //{
            SaveRecord();
            ClearAll();
            //}
            // btnShowData_Click(sender, e);
            // ViewState["Operation"] = "Submit";
            //}


            //if (ViewState["Operation"].ToString() == "Update")
            //{
            //    //-----------------------------------------------------------------------------------------------
            //    bool flag = false;
            //    //check for new ledger
            //    //DataTable dtFeesheads = new DataTable();
            //    DataSet dtFeesheads = new DataSet();
            //    dtFeesheads = GetFeeHeadAndNo(ddlRecept.SelectedValue.ToString());//Oracle

            //    DataSet dsFeeLedgerHeadNo = new DataSet();//sql table
            //    dsFeeLedgerHeadNo = objCommon.FillDropDown("ACC_FEE_" + Session["comp_code"].ToString().Trim() + "_LEDERHEAD", "*", "RECIEPT_TYPE", "", "");

            //    //if new ledger noumber found then record inserted
            //    int iResult = 0;
            //    FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
            //    AccountTransactionController objAccTran = new AccountTransactionController();

            //    int iSetFlag = 1;
            //    int temp = 0;
            //    for (int i = 0; i < dtFeesheads.Tables[0].Rows.Count; i++)// for oracle
            //    {

            //        int oi = 0;
            //        for (oi = 0; oi < dsFeeLedgerHeadNo.Tables[0].Rows.Count; oi++)//for sql
            //        {
            //            //
            //            if (dtFeesheads.Tables[0].Rows[i][0].ToString() == dsFeeLedgerHeadNo.Tables[0].Rows[oi]["FEE_HEAD_NO"].ToString())
            //            {
            //                flag = true;
            //                iSetFlag++;
            //            }
            //        }

            //        if (iSetFlag > 0)
            //            iSetFlag = 0;
            //        else if (iSetFlag == 0)
            //            flag = false;

            //        if (temp != 0)
            //        {
            //            if (flag == false)
            //            {
            //                //geting New Ledger No 

            //                int ipos = i;//for taking actual position in table
            //                // if ((ddlFH.SelectedItem.ToString() != "Please Select") && (ddlcas.SelectedItem.ToString() != "Please Select") && (ddlBank.SelectedItem.ToString() != "Please Select"))
            //                //{
            //                string newLedgerNo = dtFeesheads.Tables[0].Rows[i][0].ToString();
            //                AddSinglerecord(newLedgerNo);
            //                // }
            //            }
            //        }
            //        temp++;
            //    }


            //    //if (iResult == 1)
            //    //{
            //    //    objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);
            //    //    return;
            //    //}     
            //    //---------------------------------------------------------------------------------

            //    //for update operation
            //    UpdateRecord();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.btnSave_click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        row4.Visible = true;
        row18.Visible = true;
        rdoGenralFees.Checked = true;
        rdoMiscFees.Checked = false;

        //Added By Akshay Dixit on 05-07-2022
        ddlDegree.SelectedValue = "0";
        ddlRecept.SelectedValue = "0";


        ClearAll();
    }
    private void ClearAll()
    {
        //Added By Akshay Dixit on 05-07-2022
        ddlDegree.SelectedValue = "0";
        ddlRecept.SelectedValue = "0";

        if (Session["IsCCMS"].ToString() == "Y")
        {
            row18.Visible = false;

        }

        GridData.DataSource = null;
        GridData.DataBind();
        btnSave.Enabled = false;
        btnSave.Attributes.Remove("onClick");
        rowgrid.Visible = false;

      
    }

    public void PopulateDegreeDropdown()
    {
        try
        {
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            //string temp = " ";
            //string OraSelectStr = "SELECT DEGREENO,DEGREENAME FROM acd_degree WHERE DEGREENAME !='" + temp + "'";
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

            // string temp = " ";
            objCommon = new Common();
            //DataSet ds = objCommon.FillDropDown("acd_degree", "DEGREENO", "DEGREENAME", "DEGREENAME !='" + temp + "' AND DEGREENO>0", "");
            DataSet ds = objAccTran.PopulateDegreeFromRF(_CCMS);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Add("Please Select");
                    ddlDegree.SelectedItem.Value = "0";

                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataSource = ds.Tables[0]; ;
                    ddlDegree.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.PopulateCollegeDegree-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    public void PopulateReceptTypeDropdown()
    {

        try
        {
            //SqlConnection sqlcon = new SqlConnection(_Fees);
            //string OraSelectStr = "SELECT RECIEPT_CODE,RECIEPT_TITLE FROM ACD_RECIEPT_TYPE";
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
                ds = objAccTran.GetReceiptTypeForCCMS(_CCMS);
            }
            else
            {
                // ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                ds = objAccTran.PopulateReceiptType(_CCMS);
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
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateBranchDropdown()
    {

        try
        {
            objCommon = new Common();
            DataSet ds = new DataSet();
            if (Session["IsCCMS"].ToString() == "Y")
            {
                try
                {
                    IITMS.SQLServer.SQLDAL.SQLHelper objSQLHelper = new IITMS.SQLServer.SQLDAL.SQLHelper(_CCMS);
                    //ds = objSQLHelper.ExecuteDataSet("select BRANCHNO,LONGNAME from ACD_BRANCH");
                    ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) ", " D.DEGREENAME,B.LONGNAME", "DB.DEGREENO,DB.BRANCHNO", "", "");
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateBranchDropdown-> " + ex.ToString());
                }

            }
            else
            {
                // ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "");
                try
                {
                    IITMS.SQLServer.SQLDAL.SQLHelper objSQLHelper = new IITMS.SQLServer.SQLDAL.SQLHelper(_CCMS);
                    //ds = objSQLHelper.ExecuteDataSet("select BRANCHNO,LONGNAME from ACD_BRANCH");
                    ds = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) ", " D.DEGREENAME,B.LONGNAME", "DB.DEGREENO,DB.BRANCHNO", "", "");

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.PopulateBranchDropdown-> " + ex.ToString());
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add("Please Select");
                ddlBranch.SelectedItem.Value = "0";
                ddlBranch.DataTextField = "LONGNAME";
                ddlBranch.DataValueField = "BRANCHNO";
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataBind();
                ddlBranch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_FeeLedgerHeadMapping.PopulateRecept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //inserting if new legerhead Found 
    public void AddSinglerecord(string newLedgerhead)
    {
        FeeLedgerHeadMapingClass FLHMobj = new FeeLedgerHeadMapingClass();
        AccountTransactionController objAccTran = new AccountTransactionController();



        if (GridData.Rows.Count > 0)
        {
            int Icount = 0;
            for (Icount = 0; Icount < GridData.Rows.Count; Icount++)
            {


                //FLHMobj.COLLEGE = Convert.ToInt32(ddlDegree.SelectedValue.ToString());
                FLHMobj.COLLEGE = Session["colcode"] == null ? 0 : Convert.ToInt32(Session["colcode"].ToString());

                FLHMobj.RECIEPT_TYPE = ddlRecept.SelectedValue.ToString();

                Label lblHNO = GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                FLHMobj.FEE_HEAD_NO = lblHNO.Text;

                DropDownList ddlFH = GridData.Rows[Icount].FindControl("ddlleagerHead") as DropDownList;
                FLHMobj.LEDGER_NO = Convert.ToInt32(ddlFH.SelectedValue.ToString());
                FLHMobj.CName = Session["username"].ToString();
                FLHMobj.CPass = Session["username"].ToString();


                //Label lbl =GridData.Rows[Icount].FindControl("lblFeeHeadsNo") as Label;
                //string temp = lbl.Text;

                DropDownList ddlcas = GridData.Rows[Icount].FindControl("ddllCash") as DropDownList;
                FLHMobj.CASH_NO = Convert.ToInt32(ddlcas.SelectedValue.ToString());


                DropDownList ddlBank = GridData.Rows[Icount].FindControl("ddlBank") as DropDownList;
                FLHMobj.BANK_NO = Convert.ToInt32(ddlBank.SelectedValue.ToString());
                FLHMobj.LASTMODIFIER_DATE = DateTime.Now;
                FLHMobj.CREATE_DATE = DateTime.Now;
                FLHMobj.CREATER_NAME = Session["username"].ToString();
                FLHMobj.LASTMODIFIER = Session["username"].ToString();

                if (FLHMobj.FEE_HEAD_NO == newLedgerhead)
                {
                    int iResult = 0;
                    if ((ddlFH.SelectedItem.ToString() != "Please Select") && (ddlcas.SelectedItem.ToString() != "Please Select") && (ddlBank.SelectedItem.ToString() != "Please Select"))
                    {
                        iResult = objAccTran.AddFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim(), Convert.ToInt32(ddlBranch.SelectedValue), ddlAided_NoAided.SelectedValue == "0" ? "0" : ddlAided_NoAided.SelectedItem.Text,Convert.ToInt32(ddlBranch.SelectedValue));
                    }
                    if (iResult == 1)
                    {
                        objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);

                    }
                }
            }



            // i = objAccTran.UpdateFeeLedgerHeadMaping(FLHMobj, Session["comp_code"].ToString().Trim());
            //if (i == 1)
            //{
            //    objCommon.DisplayMessage(UPDLedger, "Record Saved Successfully", this);

            //}     




        }


    }





    protected void GridData_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
    protected void ddlRecept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearAll();

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ClearAll();
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(D.DEGREENO=DB.DEGREENO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=DB.BRANCHNO) ", "DB.BRANCHNO", "B.LONGNAME", "DB.DEGREENO=" + ddlDegree.SelectedValue, "B.LONGNAME");
    }
    protected void rdoGenralFees_CheckedChanged(object sender, EventArgs e)
    {
        row4.Visible = true;
        row18.Visible = true;
        Div4.Visible = true;
        ClearAll();
    }
    protected void rdoMiscFees_CheckedChanged(object sender, EventArgs e)
    {
        row4.Visible = false;
        row18.Visible = false;
        Div4.Visible = false;
        ClearAll();
    }


    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string CheckAlreadyFeesTransfer(string DegreeNo, string RETTYPE, string compcode)
    {
        Common objCommon = new Common();
        int count = Convert.ToInt32(objCommon.LookUp("ACC_" + compcode + "_TRANS", "COUNT(*)", "TRANSACTION_TYPE<>'OB' and DEGREE_NO='" + DegreeNo + "' and CBTYPE='" + RETTYPE + "'"));
        if (count > 0)
            return "Available";
        else
            return "Not Available";
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
