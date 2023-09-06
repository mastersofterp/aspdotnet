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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Web.Caching;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

public partial class Pay_ITDeclaration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();

    ITDeclaration objITDM = new ITDeclaration();
    ITDeclarationController objITDecContr = new ITDeclarationController();
    ITHraCalculateController objITHraContr = new ITHraCalculateController();
    string UsrStatus = string.Empty;
    decimal totalPerquisiteAmt = 0;

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;


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
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

        int empidno = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        //lvEmp.DataBind();
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
                Session["RecTblDocument"] = null;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                txtIntrHousingLoan.Text = "0";
                txtHRA1.Text = "0";
                txtSumPerquisite.Text = "0";
                txtIntrNSC.Text = "0";
                txtOtherIncTDS.Text = "0";
                txtMedical.Text = "0";

                GetOtherIncome();
                GetVIADetails();
                //  GedDedHeadDetail();
                //  GedDedHeadDetail();
                if (ua_type != 1)
                {
                    //FillLoginEmp();
                    FillLoginEmp(empidno);  // Updated on 04-02-2023
                    FillITRules();
                    FillCollege();
                    // Updated on 03-02-2023
                    divOrderBy.Visible = false;
                    ddlOrderBy.Visible = false;
                    //divCollege.Visible = false;
                    ChkSendEmail.Visible = false;
                    btnLockUnlock.Visible = false;
                }
                else
                {
                    objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "FNAME");
                    FillCollege();
                    // FillLoginEmp();
                    FillITRules();
                    // FillCollege();
                    // Updated on 03-02-2023
                    divOrderBy.Visible = true;
                    ddlOrderBy.Visible = true;
                    //  divCollege.Visible = true;
                    ChkSendEmail.Visible = true;
                    btnLockUnlock.Visible = true;
                }

                DateTime fdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITFDATE", ""));
                txtFromDate.Text = fdate.ToString();
                DateTime tdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITTDATE", ""));
                txtToDate.Text = tdate.ToString();

                if (Request.QueryString["id"] != null)
                {
                    int idno = Convert.ToInt32(ddlemp.SelectedValue);
                    //SaveDetails();
                }
            }
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (ddlemp.SelectedIndex > 0)
                {
                    bindlist();
                }
                //if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                //{
                //    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                //    int idNo = Convert.ToInt32(ddlemp.SelectedValue);
                //    int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
                //    DateTime fdate = Convert.ToDateTime(txtFromDate.Text);
                //    DateTime tdate = Convert.ToDateTime(txtToDate.Text);
                //    bindlist(idNo, fdate, tdate, collegeNo);
                //}

                //if (Page.Request.Params["__EVENTTARGET"].ToString().Contains("btnSavePerquisite"))
                //{
                //    SavePerquisite();
                //}
            }
        }

    }

    //protected void FillCollege()
    //{
    //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITDeclaration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITDeclaration.aspx");
        }
    }

    private void bindlist()
    {
        try
        {
            DataTableReader dtr;
            decimal gs = 0;
            dtr = objITDecContr.GetCalculativeValues(txtFromDate.Text, txtToDate.Text, GetEmpId());
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    gs = Convert.ToDecimal(dtr["GS"].ToString());
                }
            }
            else
            {
                gs = 0;
            }

            EmpCreateController objECC = new EmpCreateController();
            //  string collegeno = Session["college_nos"].ToString();
            int idNo = Convert.ToInt32(ddlemp.SelectedValue);
            int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            //  int collegeNo = 0;
            DateTime fdate = Convert.ToDateTime(txtFromDate.Text);
            DateTime tdate = Convert.ToDateTime(txtToDate.Text);
            DataTable dt = objITDecContr.RetrieveNatureofPerquisite(idNo, fdate, tdate, collegeNo, gs);

            lvEmp.DataSource = dt;
            lvEmp.DataBind();
            lvEmp.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.bindlist-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void SearchPerquisite(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        EmpCreateController objECC = new EmpCreateController();
    //        string collegeno = Session["college_nos"].ToString();
    //        int idNo = Convert.ToInt32(ddlemp.SelectedValue);
    //        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
    //        DateTime fdate = Convert.ToDateTime(txtFromDate.Text);
    //        DateTime tdate = Convert.ToDateTime(txtToDate.Text);
    //        DataTable dt = objITDecContr.RetrieveNatureofPerquisite(idNo, fdate, tdate, collegeNo);

    //        lvEmp.DataSource = dt;
    //        lvEmp.DataBind();
    //        lvEmp.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "payroll_empinfo.bindlist-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void CancelPerquisite(object sender, EventArgs e)
    {
        lvEmp.Items.Clear();
        lvEmp.Visible = false;
        //perquisite
        DateTime finyearFdate = Convert.ToDateTime(txtFromDate.Text);
        DateTime finyearTdate = Convert.ToDateTime(txtToDate.Text);

        string finyear = Convert.ToString(finyearFdate.Year) + '-' + Convert.ToString(finyearTdate.Year).Substring(2);

        int count = Convert.ToInt32(objCommon.LookUp("PAYROLL_NATUREOFPERQUISITE", "COUNT(1)", "IDNO=" + ddlemp.SelectedValue + " AND FINYEAR ='" + finyear + "'"));
        if (count > 0)
        {
            totalPerquisiteAmt = Convert.ToDecimal(objCommon.LookUp("PAYROLL_NATUREOFPERQUISITE", "SUM(ISNULL(VALUE5,0))", "IDNO=" + ddlemp.SelectedValue + " AND FINYEAR ='" + finyear + "'"));
        }
        txtSumPerquisite.Text = totalPerquisiteAmt.ToString();
        bindlist();
    }

    //protected void btnSendMail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        EmpCreateController objECC = new EmpCreateController();
    //        string collegeno = Session["college_nos"].ToString();
    //        int idNo = Convert.ToInt32(ddlemp.SelectedValue);
    //        int collegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
    //        DateTime fdate = Convert.ToDateTime(txtFromDate.Text);
    //        DateTime tdate = Convert.ToDateTime(txtToDate.Text);
    //        DataTable dt = objITDecContr.RetrieveNatureofPerquisite(idNo, fdate, tdate, collegeNo);

    //        lvEmp.DataSource = dt;
    //        lvEmp.DataBind();
    //        //lvEmp.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "payroll_empinfo.bindlist-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    //private void SavePerquisite()
    //{
    //    try
    //    {
    //        int count = 0;
    //        //lvEmp.Items.Clear();
    //        foreach (ListViewDataItem lvitem in lvEmp.Items)
    //        {
    //            Label lblPerquisite = lvitem.FindControl("lnkId") as Label;
    //            TextBox txtvalue3 = lvitem.FindControl("txtVALUE3") as TextBox;
    //            TextBox txtvalue4 = lvitem.FindControl("txtVALUE4") as TextBox;
    //            TextBox txtvalue5 = lvitem.FindControl("txtVALUE5") as TextBox;

    //            decimal v3 = Convert.ToDecimal(txtvalue3.Text);

    //            objITDM.PERQUISITE = lblPerquisite.Text;
    //            objITDM.VALUE3 = Convert.ToDecimal(txtvalue3.Text);
    //            objITDM.VALUE4 = Convert.ToDecimal(txtvalue4.Text);
    //            objITDM.VALUE5 = Convert.ToDecimal(txtvalue5.Text);
    //            objITDM.IDNO = Convert.ToInt32(ddlemp.SelectedValue);
    //            objITDM.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
    //            objITDM.TODATE = Convert.ToDateTime(txtToDate.Text);
    //            objITDM.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
    //            //objITDM.COLLEGE_CODE = "24";

    //            CustomStatus cs = (CustomStatus)objITDecContr.AddITNatureofPerquisite(objITDM);
    //            if (cs.Equals(CustomStatus.RecordUpdated))
    //            {
    //                count = 1;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void SavePerquisite(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            //lvEmp.Items.Clear();
            foreach (ListViewDataItem lvitem in lvEmp.Items)
            {
                Label lblPerquisite = lvitem.FindControl("lnkId") as Label;
                TextBox txtvalue3 = lvitem.FindControl("txtVALUE3") as TextBox;
                TextBox txtvalue4 = lvitem.FindControl("txtVALUE4") as TextBox;
                TextBox txtvalue5 = lvitem.FindControl("txtVALUE5") as TextBox;

                //decimal v3 = Convert.ToDecimal(txtvalue3.Text);
                decimal value3 = Convert.ToDecimal(Request.Form[txtvalue3.UniqueID]);
                decimal value4 = Convert.ToDecimal(Request.Form[txtvalue4.UniqueID]);
                decimal value5 = Convert.ToDecimal(Request.Form[txtvalue5.UniqueID]);

                objITDM.PERQUISITE = lblPerquisite.Text;
                objITDM.PERQUISITEID = Convert.ToInt32(txtvalue3.ToolTip);
                objITDM.VALUE3 = value3;
                objITDM.VALUE4 = value4;
                objITDM.VALUE5 = value5;
                objITDM.IDNO = Convert.ToInt32(ddlemp.SelectedValue);
                objITDM.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
                objITDM.TODATE = Convert.ToDateTime(txtToDate.Text);
                objITDM.COLLEGENO = Convert.ToInt32(ddlCollege.SelectedValue);
                //objITDM.COLLEGE_CODE = "24";

                CustomStatus cs = (CustomStatus)objITDecContr.AddITNatureofPerquisite(objITDM);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }
            lvEmp.Items.Clear();
            //lvEmp.Visible = false;

            totalPerquisiteAmt = Convert.ToDecimal(objCommon.LookUp("PAYROLL_NATUREOFPERQUISITE", "SUM(ISNULL(VALUE5,0))", "IDNO=" + ddlemp.SelectedValue + ""));
            txtSumPerquisite.Text = totalPerquisiteAmt.ToString();
            bindlist();
        }
        catch (Exception ex)
        {

        }
    }




    //protected void GetVIADetails()
    //{

    //    DataSet ds = objITMas.GetITHeads();
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtVIAP1.Text = ds.Tables[0].Rows[0]["HEADNAMEFULL"].ToString();

    //            txtVIAP2.Text = ds.Tables[0].Rows[1]["HEADNAMEFULL"].ToString();
    //            txtVIAP3.Text = ds.Tables[0].Rows[2]["HEADNAMEFULL"].ToString();
    //            txtVIAP4.Text = ds.Tables[0].Rows[3]["HEADNAMEFULL"].ToString();
    //            txtVIAP5.Text = ds.Tables[0].Rows[4]["HEADNAMEFULL"].ToString();
    //            txtVIAP6.Text = ds.Tables[0].Rows[5]["HEADNAMEFULL"].ToString();
    //            txtVIAP7.Text = ds.Tables[0].Rows[6]["HEADNAMEFULL"].ToString();
    //            txtVIAP8.Text = ds.Tables[0].Rows[7]["HEADNAMEFULL"].ToString();
    //            txtVIAP9.Text = ds.Tables[0].Rows[8]["HEADNAMEFULL"].ToString();
    //            txtVIAP10.Text = ds.Tables[0].Rows[9]["HEADNAMEFULL"].ToString();
    //            txtVIAP11.Text = ds.Tables[0].Rows[10]["HEADNAMEFULL"].ToString();
    //            txtVIAP12.Text = ds.Tables[0].Rows[11]["HEADNAMEFULL"].ToString();
    //            txtVIAP13.Text = ds.Tables[0].Rows[12]["HEADNAMEFULL"].ToString();

    //            txtVIAP1.ToolTip = txtVIAP1.Text;
    //            txtVIAP2.ToolTip = txtVIAP2.Text;
    //            txtVIAP3.ToolTip = txtVIAP3.Text;
    //            txtVIAP4.ToolTip = txtVIAP4.Text;
    //            txtVIAP5.ToolTip = txtVIAP5.Text;
    //            txtVIAP6.ToolTip = txtVIAP6.Text;
    //            txtVIAP7.ToolTip = txtVIAP7.Text;
    //            txtVIAP8.ToolTip = txtVIAP8.Text;
    //            txtVIAP9.ToolTip = txtVIAP9.Text;
    //            txtVIAP10.ToolTip = txtVIAP10.Text;
    //            txtVIAP11.ToolTip = txtVIAP11.Text;
    //            txtVIAP12.ToolTip = txtVIAP12.Text;
    //            txtVIAP13.ToolTip = txtVIAP13.Text;


    //            txtVIAded1.Text = ds.Tables[0].Rows[0]["SEC"].ToString();
    //            txtVIAded2.Text = ds.Tables[0].Rows[1]["SEC"].ToString();
    //            txtVIAded3.Text = ds.Tables[0].Rows[2]["SEC"].ToString();
    //            txtVIAded4.Text = ds.Tables[0].Rows[3]["SEC"].ToString();
    //            txtVIAded5.Text = ds.Tables[0].Rows[4]["SEC"].ToString();
    //            txtVIAded6.Text = ds.Tables[0].Rows[5]["SEC"].ToString();
    //            txtVIAded7.Text = ds.Tables[0].Rows[6]["SEC"].ToString();
    //            txtVIAded8.Text = ds.Tables[0].Rows[7]["SEC"].ToString();
    //            txtVIAded9.Text = ds.Tables[0].Rows[8]["SEC"].ToString();
    //            txtVIAded10.Text = ds.Tables[0].Rows[9]["SEC"].ToString();
    //            txtVIAded11.Text = ds.Tables[0].Rows[10]["SEC"].ToString();
    //            txtVIAded12.Text = ds.Tables[0].Rows[11]["SEC"].ToString();
    //            txtVIAded13.Text = ds.Tables[0].Rows[12]["SEC"].ToString();

    //            txtVIAded1.ToolTip = txtVIAded1.Text;
    //            txtVIAded2.ToolTip = txtVIAded2.Text;
    //            txtVIAded3.ToolTip = txtVIAded3.Text;
    //            txtVIAded4.ToolTip = txtVIAded4.Text;
    //            txtVIAded5.ToolTip = txtVIAded5.Text;
    //            txtVIAded6.ToolTip = txtVIAded6.Text;
    //            txtVIAded7.ToolTip = txtVIAded7.Text;
    //            txtVIAded8.ToolTip = txtVIAded8.Text;
    //            txtVIAded9.ToolTip = txtVIAded9.Text;
    //            txtVIAded10.ToolTip = txtVIAded10.Text;
    //            txtVIAded11.ToolTip = txtVIAded11.Text;
    //            txtVIAded12.ToolTip = txtVIAded12.Text;
    //            txtVIAded13.ToolTip = txtVIAded13.Text;


    //            txtLimitVI1.Text = ds.Tables[0].Rows[0]["LIMIT"].ToString();
    //            txtLimitVI2.Text = ds.Tables[0].Rows[1]["LIMIT"].ToString();
    //            txtLimitVI3.Text = ds.Tables[0].Rows[2]["LIMIT"].ToString();
    //            txtLimitVI4.Text = ds.Tables[0].Rows[3]["LIMIT"].ToString();
    //            txtLimitVI5.Text = ds.Tables[0].Rows[4]["LIMIT"].ToString();
    //            txtLimitVI6.Text = ds.Tables[0].Rows[5]["LIMIT"].ToString();
    //            txtLimitVI7.Text = ds.Tables[0].Rows[6]["LIMIT"].ToString();
    //            txtLimitVI8.Text = ds.Tables[0].Rows[7]["LIMIT"].ToString();
    //            txtLimitVI9.Text = ds.Tables[0].Rows[8]["LIMIT"].ToString();
    //            txtLimitVI10.Text = ds.Tables[0].Rows[9]["LIMIT"].ToString();
    //            txtLimitVI11.Text = ds.Tables[0].Rows[10]["LIMIT"].ToString();
    //            txtLimitVI12.Text = ds.Tables[0].Rows[11]["LIMIT"].ToString();
    //            txtLimitVI13.Text = ds.Tables[0].Rows[12]["LIMIT"].ToString();

    //            txtLimitVI1.ToolTip = txtLimitVI1.Text;
    //            txtLimitVI2.ToolTip = txtLimitVI2.Text;
    //            txtLimitVI3.ToolTip = txtLimitVI3.Text;
    //            txtLimitVI4.ToolTip = txtLimitVI4.Text;
    //            txtLimitVI5.ToolTip = txtLimitVI5.Text;
    //            txtLimitVI6.ToolTip = txtLimitVI6.Text;
    //            txtLimitVI7.ToolTip = txtLimitVI7.Text;
    //            txtLimitVI8.ToolTip = txtLimitVI8.Text;
    //            txtLimitVI9.ToolTip = txtLimitVI9.Text;
    //            txtLimitVI10.ToolTip = txtLimitVI10.Text;
    //            txtLimitVI11.ToolTip = txtLimitVI11.Text;
    //            txtLimitVI12.ToolTip = txtLimitVI12.Text;
    //            txtLimitVI13.ToolTip = txtLimitVI13.Text;

    //        }
    //    }
    //}






    //to retrive other income heads from table 

    protected void GetVIADetails()
    {

        DataSet ds = objITMas.GetITHeads();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                ////////hdn_CNO_VIA1.Value = ds.Tables[0].Rows[0]["CNO"].ToString();
                ////////hdn_CNO_VIA2.Value = ds.Tables[0].Rows[1]["CNO"].ToString();
                ////////hdn_CNO_VIA3.Value = ds.Tables[0].Rows[2]["CNO"].ToString();
                ////////hdn_CNO_VIA4.Value = ds.Tables[0].Rows[3]["CNO"].ToString();
                ////////hdn_CNO_VIA5.Value = ds.Tables[0].Rows[4]["CNO"].ToString();
                ////////hdn_CNO_VIA6.Value = ds.Tables[0].Rows[5]["CNO"].ToString();
                ////////hdn_CNO_VIA7.Value = ds.Tables[0].Rows[6]["CNO"].ToString();
                ////////hdn_CNO_VIA8.Value = ds.Tables[0].Rows[7]["CNO"].ToString();
                ////////hdn_CNO_VIA9.Value = ds.Tables[0].Rows[8]["CNO"].ToString();
                ////////hdn_CNO_VIA10.Value = ds.Tables[0].Rows[9]["CNO"].ToString();
                ////////hdn_CNO_VIA11.Value = ds.Tables[0].Rows[10]["CNO"].ToString();
                ////////hdn_CNO_VIA12.Value = ds.Tables[0].Rows[11]["CNO"].ToString();
                ////////hdn_CNO_VIA13.Value = ds.Tables[0].Rows[12]["CNO"].ToString();


                //hdnRebate1.Value = ds.Tables[0].Rows[0]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[1]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[2]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[3]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[4]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[5]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[6]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[7]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[8]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[9]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[10]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[11]["CNO"].ToString();
                //hdnRebate1.Value = ds.Tables[0].Rows[12]["CNO"].ToString();




                ////////txtVIAP1.Text = ds.Tables[0].Rows[0]["HEADNAMEFULL"].ToString();
                ////////txtVIAP2.Text = ds.Tables[0].Rows[1]["HEADNAMEFULL"].ToString();
                ////////txtVIAP3.Text = ds.Tables[0].Rows[2]["HEADNAMEFULL"].ToString();
                ////////txtVIAP4.Text = ds.Tables[0].Rows[3]["HEADNAMEFULL"].ToString();
                ////////txtVIAP5.Text = ds.Tables[0].Rows[4]["HEADNAMEFULL"].ToString();
                ////////txtVIAP6.Text = ds.Tables[0].Rows[5]["HEADNAMEFULL"].ToString();
                ////////txtVIAP7.Text = ds.Tables[0].Rows[6]["HEADNAMEFULL"].ToString();
                ////////txtVIAP8.Text = ds.Tables[0].Rows[7]["HEADNAMEFULL"].ToString();
                ////////txtVIAP9.Text = ds.Tables[0].Rows[8]["HEADNAMEFULL"].ToString();
                ////////txtVIAP10.Text = ds.Tables[0].Rows[9]["HEADNAMEFULL"].ToString();
                ////////txtVIAP11.Text = ds.Tables[0].Rows[10]["HEADNAMEFULL"].ToString();
                ////////txtVIAP12.Text = ds.Tables[0].Rows[11]["HEADNAMEFULL"].ToString();
                ////////txtVIAP13.Text = ds.Tables[0].Rows[12]["HEADNAMEFULL"].ToString();

                ////////txtVIAded1.Text = ds.Tables[0].Rows[0]["SEC"].ToString();
                ////////txtVIAded2.Text = ds.Tables[0].Rows[1]["SEC"].ToString();
                ////////txtVIAded3.Text = ds.Tables[0].Rows[2]["SEC"].ToString();
                ////////txtVIAded4.Text = ds.Tables[0].Rows[3]["SEC"].ToString();
                ////////txtVIAded5.Text = ds.Tables[0].Rows[4]["SEC"].ToString();
                ////////txtVIAded6.Text = ds.Tables[0].Rows[5]["SEC"].ToString();
                ////////txtVIAded7.Text = ds.Tables[0].Rows[6]["SEC"].ToString();
                ////////txtVIAded8.Text = ds.Tables[0].Rows[7]["SEC"].ToString();
                ////////txtVIAded9.Text = ds.Tables[0].Rows[8]["SEC"].ToString();
                ////////txtVIAded10.Text = ds.Tables[0].Rows[9]["SEC"].ToString();
                ////////txtVIAded11.Text = ds.Tables[0].Rows[10]["SEC"].ToString();
                ////////txtVIAded12.Text = ds.Tables[0].Rows[11]["SEC"].ToString();
                ////////txtVIAded13.Text = ds.Tables[0].Rows[12]["SEC"].ToString();

                ////////txtLimitVI1.Text = ds.Tables[0].Rows[0]["LIMIT"].ToString();
                ////////txtLimitVI2.Text = ds.Tables[0].Rows[1]["LIMIT"].ToString();
                ////////txtLimitVI3.Text = ds.Tables[0].Rows[2]["LIMIT"].ToString();
                ////////txtLimitVI4.Text = ds.Tables[0].Rows[3]["LIMIT"].ToString();
                ////////txtLimitVI5.Text = ds.Tables[0].Rows[4]["LIMIT"].ToString();
                ////////txtLimitVI6.Text = ds.Tables[0].Rows[5]["LIMIT"].ToString();
                ////////txtLimitVI7.Text = ds.Tables[0].Rows[6]["LIMIT"].ToString();
                ////////txtLimitVI8.Text = ds.Tables[0].Rows[7]["LIMIT"].ToString();
                ////////txtLimitVI9.Text = ds.Tables[0].Rows[8]["LIMIT"].ToString();
                ////////txtLimitVI10.Text = ds.Tables[0].Rows[9]["LIMIT"].ToString();
                ////////txtLimitVI11.Text = ds.Tables[0].Rows[10]["LIMIT"].ToString();
                ////////txtLimitVI12.Text = ds.Tables[0].Rows[11]["LIMIT"].ToString();
                ////////txtLimitVI13.Text = ds.Tables[0].Rows[12]["LIMIT"].ToString();

                ////////chkLimit1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISPERCENTAGE"]);
                ////////chkLimit2.Checked = Convert.ToBoolean(ds.Tables[0].Rows[1]["ISPERCENTAGE"]);
                ////////chkLimit3.Checked = Convert.ToBoolean(ds.Tables[0].Rows[2]["ISPERCENTAGE"]);
                ////////chkLimit4.Checked = Convert.ToBoolean(ds.Tables[0].Rows[3]["ISPERCENTAGE"]);
                ////////chkLimit5.Checked = Convert.ToBoolean(ds.Tables[0].Rows[4]["ISPERCENTAGE"]);
                ////////chkLimit6.Checked = Convert.ToBoolean(ds.Tables[0].Rows[5]["ISPERCENTAGE"]);
                ////////chkLimit7.Checked = Convert.ToBoolean(ds.Tables[0].Rows[6]["ISPERCENTAGE"]);
                ////////chkLimit8.Checked = Convert.ToBoolean(ds.Tables[0].Rows[7]["ISPERCENTAGE"]);
                ////////chkLimit9.Checked = Convert.ToBoolean(ds.Tables[0].Rows[8]["ISPERCENTAGE"]);
                ////////chkLimit10.Checked = Convert.ToBoolean(ds.Tables[0].Rows[9]["ISPERCENTAGE"]);
                ////////chkLimit11.Checked = Convert.ToBoolean(ds.Tables[0].Rows[10]["ISPERCENTAGE"]);
                ////////chkLimit12.Checked = Convert.ToBoolean(ds.Tables[0].Rows[11]["ISPERCENTAGE"]);
                ////////chkLimit13.Checked = Convert.ToBoolean(ds.Tables[0].Rows[12]["ISPERCENTAGE"]);


                ////////lblLimit1.Text = ds.Tables[0].Rows[0]["ISPERCENT"].ToString();
                ////////lblLimit2.Text = ds.Tables[0].Rows[1]["ISPERCENT"].ToString();
                ////////lblLimit3.Text = ds.Tables[0].Rows[2]["ISPERCENT"].ToString();
                ////////lblLimit4.Text = ds.Tables[0].Rows[3]["ISPERCENT"].ToString();
                ////////lblLimit5.Text = ds.Tables[0].Rows[4]["ISPERCENT"].ToString();
                ////////lblLimit6.Text = ds.Tables[0].Rows[5]["ISPERCENT"].ToString();
                ////////lblLimit7.Text = ds.Tables[0].Rows[6]["ISPERCENT"].ToString();
                ////////lblLimit8.Text = ds.Tables[0].Rows[7]["ISPERCENT"].ToString();
                ////////lblLimit9.Text = ds.Tables[0].Rows[8]["ISPERCENT"].ToString();
                ////////lblLimit10.Text = ds.Tables[0].Rows[9]["ISPERCENT"].ToString();
                ////////lblLimit11.Text = ds.Tables[0].Rows[10]["ISPERCENT"].ToString();
                ////////lblLimit12.Text = ds.Tables[0].Rows[11]["ISPERCENT"].ToString();
                ////////lblLimit13.Text = ds.Tables[0].Rows[12]["ISPERCENT"].ToString();
            }
        }
    }
    protected void GedDedHeadDetail()
    {
        try
        {
            DataSet ds = objITMas.GetITDeductionHeads();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ////////hdnRebate1.Value = ds.Tables[0].Rows[0]["FNO"].ToString();
                ////////hdnRebate2.Value = ds.Tables[0].Rows[1]["FNO"].ToString();
                ////////hdnRebate3.Value = ds.Tables[0].Rows[2]["FNO"].ToString();
                ////////hdnRebate4.Value = ds.Tables[0].Rows[3]["FNO"].ToString();
                ////////hdnRebate5.Value = ds.Tables[0].Rows[4]["FNO"].ToString();

                ////////hdnRebate6.Value = ds.Tables[0].Rows[5]["FNO"].ToString();
                ////////hdnRebate7.Value = ds.Tables[0].Rows[6]["FNO"].ToString();
                ////////hdnRebate8.Value = ds.Tables[0].Rows[7]["FNO"].ToString();
                ////////hdnRebate9.Value = ds.Tables[0].Rows[8]["FNO"].ToString();
                ////////hdnRebate10.Value = ds.Tables[0].Rows[9]["FNO"].ToString();

                ////////hdnRebate11.Value = ds.Tables[0].Rows[10]["FNO"].ToString();
                ////////hdnRebate12.Value = ds.Tables[0].Rows[11]["FNO"].ToString();
                ////////hdnRebate13.Value = ds.Tables[0].Rows[12]["FNO"].ToString();
                ////////hdnRebate14.Value = ds.Tables[0].Rows[13]["FNO"].ToString();
                ////////hdnRebate15.Value = ds.Tables[0].Rows[14]["FNO"].ToString();

                ////////hdnRebate16.Value = ds.Tables[0].Rows[15]["FNO"].ToString();
                ////////hdnRebate17.Value = ds.Tables[0].Rows[16]["FNO"].ToString();
                ////////hdnRebate18.Value = ds.Tables[0].Rows[17]["FNO"].ToString();
                ////////hdnRebate19.Value = ds.Tables[0].Rows[18]["FNO"].ToString();
                ////////hdnRebate20.Value = ds.Tables[0].Rows[19]["FNO"].ToString();
                lvVIARebate.DataSource = ds;
                lvVIARebate.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GetOtherIncome()
    {
        DataSet ds = objITMas.GetOtherIncomeHead();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtINTRP1.Text = ds.Tables[0].Rows[0]["HEAD"].ToString();
                txtINTRP2.Text = ds.Tables[0].Rows[1]["HEAD"].ToString();
                txtINTRP3.Text = ds.Tables[0].Rows[2]["HEAD"].ToString();
                txtINTRP4.Text = ds.Tables[0].Rows[3]["HEAD"].ToString();
                txtINTRP5.Text = ds.Tables[0].Rows[4]["HEAD"].ToString();

            }
        }
    }

    protected void ShowEmpInfo(int idno)
    {
        try
        {

            DateTime dt1;
            dt1 = Convert.ToDateTime(calToDate.SelectedDate);

            DataTableReader dtr = objITDecContr.ShowEmpDetails(idno);
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblName.Text = dtr["NAME"].ToString();
                    lblDesignation.Text = dtr["SUBDESIG"].ToString();
                    lblDepartment.Text = dtr["SUBDEPT"].ToString();
                    lblStaff.Text = dtr["STAFFTYPE"].ToString();
                    txtPANNO.Text = dtr["PAN_NO"].ToString();
                    txtPhoneNo.Text = dtr["PHONENO"].ToString();
                    txtEmailID.Text = dtr["EMAILID"].ToString();
                    //////// lblUptoHead.Text = "Upto " + txtToDate.Text.ToString().Trim();
                }
                dtr.Close();
            }
            divITRuleName.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

    }

    protected int GetEmpId()
    {
        int id;
        string emp = string.Empty;

        //emp = hfEmpName.Value.Substring(hfEmpName.Value.IndexOf('*') + 1);
        emp = ddlemp.SelectedValue;
        id = Convert.ToInt32(emp);

        return id;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ITDeclaration objITDec = new ITDeclaration();
        ITHRACALCULATE objITHra = new ITHRACALCULATE();
        if (!txtFromDate.Text.Trim().Equals(string.Empty))
        {
            objITDec.FROMDATE = Convert.ToDateTime(txtFromDate.Text.Trim());
        }
        if (!txtToDate.Text.Trim().Equals(string.Empty))
        {
            objITDec.TODATE = Convert.ToDateTime(txtToDate.Text.Trim());
        }
        objITDec.IDNO = GetEmpId();

        ////////if (viamt1 > limit1)
        ////////{
        ////////    if (limit1 == 0)
        ////////    {
        ////////        if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = Convert.ToDecimal(txtVIAAm1.Text);
        ////////    }
        ////////    else if (percentage1 == true)
        ////////    {
        ////////        //if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = (Convert.ToDecimal(txtVIAAm1.Text)*Convert.ToDecimal(txtLimitVI1.Text))/100;
        ////////        if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = Convert.ToInt32((Convert.ToDecimal(txtActualAmtVI1.Text) * Convert.ToDecimal(txtLimitVI1.Text)) / 100);
        ////////    }
        ////////    else
        ////////    {
        ////////        if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = Convert.ToDecimal(txtLimitVI1.Text);
        ////////    }
        ////////}
        ////////else
        ////////{
        ////////    if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = Convert.ToDecimal(txtVIAAm1.Text);
        ////////}

        ////////if (viamt2 > limit2)
        ////////{
        ////////    if (limit2 == 0)
        ////////    {
        ////////        if (!txtVIAAm2.Text.Trim().Equals(string.Empty)) objITDec.VIAMT2 = Convert.ToDecimal(txtVIAAm2.Text);
        ////////    }
        ////////    else if (percentage2 == true)
        ////////    {
        ////////        //if (!txtVIAAm1.Text.Trim().Equals(string.Empty)) objITDec.VIAMT1 = (Convert.ToDecimal(txtVIAAm1.Text)*Convert.ToDecimal(txtLimitVI1.Text))/100;
        ////////        if (!txtVIAAm2.Text.Trim().Equals(string.Empty)) objITDec.VIAMT2 = Convert.ToInt32((Convert.ToDecimal(txtActualAmtVI2.Text) * Convert.ToDecimal(txtLimitVI2.Text)) / 100);
        ////////    }
        ////////    else
        ////////    {
        ////////        if (!txtVIAAm2.Text.Trim().Equals(string.Empty)) objITDec.VIAMT2 = Convert.ToDecimal(txtLimitVI2.Text);
        ////////    }
        ////////}
        ////////else
        ////////{
        ////////    if (!txtVIAAm2.Text.Trim().Equals(string.Empty)) objITDec.VIAMT2 = Convert.ToDecimal(txtVIAAm2.Text);
        ////////}







        ////////if (!lblTotBalAmt.Text.Trim().Equals(string.Empty)) objITDec.BALAMT = Convert.ToDecimal(lblTotBalAmt.Text.Trim());
        objITDec.INCP1 = txtINTRP1.Text.Trim();
        objITDec.INCP2 = txtINTRP2.Text.Trim();
        objITDec.INCP3 = txtINTRP3.Text.Trim();
        objITDec.INCP4 = txtINTRP4.Text.Trim();
        objITDec.INCP5 = txtINTRP5.Text.Trim();

        objITDec.INCAMT1 = txtINTRAm1.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtINTRAm1.Text);
        objITDec.INCAMT2 = txtINTRAm2.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtINTRAm2.Text);
        objITDec.INCAMT3 = txtINTRAm3.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtINTRAm3.Text);
        objITDec.INCAMT4 = txtINTRAm4.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtINTRAm4.Text);
        objITDec.INCAMT5 = txtINTRAm5.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtINTRAm5.Text);
        if (!txtDate.Text.Trim().Equals(string.Empty))
        {
            objITDec.INCDATE = Convert.ToDateTime(txtDate.Text.Trim());
        }
        objITDec.REMARK = txtRemark.Text.Trim();
        objITDec.COLLEGE_CODE = Session["colcode"].ToString();

        //HRA VALUE

        decimal HRAVALUE = Convert.ToDecimal(txtHRA1.Text);
        decimal INTRHOUSELOAN = Convert.ToDecimal(txtIntrHousingLoan.Text);
        decimal NSCAMT = Convert.ToDecimal(txtIntrNSC.Text);
        objITDec.OTHERINCTDS = Convert.ToDecimal(txtOtherIncTDS.Text);

        objITHra.IDNO = GetEmpId();
        objITHra.VANO = 0;

        DateTime fromyear = Convert.ToDateTime(txtFromDate.Text);
        string fyear = fromyear.Year.ToString();
        DateTime toyear = Convert.ToDateTime(txtToDate.Text);
        string tyear = toyear.Year.ToString();
        objITHra.FINYEAR = fyear + tyear;
        //Chapter VA Limit End
        objITDec.SUMPERQUISITE = Convert.ToDecimal(txtSumPerquisite.Text);

        if (!txt80CCDNPS.Text.Trim().Equals(string.Empty)) objITDec.CCDNPS = Convert.ToDecimal(txt80CCDNPS.Text);
        if (!txtRGRSS80CCG.Text.Trim().Equals(string.Empty)) objITDec.RGESS80CCG = Convert.ToDecimal(txtRGRSS80CCG.Text);


        if (ChkIsLock.Checked == true)
        {
            objITDec.IsLock = true;
        }
        else
        {
            objITDec.IsLock = false;
        }

        objITDec.IT_RULE_ID = Convert.ToInt32(ddlITRule.SelectedValue);

        DataTable dtVIAHeads = new DataTable("dtVIAHeads");

        dtVIAHeads.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dtVIAHeads.Columns.Add(new DataColumn("CNO", typeof(int)));
        dtVIAHeads.Columns.Add(new DataColumn("FINYEAR", typeof(string)));
        dtVIAHeads.Columns.Add(new DataColumn("HEADNAMEFULL", typeof(string)));
        dtVIAHeads.Columns.Add(new DataColumn("SEC", typeof(string)));
        dtVIAHeads.Columns.Add(new DataColumn("ACTUAL_AMOUNT", typeof(double)));
        dtVIAHeads.Columns.Add(new DataColumn("LIMIT", typeof(double)));
        dtVIAHeads.Columns.Add(new DataColumn("ISPERCENTAGE", typeof(int)));
        dtVIAHeads.Columns.Add(new DataColumn("AMOUNT", typeof(double)));



        DataRow dr = null;
        foreach (ListViewItem i in lvVIAHeads.Items)
        {
            HiddenField hdnCNO = (HiddenField)i.FindControl("hdnCNO");
            Label lblHeadFullName = (Label)i.FindControl("lblHeadFullName");
            Label lblSec = (Label)i.FindControl("lblSec");
            TextBox txtActualAmount = (TextBox)i.FindControl("txtActualAmount");
            Label lblLimit = (Label)i.FindControl("lblLimit");
            HiddenField hdIsPercentage = (HiddenField)i.FindControl("hdnIsPercentage");
            TextBox txtAmount = (TextBox)i.FindControl("txtAmount");

            dr = dtVIAHeads.NewRow();
            dr["IDNO"] = Convert.ToInt32(ddlemp.SelectedValue);
            dr["CNO"] = hdnCNO.Value;
            dr["FINYEAR"] = objITHra.FINYEAR;
            dr["HEADNAMEFULL"] = lblHeadFullName.Text;
            dr["SEC"] = lblSec.Text;

            if (txtActualAmount.Text == string.Empty)
            {
                dr["ACTUAL_AMOUNT"] = 0.00;
            }
            else
            {
                dr["ACTUAL_AMOUNT"] = Convert.ToDouble(txtActualAmount.Text);
            }

            if (lblLimit.Text == string.Empty)
            {
                dr["LIMIT"] = 0.00;
            }
            else
            {
                dr["LIMIT"] = Convert.ToDouble(lblLimit.Text); ;
            }

            if (hdIsPercentage.Value == "True")
            {
                dr["ISPERCENTAGE"] = 1;
            }
            else
            {
                dr["ISPERCENTAGE"] = 0;
            }

            //if (txtAmount.Text == string.Empty)
            //{
            //    dr["AMOUNT"] = 0.00;
            //}
            //else
            //{
            //    dr["AMOUNT"] = Convert.ToDouble(txtAmount.Text); ;
            //}




            if (Convert.ToDouble(txtAmount.Text) > Convert.ToDouble(lblLimit.Text))
            {
                if (Convert.ToDouble(lblLimit.Text) == 0)
                {
                    if (!txtActualAmount.Text.Trim().Equals(string.Empty))
                    {
                        dr["AMOUNT"] = Convert.ToDouble(txtActualAmount.Text);
                        // dr["AMOUNT"] = Convert.ToDouble(txtAmount.Text);
                    }
                }
                else if (hdIsPercentage.Value == "True")
                {
                    if (!txtActualAmount.Text.Trim().Equals(string.Empty))
                    {
                        dr["AMOUNT"] = Convert.ToDouble((Convert.ToInt32(txtActualAmount.Text) * Convert.ToDouble(lblLimit.Text)) / 100);
                        //   dr["AMOUNT"] = Convert.ToDouble((Convert.ToInt32(txtAmount.Text) * Convert.ToDouble(lblLimit.Text)) / 100);
                    }
                }
                else
                {
                    if (!txtActualAmount.Text.Trim().Equals(string.Empty))
                    {
                        dr["AMOUNT"] = Convert.ToDouble(lblLimit.Text);
                    }
                }
            }
            else
            {
                if (!txtActualAmount.Text.Trim().Equals(string.Empty))
                {
                    dr["AMOUNT"] = Convert.ToDouble(txtAmount.Text);
                }
            }





            dtVIAHeads.Rows.Add(dr);
        }

        //=========================== Get IT Rebate Heads =============================
        DataTable dtRebateHeads = new DataTable("dtRebateHeads");

        dtRebateHeads.Columns.Add(new DataColumn("IDNO", typeof(int)));
        dtRebateHeads.Columns.Add(new DataColumn("FNO", typeof(int)));
        dtRebateHeads.Columns.Add(new DataColumn("FINYEAR", typeof(string)));
        dtRebateHeads.Columns.Add(new DataColumn("PAYHEAD", typeof(string)));
        dtRebateHeads.Columns.Add(new DataColumn("VALUE", typeof(double)));

        DataRow drR = null;
        foreach (ListViewItem i in lvVIARebate.Items)
        {
            HiddenField hdnFNO = (HiddenField)i.FindControl("hdnFNO");
            Label lblPayHead = (Label)i.FindControl("lblPayHead");
            TextBox txtValue = (TextBox)i.FindControl("txtValue");


            drR = dtRebateHeads.NewRow();
            drR["IDNO"] = Convert.ToInt32(ddlemp.SelectedValue);
            drR["FNO"] = hdnFNO.Value;
            drR["FINYEAR"] = objITHra.FINYEAR;
            drR["PAYHEAD"] = lblPayHead.Text;

            if (txtValue.Text == string.Empty)
            {
                drR["VALUE"] = 0.00;
            }
            else
            {
                drR["VALUE"] = Convert.ToDouble(txtValue.Text);
            }

            dtRebateHeads.Rows.Add(drR);
        }



        objITDec.VIA_HEADS_Table = dtVIAHeads;
        objITDec.FINYEAR = fyear + tyear;
        objITDec.REBATE_HEADS_Table = dtRebateHeads;
        CustomStatus cs = (CustomStatus)objITDecContr.AddITEmplyeeInfo(objITDec, HRAVALUE, INTRHOUSELOAN, NSCAMT);

        //CHAPTER VA LIMIT
        //CustomStatus cs2 = (CustomStatus)objITHraContr.AddChapVILimit(objITHra);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            Showmessage("Record Saved Successfully.");
            CheckITDeclaration();


        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            Showmessage("Record Updated Successfully.");
            CheckITDeclaration();

        }
        else
        {
            Showmessage("Exception Occured");
        }

        decimal totalIntr = Convert.ToDecimal(txtINTRAm1.Text) + Convert.ToDecimal(txtINTRAm2.Text) + Convert.ToDecimal(txtINTRAm3.Text) + Convert.ToDecimal(txtINTRAm4.Text) + Convert.ToDecimal(txtINTRAm5.Text);
        txtINTRTotal.Text = totalIntr.ToString();
        clear();
    }
    //to populate message Box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void PopulateDetails(object sender, EventArgs e)
    {
        try
        {
            DataTableReader dtr;
            //dtr = objITDecContr.GetTotalGross(txtFromDate.Text, txtToDate.Text, GetEmpId());
            //if (dtr != null)
            //{
            //    if (dtr.Read())
            //    {
            //        txttotalgross.Text = dtr["GS"].ToString();
            //    }
            //}
            //dtr.Dispose();
            dtr = objITDecContr.GetCalculativeValues(txtFromDate.Text, txtToDate.Text, GetEmpId());
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtSalHRA.Text = dtr["HRA"].ToString();


                    //lblParticular1.Text = dtr["PAYHEAD1"].ToString();
                    //lblParticular2.Text = dtr["PAYHEAD2"].ToString();
                    //lblParticular3.Text = dtr["PAYHEAD3"].ToString();
                    //lblParticular4.Text = dtr["PAYHEAD4"].ToString();
                    //lblParticular5.Text = dtr["PAYHEAD5"].ToString();
                    //lblParticular6.Text = dtr["PAYHEAD6"].ToString();
                    //lblParticular7.Text = dtr["PAYHEAD7"].ToString();
                    //lblParticular8.Text = dtr["PAYHEAD8"].ToString();
                    //lblParticular9.Text = dtr["PAYHEAD9"].ToString();
                    //lblParticular10.Text = dtr["PAYHEAD10"].ToString();
                    //lblParticular11.Text = dtr["PAYHEAD11"].ToString();
                    //lblParticular12.Text = dtr["PAYHEAD12"].ToString();
                    //lblParticular13.Text = dtr["PAYHEAD13"].ToString();
                    //lblParticular14.Text = dtr["PAYHEAD14"].ToString();
                    //lblParticular15.Text = dtr["PAYHEAD15"].ToString();
                    //lblParticular16.Text = dtr["PAYHEAD16"].ToString();
                    //lblParticular17.Text = dtr["PAYHEAD17"].ToString();
                    //lblParticular18.Text = dtr["PAYHEAD18"].ToString();
                    //lblParticular19.Text = dtr["PAYHEAD19"].ToString();
                    //lblParticular20.Text = dtr["PAYHEAD20"].ToString();
                    //txtTotal1.Text = dtr["VALUE1"].ToString();
                    //txtTotal2.Text = dtr["VALUE2"].ToString();
                    //txtTotal3.Text = dtr["VALUE3"].ToString();
                    //txtTotal4.Text = dtr["VALUE4"].ToString();
                    //txtTotal5.Text = dtr["VALUE5"].ToString();
                    //txtTotal6.Text = dtr["VALUE6"].ToString();
                    //txtTotal7.Text = dtr["VALUE7"].ToString();
                    //txtTotal8.Text = dtr["VALUE8"].ToString();
                    //txtTotal9.Text = dtr["VALUE9"].ToString();
                    //txtTotal10.Text = dtr["VALUE10"].ToString();
                    //txtTotal11.Text = dtr["VALUE11"].ToString();
                    //txtTotal12.Text = dtr["VALUE12"].ToString();
                    //txtTotal13.Text = dtr["VALUE13"].ToString();
                    //txtTotal14.Text = dtr["VALUE14"].ToString();
                    //txtTotal15.Text = dtr["VALUE15"].ToString();
                    //txtTotal16.Text = dtr["VALUE16"].ToString();
                    //txtTotal17.Text = dtr["VALUE17"].ToString();
                    //txtTotal18.Text = dtr["VALUE18"].ToString();
                    //txtTotal19.Text = dtr["VALUE19"].ToString();
                    //txtTotal20.Text = dtr["VALUE20"].ToString();
                    //txtUpto1.Text = dtr["VALUE1"].ToString();
                    //txtUpto2.Text = dtr["VALUE2"].ToString();
                    //txtUpto3.Text = dtr["VALUE3"].ToString();
                    //txtUpto4.Text = dtr["VALUE4"].ToString();
                    //txtUpto5.Text = dtr["VALUE5"].ToString();
                    //txtUpto6.Text = dtr["VALUE6"].ToString();
                    //txtUpto7.Text = dtr["VALUE7"].ToString();
                    //txtUpto8.Text = dtr["VALUE8"].ToString();
                    //txtUpto9.Text = dtr["VALUE9"].ToString();
                    //txtUpto10.Text = dtr["VALUE10"].ToString();
                    //txtUpto11.Text = dtr["VALUE11"].ToString();
                    //txtUpto12.Text = dtr["VALUE12"].ToString();
                    //txtUpto13.Text = dtr["VALUE13"].ToString();
                    //txtUpto14.Text = dtr["VALUE14"].ToString();
                    //txtUpto15.Text = dtr["VALUE15"].ToString();
                    //txtUpto16.Text = dtr["VALUE16"].ToString();
                    //txtUpto17.Text = dtr["VALUE17"].ToString();
                    //txtUpto18.Text = dtr["VALUE18"].ToString();
                    //txtUpto19.Text = dtr["VALUE19"].ToString();
                    //txtUpto20.Text = dtr["VALUE20"].ToString();
                    txtActualHra.Text = dtr["HRA"].ToString();
                    //int hra = Convert.ToInt32 (dtr["HRA"].ToString());
                    txtpay.Text = dtr["PAY"].ToString();

                    // ViewState["DA"] = dtr["DA"].ToString();
                    txtDA.Text = dtr["DA"].ToString();

                    //Gross
                    txtGross.Text = dtr["GS"].ToString();

                    //Paid Rent Amount
                    string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd")));
                    //Fdate = Convert.ToDateTime(txtMonthYear.Text).ToString("MMMyyyy");
                    Fdate = Fdate.Substring(0, 10);
                    string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd")));
                    Tdate = Tdate.Substring(0, 10);
                    //decimal paidRent = Convert.ToDecimal(objCommon.LookUp("PAYROLL_PAIDRENT","ISNULL(SUM(AMOUNT),0)","IDNO="+ddlemp.SelectedValue+" AND YEAR(FinYearFromDate)=YEAR('"+Fdate+"') AND YEAR(FinYearToDate)=YEAR('"+Tdate+"')"));
                    //txtPaidRent.Text =Convert.ToString(paidRent);
                    txtROPHra.Text = "0";
                    decimal ahra = Convert.ToDecimal(txtActualHra.Text);
                    decimal rophra = Convert.ToDecimal(txtROPHra.Text);
                    decimal hraReceived = ahra + rophra;
                    txtHRAReceived.Text = hraReceived.ToString();
                    //PAID RENT END
                    CalculateBalance(sender, e);
                    GetDetailsFromDynamicTable(sender, e);


                }
                // BIND Rebeate Heads Value
                // DataSet dsRebate = objITDecContr.GetRebateHeadsToBind(txtFromDate.Text, txtToDate.Text, GetEmpId());

                //chap VI LIMIT
                //ITHRACALCULATE objITHra = new ITHRACALCULATE();
                //DateTime fromyear = Convert.ToDateTime(txtFromDate.Text);
                //string fyear = fromyear.Year.ToString();
                //DateTime toyear = Convert.ToDateTime(txtToDate.Text);
                //string tyear = toyear.Year.ToString();
                //objITHra.FINYEAR = fyear + tyear;
                //DataSet dsRebate = objITDecContr.GetRebateHeadsToBind(GetEmpId(), objITHra.FINYEAR);
                //if (dsRebate.Tables[0].Rows.Count > 0)
                //{
                //    lvVIARebate.DataSource = dsRebate;
                //    lvVIARebate.DataBind();
                //}
                //else
                //{
                //    GedDedHeadDetail();
                //}
                DataSet dsRebate = objITDecContr.GetRebateHeadsToBindNew(txtFromDate.Text, txtToDate.Text, GetEmpId());
                if (dsRebate.Tables[0].Rows.Count > 0)
                {
                    lvVIARebate.DataSource = dsRebate;
                    lvVIARebate.DataBind();
                }
                else
                {
                    GedDedHeadDetail();
                }
            }
            dtr.Dispose();
            // BIND Chapter VI  Heads Value
            dtr = objITDecContr.GetCHAPVIValues(txtFromDate.Text, txtToDate.Text, GetEmpId());
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    ////////txtVIAP1.Text = dtr["PAYHEAD1"].ToString();
                    ////////txtVIAAm1.ToolTip = txtVIAP1.Text;
                    ////////txtVIAP2.Text = dtr["PAYHEAD2"].ToString();
                    ////////txtVIAAm2.ToolTip = txtVIAP2.Text;
                    ////////txtVIAP3.Text = dtr["PAYHEAD3"].ToString();
                    ////////txtVIAAm3.ToolTip = txtVIAP3.Text;
                    ////////txtVIAP4.Text = dtr["PAYHEAD4"].ToString();
                    ////////txtVIAAm4.ToolTip = txtVIAP4.Text;
                    ////////txtVIAP5.Text = dtr["PAYHEAD5"].ToString();
                    ////////txtVIAAm5.ToolTip = txtVIAP5.Text;
                    ////////txtVIAP6.Text = dtr["PAYHEAD6"].ToString();
                    ////////txtVIAAm6.ToolTip = txtVIAP6.Text;
                    ////////txtVIAP7.Text = dtr["PAYHEAD7"].ToString();
                    ////////txtVIAAm7.ToolTip = txtVIAP7.Text;
                    ////////txtVIAP8.Text = dtr["PAYHEAD8"].ToString();
                    ////////txtVIAAm8.ToolTip = txtVIAP8.Text;
                    ////////txtVIAP9.Text = dtr["PAYHEAD9"].ToString();
                    ////////txtVIAAm9.ToolTip = txtVIAP9.Text;
                    ////////txtVIAP10.Text = dtr["PAYHEAD10"].ToString();
                    ////////txtVIAAm10.ToolTip = txtVIAP10.Text;
                    ////////txtVIAP11.Text = dtr["PAYHEAD11"].ToString();
                    ////////txtVIAAm11.ToolTip = txtVIAP11.Text;
                    ////////txtVIAP12.Text = dtr["PAYHEAD12"].ToString();
                    ////////txtVIAAm12.ToolTip = txtVIAP12.Text;
                    ////////txtVIAP13.Text = dtr["PAYHEAD13"].ToString();
                    ////////txtVIAAm13.ToolTip = txtVIAP13.Text;

                    ////////txtVIAded1.Text = dtr["SEC1"].ToString();
                    ////////txtVIAded2.Text = dtr["SEC2"].ToString();
                    ////////txtVIAded3.Text = dtr["SEC3"].ToString();
                    ////////txtVIAded4.Text = dtr["SEC4"].ToString();
                    ////////txtVIAded5.Text = dtr["SEC5"].ToString();
                    ////////txtVIAded6.Text = dtr["SEC6"].ToString();
                    ////////txtVIAded7.Text = dtr["SEC7"].ToString();
                    ////////txtVIAded8.Text = dtr["SEC8"].ToString();
                    ////////txtVIAded9.Text = dtr["SEC9"].ToString();
                    ////////txtVIAded10.Text = dtr["SEC10"].ToString();
                    ////////txtVIAded11.Text = dtr["SEC11"].ToString();
                    ////////txtVIAded12.Text = dtr["SEC12"].ToString();
                    ////////txtVIAded13.Text = dtr["SEC13"].ToString();

                    ////////txtVIAAm1.Text = dtr["VALUE1"].ToString();
                    ////////txtVIAAm2.Text = dtr["VALUE2"].ToString();
                    ////////txtVIAAm3.Text = dtr["VALUE3"].ToString();
                    ////////txtVIAAm4.Text = dtr["VALUE4"].ToString();
                    ////////txtVIAAm5.Text = dtr["VALUE5"].ToString();
                    ////////txtVIAAm6.Text = dtr["VALUE6"].ToString();
                    ////////txtVIAAm7.Text = dtr["VALUE7"].ToString();
                    ////////txtVIAAm8.Text = dtr["VALUE8"].ToString();
                    ////////txtVIAAm9.Text = dtr["VALUE9"].ToString();
                    ////////txtVIAAm10.Text = dtr["VALUE10"].ToString();
                    ////////txtVIAAm11.Text = dtr["VALUE11"].ToString();
                    ////////txtVIAAm12.Text = dtr["VALUE12"].ToString();
                    ////////txtVIAAm13.Text = dtr["VALUE13"].ToString();
                    UpdateCHAPVIATotals(sender, e);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void txtEmpName_TextChanged(object sender, EventArgs e)
    {
        //hfEmpName.Value = txtEmpName.Text;
        if (hfEmpName.Value.Trim() != "")
        {
            //ShowEmpInfo(Convert.ToInt32(txtEmpName.Text.Substring(txtEmpName.Text.IndexOf('*') + 1)));
            ShowEmpInfo(Convert.ToInt32(hfEmpName.Value.Substring(hfEmpName.Value.IndexOf('*') + 1)));
            PopulateDetails(sender, e);
        }
        else
        {
            string a = txtEmpName.Text;

            txtEmpName.Text = a;
        }
    }
    protected void CalculateBalance(object sender, EventArgs e)
    {
        try
        {
            ////////lblBal1.Text = (Convert.ToDouble(txtTotal1.Text) - Convert.ToDouble(txtUpto1.Text)).ToString();
            ////////lblBal2.Text = (Convert.ToDouble(txtTotal2.Text) - Convert.ToDouble(txtUpto2.Text)).ToString();
            ////////lblBal3.Text = (Convert.ToDouble(txtTotal3.Text) - Convert.ToDouble(txtUpto3.Text)).ToString();
            ////////lblBal4.Text = (Convert.ToDouble(txtTotal4.Text) - Convert.ToDouble(txtUpto4.Text)).ToString();
            ////////lblBal5.Text = (Convert.ToDouble(txtTotal5.Text) - Convert.ToDouble(txtUpto5.Text)).ToString();
            ////////lblBal6.Text = (Convert.ToDouble(txtTotal6.Text) - Convert.ToDouble(txtUpto6.Text)).ToString();
            ////////lblBal7.Text = (Convert.ToDouble(txtTotal7.Text) - Convert.ToDouble(txtUpto7.Text)).ToString();
            ////////lblBal8.Text = (Convert.ToDouble(txtTotal8.Text) - Convert.ToDouble(txtUpto8.Text)).ToString();
            ////////lblBal9.Text = (Convert.ToDouble(txtTotal9.Text) - Convert.ToDouble(txtUpto9.Text)).ToString();
            ////////lblBal10.Text = (Convert.ToDouble(txtTotal10.Text) - Convert.ToDouble(txtUpto10.Text)).ToString();
            ////////lblBal11.Text = (Convert.ToDouble(txtTotal11.Text) - Convert.ToDouble(txtUpto11.Text)).ToString();
            ////////lblBal12.Text = (Convert.ToDouble(txtTotal12.Text) - Convert.ToDouble(txtUpto12.Text)).ToString();
            ////////lblBal13.Text = (Convert.ToDouble(txtTotal13.Text) - Convert.ToDouble(txtUpto13.Text)).ToString();
            ////////lblBal14.Text = (Convert.ToDouble(txtTotal14.Text) - Convert.ToDouble(txtUpto14.Text)).ToString();
            ////////lblBal15.Text = (Convert.ToDouble(txtTotal15.Text) - Convert.ToDouble(txtUpto15.Text)).ToString();
            ////////lblBal16.Text = (Convert.ToDouble(txtTotal16.Text) - Convert.ToDouble(txtUpto16.Text)).ToString();
            ////////lblBal17.Text = (Convert.ToDouble(txtTotal17.Text) - Convert.ToDouble(txtUpto17.Text)).ToString();
            ////////lblBal18.Text = (Convert.ToDouble(txtTotal18.Text) - Convert.ToDouble(txtUpto18.Text)).ToString();
            ////////lblBal19.Text = (Convert.ToDouble(txtTotal19.Text) - Convert.ToDouble(txtUpto19.Text)).ToString();
            ////////lblBal20.Text = (Convert.ToDouble(txtTotal20.Text) - Convert.ToDouble(txtUpto20.Text)).ToString();
            UpdateTotals();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void UpdateTotals()
    {
        try
        {
            ////////lblTot.Text = (Convert.ToDouble(txtTotal1.Text) + Convert.ToDouble(txtTotal2.Text) + Convert.ToDouble(txtTotal3.Text) + Convert.ToDouble(txtTotal4.Text) + Convert.ToDouble(txtTotal5.Text) + Convert.ToDouble(txtTotal6.Text) + Convert.ToDouble(txtTotal7.Text) + Convert.ToDouble(txtTotal1.Text) + Convert.ToDouble(txtTotal8.Text) + Convert.ToDouble(txtTotal9.Text) + Convert.ToDouble(txtTotal10.Text) + Convert.ToDouble(txtTotal11.Text) + Convert.ToDouble(txtTotal12.Text) + Convert.ToDouble(txtTotal13.Text) + Convert.ToDouble(txtTotal14.Text) + Convert.ToDouble(txtTotal15.Text) + Convert.ToDouble(txtTotal16.Text) + Convert.ToDouble(txtTotal17.Text) + Convert.ToDouble(txtTotal18.Text) + Convert.ToDouble(txtTotal19.Text) + Convert.ToDouble(txtTotal20.Text)).ToString();
            ////////lblUpto.Text = (Convert.ToDouble(txtUpto1.Text) + Convert.ToDouble(txtUpto2.Text) + Convert.ToDouble(txtUpto3.Text) + Convert.ToDouble(txtUpto4.Text) + Convert.ToDouble(txtUpto5.Text) + Convert.ToDouble(txtUpto6.Text) + Convert.ToDouble(txtUpto7.Text) + Convert.ToDouble(txtUpto1.Text) + Convert.ToDouble(txtUpto8.Text) + Convert.ToDouble(txtUpto9.Text) + Convert.ToDouble(txtUpto10.Text) + Convert.ToDouble(txtUpto11.Text) + Convert.ToDouble(txtUpto12.Text) + Convert.ToDouble(txtUpto13.Text) + Convert.ToDouble(txtUpto14.Text) + Convert.ToDouble(txtUpto15.Text) + Convert.ToDouble(txtUpto16.Text) + Convert.ToDouble(txtUpto17.Text) + Convert.ToDouble(txtUpto18.Text) + Convert.ToDouble(txtUpto19.Text) + Convert.ToDouble(txtUpto20.Text)).ToString();
            ////////lblTotBalAmt.Text = (Convert.ToDouble(lblBal1.Text) + Convert.ToDouble(lblBal2.Text) + Convert.ToDouble(lblBal3.Text) + Convert.ToDouble(lblBal4.Text) + Convert.ToDouble(lblBal5.Text) + Convert.ToDouble(lblBal6.Text) + Convert.ToDouble(lblBal7.Text) + Convert.ToDouble(lblBal1.Text) + Convert.ToDouble(lblBal8.Text) + Convert.ToDouble(lblBal9.Text) + Convert.ToDouble(lblBal10.Text) + Convert.ToDouble(lblBal11.Text) + Convert.ToDouble(lblBal12.Text) + Convert.ToDouble(lblBal13.Text) + Convert.ToDouble(lblBal14.Text) + Convert.ToDouble(lblBal15.Text) + Convert.ToDouble(lblBal16.Text) + Convert.ToDouble(lblBal17.Text) + Convert.ToDouble(lblBal18.Text) + Convert.ToDouble(lblBal19.Text) + Convert.ToDouble(lblBal20.Text)).ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void UpdateCHAPVIATotals(object sender, EventArgs e)
    {
        try
        {
            ////////txtVIATotal.Text = (Convert.ToDouble(txtVIAAm1.Text == "" ? "0" : txtVIAAm1.Text) + Convert.ToDouble(txtVIAAm2.Text == "" ? "0" : txtVIAAm2.Text) + Convert.ToDouble(txtVIAAm3.Text == "" ? "0" : txtVIAAm3.Text) + Convert.ToDouble(txtVIAAm4.Text == "" ? "0" : txtVIAAm4.Text) + Convert.ToDouble(txtVIAAm5.Text == "" ? "0" : txtVIAAm5.Text) + Convert.ToDouble(txtVIAAm6.Text == "" ? "0" : txtVIAAm6.Text) + Convert.ToDouble(txtVIAAm7.Text == "" ? "0" : txtVIAAm7.Text) + Convert.ToDouble(txtVIAAm8.Text == "" ? "0" : txtVIAAm8.Text) + Convert.ToDouble(txtVIAAm9.Text == "" ? "0" : txtVIAAm9.Text) + Convert.ToDouble(txtVIAAm10.Text == "" ? "0" : txtVIAAm10.Text) + Convert.ToDouble(txtVIAAm11.Text == "" ? "0" : txtVIAAm11.Text) + Convert.ToDouble(txtVIAAm12.Text == "" ? "0" : txtVIAAm12.Text) + Convert.ToDouble(txtVIAAm13.Text == "" ? "0" : txtVIAAm13.Text)).ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void UpdateOTHERINCOMETotals(object sender, EventArgs e)
    {
        try
        {

            txtINTRTotal.Text = (Convert.ToDouble(txtINTRAm1.Text) + Convert.ToDouble(txtINTRAm2.Text) + Convert.ToDouble(txtINTRAm3.Text) + Convert.ToDouble(txtINTRAm4.Text) + Convert.ToDouble(txtINTRAm5.Text)).ToString(); //+ Convert.ToDouble(txtIntrNSC.Text)).ToString();
            txtINTRTotal.Text = (Convert.ToDouble(txtINTRTotal.Text)).ToString(); //- Convert .ToDouble (txtIntrHousingLoan .Text)).ToString ();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GetDetailsFromDynamicTable(object sender, EventArgs e)
    {
        int ITRuleId;
        int Scheme;
        DataTableReader dtr;
        dtr = objITDecContr.GetDetailsFromITMaster(txtFromDate.Text, txtToDate.Text, GetEmpId());
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtRemark.Text = dtr["REMARK"].ToString();
                txtDate.Text = Convert.ToDateTime(dtr["HOUSEDT"]).ToString("dd/MM/yyyy");
                txtIntrHousingLoan.Text = dtr["HOUSEAMT"].ToString();
                txtHRA1.Text = dtr["HRA"].ToString();
                txtMedical.Text = dtr["MEDICAL"].ToString();
                txtIntrNSC.Text = dtr["NSCAMT"].ToString();

                txt80CCDNPS.Text = dtr["ACTUAL_80CCD_NPS"].ToString();
                txtRGRSS80CCG.Text = dtr["ACTUAL_RGESS_80CCG"].ToString();
                ddlITRule.SelectedValue = dtr["IT_RULE_ID"].ToString();
                // Added  on 07-06-2023
                ITRuleId = Convert.ToInt32(ddlITRule.SelectedValue);
                Scheme = Convert.ToInt32(objCommon.LookUp("PAYROLL_ITAXCAL", "SchemeType", "IT_RULE_ID=" + ITRuleId));
                if (Scheme == 0)
                {
                    lvVIAHeads.Visible = true;
                    lvVIARebate.Visible = true;
                    txtPaidRent.Enabled = true;
                    txt80CCDNPS.Enabled = true;
                    txtRGRSS80CCG.Enabled = true;
                    txtIntrHousingLoan.Enabled = true;
                    //btnUpHRA.Visible = true;
                    btnHraEntryActual.Visible = true;
                }
                else if (Scheme == 1)
                {
                    lvVIAHeads.Visible = false;
                    lvVIARebate.Visible = false;
                    //txtpay.Text = "0";
                    //txtDA.Text = "0";
                    //txtActualHra.Text = "0";
                    //txtROPHra.Text = "0";
                    //txtHRAReceived.Text = "0";
                    //txtPaidRent.Text = "0";
                    txtPaidRent.Enabled = false;
                    //txt10persal.Text = "0";
                    //txtRentPaidinAccess.Text = "0";
                    //txt40persal.Text = "0";
                    //txtHRAEntry.Text = "0";
                   // btnUpHRA.Visible = false;
                    btnHraEntryActual.Visible = false;
                    //txtIntrHousingLoan.Text = "0";
                    //txt80CCDNPS.Text = "0";
                    //txtRGRSS80CCG.Text = "0";
                    txt80CCDNPS.Enabled = false;
                    txtRGRSS80CCG.Enabled = false;
                    txtIntrHousingLoan.Enabled = false;
                }
                ////lblParticular1.Text = lblParticular1.Text.EndsWith("-E") ? dtr["REB_P1"].ToString() : lblParticular1.Text;
                ////lblParticular2.Text = lblParticular2.Text.EndsWith("-E") ? dtr["REB_P2"].ToString() : lblParticular2.Text;
                ////lblParticular3.Text = lblParticular3.Text.EndsWith("-E") ? dtr["REB_P3"].ToString() : lblParticular3.Text;
                ////lblParticular4.Text = lblParticular4.Text.EndsWith("-E") ? dtr["REB_P4"].ToString() : lblParticular4.Text;
                ////lblParticular5.Text = lblParticular5.Text.EndsWith("-E") ? dtr["REB_P5"].ToString() : lblParticular5.Text;
                ////lblParticular6.Text = lblParticular6.Text.EndsWith("-E") ? dtr["REB_P6"].ToString() : lblParticular6.Text;
                ////lblParticular7.Text = lblParticular7.Text.EndsWith("-E") ? dtr["REB_P7"].ToString() : lblParticular7.Text;
                ////lblParticular8.Text = lblParticular8.Text.EndsWith("-E") ? dtr["REB_P8"].ToString() : lblParticular8.Text;
                ////lblParticular9.Text = lblParticular9.Text.EndsWith("-E") ? dtr["REB_P9"].ToString() : lblParticular9.Text;
                ////lblParticular10.Text = lblParticular10.Text.EndsWith("-E") ? dtr["REB_P10"].ToString() : lblParticular10.Text;
                ////lblParticular11.Text = lblParticular11.Text.EndsWith("-E") ? dtr["REB_P11"].ToString() : lblParticular11.Text;
                ////lblParticular12.Text = lblParticular12.Text.EndsWith("-E") ? dtr["REB_P12"].ToString() : lblParticular12.Text;
                ////lblParticular13.Text = lblParticular13.Text.EndsWith("-E") ? dtr["REB_P13"].ToString() : lblParticular13.Text;
                ////lblParticular14.Text = lblParticular14.Text.EndsWith("-E") ? dtr["REB_P14"].ToString() : lblParticular14.Text;
                ////lblParticular15.Text = lblParticular15.Text.EndsWith("-E") ? dtr["REB_P15"].ToString() : lblParticular15.Text;
                ////lblParticular16.Text = lblParticular16.Text.EndsWith("-E") ? dtr["REB_P16"].ToString() : lblParticular16.Text;
                ////lblParticular17.Text = lblParticular17.Text.EndsWith("-E") ? dtr["REB_P17"].ToString() : lblParticular17.Text;
                ////lblParticular18.Text = lblParticular18.Text.EndsWith("-E") ? dtr["REB_P18"].ToString() : lblParticular18.Text;
                ////lblParticular19.Text = lblParticular19.Text.EndsWith("-E") ? dtr["REB_P19"].ToString() : lblParticular19.Text;
                ////lblParticular20.Text = lblParticular20.Text.EndsWith("-E") ? dtr["REB_P20"].ToString() : lblParticular20.Text;

                ////txtTotal1.Text = lblParticular1.Text.EndsWith("-E") ? dtr["REB_T1"].ToString() : txtTotal1.Text;
                //////txtTotal1.Text = dtr["REB_T1"].ToString();
                ////txtTotal2.Text = lblParticular2.Text.EndsWith("-E") ? dtr["REB_T2"].ToString() : txtTotal2.Text;
                //////txtTotal2.Text = dtr["REB_T2"].ToString();
                ////txtTotal3.Text = lblParticular3.Text.EndsWith("-E") ? dtr["REB_T3"].ToString() : txtTotal3.Text;
                //////txtTotal3.Text = dtr["REB_T3"].ToString();
                ////txtTotal4.Text = lblParticular4.Text.EndsWith("-E") ? dtr["REB_T4"].ToString() : txtTotal4.Text;
                //////txtTotal4.Text = dtr["REB_T4"].ToString();
                ////txtTotal5.Text = lblParticular5.Text.EndsWith("-E") ? dtr["REB_T5"].ToString() : txtTotal5.Text;
                //////txtTotal5.Text = dtr["REB_T5"].ToString();
                ////txtTotal6.Text = lblParticular6.Text.EndsWith("-E") ? dtr["REB_T6"].ToString() : txtTotal6.Text;
                //////txtTotal6.Text = dtr["REB_T6"].ToString();
                ////txtTotal7.Text = lblParticular7.Text.EndsWith("-E") ? dtr["REB_T7"].ToString() : txtTotal7.Text;
                //////txtTotal7.Text = dtr["REB_T7"].ToString();
                ////txtTotal8.Text = lblParticular8.Text.EndsWith("-E") ? dtr["REB_T8"].ToString() : txtTotal8.Text;
                //////txtTotal8.Text = dtr["REB_T8"].ToString();

                ////txtTotal9.Text = lblParticular9.Text.EndsWith("-E") ? dtr["REB_T9"].ToString() : txtTotal9.Text;
                //////txtTotal9.Text = dtr["REB_T9"].ToString();
                ////txtTotal10.Text = lblParticular10.Text.EndsWith("-E") ? dtr["REB_T10"].ToString() : txtTotal10.Text;
                //////txtTotal10.Text = dtr["REB_T10"].ToString();
                ////txtTotal11.Text = lblParticular11.Text.EndsWith("-E") ? dtr["REB_T11"].ToString() : txtTotal11.Text;
                //////txtTotal11.Text = dtr["REB_T11"].ToString();
                ////txtTotal12.Text = lblParticular12.Text.EndsWith("-E") ? dtr["REB_T12"].ToString() : txtTotal12.Text;
                //////txtTotal12.Text = dtr["REB_T12"].ToString();
                ////txtTotal13.Text = lblParticular13.Text.EndsWith("-E") ? dtr["REB_T13"].ToString() : txtTotal13.Text;
                //////txtTotal13.Text = dtr["REB_T13"].ToString();
                ////txtTotal14.Text = lblParticular14.Text.EndsWith("-E") ? dtr["REB_T14"].ToString() : txtTotal14.Text;
                //////txtTotal14.Text = dtr["REB_T14"].ToString();
                ////txtTotal15.Text = lblParticular15.Text.EndsWith("-E") ? dtr["REB_T15"].ToString() : txtTotal15.Text;
                //////txtTotal15.Text = dtr["REB_T15"].ToString();
                ////txtTotal16.Text = lblParticular16.Text.EndsWith("-E") ? dtr["REB_T16"].ToString() : txtTotal16.Text;
                //////txtTotal16.Text = dtr["REB_T16"].ToString();
                ////txtTotal17.Text = lblParticular17.Text.EndsWith("-E") ? dtr["REB_T17"].ToString() : txtTotal17.Text;
                //////txtTotal17.Text = dtr["REB_T17"].ToString();
                ////txtTotal18.Text = lblParticular18.Text.EndsWith("-E") ? dtr["REB_T18"].ToString() : txtTotal18.Text;
                //////txtTotal18.Text = dtr["REB_T18"].ToString();
                ////txtTotal19.Text = lblParticular19.Text.EndsWith("-E") ? dtr["REB_T19"].ToString() : txtTotal19.Text;
                //////txtTotal19.Text = dtr["REB_T19"].ToString();
                ////txtTotal20.Text = lblParticular20.Text.EndsWith("-E") ? dtr["REB_T20"].ToString() : txtTotal20.Text;
                //////txtTotal20.Text = dtr["REB_T20"].ToString();

                //////UPTO
                ////txtUpto1.Text = lblParticular1.Text.EndsWith("-E") ? dtr["REB_M1"].ToString() : txtUpto1.Text;
                //////txtUpto1.Text = dtr["REB_M1"].ToString();
                ////txtUpto2.Text = lblParticular2.Text.EndsWith("-E") ? dtr["REB_M2"].ToString() : txtUpto2.Text;
                //////txtUpto2.Text = dtr["REB_M2"].ToString();
                ////txtUpto3.Text = lblParticular3.Text.EndsWith("-E") ? dtr["REB_M3"].ToString() : txtUpto3.Text;
                //////txtUpto3.Text = dtr["REB_M3"].ToString();
                ////txtUpto4.Text = lblParticular4.Text.EndsWith("-E") ? dtr["REB_M4"].ToString() : txtUpto4.Text;
                //////txtUpto4.Text = dtr["REB_M4"].ToString();
                ////txtUpto5.Text = lblParticular5.Text.EndsWith("-E") ? dtr["REB_M5"].ToString() : txtUpto5.Text;
                //////txtUpto5.Text = dtr["REB_M5"].ToString();
                ////txtUpto6.Text = lblParticular6.Text.EndsWith("-E") ? dtr["REB_M6"].ToString() : txtUpto6.Text;
                //////txtUpto6.Text = dtr["REB_M6"].ToString();
                ////txtUpto7.Text = lblParticular7.Text.EndsWith("-E") ? dtr["REB_M7"].ToString() : txtUpto7.Text;
                //////txtUpto7.Text = dtr["REB_M7"].ToString();
                ////txtUpto8.Text = lblParticular8.Text.EndsWith("-E") ? dtr["REB_M8"].ToString() : txtUpto8.Text;
                //////txtUpto8.Text = dtr["REB_M8"].ToString();
                ////txtUpto9.Text = lblParticular9.Text.EndsWith("-E") ? dtr["REB_M9"].ToString() : txtUpto9.Text;
                //////txtUpto9.Text = dtr["REB_M9"].ToString();
                ////txtUpto10.Text = lblParticular10.Text.EndsWith("-E") ? dtr["REB_M10"].ToString() : txtUpto10.Text;
                //////txtUpto10.Text = dtr["REB_M10"].ToString();
                ////txtUpto11.Text = lblParticular11.Text.EndsWith("-E") ? dtr["REB_M11"].ToString() : txtUpto11.Text;
                //////txtUpto11.Text = dtr["REB_M11"].ToString();
                ////txtUpto12.Text = lblParticular12.Text.EndsWith("-E") ? dtr["REB_M12"].ToString() : txtUpto12.Text;
                //////txtUpto12.Text = dtr["REB_M12"].ToString();
                ////txtUpto13.Text = lblParticular13.Text.EndsWith("-E") ? dtr["REB_M13"].ToString() : txtUpto13.Text;
                //////txtUpto13.Text = dtr["REB_M13"].ToString();
                ////txtUpto14.Text = lblParticular14.Text.EndsWith("-E") ? dtr["REB_M14"].ToString() : txtUpto14.Text;
                //////txtUpto14.Text = dtr["REB_M14"].ToString();
                ////txtUpto15.Text = lblParticular15.Text.EndsWith("-E") ? dtr["REB_M15"].ToString() : txtUpto15.Text;
                //////txtUpto15.Text = dtr["REB_M15"].ToString();
                ////txtUpto16.Text = lblParticular16.Text.EndsWith("-E") ? dtr["REB_M16"].ToString() : txtUpto16.Text;
                //////txtUpto16.Text = dtr["REB_M16"].ToString();
                ////txtUpto17.Text = lblParticular17.Text.EndsWith("-E") ? dtr["REB_M17"].ToString() : txtUpto17.Text;
                //////txtUpto17.Text = dtr["REB_M17"].ToString();
                ////txtUpto18.Text = lblParticular18.Text.EndsWith("-E") ? dtr["REB_M18"].ToString() : txtUpto18.Text;
                //////txtUpto18.Text = dtr["REB_M18"].ToString();
                ////txtUpto19.Text = lblParticular19.Text.EndsWith("-E") ? dtr["REB_M19"].ToString() : txtUpto19.Text;
                //////txtUpto19.Text = dtr["REB_M19"].ToString();
                ////txtUpto20.Text = lblParticular20.Text.EndsWith("-E") ? dtr["REB_M20"].ToString() : txtUpto20.Text;
                //////txtUpto20.Text = dtr["REB_M20"].ToString();

                txtINTRP1.Text = dtr["INC_REP1"].ToString();
                txtINTRP2.Text = dtr["INC_REP2"].ToString();
                txtINTRP3.Text = dtr["INC_REP3"].ToString();
                txtINTRP4.Text = dtr["INC_REP4"].ToString();
                txtINTRP5.Text = dtr["INC_REP5"].ToString();
                txtINTRAm1.Text = dtr["INC_AMT1"].ToString();
                txtINTRAm2.Text = dtr["INC_AMT2"].ToString();
                txtINTRAm3.Text = dtr["INC_AMT3"].ToString();
                txtINTRAm4.Text = dtr["INC_AMT4"].ToString();
                txtINTRAm5.Text = dtr["INC_AMT5"].ToString();
                UpdateOTHERINCOMETotals(sender, e);
                //decimal totalIntr =Convert.ToDecimal(txtINTRAm1.Text) + Convert.ToDecimal(txtINTRAm2.Text) + Convert.ToDecimal(txtINTRAm3.Text) + Convert.ToDecimal(txtINTRAm4.Text) + Convert.ToDecimal(txtINTRAm5.Text);
                //txtINTRTotal.Text = totalIntr.ToString();
                CalculateBalance(sender, e);
            }

            else
            {
                //lblParticular1.Text = lblParticular1.Text.EndsWith("-E") ? lblParticular1.Text.Substring(0, lblParticular1.Text.Length - 2) : lblParticular1.Text;
                //lblParticular2.Text = lblParticular2.Text.EndsWith("-E") ? lblParticular2.Text.Substring(0, lblParticular2.Text.Length - 2) : lblParticular2.Text;
                //lblParticular3.Text = lblParticular3.Text.EndsWith("-E") ? lblParticular3.Text.Substring(0, lblParticular3.Text.Length - 2) : lblParticular3.Text;
                //lblParticular4.Text = lblParticular4.Text.EndsWith("-E") ? lblParticular4.Text.Substring(0, lblParticular4.Text.Length - 2) : lblParticular4.Text;
                //lblParticular5.Text = lblParticular5.Text.EndsWith("-E") ? lblParticular5.Text.Substring(0, lblParticular5.Text.Length - 2) : lblParticular5.Text;
                //lblParticular6.Text = lblParticular6.Text.EndsWith("-E") ? lblParticular6.Text.Substring(0, lblParticular6.Text.Length - 2) : lblParticular6.Text;
                //lblParticular7.Text = lblParticular7.Text.EndsWith("-E") ? lblParticular7.Text.Substring(0, lblParticular7.Text.Length - 2) : lblParticular7.Text;
                //lblParticular8.Text = lblParticular8.Text.EndsWith("-E") ? lblParticular8.Text.Substring(0, lblParticular8.Text.Length - 2) : lblParticular8.Text;
                //lblParticular9.Text = lblParticular9.Text.EndsWith("-E") ? lblParticular9.Text.Substring(0, lblParticular9.Text.Length - 2) : lblParticular9.Text;
                //lblParticular10.Text = lblParticular10.Text.EndsWith("-E") ? lblParticular10.Text.Substring(0, lblParticular10.Text.Length - 2) : lblParticular10.Text;
                //lblParticular11.Text = lblParticular11.Text.EndsWith("-E") ? lblParticular11.Text.Substring(0, lblParticular11.Text.Length - 2) : lblParticular11.Text;
                //lblParticular12.Text = lblParticular12.Text.EndsWith("-E") ? lblParticular12.Text.Substring(0, lblParticular12.Text.Length - 2) : lblParticular12.Text;
                //lblParticular13.Text = lblParticular13.Text.EndsWith("-E") ? lblParticular13.Text.Substring(0, lblParticular13.Text.Length - 2) : lblParticular13.Text;
                //lblParticular14.Text = lblParticular14.Text.EndsWith("-E") ? lblParticular14.Text.Substring(0, lblParticular14.Text.Length - 2) : lblParticular14.Text;
                //lblParticular15.Text = lblParticular15.Text.EndsWith("-E") ? lblParticular15.Text.Substring(0, lblParticular15.Text.Length - 2) : lblParticular15.Text;
                //lblParticular16.Text = lblParticular16.Text.EndsWith("-E") ? lblParticular16.Text.Substring(0, lblParticular16.Text.Length - 2) : lblParticular16.Text;
                //lblParticular17.Text = lblParticular17.Text.EndsWith("-E") ? lblParticular17.Text.Substring(0, lblParticular17.Text.Length - 2) : lblParticular17.Text;
                //lblParticular18.Text = lblParticular18.Text.EndsWith("-E") ? lblParticular18.Text.Substring(0, lblParticular18.Text.Length - 2) : lblParticular18.Text;
                //lblParticular19.Text = lblParticular19.Text.EndsWith("-E") ? lblParticular19.Text.Substring(0, lblParticular19.Text.Length - 2) : lblParticular19.Text;
                //lblParticular20.Text = lblParticular20.Text.EndsWith("-E") ? lblParticular20.Text.Substring(0, lblParticular20.Text.Length - 2) : lblParticular20.Text;

                txtINTRAm1.Text = "0";
                txtINTRAm2.Text = "0";
                txtINTRAm3.Text = "0";
                txtINTRAm4.Text = "0";
                txtINTRAm5.Text = "0";

            }
        }
    }

    private void clear()
    {
        lblName.Text = "";
        txtOtherIncTDS.Text =
            ////////txtVIAAm1.Text = txtVIAAm2.Text = txtVIAAm3.Text = txtVIAAm4.Text = txtVIAAm5.Text = txtVIAAm6.Text = txtVIATotal.Text = string.Empty;
        txtINTRAm1.Text = txtINTRAm2.Text = txtINTRAm3.Text = txtINTRAm4.Text = txtINTRAm5.Text = txtINTRTotal.Text = string.Empty;


        txtINTRP1.Text = txtINTRP2.Text = txtINTRP3.Text = txtINTRP4.Text = txtINTRP5.Text = txtINTRTotal.Text = string.Empty;

        ////////txtDed1.Text = txtDed2.Text = txtDed3.Text = txtDed4.Text = txtDed5.Text = txtDed6.Text = txtDed7.Text = txtDed8.Text = txtDed9.Text = txtDed10.Text
        //////// = txtDed11.Text = txtDed12.Text = txtDed13.Text = txtDed14.Text = txtDed15.Text = txtDed16.Text = txtDed17.Text = txtDed18.Text = txtDed19.Text = txtDed20.Text = string.Empty;

        ////////txtTotal1.Text = txtTotal2.Text = string.Empty;

        ////////txtTotal1.Text = txtTotal2.Text = txtTotal3.Text = txtTotal4.Text = txtTotal5.Text = txtTotal6.Text = txtTotal7.Text = txtTotal8.Text = txtTotal9.Text = txtTotal10.Text
        //////// = txtTotal11.Text = txtTotal12.Text = txtTotal13.Text = txtTotal14.Text = txtTotal15.Text = txtTotal16.Text = txtTotal17.Text = txtTotal18.Text = txtTotal19.Text = txtTotal20.Text = string.Empty;


        ////////txtUpto1.Text = txtUpto2.Text = txtUpto3.Text = txtUpto4.Text = txtUpto5.Text = txtUpto6.Text = txtUpto7.Text = txtUpto8.Text = txtUpto9.Text = txtUpto10.Text
        ////////= txtUpto11.Text = txtUpto12.Text = txtUpto13.Text = txtUpto14.Text = txtUpto15.Text = txtUpto16.Text = txtUpto17.Text = txtUpto18.Text = txtUpto19.Text = txtUpto20.Text = string.Empty;



        ////////lblParticular1.Text = lblParticular2.Text = lblParticular3.Text = lblParticular4.Text = lblParticular5.Text = lblParticular6.Text = lblParticular7.Text = lblParticular8.Text = lblParticular9.Text = lblParticular10.Text
        //////// = lblParticular11.Text = lblParticular12.Text = lblParticular13.Text = lblParticular14.Text = lblParticular15.Text = lblParticular16.Text = lblParticular17.Text = lblParticular18.Text = lblParticular19.Text = lblParticular20.Text = string.Empty;

        txtIntrHousingLoan.Text = txtIntrNSC.Text = txtDate.Text = string.Empty;
        lblDepartment.Text = lblDesignation.Text = lblStaff.Text = string.Empty;
        ////////lblTot.Text = lblTotBalAmt.Text = lblUpto.Text = lblUptoHead.Text = lblName.Text = string.Empty;
        txtPhoneNo.Text = txtEmailID.Text = txtPANNO.Text = string.Empty;
        txtpay.Text = txtActualHra.Text = txtROPHra.Text = txtSalHRA.Text = txtHRAReceived.Text = txtPaidRent.Text = txt10persal.Text = txtRentPaidinAccess.Text = txtHRAEntry.Text = txt40persal.Text = txtMedical.Text = txtHRA1.Text = string.Empty;
        ddlOrderBy.SelectedIndex = 0;

        ////////txtVIAAm1.Text = txtVIAAm2.Text = txtVIAAm3.Text = txtVIAAm4.Text = txtVIAAm5.Text = txtVIAAm6.Text = txtVIAAm7.Text = txtVIAAm8.Text = txtVIAAm9.Text = txtVIAAm10.Text
        ////////= txtVIAAm11.Text = txtVIAAm12.Text = txtVIAAm13.Text = "0";
        txtRemark.Text = string.Empty;
        txtHRA1.Text = "0";
        txtSalHRA.Text = "0";
        txtIntrHousingLoan.Text = "0";
        ddlemp.SelectedIndex = 0;

        lvVIAHeads.DataSource = null;
        lvVIAHeads.DataBind();
        lvVIARebate.DataSource = null;
        lvVIARebate.DataBind();
        //lvVIAHeads.Visible = false;
        //lvVIARebate.Visible = false;
        txtMedical.Text = txtIntrNSC.Text = "0";
        txtOtherIncTDS.Text = "0";
        //Response.Redirect("Pay_ITDeclaration.aspx");

        //chap VI limit
        ////////txtActualAmtVI1.Text = "0";
        ////////txtActualAmtVI2.Text = "0";
        ////////txtActualAmtVI3.Text = "0";
        ////////txtActualAmtVI4.Text = "0";
        ////////txtActualAmtVI5.Text = "0";
        ////////txtActualAmtVI6.Text = "0";
        ////////txtActualAmtVI7.Text = "0";
        ////////txtActualAmtVI8.Text = "0";
        ////////txtActualAmtVI9.Text = "0";
        ////////txtActualAmtVI10.Text = "0";
        ////////txtActualAmtVI11.Text = "0";
        ////////txtActualAmtVI12.Text = "0";
        ////////txtActualAmtVI13.Text = "0";

        txtDA.Text = string.Empty;
        txtSumPerquisite.Text = "0.00";
        txt80CCDNPS.Text = string.Empty;
        txtRGRSS80CCG.Text = string.Empty;
        //ddlemp.SelectedIndex = 0;

        //txtLimitVI1.Text = "0";
        //txtLimitVI2.Text = "0";
        //txtLimitVI3.Text = "0";
        //txtLimitVI4.Text = "0";
        //txtLimitVI5.Text = "0";
        //txtLimitVI6.Text = "0";
        //txtLimitVI7.Text = "0";
        //txtLimitVI8.Text = "0";
        //txtLimitVI9.Text = "0";
        //txtLimitVI10.Text = "0";
        //txtLimitVI11.Text = "0";
        //txtLimitVI12.Text = "0";
        //txtLimitVI13.Text = "0";


        ChkSendEmail.Checked = false;
        divMessage.Visible = false;
        btnITSendMail.Visible = false;
        txtMailMessage.Text = string.Empty;
        divITRuleName.Visible = false;


    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckITDeclaration();
        // clear();
        ShowEmpInfo(Convert.ToInt32(ddlemp.SelectedValue));
        Session["IDNO"] = (Convert.ToInt32(ddlemp.SelectedValue));
        PopulateDetails(sender, e);
        ShowChapVIA(Convert.ToInt32(ddlemp.SelectedValue));



        //perquisite
        DateTime finyearFdate = Convert.ToDateTime(txtFromDate.Text);
        DateTime finyearTdate = Convert.ToDateTime(txtToDate.Text);

        string finyear = Convert.ToString(finyearFdate.Year) + '-' + Convert.ToString(finyearTdate.Year).Substring(2);

        int count = Convert.ToInt32(objCommon.LookUp("PAYROLL_NATUREOFPERQUISITE", "COUNT(1)", "IDNO=" + ddlemp.SelectedValue + "AND FINYEAR ='" + finyear + "'"));
        if (count > 0)
        {
            totalPerquisiteAmt = Convert.ToDecimal(objCommon.LookUp("PAYROLL_NATUREOFPERQUISITE", "SUM(ISNULL(VALUE5,0))", "IDNO=" + ddlemp.SelectedValue + "AND FINYEAR ='" + finyear + "'"));
        }
        txtSumPerquisite.Text = totalPerquisiteAmt.ToString();
        btnSendMail.Enabled = true;
        //perquisite end



        // fetch Chapter VI details
        DataSet ds = objITDecContr.GET_CHAPVI_AMOUNT_BY_EMPID(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (ListViewItem i in lvVIAHeads.Items)
            {
                HiddenField hdnCNO = (HiddenField)i.FindControl("hdnCNO");
                TextBox txtAmt = (TextBox)i.FindControl("txtAmount");
                Button btnUploadDoc = (Button)i.FindControl("btnUploadDocument");
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["CNO"].ToString()) == Convert.ToInt32(hdnCNO.Value))
                {
                    txtAmt.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
                    btnUploadDoc.BackColor = System.Drawing.ColorTranslator.FromHtml("#00D800");
                }
            }
        }

        ds = objITDecContr.GET_REBEATE_AMOUNT_BY_EMPIDNO(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (ListViewItem i in lvVIARebate.Items)
            {
                HiddenField hdnFNO = (HiddenField)i.FindControl("hdnFNO");
                TextBox txtAmt = (TextBox)i.FindControl("txtValue");
                Button btnUploadDoc = (Button)i.FindControl("btnUploadRebate");
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["FNO"].ToString()) == Convert.ToInt32(hdnFNO.Value))
                {
                    txtAmt.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
                    btnUploadDoc.BackColor = System.Drawing.ColorTranslator.FromHtml("#00D800");
                }
            }
        }
        
    }

    private void BindVIAHeadsList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_CHAPVI_HEAD", "CNO, SEC, HEADNAMEFULL", "ISNULL(LIMIT,0.00) as LIMIT ,ISPERCENTAGE, 0.00 as ACTUAL_AMOUNT, 0.00 as AMOUNT, (CASE ISPERCENTAGE WHEN 0 THEN 'Rs.' ELSE '%' END) AS SHOW_PERCENTAGE", "", "CNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvVIAHeads.DataSource = ds;
                lvVIAHeads.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckITDeclaration()
    {
        try
        {
            int empuatype = Convert.ToInt32(Session["usertype"]);
            int empidno = 0;
            bool declstatus;
            string decfinyear = string.Empty;
            empidno = Convert.ToInt32(ddlemp.SelectedValue);
            decfinyear = Convert.ToDateTime(txtFromDate.Text).Year.ToString() + Convert.ToDateTime(txtToDate.Text).Year.ToString();
            if (empuatype != 1)
            {

                //  declstatus = Convert.ToInt32(objCommon.LookUp("Payroll_ITDeclarationByEmployee", "COUNT(1)", "IDNO=" + empidno + " AND FINYEAR='" + decfinyear + "'"));
                declstatus = Convert.ToBoolean(objCommon.LookUp("Payroll_ITDeclarationByEmployee", "DECSTATUS", "IDNO=" + empidno + " AND FINYEAR='" + decfinyear + "'"));
                if (declstatus)
                {
                    // ddlemp.SelectedIndex = 0;
                    /////  objCommon.DisplayMessage(this.updpanel, "Declaration Already Done and Locked!", this.Page);
                    Showmessage("Declaration Already Done and Locked!");
                    btnSave0.Visible = false;
                    btnClear.Visible = false;
                    btnSendMail.Enabled = false;
                    ChkIsLock.Checked = true;
                    ChkIsLock.Enabled = false;
                    btnLockUnlock.Visible = false;

                }
                else
                {
                    btnSave0.Visible = true;
                    btnClear.Visible = true;
                    btnSendMail.Enabled = true;
                    ChkIsLock.Enabled = true;
                    btnLockUnlock.Visible = false;


                }
            }
            else
            {
                btnSendMail.Visible = true;
                declstatus = Convert.ToBoolean(objCommon.LookUp("Payroll_ITDeclarationByEmployee", "DECSTATUS", "IDNO=" + empidno + " AND FINYEAR='" + decfinyear + "'"));
                if (declstatus)
                {
                    ChkIsLock.Checked = true;
                    ChkIsLock.Enabled = true;
                    btnLockUnlock.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderBy.SelectedValue == "1")
        {
            objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "idno");

        }
        else if (ddlOrderBy.SelectedValue == "2")
        {
            objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "FNAME");

        }
        else if (ddlOrderBy.SelectedValue == "3")
        {
            objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "seq_no");

        }
        else if (ddlOrderBy.SelectedValue == "4")
        {
            objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "pfileno");
        }
    }

    protected void ShowChapVIA(int idno)
    {


        try
        {

            DateTime dt1;
            dt1 = Convert.ToDateTime(calToDate.SelectedDate);
            string fromDate = string.Empty;
            string toDate = string.Empty;

            fromDate = txtFromDate.Text.ToString().Trim();
            toDate = txtToDate.Text.ToString().Trim();

            //chap VI LIMIT
            ITHRACALCULATE objITHra = new ITHRACALCULATE();
            objITHra.IDNO = idno;
            DateTime fromyear = Convert.ToDateTime(txtFromDate.Text);
            string fyear = fromyear.Year.ToString();
            DateTime toyear = Convert.ToDateTime(txtToDate.Text);
            string tyear = toyear.Year.ToString();
            objITHra.FINYEAR = fyear + tyear;

            DataTableReader dtr = objITDecContr.ShowEmpCHAPVIAmt(idno, fromDate, toDate);

            DataTableReader dtr1 = objITHraContr.ShowEmpCHAPVIActualAmt(objITHra);



            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //lblName .Text = dtr["NAME"].ToString();
                    ////////txtVIAAm1.Text = dtr["VI_AMT1"].ToString();
                    ////////txtVIAAm2.Text = dtr["VI_AMT2"].ToString();
                    ////////txtVIAAm3.Text = dtr["VI_AMT3"].ToString();
                    ////////txtVIAAm4.Text = dtr["VI_AMT4"].ToString();
                    ////////txtVIAAm5.Text = dtr["VI_AMT5"].ToString();
                    ////////txtVIAAm6.Text = dtr["VI_AMT6"].ToString();
                    ////////txtVIAAm7.Text = dtr["VI_AMT7"].ToString();
                    ////////txtVIAAm8.Text = dtr["VI_AMT8"].ToString();
                    ////////txtVIAAm9.Text = dtr["VI_AMT9"].ToString();
                    ////////txtVIAAm10.Text = dtr["VI_AMT10"].ToString();
                    ////////txtVIAAm11.Text = dtr["VI_AMT11"].ToString();
                    ////////txtVIAAm12.Text = dtr["VI_AMT12"].ToString();
                    ////////txtVIAAm13.Text = dtr["VI_AMT13"].ToString();


                    ////////txtVIAAm1.ToolTip = txtVIAAm1.Text;
                    ////////txtVIAAm2.ToolTip = txtVIAAm2.Text;
                    ////////txtVIAAm3.ToolTip = txtVIAAm3.Text;
                    ////////txtVIAAm4.ToolTip = txtVIAAm4.Text;
                    ////////txtVIAAm5.ToolTip = txtVIAAm5.Text;
                    ////////txtVIAAm6.ToolTip = txtVIAAm6.Text;
                    ////////txtVIAAm7.ToolTip = txtVIAAm7.Text;
                    ////////txtVIAAm8.ToolTip = txtVIAAm8.Text;
                    ////////txtVIAAm9.ToolTip = txtVIAAm9.Text;
                    ////////txtVIAAm10.ToolTip = txtVIAAm10.Text;
                    ////////txtVIAAm11.ToolTip = txtVIAAm11.Text;
                    ////////txtVIAAm12.ToolTip = txtVIAAm12.Text;
                    ////////txtVIAAm13.ToolTip = txtVIAAm13.Text;


                    ////////txtVIATotal.Text = dtr["VI_SUM"].ToString();
                    txtOtherIncTDS.Text = dtr["OTHERINC_TDS"].ToString();
                    //txtMedical.Text = dtr["MEDICAL"].ToString();
                    //txtHRA1.Text = dtr["HRA"].ToString();
                    //txtVIATotal.Text = txtVIAAm1.Text + txtVIAAm2.Text + txtVIAAm3.Text + txtVIAAm4.Text + txtVIAAm5.Text + txtVIAAm6.Text + txtVIAAm7.Text + txtVIAAm8.Text + txtVIAAm9.Text + txtVIAAm10.Text + txtVIAAm11.Text + txtVIAAm12.Text + txtVIAAm13.Text;
                    //decimal totalamt = Convert.ToDecimal(txtVIAAm1.Text + txtVIAAm2.Text + txtVIAAm3.Text + txtVIAAm4.Text + txtVIAAm5.Text + txtVIAAm6.Text + txtVIAAm7.Text + txtVIAAm8.Text + txtVIAAm9.Text + txtVIAAm10.Text + txtVIAAm11.Text + txtVIAAm12.Text + txtVIAAm13.Text);
                    // txtVIATotal.Text = totalamt.ToString();

                }
                dtr.Close();
            }

            if (dtr1 != null)
            {
                if (dtr1.Read())
                {
                    ////////txtActualAmtVI1.Text = dtr1["VA_ACTUALAMT1"].ToString();
                    ////////txtActualAmtVI2.Text = dtr1["VA_ACTUALAMT2"].ToString();
                    ////////txtActualAmtVI3.Text = dtr1["VA_ACTUALAMT3"].ToString();
                    ////////txtActualAmtVI4.Text = dtr1["VA_ACTUALAMT4"].ToString();
                    ////////txtActualAmtVI5.Text = dtr1["VA_ACTUALAMT5"].ToString();
                    ////////txtActualAmtVI6.Text = dtr1["VA_ACTUALAMT6"].ToString();
                    ////////txtActualAmtVI7.Text = dtr1["VA_ACTUALAMT7"].ToString();
                    ////////txtActualAmtVI8.Text = dtr1["VA_ACTUALAMT8"].ToString();
                    ////////txtActualAmtVI9.Text = dtr1["VA_ACTUALAMT9"].ToString();
                    ////////txtActualAmtVI10.Text = dtr1["VA_ACTUALAMT10"].ToString();
                    ////////txtActualAmtVI11.Text = dtr1["VA_ACTUALAMT11"].ToString();
                    ////////txtActualAmtVI12.Text = dtr1["VA_ACTUALAMT12"].ToString();
                    ////////txtActualAmtVI13.Text = dtr1["VA_ACTUALAMT13"].ToString();
                }
                dtr1.Close();
            }


            // this method is not in use so commented it - 
            //DataSet ds = objITHraContr.ShowEmpRebateHeads(idno, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //txtTotal1.Text = ds.Tables[0].Rows[0]["REB_T1"].ToString();
            //txtTotal2.Text = ds.Tables[0].Rows[0]["REB_T2"].ToString();
            //txtTotal3.Text = ds.Tables[0].Rows[0]["REB_T3"].ToString();
            //txtTotal4.Text = ds.Tables[0].Rows[0]["REB_T4"].ToString();
            //txtTotal5.Text = ds.Tables[0].Rows[0]["REB_T5"].ToString();
            //txtTotal6.Text = ds.Tables[0].Rows[0]["REB_T6"].ToString();
            //txtTotal7.Text = ds.Tables[0].Rows[0]["REB_T7"].ToString();
            //txtTotal8.Text = ds.Tables[0].Rows[0]["REB_T8"].ToString();
            //txtTotal9.Text = ds.Tables[0].Rows[0]["REB_T9"].ToString();
            //txtTotal10.Text = ds.Tables[0].Rows[0]["REB_T10"].ToString();
            //txtTotal11.Text = ds.Tables[0].Rows[0]["REB_T11"].ToString();
            //txtTotal12.Text = ds.Tables[0].Rows[0]["REB_T12"].ToString();
            //txtTotal13.Text = ds.Tables[0].Rows[0]["REB_T13"].ToString();
            //txtTotal14.Text = ds.Tables[0].Rows[0]["REB_T14"].ToString();
            //txtTotal15.Text = ds.Tables[0].Rows[0]["REB_T15"].ToString();
            //txtTotal16.Text = ds.Tables[0].Rows[0]["REB_T16"].ToString();
            //txtTotal17.Text = ds.Tables[0].Rows[0]["REB_T17"].ToString();
            //txtTotal18.Text = ds.Tables[0].Rows[0]["REB_T18"].ToString();
            //txtTotal19.Text = ds.Tables[0].Rows[0]["REB_T19"].ToString();
            //txtTotal20.Text = ds.Tables[0].Rows[0]["REB_T20"].ToString();
            //  }


            DataSet dsVIA = objITDecContr.GetITVIAHeadsToModify(idno, objITHra.FINYEAR);
            if (dsVIA.Tables[0].Rows.Count > 0)
            {
                lvVIAHeads.DataSource = dsVIA;
                lvVIAHeads.DataBind();
            }
            else
            {
                BindVIAHeadsList();
            }
        }

        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    protected void txtROPHra_TextChanged(object sender, EventArgs e)
    {
        decimal ahra = Convert.ToDecimal(txtActualHra.Text);
        decimal rophra = Convert.ToDecimal(txtROPHra.Text);
        txtHRAReceived.Text = Convert.ToString(ahra - rophra);
    }
    protected void btnHraEntryActual_Click(object sender, EventArgs e)
    {
        decimal pay = Convert.ToDecimal(txtpay.Text);
        decimal da = Convert.ToDecimal(txtDA.Text);

        decimal rentpaid = Convert.ToDecimal(txtPaidRent.Text);
        decimal per10 = Convert.ToInt32(((pay + da) * 10) / 100);
        txt10persal.Text = per10.ToString();
        txtRentPaidinAccess.Text = Convert.ToString(rentpaid - per10);

        //G
        decimal hrarecived = Convert.ToDecimal(txtHRAReceived.Text);

        //J
        decimal rentpaidaccess10per = Convert.ToDecimal(txtRentPaidinAccess.Text);

        //K
        if (rdbMetro.SelectedValue == "2")
        {
            decimal per40 = Convert.ToDecimal(((pay + da) * 40) / 100);
            txt40persal.Text = per40.ToString();

            if (hrarecived < rentpaidaccess10per && hrarecived < per40)
            {
                txtHRAEntry.Text = hrarecived.ToString();
            }
            else if (rentpaidaccess10per < hrarecived && rentpaidaccess10per < per40)
            {
                txtHRAEntry.Text = rentpaidaccess10per.ToString();
            }
            else if (per40 < hrarecived && per40 < rentpaidaccess10per)
            {
                txtHRAEntry.Text = per40.ToString();
            }
        }
        else
        {
            decimal per50 = Convert.ToDecimal(((pay + da) * 50) / 100);
            txt40persal.Text = per50.ToString();

            if (hrarecived < rentpaidaccess10per && hrarecived < per50)
            {
                txtHRAEntry.Text = hrarecived.ToString();
            }
            else if (rentpaidaccess10per < hrarecived && rentpaidaccess10per < per50)
            {
                txtHRAEntry.Text = rentpaidaccess10per.ToString();
            }
            else if (per50 < hrarecived && per50 < rentpaidaccess10per)
            {
                txtHRAEntry.Text = per50.ToString();
            }
        }

        txtHRA1.Text = txtHRAEntry.Text;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        int empidno = Convert.ToInt32(Session["idno"]);
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (ua_type == 1)
        {
            if (ddlOrderBy.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0 AND COLLEGE_NO=" + ddlCollege.SelectedValue + "", "IDNO");

            }
            else if (ddlOrderBy.SelectedValue == "2")
            {
                objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0 AND COLLEGE_NO=" + ddlCollege.SelectedValue + "", "FNAME");

            }
        }
        else if (ua_type != 1)
        {
            FillLoginEmp(empidno);
        }
    }


    protected void FillLoginEmp(int empidno)
    {
        // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        //  objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO=" + empidno + "", "idno");
        // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID =2", "COLLEGE_ID ASC");
        // ddlCollege.SelectedValue = "2";
        // ddlCollege.Visible = false;

        //objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "idno");
        objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO=" + empidno, "FNAME");
    }

    // Sachin 23 Mar 2018 

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
    ////////protected void btnUploadVA1_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP1.Text;
    ////////    Session["VI"] = hdn_CNO_VIA1.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_2_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP2.Text;
    ////////    Session["VI"] = hdn_CNO_VIA2.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_3_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP3.Text;
    ////////    Session["VI"] = hdn_CNO_VIA3.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_4_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP4.Text;
    ////////    Session["VI"] = hdn_CNO_VIA4.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_5_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP5.Text;
    ////////    Session["VI"] = hdn_CNO_VIA5.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();

    ////////}
    ////////protected void btnAddVI_6_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP6.Text;
    ////////    Session["VI"] = hdn_CNO_VIA6.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_7_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP7.Text;
    ////////    Session["VI"] = hdn_CNO_VIA7.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_8_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP8.Text;
    ////////    Session["VI"] = hdn_CNO_VIA8.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_9_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP9.Text;
    ////////    Session["VI"] = hdn_CNO_VIA9.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_10_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP10.Text;
    ////////    Session["VI"] = hdn_CNO_VIA10.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_11_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP11.Text;
    ////////    Session["VI"] = hdn_CNO_VIA12.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}
    ////////protected void btnAddVI_12_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP12.Text;
    ////////    Session["VI"] = hdn_CNO_VIA12.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();

    ////////}
    ////////protected void btnAddVI_13_Click(object sender, EventArgs e)
    ////////{
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = true;
    ////////    btnAddRebate.Visible = false;
    ////////    ModalPopupExtender1.Show();
    ////////    mpe.Hide();
    ////////    div_lv_VI.Visible = true;
    ////////    div_lv_Rebate.Visible = false;
    ////////    lblPerticular.Text = txtVIAP13.Text;
    ////////    Session["VI"] = hdn_CNO_VIA13.Value;
    ////////    BindGrid(Convert.ToInt32(Session["VI"].ToString()));
    ////////    ClearPopUp();
    ////////}

    private void ClearPopUp()
    {
        txtHouseOwnerName.Text = string.Empty;
        txtPanNumber.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtDeclarationDate.Text = string.Empty;
        txtRemarkPopUp.Text = string.Empty;
        lblFileNaame.Text = string.Empty;
        txtDocumentName.Text = string.Empty;
        Session["CHAPVI_ID"] = null;
        ViewState["EmpDocumentUrl"] = null;
        btnAddDocument.Text = "Add";
        btnAddRebate.Text = "Add";
    }

    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string extension = string.Empty;

        extension = ".pdf,.PDF,.doc,.DOC,.docx,.DOCX";


        string[] Ext = extension.Split(',');
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void btnAddDocument_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAmount.Text == "" || txtAmount.Text == null)
            {
                Showmessage("Please Enter Amount");
                return;
            }
            else if (txtDocumentName.Text == "" || txtDocumentName.Text == null)
            {
                Showmessage("Please Enter Document Name");
                return;
            }
            else if (fupDocument.HasFile == false)
            {
                Showmessage("Please first select a file to upload");
                return;
            }
            string file = string.Empty;
            string ToCheckPath = string.Empty;
            // BindGrid(Convert.ToInt32(Session["VI"].ToString()));
            BindGrid(Convert.ToInt32(ViewState["CNO"].ToString()));
            //  objITDM.CNO = Convert.ToInt32(Session["VI"].ToString());
            objITDM.CNO = Convert.ToInt32(ViewState["CNO"].ToString());
            objITDM.IDNO = Convert.ToInt32(ddlemp.SelectedValue);
            objITDM.HouseOwnerName = txtHouseOwnerName.Text;
            objITDM.PanNumber = txtPanNumber.Text;

            if (txtDeclarationDate.Text == string.Empty)
            {
                objITDM.DeclarationDate = DateTime.MinValue;
            }
            else
            {
                objITDM.DeclarationDate = Convert.ToDateTime(txtDeclarationDate.Text);
            }

            objITDM.Amount = Convert.ToDouble(txtAmount.Text);
            objITDM.FinYear = Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString();
            objITDM.REMARK = txtRemarkPopUp.Text;
            objITDM.COLLEGE_CODE = Session["colcode"].ToString();

            objITDM.EmpDocumentName = txtDocumentName.Text;

            if (fupDocument.HasFile)
            {
                //if (FileTypeValid(System.IO.Path.GetExtension(fupDocument.FileName)))
                //{
                file = Docpath + "\\PayrollDocuments\\" + Session["IDNO"] + "\\" + "VI A " + "\\" + Session["VI"];

                if (!System.IO.Directory.Exists(file))
                {
                    System.IO.Directory.CreateDirectory(file);
                }
                ViewState["FILE_PATH"] = file;
                dbPath = file;
                string filename = fupDocument.FileName;
                ViewState["FILE_NAME"] = filename;
                path = file + "\\" + fupDocument.FileName;

                ToCheckPath = file + "\\" + lblFileNaame.Text.Trim();
                if (!System.IO.Directory.Exists(path))
                {
                    if (!File.Exists(path))
                    {
                        if (File.Exists(ToCheckPath))
                        {
                            System.IO.File.Delete(ToCheckPath);
                            lblFileNaame.Text = fupDocument.FileName;
                            fupDocument.PostedFile.SaveAs(path);

                            objITDM.EmpDocumentUrl = path;
                            objITDM.DocumentType = fupDocument.FileName;
                        }
                        else
                        {
                            lblFileNaame.Text = fupDocument.FileName;
                            fupDocument.PostedFile.SaveAs(path);

                            objITDM.EmpDocumentUrl = path;
                            objITDM.DocumentType = fupDocument.FileName;
                        }
                    }
                }

                // }
                //  else
                // {
                //MessageBox("Please Upload Valid Files. .jpg for Photo and .pdf for Other documents");
                //  return;
                // }


            }

            //  objITDM.CollegeNo = "0";
            objITDM.CollegeNo = ddlCollege.SelectedValue;
            // ddlCollege.SelectedValue;

            if (Session["CHAPVI_ID"] == null)
            {
                objITDM.CHAPVI_ID = 0;
                CustomStatus cs = (CustomStatus)objITDecContr.Add_ChapterVI(objITDM);

                Showmessage("Record added successfully");
            }
            else
            {
                objITDM.CHAPVI_ID = Convert.ToInt32(Session["CHAPVI_ID"].ToString());
                CustomStatus cs = (CustomStatus)objITDecContr.Add_ChapterVI(objITDM);

                Showmessage("Record updated successfully");
            }

            Clear();
            Fill_VI_Amount();
            // BindGrid(Convert.ToInt32(Session["VI"].ToString()));
            BindGrid(Convert.ToInt32(ViewState["CNO"].ToString()));
            ClearPopUp();
            btnAddDocument.Text = "Add";
            ModalPopupExtender1.Show();
            Session["CHAPVI_ID"] = null;
        }
        catch (Exception ex)
        {

        }
    }

    private void Fill_VI_Amount()
    {
        DataSet ds = objITDecContr.GET_CHAPVI_HEAD_AMOUNT_BY_ID(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString()); //, Convert.ToInt32(Session["VI"].ToString()));

        ////////if (ds.Tables[0].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm1.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
        ////////    txtActualAmtVI1.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm1.Text = "0";
        //}


        ////////if (ds.Tables[1].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm2.Text = ds.Tables[1].Rows[0]["Amount2"].ToString();
        ////////    txtActualAmtVI2.Text = ds.Tables[1].Rows[0]["Amount2"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm2.Text = "0";
        //}

        ////////if (ds.Tables[2].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm3.Text = ds.Tables[2].Rows[0]["Amount3"].ToString();
        ////////    txtActualAmtVI3.Text = ds.Tables[2].Rows[0]["Amount3"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm3.Text = "0";
        //}

        ////////if (ds.Tables[3].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm4.Text = ds.Tables[3].Rows[0]["Amount4"].ToString();
        ////////    txtActualAmtVI4.Text = ds.Tables[3].Rows[0]["Amount4"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm4.Text = "0";
        //}

        ////////if (ds.Tables[4].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm5.Text = ds.Tables[4].Rows[0]["Amount5"].ToString();
        ////////    txtActualAmtVI5.Text = ds.Tables[4].Rows[0]["Amount5"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm5.Text = "0";
        //}

        ////////if (ds.Tables[5].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm6.Text = ds.Tables[5].Rows[0]["Amount6"].ToString();
        ////////    txtActualAmtVI6.Text = ds.Tables[5].Rows[0]["Amount6"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm6.Text = "0";
        //}

        ////////if (ds.Tables[6].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm7.Text = ds.Tables[6].Rows[0]["Amount7"].ToString();
        ////////    txtActualAmtVI7.Text = ds.Tables[6].Rows[0]["Amount7"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm7.Text = "0";
        //}

        ////////if (ds.Tables[7].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm8.Text = ds.Tables[7].Rows[0]["Amount8"].ToString();
        ////////    txtActualAmtVI8.Text = ds.Tables[7].Rows[0]["Amount8"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm8.Text = "0";
        //}

        ////////if (ds.Tables[8].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm9.Text = ds.Tables[8].Rows[0]["Amount9"].ToString();
        ////////    txtActualAmtVI9.Text = ds.Tables[8].Rows[0]["Amount9"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm9.Text = "0";
        //}

        ////////if (ds.Tables[9].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm10.Text = ds.Tables[9].Rows[0]["Amount10"].ToString();
        ////////    txtActualAmtVI10.Text = ds.Tables[9].Rows[0]["Amount10"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm10.Text = "0";
        //}

        ////////if (ds.Tables[10].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm11.Text = ds.Tables[10].Rows[0]["Amount11"].ToString();
        ////////    txtActualAmtVI11.Text = ds.Tables[10].Rows[0]["Amount11"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm11.Text = "0";
        //}

        ////////if (ds.Tables[11].Rows.Count > 0)
        ////////{
        ////////    txtVIAAm12.Text = ds.Tables[11].Rows[0]["Amount12"].ToString();
        ////////    txtActualAmtVI12.Text = ds.Tables[11].Rows[0]["Amount12"].ToString();
        ////////}
        //else
        //{
        //    txtVIAAm12.Text = "0";

        //}

        if (ds.Tables[0].Rows.Count > 0)
        {
            // txtVIAAm13.Text = ds.Tables[0].Rows[0]["Amount13"].ToString();
            // txtActualAmtVI13.Text = ds.Tables[0].Rows[0]["Amount13"].ToString();
        }
        //else
        ////{
        //    txtVIAAm13.Text = "0";
        //}
    }


    //private void Fill_REBATE_Amount()
    //{
    //    DataSet ds = objITDecContr.GET_REBATE_HEAD_AMOUNT_BY_ID(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());

    //    if ((ds.Tables[20].Rows[0]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[0]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtTotal1.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal1.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate1.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[1]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[1]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[1].Rows.Count > 0)
    //        {
    //            txtTotal2.Text = ds.Tables[1].Rows[0]["Amount2"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal2.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate2.Visible = false;
    //    }


    //    if ((ds.Tables[20].Rows[2]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[2]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[2].Rows.Count > 0)
    //        {
    //            txtTotal3.Text = ds.Tables[2].Rows[0]["Amount3"].ToString();
    //        }
    //        else
    //        {
    //            txtVIAAm3.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate3.Visible = false;
    //    }


    //    if ((ds.Tables[20].Rows[3]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[3]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[3].Rows.Count > 0)
    //        {
    //            txtTotal4.Text = ds.Tables[3].Rows[0]["Amount4"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal4.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate4.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[4]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[4]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[4].Rows.Count > 0)
    //        {
    //            txtTotal5.Text = ds.Tables[4].Rows[0]["Amount5"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal5.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate5.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[5]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[5]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[5].Rows.Count > 0)
    //        {
    //            txtTotal6.Text = ds.Tables[5].Rows[0]["Amount6"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal6.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate6.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[6]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[6]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[6].Rows.Count > 0)
    //        {
    //            txtTotal7.Text = ds.Tables[6].Rows[0]["Amount7"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal7.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate7.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[7]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[7]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[7].Rows.Count > 0)
    //        {
    //            txtTotal8.Text = ds.Tables[7].Rows[0]["Amount8"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal8.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate8.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[8]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[8]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[8].Rows.Count > 0)
    //        {
    //            txtTotal9.Text = ds.Tables[8].Rows[0]["Amount9"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal9.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate9.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[9]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[9]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[9].Rows.Count > 0)
    //        {
    //            txtTotal10.Text = ds.Tables[9].Rows[0]["Amount10"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal10.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate10.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[10]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[10]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[10].Rows.Count > 0)
    //        {
    //            txtTotal11.Text = ds.Tables[10].Rows[0]["Amount11"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal11.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate11.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[11]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[11]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[11].Rows.Count > 0)
    //        {
    //            txtTotal12.Text = ds.Tables[11].Rows[0]["Amount12"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal12.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate12.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[12]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[12]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[12].Rows.Count > 0)
    //        {
    //            txtTotal13.Text = ds.Tables[12].Rows[0]["Amount13"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal13.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate13.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[13]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[13]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[13].Rows.Count > 0)
    //        {
    //            txtTotal14.Text = ds.Tables[13].Rows[0]["Amount14"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal14.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate14.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[14]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[14]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[14].Rows.Count > 0)
    //        {
    //            txtTotal15.Text = ds.Tables[14].Rows[0]["Amount15"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal15.Text = "0";

    //        }
    //    }
    //    else
    //    {
    //        btnRebate15.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[15]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[15]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[15].Rows.Count > 0)
    //        {
    //            txtTotal16.Text = ds.Tables[15].Rows[0]["Amount16"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal16.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate16.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[16]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[16]["DEDHEAD"].ToString() == ""))
    //    {
    //        if (ds.Tables[16].Rows.Count > 0)
    //        {
    //            txtTotal17.Text = ds.Tables[16].Rows[0]["Amount17"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal17.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate17.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[17]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[17]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[17].Rows.Count > 0)
    //        {
    //            txtTotal18.Text = ds.Tables[17].Rows[0]["Amount18"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal18.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate18.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[18]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[18]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[18].Rows.Count > 0)
    //        {
    //            txtTotal19.Text = ds.Tables[18].Rows[0]["Amount19"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal19.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate19.Visible = false;
    //    }

    //    if ((ds.Tables[20].Rows[19]["DEDHEAD"].ToString() == string.Empty) || (ds.Tables[20].Rows[19]["DEDHEAD"].ToString() == ""))
    //    {

    //        if (ds.Tables[19].Rows.Count > 0)
    //        {
    //            txtTotal20.Text = ds.Tables[19].Rows[0]["Amount20"].ToString();
    //        }
    //        else
    //        {
    //            txtTotal20.Text = "0";
    //        }
    //    }
    //    else
    //    {
    //        btnRebate20.Visible = false;
    //    }


    //}

    private void BindGrid(int VI)
    {
        if (ddlemp.SelectedIndex > 0)
        {
            DataTable ds = objITDecContr.GET_CHAPVI_HEAD_DATA(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToInt32(VI), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());
            lvEmpAmount.DataSource = ds;
            lvEmpAmount.DataBind();
        }
        else
        {
            Showmessage("Please select employee");
            return;
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        // Session["CHAPVI_ID"] = null;
        ViewState["EmpDocumentUrl"] = null;
        ImageButton btnEdit = sender as ImageButton;
        int CHAPVI_ID = int.Parse(btnEdit.CommandArgument);
        Session["CHAPVI_ID"] = CHAPVI_ID;
        DataTable ds = objITDecContr.GET_CHAPVI_HEAD_DATA_BY_ID(CHAPVI_ID);

        if (ds.Rows.Count > 0)
        {
            txtHouseOwnerName.Text = ds.Rows[0]["HouseOwnerName"].ToString();
            txtPanNumber.Text = ds.Rows[0]["PanNumber"].ToString();
            txtAmount.Text = ds.Rows[0]["Amount"].ToString();

            txtDeclarationDate.Text = ds.Rows[0]["ChapVIADecDate"].ToString();
            txtRemarkPopUp.Text = ds.Rows[0]["Remark"].ToString();
            lblFileNaame.Text = ds.Rows[0]["DocumentType"].ToString();
            txtDocumentName.Text = ds.Rows[0]["EmpDocumentName"].ToString();

            btnAddDocument.Text = "Update";
            //ViewState["EmpDocumentUrl"] = ds.Rows[0]["EmpDocumentUrl"].ToString();
        }

        ModalPopupExtender1.Show();
        //ViewState["EmpDocumentUrl"] = null;

        //ImageButton btnEdit = sender as ImageButton;
        //int RebateHeadId = int.Parse(btnEdit.CommandArgument);
        //Session["RebateHeadId"] = RebateHeadId;
        //DataTable ds = objITDecContr.GET_REBATE_HEAD_DATA_BY_ID(RebateHeadId);

        //if (ds.Rows.Count > 0)
        //{
        //    txtAmount.Text = ds.Rows[0]["Amount"].ToString();
        //    txtDeclarationDate.Text = ds.Rows[0]["ChapDedHeadDecDate"].ToString();
        //    txtRemarkPopUp.Text = ds.Rows[0]["Remark"].ToString();
        //    ViewState["EmpDocumentUrl"] = ds.Rows[0]["EmpDocumentUrl"].ToString();
        //    lblFileNaame.Text = ds.Rows[0]["DocumentType"].ToString();
        //    txtDocumentName.Text = ds.Rows[0]["EmpDocumentName"].ToString();
        //}
        //ModalPopupExtender1.Show();
    }

    public void DownloadFile(string PathName, string DocumentName)
    {
        try
        {

            path = Docpath + "\\PayrollDocuments\\" + Session["IDNO"] + "\\" + "VI A " + "\\" + Session["VI"];
            //path = PathName;


            FileStream sourceFile = new FileStream((path + "\\" + DocumentName), FileMode.Open);
            //FileStream sourceFile = new FileStream((path), FileMode.Open);

            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(DocumentName.Substring(DocumentName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + DocumentName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDownload = sender as ImageButton;
            Session["lblCHAPVI_ID"] = int.Parse(btnDownload.CommandArgument);
            ListViewDataItem lst = btnDownload.NamingContainer as ListViewDataItem;
            Label docURL = lst.FindControl("docURL") as Label;
            string DocumentURL = docURL.Text.Trim();

            Label docNAME = lst.FindControl("docNAME") as Label;
            string DocumentName = docNAME.Text.Trim();

            DownloadFile(DocumentURL, DocumentName);
        }
        catch (Exception ex)
        {
            Showmessage("Document not available...!!!");
            ModalPopupExtender1.Show();
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    /*REBATE*/

    private void BindGrid_Rebate(int RE)
    {
        if (ddlemp.SelectedIndex > 0)
        {
            DataTable ds = objITDecContr.GET_REBATE_HEAD_DATA(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToInt32(RE), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());
            lv_Rebate.DataSource = ds;
            lv_Rebate.DataBind();
        }
        else
        {
            Showmessage("Please select employee");
            return;
        }
    }

    protected void btnAddRebate_Click(object sender, EventArgs e)
    {
        if (txtAmount.Text == "" || txtAmount.Text == null)
        {
            Showmessage("Please Enter Amount");
            return;
        }
        else if (txtDocumentName.Text == "" || txtDocumentName.Text == null)
        {
            Showmessage("Please Enter Document Name");
            return;
        }
        else if (fupDocument.HasFile == false)
        {
            Showmessage("Please first select a file to upload");
            return;
        }
        string file = string.Empty;
        string ToCheckPath = string.Empty;

        // BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
        BindGrid_Rebate(Convert.ToInt32(ViewState["FNO"].ToString()));

        tr_txtHouseOwnerName.Visible = false;
        tr_txtHouseOwnerName.Visible = false;

        //objITDM.CNO = Convert.ToInt32(ViewState["CNO"].ToString());
        objITDM.FNO = Convert.ToInt32(ViewState["FNO"].ToString());

        objITDM.IDNO = Convert.ToInt32(ddlemp.SelectedValue);

        if (txtDeclarationDate.Text == string.Empty)
        {
            objITDM.DeclarationDate = DateTime.MinValue;
        }
        else
        {
            objITDM.DeclarationDate = Convert.ToDateTime(txtDeclarationDate.Text);
        }

        //fupDocument Rohit Maske 24-12-2018 upload and Download 
        if (fupDocument.HasFile)
        {
            //if (FileTypeValid(System.IO.Path.GetExtension(fupDocument.FileName)))
            //{
            file = Docpath + "\\PayrollDocuments\\" + Session["IDNO"] + "\\" + "Rebate" + "\\" + Session["RE"];

            if (!System.IO.Directory.Exists(file))
            {
                System.IO.Directory.CreateDirectory(file);
            }
            ViewState["FILE_PATH"] = file;
            dbPath = file;
            string filename = fupDocument.FileName;
            ViewState["FILE_NAME"] = filename;
            path = file + "\\" + fupDocument.FileName;
            objITDM.EmpDocumentName = txtDocumentName.Text;
            ToCheckPath = file + "\\" + lblFileNaame.Text.Trim();
            if (!System.IO.Directory.Exists(path))
            {
                if (!File.Exists(path))
                {
                    if (File.Exists(ToCheckPath))
                    {
                        System.IO.File.Delete(ToCheckPath);
                        lblFileNaame.Text = fupDocument.FileName;
                        fupDocument.PostedFile.SaveAs(path);

                        objITDM.EmpDocumentUrl = path;
                        objITDM.DocumentType = fupDocument.FileName;
                    }
                    else
                    {
                        lblFileNaame.Text = fupDocument.FileName;
                        fupDocument.PostedFile.SaveAs(path);

                        objITDM.EmpDocumentUrl = path;
                        objITDM.DocumentType = fupDocument.FileName;
                    }
                }

                objITDM.EmpDocumentUrl = path;
                objITDM.DocumentType = fupDocument.FileName;
            }
            // }
            // else
            //  {
            // MessageBox("Please Upload Valid Files. .jpg for Photo and .pdf for Other documents");
            // return;
            //}

        }
        else if (ViewState["EmpDocumentUrl"] != null)
        {
            objITDM.EmpDocumentUrl = ViewState["EmpDocumentUrl"].ToString();
        }
        else
        {
            objITDM.EmpDocumentUrl = Server.MapPath("~/PAYROLL/TRANSACTIONS/DeclarationDocument/" + fupDocument.FileName);
        }

        //objITDM.DocumentType = fupDocument.FileName;       
        //objITDM.EmpDocumentName = txtDocumentName.Text;

        //  objITDM.CollegeNo = "0";
        objITDM.CollegeNo = ddlCollege.SelectedValue;
        //ddlCollege.SelectedValue;

        objITDM.Amount = Convert.ToDouble(txtAmount.Text);
        objITDM.FinYear = Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString();
        objITDM.REMARK = txtRemarkPopUp.Text;
        objITDM.COLLEGE_CODE = Session["colcode"].ToString();


        if (Session["RebateHeadId"] == null)
        {
            objITDM.RebateHeadId = 0;
            CustomStatus cs = (CustomStatus)objITDecContr.Add_Rebate(objITDM);
            Showmessage("Record added sucessfully");
        }
        else
        {
            objITDM.RebateHeadId = Convert.ToInt32(Session["RebateHeadId"].ToString());
            CustomStatus cs = (CustomStatus)objITDecContr.Add_Rebate(objITDM);

            Showmessage("Record updated sucessfully");
        }

        btnAddRebate.Text = "Add";
        //BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
        BindGrid_Rebate(Convert.ToInt32(ViewState["FNO"].ToString()));
        Fill_REBATE_Amount();
        Clear();
        ClearPopUp();
        ModalPopupExtender1.Show();
        Session["RebateHeadId"] = null;

    }

    private void Fill_REBATE_Amount()
    {
        DataSet ds = objITDecContr.GET_REBATE_HEAD_AMOUNT_BY_ID(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text).Year.ToString() + "" + Convert.ToDateTime(txtToDate.Text).Year.ToString());


        if (ds.Tables[0].Rows.Count > 0)
        {
            ////////txtTotal1.Text = ds.Tables[0].Rows[0]["Amount1"].ToString();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            ////////txtTotal2.Text = ds.Tables[1].Rows[0]["Amount2"].ToString();
        }


        if (ds.Tables[2].Rows.Count > 0)
        {
            ////////txtTotal3.Text = ds.Tables[2].Rows[0]["Amount3"].ToString();
        }




        if (ds.Tables[3].Rows.Count > 0)
        {
            ////////txtTotal4.Text = ds.Tables[3].Rows[0]["Amount4"].ToString();
        }



        if (ds.Tables[4].Rows.Count > 0)
        {
            ////////txtTotal5.Text = ds.Tables[4].Rows[0]["Amount5"].ToString();
        }

        if (ds.Tables[5].Rows.Count > 0)
        {
            ////////txtTotal6.Text = ds.Tables[5].Rows[0]["Amount6"].ToString();
        }


        if (ds.Tables[6].Rows.Count > 0)
        {
            ////////txtTotal7.Text = ds.Tables[6].Rows[0]["Amount7"].ToString();
        }


        if (ds.Tables[7].Rows.Count > 0)
        {
            ////////txtTotal8.Text = ds.Tables[7].Rows[0]["Amount8"].ToString();
        }


        if (ds.Tables[8].Rows.Count > 0)
        {
            ////////txtTotal9.Text = ds.Tables[8].Rows[0]["Amount9"].ToString();
        }


        if (ds.Tables[9].Rows.Count > 0)
        {
            ////////txtTotal10.Text = ds.Tables[9].Rows[0]["Amount10"].ToString();
        }

        if (ds.Tables[10].Rows.Count > 0)
        {
            ////////txtTotal11.Text = ds.Tables[10].Rows[0]["Amount11"].ToString();
        }

        if (ds.Tables[11].Rows.Count > 0)
        {
            ////////txtTotal12.Text = ds.Tables[11].Rows[0]["Amount12"].ToString();
        }

        if (ds.Tables[12].Rows.Count > 0)
        {
            ////////txtTotal13.Text = ds.Tables[12].Rows[0]["Amount13"].ToString();
        }

        if (ds.Tables[13].Rows.Count > 0)
        {
            ////////txtTotal14.Text = ds.Tables[13].Rows[0]["Amount14"].ToString();
        }

        if (ds.Tables[14].Rows.Count > 0)
        {
            ////////txtTotal15.Text = ds.Tables[14].Rows[0]["Amount15"].ToString();
        }



        if (ds.Tables[15].Rows.Count > 0)
        {
            ////////txtTotal16.Text = ds.Tables[15].Rows[0]["Amount16"].ToString();
        }

        if (ds.Tables[16].Rows.Count > 0)
        {
            ////////txtTotal17.Text = ds.Tables[16].Rows[0]["Amount17"].ToString();
        }


        if (ds.Tables[17].Rows.Count > 0)
        {
            //////////txtTotal18.Text = ds.Tables[17].Rows[0]["Amount18"].ToString();
        }


        if (ds.Tables[18].Rows.Count > 0)
        {
            ////////txtTotal19.Text = ds.Tables[18].Rows[0]["Amount19"].ToString();
        }


        if (ds.Tables[19].Rows.Count > 0)
        {
            ////////txtTotal20.Text = ds.Tables[19].Rows[0]["Amount20"].ToString();
        }

    }

    protected void btnEditRebate_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["EmpDocumentUrl"] = null;

        ImageButton btnEdit = sender as ImageButton;
        int RebateHeadId = int.Parse(btnEdit.CommandArgument);
        Session["RebateHeadId"] = RebateHeadId;
        DataTable ds = objITDecContr.GET_REBATE_HEAD_DATA_BY_ID(RebateHeadId);

        if (ds.Rows.Count > 0)
        {
            txtAmount.Text = ds.Rows[0]["Amount"].ToString();
            txtDeclarationDate.Text = ds.Rows[0]["ChapDedHeadDecDate"].ToString();
            txtRemarkPopUp.Text = ds.Rows[0]["Remark"].ToString();
            ViewState["EmpDocumentUrl"] = ds.Rows[0]["EmpDocumentUrl"].ToString();
            lblFileNaame.Text = ds.Rows[0]["DocumentType"].ToString();
            txtDocumentName.Text = ds.Rows[0]["EmpDocumentName"].ToString();
            btnAddRebate.Text = "Update";

        }
        ModalPopupExtender1.Show();

    }

    protected void btnDeleteRebate_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndelete = sender as ImageButton;
        int RebateHeadId = int.Parse(btndelete.CommandArgument);
        objITDecContr.DELETE_REBATE_HEAD_DATA_BY_ID(RebateHeadId);
        Showmessage("Record deleted sucessfully");
        BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
        ModalPopupExtender1.Show();
        Clear();

    }

    ////////protected void btnRebate1_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;

    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular1.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    txtHouseOwnerName.Visible = false;
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;

    ////////    Session["RE"] = hdnRebate1.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////}
    ////////protected void btnRebate2_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;

    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular2.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;

    ////////    Session["RE"] = hdnRebate2.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate3_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;

    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular3.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;

    ////////    Session["RE"] = hdnRebate3.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate4_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;

    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular4.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;

    ////////    Session["RE"] = hdnRebate4.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////}
    ////////protected void btnRebate5_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular5.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;

    ////////    Session["RE"] = hdnRebate5.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate6_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular6.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;

    ////////    Session["RE"] = hdnRebate6.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate7_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular7.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate7.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate8_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular8.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate8.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate9_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular9.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate9.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate10_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular10.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate10.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate11_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular11.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate11.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate12_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular12.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate12.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate13_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular13.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate13.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate14_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular14.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate14.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate15_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular15.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate15.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate16_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular16.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate16.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate17_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular17.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate17.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate18_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular18.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate18.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate19_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular19.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate19.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnRebate20_Click(object sender, EventArgs e)
    ////////{
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////    div_lv_VI.Visible = false;
    ////////    div_lv_Rebate.Visible = true;
    ////////    ModalPopupExtender1.Show();
    ////////    lblPerticular.Text = lblParticular20.Text;
    ////////    tr_txtPanNumber.Visible = false;
    ////////    tr_txtHouseOwnerName.Visible = false;
    ////////    Session["RE"] = hdnRebate20.Value;
    ////////    BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
    ////////    btnAddDocument.Visible = false;
    ////////    btnAddRebate.Visible = true;
    ////////}
    ////////protected void btnClosePopUp_Click(object sender, EventArgs e)
    ////////{
    ////////    Clear();
    ////////    ClearSessions();
    ////////    ModalPopupExtender1.Hide();
    ////////    btnAddDocument.Visible = true;
    ////////}

    private void Clear()
    {
        txtHouseOwnerName.Text = string.Empty;
        txtPanNumber.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtDeclarationDate.Text = string.Empty;
        txtRemarkPopUp.Text = string.Empty;
        ViewState["EmpDocumentUrl"] = null;
        txtDocumentName.Text = string.Empty;
        lblFileNaame.Text = string.Empty;
        //tr_txtPanNumber.Visible = true;
        btnAddRebate.Text = "Add";
        btnAddDocument.Text = "Add";
        //tr_txtHouseOwnerName.Visible = false;
    }

    private void ClearSessions()
    {
        Session["RE"] = null;
        Session["VI"] = null;
        Session["RebateHeadId"] = null;
        Session["CHAPVI_ID"] = null;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportEmployeeDeclaration("Employee_Declaration", "PayEmployeeDeclaration.rpt");
        }
        catch (Exception ex)
        {

        }
    }

    private void ShowReportEmployeeDeclaration(string reportTitle, string rptFileName)
    {
        try
        {
            string decfinyear = Convert.ToDateTime(txtFromDate.Text).Year.ToString() + Convert.ToDateTime(txtToDate.Text).Year.ToString();

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Payroll," + rptFileName + "&@P_STAFFNO=" + Convert.ToInt32(ddlStaffNo.SelectedValue) + "&@P_MONYEAR=" + ddlMonthYear.SelectedValue;


            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_IDNO=" + Convert.ToInt32(ddlemp.SelectedValue) + ",@P_FINYEAR=" + decfinyear + ",@P_COLLEGE_CODE=33";


            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ///  ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PaySlip.howReportEmployeePayslip() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void DownloadFileRebate(string PathName, string DocumentName)
    {
        try
        {

            path = Docpath + "\\PayrollDocuments\\" + Session["IDNO"] + "\\" + "Rebate" + "\\" + Session["RE"];
            //path = PathName;


            FileStream sourceFile = new FileStream((path + "\\" + DocumentName), FileMode.Open);
            //FileStream sourceFile = new FileStream((path), FileMode.Open);

            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(DocumentName.Substring(DocumentName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + DocumentName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    protected void btnDownloadRebate_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            ImageButton btnDownload = sender as ImageButton;
            Session["RebateHeadId"] = int.Parse(btnDownload.CommandArgument);
            ListViewDataItem lst = btnDownload.NamingContainer as ListViewDataItem;
            Label docURL = lst.FindControl("docURLR80C") as Label;
            string DocumentURL = docURL.Text.Trim();

            Label docNAME = lst.FindControl("docNAMER80C") as Label;
            string DocumentName = docNAME.Text.Trim();

            DownloadFileRebate(DocumentURL, DocumentName);
        }
        catch (Exception ex)
        {
            Showmessage("Document not available...!!!");
            ModalPopupExtender1.Show();
        }


        //try
        //{
        //    Session["RebateHeadId"] = null;
        //    ImageButton btnDownload = sender as ImageButton;
        //    Session["RebateHeadId"] = int.Parse(btnDownload.CommandArgument);

        //    List<ListItem> files = new List<ListItem>();
        //    string ext = ".docx,.jpg";

        //    string filePath = btnDownload.CommandName;

        //    FileStream sourceFile = new FileStream((filePath), FileMode.Open);
        //    long fileSize = sourceFile.Length;
        //    byte[] getContent = new byte[(int)fileSize];
        //    sourceFile.Read(getContent, 0, (int)sourceFile.Length);
        //    sourceFile.Close();
        //    sourceFile.Dispose();

        //    Response.ClearContent();
        //    Response.Clear();
        //    Response.BinaryWrite(getContent);
        //    Response.ContentType = GetResponseType(ext);

        //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + System.IO.Path.GetFileName(filePath.ToString()) + "\"");

        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.SuppressContent = true;
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();

        //}

        //catch (Exception ex)
        //{
        //    Showmessage("Document not available...!!!");
        //    ModalPopupExtender1.Show();

        //}
    }

    //protected void fupDocument_PreRender(object sender, EventArgs e)
    //{
    //    lblFileNaame.Text = string.Empty;
    //}
    //protected void fupDocument_DataBinding(object sender, EventArgs e)
    //{
    //    lblFileNaame.Text = string.Empty;
    //}
    //protected void fupDocument_Disposed(object sender, EventArgs e)
    //{
    //    lblFileNaame.Text = string.Empty;
    //}
    //protected void fupDocument_Unload(object sender, EventArgs e)
    //{
    //    lblFileNaame.Text = string.Empty;
    //}


    // it is used to send mail for IT declaration





    protected void btnITSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmailID.Text != string.Empty)
            {
                if (ddlemp.SelectedIndex > 0)
                {
                    string fromEmailId = string.Empty;
                    string fromEmailPwd = string.Empty;
                    string body = string.Empty;

                    //DataSet ds = objITDecContr.GetFromDataForEmail();
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    fromEmailId = ds.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                    //    fromEmailPwd = ds.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                    //}
                    string msg = txtMailMessage.Text.Trim();
                    sendmail(txtEmailID.Text, "Related To IT Declaration", "Dear Sir", msg);
                }
            }
            else
            {
                Showmessage("Employee EmailID Is Required.");
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ITDeclaration.btnITSendMail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }


    }



    public void sendmail(string toEmailId, string Sub, string body, string msg)
    {
        try
        {
            //string msg = string.Empty;
            //System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Subject = Sub;

            //string MemberEmailId = string.Empty;
            //mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            //mailMessage.To.Add(toEmailId);

            //string[] EmpName = ddlemp.SelectedItem.Text.Split('[');

            //var MailBody = new StringBuilder();
            ////MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            //MailBody.AppendFormat("Dear " + EmpName[0] + ", {0}\n", " ");
            //MailBody.AppendLine(@"<br /> " + txtMailMessage.Text);
            //MailBody.AppendLine(@"<br /> ");
            //MailBody.AppendLine(@"<br /> ");
            //MailBody.AppendLine(@"<br /> ");
            //MailBody.AppendLine(@"<br />Thanks And Regards");
            //MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());

            //mailMessage.Body = MailBody.ToString();

            //mailMessage.IsBodyHtml = true;
            //SmtpClient smt = new SmtpClient("smtp.gmail.com");

            //smt.UseDefaultCredentials = false;
            //smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            //smt.Port = 587;
            //smt.EnableSsl = true;

            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            //System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            //System.Security.Cryptography.X509Certificates.X509Chain chain,
            //System.Net.Security.SslPolicyErrors sslPolicyErrors)
            //{
            //    return true;
            //};

            //smt.Send(mailMessage);







            // here new code by Purva Raut

            //var fromAddress = "support@iitms.co.in";
            //var toAddress = toEmailId;
            //string fromPassword = "master2019";
            var fromAddress = "ccmssupport@iitms.co.in";
            var toAddress = toEmailId;
            string fromPassword = "Ccms@123";

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendFormat("<br /> Dear " + lblName.Text.Trim() + ", {0}\n", " ");
            MailBody.AppendLine(@"<br /> " + txtMailMessage.Text);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");

            using (MailMessage mm = new MailMessage(fromAddress, toAddress))
            {
                mm.Subject = Sub;
                // mm.Body = body;
                mm.Body = MailBody.ToString();
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(fromAddress, fromPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                // ViewBag.Message = "Email sent.";
            }
            Showmessage("Mail Send Successfully.");
            return;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }



    protected void ChkSendEmail_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ChkSendEmail.Checked == true)
            {
                divMessage.Visible = true;
                btnITSendMail.Visible = true;
            }
            else
            {
                divMessage.Visible = false;
                btnITSendMail.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    protected void btnLockUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            int IsLock;
            if (ChkIsLock.Checked == true)
            {
                IsLock = 1;
            }
            else
            {
                IsLock = 0;
            }

            CustomStatus cs = (CustomStatus)objITDecContr.LockUnlockITDeclaration(Convert.ToInt32(ddlemp.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), IsLock);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (IsLock == 1)
                {
                    Showmessage("IT Declaration Is Lock Successfully.");
                    return;
                }
                else
                {
                    Showmessage("IT Declaration Is Unlock Successfully.");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void ChkIsLock_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ChkIsLock.Checked == true)
            {
                //mpe.Hide();
                Showmessage("Are You Sure? Further you can not Modify IT Declaration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    protected void rdbMetro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbMetro.SelectedValue == "1")
            {
                lblMetroCity.Text = "50% Salary:";
            }
            else
            {
                lblMetroCity.Text = "40% Salary:";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnUploadDocument_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnUpload = sender as Button;
            ViewState["CNO"] = int.Parse(btnUpload.CommandArgument);
            tr_txtPanNumber.Visible = false;
            tr_txtHouseOwnerName.Visible = false;
            btnAddDocument.Visible = true;
            btnAddRebate.Visible = false;
            ModalPopupExtender1.Show();
            mpe.Hide();
            div_lv_VI.Visible = true;
            div_lv_Rebate.Visible = false;
            lblPerticular.Text = btnUpload.CommandName;
            // Session["CHAPVI_ID"] = int.Parse(btnUpload.CommandArgument);
            //BindGrid(Convert.ToInt32(Session["VI"].ToString()));
            BindGrid(Convert.ToInt32(ViewState["CNO"].ToString()));
            ClearPopUp();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    protected void btnUploadRebate_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnRebateUpload = sender as Button;
            ViewState["FNO"] = int.Parse(btnRebateUpload.CommandArgument);

            btnAddDocument.Visible = false;
            btnAddRebate.Visible = true;
            div_lv_VI.Visible = false;
            div_lv_Rebate.Visible = true;
            ModalPopupExtender1.Show();
            mpe.Hide();
            lblPerticular.Text = btnRebateUpload.CommandName;
            tr_txtPanNumber.Visible = false;
            tr_txtHouseOwnerName.Visible = false;
            txtHouseOwnerName.Visible = false;
            btnAddDocument.Visible = false;
            btnAddRebate.Visible = true;

            // Session["RE"] = int.Parse(btnRebateUpload.CommandArgument);
            // BindGrid_Rebate(Convert.ToInt32(Session["RE"].ToString()));
            BindGrid_Rebate(Convert.ToInt32(ViewState["FNO"].ToString()));
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        //objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0", "idno");
    }
    protected void FillITRules()
    {
        objCommon.FillDropDownList(ddlITRule, "PAYROLL_ITRULE", "IT_RULE_ID", "IT_RULE_NAME", "IT_RULE_ID > 0  and IsActive =1", "IT_RULE_ID");
    }
    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlemp, "PAYROLL_EMPMAS", "IDNO", "UPPER(FNAME + ' '+MNAME+' '+LNAME+ '['+ Convert (nvarchar(150),IDNO) +']')", "IDNO>0 AND COLLEGE_NO=" + ddlCollege.SelectedValue + "", "FNAME");
    //}

    protected void ddlITRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ITRuleId = Convert.ToInt32(ddlITRule.SelectedValue);
        int Scheme = Convert.ToInt32(objCommon.LookUp("PAYROLL_ITRULE", "SchemeType", "IT_RULE_ID=" + ITRuleId));
        if (Scheme == 0)
        {
            lvVIAHeads.Visible = true;
            lvVIARebate.Visible = true;
            txtPaidRent.Enabled = true;
            txt80CCDNPS.Enabled = true;
            txtRGRSS80CCG.Enabled = true;
            txtIntrHousingLoan.Enabled = true;
            //btnUpHRA.Visible = true;
            btnHraEntryActual.Visible = true;
        }
        else if (Scheme == 1)
        {
            lvVIAHeads.Visible = false;
            lvVIARebate.Visible = false;
            //txtpay.Text = "0";
            //txtDA.Text = "0";
            //txtActualHra.Text = "0";
            //txtROPHra.Text = "0";
            //txtHRAReceived.Text = "0";
            //txtPaidRent.Text = "0";
            txtPaidRent.Enabled = false;
            //txt10persal.Text = "0";
            //txtRentPaidinAccess.Text = "0";
            //txt40persal.Text = "0";
            //txtHRAEntry.Text = "0";
          //  btnUpHRA.Visible = false;
            btnHraEntryActual.Visible = false;
            //txtIntrHousingLoan.Text = "0";
            //txt80CCDNPS.Text = "0";
            //txtRGRSS80CCG.Text = "0";
            txt80CCDNPS.Enabled = false;
            txtRGRSS80CCG.Enabled = false;
            txtIntrHousingLoan.Enabled = false;
        }
    }
}