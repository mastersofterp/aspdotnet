using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement
{
    public class CreateEventEntity
    {
        #region Private Members

        private int _create_event_id = 0;
        private int _acadamic_year_id = 0;
        private int _event_category_id = 0;
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
        private bool _prizes = false;
        private decimal _winner = 0.0m;
        private decimal _runner_up = 0.0m;
        private decimal _third_place = 0.0m;
        private string _funded_by = string.Empty;
        private bool _IsActive = false;
        private string _file_name = string.Empty;
        private string _file_path = string.Empty;
        private int _activity_id = 0;  //Added by Nikhil Shende dt:-12/09/2022
        private DateTime _regdate = DateTime.Now;  //Added by Nikhil Shende dt:-12/09/2022
        private string _Time = string.Empty;
        private int _reg_capacity = 0;//Added by Arjun Patil dt:-28/11/2022
        #endregion


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
        public int event_category_id
        {
            get { return _event_category_id; }
            set { _event_category_id = value; }
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
        public bool prizes
        {
            get { return _prizes; }
            set { _prizes = value; }
        }
        public decimal winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public decimal runner_up
        {
            get { return _runner_up; }
            set { _runner_up = value; }
        }
        public decimal third_place
        {
            get { return _third_place; }
            set { _third_place = value; }
        }
        public string funded_by
        {
            get { return _funded_by; }
            set { _funded_by = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string file_name
        {
            get { return _file_name; }
            set { _file_name = value; }
        }
        public string file_path
        {
            get { return _file_path; }
            set { _file_path = value; }
        }
        public int OrganizationId
        {
            get;
            set;
        }
        public int Activity_id     // Added by Nikhil Shende dt:- 07/09/2022
        {
            get { return _activity_id; }
            set { _activity_id = value; }
        }
        public DateTime RegDate    //Added by Nikhil Shende dt:-12/09/2022
        {
            get { return _regdate; }
            set { _regdate = value; }
        }
        public string Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        public int RegCapacity
        {
            get { return _reg_capacity; }
            set { _reg_capacity = value; }
        }
    }
}
