//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Approval_Requisition.aspx                                                  
// CREATION DATE : 22-Sept-2010                                                        
// CREATED BY    : Kumar Premankit                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
 
public partial class STORES_Transactions_Quotation_Approval_Requisition : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objApp = new StoreMasterController();
    DataSet dsdetails = new DataSet();
    DataTable dtgrid = new DataTable();
    Str_Purchase_Order_Controller objstrPO = new Str_Purchase_Order_Controller();
    STR_DEPT_REQ_CONTROLLER ObjReq = new STR_DEPT_REQ_CONTROLLER();
    Str_DeptRequest objdeptRequest = new Str_DeptRequest();

    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public string path = string.Empty;
    BlobController objBlob = new BlobController();


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
                pnllist.Visible = false;
                Panel1.Visible = false;   
                pnlDept.Visible = true;
                FillDepartment();
                BindGrid();
                BlobDetails();
                pnlItemDetail.Visible = false;

                //BindLVLeaveApplPendingList();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNamestoresdoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
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

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;

        pnlAdd.Visible = false;
        pnllist.Visible = true;
        Panel1.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();

        ViewState["ReqFor"] = null;
        pnlItemDetail.Visible = false;
        pnlDept.Visible = true;
    }

    private void clear_lblvalue()
    {
        lblLeaveName.Text = string.Empty;
        lblReason.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        pnlAdd.Visible = false;
        ViewState["action"] = null;
        clear_lblvalue();
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        Panel1.Visible = false;
        pnlDept.Visible = true;
        ddlDept.SelectedIndex = 0;
        pnlDept.Visible = true;
        pnlItemDetail.Visible = false;
        BindGrid();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        Panel1.Visible = true;   
        ViewState["action"] = null;
        clear_lblvalue();
        clear();

        pnlItemDetail.Visible = false;
        pnlDept.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //int count = 0;
            //for (int i = 0; i < lvitemReq.Items.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)lvitemReq.Items[i].FindControl("chkItem");
            //    if (chk.Checked)
            //    {
            //        count = 1;
            //    }
            //}

            //if (count == 0)
            //{
            //    objCommon.DisplayMessage("Please select at least one item.", this);
            //    return;
            //}
            //else
            //{

                if (ddlSelect.SelectedValue == "F")
                {
                    Panel1_ModalPopupExtender.Show();
                }
                else
                {
                    Panel1_ModalPopupExtender.Hide();

                    for (int i = 0; i < lvitemReq.Items.Count; i++)
                    {
                        ImageButton btnEditItem = lvitemReq.Items[i].FindControl("btnEditItem") as ImageButton;
                        TextBox txtqty = lvitemReq.Items[i].FindControl("txtqty") as TextBox;
                        CheckBox chkAcceptItem = lvitemReq.Items[i].FindControl("chkItem") as CheckBox;
                        if (txtqty.Visible == true)
                        {
                            objApp.updateItemQTY(ViewState["TRNO"].ToString(), Convert.ToInt32(btnEditItem.CommandArgument.ToString()), txtqty.Text, Convert.ToInt32(Session["userno"].ToString()));
                        }
                        // 1 if check true means item is selected.
                        objApp.UpdateAceeptRejectItem(Convert.ToInt32(chkAcceptItem.ToolTip), Convert.ToChar(ddlSelect.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(btnEditItem.CommandArgument.ToString()));

                    }
                    int TRNO = Convert.ToInt32(ViewState["TRNO"].ToString());
                    int UA_NO = Convert.ToInt32(Session["userno"]);
                    int stno = Convert.ToInt32(ViewState["Stno"].ToString());

                    string Status = ddlSelect.SelectedValue.ToString();

                    string Remarks = txtRemarks.Text.ToString();
                    DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
                    int dept = Convert.ToInt32(ddlDept.SelectedValue);

                    // Convert.ToInt32(objCommon.LookUp("store_passing_authority_path","deptno",""))

                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(TRNO, UA_NO, Status, Remarks, stno, dept, Aprdate, 0, Convert.ToChar(Session["sanctioning_authority"]));
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                if (ddlSelect.SelectedValue == "A")
                                {
                                    objCommon.DisplayMessage("Requisition Approved Successfully.", this);
                                    if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                                    {
                                        SendEmailToAuthority(Convert.ToInt32(TRNO));
                                        SendEmailToRequisitionUser(Convert.ToInt32(TRNO));
                                    }
                                }
                                else if (ddlSelect.SelectedValue == "R")
                                {
                                    objCommon.DisplayMessage("Requisition Rejected Successfully.", this);
                                    if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                                    {
                                        SendEmailToAuthority(Convert.ToInt32(TRNO));
                                        SendEmailToRequisitionUser(Convert.ToInt32(TRNO));
                                    }
                                }

                              //  pnllist.Visible = true;
                                pnlAdd.Visible = false;
                                ViewState["action"] = null;
                                clear_lblvalue();
                                clear();
                                //Add to clear the list
                                pnlAdd.Visible = false;
                                pnllist.Visible = false;
                                Panel1.Visible = false;   
                                pnlDept.Visible = true;
                                BindGrid();
                                ddlDept.SelectedIndex = 0;
                                pnlDept.Visible = true;
                                pnlItemDetail.Visible = false;
                            }
                        }
                    }
                }
            }
        
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    #region
    // it is used to send email to Requisition User.
    private void SendEmailToRequisitionUser(int TRNO)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = ObjReq.GetDataForEmailToRequisitionUser(TRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
                {
                    body = "The above requisition is approved.";
                }
                else
                {
                    //body = "The above requisition is rejected by the approval authority.";
                    body = "The above requisition has been Rejected /Cancelled by the approval authority," + "<br />" + "To resend the proposal again using the same requisition number use the below link," + "<br />" + " Link :  Stores >> Transaction >> Department Proposal.";
                }
                sendmail(fromEmailId, fromEmailPwd, receiver, "New Requisition", body, Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void SendEmailToAuthority(int TRNO)
    {
        try
        {
            STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;
            string body = string.Empty;
            DataSet ds = objDeptReqController.GetNextAuthorityForSendingEmail(TRNO, Convert.ToInt32(Session["userno"]), Convert.ToChar(ddlSelect.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }

                if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
                {
                    body = "The above requisition is approved and sent it to your further approval.";
                }
                else
                {                   
                    body = "The above requisition is rejected successfully.";
                    
                }
                sendmail(fromEmailId, fromEmailPwd, receiver, "Requisition Approval", body, Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string username)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            string ReqSlipNo = string.Empty;
            //  DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");
            if (ViewState["REQ_SLIP_NO"] != null)
            {
                ReqSlipNo = ViewState["REQ_SLIP_NO"].ToString();
            }

            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />Requisition Slip No. : " + ReqSlipNo);
            //if (Convert.ToChar(ddlSelect.SelectedValue) == 'A')
            //{
            //    MailBody.AppendLine(@"<br />is approved and send it to you for further approval.");
            //}
            //else
            //{
            //    MailBody.AppendLine(@"<br />is rejected by the approval authority.");
            //}
            MailBody.AppendLine(@"<br /> " + body);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            // MailBody.AppendLine(@"<br />" + Session["userfullname"].ToString());
            MailBody.AppendLine(@"<br />" + username);


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion


    protected void btnOkDel_Click(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < lvitemReq.Items.Count; i++)
            {
                ImageButton btnEditItem = lvitemReq.Items[i].FindControl("btnEditItem") as ImageButton;
                TextBox txtqty = lvitemReq.Items[i].FindControl("txtqty") as TextBox;
                if (txtqty.Visible == true)
                {
                    objApp.updateItemQTY(ViewState["TRNO"].ToString(), Convert.ToInt32(btnEditItem.CommandArgument.ToString()), txtqty.Text, Convert.ToInt32(Session["userno"].ToString()));
                }
            }
            int TRNO = Convert.ToInt32(ViewState["TRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            int stno = Convert.ToInt32(ViewState["Stno"].ToString());

            string Status = ddlSelect.SelectedValue.ToString();

            string Remarks = txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
            int dept = Convert.ToInt32(ddlDept.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = (CustomStatus)objApp.UpdateAppPassEntry(TRNO, UA_NO, Status, Remarks, stno, dept, Aprdate, 0, Convert.ToChar(Session["sanctioning_authority"]));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Requisition Final Approved Successfully.", this);
                      //  pnllist.Visible = true;
                        pnlAdd.Visible = false;
                        ViewState["action"] = null;
                        clear_lblvalue();
                        clear();
                        //Add to clear the list
                        pnlAdd.Visible = false;
                        pnllist.Visible = false;
                        Panel1.Visible = false;   
                        pnlDept.Visible = true;
                        BindGrid();
                        ddlDept.SelectedIndex = 0;
                        pnlDept.Visible = true;
                        pnlItemDetail.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void ShowDetails(Int32 refs)
    {
        try
        {
            dsdetails = null;
          
            
            int c=Convert.ToInt32(Session["userno"]);
            int d =Convert.ToInt32(ViewState["Deptno"].ToString());
           
            string b = ViewState["TableName"].ToString();
            int a = Convert.ToInt32(ViewState["Stno"].ToString());


            dsdetails = objApp.GetApplDetail(refs, a, b, c,d);
            //dsdetails.Tables[1] = objApp.GetLeaveApplStatus(LETRNO);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                lblLeaveName.Text = ViewState["Stage"].ToString();
                lblReason.Text = refs.ToString();
            }
            lvStatus.DataSource = dsdetails.Tables[0];
            lvStatus.DataBind();

            GetReqDetails(refs);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    public void GetReqDetails(int Reqtrno)
    {
        if (Reqtrno > 0)
        {
            // ViewState["issueno"] = Reqtrno;
            Decimal AproxCost = 0;
            DataSet ds = ObjReq.GetReqItem(Reqtrno);
            ViewState["dsIssuedItem"] = ds;
            lvitemReq.DataSource = ds;
            lvitemReq.DataBind();
            hdnrowcount.Value = ds.Tables[0].Rows.Count.ToString();
            ViewState["REQ_SLIP_NO"] = ds.Tables[0].Rows[0]["REQ_NO"].ToString();

            lblReqSlipNo.Text = ds.Tables[0].Rows[0]["REQ_NO"].ToString();
            lblReqDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["REQ_DATE"]).ToString("dd/MM/yyyy");
            lblDeptName.Text = ds.Tables[0].Rows[0]["SDNAME"].ToString();
            lblAuthorityName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
            lblBudgetBalAmt.Text = ds.Tables[0].Rows[0]["BUDGET_BALANCE_AMOUNT"].ToString();
            lblInprocessBudgetAmt.Text = ds.Tables[0].Rows[0]["INPROCESS_BUDGET_AMOUNT"].ToString();

            if (ds.Tables[2].Rows.Count != 0)
            {
                lvItemDetails.DataSource = ds.Tables[2];
                lvItemDetails.DataBind();
            }
            else
            {
                lvItemDetails.DataSource = null;
                lvItemDetails.DataBind();
            }

            if (objCommon.LookUp("STORE_reference", "isnull(IS_BUDGET_HEAD,0)IS_BUDGET_HEAD", "IS_BUDGET_HEAD=1").Trim() == "1")
            {
                if (ds.Tables[1].Rows.Count > 0)
                    lblBudgetHead.Text = ds.Tables[1].Rows[0]["BUDGET_HEAD"].ToString();
                divShowAmt.Visible = true;
                 divShowInprocessAmt.Visible = true;
                divBudgetHead.Visible = true;
            }
            else
            {

                divShowAmt.Visible = false;
                divShowInprocessAmt.Visible = false;
                divBudgetHead.Visible = false;
            
            }

            pnlItemDetail.Visible = true;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AproxCost = AproxCost + Convert.ToDecimal(ds.Tables[0].Rows[i]["Tot_Cost"].ToString());
            }
            lblTotAppCost.Text = AproxCost.ToString();
            if (ds.Tables[0].Rows[0]["REQ_FOR"].ToString() == "I")
            {
                divBudgetHead.Visible = false;
                divShowAmt.Visible = false;
                divShowInprocessAmt.Visible = false;
                divApproxCost.Visible = false;
                //thApproxCost.Visible = false;
                hdnrowcount.Value = ds.Tables[0].Rows.Count.ToString();
                foreach (ListViewDataItem lv in lvitemReq.Items)
                {
                    HtmlControl tdApproxCost = (HtmlControl)lv.FindControl("tdApproxCost");
                    tdApproxCost.Visible = false;
                }
            }
            else
            {
                if (objCommon.LookUp("STORE_reference", "isnull(IS_BUDGET_HEAD,0)IS_BUDGET_HEAD", "IS_BUDGET_HEAD=1").Trim() == "1")
                {
                    divBudgetHead.Visible = true;
                    divShowAmt.Visible = true;
                    divShowInprocessAmt.Visible = true;

                }
                else
                {

                    divBudgetHead.Visible = false;
                    divShowAmt.Visible = false;
                    divShowInprocessAmt.Visible = false;
                }
               
                divApproxCost.Visible = true;
                //thApproxCost.Visible = true;
                hdnrowcount.Value = ds.Tables[0].Rows.Count.ToString();
                foreach (ListViewDataItem lv in lvitemReq.Items)
                {
                    HtmlControl tdApproxCost = (HtmlControl)lv.FindControl("tdApproxCost");
                    tdApproxCost.Visible = true;
                }
            }
        }
        else
        {
            lvitemReq.DataSource = null;
            lvitemReq.DataBind();
        }
    }

    public void BindGrid()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllSTAGE();

            ds = objCommon.FillDropDown("STORE_STAGE", "SNAME,TABLE_INV", "STNO", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                int max = Convert.ToInt32(objCommon.LookUp("STORE_STAGE AS S INNER JOIN STORE_PASSING_AUTHORITY_PATH AS P ON P.STNO=S.STNO INNER JOIN STORE_PASSING_AUTHORITY AS A ON (A.PANO=P.PAN01 OR A.PANO=P.PAN02 OR A.PANO=P.PAN03 OR A.PANO=P.PAN04 OR A.PANO=P.PAN05) ", "COUNT(*)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                GridView1.DataSource = Enumerable.Range(0, 1).Select(a => new
                {
                    ApplicationName = ds.Tables[0].Rows[a]["SNAME"].ToString(),
                    ApplicationID = a
                });
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.BindGrid ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
        // DataSet ds = objApp.GetAllSTAGE();        
    }

    protected void cbApplication_CheckedChanged(object sender, EventArgs e)
    {
        int cnt_sub;
        //int DEPTNO = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENTUSER", "DISTINCT(MDNO)", "UA_NO=" + Convert.ToInt32(Session["userno"]) + ""));
        int DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
        DataSet ds = objApp.GetPendListforLeaveApproval(Convert.ToInt32(Session["userno"]), DEPTNO, Convert.ToInt32(Session["OrgId"]));   //gayatri 09-03-2022
        CheckBox cbApplication = sender as CheckBox;
        GridViewRow parentRow = cbApplication.NamingContainer as GridViewRow;
        string applicationID = GridView1.DataKeys[parentRow.RowIndex].Value.ToString();
        DataList dlTestCases = parentRow.FindControl("dlTestCases") as DataList;
        string tabname = Convert.ToString(objCommon.LookUp("STORE_STAGE", "TABLE_INV", "SNAME LIKE '%" + cbApplication.Text + "%'"));
        if (cbApplication.Checked)
        {
            cnt_sub = ds.Tables[parentRow.RowIndex].Rows.Count;
            dlTestCases.DataSource = Enumerable.Range(0, cnt_sub).Select(a => new
            {
                CaseName = (getdetail(ds.Tables[parentRow.RowIndex].Rows[a]["COL1"].ToString(), tabname).ToString()),
                CaseID = a,
                REFS = (ds.Tables[parentRow.RowIndex].Rows[a]["COL1"])
            });
            ViewState["TableName"] = Convert.ToString(objCommon.LookUp("store_stage", "table_inv", "sname like '%" + cbApplication.Text + "%'"));
            ViewState["Stage"] = cbApplication.Text;
            ViewState["Stno"] = Convert.ToString(objCommon.LookUp("store_stage", "stno", "sname like '%" + cbApplication.Text + "%'"));
        }
        else
            dlTestCases.DataSource = null;
        dlTestCases.DataBind();
    }

    protected void edit_click(object sender, EventArgs e)
    {
        ImageButton show = sender as ImageButton;
        int val = Convert.ToInt32(show.ToolTip);
        ViewState["TRNO"] = val;
        pnlAdd.Visible = true;
        pnllist.Visible = false;
        Panel1.Visible = false;               
        pnlDept.Visible = false;
        ShowDetails(val);
        ViewState["action"] = "edit";
    }

    private void FillDepartment()
    {
        try
        {
            // Session["sanctioning_authority"]='Y' means internally sanctioning authorities which are not in passing path will insert in APP_ENTRY table 
            // for approval. It is depend upon the Requisition Amount.
            if (Convert.ToChar(Session["sanctioning_authority"]) == 'Y')
            {
                if (Convert.ToInt32(Session["strdeptcode"]) != Convert.ToInt32(Application["strrefmaindept"]))
                {
                    objCommon.FillDropDownList(ddlDept, "STORE_REQ_MAIN R INNER JOIN STORE_DEPARTMENT D ON (R.MDNO = D.MDNO)", "DISTINCT R.MDNO", "D.MDNAME", "REQTRNO IN (SELECT TRNO FROM STORE_APP_PASS_ENTRY PE INNER JOIN store_passing_authority a on (a.pano=PE.pano or a.pano=PE.pano or a.pano=PE.pano or a.pano=PE.pano or a.pano=PE.pano) WHERE STATUS='P' AND a.ua_no=" + Convert.ToInt32(Session["userno"]) + ")", "");
                    // objCommon.FillDropDownList(ddlDept, "store_passing_authority_path as p inner join STORE_DEPARTMENT S on p.deptno=s.MDNO inner join store_passing_authority a on (a.pano=p.pan05 or a.pano=p.pan04 or a.pano=p.pan03 or a.pano=p.pan02 or a.pano=p.pan01)", " DISTINCT DEPTNO", "MDNAME", "ua_no=" + Convert.ToInt32(Session["userno"]), "  MDNAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlDept, "store_passing_authority_path as p inner join STORE_DEPARTMENT S on p.deptno=s.MDNO", "DISTINCT DEPTNO", "MDNAME", "", "MDNAME");
                }
            }
            else
            {
                objCommon.FillDropDownList(ddlDept, "store_passing_authority_path as p inner join STORE_DEPARTMENT S on p.deptno=s.MDNO inner join store_passing_authority a on (a.pano=p.pan05 or a.pano=p.pan04 or a.pano=p.pan03 or a.pano=p.pan02 or a.pano=p.pan01)", " DISTINCT DEPTNO", "MDNAME", "ua_no=" + Convert.ToInt32(Session["userno"]), "  MDNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbApplication = GridView1.Rows[0].FindControl("cbApplication") as CheckBox;
            //// CheckBox cbApplication = sender as CheckBox;
            cbApplication.Checked = true;
            GridViewRow parentRow = cbApplication.NamingContainer as GridViewRow;
            string applicationID = GridView1.DataKeys[parentRow.RowIndex].Value.ToString();
            DataList dlTestCases = parentRow.FindControl("dlTestCases") as DataList;
            dlTestCases.DataSource = null;
            dlTestCases.DataBind();
            ViewState["Deptno"] = ddlDept.SelectedValue;

            ////pnlDept.Visible = false;
            pnllist.Visible = true;
            Panel1.Visible = true;
            cbApplication_CheckedChanged(cbApplication, e);
            if (objCommon.LookUp("STORE_CONFIG", "PARAMETER", "CONFIGDESC='CONDENSE REQUISITION CREATED BY APPROVAL'").Trim() == "Y")
            {
                chkClubRequisition.Visible = true;
            }
            else
            {
                chkClubRequisition.Visible = false;
            }

            BindPendingListOfReq();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.ddlDept_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindPendingListOfReq()
    {
        DataSet ds = objApp.GetPendListforRequisitionApproval(Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlDept.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvReqList.DataSource = ds;
            lvReqList.DataBind();

            foreach (ListViewDataItem item in lvReqList.Items)
            {
                Label lblStatus = item.FindControl("lblStatus") as Label;
                if (lblStatus.Text == "REJECTED")
                {
                    lblStatus.Style.Add("color", "Red");
                }
                else
                {
                    lblStatus.Style.Add("color", "Green");
                }
            }
        }
        else
        {
            lvReqList.DataSource = null;
            lvReqList.DataBind();
        }

    }

    protected void TypeReport()
    {
        string tablename = ViewState["TableName"].ToString();
        switch (tablename)
        {
            case "UAIMSSTORE.STR_REQ_MAIN":
                {
                    reqReport();
                    break;
                }
            case "STORE_PORDER":
                {
                    POREPORT();
                    break;
                }
            default:
                {
                    //ERRMESS();
                    break;
                }
        }
    }

    protected void reqReport()
    {
        //objCommon.ReportPopUp(btnReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Indent_Slip.rpt&param=@username=" + Session["userfullname"].ToString() + "," + "@P_REQTRNO=" + Convert.ToInt32(ViewState["TRNO"]) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
        string rptFileName = "Str_Indent_Slip.rpt";
        string reportTitle = "Requisition";
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Store," + rptFileName;
            url += "&param=@username=" + Session["userfullname"].ToString() + ",@P_REQTRNO=" + Convert.ToInt32(ViewState["TRNO"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void POREPORT()
    {
        string rptFileName, reportTitle;
        reportTitle = "Purchase_Order_Report";
        DataSet dss = objstrPO.GetSinglePONO(Convert.ToInt32(ViewState["TRNO"]));
        if (dss.Tables[0].Rows[0]["ISTYPE"].ToString() != "D")
        {
            rptFileName = "Str_Purchase_order_Report.rpt";
        }
        else
        {
            rptFileName = "str_porder_dpurchase.rpt";
        }
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Store," + rptFileName;
            url += "&param=@P_PORDNO=" + Convert.ToInt32(ViewState["TRNO"]) + "," + "@username=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Approval_Requisition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ERRMESS()
    {
        objCommon.DisplayMessage("No Report Attached to this File", this);
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        this.TypeReport();
    }

    protected string getdetail(string a, string tabname)
    {
        string val = Convert.ToString(a);
        switch (tabname)
        {
            case "STORE_REQ_MAIN":
                {
                    //val = Convert.ToString(objCommon.LookUp("STORE_REQ_MAIN", "REQ_NO", "REQTRNO=" + Convert.ToInt32(a)));
                    int PANO = Convert.ToInt32(objCommon.LookUp("STORE_PASSING_AUTHORITY", "PANO", "UA_NO=" + Session["userno"].ToString()));
                    //val = Convert.ToString(objCommon.LookUp("STORE_REQ_MAIN A INNER JOIN STORE_APP_PASS_ENTRY B ON (A.REQTRNO=B.TRNO)", "REQ_NO+' '+CONVERT(NVARCHAR(15),REQ_DATE,106)+'--'+ case WHEN B.[STATUS]='A' THEN 'APPROVED' ELSE 'PENDING' END", "REQTRNO=" + Convert.ToInt32(a)));

                    val = Convert.ToString(objCommon.LookUp("STORE_REQ_MAIN A INNER JOIN STORE_APP_PASS_ENTRY B ON (A.REQTRNO=B.TRNO)", "REQ_NO+'--'+CONVERT(NVARCHAR(15),REQ_DATE,106)+'--'+ case WHEN B.[STATUS]='A' THEN 'APPROVED' WHEN B.[STATUS]='R' THEN 'REJECTED' ELSE 'PENDING' END", " B.PANO=" + PANO + "  AND REQTRNO=" + Convert.ToInt32(a)));
                    break;
                }
            case "STORE_PORDER":
                {
                    val = Convert.ToString(objCommon.LookUp("STORE_PORDER", "REFNO", "PORDNO=" + Convert.ToInt32(a)));
                    break;
                }
            default:
                {
                    break;
                }
        }
        return val;
    }

    protected void lvitemReq_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "modify")
        {
            TextBox txtQty = e.Item.FindControl("txtqty") as TextBox;
            Label lblQty = e.Item.FindControl("lblQty") as Label;
            //Button btnSave = e.Item.FindControl("btnSave") as Button;
            //btnSave.Visible = true;
            txtQty.Visible = true;
            lblQty.Visible = false;
            txtQty.Focus();
        }
        if (e.CommandName == "remove")
        {
            objApp.DeleteItemQTY(ViewState["TRNO"].ToString(), Convert.ToInt32(e.CommandArgument.ToString()), "0", Convert.ToInt32(Session["userno"].ToString()));
            ShowDetails(Convert.ToInt32(ViewState["TRNO"].ToString()));
        }
    }

    protected void lvitemReq_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkItem = e.Item.FindControl("chkItem") as CheckBox;
        HiddenField hdnARstaus = e.Item.FindControl("hdbAcceptReject") as HiddenField;
        if (hdnARstaus.Value == "A")
        {
            chkItem.Checked = true;
        }
        else if (hdnARstaus.Value == "R")
        {
            chkItem.Checked = false;
            chkItem.Enabled = false;
        }
    }

    protected void chkClubRequisition_onchange(object sender, EventArgs e)
    {
        if (chkClubRequisition.Checked)
        {
            DataSet ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY P,STORE_REQ_MAIN AS T,STORE_PASSING_AUTHORITY_PATH AS H", "ROW_NUMBER() OVER (ORDER BY P.PANO) AS CASEID,T.REQ_NO CaseName,P.UA_NO ,REQTRNO as REFS", "", "P.UA_NO=" + Session["userno"].ToString() + " AND T.STAPPROVAL='A' AND H.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND T.MDNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " and [STATUS]='S' and T.CLUBREQSTATUS IS NULL", "REQ_DATE desc");
            objCommon.FillDropDownList(ddlbudget, "STORE_BUDGET_HEAD B,STORE_BUDGETHEAD_ALLOCTION A", "A.BHALNO", "B.BHNAME", "A.BHNO=B.BHNO", "BHNAME");
            dlTestCases.DataSource = ds;
            dlTestCases.DataBind();
            tdpending.Visible = false;
           
            tdapprove.Visible = true;
        }
        else
        {
            dlTestCases.DataSource = null;
            dlTestCases.DataBind();
            tdpending.Visible = true;
           
            tdapprove.Visible = false;
        }
    }

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    protected void imgFile_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton btn = sender as ImageButton;
        //DownloadFile(btn.AlternateText, btn.CommandName, btn.CommandArgument);

        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void DownloadFile(string fileName, string ItemName, string FilePath)
    {
        try
        {
            if (fileName != "")
            {
                if (lblReqSlipNo.Text == "")
                {
                   // path = Docpath + "STORES\\REQUISITIONS\\REQ_" + (lblReqSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ItemName.Trim();
                    path = FilePath + "\\" + fileName;
                }
                else
                {
                    // path = Docpath + "STORES\\REQUISITIONS\\REQ_" + (lblReqSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ItemName.Trim();
                    // path = Docpath + "STORES\\REQUISITIONS\\REQ_" + (lblReqSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\" + ItemName.Trim();
                    path = FilePath + "\\" + fileName;

                }



                //FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
                FileStream sourceFile = new FileStream((path), FileMode.Open);

                long fileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)fileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                sourceFile.Dispose();

                Response.ClearContent();
                Response.Clear();
                Response.BinaryWrite(getContent);
                Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Showmessage("File Not Found.");
                return;
            }
        }
        catch (Exception ex)
        {
            //throw;
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    //private string FillRequisitionNo()
    //{
    //    DataSet ds = new DataSet();
    //    ds = ObjReq.GenrateReq(Convert.ToInt32(Session["strdeptcode"].ToString()), Convert.ToInt32(Session["OrgId"]));
    //    return Convert.ToString(ds.Tables[0].Rows[0]["REQNO"].ToString());
    //}

    protected void btnClubReq_click(object sender, EventArgs e)
    {
        try
        {
            Str_DeptRequest objdeptRequest = new Str_DeptRequest();
            objdeptRequest.COLLEGE_CODE = Session["colcode"].ToString();


            objdeptRequest.BHALNO = Convert.ToInt32(ddlbudget.SelectedValue);

            objdeptRequest.SDNO = Convert.ToInt32(Session["strdeptcode"].ToString());
            objdeptRequest.REQ_DATE = DateTime.Now;
           // objdeptRequest.REQ_NO = FillRequisitionNo();

            objdeptRequest.STATUS = 'S';
            objdeptRequest.REMARK = "This club requisition created by " + Session["userfullname"].ToString();
            objdeptRequest.NAME = Session["userfullname"].ToString();
            objdeptRequest.TEQIP = false;

            string reqNos = ReqNOS.Value.ToString();

            DataSet dsItems = objCommon.FillDropDown("STORE_REQ_TRAN", "SUM(REQ_QTY) as REQ_QTY,ITEM_NO", "", "REQTRNO IN (" + reqNos + ") GROUP BY ITEM_NO", "");


            for (int i = 0; i < dsItems.Tables[0].Rows.Count; i++)
            {
                objdeptRequest.ITEMNO = Convert.ToInt32(dsItems.Tables[0].Rows[i]["ITEM_NO"].ToString());

                //CHECK ITEM APPROVAL

                objdeptRequest.STAPPROVAL = "A";


                objdeptRequest.REQ_QTY = Convert.ToInt32(dsItems.Tables[0].Rows[i]["REQ_QTY"].ToString());
                objdeptRequest.RATE = Convert.ToDecimal(0);

                //objdeptRequest.TECHSPEC = this.SpecificationDoc();

                objdeptRequest.REMARKTRN = "Passed by " + Session["userfullname"].ToString();

                objdeptRequest.TECHSPEC = null;



                CustomStatus cs = (CustomStatus)ObjReq.AddDeptRequest(objdeptRequest);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["action"] = "add";

                }
                else
                {
                    return;
                }

            }

            int retval = ObjReq.updateReqStatus(reqNos);
            if (retval > 0)
            {
                DataSet ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY P,STORE_REQ_MAIN AS T,STORE_PASSING_AUTHORITY_PATH AS H", "ROW_NUMBER() OVER (ORDER BY P.PANO) AS CASEID,T.REQ_NO CaseName,P.UA_NO ,REQTRNO as REFS", "", "P.UA_NO=" + Session["userno"].ToString() + " AND T.STAPPROVAL='P' AND H.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND T.MDNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " and [STATUS]='S' and T.CLUBREQSTATUS IS NULL", "REQ_DATE desc");
                objCommon.FillDropDownList(ddlbudget, "STORE_BUDGET_HEAD B,STORE_BUDGETHEAD_ALLOCTION A", "A.BHALNO", "B.BHNAME", "A.BHNO=B.BHNO", "BHNAME");
                dlTestCases.DataSource = ds;
                dlTestCases.DataBind();
                objCommon.DisplayMessage("Requisitions are club successfully", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }


}
