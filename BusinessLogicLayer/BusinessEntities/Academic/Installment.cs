using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Installment
            {
                #region Private Members
                private int _idno = 0;
                private int _receiptno = 0;
                private int _install_status = 0;
                private int _noOfinstallment = 0;
                private int _uano = 0;
                private DateTime _transDate = DateTime.Today;
                private string _IPADDRESS;
                private int _sessionNo = 0;
                private string _colcode = string.Empty;
                private string _install_status_YN = string.Empty;
                private string _studentName = string.Empty;
                #endregion
               
                #region Public Property Fields

                public int IDNO
                {
                    get { return _idno; }
                    set { _idno = value; }
                }
                public int ReciptNo
                {
                    get { return _receiptno; }
                    set { _receiptno = value; }
                }
                public int Install_Status
                {
                    get { return _install_status; }
                    set { _install_status = value; }
                }
                public int NoOfInstallment
                {
                    get { return _noOfinstallment; }
                    set { _noOfinstallment = value; }
                }
                public int UANO
                {
                    get { return _uano; }
                    set { _uano = value; }
                }
                public DateTime TransDate
                {
                    get { return _transDate; }
                    set { _transDate = value; }
                }
                public string IP_Address
                {
                    get
                    {
                        return this._IPADDRESS;
                    }
                    set
                    {
                        if ((this._IPADDRESS != value))
                        {
                            this._IPADDRESS = value;
                        }
                    }
                }

                public int SessionNo
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }
                public string CollegeCode
                {
                    get { return _colcode; }
                    set { _colcode = value; }
                }
                public string Install_Status_YN
                {
                    get { return _install_status_YN; }
                    set { _install_status_YN = value; }
                }
                public string StudentName
                {
                    get { return _studentName; }
                    set { _studentName = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS