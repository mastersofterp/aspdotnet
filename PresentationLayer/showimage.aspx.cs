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

public partial class showimage : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();

    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["id"] != null && Request.QueryString["type"] != null)
        //{
        //    DataTableReader dtr = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[2];
        //        objParams[0] = new SqlParameter("@P_Idno", Convert.ToInt32(Request.QueryString["id"]));
        //        objParams[1] = new SqlParameter("@P_TYPE", Request.QueryString["type"]);

        //        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
        //        byte[] imgData = null;
        //        byte[] imgSign = null;

        //        if (dtr.Read())
        //            if (Request.QueryString["type"] == "STUDENT" && dtr["PHOTO"].ToString() == "")
        //            {
        //                imgData = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/IMAGES/nophoto.jpg"));
        //                Response.BinaryWrite(imgData);
        //            }
        //            else if (Request.QueryString["type"] == "STUDENT" && dtr["PHOTO"].ToString() != "")
        //            {
        //                imgData = dtr["PHOTO"] as byte[];
        //                Response.BinaryWrite(imgData);
        //            }
        //            else if (Request.QueryString["type"] == "STUDENTSIGN" && dtr["STUD_SIGN"].ToString() == "")
        //            {
        //                imgSign = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/IMAGES/nophoto.jpg"));
        //                Response.BinaryWrite(imgSign);
        //            }
        //            else
        //            {
        //                imgSign = dtr["STUD_SIGN"] as byte[];
        //                Response.BinaryWrite(imgSign);
        //            }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.showimage->" + ex.ToString());
        //    }
        //}


        if (Request.QueryString["id"] != null && Request.QueryString["type"] != null)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_Idno", Convert.ToInt32(Request.QueryString["id"]));
                objParams[1] = new SqlParameter("@P_TYPE", Request.QueryString["type"]);

                dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
                byte[] imgData = null;
                byte[] imgSign = null;

                if (dtr.Read())
                if (Request.QueryString["type"] == "STUDENT" && dtr["PHOTO"].ToString() != "")
                {
                    imgData = dtr["PHOTO"] as byte[];
                    Response.BinaryWrite(imgData);
                }

                else if (Request.QueryString["type"] == "EMP" && dtr["PHOTO"].ToString() != "")
                {
                    imgData = dtr["PHOTO"] as byte[];
                    Response.BinaryWrite(imgData);
                }

                else if (Request.QueryString["type"] == "EMPSIGN" && dtr["PHOTO"].ToString() != "")
                {
                    imgData = dtr["PHOTO"] as byte[];
                    Response.BinaryWrite(imgData);
                }
                else if (Request.QueryString["type"] == "STUDENTSIGN" && dtr["STUD_SIGN"].ToString() != "")
                {
                    imgSign = dtr["STUD_SIGN"] as byte[];
                    Response.BinaryWrite(imgSign);
                }

                else if (Request.QueryString["type"] == "college" && dtr["PHOTO"].ToString() != "")
                {
                    imgData = dtr["PHOTO"] as byte[];
                    Response.BinaryWrite(imgData);
                }
               
                // Added on 14-09-2022
                else  if (Request.QueryString["type"] == "REGISTRAR_SIGN" && dtr["STUD_SIGN"].ToString() != "")
                {
                    imgData = dtr["STUD_SIGN"] as byte[];
                    Response.BinaryWrite(imgData);
                }
               
                //added by tanu 08/12/2022 for college banner

                else if (Request.QueryString["type"] == "CLGBANNER" && dtr["CLGBANNER"].ToString() != "")
                {
                    imgData = dtr["CLGBANNER"] as byte[];
                    Response.BinaryWrite(imgData);
                }
                else if (Request.QueryString["type"] == "COMPIMG" && dtr["PHOTO"].ToString() != "")
                {
                    imgData = dtr["PHOTO"] as byte[];
                    Response.BinaryWrite(imgData);
                }
                else
                {
                    imgSign = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/IMAGES/nophoto.jpg"));
                    Response.BinaryWrite(imgSign);
                }

                //else
                //{
                //    imgSign = dtr["STUD_SIGN"] as byte[];
                //    Response.BinaryWrite(imgSign);
                //}

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.showimage->" + ex.ToString());
            }
        }

    }
}
