using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class PBIConfigurationEntity
    {

        #region Declared Variable & Initialize;

        private int _Workspace_id = 0;
        private string _workspace_name = string.Empty;
        private bool _status = false;
        private int _sub_Workspace_id = 0;
        private string _sub_workspace_name = string.Empty;
        private int _pbi_link_configuration = 0;
        private string _pbi_link_name = string.Empty;

        #endregion
        #region

        public int Workspace_id
        {
            get { return _Workspace_id; }
            set { _Workspace_id = value; }
        }
        public int sub_Workspace_id
        {
            get { return _sub_Workspace_id; }
            set { _sub_Workspace_id = value; }
        }
        public string workspace_name
        {
            get { return _workspace_name; }
            set { _workspace_name = value; }
        }
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string sub_workspace_name
        {
            get { return _sub_workspace_name; }
            set { _sub_workspace_name = value; }
        }
        public int pbi_link_configuration
        {
            get { return _pbi_link_configuration; }
            set { _pbi_link_configuration = value; }
        }
        public string pbi_link_name
        {
            get { return _pbi_link_name; }
            set { _pbi_link_name = value; }
        }
        public int OrganizationId
        {
            get;
            set;
        }
        #endregion
    }
}
