using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SlackWeather.Startup))]
namespace SlackWeather
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
