using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Action.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _jwtOptions;
        private readonly SecurityKey _issuerSigninKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
            _issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
        }

        public JsonWebToken Create(Guid userId)
        {
            var iat = DateTimeOffset.Now.ToUnixTimeSeconds();
            var exp = iat + (_jwtOptions.ExpiryMinutes * 60);
            var payload = new JwtPayload()
            {
                { "sub", userId},
                { "iss", _jwtOptions.Issuer},
                { "iat", iat },
                { "exp", exp},
                { "unique_name", userId}
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            return new JsonWebToken()
            {
                Token = token,
                Expires = exp
            };
        }
    }
}