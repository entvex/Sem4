using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPDS.Startup))]
namespace SPDS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
