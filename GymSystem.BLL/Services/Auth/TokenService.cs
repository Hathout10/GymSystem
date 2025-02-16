using GymSystem.BLL.Interfaces.Auth;
using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Services.Auth
{
    public class TokenService : ITokenService
    {
        //hold the configuration settings
        private readonly IConfiguration configuration;

        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            this.configuration = configuration;
            _userManager = userManager;
        }

        //create a JWT token for a given user
        //public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        //{

        //    // Create a list of claims containing user's email and display name
        //    var authClaims = new List<Claim>()
        //    {
        //       new Claim(ClaimTypes.Email, user.Email),
        //       new Claim(ClaimTypes.GivenName , user.DisplayName )
        //    }; // Privvate Claims(UserDefinded)

        //    // Get the roles assigned to the user
        //    var userRoles = await userManager.GetRolesAsync(user);

        //    // Add each role as a claim
        //    foreach (var role in userRoles)
        //        authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

        //    // Create a security key from the configuration
        //    var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

        //    // Define the token(Generate Token)
        //    var token = new JwtSecurityToken(

        //        issuer: configuration["JWT:ValidIssuer"],
        //        audience: configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDayes"])),
        //        claims: authClaims,
        //         //Determine how to generate hashing result
        //         signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
        //         );

        //    // Write the token to a string and return it
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public async Task<(string, RefreshToken)> CreateTokenAsync(AppUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Create a list of claims containing user's email and display name
            var authClaims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? throw new InvalidOperationException("User email cannot be null")),
            new Claim("TrainerId", user.Id.ToString()),
            new Claim("UserId", user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                throw new InvalidOperationException("User roles cannot be null or empty");
            }

            authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtKey = configuration["JWT:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key cannot be null or empty");
            }

            // Create a security key from the configuration
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            // Define the token(Generate Token)
            var token = new JwtSecurityToken(

                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDayes"])),
                claims: authClaims,
                 //Determine how to generate hashing result
                 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                 );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(30),
                Created = DateTime.UtcNow
            };

            user.RefreshTokens.Add(refreshToken);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user with refresh token");
            }

            return (jwtToken, refreshToken);
        }

        public async Task<(string, RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));

            if (user == null || !user.RefreshTokens.Any(t => t.Token == refreshToken && t.IsActive))
                throw new UnauthorizedAccessException("Invalid refresh token");

            var refreshTokenEntity = user.RefreshTokens.Single(t => t.Token == refreshToken);

            if (!refreshTokenEntity.IsActive)
                throw new UnauthorizedAccessException("Refresh token has expired");

            refreshTokenEntity.Revoked = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return await CreateTokenAsync(user);
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));

            if (user == null) return false;

            var refreshTokenEntity = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken);
            if (refreshTokenEntity == null || !refreshTokenEntity.IsActive)
                return false;

            refreshTokenEntity.Revoked = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return true;
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };
            }
        }
    }
}
