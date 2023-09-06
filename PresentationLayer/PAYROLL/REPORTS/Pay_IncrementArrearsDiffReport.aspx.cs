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

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_REPORTS_Pay_IncrementArrearsDiffReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayArrearsController objpay = new PayArrearsController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //For displaying user friendly messages
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

        if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
        {

            Response.Redirect("~/Default.aspx");
        }

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
            }

            ViewState["action"] = "add";
            //function to fill dropdownlists
            FillDropdown();
            //function to Bind Listview with Payheads

            DateTime fdate = System.DateTime.Now;

            txtGovDt.Text = fdate.ToShortDateString();
            txtOffDt.Text = fdate.ToShortDateString();
            pnlsupl.Visible = false;

        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Pay_ArrearsDiffReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ArrearsDiffReport.aspx");
        }
    }

    //Function to fill dropdownlist for Employee
    protected void FillDropdown()
    {
        //FILL EMPLOYEE
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 ", "FNAME");
        //objCommon.FillDropDownList(ddlArrears, "PAYROLL_ARREARS A,PAYROLL_STAFF C,PAYROLL_RULE R", "ARNO", "CAST(PER AS NVARCHAR(25) )+'%'+ ' ; ' + CAST(DateName( month , DateAdd( month , Datepart(month,AFRM) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,AFRM) AS NVARCHAR(25))+' '+'TO'+' '+ CAST(DateName( month , DateAdd( month , Datepart(month,ATO) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,ATO) AS NVARCHAR(25))+' ; '+STAFF+' ; '+R.PAYRULE", "C.STAFFNO = A.COLLEGENO  AND R.RULENO=A.RULENO", "");
        objCommon.FillDropDownList(ddlMonth, "PAYROLL_SALFILE", "DISTINCT(CAST(MONYEAR AS DATETIME)) AS MONYEAR_DATE", "MONYEAR", "SALNO>0", "MONYEAR_DATE DESC");

    }

    protected void ddlArrears_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetOrderNo();

        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_ARREARS_DIFF A INNER JOIN PAYROLL_EMPMAS E ON(A.IDNO=E.IDNO)", "DISTINCT A.IDNO","E.FNAME+' '+E.MNAME+' '+E.LNAME", "ARNO=" + ddlArrears.SelectedValue + "","A.IDNO");
    }

    private void GetOrderNo()
    {
        DataSet ds = null;
        ds = objpay.GetArrearsInfo(Convert.ToInt32(ddlArrears.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtGovOrdNo.Text = ds.Tables[0].Rows[0]["GOVORDNO"].ToString();
            txtGovDt.Text = ds.Tables[0].Rows[0]["GOVORDDT"].ToString();
            txtOffOrdNo.Text = ds.Tables[0].Rows[0]["OFFORDNO"].ToString();
            txtOffDt.Text = ds.Tables[0].Rows[0]["OFFORDDT"].ToString();

            string frm = ds.Tables[0].Rows[0]["AFRM"].ToString();
            string to = ds.Tables[0].Rows[0]["ATO"].ToString();
            int staffno = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGENO"].ToString());
            int collegeno = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString());
            DataSet dsmon = objpay.GetAllMonth(frm, to, staffno, collegeno);

            objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS", "IDNO", "FNAME + ' ' + MNAME + ' ' + LNAME + '['+ convert(nvarchar(150),IDNO) + ']'", "IDNO>0 and COLLEGE_NO=" + collegeno + " AND STAFFNO=" + staffno, "FNAME");


            ddlMonth.DataSource = dsmon;
            ddlMonth.DataValueField = dsmon.Tables[0].Columns[0].ToString();
            ddlMonth.DataTextField = dsmon.Tables[0].Columns[0].ToString();
            ddlMonth.DataBind();
        }

    }

    protected void GetArrearsDiff()
    {
        DataSet ds = null;
        ds = objpay.GetArrearsDiff(Convert.ToInt32(ddlArrears.SelectedValue), Convert.ToInt32(ddlEmp.SelectedValue), ddlMonth.SelectedItem.Text);
        //Creaded By : swati ghate
        //Created Date: 23-sept-2014
        //Reason: To avoid error for no record on 0th position
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtBasic.Text = ds.Tables[0].Rows[0]["BASIC"].ToString();
            txtGradePay.Text = ds.Tables[0].Rows[0]["GRADEPAY"].ToString();
            txtPay.Text = ds.Tables[0].Rows[0]["PAY"].ToString();
            txtRate.Text = ds.Tables[0].Rows[0]["PER"].ToString();
        }

    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.GetArrearsDiff();
            DataSet dsarr = objCommon.FillDropDown("PAYROLL_ARREARS", "*", "", "ARNO=" + ddlArrears.SelectedValue, "");
            txtRemark.Text = dsarr.Tables[0].Rows[0]["REMARK"].ToString();
            string payhead = dsarr.Tables[0].Rows[0]["PAYHEAD"].ToString();

            DataSet ds = null;
            double amt = 0, aldpaid = 0, oldgross = 0, oldnet = 0, gsdiff = 0, newgross = 0, diffarrs = 0, diffgross = 0, newnet = 0, diffnet = 0;
            ds = objpay.GetArrearsAmount(Convert.ToInt32(ddlArrears.SelectedValue), Convert.ToInt32(ddlEmp.SelectedValue), ddlMonth.SelectedItem.Text, payhead);
            if (ds.Tables[0].Rows.Count > 0)
            {
                amt = Convert.ToDouble(ds.Tables[0].Rows[0]["PAYHEAD"].ToString());
                txttobePaid.Text = amt.ToString();
            }

            DataSet dsactual = null;

            dsactual = objpay.GetActualAmount(Convert.ToInt32(ddlEmp.SelectedValue), ddlMonth.SelectedItem.Text, payhead);
            if (dsactual.Tables[0].Rows.Count > 0)
            {

                aldpaid = Convert.ToDouble(dsactual.Tables[0].Rows[0]["PAYHEAD_ACTUAL"].ToString());
                txtAlrdyPaid.Text = aldpaid.ToString();
            }
            oldgross = Convert.ToDouble(dsactual.Tables[0].Rows[0]["GS"]);
            txtOldGross.Text = oldgross.ToString();
            oldnet = Convert.ToDouble(dsactual.Tables[0].Rows[0]["NET_PAY"]);
            txtOldRate.Text = oldnet.ToString();

            gsdiff = Convert.ToDouble(dsactual.Tables[0].Rows[0]["GSDIFF"]);
            newgross = amt + gsdiff;
            txtNwGross.Text = newgross.ToString();

            diffarrs = amt - aldpaid;
            txtDiffArrears.Text = diffarrs.ToString();

            diffgross = newgross - oldgross;
            txtDiffGross.Text = diffgross.ToString();

            newnet = newgross;
            txtNwRate.Text = newnet.ToString();

            diffnet = newnet - oldnet;
            txtDiffNet.Text = diffnet.ToString();

        }
        catch (Exception ex)
        {

        }
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ARREARS INCREMENT DIFFERENCE", "Pay_IncrementArrears_Report_New.rpt");
    }
    //Function to show report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_ARREARNO=" + Convert.ToInt32(ddlArrears.SelectedValue) + ",@P_IDNO=" + Convert.ToInt32(ddlEmployee.SelectedValue) + ",@P_COLLEGE_CODE=33";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NoReport, Common.MessageType.Error);
        }
    }


    protected void btnTransfer_Click(object sender, EventArgs e)
    {

        if (ddlCollege.SelectedValue == "0")
        {
            objCommon.DisplayMessage("Please select College from the list", this.Page);
        }
        else
        {
            pnlinfo.Visible = false;
            pnlsupl.Visible = true;
            FillSupllimentaryHead();
        }



    }
    //Populate SupplementaryBillHead DropDownList
    private void FillSupllimentaryHead()
    {

        try
        {
            objCommon.FillDropDownList(ddlSuplBillHead, "PAYROLL_SUPPLIMENTARY_HEAD", "SUPLHNO", "SUPLHEAD", "", "SUPLHNO");
            //objCommon.FillDropDownList(ddlsuplarrear, "PAYROLL_ARREARS A,PAYROLL_STAFF C", "ARNO", "CAST(PER AS NVARCHAR(25) )+'%'+ ' ; ' + CAST(DateName( month , DateAdd( month , Datepart(month,AFRM) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,AFRM) AS NVARCHAR(25))+' '+'TO'+' '+ CAST(DateName( month , DateAdd( month , Datepart(month,ATO) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,ATO) AS NVARCHAR(25))+' ; '+STAFF", "C.STAFFNO = A.COLLEGENO", "");

        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "PayRoll_Transactions_Pay_SupplimentaryBill.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
            objCommon = new Common();
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);

        }
    }
    protected void btnSuplBack_Click(object sender, EventArgs e)
    {
        pnlsupl.Visible = false;
        pnlinfo.Visible = true;
    }
    protected void btnSuplSave_Click(object sender, EventArgs e)
    {
        int suplarrearno, suplbillHeadno, supplarnocount;
        string suplordno, arrearname, suplmon, suplheadname;
        DateTime supldate;
        suplordno = txtSuplOrderNo.Text;
        suplarrearno = Convert.ToInt32(ddlsuplarrear.SelectedValue);
        arrearname = ddlsuplarrear.SelectedItem.Text;
        suplbillHeadno = Convert.ToInt32(ddlSuplBillHead.SelectedValue);
        suplheadname = ddlSuplBillHead.SelectedItem.Text;
        supldate = Convert.ToDateTime(txtSupldate.Text);
        suplmon = Convert.ToDateTime(txtSupldate.Text).ToString("MMM").ToUpper() + Convert.ToDateTime(txtSupldate.Text).ToString("yyyy").ToUpper();

        supplarnocount = Convert.ToInt32(objCommon.LookUp("PAYROLL_SUPPLIMENTARY_BILL", "COUNT(ARNO)", "ARNO=" + suplarrearno));

        if (supplarnocount == 0)
        {

            CustomStatus cs = (CustomStatus)objpay.TransferToSuplBill(suplarrearno, suplordno, supldate, suplbillHeadno, suplheadname, suplmon);
            Clear();
            MessageBox("Transfer to Supl. Bill Successfully");
        }
        else
        {
            CustomStatus cs1 = (CustomStatus)objpay.DeleteSupplBillTransferRecord(suplarrearno);
            CustomStatus cs = (CustomStatus)objpay.TransferToSuplBill(suplarrearno, suplordno, supldate, suplbillHeadno, suplheadname, suplmon);
            Clear();
            MessageBox("Overwrite Supl. Bill Successfully");
        }

    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnSuplCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public void Clear()
    {
        ddlsuplarrear.SelectedIndex = 0;
        txtSuplOrderNo.Text = string.Empty;
        txtSupldate.Text = string.Empty;
        ddlSuplBillHead.SelectedIndex = 0;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int supltransfer;
        if (ddlArrears.SelectedIndex > 0)
        {
            supltransfer = Convert.ToInt32(objCommon.LookUp("PAYROLL_SUPPLIMENTARY_BILL", "COUNT(ARNO)", "ARNO=" + ddlArrears.SelectedValue));
            if (supltransfer == 0)
            {
                int arnodelete = Convert.ToInt32(ddlArrears.SelectedValue);
                CustomStatus cs = (CustomStatus)objpay.DeleteArrearsRecord(arnodelete);
                MessageBox("Arrears Record Deleted Successfully!");
            }
            else
            {
                MessageBox("Can not Delete. Already Transfer to Supplimentary Bill.");
            }
        }
        else
        {
            MessageBox("Please Select Arrears");
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlArrears.SelectedIndex = 0;

        txtGovOrdNo.Text = string.Empty;
        txtGovDt.Text = string.Empty;
        txtOffOrdNo.Text = string.Empty;
        txtOffDt.Text = string.Empty;
        ddlEmp.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        txtBasic.Text = string.Empty;
        txtPay.Text = string.Empty;
        txttobePaid.Text = string.Empty;
        txtAlrdyPaid.Text = string.Empty;
        txtDiffArrears.Text = string.Empty;
        txtGradePay.Text = string.Empty;
        txtNwGross.Text = string.Empty;
        txtOldGross.Text = string.Empty;
        txtDiffGross.Text = string.Empty;
        txtNwRate.Text = string.Empty;
        txtOldRate.Text = string.Empty;
        txtDiffNet.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtNoofRecords.Text = string.Empty;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlArrears, "PAYROLL_ARREARS A,PAYROLL_STAFF C", "ARNO", "CAST(PER AS NVARCHAR(25) )+'%'+ ' ; ' + CAST(DateName( month , DateAdd( month , Datepart(month,AFRM) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,AFRM) AS NVARCHAR(25))+' '+'TO'+' '+ CAST(DateName( month , DateAdd( month , Datepart(month,ATO) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,ATO) AS NVARCHAR(25))+' ; '+STAFF", "C.STAFFNO = A.COLLEGENO  AND A.COLLEGE_NO=" + ddlCollege.SelectedValue + " ", "");
        //objCommon.FillDropDownList(ddlsuplarrear, "PAYROLL_ARREARS A,PAYROLL_STAFF C", "ARNO", "CAST(PER AS NVARCHAR(25) )+'%'+ ' ; ' + CAST(DateName( month , DateAdd( month , Datepart(month,AFRM) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,AFRM) AS NVARCHAR(25))+' '+'TO'+' '+ CAST(DateName( month , DateAdd( month , Datepart(month,ATO) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,ATO) AS NVARCHAR(25))+' ; '+STAFF", "C.STAFFNO = A.COLLEGENO", "");
        objCommon.FillDropDownList(ddlsuplarrear, "PAYROLL_ARREARS A,PAYROLL_STAFF C", "ARNO", "CAST(PER AS NVARCHAR(25) )+'%'+ ' ; ' + CAST(DateName( month , DateAdd( month , Datepart(month,AFRM) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,AFRM) AS NVARCHAR(25))+' '+'TO'+' '+ CAST(DateName( month , DateAdd( month , Datepart(month,ATO) , 0 ) - 1 ) AS NVARCHAR(25))+' '+ CAST(Datepart(YEAR,ATO) AS NVARCHAR(25))+' ; '+STAFF", "C.STAFFNO = A.COLLEGENO  AND A.COLLEGE_NO=" + ddlCollege.SelectedValue + " ", "");
    }
}