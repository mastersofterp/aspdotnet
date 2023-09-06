//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : STANDARD FEE (FIELDS AND PROPERTIES)                                                     
// CREATION DATE : 15-MAY-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StandardFee
    {
        #region Private Fields

        int stdFeeNo = 0;        
        int feeCatNo = 0;       
        string feeHead = string.Empty;
        string feeDesc = string.Empty;
        string recieptCode = string.Empty;
        int serialNo = 0;        
        int batchNo = 0;
        int degreeNo = 0;
        int branchno = 0;        
        int paymentTypeNo = 0;
        int roomCapacity = 0;
        int college_id = 0;
        double sem1_Fee = 0;
        double sem2_Fee = 0;
        double sem3_Fee = 0;
        double sem4_Fee = 0;
        double sem5_Fee = 0;
        double sem6_Fee = 0;
        double sem7_Fee = 0;
        double sem8_Fee = 0;
        double sem9_Fee = 0;
        double sem10_Fee = 0;
        double sem11_Fee = 0;
        double sem12_Fee = 0;
        string collegeCode = string.Empty;
        //added for StandardFees by Shubham B
        // for Hostel Module
        int _Session_no = 0;
        int _Hostel_no = 0;
        int _Hostel_Name = 0;
        int _bathtype = 0;
        int _Capacity = 0;

	    #endregion

        #region Public Properties
        public int BATH
        {
            get { return _bathtype; }
            set { _bathtype = value; }
        }

        public int CAPACITY
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }

        public int Hostel_Name
        {
            get { return _Hostel_Name; }
            set { _Hostel_Name = value; }
        }

        public int Hostel_No
        {
            get { return _Hostel_no; }
            set { _Hostel_no = value; }
        }

        public int Session_No
        {
            get { return _Session_no; }
            set { _Session_no = value; }
        }

        public int CollegeId
        {
            get { return college_id; }
            set { college_id = value; }
        }

        public int StdFeeNo
        {
            get { return stdFeeNo; }
            set { stdFeeNo = value; }
        }

        public int FeeCatNo
        {
            get { return feeCatNo; }
            set { feeCatNo = value; }
        }

        public string FeeHead
        {
            get { return feeHead; }
            set { feeHead = value; }
        }

        public string FeeDesc
        {
            get { return feeDesc; }
            set { feeDesc = value; }
        }

        public string RecieptCode
        {
            get { return recieptCode; }
            set { recieptCode = value; }
        }

        public int SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public int BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }

        public int DegreeNo
        {
            get { return degreeNo; }
            set { degreeNo = value; }
        }

        public int Branchno
        {
            get { return branchno; }
            set { branchno = value; }
        }

        public int PaymentTypeNo
        {
            get { return paymentTypeNo; }
            set { paymentTypeNo = value; }
        }


        public int RoomCapacity
        {
            get { return roomCapacity; }
            set { roomCapacity = value; }
        }

        public double Sem1_Fee
        {
            get { return sem1_Fee; }
            set { sem1_Fee = value; }
        }

        public double Sem2_Fee
        {
            get { return sem2_Fee; }
            set { sem2_Fee = value; }
        }

        public double Sem3_Fee
        {
            get { return sem3_Fee; }
            set { sem3_Fee = value; }
        }

        public double Sem4_Fee
        {
            get { return sem4_Fee; }
            set { sem4_Fee = value; }
        }

        public double Sem5_Fee
        {
            get { return sem5_Fee; }
            set { sem5_Fee = value; }
        }

        public double Sem6_Fee
        {
            get { return sem6_Fee; }
            set { sem6_Fee = value; }
        }

        public double Sem7_Fee
        {
            get { return sem7_Fee; }
            set { sem7_Fee = value; }
        }

        public double Sem8_Fee
        {
            get { return sem8_Fee; }
            set { sem8_Fee = value; }
        }

        public double Sem9_Fee
        {
            get { return sem9_Fee; }
            set { sem9_Fee = value; }
        }

        public double Sem10_Fee
        {
            get { return sem10_Fee; }
            set { sem10_Fee = value; }
        }

        public double Sem11_Fee
        {
            get { return sem11_Fee; }
            set { sem11_Fee = value; }
        }

        public double Sem12_Fee
        {
            get { return sem12_Fee; }
            set { sem12_Fee = value; }
        }

        public string CollegeCode
        {
            get { return collegeCode; }
            set { collegeCode = value; }
        } 
        #endregion
    }
}