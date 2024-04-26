using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By :                                                                   
Created On :                                                 
Purpose    :                                          
Version    : 1.0.0                                                                 
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version   Modified On   Modified By        Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
 1.0.1    14-03-2024    Ashutosh Dhobe     Changes for added iseditable ,isdiplaysectionname                      
------------------------------------------- -------------------------------------------------------------------------------                                                                                                                     
*/

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.Academic
        {

            public class StudentModuleConfig
            {
                //general info 
                #region Private Member
                private int _studconfig_id = 0;
                private string _caption_name = string.Empty;
                private string _control_to_hide = string.Empty;
                private bool _isactive = false;
                private string _control_to_mandatory = string.Empty;
                private bool _ismandatory = false;
                //<1.0.1>
                private bool _iseditable = false;
                //</1.0.1>
                private int _organization_id = 0;
                private string _page_no = string.Empty;
                //<1.0.1>
                private bool _display_section = false;
                //</1.0.1>
                #endregion

                #region Public Property Fields
                public int STUDCONFIG_ID
                {
                    get { return _studconfig_id; }
                    set { _studconfig_id = value; }
                }
                public string CAPTION_NAME
                {
                    get { return _caption_name; }
                    set { _caption_name = value; }
                }
                public string CONTROL_TO_HIDE
                {
                    get { return _control_to_hide; }
                    set { _control_to_hide = value; }
                }
                public bool ISACTIVE
                {
                    get { return _isactive; }
                    set { _isactive = value; }
                }
                public string CONTROL_TO_MANDATORY
                {
                    get { return _control_to_mandatory; }
                    set { _control_to_mandatory = value; }
                }
                public bool ISMANDATORY
                {
                    get { return _ismandatory; }
                    set { _ismandatory = value; }
                }
                public int ORGANIZATION_ID
                {
                    get { return _organization_id; }
                    set { _organization_id = value; }
                }
                public string PAGE_NO
                {
                    get { return _page_no; }
                    set { _page_no = value; }
                }


                public bool ISEDITABLE
                {
                    get { return _iseditable; }
                    set { _iseditable = value; }
                }
                public bool DISPLAYSECTION
                {
                    get { return _display_section; }
                    set { _display_section = value; }
                }

                #endregion
            }
        }
    }
}
