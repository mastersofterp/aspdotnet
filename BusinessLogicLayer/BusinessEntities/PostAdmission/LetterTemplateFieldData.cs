using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.PostAdmission
{
   public  class LetterTemplateFieldData
    {
        #region Private Member
        private int letter_template_id = 0;
        private int letter_type_id = 0;
        private string Display_Data_Field = string.Empty;
        private string Data_Field = string.Empty;        
        private int ua_no = 0;
        #endregion

        #region Public Member
        public int LetterTemplateId
        {
            get { return letter_template_id; }
            set { letter_template_id = value; }
        }
        public int LetterTypeId
        {
            get { return letter_type_id; }
            set { letter_type_id = value; }
        }
        public string DisplayDataField
        {
            get { return Display_Data_Field; }
            set { Display_Data_Field = value; }
        }
        public string DataField
        {
            get { return Data_Field; }
            set { Data_Field = value; }
        }      
   
        public int UaNo
        {
            get { return ua_no; }
            set { ua_no = value; }
        }
        #endregion
    }
}
