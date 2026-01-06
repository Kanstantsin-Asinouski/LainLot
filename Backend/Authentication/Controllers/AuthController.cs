using System.Security.Claims;
using Authentication.DTOs;
using Authentication.ModeDTOs;
using Authentication.Services;
using DatabaseProvider.Models;
using DatabaseRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.Controllers
{
    [ApiController]
    [Authorize(Roles = "Registered User")]
    [Route("api/v1/[controller]")]
    public class AuthController(
        ILogger<AuthController> logger,
        JwtService jwtService,
        EmailService emailService,
        IRepository<User> userRepository,
        IRepository<UserRole> userRoleRepository)
        : ControllerBase
    {
        /// <summary>
        /// CTRL + M + P - expand all
        /// CTRL + M + O - collapse all
        /// </summary>

        #region repos init
        private readonly ILogger<AuthController> _logger = logger;
        private readonly JwtService _jwtService = jwtService;
        private readonly EmailService _emailService = emailService;
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserRole> _userRoleRepository = userRoleRepository;
        #endregion

        #region Login page
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Login(LoginInfo entity)
        {
            if (entity == null)
                return BadRequest("WrongCredentials");

            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Email.Equals(entity.Email) && u.Password.Equals(entity.Password));

            _logger.LogInformation($"Trying login for: {entity.Email}");
            _logger.LogInformation($"Login attempt: {entity.Email} | {entity.Password}");

            if (user == null)
            {
                _logger.LogError("AuthController. Wrong credentials.");
                return Unauthorized("WrongCredentials");
            }
            else
            {
                if (user.ConfirmEmail == false)
                {
                    _logger.LogError("AuthController. Email not confirmed.");
                    return Unauthorized("EmailNotConfirmed");
                }
            }

            var role = (await _userRoleRepository.GetById(user.FkUserRoles))?.Name ?? "User";
            var token = _jwtService.GenerateToken(user.Email, role);

            _logger.LogInformation("AuthController. Token has been generated.");
            return CreatedAtAction(nameof(Login), new { token });
        }
        #endregion

        #region Profile page
        [Authorize]
        [HttpGet("Me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCurrentUser()
        {
            var email = User.Identity?.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { email, role });
        }
        #endregion

        #region Registration page
        [AllowAnonymous]
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Registration(Registration entity)
        {
            if (entity == null)
                return BadRequest();

            if (_userRepository.GetAll().Any(u => u.Email == entity.Email || u.Login == entity.Login))
                return BadRequest("UserAlreadyExists");

            var user = new User
            {
                Email = entity.Email,
                Login = entity.Login,
                Password = entity.Password
            };

            var userRole = _userRoleRepository.GetAll()
                .Where(x => x.Name.ToLower().Equals("registered user"))
                .FirstOrDefault();

            if (userRole != null)
                user.FkUserRoles = userRole.Id;

            var confirmationToken = Guid.NewGuid().ToString();
            var tokenExpiration = DateTime.UtcNow.AddHours(24);

            // Logic for email confirmation here
            user.ConfirmEmail = false;
            user.ConfirmationToken = confirmationToken;
            user.ConfirmationTokenExpires = tokenExpiration;

            try
            {
                await _userRepository.Add(user);

                var confirmationUrl = $"{Request.Scheme}://{Request.Host}/api/v1/Auth/Verify?token={confirmationToken}";
                await _emailService.SendVerificationEmail(user.Email, confirmationUrl);

                return Ok("RegistrationSuccessful");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "InternalServerError");
            }
        }

        [AllowAnonymous]
        [HttpGet("Verify")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(u => u.ConfirmationToken == token);

            if (user == null)
                return BadRequest("Invalid token.");

            if (user.ConfirmationTokenExpires < DateTime.UtcNow)
                return BadRequest("Token expired.");

            user.ConfirmEmail = true;
            user.ConfirmationToken = null;
            user.ConfirmationTokenExpires = null;

            await _userRepository.Update(user);
            return Ok("Email confirmed successfully.");
        }

        #endregion
    }
}