using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ApplicationProcess
    {
        #region Private Member
        private int _batchno = 0;
        private int _ugpgot = 0;
        private int _degreeno = 0;
        private int _branchno = 0;
        private int _application_stage = 0;
        private string _userno = string.Empty;
        private int _reg_status = 0;
        private int _ua_type = 0;
        private int _ua_no = 0;
        private int _email_send_by = 0;
        private int _sms_send_by = 0;
        private string _exam_schedule = string.Empty;
        private int _schedule_no = 0;
        private string _schedule_nos = string.Empty;
        private char _process_type;
        private int _user_no = 0;
        private string _documentno = string.Empty;
        private string _document_from = string.Empty;
        private int _qualify_no = 0;
        private Boolean _isverify;
        private string _description = string.Empty;
        // 
        private int _faculty_uano = 0;
        private string _login_url = string.Empty;
        private string _e_subject = string.Empty;
        private string _ew_body = string.Empty;
        private int _template_id = 0;

        #endregion
        #region Public Member
        public int BatchNo
        {
            get { return _batchno; }
            set { _batchno = value; }
        }
        public int UGPGOT
        {
            get { return _ugpgot; }
            set { _ugpgot = value; }
        }
        public int DegreeNo
        {
            get { return _degreeno; }
            set { _degreeno = value; }
        }
        public int BranchNo
        {
            get { return _branchno; }
            set { _branchno = value; }
        }
        public int ApplicationStage
        {
            get { return _application_stage; }
            set { _application_stage = value; }
        }
        public string UserNo
        {
            get { return _userno; }
            set { _userno = value; }
        }
        public int RegStatus
        {
            get { return _reg_status; }
            set { _reg_status = value; }
        }

        public int UaType
        {
            get { return _ua_type; }
            set { _ua_type = value; }
        }
        public int UaNo
        {
            get { return _ua_no; }
            set { _ua_no = value; }
        }
        public int EmailSendBy
        {
            get { return _email_send_by; }
            set { _email_send_by = value; }
        }
        public int SmsSendBy
        {
            get { return _sms_send_by; }
            set { _sms_send_by = value; }
        }
        public string ExamSchedule
        {
            get { return _exam_schedule; }
            set { _exam_schedule = value; }
        }
        public int ScheduleNo
        {
            get { return _schedule_no; }
            set { _schedule_no = value; }
        }
        public string ScheduleNos
        {
            get { return _schedule_nos; }
            set { _schedule_nos = value; }
        }
        public char ProcessType
        {
            get { return _process_type; }
            set { _process_type = value; }
        }
        public int Userno
        {
            get { return _user_no; }
            set { _user_no = value; }
        }
        public string DocumentNo
        {
            get { return _documentno; }
            set { _documentno = value; }
        }
        public string DocumentFrom
        {
            get { return _document_from; }
            set { _document_from = value; }
        }
        public Boolean IsVerify
        {
            get { return _isverify; }
            set { _isverify = value; }
        }
        public int QualifyNo
        {
            get { return _qualify_no; }
            set { _qualify_no = value; }
        }

        public int FacultyUaNo
        {
            get { return _faculty_uano; }
            set { _faculty_uano = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string LoginUrl
        {
            get { return _login_url; }
            set { _login_url = value; }
        }
        public string ESubject
        {
            get { return _e_subject; }
            set { _e_subject = value; }
        }
        public string EWBody
        {
            get { return _ew_body; }
            set { _ew_body = value; }
        }
        public int TemplateId
        {
            get { return _template_id; }
            set { _template_id = value; }
        } 

        #endregion
    }
}
