using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;
public partial class ACADEMIC_EVENT_EventCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventCreation objEve = new EventCreation();
    EventCreationController objEC = new EventCreationController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
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
                CheckPageAuthorization();
                //if (Session["usertype"].Equals(14))
                //{                 
                //}
                //else
                //{
                //    Response.Redirect("~/notauthorized.aspx?page=AffiliatedProfileInstitute.aspx");
                //}
            }
            PopulateDropDown();            
            Session["RegFeeTbl"] = null;
            GetEventDetails();
        }
        
        
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EventCreation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EventCreation.aspx");
        }
    }
    protected void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlEventType, "ACD_EVENT_TYPE", "EVENT_ID", "EVENT_TYPE", "EVENT_ID > 0", "EVENT_ID");
        objCommon.FillDropDownList(ddlParticipant, "ACD_PARTICIPANT_TYPE", "PARTICIPANT_ID", "PARTICIPANT_TYPE", "PARTICIPANT_ID > 0", "PARTICIPANT_ID");
    }
    protected void chkPaid_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkPaid.Checked)
            {
                divPaid.Visible = true;
                ddlParticipant.Focus();
            }
            else
            {
                divPaid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventCreation.chkPaid_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;
            if (Session["RegFeeTbl"] != null && ((DataTable)Session["RegFeeTbl"]) != null)
            {
                dt = (DataTable)Session["RegFeeTbl"];
                DataTable dt1 = (DataTable)Session["RegFeeTbl"];
                foreach (DataRow drow in dt1.Rows)
                {

                    if (drow["PARTICIPANT_ID"].ToString().Equals(ddlParticipant.SelectedValue.ToString()))
                    {
                        objCommon.DisplayMessage(this.Page, "Participant Already Exists", this.Page);
                        ddlParticipant.Focus();
                        return;

                    }
                }

            }
            else
            {
                dt = this.CreateTable_RegFee();
            }
            DataRow dr = dt.NewRow();
            dr["PARTICIPANT_ID"] = Convert.ToInt32(ddlParticipant.SelectedValue);
            dr["PARTICIPANT_TYPE"] = ddlParticipant.SelectedItem;
            dr["REG_FEE"] = txtRegFee.Text.Trim() == null ? string.Empty : txtRegFee.Text.Trim();
            dr["EVENT_TYPE"] = Convert.ToInt32(ddlEventType.SelectedValue);

            dt.Rows.Add(dr);
            Session["RegFeeTbl"] = dt;
            lvRegistrationFee.DataSource = dt;
            lvRegistrationFee.DataBind();
            pnlRegFee.Visible = true;
            lvRegistrationFee.Visible = true;
            ClearRegField();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventCreation.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkPaid.Checked && Session["RegFeeTbl"]==null)
            {
                objCommon.DisplayMessage(this.Page, "Please Enter Participant", this.Page);
                return;
            }

            string Event_File = string.Empty;
            objEve.EventTitle = txtEventTitle.Text.Trim();
            objEve.EventType = Convert.ToInt32(ddlEventType.SelectedValue);
            objEve.EventStartDate = Convert.ToDateTime(txtEventStart.Text);
            objEve.EventEndDate = Convert.ToDateTime(txtEventEnd.Text);
            objEve.EventStartRegDate = Convert.ToDateTime(txtRegStart.Text);
            objEve.EventEndRegDate = Convert.ToDateTime(txtRegEnd.Text);
            objEve.EventVenue = txtVenue.Text.Trim();
            objEve.EventDesc = txtDesc.Text.Trim();
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            int IsPaid = 0;
            
            DataTable dt;
            //-------Participant Details
            objEve.EventParticipant = string.Empty;
            objEve.EventRegFee = string.Empty;
            if (chkPaid.Checked)
            {
                if (Session["RegFeeTbl"] != null && ((DataTable)Session["RegFeeTbl"]) != null)
                {
                    dt = (DataTable)Session["RegFeeTbl"];
                    bool isParticipant = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        isParticipant = true;
                        objEve.EventParticipant += dr["PARTICIPANT_ID"].ToString() + "$";
                        objEve.EventRegFee += dr["REG_FEE"].ToString() + "$";
                    }
                    if (isParticipant == true)
                    {
                        objEve.EventParticipant = objEve.EventParticipant.Substring(0, objEve.EventParticipant.Length - 1);
                        objEve.EventRegFee = objEve.EventRegFee.Substring(0, objEve.EventRegFee.Length - 1);
                    }
                }
               
                IsPaid = 1;
                objEve.EventParticipant = objEve.EventParticipant;
                objEve.EventRegFee = objEve.EventRegFee;
            }
            else
            {
                IsPaid = 0;
                objEve.EventParticipant = string.Empty;
                objEve.EventRegFee = string.Empty;
            }

            
            
            if (!fuEvent.FileName.Equals(""))
            {
                string path = MapPath("~/UPLOAD_FILES/Event/");
                try
                {
                    if (!(Directory.Exists(path)))
                        Directory.CreateDirectory(path);
                    if (fuEvent.HasFile)
                    {
                        if (fuEvent != null)
                        {
                            string[] validFileTypes = { "docx", "doc", "pdf" };
                            string ext1 = System.IO.Path.GetExtension(fuEvent.PostedFile.FileName);
                            bool isValidFile = false;
                            for (int i = 0; i < validFileTypes.Length; i++)
                            {
                                if (ext1 == "." + validFileTypes[i])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            if (fuEvent == null)
                            {
                                objCommon.DisplayMessage(this.Page, "Select Leaflet/Brochure File to Upload or Uploaded file size should be greater than 0 kb !", this.Page);
                                return;
                            }
                            if (!isValidFile)
                            {
                                objCommon.DisplayMessage(this.Page, "Upload the Leaflet/Brochure File only with following formats: .docx, .doc, .pdf", this.Page);
                            }
                            else
                            {
                                string[] array1 = Directory.GetFiles(path);
                                foreach (string str in array1)
                                {
                                    if ((path + fuEvent.FileName.ToString().Replace(' ', ' ')).Equals(str))
                                    {

                                        objCommon.DisplayMessage(this.Page, "File Already Exists!", this.Page);
                                        return;
                                    }

                                }
                                Event_File = fuEvent.FileName.ToString();
                                fuEvent.SaveAs(MapPath("~/UPLOAD_FILES/Event/" + fuEvent.FileName.Replace(' ', ' ')));
                                lblBrochure.Visible = true;
                                lblBrochure.Text = Event_File;
                                lblBrochure.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Select File to Upload or Check Uploaded file size should be greater than 0 kb !", this.Page);
                        return;
                        //objCommon.DisplayMessage(updPersonalInfo, "Select Resume to Upload !", this);
                        //return;
                    }
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "EventCreation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
            }
            else
            {
                Event_File = lblBrochure.Text;
            }

            if (ViewState["action"].Equals("edit"))
            {
                int TitleId = Convert.ToInt32(ViewState["TITLEID"]);
                CustomStatus cs = (CustomStatus)objEC.UpdateEventCreation(objEve, Convert.ToInt32(Session["userno"]), ipAddress, Event_File, TitleId,IsPaid);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Event Updated Successfully", this.Page);
                    ClearField();
                    btnAdd.Text = "Add";
                    GetEventDetails();
                    return;
                }

            }
            else
            {
                CustomStatus cs = (CustomStatus)objEC.AddEventCreation(objEve, Convert.ToInt32(Session["userno"]), ipAddress, Event_File,IsPaid);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Event Created Successfully", this.Page);
                    ClearField();
                    GetEventDetails();
                    
                    return;
                }
                else if (cs.Equals(CustomStatus.Error))
                {
                    objCommon.DisplayMessage(this.Page, "Event Title Is Already Exists For Selected Event Type", this.Page);
                    return;
                }
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventCreation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private DataTable CreateTable_RegFee()
    {
        DataTable dtRegFees = new DataTable();
        dtRegFees.Columns.Add(new DataColumn("PARTICIPANT_ID", typeof(int)));
        dtRegFees.Columns.Add(new DataColumn("PARTICIPANT_TYPE", typeof(string)));
        dtRegFees.Columns.Add(new DataColumn("REG_FEE", typeof(string)));
        dtRegFees.Columns.Add(new DataColumn("EVENT_TYPE", typeof(int)));
        return dtRegFees;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lvRegistrationFee.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["RegFeeTbl"] != null && ((DataTable)Session["RegFeeTbl"]) != null)
            {
                dt = ((DataTable)Session["RegFeeTbl"]);
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                ddlParticipant.SelectedValue = dr["PARTICIPANT_ID"].ToString();
                txtRegFee.Text = dr["REG_FEE"].ToString();
                dt.Rows.Remove(dr);
                Session["RegFeeTbl"] = dt;
                lvRegistrationFee.DataSource = dt;
                lvRegistrationFee.DataBind();
                btnAdd.Text = "Update";
                //lvRegistrationFee.Visible = true;


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EventCreation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ClearRegField()
    {
        ddlParticipant.SelectedIndex = 0;
        txtRegFee.Text = string.Empty;
    }
    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PARTICIPANT_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.GetEditableDataRow --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }
    protected void btnEditEvent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditEvent = sender as ImageButton;
            ViewState["action"] = "edit";
            int TitleId = int.Parse(btnEditEvent.CommandArgument);
            ViewState["TITLEID"] = TitleId;
            GetEventDetailsByTitleId(TitleId);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.btnEditEvent_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void GetEventDetails()
    {
        try
        {
            DataSet ds = objEC.GetEventCreationDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlParticipant.SelectedValue = dr["PARTICIPANT_ID"].ToString();
                //txtEventTitle.Text = ds.Tables[0].Rows[0]["EVENT_TITLE"].ToString();
                lvEventDetails.DataSource = ds;
                lvEventDetails.DataBind();
                ViewState["action"] = "edit";
            }
            ViewState["action"] = "add";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.GetEventDetails --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void GetEventDetailsByTitleId(int EventTitleId)
    {
        try
        {
            DataSet ds = objEC.GetEventCreationDetailsByTitleID(EventTitleId);
            if (ViewState["action"].ToString() == "edit")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEventTitle.Text = ds.Tables[0].Rows[0]["EVENT_TITLE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_TITLE"].ToString();
                    ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["EventID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["EventID"].ToString();
                    txtEventStart.Text = ds.Tables[0].Rows[0]["EVENT_START_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_START_DATE"].ToString();
                    txtEventEnd.Text = ds.Tables[0].Rows[0]["EVENT_END_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_END_DATE"].ToString();
                    txtRegStart.Text = ds.Tables[0].Rows[0]["EVENT_REG_START_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_REG_START_DATE"].ToString();
                    txtRegEnd.Text = ds.Tables[0].Rows[0]["EVENT_REG_END_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_REG_END_DATE"].ToString();
                    txtVenue.Text = ds.Tables[0].Rows[0]["EVENT_VENUE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_VENUE"].ToString();
                    txtDesc.Text = ds.Tables[0].Rows[0]["EVENT_DESC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_DESC"].ToString();
                    //chkPaid.Checked = true;
                    //divPaid.Visible = true;                                        
                    lblBrochure.Visible = true;
                    lblBrochure.Text = ds.Tables[0].Rows[0]["EVENT_BROCHURE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_BROCHURE"].ToString();
                    //pnlRegFee.Visible = true;
                    //lvRegistrationFee.Visible = true;
                    DataTable dt1;
                    dt1 = this.CreateTable_RegFee();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        DataRow dr = dt1.NewRow();
                        dr["PARTICIPANT_ID"] = ds.Tables[0].Rows[i]["PARTICIPANT_ID"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["PARTICIPANT_ID"].ToString();
                        dr["PARTICIPANT_TYPE"] = ds.Tables[0].Rows[i]["PARTICIPANT_TYPE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["PARTICIPANT_TYPE"].ToString();
                        dr["REG_FEE"] = ds.Tables[0].Rows[i]["REG_FEE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["REG_FEE"].ToString();

                        dt1.Rows.Add(dr);
                    }
                    Session["RegFeeTbl"] = dt1;
                    lvRegistrationFee.DataSource = dt1;
                    lvRegistrationFee.DataBind();

                    if (ds.Tables[0].Rows[0]["ISPAID"].Equals(1))
                    {
                        chkPaid.Checked = true;
                        divPaid.Visible = true;
                        pnlRegFee.Visible = true;
                        lvRegistrationFee.Visible = true;
                    }
                    else
                    {
                        chkPaid.Checked = false;
                        divPaid.Visible = false;
                        pnlRegFee.Visible = false;
                        lvRegistrationFee.Visible = false;
                        Session["RegFeeTbl"] = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.GetEventDetailsByTitleId --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearField()
    {
        txtEventTitle.Text = string.Empty;
        ddlEventType.SelectedIndex = 0;
        txtEventStart.Text = string.Empty;
        txtEventEnd.Text = string.Empty;
        txtRegStart.Text = string.Empty;
        txtRegEnd.Text = string.Empty;
        txtVenue.Text = string.Empty;
        txtDesc.Text = string.Empty;
        chkPaid.Checked = false;
        txtRegFee.Text = string.Empty;
        ddlParticipant.SelectedIndex = 0;
        Session["RegFeeTbl"] = null;
        pnlRegFee.Visible = false;
        lvRegistrationFee.Visible = false;
        lblBrochure.Visible = false;
        divPaid.Visible = false;
    }
    protected void txtEventEnd_TextChanged(object sender, EventArgs e)
    {
        try
        {            
            Int64 StartDate = Convert.ToInt64(Convert.ToDateTime(txtEventStart.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Year);            
            Int64 EndDate = Convert.ToInt64(Convert.ToDateTime(txtEventEnd.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Year);

            if (EndDate < StartDate)
            {
                objCommon.DisplayMessage(this.Page, "Event End Date Should be Greater than Event Start Date", this.Page);
                txtEventEnd.Text = "";
                //txtEventEnd.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.txtEventEnd_TextChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtRegStart_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int64 StartDate = Convert.ToInt64(Convert.ToDateTime(txtEventStart.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Year);
            Int64 EndDate = Convert.ToInt64(Convert.ToDateTime(txtEventEnd.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Year);
            Int64 RegStart = Convert.ToInt64(Convert.ToDateTime(txtRegStart.Text.Trim()).Day + "" + Convert.ToDateTime(txtRegStart.Text.Trim()).Month + "" + Convert.ToDateTime(txtRegStart.Text.Trim()).Year);

            if (RegStart > StartDate)
            {
                objCommon.DisplayMessage(this.Page, "Registration Start Date Should be Smaller than Event Start Date", this.Page);
                txtRegStart.Text = "";
                //txtRegStart.Focus();
                return;
            }
            else if (RegStart > EndDate)
            {
                objCommon.DisplayMessage(this.Page, "Registration Start Date Should be Smaller than Event End Date", this.Page);
                txtRegStart.Text = "";
                //txtRegStart.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.txtRegStart_TextChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
       
    }
    protected void txtRegEnd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Int64 RegStart = Convert.ToInt64( Convert.ToDateTime(txtRegStart.Text.Trim()).Day +""+ Convert.ToDateTime(txtRegStart.Text.Trim()).Month +""+ Convert.ToDateTime(txtRegStart.Text.Trim()).Year);
            Int64 RegEnd = Convert.ToInt64(Convert.ToDateTime(txtRegEnd.Text.Trim()).Day + "" + Convert.ToDateTime(txtRegEnd.Text.Trim()).Month + "" + Convert.ToDateTime(txtRegEnd.Text.Trim()).Year);
            Int64 EndDate = Convert.ToInt64(Convert.ToDateTime(txtEventEnd.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventEnd.Text.Trim()).Year);
            Int64 StartDate = Convert.ToInt64(Convert.ToDateTime(txtEventStart.Text.Trim()).Day + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Month + "" + Convert.ToDateTime(txtEventStart.Text.Trim()).Year);
            if (RegEnd < RegStart)
            {
                objCommon.DisplayMessage(this.Page, "Registration End Date Should be Greater than Registration Start Date", this.Page);
                txtRegEnd.Text = "";
                //txtRegEnd.Focus();
                return;
            }
            else if (RegEnd > EndDate)
            {
                objCommon.DisplayMessage(this.Page, "Registration End Date Should be Smaller than Event End Date", this.Page);
                txtRegEnd.Text = "";
                //txtRegEnd.Focus();
                return;
            }
            else if (RegEnd > StartDate)
            {
                objCommon.DisplayMessage(this.Page, "Registration End Date Should not be Equal or Greater than Event End Date", this.Page);
                txtRegEnd.Text = "";
                //txtRegEnd.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.txtRegEnd_TextChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

     private void fileDownload(string fileName, string fileUrl)
    {
        Page.Response.Clear();
        bool success = ResponseFile(Page.Request, Page.Response, fileName, fileUrl, 1024000);
        if (!success)
        {
            objCommon.DisplayMessage(this.Page,"File Not Found!",this.Page);
            return;
        }
        Page.Response.End();
    }

    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkDownload = (ImageButton)(sender);
            string filename = lnkDownload.CommandArgument;
            string filepath = Server.MapPath("~/UPLOAD_FILES/Event/" + filename);
            fileDownload(filename, filepath);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EventCreation.btnDownload_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
   
}
