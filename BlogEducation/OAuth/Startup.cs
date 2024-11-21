using BlogEducation.OAuth.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using System.Text;

[assembly: OwinStartup(typeof(BlogEducation.OAuth.Startup))]

namespace BlogEducation.OAuth
{
    public class Startup
    {
        //public static OAuthGrantResourceOwnerCredentialsContext context {  get; private set; }
        //public static OAuthAuthorizationServerOptions oAuthAuthorization { get; private set; }
        //public void Configuration(IAppBuilder app)
        //{
        //    HttpConfiguration httpConfiguration = new HttpConfiguration();
        //    ConfigureOAuth(app);
        //    WebApiConfig.Register(httpConfiguration);
        //    app.UseWebApi(httpConfiguration);
        //    app.UseCors(CorsOptions.AllowAll);
        //}
        //public void ConfigureOAuth(IAppBuilder appBuilder)
        //{
        //    oAuthAuthorization = new OAuthAuthorizationServerOptions()
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
        //        Provider = new OAuthCustomeTokenProvider(),
        //    };
        //    appBuilder.UseOAuthAuthorizationServer(oAuthAuthorization);
        //    appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        //    var data = new OAuthCustomeTokenProvider();
        //    var dbc = data.GrantResourceOwnerCredentials(context);
        //}
        public void Configration(IAppBuilder app)
        {
            var issuer = "https://localhost:44300";
            var audience = "https://localhost:44330";
            var secret = Convert.FromBase64String("cEwzYVMzVTUzQTV0cjBuRyZTMyNjdXIzS2V5PQ==");

            app.UseJwtBearerAuthentication(
           new JwtBearerAuthenticationOptions
           {
               AuthenticationMode = AuthenticationMode.Active,
               TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = issuer,
                   ValidAudience = audience,
                   IssuerSigningKey = new SymmetricSecurityKey(secret)
               }
           });
            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);
        }
    }
}
