//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD THESIS ENTRY                                                     
// CREATION DATE : 24-FEB-2023                                                          
// CREATED BY    : NEHAL                                                                     
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

public partial class ACADEMIC_PHD_Phd_Thesis_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();

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
                //if (Session["usertype"] != "2")
                //{
                CheckPageAuthorization();
                if (Session["usertype"].ToString() == "2")
                {
                    //objCommon.FillDropDownList(ddlAcdYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Entry.aspx");
                }
                //}

                //Set the Page Title
                //Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                //Session["idno"] = "9000";
                BindListView();
                string data = objCommon.LookUp("ACD_PHD_THESIS_ENTRY", "IDNO", "IDNO =" + (Session["idno"].ToString()));
                if (data != string.Empty)
                {
                    txtTitleThesis.Enabled = false;
                    txtThesisdate.Enabled = false;
                    txtSynopsisDate.Enabled = false;
                    btnSubmit.Enabled = false;
                }
                else
                {
                    txtTitleThesis.Enabled = true;
                    txtThesisdate.Enabled = true;
                    txtSynopsisDate.Enabled = true;
                    btnSubmit.Enabled = true;
                }
                MpHistory.Hide();
                updMaindiv.Visible = false;
            }
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Entry.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objPhd.GetAllStudentThesisList(Convert.ToInt32(Session["idno"].ToString()));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                
                //lvTracking.DataSource = ds;
                //lvTracking.DataBind();

                DataTable dt = new DataTable();
                dt.Columns.Add("Thesis Title");
                dt.Columns.Add("Thesis Submission Date");
                dt.Columns.Add("Synopsis Submission Date");

                int i = ds.Tables[1].Rows.Count;
                int j = 0;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    while (j < i)
                    {
                        dt.Columns.Add((ds.Tables[1].Rows[j]["STATUSNAME"].ToString()), typeof(string));
                        dt.Columns[ds.Tables[1].Rows[j]["STATUSNAME"].ToString()].DefaultValue = "Pending";
                        j++;
                    }
                }


                DataRow dr = dt.NewRow();
                dr["Thesis Title"] = ds.Tables[0].Rows[0]["THESIS_TITLE"].ToString();
                dr["Thesis Submission Date"] = ds.Tables[0].Rows[0]["THESIS_SUBMIT_DATE"].ToString();
                dr["Synopsis Submission Date"] = ds.Tables[0].Rows[0]["SYNOPSIS_SUBMIT_DATE"].ToString();
                dt.Rows.Add(dr);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    int p = ds.Tables[2].Rows.Count;
                    int q = 0;
                   
                    string statusdata = "Pending";
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        while (q < p)
                        {
                            statusdata = ds.Tables[2].Rows[q]["STATUSNAME"].ToString();
                            q++;
                            int r = ds.Tables[1].Rows.Count;
                            int s = 0;
                            while (s < r)
                            {

                                string verifystatus = ds.Tables[1].Rows[s]["STATUSNAME"].ToString();

                                if (verifystatus == statusdata)
                                {
                                    dr[verifystatus] = "Approved";
                                }
                                s++;
                            }
                        }
                    }
                }
                divTracking.Visible = true;
                grvStatusDetails.DataSource = dt;
                grvStatusDetails.DataBind();
            }
            else
            {
                divTracking.Visible = false;
                grvStatusDetails.DataSource = null;
                grvStatusDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Response.Redirect(Request.Url.ToString());
    }

    private void ClearControls()
    {
        txtThesisdate.Text = string.Empty;
        txtSynopsisDate.Text = string.Empty;
        txtTitleThesis.Text = string.Empty;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = Session["idno"].ToString();
            string data = objCommon.LookUp("ACD_PHD_THESIS_ENTRY", "IDNO", "IDNO =" + idno);
            if (data != string.Empty)
            {
                txtTitleThesis.Enabled = false;
                txtThesisdate.Enabled = false;
                txtSynopsisDate.Enabled = false;
                objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
            }
            else
            {
                if (txtThesisdate.Text == string.Empty || txtTitleThesis.Text == string.Empty || txtSynopsisDate.Text == string.Empty)
                {
                    if (txtThesisdate.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.updSession, "Please Enter Thesis Submission Date", this.Page);
                    }
                    else if (txtTitleThesis.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.updSession, "Please Enter Thesis Title", this.Page);
                    }
                    else if (txtSynopsisDate.Text == string.Empty)
                    {
                        objCommon.DisplayMessage(this.updSession, "Please Enter Synopsis Submission Date", this.Page);
                    }
                }
                else
                {
                    string thesis_title = txtTitleThesis.Text.Trim();
                    DateTime thesis_subdate = Convert.ToDateTime(txtThesisdate.Text);
                    DateTime synopis_subdate = Convert.ToDateTime(txtSynopsisDate.Text);

                    CustomStatus cs = (CustomStatus)objPhd.InsertPhdThesisEntry(idno, thesis_title, thesis_subdate, synopis_subdate);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControls();
                        BindListView();
                        txtTitleThesis.Enabled = false;
                        txtThesisdate.Enabled = false;
                        txtSynopsisDate.Enabled = false;
                        objCommon.DisplayMessage(this.updSession, "Thesis Entry Submitted Successfully", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        ClearControls();
                        objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        ClearControls();
                        objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvTracking_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            DataSet ds = objPhd.GetAllStudentThesisList(Convert.ToInt32(Session["idno"].ToString()));
            lv.DataSource = ds;
            lv.DataBind();
        }
        catch { }
    }
    protected void grvStatusDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet ds = objPhd.GetAllStudentThesisList(Convert.ToInt32(Session["idno"].ToString()));
        //lv.DataSource = ds;
        //lv.DataBind();

        try
        {
            updMaindiv.Visible = true;
            updMaindiv.Visible = true;
            MpHistory.Show();
            ImageButton idno = sender as ImageButton;
            DataSet ds1 = new DataSet();
            ds1 = objPhd.GetAllStudStatusDetails(Convert.ToInt32(Session["idno"].ToString()));
            if (ds1.Tables[7].Rows.Count > 0)
            {
                lvstustslist.DataSource = ds1.Tables[7];
                lvstustslist.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Approval pending by faculty", this.Page);
                updMaindiv.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Phd_Thesis_Entry.btnEdit_Click" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}