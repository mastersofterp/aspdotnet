using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;



using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

public partial class UnderConstruction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
            Common objCommon = new Common();
            DataSet ds = objCommon.FillDropDown("REFF", "isnull(MAINTENANCE,1)MAINTENANCE", "MAINTENANCE_STIME,isnull(MAINTENANCE_ENDTIME,60)MAINTENANCE_ENDTIME,CollegeName", "", "");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string chk = ds.Tables[0].Rows[0]["MAINTENANCE"].ToString();

                if (ds.Tables[0].Rows[0]["MAINTENANCE_STIME"] != DBNull.Value && ds.Tables[0].Rows[0]["MAINTENANCE_ENDTIME"] != DBNull.Value)
                {
                    string date = ds.Tables[0].Rows[0]["MAINTENANCE_STIME"].ToString();
                    DateTime StartTime = DateTime.Parse(date);
                    hdfdate.Value = StartTime.ToString("MM/dd/yyyy HH:mm:ss");

                    string endTime = ds.Tables[0].Rows[0]["MAINTENANCE_ENDTIME"].ToString();//"2023-02-14 01:35:00";//get date from ds
                    //DateTime EndTime = DateTime.Parse(endTime);
                    hdfEndTime.Value = endTime;//EndTime.ToString("MM/dd/yyyy HH:mm:ss");

                    DateTime TimeNow = DateTime.Now;

                    if (chk == "1" || StartTime >= TimeNow)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                }
                else {
                    if (chk == "1")
                    {
                        Response.Redirect("~/default.aspx");
                    }
                }



                string colName = ds.Tables[0].Rows[0]["CollegeName"].ToString();

                lblclgName.Text = colName == string.Empty ? "" : colName;
                imgLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
            }
        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateMaintenance(int flag)
    {
        object ret = 0;
        Common objCommon = new Common();
        UnderConstruction objUC = new UnderConstruction();
        objCommon.DisplayMessage("hit",objUC.Page);

        string p_call = "" + flag + "," + 0;
        string p_para = "@P_FLAG,@P_OUTPUT";
        string p_name = "ACD_UPDATE_REFF";

        try
        {
            ret = objCommon.DynamicSPCall_IUD(p_name,p_para,p_call,true);
        }
        catch (Exception ex)
        { 
        }


        return JsonConvert.SerializeObject(ret);
    }

    
}