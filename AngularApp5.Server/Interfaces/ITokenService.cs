using AngularApp5.Server.Models.Identity;

namespace AngularApp5.Server.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
