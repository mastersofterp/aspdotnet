//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : QUESTION BANK MODULE (YCCE)                      
// CREATION DATE : 18-FEB-2017                                                          
// CREATED BY    : NIKHIL DHONGE                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
#region NAMESPACES
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
using System.Web.UI.WebControls.Adapters;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization;
using System.IO;
using MSXML2;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
#endregion

public partial class Tally_tallyFeeHeads : System.Web.UI.Page
{
    Con_Acd_TallyFeeHeads ObjTFC = new Con_Acd_TallyFeeHeads();
    Ent_Acd_TallyFeeHeads ObjTFM = new Ent_Acd_TallyFeeHeads();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string Message = string.Empty;
    string UsrStatus = string.Empty;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region PAGE LOAD EVENTS

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
                   BindDropDowns();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
              //  if (Request.QueryString["pageno"] != null) ;
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
  
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    public void BindDropDowns()
    {
      
          //  objCommon.FillDropDownList(ddlCashBook, "TallyConfig", "ServerName+':'+Convert(varchar(10),PortNumber) as ServerName", "ServerName", "IsActive = 1 and CollegeId=" + Convert.ToInt32(Session["colcode"]) + " AND EndTime = '9999-12-31'", "AL_NO");
        objCommon.FillDropDownList(ddlCashBook, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO > 0", "RCPTTYPENO");
    }

   

    protected void ddlCashBook_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            repFeeHeads.DataSource = null;
            repFeeHeads.DataBind();

          //  ObjTFM.CommandType = "BindGrid";
            ObjTFM.CashBookName = (ddlCashBook.SelectedValue);
            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);

           DataSet ds= ObjTFC.GetFeesHeads(ObjTFM);

          //  DataSet ds = objCommon.FillDropDown("ACD_RECIEPT_TYPE a inner join ACD_FEE_TITLE b on(a.RECIEPT_CODE = b.RECIEPT_CODE) ", "b.FEE_HEAD, b.FEE_SHORTNAME, b.FEE_LONGNAME", "b.BankLedgerName, b.CashLedgerName", "a.RECIEPT_CODE = '" + ddlCashBook.SelectedValue + "' AND FEE_LONGNAME <> '' AND FEE_LONGNAME IS NOT NULL", "b.FEE_TITLE_NO");


         //   DataSet ds = ObjTFC.GetAllDetails(ObjTFM);



            repFeeHeads.DataSource = ds;
            repFeeHeads.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivFeeHeads.Visible = true;
            }
            else
            {
                DivFeeHeads.Visible = false;
                objCommon.DisplayMessage(upDetails,"Record Not Found", this.Page);
            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void repFeeHeads_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {

    }
    protected void repFeeHeads_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if(e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView rowView = (DataRowView)dataItem.DataItem;
                if (ViewState["Ledgers"] == null)
                {


                    ObjTFM.CommandType = "GetServer";
                    ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
                   // DataSet ds = ObjTFC.GetAllDetails(ObjTFM);

                    DataSet ds = objCommon.FillDropDown("ACD_FEE_TITLE", "FEE_TITLE_NO,FEE_HEAD,FEE_SHORTNAME,FEE_LONGNAME", "ISNULL(CashLedgerName,'0')CashLedgerName,	ISNULL(BankLedgerName,'0')BankLedgerName", "RECIEPT_CODE='" + ddlCashBook.SelectedValue + "'", "FEE_TITLE_NO");

                    DataSet dsServer = objCommon.FillDropDown("TallyConfig","ServerName+':'+Convert(varchar(10),PortNumber) as ServerName","Convert(varchar(10),PortNumber) portnumber","IsActive = 1 and CollegeId = 6 and EndTime = '9999-12-31'","");


                    if (dsServer.Tables[0].Rows[0]["ServerName"] == "")
                    {
                        Showmessage("Server Not Found!. \\n Server Name or Port Number Not Specified.");
                        return;
                    }
                    DataTable dt = GetLedgers(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    ViewState["Ledgers"] = dt;

                }

                DropDownList ddlCashLedgerName = (DropDownList)e.Item.FindControl("ddlCashLedgerName");
                DropDownList ddlBankLedgerName = (DropDownList)e.Item.FindControl("ddlBankLedgerName");


                DataTable dtLedger = (DataTable)ViewState["Ledgers"];


                ddlCashLedgerName.DataSource = dtLedger;
                ddlCashLedgerName.DataTextField = "Name_Text";
                ddlCashLedgerName.DataValueField = "Name_Text";
                ddlCashLedgerName.DataBind();

             //   ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["CashLedgerName"]);

                ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["CashLedgerName"]);


                ddlBankLedgerName.DataSource = dtLedger;
                ddlBankLedgerName.DataTextField = "Name_Text";
                ddlBankLedgerName.DataValueField = "Name_Text";
                ddlBankLedgerName.DataBind();

                ddlBankLedgerName.SelectedValue = Convert.ToString(rowView["BankLedgerName"]);

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    public DataTable GetLedgers(string serverName)
    {
        DataTable dt = new DataTable();

        try
        {
            ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();
            string responseStr = "";
            StringBuilder StrRequestXML = new StringBuilder();
            string nl = System.Environment.NewLine;
            StrRequestXML.AppendLine("<ENVELOPE>");
            StrRequestXML.AppendLine("   <HEADER>");
            StrRequestXML.AppendLine("       <VERSION>1</VERSION>");
            StrRequestXML.AppendLine("       <TALLYREQUEST>Export</TALLYREQUEST>");
            StrRequestXML.AppendLine("       <TYPE>Data</TYPE>");
            StrRequestXML.AppendLine("       <ID>List of Ledgers</ID>");
            StrRequestXML.AppendLine("   </HEADER>");
            StrRequestXML.AppendLine("   <BODY>");
            StrRequestXML.AppendLine("      <DESC>");
            StrRequestXML.AppendLine("        <STATICVARIABLES>");
            StrRequestXML.AppendLine("          <SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>");
            StrRequestXML.AppendLine("        </STATICVARIABLES>");
            StrRequestXML.AppendLine("      </DESC>");
            StrRequestXML.AppendLine("  </BODY>");
            StrRequestXML.AppendLine("</ENVELOPE>");




            DataSet dsLedgers = new DataSet();

            // string Address = "http://" + IpAddress + ":" + portNumber;
            string Address = "http://" + serverName;
            serverHTTP.open("POST", Address, false, null, null);
            serverHTTP.send(StrRequestXML.ToString());
            responseStr = serverHTTP.responseText;
            dsLedgers.ReadXml(new StringReader(responseStr));

            if (dsLedgers.Tables.Count > 0)
            {
                DataRow dr = dsLedgers.Tables[0].NewRow();
                dr[0] = "Please Select";
                dsLedgers.Tables[0].Rows.InsertAt(dr, 0);
                dt = dsLedgers.Tables[0];
            }

        }
        catch (Exception ex)
        {

        }



        return dt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {


            DataTable dt = new DataTable();
            DataColumn dc;

            dc = new DataColumn("FeeHeadId", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CashLedgerName", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("BankLedgerName", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr;

            foreach (ListViewDataItem itm in repFeeHeads.Items)
            {
                HiddenField hdnFeeHeadId = (HiddenField)itm.FindControl("hdnFeeHeadId");
                DropDownList ddlCashLedgerName = (DropDownList)itm.FindControl("ddlCashLedgerName");
                DropDownList ddlBankLedgerName = (DropDownList)itm.FindControl("ddlBankLedgerName");
                if (hdnFeeHeadId != null && ddlCashLedgerName != null && ddlBankLedgerName != null)
                {
                    dr = dt.NewRow();
                    dr["FeeHeadId"] = (hdnFeeHeadId.Value);
                    dr["CashLedgerName"] = ddlCashLedgerName.SelectedValue;
                    dr["BankLedgerName"] = ddlBankLedgerName.SelectedValue;
                    dt.Rows.Add(dr);
                }
                else
                {
                    objCommon.DisplayMessage("Exception Occure", this.Page);
                    return;
                }
            }


            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
            ObjTFM.CashBookName = Convert.ToString(ddlCashBook.SelectedValue);
            
            ObjTFM.ModifiedBy = Convert.ToInt32(Session["userno"]);
            ObjTFM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTFM.MACAddress = Convert.ToString("0");
            ObjTFM.FeeHeadTally = dt;
            long res = ObjTFC.UpdateFeeHeadsTally(ObjTFM, ref Message);

            if (res == -99)
            {
              
                objCommon.DisplayMessage(upDetails,"Exception Occure", this.Page);

                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(upDetails,"Record Already Exsits", this.Page);

                return;

            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(upDetails,"Record Not Save", this.Page);
                return;
            }
            else if (res > 0)
            {
                objCommon.DisplayMessage(upDetails,"Record Save Successfully", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlCashBook.SelectedIndex = 0;
            DivFeeHeads.Visible = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }   
}