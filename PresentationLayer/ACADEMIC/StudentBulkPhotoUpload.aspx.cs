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



public partial class ACADEMIC_StudentBulkPhotoUpload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstudent = new StudentController();
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
                    this.FillDegreeBranchSemester();
                }
                
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch
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
                Response.Redirect("~/notauthorized.aspx?page=StudentBulkPhotoUpload.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentBulkPhotoUpload.aspx");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            dt = ((DataTable)Session["studentTbl"]);

            dt.Columns.Remove("SRNO");
            dt.Columns.Remove("SIZE");
            dt.Columns.Remove("ACTION");

            int ddlValue = Convert.ToInt32(ddlcategory.SelectedValue);

            CustomStatus cs = (CustomStatus)objstudent.StudentBulkPhotoUpdate(ddlValue, dt);
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
        catch 
        {
            throw;
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
                    Session["studentTbl"] = ds;
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
        catch 
        {
            throw;
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "acd_degree", "degreeno", "degreename", "degreeno>0", "degreename");
        ddlBranch.ClearSelection();
        ddlType.ClearSelection();
        ddlPhotoCategory.ClearSelection();
        ddlDegree.Focus();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"], "A.LONGNAME");
        ddlType.ClearSelection();
        ddlBranch.Focus();
    }
    protected void FillDegreeBranchSemester()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "acd_admbatch", "batchno", "batchname", "batchno>0", "batchname desc");
            objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO=SC.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
            ddlPhotoCategory.ClearSelection();

        }
        catch 
        {
            throw;
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                if (Convert.ToInt16(objCommon.LookUp(" ACD_STUDENT A,[DBO].ACD_STUD_PHOTO B ,ACD_ADMBATCH AB,ACD_BRANCH BR ", "COUNT(*)", "A.IDNO = B.IDNO AND A.ADMBATCH = AB.BATCHNO AND A.BRANCHNO = BR.BRANCHNO AND A.IDNO = B.IDNO AND A.BRANCHNO = " + Convert.ToInt32(ViewState["branchno"]) + " AND A.ADMBATCH = " + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND A.DEGREENO =" + Convert.ToInt32(ViewState["degreeno"]) + " AND B.PHOTO is not null")) != 0)
                {

                    ShowReport("BULK_PHOTO_UPLOAD", "StudentPhotos.rpt");

                }
                else
                {
                    objCommon.DisplayMessage(this, "No Photos available !!", this.Page);
                }
            }
            else
            {
                ShowReport("BULK_PHOTO_UPLOAD", "StudentPhotos.rpt");
            }
        }
        catch
        {
            throw;
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + ",@P_SESSIONNO=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_TYPE=" + Convert.ToInt32(ddlType.SelectedValue) + ",@P_PHOTOTYPE=" + Convert.ToInt32(ddlPhotoCategory.SelectedValue);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch 
        {
            throw;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt;
            dt = ((DataTable)Session["studentTbl"]);
            dt.Rows.Remove(this.GetEditableDataRow(dt, btnDelete.CommandArgument));
            Session["studentTbl"] = dt;
            this.BindListView_DemandDraftDetails(dt);
        }
        catch
        {
            throw;
        }
        // message.Text = "deleted";
    }

    private void BindListView_DemandDraftDetails(DataTable dt)
    {
        try
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        catch 
        {
            throw;
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
        catch 
        {
            throw;
        }
        return dataRow;
    }
    protected void ddlClgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlAdmBatch.SelectedIndex = -1;
            ddlType.SelectedIndex = -1;
            if (ddlClgScheme.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgScheme.SelectedValue));
                //ViewState["degreeno"]

                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }
            }
        }
        catch
        {
            throw;
        }
    }
}

