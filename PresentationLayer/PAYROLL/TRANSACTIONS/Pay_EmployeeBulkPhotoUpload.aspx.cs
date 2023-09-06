using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class PAYROLL_TRANSACTIONS_Pay_EmployeeBulkPhotoUpload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpCreateController objEmp = new EmpCreateController();
    DataTable ds = new DataTable();
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    btnSave.Enabled = false;
                    this.FillDropDown();
                }

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RegistraionNoGeneration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeBulkPhotoUpload.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmployeeBulkPhotoUpload.aspx");
        }
    }

    protected void FillDropDown()
    {


        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            dt = ((DataTable)Session["EmpTbl"]);

            dt.Columns.Remove("SRNO");
            dt.Columns.Remove("SIZE");
            dt.Columns.Remove("ACTION");

            int ddlValue = Convert.ToInt32(ddlcategory.SelectedValue);

            CustomStatus cs = (CustomStatus)objEmp.EmployeeBulkPhotoUpdate(ddlValue, dt);
            System.Threading.Thread.Sleep(8000);
            if (Convert.ToInt32(cs) == 2)
            {
                ListView1.DataSource = null;
                ListView1.Items.Clear();
                btnSave.Enabled = false;
                lblmessageShow.Text = "Uploaded Successfully !";
                lblmessageShow.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.Items.Clear();
                btnSave.Enabled = false;
                lblmessageShow.Text = "Unable to Update !";
                lblmessageShow.ForeColor = System.Drawing.Color.OrangeRed;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_StudenBulkPhotoUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    public byte[] ResizePhoto(byte[] bytes)
    {
        byte[] image = null;
        System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(bytes);

        System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(myMemStream);

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
        return image;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ds.Columns.Add("SRNO");
            ds.Columns.Add("NAME");
            ds.Columns.Add("IMAGE", typeof(SqlBinary));
            ds.Columns.Add("SIZE");
            ds.Columns.Add("ACTION");
            byte[] image;
            byte[] bytes;
            int flag = 0;
            long checkSize = 0;

            int ddlValue = Convert.ToInt32(ddlcategory.SelectedValue);
            int srno = 1;

            if (fuStudPhoto.HasFile)
            {
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    //foreach (HttpPostedFile postedFile in fuStudPhoto.PostedFiles)
                    //{
                    HttpPostedFile postedFile = hfc[i];
                    string filename = Path.GetFileNameWithoutExtension(postedFile.FileName);

                    string contentType = postedFile.ContentType;
                    using (Stream fs = postedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {

                            image = br.ReadBytes((Int32)fs.Length);
                            checkSize += image.Length;
                            if (checkSize > 1073714824)
                            {
                                flag = 1;
                                lblmessageShow.Text = "Images Size exceed 1 GB !";
                                lblmessageShow.ForeColor = System.Drawing.Color.OrangeRed;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                break;
                            }
                            bytes = ResizePhoto(image);
                            int size1 = bytes.Length;
                            int size = size1 / 1024;
                            ds.Rows.Add(new object[] { srno, filename, bytes, size + "kb", filename });
                            srno++;
                        }
                    }
                }
                if (flag != 1)
                {
                    Session["EmpTbl"] = ds;
                    ListView1.DataSource = ds;
                    ListView1.DataBind();
                    btnSave.Enabled = true;
                }
            }
            else
            {
                image = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_StudenBulkPhotoUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    

    protected void btnShowReport_Click(object sender, EventArgs e)
    {

        //if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //{
            //if (Convert.ToInt16(objCommon.LookUp(" ACD_STUDENT A,[DB_SVCE_IMAGES].[DBO].ACD_STUD_PHOTO B ,ACD_ADMBATCH AB,ACD_BRANCH BR ", "COUNT(*)", "A.IDNO = B.IDNO AND A.ADMBATCH = AB.BATCHNO AND A.BRANCHNO = BR.BRANCHNO AND A.IDNO = B.IDNO AND A.BRANCHNO = " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND A.ADMBATCH = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.PHOTO is not null")) != 0)
            //{

                ShowReport("BULK_PHOTO_UPLOAD", "EmployeePhotos.rpt");

            //}
            //else
            //{
            //    objCommon.DisplayMessage(this, "No Photos available !!", this.Page);
            //}
        //}
        //else
        //{
        //    ShowReport("BULK_PHOTO_UPLOAD", "StudentPhotos.rpt");
        //}
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,payroll," + rptFileName;

            url += "&param=@P_COLLEGENO=" + ddlCollege.SelectedValue + ",@P_STAFFNO=" + ddlStaff.SelectedValue + ",@P_PHOTOTYPE=" + ddlphototype.SelectedValue + ",@P_DEPTNO=" + ddlDepartment.SelectedValue + ",@P_TYPE=" + ddlType.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentBulkPhotoUpload.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btnDelete = sender as ImageButton;
        DataTable dt;
        dt = ((DataTable)Session["EmpTbl"]);
        dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
        Session["EmpTbl"] = dt;
        this.BindListView_DemandDraftDetails(dt);
        // message.Text = "deleted";
    }

    private void BindListView_DemandDraftDetails(DataTable dt)
    {
        try
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_StudenBulkPhotoUpdate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ACTION"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    //protected void lnkItem_Click(object sender, EventArgs e)
    //{
    //    MultiViewShowPhoto.ActiveViewIndex = 0;
    //    lnkItem.BackColor = System.Drawing.Color.SkyBlue;
    //    lnkItem.BackColor = System.Drawing.Color.Transparent;
       
    //}
    protected void lnkUploadBulkPhoto_Click(object sender, EventArgs e)
    {
        MultiViewShowPhoto.ActiveViewIndex = 0;
        lnkUploadBulkPhoto.BackColor = System.Drawing.Color.Yellow;
        lnkUploadBulkPhoto.BackColor = System.Drawing.Color.Orange;
        lnkShowphoto.Visible = true;
        lnkUploadBulkPhoto.Visible = true;
    }
    protected void lnkShowphoto_Click(object sender, EventArgs e)
    {
        MultiViewShowPhoto.ActiveViewIndex = 1;
        lnkShowphoto.BackColor = System.Drawing.Color.Yellow;
        lnkShowphoto.BackColor = System.Drawing.Color.Green;
        lnkShowphoto.Visible = true;
        lnkUploadBulkPhoto.Visible = true;
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if((ddlType.SelectedValue) == "2")
        {
            ddlphototype.Enabled = false;
        }
    }
}