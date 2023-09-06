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



public partial class ACADEMIC_ClubActivityMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   // StudentController objSC = new StudentController();
    ClubController OBJCLUB = new ClubController();
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
            populateDropDown();
            BindListView();
            ViewState["action"] = "add";
        }
        divMsg.InnerHtml = string.Empty;
    }

     private void populateDropDown()
    {
        objCommon.FillDropDownList(ddlIncharge, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE NOT IN (1,2)", "UA_NO");
 
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = OBJCLUB.GetAllclubdata();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvclub.DataSource = ds;
                lvclub.DataBind();
                pnlclub.Visible = true;

            }
            else
            {
                lvclub.DataSource = null;
                lvclub.DataBind();
                pnlclub.Visible = false;
                //objCommon.DisplayMessage(this.updLoan, "No Record found for selected criteria.", this.Page);
            }

            foreach (ListViewDataItem dataitem in lvclub.Items)
            {
                Label lblStatus = dataitem.FindControl("lblStatus") as Label;

                string status = (lblStatus.Text);

                if (status.Equals("InActive"))
                {
                    lblStatus.CssClass = "badge badge-danger";
                }
                else
                {
                    lblStatus.CssClass = "badge badge-success";
                }

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
        try
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "validate();", true);

            int CLUBNO = 0;
            int organizationid = 0;
            string Typeofactivity = string.Empty;
            int InchargeNo = Convert.ToInt32(ddlIncharge.SelectedValue);
            string Email = string.Empty;
            int Active = 0;
            string Regno =string.Empty;
            Typeofactivity = txttypeactivity.Text;
            Email = txtemail.Text;
            Regno = txtregno.Text;
            int CREATED_BY = 1;
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            organizationid = Convert.ToInt32(Session["OrgId"]);

            /*if (chkActive.Checked)
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }*/
            
            if (hfdActiveClub.Value == "true")
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }


            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && Active == 0)
            {
                string refStatus = OBJCLUB.CheckReferMasterTable(11, "Club Master", Convert.ToInt32(ViewState["CLUBNO"]));

                /*string[] commandArgs = refStatus.Split(new char[] { ',' });
                string statusCode = commandArgs[0];
                string statusName = commandArgs[1];*/

                if (refStatus.Equals("2"))
                {
                    objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                    return;
                }

            }



            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)OBJCLUB.AddClubData(Typeofactivity,InchargeNo, Email, Regno, Active, CREATED_BY, ipAddress, organizationid);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        ClearField();
                        objCommon.DisplayMessage(this.updclub, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        ViewState["action"] = "add";
                        ClearField();
                        objCommon.DisplayMessage(this.updclub, "Record Already Exist !", this.Page);
                    }

                }
                else
                {
                    if (ViewState["CLUBNO"] != null)
                    {
                        CLUBNO = Convert.ToInt32(ViewState["CLUBNO"].ToString());
                        CustomStatus cs = (CustomStatus)OBJCLUB.UpdateClubData(CLUBNO, Typeofactivity,InchargeNo, Email, Regno, Active, CREATED_BY, ipAddress, organizationid);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            ClearField();
                            objCommon.DisplayMessage(this.updclub, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            ViewState["action"] = null;
                            ClearField();
                            objCommon.DisplayMessage(this.updclub, "Record Already Exist !", this.Page);
                        }

                    }
                }

            }

            BindListView();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AffiliatedFeesCategory.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        Response.Redirect(Request.Url.ToString());
    }

    protected void ClearField()
    {
        ViewState["action"] = "add";
        txttypeactivity.Text = string.Empty;
        ddlIncharge.SelectedIndex = 0;
        txtemail.Text = string.Empty;
        ////chkActive.Checked = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActiveClub(true);", true);

        txtregno.Text = string.Empty;
       
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Edit = sender as ImageButton;
        ViewState["CLUBNO"] = Convert.ToInt32(Edit.CommandArgument);
        DataSet dsEdit;
        dsEdit = objCommon.FillDropDown("ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE,INCHARGE_NO,EMAIL,TOTAL_REGNO_LIMIT,ACTIVESTATUS", "CLUB_ACTIVITY_NO= " + Convert.ToInt32(ViewState["CLUBNO"]), "CLUB_ACTIVITY_NO");
        if (dsEdit.Tables[0].Rows.Count > 0)
        {
            txttypeactivity.Text = dsEdit.Tables[0].Rows[0]["CLUB_ACTIVITY_TYPE"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["CLUB_ACTIVITY_TYPE"].ToString();
            txtemail.Text = dsEdit.Tables[0].Rows[0]["EMAIL"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["EMAIL"].ToString();
            txtregno.Text = dsEdit.Tables[0].Rows[0]["TOTAL_REGNO_LIMIT"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["TOTAL_REGNO_LIMIT"].ToString();
            ddlIncharge.SelectedValue = dsEdit.Tables[0].Rows[0]["INCHARGE_NO"].ToString();
            int Active = Convert.ToInt32(dsEdit.Tables[0].Rows[0]["ACTIVESTATUS"]);
            /*if (Active == 1)
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }*/

            if (Active == 1)
            {               
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActiveClub(true);", true);                
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActiveClub(false);", true);
            }

            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
        }
        else
        {
            ViewState["action"] = "add";
        }
    }
    //protected void txtregno_TextChanged(object sender, EventArgs e)
    //{
    //    int regno = Convert.ToInt32(txtregno.Text);
    //   // int regno = 0;
    //    //if (Convert.ToDecimal(txtyear.Text.ToString()) >= 45)
    //    if (regno  > 500)
    //    {
    //        objCommon.DisplayMessage(this.Page, "Maximum limit of one acitivty registration is 500", this.Page);
    //    }
    //}
}