using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMaster.Web.Startup))]
namespace TaskMaster.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
