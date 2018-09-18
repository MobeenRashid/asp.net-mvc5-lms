using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Debugtime.Common.Utilities
{
    public class AppJsonUtility 
    {
        public List<string> UnwrapHttpBadRequestResult(string jsonErrorResult)
        {
            var messageObject = new { Message = "", ModelState = new Dictionary<string, string[]>() };
            var deserailizedMessage = JsonConvert.DeserializeAnonymousType(jsonErrorResult, messageObject);

            var errors = deserailizedMessage.ModelState?.Select(kvp => String.Join(". ", kvp.Value));

            return errors.ToList();
        }
    }
}