using System;
using TaskMaster.DAL.Enum;

namespace TaskMaster.WebApi.Models
{
    public class UserWebApi
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public string Token { get; set; }
        //public BrowserType BrowserType { get; set; }
        //public PlatformType PlatformType { get; set; }

    }
}