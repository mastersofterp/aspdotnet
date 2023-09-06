using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class MeterChange
    {

        #region Private Members

        private int _quatertypeno = 0;
        private int _quaternoID = 0;
        private int _cONSUMERTYPE_ID = 0;
        private int _nAME_ID = 0;
        private string _qUATERTYPE = string.Empty;
        private string _qUATERNO = string.Empty;
        private DateTime _m_CHANGE_DATE = DateTime.MinValue;
        private int  _oLD_EN_MID   =0;
        private int  _oLD_EN_MTYPE =0;
        private Int64 _pREV_EN_MONTH_R = 0;
        private Int64 _cURRENT_EN_MONTH_R = 0;
        private int _nEW_EN_MID = 0;
        private int _nEW_EN_MTYPE = 0;
        private Int64 _nEW_EN_MSTART_R = 0;
        private Int64 _dIFF_PREV_EN_METER_R = 0;
        private int  _oLD_WA_MID = 0;
        private int  _oLD_WA_MTYPE =0;
        private Int64 _pREV_WA_MONTH_R = 0;
        private Int64 _cURRENT_WA_MONTH_R = 0;
        private int  _nEW_WA_MID = 0;
        private int  _nEW_WA_MTYPE =0;
        private Int64 _nEW_WA_MSTART_R = 0;
        private Int64 _DIFF_PREV_WA_METER_R = 0;

        private int _QA_ID= 0;

        #endregion

        #region Public Members

        public int QA_ID
        {
            get { return _QA_ID; }
            set { _QA_ID = value; }
        }

        public int QUATERTYPENO
        {
            get { return _quatertypeno; }
            set { _quatertypeno = value; }
        }

        public int QUATERNOID
        {
            get { return _quaternoID; }
            set { _quaternoID = value; }
        }

        public int CONSUMERTYPE_ID
        {
            get { return _cONSUMERTYPE_ID; }
            set { _cONSUMERTYPE_ID = value; }
        }
        

        public int NAME_ID
        {
            get { return _nAME_ID; }
            set { _nAME_ID = value; }
        }
        

        public string QUATERTYPE
        {
            get { return _qUATERTYPE; }
            set { _qUATERTYPE = value; }
        }
        

        public string QUATERNO
        {
            get { return _qUATERNO; }
            set { _qUATERNO = value; }
        }
        

        public DateTime M_CHANGE_DATE
        {
            get { return _m_CHANGE_DATE; }
            set { _m_CHANGE_DATE = value; }
        }
        

        public int OLD_EN_MID
        {
            get { return _oLD_EN_MID; }
            set { _oLD_EN_MID = value; }
        }
        

        public int OLD_EN_MTYPE
        {
            get { return _oLD_EN_MTYPE; }
            set { _oLD_EN_MTYPE = value; }
        }
        

        public Int64 PREV_EN_MONTH_R
        {
            get { return _pREV_EN_MONTH_R; }
            set { _pREV_EN_MONTH_R = value; }
        }
        

        public Int64 CURRENT_EN_MONTH_R
        {
            get { return _cURRENT_EN_MONTH_R; }
            set { _cURRENT_EN_MONTH_R = value; }
        }
        

        public int NEW_EN_MID
        {
            get { return _nEW_EN_MID; }
            set { _nEW_EN_MID = value; }
        }
        

        public int NEW_EN_MTYPE
        {
            get { return _nEW_EN_MTYPE; }
            set { _nEW_EN_MTYPE = value; }
        }
        

        public Int64 NEW_EN_MSTART_R
        {
            get { return _nEW_EN_MSTART_R; }
            set { _nEW_EN_MSTART_R = value; }
        }
        

        public Int64 DIFF_PREV_EN_METER_R
        {
            get { return _dIFF_PREV_EN_METER_R; }
            set { _dIFF_PREV_EN_METER_R = value; }
        }
        

        public int  OLD_WA_MID
        {
            get { return _oLD_WA_MID; }
            set { _oLD_WA_MID = value; }
        }
        

        public int OLD_WA_MTYPE
        {
            get { return _oLD_WA_MTYPE; }
            set { _oLD_WA_MTYPE = value; }
        }
        

        public Int64 PREV_WA_MONTH_R
        {
            get { return _pREV_WA_MONTH_R; }
            set { _pREV_WA_MONTH_R = value; }
        }
       

        public Int64 CURRENT_WA_MONTH_R
        {
            get { return _cURRENT_WA_MONTH_R; }
            set { _cURRENT_WA_MONTH_R = value; }
        }
        

        public int NEW_WA_MID
        {
            get { return _nEW_WA_MID; }
            set { _nEW_WA_MID = value; }
        }
        

        public int NEW_WA_MTYPE
        {
            get { return _nEW_WA_MTYPE; }
            set { _nEW_WA_MTYPE = value; }
        }
        

        public Int64 NEW_WA_MSTART_R
        {
            get { return _nEW_WA_MSTART_R; }
            set { _nEW_WA_MSTART_R = value; }
        }
        

        public Int64 DIFF_PREV_WA_METER_R
        {
            get { return _DIFF_PREV_WA_METER_R; }
            set { _DIFF_PREV_WA_METER_R = value; }
        }

      
        
        
        #endregion

    }
}
