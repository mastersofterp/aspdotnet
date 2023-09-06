using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DocumentAcad
    {
        #region Private Member
        //Modified By Rishabh

        private string _documentname = string.Empty;
        private int _documentno = 0;
        private int _degree = 0;
        private int _ptype = 0;
        private int _collegeCode = 0;

        private int _idtype = 0; //added by deepali on 20/05/2021

        private int _documentsrno = 0; //added by deepali on 20/05/2021
        #endregion

        #region Public Member

        //Modified By Rishabh on 29/10/2021
        public int chkstatus
        {
            get;
            set;
        }

        public int MandtStatus
        {
            get;
            set;
        }

        public string Documentname
        {
            get { return _documentname; }
            set { _documentname = value; }
        }

        public int Documentno
        {
            get { return _documentno; }
            set { _documentno = value; }
        }

        public int Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }

        public int Ptype
        {
            get { return _ptype; }
            set { _ptype = value; }
        }

        public int CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int Idtype
        {
            get { return _idtype; }
            set { _idtype = value; }
        }

        public int DocumentSrno
        {
            get { return _documentsrno; }
            set { _documentsrno = value; }
        }

        #endregion
    }
    
}
