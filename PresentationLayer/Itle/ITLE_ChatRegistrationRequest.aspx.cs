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
using IITMS.NITPRM;

public partial class Itle_ITLE_ChatRegistrationRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITLE_Chat objChatE = new ITLE_Chat();
    ITLE_ChatController objChatC = new ITLE_ChatController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                //if (Session["ICourseNo"] == null)
                //   Response.Redirect("selectCourse.aspx");

                    Page.Title = Session["coll_name"].ToString();
                
                //lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                
                //lblName.Text = Session["userfullname"].ToString();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

            }
            BindListView();
            BindListViewSendRequest();
        }
    }

    private void BindListView()
    {
        try
        {
            //AssignmentController objAM = new AssignmentController();
            //DataSet ds = //objAM.GetStudent_Assignment_ReplyListByAs_No(Convert.ToInt32(Request.QueryString["as_no"])); // Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));
            ITLE_Chat objChatE = new ITLE_Chat();
            ITLE_ChatController objChatC = new ITLE_ChatController();
            objChatE.Operation = "ShowRequest";
            objChatE.UA_IDNORequestReceiver = Convert.ToInt32(Session["userno"]);
            DataSet ds=objChatC.ShowChatRequest(objChatE);
            lvChatRequestList.DataSource = ds;
            lvChatRequestList.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewSendRequest()
    {
        try
        {
            //AssignmentController objAM = new AssignmentController();
            //DataSet ds = //objAM.GetStudent_Assignment_ReplyListByAs_No(Convert.ToInt32(Request.QueryString["as_no"])); // Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));
            ITLE_Chat objChatE = new ITLE_Chat();
            ITLE_ChatController objChatC = new ITLE_ChatController();
            //objChatE.UA_IDNORequestReceiver = Convert.ToInt32(Session["userno"]);
            //DataSet ds = objChatC.GetAllExecutedTPlan(objChatE);
            //lvChatRequestList.DataSource = ds;
            //lvChatRequestList.DataBind();
            objChatE.Operation = "SendRequest";
            objChatE.UA_IDNORequestSender =Convert.ToInt32(Session["userno"]);
            DataSet ds=objChatC.ShowSendRequest(objChatE);
            //LVSendRequest.DataSource = ds;
            //LVSendRequest.DataBind();
            RptrSendRequest.DataSource = ds;
            RptrSendRequest.DataBind();
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetFileName(object filename, object sa_no)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/student_assignment_reply/assignment_reply" + Convert.ToInt32(sa_no) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //setControl(false);
        lblStatus.Text = string.Empty;

    }

    //private void setControl(Boolean status)
    //{
    //    trButtons.Visible = status;
    //    trReplyDate.Visible = status;
    //    trRemark.Visible = status;


    //    if (status == false)
    //    {   
    //        tdDate.InnerText = "";
    //    }

    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ITLE_ChatController objChatC = new ITLE_ChatController();
            ITLE_Chat objChatE = new ITLE_Chat();

            objChatE.Operation = "AddRegistration";
            objChatE.UA_IDNO = Convert.ToInt32(Session["userno"]); //UA_NO USE , NOT A UA_IDNO FROM USER_ACC TABLE
            objChatE.UA_IDNO_NETWORKMEMBERID = string.Empty;
            objChatE.ISActiveApply = true;
            objChatE.ISActiveFriendRequest = true;
            CustomStatus cs = (CustomStatus)objChatC.AddRegistration(objChatE);
            if (cs.Equals(CustomStatus.RecordSaved))
                lblStatus.Text = "Registration Successfully";

           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.submitAssignReplyRemark-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //submitAssignReplyRemark();
        //setControl(false);
        //ViewState["sa_no"] = null;
        //return;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int count =Convert.ToInt32(objCommon.LookUp("ITLE_CHATNETWORK", "COUNT(UA_IDNO)", "UA_IDNO=" + Convert.ToInt32(Session["userno"])));

            if (count > 0)
            {

                ImageButton btnEdit = sender as ImageButton;
                int sa_no = int.Parse(btnEdit.CommandArgument);
                objChatE.Operation = "Add";
                objChatE.UA_IDNORequestReceiver = sa_no;
                objChatE.UA_IDNORequestSender = Convert.ToInt32(Session["userno"]);
                CustomStatus cs = (CustomStatus)objChatC.AddChatRequest(objChatE);
                if (cs.Equals(CustomStatus.RecordSaved))
                    lblStatus.Text = "Request Send Successfully";
                //ShowDetail(sa_no);


                ViewState["action"] = "edit";
                //trButtons.visible = true;
            }
            else
            {
                lblStatus.Text = "Please Sign In";
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int sa_no)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["sa_no"] = sa_no;
            DataTableReader dtr = objAM.GetSingleAssignmentStudentReply(Convert.ToInt32(ViewState["sa_no"]));

            //trButtons.Visible = false;
            //trDesc.Visible = false;
            //trReplyDate.Visible = false;
            //trfile.Visible = false;
            //trRemark.Visible = false;
            ////Show Assignment Reply Details
            //setControl(false);
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //trButtons.Visible = true;
                    //trDesc.Visible = true;
                    //trReplyDate.Visible = true;
                    //trfile.Visible = true;
                    //trRemark.Visible = true;
                    //setControl(true);
                    
                    
                    //tdDate.InnerText = dtr["REPLY_DATE"] == null ? "" : dtr["REPLY_DATE"].ToString(); //ATTACHMENT
                    

                    if (dtr["CHECKED"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(dtr["CHECKED"].ToString()) == 1)
                        {
                            
                        }
                    }





                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int requestsender = int.Parse(btnDel.CommandArgument);

            objChatE.Operation = "RejectRequest";
            objChatE.UA_IDNO = Convert.ToInt32(Session["userno"]);
            objChatE.UA_IDNO_NETWORKMEMBERID = requestsender.ToString();
            CustomStatus cs = (CustomStatus)objChatC.RejectRequest(objChatE);
            if (cs.Equals(CustomStatus.RecordSaved))
                lblStatus.Text = "Request Reject Successfully";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnAcceptRequest_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int requestsender = int.Parse(btnDel.CommandArgument);
            string Reqsender = string.Empty;
            //Reqsender =","+requestsender.ToString();
            objChatE.Operation = "AcceptRequest";
            objChatE.UA_IDNO = Convert.ToInt32(Session["userno"]);
            objChatE.UA_IDNO_NETWORKMEMBERID = requestsender.ToString();//Convert.ToString(Reqsender);
            CustomStatus cs = (CustomStatus)objChatC.AcceptRequest(objChatE);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                lblStatus.Text = "Request Accept Successfully";
                BindListView();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
