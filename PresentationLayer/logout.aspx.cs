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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class logout : System.Web.UI.Page
{
    Common objCommon = new Common();
    //public string sMarquee = string.Empty;

    ////ConnectionStrings
    //private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["username"] != null)
            {
                //Code for Logtable
                LogFile objLF = new LogFile();
                objLF.Ua_Name = Session["username"].ToString();
                objLF.LogoutTime = DateTime.Now;
                objLF.ID = Convert.ToInt32(Session["loginid"].ToString());
                LogTableController.UpdateLog(objLF);
                string colname = objCommon.LookUp("reff", "collegename", string.Empty);
                string coladdress = objCommon.LookUp("reff", "college_address", string.Empty);
                //string address = objCommon.LookUp("reff", "", string.Empty);
                if (objCommon.LookUp("REFF", "COLLEGE_LOGO", string.Empty) == null)
                    imgLogo.ImageUrl = "~/images/logo.gif";
                else
                    imgLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";

                if (string.IsNullOrEmpty(colname))
                    spnHead.InnerText = "IT THE MASTERS SOFTWARE NAGPUR";
                else
                {
                    Session["coll_name"] = colname;
                    spnHead.InnerText = colname;
                    SpnAddress.InnerText = coladdress;
                }
                //Set Session Timeout
                Session.Remove("error");
                Session.Remove("coll_name");
                Session.Remove("userno");
                Session.Remove("username");
                Session.Remove("usertype");
                Session.Remove("teacherno");

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
   
}
