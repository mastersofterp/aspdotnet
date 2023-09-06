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
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using Ionic.Zip;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ICSharpCode.SharpZipLib.Zip;

public partial class ACADEMIC_BulkPhotoExtract : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    String Type = string.Empty;
    string FileName = string.Empty;

    StudentController objSC = new StudentController();


    //PATH TO EXTRACT IMAGES
    //private string DirPath = System.Configuration.ConfigurationManager.AppSettings["DirPhoto"].ToString();
    //private string DirPath = "D://2016-2017_BATCH_PHOTO//";
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
                lbltotal.Text = "0";
                PopulateDropDownList();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                //string script = "$(document).ready(function () { $('[id*=btnExtract]').click(); });";
                //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

            }
        }
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





    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNAME DESC");
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");


    //   ddlAdmissionBatch.SelectedIndex = 1;

      //  ddlInstitute.Focus();
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

            ds = objSC.GetStudentCount(Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), type);
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



    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        FileNameBy();
        CheckType();
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0)
        {
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO=" + ddlDegree.SelectedValue + "", "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", " P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND " + Type + " IS NOT NULL AND " + FileName + " <> '' "));
            // Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue)));
            TotalStudentCount();
        }
        //else
        //{
        ddlBranch.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;
        ddlStudentType.SelectedIndex = -1;
        //ddlAdmissionBatch_SelectedIndexChanged(sender, e);
        //}



    }
    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FileNameBy();
        CheckType();
        if (Convert.ToInt32(ddlBranch.SelectedValue) > 0)
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
        ddlStudentType.SelectedIndex = -1;
       // ddlDegree_SelectedIndexChanged(sender, e);
        //}
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
            ddlStudentType.SelectedIndex = 0;
            ddlBranch_SelectedIndexChanged1(sender, e);
        }
    }
    protected void ddlStudentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStudentType.SelectedValue) > 0)
        {
            FileNameBy();
            CheckType();
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P LEFT JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO) INNER JOIN ACD_STUDENT_ADMISSION_APPROVAL A ON (S.IDNO = A.IDNO)", "COUNT(1)", "P.PHOTO IS NOT NULL AND ENROLLNO IS NOT NULL AND ISNULL(S.ADMCAN,0)=0 AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND S.IDTYPE=" + Convert.ToInt32(ddlStudentType.SelectedValue) + " AND " + Type + " IS NOT NULL AND " + FileName + " <> '' "));
            //DB_SVCE_IMAGES.dbo.
            // Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO P INNER JOIN ACD_STUDENT S ON(P.IDNO=S.IDNO)", "COUNT(1)", "ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0 AND " + Type + " IS NOT NULL AND " + FileName + " <> '' AND S.ADMBATCH=" + Convert.ToInt32(ddlAdmissionBatch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND S.IDTYPE=" + Convert.ToInt32(ddlStudentType.SelectedValue)));
            // lbltotal.Text = count.ToString();

            TotalStudentCount();
        }
        else
        {
            ddlStudentType.SelectedIndex = 0;
            ddlSem_SelectedIndexChanged(sender, e);
        }
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
                    DataSet ds = objSC.GetStudentPhotoAndSign(Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue),type);

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
    public void FileNameBy()
    {
        FileName = "REGNO";
    }
    protected void rdoSTUDSIG_CheckedChanged(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        lbltotal.Text = "0";
        ddlInstitute.SelectedIndex = -1;
    }
    protected void rdoSTUDPHOTO_CheckedChanged(object sender, EventArgs e)
    {
        ddlAdmissionBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        lbltotal.Text = "0";
        ddlInstitute.SelectedIndex = -1;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        //ClearAllFields();
        Response.Redirect(Request.Url.ToString());
    }

    private void ClearAllFields()
    {
        
        ddlInstitute.Items.Clear();
        ddlInstitute.Items.Add(new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        lbltotal.Text = "0";
    }


    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstitute.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON (D.DEGREENO=CDB.DEGREENO)", "DISTINCT D.DEGREENO", "DEGREENAME", "D.DEGREENO > 0 AND COLLEGE_ID=" + ddlInstitute.SelectedValue, "DEGREENO");
            ddlDegree.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlSem.SelectedIndex = -1;
            ddlStudentType.SelectedIndex = -1;
           // ddlAdmissionBatch_SelectedIndexChanged(sender, e);
            TotalStudentCount();
        }
        else
        {
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    //Added By Dileep Kare on 02.09.2021 Due not required to save directories.
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
}