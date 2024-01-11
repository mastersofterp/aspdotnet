//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : TO CREATE NEW LINKS FOR PAGES                                   
// CREATION DATE : 13-April-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================


using System;
using System.IO;
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
using IITMS.UAIMS;

public partial class access_links : System.Web.UI.Page
{
    CustomStatus cs = new CustomStatus();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    public static int PARID = 0;

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
                CheckPageAuthorization();

                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //else
                //    lblHelp.Text = "No Help Added";
                //Bind the ListView with Access Links

                this.BindListView();
                PopulateDropDownList();

                if (Request.QueryString["action"] == null)
                {
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                }
                else
                {
                    if (Request.QueryString["action"].ToString().Equals("add"))
                    {   //add
                        pnlAdd.Visible = true;
                        pnlList.Visible = false;
                    }
                    else
                    {
                        if (Request.QueryString["al_no"] != null)
                        {   //edit
                            int al_no = Convert.ToInt32(Request.QueryString["al_no"].ToString());
                            ShowDetail(al_no);
                            pnlAdd.Visible = true;
                            pnlList.Visible = false;
                        }
                    }
                }
            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
        }
    }

    /// <summary>
    /// This method binds the Listview with the DataSource
    /// </summary>
    private void BindListView()
    {
        try
        {
            DataSet dsLink = objCommon.FillDropDown("ACC_SECTION, ACCESS_LINK", "ACCESS_LINK.*", "AS_TITLE", "AL_ASNO = AS_NO AND AL_ASNO =" + ddlModule.SelectedValue, "srno");

            if (dsLink.Tables[0].Rows.Count > 0)
            {
                lvALinks.DataSource = dsLink;
                lvALinks.DataBind();
                foreach (ListViewItem item in lvALinks.Items)
                {
                    Label lblactinestatus = item.FindControl("lblactinestatus") as Label;
                    if (lblactinestatus.Text == "1")
                    {
                        lblactinestatus.Text = "Active";
                        lblactinestatus.Style.Add("color", "Green");
                    }
                    else
                    {
                        lblactinestatus.Text = "DeActive";
                        lblactinestatus.Style.Add("color", "Red");
                    }
                }
            }

            else
            {
                lvALinks.DataSource = null;
                lvALinks.DataBind();
            }

        }
        //if (lblactivestatus.Text == "1")
        //{
        //    lblactivestatus.Text = "Active";
        //    lblactivestatus.Style.Add("color", "Green");
        //}
        //else
        //{
        //    lblactivestatus.Text = "De-Active";
        //    lblactivestatus.Style.Add("color", "Red");
        //}
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            decimal mastno = 0.0m;

            //Check whether to add or update
            if (Request.QueryString["action"] != null)
            {
               

                //Set all properties
                Access_LinkController objACLink = new Access_LinkController();
                Access_Link objAL = new Access_Link();

                if (ddlParentLink.Items.Count > 0)
                {
                    if (Convert.ToInt32(ddlParentLink.SelectedValue) > 0)
                    {
                        if (ddlParentLink.SelectedItem.Value != string.Empty)
                            mastno = Convert.ToDecimal(ddlParentLink.SelectedItem.Value);
                    }
                    else
                    {
                        mastno = Convert.ToDecimal(PARID);
                    }
                }
                else
                {
                    mastno = Convert.ToDecimal(PARID);
                }

                //Added By Rishabh on Dated 01/11/2021
                if (hfdActive.Value == "true")
                {
                    objAL.chklinkstatus = 1;
                }
                else
                {
                    objAL.chklinkstatus = 0;
                }

                if (hfdTrans.Value == "true")
                {
                    objAL.chkTrans = 1;
                }
                else
                {
                    objAL.chkTrans = 0;
                }
                //Added By Hemanth G
                //if(chknlstatus.Checked)
                //{
                //    objAL.chklinkstatus = 1;
                //}
                //else
                //{
                //    objAL.chklinkstatus = 0;
                //}
                //Added By Hemanth G
                objAL.Al_Link = txtLinkTitle.Text.Trim();
                objAL.Al_Url = txtLinkUrl.Text.Trim();
                objAL.SrNo = Convert.ToDecimal(txtSrNo.Text.Trim());
                objAL.Al_AsNo = int.Parse(ddlDomain.SelectedValue);
                objAL.MastNo = mastno;

                // Added By Anurag B. on 31-10-2023
                if (FilePDF.HasFile)
                {
                    objAL.Al_PdfName = FilePDF.FileName;
                    string directoryPath = Server.MapPath("~/UserManual");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    FilePDF.SaveAs(Server.MapPath("~") + "//UserManual//" + FilePDF.FileName);
                }
                // End Anurag B. on 31-10-2023

                

                int lev = Convert.ToInt32(ddllevel.SelectedValue);

                if (Convert.ToInt32(ddllevel.SelectedValue) == -1 && Convert.ToInt32(ddlParentLink.Items.Count) == 0)
                {
                    objAL.levelno = 1;
                }
                else if (Convert.ToInt32(ddllevel.SelectedValue) == 1 && Convert.ToInt32(ddlParentLink.SelectedValue) == 0)
                {
                    objAL.levelno = 1;
                }
                else if (lev == 4)
                {
                    objAL.levelno = 3;

                }
                else
                {
                    objAL.levelno = Convert.ToInt32(ddllevel.SelectedValue) + 1;

                }

                if (Request.QueryString["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objACLink.AddAccessLink(objAL);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        string alertMessage = "Link Created Successfully!";
                        string script = "<script type='text/javascript'>alert('" + alertMessage + "'); window.location='" + Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")) + "';</script>";   // Added By Shrikant W. on 21-11-2023
                        ClientScript.RegisterStartupScript(this.GetType(), "insertSuccess", script);
                        //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                    }
                    else
                        lblStatus.Text = "Error..";
                }
                else
                {
                    //Edit
                    if (Request.QueryString["al_no"] != null)
                    {
                        //Addtional property 
                        objAL.Al_No = Convert.ToInt32(Request.QueryString["al_no"].ToString());
                        

                        CustomStatus cs = (CustomStatus)objACLink.UpdateAccessLink(objAL);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            string alertMessage = "Link Updated Successfully!";
                            string script = "<script type='text/javascript'>alert('" + alertMessage + "'); window.location='" + Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")) + "';</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "updateSuccess", script);
                            //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                        }

                        else
                            lblStatus.Text = "Error..";
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
    }

    //protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        int al_no = int.Parse(btnDelete.CommandArgument);
    //        Access_LinkController objACLink = new Access_LinkController();
    //        CustomStatus cs = (CustomStatus)objACLink.DeleteAccessLink(al_no);
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //            Response.Redirect(Request.Url.ToString());

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "access_links.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int al_no = int.Parse(btnEdit.CommandArgument);

            string pageurl = Request.Url.ToString() + "&action=edit&al_no=" + al_no.ToString();
            Response.Redirect(pageurl);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int al_no)
    {
        try
        {
            Access_LinkController objACLink = new Access_LinkController();
            SqlDataReader dr = objACLink.GetSingleRecord(al_no);

            int LEVELNO = 0;
            int PARENTNO = 0;
            //Show Detail
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlDomain.Text = dr["al_asno"] == null ? "0" : dr["al_asno"].ToString();
                    if (dr["mastno"] == null)
                        Session["mastno"] = 0;
                    else
                        Session["mastno"] = Convert.ToInt32(dr["mastno"]);

                    //ddlParentLink.Text = dr["mastno"]; // This is set from the WebService.cs file
                    ddlDomain.SelectedValue = ddlDomain.Text;
                    LEVELNO = Convert.ToInt32(dr["LevelNo"] == null ? "0" : dr["LevelNo"].ToString());
                    PARENTNO = Convert.ToInt32(dr["MASTNO"] == null ? "0" : dr["MASTNO"].ToString());
                    ddllevel.SelectedValue = (LEVELNO - 1).ToString();
                    PARID = Convert.ToInt32(dr["mastno"]);
                    //Added By Hemanth G
                    //int stat = Convert.ToInt32(dr["Active_Status"]);
                    //if (stat == 1)
                    //{
                    //    chknlstatus.Checked = true;
                    //}
                    //else
                    //{
                    //    chknlstatus.Checked = false;
                    //}
                    //Added By Hemanth G
                    //Added By Rishabh on Dated 01/11/2021
                    if (dr["Active_Status"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }

                    if (dr["TRANCACTION"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatTrans(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatTrans(false);", true);
                    }
                    FILLLINKS();
                    string a =dr["AL_No"].ToString();
                    ddlParentLink.SelectedValue = PARID.ToString();

                    //GETSRNO(PARID);
                    txtLinkTitle.Text = dr["al_link"] == null ? "" : dr["al_link"].ToString();
                    txtLinkUrl.Text = dr["al_url"] == null ? "" : dr["al_url"].ToString();
                    txtSrNo.Text = dr["srno"] == null ? "" : dr["srno"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString() + "&action=add");
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //    BindListView();
    //}

    /// <summary>
    /// Populates the Domain DropDownList
    /// </summary>
    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_DROPDOWN_SP_ALL_DOMAIN");
            ddlDomain.DataSource = ds;
            ddlDomain.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDomain.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDomain.DataBind();

            ddlModule.DataSource = ddlDomain.DataSource = ds;
            ddlModule.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlModule.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlModule.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    protected void ddlDomain_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlParentLink.Items.Clear();
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and (AL_No=MastNo) ", "");

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbldomain.Text = ds.Tables[0].Rows[0]["srno"].ToString();
            }
        }

        //ddlParentLink.Items.Clear();
        //DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and (AL_No=MastNo) ", "");

        //if (ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lbllevel.Text = ds.Tables[0].Rows[0]["srno"].ToString();
        //    }
        //}

    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        chk_Edit.Checked = false;
        //  dpPager.Visible = true;
        lvALinks.Visible = true;
        BindListView_Edit();
    }

    protected void ddllevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsrno.Text = string.Empty;
        lbllevel.Text = string.Empty;
        ddlParentLink.Items.Clear();

        if (Convert.ToInt32(ddllevel.SelectedValue) != 3)
        {
            FILLLINKS();
        }
        else
        {
            ddlParentLink.Items.Clear();
        }

    }

    public void FILLLINKS()
    {
        if (Convert.ToInt32(ddllevel.SelectedValue) == 1)
        {
            int LEVEL = (Convert.ToInt32(ddllevel.SelectedValue) + 1);
            DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "AL_LINK", "AL_No", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and LevelNo='" + Convert.ToInt32(ddllevel.SelectedValue) + "' ", "");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                    ddlParentLink.DataSource = ds;
                    ddlParentLink.DataValueField = ds.Tables[0].Columns["AL_No"].ToString();
                    ddlParentLink.DataTextField = ds.Tables[0].Columns["AL_LINK"].ToString();
                    ddlParentLink.DataBind();
                    ddlParentLink.SelectedIndex = 0;
                }
                else
                {
                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                }
            }
        }
        else if (Convert.ToInt32(ddllevel.SelectedValue) == 2)
        {
            DataSet ds = objCommon.FillDropDown("ACCESS_LINK AL,(select AL_No,AL_Link from ACCESS_LINK )A", "a.AL_Link +'->' +AL.AL_Link as AL_LINK", "AL.AL_No", "AL.MastNo = A.AL_No and AL.AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and AL.LevelNo='" + Convert.ToInt32(ddllevel.SelectedValue) + "' ", "");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                    ddlParentLink.DataSource = ds;
                    ddlParentLink.DataValueField = ds.Tables[0].Columns["AL_No"].ToString();
                    ddlParentLink.DataTextField = ds.Tables[0].Columns["AL_LINK"].ToString();
                    ddlParentLink.DataBind();
                    ddlParentLink.SelectedIndex = 0;
                }
                else
                {
                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                }
            }
        }
        else if (Convert.ToInt32(ddllevel.SelectedValue) == 3)
        {
            DataSet ds = objCommon.FillDropDown("ACCESS_LINK AL,(select AL_No,AL_Link,MastNo from ACCESS_LINK )A,(select AL_No,AL_Link from ACCESS_LINK )B", "b.AL_Link +' -> '+a.AL_Link +' -> ' +AL.AL_Link as AL_LINK", "AL.AL_No", "AL.MastNo = A.AL_No and A.MastNo=b.AL_No and AL.AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and AL.LevelNo='" + Convert.ToInt32(ddllevel.SelectedValue) + "' and al.MastNo='" + PARID + "'", "");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                    ddlParentLink.DataSource = ds;
                    ddlParentLink.DataValueField = ds.Tables[0].Columns["AL_No"].ToString();
                    ddlParentLink.DataTextField = ds.Tables[0].Columns["AL_LINK"].ToString();
                    ddlParentLink.DataBind();
                    ddlParentLink.SelectedIndex = 0;
                }

                else
                {
                    ddlParentLink.Items.Clear();
                    ddlParentLink.Items.Add("Please Select");
                    ddlParentLink.SelectedItem.Value = "0";
                }

            }

        }

    }

    protected void ddlParentLink_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlParentLink.SelectedIndex == 0)
        //{
        //    lblsrno.Text = string.Empty;
        //}
        //else
        //{
        //    Access_LinkController objACLink = new Access_LinkController();
        //    SqlDataReader dr = objACLink.GetSingleRecord(Convert.ToInt32(ddlParentLink.SelectedValue));
        //    if (dr != null)
        //    {
        //        if (dr.Read())
        //        {
        //            GETSRNO(Convert.ToInt32(dr["AL_No"]));
        //        }
        //    }
        //}
        if (Convert.ToInt32(ddllevel.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlParentLink.SelectedValue) > 0)
            {
                int LEVEL = (Convert.ToInt32(ddllevel.SelectedValue));
                DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and MastNo='" + Convert.ToInt32(ddlParentLink.SelectedValue) + "' and LevelNo='" + LEVEL + "' and (AL_No=MastNo)", "");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbllevel.Text = ds.Tables[0].Rows[0]["srno"].ToString();
                    }
                }



                int LEVEL1 = (Convert.ToInt32(ddllevel.SelectedValue) + 1);
                DataSet ds2 = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and MastNo='" + Convert.ToInt32(ddlParentLink.SelectedValue) + "' and LevelNo='" + LEVEL1 + "' ", "");

                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        lblsrno.Text = ds2.Tables[0].Rows[0]["srno"].ToString();
                    }
                }


            }
        }

        if (Convert.ToInt32(ddllevel.SelectedValue) == 2)
        {
            if (Convert.ToInt32(ddlParentLink.SelectedValue) > 0)
            {

                int LEVEL = (Convert.ToInt32(ddllevel.SelectedValue));
                DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and al_no='" + Convert.ToInt32(ddlParentLink.SelectedValue) + "' and LevelNo='" + LEVEL + "' ", "");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lbllevel.Text = ds.Tables[0].Rows[0]["srno"].ToString();
                    }
                }

                int LEVEL1 = (Convert.ToInt32(ddllevel.SelectedValue));
                DataSet ds2 = objCommon.FillDropDown("ACCESS_LINK", "isnull(MAX(srno),0.0) srno", "isnull(MAX(srno),0.0) srno1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and MastNo='" + Convert.ToInt32(ddlParentLink.SelectedValue) + "' and LevelNo='" + LEVEL1 + "' ", "");

                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        lblsrno.Text = ds2.Tables[0].Rows[0]["srno"].ToString();
                    }
                }
            }
        }

    }

    public void GETSRNO(int ID)
    {
        lblsrno.Text = string.Empty;
        int LEVEL = (Convert.ToInt32(ddllevel.SelectedValue) + 1);
        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", " MAX(SrNo) sRNO2", "MAX(SrNo) SRNO1", "AL_ASNo='" + Convert.ToInt32(ddlDomain.SelectedValue) + "' and LevelNo='" + LEVEL + "' and MastNo='" + ID + "' ", "");

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SRNO1"].ToString() == "")
                {
                    lblsrno.Text = " First Element In This Category ";
                }
                else
                {
                    lblsrno.Text = "MAX SRNO  : " + ds.Tables[0].Rows[0]["SRNO1"].ToString();
                }
            }
            else
            {
                lblsrno.Text = "MAX SRNO  : 0.0 ";// +ds.Tables[0].Rows[0]["SRNO"].ToString();
            }
        }
    }

    protected void btn_UpdateAll_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            bool allUpdatesSuccessful = true;

            for (int i = 0; i < lvALinks_Edit.Items.Count; i++)
            {
                ListViewItem item = lvALinks_Edit.Items[i];


                Label txt_al_link_Edit = (Label)item.FindControl("txt_al_link_Edit");
                Label txt_al_url_Edit = (Label)item.FindControl("txt_al_url_Edit");
                Label txt_al_asno_Edit = (Label)item.FindControl("txt_al_asno_Edit");
                TextBox txt_Mastno_Edit = (TextBox)item.FindControl("txt_Mastno_Edit");
                TextBox txt_Srno_Edit = (TextBox)item.FindControl("txt_Srno_Edit");
                TextBox txt_Level_No_Edit = (TextBox)item.FindControl("txt_Level_No_Edit");
                Label lbl_al_no_Edit = (Label)item.FindControl("lbl_al_no_Edit");


                Access_LinkController objACLink = new Access_LinkController();
                Access_Link objAL = new Access_Link();

                objAL.Al_Link = txt_al_link_Edit.Text.Trim();
                objAL.Al_Url = txt_al_url_Edit.Text.Trim().Length > 0 ? txt_al_url_Edit.Text.Trim() : "";
                if (txt_al_asno_Edit.Text.Trim() != "")
                    objAL.Al_AsNo = Convert.ToInt32(txt_al_asno_Edit.Text.Trim());
                if (txt_Mastno_Edit.Text.Trim() != "")
                    objAL.MastNo = Convert.ToDecimal(txt_Mastno_Edit.Text.Trim());
                if (txt_Srno_Edit.Text.Trim() != "")
                    objAL.SrNo = Convert.ToDecimal(txt_Srno_Edit.Text.Trim());
                if (txt_Level_No_Edit.Text.Trim() != "")
                    objAL.levelno = Convert.ToInt32(txt_Level_No_Edit.Text.Trim());
                objAL.Al_No = Convert.ToInt32(lbl_al_no_Edit.Text.Trim());
               // //Added By Hemanth G
                CheckBox chk = (CheckBox)item.FindControl("cbRow");
                if (chk.Checked == true)
                {
                    objAL.chklinkstatus = 1;


                }
                else
                {
                    objAL.chklinkstatus = 0;
                }
                //Added By Hemanth G

                cs = (CustomStatus)objACLink.UpdateAccessLink(objAL);
            }

            if (allUpdatesSuccessful)
            {
                string url = Request.Url.ToString();
                int index = url.IndexOf("&action");

                if (index != -1)
                {
                    url = url.Remove(index);
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "updateAlert", "alert('Link(s) updated successfully'); window.location='" + url + "';", true);
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chk_Edit_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Edit.Checked)
        {
            pnlListEdit.Visible = true;
            Panel1.Visible = false;
            lvALinks.Visible = false;
            //dpPager.Visible = false;
            BindListView_Edit();
        }
        else
        {
            BindListView();
            pnlListEdit.Visible = false;
            //  dpPager.Visible = true;
            lvALinks.Visible = true;
            Panel1.Visible = true;
        }

    }

    private void BindListView_Edit()
    {
        try
        {
            DataSet dsLink = objCommon.FillDropDown("ACC_SECTION, ACCESS_LINK", "ACCESS_LINK.*", "AS_TITLE", "AL_ASNO = AS_NO AND AL_ASNO =" + ddlModule.SelectedValue, "srno");
            if (chk_Edit.Checked)
            {
                if (dsLink.Tables[0].Rows.Count > 0)
                {
                    lvALinks_Edit.DataSource = dsLink;
                    lvALinks_Edit.DataBind();

                    btn_UpdateAll_Edit.Visible = true;
                    lvALinks_Edit.Visible = true;
                    Panel2.Visible = true;
                }
                else
                {
                    lvALinks_Edit.Visible = false;
                    lvALinks_Edit.DataSource = null;
                    lvALinks_Edit.DataBind();
                    btn_UpdateAll_Edit.Visible = false;
                    Panel2.Visible = false;
                }
            }
            else
            {
                if (dsLink.Tables[0].Rows.Count > 0)
                {
                    //  dpPager.Visible = true;
                    Panel2.Visible = true;
                    lvALinks.Visible = true;
                    lvALinks_Edit.DataSource = null;
                    lvALinks_Edit.DataBind();
                    btn_UpdateAll_Edit.Visible = false;
                }
                else
                {
                    lvALinks_Edit.DataSource = null;
                    lvALinks_Edit.DataBind();
                    //  dpPager.Visible = false ;
                    lvALinks.Visible = false;
                    lvALinks_Edit.Visible = false;
                    Panel2.Visible = false;

                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "access_links.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //Added By Hemanth G
    protected void lvALinks_Edit_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chk = e.Item.FindControl("cbRow") as CheckBox;
        if (chk.ToolTip == "1")
        {
            chk.Checked = true;

        }
        else
        {
            chk.Checked = false;
        }

    }
    //Added By Hemanth G


    //protected void lvALinks_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    Label lblactivestatus=e.Item.FindControl("Active_Status") as Label;
    //    if (lblactivestatus.Text == "1")
    //    {
    //        lblactivestatus.Text = "Active";
    //        lblactivestatus.Style.Add("color", "Green");
    //    }
    //    else
    //    {
    //        lblactivestatus.Text = "De-Active";
    //        lblactivestatus.Style.Add("color", "Red");
    //    }
    //}
}