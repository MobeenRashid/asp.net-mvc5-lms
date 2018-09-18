using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Business.Utilities.Contracts
{
    interface IEncodingUtility/*<T> where T : class*/
    {
        FormUrlEncodedContent MapToUrlEncodedContent<T>(T sourceObject);
    }
}
