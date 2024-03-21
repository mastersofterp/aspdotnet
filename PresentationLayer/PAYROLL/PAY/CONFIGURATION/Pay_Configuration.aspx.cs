using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Transactions;


using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

using System.Web;
using mastersofterp_MAKAUAT;

public partial class PAYROLL_PAY_Configuration_Pay_Configuration : System.Web.UI.Page
{


    Common objCommon =  new Common();
    //UserAuthorizationController objucc;
    PayConfigurationController objConfig = new PayConfigurationController();
    //PayConfiguration objPayCon = new PayConfiguration();
    PayConfiguration objPayCon = new PayConfiguration();
    string UsrStatus = string.Empty;
    int Organizationid;
    string filename;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        //{
        //    objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        //    objConfig = new PayConfigurationController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());

        //}
        //else
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
        if (!Page.IsPostBack)
        {
            
        //        DataSet ds = new DataSet();
        //        if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
        //        {
        //            ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
        //        }
        //        else
        //        {
        //            ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
        //            DataView dv = ds.Tables[0].DefaultView;
        //            dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
        //            ds.Tables.Remove("Table");
        //            ds.Tables.Add(dv.ToTable());
        //        }
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
        //        }
        //        else
        //        {
        //            lblHelp.Text = "No Help Present!";
        //        }
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ViewState["imgEmpSign"] = null;
                CheckPageAuthorization();
                GetCurrentConfiguration();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Configuration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Configuration.aspx");
        }
    }
    //To Get Existing Data and display on page.
    private void GetCurrentConfiguration()
    {
        DataSet dtr = objConfig.GetConfiguration();
        if (dtr != null)
        {
            //if (dtr.Read())
            //{
                BindStaff();
                txtDAField.Text = dtr.Tables[0].Rows[0]["DA_FIELD"].ToString().Trim();
                txtHRAField.Text = dtr.Tables[0].Rows[0]["HRA_FIELD"].ToString().Trim();
                txtCLAField.Text = dtr.Tables[0].Rows[0]["CLA_FIELD"].ToString().Trim();
                txtTAField.Text = dtr.Tables[0].Rows[0]["TA_FIELD"].ToString().Trim();
                txtDAONTAField.Text = dtr.Tables[0].Rows[0]["DA_ON_TA_FIELD"].ToString().Trim();
                txtRecoveryField.Text = dtr.Tables[0].Rows[0]["RECOVERY_FIELD"].ToString().Trim();
                txtGPFADDField.Text = dtr.Tables[0].Rows[0]["GPFADD"].ToString().Trim();
                txtGPFADVField.Text = dtr.Tables[0].Rows[0]["GPFADV"].ToString().Trim();
                txtITField.Text = dtr.Tables[0].Rows[0]["IT_FIELD"].ToString().Trim();
                txtPTField.Text = dtr.Tables[0].Rows[0]["PT_FIELD"].ToString().Trim();
                txtLICField.Text = dtr.Tables[0].Rows[0]["LIC_FIELD"].ToString().Trim();
                //txtGISField.Text = dtr.Tables[0].Rows[0]["GIS_FIELD"].ToString();
                //txtLISCFEEField.Text = dtr.Tables[0].Rows[0]["LICENSE_FEE_FIELD"].ToString().Trim();
                
                
                txtLWPField.Text = dtr.Tables[0].Rows[0]["LWP_FIELD"].ToString().Trim();
                txtDEField.Text = dtr.Tables[0].Rows[0]["DE_FIELD"].ToString().Trim();
                txtG80Field.Text = dtr.Tables[0].Rows[0]["G80_FIELD"].ToString().Trim();
                txtCPFField.Text = dtr.Tables[0].Rows[0]["CPF_FIELD"].ToString().Trim();
                txtCPFLOANField.Text = dtr.Tables[0].Rows[0]["CPF_LOAN"].ToString().Trim();
                txtPHONEField.Text = dtr.Tables[0].Rows[0]["PHONE_FIELD"].ToString().Trim();
                txtMEDICALField.Text = dtr.Tables[0].Rows[0]["MEDICAL_FIELD"].ToString().Trim();
                txtRDField.Text = dtr.Tables[0].Rows[0]["RD_FIELD"].ToString().Trim();
                txtHONOField.Text = dtr.Tables[0].Rows[0]["HONO_FIELD"].ToString().Trim();


                //txtGPFLOANField.Text = dtr.Tables[0].Rows[0]["GPF_LOAN"].ToString().Trim();
                
            
            
                txtGPFField.Text = dtr.Tables[0].Rows[0]["GPF"].ToString().Trim();
                txtPrincipalName.Text = dtr.Tables[0].Rows[0]["PRINCNAME"].ToString().Trim();
                txtFromDate.Text = dtr.Tables[0].Rows[0]["FDATE"].ToString().Trim();
                
                txtCPFPercentage.Text = dtr.Tables[0].Rows[0]["CPFPER"].ToString().Trim();
                txtLICBranch.Text = dtr.Tables[0].Rows[0]["LICBRANCH"].ToString().Trim();
                txtTANNo.Text = dtr.Tables[0].Rows[0]["TANNO"].ToString().Trim();
                txtPFFStartDate.Text = dtr.Tables[0].Rows[0]["PFFSDATE"].ToString().Trim();
                txtCPFFromDate.Text = dtr.Tables[0].Rows[0]["CPFFDATE"].ToString().Trim();
                txtDAMergeIntoBasic.Text = dtr.Tables[0].Rows[0]["DA_PER"].ToString().Trim();
                txtToDate.Text = dtr.Tables[0].Rows[0]["TDATE"].ToString().Trim();
                txtLICPACNO.Text = dtr.Tables[0].Rows[0]["LICPACNO"].ToString().Trim();
                txtPANNo.Text = dtr.Tables[0].Rows[0]["PANNO"].ToString().Trim();
                txtPFFEndDate.Text = dtr.Tables[0].Rows[0]["PFFEDATE"].ToString().Trim();
                txtCPFToDate.Text = dtr.Tables[0].Rows[0]["CPFTDATE"].ToString().Trim();
                //txtLICEFEE_FIELD.Text = dtr.Tables[0].Rows[0]["COLLEGE_CODE_NO"].ToString().Trim();
                
                
                
                //txtMODULENO.Text = dtr.Tables[0].Rows[0]["BANK_NAME"].ToString().Trim();
                //txtRD_PER.Text = dtr.Tables[0].Rows[0]["LOCATION"].ToString().Trim();
                //txtDOSFONT.Text = dtr.Tables[0].Rows[0]["STD_XI_SECTION"].ToString().Trim();
                //txtLWP_PDAY.Text = dtr.Tables[0].Rows[0]["STD_XII_SECTION"].ToString().Trim();


                txtCOLNAME.Text = dtr.Tables[0].Rows[0]["COLNAME"].ToString().Trim();
                txtSOCNAME.Text = dtr.Tables[0].Rows[0]["SOCNAME"].ToString().Trim();
                txtCompany.Text = dtr.Tables[0].Rows[0]["COMPANY"].ToString().Trim();
                txtACCPW.Text = dtr.Tables[0].Rows[0]["ACCPW"].ToString().Trim();
                txtSINGLE_INV.Text = dtr.Tables[0].Rows[0]["SINGLE_INV"].ToString().Trim();
                txtRW_WIDTH.Text = dtr.Tables[0].Rows[0]["RWIDTH"].ToString().Trim();
                txtRHEIGTH.Text = dtr.Tables[0].Rows[0]["RHEIGHT"].ToString().Trim();
                txtSAL_PWD.Text = dtr.Tables[0].Rows[0]["SALPASSWORD"].ToString().Trim();
                txtGPFPER.Text = dtr.Tables[0].Rows[0]["GPFPER"].ToString().Trim();
                txtENAMESIZE.Text = dtr.Tables[0].Rows[0]["ENAMESIZE"].ToString().Trim();
                txtANOSIZE.Text = dtr.Tables[0].Rows[0]["ANOSIZE"].ToString().Trim();
                txtAMTSIZE.Text = dtr.Tables[0].Rows[0]["AMTSIZE"].ToString().Trim();
                txtLWP_CAL.Text = dtr.Tables[0].Rows[0]["LWP_CAL"].ToString().Trim();
                txtDOSFONT.Text = dtr.Tables[0].Rows[0]["DOSFONT"].ToString().Trim();
                txtZERO.Text = dtr.Tables[0].Rows[0]["ZERO"].ToString().Trim();
                txtRDField.Text = dtr.Tables[0].Rows[0]["RD_FIELD"].ToString().Trim();
                txtRD_PER.Text = dtr.Tables[0].Rows[0]["RD_PER"].ToString().Trim();
                txtLWP_PDAY.Text = dtr.Tables[0].Rows[0]["LWP_PDAY"].ToString().Trim();
                txtLICEFEE_FIELD.Text = dtr.Tables[0].Rows[0]["LICEFEE_FIELD"].ToString().Trim();
                txtPRO_SAL.Text = dtr.Tables[0].Rows[0]["PROPOSED_SAL"].ToString().Trim();
                txtMODULENO.Text = dtr.Tables[0].Rows[0]["MODULE_NO"].ToString().Trim();
                txtLINKNO.Text = dtr.Tables[0].Rows[0]["LINK_NO"].ToString().Trim();
                txtLEAVE_APP.Text = dtr.Tables[0].Rows[0]["LEAVE_APPROVAL"].ToString().Trim();
                txtEPFAmount.Text = dtr.Tables[0].Rows[0]["EPF_AMOUNT"].ToString().Trim();

                txtOverTimeHead.Text = dtr.Tables[0].Rows[0]["OverTimeHead"].ToString().Trim(); 
                //txtbuscharges.Text = dtr.Tables[0].Rows[0]["BUS_CHARGES_FIELD"].ToString().Trim();
                //txtnps.Text = dtr.Tables[0].Rows[0]["NPS_FIELD"].ToString().Trim();

                //txtPH_AMT.Text = dtr.Tables[0].Rows[0]["PH_AMT"].ToString().Trim();
                //txtSTAFFNO.Text = dtr.Tables[0].Rows[0]["STAFFNO"].ToString().Trim();
                
                //txtPRO_SAL.Text = dtr.Tables[0].Rows[0]["IT_REGISTRATION_NO"].ToString().Trim();
                //txtSAL_PWD.Text = dtr.Tables[0].Rows[0]["PT_REGISTRATION_NO"].ToString().Trim();
                //txtACCPW.Text = dtr.Tables[0].Rows[0]["BANKLOAN"].ToString().Trim();
                
                
                
                //txtSINGLE_INV.Text = dtr.Tables[0].Rows[0]["PUJA_FUND"].ToString().Trim();
                //txtRW_WIDTH.Text = dtr.Tables[0].Rows[0]["NT_FUND"].ToString().Trim();
                //txtRHEIGTH.Text = dtr.Tables[0].Rows[0]["CR_FUND"].ToString().Trim();
                //txtLWP_CAL.Text = dtr.Tables[0].Rows[0]["OTHERDED"].ToString().Trim();
                //txtAMTSIZE.Text = dtr.Tables[0].Rows[0]["PAY_UNIT_NO"].ToString().Trim();
                //txtENAMESIZE.Text = dtr.Tables[0].Rows[0]["CODE_NUMBER"].ToString().Trim();
                //txtANOSIZE.Text = dtr.Tables[0].Rows[0]["ZONE"].ToString().Trim();
                //txtGPFPER.Text = dtr.Tables[0].Rows[0]["WARD"].ToString().Trim();
                //txtLINKNO.Text = dtr.Tables[0].Rows[0]["REGISTRARNAME"].ToString().Trim();

                //if (dtr.Tables[0].Rows[0]["LINK_WITH_ACCOUNTS"].ToString() == "1")
                //{
                //    chkLinkWithAccounts.Checked = true;
                //}
                //else
                //{
                //    chkLinkWithAccounts.Checked = false;
                //}


                if (dtr.Tables[0].Rows[0]["GP_Status"].ToString() == "1")
                {
                    chkGradePay.Checked = true;
                }
                else
                {
                    chkGradePay.Checked = false;
                }

                if (dtr.Tables[0].Rows[0]["DP_Status"].ToString() == "1")
                {
                    chkDP.Checked = true;
                }
                else
                {
                    chkDP.Checked = false;
                }
            //}
            //dtr.Close();

            //txtCOLNAME.Text = dtr.Tables[0].Rows[0]["COLNAME"].ToString().Trim();

            //Amol sawarkar 04/03/2022

                txtESICNO.Text = dtr.Tables[0].Rows[0]["ESICNO"].ToString().Trim();
                txtESI_LIMIT_HP.Text = dtr.Tables[0].Rows[0]["ESI_LIMIT_HP"].ToString().Trim();
                txtESI_PER_HP.Text = dtr.Tables[0].Rows[0]["ESI_PER_HP"].ToString().Trim();
                txtEmployerContribution.Text = dtr.Tables[0].Rows[0]["EmployerContribution"].ToString().Trim();
                txtESIC_LIMIT.Text = dtr.Tables[0].Rows[0]["ESIC_LIMIT"].ToString().Trim();
                txtESIFIRSTFROMDATE.Text = dtr.Tables[0].Rows[0]["ESIFIRSTFROMDATE"].ToString().Trim();
                txtESIFIRSTTODATE.Text = dtr.Tables[0].Rows[0]["ESIFIRSTTODATE"].ToString().Trim();
                txtESISECONDFROMDATE.Text = dtr.Tables[0].Rows[0]["ESISECONDFROMDATE"].ToString().Trim();
                txtESISECONDTODATE.Text = dtr.Tables[0].Rows[0]["ESISECONDTODATE"].ToString().Trim();


                txtEMPLOYEEPAYSLIPEFFECTFROM.Text = dtr.Tables[0].Rows[0]["EmpPayslipShowFromDate"].ToString().Trim();

                ddluserLoginType.SelectedValue = dtr.Tables[0].Rows[0]["User_Login_Type"].ToString().Trim();
                txtUserPass.Text = clsTripleLvlEncyrpt.ThreeLevelDecrypt(dtr.Tables[0].Rows[0]["User_Default_Password"].ToString().Trim());
                Organizationid =Convert.ToInt32(dtr.Tables[0].Rows[0]["OrganizationId"]);

                if ( Convert.ToBoolean( dtr.Tables[0].Rows[0]["IsAutoUserCreated"]) == true)
                {
                    ChkAutoUserCreated.Checked = true;
                }
                else
                {
                    ChkAutoUserCreated.Checked = false;
                }
                if (Convert.ToBoolean(dtr.Tables[0].Rows[0]["IsRetirmentDateCalculation"]) == true)
                {
                    chkretirmentdatecalculate.Checked = true;
                }
                else
                {
                    chkretirmentdatecalculate.Checked = false;
                }
                if (Convert.ToBoolean(dtr.Tables[0].Rows[0]["EnableEmpSignonEmpInfoPage"]) == true)
                {
                    chkenabledEmpSignature.Checked = true;
                }
                else
                {
                    chkenabledEmpSignature.Checked=false;
                }
                ViewState["imgEmpSign"] = (dtr.Tables[0].Rows[0]["REGISTRAR_SIGN"])as byte[];  
                imgEmpSign.ImageUrl = "../../../showimage.aspx?id=" + Convert.ToString(Organizationid) + "&type=REGISTRAR_SIGN";
        }
    }

    //To update the Existing Information
    private void UpdateConfig()
    {
        try
        {
            objPayCon.DA = txtDAField.Text.Trim();
            objPayCon.HRA = txtHRAField.Text;
            objPayCon.CLA = txtCLAField.Text.Trim();
            objPayCon.TA = txtTAField.Text.Trim();
            //objPayCon.DAPER = txtDAMergeIntoBasic.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtDAMergeIntoBasic.Text.Trim());
            objPayCon.DAONTA = txtDAONTAField.Text.Trim();
            objPayCon.RECOVERY = txtRecoveryField.Text.Trim();
            objPayCon.GPFADD = txtGPFADDField.Text.Trim();
            objPayCon.GPF_ADV = txtGPFADVField.Text.Trim();
            objPayCon.IT = txtITField.Text.Trim();
            objPayCon.PT = txtPTField.Text.Trim();
            objPayCon.LIC = txtLICField.Text.Trim();
            //objPayCon.GIS = txtGISField.Text.Trim();
            //objPayCon.LISEFEE = txtLISCFEEField.Text.Trim();
           
            objPayCon.LWP = txtLWPField.Text.Trim();
            objPayCon.DE = txtDEField.Text.Trim();
            objPayCon.G80 = txtG80Field.Text.Trim();
            objPayCon.CPF = txtCPFField.Text.Trim();
            objPayCon.CPFLOAN = txtCPFLOANField.Text.Trim();



            //objPayCon.GPFLOAN = txtGPFLOANField.Text.Trim();





            objPayCon.PHONE = txtPHONEField.Text.Trim();
            objPayCon.MEDICAL = txtMEDICALField.Text.Trim();
            objPayCon.RD = txtRDField.Text.Trim();
            objPayCon.HONO = txtHONOField.Text.Trim();
           
            objPayCon.GPF = txtGPFField.Text.Trim();
            objPayCon.PRINCIPAL = txtPrincipalName.Text.Trim();
            //objPayCon.COLLEGECODE = txtLICEFEE_FIELD.Text.Trim();
            //objPayCon.BANK = txtMODULENO.Text.Trim();
            // objPayCon.BANKLOCATION = txtRD_PER.Text.Trim();
            //objPayCon.SECTIONXI = txtDOSFONT.Text.Trim();
            //objPayCon.SECTIONXII = txtLWP_PDAY.Text.Trim();
            //objPayCon.DAPER = Convert.ToDecimal(txtDAMergeIntoBasic.Text.Trim());

            objPayCon.DAPER = txtDAMergeIntoBasic.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtDAMergeIntoBasic.Text.Trim());

            if (txtFromDate.Text.ToString().Trim() != string.Empty)
                objPayCon.FROMDATE = Convert.ToDateTime(txtFromDate.Text.Trim());
            if (txtToDate.Text.ToString().Trim() != string.Empty)
                objPayCon.TODATE = Convert.ToDateTime(txtToDate.Text.Trim());
           
            objPayCon.CPFPER = txtCPFPercentage.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtCPFPercentage.Text.Trim());
            //objPayCon.ACCOUNT = Convert.ToInt64(txtZERO.Text.Trim());
            objPayCon.LICBRANCH = txtLICBranch.Text.Trim();
            objPayCon.PACNO = txtLICPACNO.Text.Trim();
            objPayCon.TANNO = txtTANNo.Text.Trim();
            objPayCon.PANNO = txtPANNo.Text.Trim();
            //objPayCon.ITREGNO = txtPRO_SAL.Text.Trim();
            // objPayCon.PTREGNO = txtSAL_PWD.Text.Trim();
            //objPayCon.BANKLOAN = txtACCPW.Text.Trim();
            //objPayCon.PUJAFUND = txtSINGLE_INV.Text.Trim();
            //objPayCon.NTFUND = txtRW_WIDTH.Text.Trim();
            //objPayCon.CRFUND = txtRHEIGTH.Text.Trim();
            //objPayCon.OTHERDED = txtLWP_CAL.Text.Trim();
            //objPayCon.PAYUNITNO = txtAMTSIZE.Text.Trim();
            // objPayCon.CODENO = txtENAMESIZE.Text.Trim();
            //objPayCon.ZONE = txtANOSIZE.Text.Trim();
            //objPayCon.WARD = txtGPFPER.Text.Trim();
            //objPayCon.REGISTRAR = txtLINKNO.Text.Trim();
            //objPayCon.CPFPER = txtCPFPercentage.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtCPFPercentage.Text.Trim());
            
            
            // Amol sawarkar 04-03-2022 

            objPayCon.ESICNO = txtESICNO.Text.Trim();
            objPayCon.ESI_LIMIT_HP = txtESI_LIMIT_HP.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtESI_LIMIT_HP.Text.Trim());
            objPayCon.ESI_PER_HP = txtESI_PER_HP.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtESI_PER_HP.Text.Trim());
            objPayCon.EmployerContribution = txtEmployerContribution.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtEmployerContribution.Text.Trim());
            objPayCon.ESIC_LIMIT = txtESIC_LIMIT.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtESIC_LIMIT.Text.Trim());


            if (txtESIFIRSTFROMDATE.Text.ToString().Trim() != string.Empty)
                objPayCon.ESIFIRSTFROMDATE = Convert.ToDateTime(txtESIFIRSTFROMDATE.Text.Trim());
            if (txtESIFIRSTTODATE.Text.ToString().Trim() != string.Empty)
                objPayCon.ESIFIRSTTODATE = Convert.ToDateTime(txtESIFIRSTTODATE.Text.Trim());

            if (txtESISECONDFROMDATE.Text.ToString().Trim() != string.Empty)
                objPayCon.ESISECONDFROMDATE = Convert.ToDateTime(txtESISECONDFROMDATE.Text.Trim());
            if (txtESISECONDTODATE.Text.ToString().Trim() != string.Empty)
                objPayCon.ESISECONDTODATE = Convert.ToDateTime(txtESISECONDTODATE.Text.Trim());

            if (txtEMPLOYEEPAYSLIPEFFECTFROM.Text.ToString().Trim() != string.Empty)
                objPayCon.EmpPayslipShowFromDate = Convert.ToDateTime(txtEMPLOYEEPAYSLIPEFFECTFROM.Text.Trim());

            //if (txtESIFIRSTFROMDATE.ToString().Trim() != string.Empty)
            //    objPayCon.ESIFIRSTFROMDATE = Convert.ToDateTime(txtESIFIRSTFROMDATE.Text.Trim());
            //if (txtESIFIRSTTODATE.Text.ToString().Trim() != string.Empty)
            //    objPayCon.ESIFIRSTTODATE = Convert.ToDateTime(txtESIFIRSTTODATE.Text.Trim());

            //if (txtESISECONDFROMDATE.ToString().Trim() != string.Empty)
            //    objPayCon.ESISECONDFROMDATE = Convert.ToDateTime(txtESISECONDFROMDATE.Text.Trim());
            //if (txtESISECONDTODATE.Text.ToString().Trim() != string.Empty)
            //    objPayCon.ESISECONDTODATE = Convert.ToDateTime(txtESISECONDTODATE.Text.Trim());

            //if (txtEMPLOYEEPAYSLIPEFFECTFROM.ToString().Trim() != string.Empty)
            //    objPayCon.EmpPayslipShowFromDate = Convert.ToDateTime(txtEMPLOYEEPAYSLIPEFFECTFROM.Text.Trim());

            objPayCon.COLNAME = txtCOLNAME.Text.Trim();
            objPayCon.SOCNAME = txtSOCNAME.Text.Trim();
            objPayCon.COMPANY = txtCompany.Text.Trim();
            objPayCon.ACCPW = txtACCPW.Text.Trim();

            objPayCon.SINGLE_INV = txtSINGLE_INV.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtSINGLE_INV.Text.Trim());

            
            objPayCon.RWIDTH = Convert.ToDouble(txtRW_WIDTH.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtRW_WIDTH.Text.Trim()));
           
            objPayCon.RHEIGHT = Convert.ToDouble(txtRHEIGTH.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtRHEIGTH.Text.Trim()));
            objPayCon.SALPASSWORD = txtSAL_PWD.Text.Trim();
            
            objPayCon.GPFPER = Convert.ToDouble(txtGPFPER.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtGPFPER.Text.Trim()));
            
            objPayCon.ENAMESIZE = Convert.ToDouble(txtENAMESIZE.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtENAMESIZE.Text.Trim()));
            //objPayCon.ANOSIZE = Convert.ToInt32(txtANOSIZE.Text.Trim());
            objPayCon.ANOSIZE = txtANOSIZE.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtANOSIZE.Text.Trim());
            //objPayCon.AMTSIZE = Convert.ToInt32(txtAMTSIZE.Text.Trim());
            objPayCon.AMTSIZE = Convert.ToInt32(txtAMTSIZE.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtAMTSIZE.Text.Trim()));
            objPayCon.LWP_CAL = txtLWP_CAL.Text.Trim();
            if (!string.IsNullOrEmpty(txtDOSFONT.Text))

                objPayCon.DOSFONT = Convert.ToBoolean(txtDOSFONT.Text.Trim());
            if (!string.IsNullOrEmpty(txtZERO.Text))
                //objPayCon.ZERO = Convert.ToBoolean(txtZERO.Text.Trim());
                //objPayCon.ZERO = Convert.ToBoolean(txtZERO.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txtZERO.Text.Trim()));
            objPayCon.ZERO = Convert.ToBoolean((txtZERO.Text.Trim()));
            //objPayCon.RD_PER = Convert.ToDouble(txtRD_PER.Text.Trim());
            objPayCon.RD_PER = Convert.ToDouble(txtRD_PER.Text.Trim() == string.Empty ? 0.0m : Convert.ToDecimal(txtRD_PER.Text.Trim()));
            //objPayCon.LWP_PDAY = Convert.ToInt32(txtLWP_PDAY.Text.Trim());
            objPayCon.LWP_PDAY = txtLWP_PDAY.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtLWP_PDAY.Text.Trim());
            objPayCon.LICEFEE_FIELD = txtLICEFEE_FIELD.Text.Trim();
            //objPayCon.PROPOSED_SAL = Convert.ToInt32(txtPRO_SAL.Text.Trim());
            objPayCon.PROPOSED_SAL = txtPRO_SAL.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtPRO_SAL.Text.Trim());
            //objPayCon.MODULE_NO = Convert.ToInt32(txtMODULENO.Text.Trim());
            objPayCon.MODULE_NO = txtMODULENO.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtMODULENO.Text.Trim());
            //objPayCon.LINK_NO = Convert.ToInt32(txtLINKNO.Text.Trim());
            objPayCon.LINK_NO = txtLINKNO.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtLINKNO.Text.Trim());
            objPayCon.LEAVE_APPROVAL = txtLEAVE_APP.Text.Trim();
            //objPayCon.PH_AMT = Convert.ToInt32(txtPH_AMT.Text.Trim());
            //objPayCon.PH_AMT = txtPH_AMT.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtPH_AMT.Text.Trim());
            //objPayCon.STAFFNO = txtSTAFFNO.Text.Trim();



            objPayCon.OverTimeHead = txtOverTimeHead.Text.Trim();
            if (txtPFFStartDate.Text.ToString().Trim() != string.Empty)
                objPayCon.PFFSDATE = Convert.ToDateTime(txtPFFStartDate.Text.Trim());
            if (txtPFFEndDate.Text.ToString().Trim() != string.Empty)
                objPayCon.PFFEDATE = Convert.ToDateTime(txtPFFEndDate.Text.Trim());
            if (txtCPFFromDate.Text.ToString().Trim() != string.Empty)
                objPayCon.CPFFROMDATE = Convert.ToDateTime(txtCPFFromDate.Text.Trim());
            if (txtCPFToDate.Text.ToString().Trim() != string.Empty)
                objPayCon.CPFTODATE = Convert.ToDateTime(txtCPFToDate.Text.Trim());




            objPayCon.LINKACC = chkLinkWithAccounts.Checked ? 1 : 0;
            objPayCon.GP_Status = chkGradePay.Checked ? 1 : 0;
            objPayCon.DP_Status = chkDP.Checked ? 1 : 0;

            string buschargefield = string.Empty;
            buschargefield = txtbuscharges.Text;

            string npsfiels = string.Empty;
            npsfiels = txtnps.Text;

            decimal epfamount = Convert.ToDecimal(txtEPFAmount.Text);
            objPayCon.EPFAMOUNT = epfamount;

            string colvalues;
            int checkcount = 0;
            colvalues = string.Empty;
            string colval = string.Empty;
            foreach ( ListViewDataItem lvitem in lvStatff.Items)
            {
                CheckBox chk = lvitem.FindControl("chkStaff") as CheckBox;

                if (chk.Checked)
                {
                    checkcount = 1;
                    colvalues = colvalues + chk.ToolTip + ",";
                }

            }
            if (checkcount == 1)
            {
                colval = colvalues.Substring(0, colvalues.Length - 1);
            }
            objPayCon.STAFF_APPLICABLE = colval;

            objPayCon.USERLOGINTYPE = ddluserLoginType.SelectedValue;
           // objPayCon.USERPASSWORD = txtUserPass.Text.Trim();
            if (txtUserPass.Text.Trim() != string.Empty)
            {
                objPayCon.USERPASSWORD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(txtUserPass.Text.Trim());
            }
            else
            {
                objPayCon.USERPASSWORD = string.Empty;
            }
            if (fuplEmpSign.HasFile)
            {
                filename = fuplEmpSign.FileName.ToString();
            }
            else
            {
                filename = fuplEmpSign.FileName.ToString();
            }
            if (fuplEmpSign.HasFile)
            {
                objPayCon.PhotoSign = objCommon.GetImageData(fuplEmpSign); //14-09-2022
            }
            else
            {
                objPayCon.PhotoSign = (byte[])(ViewState["imgEmpSign"]); //27-09-2022
            }
            objPayCon.IsAutoUserCreated = ChkAutoUserCreated.Checked ? true : false;
            objPayCon.IsRetirmentDateCalculation = chkretirmentdatecalculate.Checked ? true : false;
            objPayCon.IsEnableEmpSignatureinEmpPage = chkenabledEmpSignature.Checked ? true : false;

            CustomStatus cs = (CustomStatus)objConfig.UpdateRefTable(objPayCon);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                //objCommon.DisplayMessage("Record Updated Successfully..", this);
                //objCommon.DisplayMessage("Record Updated Successfully", this);
                ViewState["imgEmpSign"] = null;
                GetCurrentConfiguration();
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Record Updated Successfully...')", true);

            }
            else
            {
                //objCommon.ShowErrorMessage(Panel_Confirm, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
            }
        }
        catch { }
    }




        
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //UsrStatus = GetUserRight();
        //if (UsrStatus.Trim().Substring(2, 2) == "YM")
            this.UpdateConfig();
      //  else
           // objCommon.ShowErrorMessage(Panel_Confirm, Label_ErrorMessage, Common.Message.NoModify, Common.MessageType.Error);



    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("Pay_Configuration.aspx");
        Response.Redirect(Request.Url.ToString());
    }

    private void BindStaff()
    {
        DataSet ds = objConfig.GetAllStaff();
        lvStatff.DataSource = ds;
        lvStatff.DataBind();

        DataSet dsedit = objConfig.EditStaff();

        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            CheckBox ChkPayHead = lvStatff.Items[i].FindControl("chkStaff") as CheckBox;


            for (int j = 0; j <= dsedit.Tables[0].Rows.Count - 1; j++)
            {
                string payHead = ds.Tables[0].Rows[i]["STAFFNO"].ToString();
                string payHeadUser = dsedit.Tables[0].Rows[j]["STAFFNO"].ToString();

                if (payHead == payHeadUser)
                {
                    ChkPayHead.BackColor = System.Drawing.Color.Gold;
                    ChkPayHead.Checked = true;
                }
            }

        }

    }


}
