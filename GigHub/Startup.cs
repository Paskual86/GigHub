using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHub.Startup))]
namespace GigHub
{
    /*
     Review https://docs.fluentvalidation.net/en/latest/collections.html
     */
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
