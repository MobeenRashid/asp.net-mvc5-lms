using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace Debugtime.Paypal
{
    public class Configuration
    {
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            //var configDictionary = (Dictionary<string,string>)ConfigurationManager.GetSection("paypal");
            //return configDictionary;
            Dictionary<string, string> configDictionary = new Dictionary<string, string>
            {
                {"mode", "sandbox"},
                {"connectionTimeout", "360000"},
                {"requestRetries", "1"},
                {"clientId", "ARYYN2rd_v0Oq2ADfQnl6c3DZEOuKPl17SYXBEPidQtVLqipsAhg0PWIRd07rKBgS_98eE4v9UjJuGsu"},
                {"clientSecret", "EATbYZOUy5BVMh5mrOOUCPlQUplMzS6KI9ODjq2e394Df8AXYPQ2rKGR7Oh2fWODtr56YnAHjCD5yFwz"}
            };
            return configDictionary;
        }

        private static string GetAccessToken()
        {
            var accessToken = new OAuthTokenCredential(ClientId,ClientSecret,GetConfig());
            return accessToken.GetAccessToken();
        }


        public static APIContext GetApiContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken()) {Config = GetConfig()};
            return apiContext;
        }
    }
}