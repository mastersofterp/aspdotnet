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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;

public partial class ACADEMIC_EmailConfiguration : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //CheckPageAuthorization();
                if (Session["usertype"].Equals(1))
                {
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=AffiliatedFeesCategory.aspx");
                }

            }
            this.FillDropdown();
            BindListview();
            ViewState["action"] = "add";
        }
        divMsg.InnerHtml = string.Empty;
        
    }



    private void FillDropdown()
    {
        //objCommon.FillDropDownList(ddlModule, "ACC_SECTION", "AS_NO", "AS_TITLE", "AS_NO > 0", "AS_NO");
        DataSet ds = objCommon.FillDropDown("ACC_SECTION", "AS_NO", "AS_TITLE", "AS_NO > 0", "AS_NO");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlModule.Items.Clear();
                ddlModule.Items.Add("Please Select");
                ddlModule.SelectedItem.Value = "0";
                ddlModule.DataSource = ds;
                ddlModule.DataValueField = ds.Tables[0].Columns["AS_NO"].ToString();
                ddlModule.DataTextField = ds.Tables[0].Columns["AS_TITLE"].ToString();
                ddlModule.DataBind();
                ddlModule.SelectedIndex = 0;
            }
            else
            {
                ddlModule.Items.Clear();
                ddlModule.Items.Add("Please Select");
                ddlModule.SelectedItem.Value = "0";
            }
        }
        

    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)

    {
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK AL,(select AL_No,AL_Link from ACCESS_LINK )A", "a.AL_Link +'->' +AL.AL_Link as AL_LINK", "AL.AL_No", "AL.MastNo = A.AL_No and AL.AL_ASNo='" + Convert.ToInt32(ddlModule.SelectedValue) + "' AND Active_Status=1 ", "");

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlpagelink.Items.Clear();
                ddlpagelink.Items.Add("Please Select");
                ddlpagelink.SelectedItem.Value = "0";
                ddlpagelink.DataSource = ds;
                ddlpagelink.DataValueField = ds.Tables[0].Columns["AL_No"].ToString();
                ddlpagelink.DataTextField = ds.Tables[0].Columns["AL_LINK"].ToString();
                ddlpagelink.DataBind();
                ddlpagelink.SelectedIndex = 0;
            }
            else
            {
                ddlpagelink.Items.Clear();
                ddlpagelink.Items.Add("Please Select");
                ddlpagelink.SelectedItem.Value = "0";
            }
        }
    

    }
    private void BindListview()
    {
        try
        {
            DataSet ds = objSC.GetAllEmailConfiguration();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
              
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
                //objCommon.DisplayMessage(this.updLoan, "No Record found for selected criteria.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ECNO = 0;
        try
        {
        int EC_AsNo = int.Parse(ddlModule.SelectedValue);
        int EC_AL_No = int.Parse(ddlpagelink.SelectedValue);
        string Email = txtemail.Text;
        string Password = txtpassword.Text;
        int CREATED_BY = 1;
        string IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
           if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)objSC.AddEmailConfiguration(Convert.ToInt32(ddlModule.SelectedValue), Convert.ToInt32(ddlpagelink.SelectedValue), Email, Password, CREATED_BY, IPADDRESS);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        ClearField();
                        objCommon.DisplayMessage(this.updtime, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        ViewState["action"] = "add";
                        ClearField();
                        objCommon.DisplayMessage(this.updtime, "Record Already Exist !", this.Page);
                    }

                }
                else
                {
                    if (ViewState["ECNO"] != null)
                    {
                        ECNO = Convert.ToInt32(ViewState["ECNO"].ToString());

                        CustomStatus cs = (CustomStatus)objSC.UpdateEmailConfiguration(ECNO, Convert.ToInt32(ddlModule.SelectedValue), Convert.ToInt32(ddlpagelink.SelectedValue), Email, Password ,CREATED_BY, IPADDRESS);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            //ClearField();
                            objCommon.DisplayMessage(this.updtime, "Record Updated Successfully!", this.Page);
                            //ddlModule.Items.Insert(0, new ListItem("Please Select", "0"));
                            ClearField();
                        }
                        else
                        {
                            ViewState["action"] = null;
                           // ClearField();
                            objCommon.DisplayMessage(this.updtime, "Record Already Exist !", this.Page);
                            ClearField();
                        }

                    }
                }

            }
            
           BindListview();
        }
       
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AffiliatedFeesCategory.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    
}
    protected void ClearField()
    {
        
        ddlModule.Items.Clear();
        ddlModule.Items.Insert(0, new ListItem("Please Select", "0"));
        FillDropdown();
        //ddlModule.SelectedIndex =0;
        ddlpagelink.SelectedIndex = 0;
        txtemail.Text = string.Empty;
        txtpassword.Text = string.Empty;
      

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ViewState["action"] = null;
      
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = sender as ImageButton;
        ViewState["ECNO"] = Convert.ToInt32(btnEdit.CommandArgument);
        int ecno = int.Parse(btnEdit.CommandArgument);

        ShowDetails(ecno);
        ViewState["action"] = "edit";

    }
       
    


    private void ShowDetails(int ecno)
    {
        DataSet ds = null;
        try
        {

            ds = objSC.GetEmailConfigurationByNo(ecno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dt = objCommon.FillDropDown("ACCESS_LINK AL,(select AL_No,AL_Link from ACCESS_LINK )A", "a.AL_Link +'->' +AL.AL_Link as AL_LINK", "AL.AL_No", "AL.MastNo = A.AL_No and AL.AL_ASNo='" + Convert.ToInt32(ds.Tables[0].Rows[0]["AS_NO"].ToString()) + "' AND Active_Status=1 ", "");

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {

                        ddlpagelink.Items.Clear();
                        ddlpagelink.Items.Add("Please Select");
                        ddlpagelink.SelectedItem.Value = "0";
                        ddlpagelink.DataSource = dt;
                        ddlpagelink.DataValueField = dt.Tables[0].Columns["AL_No"].ToString();
                        ddlpagelink.DataTextField = dt.Tables[0].Columns["AL_LINK"].ToString();
                        ddlpagelink.DataBind();
                        ddlpagelink.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlpagelink.Items.Clear();
                        ddlpagelink.Items.Add("Please Select");
                        ddlpagelink.SelectedItem.Value = "0";
                    }
                }




                ddlModule.SelectedValue = ds.Tables[0].Rows[0]["AS_NO"].ToString();
                ddlpagelink.SelectedValue =(ds.Tables[0].Rows[0]["AL_No"]).ToString();
               // ddlModule.SelectedItem.Text = ds.Tables[0].Rows[0]["ModuleName"] == null ? string.Empty : ds.Tables[0].Rows[0]["ModuleName"].ToString();
               // ddlpagelink.SelectedItem.Text = ds.Tables[0].Rows[0]["PageLink"] == null ? string.Empty : ds.Tables[0].Rows[0]["PageLink"].ToString();

                txtemail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtpassword.Text = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                ViewState["action"] = "edit";
                btnSubmit.Text = "Update";

            }
            else
            {
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlpagelink_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}