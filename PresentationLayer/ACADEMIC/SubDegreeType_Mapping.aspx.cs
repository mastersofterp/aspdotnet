// Added by Pooja Sandel on 04/02/2022
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


public partial class ACADEMIC_SubDegreeType_Mapping : System.Web.UI.Page
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
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //Fill DropDownLists


                    // this.BindListView();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["Action"] = "add";
                PopulatedropDown();
                BindListView();
            }

        }
    }



    private void BindListView()
    {

        try
        {
            OnlineAdmissionController Admcontroller = new OnlineAdmissionController();

            DataSet ds = Admcontroller.GetAllAdmission();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvOAdmission.DataSource = ds;
                lvOAdmission.DataBind();
                pnlListView.Visible = true;
                lvOAdmission.Visible = true;
            }
            else
            {
                lvOAdmission.DataSource = null;
                lvOAdmission.DataBind();
                lvOAdmission.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineAdmission.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void PopulatedropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDtype ,"ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
             objCommon.FillDropDownList(ddlDtype, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");
             objCommon.FillDropDownList(ddlSubtype, "ACD_SUB_DEGREE_TYPE", "SUB_DEGREE", "SUB_DEGREE_NAME", "COLLEGE_CODE>0", "SUB_DEGREE");
             objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "ACTIVESTATUS=1", "DEGREENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineAdmission.PopulatedropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }

    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlDtype.SelectedIndex = 0;
        ddlSubtype.SelectedIndex = 0;      
        ddldegree.SelectedIndex=0;
        txtDcode.Text = string.Empty;
        ViewState["action"] = "add";
               
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
            IITMS.UAIMS.BusinessLayer.BusinessEntities.OnlineAdmission objAd = new IITMS.UAIMS.BusinessLayer.BusinessEntities.OnlineAdmission();
            objAd.DEGREE_NO = Convert.ToInt32(ddlDtype.SelectedValue);
            objAd.DEGREE_TYPE = Convert.ToInt32(ddlDtype.SelectedValue);
            objAd.SUBDEGREE_TYPE = Convert.ToInt32(ddlSubtype.SelectedValue);
            objAd.DEGREE = ddldegree.SelectedValue;
            objAd.DEGREE_CODE = Convert.ToInt32(txtDcode.Text.Trim());
            objAd.CREATED_BY = 1;
            objAd.IP_ADDRESS = "::1";


            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit
                objAd.DEGREE_NO = Convert.ToInt32(Session["DEGREE_NO"]);
                CustomStatus cs = (CustomStatus)Admcontroller.UpdateOAdm(objAd);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    clear();                  
                    BindListView();
                    objCommon.DisplayMessage(this.Page, "Record Updated successfully.", this.Page);                  
                    //clear();
                }
            }
            else
            {

                //Add New

                CustomStatus cs = (CustomStatus)Admcontroller.AddOAdm(objAd);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    clear();
                    BindListView();
                    objCommon.DisplayMessage(this.Page, "Record added successfully.", this.Page);
                   // ViewState["Action"] = "add";
                }
                else
                {
                    //msgLbl.Text = "Record already exist";
                    objCommon.DisplayMessage(this.Page, "Record already exist.", this.Page);
                }
                //else if (cs.Equals(CustomStatus.TransactionFailed))                    
                //{
                //    objCommon.DisplayMessage(this.updSession, "Transaction Failed", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineAdmission.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            ImageButton btnEdit = sender as ImageButton;
            int DEGREENO = int.Parse(btnEdit.CommandArgument);
            Session["DEGREE_NO"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(DEGREENO);
            //ViewState["edit"] = "add";
            ViewState["action"] = "edit";
         

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    private void ShowDetails(Int32 DEGREE_NO)
    {
        DataSet ds = null;
        try
        {
            ds = Admcontroller.GetSingleOAdm(DEGREE_NO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DEGREE_NO"] = DEGREE_NO;

                //  objCommon.FillDropDownList(ddlstate, "ACD_STATE_TASK", "StateId", "State", "", "");
                //PopulatedropDown();
                ddlDtype.SelectedValue = ds.Tables[0].Rows[0]["DEGREE_TYPE"].ToString();

                ddlSubtype.SelectedValue = ds.Tables[0].Rows[0]["SUBDEGREE_TYPE"].ToString();
               // ddldegree.SelectedItem.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                ddldegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                txtDcode.Text = ds.Tables[0].Rows[0]["DEGREE_CODE"].ToString();


            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineAdmission.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
}