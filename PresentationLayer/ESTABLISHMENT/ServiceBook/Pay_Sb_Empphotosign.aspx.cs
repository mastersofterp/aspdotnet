using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ESTABLISHMENT_ServiceBook_Pay_Sb_Empphotosign : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    static int ImgId = 0;
    public int _idnoEmp;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string empno = ViewState["idno"].ToString();

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

            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }
        //int user_type = 0;
        //user_type = Convert.ToInt32(Session["usertype"].ToString());
        //if (user_type != 1)
        //{
        //    btnSubmit.Enabled = false;            
        //    btnUpload.Enabled = false;
        //}
        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        this.BindListViewEmpPhotoSign();
        GetConfigForEditAndApprove();
    }

    private void BindListViewEmpPhotoSign()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllEmpPhotoSign(_idnoEmp);
            lvEmpImage.DataSource = ds;
            lvEmpImage.DataBind();

            for (int i = 0; i <= lvEmpImage.Items.Count - 1; i++)
            {

                Image imgPhoto = lvEmpImage.Items[i].FindControl("imgPhoto") as Image;

                Image imgsign = lvEmpImage.Items[i].FindControl("imgsign") as Image;

                // imgPhoto.ImageUrl = "showimage.aspx?id=" + ds.Tables[0].Rows[i]["IMAGEID"].ToString() + "&type=BOOKIMG";

                //ViewState["photo"] = ds.Tables[0].Rows[i]["empimage"] as byte[];
                //Byte[] bytes = ViewState["photo"] as byte[];
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                //imgPhoto.Attributes.Add("src", "data:image/png;base64," + base64String);


                //Image imgsign = lvEmpImage.Items[i].FindControl("imgsign") as Image;
                //ViewState["Sign"] = ds.Tables[0].Rows[i]["empimage"] as byte[];

                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + _idnoEmp + "&type=EMP";
                imgsign.ImageUrl = "../../showimage.aspx?id=" + _idnoEmp + "&type=EMPSIGN";


            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.BindListViewEmpImage-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            ServiceBook objSevBook = new ServiceBook();
            if (fuplEmpPhoto.HasFile)
            {
                if (fuplEmpPhoto.FileContent.Length >= 102400)
                {

                    MessageBox("File Size Should Not Be Greater Than 100 Kb");
                    fuplEmpPhoto.Dispose();
                    fuplEmpPhoto.Focus();
                    return;
                }
            }

            if (fuplEmpSign.HasFile)
            {
                if (fuplEmpSign.FileContent.Length >= 102400)
                {

                    MessageBox("File Size Should Not Be Greater Than 100 Kb");
                    fuplEmpSign.Dispose();
                    fuplEmpSign.Focus();
                    return;
                }

            }
            objSevBook.IDNO = _idnoEmp;


            if (fuplEmpPhoto.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(fuplEmpPhoto.FileName)))
                {
                    objSevBook.Photo = objCommon.GetImageData(fuplEmpPhoto);
                }
                else
                {
                    MessageBox("Please Upload Valid Files[.jpg,.jpeg]");
                    fuplEmpPhoto.Dispose();
                    fuplEmpPhoto.Focus();
                    return;
                }
            }
            else
            {
                objSevBook.Photo = null;
                MessageBox("Please Upload Photo");
                return;
            }
            if (fuplEmpSign.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(fuplEmpSign.FileName)))
                {
                    objSevBook.PhotoSign = objCommon.GetImageData(fuplEmpSign);
                }
                else
                {
                    MessageBox("Please Upload Valid Files[.jpg,.jpeg]");
                    fuplEmpSign.Dispose();
                    fuplEmpSign.Focus();
                    return;
                }
            }
            else
            {
                objSevBook.PhotoSign = null;
                MessageBox("Please Upload Sign");
                return;
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objServiceBook.AddEmployeePhotoSign(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        this.BindListViewEmpPhotoSign();
                        MessageBox("Record Saved Successfully");
                    }
                }
                else
                {
                    if (ViewState["IDNO"] != null)
                    {
                        if (objSevBook.Photo == null)
                        {
                            objSevBook.Photo = ViewState["PHOTO"] as byte[];
                        }

                        if (objSevBook.PhotoSign == null)
                        {
                            objSevBook.PhotoSign = ViewState["SIGNA"] as byte[];
                        }
                        CustomStatus cs = (CustomStatus)objServiceBook.AddEmployeePhotoSign(objSevBook);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            this.BindListViewEmpPhotoSign();
                            this.Clear();
                            // this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            MessageBox("Record Updated Successfully");
                        }
                    }
                }
            }




        }
        catch (Exception ex)
        {

        }

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GetConfigForEditAndApprove();
    }

    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int idno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(idno);
            ViewState["action"] = "edit";


        }
        catch (Exception ex)
        {
        }
    }

    private void Clear()
    {


        ViewState["action"] = "add";
        Image1.Attributes.Clear();
        Image1.ImageUrl = "~/IMAGES/" + "nophoto.jpg";

        imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
        imgEmpPhoto.Attributes.Clear();
        Image1.ImageUrl = "~/images/nophoto.jpg";
        ViewState["IDNO"] = null;
        ViewState["PHOTO"] = null;
        ViewState["SIGNA"] = null;
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;


    }

    private void ShowDetails(int idno)
    {

        ViewState["PHOTO"] = null;
        ViewState["IDNO"] = null;
        ViewState["SIGNA"] = null;
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetAllEmpPhotoSign(idno);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["idno"] = idno.ToString();


                // Image1.ImageUrl = "~/IMAGES/" + "UpLoadImage.jpg";
                ImgId = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["IDNO"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString());
                ViewState["PHOTO"] = ds.Tables[0].Rows[0]["PHOTO"] as byte[];
                ViewState["SIGNA"] = ds.Tables[0].Rows[0]["SIGNA"] as byte[];
                //Byte[] bytes = ViewState["PHOTO"] as byte[];
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                //Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                //Image1.Attributes.Add("src", "data:image/png;base64," + base64String);

                imgEmpPhoto.ImageUrl = "../../showimage.aspx?id=" + _idnoEmp + "&type=EMP";
                Image1.ImageUrl = "../../showimage.aspx?id=" + _idnoEmp + "&type=EMPSIGN";
                if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
                {
                    //string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                    //if (STATUS == "A")
                    //{
                    //    MessageBox("Your Details Are Approved You Cannot Edit.");
                    //    btnSubmit.Enabled = false;
                    //    return;
                    //}
                    //else
                    //{
                    //    btnSubmit.Enabled = true;
                    //}
                    GetConfigForEditAndApprove();
                }
                else
                {
                    btnSubmit.Enabled = true;
                    GetConfigForEditAndApprove();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".jpg", ".JPG",".jpeg", ".JPEG" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Photo Signature Upload";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}