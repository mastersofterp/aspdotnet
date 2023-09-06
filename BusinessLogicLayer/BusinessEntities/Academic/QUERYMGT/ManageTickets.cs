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
            public class ManageTickets
            {
                private int _QMRaiseTicketID;
                private int _PendingId;
                private string _Remark;
                private char _Status;
                private string _Filepath;
                private string _Filename;
                private int _UserId;           

                // add 22-12-2022
                private System.Nullable<bool> _IsStudentRemark;

                private string _RequiredInformation;


                private System.Nullable<bool> _IsDeptChange;
                private int _NewDeptId;

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
                public int PendingId
                {
                    get
                    {
                        return this._PendingId;
                    }
                    set
                    {
                        if ((this._PendingId != value))
                        {
                            this._PendingId = value;
                        }
                    }
                }
                public string Remark
                {
                    get
                    {
                        return this._Remark;
                    }
                    set
                    {
                        if ((this._Remark != value))
                        {
                            this._Remark = value;
                        }
                    }
                }
                public char Status
                {
                    get
                    {
                        return this._Status;
                    }
                    set
                    {
                        if ((this._Status != value))
                        {
                            this._Status = value;
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

               
                public int UserId
                {
                    get
                    {
                        return this._UserId;
                    }
                    set
                    {
                        if ((this._UserId != value))
                        {
                            this._UserId = value;
                        }
                    }
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


                public System.Nullable<bool> IsDeptChange
                {
                    get { return _IsDeptChange; }
                    set { _IsDeptChange = value; }
                }

                public int NewDeptId
                {
                    get
                    {
                        return this._NewDeptId;
                    }
                    set
                    {
                        if ((this._NewDeptId != value))
                        {
                            this._NewDeptId = value;
                        }
                    }
                }


            }
        }
    }

}
