//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : MASTER
// PAGE NAME     : TIME SLOT                                     
// CREATION DATE : 
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 28-DEC-2011
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_MASTERS_TimeSlot : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SlotMaster objSM = new SlotMaster();
    SlotController objSC = new SlotController();

    string degrees = "0,";
    string college_ids = "0,";


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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
                BindListLiew();
                ViewState["action"] = "add";
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label - 
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header
            }
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
                Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentResultList.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO Desc");
            //objCommon.FillDropDownList(ddldegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO>0", "SLOTTYPENO");
           // objCommon.FillDropDownList(ddlIstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            PopulateInstituteList();// add by maithili [23-08-2022]
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {        
        int ActiveStatus = 0;
        try
        {
            objSM.SLOTNAME = txtslotname.Text.ToString().Trim();
            objSM.SESSIONNO = Convert.ToInt32(Session["currentsession"]); //changed by reena
            objSM.SlotTypeNo = Convert.ToInt32(ddlSlotType.SelectedValue);
            objSM.SequenceNo = Convert.ToInt32(txtSequenceNo.Text == string.Empty ? "0" : txtSequenceNo.Text); //Added Mahesh on Dated 19-05-2021

            //Add by maithili [29-08-2022]
            objSM.College_Ids = "0,";
            objSM.Degrees     = "0,";
            getDegreesCollege_IDsValues(); //ref college_ids, ref degrees
            objSM.Degrees = degrees;
            objSM.College_Ids = college_ids;
            int count1 = 0;
            string values = string.Empty;
            foreach (ListItem Item in chkIstitutelist.Items)
            {
                if (Item.Selected)
                {
                   // values += Item.Value + ",";
                    count1++;
                } 
            }
            if (count1 == 0)
            {
                objCommon.DisplayMessage(updpnl, "Please Select at least one School", this.Page);
                return; 
            }
            if (objSM.Degrees == "0")
            {
                objCommon.DisplayMessage(updpnl, "Please Select at least one Degree", this.Page);
                return;
            }
           
            objSM.TIMEFROM = txtfrom.Text;
            objSM.TIMETO=txtTo.Text;
            ActiveStatus = hfdStat.Value == "true" ? 1 : 0; // Added By Rishabh on 24/01/2022
            DataSet ds = (DataSet)ViewState["ds"];
           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = null;
                dt = ds.Tables[0];
                int count = 0;
                count = dt.AsEnumerable().Count(TimeSlot => TimeSlot.Field<string>("SLOTNAME").Equals(txtslotname.Text)
                                           && TimeSlot.Field<string>("TIMEFROM").Equals(objSM.TIMEFROM)
                                           && TimeSlot.Field<string>("TIMETO").Equals(objSM.TIMETO)
                                           && TimeSlot.Field<int>("SLOTTYPE").Equals(objSM.SlotTypeNo)
                                           && TimeSlot.Field<int>("COLLEGE_ID").Equals(objSM.College_Id)
                                           && TimeSlot.Field<int>("DEGREENO").Equals(objSM.DEGREENO)
                                           && TimeSlot.Field<int>("SEQUENCENO").Equals(objSM.SequenceNo));
                if (count > 0)
                {
                    objCommon.DisplayMessage(updpnl, "Time slot already exists.", this.Page);
                    this.ClearAfterUpdate();
                    return;
                }
            }           
            if (ViewState["action"] != null && ViewState["action"].ToString() == "edit")
            {
                //For Update
                //objSM.IDNO  = Convert.ToInt32(hdnoldslotno.Value)

                objSM.IDNO = Convert.ToInt32(hdnidno.Value);  //Identity column for updating
                int slot = Convert.ToInt32(objCommon.LookUp("ACD_TIME_SLOT", "SLOTNO", "IDNO=" + Convert.ToInt32(hdnidno.Value)));

                objSM.SLOTNO = slot;
                //objSM.SLOTNO = 1;
                int ret = objSC.InsertSlot(objSM, ActiveStatus);
                if (ret >= 0)
                    objCommon.DisplayMessage(updpnl, "Record Updated Successfully", this.Page);
                else
                    objCommon.DisplayMessage(updpnl, "Error...", this.Page);               
            }
            else //for Insert
            {
                int ret = objSC.InsertSlot(objSM, ActiveStatus);
                // objSM.SLOTNO = 0;
                if (ret >= 0)
                    objCommon.DisplayMessage(updpnl, "Record Saved Successfully", this.Page);
                else
                    objCommon.DisplayMessage(updpnl, "Error...", this.Page);
            }

            this.ClearAfterUpdate();
            BindListLiew();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void ClearAfterUpdate()
    {
        ViewState["action"] = null;

        //COMMENTED BY SUMIT ON 28-JAN-2020
        //ddldegree.Items.Clear();
        //ddldegree.Items.Add(new ListItem("Please Select", "0"));

        //ddldegree.SelectedIndex = 0;
        txtslotname.Text = string.Empty;
        txtfrom.Text = string.Empty;
        txtTo.Text = string.Empty;
        ddlSlotType.SelectedIndex = 0;
        //ddlIstitute.SelectedIndex = 0;
        chkIstitute.Checked = false;//modify by maithili [23-08-2022]
        college_ids = "0,";
        txtSequenceNo.Text = string.Empty;
        //chkFrom.Checked = false;
       // chkTo.Checked = false;
        chkDegreeList.Items.Clear();//modify by maithili [23-08-2022];
        chkIstitutelist.SelectedIndex=-1;//modify by maithili [23-08-2022];
        
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int uano = int.Parse(btnEdit.CommandArgument);
            hdnidno.Value = Convert.ToString(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblslotname = lst.FindControl("lblslotname") as Label;
            Label lbldegree = lst.FindControl("lbldegree") as Label;
            Label lbltimefrom1 = lst.FindControl("lbltimefrom") as Label;
            Label lbltimeto1 = lst.FindControl("lbltimeto") as Label;
            Label lblSequenceNo = lst.FindControl("lblSequenceNo") as Label;
            Label lblStatus = lst.FindControl("lblStatus") as Label;

            HiddenField hdnslotno = lst.FindControl("hdnslotno") as HiddenField;
            hdnoldslotno.Value = hdnslotno.Value;
            if (lblslotname.Text.ToString().Trim() != string.Empty.ToString().Trim())
            {
                if (hdnslotno.Value == "")
                    hdnslotno.Value = "0";

                if (lblStatus.Text.ToString().Equals("Active"))
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
             
                txtSequenceNo.Text = lblSequenceNo.Text;
                txtslotname.Text = lblslotname.Text.ToString().Trim();
            }
            else
                txtslotname.Text = string.Empty;

            HiddenField hdnsession = lst.FindControl("hdnsession") as HiddenField;
            String DegreeNo = "0";
            DegreeNo = objCommon.LookUp("ACD_TIME_SLOT", "(cast (DEGREENO as nvarchar(10)) +'$'+ cast(COLLEGE_ID as nvarchar(10))) as DEGREENO", "IDNO=" + uano);
            //DegreeNo = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREENO", "DEGREENAME='" + lbldegree.Text.ToString().Trim() + "'"));
            int slottypeno = Convert.ToInt32(objCommon.LookUp("ACD_TIME_SLOT", "DISTINCT SLOTTYPE", "IDNO=" + uano));
            int College_ID = Convert.ToInt32(objCommon.LookUp("ACD_TIME_SLOT", "DISTINCT ISNULL(COLLEGE_ID,0)", "IDNO=" + uano));
            //objCommon.FillDropDownList(ddldegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.DEGREENO > 0 AND CDB.COLLEGE_ID=" + College_ID, "D.DEGREENO");
            HiddenField hdnDegreeNo = lst.FindControl("hdnDegreeNo") as HiddenField;
            if (DegreeNo != "0")
            {
                PopulateDropDownList();
                ddlSlotType.SelectedValue = Convert.ToString(slottypeno);
              
                for (int j = 0; j < College_ID.ToString().Length; j++)
                {
                    for (int i = 0; i < chkIstitutelist.Items.Count; i++)
                    {
                        if (College_ID.ToString() == chkIstitutelist.Items[i].Value)
                            chkIstitutelist.Items[i].Selected = true;
                    }
                }
                PopulateDegreeList();
                chkDegreeList.SelectedValue = DegreeNo; //Add by maithili [23-08-2022]
            }
            else
            {
                ddlSlotType.SelectedIndex = 0;
                chkIstitute.Checked = false;//Add by maithili [23-08-2022]
            }
            if (lbltimefrom1.Text.ToString().Trim() != string.Empty.ToString().Trim())
            {

                txtfrom.Text = lbltimefrom1.Text.ToString().Trim();
            }
            else
                txtfrom.Text = string.Empty;
            if (lbltimeto1.Text.ToString().Trim() != string.Empty.ToString().Trim())
            {
                txtTo.Text = lbltimeto1.Text.ToString().Trim();
            }
            else
                txtTo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListLiew()
    {
        DataSet ds = new DataSet();

        getDegreesCollege_IDsValues();
        ds = objSC.GetSlotDetails(college_ids, degrees);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTimeTable.DataSource = ds;
            lvTimeTable.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvTimeTable);//Set label 
            ViewState["ds"] = ds;
        }
        else
        {
            lvTimeTable.DataSource = null;
            lvTimeTable.DataBind();
            ViewState["ds"] = null;
        }
    }

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDelete = sender as ImageButton;
    //        CustomStatus cs = (CustomStatus)objSC.DeleteSlot(Convert.ToInt32(btnDelete.CommandArgument));
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //            objCommon.DisplayMessage(updpnl,"Record Deleted Successfully", this.Page);
    //        else
    //            objCommon.DisplayMessage(updpnl,"Error in deleting record...", this.Page);
    //        BindListLiew();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //protected void chkTo_CheckedChanged(object sender, EventArgs e)
    //{
    //   // chkTo.Text = chkTo.Checked == true ? "AM" : "PM";
    //}

    //protected void chkFrom_CheckedChanged(object sender, EventArgs e)
    //{
    //  //  chkFrom.Text = chkFrom.Checked == true ? "AM" : "PM";
    //}

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlIstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddldegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "DEGREENAME", "D.DEGREENO > 0 AND CDB.COLLEGE_ID=" + ddlIstitute.SelectedValue + "AND CDB.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "D.DEGREENO");
        BindListLiew();
        PopulateDegreeList();
    
    }
    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListLiew();
    }


    //void getChk()//Add by maithili [23-08-2022]
    //{
    //    for (int i = 0; i < chkIstitutelist.Items.Count; i++)
    //    {
    //        if (chkIstitutelist.Items[i].Selected)
    //        {
    //            college_ids += chkIstitutelist.Items[i].Value + ",";
    //        }
    //    }

    //    for (int i = 0; i < chkDegreeList.Items.Count; i++)
    //    {
    //        if (chkDegreeList.Items[i].Selected)
    //        {
    //            degrees += chkDegreeList.Items[i].Value + ",";
    //        }
    //    }

    //    if (!string.IsNullOrEmpty(degrees))
    //        degrees = degrees.Substring(0, degrees.Length - 1);

    //    if (!string.IsNullOrEmpty(college_ids))
    //        college_ids = college_ids.Substring(0, college_ids.Length - 1);

    //}

    void getDegreesCollege_IDsValues()//Add by maithili [23-08-2022]  //ref string college_ids, ref string degrees
    {
        for ( int i = 0; i < chkDegreeList.Items.Count; i++)
        {
            if (chkDegreeList.Items[i].Selected)
            {              
                degrees += chkDegreeList.Items[i].Value.Split('$')[0] + ",";
                college_ids += chkDegreeList.Items[i].Value.Split('$')[1] + ",";
            }
        }

        if (!string.IsNullOrEmpty(degrees)) degrees = degrees.TrimEnd(','); ;
        if (!string.IsNullOrEmpty(college_ids)) college_ids = college_ids.Substring(0, college_ids.Length - 1); ;
    }
    private void PopulateInstituteList()//Add by maithili [23-08-2022]
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkIstitutelist.DataTextField = "COLLEGE_NAME";
                    chkIstitutelist.DataValueField = "COLLEGE_ID";
                    chkIstitutelist.ToolTip = "COLLEGE_ID";
                    chkIstitutelist.DataSource = ds.Tables[0];
                    chkIstitutelist.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void PopulateDegreeList()//Add by maithili [23-08-2022]
    {
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkIstitutelist.Items)
            {
                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }

            if (count > 0)
            {
                string institueNos = values.TrimEnd(',');
                DataSet ds = objCommon.FillDropDown(@"ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_DEGREE CD ON (CM.COLLEGE_ID=CD.COLLEGE_ID) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=CD.DEGREENO)",
                    "(cast (CD.DEGREENO as nvarchar(10)) +'$'+ cast(CD.COLLEGE_ID as nvarchar(10))) as DEGREENO",
                    "concat(cm.COLLEGE_NAME,' - ',D.CODE)CODE", "CM.COLLEGE_ID in (" + institueNos + ") or 0 in (" + institueNos + ")", "D.DEGREENO");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkDegreeList.Visible = true;
                        chkDegreeList.DataTextField = "CODE";
                        chkDegreeList.DataValueField = "DEGREENO";
                        chkDegreeList.ToolTip = "DEGREENO";
                        chkDegreeList.DataSource = ds.Tables[0];
                        chkDegreeList.DataBind();
                    }
                }
                else
                    chkDegreeList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void chkDegreeList_SelectedIndexChanged(object sender, EventArgs e) //Add by maithili [23-08-2022]
    {
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkDegreeList.Items)
            {
                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }
            ViewState["Degnos"] = values;
            if (count > 0)
            {
                BindListLiew();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void chkIstitutelist_SelectedIndexChanged(object sender, EventArgs e)//Add by maithili [23-08-2022]
    {
      
        chkDegreeList.Items.Clear();
        try
        {
            int count = 0;

            string values = string.Empty;
            foreach (ListItem Item in chkIstitutelist.Items)
            {

                if (Item.Selected)
                {
                    values += Item.Value + ",";
                    count++;
                }
            }
            ViewState["Institutenos"] = values;
            if (count > 0)
            {
                PopulateDegreeList();
                BindListLiew();
            }

        }

        catch (Exception ex)
        {
            throw;
        }
    }
}
