using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
public partial class Company_Details : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    ViewState["action"] = "add";
                    //---------start-------19-05-2023-----handel----tab for student----
                   DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "REGNO", "IDNO = (select UA_IDNO from user_acc where UA_NO='" +Convert.ToInt32( Session["userno"]) + "')", "");
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        tab1.Visible = true;
                        tab2.Visible = true;
                        tab3.Visible = false;
                    }
                    //----------end------19-05-2023-----handel----tab for student----

                    objCommon.FillDropDownList(ddlJobSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS=1", "JOBSECTOR");
                    objCommon.FillDropDownList(ddlAJobSec, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS=1", "JOBSECTOR");
                    objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "", "COUNTRYNAME");
                    objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENO");
                    objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITYNO");
                    // objCommon.FillDropDownList(ddlDesignation, "ACD_TP_JOB_ROLE", "ROLENO", "JOBROLETYPE", "", "JOBROLETYPE");
                    objCommon.FillListBox(lstbxCareerAreas, "ACD_TP_WORK_AREA ", "WORKAREANO", "WORKAREANAME ", "", "WORKAREANAME ");
                    objCommon.FillListBox(lstbxCareerArea, "ACD_TP_WORK_AREA ", "WORKAREANO", "WORKAREANAME ", "", "WORKAREANAME ");
                    objCommon.FillListBox(lstbxAssociation, "ACD_TP_ASSOCIATION_FOR ", "ASSOCIATIONNO", "ASSOCIATION_FOR ", "STATUS=1", "ASSOCIATION_FOR ");
                    objCommon.FillListBox(lstbxAssociate, "ACD_TP_ASSOCIATION_FOR ", "ASSOCIATIONNO", "ASSOCIATION_FOR ", "STATUS=1", "ASSOCIATION_FOR ");
                    objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "COMPNAME");
                    
                   
                    BindListCompanyapprovalforallrequest();
                   // BindListCompanyapproval();
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Add Company Code

    protected void btnSubmitAddCompany_Click(object sender, EventArgs e)
    {
        TrainingPlacement objTP = new TrainingPlacement();

        try
        {
                   


                    objTP.COMPNAME = Convert.ToString(txtCompanyName.Text);

                    objTP.COMPCODE = Convert.ToString(txtShortName.Text);

                    objTP.CompRegNo = txtRegistrationNo.Text;

                    objTP.WEBSITE = Convert.ToString(txtWebsite.Text);
                    //if (imgUpFile.ImageUrl != null)
                    //{
                       // objTP.Logo = objCommon.GetImageData(fuCollegeLogo) as byte[];
                    //}

                        if (fuCollegeLogo.HasFile)
                        {
                            objTP.Logo = objCommon.GetImageData(fuCollegeLogo);
                        }
                        else
                        {

                            objTP.Logo = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/IMAGES/nophoto.jpg"));// null; //"~/images/nophoto.jpg";

                           // objTP.Logo =new byte[0];
                        }

                    //else
                    //{
                        //imgUpFile.ImageUrl = "~/Images/rc-patel-institute-of-technology-dhule-logo.png";
                       // objTP.Logo = (@"/Images/rc-patel-institute-of-technology-dhule-logo.png");  
                           //("~/Images/rc-patel-institute-of-technology-dhule-logo.png");
                        //byte imglogo =Convert.ToByte(imgUpFile.ImageUrl);
                        //objTP.Logo = objCommon.GetImageData(imglogo) as byte[];
                        //objTP.Logo = Convert.ToByte("~/Images/rc-patel-institute-of-technology-dhule-logo.png");
                    //}
                      

                    if (!(ddlJobSector.SelectedValue.Equals(ddlJobSector.SelectedItem.ToString())))
                        objTP.JobSector = Convert.ToInt32(ddlJobSector.SelectedValue);
                    else
                        objTP.JobSector = 0;

                    string CarrerAreas = "";
                    string Associtionfor = "";

                    foreach (ListItem items in lstbxCareerAreas.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            CarrerAreas += items.Value + ',';
                        }
                    }
                    if (CarrerAreas != "")
                        {
                        CarrerAreas = CarrerAreas.Remove(CarrerAreas.Length - 1);
                        }
                    else
                        {
                        CarrerAreas = "0";
                        }
                    

                    foreach (ListItem items in lstbxAssociation.Items)
                    {
                        if (items.Selected == true)
                        {
                            //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            Associtionfor += items.Value + ',';
                        }

                    }
                    if (Associtionfor != "")
                        {
                        Associtionfor = Associtionfor.Remove(Associtionfor.Length - 1);
                        }
                    else
                        {
                        Associtionfor="0";
                        }
                    objTP.COLLEGE_CODE = Convert.ToString(Session["colcode"]);



                    DataSet ds = objCommon.FillDropDown("ACD_TP_COMPANY", "COMPID", "COMPNAME", "COMPNAME='" + Convert.ToString(txtCompanyName.Text.Trim()) + "' and JOBSECTOR='" + ddlJobSector.SelectedValue + "' and COMPID!='" +Convert.ToInt32 (ViewState["COMPID"] )+ "'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayMessage(this.Page, "Company Is Already Exists For Same Job Sector.", this.Page);
                        return;
                    }

            //----start--14-11-2022
                    //objTP.LocationName = Convert.ToString(txtLocationName.Text);

                    //objTP.COMPADD = Convert.ToString(txtaddress.Text);

                    //if (!(ddlCity.SelectedValue).ToString().Equals(string.Empty))
                    
                    //     objTP.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
                    //else
                    //    objTP.CITYNO = 0; 

                    //objTP.CONTPERSON = Convert.ToString(txtContactPerson.Text);
                    //objTP.CONTDESIGNATION = Convert.ToString(txtDesignation.Text);
                    //objTP.PHONENO = Convert.ToString(txtOfficialContact.Text);
                    //objTP.EMAILID = Convert.ToString(txtOfficialMail.Text);

                 //----end--14-11-2022

                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            CustomStatus cs = (CustomStatus)objCompany.AddCompanyNew(objTP, CarrerAreas, Associtionfor, fuCollegeLogo);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(upnlAddCompany, "Record Saved Successfully.", this.Page);
                                ViewState["action"] = null;
                                //Response.Redirect(Request.Url.ToString());
                                //clear();
                                ClearAddCompany();
                                objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "COMPNAME");
                                ClearCompanyLocation();
                                //BindListViewAddCompany();
                              //  BindListCompanyapproval();
                                ViewState["action"] = "add";
                                lvAddCompany.Visible = false;
                            }
                            else if (cs.Equals(CustomStatus.RecordExist))
                            {
                                objCommon.DisplayMessage(upnlAddCompany, "Company Name is Already Registered Please Enter Different Company Name.", this.Page);
                            }
                        }
                        else
                        {
                            if (ViewState["COMPID"] != null)
                            {
                                objTP.COMPID = Convert.ToInt32(ViewState["COMPID"]);
                                CustomStatus CS = (CustomStatus)objCompany.UpdateCompanyNew(objTP, CarrerAreas, Associtionfor, fuCollegeLogo);
                                if (CS.Equals(CustomStatus.RecordUpdated))
                                {
                                    objCommon.DisplayMessage(upnlAddCompany, "Record Updated Successfully.", this.Page);
                                    ViewState["action"] = null;
                                    // Response.Redirect(Request.Url.ToString());
                                    //clear();
                                    ClearAddCompany();
                                    objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "COMPNAME");
                                    ClearCompanyLocation();
                                   // BindListViewAddCompany();
                                   // BindListCompanyapproval();
                                    lvAddCompany.Visible = false;
                                }
                            }
                        }
                        ViewState["action"] = "add";
                    }
            }
        

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelAddCompany_Click(object sender, EventArgs e)
    {
        ClearAddCompany();
        ClearCompanyLocation();
        lvAddCompany.DataSource = null;
        lvAddCompany.DataBind();
       lvComapnyLocation.Visible = false;

        
    }


    private void BindListViewAddCompany()
    {
        try
        {
            DataSet ds = objCompany.GetAddCompanyLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAddCompany.DataSource = ds;
                lvAddCompany.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnEditAddCompany_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Compid = int.Parse(btnEdit.CommandArgument);
            ViewState["COMPID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsAddCompany(Compid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsAddCompany(int Compid)
    {
        try
        {
            char delimiterChars = ',';
            char delimiter = ',';

            DataSet ds = objCompany.GetIdAddCompany(Compid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCompanyName.Text = ds.Tables[0].Rows[0]["COMPNAME"].ToString();

                txtShortName.Text = ds.Tables[0].Rows[0]["COMPCODE"].ToString();
                txtRegistrationNo.Text = ds.Tables[0].Rows[0]["COMPREGNO"].ToString();
                txtWebsite.Text = ds.Tables[0].Rows[0]["WEBSITE"].ToString();
               // if (ds.Tables[0].Rows[0]["LOGO"].ToString() != string.Empty)
                //object val = ds.Tables[0].Rows[0]["LOGO"].ToString();
                //if (val == DBNull.Value)
               if (!DBNull.Value.Equals(ds.Tables[0].Rows[0]["LOGO"]))
          {
                fuCollegeLogo.SaveAs ( ds.Tables[0].Rows[0]["LOGO"].ToString());
          }
              //  this.fuCollegeLogo = ds.Tables[0].Rows[0]["LOGO"].ToString();
               objCommon.FillDropDownList(ddlJobSector, "ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "STATUS=1", "JOBSECTOR");
                ddlJobSector.SelectedValue = ds.Tables[0].Rows[0]["JOBSECNO"].ToString();


                string  lstbxCareerArea=ds.Tables[0].Rows[0]["CAREERAREAS"].ToString();


                string lstbxAssociationFor = ds.Tables[0].Rows[0]["ASSOCIATIONFOR"].ToString();

                string[] utype = lstbxCareerArea.Split(delimiterChars);
                string[] DeptTypes = lstbxCareerArea.Split(delimiter);

                string[] Atype = lstbxAssociationFor.Split(delimiterChars);
                string[] AssTypes = lstbxAssociationFor.Split(delimiter);

                //string[] Atype = lstbxCareerArea.Split(delimiterChars);
                //string[] Association = lstbxCareerArea.Split(delimiter);
                for (int j = 0; j < utype.Length; j++)
                {
                    for (int i = 0; i < lstbxCareerAreas.Items.Count; i++)
                    {
                        if (utype[j] == lstbxCareerAreas.Items[i].Value)
                        {
                            lstbxCareerAreas.Items[i].Selected = true;
                        }
                    }
                }

                //lstbxCareerAreas.SelectedIndex = Convert.ToInt32(lstbxCareerArea);

                for (int k = 0; k < AssTypes.Length; k++)
                {
                    for (int m = 0; m < lstbxAssociation.Items.Count; m++)
                    {
                        if (Atype[k] == lstbxAssociation.Items[m].Value)
                        {
                            lstbxAssociation.Items[m].Selected = true;
                        }
                    }
                }

                imgUpFile.ImageUrl = "../../showimage.aspx?id=" + Compid + "&type=COMPIMG";

                //txtLocationName.Text = ds.Tables[0].Rows[0]["LOCATIONNAME"].ToString();
                //txtaddress.Text = ds.Tables[0].Rows[0]["COMPADD"].ToString();
                //ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
                //txtContactPerson.Text = ds.Tables[0].Rows[0]["CONTPERSON"].ToString();
                //txtDesignation.Text = ds.Tables[0].Rows[0]["CONTDESIGNATION"].ToString();
                //txtOfficialContact.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                //txtOfficialMail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearAddCompany()
    {

        txtCompanyName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtRegistrationNo.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        ddlJobSector.ClearSelection();
        lstbxCareerAreas.ClearSelection();
        lstbxAssociation.ClearSelection();
        //imgUpFile.Visible = false;
        imgUpFile.ImageUrl = null;
        imgUpFile.ImageUrl = "~/Images/default-fileupload.png";

    }
    #endregion

    #region Comp Location 
    protected void btnSubmitCompanyLocation_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "tab2();", true);
        TrainingPlacement objTP = new TrainingPlacement();

        try
        {

            if (!(ddlCompanyName.SelectedValue.Equals(ddlCompanyName.SelectedItem.ToString())))
                objTP.COMPNAME = ddlCompanyName.SelectedValue;
            else
                objTP.COMPNAME = "";

            objTP.LocationName = Convert.ToString(txtLocationName.Text);

            objTP.COMPADD = Convert.ToString(txtaddress.Text);

            //objTP.COMPADD = Convert.ToString(txtComp.Text);
            if (!(ddlCompanyName.SelectedValue.Equals(ddlCity.SelectedItem.ToString())))
                objTP.CITYNO = Convert.ToInt32(ddlCity.SelectedValue);
            else
                objTP.CITYNO = 0;
            if (!(ddlCompanyName.SelectedValue.Equals(ddlCountry.SelectedItem.ToString())))
                objTP.Country = Convert.ToInt32(ddlCountry.SelectedValue);
            else
                objTP.Country = 0;
            if (!(ddlCompanyName.SelectedValue.Equals(ddlState.SelectedItem.ToString())))
                objTP.State = Convert.ToInt32(ddlState.SelectedValue);
            else
                objTP.State = 0; 

            objTP.CONTPERSON = Convert.ToString(txtContactPerson.Text);
            objTP.CONTDESIGNATION = Convert.ToString(txtDesignation.Text);
            objTP.PHONENO = Convert.ToString(txtOfficialContact.Text);
            objTP.EMAILID = Convert.ToString(txtOfficialMail.Text);

            objTP.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objCompany.AddCompanyLocation(objTP);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(upnlAddCompany, "Record Saved Successfully.", this.Page);
                        ClearCompanyLocation();
                        //ViewState["action"] = null;
                       // BindListViewCompanyLocation();
                       // BindListCompanyapproval();
                        //Response.Redirect(Request.Url.ToString());
                        //clear();
                        lvComapnyLocation.Visible = false;
                    }
                }
                else
                {
                    if (ViewState["COMPLOID"] != null)
                    {
                       // objTP.COMPID = Convert.ToInt32(ddlCompanyName.SelectedValue);
                        objTP.COMPID = Convert.ToInt32(ViewState["COMPLOID"]);
                        CustomStatus CS = (CustomStatus)objCompany.UpdateCompanyLocation(objTP );
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.Page, "Record Update Successfully.", this.Page);
                            ViewState["action"] = "add";
                            ClearCompanyLocation();
                          //  BindListViewCompanyLocation();
                            //BindListCompanyapproval();
                           // Response.Redirect(Request.Url.ToString());
                            //clear();
                            lvComapnyLocation.Visible = false;
                            
                        }
                    }

                    
                }
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewCompanyLocation()
    {
        try
        {
            DataSet ds = objCompany.GetAddCompanyLocationLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvComapnyLocation.DataSource = ds;
                lvComapnyLocation.DataBind();
                lvAddCompany.DataSource = null;
                lvAddCompany.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   

    protected void btnEditComapnyLocation_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Compid = int.Parse(btnEdit.CommandArgument);
            ViewState["COMPLOID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCompanyLoaction(Compid);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetailsCompanyLoaction(int Compid)
    {
        try
        {
            DataSet ds = objCompany.GetIdAddCompanyLocation(Compid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCompanyName.SelectedValue = ds.Tables[0].Rows[0]["COMPID"].ToString();
                txtLocationName.Text = ds.Tables[0].Rows[0]["LOCATIONNAME"].ToString();
                //objCommon.FillDropDownList(ddlCompanyName, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "", "COMPNAME");

                txtaddress.Text = ds.Tables[0].Rows[0]["COMPADD"].ToString();
                ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CITYNO"].ToString();
               // objCommon.FillDropDownList(ddlCity, "ACD_CITY", "CITYNO", "CITY", "", "CITY");

                txtContactPerson.Text = ds.Tables[0].Rows[0]["CONTPERSON"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["CONTDESIGNATION"].ToString();
                txtOfficialContact.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                txtOfficialMail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearCompanyLocation()
    {
        //ddlCompanyName.SelectedIndex = 0;
        txtLocationName.Text = string.Empty;
        txtaddress.Text = string.Empty;
        ddlCity.SelectedIndex = 0;
        txtContactPerson.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtOfficialContact.Text = string.Empty;
        txtOfficialMail.Text = string.Empty;
        ddlCompanyName.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
    
    }
    protected void btnCancelComapnyLocation_Click(object sender, EventArgs e)
    {
        ClearCompanyLocation();
        lvComapnyLocation.DataSource = null;
        lvComapnyLocation.DataBind();
        lvAddCompany.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    #endregion

    #region Comp Approved

    protected void rdbNewReq_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["REQSTATUS"] = 1;
        BindListCompanyapproval();
       // lvCompApproved.Visible = true;
        lvComapnyLocation.Visible = false;
        ViewState["REQSTATUS"] = null;
        
    }
    protected void rdbAccepted_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["REQSTATUS"] = 2;
        BindListCompanyapproval();
        //lvCompApproved.Visible = true;
        lvComapnyLocation.Visible = false;
        ViewState["REQSTATUS"] = null;
    }
    protected void rdbRejected_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["REQSTATUS"] = 3;
        BindListCompanyapproval();
        //lvCompApproved.Visible = true;
        lvComapnyLocation.Visible = false;
        ViewState["REQSTATUS"] = null;
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        BindListCompanyapprovalforallrequest();
        lvCompApproved.Visible = true;
        lvComapnyLocation.Visible = false;
        ViewState["REQSTATUS"] = null;
    }

    private void BindListCompanyapproval()
    {
        try
        {
            DataSet ds = objCompany.BindLVCompanyAproval(Convert.ToInt32(ViewState["REQSTATUS"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCompApproved.DataSource = ds;
                lvCompApproved.DataBind();
                // lvCompApproved.Visible = true;
                lvCompApproved.Visible = true;
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                             
                lvCompApproved.Visible = false;
                lvCompApproved = null;
                objCommon.DisplayMessage(upnlCompanyApproved, "Record Does Not Found For This Selection.", Page);
            }

            foreach (ListViewDataItem dataitem in lvCompApproved.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "REJECTED")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else if (Statuss == "ACCEPTED")
                {
                    Status.CssClass = "badge badge-success";
                }
                else {
                    Status.CssClass = "badge badge-warning";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

      
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        try
        {

            objTP.CompRegNo = txtARegno.Text;

            if (!fuALogo.HasFile)
            {
                objTP.Logo = objCommon.GetImageData(fuALogo) as byte[];
            }
            else
            {
                objTP.Logo = null;
            }
          
            string CarrerAreas = "";
            string Associtionfor = "";

            foreach (ListItem items in lstbxCareerArea.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    CarrerAreas += items.Value + ',';
                }
            }
            CarrerAreas = CarrerAreas.Remove(CarrerAreas.Length - 1);

            foreach (ListItem items in lstbxAssociate.Items)
            {
                if (items.Selected == true)
                {
                    //strSplitAry = ddlSchedule.SelectedItem.Text.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    Associtionfor += items.Value + ',';
                }
            }
            Associtionfor = Associtionfor.Remove(Associtionfor.Length - 1);
            objTP.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            string Remark = txtRemark.Text;
            if (ViewState["COMPIDNO"] != null)
            {
                objTP.COMPID = Convert.ToInt32(ViewState["COMPIDNO"]);
                int ReqStatus = 2;
                CustomStatus CS = (CustomStatus)objCompany.InsertNewCompanyCompApprovalPage(objTP, CarrerAreas, Associtionfor, fuCollegeLogo, Remark, ReqStatus);
                if (CS.Equals(CustomStatus.RecordUpdated))
                {
                objCommon.DisplayMessage(upnlCompanyApproved, "Company Added Successfully.", this.Page);
                    ViewState["action"] = null;
                    // Response.Redirect(Request.Url.ToString());
                    //clear();
                   
                    clearmodalCompdetails();
                 
                    BindListViewAddCompany();
                    BindListCompanyapprovalforallrequest();
                   // upd_ModalPopupExtender1.Hide();
                    //upnlreqprocess.Visible = false;
                  ScriptManager.RegisterStartupScript(this.upnlreqprocess, this.GetType(), "Pop", "Hide();", true);
                    
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnRejectCompany_Click(object sender, EventArgs e)
    {
        try
        {
            pnlcompdetails.Visible = false;
            
            if (ViewState["COMPIDNO"] != null)
            {
                int COMPID = Convert.ToInt32(ViewState["COMPIDNO"]);
                int ReqStatus = 3;
                CustomStatus CS = (CustomStatus)objCompany.UpdateCompanyRejectStatus(COMPID,ReqStatus);
                if (CS.Equals(CustomStatus.RecordUpdated))
                {
                objCommon.DisplayMessage(upnlCompanyApproved, "Company Rejected Successfully.", this.Page);
                    ViewState["action"] = null;
                    // Response.Redirect(Request.Url.ToString());
                    //clear();
                    clearmodalCompdetails();
                    BindListViewAddCompany();
                    upnlreqprocess.Visible = false;
                    ScriptManager.RegisterStartupScript(this.upnlreqprocess, this.GetType(), "Pop", "Hide();", true);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        
        }
    }
   
    protected void btnProcessRequest_Click(object sender, EventArgs e)
    
    {
        LinkButton btnidno = sender as LinkButton;
        ViewState["COMPIDNO"] = int.Parse(btnidno.CommandArgument);
        BindCompDetails();
       // upd_ModalPopupExtender1.Show();
        ScriptManager.RegisterStartupScript(this.upnlreqprocess, this.GetType(), "Pop", "Show();", true);
    }

    protected void BindCompDetails()
    {
        try
        {
            DataSet ds = objCompany.BindCompDetailsonCompApproval(Convert.ToInt32(ViewState["COMPIDNO"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtACompName.Text = ds.Tables[0].Rows[0]["COMPNAME"].ToString();
                txtAshortName.Text= ds.Tables[0].Rows[0]["COMPCODE"].ToString();
                txtAwebsite.Text = ds.Tables[0].Rows[0]["WEBSITE"].ToString();
                ddlAJobSec.SelectedValue = ds.Tables[0].Rows[0]["JOBSECTOR"].ToString();
                txtARegno.Text = ds.Tables[0].Rows[0]["COMPREGNO"].ToString();
                lstbxCareerArea.SelectedValue = ds.Tables[0].Rows[0]["CAREERAREAS"].ToString();
                lstbxAssociate.SelectedValue = ds.Tables[0].Rows[0]["ASSOCIATIONFOR"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                fuALogo.SaveAs(ds.Tables[0].Rows[0]["LOGO"].ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_Company.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void BindListCompanyapprovalforallrequest()
    {
        try
        {
            DataSet ds = objCompany.BindLVforCompanyApprovedAllRequest();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCompApproved.DataSource = ds;
                lvCompApproved.DataBind();
                lvCompApproved.Visible = true;
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {

                lvCompApproved.Visible = false;
                lvCompApproved = null;
                objCommon.DisplayMessage(upnlCompanyApproved, "Record Does Not Found For This Selection.", Page);
            }
            foreach (ListViewDataItem dataitem in lvCompApproved.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                LinkButton lnk1 = dataitem.FindControl("btnProcessRequest") as LinkButton;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "REJECTED")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else if (Statuss == "ACCEPTED")
                {
                    Status.CssClass = "badge badge-success";
                   // lnk1.Enabled = false;
                    
                }
                else {
                    Status.CssClass = "badge badge-warning";
                }             
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btncanceladdcomp_Click(object sender, EventArgs e)
    {
        txtARegno.Text = string.Empty;
        txtRemark.Text = string.Empty;
        lstbxCareerAreas.ClearSelection();
        lstbxAssociation.ClearSelection();
        ScriptManager.RegisterStartupScript(this.upnlreqprocess, this.GetType(), "Pop", "Hide();", true);
        
    }
    public void clearmodalCompdetails()
        {
        lstbxCareerAreas.ClearSelection();
        lstbxAssociation.ClearSelection();

        }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListViewAddCompany();
        lvAddCompany.Visible = true;
    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {
        BindListViewCompanyLocation();
        lvComapnyLocation.Visible = true;
    }
}