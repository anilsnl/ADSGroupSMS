using ADSGroupSMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADSGroupSMS.Dependencies
{
    public interface ISMSDependency
    {
        Task<OperationResult> SendOneAsync(SMSModel model);
        Task<OperationResult> SendManyContactAsync(SMSModel model);
    }

}
