using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Aplication.Authentication;

public class JwtAuthentication : IJwtTokenAuthentication
{
    public object GerarToken(string cpf)
    {
        var claims = new List<Claim>
        {
            new("CPF", cpf.Trim())
        };

        var expires = DateTime.Now.AddHours(5);			
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ProjetoPadraoDotnet6"));
        var tokenData = new JwtSecurityToken(			
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
            claims: claims				
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
        return new
        {
            acess_token = token,
            expiration = expires
        };
    }
}