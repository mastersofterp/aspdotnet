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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
using Ionic.Zip;

using System.Drawing;


public partial class PAYROLL_TRANSACTIONS_Pay_Bulk_Photo_Update : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    String Type = string.Empty;
    string FileName = string.Empty;
    static string ExtractedBy = string.Empty;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    PayController objP = new PayController();
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
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                // btnSubmit.Visible = false;
            }
                this.FillDropDown();
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
   
    protected void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffName, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
         }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Bulk_Photo_Update.FillUser-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void CheckType()
    {
        if (rdoEMPPHOTO.Checked == true)
        {
            Type = "PHOTO";
        }
        else if (rdoEMPSIG.Checked == true)
        {
            Type = "SIGNA";
        }
    }
    public void FileNameBy()
    {

        if (rdoEMPPHOTO.Checked == true || rdoEMPSIG.Checked == true)
        {
            if (ddlExtractedby.SelectedValue == "1")
            {
                ExtractedBy = "EmployeeCode";
                FileName = "EmployeeCode";
            }
            else if (ddlExtractedby.SelectedValue == "2")
            {
                ExtractedBy = "IDNO";
                FileName = "IDNO";
            }
            else if (ddlExtractedby.SelectedValue == "3")
            {
                ExtractedBy = "SEQ_NO";
                FileName = "SEQ_NO";
            }
        }
    }
    protected void PopulateDropDownList()
    {
        try
        {
            //FILL STAFF
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_NAME", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");            
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID ASC");
            objCommon.FillDropDownList(ddlStaffName, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_BulkPhoto_Extract.aspx.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0 && ddlStaffName.SelectedIndex > 0)
        {
            if (rdoEMPPHOTO.Checked == true)
            {
                Type = "PHOTO";
                TotalEmployeeCount(Type);

            }
            else if (rdoEMPSIG.Checked == true)
            {
                Type = "SIGNA";
                TotalEmployeeCount(Type);
            }
        }
    }
    protected void ddlStaffName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0 && ddlStaffName.SelectedIndex > 0)
        {
            if (rdoEMPPHOTO.Checked == true)
            {
                Type = "PHOTO";
                TotalEmployeeCount(Type);

            }
            else if (rdoEMPSIG.Checked == true)
            {
                Type = "SIGNA";
                TotalEmployeeCount(Type);
            }
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void ddlExtractedby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExtractedby.SelectedValue == "1")
        {
            ExtractedBy = "EmployeeCode";
        }
        else if (ddlExtractedby.SelectedValue == "2")
        {
            ExtractedBy = "IDNO";
        }
        else if (ddlExtractedby.SelectedValue == "3")
        {
            ExtractedBy = "SEQ_NO";
        }
    }
    protected void btnExtract_Click(object sender, EventArgs e)
    {
        try
        {
            FileNameBy();
            CheckType();
            if (FileName != "" && Type != "")
            {
                string FolderName = ddlStaffName.SelectedItem.Text;
                string collegename = ddlCollege.SelectedItem.Text;

                //ExtractedBy = ddlExtractedby.SelectedItem.Text;
                //if (!Directory.Exists("C://"+collegename+"//" + FolderName+"_"))
                //{
                //    Directory.CreateDirectory("C://" + collegename + "//" + FolderName+"_");
                //}
                //string DirPath = "C://" + collegename + "//" + FolderName+"_";

                if (!Directory.Exists("C://" + collegename + "//"))
                {
                    Directory.CreateDirectory("C://" + collegename + "//");
                }
                string DirPath = "C://" + collegename + "//";
                if (Directory.Exists(DirPath))
                {
                    if (Type == "PHOTO")
                    {

                        DataSet ds = objP.GetEmployeePhoto(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffName.SelectedValue), ExtractedBy);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            using (ZipFile zip = new ZipFile())
                            {
                                // FIRST DELETE PREVIOUS FILES...
                                Array.ForEach(Directory.GetFiles(DirPath), File.Delete);
                                // .......................
                                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip.AddDirectoryByName("files");
                                foreach (DataTable table in ds.Tables)
                                {
                                    foreach (DataRow row in table.Rows)
                                    {
                                        try
                                        {
                                            byte[] imgData = null;
                                            imgData = row["" + Type + ""] as byte[];

                                            byte[] imgDataee = null;//System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/images/528.jpg"));
                                            imgDataee = new byte[17406];
                                            if (imgData.Length != imgDataee.Length)
                                            {
                                                MemoryStream memStream = new MemoryStream(imgData);
                                                System.Drawing.Bitmap.FromStream(memStream).Save(DirPath + row[FileName].ToString() + ".jpg");//.jpg
                                                //System.Drawing.Bitmap.FromStream(memStream).Save(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"));


                                                string filename;
                                                filename = row[FileName].ToString() + ".jpg";
                                                string filePath = DirPath + filename;
                                                zip.AddFile(filePath, "files");
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            throw new IITMSException("IITMS.UAIMS.showimage->" + ex.ToString());

                                        }
                                    }
                                }
                                Response.Clear();
                                Response.BufferOutput = false;
                                string ZipFileName = ddlCollege.SelectedItem.Text + "_" + ddlStaffName.SelectedItem.Text + "_" + (ddlExtractedby.SelectedValue == "0" ? "" : ddlExtractedby.SelectedItem.Text.Trim());
                                string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                                Response.ContentType = "application/zip";
                                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                                zip.Save(Response.OutputStream);
                                Response.End();
                            }
                            objCommon.DisplayMessage(this.UpdatePanel1, "REOCRD EXTRACTED SELECTION", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "REOCRD NOT FOUND FOR THIS SELECTION", this.Page);
                        }

                        objCommon.DisplayMessage(this.UpdatePanel1, "RECORD EXTRACTED SUCCESSFULLY", this.Page);
                    }
                    else if (Type == "SIGNA")
                    {
                        DataSet ds = objP.GetEmployeeSign(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffName.SelectedValue), ExtractedBy);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            using (ZipFile zip = new ZipFile())
                            {
                                // FIRST DELETE PREVIOUS FILES...
                                Array.ForEach(Directory.GetFiles(DirPath), File.Delete);
                                // .......................
                                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                                zip.AddDirectoryByName("files");
                                foreach (DataTable table in ds.Tables)
                                {
                                    foreach (DataRow row in table.Rows)
                                    {
                                        try
                                        {
                                            byte[] imgData = null;
                                            imgData = row["" + Type + ""] as byte[];

                                            byte[] imgDataee = null;//System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/images/528.jpg"));
                                            imgDataee = new byte[17406];
                                            if (imgData.Length != imgDataee.Length)
                                            {

                                                MemoryStream memStream = new MemoryStream(imgData);
                                                System.Drawing.Bitmap.FromStream(memStream).Save(DirPath + row[FileName].ToString() + ".jpg");//.jpg
                                                string filename;
                                                filename = row[FileName].ToString() + ".jpg";
                                                string filePath = DirPath + filename;
                                                zip.AddFile(filePath, "files");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new IITMSException("IITMS.UAIMS.showimage->" + ex.ToString());
                                        }
                                    }
                                }
                                Response.Clear();
                                Response.BufferOutput = false;
                                string ZipFileName = ddlCollege.SelectedItem.Text + "_" + ddlStaffName.SelectedItem.Text + "_" + (ddlExtractedby.SelectedValue == "0" ? "" : ddlExtractedby.SelectedItem.Text.Trim());
                                string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                                Response.ContentType = "application/zip";
                                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                                zip.Save(Response.OutputStream);
                                Response.End();
                            }
                            objCommon.DisplayMessage(this.UpdatePanel1, "REOCRD EXTRACTED SELECTION", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "REOCRD NOT FOUND FOR THIS SELECTION", this.Page);
                        }
                        objCommon.DisplayMessage(this.UpdatePanel1, "RECORD EXTRACTED SUCCESSFULLY", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "EXTRACT FOLDER NOT SPECIFIED.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Something Went Wrong", this.Page);
            }
        }
        catch (Exception ex)
        {

            // throw new IITMSException("IITMS.UAIMS.Pay_BulkPhoto_Extract.aspx.btnExtract_Click-> " + ex.ToString());
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void rdoEMPPHOTO_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0 && ddlStaffName.SelectedIndex > 0)
        {
            if (rdoEMPPHOTO.Checked == true)
            {
                Type = "PHOTO";
                TotalEmployeeCount(Type);

            }
        }
    }
    protected void rdoEMPSIG_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0 && ddlStaffName.SelectedIndex > 0)
        {
            if (rdoEMPSIG.Checked == true)
            {
                Type = "SIGNA";
                TotalEmployeeCount(Type);
            }
        }
    }
    public void TotalEmployeeCount(string Type)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objP.GetEmployeeCount(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlStaffName.SelectedValue), Type);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltotal.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lbltotal.Text = "0";
            }
        }
        catch { }
    }
}
