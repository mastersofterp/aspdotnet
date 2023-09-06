using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using Google.API.Translate;


public partial class PAYROLL_MASTERS_PayDesignation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    DeptDesigMaster objDeptDesig = new DeptDesigMaster();
    string UsrStatus = string.Empty;

    #region Page Load

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
            Form.Attributes.Add("onLoad()", "msg()");
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ViewState["action"] = "add";
                BindListView();
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "onLoad();", true);
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
                Response.Redirect("~/notauthorized.aspx?page=PayDesignation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayDesignation.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objDeptDesig.GetDesignation();

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDesignation.DataSource = ds;
                lvDesignation.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("PAYROLL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtDesigName.Text = "";
        txtDesigShortName.Text = "";
        // txtDesigKannad.Text = "";
        txtDesigSeqNumber.Text = "";
        ViewState["lblFNO"] = null;
        ViewState["action"] = "add";
    }

    #endregion

    #region Page Events

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Update();
        PayMaster objPay = new PayMaster();
        objPay.DESIGNATION = txtDesigName.Text.Trim();
        objPay.DESIGSHORT = txtDesigShortName.Text.Trim();
        //  objPay.DESIGNATION_KANNADA = txtDesigKannad.Text.Trim();
        objPay.DESIGNATION_SEQNO = Convert.ToDecimal(txtDesigSeqNumber.Text.Trim());
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    objPay.SUBDESIGNO = 0;
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddDesignation(objPay);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Sequence No Already Exist");
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
            else
            {
                bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    ViewState["action"] = "add";
                    objPay.SUBDESIGNO = Convert.ToInt32(ViewState["lblFNO"].ToString().Trim());
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddDesignation(objPay);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        MessageBox("Sequence No Already Exist");
                        return;
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                    }
                }
            }

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblDesig = lst.FindControl("lblDesig") as Label;
            Label lblDesigShort = lst.FindControl("lblDesigShort") as Label;
            Label lblDesigKannada = lst.FindControl("lblDesigKannada") as Label;
            Label lblDesigSeqNo = lst.FindControl("lblDesigSeqNo") as Label;
            //txtHead.Text = lblPayhead.Text.Trim();
            //txtField.Text = lblDeduHead.Text.Trim();
            txtDesigName.Text = lblDesig.Text.Trim();
            txtDesigShortName.Text = lblDesigShort.Text.Trim();
            // txtDesigKannad.Text = lblDesigKannada.Text.Trim();
            txtDesigSeqNumber.Text = lblDesigSeqNo.Text.Trim();
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("ITDEDUCTIONHEADS", "Pay_ITDeductionHead.rpt");
    }
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();
      //  int id = 0;
    //    dsPURPOSE = objCommon.FillDropDown("PAYROLL_SUBDESIG", "*", "SUBDESIG='" + txtDesigName.Text + "' or SUBSDESIG='" + txtDesigShortName.Text + "'","");
        //
        // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
       dsPURPOSE = objCommon.FillDropDown("PAYROLL_SUBDESIG", "*", "", "SUBDESIG='" + txtDesigName.Text + "' and SUBSDESIG='" + txtDesigShortName.Text + "'", "");
       if (dsPURPOSE.Tables[0].Rows.Count > 0)
       {
           result = true;

       }

        return result;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    #endregion
}
