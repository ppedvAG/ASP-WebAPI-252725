using M007_Authentication.Authentication.Models;

namespace M007_Authentication;

public interface ITokenService
{
	string CreateToken(AppUser user);
}
