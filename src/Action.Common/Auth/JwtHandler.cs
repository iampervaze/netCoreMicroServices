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
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
            _issuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigninKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidIssuer = _jwtOptions.Issuer,
                IssuerSigningKey = _issuerSigninKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var utcNow = DateTime.UtcNow;
            var expires = utcNow.AddMinutes(_jwtOptions.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(utcNow.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload()
            {
                { "sub", userId},
                { "iss", _jwtOptions.Issuer},
                { "iat", now },
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