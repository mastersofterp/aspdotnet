using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using mastersofterp_MAKAUAT;
using System.IO;
using System.Net;
using System.Data;
using System.Threading;
public partial class ACADEMIC_NeftRtgsPayment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ReceiptPaymentController objRP = new ReceiptPaymentController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
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

                if (Session["usertype"].ToString() == "2")
                {
                    CheckPageAuthorization();
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPayment.aspx");
                }                
            }
            objCommon.FillDropDownList(ddlReceipt, "ACD_DEMAND D INNER JOIN ACD_RECIEPT_TYPE R ON(D.RECIEPT_CODE=R.RECIEPT_CODE)", "DISTINCT RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO = 1","RCPTTYPENO");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPayment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPayment.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string File = string.Empty;
            int idno = Convert.ToInt32(Session["idno"]);
            int receiptNo = 0;
            string receiptCode = string.Empty;
            int semesterno = 0;
            string transactionID = string.Empty;
            DateTime transactionDate = new DateTime();
            string bankName = string.Empty;
            decimal amount = 0;
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            string log= objCommon.LookUp("ACD_NEFT_RTGS_PAYMENT_LOG", "LOG_NO", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND RECEIPT_NO= " + Convert.ToInt32(ddlReceipt.SelectedValue) + " AND SEMESTERNO= " + Convert.ToInt32(ddlSemester.SelectedValue));
            if (!log.Equals(string.Empty))
            {
                objCommon.DisplayMessage(this.Page, "Payment Details Is Already Exists.", this.Page);
                return;
            }
            if (!ddlReceipt.SelectedValue.Equals(string.Empty)) receiptNo = Convert.ToInt32(ddlReceipt.SelectedValue);
            if (!ddlSemester.SelectedValue.Equals(string.Empty)) semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            if (!txtTransaction.Text.Trim().Equals(string.Empty)) transactionID = txtTransaction.Text.Trim().ToUpper();
            if (!txtDate.Text.Equals(string.Empty)) transactionDate = Convert.ToDateTime(txtDate.Text);
            if (!txtBankName.Text.Equals(string.Empty)) bankName = txtBankName.Text.Trim().ToUpper();
            if (!txtAmount.Text.Trim().Equals(string.Empty)) amount =Convert.ToDecimal(txtAmount.Text.Trim());
            string RCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO= " + Convert.ToInt32(ddlReceipt.SelectedValue));

            if (!fuUpload.FileName.Equals(""))
            {
                string path = MapPath("~/UPLOAD_FILES/NEFT_RTGS_DOCUMENT");
                try
                {
                    if (!(Directory.Exists(path)))
                        Directory.CreateDirectory(path);
                    if (fuUpload.HasFile)
                    {

                        if (fuUpload != null)
                        {
                            string[] validFileTypes = { "pdf", "jpg", "jpeg" };
                            string ext1 = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                            bool isValidFile = false;
                            for (int i = 0; i < validFileTypes.Length; i++)
                            {
                                if (ext1 == "." + validFileTypes[i])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            if (fuUpload == null)
                            {
                                objCommon.DisplayMessage(this.Page, "Select File to Upload or Uploaded file size should be greater than 0 kb !", this.Page);
                                return;
                            }
                            if (!fuUpload.PostedFile.ContentLength.Equals(string.Empty) || fuUpload.PostedFile.ContentLength != null)
                            {
                                int fileSize = fuUpload.PostedFile.ContentLength;
                                int KB = fileSize / 1024;
                                if (KB > 300)
                                {
                                    objCommon.DisplayMessage(updPayment, "Uploaded File size should be less than 300 kb.", this.Page);
                                    return;
                                }
                            }
                            if (!isValidFile)
                            {
                                objCommon.DisplayMessage(this.Page, "Upload the File only with following formats: .pdf,jpg,jpeg", this.Page);
                                return;
                            }
                            else
                            {
                                string[] array1 = Directory.GetFiles(path);
                                foreach (string str in array1)
                                {
                                    if ((path + fuUpload.FileName.ToString().Replace(' ', ' ')).Equals(str))
                                    {
                                        objCommon.DisplayMessage(this.Page, "File Already Exists!", this.Page);
                                        return;
                                    }
                                }
                                File = fuUpload.FileName.ToString();
                                fuUpload.SaveAs(MapPath("~/UPLOAD_FILES/NEFT_RTGS_DOCUMENT/" + fuUpload.FileName.Replace(' ', ' ')));
                                lblUpload.Visible = true;
                                lblUpload.Text = File;
                                lblUpload.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "NeftRtgsPayment.btnSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
            }

            CustomStatus cs = (CustomStatus)objRP.Add_NEFT_RTGS_Payment_Offline(idno, receiptNo, RCode, semesterno, transactionID, transactionDate, bankName, amount, File, Convert.ToInt32(Session["userno"]), ipAddress);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updPayment, "Payment Details Saved Successfully.", this.Page);
                Clear();
            }
            else
            {
                objCommon.DisplayMessage(updPayment, "Payment Details Not Saved Successfully.", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPayment.btnSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceipt.SelectedIndex > 0)
            {
                string receiptCode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO= " + Convert.ToInt32(ddlReceipt.SelectedValue));
                objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND D INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO=S.SEMESTERNO)", "DISTINCT D.SEMESTERNO", "SEMESTERNAME", "RECIEPT_CODE=" + "'" + receiptCode + "'" + " AND IDNO= " + Convert.ToInt32(Session["idno"]), "D.SEMESTERNO");
            }
            ddlSemester.SelectedIndex = 0;

            DataSet ds = objRP.Get_NEFT_RTGS_StudentListByIdno(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlReceipt.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                pnlPaymentDetails.Visible = true;
                lvDetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPayment.ddlReceipt_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {
        ddlReceipt.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        txtTransaction.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtAmount.Text = string.Empty;
        lblUpload.Text = string.Empty;
        lblUpload.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            DataSet ds = objRP.Get_NEFT_RTGS_StudentListByIdno(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlReceipt.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
                pnlPaymentDetails.Visible = true;
                lvDetails.Visible = true;
            }
        }
    }
    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkDownload = (ImageButton)(sender);
        string filename = lnkDownload.CommandArgument;
        string filepath = Server.MapPath("~/UPLOAD_FILES/NEFT_RTGS_DOCUMENT/" + filename);
        fileDownload(filename, filepath);

    }
    private void fileDownload(string fileName, string fileUrl)
    {
        Page.Response.Clear();
        bool success = ResponseFile(Page.Request, Page.Response, fileName, fileUrl, 1024000);
        if (!success)
        {
            objCommon.DisplayMessage(this.Page, "File Not Found!", this.Page);
            return;
        }
        Page.Response.End();
    }

    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
}