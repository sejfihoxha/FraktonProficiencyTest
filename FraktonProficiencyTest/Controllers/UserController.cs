using AutoMapper;
using FraktonProficiencyTest.Helpers;
using FraktonProficiencyTest.Models;
using FraktonProficiencyTest.Services.Token;
using FraktonProficiencyTest.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace FraktonProficiencyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private IUserService _userService;
        private ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructor
        public UserController(
                 IUserService userService,
                 ITokenService tokenService,
                 IOptions<JwtSettings> jwtSettings,
                 IMapper mapper
                 )
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;

        }
        #endregion

        #region Endpoints

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = _tokenService.GenerateAccessToken(user);

            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSettings.TokenLifetime);

            _userService.Update(user);

            return Ok(new { token = tokenString, refreshToken });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            try
            {
                _userService.Create(model, model.Password);

                return Ok(new { message = "User registered successfully" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refreshToken")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userId = principal.Identity.Name;
            var user = _userService.GetById(int.Parse(userId));

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }
            var newAccessToken = _tokenService.GenerateAccessToken(user);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            _userService.Update(user);

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }
        #endregion
    }
}