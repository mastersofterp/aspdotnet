using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
           public class TPLASTQUAL
           {
               #region Private Members
               int _idno = 0;
               //int _qualifyno = 0;
               //string _year_of_exam = string.Empty;
               //int _no_of_suboffer = 0;
               //int _no_of_subpass = 0;
               //int _no_of_backlog = 0;
               //string _remark = string.Empty;
               //string _attempt = string.Empty;
               //decimal _per = 0.0m;
               //string _college_code = string.Empty;

               string _Examnos = string.Empty;
               string _PassingYrs = string.Empty;
               string _OfferSubjects = string.Empty;               
               string _PassedSubjects = string.Empty;               
               string _Backlogs = string.Empty;
               string _Percentages = string.Empty;
               string _Remarks = string.Empty;               
               string _Attempts = string.Empty;
               string _Semesetername = string.Empty;
               string _Board = string.Empty;

               //10-12 INSERTION
               string _mrkObt = string.Empty;
               string _mrkOutOf = string.Empty;
               string _percentile = string.Empty;
               string _cgpa = string.Empty;
               string _rollno = string.Empty;
               string _grade = string.Empty;

               //10-12 UPDATION
               string _Examnos_1012 = string.Empty;
               string _PassingYrs_1012 = string.Empty;
               string _Attempts_1012 = string.Empty;
               string _Board_1012 = string.Empty;
               string _mrkObt_1012 = string.Empty;
               string _mrkOutOf_1012 = string.Empty;
               string _Percentages_1012 = string.Empty;
               string _percentile_1012 = string.Empty;
               string _rollno_1012 = string.Empty;
               string _grade_1012 = string.Empty;
               string _cgpa_1012 = string.Empty;

               #endregion

               #region Public Properties

               //public decimal Per
               //{
               //    get { return _per; }
               //    set { _per = value; }
               //}
               //public string College_code
               //{
               //    get { return _college_code; }
               //    set { _college_code = value; }
               //}
               //public string Attempt
               //{
               //    get { return _attempt; }
               //    set { _attempt = value; }
               //}
               //public string Year_of_exam
               //{
               //    get { return _year_of_exam; }
               //    set { _year_of_exam = value; }
               //}
               //public int Qualifyno
               //{
               //    get { return _qualifyno; }
               //    set { _qualifyno = value; }
               //}
               public int Idno
               {
                   get { return _idno; }
                   set { _idno = value; }
               }

               //public int No_of_SubOffer
               //{
               //    get { return _no_of_suboffer; }
               //    set { _no_of_suboffer = value; }
               //}
               //public int No_of_SubPass
               //{
               //    get {  return _no_of_subpass; }
               //    set { _no_of_subpass = value; }
               //}
               //public int No_of_Backlog
               //{
               //    get { return _no_of_backlog; }
               //    set { _no_of_backlog = value; }
               //}
               //public String Remark
               //{
               //    get { return _remark; }
               //    set { _remark = value; }
               //}
               public string ExamNos
               {
                   get { return _Examnos; }
                   set { _Examnos = value; }
               }
               public string PassingYrs
               {
                   get { return _PassingYrs; }
                   set { _PassingYrs = value; }
               }
               public string OfferSubjects
               {
                   get { return _OfferSubjects; }
                   set { _OfferSubjects = value; }
               }
               public string PassedSubjects
               {
                   get { return _PassedSubjects; }
                   set { _PassedSubjects = value; }
               }
               public string Backlogs
               {
                   get { return _Backlogs; }
                   set { _Backlogs = value; }
               }
               public string Percentages
               {
                   get { return _Percentages; }
                   set { _Percentages = value; }
               }
               public string Cgpas
               {
                   get { return _cgpa; }
                   set { _cgpa = value; }
               }
               public string Remarks
               {
                   get { return _Remarks; }
                   set { _Remarks = value; }
               }
               public string Attempts
               {
                   get { return _Attempts; }
                   set { _Attempts = value; }
               }
               public string Semesetername
               {
                   get { return _Semesetername; }
                   set { _Semesetername = value; }
               }
               public string Board
               {
                   get { return _Board; }
                   set { _Board = value; }
               }

               public string MrkObt
               {
                   get { return _mrkObt ; }
                   set { _mrkObt = value; }
               }
               public string MrkOutOf
               {
                   get { return _mrkOutOf ; }
                   set { _mrkOutOf = value; }
               }
               public string Percentile
               {
                   get { return _percentile ; }
                   set { _percentile = value; }
               }
               public string Grade
               {
                   get { return _grade; }
                   set { _grade = value; }
               }
               public string RollNo
               {
                   get { return _rollno; }
                   set { _rollno = value; }
               }

               //10-12 UPDATION
               public string Examnos_1012
               {
                   get { return _Examnos_1012; }
                   set { _Examnos_1012 = value; }
               }
               public string PassingYrs_1012
               {
                   get { return _PassingYrs_1012; }
                   set { _PassingYrs_1012 = value; }
               }
               public string Attempts_1012
               {
                   get { return _Attempts_1012; }
                   set { _Attempts_1012 = value; }
               }
               public string Board_1012
               {
                   get { return _Board_1012; }
                   set { _Board_1012 = value; }
               }

               public string mrkObt_1012
               {
                   get { return _mrkObt_1012; }
                   set { _mrkObt_1012 = value; }
               }
               public string mrkOutOf_1012
               {
                   get { return _mrkOutOf_1012; }
                   set { _mrkOutOf_1012 = value; }
               }
               public string Percentages_1012
               {
                   get { return _Percentages_1012; }
                   set { _Percentages_1012 = value; }
               }
               public string percentile_1012
               {
                   get { return _percentile_1012; }
                   set { _percentile_1012 = value; }
               }
               public string rollno_1012
               {
                   get { return _rollno_1012; }
                   set { _rollno_1012 = value; }
               }

               public string grade_1012
               {
                   get { return _grade_1012; }
                   set { _grade_1012 = value; }
               }
               //Added on date 15/07/2016
               public string Cgpas_1012
               {
                   get { return _cgpa_1012; }
                   set { _cgpa_1012 = value; }
               }
               #endregion
           }
        }
    }
}
