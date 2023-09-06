using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities
        {
           public class LevelConfigurationEntity
           {
               #region Private

               private int _LC_NO;
               private int _UA_NO;
               private int _LevelNo;
               private int _ReminderDay;
               private int _CreateModifyBy;
               private string _IPAddress;

               #endregion Private


               #region Public

               public int LC_NO { get { return _LC_NO; } set { _LC_NO = value; } }
               public int UA_NO { get { return _UA_NO; } set { _UA_NO = value; } }
               public int LevelNo { get { return _LevelNo; } set { _LevelNo = value; } }
               public int ReminderDay { get { return _ReminderDay; } set { _ReminderDay = value; } }
               public int CreateModifyBy { get { return _CreateModifyBy; } set { _CreateModifyBy = value; } }
               public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }

               #endregion Public
           }
        }
    }
}
