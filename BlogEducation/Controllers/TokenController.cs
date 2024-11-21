using BlogEducation.OAuth.Provider;
using Buisenss.Interface.Abstract;
using Entity.Concrate;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlogEducation.Controllers
{
    public class TokenController : ApiController
    {
        private readonly IUserService _userService;
        public TokenController (IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("api/Token/LogUser")]
        public User getUserLogInfo([FromBody] User user)
        {
            var result = _userService.CheckAuthorization(user);
            return user;
        }
    }
}
