//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : FINAL DETENTION
// CREATION DATE : 21/01/2021                                              
// CREATED BY    : SAFAL GUPTA
//MODIFIED BY    :                                          
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_FinalDetention : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegist objSR = new StudentRegist();
    StudentRegistration objSReg = new StudentRegistration();
    string finaldet = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));


                    //Student
                    if (Session["usertype"].ToString().Equals("2"))
                    {
                        btnShow.Visible = false;
                    }
                    else if (Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("4"))
                    {
                        btnShow.Visible = true;
                    }
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_DETENTION_INFO WHERE ISNULL(PROV_DETAIN,0)=1 AND ISNULL(CANCEL_DETAIN,0)=0 AND ORGANIZATIONID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + ")", "COLLEGE_ID");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_DETENTION_INFO D ON (SM.SESSIONNO=D.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_NAME", "SM.SESSIONNO > 0", "SM.SESSIONNO DESC");

                //ddlSession.Focus();
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FinalDetaintion.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FinalDetaintion.aspx");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        lvDetend.Visible = false;
        // lblshow.Visible = false;
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_DETENTION_INFO D ON(S.SESSIONNO=D.SESSIONNO AND S.ORGANIZATIONID=D.ORGANIZATIONID)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "S.SESSIONNO > 0 AND ISNULL(PROV_DETAIN,0)=1 AND ISNULL(CANCEL_DETAIN,0)=0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "S.SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }

    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_DETENTION_INFO D INNER JOIN ACD_COURSE C ON C.COURSENO=D.COURSENO", "DISTINCT C.COURSENO", "(C.CCODE +'-'+C.COURSE_NAME)COURSENAME", "D.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND D.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND D.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND D.PROV_DETAIN=1", "C.COURSENO");
            ddlSem.Focus();
        }
        else
        {
        }
        btnSubmit.Visible = false;
        //btnCancel.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        //lblshow.Visible = false;
        lvDetend.Visible = false;
    }
    private void Clearall()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowDetendInfo();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string final_detend = string.Empty;
        int count = 0;
        foreach (ListViewDataItem dataitem in lvDetend.Items)
        {
            CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
            if (chk.Checked == true && chk.Enabled == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Check at least one Student for Final Detention", this.Page);
            return;
        }

        try
        {
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.DEGREENO = Convert.ToInt32(ViewState["degreeno"]);
            objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
            objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            objSR.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            foreach (ListViewDataItem item in lvDetend.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                //Label lblcourse = item.FindControl("lblCourse") as Label;

                if (chk.Checked && chk.Enabled)
                {
                    objSR.FINAL_DETEND += "1,";
                    objSR.IDNOS += chk.ToolTip + ",";
                    //  count++;
                }
                else
                    objSR.FINAL_DETEND += "0,";
            }

            if (rblSelection.SelectedValue == "2")
            {
                if (objSReg.UpdateFinalDetend(objSR) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Final Detention Entry Done Successfully. ", this);
                    ShowDetendInfo();
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Server Error", this.Page);
                }
            }
            else
            {
                if (objSReg.UpdateCourseFinalDetend(objSR) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Final Detention Entry Done Successfully. ", this);
                    ShowDetendInfo();
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Server Error", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lvDetend_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkRow = e.Item.FindControl("chkAccept") as CheckBox;
        Label prov = e.Item.FindControl("lblProv") as Label;
        Label provstatus = e.Item.FindControl("lblProv") as Label;

        Label final = e.Item.FindControl("lblFinal") as Label;
        if (prov.ToolTip == "1")
        {

            chkRow.Enabled = false;
            chkRow.BackColor = System.Drawing.Color.Green;
            chkRow.Checked = false;
        }
        else
        {
        }

        if (final.ToolTip == "1")
        {
            final.Text = "YES";
            final.Style.Add("color", "Green");
        }
        else
        {

            final.Text = "NO";
            final.Style.Add("color", "Red");
        }
        if (provstatus.Text == "YES")
        {
            provstatus.Text = "YES";
            provstatus.Style.Add("color", "Green");
        }
        else
        {
            provstatus.Text = "NO";
            provstatus.Style.Add("color", "Red");
        }

    }

    private void ShowDetendInfo()
    {
        try
        {
            DataSet ds = null;
            if (rblSelection.SelectedValue != "1" && rblSelection.SelectedValue != "2")
            {
                objCommon.DisplayMessage(UpdatePanel1, "Please Select any one Detention Type.", this);
                return;
            }
            if (rblSelection.SelectedValue == "2")
            {
                ds = objSReg.GetProvDetendInfo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), 2);
            }
            else
            {
                if (ddlCourse.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please Select Course.", this);
                    return;
                }
                ds = objSReg.GetProvDetendInfo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), 1);
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvDetend.DataSource = ds;
                    lvDetend.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDetend);//Set label - 
                    lvDetend.Visible = true;
                    int i, count = 0;
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["FINAL_DETAIN"].ToString() == "1")
                        {
                            count++;
                        }
                    }
                    txtAllSubjects.Text = count.ToString();
                    //lblshow.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    lvDetend.DataSource = null;
                    lvDetend.DataBind();
                    lvDetend.Visible = false;
                    // lblshow.Visible = false;
                    objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
                lvDetend.DataSource = null;
                lvDetend.DataBind();
                lvDetend.Visible = false;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_DETENTION_INFO CT ON S.SEMESTERNO=CT.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND PROV_DETAIN=1 AND ISNULL(CANCEL_DETAIN,0)=0" + "", "S.SEMESTERNO");
        ddlSem.Focus();
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        //lblshow.Visible = false;
        lvDetend.Visible = false;
    }

    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        // lblshow.Visible = false;
        lvDetend.Visible = false;
        ddlCourse.SelectedIndex = 0;

        if (rblSelection.SelectedValue == "1")
        {
            divCourse.Visible = true;
            ddlCourse.Focus();
        }
        else
        {
            divCourse.Visible = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        // DataSet ds = objSReg.Get_Detent_StudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
        // CODE FOR GENRATE REPORT FOR CANCEL DETENTION STUDENTS 
        DataSet ds = objSReg.Get_Final_Detain_StudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;

            GV.DataSource = ds;
            GV.DataBind();
            string attachment = "attachment; filename=" + "Final Detaintion Report" + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(UpdatePanel1, "Record Not Found.", this);
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        lvDetend.Visible = false;
    }
}
