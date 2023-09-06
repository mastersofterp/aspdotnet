using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;

public partial class ACADEMIC_UploadSyllabus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objStud = new StudentController();
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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDown();
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
                Response.Redirect("~/notauthorized.aspx?page=UploadSyllabus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UploadSyllabus.aspx");
        }
    }

    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DISTINCT DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue=="0")
        {
            ddlBranch.SelectedIndex = -1;
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
        }
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue == "0")
        {
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
        }
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)+"AND DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue), "SCHEMENO");
        BindListView();
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlSemester.SelectedIndex = -1;
        }
     //   objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON S.SEMESTERNO=SM.SEMESTERNO", "DISTINCT SM.SEMESTERNO", "SEMESTERNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)+" AND SCHEMENO="+Convert.ToInt32(ddlScheme.SelectedValue), "SEMESTERNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER SM ", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND SEMESTERNO<=8", "SEMESTERNO");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Byte[] SyllabusFile = null;
        string path = MapPath("~/UploadSyllabus/");
        if (fuSyllabusUpload.HasFile)
        {
            
            if (fuSyllabusUpload.FileName.ToString().Contains(".doc") || fuSyllabusUpload.FileName.ToString().Contains(".pdf") || fuSyllabusUpload.FileName.ToString().Contains(".xls"))
            {
                if (!(Directory.Exists(MapPath("~PresentationLayer/UploadSyllabus"))))
                    Directory.CreateDirectory(path);
                SyllabusFile = GetSyllabusDataForDocumentation(fuSyllabusUpload);
                double checkFileLength = SyllabusFile.Length;
                if ((checkFileLength / 1024) > 10000.0 || (checkFileLength / 1024) < 0.01)
                {
                    objCommon.DisplayMessage(this.Page, "File  Size Required Between 0 kb - 10 MB!!", this.Page);
                    fuSyllabusUpload.Focus();
                    return;
                }
        
                  string[] array1= Directory.GetFiles(path);
                  foreach (string str in array1)
                  {
                      if ((path + fuSyllabusUpload.FileName.ToString()).Equals(str))
                      {
                          objCommon.DisplayMessage(updStudent, "File with similar name already exists!",this.Page);
                          return;
                      }
                  }
                  ViewState["FileName"] = fuSyllabusUpload.FileName.ToString();
                  fuSyllabusUpload.SaveAs(MapPath("~/UploadSyllabus/" + fuSyllabusUpload.FileName));
                int degreeno=Convert.ToInt32(ddlDegree.SelectedValue);
                int branchno=Convert.ToInt32(ddlBranch.SelectedValue);
                int schemeno=Convert.ToInt32(ddlScheme.SelectedValue);
                int semesterno=Convert.ToInt32(ddlSemester.SelectedValue);
                string filename= ViewState["FileName"].ToString();
                 
                string filepath = path + fuSyllabusUpload.FileName.ToString();
                int ret =objStud.UploadSyllabus(degreeno, branchno, schemeno,  filename, filepath,semesterno);
                if(ret==2)
                {
                    objCommon.DisplayMessage(updStudent,"Syllabus uploaded succesfully",this.Page);
                }
            }
            else
              objCommon.DisplayMessage(updStudent, "Please upload valid syllabus file like .pdf,.doc,.xls files", this);
                   
        }

            ddlDegree.SelectedIndex = -1;
           ddlBranch.SelectedIndex = -1;
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
    }

    public byte[] GetSyllabusDataForDocumentation(FileUpload fu)
    {
        if (fu.HasFile)
        {
            int FileSize = fu.PostedFile.ContentLength;
            Stream FileStream = fu.PostedFile.InputStream as Stream;
            byte[] FileContent = new byte[FileSize];
            int intStatus = FileStream.Read(FileContent, 0, FileSize);
            //ImageStream.Close();
            // ImageStream.Dispose();
            return FileContent;
        }
        else
        {
            FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/UploadSyllabus/nophoto.pdf"), FileMode.Open);
            int FileSize = (int)ff.Length;
            byte[] FileContent = new byte[ff.Length];
            ff.Read(FileContent, 0, FileSize);
            ff.Close();
            ff.Dispose();
            return FileContent;
        }
    }
    private void BindListView()
    {
        DataSet ds = objStud.GetSyllabus(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlist.DataSource = ds;
                lvlist.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvlist);//Set label -
                lvlist.Visible = true;
            }
            else
                lvlist.Visible = false;
        }
        else
            lvlist.Visible = false;
    }
    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel =sender as ImageButton;
        int usno=int.Parse(btnDel.CommandArgument);
        int output = objStud.deleteSyllabus(usno);
        if (output != -99 && output != 99)
        {
            objCommon.DisplayMessage(this.updProg , " Syllabus Deleted Successfully!!", this.Page);
           
        }
        else
        {
            objCommon.DisplayMessage(this.updProg, " Syllabus Is Not Deleted ", this.Page);
        }
        BindListView();

        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkDownload = sender as LinkButton;
        string FileName = lnkDownload.ToolTip.ToString();
        string file_path = Server.MapPath("~/UploadSyllabus/");
        string filepath = file_path + FileName; ;// +System.IO.Path.GetExtension(FileName); ;
        Response.Redirect("DownloadAttachment.aspx?file=" + filepath + "&filename=" + FileName);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Academic/UploadSyllabus.aspx?");
    }
}