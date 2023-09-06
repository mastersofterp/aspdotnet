//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 16-APR-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class Health_Transaction_DisposableItemIssue : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DisposableItemIssue_Ent objDisp = new DisposableItemIssue_Ent();
    DisposableItemIssueCon objDispController = new DisposableItemIssueCon();

  
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
        try
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
                    this.CheckPageAuthorization();

                    ViewState["action"] = "add";
                  //  objCommon.FillDropDownList(ddlItem, "HEALTH_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");                   
                    BindListView();
                    txtIssueDate.Text = System.DateTime.Today.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private void BindListView()
    {
        try
        {

            DataSet ds = objDispController.GetDisposableItemIssueList();
            if (ds != null)
            {
                lvDisposableItem.DataSource = ds;
                lvDisposableItem.DataBind();
                lvDisposableItem.Visible = true;
            }
            else
            {
                lvDisposableItem.DataSource = null;
                lvDisposableItem.DataBind();
                lvDisposableItem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.ShowSummaryReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }

    private void Clear()
    {
        txtIssueDate.Text = System.DateTime.Today.ToString();
        txtAvailableQty.Text = string.Empty;
        txtIssueQty.Text = string.Empty;
        txtRemark.Text = string.Empty;
       // ddlItem.SelectedIndex = 0;
        txtItemName.Text = string.Empty;
        hfItemName.Value = "0";
       
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = "select ITEM_NO, ITEM_NAME AS ITEM_NAME from HEALTH_ITEM where ITEM_NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "---------*" + sdr["ITEM_NAME"].ToString());
                    }
                }
                conn.Close();
                return ItemsName;
            }
        }
    }        

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("DisposableItemIssueDetails", "rptDisposalItemIssue.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_ITEMNO=0";  
         
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objDisp.ITEM_NO = Convert.ToInt32(hfItemName.Value);  //Convert.ToInt32(ddlItem.SelectedValue);
            objDisp.AVAILABLE_QTY = Convert.ToInt32(txtAvailableQty.Text);
            objDisp.ISSUE_QTY = Convert.ToInt32(txtIssueQty.Text);
            if (txtIssueDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updActivity, "Please select issue date.", this.Page);
                return;
            }
            else
            {
                objDisp.ISSUE_DATE = Convert.ToDateTime(txtIssueDate.Text);
            }
            objDisp.REMARK = txtRemark.Text.Trim() == string.Empty ? string.Empty : txtRemark.Text.Trim();
            objDisp.COLLEGE_CODE = Session["colcode"].ToString();
            objDisp.USER_ID = Convert.ToInt32(Session["userno"]);


            DataSet ds = objDispController.GetInsufficientStockDetails(Convert.ToInt32(hfItemName.Value));
            if (Convert.ToInt32(txtIssueQty.Text) > Convert.ToInt32(ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString()))
            {
                objCommon.DisplayMessage(this.updActivity, "No sufficient stock", this.Page);               
                return;
            }
                                

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objDisp.DINO = 0;
                    CustomStatus cs = (CustomStatus)objDispController.DisposableItemIssueIU(objDisp);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                        Clear();
                        ViewState["action"] = "add";
                        BindListView();
                    }
                }
                else
                {
                    if (ViewState["DINO"] != null)
                    {
                        objDisp.DINO = Convert.ToInt32(ViewState["DINO"].ToString());
                        CustomStatus cs = (CustomStatus)objDispController.DisposableItemIssueIU(objDisp);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            Clear();
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);                           
                            ViewState["action"] = "add";
                            BindListView();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.btnSummary_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int DINO = int.Parse(btnEdit.CommandArgument);
            ViewState["DINO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(DINO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int DINO)
    {
        try
        {
            int IssueQty = 0;
            int AvailableQty = 0;
            DataSet ds = objCommon.FillDropDown("HEALTH_PRESC", "*", "", "PRESCNO=" + DINO, "PRESCNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlItem.SelectedValue = ds.Tables[0].Rows[0]["INO"].ToString();
                hfItemName.Value =  ds.Tables[0].Rows[0]["INO"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ITEMNAME"].ToString();
                txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUE_DATE"].ToString();              
                txtIssueQty.Text = ds.Tables[0].Rows[0]["QTY_ISSUE"].ToString();
               IssueQty = Convert.ToInt32(txtIssueQty.Text);
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();

                if (ds.Tables[0].Rows[0]["INO"].ToString() != "")
                {
                    DataSet dsRec = objDispController.GetDispItemAvailableQty(Convert.ToInt32(hfItemName.Value));
                    if (dsRec.Tables[0].Rows.Count > 0)
                    {
                        txtAvailableQty.Text = dsRec.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString();
                       AvailableQty = Convert.ToInt32(txtAvailableQty.Text);
                    }
                }
                AvailableQty += IssueQty;
               txtAvailableQty.Text = AvailableQty.ToString();
            }
        } 
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    

   


    protected void txtItemName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hfItemName.Value != "0")
            {
                DataSet ds = objDispController.GetDispItemAvailableQty(Convert.ToInt32(hfItemName.Value));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAvailableQty.Text = ds.Tables[0].Rows[0]["AVAILABLE_QTY"].ToString();
                    txtIssueQty.Focus();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Transaction_DisposableItemIssue.txtItemName_TextChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}