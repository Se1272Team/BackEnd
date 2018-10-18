using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookShopUI.Startup))]
namespace BookShopUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
