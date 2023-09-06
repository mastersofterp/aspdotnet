using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {

            public class RaiseTicket
            {
                private int _QMRaiseTicketID;
                private int _QMRequestSubCategoryID;
                private int _QMRequestCategoryID;
                private int _QMRequestTypeID;
                private string _TicketDescription;
                private string _FeedBack;
                private double _FeedBackPoints;
                private char _TicketStatus;
                private string _Attachment;
                private bool _IsPaidService;
                private bool _IsEmergencyService;
                private int _CreatedBy;
                private string _Filepath;
                private string _Filename;
                
                private double _PaidServiceAmount;
                private double _EmergencyServiceAmount;
                
                //
                private System.Nullable<bool> _IsStudentRemark;
                private string _RequiredInformation;

                public int CreatedBy
                {
                    get
                    {
                        return this._CreatedBy;
                    }
                    set
                    {
                        if ((this._CreatedBy != value))
                        {
                            this._CreatedBy = value;
                        }
                    }
                }

                public int QMRaiseTicketID
                {
                    get
                    {
                        return this._QMRaiseTicketID;
                    }
                    set
                    {
                        if ((this._QMRaiseTicketID != value))
                        {
                            this._QMRaiseTicketID = value;
                        }
                    }
                }
                public int QMRequestTypeID
                {
                    get
                    {
                        return this._QMRequestTypeID;
                    }
                    set
                    {
                        if ((this._QMRequestTypeID != value))
                        {
                            this._QMRequestTypeID = value;
                        }
                    }
                }
                public int QMRequestCategoryID
                {
                    get
                    {
                        return this._QMRequestCategoryID;
                    }
                    set
                    {
                        if ((this._QMRequestCategoryID != value))
                        {
                            this._QMRequestCategoryID = value;
                        }
                    }
                }
                public int QMRequestSubCategoryID
                {
                    get
                    {
                        return this._QMRequestSubCategoryID;
                    }
                    set
                    {
                        if ((this._QMRequestSubCategoryID != value))
                        {
                            this._QMRequestSubCategoryID = value;
                        }
                    }
                }

                public string TicketDescription
                {
                    get
                    {
                        return this._TicketDescription;
                    }
                    set
                    {
                        if ((this._TicketDescription != value))
                        {
                            this._TicketDescription = value;
                        }
                    }
                }
                public bool IsEmergencyService
                {
                    get
                    {
                        return this._IsEmergencyService;
                    }
                    set
                    {
                        if ((this._IsEmergencyService != value))
                        {
                            this._IsEmergencyService = value;
                        }
                    }
                }
                public double EmergencyServiceAmount
                {
                    get
                    {
                        return this._EmergencyServiceAmount;
                    }
                    set
                    {
                        if ((this._EmergencyServiceAmount != value))
                        {
                            this._EmergencyServiceAmount = value;
                        }
                    }
                }
                public bool IsPaidService
                {
                    get
                    {
                        return this._IsPaidService;
                    }
                    set
                    {
                        if ((this._IsPaidService != value))
                        {
                            this._IsPaidService = value;
                        }
                    }
                }
                public double PaidServiceAmount
                {
                    get
                    {
                        return this._PaidServiceAmount;
                    }
                    set
                    {
                        if ((this._PaidServiceAmount != value))
                        {
                            this._PaidServiceAmount = value;
                        }
                    }
                }
                public string FeedBack
                {
                    get
                    {
                        return this._FeedBack;
                    }
                    set
                    {
                        if ((this._FeedBack != value))
                        {
                            this._FeedBack = value;
                        }
                    }
                }

                public string Filepath
                {
                    get
                    {
                        return this._Filepath;
                    }
                    set
                    {
                        if ((this._Filepath != value))
                        {
                            this._Filepath = value;
                        }
                    }
                }
                public string Filename
                {
                    get
                    {
                        return this._Filename;
                    }
                    set
                    {
                        if ((this._Filename != value))
                        {
                            this._Filename = value;
                        }
                    }
                }
                
                public double FeedBackPoints
                {
                    get
                    {
                        return this._FeedBackPoints;
                    }
                    set
                    {
                        if ((this._FeedBackPoints != value))
                        {
                            this._FeedBackPoints = value;
                        }
                    }
                }

                
                public char TicketStatus
                {
                    get
                    {
                        return this._TicketStatus;
                    }
                    set
                    {
                        if ((this._TicketStatus != value))
                        {
                            this._TicketStatus = value;
                        }
                    }
                }
                public string Attachment
                {
                    get
                    {
                        return this._Attachment;
                    }
                    set
                    {
                        if ((this._Attachment != value))
                        {
                            this._Attachment = value;
                        }
                    }
                }




                private int _userno = 0;
                private int _recon = 0;
                private int _feemode = 0;
                
                private double _amount = 0;
                private double _totalamount = 0;
                
                private string _feeType = string.Empty;
                
                
                private string _transid = string.Empty;
                private string _ReceiptNO = string.Empty;
                private DateTime _transdate = System.DateTime.Today;
                private string _OrderID = string.Empty;

                public int UserNo
                {
                    get { return _userno; }
                    set { _userno = value; }
                }

                public double Amount
                {
                    get { return _amount; }
                    set { _amount = value; }
                }

                public double TotalAmount
                {
                    get { return _totalamount; }
                    set { _totalamount = value; }
                }

                public string FeeType
                {
                    get { return _feeType; }
                    set { _feeType = value; }
                }

               
                public int Recon
                {
                    get { return _recon; }
                    set { _recon = value; }
                }

                public int Feemode
                {
                    get { return _feemode; }
                    set { _feemode = value; }
                }

                

                public string TransID
                {
                    get { return _transid; }
                    set { _transid = value; }
                }

                public DateTime TransDate
                {
                    get { return _transdate; }
                    set { _transdate = value; }
                }


                public string ReceiptNo
                {
                    get
                    {
                        return _ReceiptNO;
                    }
                    set
                    {
                        _ReceiptNO = value;
                    }
                }

                public string OrderID
                {
                    get { return _OrderID; }
                    set { _OrderID = value; }
                }


                //add 22-12-2022 two entity add 
                public System.Nullable<bool> IsStudentRemark
                {
                    get { return _IsStudentRemark; }
                    set { _IsStudentRemark = value; }
                }

                public string RequiredInformation
                {
                    get { return _RequiredInformation; }
                    set { _RequiredInformation = value; }
                }


            }
        }
    }
}
