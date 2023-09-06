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

public partial class PopUp : System.Web.UI.Page
{
    DataSet ds = null;
    Common objCommon = new Common();
protected void Page_Load(object sender, EventArgs e)
{
 
 if (!IsPostBack)
 {

//     string updateParentScript = @"function updateParentWindow()
//                              {                                                                               
//                               var fName='d';     
//                               var lName='d';   
//                               var arrayValues= new Array(fName,lName);
//                               window.opener.updateValues(arrayValues);       
//                               window.close(); 
//                              }";
//     this.ClientScript.RegisterStartupScript(this.GetType(), "UpdateParentWindow", updateParentScript, true);



   
     if (Request["fn"].ToString().Trim() == "LedgerHelp")
     {
         BindHelp("LedgerHelp");
     
     }

 }
   //Button1.Attributes.Add("onclick", "updateParentWindow()");
}
private void BindHelp(String HelpType)
{

   
    if (HelpType == "LedgerHelp")
    {
        if (txtSearch.Text.ToString().Trim() != "")
        {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0 and  PARTY_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%'", "PARTY_NO");// "PARTY_NAME"); 
        }
        else
        {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString() + "_" + Session["fin_yr"], "PARTY_NO", "UPPER(PARTY_NAME) AS PARTY_NAME", "PARTY_NO > 0 ", "PARTY_NO");// "PARTY_NAME");  
        
        }

        
     
    }

    if (ds != null)
    {
        lvGrp.DataSource = ds;
        lvGrp.DataBind();
        

    }
    else
    {
        lvGrp.DataSource = null ;
        lvGrp.DataBind();

    }




}


protected void txtSearch_TextChanged(object sender, EventArgs e)
{
    BindHelp("LedgerHelp");
}



//protected void lvGrp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
//{


//    HiddenField hdnid = lvGrp.Rows[e.NewSelectedIndex].FindControl("hdnPcd") as HiddenField;
//    LinkButton lnk = lvGrp.Rows[e.NewSelectedIndex].FindControl("lnkHead") as LinkButton;
//    if (hdnid != null && lnk != null)
//    {
//        Button1.Attributes.Add("onclick", "UpdateParentWindow('" + hdnid.Value + "','" + lnk.Text + "');");
//    }
//    else
//    {
//        Session["BookTran"] = null;

//    }

   

//}
protected void lvGrp_RowDataBound(object sender, GridViewRowEventArgs e)
{

    HiddenField hdnid = e.Row.FindControl("hdnPcd") as HiddenField;
    LinkButton lnk = e.Row.FindControl("lnkHead") as LinkButton;
    if (hdnid != null && lnk != null)
    {
        e.Row.Attributes.Add("onclick", "UpdateParentWindow('" + hdnid.Value + "','" + lnk.Text + "');");
    }
    else
    {
        Session["BookTran"] = null;

    }
}
}
