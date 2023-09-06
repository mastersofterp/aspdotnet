using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


      namespace BusinessLogicLayer.BusinessEntities.Academic
      {
      public  class TemplateType
        {
         #region Private Members
         private int _template_id = 0;
         private string _template_type = string.Empty;
         private string _college_code = string.Empty;
         private string _page_name = string.Empty;
         private string _template_name = string.Empty;
         private string _template = string.Empty;
         private bool _Status = false;
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
          public string COLLEGE_CODE
          {
               get { return _college_code; }
               set { _college_code = value; }
          }
          public string PAGE_NAME
          {
              get { return _page_name; }
              set { _page_name = value; }
          }
          public string TEMPLATES_NAME
          {
              get { return _template_name; }
              set { _template_name = value; }
          }
          
          public string TEMPLATE
          {
              get { return _template; }
              set { _template = value; }
          }
          public bool ActiveStatus
          {
              get { return _Status; }
              set { _Status = value; }
          }
         #endregion


     }
}

 