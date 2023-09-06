using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement
{
   public class EventParticipationEntity
   {
       #region Private Members
       private int _uno = 0;
       private int _idno = 0;
       private int _event_participation_id = 0;
       private int _acadamic_year_id = 0;
       private int _event_category_id = 0;
       private int _activity_category_id = 0;
       private int _create_event_id = 0;
       private int _participation_type_id = 0;
       private bool _fc_status = false;
       private string _file_name = string.Empty;
       private string _file_path = string.Empty;
       private decimal _amount = 0.0m;
       private string _IPADDRESS;
       private DateTime _Current_Date = DateTime.Now;
       #endregion
       public int uno
       {
           get { return _uno; }
           set { _uno = value; }
       }
       public int idno
       {
           get { return _idno; }
           set { _idno = value; }
       }
       public int event_participation_id
       {
           get { return _event_participation_id; }
           set { _event_participation_id = value; }
       }
       public int acadamic_year_id
       {
           get { return _acadamic_year_id; }
           set { _acadamic_year_id = value; }
       }
       public int event_category_id
       {
           get { return _event_category_id; }
           set { _event_category_id = value; }
       }
       public int activity_category_id
       {
           get { return _activity_category_id; }
           set { _activity_category_id = value; }
       }
       public int create_event_id
       {
           get { return _create_event_id; }
           set { _create_event_id = value; }
       }
       
       public int participation_type_id
       {
           get { return _participation_type_id; }
           set { _participation_type_id = value; }
       }
       public bool fc_status
       {
           get { return _fc_status; }
           set { _fc_status = value; }
       }
       public string file_name
       {
           get { return _file_name; }
           set { _file_name = value; }
       }
       public string file_path
       {
           get { return _file_path; }
           set { _file_path = value; }
       }
       public string IPADDRESS
       {
           get
           {
               return this._IPADDRESS;
           }
           set
           {
               if ((this._IPADDRESS != value))
               {
                   this._IPADDRESS = value;
               }
           }
       }

       public decimal amount
       {
           get { return _amount; }
           set { _amount = value; }
       }
       public DateTime Current_Date
       {
           get { return _Current_Date; }
           set { _Current_Date = value; }
       }
       public int OrganizationId
       {
           get;
           set;
       }
      



   }
}
