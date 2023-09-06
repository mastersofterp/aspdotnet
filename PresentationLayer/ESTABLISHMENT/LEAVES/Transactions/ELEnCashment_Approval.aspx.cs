using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ESTABLISHMENT;
public partial class ESTABLISHMENT_LEAVES_Transactions_ELEnCashment_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EnCashmentControllar objEnCash = new EnCashmentControllar();
    Leaves objLeaveMaster = new Leaves();

    #region Load

    protected void Pre_Init(object sender, EventArgs e)
    {
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
                CheckPageAuthorization();
                pnlAdd.Visible = true;
                ViewState["action"] = "add";
               
                FillCollege();
                BindListView();
            }
        }
    }

    #endregion

    #region Page Event

    protected void btnApprove_Click(Object sender, EventArgs e)
    {
        int SRNO = 0;
        DataTable dtApprove = new DataTable();
        dtApprove.Columns.Add("SRNO");
       
        objLeaveMaster.APPROVEDBY = Convert.ToInt32(Session["userno"]);
       
        foreach (RepeaterItem items in lvEnCashApproval.Items)
        {
            CheckBox chsr = items.FindControl("chsr") as CheckBox;
            if (chsr.Checked)
            {
                SRNO = Convert.ToInt32(chsr.ToolTip);
                DataRow dr = dtApprove.NewRow();
                dr["SRNO"] = SRNO;
                dtApprove.Rows.Add(dr);
                dtApprove.AcceptChanges();
            }
        }
        if (SRNO != 0)
        {
            CustomStatus cs = (CustomStatus)objEnCash.ApproveRecord(objLeaveMaster, dtApprove);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updAll, "Record Saved Successfully", this.Page);
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(updAll, "Sorry! Transaction Failed", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Record Not Approved", this.Page);
            }

            BindListView();
        }
        else
        {
            objCommon.DisplayMessage(updAll,"Please Select Atleast One Record ", this.Page);
          
        }
    }    

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindListView();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_ELEnCashment_ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDt.Text.ToString() != string.Empty && txtFromDt.Text.ToString() != "__/__/____" && txtToDt.Text.ToString() != string.Empty && txtToDt.Text.ToString() != "__/__/____")
            {

                if (Convert.ToDateTime(txtToDt.Text) < Convert.ToDateTime(txtFromDt.Text))
                {
                    MessageBox("To Date Must Be Equal To Or Greater Than From Date");
                    return;
                }
            }
            this.BindListView();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_ELEnCashment_ToDateSelection -> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void txtFromDt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDt.Text.ToString() != string.Empty && txtFromDt.Text.ToString() != "__/__/____" && txtToDt.Text.ToString() != string.Empty && txtToDt.Text.ToString() != "__/__/____")
            {

                if (Convert.ToDateTime(txtToDt.Text) < Convert.ToDateTime(txtFromDt.Text))
                {
                    MessageBox("To Date Must Be Equal To Or Greater Than From Date");
                    return;
                }
            }
            this.BindListView();
        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_ELEnCashment_FromDateSelection -> " + ex.Message + " " + ex.StackTrace);
        }
    }

  

    #endregion

    #region Private Method

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

    private void BindListView( )
    {
        try
        {
            int collegeno = Convert.ToInt32(ddlCollege.SelectedValue);
            if (!txtFromDt.Text.Trim().Equals(string.Empty)) objLeaveMaster.FROMDT = Convert.ToDateTime(txtFromDt.Text);
            if (!txtToDt.Text.Trim().Equals(string.Empty)) objLeaveMaster.TODT = Convert.ToDateTime(txtToDt.Text);
            DataSet ds = objEnCash.GetAllEncashApplyData(collegeno,objLeaveMaster);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEnCashApproval.DataSource = ds;
                lvEnCashApproval.DataBind();
                DivNote.Visible = false;
                btnApprove.Visible = true;
            }
            else
            {
                lvEnCashApproval.DataSource = null;
                lvEnCashApproval.DataBind();
                DivNote.Visible = true;
                btnApprove.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_ELEnCashment_Approve.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       

    }

    protected void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #endregion


   
  
}