using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class TestPrepThroughtMISEntity
            {
                public DataTable dt { get; set; }
                public int SESSIONNO { get; set; }
                public int CreatedBy { get; set; }
                public string CreatedIP { get; set; }
            }
        }
    }
}
