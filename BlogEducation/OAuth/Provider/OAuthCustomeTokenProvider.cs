using Buisenss.Interface.Abstract;
using Entity.Concrate;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogEducation.OAuth.Provider
{
    public class OAuthCustomeTokenProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserService _userService;
        public OAuthCustomeTokenProvider(IUserService userService)
        {
            _userService = userService;
        }

        public OAuthCustomeTokenProvider()
        {
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            //var user = new User
            //{
            //    UserName = context.UserName,
            //    Password = context.Password,
            //};
            //var userLog = _userService.UserLog(user);

            //if (context.UserName == "Deniz" && context.Password == "deniz" /*userLog != null*/)
            //{
            //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //    identity.AddClaim(new Claim("username", "Deniz"));
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Kullanıcı"));
            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("invalid_grant", "User Name or Password is incorrect!");
            //}
        }
        public User getUser(User user)
        {
            var newuser = new User
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            return newuser;
        }
    }
}