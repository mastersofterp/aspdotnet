using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ACADEMIC_DegreeDocumentMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmission objAd = new OnlineAdmission();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
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
            if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                pnlListView.Visible = true;
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    ViewState["Action"] = "add";
                PopulatedropDown();
            }
            BindList();
        }
    }

    private void BindList()
    {
        try
        {
            OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
            DataSet ds = Admcontroller.GetDegreeDoc_AndByDocNo(0);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDegreeMap.DataSource = ds;
                lvDegreeMap.DataBind();
                pnlListView.Visible = true;
                lvDegreeMap.Visible = true;
            }
            else
            {
                lvDegreeMap.DataSource = null;
                lvDegreeMap.DataBind();
                lvDegreeMap.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void PopulatedropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "COLLEGE_CODE>0", "DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlDegree.SelectedIndex = 0;
        txtDoc.Text = string.Empty;
        chkActive.Checked = false;
        ViewState["action"] = null;
        ViewState["DOC_NO"] = null;
        chkMand.Checked = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string docType=txtDoc.Text.ToString().Trim();
            int activeStatus = chkActive.Checked == true ? 1 : 0;
             string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
             int mandatory = chkMand.Checked == true ? 1 : 0;
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objAd.DEGREE_NO = Convert.ToInt32(Session["SPEC_NO"]);
                CustomStatus cs = (CustomStatus)Admcontroller.UpdateDegreeDoc(Convert.ToInt32(ViewState["DOC_NO"]), Convert.ToInt32(ddlDegree.SelectedValue)
               ,docType, Convert.ToInt32(Session["userno"]), ipAddress, activeStatus,mandatory);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    clear();
                    BindList();
                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)Admcontroller.AddDegreeDoc(Convert.ToInt32(ddlDegree.SelectedValue), docType,
                Convert.ToInt32(Session["userno"]), ipAddress, activeStatus,mandatory);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    clear();
                    BindList();
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                }
                else
                {
                    clear();
                    objCommon.DisplayMessage(this.Page, "Record Already Exist.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int docNo = int.Parse(btnEdit.CommandArgument);
            ViewState["DOC_NO"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails();
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails()
    {
        DataSet ds = null;
        try
        {
            ds = Admcontroller.GetDegreeDoc_AndByDocNo(Convert.ToInt32(ViewState["DOC_NO"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["SPEC_NO"] = specNo;   
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                txtDoc.Text = ds.Tables[0].Rows[0]["DOC_TYPE"].ToString();
                string activeStatus = ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
                if (Convert.ToInt32(activeStatus) == 1)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                string mandatoryStatus = ds.Tables[0].Rows[0]["IS_MANDATORY"].ToString();
                if (Convert.ToInt32(mandatoryStatus) == 1)
                {
                    chkMand.Checked = true;
                }
                else
                {
                    chkMand.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}