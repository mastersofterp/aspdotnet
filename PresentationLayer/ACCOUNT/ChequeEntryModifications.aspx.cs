//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CHEQUE ENTRY MODIFICATIONS                                                     
// CREATION DATE : 06-MAY-2010                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using System.Data.SqlClient;
using System.IO.Ports;
using IITMS.NITPRM;

public partial class ChequeEntryModifications : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    string ReceiveParameters;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            ReceiveParameters = Request.QueryString["obj"].ToString().Trim();

            if (ReceiveParameters.ToString().Trim() == "ChequePrinting")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");
            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
    }
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    string isMessagingEnabled = string.Empty;
    string back = string.Empty;
    DataTable dt = new DataTable();
    public string[] para;
    public static string isEdit;
    public static int RowIndex=-1;
    GsmCommMain comm;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!Page.IsPostBack)
        {


            txtfrmDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Trim();
            txtUptoDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Trim();

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
                                     
                }

                

            }
            
            
          
            

        }

       divMsg.InnerHtml = string.Empty;
    }


    //===============


    private void ShowChequeEntryRecord()
    {


        DataSet dschq = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_CHECK_PRINT", "*", "", "trno=0 AND CHECKDT BETWEEN '" + Convert.ToDateTime(txtfrmDate.Text).ToString("dd-MMM-yyyy") + "' AND '" + Convert.ToDateTime(txtUptoDate.Text).ToString("dd-MMM-yyyy") + "' AND TRNO =0", "");
        if (dschq != null)
        {
            if (dschq.Tables[0].Rows.Count > 0)
            {
                GridData.DataSource = dschq;
                GridData.DataBind();
                
            }

        }
    
    }

    
    private void CheckPageAuthorization()
    {
        
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=BankEntry.aspx");
            //}
       
    }



    protected void btnGet_Click(object sender, EventArgs e)
    {
        ShowChequeEntryRecord();
    }
    //protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    HiddenField hdnid = e.Row.FindControl("hdntrno") as HiddenField;
    //    LinkButton lnk = e.Row.FindControl("lnkparty") as LinkButton;
    //    if (hdnid != null && lnk != null)
    //    {
    //        e.Row.Attributes.Add("onclick", "UpdateParentWindow('" + hdnid.Value + "','" + lnk.Text + "');");
    //    }
    //    else
    //    {
    //        Session["BookTran"] = null;

    //    }
    //}

    protected void GridData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

       
      HiddenField hdnid=  GridData.Rows[e.NewSelectedIndex].FindControl("hdntrno") as HiddenField;
      HiddenField hdnpartyno1 = GridData.Rows[e.NewSelectedIndex].FindControl("hdnPartyNo1") as HiddenField;
      if (hdnid != null)
      {
          Response.Redirect("ChequePrinting.aspx?objMod=ChequePrinting," + hdnid.Value.ToString().Trim() + "," + hdnpartyno1.Value.ToString().Trim());
          
      
      }

        


    }
}
