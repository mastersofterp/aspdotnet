﻿using System;
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
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ESTABLISHMENT_ServiceBook_Pay_IPRIssuing : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();

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
        if (!this.Page.IsPostBack)
        {
            this.Form.Attributes.Add("onLoad()", "msg()");
            if (this.Session["userno"] == null || this.Session["username"] == null || this.Session["usertype"] == null || this.Session["userfullname"] == null)
            {
                this.Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                ViewState["action"] = "add";
                BindListView();
            }
        }
        ScriptManager.RegisterStartupScript((Page)this, this.GetType(), "onLoad", "onLoad();", true);

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceBook serviceBook = new ServiceBook();
            serviceBook.IPRIssuingAgency = this.txtIPRIssuing.Text.Trim();
            serviceBook.ACTIVESTATUS = !this.cbuIPRIssuing.Checked ? "DeActive" : "Active";
            serviceBook.UCOLLEGE_CODE = this.Session["colcode"].ToString();
            if (this.ViewState["action"] == null)
                return;
            if (this.ViewState["action"].ToString().Equals("add"))
            {
                serviceBook.IPRNOAGNO = 0;
                if (((CustomStatus)this.objServiceBook.AddIPRAgency(serviceBook)).Equals((object)CustomStatus.RecordSaved))
                {
                    BindListView();
                    objCommon.DisplayMessage((Control)this.updpanel, "Record Saved Successfully", this.Page);
                    Clear();
                    BindListView();
                }
                else
                {
                    Clear();
                    BindListView();
                    MessageBox("Record Already Exist");
                }
            }
            else
            {
                if (this.ViewState["IPRNOAGNO"] == null)
                    return;
                serviceBook.IPRNOAGNO = Convert.ToInt32(this.ViewState["IPRNOAGNO"].ToString());
                int IPRNOAGNO = serviceBook.IPRNOAGNO;
                if (((CustomStatus)this.objServiceBook.AddIPRAgency(serviceBook)).Equals((object)CustomStatus.RecordUpdated))
                {
                    this.objCommon.DisplayMessage((Control)this.updpanel, "Record Updated Successfully", this.Page);
                    ViewState["action"] = (object)"add";
                    Clear();
                    BindListView();
                }
                else
                {
                    Clear();
                    MessageBox("Record Already Exist");
                    BindListView();
                }
            }

        }
        catch (Exception ex)
        {
        }

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
        }
    }


    private void BindListView()
    {
        try
        {
            ServiceBookController objServiceBook = new ServiceBookController();
            DataSet university = objServiceBook.GetIIssuingAgency();

            if (university.Tables[0].Rows.Count > 0)
            {
                lvIPRCategory.DataSource = university;
                lvIPRCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.BindListViewQualification-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        try
        {
            txtIPRIssuing.Text = "";
            cbuIPRIssuing.Checked = false;
            ViewState["IPRNOAGNO"] = null;
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.ShowDetails(int.Parse((sender as ImageButton).CommandArgument));
            this.ViewState["action"] = (object)"edit";
        }
        catch (Exception ex)
        {
        }

    }

    private void ShowDetails(int IPRNOAGNO)
    {
        DataSet dataSet = (DataSet)null;
        try
        {
            dataSet = this.objServiceBook.GetSingleIPRAgency(IPRNOAGNO);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return;
            this.ViewState["IPRNOAGNO"] = IPRNOAGNO.ToString();
            this.txtIPRIssuing.Text = dataSet.Tables[0].Rows[0]["IPRIssuing_Agency"].ToString();
            if (dataSet.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                this.cbuIPRIssuing.Checked = true;
            else
                this.cbuIPRIssuing.Checked = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(this.Session["error"]))
                this.objUCommon.ShowError(this.Page, "PayRoll_Pay_Scale.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                this.objUCommon.ShowError(this.Page, "Server UnAvailable");
        }
        finally
        {
            dataSet.Clear();
            dataSet.Dispose();
        }
    }
}