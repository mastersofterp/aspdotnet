//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BATCH MASTER                                                         
// CREATION DATE : 02-SEPT-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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


public partial class Academic_Masters_CityMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //Page Authorization
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
            objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "DISTRICTNO >0", "DISTRICTNO");   //Added By Sachin on 06-07-2022        
            BindListView();
            ViewState["action"] = "add";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BatchMaster.aspx");
        }
    }
    #endregion

    #region Form Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ActiveStatus = 0;
        try
        {
            Common objBC = new Common();
            int StateNo = Convert.ToInt32(ddlState.SelectedValue);
            int DISTRICTNO = Convert.ToInt32(ddlDistrict.SelectedValue);
            string CityName = txtCity.Text.Trim();
            string CollegeCode = Session["colcode"].ToString();
            if (hfdStat.Value == "true") // Added By Rishabh on 24/01/2022
            {
                ActiveStatus = 1;
            }
            else
            {
                ActiveStatus = 0;
            }


            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    string CityNo = objCommon.LookUp("ACD_CITY", "CITYNO", "CITY='" + txtCity.Text.Trim() + "' and STATENO=" + ddlState.SelectedValue + "");
                    //if (CityNo != null && CityNo != string.Empty)   
                    //{
                    //    objCommon.DisplayMessage(this.updBatch, "Record Already Exist", this.Page);
                    //    return;
                    //}

                    //Add Batch
                    CustomStatus cs = (CustomStatus)objBC.AddCity(CityName, StateNo, CollegeCode, ActiveStatus, DISTRICTNO);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                        //Label1.Text = "Record already exist";
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["cityno"] != null)
                    {
                        int CityNo = Convert.ToInt32(ViewState["cityno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdateCity(CityName, StateNo, DISTRICTNO, CollegeCode, ActiveStatus, CityNo);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updBatch, "Record already exist", this.Page);
                            //Label1.Text = "Record already exist";
                        }
                    }
                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CityNo = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

            ShowDetail(CityNo);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Private Methods

    private void ShowDetail(int CityNo)
    {
        Common objBatch = new Common();
        SqlDataReader dr = objBatch.GetBatchByNo(CityNo);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["cityno"] = CityNo.ToString();
                int DISTRICTNO = Convert.ToInt32(ddlDistrict.SelectedValue);

                //ddlState.Text = dr["STATENO"] == null ? string.Empty : dr["STATENO"].ToString();

                ddlState.SelectedValue = dr["STATENO"] == null ? string.Empty : dr["STATENO"].ToString();
                txtCity.Text = dr["CITY"] == null ? string.Empty : dr["CITY"].ToString();


                objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "ISNULL(ACTIVESTATUS,0)=1", "DISTRICTNO"); //Added By Sachin on 07-07-2022
                if (dr["DISTRICTNO"].ToString().Equals("0"))
                {

                }
                else
                {
                    ddlDistrict.SelectedValue = dr["DISTRICTNO"] == null ? string.Empty : dr["DISTRICTNO"].ToString();
                }


                if (dr["ACTIVESTATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlState.SelectedIndex = 0;
        txtCity.Text = string.Empty;
        //Label1.Text = string.Empty;
        ddlDistrict.SelectedIndex = 0;
    }

    private void BindListView()
    {
        try
        {
            Common objBC = new Common();
            DataSet ds = objBC.GetAllCity();
            lvBatchName.DataSource = ds;
            lvBatchName.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_BatchMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    //protected void btnShowReport_Click(object sender, EventArgs e)
    //{
    //    ShowReport("BatchMaster", "rptBatchMaster.rpt");
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        //try
        //{
        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)    //Added By Sachin on 06-07-2022  
    {
        try
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Add(new ListItem("Please Select", "0"));
            if (ddlState.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTRICTNO", "DISTRICTNAME", "ISNULL(ACTIVESTATUS,0)=1", "DISTRICTNO");

                //objCommon.FillDropDownList(ddlDistrict, "ACD_DISTRICT", "DISTINCT DISTRICTNO", "DISTRICTNAME", "DISTRICTNO =" + ddlDistrict.SelectedValue +" AND STATENO =" + ddlState.SelectedValue,"DISTRICTNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CityMaster.ddlState_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
