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
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using NonAcadBusinessLogicLayer.BusinessEntities;
using NonAcadBusinessLogicLayer.BusinessEntities.Account;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using IITMS.NITPRM;
using BusinessLayer.BusinessEntities;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic.Account;




public partial class ACCOUNT_Acc_Funding_Agency : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Funding_Agency_Entity objFAE = new Funding_Agency_Entity();
    Acc_Funding_Agency_Controller objFAEController = new Acc_Funding_Agency_Controller();

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
               
            }

            BindListView();
        }
        ScriptManager.RegisterStartupScript((Page)this, this.GetType(), "onLoad", "onLoad();", true);
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


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objFAE.FAGENCY = txtfunding.Text;
            objFAE.AGENCYID = 0;
           
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                   // DataSet ds = objCommon.FillDropDown("ACC_FUNDING_AGENCY", "*", "", "FUNDING_AGENCY='" + txtfunding.Text + "'", "");
                    DataSet ds = objCommon.FillDropDown("ACC_FUNDING_AGENCY", "*", "", "FUNDING_AGENCY='" + txtfunding.Text + "' and AGENCY_ID!='" + Convert.ToInt32(ViewState["AgencyId"]) + "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        MessageBox("Sorry! Record Already Exists");
                        Clear();
                        return;
                      
                    }
                    CustomStatus cs = (CustomStatus)objFAEController.AddFunding(objFAE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        Clear();
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        MessageBox("Record Saved Successfully");
                        
                    }

                }
                else
                {
                    objFAE.AGENCYID = Convert.ToInt32(ViewState["AgencyId"]);
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        DataSet ds = objCommon.FillDropDown("ACC_FUNDING_AGENCY", "FUNDING_AGENCY", "", "FUNDING_AGENCY='" + txtfunding.Text + "' and AGENCY_ID!='" + Convert.ToInt32(ViewState["AgencyId"]) + "'", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            MessageBox("Sorry! Record Already Exists");
                            Clear();
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objFAEController.AddFunding(objFAE);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                          
                            MessageBox("Record Updated Successfully");
                            BindListView();
                            Clear();
                            ViewState["action"] = null;
                            btnsubmit.Visible = true;
                            btncancel.Visible = true;                         
                            //objCommon.DisplayMessage("Record Updated Sucessfully", this.Page);

                       }
                    
                    }                
                }
            }
        }
        catch
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            { 
            }
           // objUCommon.ShowError(Page, "ACCOUNT_Master_Account_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }

    private void Clear()
    {
        txtfunding.Text = string.Empty;
        ViewState["action"] = null;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["AgencyId"] = int.Parse(btnEdit.CommandArgument);


           DataSet ds = objCommon.FillDropDown("ACC_FUNDING_AGENCY", "FUNDING_AGENCY", "", "AGENCY_ID='" + Convert.ToInt32(ViewState["AgencyId"]) + "'","");

           txtfunding.Text = ds.Tables[0].Rows[0]["FUNDING_AGENCY"].ToString();

           ViewState["action"] = "edit";
            
        }
        catch(Exception ex)
        { 
        
        }
    }

    protected void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACC_FUNDING_AGENCY", "AGENCY_ID,FUNDING_AGENCY", "", "", "AGENCY_ID");

            lvFundingAgency.DataSource = ds;
            lvFundingAgency.DataBind();
            pnlList.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_Account_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"]="add";
    }
}