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

using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;

public partial class ACADEMIC_EXAMINATION_QrCodeGeneration : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
             
               
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRegNo.Text = string.Empty;
        hfRegNo.Value = string.Empty;
        Session["qr"] = string.Empty;
        ViewState["File"] = string.Empty;
        Image1.ImageUrl = "~/Images/nophoto.jpg";
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void BtnGenerate_Click(object sender, EventArgs e)
    {
        string RegNo = txtRegNo.Text.ToString();
        string RegNo1 = objCommon.LookUp("ACD_STUDENT", "REGNO", "REGNO='" + RegNo + "'");
        QRCodeEncoder encoder = new QRCodeEncoder();
        encoder.QRCodeVersion = 10;
        Bitmap img = encoder.Encode(Session["qr"].ToString());
        img.Save(Server.MapPath("~\\QrCode Files\\" + RegNo1 + ".Jpeg"));
        img.Save(Server.MapPath("~\\QrCode Files\\img.Jpeg"));
        Image1.ImageUrl = "~\\QrCode Files\\img.Jpeg";
        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\QrCode Files\\img.Jpeg");
    }
 
    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }

    protected void txtRegNo_OnTextChanged(object sender, EventArgs e)
    {
        //string[] strRegNo = txtRegNo.Text;//SelectedValue.Trim().Split('*')
        //if (strRegNo.Length == 2)
        //{
        string RegNo = txtRegNo.Text.ToString();
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "*", "", "REGNO='" + RegNo + "'", "REGNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString().Trim();
                txtRegNo.ToolTip = (ds.Tables[0].Rows[0]["REGNO"].ToString());
                lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim();
             
            }
            int Idno = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "'"));
            DataSet ds1 = objQrC.GetStudentResultData(Idno);

            hfRegNo.Value = ds.Tables[0].Rows[0]["REGNO"].ToString().Trim();
            string Qrtext = "RegNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Session=" + ds1.Tables[0].Rows[0]["SESSION"].ToString().Trim() + "; S1(SGPA)=" +
                       ds1.Tables[0].Rows[0]["SGPA1"].ToString().Trim() + "; S2=" +
                       ds1.Tables[0].Rows[0]["SGPA2"].ToString().Trim() + "; S3=" +
                       ds1.Tables[0].Rows[0]["SGPA3"].ToString().Trim() + "; S4=" +
                       ds1.Tables[0].Rows[0]["SGPA4"].ToString().Trim() + "; S5=" +
                       ds1.Tables[0].Rows[0]["SGPA5"].ToString().Trim() + "; S6=" +
                       ds1.Tables[0].Rows[0]["SGPA6"].ToString().Trim() + "; S7=" +
                       ds1.Tables[0].Rows[0]["SGPA7"].ToString().Trim() + "; S8=" +
                       ds1.Tables[0].Rows[0]["SGPA8"].ToString().Trim() + "; CGPA=" +
                       ds1.Tables[0].Rows[0]["CGPA"].ToString().Trim() + ";"; 
        
                //string Qrtext = "RegNo:-" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "" ; 
                //    ds1.Tables[0].Rows[0]["BTYPENAME"].ToString().Trim() + " ; Subject:-" + dsSubjectNo.Tables[0].Rows[0]["SSUBNAME"].ToString().Trim() +
                //    " ; ISBN No.:-" + ds.Tables[0].Rows[0]["ISBN"].ToString() + " ; Class. No.:-" + ds.Tables[0].Rows[0]["CLASSNO"].ToString() + " ; Publisher:-" +
                //    dsPubNo.Tables[0].Rows[0]["PUBNAME"].ToString().Trim() + "<p> Author:-" + dsAuth1.Tables[0].Rows[0]["AUTHORNAME"].ToString().Trim();
                Session["qr"] = Qrtext.ToString();
            //}
            //else
            //    Session["qr"] = string.Empty;
      }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string RegNo = txtRegNo.Text.ToString();
            int Idno = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "'"));
           byte[] QR_IMAGE = ViewState["File"] as byte[];
           long ret = objQrC.AddUpdateQrCode(Idno, QR_IMAGE);
            if (ret > 0)
            {
                objCommon.DisplayMessage( "Record Saved Successfully!!", this.Page);
                //Clear();
                ShowReport("QrCode", "rpt_QrCode.rpt");
            }
            else
            {
                objCommon.DisplayMessage( "Record Not Saved,Try Again!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Exception Occured,Contact To Administrator!", this.Page);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("QrCode", "rpt_QrCode.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string RegNo = txtRegNo.Text.ToString();
            int Idno = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "'"));
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Idno + "";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
