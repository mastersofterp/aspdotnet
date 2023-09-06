//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL
// PAGE NAME     : BULK EMPLOYEE PHOTO UPLOADED                                                    
// CREATION DATE :                                                      
// ADDED BY      : ZUBAIR AHMAD 
// ADDED DATE    : 23-FEB-2015                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.IO;
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

public partial class PAYROLL_TRANSACTIONS_Pay_Bulk_Photo_Update : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    PayController objPay = new PayController();

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
               // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                this.FillDropDown();
                // btnSubmit.Visible = false;
            }


        }
        divMsg.InnerHtml = string.Empty;

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
        }
    }

    private void BindListViewList(int staffNo, int deptNo,int collegeno)
    {
        try
        {
            lvUpdatePhoto.Visible = true;
            DataSet ds = objPay.GetEmployeeForUpdateBulkPhoto(staffNo, deptNo, collegeno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                btnReport.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
                btnReport.Visible = false;
            }


            lvUpdatePhoto.DataSource = ds;
            lvUpdatePhoto.DataBind();

            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                HiddenField hididno = lvUpdatePhoto.Items[i].FindControl("hididno") as HiddenField;
                Image ImgPhoto = lvUpdatePhoto.Items[i].FindControl("ImgPhoto") as Image;
                ImgPhoto.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[i]["IDNO"].ToString() + "&type=EMP";

                Image ImgSignature = lvUpdatePhoto.Items[i].FindControl("ImgSignature") as Image;
                ImgSignature.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[i]["IDNO"].ToString() + "&type=EMPSIGN";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.BindListViewList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.BindListViewList(Convert.ToInt32(ddlStaffName.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        //btnSubmit.Visible = true;
    }

    protected void bntSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (ListViewDataItem lvitem in lvUpdatePhoto.Items)
            {
                HiddenField hididno = lvitem.FindControl("hididno") as HiddenField;
                FileUpload fuEmpPhoto = lvitem.FindControl("fuEmpPhoto") as FileUpload;
                FileUpload fuEmpSignature = lvitem.FindControl("fuEmpSignature") as FileUpload;

                byte[] image;
                byte[] signature_image;
                if (fuEmpPhoto.HasFile)
                {
                    //image = objCommon.GetImageData(fuStudPhoto);
                    image = this.ResizePhoto(fuEmpPhoto);
                }
                else
                {
                    image = null;
                }

                if (fuEmpSignature.HasFile)
                {
                    //image = objCommon.GetImageData(fuStudPhoto);
                    signature_image = this.ResizePhoto(fuEmpSignature);
                }
                else
                {
                    signature_image = null;
                }

                CustomStatus cs = (CustomStatus)objPay.Update_Bulk_Photo(Convert.ToInt32(hididno.Value), image);
                CustomStatus cs1 = (CustomStatus)objPay.Update_Bulk_Signature(Convert.ToInt32(hididno.Value), signature_image);
            }
            this.BindListViewList(Convert.ToInt32(ddlStaffName.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            objCommon.DisplayMessage("Record Updated Successfully", this);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.butSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillDropDown()
    {


        try
        {
            objCommon.FillDropDownList(ddlStaffName, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO>0", "SUBDEPTNO");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.FillUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCollege.SelectedIndex = 0;       // add 23-09-2022
        ddlStaffName.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        DataSet ds = null;
        lvUpdatePhoto.DataSource = ds;
        lvUpdatePhoto.DataBind();
        lvUpdatePhoto.Visible = false;
        btnReport.Visible = false;
        btnSubmit.Visible = false;
        //pnlUpdatePhoto.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("BULK_PHOTO_UPLOAD", "Pay_EmployeePhotos.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.butReport_Click1()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_STAFFNO=" + ddlStaffName.SelectedValue + ",@P_DEPTNO=" + ddlDepartment.SelectedValue + ",username=" + Session["userfullname"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public byte[] ResizePhoto(FileUpload fu)
    {

        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListViewList(Convert.ToInt32(ddlStaffName.SelectedValue), Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

    }
}
