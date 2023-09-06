using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;

public partial class ACADEMIC_Receipt_Details : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                   this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    objCommon.FillDropDownList(ddlReceipt, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", string.Empty, "RECIEPT_TITLE");
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

                }
            }
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Receipt_Details.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Receipt_Details.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Receipt_Details.aspx");
        }
    }
    protected void ddlReceipt_TextChanged(object sender, EventArgs e)
    {
        GetStudentsList();
    }
    
    string Receipt_Code=string.Empty;
    public void GetStudentsList()
    {
        try
        {
            DataSet ds = feeController.Get_Paid_Students_List(ddlReceipt.SelectedValue);
            Receipt_Code = ddlReceipt.SelectedValue;
            ViewState["Receipt_Code"] = Receipt_Code;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                divListView.Visible = true;
                lvStudList.DataSource = ds;
                lvStudList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Receipt_Details.GetStudentsList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void btnReceiptInfo_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkButton = sender as LinkButton;
            int Idno = int.Parse(lnkButton.CommandArgument);
            ViewState["Idno"] = Idno;
            Receipt_Code =Convert.ToString(ViewState["Receipt_Code"]);
            this.ShowReceiptInfo(Idno,Receipt_Code);
            //OPEN POPUP
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Receipt_Details.btnReceiptInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReceiptInfo(int Idno, string Receipt_Code)
    {
        try
        {
            //Receipt_Code =Convert.ToString(ViewState["Receipt_Code"]);
            DataSet ds = feeController.GetReceiptInfo(Idno, Receipt_Code);


            // Create new DataTable and DataSource objects.
            DataTable dtPaidFees = new DataTable();

            dtPaidFees.Columns.Add("Semster", typeof(string));
            dtPaidFees.Columns.Add("REC_NO", typeof(string));
            dtPaidFees.Columns.Add(new DataColumn("REC_DATE", typeof(DateTime)));
            dtPaidFees.Columns.Add("APPLIED_AMT", typeof(string));
            dtPaidFees.Columns.Add("PAID_AMT", typeof(float));
            dtPaidFees.Columns.Add("BAL_AMT", typeof(float));
            double TOTALPAID_AMT = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtPaidFees.NewRow();  
                    if (i > 0)
                    {
                        if (ds.Tables[0].Rows[i - 1]["SEMESTERNAME"].ToString() == ds.Tables[0].Rows[i]["SEMESTERNAME"].ToString())
                        {
                            dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                            dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                            dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                            dr["APPLIED_AMT"] = dtPaidFees.Rows[i - 1]["BAL_AMT"];
                            //dr["APPLIED_AMT"] = ds.Tables[0].Rows[i - 1]["BAL_AMT"].ToString();
                            dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                            dr["BAL_AMT"] = Convert.ToDouble(ds.Tables[0].Rows[i]["APPLIED_AMOUNT"]) - (this.TOTALPAID_AMOUNT(i, ds, ds.Tables[0].Rows[i]["SEMESTERNAME"].ToString())); //(Convert.ToDouble(ds.Tables[0].Rows[i - 1]["PAID_AMOUNT"]) + Convert.ToDouble(ds.Tables[0].Rows[i]["PAID_AMOUNT"]));
                        }
                        else
                        {
                            dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                            dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                            dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                            dr["APPLIED_AMT"] = ds.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                            dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                            dr["BAL_AMT"] = ds.Tables[0].Rows[i]["BAL_AMT"];
                        }
                    }
                    else
                    {
                        dr["Semster"] = ds.Tables[0].Rows[i]["SEMESTERNAME"];
                        dr["REC_NO"] = ds.Tables[0].Rows[i]["REC_NO"];
                        dr["REC_DATE"] = ds.Tables[0].Rows[i]["REC_DT"];
                        dr["APPLIED_AMT"] = ds.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                        dr["PAID_AMT"] = ds.Tables[0].Rows[i]["PAID_AMOUNT"];
                        dr["BAL_AMT"] = ds.Tables[0].Rows[i]["BAL_AMT"];
                    }
                    dtPaidFees.Rows.Add(dr);  
                }


                lvReceipt.DataSource = dtPaidFees;
                lvReceipt.DataBind();
            }
            else
            {
                lvReceipt.DataSource = null;
                lvReceipt.DataBind();
            }

            //foreach (ListViewDataItem item in lvReceipt.Items)
            //{
            //    Label lblApply = item.FindControl("lblAppliedAmount") as Label;
            //    Label lblPaid = item.FindControl("lblPaidAmount") as Label;
            //    Label lblBal = item.FindControl("lblBalanceAmount") as Label;
            //    double lblamount =convert.ToDouble(lblApply.Text
            //    if (lblApply.Text == lblPaid.Text)
            //    {
            //        lblBal.Text=Convert.ToString();
            //    }

            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Receipt_Details.ShowReceiptInfo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private double TOTALPAID_AMOUNT(int I,DataSet dstot,string semester)
    {
        double totpaid_amt = 0;
        for (int j = 0; j <= I; j++)
        {
            if (dstot.Tables[0].Rows[j]["SEMESTERNAME"].ToString() == semester)
            {
                totpaid_amt += Convert.ToDouble(dstot.Tables[0].Rows[j]["PAID_AMOUNT"]);
            }
        }
        return totpaid_amt;
    }

}