using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class TRAININGANDPLACEMENT_Reports_StudentList : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,TPController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objStud = new TPController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                // Check User Authority 
                this.CheckPageAuthorization(); 
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                
                //c
                Fill_Schedule();                
                Fill_ScheduleAll();
                //Final
                Fill_SeleComp();

                pnlBiodata.Visible = false;
                pnlschedule.Visible = false;
                pnlSelComp.Visible = false;
                pnlAll.Visible = false;
                //ddlSceduleAll.Visible = false;

            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
        }
    }
    protected void Fill_Schedule()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 8)
            {
                ds = objStud.FillSchedule_for_Rpt("C", Convert.ToInt32(Session["idno"]));
            }
            else
            {
                //ds = objStud.FillSchedule_for_Rpt("C", 0);
                ds = objStud.FillSchedule_for_Rpt("", 0);
            }

            ddlSchedule.Items.Clear();
            ddlSchedule.Items.Add("Please Select");
            ddlSchedule.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSchedule.DataSource = ds;
                ddlSchedule.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSchedule.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSchedule.DataBind();
                ddlSchedule.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.Fill_Schedule ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void Fill_SeleComp()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 8)
            {
                ds = objStud.FillSchedule_for_Rpt("F", Convert.ToInt32(Session["idno"]));
            }
            else
            {
                ds = objStud.FillSchedule_for_Rpt("F", 0);
            }

            ddlScheduleSelComp.Items.Clear();
            ddlScheduleSelComp.Items.Add("Please Select");
            ddlScheduleSelComp.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlScheduleSelComp.DataSource = ds;
                ddlScheduleSelComp.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlScheduleSelComp.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlScheduleSelComp.DataBind();
                ddlScheduleSelComp.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.Fill_SeleComp ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void Fill_ScheduleAll()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 8)
            {
                //ds = objStud.FillSchedule("A", Convert.ToInt32(Session["idno"]));
                ds = objStud.FillSchedule_for_Rpt("", Convert.ToInt32(Session["idno"]));//ALL
            }
            else
            {
                //ds = objStud.FillSchedule("A", 0);
                //ds = objStud.FillSchedule(0);
                ds = objStud.FillSchedule_for_Rpt("",0);//ALL
            }

            ddlSceduleAll.Items.Clear();
            ddlSceduleAll.Items.Add("Please Select");
            ddlSceduleAll.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSceduleAll.DataSource = ds;
                ddlSceduleAll.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlSceduleAll.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlSceduleAll.DataBind();
                ddlSceduleAll.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.Fill_ScheduleAll ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    //protected void radlTransfer_OnSelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (radlTransfer.SelectedValue == "I")
            {
                ShowReport("Biodata", "TPStudInterview.rpt");
            }
            else if (radlTransfer.SelectedValue == "C")
            {
                ShowReport("Biodata", "TPStudCompany.rpt");
            }
            else if (radlTransfer.SelectedValue == "B")
            {
                //ShowReport("Biodata", "TpBiodatas.rpt");
                if (radlStudentType.SelectedValue == "R")
                {
                    ShowReport("Biodata_R", "TPStudRegBiodata.rpt");
                }
                else if (radlStudentType.SelectedValue == "P")
                {
                    ShowReport("Biodata_P", "TPPassStudRegBiodata.rpt");
                }

            }
            else if (radlTransfer.SelectedValue == "S")
            {
                if (ddlSceduleAll.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage("Please Select Schedule", this.Page);
                    ddlSceduleAll.Focus();
                    return;
                }
                if (radlschedule.SelectedValue == "F1")
                {
                    ShowReport("Biodata", "TPSchedule.rpt");
                }
                else
                {
                    //ShowReport("Schedule2", "TPScheduleNotice.rpt");
                    ShowReport("Schedule2", "TPScheduleNoticeRaipur.rpt");
                }
            }
            else if (radlTransfer.SelectedValue == "A")
            {
                //if (ddlSceduleAll.SelectedIndex == 0)
                //{
                //    objCommon.DisplayMessage("Please Select Schedule", this.Page);
                //    ddlSceduleAll.Focus();
                //    return;
                //}
                ShowReport("Biodata", "TPStudInterview.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }  
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string[] separator = new string[] { "--" };
            string[] strSplitAry ;

            DataSet ds = new DataSet();
            if (radlTransfer.SelectedValue == "I")
            {
                //title = ddlSchedule.SelectedValue.Split("--");

                strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);

                //ds = objStud.GetStudentsForInterviewExcel(Convert.ToInt32(ddlSchedule.SelectedValue), 1);
                ds = GetStudentsForInterviewExcel(Convert.ToInt32(ddlSchedule.SelectedValue), 1);
                ds.Tables[0].Columns.Remove("IDNO"); 
                ds.Tables[0].Columns.Remove("RES_DECL_STATUS_CNT");
                ds.Tables[0].Columns.Remove("OBTD_MARKS");
                ds.Tables[0].Columns.Remove("OUTOFMARKS");
                ds.Tables[0].Columns.Remove("INTVSELECT");
                //ds.Tables[0].Columns.Remove("AGG_PER");
                //ds.Tables[0].Columns.Remove("AVRG_PER");
                //ds.Tables[0].Columns.Remove("SCHEDULENO");
                ShowReportExcel("xls", "SelectedStudListInInterviewFor_" + strSplitAry[0].Replace(" ", ""), ds);
            }
            else if (radlTransfer.SelectedValue == "C")
            {
                strSplitAry = ddlScheduleSelComp.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                
                ds = objStud.GetStudentListByComp(Convert.ToInt32(ddlScheduleSelComp.SelectedValue));
                ShowReportExcel("xls", "SelectedStudListInCompanyFor_" + strSplitAry[0].Replace(" ", ""), ds);
            }
            else if (radlTransfer.SelectedValue == "A")
            {
                strSplitAry = ddlSceduleAll.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);

                //ds = objStud.GetStudentsForInterview(Convert.ToInt32(ddlSchedule.SelectedValue), 0);
                //ds = objStud.GetStudentsForInterviewExcel(Convert.ToInt32(ddlSceduleAll.SelectedValue), 0);
                ds = GetStudentsForInterviewExcel(Convert.ToInt32(ddlSceduleAll.SelectedValue), 0);
                ds.Tables[0].Columns.Remove("IDNO");
                ds.Tables[0].Columns.Remove("RES_DECL_STATUS_CNT");
                ds.Tables[0].Columns.Remove("OBTD_MARKS");
                ds.Tables[0].Columns.Remove("OUTOFMARKS");
                ds.Tables[0].Columns.Remove("INTVSELECT");
                ShowReportExcel("xls", "AppliedStudListFor_" + strSplitAry[0].Replace(" ",""), ds);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.btnExcel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            if (radlTransfer.SelectedValue == "I")
            {
                string RptHeader = "SELECTED STUDENT LIST FOR INTERVIEW";
                url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSchedule.SelectedValue) + "," + "@P_INTVSELECT=1,@RptTitle=" + RptHeader.ToString() + ",username=" + Session["userfullname"].ToString() + ",@CompName=" + ddlSchedule.SelectedItem.Text.Trim();
            }
            else if (radlTransfer.SelectedValue == "C")
            {
                url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlScheduleSelComp.SelectedValue) + "," + "username=" + Session["userfullname"].ToString();
            }
            else if (radlTransfer.SelectedValue == "B")
            {
                //SqlDataReader dr = objStud.GetRegidBySchedule_Stype(Convert.ToInt16(ddlSchedule.SelectedValue),Convert.ToChar(radlStudentType.SelectedValue));
                SqlDataReader dr = objStud.GetRegidBySchedule_Stype(Convert.ToInt16(ddlSceduleAll.SelectedValue), Convert.ToChar(radlStudentType.SelectedValue));
                string ID = string.Empty;
                while (dr.Read())
                {
                    if (ID == string.Empty)
                    {
                        ID = dr["IDNO"].ToString();
                    }
                    else
                    {
                        ID = ID + "." + dr["IDNO"].ToString();
                    }
                }
                dr.Close();
               

                url += "&param=@P_IDNO=" + Convert.ToString(ID) ;
            }
            else if (radlTransfer.SelectedValue == "S")
            {
                url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSceduleAll.SelectedValue) + "," + "username=" + Session["userfullname"].ToString();
            }
            else if (radlTransfer.SelectedValue == "A")
            {
                string RptHeader = "APPLIED STUDENT LIST ";
                //url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSceduleAll.SelectedValue) + "," + "@P_INTVSELECT=0,@RptTitle=" + RptHeader.ToString() + ",username=" + Session["userfullname"].ToString();
                //url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSchedule .SelectedValue) + "," + "@P_INTVSELECT=0,@RptTitle=" + RptHeader.ToString() + ",username=" + Session["userfullname"].ToString();
                url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSceduleAll.SelectedValue) + "," + "@P_INTVSELECT=0,@RptTitle=" + RptHeader.ToString() + ",username=" + Session["userfullname"].ToString() + ",@CompName=" + ddlSceduleAll.SelectedItem.Text.Trim();
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReportExcel(string exporttype, string reportTitle,DataSet ds)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
                        
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                
                string attachment = "attachment; filename=" + reportTitle + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }
                                   
            ////objStud. GetStudentsForInterview(Convert.ToInt32 (ddl)
            //DataSet ds = new DataSet();
            //if (radlTransfer.SelectedValue == "I")
            //{
            //  ds= objStud.GetStudentsForInterview(Convert.ToInt32(ddlSchedule.SelectedValue), 0);
            //  grdData.DataSource = ds.Tables[0];
            //  grdData.DataBind();
            //}
            //Response.Clear();
            //Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //grdData.RenderControl(htmlWrite);
            ////GridView1.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //Response.End();
            
            ////string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            ////url += "Reports/CommonReport.aspx?";
            ////url += "exporttype=xls";
            ////url += "&filename=" + reportTitle + ".xls";
            ////url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            ////if (radlTransfer.SelectedValue == "I")
            ////{
            ////    string RptHeader = "SELECTED STUDENT LIST FOR INTERVIEW";
            ////    url += "&param=@P_SCHEDULENO=" + Convert.ToInt32(ddlSchedule.SelectedValue) + "," + "@P_INTVSELECT=1,@RptTitle=" + RptHeader.ToString() + ",username=" + Session["userfullname"].ToString();
            ////}

            ////divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            ////divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ////divMsg.InnerHtml += " window.close();";
            ////divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void radlTransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSelection.Visible = true;
        pnlBiodata.Visible = false;
        pnlschedule.Visible = false;        
        pnlSelComp.Visible = false;
        pnlAll.Visible = false;
        btnExcel.Enabled = false;

        if (radlTransfer.SelectedValue == "I")
        {
            btnExcel.Enabled = true;
        }
        else if (radlTransfer.SelectedValue == "C")
        {
            //ddlSceduleAll.Visible = false;
            //ddlSchedule.Visible = true;
            pnlSelComp.Visible = true;
            pnlSelection.Visible = false;
            btnExcel.Enabled = true;
        }
        else if (radlTransfer.SelectedValue == "B")
        {
            ////ddlSceduleAll.Visible = false;
            ////ddlSchedule.Visible = true;
            //pnlBiodata.Visible = true;
            ////pnlSelection.Visible = false;

            //ddlSceduleAll.Visible = false;
            //ddlSchedule.Visible = true;
            pnlBiodata.Visible = true;
            pnlAll.Visible = true;
            pnlSelection.Visible = false;
        }
        else if (radlTransfer.SelectedValue == "S")
        {
            pnlschedule.Visible = true;
            pnlAll.Visible = true;
            pnlSelection.Visible = false;
        }
        else if (radlTransfer.SelectedValue == "A")
        {
            //pnlschedule.Visible = true;
            pnlSelection.Visible = false;
            pnlAll.Visible = true ;
            pnlSelComp .Visible = false  ;
            btnExcel.Enabled = true;
        }
    }

    public DataSet GetStudentsForInterviewExcel(int Scheduleno, int INTVSELECT)
    {
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString; 
        DataSet ds = null;
        //try
        //{
        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        SqlParameter[] objParams = null;
        objParams = new SqlParameter[2];
        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
        objParams[1] = new SqlParameter("@P_INTVSELECT", INTVSELECT);
        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_REGISTERED_LIST_EXCEL", objParams);

        SqlConnection conn = new SqlConnection(_nitprm_constr);
        //conn.ConnectionTimeout = 1000;
        SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STUDENT_REGISTERED_LIST_EXCEL", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 2000;
        int i;
        for (i = 0; i < objParams.Length; i++)
            cmd.Parameters.Add(objParams[i]);
        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
        }

    //}
        catch (Exception ex)
        {
            return ds;
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentsForInterview-> " + ex.ToString());
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
}
