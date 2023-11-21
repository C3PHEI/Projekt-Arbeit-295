using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend295.Services
{
    public class TokenService : ITokenService
    {
        //Injizieren von IConfiguration in diese Klasse, um den Token Key aus der Konfigurationsdatei zu lesen
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        }

        public string CreateToken(string username)
        {
            //Erstellen von Ansprüchen. Sie können weitere Informationen in diese Ansprüche einfügen. Zum Beispiel E-Mail-ID.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username)
            };

            //Erstellen von Anmeldeinformationen. Angeben, welche Art von Sicherheitsalgorithmus wir verwenden
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Erstellen der Tokens
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Token wiedergabe
            return tokenHandler.WriteToken(token);
        }
    }
}
