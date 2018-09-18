using System.Web.Http;
using AutoMapper;

namespace Debugtime.Common.Configurations
{
    public class CommonConfigurations
    {
        public static void ConfigureApp()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
        }
    }
}