using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement
{
    public class OrganisedActivityEntity
    {
        #region Private Members

        private int _organised_activity_id = 0;
        private int _acadamic_year_id = 0;
        private int _create_event_id = 0;
        private string _event_titlte = string.Empty;
        private string _organize_by = string.Empty;
        private string _conduct_by = string.Empty;
        private int _event_level_id = 0;
        private int _ua_no_id = 0;
        private string _ua_no = string.Empty;
        private DateTime _sdate = DateTime.Now;
        private DateTime _edate = DateTime.Now;
        private string _venue = string.Empty;
        private string _event_mode = string.Empty;
        private int _duration_id = 0;
        private int _student_participants_no = 0;
        private int _teachers_staff_participants_no = 0;
        private string _funded_by = string.Empty;
        private decimal _sanctioned_amount = 0.0m;
        private string _converner = string.Empty;
        private string _co_ordinator = string.Empty;
        private string _members = string.Empty;
        private string _activity_title = string.Empty;
       
         private int _technical_type_id = 0;
        #endregion
         public int organised_activity_id
         {
             get { return _organised_activity_id; }
             set { _organised_activity_id = value; }
         }

         public int create_event_id
           {
               get { return _create_event_id; }
               set { _create_event_id = value; }
           }

           public int acadamic_year_id
           {
               get { return _acadamic_year_id; }
               set { _acadamic_year_id = value; }
           }
           
           public string event_titlte
           {
               get { return _event_titlte; }
               set { _event_titlte = value; }
           }
           public string organize_by
           {
               get { return _organize_by; }
               set { _organize_by = value; }
           }
           public string conduct_by
           {
               get { return _conduct_by; }
               set { _conduct_by = value; }
           }
           public int event_level_id
           {
               get { return _event_level_id; }
               set { _event_level_id = value; }
           }
           public int ua_no_id
           {
               get { return _ua_no_id; }
               set { _ua_no_id = value; }
           }
           public DateTime SDate
           {
               get { return _sdate; }
               set { _sdate = value; }
           }

           public DateTime EDate
           {
               get { return _edate; }
               set { _edate = value; }
           }
           public string venue
           {
               get { return _venue; }
               set { _venue = value; }
           }
           public string event_mode
           {
               get { return _event_mode; }
               set { _event_mode = value; }
           }
           public int duration_id
           {
               get { return _duration_id; }
               set { _duration_id = value; }
           }
           
          
          public int technical_type_id
           {
               get { return _technical_type_id; }
               set { _technical_type_id = value; }
           }
           
          public int student_participants_no
           {
               get { return _student_participants_no; }
               set { _student_participants_no = value; }
           }
          public int teachers_staff_participants_no
           {
               get { return _teachers_staff_participants_no; }
               set { _teachers_staff_participants_no = value; }
           }

           public string funded_by
           {
               get { return _funded_by; }
               set { _funded_by = value; }
           }
           public decimal sanctioned_amount
           {
               get { return _sanctioned_amount; }
               set { _sanctioned_amount = value; }
           }
         public string converner
           {
               get { return _converner; }
               set { _converner = value; }
           }
        public string co_ordinator
           {
               get { return _co_ordinator; }
               set { _co_ordinator = value; }
           }
        public string members
           {
               get { return _members; }
               set { _members = value; }
           }
        public string activity_title
           {
               get { return _activity_title; }
               set { _activity_title = value; }
           }
           public int OrganizationId
           {
               get;
               set;
           }
  }

    }
