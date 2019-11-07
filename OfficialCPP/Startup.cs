using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OfficialCPP.Startup))]
namespace OfficialCPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
