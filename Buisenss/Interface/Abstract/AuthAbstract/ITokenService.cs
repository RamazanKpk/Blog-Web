using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Buisenss.Interface.Abstract.AuthAbstract
{
    public interface ITokenService
    {
        string GenerateToken(string userName, bool isAdmin, int expireMinutes = 30);
        ClaimsPrincipal GetClaimsPrincipal(string token);
    }
}
