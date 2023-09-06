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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization;
using System.IO;
using MSXML2;
#endregion

public partial class Tally_tallyFeeHeads : System.Web.UI.Page
{
    Con_TallyFeeHeads ObjTFC = new Con_TallyFeeHeads();
    Ent_TallyFeeHeads ObjTFM = new Ent_TallyFeeHeads();
    Con_PayrollTallyConfig ObjPTC = new Con_PayrollTallyConfig();
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                BindDropDowns();

            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    public void BindDropDowns()
    {
        objCommon.FillDropDownList(ddlCollegeName, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO <> 0", "STAFFNO");
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStaff.SelectedIndex == 0)
            {
                repFeeHeads.DataSource = null;
                repFeeHeads.DataBind();
                DivFeeHeads.Visible = false;
            }
            else
            {
                repFeeHeads.DataSource = null;
                repFeeHeads.DataBind();

                ObjTFM.CommandType = "BindGrid";
                ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                DataSet ds = ObjPTC.GetTallyPayHeadDataStaffTypeWise(Convert.ToInt32(ddlCollegeName.SelectedValue), Convert.ToInt32(ddlStaff.SelectedValue));

                repFeeHeads.DataSource = ds;
                repFeeHeads.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DivFeeHeads.Visible = true;
                }
                else
                {
                    DivFeeHeads.Visible = false;
                }
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
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView rowView = (DataRowView)dataItem.DataItem;
                if (ViewState["Ledgers"] == null || (ViewState["Ledgers"] as DataTable).Rows.Count == 0)
                {
                    ObjTFM.CommandType = "GetServer";
                    ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
                    //DataSet ds = objCommon.FillDropDown("PAYROLL_PAYHEAD", "PAYHEAD,PAYFULL,SRNO", "ISNULL(CashLedgerName,'0')CashLedgerName", "", "PAYHEAD");

                    DataSet dsServer = objCommon.FillDropDown("Payroll_TallyConfig", "ServerName+':'+Convert(varchar(10),PortNumber) as ServerName", "Convert(varchar(10),PortNumber) portnumber", "IsActive = 1 and CollegeId = " + ddlCollegeName.SelectedValue + " and EndTime = '9999-12-31'", "");

                    if (dsServer.Tables[0].Rows[0]["ServerName"] == "")
                    {
                        Showmessage("Server Not Found!. \\n Server Name or Port Number Not Specified.");
                        return;
                    }
                    DataTable dt = GetLedgers(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    ViewState["Ledgers"] = dt;

                    //DataTable dtCostCategory = GetCostCategory(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    //ViewState["CostCategory"] = dtCostCategory;

                    //DataTable dtCost = GetCostCenter(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    //ViewState["CostCenter"] = dtCost;
                }

                DropDownList ddlCashLedgerName = (DropDownList)e.Item.FindControl("ddlCashLedgerName");
                DropDownList ddlCostCategory = (DropDownList)e.Item.FindControl("ddlCostCategory");
                DropDownList ddlCostCenter = (DropDownList)e.Item.FindControl("ddlCostCenter");

                DataTable dtLedger = (DataTable)ViewState["Ledgers"];

                ddlCashLedgerName.DataSource = dtLedger;
                ddlCashLedgerName.DataTextField = "Name_Text";
                ddlCashLedgerName.DataValueField = "Name_Text";
                ddlCashLedgerName.DataBind();

                ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["CashLedgerName"]);

                //DataTable dtCostCategory1 = (DataTable)ViewState["CostCategory"];

                //ddlCostCategory.DataSource = dtCostCategory1;
                //ddlCostCategory.DataTextField = "Name_Text";
                //ddlCostCategory.DataValueField = "Name_Text";
                //ddlCostCategory.DataBind();

                //DataTable dtCostCenter1 = (DataTable)ViewState["CostCenter"];

                //ddlCostCenter.DataSource = dtCostCenter1;
                //ddlCostCenter.DataTextField = "Name_Text";
                //ddlCostCenter.DataValueField = "Name_Text";
                //ddlCostCenter.DataBind();
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
        Ent_TransferRecordsToTally objTRM = new Ent_TransferRecordsToTally();
        Con_TransferRecordsToTally objTRC = new Con_TransferRecordsToTally();
        Con_PayrollTallyConfig objPTC = new Con_PayrollTallyConfig();

        DataSet dsCompany = objPTC.GetTallyCompanyName(objTRM, Convert.ToInt32(ddlCollegeName.SelectedValue));
        string CompanyName = dsCompany.Tables[0].Rows[0]["TallyCompanyName"].ToString();
        CompanyName = "Test Company";


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
            StrRequestXML.AppendLine("          <SVCURRENTCOMPANY>" + CompanyName + "</SVCURRENTCOMPANY>");
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
                //dr[0] = "Please Select";
                //dsLedgers.Tables[0].Rows.InsertAt(dr, 0);
                dt = dsLedgers.Tables[0];
            }

        }
        catch (Exception ex)
        {

        }



        return dt;
    }

    public DataTable GetCostCategory(string serverName)
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
            StrRequestXML.AppendLine("       <ID>List of Categories</ID>");
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
                //dr[0] = "Please Select";
                //dsLedgers.Tables[0].Rows.InsertAt(dr, 0);
                dt = dsLedgers.Tables[0];
            }
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable GetCostCenter(string serverName)
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
            StrRequestXML.AppendLine("       <CATEGORY>List of Categories</CATEGORY>");
            StrRequestXML.AppendLine("       <ID>List of Cost Centres</ID>");
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
                //dr[0] = "Please Select";
                //dsLedgers.Tables[0].Rows.InsertAt(dr, 0);
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

            dc = new DataColumn("PayHeadId", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CashLedgerName", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CostCategory", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CostCenter", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr;

            foreach (ListViewDataItem itm in repFeeHeads.Items)
            {
                HiddenField hdnPayHeadId = (HiddenField)itm.FindControl("hdnPayHeadId");
                DropDownList ddlCashLedgerName = (DropDownList)itm.FindControl("ddlCashLedgerName");
                if (hdnPayHeadId != null && ddlCashLedgerName != null)
                {
                    dr = dt.NewRow();
                    dr["PayHeadId"] = (hdnPayHeadId.Value);
                    dr["CashLedgerName"] = ddlCashLedgerName.SelectedValue;
                    dr["CostCategory"] = string.Empty;
                    dr["CostCenter"] = string.Empty;
                    dt.Rows.Add(dr);
                }
                else
                {
                    objCommon.DisplayMessage("Exception Occure", this.Page);
                    return;
                }
            }

            ObjTFM.CollegeId = Convert.ToInt32(ddlCollegeName.SelectedValue);
            ObjTFM.CashBookName = Convert.ToString(ddlStaff.SelectedValue);
            ObjTFM.ModifiedBy = Convert.ToInt32(Session["userno"]);
            ObjTFM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTFM.MACAddress = Convert.ToString("0");
            ObjTFM.FeeHeadTally = dt;
            long res = ObjPTC.UpdatePayHeadsTallyStaffType(ObjTFM, ref Message);

            if (res == -99)
            {

                objCommon.DisplayMessage(upDetails, "Exception Occure", this.Page);

                return;

            }
            else if (res == 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Already Exsits", this.Page);

                return;

            }
            else if (res <= 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Not Save", this.Page);
                return;
            }
            else if (res > 0)
            {
                objCommon.DisplayMessage(upDetails, "Record Save Successfully", this.Page);
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
            ddlStaff.SelectedIndex = 0;
            DivFeeHeads.Visible = false;
        }
        catch (Exception ex)
        {

            throw;
        }
    }



}