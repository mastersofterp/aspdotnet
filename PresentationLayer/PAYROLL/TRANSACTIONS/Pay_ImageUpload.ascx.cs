//======================================================================================
// PROJECT NAME  : NITPRM                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ImageUpload.ascx                                                
// CREATION DATE : 23-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PayRoll_Pay_ImageUpload : System.Web.UI.UserControl
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
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
                //Page Authorization
                //CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
            this.FillDropDown();
            //NormalImage.Src = "~/IMAGES/" + "nophoto.jpg";
            Image1.ImageUrl = "~/IMAGES/" + "nophoto.jpg";
            // this.BindListViewEmpImage();
        }
        int user_type = 0;
        user_type = Convert.ToInt32(Session["usertype"].ToString());
        if (user_type != 1)
        {
            btnSubmit.Enabled = false;
            ddlImageType.Enabled = false;
            btnUpload.Enabled = false;
        }
        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        this.BindListViewEmpImage();


    }

    private void BindListViewEmpImage()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllEmpImageDetails(_idnoEmp);
            lvEmpImage.DataSource = ds;
            lvEmpImage.DataBind();

            for (int i = 0; i <= lvEmpImage.Items.Count - 1; i++)
            {

                Image imgPhoto = lvEmpImage.Items[i].FindControl("imgPhoto") as Image;

                // imgPhoto.ImageUrl = "showimage.aspx?id=" + ds.Tables[0].Rows[i]["IMAGEID"].ToString() + "&type=BOOKIMG";

                ViewState["photo"] = ds.Tables[0].Rows[i]["empimage"] as byte[];
                Byte[] bytes = ViewState["photo"] as byte[];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgPhoto.Attributes.Add("src", "data:image/png;base64," + base64String);


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
            if (ViewState["File"] == null)
            {
                this.objCommon.DisplayMessage("Please Upload Image", this.Page);
                return;
            }
            // UpdatePanel updpersonaldetails = (UpdatePanel)this.Parent.FindControl("upWebUserControl");
            //Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
           
            objSevBook.imageid = Convert.ToInt32(ddlImageType.SelectedValue);
            objSevBook.imagetype = Convert.ToString(ddlImageType.SelectedItem);

            objSevBook.empimage = ViewState["File"] as byte[];
            // ViewState["imageTrxId"] 
            // objSevBook.empimage = objCommon.GetImageData(fuUploadImage);
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds = objCommon.FillDropDown("PAYROLL_EMP_IMAGE", "*", "", "IDNO=" + _idnoEmp + " AND imageid=" + ddlImageType.SelectedValue + "", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Already Exists", this.Page);
                        return;
                    }
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddEmpImage(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        this.BindListViewEmpImage();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["imageTrxId"] != null)
                    {

                        if (objSevBook.imageid == ImgId)
                        {
                            //current record updation without any change
                            //allow 
                            if (objSevBook.empimage == null)
                            {
                                objSevBook.empimage = ViewState["photo"] as byte[];
                            }

                            objSevBook.imagetrxid = Convert.ToInt32(ViewState["imageTrxId"].ToString());
                            CustomStatus cs = (CustomStatus)objServiceBook.UpdateEmpImage(objSevBook);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                this.BindListViewEmpImage();
                                this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);

                            }
                        }
                        else
                        {
                            // to update, first check duplication
                            DataSet ds = objCommon.FillDropDown("PAYROLL_EMP_IMAGE", "*", "", "imageid=" + ddlImageType.SelectedValue + " ", "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                this.objCommon.DisplayMessage(updpersonaldetails, "Record Already Exists", this.Page);
                                return;
                            }
                            else
                            {
                                //update by changing image type
                                objSevBook.imagetrxid = Convert.ToInt32(ViewState["imageTrxId"].ToString());
                                CustomStatus cs = (CustomStatus)objServiceBook.UpdateEmpImage(objSevBook);
                                if (cs.Equals(CustomStatus.RecordUpdated))
                                {
                                    ViewState["action"] = "add";
                                    this.Clear();
                                    this.BindListViewEmpImage();
                                    this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);

                                }
                            }
                        }


                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int imageTrxId = int.Parse(btnEdit.CommandArgument);
            ShowDetails(imageTrxId);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int imageTrxId)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleEmpImageDetails(imageTrxId);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["imageTrxId"] = imageTrxId.ToString();

                ddlImageType.SelectedValue = ds.Tables[0].Rows[0]["imageid"].ToString();
                ImgId = Convert.ToInt32(ds.Tables[0].Rows[0]["imageid"].ToString());
                ViewState["photo"] = ds.Tables[0].Rows[0]["empimage"] as byte[];
                Byte[] bytes = ViewState["photo"] as byte[];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);

                Image1.Attributes.Add("src", "data:image/png;base64," + base64String);




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
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int user_type = 0;
            user_type = Convert.ToInt32(Session["usertype"].ToString());
            if (user_type != 1)
            {
                MessageBox("Sorry! U have not privilege for this tab!");
                return;
            }
            ImageButton btnDel = sender as ImageButton;
            int imageTrxId = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteEmpImage(imageTrxId);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewEmpImage();
                ViewState["action"] = "add";
                //UpdatePanel updpersonaldetails = (UpdatePanel)this.Parent.FindControl("upWebUserControl");
                Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");

                this.objCommon.DisplayMessage(updpersonaldetails, "Record Deleted Successfully", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {

        ddlImageType.SelectedValue = "0";
        ViewState["action"] = "add";
        Image1.Attributes.Clear();
        Image1.ImageUrl = "~/IMAGES/" + "nophoto.jpg";
        ViewState["File"] = null;
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlImageType, "payroll_photo_type", "photo_typeid", "photo_type", "photo_typeid > 0", "photo_typeid");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ImageUpload.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }

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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuUploadImage.HasFile)
        {
            if (fuUploadImage.FileName.Contains(".jpeg") || fuUploadImage.FileName.Contains(".png") || fuUploadImage.FileName.Contains(".jpg") || fuUploadImage.FileName.Contains(".JPEG") || fuUploadImage.FileName.Contains(".PNG") || fuUploadImage.FileName.Contains(".JPG"))
            {
                Image1.Attributes.Clear();
                fuUploadImage.SaveAs(MapPath("~/IMAGES/" + fuUploadImage.FileName));
                System.Drawing.Image img1 = System.Drawing.Image.FromFile(MapPath("~/IMAGES/") + fuUploadImage.FileName);
                Image1.ImageUrl = "~\\IMAGES\\" + fuUploadImage.FileName;
                ViewState["File"] = objCommon.GetImageData(fuUploadImage);
            }
            else
            {
                this.objCommon.DisplayMessage("Only .Jpeg/.Jpg/.Png Format is Allow", this.Page);
            }
        }
        else
        {
            Image1.ImageUrl = "~/IMAGES/" + "nophoto.jpg";
            this.objCommon.DisplayMessage("Please Select Image", this.Page);
        }

    }
    protected void ddlImageType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
