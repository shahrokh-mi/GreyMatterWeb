using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreyMatterWeb.Startup))]
namespace GreyMatterWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
