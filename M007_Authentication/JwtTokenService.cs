using M007_Authentication.Authentication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace M007_Authentication;

public class JwtTokenService : ITokenService
{
	public string CreateToken(AppUser user)
	{
		//Wenn der Client später mit einem Token zu uns kommt, wird dieser Schlüssel verwendet, um den Token zu verfizieren
		string unencryptedKey = "dasisteinsehrlangerschlüsseldereigentlichineinerdateigespeichertwerdensollte";
		SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(unencryptedKey));

		//Signatur: Wir als die API können bei einem gegebenen Schlüssel feststellen, ob dieser von uns gekommen ist
		SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
		{
			Issuer = "example.com",
			Audience = "example.com",
			Expires = DateTime.Now + TimeSpan.FromDays(1),
			SigningCredentials = credentials //Später beim Empfangen des Keys wird die Signatur verifiziert
		};

		//Token String erzeugen
		JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
		SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
