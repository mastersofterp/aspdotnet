using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class ShowVoucherImage : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
       string id=string.Empty;
        imgPhoto.ImageUrl = "~/images/nophoto.jpg";

        

        if (!IsPostBack)
        { 
         if(Request.QueryString["id"] != null)
         {
             id = Request.QueryString["id"].ToString().Split(',')[1].ToString().Trim();
             if (id.ToString().Trim() != "no")
             {

                 DataSet dsphoto = objCommon.FillDropDown("ACC_VOUCHER_PHOTO", "*", "", "voucher_no=" + id.ToString().Trim() + " AND company_code='" + Session["comp_code"].ToString().Trim() + "'", "");
                 if (dsphoto != null)
                 {
                     if (dsphoto.Tables[0].Rows.Count > 0)
                     {
                         byte[] imgData = null;
                         imgData = dsphoto.Tables[0].Rows[0]["PHOTO"] as byte[];
                         Response.BinaryWrite(imgData);


                     }
                     else
                     {
                         objCommon.DisplayMessage("No Scan Voucher Present", this);
                         //Response.Write("<Script language='javascript' type='text/javascript'> window.close(); </Script>");
                         this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<Script language='javascript' type='text/javascript'> window.close(); </Script>");
                         return;
                     
                     }


                 }
             }
             else
             {

                 ViewState["vchid"] = Request.QueryString["id"].ToString().Split(',')[2].ToString().Trim();
               
             }

         }
         
        
        }

        //if (Request.QueryString["id"] != null && Request.QueryString["type"] != null)
        //{
        //    //DataTableReader dtr = null;
        //    //try
        //    //{
        //    //    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //    //    SqlParameter[] objParams = null;
        //    //    objParams = new SqlParameter[2];
        //    //    objParams[0] = new SqlParameter("@P_Idno", Convert.ToInt32(Request.QueryString["id"]));
        //    //    objParams[1] = new SqlParameter("@P_TYPE", Request.QueryString["type"]);

        //    //    dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
        //    //    byte[] imgData = null;
        //    //    if (dtr.Read()) imgData = dtr["PHOTO"] as byte[];
        //    //    Response.BinaryWrite(imgData);

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw new IITMSException("IITMS.NITPRM.showimage->" + ex.ToString());
        //    //}
        //}
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        
        VoucherPhoto objSPhoto = new VoucherPhoto();
        objSPhoto.Idno = 0;

        if (fuPhotoUpload.HasFile)
        {
            objSPhoto.Photo1 = objCommon.GetImageData(fuPhotoUpload);
        }
        else
        {
            objSPhoto.Photo1 = null;
        }
        objSPhoto.CollegeCode = Session["colcode"].ToString().Trim();
        objSPhoto.PhotoPath = fuPhotoUpload.FileName.ToString();
        objSPhoto.PhotoSize = 1;
        
        if (ViewState["vchid"] != null)
        {
            if (ViewState["vchid"].ToString().Trim() == "no")
            {
                objSPhoto.VoucherNo = "0";


            }
            else
            {
                objSPhoto.VoucherNo = ViewState["vchid"].ToString().Trim();

            }
        
        
        
        }

        AccountTransactionController atc = new AccountTransactionController();
        atc.AddVoucherPhotoTemp(objSPhoto, Session["comp_code"].ToString().Trim());

        objCommon.DisplayMessage("Voucher Scaned Successfully.", this);
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<Script language='javascript' type='text/javascript'> window.close(); </Script>");
        return;
        


        


    }
}
