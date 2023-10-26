
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ClubActivityReport                                 
// CREATION DATE : 25-10-2023                                                      
// CREATED BY    : Vipul Tichakule                                                        
// MODIFIED DATE : 
// MODIFIED BY                                                     
// MODIFIED DESC :                                                                  
//======================================================================================
using BusinessLogicLayer.BusinessLogic.Academic;
using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ClubActivityReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubActivityR objclub = new ClubActivityR();

    #region pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                if (Session["usertype"].ToString() == "1" )
                {
                    FillDropDownList();
                }
                else
                {
                    if (Session["usertype"].ToString() == "2")
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ClubActivityReport.aspx");
                    }
                    else
                    {
                        int inchargeno = 0;
                        int uano = Convert.ToInt32(Session["userno"].ToString());
                        inchargeno = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_MASTER", "count(INCHARGE_NO)", "INCHARGE_NO=" + uano +""));
                        if (inchargeno > 0)
                        {
                            FillDropDownList();
                        }
                        else
                        {
                            Response.Redirect("~/notauthorized.aspx?page=ClubActivityReport.aspx");
                        }

                    }
                   
                }
          
            }
            
        }
    }
    #endregion

    #region Filldropdownlist
    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "[dbo].[ACD_SESSION]", "SESSIONID", "SESSION_NAME", "isnull(FLOCK,0)=1", "SESSION_NAME");
    }
    #endregion 

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ClubActivityReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ClubActivityReport.aspx");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillListBox(lstbxClub, "ACD_CLUB_MASTER AM INNER JOIN ACD_CLUB_ACTIVITY_REGISTRATION CR ON CR.CLUBACTIVITY_TYPE = AM.CLUB_ACTIVITY_NO", "DISTINCT CR.CLUBACTIVITY_TYPE", "AM.CLUB_ACTIVITY_TYPE", "SESSIONNO IN (select SESSIONNO from acd_session_MASTER WHERE ISNULL(FLOCK,0)=1 AND SESSIONID=" + ddlSession.SelectedValue + " )", "CLUB_ACTIVITY_TYPE");

        }
        else
        {
            objCommon.FillListBox(lstbxClub, "ACD_CLUB_MASTER AM INNER JOIN ACD_CLUB_ACTIVITY_REGISTRATION CR ON CR.CLUBACTIVITY_TYPE = AM.CLUB_ACTIVITY_NO", "DISTINCT CR.CLUBACTIVITY_TYPE", "AM.CLUB_ACTIVITY_TYPE", "SESSIONNO IN (select SESSIONNO from acd_session_MASTER WHERE ISNULL(FLOCK,0)=1 AND SESSIONID=" + ddlSession.SelectedValue + ")AND INCHARGE_NO=" + Convert.ToInt32(Session["userno"].ToString()) + "", "CLUB_ACTIVITY_TYPE");
        }
    }

    #region ExcelReport
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ClubActivityExcelReport();
    }

    private void ClubActivityExcelReport()
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        string clubno = string.Empty;
        foreach (ListItem listItem in lstbxClub.Items)
        {
            if (listItem.Selected == true)
            {
                clubno += listItem.Value + ",";
            }
        }
        clubno = clubno.TrimEnd(',');

        DataSet dsclubnew = objclub.GetClubActivityReport(clubno, sessionno);
        if ( dsclubnew != null && dsclubnew.Tables[0].Rows.Count > 0 )
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsclubnew.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ClubActivityReport.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
             ClearControls();
        }
        else
        {
            objCommon.DisplayUserMessage(updFeed, "No Record Found", this.Page);
            return;
        }

    }
    #endregion

    protected void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        lstbxClub.Items.Clear();
    }

    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
}