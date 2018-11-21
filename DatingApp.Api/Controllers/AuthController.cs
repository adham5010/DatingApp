using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthReposatory _Repo;

        private IConfiguration Config;

        public AuthController (IAuthReposatory repo, IConfiguration Config) {
            this._Repo = repo;
            this.Config = Config;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserRegisterModelDTO User) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            User.UserName = User.UserName.ToLower ();
            if (await _Repo.IsUserExists (User.UserName)) {
                return BadRequest ("User Already Exists!");
            }

            return Ok (await _Repo.Register (new User () { UserName = User.UserName, FullName = User.FullName }, User.Password));
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login (LoginModelDTO User) {
            var RepoUser = await _Repo.Login (User.UserName, User.Password);
            if (RepoUser == null) {
                return Unauthorized ();
            }
            #region GenerateAuthToken
            var Claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, RepoUser.UserId.ToString ()),
                new Claim (ClaimTypes.Name, RepoUser.UserName)
            };

            var Key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Config.GetSection ("AppSetting:AuthToken").Value));

            var Cred = new SigningCredentials (Key, SecurityAlgorithms.HmacSha512);

            var Tokendescriptor = new SecurityTokenDescriptor () {
                Subject = new ClaimsIdentity (Claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = Cred
            };

            var TokenHandler = new JwtSecurityTokenHandler ();
            var token = TokenHandler.CreateToken (Tokendescriptor);
            #endregion
            return Ok (new { Token = TokenHandler.WriteToken(token) });
        }

    }
}