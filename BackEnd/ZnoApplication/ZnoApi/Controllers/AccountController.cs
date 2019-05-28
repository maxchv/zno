using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zno.Server.Models;
using Zno.Server.Services;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;

namespace Zno.Server.Controllers
{
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        private IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ApplicationRoleManager roleManager,
                                 IConfiguration configuration,
                                 IUnitOfWork unitOfWork,
                                 IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Модель для авторизации</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users.FindByLogin(model.Login);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,
                                                                          model.Password,
                                                                          model.RememberMe,
                                                                          lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return Ok(await GenerateJwtToken(model.Login, user));
                    }
                }
            }

            return BadRequest("Invalid Login Or Password");
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model">Модель для регистрации</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    PhoneNumber = model.Phone,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await _roleManager.CreateRoleAsync("User");
                    await _userManager.AddToRoleAsync(newUser, "User");

                    return Ok();
                }

                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine($"{error.Code}: {error.Description}");
                }

                return BadRequest(builder.ToString());
            }

            return BadRequest("Invalid input parameters");
        }

        /// <summary>
        /// Регистрация пользователя с ролью преподавателя
        /// </summary>
        /// <param name="model">Модель для регистрации</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAsTeacher([FromBody] TeacherRegisterViewModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    PhoneNumber = model.Phone,
                    UserName = model.FullName,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await _roleManager.CreateRoleAsync("Teacher");
                    await _userManager.AddToRoleAsync(newUser, "Teacher");

                    return Ok();
                }

                StringBuilder builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine($"{error.Code}: {error.Description}");
                }

                return BadRequest(builder.ToString());
            }

            return BadRequest("Invalid input parameters");
        }

        /// <summary>
        /// Подтверждение электронной почты
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="code">Код для подтверждения электронной почты</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            IdentityResult result = null;
            string errorText = null;

            if (userId != null && code != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    result = await _userManager.ConfirmEmailAsync(user, code);
                }
                else
                {
                    errorText = $"Unable to load user with ID '{userId}'.";
                }
            }
            else
            {
                errorText = "Invalid input parameters";
            }

            if (result != null && result.Succeeded)
            {
                return Ok();
            }

            if (result != null)
            {
                var builder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine($"{error.Code}: {error.Description}");
                }

                errorText = builder.ToString();
            }

            return BadRequest(errorText);
        }

        /// <summary>
        /// Отправка инструкции для сброса пароля
        /// </summary>
        /// <param name="email"></param>
        /// <param name="callbackUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string login, string callbackUrl)
        {
            try
            {
                var user = await _unitOfWork.Users.FindByLogin(login);

                if (user is null)
                {
                    return BadRequest("Пользователь с указанным логином не найден!");
                }

                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedCode = HttpUtility.UrlEncode(code);
                callbackUrl = callbackUrl.Replace("code_value", encodedCode);

                await _emailSender.SendEmailAsync(user.Email, "Сброс пароля", "Для сброса пароля нажмите <a href=\"" + callbackUrl + "\">здесь</a>");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="model">Модель для сброса пароля</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Inputs data is invalid!!!");
            }

            var user = await _unitOfWork.Users.FindByLogin(model.Login);
            if (user == null)
            {
                return BadRequest("The user with the specified login was not found!");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors.ToString());
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Возвращает роли пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userRoles = await _userManager.GetRolesAsync(currentUser);

                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Генерация JWT токена
        /// </summary>
        /// <param name="email">Электронная почта пользователя</param>
        /// <param name="user">Текущий пользователь</param>
        /// <returns></returns>
        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Добавляем список ролей доступных пользователю
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenResult = new
            {
                access_token_type = "Bearer",
                access_token = tokenHandler.WriteToken(token),
                expires = expires
            };

            return tokenResult;
        }
    }
}