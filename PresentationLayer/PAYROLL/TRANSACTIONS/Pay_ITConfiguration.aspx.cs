//======================================================================================
// PROJECT NAME  : CCMS                                                               
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PAY_ITConfiguration.aspx.cs                                             
// CREATION DATE :  Mrunal Bansod                                                     
// CREATED BY    :  2/04/2011      
// Modified By   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//=======================================================================================
 
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.Caching;
//using System.Windows.Forms;

public partial class Pay_ITConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    string UsrStatus = string.Empty;


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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                GetConfiguration();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITConfiguration.aspx");
        }
    }


    private void GetConfiguration()
    {
        DataTableReader dtr = objITMas.GetITConfiguration();

        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtCollegeName.Text = dtr["COLNAME"].ToString().Trim();
                txtPANNo .Text = dtr["PAN"].ToString().Trim();
                txtTANNo .Text = dtr["TAN"].ToString().Trim();
                txtSignature.Text = dtr["IT_SINGNAME"].ToString().Trim();
                txtSonOf .Text  = dtr ["IT_SIGNFNAME"].ToString ().Trim();
                txtSection .Text = dtr["SECTION"].ToString().Trim();
                txtDesignation.Text = dtr["SINGDESIG"].ToString().Trim();
                txtEmpBlockNo .Text = dtr["BLOCKNO"].ToString().Trim();
                txtEmpBuildingName.Text = dtr["BUILDING"].ToString().Trim();
                txtEmpRoad .Text = dtr["ROAD"].ToString().Trim();
                txtEmpArea .Text = dtr["AREA"].ToString().Trim();
                txtEmpCity .Text = dtr["CITY"].ToString().Trim();
                txtEmpState .Text = dtr["STATE"].ToString().Trim();
                txtEmpPinCode .Text = dtr["PIN"].ToString().Trim();
                txtEmpTelNo .Text = dtr["PHONE"].ToString().Trim();
                txtEmpEmail .Text = dtr["EMAIL"].ToString().Trim();
                txtPerBlockNo .Text = dtr["PERBLOCKNO"].ToString().Trim();
                txtPerBuildingName .Text = dtr["PERBUILDING"].ToString().Trim();
                txtPerRoad .Text = dtr["PERROAD"].ToString().Trim();
                txtPerArea .Text = dtr["PERAREA"].ToString().Trim();
                txtPerCity .Text = dtr["PERCITY"].ToString().Trim();
                txtPerState.Text = dtr["PERSTATE"].ToString().Trim();
                txtPerPinCode .Text = dtr["PERPIN"].ToString().Trim();
                txtPerTelNo .Text = dtr["PERPHONE"].ToString().Trim();
                txtPerEmail .Text = dtr["PEREMAIL"].ToString().Trim();
                txtBankName.Text = dtr["ITBNAME"].ToString().Trim();
                txtBranchName.Text = dtr["ITBBRANCH"].ToString().Trim();
                txtBankPlace .Text = dtr["ITBPLACE"].ToString().Trim();
                txtPrintdate.Text = dtr["ITDATE"].ToString().Trim();
                txtRlimit.Text = dtr["RELIF_LIMIT"].ToString().Trim();
                txtBondLimit.Text = dtr["BOND_LIMIT"].ToString().Trim();
                txtFemaleLimit.Text = dtr["LAD_DISC"].ToString().Trim();
                txtSurcharge.Text = dtr["SURCHARGE"].ToString().Trim();
                txtMorethan.Text = dtr["SURABOVE"].ToString().Trim();
                txtMale.Text = dtr["MSTDDED"].ToString().Trim();
                txtFemale.Text = dtr["FSTDDED"].ToString().Trim();
                txtLimit.Text = dtr["TI_LIMIT"].ToString().Trim();
                txtUpLimit .Text = dtr["TI_UPLIMIT"].ToString().Trim();
                txtEducess.Text = dtr["EDUCESS"].ToString().Trim();
                txtPT.Text = dtr["ADDPT"].ToString().Trim();
                txtMedExeption.Text = dtr["MEDIEXEM"].ToString().Trim();
                txtBSRCode.Text = dtr["BSRCODE"].ToString().Trim();
                txtFromDate.Text = dtr["ITFDATE"].ToString().Trim();
                txtToDate.Text = dtr["ITTDATE"].ToString().Trim();
                txthigheducess.Text = dtr["HIGH_EDUCESS"].ToString().Trim();
                txtTALimit.Text = dtr["TA_LIMIT"].ToString().Trim();
                txtTaxRebateLimit.Text = dtr["RELIEF_200"].ToString().Trim();
                txtHouseAmtLimit.Text = dtr["HOUSEAMTLIMIT"].ToString().Trim();
                txt80CCDNPSLimit.Text = dtr["80CCD_NPS_LIMIT"].ToString().Trim();
                txtRGESSLimit.Text = dtr["RGESS_80CCG_LIMIT"].ToString().Trim();

                if (Convert .ToDecimal ( dtr["ITMONTAX"]) == 1)
                    chkFormNo16 .Checked = true;
                else if (Convert.ToDecimal(dtr["ITMONTAX"]) == 0)
                    chkFormNo16 .Checked = false;


                if (Convert.ToDecimal(dtr["PROPOSED_SAL"]) == 1)
                    chkProposedSalary .Checked = true;
                else if (Convert.ToDecimal(dtr["PROPOSED_SAL"]) == 0)
                    chkProposedSalary .Checked = false;

                if (Convert.ToDecimal(dtr["PREVMON"]) == 1)
                    chkPreviousSalary .Checked = true;
                else if (Convert.ToDecimal(dtr["PREVMON"]) == 0)
                    chkPreviousSalary .Checked = false;

                if (Convert.ToDecimal(dtr["GS_NPS_ADD"]) == 1)
                    chkNPSGrossIT.Checked = true;
                else if (Convert.ToDecimal(dtr["GS_NPS_ADD"]) == 0)
                    chkNPSGrossIT.Checked = false;

               
            }
            dtr.Close();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         

        try 
        {
        ITConfiguration objIT = new ITConfiguration();

        objIT.COLLEGENAME = txtCollegeName.Text.Trim();
        objIT.PANNO = txtPANNo.Text.Trim();
        objIT.TANNO = txtTANNo.Text.Trim();
        objIT.SECTION = txtSection.Text;
        objIT.SIGNNAME = txtSignature.Text.Trim();
        objIT.SIGNFNAME  = txtSonOf.Text.Trim();
        objIT.DESIGNATION = txtDesignation.Text.Trim();
        objIT.EMPBLOCKNO = txtEmpBlockNo.Text.Trim();
        objIT.EMPBUILDINGNAME = txtEmpBuildingName.Text.Trim();
        objIT.EMPROAD = txtEmpRoad.Text.Trim();
        objIT.EMPAREA = txtEmpArea.Text.Trim();
        objIT.EMPCITY = txtEmpCity.Text.Trim();
        objIT.EMPSTATE = txtEmpState.Text.Trim();
        objIT.EMPPINCODE = txtEmpPinCode.Text;
        objIT.EMPPHONENO = txtEmpTelNo .Text;
        objIT.EMPEMAIL = txtEmpEmail.Text.Trim();
        objIT.PERSONBLOCKNO = txtPerBlockNo.Text.Trim();
        objIT.PERSONBUILDINGNAME = txtPerBuildingName.Text.Trim();
        objIT.PERSONROAD = txtPerRoad.Text.Trim();
        objIT.PERSONAREA = txtPerArea.Text.Trim();
        objIT.PERSONCITY = txtPerCity.Text.Trim();
        objIT.PERSONSTATE = txtPerState.Text.Trim();
        objIT.PERSONPINCODE = txtPerPinCode .Text;
        objIT.PERSONPHONENO =txtPerTelNo .Text;
        objIT.PERSONEMAIL = txtPerEmail.Text.Trim();
        objIT.BANKNAME = txtBankName.Text.Trim();
        objIT.BRANCHNAME = txtBranchName.Text.Trim();
        objIT.BANKPLACE = txtBankPlace.Text.Trim();
        if (!txtPrintdate.Text.Trim().Equals(string.Empty))
        {
            objIT.PRINDATE = Convert.ToDateTime(txtPrintdate.Text);
        }
        objIT.LIMIT = Convert.ToDecimal(txtRlimit.Text);
        objIT.BONDLIMIT = Convert.ToDecimal(txtBondLimit .Text);
        objIT.FEMALELIMIT = Convert.ToDecimal(txtFemaleLimit .Text);
        objIT.STDDEDMALE = Convert.ToDecimal(txtMale .Text);
        objIT.STDDEDFEMALE = Convert.ToDecimal(txtFemale .Text);
        objIT.SURCHARGE = Convert.ToDecimal(txtSurcharge.Text);
        objIT.MORETHAN = Convert.ToDecimal(txtMorethan.Text);
        objIT.MEDICALEXEMPTION = Convert.ToDecimal(txtMedExeption .Text);
        objIT.TAXINC_LIMIT = Convert.ToDecimal(txtLimit .Text);
        objIT.TAXINCUP_LIMIT = Convert.ToDecimal(txtUpLimit .Text);
        objIT.EDUCESS = Convert.ToDecimal(txtEducess.Text);
        objIT.ADDITIONALPT = Convert.ToDecimal(txtPT.Text);
        objIT.BSRCODE = txtBSRCode.Text.Trim();
        if (!txtFromDate.Text.Trim().Equals(string.Empty))
        {
            objIT.FROMDATE = Convert.ToDateTime(txtFromDate .Text);
        }
        if (!txtToDate.Text.Trim().Equals(string.Empty))
        {
            objIT.TODATE = Convert.ToDateTime(txtToDate .Text);
        }

        objIT.SHOWFORMNO16 = chkFormNo16.Checked ? 1 : 0;
        objIT.PROPOSEDSALARY = chkProposedSalary.Checked ? 1 : 0;
        objIT.PREVIOUSSALARY = chkPreviousSalary.Checked ? 1 : 0;
        objIT.COLLEGE_CODE = Session["colcode"].ToString();
        objIT.HIGHEDUCESS = Convert.ToDecimal(txthigheducess.Text);
        objIT.ADDNPSGROSSFORIT = chkNPSGrossIT.Checked ? 1 : 0;
        objIT.TALIMIT =Convert.ToDecimal(txtTALimit.Text);
        objIT.TAXREBATE = Convert.ToDecimal(txtTaxRebateLimit.Text);
        objIT.HOUSEAMTLIMIT = Convert.ToDecimal(txtHouseAmtLimit.Text);
        objIT.CCDNPS80 = Convert.ToDecimal(txt80CCDNPSLimit.Text);
        objIT.RGESS_CCG = Convert.ToDecimal(txtRGESSLimit.Text);

        //if (ViewState["action"] != null)
        //{
        //    if (ViewState["action"].ToString().Equals("edit"))
        //    {
                //string uRight = GetUserRight();
                //string TwoCharAdd = uRight.Substring(0, 2).ToString();
                //string TwoCharMod = uRight.Substring(2, 2).ToString();

                //if (TwoCharMod == "YM")
                //{
                    CustomStatus cs = (CustomStatus)objITMas.AddITConfiguration(objIT);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);

                        //ViewState["action"] = "edit";
                    }

                //}
                //else
                //{
                //    objCommon.DisplayMessage(updAdd, Common.Message.NoModify, this);
                //    return;
                //}
        //    }
        //}


    }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        GetConfiguration();
    }

    private void Clear()
    {
        ViewState["action"] = "add";
        txtCollegeName.Text = string.Empty;
        txtPANNo.Text = string.Empty;
        txtTANNo.Text = string.Empty;
        txtSection.Text = string.Empty;
        txtSignature.Text = string.Empty;
        txtSonOf.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtEmpBlockNo.Text = string.Empty;
        txtEmpBuildingName.Text = string.Empty;
        txtEmpRoad.Text = string.Empty;
        txtEmpArea.Text = string.Empty;
        txtEmpCity.Text = string.Empty;
        txtEmpState.Text = string.Empty;
        txtEmpPinCode.Text = string.Empty;
        txtEmpTelNo.Text = string.Empty;
        txtEmpEmail.Text = string.Empty;
        txtPerBlockNo.Text = string.Empty;
        txtPerBuildingName.Text = string.Empty;
        txtPerRoad.Text = string.Empty;
        txtPerArea.Text = string.Empty;
        txtPerCity.Text = string.Empty;
        txtPerState.Text = string.Empty;
        txtPerPinCode.Text = string.Empty;
        txtPerTelNo.Text = string.Empty;
        txtPerEmail.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        txtBankPlace.Text = string.Empty;
        txtPrintdate.Text = string.Empty;
        txtRlimit.Text = string.Empty;
        txtFemaleLimit.Text = string.Empty;
        txtBondLimit.Text = string.Empty;
        txtMale.Text = string.Empty;
        txtFemale.Text = string.Empty;
        txtMedExeption.Text = string.Empty;
        txtSurcharge.Text = string.Empty;
        txtMorethan.Text = string.Empty;
        txtLimit.Text = string.Empty;
        txtUpLimit.Text = string.Empty;
        txtEducess.Text = string.Empty;
        txtPT.Text = string.Empty;
        txtBSRCode.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        chkFormNo16.Checked = false;
        chkProposedSalary.Checked = false;
        chkPreviousSalary.Checked = false;


    }


     
   
}
