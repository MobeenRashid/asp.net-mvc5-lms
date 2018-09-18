using Debugtime.Master.Rest_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Debugtime.Common.Wrappers;
using Debugtime.Master.Models.Input;
using System.Threading.Tasks;
using Debugtime.Master.Extensions;
using System.Net.Http;

namespace Debugtime.Master.Rest_Services.Concretes
{
    public class Roles : IRoles
    {
        private readonly AppHttpClientMaster _httpClient;

        public Roles(AppHttpClientMaster httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppHttpResponseMessageWrapper> GetRolesAsync(RolesInputModel roleInfo)
        {
            var responce = await _httpClient.PostAsJsonAsync<RolesInputModel>("api/users/role", roleInfo);
            return new AppHttpResponseMessageWrapper(responce);
        }
    }
}