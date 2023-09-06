//================================================================
// Namespace      : IITMS                                         
// Class          : IITMSException                                
// Description    : This Class is used for throwing the exception 
// Developer      : NIRAJ D. PHALKE                               
// Creation Date  : 07-April-2009                                 
// Modifying Date :                                               
//================================================================

using System;

namespace IITMS
{
    /// <summary>
    /// Summary description for IITMSException
    /// </summary>
    public class IITMSException : Exception
    {
        public IITMSException(string exception)
        {
            throw new Exception(exception);
        }
    }
}