using Buisenss.Interface.Abstract;
using Buisenss.Interface.Abstract.AuthAbstract;
using Buisenss.Interface.AuthService;
using Buisenss.Interface.Service;
using Entity.Concrate;
using Microsoft.Owin;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var configre = new UnityContainer();
            configre.RegisterType<IUserService, UserService>();
            configre.RegisterType<IAllService<Category>, CategoryService>();
            configre.RegisterType<IAllService<User>, UserService>();
            configre.RegisterType<IAllService<ArticleComment>, ArticleCommentService>();
            configre.RegisterType<IArticleService, ArticleService>();
            configre.RegisterType<IAllService<Article>, ArticleService>();
            configre.RegisterType<ITokenService, AuthService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(configre));
        }
    }
}
