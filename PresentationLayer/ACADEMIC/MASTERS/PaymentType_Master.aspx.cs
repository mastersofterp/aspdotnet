//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BATCH MASTER                                                         
// CREATION DATE : 02-SEPT-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
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


public partial class Academic_Masters_PayTypeMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //Page Authorization
               // this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
           // objCommon.FillDropDownList(ddlSubjectType, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0", "SECTIONNO");
            BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayType_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayType_Master.aspx");
        }
    }
    #endregion
    
    #region Form Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ActiveStatus = 0;
        try
        {
            BatchController objBC = new BatchController();
            Batch objBatch = new Batch();
            string PaytypeName = txtPaymentType.Text.Trim();
            string Remark = txtRemark.Text.Trim();
            string CollegeCode = Session["colcode"].ToString();
            if (hfdStat.Value == "true") // Added By Rishabh on 24/01/2022
            {
                ActiveStatus = 1;
            }
            else
            {
                ActiveStatus = 0;
            }

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string PAYTYPENO = objCommon.LookUp("ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME='" + txtPaymentType.Text.Trim() + "'");
                    if (PAYTYPENO != null && PAYTYPENO != string.Empty)
                    {
                        objCommon.DisplayMessage(this.updPaymentType, "Record Already Exist", this.Page);
                        return;
                    }
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objBC.AddPayTypeNo(PaytypeName,Remark,CollegeCode, ActiveStatus);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updPaymentType, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        Clear();
                        objCommon.DisplayMessage(this.updPaymentType, "Record already exist", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["PaymentTypeno"] != null)
                    {
                        int PayTypeNo = Convert.ToInt32(ViewState["PaymentTypeno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdatePayTypeNo(PaytypeName,Remark,CollegeCode, ActiveStatus,PayTypeNo);
                        if (cs.Equals(CustomStatus.RecordUpdated))          
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updPaymentType, "Record Updated Successfully!", this.Page);
                        }
                        //else
                        //{
                        //    objCommon.DisplayMessage(this.updPaymentType, "Record already exist", this.Page);
                        //}
                    }
                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_PayTypeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PaymentTypeno = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

            ShowDetail(PaymentTypeno);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_PayTypeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Private Methods

    private void ShowDetail(int PaymentTypeno)
    {
        BatchController objBatch = new BatchController();
        SqlDataReader dr = objBatch.GetPaymentTypeByNo(PaymentTypeno);
        int PAYMENTTYPENO = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*)", "PAYTYPENO=" + Convert.ToInt32(PaymentTypeno) + ""));
        if (dr != null)
        {
            if (dr.Read())
            {
                if (PAYMENTTYPENO > 0)
                {               
                    objCommon.DisplayMessage(this.updPaymentType, "Standard fees is already created for this Selected Payment Type .So you cannot Update", this.Page);
                    return;
                }
                else
                {
                    ViewState["PaymentTypeno"] = PaymentTypeno.ToString();
                    txtRemark.Text = dr["REMARK"] == null ? string.Empty : dr["REMARK"].ToString();
                    txtPaymentType.Text = dr["PAYTYPENAME"] == null ? string.Empty : dr["PAYTYPENAME"].ToString();
                    if (dr["ACTIVESTATUS"].ToString() == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                    }
                }
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        txtRemark.Text = string.Empty;
        txtPaymentType.Text = string.Empty;
        //Label1.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            BatchController objBC = new BatchController();
            DataSet ds = objBC.GetAllPayType();
            lvPayment.DataSource = ds;
            lvPayment.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_PayTypeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

}
