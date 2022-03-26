using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace webAPI.Helpers
{
    public class JWThelper
    {
        private readonly byte[] secret;

        public JWThelper(string secretKey)
        {
            this.secret = Encoding.ASCII.GetBytes(@secretKey);
        }

        public string CrearToken(string email)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, email));
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.secret), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createToken = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(createToken);
        }

        public byte[] GetHash(string Email, string Salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(Salt, Email));
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);
            return hashedBytes;
        }

        public string ValidarJwtToken(string Token, string secretKey)
        {
            string ret = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            try
            {
                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                ret = jwtToken.Claims.First(x => x.Type == "name").Value;
            }
            catch{ }
            return ret;
        }

        
    }     
}
