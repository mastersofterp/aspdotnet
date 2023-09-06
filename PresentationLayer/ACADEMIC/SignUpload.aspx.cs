//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : SignUpload                                                   
// CREATION DATE : 4/10/2019                                                        
// CREATED BY    : Nidhi Gour                                
// MODIFIED DATE : 16/10/2019                                                         
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_SignUpload : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    byte[] image;
    string authorityno;
    int uano;
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillAuthority();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
        }
    }

    public void fillAuthority()
    {
        DataSet ds = objCommon.FillDropDown("acd_authority", "AUTHORITYNO", "AUTHORITYNAME", "AUTHORITYNO>0", "AUTHORITYNO");

        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    ListItem rbitem;
                    string itemtext = "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["AUTHORITYNAME"].ToString();
                    rbitem = new ListItem(itemtext, ds.Tables[0].Rows[i]["AUTHORITYNO"].ToString());
                    rblAthority.Items.Add(rbitem);
                }

            }
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
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string ext = System.IO.Path.GetExtension(this.fuSignUpload.PostedFile.FileName);
        if (fuSignUpload.HasFile)
        {
            if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG")
            {
                if (fuSignUpload.PostedFile.ContentLength < 153600)
                {

                    byte[] resizephoto = ResizePhoto(fuSignUpload);
                    if (resizephoto.LongLength >= 153600)
                    {
                        objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                        return;
                    }
                    else
                    {
                        image = this.ResizePhoto(fuSignUpload);

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpersonalinformation, "File size must be less or equal to 150kb", this.Page);
                    return;
                }


            }
            else
            {
                objCommon.DisplayMessage(this.updpersonalinformation, "Only JPG,JPEG,PNG files are allowed!", this.Page);
                return;
            }

        }
        else
        {
            System.IO.FileStream ff = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/logo.gif"), System.IO.FileMode.Open);
            int ImageSize = (int)ff.Length;
            byte[] ImageContent = new byte[ff.Length];
            ff.Read(ImageContent, 0, ImageSize);
            ff.Close();
            ff.Dispose();
            //objRef.CollegeLogo = ImageContent;
        }


        string authorityno = rblAthority.SelectedValue.Trim();
        CustomStatus cs = (CustomStatus)objCommon.AddSign(image, authorityno, Convert.ToInt32(Session["userno"]));
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this.updpersonalinformation, "Signature Uploaded Successfully!!", this.Page);
            //showstudentsignature();

        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(this.updpersonalinformation, "Signature Updated Successfully!!", this.Page);

        }
        else
        {
            objCommon.DisplayMessage("Error!!", this.Page);
        }

        clear();
    }
    protected void clear()
    {
        rblAthority.SelectedValue = null;
        //Image2.Visible = false;
    }




    protected void showstudentphoto()
    {
        try
        {

            string athorityno = objCommon.LookUp("ACD_AUTHORITY_SIGN", "ISNULL(AUTH_ID,0)", "AUTH_ID='" + rblAthority.SelectedValue.Trim() + "'");
            if (authorityno != "")
            {
                string signphoto = objCommon.LookUp("ACD_AUTHORITY_SIGN", "AUTH_SIGN", "AUTH_ID='" + rblAthority.SelectedValue.Trim() + "'");

                if (signphoto == string.Empty)
                {

                    // Image2.ImageUrl = "~/images/sign11.jpg"; ;
                }
                else
                {
                    //Image2.ImageUrl = "~/showimage.aspx?id=0&type=" + rblAthority.SelectedValue.Trim().ToString();
                    //Image2.ImageUrl = "~/showimage.aspx?authority=" + authority.Trim().ToString();

                }
            }
            else
            {
                //Image2.ImageUrl = null;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void rblAthority_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Image2.Visible = true;
        showstudentphoto();
    }

}
