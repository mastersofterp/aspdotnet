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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Threading;
public partial class ACADEMIC_NeftRtgsPaymentReconcile : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    //ReceiptPaymentController objRP = new ReceiptPaymentController();
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

                if (Session["usertype"].ToString() == "1")
                {
                    CheckPageAuthorization();
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPaymentReconcile.aspx");
                }                
            }
            objCommon.FillDropDownList(ddlReceipt, "ACD_NEFT_RTGS_PAYMENT_LOG P INNER JOIN ACD_RECIEPT_TYPE R ON(P.RECEIPT_NO=R.RCPTTYPENO)", "DISTINCT P.RECEIPT_NO", "RECIEPT_TITLE", "P.RECEIPT_NO > 0", "P.RECEIPT_NO");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPaymentReconcile.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NeftRtgsPaymentReconcile.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            lvReconcile.DataSource = null;
            lvReconcile.DataBind();
            DataSet ds = feeController.Get_NEFT_RTGS_StudentList(Convert.ToInt32(ddlReceipt.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvReconcile.DataSource = ds;
                lvReconcile.DataBind();
                pnlReconcile.Visible = true;
                lvReconcile.Visible = true;
                btnSubmit.Enabled = true;
                hdncount1.Value = lvReconcile.Items.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found.", this.Page);
                lvReconcile.DataSource = null;
                lvReconcile.DataBind();
                pnlReconcile.Visible =false;
                lvReconcile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPaymentReconcile.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int chkCount = 0;
            int updCount=0;
            int idno=0;
            int semesterno=0;
            foreach (ListViewDataItem lv in lvReconcile.Items)
            {
                CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
                HiddenField hdnIdno = lv.FindControl("hdnIdno") as HiddenField;
                HiddenField hdnSem = lv.FindControl("hdnSemester") as HiddenField;
                Label lblTransactionId=lv.FindControl("lblTransactionID") as Label;
                if (chkSelect.Checked)
                {
                    chkCount++;
                    idno=Convert.ToInt32(hdnIdno.Value);
                    semesterno=Convert.ToInt32(hdnSem.Value);
                    int sessionno=Convert.ToInt32(objCommon.LookUp("ACD_NEFT_RTGS_PAYMENT_LOG","SESSIONNO","IDNO= "+idno+" AND RECEIPT_NO="+Convert.ToInt32(ddlReceipt.SelectedValue)+" AND SEMESTERNO= "+semesterno+" AND TRANSACTIONID="+"'"+lblTransactionId.Text+"'"));

                    CustomStatus cs = (CustomStatus)feeController.Update_Recon_status_For_NEFT_RTGS(idno, Convert.ToInt32(ddlReceipt.SelectedValue), sessionno, Convert.ToInt32(ddlSemester.SelectedValue), lblTransactionId.Text);
                    if(cs.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;
                    }                    
                }
            }
            if (chkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student.", this.Page);
                return;
            }
            if (chkCount == updCount && chkCount > 0 && updCount > 0)
            {
                objCommon.DisplayMessage(this.Page, "Payment Reconcile Saved Successfully.", this.Page);
                Clear();
                btnSubmit.Enabled = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPaymentReconcile.btnSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.FillDropDownList(ddlSemester, "ACD_NEFT_RTGS_PAYMENT_LOG P INNER JOIN ACD_SEMESTER S ON(P.SEMESTERNO = S.SEMESTERNO)", "DISTINCT P.SEMESTERNO", "S.SEMESTERNAME", "P.RECEIPT_NO= " + Convert.ToInt32(ddlReceipt.SelectedValue), "P.SEMESTERNO");
            }
            ddlSemester.Focus();
            ddlSemester.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPaymentReconcile.ddlReceipt_SelectedIndexChanged()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {
        ddlReceipt.SelectedIndex = 0;
        ddlSemester.SelectedIndex= 0;
        pnlReconcile.Visible = false;
        lvReconcile.Visible = false;
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
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvReconcile.DataSource = null;
        lvReconcile.DataBind();
        pnlReconcile.Visible = false;
        lvReconcile.Visible = false;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GvStudent = new GridView();
            DataSet ds = feeController.Get_NEFT_RTGS_StudentListExcel(Convert.ToInt32(ddlReceipt.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GvStudent.DataSource = ds;
                GvStudent.DataBind();
                string attachment = "attachment; filename=NEFT/RTGS_ReconcilePaymentDetails.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/xlsx";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GvStudent.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "NeftRtgsPaymentReconcile.btnExcel_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}