//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES FILE [SESSION MASTER]                              
// CREATION DATE : 18-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Session
            {
                #region Private Members
                private int _sessionno = 0;
                private DateTime _session_sdate = DateTime.Now;
                private DateTime _session_edate = DateTime.Now;
                private string _session_name = string.Empty;
                private string _session_pname = string.Empty;
                private string _degree_name = string.Empty;
                private int _degree_no = 0;
                private int _odd_even = 0;
                //private int _seq_no = 0;
                private int _examtype = 0;
                private int _currentsessionno = 0;
                private int _holidayno = 0;
                private string _currentsession_name = string.Empty;
                private string _event_name = string.Empty;
                private string _event_detail = string.Empty;

                private string _sessname_hindi = string.Empty;
                private int _session_status = 0;
                private string _college_code = string.Empty;
                private bool _flock = false;

                private string _academic_year = string.Empty;
                private int _is_holiday_event = 0;//added by S.Patil - 01 Nov 2019

                private bool _IsActive = false; //Added by Mahesh on Dated 21-01-2021
                private string _PROVISIONAL_CERTIFICATE_SESSION_NAME; //Added by Mahesh on Dated 29-07-2021

                private int _college_id = 0; //Added By Rishabh on 25/01/2022

                private int _SrNo = 0;
                private int _Mark_Out_Of = 0;
                private int _Weightage = 0;
                private string _Assesment_Category = string.Empty;
                private string _Assesment_Name = string.Empty;

               
                private string _college_id_str = string.Empty; //add by maithili 19/08/2022
                private int _sequence_no = 0; //For Session to add Sequence Number
                private string _projectname = string.Empty;
                private string _projectid = string.Empty;  //Added by Gunesh Mohane on 26-03-2024
                private string _selection = string.Empty;  //Added by Gunesh Mohane on 26-03-2024
                #endregion

                #region Public Properties
                public string College_Id_str //Added By maithili 19/08/2022
                {
                    get { return _college_id_str; }
                    set { _college_id_str = value; }
                }
               
                //added by S.Patil - 01 Nov 2019
                public int Holiday_Event
                {
                    get { return _is_holiday_event; }
                    set { _is_holiday_event = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }

                public int CurrentSession
                {
                    get { return _currentsessionno; }
                    set { _currentsessionno = value; }
                }

                public int Holiday_No
                {
                    get { return _holidayno; }
                    set { _holidayno = value; }
                }

                public string CurrentSession_Name
                {
                    get { return _currentsession_name; }
                    set { _currentsession_name = value; }
                }

                public string Event_Name//For HOliday Entry page
                {
                    get { return _event_name; }
                    set { _event_name = value; }
                }

                public string Event_Detail//For HOliday Entry page
                {
                    get { return _event_detail; }
                    set { _event_detail = value; }
                }

                public DateTime Session_SDate
                {
                    get { return _session_sdate; }
                    set { _session_sdate = value; }
                }

                public DateTime Session_EDate
                {
                    get { return _session_edate; }
                    set { _session_edate = value; }
                }

                public string Session_Name
                {
                    get { return _session_name; }
                    set { _session_name = value; }
                }

                public string Session_PName
                {
                    get { return _session_pname; }
                    set { _session_pname = value; }
                }

                public string Degree_Name
                {
                    get { return _degree_name; }
                    set { _degree_name = value; }
                }
                public int Degree_No
                {
                    get { return _degree_no; }
                    set { _degree_no = value; }
                }

                public int Odd_Even
                {
                    get { return _odd_even; }
                    set { _odd_even = value; }
                }

                //public int Seq_No
                //{
                //    get { return _seq_no; }
                //    set { _seq_no = value; }
                //}



                public string Sessname_hindi
                {
                    get { return _sessname_hindi; }
                    set { _sessname_hindi = value; }
                }

                public int Session_status
                {
                    get { return _session_status; }
                    set { _session_status = value; }
                }

                public string College_code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }

                public int ExamType
                {
                    get { return _examtype; }
                    set { _examtype = value; }
                }

                public bool Flock
                {
                    get { return _flock; }
                    set { _flock = value; }
                }

                public string academic_year
                {
                    get { return _academic_year; }
                    set { _academic_year = value; }
                }

                public bool IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }

                public string PROVISIONAL_CERTIFICATE_SESSION_NAME
                {
                    get { return _PROVISIONAL_CERTIFICATE_SESSION_NAME; }
                    set { _PROVISIONAL_CERTIFICATE_SESSION_NAME = value; }
                }

                public int College_Id //Added By Rishabh on 25/01/2022
                {
                    get { return _college_id; }
                    set { _college_id = value; }
                }
                public int MarkOutOf
                {
                    get { return _Mark_Out_Of; }
                    set { _Mark_Out_Of = value; }
                }
                public int Weightage
                {
                    get { return _Weightage; }
                    set { _Weightage = value; }
                }
                public string AssesmentCategory
                {
                    get { return _Assesment_Category; }
                    set { _Assesment_Category = value; }
                }
                public string AssesmentName
                {
                    get { return _Assesment_Name; }
                    set { _Assesment_Name = value; }
                }
                
                

               

                //Added by Vinay Mishra on 16/06/2023
                public int sequence_no
                {
                    get { return _sequence_no; }
                    set { _sequence_no = value; }
                }
               
                public string ProjectName
                {
                    get { return _projectname; }
                    set { _projectname = value; }
                }

                //Added by  Shailendra K on dated 13.03.2024 as per T-55537 & 55470
                private int _academicYearID;
                public int AcademicYearID
                {
                    get { return _academicYearID; }
                    set { _academicYearID = value; }
                }

                private int _studyPattern = 0;
                public int StudyPatternNo
                {
                    get { return _studyPattern; }
                    set { _studyPattern = value; }
                }

                //Added by Gunesh Mohane on 26-03-2024
                public string ProjectID
                {
                    get { return _projectid; }
                    set { _projectid = value; }
                }

                //Added by Gunesh Mohane on 26-03-2024
                public string Selection
                {
                    get { return _selection; }
                    set { _selection = value; }
                }
                #endregion
            }
        }
    }
}
