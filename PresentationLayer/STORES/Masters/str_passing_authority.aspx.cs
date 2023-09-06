using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Masters_str_passing_authority : System.Web.UI.Page
{  //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMaster objStr = new StoreMaster();
    StoreMasterController objStrMaster = new StoreMasterController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillUser();
                BindListViewPAuthority();
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    protected void BindListViewPAuthority()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllPassAuthority();
            ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY A INNER JOIN DBO.USER_ACC U ON (A.UA_NO=U.UA_NO)", "PANAME, A.UA_NO, UA_FULLNAME, AMOUNT_FROM, AMOUNT_TO", "PANO, (CASE IS_SPECIAL WHEN 1 THEN 'Special Authority' ELSE '' END)  as IS_SPECIAL", "", "");
            //if (ds.Tables[0].Rows.Count <= 0)
            //{

            //    dpPager.Visible = false;
            //}
            //else
            //{

            //    dpPager.Visible = true;
            //}
            lvPAuthority.DataSource = ds;
            lvPAuthority.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    //Bind the ListView with Domain            
    //    BindListViewPAuthority();
    //}
    private void Clear()
    {
        txtPAuthority.Text = string.Empty;
        ddlUser.SelectedIndex = 0;
        txtFValue.Text = string.Empty;
        txtTValue.Text = string.Empty;
        chkSpeAuthority.Checked = false;
        DivAmtFr.Visible=false;
        Div3AmtTo.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        ViewState["action"] = "add";
    }
    private void FillUser()
    {
        try
        {
            objCommon.FillDropDownList(ddlUser, "USER_ACC", "UA_NO", "UA_FULLNAME", " UA_Type IN (1,3,4,5,8,11,12) ", "UA_FULLNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkSpeAuthority.Checked == true)
            {
                if (txtFValue.Text == string.Empty || txtTValue.Text == string.Empty)
                {
                    objCommon.DisplayMessage(pnlMessage, "Sanctioning Amount From Range And To Range should not be blank.", this);
                    return;
                }

               

            }
            objStr.PANAME = Convert.ToString(txtPAuthority.Text);
            objStr.UANO = Convert.ToInt32(ddlUser.SelectedValue);
            objStr.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            //objStr.AMOUNT_FROM = txtFValue.Text == string.Empty ? 0.00 : Convert.ToDouble(txtFValue.Text);
            //objStr.AMOUNT_TO = txtTValue.Text == string.Empty ? 0.00 : Convert.ToDouble(txtTValue.Text);

            objStr.AMOUNT_FROM = txtFValue.Text == string.Empty ? 0 : Convert.ToDouble(txtFValue.Text);
            objStr.AMOUNT_TO = txtTValue.Text == string.Empty ? 0 : Convert.ToDouble(txtTValue.Text);


           
           
            if (txtFValue.Text != string.Empty && txtTValue.Text != string.Empty)
            {
                if (objStr.AMOUNT_FROM >= objStr.AMOUNT_TO)
                {
                    objCommon.DisplayMessage(pnlMessage, "Sanctioning Amount From Range should not be greater than equal to To Range.", this);
                    return;
                }
            } 

            if(chkSpeAuthority.Checked == true)
            {
                objStr.IS_SPECIAL = 1;
            }
            else
            {
                    objStr.IS_SPECIAL = 0;
            }      
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PASSING_AUTHORITY", " count(*)", "PANAME='" + txtPAuthority.Text + "' OR UA_NO=" + Convert.ToInt32(ddlUser.SelectedValue)));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddPassAuthority(objStr);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(pnlMessage, "Record Saved Successfully", this);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(pnlMessage, "This Passing Authority Already Exist.", this);
                        return;
                    }
                }
                else
                {
                    if (ViewState["PANO"] != null)
                    {
                        objStr.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
                        CustomStatus cs = (CustomStatus)objStrMaster.UpdatePassAuthority(objStr);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(pnlMessage, "Record Updated Successfully", this);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
                BindListViewPAuthority();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = "add";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PANO = int.Parse(btnEdit.CommandArgument);

            int ret = Convert.ToInt32(objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "count(*)", "PAN01 =" + PANO + "or PAN02=" + PANO + " or PAN03=" + PANO));

            if (ret > 0)
            {
                objCommon.DisplayMessage(pnlMessage, "Can Not Modify,This Authority Is In Use.", this);
                return;

            }





            ShowDetails(PANO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int PANO = int.Parse(btnDelete.CommandArgument);

            int ret = Convert.ToInt32(objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "count(*)", "PAN01 =" + PANO + "or PAN02=" + PANO + " or PAN03=" + PANO));

            if (ret > 0)
            {
                objCommon.DisplayMessage(pnlMessage, "This Athority Is In Used.", this);
                return;
            
            }

            CustomStatus cs = (CustomStatus)objStrMaster.DeletePassAuthority(PANO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(pnlMessage, "Record Deleted Successfully", this);
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetails(Int32 PANO)
    {
        DataSet ds = null;
        try
        {
            // ds = objStrMaster.GetSingPassAuthority(PANO);
            ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY  A INNER  JOIN DBO.USER_ACC U ON  (A.UA_NO=U.UA_NO  )", "PANAME, A.UA_NO, UA_FULLNAME, AMOUNT_FROM, AMOUNT_TO, isnull(IS_SPECIAL,0) as IS_SPECIAL", "PANO", "PANO=" + PANO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PANO"] = PANO;
                txtPAuthority.Text = ds.Tables[0].Rows[0]["PANAME"].ToString();
                ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                txtFValue.Text = ds.Tables[0].Rows[0]["AMOUNT_FROM"].ToString();
                txtTValue.Text = ds.Tables[0].Rows[0]["AMOUNT_TO"].ToString();
                if (ds.Tables[0].Rows[0]["IS_SPECIAL"].ToString() == "1")
                {
                    chkSpeAuthority.Checked = true;
                    DivAmtFr.Visible = true;
                    Div3AmtTo.Visible = true;
                }
                else
                {
                    chkSpeAuthority.Checked = false;
                    DivAmtFr.Visible=false;
                    Div3AmtTo.Visible = false;
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void chkSpeAuthority_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSpeAuthority.Checked == true)
            {
                DivAmtFr.Visible = true;
                Div3AmtTo.Visible = true;
            }
            else
            {
                DivAmtFr.Visible = false;
                Div3AmtTo.Visible = false;
                txtFValue.Text = string.Empty;
                txtTValue.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_str_passing_authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
