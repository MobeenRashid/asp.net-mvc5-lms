using Debugtime.Common.Wrappers;
using Debugtime.Master.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Master.Rest_Services.Contracts
{
    public interface IRoles
    {
        Task<AppHttpResponseMessageWrapper> GetRolesAsync(RolesInputModel roleInfo);
    }
}
