using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
public partial class HOSTEL_HostelReconcile : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    RoomAllotmentController objRC = new RoomAllotmentController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    /// Fill Dropdown lists                
                    ViewState["action"] = null;
                }
                string date = Convert.ToString(DateTime.Now);
                //objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
                ddlSession.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlDBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_HostelReconcile.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            FeeCollectionController objFCC = new FeeCollectionController();
            string id = string.Empty;
            DataSet ds =null;
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            if (txtrollNo.Text.Trim() != string.Empty)
            {
                id = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT HRA INNER JOIN ACD_STUDENT S ON(HRA.RESIDENT_NO=S.IDNO) INNER JOIN ACD_DCR D ON(S.IDNO=D.IDNO AND HRA.HOSTEL_SESSION_NO=D.SESSIONNO)", "S.IDNO", "HRA.CAN=0 AND D.CAN=0 AND RECIEPT_CODE IN ('MF','HF') AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND S.REGNO='" + txtrollNo.Text.Trim() + "'");
                if (id == "")
                {
                    objCommon.DisplayMessage("Please Enter Valid Roll No.", this.Page);
                    return;
                }
                //ds = objRC.GetStudentInfoById(Convert.ToInt32(id));
                ds = objRC.GetStudentInfoById(Convert.ToInt32(id), OrganizationId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRegNo.Text = txtrollNo.Text.Trim();
                    lblBatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    if (ds.Tables[0].Rows[0]["ADMDATE"].ToString() != "")
                        lblDateOfAdm.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ADMDATE"].ToString()).ToString("dd-MMM-yyyy");
                    else
                        lblDateOfAdm.Text = "-";
                    lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                    lblRegNo.Text = ds.Tables[0].Rows[0]["ROLLNO"].ToString();
                    lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSex.Text = ds.Tables[0].Rows[0]["SEX"].ToString();
                    lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblStudName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();
                    lblPaymentType.Text = ds.Tables[0].Rows[0]["PAYTYPENAME"].ToString();
                    lblYear.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                    divStudInfo.Visible = true;
                    divReconcile.Visible = true;
                }
                FillHostelDetail(Convert.ToInt32(id));
            }
            else
                objCommon.DisplayMessage("Please Enter Valid Roll No.", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_HostelReconcile.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillHostelDetail(int idno)
    {
        DataSet ds = null;
        DataSet ds1 = null;
        string roomAllotNo = objCommon.LookUp("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_HOSTEL_RESIDENT_TYPE T ON(H.RESIDENT_TYPE_NO=T.RESIDENT_TYPE_NO)", "ROOM_ALLOTMENT_NO", "CAN=0 AND IS_STUDENT=1 AND HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND RESIDENT_NO=" + idno);
        ds = objRC.GetRoomAllotmentByNo(Convert.ToInt32(roomAllotNo));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblAllotDate.Text =Convert.ToDateTime(ds.Tables[0].Rows[0]["ALLOTMENT_DATE"].ToString()).ToString("dd-MMM-yyyy");
            lblHostel.Text = ds.Tables[0].Rows[0]["HOSTEL"].ToString();
            //lblMess.Text = ds.Tables[0].Rows[0]["MESS_NAME"].ToString();
            lblBlock.Text = ds.Tables[0].Rows[0]["BLOCK_NAME"].ToString();
            lblRoom.Text = ds.Tables[0].Rows[0]["ROOM_NAME"].ToString();
        }
        ds1 = objCommon.FillDropDown("ACD_HOSTEL_ROOM_ALLOTMENT H INNER JOIN ACD_DCR D ON(H.RESIDENT_NO=D.IDNO AND H.HOSTEL_SESSION_NO=D.SESSIONNO)", "IDNO", "ROW_NUMBER()OVER (ORDER BY IDNO) SRNO,DCR_NO,(CASE RECIEPT_CODE WHEN 'MF' THEN 'MESS FEE' WHEN 'HF' THEN 'HOSTEL FEE' END)RECEIPT,TOTAL_AMT,(CASE RECON WHEN 1 THEN 'RECONCILED' ELSE '' END)RECONCILE,ISNULL(RECON,0)RECON,REC_DT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + idno + " AND D.CAN=0 AND H.CAN=0 AND RECIEPT_CODE IN ('MF','HF')", "RECON DESC");
        if (ds1.Tables[0].Rows.Count > 0)
            lvReconcile.DataSource = ds1;
        else
            lvReconcile.DataSource = null;
        lvReconcile.DataBind();
        txtPayType.Text = string.Empty;
        txtReconDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        divStudentSearch.Visible = false;
    }

    protected void lvReconcile_DataBound(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvReconcile.Items)
        {
            CheckBox chkRecon =item.FindControl("chkRecon") as CheckBox;
            if (chkRecon.ToolTip == "True")
            {
                chkRecon.Checked = true;
                chkRecon.Enabled = false;
                chkRecon.Text = "Reconciled";
            }
            else
            {
                chkRecon.Checked = false;
                chkRecon.Enabled = true;
                chkRecon.Text = "Please Select for Reconcile.";
                chkRecon.Style.Add("color", "Red");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        RoomAllotment objR=new RoomAllotment();
        objR.ResidentNo = Convert.ToInt32(lblStudName.ToolTip);
        objR.HostelSessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objR.CollegeCode = Session["colcode"].ToString();
        objR.UserNo = Convert.ToInt32(Session["userno"].ToString());
        int output = -99;
        string dcrno = string.Empty;
       
        foreach (ListViewDataItem item in lvReconcile.Items)
        {
            CheckBox chkRecon = item.FindControl("chkRecon") as CheckBox;
            Label lblDcrNo = item.FindControl("lblDcrNo") as Label;
            if (chkRecon.Enabled == true && chkRecon.Checked == true)
            {
                dcrno += lblDcrNo.Text + ",";      
            }
        }
        if (dcrno == "")
        {
            objCommon.DisplayMessage("Please Select checkbox for Reconsile process!!", this.Page);
            return;
        }
        dcrno =dcrno.Remove(dcrno.Length-1);
        DateTime dd_date=DateTime.MinValue;
        if(txtDDDate.Text.Trim()!="") dd_date=Convert.ToDateTime(txtDDDate.Text.Trim());
        
        output = objRC.ReconcileHostelFee(objR, Convert.ToInt32(dcrno), txtDDNo.Text.Trim(), dd_date, ddlDBank.SelectedItem.Text, txtDDCity.Text.Trim(), Convert.ToInt32(ddlDBank.SelectedValue));
        if (output == 1)
        {
            objCommon.DisplayMessage("Reconciled Successfully !!", this.Page);
            ClearControl();
            FillHostelDetail(Convert.ToInt32(lblStudName.ToolTip));
        }
        else
            objCommon.DisplayMessage("Transaction Failed !!", this.Page);

    }

    private void ClearControl()
    {
        txtDDCity.Text = string.Empty;
        txtDDAmount.Text = string.Empty;
        txtDDDate.Text = string.Empty;
        txtDDNo.Text = string.Empty;
        txtPayType.Text = string.Empty;
        txtReconDate.Text = string.Empty;
        ddlDBank.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

}
