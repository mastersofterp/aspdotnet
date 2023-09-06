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

public partial class PAYROLL_Tally_EmployeePayHeadMapping : System.Web.UI.Page
{
    Con_TallyFeeHeads ObjTFC = new Con_TallyFeeHeads();
    Ent_TallyFeeHeads ObjTFM = new Ent_TallyFeeHeads();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string Message = string.Empty;
    string UsrStatus = string.Empty;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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

                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                BindDropDowns();

            }
        }

        divMsg.InnerHtml = string.Empty;
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_pf_entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_pf_entry.aspx");
        }
    }


    public void BindDropDowns()
    {
        objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO <> 0", "STAFFNO");
    }

    protected void ddlStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            repEmployeeHeads.DataSource = null;
            repEmployeeHeads.DataBind();

            ObjTFM.CommandType = "BindGrid";
            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(connectionString);

            DataSet ds = objSQLHelper.ExecuteDataSet("select * from (SELECT 'PAY' PAYHEAD,'PAY' FULLNAME,0 SRNO,'' CashLedgerName FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'' UNION  SELECT PAYHEAD,PAYFULL AS FULLNAME,SRNO, CashLedgerName FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'' UNION SELECT 'NET PAY' PAYHEAD,'NET PAY' FULLNAME,99 SRNO,'' CashLedgerName FROM DBO.PAYROLL_PAYHEAD WHERE PAYFULL IS NOT NULL AND PAYFULL<>'')A  order by SRNO");
            DataSet dsEmployees = objSQLHelper.ExecuteDataSet("select E.IDNO, TITLE+' '+FNAME +' '+MNAME+' '+LNAME AS EMPNAME, E.EmployeeLedgerName from PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO) where PSTATUS='Y' ORDER BY E.IDNO");

            repEmployeeHeads.DataSource = dsEmployees;
            repEmployeeHeads.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivFeeHeads.Visible = true;
            }
            else
            {
                DivFeeHeads.Visible = false;
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
    protected void repEmployeeHeads_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRowView rowView = (DataRowView)dataItem.DataItem;
                if (ViewState["Ledgers"] == null)
                {
                    ObjTFM.CommandType = "GetServer";
                    ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
                    //DataSet ds = ObjTFC.GetAllDetails(ObjTFM);
                    //DataSet ds = objCommon.FillDropDown("PAYROLL_PAYHEAD", "PAYHEAD,PAYFULL,SRNO", "ISNULL(CashLedgerName,'0')CashLedgerName", "", "PAYHEAD");
                    DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO)", "E.IDNO", "TITLE+' '+FNAME +' '+MNAME+' '+LNAME AS EMPNAME, E.EmployeeLedgerName", "PSTATUS='Y'", "E.IDNO");
                    DataSet dsServer = objCommon.FillDropDown("Payroll_TallyConfig", "ServerName+':'+Convert(varchar(10),PortNumber) as ServerName", "Convert(varchar(10),PortNumber) portnumber", "IsActive = 1 and CollegeId = 1 and EndTime = '9999-12-31'", "");
                 //   DataSet dsServer = objCommon.FillDropDown("Payroll_TallyConfig", "ServerName+':'+Convert(varchar(100),ServerName) as ServerName", "Convert(varchar(10),PortNumber) portnumber", "IsActive = 1 and CollegeId =1 and EndTime = '9999-12-31'", "");

                    if (dsServer.Tables[0].Rows[0]["ServerName"] == "")
                    {
                        Showmessage("Server Not Found!. \\n Server Name or Port Number Not Specified.");
                        return;
                    }
                    DataTable dt = GetLedgers(Convert.ToString(dsServer.Tables[0].Rows[0]["ServerName"]));
                    ViewState["Ledgers"] = dt;
                }

                DropDownList ddlCashLedgerName = (DropDownList)e.Item.FindControl("ddlCashLedgerName");

                DataTable dtLedger = (DataTable)ViewState["Ledgers"];

                ddlCashLedgerName.DataSource = dtLedger;
                ddlCashLedgerName.DataTextField = "Name_Text";
                ddlCashLedgerName.DataValueField = "Name_Text";
                ddlCashLedgerName.DataBind();

                ddlCashLedgerName.SelectedValue = Convert.ToString(rowView["EmployeeLedgerName"]);
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

            dc = new DataColumn("EmployeeId", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CashLedgerName", typeof(string));
            dt.Columns.Add(dc);

            DataRow dr;

            foreach (ListViewDataItem itm in repEmployeeHeads.Items)
            {
                HiddenField hdnEmplyeeId = (HiddenField)itm.FindControl("hdnEmplyeeId");
                DropDownList ddlCashLedgerName = (DropDownList)itm.FindControl("ddlCashLedgerName");
                if (hdnEmplyeeId != null && ddlCashLedgerName != null)
                {
                    dr = dt.NewRow();
                    dr["EmployeeId"] = (hdnEmplyeeId.Value);
                    dr["CashLedgerName"] = ddlCashLedgerName.SelectedValue;
                    dt.Rows.Add(dr);
                }
                else
                {
                    objCommon.DisplayMessage("Exception Occure", this.Page);
                    return;
                }
            }

            ObjTFM.CollegeId = Convert.ToInt32(Session["colcode"]);
            ObjTFM.CashBookName = Convert.ToString(ddlStaff.SelectedValue);
            ObjTFM.ModifiedBy = Convert.ToInt32(Session["userno"]);
            ObjTFM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjTFM.MACAddress = Convert.ToString("0");
            ObjTFM.FeeHeadTally = dt;
            long res = ObjTFC.UpdateEmployeeLedgerHeadTally(ObjTFM, ref Message);

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