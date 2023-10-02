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
using Ionic.Zip;



public partial class ACADEMIC_StudentBulkPhotoUpload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstudent = new StudentController();
    DataTable ds = new DataTable();
    String Type = string.Empty;
    string FileName = string.Empty;
    StudentController objSC = new StudentController();

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
                    //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
                    //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
            int UploadBy = 0;
            
            if (rdoRegno.Checked == true)
            {
                UploadBy = 0;
            }
            else if (RdoRollno.Checked == true)
            {
                UploadBy = 1;
            }
            CustomStatus cs = (CustomStatus)objstudent.StudentBulkPhotoUpdate(ddlValue, dt, UploadBy);
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
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), ListView1);//Set label - 
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
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "acd_admbatch", "batchno", "batchname", "batchno>0 AND ISNULL(ACTIVESTATUS,0)=1", "batchname desc");
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
            int ShowBy = 0;
            if (rdoShowRegNo.Checked == true)
            {
                ShowBy = 1;
            }
            else if (rdoShowRollNo.Checked == true)
            {
                ShowBy = 2;
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + ",@P_SESSIONNO=" + ddlAdmBatch.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_TYPE=" + Convert.ToInt32(ddlType.SelectedValue) + ",@P_PHOTOTYPE=" + Convert.ToInt32(ddlPhotoCategory.SelectedValue) + ",@P_SHOWBY=" + Convert.ToInt32(ShowBy);

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
            btnSave.Enabled = false;
           // btnSave.Visible = false;
            
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

    protected void rdoSTUDSIG_CheckedChanged(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegreeEx.SelectedIndex = 0;
        ddlbranchEx.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        //ddlStudentType.SelectedIndex = 0;
        lbltotal.Text = "0";
        ddlInstitute.SelectedIndex = -1;
    }
    protected void rdoSTUDPHOTO_CheckedChanged(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegreeEx.SelectedIndex = 0;
        ddlbranchEx.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        //ddlStudentType.SelectedIndex = 0;
        lbltotal.Text = "0";
        ddlInstitute.SelectedIndex = -1;
    }
    protected void ddlAdmissionBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlAdmissionBatch.SelectedValue) > 0)
        {
            FileNameBy();
            CheckType();
            objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND ISNULL(ActiveStatus,0)=1", "COLLEGE_ID");
            // int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", "P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND " + Type + " IS NOT NULL AND " + FileName + " <> '' "));

            //Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue)));
            //lbltotal.Text = count.ToString();
            TotalStudentCount();
        }
        else
        {
            ClearAllFields();
        }
    }
    public void FileNameBy()
    {
        FileName = "REGNO";
    }
    public void CheckType()
    {
        if (rdoSTUDPHOTO.Checked == true)
        {
            Type = "PHOTO";
        }
        else
        {
            Type = "STUD_SIGN";
        }
    }
    public void TotalStudentCount()
    {
        try
        {
            DataSet ds = new DataSet();
            int type = 0;
            if (rdoSTUDPHOTO.Checked == true)
            {
                type = 1;
            }
            else if (rdoSTUDSIG.Checked == true)
            {
                type = 2;
            }

            ds = objSC.GetStudentCount(Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegreeEx.SelectedValue), Convert.ToInt32(ddlbranchEx.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), 0, type);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbltotal.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lbltotal.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }
    private void ClearAllFields()
    {

        ddlInstitute.Items.Clear();
        ddlInstitute.Items.Add(new ListItem("Please Select", "0"));
        ddlDegreeEx.Items.Clear();
        ddlDegreeEx.Items.Add(new ListItem("Please Select", "0"));
        ddlbranchEx.Items.Clear();
        ddlbranchEx.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        lbltotal.Text = "0";
    }
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstitute.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegreeEx, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "D.DEGREENO > 0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue, "DEGREENO");
            ddlDegreeEx.SelectedIndex = -1;
            ddlbranchEx.SelectedIndex = -1;
            ddlSem.SelectedIndex = -1;
            //ddlStudentType.SelectedIndex = -1;
            // ddlAdmissionBatch_SelectedIndexChanged(sender, e);
            TotalStudentCount();
        }
        else
        {
            ddlDegreeEx.Items.Clear();
            ddlDegreeEx.Items.Add(new ListItem("Please Select", "0"));
            ddlbranchEx.Items.Clear();
            ddlbranchEx.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSem.SelectedValue) > 0)
        {
            FileNameBy();
            CheckType();
            // int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", "P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND " + Type + " IS NOT NULL AND " + FileName + " <> '' "));
            //DB_SVCE_IMAGES.dbo.
            //Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            // lbltotal.Text = count.ToString();

            TotalStudentCount();
        }
        else
        {
            //ddlStudentType.SelectedIndex = 0;
            ddlBranch_SelectedIndexChanged1(sender, e);
        }
    }
    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FileNameBy();
        CheckType();
        if (Convert.ToInt32(ddlbranchEx.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", "P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND " + Type + " IS NOT NULL AND " + FileName + " <> '' ")); 
            // Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL  AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)));
            // lbltotal.Text = count.ToString();
            TotalStudentCount();
        }
        // else
        // {
        ddlSem.SelectedIndex = -1;
        //ddlStudentType.SelectedIndex = -1;
        // ddlDegree_SelectedIndexChanged(sender, e);
        //}
    }


    protected void btnExtract_Click(object sender, EventArgs e)
    {
        try
        {
            //string str = @"Loading...'";
            //Response.Write(str);
            //Response.Flush();
            ////A long process is going on here
            // Add Fake Delay to simulate long running process.
            //System.Threading.Thread.Sleep(3000);

            FileNameBy();
            CheckType();
            if (FileName != "" && Type != "")
            {


                //string FolderName = ddlDegree.SelectedItem.Text + '_' + ddlBranch.SelectedItem.Text + '_' + Type;
                string FolderName = ddlAdmissionBatch.SelectedItem.Text + '_' + ddlInstitute.SelectedItem.Text + '_' + Type;
                if (!Directory.Exists("C://BULK_PHOTO_EXTRACT//" + FolderName))
                {
                    Directory.CreateDirectory("C://BULK_PHOTO_EXTRACT//" + FolderName);
                }

                string DirPath = "C://BULK_PHOTO_EXTRACT//" + FolderName + "//";
                if (Directory.Exists(DirPath))
                {
                    int type = 0;
                    int ExtractType = 0;
                    if (rdoRegno.Checked == true)
                    {
                        ExtractType = 1;
                    }
                    else if (RdoRollno.Checked == true)
                    {
                        ExtractType = 2;
                    }
                    if (rdoSTUDPHOTO.Checked == true)
                    {
                        type = 1;
                    }
                    else if (rdoSTUDSIG.Checked == true)
                    {
                        type = 2;
                    }
                    //  DataSet ds = objCommon.FillDropDown("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "S.IDNO,S.REGNO", "P.PHOTO,P.STUD_SIGN", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " OR " + Convert.ToInt32(ddlSem.SelectedValue) + "=0) AND (S.IDTYPE=" + Convert.ToInt32(ddlStudentType.SelectedValue) + " OR " + Convert.ToInt32(ddlStudentType.SelectedValue) + "=0) AND " + Type + " IS NOT NULL AND " + FileName + " <> '' ", " S.ADMBATCH,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,RIGHT(S.REGNO,2)");
                    //  DataSet ds = objCommon.FillDropDown("DB_SVCE_IMAGES.dbo.ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "S.IDNO,S.ENROLLNO as REGNO", "P.PHOTO,P.STUD_SIGN", "ADMBATCH=(14)  AND SEMESTERNO=1 AND PHOTO Is NOT NULL and ENROLLNO is not null", " S.ADMBATCH,S.SEMESTERNO,RIGHT(S.REGNO,2)");

                    // DataSet ds = objCommon.FillDropDown("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "S.IDNO,S.ENROLLNO as REGNO", "P.PHOTO,P.STUD_SIGN", " P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " OR " + Convert.ToInt32(ddlSem.SelectedValue) + "=0) AND (S.IDTYPE=" + Convert.ToInt32(ddlStudentType.SelectedValue) + " OR " + Convert.ToInt32(ddlStudentType.SelectedValue) + "=0) AND " + Type + " IS NOT NULL AND " + FileName + " <> '' ", " S.ADMBATCH,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,RIGHT(S.REGNO,2)");
                    DataSet ds = objSC.GetStudentPhotoAndSign(Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegreeEx.SelectedValue), Convert.ToInt32(ddlbranchEx.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), ExtractType, type);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
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
                                        if (imgData.Length > 0 && imgData != null)
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
                                        throw;
                                    }
                                }
                            }


                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>hideProgress();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {hideProgress();});</script>", false);
                            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "YourUniqueScriptKey", "HideProgress()", true);
                            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, GetType(), "YourUniqueScriptKey", "$('.loading').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('.loading').hide(); });", true);
                            //UpdatePanel1.Update();

                            Response.Clear();
                            Response.BufferOutput = false;
                            // string ZipFileName = ddlDegree.SelectedItem.Text.Trim() + "_" + ddlAdmissionBatch.SelectedItem.Text.Trim() + "_" + (ddlBranch.SelectedValue == "0" ? "" : ddlBranch.SelectedItem.Text.Trim()) + "_" + (ddlSem.SelectedValue == "0" ? "" : ddlSem.SelectedItem.Text.Trim()) + "_" + (ddlStudentType.SelectedValue == "0" ? "" : ddlStudentType.SelectedItem.Text.Trim()) + "_" + Type;
                            string ZipFileName = ddlAdmissionBatch.SelectedItem.Text.Trim() + "_" + ddlInstitute.SelectedItem.Text.Trim() + "_" + Type;
                            string zipName = String.Format("Zip_{0}.zip", ZipFileName.Replace(' ', '_'));
                            Response.ContentType = "application/zip";
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                            zip.Save(Response.OutputStream);

                            //string script1 = "$(document).ready(function () { hideProgress(); });";
                            //ClientScript.RegisterStartupScript(this.GetType(), "load", script1, true);

                            // Response.Redirect(Request.Url.ToString());

                            //  Response.End();
                            this.DeleteDirectory(DirPath);
                            HttpContext.Current.Response.End();
                        }
                    }
                    objCommon.DisplayMessage(this.UpdatePanel1, "Record not found for this selection.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Extract folder not specified.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Something Went Wrong", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void DeleteDirectory(string path)
    {
        // Delete all files from the Directory  
        foreach (string filename in Directory.GetFiles(path))
        {
            File.Delete(filename);
        }
        // Check all child Directories and delete files  
        foreach (string subfolder in Directory.GetDirectories(path))
        {
            DeleteDirectory(subfolder);
        }
        string directory = "C://BULK_PHOTO_EXTRACT";
        Directory.Delete(path);
        Directory.Delete(directory, true);
    }

    protected void ddlDegreeEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        FileNameBy();
        CheckType();
        if (Convert.ToInt32(ddlDegreeEx.SelectedValue) > 0)
        {
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue + "", "BRANCHNO");
            objCommon.FillDropDownList(ddlbranchEx, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegreeEx.SelectedValue, "A.LONGNAME");
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", " P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND " + Type + " IS NOT NULL AND " + FileName + " <> '' "));
            // Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue)));
            TotalStudentCount();
        }
        //else
        //{
        ddlbranchEx.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;



    }
}

