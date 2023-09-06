using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ExecutedTPlan
    {
        #region Private Fields

        int _executedTPlanNo = 0;
        int _planNo = 0;
        string _coveredTopics = "";
        string _deviation = "";
        string _reasonForDeviation = "";
        string _resourcesUsed = "";
        #endregion

        #region Public Properties

        public int ExecutedTPlanNo
        {
            get { return _executedTPlanNo; }
            set { _executedTPlanNo = value; }
        }

        public int TPlanNo
        {
            get { return _planNo; }
            set { _planNo = value; }
        }

        public string CoveredTopics
        {
            get { return _coveredTopics; }
            set { _coveredTopics = value; }
        }

        public string Deviation
        {
            get { return _deviation; }
            set { _deviation = value; }
        }

        public string ReasonForDeviation
        {
            get { return _reasonForDeviation; }
            set { _reasonForDeviation = value; }
        }

        public string ResourcesUsed
        {
            get { return _resourcesUsed; }
            set { _resourcesUsed = value; }
        }

        #endregion
    }
}