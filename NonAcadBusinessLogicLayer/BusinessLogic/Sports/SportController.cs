//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 30-SEP-2014   
//MODIFY BY     : 
//MODIFIED DATE :    
//================================================================================================   
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL; 

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class SportController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region SPORT
      

                #region Sport Master
                /// <summary>
                /// This is used for Insert and Update Sport Master Entry
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateSportMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[1] = new SqlParameter("@P_SNAME", objSport.SNAME);
                        objParams[2] = new SqlParameter("@P_NO_OF_PLAYERS", objSport.NO_OF_PLAYERS);
                        objParams[3] = new SqlParameter("@P_NO_OF_RESERVE", objSport.NO_OF_RESERVE);
                        objParams[4] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[5] = new SqlParameter("@P_USERID",objSport.USERID);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_SPORT_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_SportMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Team Master
                /// <summary>
                /// This is used for Insert and Update Team Name
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateTeamMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[1] = new SqlParameter("@P_TEAMNAME", objSport.TEAMNAME);
                        objParams[2] = new SqlParameter("@P_ACAD_YEAR", objSport.ACAD_YEAR);
                        objParams[3] = new SqlParameter("@P_TEAMID", objSport.TEAMID);
                        objParams[4] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objSport.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NAME", objSport.COLLEGE_NAME);
                        objParams[7] = new SqlParameter("@P_TEAM_TYPE", objSport.TEAM_TYPE);
                        objParams[8] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_TEAM_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_TeamMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Role Master
                /// <summary>
                /// This is used for Insert and Update role of the Player
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateRoleMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[1] = new SqlParameter("@P_ROLENAME", objSport.ROLENAME);
                        objParams[2] = new SqlParameter("@P_ROLEID", objSport.ROLEID);
                        objParams[3] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_ROLE_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_RollMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetListPlayerPost(int player_post, int teamid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PLAYER_POST", player_post);
                        objParams[1] = new SqlParameter("@P_TEAMID", teamid); 
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_TEAM_DETAILS_GETALL_PLAYER_POST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.GetListPlayerPost-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Staff Master
                /// <summary>
                /// This is used for Insert and Update the Post of the staff members.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateStaffMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_POSTID", objSport.POSTID);
                        objParams[1] = new SqlParameter("@P_POSTNAME", objSport.POSTNAME);
                        objParams[2] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_STAFF_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_StaffMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Club/society Master
                /// <summary>
                /// This is used for Insert and Update the Club Name
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateClubMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CLUBID", objSport.CLUBID);
                        objParams[1] = new SqlParameter("@P_CLUBNAME", objSport.CLUBNAME);
                        objParams[2] = new SqlParameter("@P_CLUB_ADDRESS", objSport.CLUBADDRESS);
                        objParams[3] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_CLUB_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_ClubMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion 

                #region Venue Master
                /// <summary>
                /// This is used for Insert and Update Venue Name
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateVenueMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_VENUEID", objSport.VENUEID);
                        objParams[1] = new SqlParameter("@P_VENUENAME", objSport.VENUENAME);
                        objParams[2] = new SqlParameter("@P_VENUE_ADDRESS", objSport.VENUEADDRESS);
                        objParams[3] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_VENUE_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_VenueMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion


                #region Medal Master
                /// <summary>
                /// This is used for Insert and Update the Medal Name.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateMedalMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MEDALID", objSport.MEDALID);
                        objParams[1] = new SqlParameter("@P_MEDALNAME", objSport.MEDALNAME);
                        objParams[2] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[3] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[4] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_MEDAL_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdateMedalMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Player Master
                /// <summary>
                /// This is used for Insert or Update Players.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdatePlayerMaster(Sport objSport, string SportSrno)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_PLAYERID", objSport.PLAYERID);
                        objParams[1] = new SqlParameter("@P_PLAYERNAME ", objSport.PLAYERNAME);
                        objParams[2] = new SqlParameter("@P_PLAYER_REGNO", objSport.PLAYER_REGNO);
                        objParams[3] = new SqlParameter("@P_PLAYER_ADDRESS", objSport.PLAYER_ADDRESS);
                        objParams[4] = new SqlParameter("@P_PLAYER_CONTACTNO", objSport.PLAYER_CONTACTNO);
                        objParams[5] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[6] = new SqlParameter("@P_PLAYER_PYEAR", objSport.PLAYER_PYEAR);
                        objParams[7] = new SqlParameter("@P_SPORTNO", SportSrno);
                        objParams[8] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[9] = new SqlParameter("@P_IDNO", objSport.IDNO);
                        objParams[10] = new SqlParameter("@P_COLLEGE_NO", objSport.COLLEGE_NO);
                        objParams[11] = new SqlParameter("@P_ACAD_YEAR", objSport.ACAD_YEAR);
                        objParams[12] = new SqlParameter("@P_COLLEGE_NAME", objSport.COLLEGE_NAME);
                        objParams[13] = new SqlParameter("@P_PLAYER_TYPE", objSport.PLAYER_TYPE);
                        objParams[14] = new SqlParameter("@P_DEGREE", objSport.DEGREE);
                        objParams[15] = new SqlParameter("@P_BRANCH", objSport.BRANCH);
                        objParams[16] = new SqlParameter("@P_SCHEME", objSport.SCHEME);
                        objParams[17] = new SqlParameter("@P_SEM", objSport.SEMESTER);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PLAYER_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdatePlayerMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSports(string regno, string player_pyear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PLAYER_REGNO", regno);
                        objParams[1] = new SqlParameter("@P_PLAYER_PYEAR", player_pyear);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PLAYER_SPORT_NAME_IU", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.GetSports ->" + ex.ToString());
                    }
                    return ds;
                }


                #endregion
               
                
                #region Team Details
                /// <summary>
                /// This is used for Insert and Update Team Details.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateTeamDetails(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TDID", objSport.TDID);
                        objParams[1] = new SqlParameter("@P_TEAMID ", objSport.TEAMID);
                        objParams[2] = new SqlParameter("@P_POSTID", objSport.POSTID);
                        objParams[3] = new SqlParameter("@P_PLAYERID", objSport.PLAYERID);
                        objParams[4] = new SqlParameter("@P_ROLEID", objSport.ROLEID);
                        objParams[5] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_TEAM_DETAILS_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_TeamDetails_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion
              
           

                #region Event Creation
                /// <summary>
                /// This function is used to create Event.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateEventOrganize(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_EORGID", objSport.EORGID);
                        objParams[1] = new SqlParameter("@P_BATCHNO", objSport.ACAD_YEAR);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objSport.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_EVENTID", objSport.EVENTID);
                        objParams[4] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[5] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[6] = new SqlParameter("@P_VENUEID", objSport.VENUEID);                        
                        objParams[7] = new SqlParameter("@P_FROM_DATE", objSport.EVENT_FROMDATE);
                        objParams[8] = new SqlParameter("@P_TO_DATE", objSport.EVENT_TODATE);                        
                        objParams[9] = new SqlParameter("@P_GAME_DETAILS", objSport.GAME_DETAILS);
                        objParams[10] = new SqlParameter("@P_REMARK", objSport.REMARK);
                        objParams[11] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_EVENT_ORGANIZE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_Event->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Sport Participation Entry
                /// <summary>
                /// This function is used for Sport Participation Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdate_Sport_Participation(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_PARTICID", objSport.PARTICID);
                        objParams[1] = new SqlParameter("@P_NITSTATUS", objSport.NITSTATUS);
                        objParams[2] = new SqlParameter("@P_TEAMID", objSport.TEAMID);
                        objParams[3] = new SqlParameter("@P_EVENTID", objSport.EVENTID);
                        //objParams[4] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[4] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[5] = new SqlParameter("@P_MEDALID", objSport.MEDALID);
                        //objParams[7] = new SqlParameter("@P_POSTID", objSport.POSTID);
                        objParams[6] = new SqlParameter("@P_DESCRIPTION", objSport.DESCRIPTION);
                        objParams[7] = new SqlParameter("@P_FILEPATH", objSport.FILEPATH);
                        objParams[8] = new SqlParameter("@P_FILENAME", objSport.FILENAME);
                        objParams[9] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_SPORT_PARTICIPATION_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_Sport_Participation->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetTeamDetails(Sport objSport)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TEAMID", objSport.TEAMID);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_TEAM_DETAILS_FOR_SPORT_PARTICIPATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.GetSports ->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region SAC Entry
                /// <summary>
                /// This function is used for SAC Program Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdate_SAC_Program(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SACID", objSport.SACID);
                        objParams[1] = new SqlParameter("@P_EVENTID", objSport.EVENTID);
                        objParams[2] = new SqlParameter("@P_SAC_FROM_DATE", objSport.EVENT_FROMDATE);
                        objParams[3] = new SqlParameter("@P_SAC_TO_DATE", objSport.EVENT_TODATE);
                        objParams[4] = new SqlParameter("@P_CLUBID", objSport.CLUBID);
                        objParams[5] = new SqlParameter("@P_VENUEID", objSport.VENUEID);
                        objParams[6] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_SAC_PROGRAM_ENTRY_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_SAC_Program->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion
              

                #region Event Detail Entry
                /// <summary>
                /// This function is used to insert & update Event Details Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdate_EventDetails(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_EDID", objSport.EDID);
                        objParams[1] = new SqlParameter("@P_EVENTID", objSport.EVENTID);
                        objParams[2] = new SqlParameter("@P_SPID", objSport.SPID);
                        objParams[3] = new SqlParameter("@P_TEAMID", objSport.TEAMID);
                        objParams[4] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[5] = new SqlParameter("@P_ETID", objSport.ETID);
                        objParams[6] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_EVENT_DETAIL_ENTRY_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_EventDetails->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Sport Type Master
                /// <summary>
                /// This is used for Insert and Update Sport Type Master Entry
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateSportTypeMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TYPID", objSport.TYPID);
                        objParams[1] = new SqlParameter("@P_GAME_TYPE", objSport.GAME_TYPE);
                        objParams[2] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_SPORT_TYPE_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdate_SportMaster_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion


                #region Event Master
                /// <summary>
                /// This is used for Insert and Update the event name.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateEventMaster(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_EVENTID", objSport.EVENTID);
                        objParams[1] = new SqlParameter("@P_EVENTNAME", objSport.EVENTNAME);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objSport.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_ETID", objSport.ETID);
                        objParams[4] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_EVENT_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdateEventMaster->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion



                #region Event Type Master
                /// <summary>
                /// This is used for Insert and Update Event Type Master Entry
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdateEventType(Sport objSport)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ETID", objSport.ETID);
                        objParams[1] = new SqlParameter("@P_EVENT_TYPE_NAME", objSport.EVENT_TYPE_NAME);
                        objParams[2] = new SqlParameter("@P_USERID", objSport.USERID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_EVENT_TYPE_MASTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SportController.AddUpdateEventType->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion


                #endregion

            }
        }
    }
}
