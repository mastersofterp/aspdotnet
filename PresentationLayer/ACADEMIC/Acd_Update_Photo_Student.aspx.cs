//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADAMIC
// PAGE NAME     : BULK PHOTO UPLOADED                                                    
// CREATION DATE :                                                      
// ADDED BY      : ASHISH DHAKATE 
// ADDED DATE    : 16-DEC-2011                                                      
// MODIFIED DATE : 12-06-2023
// MODIFIED By   :  Jay Takalkhede
//=======================================================================================
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.IO;



public partial class ACADEMIC_Acd_Update_Photo_Student : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    IITMS.UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    StudentController objstudent = new StudentController();

    #region Page Load
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
                if (Request.QueryString["pageno"] != null)
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    this.FillSchemeSemester();
                butSubmit.Visible = false;
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label 
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header
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
                Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Acd_Update_Photo_Student.aspx");
        }
    }

    #endregion


    #region dropdown

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlUpdateSign.Visible = false;
        lvUpdateSign.DataSource = null;
        lvUpdateSign.DataBind();
        pnlUpdatePhoto.Visible = false;
        lvUpdatePhoto.DataSource = null;
        lvUpdatePhoto.DataBind();
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.LONGNAME");
            lvUpdatePhoto.DataSource = null;
            lvUpdatePhoto.DataBind();
            //  Commented By Vipul T on date 13-02-2024
            //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvUpdatePhoto);//Set label -
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmbatch.SelectedIndex = 0;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlUpdateSign.Visible = false;
        lvUpdateSign.DataSource = null;
        lvUpdateSign.DataBind();
        pnlUpdatePhoto.Visible = false;
        lvUpdatePhoto.DataSource = null;
        lvUpdatePhoto.DataBind();
        if (ddlBranch.SelectedIndex > 0)
        {
            lvUpdatePhoto.DataSource = null;
            lvUpdatePhoto.DataBind();
            //  Commented By Vipul T on date 13-02-2024
            // objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvUpdatePhoto);//Set label -
            ddlAdmbatch.Focus();
        }
        else
        {
            ddlAdmbatch.SelectedIndex = 0;
        }
    }

    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlUpdateSign.Visible = false;
        lvUpdateSign.DataSource = null;
        lvUpdateSign.DataBind();
        pnlUpdatePhoto.Visible = false;
        lvUpdatePhoto.DataSource = null;
        lvUpdatePhoto.DataBind();
    }

    protected void rboStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rboStudent.SelectedValue == "2")
        {
            pnlUpdateSign.Visible = false;
            lvUpdateSign.DataSource = null;
            lvUpdateSign.DataBind();
            pnlUpdatePhoto.Visible = false;
            lvUpdatePhoto.DataSource = null;
            lvUpdatePhoto.DataBind();
        }
        else if (rboStudent.SelectedValue == "1")
        {
            pnlUpdateSign.Visible = false;
            lvUpdateSign.DataSource = null;
            lvUpdateSign.DataBind();
            pnlUpdatePhoto.Visible = false;
            lvUpdatePhoto.DataSource = null;
            lvUpdatePhoto.DataBind();
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            ViewState["college_id"] = Convert.ToInt32(ddlCollege.SelectedValue).ToString();

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN  ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO = A.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + "AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "A.degreeno");
            ddlDegree.Focus();
            lvUpdatePhoto.DataSource = null;
            lvUpdatePhoto.DataBind();
            //  Commented By Vipul T on date 13-02-2024
            //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvUpdatePhoto);//Set label -
            ddlDegree.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmbatch.SelectedIndex = 0;
        }
        pnlUpdateSign.Visible = false;
        lvUpdateSign.DataSource = null;
        lvUpdateSign.DataBind();
        pnlUpdatePhoto.Visible = false;
        lvUpdatePhoto.DataSource = null;
        lvUpdatePhoto.DataBind();
    }

    protected void FillSchemeSemester()
    {
        try
        {
            //objCommon.FillDropDownList(ddlBranch, "acd_branch", "distinct branchno", "longname", "branchno>0", "longname");

            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            objCommon.FillDropDownList(ddlAdmbatch, "acd_admbatch", "batchno", "batchname", "batchno>0", "batchname desc");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion


    //protected void lvGlobalTimeTable_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    ListViewDataItem dataitem = (ListViewDataItem)e.Item;
    //    ImageButton FacultyNo = dataitem.FindControl("btnEditTimeTable") as ImageButton;
    //    HiddenField alternateflag = dataitem.FindControl("hdfalternateflag") as HiddenField;
    //    //Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
    //    HiddenField hdfStartEndDate = dataitem.FindControl("hdfStartEndDate") as HiddenField;

    //    int facultyno = Convert.ToInt32(FacultyNo.CommandArgument);
    //    int courseno = Convert.ToInt32(FacultyNo.AlternateText);
    //    int alternate = Convert.ToInt32(alternateflag.Value);
    //    string startendate = hdfStartEndDate.Value;
    //    string[] ttDates = startendate.Split('-');
    //    string startDate; string endDate;
    //    startDate = ttDates[0].Trim();
    //    endDate = ttDates[1].Trim();
    //    ListView lv = dataitem.FindControl("lvDetails") as ListView;
    //    try
    //    {
    //        //Added By Rahul M. for lightweight performance.
    //        if (ViewState["dsTimeslot"].ToString() != null)
    //        {
    //            DataSet dsFromViewState = ViewState["dsTimeslot"] as DataSet;
    //            //DataSet ds = objCC.GetGlobalCoursesTimeTableDetailsSectionModified(facultyno, alternate, Convert.ToInt32(ddlSessionTimeTable.SelectedValue), startDate, endDate, courseno);
    //            DataTable xdata = (from r in dsFromViewState.Tables[0].AsEnumerable()
    //                               where Convert.ToInt32(r["UA_NO"]) == facultyno &&
    //                                   Convert.ToDateTime(r["START_DATE"]) == Convert.ToDateTime(startDate) &&
    //                                   Convert.ToDateTime(r["END_DATE"]) == Convert.ToDateTime(endDate) &&
    //                                   Convert.ToInt32(r["COURSENO"]) == courseno &&
    //                                   Convert.ToInt32(r["ALTERNATE_FLAG"]) == alternate
    //                               select r).CopyToDataTable();
    //            lv.DataSource = xdata;
    //            lv.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_Global_Offered_Course.GetGlobalCoursesTimeTableDetailsSection -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}





    #region bind data

    private void BindListViewList(int branchNo, int admbatch, int degree, int college)
    {
        try
        {
            ViewState["DYNAMIC_DATASET"] = null;
            ViewState["DYNAMIC_DATASET_SIGN"] = null;
            ViewState["dsoptimize"] = null;
            DataSet ds = objstudent.GetStudentsForUpdateBulkPhotoUpload(branchNo, admbatch, degree, college, Convert.ToInt32(Session["OrgId"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsoptimize"] = ds;
                if (rboStudent.SelectedValue == "1")
                {
                    butSubmit.Visible = true;
                    pnlUpdatePhoto.Visible = true;
                    lvUpdatePhoto.DataSource = ds;
                    lvUpdatePhoto.DataBind();
                    ViewState["DYNAMIC_DATASET"] = ds.Tables[0];
                    pnlUpdateSign.Visible = false;
                    lvUpdateSign.DataSource = null;
                    lvUpdateSign.DataBind();
                    butReport.Visible = true;
                    btnSignReport.Visible = false;
                    // Commented By Vipul T on on date 13-02-2024
                    // objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvUpdatePhoto);//Set label -

                    //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    //{
                    //    HiddenField hididno = lvUpdatePhoto.Items[i].FindControl("hididno") as HiddenField;

                    //    Image ImgPhoto = lvUpdatePhoto.Items[i].FindControl("ImgPhoto") as Image;
                    //    ImgPhoto.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[i]["IDNO"].ToString() + "&type=STUDENT";

                    //  }
                }
                else if (rboStudent.SelectedValue == "2")
                {
                    butSubmit.Visible = true;
                    pnlUpdateSign.Visible = true;
                    lvUpdateSign.DataSource = ds;
                    lvUpdateSign.DataBind();
                    ViewState["DYNAMIC_DATASET_SIGN"] = ds.Tables[0];
                    pnlUpdatePhoto.Visible = false;
                    lvUpdatePhoto.DataSource = null;
                    lvUpdatePhoto.DataBind();
                    butReport.Visible = false;
                    btnSignReport.Visible = true;
                    // Commented By Vipul T on on date 13-02-2024
                    //  objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvUpdatePhoto);//Set label -
                    //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    //{
                    //    HiddenField hididno = lvUpdateSign.Items[i].FindControl("hididno1") as HiddenField;

                    //    Image ImgSign = lvUpdateSign.Items[i].FindControl("ImgSign") as Image;
                    //    ImgSign.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[i]["IDNO"].ToString() + " &type=STUDENTSIGN";
                    //}
                }
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found", this.Page);
                butSubmit.Visible = false;
            }
        }

     //   }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void butShow_Click(object sender, EventArgs e)
    {
        this.BindListViewList(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        //divNote.Visible = true; // Added By Vipul T no date 13-02-2024
    }

    #endregion

    #region Submit

    protected void butSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string idno = string.Empty;
            int count1 = 0;
            int count = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;
            string namephoto = string.Empty;
            string namesign = string.Empty;
            Student objstud = new Student();
            StudentController objEc = new StudentController();
            if (rboStudent.SelectedValue == "1")
            {

                foreach (ListViewDataItem lvitem in lvUpdatePhoto.Items)
                {
                    count2++;
                    //HiddenField hididno = lvitem.FindControl("hididno") as HiddenField;
                    Label lblidno = lvitem.FindControl("lblRegno") as Label;
                    idno = lblidno.ToolTip;
                    FileUpload fuStudPhoto = lvitem.FindControl("fuStudPhoto") as FileUpload;
                    //FileUpload fuStudSign = lvitem.FindControl("fuStudSign") as FileUpload;
                    byte[] image = new byte[count2];
                    byte[] sign = new byte[count2];
                    byte[] image2 = new byte[count2];
                    int status = Convert.ToInt32(rboStudent.SelectedValue);

                    if (fuStudPhoto.HasFile)
                    {
                        string ext = System.IO.Path.GetExtension(fuStudPhoto.PostedFile.FileName);
                        //if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".heif" || ext == ".gif" || ext == ".JPG" || ext == ".PNG" || ext == ".JPEG" || ext == ".HEIF" || ext == ".GIF")
                        if (ext == ".jpg" || ext == ".jpeg")
                        {
                            count3++;
                            if (fuStudPhoto.PostedFile.ContentLength < 150000)
                            {

                                byte[] resizephoto = ResizePhoto(fuStudPhoto);
                                if (resizephoto.LongLength >= 150000)
                                {
                                    objCommon.DisplayMessage(this.Page, "File size must be less or equal to 150kb", this.Page);
                                    return;
                                }
                                else
                                {
                                    objstud.StudPhoto = this.ResizePhoto(fuStudPhoto);
                                    objstud.IdNo = Convert.ToInt32(idno);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "File size must be less or equal to 150kb", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Please Upload file with .jpg .jpeg format only.", this.Page);
                            return;
                        }

                    }
                    else
                    {
                        image = null;
                        sign = null;
                    }
                    CustomStatus cs = (CustomStatus)objEc.UpdateStudPhoto(objstud);

                }
                if (count3 == 0 & count4 == 0)
                {
                    objCommon.DisplayMessage("Please Browse File!", this.Page);
                }
                else
                {
                    if (count == 0 & count1 == 0)
                    {
                        objCommon.DisplayMessage("Record Updated Successfully", this.Page);
                    }
                    else if (count > 0 || count1 > 0)
                    {
                        objCommon.DisplayMessage("Record Updated Successfully but Regno for photo" + namephoto + "record not updated beacause of Uploaded File size more than 150 kb.", this.Page);
                    }
                }
                this.BindListViewList(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
            }
            else if (rboStudent.SelectedValue == "2")
            {
                foreach (ListViewDataItem lvitem in lvUpdateSign.Items)
                {
                    count2++;
                    //HiddenField hididno = lvitem.FindControl("hididno") as HiddenField;
                    Label lblidno = lvitem.FindControl("lblRegno") as Label;
                    idno = lblidno.ToolTip;
                    FileUpload fuStudSign = lvitem.FindControl("fuStudSign") as FileUpload;
                    byte[] image = new byte[count2];
                    byte[] sign = new byte[count2];
                    byte[] image2 = new byte[count2];
                    int status = Convert.ToInt32(rboStudent.SelectedValue);
                    if (fuStudSign.HasFile)
                    {

                        string ext = System.IO.Path.GetExtension(fuStudSign.PostedFile.FileName);
                        if (ext == ".jpg" || ext == ".jpeg" )
                        {
                            count4++;
                            if (fuStudSign.PostedFile.ContentLength < 150000)
                            {

                                byte[] resizephoto = ResizePhoto(fuStudSign);

                                if (resizephoto.LongLength >= 150000)
                                {
                                    objCommon.DisplayMessage(this.Page, "File size must be less or equal to 150kb", this.Page);
                                    return;
                                }
                                else
                                {
                                    objstud.StudSign = this.ResizePhoto(fuStudSign);
                                    objstud.IdNo = Convert.ToInt32(idno);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "File size must be less or equal to 150kb", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Please Upload file with .jpg .jpeg format only.", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        sign = null;
                        image = null;
                    }
                    CustomStatus cs = (CustomStatus)objEc.UpdateStudSign(objstud);
                    //CustomStatus cs = (CustomStatus)objstudent.UpdateStudentPhoto(Convert.ToInt32(idno), image, sign,status);

                }
                if (count3 == 0 & count4 == 0)
                {
                    objCommon.DisplayMessage("Please Browse File!", this.Page);
                }
                else
                {
                    if (count == 0 & count1 == 0)
                    {
                        objCommon.DisplayMessage("Record Updated Successfully", this.Page);
                    }
                    else if (count > 0 || count1 > 0)
                    {
                        objCommon.DisplayMessage("Record Updated Successfully but Regno for Sign" + namesign + "record not updated beacause of Uploaded File size more than 150 kb.", this.Page);
                    }
                }
                this.BindListViewList(Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Report
    protected void btnSignReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport_SIGN("BULK_SIGNATURE_UPLOAD", "StudentSignature.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void butReport_Click1(object sender, EventArgs e)
    {
        try
        {
            // Change done  by jay - 29/09/2022......
            ShowReport("BULK_PHOTO_UPLOAD", "StudentSignature.rpt");
        }
        catch (Exception ex)
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
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_BRANCHNO=" + ddlBranch.SelectedValue
                + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue
                + ",@P_DEGREENO=" + ddlDegree.SelectedValue
                + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["college_id"])
                + ",username=" + Session["userfullname"].ToString()
                + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(Session["OrgId"])
                + ",@P_PHOTOTYPE=1";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReport_SIGN(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_BRANCHNO=" + ddlBranch.SelectedValue
                + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue
                + ",@P_DEGREENO=" + ddlDegree.SelectedValue
                + ",@P_COLLEGEID=" + Convert.ToInt32(ViewState["college_id"])
                + ",username=" + Session["userfullname"].ToString()
                + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(Session["OrgId"])
                + ",@P_PHOTOTYPE=2";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
    protected void lvUpdatePhoto_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        HiddenField hididno = dataitem.FindControl("hididno") as HiddenField;
        Image ImgPhoto = dataitem.FindControl("ImgPhoto") as Image;

        if (hididno != null && ImgPhoto != null)
        {
            DataSet dsFromViewState = ViewState["dsoptimize"] as DataSet;
            if (dsFromViewState != null)
            {
                DataTable xdata = (from r in dsFromViewState.Tables[0].AsEnumerable()
                                   where Convert.ToInt32(r["idno"]) == Convert.ToInt32(hididno.Value)
                                   select r).CopyToDataTable();

                ImgPhoto.ImageUrl = "~/showimage.aspx?id=" + xdata.Rows[0]["IDNO"].ToString() + "&type=STUDENT";
            }
        }
    }

    // Added By Vipul T on date 13-02-2024
    protected void lvUpdateSign_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        HiddenField hididno = dataitem.FindControl("hididno1") as HiddenField;
        Image ImgSign = dataitem.FindControl("ImgSign") as Image;

        if (hididno != null && ImgSign != null)
        {
            DataSet dsFromViewState = ViewState["dsoptimize"] as DataSet;
            if (dsFromViewState != null)
            {
                DataTable xdata = (from r in dsFromViewState.Tables[0].AsEnumerable()
                                   where Convert.ToInt32(r["idno"]) == Convert.ToInt32(hididno.Value)
                                   select r).CopyToDataTable();

                ImgSign.ImageUrl = "~/showimage.aspx?id=" + xdata.Rows[0]["IDNO"].ToString() + "&type=STUDENTSIGN";
            }
        }
    }
    protected void lvUpdatePhoto_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lvUpdatePhoto.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET"];
            lvUpdatePhoto.DataSource = dt;
            lvUpdatePhoto.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void FilterData2_TextChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox searchTextBox = (System.Web.UI.WebControls.TextBox)lvUpdatePhoto.FindControl("FilterData2");
        string searchText = searchTextBox.Text.Trim();

        try
        {
            System.Data.DataTable dt = ViewState["DYNAMIC_DATASET"] as System.Data.DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                if (searchText != string.Empty)
                {
                    string searchedData = "REGNO LIKE '%" + searchText + "%' OR STUDNAME LIKE '%" + searchText + "%'";
                    dv.RowFilter = searchedData;
                    if (dv != null && dv.ToTable().Rows.Count > 0)
                    {
                        lvUpdatePhoto.DataSource = dv;
                        lvUpdatePhoto.DataBind();
                    }

                }
                else
                {
                    lvUpdatePhoto.DataSource = dt;
                    lvUpdatePhoto.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
        }

    }

    protected void FilterData2_TextChanged1(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox searchTextBox = (System.Web.UI.WebControls.TextBox)lvUpdateSign.FindControl("FilterData1");
        string searchText = searchTextBox.Text.Trim();

        try
        {
            System.Data.DataTable dt = ViewState["DYNAMIC_DATASET_SIGN"] as System.Data.DataTable;
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                if (searchText != string.Empty)
                {
                    string searchedData = "REGNO LIKE '%" + searchText + "%' OR STUDNAME LIKE '%" + searchText + "%'";
                    dv.RowFilter = searchedData;
                    if (dv != null && dv.ToTable().Rows.Count > 0)
                    {
                        lvUpdateSign.DataSource = dv;
                        lvUpdateSign.DataBind();
                    }

                }
                else
                {
                    lvUpdateSign.DataSource = dt;
                    lvUpdateSign.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void lvUpdateSign_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        try
        {
            (lvUpdateSign.FindControl("DataPager2") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DYNAMIC_DATASET_SIGN"];
            lvUpdateSign.DataSource = dt;
            lvUpdateSign.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
}
