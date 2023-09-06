using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class ACADEMIC_Fitnessdoc_Submission : System.Web.UI.Page
{
    Common objCommon = new Common();
    College objCollege = new College();
    CollegeController objController = new CollegeController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objstud = new StudentController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                
                
                BindListView();
                ShowStudents();
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.CheckPageAuthorization();
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_DOCUMENT_SUBMISSION.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        findroll();
        if (Session["username"].ToString() == "admin")
        {
            try
            {
                string studno = txtTempIdno.Text.Trim();
                int idno1 = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "COUNT(IDNO)COUNT", "ENROLL='" + studno + "' or ROLL = '" + studno + "' "));

                if (idno1 == 0)
                {
                    if (FileUpload1 != null)
                    {
                        string[] validFileTypes = { "jpg", "jpeg", "doc", "pdf" };
                        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        bool isValidFile = false;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {
                            if (ext == "." + validFileTypes[i])
                            {
                                isValidFile = true;
                                break;
                            }
                        }
                        if (!isValidFile)
                        {
                            objCommon.DisplayMessage(updCollege, "Upload the Documents only with following formats: .jpg, .jpeg, .doc, .pdf", this);
                        }
                        else
                        {
                            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + txtTempIdno.Text.Trim() + "' or ROLLNO = '" + txtTempIdno.Text.Trim() + "' "));
                            string enroll = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "ENROLLNO='" + txtTempIdno.Text.Trim() + "' or ROLLNO = '" + txtTempIdno.Text.Trim() + "' "));                           
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UPLOAD_FILES/Fitness_Doc/") + "idno_" + idno + "_" + fileName);
                            string path = MapPath("~/UPLOAD_FILES/Fitness_Doc/");
                            string fileType = System.IO.Path.GetExtension(fileName);

                            objCollege.IDNO = idno;
                            objCollege.ENROLL = enroll;
                            objCollege.ROLL = Session["rollno"].ToString();
                            objCollege.DOCTYPE = fileType;
                            objCollege.DOCPATH = path;
                            objCollege.DOCNAME = "idno_" + idno + "_" + fileName;
                            objCollege.DOCUMENTNO = 1;

                            string output = objController.addfitfile(objCollege);
                            objCommon.DisplayMessage(updCollege, "File Uploaded Successfully", this);
                            BindListadmin();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "File Not Uploaded", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCollege, "File Already Uploaded", this);
                }
            }
            catch (Exception ex)
            {
                objCommon.DisplayMessage(updCollege, "ERROR", this);
            }

        }
        else 
        {
            try
            {
                findroll();
                int idno1 = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "COUNT(IDNO)COUNT", "ENROLL='" + Session["username"].ToString() + "'"));

                if (idno1 == 0)
                {
                    if (FileUpload1 != null)
                    {
                        string[] validFileTypes = { "jpg", "jpeg", "doc", "pdf" };
                        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        bool isValidFile = false;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {
                            if (ext == "." + validFileTypes[i])
                            {
                                isValidFile = true;
                                break;
                            }
                        }
                        if (!isValidFile)
                        {
                            objCommon.DisplayMessage(updCollege, "Upload the Documents only with following formats: .jpg, .jpeg, .doc, .pdf", this);
                        }
                        else
                        {
                            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + Session["username"].ToString() + "'"));
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UPLOAD_FILES/Fitness_Doc/") + "idno_" + idno + "_" + fileName);
                            string path = MapPath("~/UPLOAD_FILES/Fitness_Doc/");
                            string fileType = System.IO.Path.GetExtension(fileName);

                            objCollege.IDNO = idno;
                            objCollege.ENROLL = Session["username"].ToString();
                            objCollege.ROLL = Session["rollno"].ToString();
                            objCollege.DOCTYPE = fileType;
                            objCollege.DOCPATH = path;
                            objCollege.DOCNAME = "idno_" + idno + "_" + fileName;
                            objCollege.DOCUMENTNO = 1;

                            string output = objController.addfitfile(objCollege);
                            objCommon.DisplayMessage(updCollege, "File Uploaded Successfully", this);
                            BindListView();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updCollege, "File Not Uploaded", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updCollege, "File Already Uploaded", this);
                }
            }
            catch (Exception ex)
            {
                objCommon.DisplayMessage(updCollege, "ERROR", this);
            }
        }
        
    }

    private void BindListView()
    {
        if (Session["username"].ToString() == "admin")
        {
            
        }
        else 
        {
            try
            {
                int cntidno = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "COUNT(IDNO)COUNT", "ENROLL='" + Session["username"].ToString() + "'"));
                if (cntidno != 0)
                {
                    int bndidno = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "IDNO", "ENROLL='" + Session["username"].ToString() + "'"));
                    objCollege.IDNO = bndidno;
                    //lblfit.visible = true;
                    CollegeController objController = new CollegeController();
                    DataSet ds = objController.GetAllFit(objCollege);
                    lvStud.DataSource = ds;
                    lvStud.DataBind();
                    lblstat.Visible = false;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.DisplayMessage("Server UnAvailable", this.Page);
            }
        }
        
    }

    private void BindListadmin()
    {
        string studno = txtTempIdno.Text.Trim();

        try
        {
            int cntidno = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "COUNT(IDNO)COUNT", "ENROLL='" + studno + "' or ROLL='" + studno + "' "));
            if (cntidno != 0)
            {
                int bndidno = Convert.ToInt32(objCommon.LookUp("ACD_ADM_FITNESS_DOC", "IDNO", "ENROLL='" + studno + "' or ROLL='" + studno + "' "));
                objCollege.IDNO = bndidno;
                //lblfit.visible = true;
                CollegeController objController = new CollegeController();
                DataSet ds = objController.GetAllFit(objCollege);
                lvStud.DataSource = ds;
                lvStud.DataBind();
                lblstat.Visible = false;
            }
            else
            {
                lblstat.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ShowStudents()
    {
        if (Session["username"].ToString() == "admin")
        {
            divadmin.Visible = true;          
        }

        else 
        
        {
           divuplod.Visible = true;
           int idno1 = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + Session["username"].ToString() + "'"));
           objCollege.IDNO = idno1;
           DataSet ds = objController.GetAllstudinfo(objCollege);

           if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
           {
               if (ds.Tables[0].Rows.Count > 0)
               {
                   //Show Student Details..
                   lblDegree.Text = ds.Tables[0].Rows[0]["degreename"].ToString();
                   lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                   lblRegno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                   lblName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();
                   //lblAdmbatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                   lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                   lblBranch.ToolTip = ds.Tables[0].Rows[0]["branchno"].ToString();
                   lblSemester.Text = ds.Tables[0].Rows[0]["semfullname"].ToString();
                   lblDegree.ToolTip = ds.Tables[0].Rows[0]["degreeno"].ToString();
                   lblroll.Text = ds.Tables[0].Rows[0]["rollno"].ToString();
                   divDetails.Visible = true;

               }
           }
           else
           {

           }
        }
        
       
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
        string studno = txtTempIdno.Text.Trim();
        try
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "ENROLLNO='" + studno + "'  OR ROLLNO='" + studno + "' "));
            objCollege.IDNO = idno;
            DataSet ds = objController.GetAllstudinfo(objCollege);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblDegree.Text = ds.Tables[0].Rows[0]["degreename"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblRegno.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                    lblName.ToolTip = ds.Tables[0].Rows[0]["IDNO"].ToString();
                    //lblAdmbatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = ds.Tables[0].Rows[0]["branchno"].ToString();
                    lblSemester.Text = ds.Tables[0].Rows[0]["semfullname"].ToString();
                    lblDegree.ToolTip = ds.Tables[0].Rows[0]["degreeno"].ToString();
                    lblroll.Text = ds.Tables[0].Rows[0]["rollno"].ToString();
                    divDetails.Visible = true;
                    BindListadmin();
                    divuplod.Visible = true;
                }
            }
            else
            {
                
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.DisplayMessage(updCollege, "Please Input Valid Enrollment or RollNo.", this);
                //objUCommon.ShowError(Page, "Academic " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
        
    }

    public void findroll()
    {
        if (Session["username"].ToString() == "admin")
        {
            string rollno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "ROLLNO", "ENROLLNO='" + txtTempIdno.Text.Trim() + "' or ROLLNO = '" + txtTempIdno.Text.Trim() + "'"));
            Session["rollno"] = rollno;
        }
        else
        {
            string rollno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "ROLLNO", "ENROLLNO='" + Session["username"].ToString() + "'"));
            Session["rollno"] = rollno;
        }

    
    }
}