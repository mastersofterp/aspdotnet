using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;



public partial class ACADEMIC_StudentAchievement_ClubRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ClubController OBJCLUB = new ClubController();
    string PageId;
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

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    pnlMain.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                    pnlMain.Visible = false;
                    //div3.Visible = false;
                    //lvPraticipation.Visible = false;

                    return;

                    Page.Title = Session["coll_name"].ToString();

                    PageId = Request.QueryString["pageno"];

                }
                objCommon.FillListBox(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
                ViewState["action"] = "add";
            }
            BindListView();
        }



        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

    }
    public void Clear()
    {

        ddlclub.ClearSelection();


    }


    private void BindListView()
    {
        int nextRow = 0, currentRow = 0;
        string club = string.Empty;
        int idno = Convert.ToInt32(Session["idno"]);
        DataSet ds = OBJCLUB.GetShowClubRegistration(idno);

        //  DataTable dt = ds.Tables[0];

        //  nextRow = 0;
        //for (int i = 0; i < dt.Rows.Count ; i++)
        //{

        //string j = dt.Rows[i]["CLUB_ACTIVITY_NO"].ToString();

        //for (int k = 0; i < ddlclub.Items.Count; i++)
        //{
        //    if (j.Trim() == ddlclub.Items[k].Value.Trim())
        //    {
        //       ddlclub.Items[k].Selected = true;
        //    }
        //}
        string k = string.Empty;
        objCommon.FillListBox(ddlclub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO>0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO");
        DataTable dt = ds.Tables[0];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            k = dt.Rows[j]["CLUB_ACTIVITY_NO"].ToString();
            for (int i = 0; i < ddlclub.Items.Count; i++)
            {

                if (k == ddlclub.Items[i].Value.Trim())
                {
                    ddlclub.Items[i].Selected = true;
                }
            }
        }


        //  }
        //    currentRow++;

        //if (ds != null)

        //{

        //    char Clubs = ',';

        //    string clubb = ds.Tables[0].Rows[0]["CLUB_ACTIVITY_NO"].ToString();

        //    string[] stu = clubb.Split(Clubs);
        //    for (int j = 0; j < stu.Length; j++)
        //    {
        //        for (int i = 0; i < ddlclub.Items.Count; i++)
        //        {
        //            if (stu[j].Trim() == ddlclub.Items[i].Value.Trim())
        //            {

        //                ddlclub.Items[i].Selected = true;
        //            }
        //        }
        //    }
        //}

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int idno = Convert.ToInt32(Session["idno"]);
        int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_CLUB_REGISTRATION", "Count(CLUB_REGISTRATION_NO)", "idno=" + Convert.ToInt32(Session["idno"])));
        if (COUNT >= 0 && COUNT <= 3)
        {
            string club = string.Empty;
            foreach (ListItem items in ddlclub.Items)
            {
                if (items.Selected == true)
                {

                    club += items.Value + ',';


                }
            }
            if (!club.Equals(string.Empty))
            {
                club = club.Substring(0, club.Length - 1);
            }
            CustomStatus cs = (CustomStatus)OBJCLUB.AddClubRegistrationDetails(idno, club);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Clear();
                objCommon.DisplayMessage(this.updclub, "Record Saved Successfully!", this.Page);
            }
            else
            {

                Clear();
                objCommon.DisplayMessage(this.updclub, "Record Already Exist !", this.Page);
            }

        }
        else
        {
            objCommon.DisplayMessage(this.Page, "student allowed registration maximum three club only. ", this.Page);
            return;
        }


        BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

}