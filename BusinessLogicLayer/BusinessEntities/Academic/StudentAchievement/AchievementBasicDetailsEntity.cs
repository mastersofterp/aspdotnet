using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement
{
   public class AchievementBasicDetailsEntity
    {
       #region Private Members
       private int _participation_level_id = 0;
       private string _participation_level_name = string.Empty;

       private bool _IsActive = false;

       private int _event_nature_id = 0;
       private string _event_nature_name = string.Empty;

       private int _event_category_id = 0;
       private string _event_category_name = string.Empty;

       private int _activity_category_id = 0;
       private string _activity_category_name = string.Empty;

       private int  _acadamic_year_id= 0;
       private string _acadamic_year_name = string.Empty;

       private int _event_level_id = 0;
       private string _event_level_name = string.Empty;

       private int _technical_type_id = 0;
       private string _technical_type = string.Empty;

       private int _participation_type_id = 0;
       private string _participation_type = string.Empty;

       private int _moocs_platform_id = 0;
       private string _mooc_platform = string.Empty;

       private int _duration_id = 0;
       private string _duration = string.Empty;


       private DateTime _sdate = DateTime.Now;
       private DateTime _edate = DateTime.Now;
       private bool _status = false;
       
       
    


       #endregion
       #region Public

       public int participation_level_id
       {
           get { return _participation_level_id; }
           set { _participation_level_id = value; }
       }
       public string participation_level_name
       {
           get { return _participation_level_name; }
           set { _participation_level_name = value; }
       }

       public bool IsActive
       {
           get { return _IsActive; }
           set { _IsActive = value; }
       }
       public int OrganizationId
       {
           get;
           set;
       }


       public int event_nature_id
       {
           get { return _event_nature_id; }
           set { _event_nature_id = value; }
       }
       public string event_nature_name
       {
           get { return _event_nature_name; }
           set { _event_nature_name = value; }
       }



       public int event_category_id
       {
           get { return _event_category_id; }
           set { _event_category_id = value; }
       }
       public string event_category_name
       {
           get { return _event_category_name; }
           set { _event_category_name = value; }
       }

       public int activity_category_id
       {
           get { return _activity_category_id; }
           set { _activity_category_id = value; }
       }
       public string activity_category_name
       {
           get { return _activity_category_name; }
           set { _activity_category_name = value; }
       }



       public int acadamic_year_id
       {
           get { return _acadamic_year_id; }
           set { _acadamic_year_id = value; }
       }
       public string acadamic_year_name
       {
           get { return _acadamic_year_name; }
           set { _acadamic_year_name = value; }
       }



       public int event_level_id
       {
           get { return _event_level_id; }
           set { _event_level_id = value; }
       }
       public string event_level_name
       {
           get { return _event_level_name; }
           set { _event_level_name = value; }
       }


       public int technical_type_id
       {
           get { return _technical_type_id; }
           set { _technical_type_id = value; }
       }
       public string technical_type
       {
           get { return _technical_type; }
           set { _technical_type = value; }
       }

       public int participation_type_id
       {
           get { return _participation_type_id; }
           set { _participation_type_id = value; }
       }
       public string participation_type
       {
           get { return _participation_type; }
           set { _participation_type = value; }
       }

       public int moocs_platform_id
       {
           get { return _moocs_platform_id; }
           set { _moocs_platform_id = value; }
       }
       public string mooc_platform
       {
           get { return _mooc_platform; }
           set { _mooc_platform = value; }
       }
       public int duration_id
       {
           get { return _duration_id; }
           set { _duration_id = value; }
       }
       public string duration
       {
           get { return _duration; }
           set { _duration = value; }
       }

       public DateTime SDate
       {
           get { return _sdate; }
           set { _sdate = value; }
       }

       public DateTime EDate
       {
           get { return _edate; }
           set { _edate = value; }
       }
       public bool status
       {
           get { return _status; }
           set { _status = value; }
       }
       #endregion

   }

    }

