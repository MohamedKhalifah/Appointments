using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Appointments.Web.Startup))]
namespace Appointments.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
