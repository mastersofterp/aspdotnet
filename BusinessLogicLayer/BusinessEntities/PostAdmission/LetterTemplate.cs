using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class LetterTemplate
    {
        #region Private Member
        private int letter_template_id = 0;
        private int letter_type_id = 0;
        private string letter_template_name = string.Empty;
        private string short_desc = string.Empty;
        private string letter_text = string.Empty;
        private bool active_status;
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
        public string LetterTemplateName
        {
            get { return letter_template_name; }
            set { letter_template_name = value; }
        }
        public string ShortDesc
        {
            get { return short_desc; }
            set { short_desc = value; }
        }
        public string LetterText
        {
            get { return letter_text; }
            set { letter_text = value; }
        }
        public bool ActiveStatus
        {
            get { return active_status; }
            set { active_status = value; }
        }
        public int UaNo
        {
            get { return ua_no; }
            set { ua_no = value; }
        }
        #endregion
    }
}
