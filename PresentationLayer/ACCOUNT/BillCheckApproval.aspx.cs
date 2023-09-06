//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :Bill Check Approval                                                  
// CREATION DATE :05-DEC-2018                                              
// CREATED BY    :Nokhlal Kumar                                       
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
//using System.Windows;
//using System.Windows.Forms;
using IITMS;
using IITMS.NITPRM;

public partial class ACCOUNT_BillCheckApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                // CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                txtApprovalDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                //BillCheckList();

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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    //private void BillCheckList()
    //{
    //    DataSet ds = null;
    //    DataTable dt = new DataTable();

    //    DataColumn dc = new DataColumn();
    //    dc.ColumnName = "SLNO";
    //    dt.Columns.Add(dc);

    //    DataColumn dc1 = new DataColumn();
    //    dc1.ColumnName = "COMP_CODE";
    //    dt.Columns.Add(dc1);

    //    DataColumn dc2 = new DataColumn();
    //    dc2.ColumnName = "ID";
    //    dt.Columns.Add(dc2);

    //    DataColumn dc3 = new DataColumn();
    //    dc3.ColumnName = "BILL_NO";
    //    dt.Columns.Add(dc3);

    //    DataColumn dc4 = new DataColumn();
    //    dc4.ColumnName = "VOUCHER_NO";
    //    dt.Columns.Add(dc4);

    //    DataColumn dc5 = new DataColumn();
    //    dc5.ColumnName = "NAME";
    //    dt.Columns.Add(dc5);

    //    DataColumn dc6 = new DataColumn();
    //    dc6.ColumnName = "NATURE_SERVICE";
    //    dt.Columns.Add(dc6);

    //    DataColumn dc7 = new DataColumn();
    //    dc7.ColumnName = "AMOUNT";
    //    dt.Columns.Add(dc7);

    //    DataColumn dc8 = new DataColumn();
    //    dc8.ColumnName = "DEPARTMENT";
    //    dt.Columns.Add(dc8);

    //    DataColumn dc9 = new DataColumn();
    //    dc9.ColumnName = "APR_LETTER";
    //    dt.Columns.Add(dc9);

    //    try
    //    {
    //        ds = objRPBController.BillCheckList();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dt = ds.Tables[0];
    //            double Totalamt = 0;
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                Totalamt = Totalamt + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
    //            }

    //            ViewState["Totalamt"] = Totalamt.ToString();
    //            //lblTotalAmt.Text = Totalamt.ToString();
    //            //DataRow row;
    //            //row = dt.NewRow();
    //            //row["SLNO"] = 0;
    //            //row["COMP_CODE"] = "";
    //            //row["ID"] = "0";
    //            //row["BILL_NO"] = "0";
    //            //row["VOUCHER_NO"] = "0";
    //            //row["NAME"] = "";
    //            //row["NATURE_SERVICE"] = "Total";
    //            //row["AMOUNT"] = Totalamt.ToString();
    //            //row["DEPARTMENT"] = "";
    //            //row["APR_LETTER"] = "";
    //            //dt.Rows.Add(row);

    //            lvBillCheck.DataSource = ds.Tables[0];
    //            lvBillCheck.DataBind();
    //        }
    //        else
    //        {
    //            lvBillCheck.DataSource = null;
    //            lvBillCheck.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int count = 0, saved = 0;
        //foreach (ListViewDataItem lvItem in lvBillCheck.Items)
        //{
        //    CheckBox chkBillid = lvItem.FindControl("chkBillid") as CheckBox;
        //    TextBox txtCheckNo = lvItem.FindControl("txtCheckNo") as TextBox;
        //    if (chkBillid.Checked)
        //    {
        //        count++;
        //        if (txtCheckNo.Text == "" || txtCheckNo.Text == string.Empty)
        //        {
        //            objCommon.DisplayMessage(updChkBill, "Please Enter Cheque number for selected record.", this.Page);
        //            return;
        //        }
        //    }
        //    else if (chkBillid.Checked == false)
        //    {
        //        if (txtCheckNo.Text.Length > 0)
        //        {
        //            objCommon.DisplayMessage(updChkBill, "Please select the records for given check number :- " + txtCheckNo.Text, this.Page);
        //            return;
        //        }
        //    }
        //}

        //if (count == 0)
        //{
        //    objCommon.DisplayMessage(updChkBill, "Please Select atleast one record...!", this.Page);
        //    return;
        //}

        //foreach (ListViewDataItem lvitem in lvBillCheck.Items)
        //{
        //    CheckBox chkBillid = lvitem.FindControl("chkBillid") as CheckBox;
        //    HiddenField hdnBillNo = lvitem.FindControl("hdnBillNo") as HiddenField;
        //    Label lblName = lvitem.FindControl("lblName") as Label;
        //    HiddenField hdnCompCode = lvitem.FindControl("hdnCompCode") as HiddenField;
        //    Label lblSlNo = lvitem.FindControl("lblSlNo") as Label;
        //    Label lblVoucherNo = lvitem.FindControl("lblVoucherNo") as Label;
        //    Label lblNatureService = lvitem.FindControl("lblNatureService") as Label;
        //    Label lblAmount = lvitem.FindControl("lblAmount") as Label;
        //    Label lblDept = lvitem.FindControl("lblDept") as Label;
        //    Label lblAprLetter = lvitem.FindControl("lblAprLetter") as Label;
        //    TextBox txtCheckNo = lvitem.FindControl("txtCheckNo") as TextBox;

        //    if (chkBillid.Checked && txtCheckNo.Text != string.Empty)
        //    {
        //        ObjRPB.RAISE_PAY_NO = Convert.ToInt32(chkBillid.ToolTip.ToString());
        //        ObjRPB.SERIAL_NO = Convert.ToInt32(hdnBillNo.Value.ToString());
        //        ObjRPB.PAYEE_NAME_ADDRESS = lblName.Text.ToString();
        //        ObjRPB.COMPANY_CODE = hdnCompCode.Value.ToString();
        //        ObjRPB.NATURE_SERVICE = lblNatureService.Text.ToString();
        //        ObjRPB.TOTAL_BILL_AMT = Convert.ToDouble(lblAmount.Text.ToString());
        //        //ObjRPB.DEPT_ID = Convert.ToInt32(lblDept.Text.ToString());
        //        ObjRPB.REMARK = lblAprLetter.Text.ToString();
        //        string dttime = Convert.ToDateTime(txtApprovalDate.Text).ToString("dd-MM-yyyy");
        //        ObjRPB.APPROVAL_DATE = Convert.ToDateTime(dttime.ToString());

        //        string aprchkno = lblBillCheckNo.Text.ToString();
        //        string checkno = txtCheckNo.Text.ToString();
        //        string BankAccNo = lblAccountno.Text.ToString();
        //        int voucherno = Convert.ToInt32(lblVoucherNo.Text.ToString());
        //        string deptname = lblDept.Text.ToString();
        //        int deptno = Convert.ToInt32(objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT = '" + deptname + "'"));
        //        ObjRPB.DEPT_ID = Convert.ToInt32(deptno.ToString());

        //        //int objret = objRPBController.InsertCheckBillApproval(ObjRPB, voucherno, checkno, BankAccNo, aprchkno);

        //        //if (objret == 1)
        //        //{
        //        //    saved++;
        //        //}
        //    }
        //}
        //if (saved > 0)
        //{
        //    objCommon.DisplayMessage(updChkBill, "Bill Cheque Approved Successfully!", this.Page);
        //    BillCheckList();
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/Account/BillCheckApproval.aspx");
        //BillCheckList();
    }
    protected void lvBillCheck_DataBound(object sender, EventArgs e)
    {
        //Label lbl = (Label)lvBillCheck.FindControl("lblTotalAmt");
       // lbl.Text = String.Format("{0:0.00}", Convert.ToDouble(ViewState["Totalamt"].ToString()));
    }
}