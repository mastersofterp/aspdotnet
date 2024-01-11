using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;


public partial class ACADEMIC_ClubActivityRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubController OBJCLUB = new ClubController();
    string PageId;
    string blob_ConStr = string.Empty;
    string blob_ContainerName = string.Empty;
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
           
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                populateDropDown();

                
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        if (CheckActivity())
                        {
                            pnlMain.Visible = true;
                            ShowStudentDetails();
                        }
                        else
                        {
                            pnlMain.Visible = false;
                        }
                       BindListView();
                       bindDropdown();

                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                        pnlMain.Visible = false;
                        //div3.Visible = false;
                        //lvPraticipation.Visible = false;
                        return;

                        Page.Title = Session["coll_name"].ToString();

                        PageId = Request.QueryString["pageno"];

                    }
                       int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
                     //  objCommon.FillDropDownList(ddlsubuser, "ACD_CLUB_REGISTRATION  R INNER JOIN ACD_CLUB_MASTER cm ON (R.CLUB_ACTIVITY_NO=cm.CLUB_ACTIVITY_NO) ", "CM.CLUB_ACTIVITY_NO", "CM.CLUB_ACTIVITY_TYPE", "IDNO=" + idno, "R.CLUB_REGISTRATION_NO");

                      // objCommon.FillDropDownList(ddlsubuser, "ACD_CLUB_REGISTRATION", "CLUB_REGISTRATION_NO", "", "CLUB_REGISTRATION_NO > 0", "CLUB_REGISTRATION_NO");

                       objCommon.FillDropDownList(ddlsubuser, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
                    ViewState["action"] = "add";
                }
            }


        
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        
    }
    //}


    private void bindDropdown()
    {
        DataSet ds = null;
        ds = OBJCLUB.PointMapping(0);

           #region ddlWeightage

        ddlWeightage.DataSource = null;
        ddlWeightage.DataBind();

        if (ds.Tables[6].Rows.Count > 0)
        {
           
            ddlWeightage.DataSource = ds.Tables[6];
            ddlWeightage.DataTextField = "WEIGHTAGE_NAME";
            ddlWeightage.DataValueField = "WEIGHTAGE_NO";
            ddlWeightage.DataBind();
        }
        else
        {
            ddlWeightage.DataSource = null;
            ddlWeightage.DataBind();
        }
        ddlWeightage.Items.Insert(0, new ListItem("Please Select ", ""));
        
        #endregion ddlWeightage

           #region ddlCampus

        ddlCampus.DataSource = null;
        ddlCampus.DataBind();
        if (ds.Tables[7].Rows.Count > 0)
        {
          
            ddlCampus.DataSource = ds.Tables[7];
            ddlCampus.DataTextField = "CAMPUS_NAME";
            ddlCampus.DataValueField = "CAMPUS_NO";
            ddlCampus.DataBind();
        }
        else
        {
            ddlCampus.DataSource = null;
            ddlCampus.DataBind();
        }
        ddlCampus.Items.Insert(0, new ListItem("Please Select ", ""));

        #endregion ddlCampus

           #region ddlCount

        ddlHours.DataSource = null;
        ddlHours.DataBind();
        if (ds.Tables[8].Rows.Count > 0)
        {

            ddlHours.DataSource = ds.Tables[8];
            ddlHours.DataTextField = "COUNT_NAME";
            ddlHours.DataValueField = "HC_NO";
            ddlHours.DataBind();
        }
        else
        {
            ddlHours.DataSource = null;
            ddlHours.DataBind();
        }
        ddlHours.Items.Insert(0, new ListItem("Please Select ", ""));
        #endregion ddlCount

    }

    protected void BindListView()
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            DataSet ds = OBJCLUB.GetClubActivityRegistrationDetails(idno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlclubReg.Visible = true;
                lvclubReg.DataSource = ds.Tables[0];
                lvclubReg.DataBind();

            }
            else
            {
                pnlclubReg.Visible = true;
                lvclubReg.DataSource = null;
                lvclubReg.DataBind();
                //objCommon.DisplayMessage(this.Page, "No Data Found ", this.Page);
            
            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowStudentDetails()
    {

        int idno = 0;
       idno=Convert.ToInt32( objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" +Convert.ToInt32( Session["userno"])));
       ClubController OBJCLUB = new ClubController();
        DataSet ds = null;
        try
        {


            ds = OBJCLUB.Getshowstudent(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["idno"] = idno;
                lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                lblMobileNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();

                //divsession.Visible = true;
            }
            else
            {
                //divsession.Visible = false;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForPost.ShowStudentDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void populateDropDown()
    {

        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
      //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        //mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.Page, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.Page, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ClubController OBJCLUB = new ClubController();
        int club=0;
       // string subUser = string.Empty;
        //string  typeofactivity = string.Empty;
        string tileofevent = string.Empty;
        string venue = string.Empty;
        string Fdate = string.Empty;
        string Todate = string.Empty;
        string duration=string.Empty;
        string description =string.Empty;
        string file_name = string.Empty;
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            string SP_name = string.Empty; string SP_parameters = string.Empty; string SP_call = string.Empty; int activityNo = 1;
            SP_name = "PKG_ACD_GET_BLOB_STORAGE_BY_ACTIVITY";
            SP_parameters = "@P_ACTIVITY_NO,@P_ORGANIZATION_ID";
            SP_call = "" + activityNo + "," + Convert.ToInt32(Session["OrgId"].ToString()) + "";
            DataSet dsGetConfig = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_call);
            if (dsGetConfig.Tables[0].Rows.Count > 0)
            {
                //blob_ConStr = "DefaultEndpointsProtocol=https;AccountName=cresentdoc;AccountKey=0vZIzNJw2hPGRPDvXUv3eGQsYy7VAOz9GHNnRHIZm0Y0Z/mp7Z40hxbT4ppO2v+AYcslrmw/7h5AUQWr1Otx4A==;EndpointSuffix=core.windows.net";
                ////dsGetConfig.Tables[0].Rows[0]["CONN_STRING"].ToString();
                //blob_ContainerName = dsGetConfig.Tables[0].Rows[0]["CONTAINER_NAME"].ToString();
                //string connection_String = dsGetConfig.Tables[0].Rows[0]["CONN_STRING"].ToString().Trim();
                //string container = dsGetConfig.Tables[0].Rows[0]["CONTAINER_NAME"].ToString().Trim();
                //blob_ConStr = "DefaultEndpointsProtocol=https;AccountName=clubactivity;AccountKey=0vZIzNJw2hPGRPDvXUv3eGQsYy7VAOz9GHNnRHIZm0Y0Z/mp7Z40hxbT4ppO2v+AYcslrmw/7h5AUQWr1Otx4A==;EndpointSuffix=core.windows.net";
                //dsGetConfig.Tables[0].Rows[0]["CONN_STRING"].ToString();
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameCLUB"].ToString();
                 blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                //blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
            }
              if (ViewState["action"].ToString().Equals("add"))
              {
            //int collegeid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID","IDNO="+idno+ ""));
            //int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_ACTIVITY_REGISTRATION", "count(idno)", "IDNO="+idno+ "AND CLUBACTIVITY_TYPE=" + ddlsubuser.SelectedValue + ""));
           
            int maxlimit = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_ACTIVITY_REGISTRATION", "count(idno)", " CLUBACTIVITY_TYPE=" + ddlsubuser.SelectedValue + ""));
            int totalregistration = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_MASTER", "TOTAL_REGNO_LIMIT", " CLUB_ACTIVITY_NO=" + ddlsubuser.SelectedValue + ""));

            //if (COUNT != null)
            //{
            if (maxlimit > totalregistration)
            {
                objCommon.DisplayMessage(this.Page, "Maximum Registration Limit is Reached.", this.Page);
                return;
            }
            //if (COUNT >= 1)
            //{
            //    objCommon.DisplayMessage(this.Page, "Club already Registered.", this.Page);
            //    return;
            //}

           
         
            int CLUBNO = 0;
            //int idno = Convert.ToInt32(Session["idno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            
            if (ddlsubuser.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please  Select Club.", this.Page);
                return;
            }
            {
                club = Convert.ToInt32(ddlsubuser.SelectedValue);
           }



            //foreach (ListItem items in ddlsubuser.Items)
            //{
            //    if (items.Selected == true)
            //    {

            //        subUser += items.Value + ',';
            //    }
            //}
            //if (!subUser.Equals(string.Empty))
            //{
            //    subUser = subUser.Substring(0, subUser.Length - 1);
            //}
            if (txttitle.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Title of Event.", this.Page);
                return;
            }
            {

                tileofevent = txttitle.Text;
            }

            if (txtvenue.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Venue.", this.Page);
                return;
            }
            {

                venue = txtvenue.Text;
            }
            if (txtFromDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter FromDate.", this.Page);
                return;
            }
            {

                Fdate = txtFromDate.Text;
            }
            //if (txtFromDate.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter FromDate.", this.Page);
            //    return;
            //}
            ////else if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtToDate.Text))
            //{

            //    Fdate = txtFromDate.Text;
            //}
            ////else
            ////{
            ////    objCommon.DisplayMessage(this.Page, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
            ////}
            if (txtToDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter ToDate.", this.Page);
                return;
            }
            else if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtToDate.Text))
            {
                Todate = txtToDate.Text;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
                return;
            }
            if (txtduration.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Duration in Hours.", this.Page);
                return;
            }
            {
                duration = txtduration.Text;
            }
            if (txtdescription.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Description of the Event.", this.Page);
                return;
            }
            {
                description = txtdescription.Text;
            }
            
            string photo = string.Empty;



            if (FileUpload1.HasFile)
            {
                file_name = FileUpload1.FileName.ToString();
                string filename = FileUpload1.FileName.ToString().Replace("/","_");
                file_name = filename.ToString().Replace("\"", "_");
                file_name = filename.ToString().Replace(" ", "_");
                //file_name = FileUpload1.FileName.ToString();
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                if (fileExtension.ToUpper().Trim() == ".PDF")
                {

                    //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ACADEMIC/FileUpload/" + FileUpload1.FileName));
                    //string path = Server.MapPath("~/ACADEMIC/FileUpload/");
                    //path = path + filename;
                    //decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    Double FileSize = FileUpload1.PostedFile.ContentLength;
                    //byte[] ImagePhotoByte;
                    double PhotoFileSize = FileUpload1.PostedFile.ContentLength;

                    if (FileSize > 5242880)
                    {
                        objCommon.DisplayMessage(this, "File size should be 5 MB.", this.Page);
                        return;
                    }


                    else
                    {
                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_" + file_name, FileUpload1);// + "_" + docNo + "", fuDoc);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
                    return;
                }


            }
            //if (ViewState["action"] != null)
            //{

            //    if (ViewState["action"].ToString().Equals("add"))
            //    {

            CustomStatus cs = (CustomStatus)OBJCLUB.AddClubRegistration(idno, SessionNo, club, tileofevent, venue, Fdate, Todate, duration, description, file_name, Convert.ToInt32(ddlWeightage.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlHours.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       //ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updclub, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                       // ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updclub, "Record Already Exist !", this.Page);
                    }
                }
            //}
            else
            {
                //if (ViewState["CLUBNO"] != null)
                //{

             int CLUBNO = 0;
            //int idno = Convert.ToInt32(Session["idno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

            if (ddlsubuser.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please  Select Club.", this.Page);
                return;
            }
            {
                club = Convert.ToInt32(ddlsubuser.SelectedValue);
           }

            //foreach (ListItem items in ddlsubuser.Items)
            //{
            //    if (items.Selected == true)
            //    {

            //        subUser += items.Value + ',';
            //    }
            //}
            //if (!subUser.Equals(string.Empty))
            //{
            //    subUser = subUser.Substring(0, subUser.Length - 1);
            //}
            if (txttitle.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Title of Event.", this.Page);
                return;
            }
            {
                tileofevent = txttitle.Text;
            }

            if (txtvenue.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Venue.", this.Page);
                return;
            }
            {
                venue = txtvenue.Text;
            }
            if (txtFromDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter FromDate.", this.Page);
                return;
            }
            {
                Fdate = txtFromDate.Text;
            }
            //if (txtFromDate.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter FromDate.", this.Page);
            //    return;
            //}
            ////else if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtToDate.Text))
            //{

            //    Fdate = txtFromDate.Text;
            //}
            ////else
            ////{
            ////    objCommon.DisplayMessage(this.Page, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
            ////}
            if (txtToDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter ToDate.", this.Page);
                return;
            }
            else if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtToDate.Text))
            {
                Todate = txtToDate.Text;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
                return;
            }
            if (txtduration.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Duration in Hours.", this.Page);
                return;
            }
            {
                duration = txtduration.Text;
            }
            if (txtdescription.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Description of the Event.", this.Page);
                return;
            }
            {
                description = txtdescription.Text;
            }
            
            string photo = string.Empty;
          
            if (FileUpload1.HasFile)
            {
                file_name = FileUpload1.FileName.ToString();
                string filename = FileUpload1.FileName.ToString();
                file_name = FileUpload1.FileName.ToString();
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
                if (fileExtension.ToUpper().Trim() == ".PDF")
                {

                    //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ACADEMIC/FileUpload/" + FileUpload1.FileName));
                    //string path = Server.MapPath("~/ACADEMIC/FileUpload/");
                    //path = path + filename;
                    //decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    Double FileSize = FileUpload1.PostedFile.ContentLength;
                    //byte[] ImagePhotoByte;
                    double PhotoFileSize = FileUpload1.PostedFile.ContentLength;

                    if (FileSize > 5242880)
                    {
                        objCommon.DisplayMessage(this, "File size should be 5 MB.", this.Page);
                        return;
                    }


                    else
                    {
                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_" + file_name, FileUpload1);// + "_" + docNo + "", fuDoc);
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
                    return;
                }
            }
                    CLUBNO = Convert.ToInt32(ViewState["CLUBNO"].ToString());
                    CustomStatus cs = (CustomStatus)OBJCLUB.UpdateClubRegistration(CLUBNO, club, tileofevent, venue, Fdate, Todate, duration, description,file_name ,Convert.ToInt32(ddlWeightage.SelectedValue), Convert.ToInt32(ddlCampus.SelectedValue), Convert.ToInt32(ddlHours.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                      ViewState["action"] = null;
                        Clear();
                        objCommon.DisplayMessage(this.updclub, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        ViewState["action"] = null;
                        Clear();
                        objCommon.DisplayMessage(this.updclub, "Record Already Exist !", this.Page);
                    }

                }
            //}

            //}

            BindListView();
        }
                  
        catch (Exception ex)
        {
            throw;
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
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized,imageWidth,imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }

    public void Clear()
    {
       //ViewState["action"] = null;
        ddlsubuser.ClearSelection();
        txttitle.Text = string.Empty;
        txtvenue.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtdescription.Text = string.Empty;
        txtduration.Text = string.Empty;
        ddlWeightage.ClearSelection();
        ddlHours.ClearSelection();
        ddlCampus.ClearSelection();
       
         

    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        Clear();
    }
    //protected void ddlsubuser_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string subUser = string.Empty;
    //    if (ddlsubuser.Items.Count > 0)
    //    {
    //        foreach (ListItem items in ddlsubuser.Items)
    //        {
    //            if (items.Selected == true)
    //            {
    //                subUser += items.Value + ',';
    //            }
    //        }
    //        subUser = subUser.Substring(0, subUser.Length - 1);
    //        DataSet ds = OBJCLUB.GetClubActivityRegByNo((subUser));


    //    }
        
    //    //foreach (ListViewDataItem item in lvclubReg.Items)
    //    //{

    //    //}
    //    //foreach (ListItem items in ddlsubuser.Items)
    //    //{
    //    //    if (items.Selected == true)
    //    //    {
    //    //        subUser += items.Value + ',';

    //    //    }

    //    //}

    //}

  
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton Edit = sender as ImageButton;
        ViewState["CLUBNO"] = Convert.ToInt32(Edit.CommandArgument);
        string subUser = string.Empty;
        // int clubid = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_ACTIVITY_REGISTRATION", "CLUB_NO", "IDNO = '" + idno + "'"));
        //"CLUB_NO= " + Convert.ToInt32(ViewState["CLUBNO"])
        DataSet dsEdit;
        dsEdit = OBJCLUB.GetClubActivityRegeShowDetails(Convert.ToInt32(ViewState["CLUBNO"]));
        //dsEdit = objCommon.FillDropDown("ACD_CLUB_ACTIVITY_REGISTRATION", "CLUB_NO", "CLUBACTIVITY_TYPE,TITLE_OF_EVENT,VENUE,FROMDATE,TODATE,DURATION,DESCRIPTION_OF_EVENT", "CLUB_NO= " + Convert.ToInt32(ViewState["CLUBNO"]), "CLUB_NO");
        if (dsEdit.Tables[0].Rows.Count > 0)
        {
            //char clubChars = ',';
            ddlsubuser.SelectedValue = dsEdit.Tables[0].Rows[0]["CLUBACTIVITY_TYPE"].ToString();

            txttitle.Text = dsEdit.Tables[0].Rows[0]["TITLE_OF_EVENT"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["TITLE_OF_EVENT"].ToString();
            txtvenue.Text = dsEdit.Tables[0].Rows[0]["VENUE"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["VENUE"].ToString();

            txtFromDate.Text = dsEdit.Tables[0].Rows[0]["FROMDATE"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["FROMDATE"].ToString();
            txtToDate.Text = dsEdit.Tables[0].Rows[0]["TODATE"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["TODATE"].ToString();

            txtduration.Text = dsEdit.Tables[0].Rows[0]["DURATION"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["DURATION"].ToString();
            txtdescription.Text = dsEdit.Tables[0].Rows[0]["DESCRIPTION_OF_EVENT"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["DESCRIPTION_OF_EVENT"].ToString();

            ddlWeightage.SelectedValue = dsEdit.Tables[0].Rows[0]["HOUR_WEIGHTAGENO"].ToString();
            ddlHours.SelectedValue = dsEdit.Tables[0].Rows[0]["HOUR_COUNTNO"].ToString();
            ddlCampus.SelectedValue = dsEdit.Tables[0].Rows[0]["CAMPUS_NO"].ToString();


            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
        }
        else
        {
            ViewState["action"] = "add";
        }

    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

}