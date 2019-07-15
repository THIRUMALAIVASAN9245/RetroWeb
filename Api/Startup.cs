using Microsoft.Owin;

[assembly: OwinStartup(typeof(Retrospective.Application.API.Startup))]

namespace Retrospective.Application.API
{
    using Microsoft.AspNet.SignalR;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.Map("/signalr", map =>
            {
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
