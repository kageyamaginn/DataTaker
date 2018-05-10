using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataTaker.Startup))]
namespace DataTaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
