//================================================================================================
//CREATED BY    : MRUNAL SINGH
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATION DATE : 30-SEP-2014      
//================================================================================================  

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Sport
            {
               
                #region Sport Master
                /// <summary>
                /// This Entity is used for Sport Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>


                private int _SPID = 0;
                private int _USERID = 0;
                private string _SNAME = string.Empty;
                private string _NO_OF_PLAYERS = string.Empty;
                private string _NO_OF_RESERVE = string.Empty;
                private int _TYPID = 0;
               

                #region public member
                public int TYPID
                {
                    get { return _TYPID; }
                    set { _TYPID = value; }
                }
                public int SPID
                {
                    get { return _SPID; }
                    set { _SPID = value; }
                }
                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }

                public string SNAME
                {
                    get { return _SNAME; }
                    set { _SNAME = value; }
                }

                public string NO_OF_PLAYERS
                {
                    get { return _NO_OF_PLAYERS; }
                    set { _NO_OF_PLAYERS = value; }
                }

                public string NO_OF_RESERVE
                {
                    get { return _NO_OF_RESERVE; }
                    set { _NO_OF_RESERVE = value; }
                }

                #endregion

                #endregion

                #region Team Master
                /// <summary>
                /// This Entity is used for Team Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                
                private string _TEAMNAME = string.Empty;
                private int _ACAD_YEAR = 0;
                private int _TEAMID = 0;
                private int _TDID = 0;
                private char _TEAM_TYPE;
            
              #region public member
                public char TEAM_TYPE
                {
                    get { return _TEAM_TYPE; }
                    set { _TEAM_TYPE = value; }
                }
                public int TEAMID
                {
                    get { return _TEAMID; }
                    set { _TEAMID = value; }
                }
               public string TEAMNAME
                {
                    get { return _TEAMNAME; }
                    set { _TEAMNAME = value; }
                }

                public int ACAD_YEAR
                {
                    get { return _ACAD_YEAR; }
                    set { _ACAD_YEAR = value; }
                }
                public int TDID
                {
                    get { return _TDID; }
                    set { _TDID = value; }
                }
  
                #endregion

                #endregion

                #region Role Master
                /// <summary>
                /// This Entity is used for Role Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _ROLEID = 0;
                private string _ROLENAME = string.Empty;
                
                #region public member

                public int ROLEID
                {
                    get { return _ROLEID; }
                    set { _ROLEID = value; }
                }
                
                public string ROLENAME
                {
                    get { return _ROLENAME; }
                    set { _ROLENAME = value; }
                }

                #endregion 

                #endregion
                
                #region Staff Master
                /// <summary>
                /// This Entity is used for Staff Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
               
                private int _POSTID = 0;
                private string _POSTNAME = string.Empty;

                #region public member

                public int POSTID
                {
                    get { return _POSTID; }
                    set { _POSTID = value; }
                }

                public string POSTNAME
                {
                    get { return _POSTNAME; }
                    set { _POSTNAME = value; }
                }

                #endregion

                #endregion

                #region Club/Society Master
                /// <summary>
                /// This Entity is used for Club/Society Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _CLUBID = 0;
                private string _CLUBNAME = string.Empty;
                private string _CLUBADDRESS = string.Empty;

                #region public member

                public int CLUBID
                {
                    get { return _CLUBID; }
                    set { _CLUBID = value; }
                }

                public string CLUBNAME
                {
                    get { return _CLUBNAME; }
                    set { _CLUBNAME = value; }
                }
                public string CLUBADDRESS
                {
                    get { return _CLUBADDRESS; }
                    set { _CLUBADDRESS = value; }
                }

                #endregion

                #endregion

                #region Venue Master
                /// <summary>
                /// This Entity is used for Venue Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _VENUEID = 0;
                private string _VENUENAME = string.Empty;
                private string _VENUEADDRESS = string.Empty;

                #region public member

                public int VENUEID
                {
                    get { return _VENUEID; }
                    set { _VENUEID = value; }
                }

                public string VENUENAME
                {
                    get { return _VENUENAME; }
                    set { _VENUENAME = value; }
                }
                public string VENUEADDRESS
                {
                    get { return _VENUEADDRESS; }
                    set { _VENUEADDRESS = value; }
                }

                #endregion

                #endregion

                #region Medal Master
                /// <summary>
                /// This Entity is used for Medal Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _MEDALID = 0;
                private string _MEDALNAME = string.Empty;
                

                #region public member

                public int MEDALID
                {
                    get { return _MEDALID; }
                    set { _MEDALID = value; }
                }

                public string MEDALNAME
                {
                    get { return _MEDALNAME; }
                    set { _MEDALNAME = value; }
                }
               
                #endregion

                #endregion

                #region Player Master
                /// <summary>
                /// This Entity is used for Player Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                /// 
                private int _PLAYERID = 0;
                private string _PLAYERNAME = string.Empty;
                private string _PLAYER_REGNO = string.Empty;
                private string _PLAYER_ADDRESS = string.Empty;
                private string _PLAYER_CONTACTNO = string.Empty;
                private string _PLAYER_PYEAR = string.Empty;
                private int _IDNO = 0;
                private char _PLAYER_TYPE;

                private string _DEGREE = string.Empty;
                private string _BRANCH = string.Empty;
                private string _SCHEME = string.Empty;
                private int _SEMESTER = 0;

                #region public member



                public string DEGREE
                {
                    get { return _DEGREE; }
                    set { _DEGREE = value; }
                }

                public string BRANCH
                {
                    get { return _BRANCH; }
                    set { _BRANCH = value; }
                }


                public string SCHEME
                {
                    get { return _SCHEME; }
                    set { _SCHEME = value; }
                }

                public int SEMESTER
                {
                    get { return _SEMESTER; }
                    set { _SEMESTER = value; }
                }

                public char PLAYER_TYPE
                {
                    get { return _PLAYER_TYPE; }
                    set { _PLAYER_TYPE = value; }
                }
                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public int PLAYERID
                {
                    get { return _PLAYERID; }
                    set { _PLAYERID = value; }
                }
                public string PLAYERNAME
                {
                    get { return _PLAYERNAME; }
                    set { _PLAYERNAME = value; }
                }
                public string PLAYER_PYEAR 
                {
                    get { return _PLAYER_PYEAR; }
                    set { _PLAYER_PYEAR = value; }
                }

                public string PLAYER_REGNO
                {
                    get { return _PLAYER_REGNO; }
                    set { _PLAYER_REGNO = value; }
                }

                public string  PLAYER_ADDRESS
                {
                    get { return _PLAYER_ADDRESS; }
                    set { _PLAYER_ADDRESS = value; }
                }
                public string  PLAYER_CONTACTNO
                {
                    get { return _PLAYER_CONTACTNO; }
                    set { _PLAYER_CONTACTNO = value; }
                }

                #endregion
                #endregion

                #region Event Creation
                /// <summary>
                /// This Entity is used for Event Creation.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _EVENTID = 0;
                private string _EVENTNAME = string.Empty;
                private DateTime _EVENT_FROMDATE;
                private DateTime _EVENT_TODATE;
                private string _GAME_DETAILS = string.Empty;
                private string _REMARK = string.Empty;
                private int _EORGID = 0;                

               
       

                 
                #region public Event Members
                public int EORGID
                {
                    get { return _EORGID; }
                    set { _EORGID = value; }
                }
            
                public int EVENTID
                {
                    get { return _EVENTID; }
                    set { _EVENTID = value; }
                }
                public string EVENTNAME
                {
                    get { return _EVENTNAME; }
                    set { _EVENTNAME = value; }
                }
                public DateTime EVENT_FROMDATE
                {
                    get { return _EVENT_FROMDATE; }
                    set { _EVENT_FROMDATE = value; }
                }
                public DateTime EVENT_TODATE
                {
                    get { return _EVENT_TODATE; }
                    set { _EVENT_TODATE = value; }
                }
                public string GAME_DETAILS
                {
                    get { return _GAME_DETAILS; }
                    set { _GAME_DETAILS = value; }
                }
                public string REMARK 
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }
                #endregion
                #endregion

                #region Sport Participation
                /// <summary>
                /// This Entity is used for Sport Participation Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _PARTICID = 0;
                private int _NITSTATUS = 0;
                private string _DESCRIPTION;
                private string _FILEPATH;
                private string _FILENAME;
               
                #region public Sport Participation Members
                public int PARTICID
                {
                    get { return _PARTICID; }
                    set { _PARTICID = value; }
                }
                public int NITSTATUS
                {
                    get { return _NITSTATUS; }
                    set { _NITSTATUS = value; }
                }
                public string DESCRIPTION
                {
                    get { return _DESCRIPTION; }
                    set { _DESCRIPTION = value; }
                }
                public string FILEPATH
                {
                    get { return _FILEPATH; }
                    set { _FILEPATH = value; }
                }
                public string FILENAME
                {
                    get { return _FILENAME; }
                    set { _FILENAME = value; }
                }
              
                #endregion
                #endregion

                #region SAC Program Entry
                /// <summary>
                /// This Entity is used for SAC Program Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _SACID = 0;
                

                #region public SAC Entry
                public int SACID
                {
                    get { return _SACID;}
                    set { _SACID = value;}
                }
              

                #endregion
                #endregion

                #region Event Details
                /// <summary>
                /// This Entity is used for Event Detail Entry.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>

                private int _EDID = 0;


                #region public Event Detail Entry
                public int EDID
                {
                    get { return _EDID; }
                    set { _EDID = value; }
                }


                #endregion
                #endregion

                #region Sport Type Master
                /// <summary>
                /// This Entity is used for Sport Type Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
              
                private string _GAME_TYPE = string.Empty;
               
                #region public member

                public string GAME_TYPE
                {
                    get { return _GAME_TYPE; }
                    set { _GAME_TYPE = value; }
                }             

                #endregion

                #endregion

                #region Event Type Master
                /// <summary>
                /// This is used for Event Type Master
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                /// 

                private int _ETID = 0;
                private string _EVENT_TYPE_NAME = string.Empty;
                private int _COLLEGE_NO = 0;
                private string _COLLEGE_NAME = string.Empty;

                public int ETID
                {
                    get { return _ETID; }
                    set { _ETID = value; }
                }

                public string EVENT_TYPE_NAME
                {
                    get { return _EVENT_TYPE_NAME; }
                    set { _EVENT_TYPE_NAME = value; }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }

                public string COLLEGE_NAME
                {
                    get { return _COLLEGE_NAME; }
                    set { _COLLEGE_NAME = value; }
                }


         
                #endregion
            }
        }
    }
}
