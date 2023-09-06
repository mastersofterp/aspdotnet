//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE LOGOUT                                                
// CREATION DATE : 14-April-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;
using IITMS.UAIMS;

public partial class logout : System.Web.UI.Page
{
    Common objCommon = new Common();
    //public string sMarquee = string.Empty;
    int marks=0;
    ////ConnectionStrings
    //private string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["username"] != null)
            {
                //Code for Logtable
                IITMS.UAIMS.BusinessLayer.BusinessEntities.LogFile objLF = new IITMS.UAIMS.BusinessLayer.BusinessEntities.LogFile();
                objLF.Ua_Name = Session["username"].ToString();
                objLF.LogoutTime = DateTime.Now;

                IITMS.UAIMS.BusinessLayer.BusinessLogic.LogTableController.UpdateLog(objLF);
                //Set Session Timeout
                Session.Remove("error");
                Session.Remove("coll_name");
                Session.Remove("userno");
                Session.Remove("username");
                Session.Remove("usertype");
                Session.Remove("teacherno");
                Check();
                Session.RemoveAll();
                Session.Abandon();

                try
                {
                    //Get Common Details
                    SqlDataReader dr = objCommon.GetCommonDetails();
                    if (dr != null)
                        if (dr.Read())
                            Page.Title = dr["CollegeName"].ToString();

                    dr.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
    private void Check()
    {
        try
        {
           
            DataTable dt = (DataTable)Session["Answered"];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SELECTED"].ToString().Equals(dr["CORRECTANS"]))
                {

                    marks = marks + Convert.ToInt32(dr["CORRECT_MARKS"].ToString());
                    Session["marks"] = Convert.ToInt32(marks);
                }

            }


        }
        catch (Exception)
        {

            throw;
        }
    }
}
