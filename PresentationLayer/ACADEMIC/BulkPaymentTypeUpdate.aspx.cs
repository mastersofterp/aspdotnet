﻿//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BULK PAYMENT TYPE UPDATE                                   
// CREATION DATE : 28-AUGUST-2015
// ADDED BY      : MR. MANISH CHAWADE
// ADDED DATE    : 
// MODIFIED BY   : 
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
using System.Net;

public partial class HOSTEL_BulkPaymentTypeUpdate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RecieptTypeController objRC = new RecieptTypeController();

    #region Page Load
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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                FillDropDownList();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkPaymentTypeUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkPaymentTypeUpdate.aspx");
        }
    }
    #endregion

    #region Private Methods
    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddladmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
    }

    private void ShowStudents()
    {
        try
        {
            int branchno = 0;
            int admbatch = Convert.ToInt32(ddladmbatch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddladmbatch.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDegree.SelectedValue);
            if (Convert.ToInt32(ddlDegree.SelectedValue) == 0)
            {
                branchno = 0;
            }
            else
            {
                branchno = Convert.ToInt32(ddlBranch.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlBranch.SelectedValue);
            }

            DataSet ds = objRC.GetStudentsForUpdatePaymentType(admbatch, degreeno, branchno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnUpdate.Enabled = true;
                lvPaymenttype.DataSource = ds;
                lvPaymenttype.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvPaymenttype);//Set label - 
            }
            else
            {
                btnUpdate.Enabled = false;
                lvPaymenttype.DataSource = null;
                lvPaymenttype.DataBind();
                objCommon.DisplayMessage("Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region Page Events
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowStudents();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "acd_scheme a inner join acd_branch b on (a.branchno=b.branchno)", "distinct a.branchno", "b.longname", "DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
        }

        lvPaymenttype.DataSource = null;
        lvPaymenttype.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string studids = string.Empty;
            string ptype = string.Empty;
            foreach (ListViewDataItem items in lvPaymenttype.Items)
            {
                if (Convert.ToInt32((items.FindControl("ddlPaymentType") as DropDownList).SelectedValue) > 0)
                {
                    studids += (items.FindControl("hdfIdno") as HiddenField).Value + "$";
                    ptype += (items.FindControl("ddlPaymentType") as DropDownList).SelectedValue + "$";
                }
            }
            if (studids.Length <= 0 && ptype.Length <= 0)
            {
                objCommon.DisplayMessage(this.pnlStudentList, "Please Select Payment Type.", this.Page);
                return;
            }

            if (objRC.UpdatePaymenttypeofStudents(studids.TrimEnd('$'), ptype.TrimEnd('$')) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                ShowStudents();
                objCommon.DisplayMessage(this.pnlStudentList, "Payment Updated Successfully.", this.Page);
            }
            else
                objCommon.DisplayMessage(this.pnlStudentList, "Server Error.", this.Page);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void lvPaymenttype_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlPaymentType = e.Item.FindControl("ddlPaymentType") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlPaymentType.Items.Add(new ListItem(dtr["PAYTYPENAME"].ToString(), dtr["PAYTYPENO"].ToString()));
                }
            }
            ddlPaymentType.SelectedValue = ddlPaymentType.ToolTip;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
}
