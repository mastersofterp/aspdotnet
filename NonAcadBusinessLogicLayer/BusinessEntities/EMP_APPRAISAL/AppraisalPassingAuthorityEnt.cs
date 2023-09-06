//=================================================================================
// MODULE NAME   : EMLOYEE APPRAISAL
// PAGE NAME     : AppraisalPassingAuthorityEnt.aspx
// CREATION DATE : 10-06-2021
// CREATED BY    : TANU BALGOTE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
            public class AppraisalPassingAuthorityEnt
            {
                #region Approval Authority

                private string _PANAME { get; set; }
                private int _PANO { get; set; }
                private int _UANO { get; set; }
                private int _COLLEGE_NO { get; set; }
                private string _COLLEGE_CODE { get; set; }
                private int _ATID { get; set; }
                private Boolean _COMMON_AUTHORITY { get; set; }
                private int _CREATED_BY { get; set; }
                private int _MODIFIEDBY { get; set; }
                private DateTime _MODIFIED_DATE { get; set; }
                private DateTime _CREATION_DATE { get; set; }
                private int _SESSION_NO { get; set; }

                public string PANAME
                {
                    get { return _PANAME; }
                    set { _PANAME = value; }
                }
                public int PANO
                {
                    get { return _PANO; }
                    set { _PANO = value; }
                }
                public int UANO
                {
                    get { return _UANO; }
                    set { _UANO = value; }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                public int ATID
                {
                    get { return _ATID; }
                    set { _ATID = value; }
                }
                public Boolean COMMON_AUTHORITY
                {
                    get { return _COMMON_AUTHORITY; }
                    set { _COMMON_AUTHORITY = value; }
                }

                public int SESSION_NO
                {
                    get { return _SESSION_NO; }
                    set { _SESSION_NO = value; }
                }
                public int CREATEDBY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }
                public DateTime CREATION_DATE
                {
                    get { return _CREATION_DATE; }
                    set { _CREATION_DATE = value; }
                }
                public DateTime MODIFIED_DATE
                {
                    get { return _MODIFIED_DATE; }
                    set { _MODIFIED_DATE = value; }
                }
                public int MODIFIEDBY
                {
                    get { return _MODIFIEDBY; }
                    set { _MODIFIEDBY = value; }
                }

                #endregion

                #region Approval Passing Path


                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _DEPTNO;
                private int _EMPNO;
                private int _STNO;


                public int PAPNO
                {
                    get
                    {
                        return this._PAPNO;
                    }
                    set
                    {
                        if ((this._PAPNO != value))
                        {
                            this._PAPNO = value;
                        }
                    }
                }

                public int PAN01
                {
                    get
                    {
                        return this._PAN01;
                    }
                    set
                    {
                        if ((this._PAN01 != value))
                        {
                            this._PAN01 = value;
                        }
                    }
                }
                public int PAN02
                {
                    get
                    {
                        return this._PAN02;
                    }
                    set
                    {
                        if ((this._PAN02 != value))
                        {
                            this._PAN02 = value;
                        }
                    }
                }
                public int PAN03
                {
                    get
                    {
                        return this._PAN03;
                    }
                    set
                    {
                        if ((this._PAN03 != value))
                        {
                            this._PAN03 = value;
                        }
                    }
                }

                public int PAN04
                {
                    get
                    {
                        return this._PAN04;
                    }
                    set
                    {
                        if ((this._PAN04 != value))
                        {
                            this._PAN04 = value;
                        }
                    }
                }

                public int PAN05
                {
                    get
                    {
                        return this._PAN05;
                    }
                    set
                    {
                        if ((this._PAN05 != value))
                        {
                            this._PAN05 = value;
                        }
                    }
                }
                public string PAPATH
                {
                    get
                    {
                        return this._PAPATH;
                    }
                    set
                    {
                        if ((this._PAPATH != value))
                        {
                            this._PAPATH = value;
                        }

                    }
                }

                public int DEPTNO
                {
                    get
                    {
                        return this._DEPTNO;
                    }
                    set
                    {
                        if ((this._DEPTNO != value))
                        {
                            this._DEPTNO = value;
                        }
                    }
                }

                public int STNO
                {
                    get { return _STNO; }
                    set { _STNO = value; }
                }

                public int EMPNO
                {
                    get
                    {
                        return this._EMPNO;
                    }
                    set
                    {
                        if ((this._EMPNO != value))
                        {
                            this._EMPNO = value;
                        }
                    }

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
                public string EMPLOYEE_NAME
                {
                    get { return _EMPLOYEE_NAME; }
                    set { _EMPLOYEE_NAME = value; }
                }
                public int SUBDEPTNO
                {
                    get { return _SUBDEPTNO; }
                    set { _SUBDEPTNO = value; }
                }
                public int SUBDESIGNO
                {
                    get { return _SUBDESIGNO; }
                    set { _SUBDESIGNO = value; }
                }

                #endregion

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


            }
        }

    }
}