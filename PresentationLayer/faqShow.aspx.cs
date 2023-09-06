//=================================================================================
// PROJECT NAME  : UIMAS                                                           
// MODULE NAME   : TO CREATE FAQ SHOW                                              
// CREATION DATE : 
// CREATED BY    : NIRAJ D. PHALKE                                                 
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

public partial class faqShow : System.Web.UI.Page
{
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if ( Request.QueryString["pageno"] != null )
                {
                    //lblHelp.Text = objCommon.GetPageHelp( int.Parse( Request.QueryString["pageno"].ToString() ) );
                }

                BindFAQList();

                pnlQuestion.Visible = false;
                pnlList.Visible = true;
                pnlShow.Visible = false;
            }
        }
    }

    private void BindFAQList()
    {
        try
        {
            FaqController objFc = new FaqController();
            //News Stored Procedure
            DataSet dsFaq = objFc.GetAllFaq();
            if (dsFaq.Tables[0].Rows.Count > 0)
            {
                lvList.DataSource = dsFaq;
                lvList.DataBind();
                dsFaq.Dispose();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FaqController objFc = new FaqController();
            Faq objFaq = new Faq();
            objFaq.PARENTID = 0;
            objFaq.IDNO = Convert.ToInt32(Session["userno"]);
            objFaq.TITLE = txtQuestion.Text.Trim();
            
            //objFaq.FDATE = DateTime.Now.ToString("dd-MMM-yyyy");
            objFaq.FNAME = Session["userfullname"].ToString();
            objFaq.DEL = 0;
            objFaq.STATUS = "QUE";
            
            int qid = 0;
            if (ViewState["Qid"] != null)
            {
                qid = Convert.ToInt32(ViewState["Qid"]);
            }
            //CustomStatus cs = (CustomStatus)objFc.AddFAQ(qid,txtQuestion.Text,txtAnswer.Text);
            int ret = objFc.AddFAQ(qid, txtQuestion.Text, txtAnswer.Text, Convert.ToInt32(Session["OrgId"]));
            //if (cs.Equals(CustomStatus.RecordSaved))
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.updFAQ, "FAQ's Saved Successfully.", this.Page);
                BindFAQList();
                //Response.Redirect(Request.Url.ToString());

                txtAnswer.Text = string.Empty;
                txtQuestion.Text = string.Empty;
                pnlQuestion.Visible = false;
                pnlList.Visible = true;
            }
            else if (ret == 2)
            {
                objCommon.DisplayUserMessage(this.updFAQ, "FAQ's Updated Successfully.", this.Page);
                BindFAQList();
                txtAnswer.Text = string.Empty;
                txtQuestion.Text = string.Empty;
                pnlQuestion.Visible = false;
                pnlList.Visible = true;
                //Response.Redirect(Request.Url.ToString());
            }
                //Response.Redirect(Request.Url.ToString());

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int fid = int.Parse(btnDel.CommandArgument);

            FaqController objFc = new FaqController();

            CustomStatus cs = (CustomStatus)objFc.Delete(fid);
            if (cs.Equals(CustomStatus.RecordDeleted))
                Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSelect_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnSelect = sender as ImageButton;
            int qid = int.Parse(btnSelect.CommandArgument);
            ViewState["Qid"] = qid;

            FaqController objFc = new FaqController();
            SqlDataReader dr = objFc.GetSingleFaq(qid);

            //Show Faq Detail
            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["Qid"] = int.Parse(dr["QID"].ToString());
                    //lblQuestion.Text = dr["QUESTION"].ToString();
                    txtQuestion.Text = dr["QUESTION"].ToString();
                    txtAnswer.Text = dr["ANSWER"].ToString();
                }
            }
            if (dr != null) dr.Close();
            
            lblUserName.Text = Session["userfullname"].ToString();
            
            pnlQuestion.Visible = true;
            pnlShow.Visible = false;
            pnlList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.btnSelect_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSaveAnswer_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["fid"] != null)
            {
                int fid = int.Parse(ViewState["fid"].ToString());

                FaqController objFc = new FaqController();
                Faq objFaq = new Faq();

                objFaq.PARENTID = fid;
                objFaq.IDNO = Convert.ToInt32(Session["userno"]);
                objFaq.TITLE = txtAnswer.Text.Trim();
                //objFaq.FDATE = DateTime.Now.ToString("dd-MMM-yyyy");
                objFaq.FNAME = Session["userfullname"].ToString();
                objFaq.DEL = 0;
                objFaq.STATUS = "ANS";

                CustomStatus cs = (CustomStatus)objFc.AddAns(objFaq);
                if (cs.Equals(CustomStatus.RecordSaved))
                    Response.Redirect(Request.Url.ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.btnSaveAnswer_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlQuestion.Visible = true;
        pnlShow.Visible = false;
        pnlList.Visible = false;
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ImageButton btn = dataitem.FindControl("btnSelect") as ImageButton;
        int fid = Convert.ToInt32(btn.CommandArgument);
        
        ListView lv = dataitem.FindControl("lvAnswers") as ListView;
                
        try
        {
            FaqController objFC = new FaqController();
            DataSet ds = objFC.GetAllAnsFaq(fid);
            lv.DataSource = ds;
            lv.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "faqShow.lvList_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlShow.Visible = false;
        pnlList.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlQuestion.Visible = false;
        pnlList.Visible = true;
    }
}