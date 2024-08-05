using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5120Project.Startup))]
namespace FIT5120Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
