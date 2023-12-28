using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class HostelInOutReq
    {
        private int _idno=0;
        private int _regno=0;
        private Nullable<System.DateTime> _indate;
        private string _intime;
        private string _outtime;
        private Nullable<System.DateTime> _outdate;
        private string _studentname;
        private string _gatepassno;
        private string _purpose;
        private string _remarks;
        private Nullable<System.DateTime> _applydate;
        private string _status;
        private int _hgpid;
        private string _infromto;
        private string _studentemail;
        private string _studentmobile;
        private string _fathername;
        private string _fatheremail;
        private string _fathermobile;
        private string _motherName;
        private string _motheremail;
        private string _mothermobile;
        private string _UploadedfileName;
        private string _UploadedFile;
        private string _firstapprovername;
        private string _secondapprovername;
        private string _thirdapprovarname;
        private string _fourthapprovarname;
        private string _firstapprovaldesignation;
        private string _secondapprovaldesignation;
        private string _thirdapprovaldesignation;
        private string _fourthapprovaldesignation;
        private string _firstrapprovalstatus;
        private string _secondapprovalstatus;
        private string _thirdapprovalstatus;
        private string _fourthapprovalstatus;
        private Nullable<System.DateTime> _firstapprovaldate;
        private Nullable<System.DateTime> _secondapprovaldate;
        private Nullable<System.DateTime> _thirdapprovaldate;
        private Nullable<System.DateTime> _fourthapprovaldate;

        public Nullable<System.DateTime> Fourthapprovaldate
        {
            get { return _fourthapprovaldate; }
            set { _fourthapprovaldate = value; }
        }

        public Nullable<System.DateTime> Thirdapprovaldate
        {
            get { return _thirdapprovaldate; }
            set { _thirdapprovaldate = value; }
        }

        public Nullable<System.DateTime> Secondapprovaldate
        {
            get { return _secondapprovaldate; }
            set { _secondapprovaldate = value; }
        }

        public Nullable<System.DateTime> Firstapprovaldate
        {
            get { return _firstapprovaldate; }
            set { _firstapprovaldate = value; }
        }
     

        public string Fourthapprovalstatus
        {
            get { return _fourthapprovalstatus; }
            set { _fourthapprovalstatus = value; }
        }

        public string Thirdapprovalstatus
        {
            get { return _thirdapprovalstatus; }
            set { _thirdapprovalstatus = value; }
        }

        public string Secondapprovalstatus
        {
            get { return _secondapprovalstatus; }
            set { _secondapprovalstatus = value; }
        }

        public string Firstrapprovalstatus
        {
            get { return _firstrapprovalstatus; }
            set { _firstrapprovalstatus = value; }
        }

        public string FourthApprovalDesignation
        {
            get { return _fourthapprovaldesignation; }
            set { _fourthapprovaldesignation = value; }
        }

        public string Thirdapprovaldesignation
        {
            get { return _thirdapprovaldesignation; }
            set { _thirdapprovaldesignation = value; }
        }

        public string Secondapprovaldedignation
        {
            get { return _secondapprovaldesignation; }
            set { _secondapprovaldesignation = value; }
        }

        public string Firstapprovaldesignation
        {
            get { return _firstapprovaldesignation; }
            set { _firstapprovaldesignation = value; }
        }

        public string Fourthapprovarname
        {
            get { return _fourthapprovarname; }
            set { _fourthapprovarname = value; }
        }

        public string Thirdapprovarname
        {
            get { return _thirdapprovarname; }
            set { _thirdapprovarname = value; }
        }

        public string Secondapprovername
        {
            get { return _secondapprovername; }
            set { _secondapprovername = value; }
        }
        public string Firstapprovername
        {
            get { return _firstapprovername; }
            set { _firstapprovername = value; }
        }
        public string UploadedFile
        {
            get { return _UploadedFile; }
            set { _UploadedFile = value; }
        }

        public string UploadedfileName
        {
            get { return _UploadedfileName; }
            set { _UploadedfileName = value; }
        }

        public string Mothermobile
        {
            get { return _mothermobile; }
            set { _mothermobile = value; }
        }

        public string Motheremail
        {
            get { return _motheremail; }
            set { _motheremail = value; }
        }

        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }

        public string Fathermobile
        {
            get { return _fathermobile; }
            set { _fathermobile = value; }
        }

        public string Fatheremail
        {
            get { return _fatheremail; }
            set { _fatheremail = value; }
        }

        public string Fathername
        {
            get { return _fathername; }
            set { _fathername = value; }
        }
        

        public string Studentmobile
        {
          get { return _studentmobile; }
          set { _studentmobile = value; }
        }
        public string Studentemail
        {
            get { return _studentemail; }
            set { _studentemail = value; }
        }
        public string Infromto
        {
            get { return _infromto; }
            set { _infromto = value; }
        }

        public int Hgpid
        {
            get { return _hgpid; }
            set { _hgpid = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Nullable<System.DateTime> Applydate
        {
            get { return _applydate; }
            set { _applydate = value; }
        }

        public int Idno
        {
            get { return _idno; }
            set { _idno = value; }
        }

        public int Regno
        {
            get { return _regno; }
            set { _regno = value; }
        }

        public Nullable<System.DateTime> Indate
        {
            get { return _indate; }
            set { _indate = value; }
        }

        public string Intime
        {
            get { return _intime; }
            set { _intime = value; }
        }

        public string Outtime
        {
            get { return _outtime; }
            set { _outtime = value; }
        }

        public Nullable<System.DateTime> Outdate
        {
            get { return _outdate; }
            set { _outdate = value; }
        }

        public string Studentname
        {
            get { return _studentname; }
            set { _studentname = value; }
        }

        public string Gatepassno
        {
            get { return _gatepassno; }
            set { _gatepassno = value; }
        }

        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
    }
}
