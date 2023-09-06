using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Str_CalibartionEnt
    {
        #region Calibration
        private int _SRNO = 0;
        private int _ITEMNO = 0;
        private DateTime _DATE_OF_CALIBRATION;
        private int _UANO = 0;
        private double _COST = 0.0;
        private string _CALIBRATED_BY = string.Empty;
        private DataTable _CalibrationTable;
        private DataTable _ServicingTable;
        private DateTime _NEXT_DUE_DATE;
        //public object NATURE_OF_COMPLAINT;
        private int _CID = 0;
        private int _ISNO = 0;

        private int _MDNO = 0;
        private string _MAKE = string.Empty;
        private string _SPECIFICATION = string.Empty;
        private string _YEAR_OF_MANUFACTURING = string.Empty;
        private DateTime _DATE_OF_PURCHASE ;
        private string _VALUE = string.Empty;
        private string _MAINTAINED_BY = string.Empty;
        private DateTime _GUARANTEE_FROM;
        private DateTime _GUARANTEE_TO;
        private string _CALIBRATION_FREQUENCY = string.Empty;
        public string CALIBRATED_BY
        {
            get { return _CALIBRATED_BY; }
            set { _CALIBRATED_BY = value; }
        }

        public DataTable ServicingTable
        {
            get { return _ServicingTable; }
            set { _ServicingTable = value; }
        }
        public int ITEMNO
        {
            get { return _ITEMNO; }
            set { _ITEMNO = value; }
        }
        public int ISNO
        {
            get { return _ISNO; }
            set { _ISNO = value; }
        }

        public int UANO
        {
            get { return _UANO; }
            set { _UANO = value; }
        }
        public int CID
        {
            get { return _CID; }
            set { _CID = value; }
        }

        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }

      

        public int MDNO
        {
            get { return _MDNO; }
            set { _MDNO = value; }
        }

        public string MAKE
        {
            get { return _MAKE; }
            set { _MAKE = value; }
        }


        public string SPECIFICATION
        {
            get { return _SPECIFICATION; }
            set { _SPECIFICATION = value; }
        }


        public string YEAR_OF_MANUFACTURING
        {
            get { return _YEAR_OF_MANUFACTURING; }
            set { _YEAR_OF_MANUFACTURING = value; }
        }

        public string VALUE
        {
            get { return _VALUE; }
            set { _VALUE = value; }
        }

        public string MAINTAINED_BY
        {
            get { return _MAINTAINED_BY; }
            set { _MAINTAINED_BY = value; }
        }

        public string CALIBRATION_FREQUENCY
        {
            get { return _CALIBRATION_FREQUENCY; }
            set { _CALIBRATION_FREQUENCY = value; }
        }

        public DateTime DATE_OF_CALIBRATION
        {
            get { return _DATE_OF_CALIBRATION; }
            set { _DATE_OF_CALIBRATION = value; }
        }
        public DateTime DATE_OF_PURCHASE
        {
            get { return _DATE_OF_PURCHASE; }
            set { _DATE_OF_PURCHASE = value; }
        }

        public DateTime GUARANTEE_FROM
        {
            get { return _GUARANTEE_FROM; }
            set { _GUARANTEE_FROM = value; }
        }

        public DateTime GUARANTEE_TO
        {
            get { return _GUARANTEE_TO; }
            set { _GUARANTEE_TO = value; }
        }

        public DateTime NEXT_DUE_DATE
        {
            get { return _NEXT_DUE_DATE; }
            set { _NEXT_DUE_DATE = value; }
        }

        public DataTable CalibrationTable
        {
            get { return _CalibrationTable; }
            set { _CalibrationTable = value; }
        }



      

        #endregion

        #region Item_Service

        private DateTime _SERVICED_ON;
        private string _NATURE_OF_COMPLAINT = string.Empty;
        private string _SERVICED_BY = string.Empty;
        private string _INITIALS_IF_INCHARGE = string.Empty;
      


        public string NATURE_OF_COMPLAINT
        {
            get{return _NATURE_OF_COMPLAINT;}
            set{_NATURE_OF_COMPLAINT =value;}
        }
        public string SERVICED_BY
        {
            get { return _SERVICED_BY; }
            set { _SERVICED_BY = value; }
        }
        public string INITIALS_IF_INCHARGE
        {
            get { return _INITIALS_IF_INCHARGE; }
            set { _INITIALS_IF_INCHARGE=value; }
        }
        public DateTime SERVICED_ON
        {
            get { return _SERVICED_ON; }
            set { _SERVICED_ON = value; }
        }






        #endregion


       


        

    }
}
