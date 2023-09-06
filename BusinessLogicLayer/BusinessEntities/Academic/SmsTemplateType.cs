using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
   public class SmsTemplateType
    {
        #region Private Members
        private int _template_id = 0;
        private string _template_type = string.Empty;
        private string _template_name = string.Empty;
        private int _al_no = 0;
        private string _al_nos = string.Empty;
        private string _template = string.Empty;
        private int _sms_template_id = 0;
        private bool _Status = false;
        private string _tem_id = string.Empty;
        private int _variable_count = 0;
        #endregion


        #region Public

        public int TEMPLATE_ID
        {
            get { return _template_id; }
            set { _template_id = value; }
        }
        public string TEMPLATE_TYPE
        {
            get { return _template_type; }
            set { _template_type = value; }
        }
        
        public int AL_NO
        {
            get { return _al_no; }
            set { _al_no = value; }
        }

       // for saving muliple pagenames added on date 2022-feb-01
        public string AL_NOS
        {
            get { return _al_nos; }
            set { _al_nos = value; }
        }
       //

        public string TEMPLATE_NAME
        {
            get { return _template_name; }
            set { _template_name = value; }
        }

        public string TEMPLATE
        {
            get { return _template; }
            set { _template = value; }
        }
        public int SMS_TEMPLATE_ID
        {
            get { return _sms_template_id; }
            set { _sms_template_id = value; }
        }
        public bool ActiveStatus
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string TEM_ID
        {
            get { return _tem_id; }
            set { _tem_id = value; }
        }
        public int VARIABLE_COUNT
        {
            get { return _variable_count; }
            set { _variable_count = value; }
        }
        #endregion

    }
}
