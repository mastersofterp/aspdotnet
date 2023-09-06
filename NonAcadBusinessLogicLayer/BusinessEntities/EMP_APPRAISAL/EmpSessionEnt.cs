//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [EMP APPRAISAL]                                  
// CREATION DATE : 6-03-2021                                                        
// CREATED BY    : TANU BALGOTE                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class EmpSessionEnt
            {
                #region Appraisal Session
                private int _SESSIONS_ID { get; set; }
                private string _SESSIONS_NAME { get; set; }
                private string _SESSIONS_SHORTNAME { get; set; }
                private DateTime _FROMDATES { get; set; }
                private DateTime _TODATES { get; set; }
                private System.Nullable<bool> _ISACTIVES { get; set; }
                private System.Nullable<char> _IS_SPECIALS { get; set; }
                private int _CREATEDBYS { get; set; }
                private DateTime _MODIFIED_DATES { get; set; }
                private int _MODIFIEDBYS { get; set; }



                public int SESSION_ID
                {
                    get { return _SESSIONS_ID; }
                    set { _SESSIONS_ID = value; }
                }
                public string SESSION_NAME
                {
                    get { return _SESSIONS_NAME; }
                    set { _SESSIONS_NAME = value; }
                }
                public string SESSION_SHORTNAME
                {
                    get { return _SESSIONS_SHORTNAME; }
                    set { _SESSIONS_SHORTNAME = value; }
                }
                public DateTime FROMDATE
                {
                    get { return _FROMDATES; }
                    set { _FROMDATES = value; }
                }
                public DateTime TODATE
                {
                    get { return _TODATES; }
                    set { _TODATES = value; }
                }
                public System.Nullable<bool> ISACTIVE
                {
                    get { return _ISACTIVES; }
                    set { _ISACTIVES = value; }
                }
                public System.Nullable<char> IS_SPECIAL
                {
                    get { return _IS_SPECIALS; }
                    set { _IS_SPECIALS = value; }
                }
                public int CREATEDBY
                {
                    get { return _CREATEDBYS; }
                    set { _CREATEDBYS = value; }
                }
                public int MODIFIEDBY
                {
                    get { return _MODIFIEDBYS; }
                    set { _MODIFIEDBYS = value; }
                }
                public DateTime MODIFIED_DATE
                {
                    get { return _MODIFIED_DATES; }
                    set { _MODIFIED_DATES = value; }
                }

                #endregion

                #region Appraisal Session Activity

                private int _SESSION_ACTIVITY_NOS { get; set; }
                private int _SESSION_NOS { get; set; }
                private int _SESSION_TYPE_NOS { get; set; }
                private DateTime _STARTDATES { get; set; }
                private DateTime _ENDDATES { get; set; }
                private System.Nullable<bool> _IS_STARTEDS { get; set; }
                private int _COLLEGE_NOS { get; set; }
                private string _COLLEGE_CODES { get; set; }



                public int SESSION_ACTIVITY_NO
                {
                    get { return _SESSION_ACTIVITY_NOS; }
                    set { _SESSION_ACTIVITY_NOS = value; }
                }
                public int SESSION_NO
                {
                    get { return _SESSION_NOS; }
                    set { _SESSION_NOS = value; }
                }
                public int SESSION_TYPE_NO
                {
                    get { return _SESSION_TYPE_NOS; }
                    set { _SESSION_TYPE_NOS = value; }
                }
                public DateTime STARTDATE
                {
                    get { return _STARTDATES; }
                    set { _STARTDATES = value; }
                }
                public DateTime ENDDATE
                {
                    get { return _ENDDATES; }
                    set { _ENDDATES = value; }
                }
                public System.Nullable<bool> IS_STARTED
                {
                    get { return _IS_STARTEDS; }
                    set { _IS_STARTEDS = value; }
                }
                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NOS; }
                    set { _COLLEGE_NOS = value; }
                }

                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODES; }
                    set { _COLLEGE_CODES = value; }
                }



                #endregion

                #region Employee Appraisal

                private int _EMPLOYEE_ID = 0;
                private string _EMPLOYEE_NAME = string.Empty;
                private int _SUBDEPTNO = 0;
                private int _SUBDESIGNO = 0;

                public int EMPLOYEE_ID
                {
                    get { return _EMPLOYEE_ID; }
                    set { _EMPLOYEE_ID = value; }
                }
                public int SUBDEPTNO
                {
                    get { return _SUBDEPTNO; }
                    set { _SUBDEPTNO = value; }
                }
                public string EMPLOYEE_NAME
                {
                    get { return _EMPLOYEE_NAME; }
                    set { _EMPLOYEE_NAME = value; }
                }                
                 
                #region PUBLISHED JOURNAL

                private string _TITLE_PAGE_NO = string.Empty;
                private string _JOURNAL = string.Empty;
                private string _JOURNALISBNUMBER = string.Empty;
                private string _PEER_REVIEWED = string.Empty;
                private string _IMPACT_FACTOR = string.Empty;
                private int __NO_OF_CO_AUTHORS = 0;


                public string TITLE_PAGE_NO
                {
                    get { return _TITLE_PAGE_NO; }
                    set { _TITLE_PAGE_NO = value; }
                }
                public string JOURNAL
                {
                    get { return _JOURNAL; }
                    set { _JOURNAL = value; }
                }
                public string JOURNALISBNUMBER
                { 
                    get { return _JOURNALISBNUMBER; }
                    set { _JOURNALISBNUMBER = value; }
                }

                public string PEER_REVIEWED
                {
                    get { return _PEER_REVIEWED; }
                    set { _PEER_REVIEWED = value; }
                }

                public string IMPACT_FACTOR
                {
                    get { return _IMPACT_FACTOR; }
                    set { _IMPACT_FACTOR = value; }
                }
                public int NO_OF_CO_AUTHORS
                {
                    get { return _NO_OF_CO_AUTHORS; }
                    set { _NO_OF_CO_AUTHORS = value; }
                }
                


                #endregion

                #region PUBLISHED BOOKS

                private string _BOOK_TITLE = string.Empty;
                private string _PUBLISHER_NAME = string.Empty;
                private string _BOOK_NATION = string.Empty;
                private string _BOOKISBNUMBER = string.Empty;
                private int _NO_OF_CO_AUTHORS = 0;

                public string BOOK_TITLE
                {
                    get { return _BOOK_TITLE; }
                    set { _BOOK_TITLE = value; }
                }
                public string PUBLISHER_NAME
                {
                    get { return _PUBLISHER_NAME; }
                    set { _PUBLISHER_NAME = value; }
                }
                public string BOOK_NATION
                {
                    get { return _BOOK_NATION; }
                    set { _BOOK_NATION = value; }
                }
                public string BOOKISBNUMBER
                {
                    get { return _BOOKISBNUMBER; }
                    set { _BOOKISBNUMBER = value; }
                }


                #endregion

                #region PUBLISHED CHAPTERS

                private int _NO_OF_CHAPTERS = 0;

                public int NO_OF_CHAPTERS
                {
                    get { return _NO_OF_CHAPTERS; }
                    set { _NO_OF_CHAPTERS = value; }
                }


                #endregion

                #region PAPERS IN CONFERENCE


                private string _PUBLISH_CONFERENCE = string.Empty;
                private string _PAPER_ABSTRACT = string.Empty;
                private string _CONFERISBNUMBER = string.Empty;

                public string PUBLISH_CONFERENCE
                {
                    get { return _PUBLISH_CONFERENCE; }
                    set { _PUBLISH_CONFERENCE = value; }
                }
                public string PAPER_ABSTRACT
                {
                    get { return _PAPER_ABSTRACT; }
                    set { _PAPER_ABSTRACT = value; }
                }

                public string CONFERISBNUMBER
                {
                    get { return _CONFERISBNUMBER; }
                    set { _CONFERISBNUMBER = value; }
                }


                #endregion

                #region AVISHKAR/ANY OTHER

                private string _TITLE_OF_PAPER = string.Empty;
                private string _AVISHKAR = string.Empty;
                private string _PRIZE_WON = string.Empty;

                public string TITLE_OF_PAPER
                {
                    get { return _TITLE_OF_PAPER; }
                    set { _TITLE_OF_PAPER = value; }
                }
                public string AVISHKAR
                {
                    get { return _AVISHKAR; }
                    set { _AVISHKAR = value; }
                }
                public string PRIZE_WON
                {
                    get { return _PRIZE_WON; }
                    set { _PRIZE_WON = value; }
                }

                #endregion

                #region RESEARCH PROJECT AND CONSULTANCIES

                private string _TITLE = string.Empty;
                private string _AGENCY = string.Empty;
                private string _PERIOD = string.Empty;
                private string _TYPE_OF_PROJECT = string.Empty;
                private string _AMOUNT = string.Empty;
                private string _INVESTIGATOR = string.Empty;
                private int _NO_OF_CO_INVESTOR = 0;

                public string TITLE
                {
                    get { return _TITLE; }
                    set { _TITLE = value; }
                }
                public string AGENCY
                {
                    get { return _AGENCY; }
                    set { _AGENCY = value; }
                }
                public string PERIOD
                {
                    get { return _PERIOD; }
                    set { _PERIOD = value; }
                }
                public string TYPE_OF_PROJECT
                {
                    get { return _TYPE_OF_PROJECT; }
                    set { _TYPE_OF_PROJECT = value; }
                }
                public string AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
                public string INVESTIGATOR
                {
                    get { return _INVESTIGATOR; }
                    set { _INVESTIGATOR = value; }
                }
                public int NO_OF_CO_INVESTOR
                {
                    get { return _NO_OF_CO_INVESTOR; }
                    set { _NO_OF_CO_INVESTOR = value; }
                }



                #endregion

                #endregion

            }

        }
    }
}