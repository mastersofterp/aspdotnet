//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                           
// PAGE NAME     : MESS MONTHLY EXPENDITURE                                                 
// CREATION DATE : 29/12/2012                                                      
// CREATED BY    : MRUNAL BANSOD                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
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
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

//using System.Transactions;

public partial class HOSTEL_MASTERS_MonthlyExpenditure : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MessExpenditureMaster objMEM = new MessExpenditureMaster();
    MessExpenditureController objMEC =new MessExpenditureController ();
    MessBillMaster objMBM = new MessBillMaster();
    MessBillController objMBC = new MessBillController ();
    string Message = string.Empty;
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
                Session["colcode"] = "1";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //pnlfees.Visible = false;
                txtDate.Text = System.DateTime.Now.ToShortDateString ();
                ddlMess.Focus();
                FillDDL();
                BindListView();
                ViewState["TRNO"] = "0";
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MessHeadEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MessHeadEntry.aspx");
        }
    }

    public void FillDDL()
    {
        objCommon.FillDropDownList(ddlMess, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "", "");
        if (Session["usertype"].ToString() == "1")
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
        else
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO>0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
        ddlSession.SelectedIndex = 1;
    }

    public void BindListView()
    {
        DataSet ds = new DataSet();
        string no = "0";
        ds = objMEC.GetMessHeads(no);
        int month =Convert.ToInt32(Convert.ToDateTime(txtDate.Text.Trim()).Month);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Lsv1.DataSource = ds.Tables[0];
            Lsv1.DataBind();
            TextBox txttot = (TextBox)Lsv1.FindControl("txttotal");
            txttot.Text = "";
            if (ddlMess.SelectedIndex != 0 && txtDate.Text != string.Empty)
            {
                DataSet dsstd = objMEC.GetMessHeadsbyMessno_Month(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue.ToString().Trim()), month);
                if (dsstd != null && dsstd.Tables[0].Rows.Count > 0)
                {
                    txttot.Text = "0";
                    for (int i = 0; i < Lsv1.Items.Count; i++)
                    {
                        TextBox txtAmt = (TextBox)Lsv1.Items[i].FindControl("txtshead");
                        txtAmt.Text = "";
                        Label lbl = (Label)Lsv1.Items[i].FindControl("lblshead");


                        if (txtAmt != null && txttot != null && lbl != null)
                        {
                            txtAmt.Text = dsstd.Tables[0].Rows[0][lbl.Text.ToString().Trim()].ToString().Trim();

                            txttot.Text = (Convert.ToDouble(txttot.Text) + Convert.ToDouble(dsstd.Tables[0].Rows[0][lbl.Text.ToString().Trim()].ToString().Trim())).ToString();

                        }
                        ViewState["Total"] = Convert.ToInt32(txttot.Text);

                    }

                }
            }
        }
        else
        {
            //objCommon.DisplayUserMessage(updpanel, "Mess Heads Are Not Alloted!", this);
            Lsv1.DataSource = null;
            Lsv1.DataBind();
            ddlMess.Focus();
            return;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //TransactionOptions transactionOptions = new TransactionOptions();
            //transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            //transactionOptions.Timeout = TimeSpan.FromDays(1);
            //TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions, EnterpriseServicesInteropOption.Full);
            //using (transactionScope)
            //{
                //string uRight = GetUserRight();
                //string TwoCharAdd = uRight.Substring(0, 2).ToString();
                //string TwoCharMod = uRight.Substring(2, 2).ToString();

                //if (uRight == "NANMNR")
                //{
                //    objCommon.DisplayMessage(updpanel, Common.Message.NoRights, this);
                //    return;
                //}
                if (Lsv1.Items.Count == 0)
                {
                    //objCommon.DisplayUserMessage(updpanel, "Mess Heads Are Not Configured!", this);
                    objCommon.DisplayMessage("Mess Heads Are Not Configured!", this.Page);
                    ddlMess.Focus();
                    return;
                }

                if (ddlMess.SelectedIndex == 0)
                {
                   // objCommon.DisplayUserMessage(updpanel, "Select Mess", this);
                    objCommon.DisplayMessage("Select Mess", this.Page);
                    return;
                }
                if (txtDate.Text == "")
                {
                    //objCommon.DisplayUserMessage(updpanel, "Select Month", this);
                    objCommon.DisplayMessage("Select Month", this.Page);
                    return;
                }
                if ((ViewState["edit"].ToString()) == "add")
                {
                    bool result = CheckMonth();
                    if (result == true)
                    {
                        //objCommon.DisplayUserMessage(updpanel, "Expenditure for this month is already defined", this);
                        objCommon.DisplayMessage("Expenditure for this month is already defined", this.Page);
                        return;
                    }
                }

                if ((ViewState["edit"].ToString()) == "edit")
                {
                    DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_MESS_EXPENDITURE", "*", "", "TRNO='" + Convert.ToInt32(ViewState["TRNO"]) + "'", "");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(txtDate.Text).ToString()== Convert.ToDateTime(ds.Tables[0].Rows[0]["MONTH"].ToString().Trim()).ToString())
                        {
                           // objCommon.DisplayUserMessage(updpanel, "Expenditure for this month is already defined", this);
                            objCommon.DisplayMessage("Expenditure for this month is already defined", this.Page);
                            return;
                        }
                        //else
                        //{ }
                    }
                }

                objMEM.MESSNO = Convert.ToInt32(ddlMess.SelectedValue.ToString().Trim());
                objMEM.MONTH = Convert.ToDateTime(txtDate.Text);// System.DateTime.Now;//.Month; //Convert.ToDateTime(ddlMonth.SelectedValue.ToString().Trim());
                objMEM.DIET = 0.0;

                TextBox txttot = (TextBox)Lsv1.FindControl("txttot");

                if (hdnTotal.Value.ToString().Trim() == "")
                {
                    objMEM.TOTALEXPENDITURE = Convert.ToInt32(ViewState["Total"]);

                }
                else
                {
                    objMEM.TOTALEXPENDITURE = Convert.ToDouble(hdnTotal.Value);
                }

                Hashtable hlist = new Hashtable(40);
                hlist.Clear();

                for (int i = 0; i < Lsv1.Items.Count; i++)
                {
                    TextBox txtshead = Lsv1.Items[i].FindControl("txtshead") as TextBox;
                    //txtshead.Text = "";
                    Label lblshead = Lsv1.Items[i].FindControl("lblshead") as Label;

                    if (txtshead != null && lblshead != null)
                    {
                        hlist.Add(lblshead.Text, txtshead.Text.ToString().Trim());
                    }
                }

                if (hlist["F1"] != null)
                {
                    if (hlist["F1"].ToString().Trim() == "")
                    {
                        objMEM.F1 = 0;
                    }
                    else
                    {
                        objMEM.F1 = Convert.ToDouble(hlist["F1"].ToString().Trim());
                    }
                }
                if (hlist["F2"] != null)
                {
                    if (hlist["F2"].ToString().Trim() == "")
                    {

                        objMEM.F2 = 0;
                    }
                    else
                    {
                        objMEM.F2 = Convert.ToDouble(hlist["F2"].ToString().Trim());

                    }
                }
                if (hlist["F3"] != null)
                {
                    if (hlist["F3"].ToString().Trim() == "")
                    {
                        objMEM.F3 = 0;
                    }
                    else
                    {
                        objMEM.F3 = Convert.ToDouble(hlist["F3"].ToString().Trim());
                    }
                }
                if (hlist["F4"] != null)
                {
                    if (hlist["F4"].ToString().Trim() == "")
                    {
                        objMEM.F4 = 0;
                    }
                    else
                    {
                        objMEM.F4 = Convert.ToDouble(hlist["F4"].ToString().Trim());
                    }
                }
                if (hlist["F5"] != null)
                {
                    if (hlist["F5"].ToString().Trim() == "")
                    {
                        objMEM.F5 = 0;
                    }
                    else
                    {
                        objMEM.F5 = Convert.ToDouble(hlist["F5"].ToString().Trim());
                    }
                }
                if (hlist["F6"] != null)
                {
                    if (hlist["F6"].ToString().Trim() == "")
                    {
                        objMEM.F6 = 0;
                    }
                    else
                    {
                        objMEM.F6 = Convert.ToDouble(hlist["F6"].ToString().Trim());
                    }

                }
                if (hlist["F7"] != null)
                {
                    if (hlist["F7"].ToString().Trim() == "")
                    {
                        objMEM.F7 = 0;
                    }
                    else
                    {

                        objMEM.F7 = Convert.ToDouble(hlist["F7"].ToString().Trim());
                    }
                }
                if (hlist["F8"] != null)
                {
                    if (hlist["F8"].ToString().Trim() == "")
                    {
                        objMEM.F8 = 0;
                    }
                    else
                    {
                        objMEM.F8 = Convert.ToDouble(hlist["F8"].ToString().Trim());
                    }
                }
                if (hlist["F9"] != null)
                {
                    if (hlist["F9"].ToString().Trim() == "")
                    {
                        objMEM.F9 = 0;
                    }
                    else
                    {

                        objMEM.F9 = Convert.ToDouble(hlist["F9"].ToString().Trim());
                    }
                }
                if (hlist["F10"] != null)
                {
                    if (hlist["F10"].ToString().Trim() == "")
                    {
                        objMEM.F10 = 0;
                    }
                    else
                    {
                        objMEM.F10 = Convert.ToDouble(hlist["F10"].ToString().Trim());
                    }
                }
                if (hlist["F11"] != null)
                {
                    if (hlist["F11"].ToString().Trim() == "")
                    {
                        objMEM.F11 = 0;
                    }
                    else
                    {
                        objMEM.F11 = Convert.ToDouble(hlist["F11"].ToString().Trim());
                    }
                }
                if (hlist["F12"] != null)
                {
                    if (hlist["F12"].ToString().Trim() == "")
                    {
                        objMEM.F12 = 0;
                    }
                    else
                    {
                        objMEM.F12 = Convert.ToDouble(hlist["F12"].ToString().Trim());
                    }
                }
                if (hlist["F13"] != null)
                {
                    if (hlist["F13"].ToString().Trim() == "")
                    {
                        objMEM.F13 = 0;
                    }
                    else
                    {
                        objMEM.F13 = Convert.ToDouble(hlist["F13"].ToString().Trim());
                    }
                }
                if (hlist["F14"] != null)
                {
                    if (hlist["F14"].ToString().Trim() == "")
                    {
                        objMEM.F14 = 0;
                    }
                    else
                    {
                        objMEM.F14 = Convert.ToDouble(hlist["F14"].ToString().Trim());
                    }
                }
                if (hlist["F15"] != null)
                {
                    if (hlist["F15"].ToString().Trim() == "")
                    {
                        objMEM.F15 = 0;
                    }
                    else
                    {
                        objMEM.F15 = Convert.ToDouble(hlist["F15"].ToString().Trim());
                    }
                }
                if (hlist["F16"] != null)
                {
                    if (hlist["F16"].ToString().Trim() == "")
                    {
                        objMEM.F16 = 0;
                    }
                    else
                    {
                        objMEM.F16 = Convert.ToDouble(hlist["F16"].ToString().Trim());
                    }
                }
                if (hlist["F17"] != null)
                {
                    if (hlist["F17"].ToString().Trim() == "")
                    {
                        objMEM.F17 = 0;
                    }
                    else
                    {

                        objMEM.F17 = Convert.ToDouble(hlist["F17"].ToString().Trim());
                    }
                }
                if (hlist["F18"] != null)
                {
                    if (hlist["F18"].ToString().Trim() == "")
                    {
                        objMEM.F18 = 0;
                    }
                    else
                    {
                        objMEM.F18 = Convert.ToDouble(hlist["F18"].ToString().Trim());
                    }
                }
                if (hlist["F19"] != null)
                {
                    if (hlist["F19"].ToString().Trim() == "")
                    {
                        objMEM.F19 = 0;
                    }
                    else
                    {
                        objMEM.F19 = Convert.ToDouble(hlist["F19"].ToString().Trim());
                    }
                }
                if (hlist["F20"] != null)
                {
                    if (hlist["F20"].ToString().Trim() == "")
                    {
                        objMEM.F20 = 0;
                    }
                    else
                    {
                        objMEM.F20 = Convert.ToDouble(hlist["F20"].ToString().Trim());
                    }
                }
                if (hlist["F21"] != null)
                {
                    if (hlist["F21"].ToString().Trim() == "")
                    {
                        objMEM.F21 = 0;
                    }
                    else
                    {
                        objMEM.F21 = Convert.ToDouble(hlist["F21"].ToString().Trim());
                    }
                }
                if (hlist["F22"] != null)
                {
                    if (hlist["F22"].ToString().Trim() == "")
                    {
                        objMEM.F22 = 0;
                    }
                    else
                    {
                        objMEM.F22 = Convert.ToDouble(hlist["F22"].ToString().Trim());
                    }
                }
                if (hlist["F23"] != null)
                {
                    if (hlist["F23"].ToString().Trim() == "")
                    {
                        objMEM.F23 = 0;
                    }
                    else
                    {
                        objMEM.F23 = Convert.ToDouble(hlist["F23"].ToString().Trim());
                    }
                }
                if (hlist["F24"] != null)
                {
                    if (hlist["F24"].ToString().Trim() == "")
                    {
                        objMEM.F24 = 0;
                    }
                    else
                    {
                        objMEM.F24 = Convert.ToDouble(hlist["F24"].ToString().Trim());
                    }
                }
                if (hlist["F25"] != null)
                {
                    if (hlist["F25"].ToString().Trim() == "")
                    {
                        objMEM.F25 = 0;
                    }
                    else
                    {
                        objMEM.F25 = Convert.ToDouble(hlist["F25"].ToString().Trim());
                    }
                }
                if (hlist["F26"] != null)
                {
                    if (hlist["F26"].ToString().Trim() == "")
                    {
                        objMEM.F26 = 0;
                    }
                    else
                    {
                        objMEM.F26 = Convert.ToDouble(hlist["F26"].ToString().Trim());
                    }
                }
                if (hlist["F27"] != null)
                {
                    if (hlist["F27"].ToString().Trim() == "")
                    {
                        objMEM.F27 = 0;
                    }
                    else
                    {
                        objMEM.F27 = Convert.ToDouble(hlist["F27"].ToString().Trim());
                    }
                }
                if (hlist["F28"] != null)
                {
                    if (hlist["F28"].ToString().Trim() == "")
                    {
                        objMEM.F28 = 0;
                    }
                    else
                    {
                        objMEM.F28 = Convert.ToDouble(hlist["F28"].ToString().Trim());
                    }
                }
                if (hlist["F29"] != null)
                {
                    if (hlist["F29"].ToString().Trim() == "")
                    {
                        objMEM.F29 = 0;
                    }
                    else
                    {
                        objMEM.F29 = Convert.ToDouble(hlist["F29"].ToString().Trim());
                    }
                }
                if (hlist["F30"] != null)
                {
                    if (hlist["F30"].ToString().Trim() == "")
                    {
                        objMEM.F30 = 0;
                    }
                    else
                    {
                        objMEM.F30 = Convert.ToDouble(hlist["F30"].ToString().Trim());
                    }
                }
                if (hlist["F31"] != null)
                {
                    if (hlist["F31"].ToString().Trim() == "")
                    {
                        objMEM.F31 = 0;
                    }
                    else
                    {
                        objMEM.F31 = Convert.ToDouble(hlist["F31"].ToString().Trim());
                    }
                }
                if (hlist["F32"] != null)
                {
                    if (hlist["F32"].ToString().Trim() == "")
                    {
                        objMEM.F32 = 0;
                    }
                    else
                    {
                        objMEM.F32 = Convert.ToDouble(hlist["F32"].ToString().Trim());
                    }
                }
                if (hlist["F33"] != null)
                {
                    if (hlist["F33"].ToString().Trim() == "")
                    {
                        objMEM.F33 = 0;
                    }
                    else
                    {
                        objMEM.F33 = Convert.ToDouble(hlist["F33"].ToString().Trim());
                    }
                }
                if (hlist["F34"] != null)
                {
                    if (hlist["F34"].ToString().Trim() == "")
                    {
                        objMEM.F34 = 0;
                    }
                    else
                    {
                        objMEM.F34 = Convert.ToDouble(hlist["F34"].ToString().Trim());
                    }
                }
                if (hlist["F35"] != null)
                {
                    if (hlist["F35"].ToString().Trim() == "")
                    {
                        objMEM.F35 = 0;
                    }
                    else
                    {
                        objMEM.F35 = Convert.ToDouble(hlist["F35"].ToString().Trim());
                    }
                }
                if (hlist["F36"] != null)
                {
                    if (hlist["F36"].ToString().Trim() == "")
                    {
                        objMEM.F36 = 0;
                    }
                    else
                    {
                        objMEM.F36 = Convert.ToDouble(hlist["F36"].ToString().Trim());
                    }
                }
                if (hlist["F37"] != null)
                {
                    if (hlist["F37"].ToString().Trim() == "")
                    {
                        objMEM.F37 = 0;
                    }
                    else
                    {
                        objMEM.F37 = Convert.ToDouble(hlist["F37"].ToString().Trim());
                    }
                }
                if (hlist["F38"] != null)
                {
                    if (hlist["F38"].ToString().Trim() == "")
                    {
                        objMEM.F38 = 0;
                    }
                    else
                    {
                        objMEM.F38 = Convert.ToDouble(hlist["F38"].ToString().Trim());
                    }
                }
                if (hlist["F39"] != null)
                {
                    if (hlist["F39"].ToString().Trim() == "")
                    {
                        objMEM.F39 = 0;
                    }
                    else
                    {
                        objMEM.F39 = Convert.ToDouble(hlist["F39"].ToString().Trim());
                    }
                }
                if (hlist["F40"] != null)
                {
                    if (hlist["F40"].ToString().Trim() == "")
                    {
                        objMEM.F40 = 0;
                    }
                    else
                    {
                        objMEM.F40 = Convert.ToDouble(hlist["F40"].ToString().Trim());
                    }
                }

                objMEM.USERID = Session["idno"].ToString();
                objMEM.IPADDRESS = "0";// Session["IPADDR"].ToString();
                objMEM.MACADDRESS = "0";// Session["MACADDR"].ToString();
                objMEM.AUDITDATE = System.DateTime.Now;
                objMEM.COLLEGE_CODE = Session["colcode"].ToString();
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int Hostelno = Convert.ToInt32(ddlHostel.SelectedValue);
                objMEM.TRNO = Convert.ToInt32(ViewState["TRNO"].ToString());

                long ret = objMEC.AddUpdateMessExpenditure(objMEM, Sessionno, Hostelno, ref Message);
                if (ret > 0)
                {
                    objCommon.DisplayMessage("Record Saved successfully!!", this.Page);
                    GetMonthlyExpenditure();
                    Clear();
                    ViewState["TRNO"] = "0";
                }
                if (ret <= 0)
                {
                    if (Message.ToString().Trim() == "")
                    {
                        //objCommon.DisplayMessage(updpanel, Common.Message.NotSaved, this);
                        objCommon.DisplayMessage("Record Not saved", this.Page);
                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
                        objCommon.DisplayMessage("Exception occured", this.Page);
                    }
                }
                else
                {
                    //HttpRuntime.Cache.Remove("MessExpenditure" + Session["DataBase"].ToString().Trim());
                    //Common objMenu = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
                    //DataSet dsbs = objMenu.FillDropDown("HSL_MESS_EXPENDITURE", "*", "", "", "");
                    //if (dsbs != null && dsbs.Tables[0].Rows.Count > 0)
                    //{
                    //   HttpRuntime.Cache.Insert("MessExpenditure" + Session["DataBase"].ToString().Trim(), dsbs, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

                    //}
                    //Inserting record into the Messbill
                    if ((ViewState["edit"].ToString()) == "add")
                    {
                        objMBM.MESSNO = Convert.ToInt32(ddlMess.SelectedIndex.ToString().Trim());
                        objMBM.BILLDATE = Convert.ToDateTime(txtDate.Text.ToString().Trim());
                        long ret2 = objMBC.MessBillInsert(objMBM, ref Message);
                        if (ret2 <= 0)
                        {
                            if (Message.ToString().Trim() == "")
                            {
                                objCommon.DisplayMessage("Record not saved", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage("Exception occurd", this.Page);
                            }
                        }
                    }
                    objCommon.DisplayMessage("Record Saved successfully!!", this.Page);
                    Clear();
                    //transactionScope.Complete();
                //}

                ViewState["edit"] = "add";
            }
        }
        catch (Exception ee)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeHeads.BindListViewFeesHead-> " + ee.Message + " " + ee.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlMess.SelectedIndex == 0)
        {
           // objCommon.DisplayUserMessage(updpanel, "Please Select Mess", this);
            objCommon.DisplayMessage("Please Select Mess", this.Page);
            ClearListviews();
            return;
        }

        for (int i = 0; i < Lsv1.Items.Count; i++)
        {
            TextBox txtAmt = (TextBox)Lsv1.Items[i].FindControl("txtshead");
            Label lbl = (Label)Lsv1.Items[i].FindControl("lblshead");

            if (txtAmt.Text == string.Empty)
            {
                //objCommon.DisplayUserMessage(updpanel, "Please select the record to print.", this);
                objCommon.DisplayMessage("Please select the record to print", this.Page);
                return;
            }

        }
        //string uRight = GetUserRight();
        //string TwoCharReport = uRight.Substring(4, 2).ToString();
        //if (TwoCharReport == "YR")
        //{
        ShowReport("MONTHLYEXPENDITURE", "MonthlyExpenditureReport.rpt");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(UpdatePanel1, Common.Message.NoReport, this);
        //    return;
        //}
    }

    protected void ddlMess_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDate.Focus();
        GetMonthlyExpenditure();
        //BindLsv();
        for (int i = 0; i < Lsv1.Items.Count; i++)
        {
            TextBox txtAmt = (TextBox)Lsv1.Items[i].FindControl("txtshead");
            txtAmt.Text = "";
        }
        TextBox txttot = (TextBox)Lsv1.FindControl("txttotal");
        txttot.Text = string.Empty;
        txtDate.Text = DateTime.Now.ToString();
        ViewState["edit"] = "add";
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["edit"] = "edit";
            ImageButton btnEdit = sender as ImageButton;
            int trno = int.Parse(btnEdit.CommandArgument);
            GetData(trno);
            ViewState["TRNO"] = trno;
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Exception occured", this.Page);
        }
    }

    protected void hdnval_ValueChanged(object sender, EventArgs e)
    {

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Title = string.Empty;
            DateTime dt = Convert.ToDateTime(txtDate.Text.ToString().Trim());
            Title = "MONTHLY EXPENDITURE FOR" + " " + ddlMess.SelectedItem.Text;
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MonthlyExpenditure.aspx")));

            url += "../Reports/Commonreport.aspx?"; ;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_MESS_NO=" + Convert.ToInt32(ddlMess.SelectedValue.ToString()) + "," + "@P_MONTH=" + Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy") + "," + "@P_COLLEGE_CODE=" + Session["Database"].ToString() + "," + "@P_Session=" + Session["Session"].ToString() + "," + "@P_ReportTitle=" + Title.ToString() + "," + "@P_IPADDRESS=" + Session["IPADDR"].ToString() + "," + "@P_USERID=" + Session["useridname"].ToString();

            Script += "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FeeHeads.BindListViewFeesHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetData(int trno)
    {
        Lsv1.DataSource = null;
        Lsv1.DataBind();
        //DataSet dsstd = objMEC.GetMessHeadsbyMessno_Month(Convert.ToInt32(ddlMess.SelectedValue.ToString().Trim()), Convert.ToDateTime(txtDate.Text.ToString().Trim()));
        DataSet dsstd = objCommon.FillDropDown("ACD_HOSTEL_MESS_EXPENDITURE", "*", "", "TRNO=" + trno, "");
        if (dsstd != null && dsstd.Tables[0].Rows.Count > 0)
        {
            BindListView();
            txtDate.Text = dsstd.Tables[0].Rows[0]["MONTH"].ToString().Trim();
            //ddlMess.SelectedValue = dsstd.Tables[0].Rows[0]["MESS_NO"].ToString().Trim();
            TextBox txttot = (TextBox)Lsv1.FindControl("txttotal");
            txttot.Text = "0";
            for (int i = 0; i < Lsv1.Items.Count; i++)
            {
                TextBox txtAmt = (TextBox)Lsv1.Items[i].FindControl("txtshead");
                txtAmt.Text = "";
                Label lbl = (Label)Lsv1.Items[i].FindControl("lblshead");


                if (txtAmt != null && txttot != null && lbl != null)
                {
                    txtAmt.Text = dsstd.Tables[0].Rows[0][lbl.Text.ToString().Trim()].ToString().Trim();

                    txttot.Text = (Convert.ToDouble(txttot.Text) + Convert.ToDouble(dsstd.Tables[0].Rows[0][lbl.Text.ToString().Trim()].ToString().Trim())).ToString();

                }
                ViewState["Total"] = Convert.ToInt32(txttot.Text);

            }

        }
    }

    private void GetMonthlyExpenditure()
    {
        DataSet ds = new DataSet();
        DateTime month = System.DateTime.MinValue;
        ds = objMEC.GetMessExpenditurebyMessno_Month(Convert.ToInt32(ddlMess.SelectedValue.ToString().Trim()),Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlHostel.SelectedValue));//,month);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Lsv2.DataSource = ds.Tables[0];
            Lsv2.DataBind();
        }
        else
        {
            Lsv2.DataSource = null;
            Lsv2.DataBind();
        }
    }

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMess.SelectedIndex = 0;
        ClearListviews();
        Clear();
    }

    public bool CheckMonth()
    {
        bool result = false;
        DataSet dsMonth = new DataSet();

        DateTime dt = Convert.ToDateTime(txtDate.Text);
        string month = dt.Month.ToString().Trim();
        string year = dt.Year.ToString().Trim();

        dsMonth = objCommon.FillDropDown("ACD_HOSTEL_MESS_EXPENDITURE", "TRNO", "MESS_NO", "SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+" AND MESS_NO='" + ddlMess.SelectedIndex.ToString().Trim() + "' and MONTH([MONTH])='" + month + "' and year([MONTH])='" + year + "'", "");
        if (dsMonth.Tables[0].Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }

    public void Clear()
    {
        //ddlMess.SelectedIndex = 0;
        txtDate.Text = System .DateTime .Now.ToShortDateString ();
        ddlMess.Focus();
        BindListView();
        hdnTotal.Value = "";
        hdnval.Value = "";
        TextBox txttot = (TextBox)Lsv1.FindControl("txttotal");
        txttot.Text = string.Empty;
        //ClearListviews();
        ViewState["edit"] = "add";
        for (int i = 0; i < Lsv1.Items.Count; i++)
        {
            TextBox txtAmt = (TextBox)Lsv1.Items[i].FindControl("txtshead");
            txtAmt.Text = "";
        }
    }

    private void ClearListviews()
    {
        Lsv2.DataSource = null;
        Lsv2.DataBind();
    }
}
