//====================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SPORTS 
// CREATED BY    : MRUNAL SINGH
// CREATED DATE  : 26-09-2014
// DESCRIPTION   : USED TO DEFINE PLAYERS FOR DIFFERENT SPORTS
//                 IN DIFF. PARTICIPATION YEAR.
//=====================================================

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;




public partial class Sports_Masters_PlayerMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SportController objSportC = new SportController();
    Sport objSport = new Sport();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlSportType, "SPRT_SPORT_TYPE", "TYPID", "GAME_TYPE", "", "GAME_TYPE");
                    objCommon.FillDropDownList(ddlAcadYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");                  
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
                    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO != 0", "SEMESTERNO");
                  //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAMENAME", "LONGNAME","COLLEGE_CODE");
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "", "COLLEGE_CODE");  
                    lvSprtName.DataSource = null;
                    lvSprtName.DataBind();
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;                    
                }
                BindlistView(Convert.ToChar(rdbPlayerType.SelectedValue));               
            }
            if (Session["RecTbl"] == null)
            {
                lvSprtName.DataSource = null;
                lvSprtName.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This method is used to check page authorization.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetStudName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
               // cmd.CommandText = "select IDNO, STUDNAME +' -- '+ ENROLLNO as STUDNAME FROM  ACD_STUDENT WHERE  STUDNAME like  @SearchText + '%'";
                cmd.CommandText = "select IDNO, STUDNAME +' -- '+ COALESCE(ENROLLNO, '') as STUDNAME FROM  ACD_STUDENT WHERE  STUDNAME like  '%' + @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();

                List<string> StudentName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        StudentName.Add(sdr["IDNO"].ToString() + "---------*" + sdr["STUDNAME"].ToString());
                    }
                }
                conn.Close();
                return StudentName;
            }
        }
    }



    //This method is used to check, that player is already exist or not.
    private bool checkPlayerExist()
    {
        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER", "SPID", "PLAYERNAME, PLAYER_PYEAR", "PLAYER_REGNO='" + txtRegNo.Text + "'", "PLAYERID");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "D.DEGREENO");
            ddlDegree.Focus();
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SportSrno = string.Empty;
            
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = (DataTable)Session["RecTbl"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (SportSrno.Trim().Equals(string.Empty))
                    {
                        SportSrno = dr["SRNO"].ToString();
                    }
                    else
                    {
                        SportSrno += "," + dr["SRNO"].ToString();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Add Registration Details/Click on Add Button.');", true);
               // objCommon.DisplayMessage("Please Add Registration Details/Clisck on Add Button.", this.Page);
               // objCommon.DisplayMessage("Please Select Sport Name.", this.Page);
                return;
            }
            //if (Session["RecTbl"] == null)
            //{

            //    objCommon.DisplayMessage("Please Add Registration Details.", this.Page);
            //    return;


            //}

            if (rdbPlayerType.SelectedValue == "O")
            {
                objSport.PLAYERNAME = txtOtherPlayerName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtOtherPlayerName.Text);
                objSport.DEGREE = txtDegree.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtDegree.Text);
                objSport.BRANCH = txtBranch.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtBranch.Text);
                objSport.SCHEME = txtScheme.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtScheme.Text);
                objSport.SEMESTER = Convert.ToInt32(ddlSem.SelectedValue);
            }
            else
            {
                objSport.PLAYERNAME = txtStudName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtStudName.Text);
            }

            objSport.PLAYER_REGNO = txtRegNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRegNo.Text);
            objSport.PLAYER_ADDRESS = txtAddress.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtAddress.Text);
            objSport.PLAYER_CONTACTNO = txtContactNo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtContactNo.Text);
            objSport.TYPID = Convert.ToInt32(ddlSportType.SelectedValue);           
            objSport.USERID = Convert.ToInt32(Session["userno"]);
            objSport.PLAYER_PYEAR = txtPYear.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPYear.Text);

            if (hfStudNo.Value == "")
            {
                objSport.IDNO = 0;
            }
            else
            {
                objSport.IDNO = Convert.ToInt32(hfStudNo.Value);
            }
            objSport.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSport.ACAD_YEAR = Convert.ToInt32(ddlAcadYear.SelectedValue);
            objSport.COLLEGE_NAME = txtCollegeName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCollegeName.Text);
            objSport.PLAYER_TYPE = Convert.ToChar(rdbPlayerType.SelectedValue);

            if (ViewState["PLAYERID"] == null)
            {  // for check duplicate 09/03/2022
                if (rdbPlayerType.SelectedValue == "U")
                {
                    int duplcountt = 0;
                    duplcountt = Convert.ToInt32(objCommon.LookUp("SPRT_PLAYER_MASTER", "count(*)", "IDNO =" + objSport.IDNO + " and COLLEGE_NO=" + objSport.COLLEGE_NO + " and PLAYER_REGNO='" + objSport.PLAYER_REGNO + "' and SPID=" + SportSrno + " and PLAYER_PYEAR='" + objSport.PLAYER_PYEAR + "' and PLAYER_TYPE='U'"));
                    if (duplcountt > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Player Already Registered in the Same Sport.');", true);
                        return;
                    }
                }
                else if (rdbPlayerType.SelectedValue == "O")
                {
                    int duplcountt = 0;
                    duplcountt = Convert.ToInt32(objCommon.LookUp("SPRT_PLAYER_MASTER", "count(*)", "IDNO =" + objSport.IDNO + " and COLLEGE_NAME='" + objSport.COLLEGE_NAME + "' and PLAYER_REGNO='" + objSport.PLAYER_REGNO + "' and SPID=" + SportSrno + " and PLAYER_PYEAR='" + objSport.PLAYER_PYEAR + "' and PLAYER_TYPE='O'"));
                    if (duplcountt > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Player Already Registered in the Same Sport.');", true);
                        return;
                    }
                }
                // end // for check duplicate 09/03/2022


                objSport.PLAYERID = 0;
                objSportC.AddUpdatePlayerMaster(objSport, SportSrno);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Player Registered Successfully.');", true);

                if (rdbPlayerType.SelectedValue == "O")
                {
                    ClearWhenOtherPlayerInsert();
                }
                else
                {
                    Clear();
                }

                BindlistView(Convert.ToChar(rdbPlayerType.SelectedValue));
            }
            else
            {
                // for check duplicate 09/03/2022
                if (rdbPlayerType.SelectedValue == "U")
                {
                    int duplcountt = 0;
                    duplcountt = Convert.ToInt32(objCommon.LookUp("SPRT_PLAYER_MASTER", "count(*)", "IDNO =" + objSport.IDNO + " and COLLEGE_NO=" + objSport.COLLEGE_NO + " and PLAYER_REGNO='" + objSport.PLAYER_REGNO + "' and SPID=" + SportSrno + " and PLAYER_PYEAR='" + objSport.PLAYER_PYEAR + "' and PLAYER_TYPE='U'"));
                    if (duplcountt > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Player Already Registered in the Same Sport.');", true);
                        return;
                    }
                }
                else if (rdbPlayerType.SelectedValue == "O")
                {
                    int duplcountt = 0;
                    duplcountt = Convert.ToInt32(objCommon.LookUp("SPRT_PLAYER_MASTER", "count(*)", "IDNO =" + objSport.IDNO + "and COLLEGE_NAME='" + objSport.COLLEGE_NAME + "' and PLAYER_REGNO=" + objSport.PLAYER_REGNO + "and SPID=" + SportSrno + "and PLAYER_PYEAR=" + objSport.PLAYER_PYEAR + "and PLAYER_TYPE='O'"));
                    if (duplcountt > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Player Already Registered in the Same Sport.');", true);
                        return;
                    }
                }
                // end // for check duplicate 09/03/2022
                objSport.PLAYERID = Convert.ToInt32(ViewState["PLAYERID"].ToString());
                objSportC.AddUpdatePlayerMaster(objSport, SportSrno);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Player Updated Successfully.');", true);
                if (rdbPlayerType.SelectedValue == "O")
                {
                    ClearWhenOtherPlayerInsert();
                }
                else
                {
                    Clear();
                }
                BindlistView(Convert.ToChar(rdbPlayerType.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            string player_pyear = string.Empty;
            player_pyear = btnEdit.AlternateText;
            ViewState["REGNO"] = btnEdit.CommandArgument;
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER", "TOP 1 PLAYERID", "", "PLAYER_REGNO='" + ViewState["REGNO"] + "' AND PLAYER_PYEAR='" + player_pyear + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PLAYERID"] = ds.Tables[0].Rows[0]["PLAYERID"].ToString();
            }
            ShowDetails(player_pyear);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This method is used to bind the registered players.
    private void BindlistView(char Player_Type)
    {
        try
        {
            //string a=rdbPlayerType.SelectedValue;
         //string[] PlayerType = PoValues.Split('');
           // DataSet ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER PM INNER JOIN SPRT_SPORT_MASTER SM ON (PM.SPID = SM.SPID) INNER JOIN SPRT_SPORT_TYPE ST ON (SM.TYPID = ST.TYPID)", "PM.*", "SM.SNAME,SM.SPID,ST.TYPID,ST.GAME_TYPE", "PLAYER_TYPE='" + Convert.ToChar(rdbPlayerType.SelectedValue)+"'", "PLAYERID DESC");

            DataSet ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER PM left JOIN SPRT_SPORT_MASTER SM ON (PM.SPID = SM.SPID) left JOIN SPRT_SPORT_TYPE ST ON (SM.TYPID = ST.TYPID)left join ACD_COLLEGE_MASTER ACM on (ACM.COLLEGE_ID = PM.COLLEGE_NO)", "PM.PLAYERNAME,PM.PLAYER_REGNO,PM.PLAYER_ADDRESS,PM.PLAYER_CONTACTNO,PM.PLAYER_PYEAR", "SM.SNAME,SM.SPID,ST.TYPID,ST.GAME_TYPE,ACM.COLLEGE_NAME", "PLAYER_TYPE='" + Convert.ToChar(rdbPlayerType.SelectedValue) + "'", "PLAYERID DESC"); //Shaikh Juned (29-08-2022)Convert inner join to left join
         //SHAIKH JUNED (10-08-2022) COLLEGE NAME IS NOT FEACH MAP THE COLLEGE NAME THROUGH JOIN
            
            //  DataSet ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER P INNER JOIN SPRT_SPORT_MASTER SM ON (P.SPID = SM.SPID)", "PLAYER_REGNO, PLAYERNAME, PLAYER_ADDRESS, PLAYER_CONTACTNO, PLAYER_PYEAR", "(CASE PM.COLLEGE_NO WHEN 0 THEN PM.COLLEGE_NAME ELSE ACM.COLLEGE_NAME END) AS COLLEGE_NAME, STUFF((SELECT  ', ' + SNAME", "(PLAYER_REGNO = PM.PLAYER_REGNO AND PLAYER_PYEAR=PM.PLAYER_PYEAR) FOR XML PATH ('')),1,2,'') AS SNAME FROM SPRT_PLAYER_MASTER PM LEFT JOIN ACD_COLLEGE_MASTER ACM ON (PM.COLLEGE_NO = ACM.COLLEGE_ID) GROUP BY PLAYER_REGNO, PLAYERNAME, PLAYER_ADDRESS, PLAYER_CONTACTNO, PLAYER_PYEAR, PM.COLLEGE_NO, PM.COLLEGE_NAME, ACM.COLLEGE_NAME", "PLAYER_PYEAR DESC, PLAYERNAME");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlayer.DataSource = ds;
                lvPlayer.DataBind();
            }
            else
            {
                lvPlayer.DataSource = null;
                lvPlayer.DataBind();
                lvPlayer.Visible = false;
                objUCommon.ShowError(Page, "No Record Found.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This method is used to show details of the selected player
    private void ShowDetails(string player_pyear)
    {

        try
        {
            rdbPlayerType.Enabled = false;
            string regno = string.Empty;
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_PLAYER_MASTER PM INNER JOIN SPRT_SPORT_MASTER SM ON (PM.SPID = SM.SPID) INNER JOIN SPRT_SPORT_TYPE ST ON (SM.TYPID = ST.TYPID)", "PM.*", "SM.SNAME,SM.SPID,ST.TYPID,ST.GAME_TYPE", "PLAYER_REGNO='" + ViewState["REGNO"] + "' AND PLAYER_PYEAR='" + player_pyear + "'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                ddlSportType.SelectedValue = ds.Tables[0].Rows[0]["TYPID"].ToString();
                objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID=" + ddlSportType.SelectedValue + "", "SPID DESC");
                ddlSportName.SelectedValue = ds.Tables[0].Rows[0]["SPID"].ToString();                
                txtRegNo.Text = ds.Tables[0].Rows[0]["PLAYER_REGNO"].ToString();
                regno = ds.Tables[0].Rows[0]["PLAYER_REGNO"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["PLAYER_ADDRESS"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["PLAYER_CONTACTNO"].ToString();
                txtPYear.Text = ds.Tables[0].Rows[0]["PLAYER_PYEAR"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlAcadYear.SelectedValue = ds.Tables[0].Rows[0]["ACAD_YEAR"].ToString();
                hfStudNo.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                rdbPlayerType.SelectedValue = ds.Tables[0].Rows[0]["PLAYER_TYPE"].ToString();
            }

            if (rdbPlayerType.SelectedValue == "O")
            {
                trCollegeNo.Visible = false;
                trCollegeName.Visible = true;
                trPlayerName.Visible = true;
                txtRegNo.Enabled = true;
                trDegree.Visible = false;
                trDegreeOther.Visible = true;
                trBranch.Visible = false;
                trBranchOther.Visible = true;
                trScheme.Visible = false;
                trSchemeOther.Visible = true;



                txtDegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                txtBranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                txtScheme.Text = ds.Tables[0].Rows[0]["SCHEME"].ToString();
                ddlSem.SelectedValue = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
                txtOtherPlayerName.Text = ds.Tables[0].Rows[0]["PLAYERNAME"].ToString();
                ddlCollege.SelectedIndex = 0;
                txtCollegeName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();

              
               

            }
            else
            {
                trCollegeNo.Visible = true;
                trCollegeName.Visible = false;
                trPlayerName.Visible = false;
                txtRegNo.Enabled = false;
                trDegree.Visible = true;
                trDegreeOther.Visible = false;
                trBranch.Visible = true;
                trBranchOther.Visible = false;
                trScheme.Visible = true;
                trSchemeOther.Visible = false;
                txtStudName.Text = ds.Tables[0].Rows[0]["PLAYERNAME"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();

                DataSet dsAcad = null;
                //dsAcad = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_ADMBATCH AB ON(S.ADMBATCH = AB.BATCHNO) INNER JOIN ACD_DEGREE D ON(S.DEGREENO = D.DEGREENO) INNER JOIN ACD_BRANCH B ON(S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SCHEME SC ON(S.SCHEMENO = SC.SCHEMENO) INNER JOIN ACD_SEMESTER SEM ON(S.SEMESTERNO = SEM.SEMESTERNO)", "S.REGNO", "S.IDNO,S.STUDNAME,AB.BATCHNAME,D.DEGREENAME,B.LONGNAME,SC.SCHEMENAME,SEM.SEMESTERNAME", "S.REGNO ='" + regno + "'", "");
                dsAcad = objCommon.FillDropDown("ACD_STUDENT", "REGNO", "IDNO, STUDNAME, ADMBATCH, DEGREENO, BRANCHNO, SCHEMENO, SEMESTERNO", "REGNO ='" + regno + "'", "");
                if (dsAcad.Tables[0].Rows.Count > 0)
                {


                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "D.DEGREENO");
                    ddlDegree.SelectedValue = dsAcad.Tables[0].Rows[0]["DEGREENO"].ToString();
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                    ddlBranch.SelectedValue = dsAcad.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                    ddlScheme.SelectedValue = dsAcad.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMFULLNAME", "", "SEMESTERNO");
                    ddlSem.SelectedValue = dsAcad.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                }
            }

            DataSet dsRec = null;
            // This method is used to get the queries against the selected record.
            dsRec = objSportC.GetSports(regno, player_pyear);
            if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
            {
                lvSprtName.DataSource = dsRec.Tables[0];
                lvSprtName.DataBind();
                Session["RecTbl"] = dsRec.Tables[0];
                ViewState["SRNO"] = Convert.ToInt32(dsRec.Tables[0].Rows.Count);
                btnAdd.Visible = true;
                divSportList.Visible = true;
            }           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //This method is used to clear the controls.
    private void Clear()
    {
        ddlAcadYear.SelectedIndex = 0;
        if (rdbPlayerType.SelectedValue == "U")
        {
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlCollege.SelectedIndex = 0;
        }

        ddlSem.SelectedIndex = 0;
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        txtStudName.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        ViewState["PLAYERID"] = null;
        txtPYear.Text = string.Empty;
        lvSprtName.DataSource = null;
        lvSprtName.DataBind();
        Session["RecTbl"] = null;
        btnAdd.Visible = false;
        hfStudNo.Value = null;
      
        rdbPlayerType.Enabled = true;
        divSportList.Visible = false;
    }
    //This method is used to clear controls in Edit mode.
    private void ClearEdit()
    {
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        txtStudName.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        lvSprtName.DataSource = null;
        lvSprtName.DataBind();
        Session["RecTbl"] = null;
        ddlCollege.SelectedIndex = 0;
        divSportList.Visible = false;
    }

    private void ClearWhenOtherPlayerInsert()
    {
        txtCollegeName.Text = string.Empty;
        txtDegree.Text = string.Empty;
        txtBranch.Text = string.Empty;
        txtScheme.Text = string.Empty;
        ddlSem.SelectedIndex = 0;
        txtOtherPlayerName.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlAcadYear.SelectedIndex = 0;
        txtPYear.Text = string.Empty;
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        lvSprtName.DataSource = null;
        lvSprtName.DataBind();
        Session["RecTbl"] = null;
        ViewState["PLAYERID"] = null;
        divSportList.Visible = false;
    }   

    


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {        
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
    }
   protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {       
       objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
    }
   //protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
   //{
   //    objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT", "IDNO", "STUDNAME", "SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + "", "STUDNAME");
   //}
   //protected void ddlStudent_SelectedIndexChanged(object sender, EventArgs e)
   //{
   //    try
   //    {
   //        DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "REGNO, STUDENTMOBILE, BIRTH_PLACE", "STUDNAME", "IDNO=" + ddlStudent.SelectedValue, "");
   //        if (ds.Tables[0].Rows.Count > 0)
   //        {

   //            //  txtPlayerName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
   //            txtRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
   //            txtAddress.Text = ds.Tables[0].Rows[0]["BIRTH_PLACE"].ToString();
   //            txtContactNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
   //        }
   //    }
   //    catch (Exception ex)
   //    {
   //        if (Convert.ToBoolean(Session["error"]) == true)
   //            objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.ddlStudent_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
   //        else
   //            objUCommon.ShowError(Page, "Server UnAvailable");
   //    }
   //}
    protected void ddlSportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSportType.SelectedIndex > 0)
        {
            PopulateSportName();
            btnAdd.Visible = true;
            ddlSportName.Focus();
        }
    }
    //This method is used to fill sport list in dropdown.
    private void PopulateSportName()
    {
        try
        {
            objCommon.FillDropDownList(ddlSportName, "SPRT_SPORT_MASTER", "SPID", "SNAME", "TYPID=" + ddlSportType.SelectedValue + "", "SPID DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_PlayerMaster.PopulateSportName()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (ViewState["PLAYERID"] == null)
        {
            Clear();
        }
        else
        {
            ClearEdit();
            Clear();
        }
        txtCollegeName.Text = string.Empty;
        txtDegree.Text = string.Empty;
        txtBranch.Text = string.Empty;
        txtScheme.Text = string.Empty;
        ddlSem.SelectedIndex = 0;
        txtOtherPlayerName.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlAcadYear.SelectedIndex = 0;
        txtPYear.Text = string.Empty;
        ddlSportType.SelectedIndex = 0;
        ddlSportName.SelectedIndex = 0;
        lvSprtName.DataSource = null;
        lvSprtName.DataBind();
        Session["RecTbl"] = null;
        ViewState["PLAYERID"] = null;
        divSportList.Visible = false;
        //ddlBranch.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
    }
    // This method is used to create table.
    private DataTable CreateTabel()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("SNAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("SPID", typeof(string)));
        return dtRe;
    }
    // This button is used to add multiple sports for single player.
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlSportName.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Sport Name.');", true);
            //}
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToString(ddlSportName.SelectedValue);
                dr["SNAME"] = Convert.ToString(ddlSportName.SelectedItem);
                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvSprtName.DataSource = dt;
                lvSprtName.DataBind();
                divSportList.Visible = true;
                //Response.Write("<script>alert('Added Successfully');</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Player Added Successfully.');", true);
                ClearRec();
            }
            else
            {
                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToString(ddlSportName.SelectedValue);
                dr["SNAME"] = Convert.ToString(ddlSportName.SelectedItem);
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvSprtName.DataSource = dt;
                lvSprtName.DataBind();
                divSportList.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Player Added Successfully.');", true);
            //Response.Write("<script>alert('Added Successfully');</script>");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_PlayerMaster.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    // This function is used to clear the sport list.
    protected void ClearRec()
    {
        //ddlSportName.SelectedIndex = 0;    25/04/2022
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvSprtName.DataSource = dt;
                lvSprtName.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sport Name Deleted Successfully.');", true);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_PlayerMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // This method is used fetch data of the individual Query. 
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_PlayerMaster.GetEditableDatarow -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfStudNo.Value != "0" || hfStudNo.Value != "" || txtStudName.Text==null)
            {
                
                DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "COLLEGE_ID, BRANCHNO, SCHEMENO, DEGREENO, REGNO,SEMESTERNO", "STUDENTMOBILE, BIRTH_PLACE", "IDNO = " + Convert.ToInt32(hfStudNo.Value), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "D.DEGREENO");
                    ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                    ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                    ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    ddlSem.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    txtRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                    //Modified by Saahil Trivedi 12/1/2022
                    objCommon.FillDropDown("SPRT_PLAYER_MASTER","SPID", "", "PLAYER_ADDRESS='" + txtAddress.Text + "'", "");
                    txtAddress.Text = ds.Tables[0].Rows[0]["BIRTH_PLACE"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    txtRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                }
                ddlCollege.Focus();
            }
            
            else
            {
                txtStudName.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Player Registered Successfully.');", true);

                return;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_Masters_PlayerMaster.btnSearch_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGECODE=" + Session["colcode"].ToString() + "," + "@P_SPID=0";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_SportMaster.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("SportsList", "SportsList.rpt");
    }
    protected void rdbPlayerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbPlayerType.SelectedValue == "O")
            {
                txtCollegeName.Text = string.Empty;
                txtDegree.Text = string.Empty;
                txtBranch.Text = string.Empty;
                txtScheme.Text = string.Empty;
                txtStudName.Text = string.Empty;
                txtOtherPlayerName.Text = string.Empty;
                ddlSem.SelectedIndex = 0;
                txtRegNo.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtAddress.Text = string.Empty;
                ddlAcadYear.SelectedIndex = 0;
                txtPYear.Text = string.Empty;
                ddlSportType.SelectedIndex = 0;
                ddlSportName.SelectedIndex = 0;

                trCollegeNo.Visible = false;
                trCollegeName.Visible = true;
                trPlayerName.Visible = true;
                txtRegNo.Enabled = true;
                trDegree.Visible = false;
                trDegreeOther.Visible = true;
                trBranch.Visible = false;
                trBranchOther.Visible = true;
                trScheme.Visible = false;
                trSchemeOther.Visible = true;
                txtCollegeName.Focus();

                Session["RecTbl"] = null;
                ViewState["PLAYERID"] = null;
                divSportList.Visible = false;
            }
            else
            {
                ddlCollege.SelectedIndex = 0;
                ddlDegree.SelectedIndex = 0;
                ddlBranch.SelectedIndex = 0;
                ddlScheme.SelectedIndex = 0; 
                ddlSem.SelectedIndex = 0;
                txtRegNo.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtAddress.Text = string.Empty;
                ddlAcadYear.SelectedIndex = 0;
                txtPYear.Text = string.Empty;
                ddlSportType.SelectedIndex = 0;
                ddlSportName.SelectedIndex = 0;

                trCollegeNo.Visible = true;
                trCollegeName.Visible = false;
                trPlayerName.Visible = false;
                txtRegNo.Enabled = false;
                trDegree.Visible = true;
                trDegreeOther.Visible = false;
                trBranch.Visible = true;
                trBranchOther.Visible = false;
                trScheme.Visible = true;
                trSchemeOther.Visible = false;
                ddlCollege.Focus();

                Session["RecTbl"] = null;
                ViewState["PLAYERID"] = null;
                divSportList.Visible = false;

            }
            BindlistView(Convert.ToChar(rdbPlayerType.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_Masters_TeamMaster.rdbTeamType_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
