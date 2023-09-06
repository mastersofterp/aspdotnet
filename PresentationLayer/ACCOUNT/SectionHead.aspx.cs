//=================================================================================
// PROJECT NAME  : RF-CAMPUS                                                           
// MODULE NAME   : 
// CREATION DATE : 15-Oct-2018                                             
// CREATED BY    : Nokhlal Kumar                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
// AIM           : This form is used to Insert Section Head
//=================================================================================
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

using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_SectionHead : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SectionHead ObjSectionEntity = new SectionHead();
    SectionHeadController objSectionController = new SectionHeadController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common();
            //objCcbc = new CombinedCashBankBookController();
            //objrpc = new ReceiptPaymentController();

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        //Session["WithoutCashBank"] = "N";

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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayUserMessage(UpdSection, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    //Check Page Authorization
                    CheckPageAuthorization();
                    PopulateListBox();
                    ViewState["Edit"] = "N";
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtSection.Text == string.Empty || txtSection.Text == "")
        {
            objCommon.DisplayMessage(UpdSection, "Please enter section head", this.Page);
            return;
        }

        if (ViewState["Edit"].ToString() == "N")
        {
            ObjSectionEntity.SectionNo = 0;
        }
        else
        {
            ObjSectionEntity.SectionNo = Convert.ToInt32(ViewState["id"].ToString());
        }
        ObjSectionEntity.SectionName = txtSection.Text.ToString().Trim();
        if (txtSectionPercent.Text != "" || txtSectionPercent.Text != string.Empty)
        {
            ObjSectionEntity.SectionPer = Convert.ToDouble(txtSectionPercent.Text.ToString().Trim());
        }
        else
        {
            ObjSectionEntity.SectionPer = 0;
        }

        string IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_Section", "SECTION_NAME", "SECTION_NAME='" + txtSection.Text + "'");
        if (IsAvailable != string.Empty)
        {
            objCommon.DisplayUserMessage(UpdSection, "Section Already Exist", this.Page);
            return;
        }

        string comp_code = Session["Comp_Code"].ToString();
        int collegecode = 33;

        int res = objSectionController.AddSection(ObjSectionEntity, comp_code, collegecode, Convert.ToInt32(Session["Userno"].ToString()));
        if (res == 1)
        {
            objCommon.DisplayMessage(UpdSection, "Section Created successfully", this);
            ClearAll();
            PopulateListBox();
            txtSection.Focus();
            return;
        }
        else if (res == 2)
        {
            objCommon.DisplayMessage(UpdSection, "Section Updated successfully", this);
            ClearAll();
            PopulateListBox();
            txtSection.Focus();
            return;
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void PopulateListBox()
    {
        objCommon = new Common();
        try
        {
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_SECTION", "SECTION_NO", "UPPER(SECTION_NAME) AS SECTIONNAME", "SECTION_NO > 0", "SECTION_NO");// "PARTY_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstSectionName.Items.Clear();
                    lstSectionName.DataTextField = "SECTIONNAME";
                    lstSectionName.DataValueField = "SECTION_NO";
                    lstSectionName.DataSource = ds.Tables[0];
                    lstSectionName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SectionHead.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
        }
    }

    private void ClearAll()
    {
        txtSection.Text = string.Empty;
        txtSectionPercent.Text = string.Empty;
        ViewState["Edit"] = "N";
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SectionHead.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SectionHead.aspx");
        }
    }
    protected void lstSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = Request.Form[lstSectionName.UniqueID].ToString();
        DataSet DsSection = null;
        if (id != "" | id != string.Empty)
        {
            ClearAll();
            ViewState["Edit"] = "Y";
            ViewState["id"] = id.ToString();

            DsSection = objCommon.FillDropDown("ACC_" + Session["Comp_Code"] + "_SECTION", "Section_No", "*", "Section_No=" + Convert.ToInt32(ViewState["id"].ToString()), "");

            if (DsSection != null)
            {
                if (DsSection.Tables[0].Rows.Count > 0)
                {
                    txtSection.Text = DsSection.Tables[0].Rows[0]["Section_Name"].ToString();
                    txtSectionPercent.Text = DsSection.Tables[0].Rows[0]["Section_percent"].ToString();
                }
            }
        }
    }
}