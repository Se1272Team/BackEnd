using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookShopWithAuthen.Startup))]
namespace BookShopWithAuthen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
