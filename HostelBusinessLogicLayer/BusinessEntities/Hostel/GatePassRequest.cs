using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class GatePassRequest
            {
                #region Private Members
                private int _gatepassNo = 0;
                private int _studtype;
                private string _apprpath = string.Empty;
                private int _idno = 0;
                private DateTime _outDate;
                private int _outHourFrom = 0;
                private int _outMinFrom = 0;
                private string _outAmPm = string.Empty;
                private DateTime _inDate;
                private int _inHourFrom = 0;
                private int _inMinFrom = 0;
                private string _inAmPm = string.Empty;
                private int _purposeID = 0;
                private string _purposeOther = string.Empty;
                private string _remarks = string.Empty;
                private string _college_Code = string.Empty;
                private string _organizationid = string.Empty;
                private int _adminuano = 0;
                #endregion

                #region Public Property Fields
                public int GatePassNo
                {
                    get { return _gatepassNo; }
                    set { _gatepassNo = value; }
                }
                public int StudType
                {
                    get { return this._studtype; }
                    set { if ((this._studtype != value)) this._studtype = value; }
                }
                public string ApprPath
                {
                    get { return _apprpath; }
                    set { _apprpath = value; }
                }
                public int IDNO
                {
                    get { return _idno; }
                    set { _idno = value; }
                }
                public DateTime OutDate
                {
                    get { return _outDate; }
                    set { _outDate = value; }
                }
                public int OutHourFrom
                {
                    get { return _outHourFrom; }
                    set { _outHourFrom = value; }
                }
                public int OutMinFrom
                {
                    get { return _outMinFrom; }
                    set { _outMinFrom = value; }
                }
                public string OutAMPM
                {
                    get { return _outAmPm; }
                    set { _outAmPm = value; }
                }
                public DateTime InDate
                {
                    get { return _inDate; }
                    set { _inDate = value; }
                }
                public int InHourFrom
                {
                    get { return _inHourFrom; }
                    set { _inHourFrom = value; }
                }
                public int InMinFrom
                {
                    get { return _inMinFrom; }
                    set { _inMinFrom = value; }
                }
                public string InAMPM
                {
                    get { return _inAmPm; }
                    set { _inAmPm = value; }
                }
                public int PurposeID
                {
                    get { return _purposeID; }
                    set { _purposeID = value; }
                }
                public string PurposeOther
                {
                    get { return _purposeOther; }
                    set { _purposeOther = value; }
                }
                public string Remarks
                {
                    get { return _remarks; }
                    set { _remarks = value; }
                }
                public string CollegeCode
                {
                    get { return _college_Code; }
                    set { _college_Code = value; }
                }
                public string organizationid
                {
                    get { return _organizationid; }
                    set { _organizationid = value; }
                }
                public int Admin_UANO
                {
                    get { return _adminuano; }
                    set { _adminuano = value; }
                }
                #endregion
            }
        }
    }
}
