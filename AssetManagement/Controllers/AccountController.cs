using AssetManagement.Base;
using AssetManagement.Context;
using AssetManagement.Models;
using AssetManagement.Repositories.Data;
using AssetManagement.Repositories.Interface;
using AssetManagement.Services;
using AssetManagement.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, int>
    {
        private AccountRepository accountRepository;
        private readonly IConfiguration _config;
        private readonly MyContext _context;
        private readonly IGenericDapper _dapper;
        public AccountController(AccountRepository accountRepository, IConfiguration config, MyContext context, IGenericDapper depper) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            _config = config;
            _context = context;
            _dapper = depper;
        }

        private string GenerateJWT(string Name, string Email, string Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuth:secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
                new Claim(ClaimTypes.Name, Name),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role, Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(_config["JwtAuth:ValidIssuer"],
              _config["JwtAuth:ValidIssuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateResetPasswordToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtAuth:secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["JwtAuth:ValidIssuer"], _config["JwtAuth:ValidIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Email", login.Email, DbType.String);
            dynamic result = _dapper.Get<dynamic>(
                "[dbo].[SP_Logins]", dbparams, CommandType.StoredProcedure);

            if (BCrypt.Net.BCrypt.Verify(login.Password, result.Password))
            {
                var token = GenerateJWT(result.Name, result.Email, result.Role);
                return Ok(token);
            }

            return BadRequest("Wrong Password");
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] Register register)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);

            var dbparams = new DynamicParameters();
            dbparams.Add("Name", register.Name, DbType.String);
            dbparams.Add("Email", register.Email, DbType.String);
            dbparams.Add("Password", hashPassword, DbType.String);
            dbparams.Add("Contact", register.Contact, DbType.String);
            dbparams.Add("RoleId", register.RoleId, DbType.String);
            dbparams.Add("DepartmentId", register.DepartmentId, DbType.String);

            var result = Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Register]",
                dbparams, commandType: CommandType.StoredProcedure));

            return Ok(new { Status = "Success", Message = "User has been registered successfully" });
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public ActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            if (changePassword.NewPassword == changePassword.ConfirmPassword)
            {
                var currentUser = HttpContext.User;
                var claims = currentUser.Claims.ToList();
                var email = claims.FirstOrDefault(c => c.Type.Contains("email")).Value;

                Account toUpdate = _context.Accounts.SingleOrDefault(a => a.Employee.Email == email);
                //_context.Accounts.FirstOrDefault(l => l.Employee.Email == email);

                toUpdate.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);

                _context.Entry(toUpdate).State = EntityState.Modified;
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return Ok("Change Password Success");
                }
            }
            return BadRequest("Change Password Failed");
        }

        [HttpPost("ForgetPassword")]
        [AllowAnonymous]

        public ActionResult ForgetPassword(ForgetPassword forget)
        {
            var isValid = _context.Accounts.SingleOrDefault(l => l.Employee.Email == forget.Email);
            EmailManager emailManager = new EmailManager(_config, _context);
            if (isValid != null)
            {
                var token = GenerateResetPasswordToken(forget.Email);

                //var callback = Url.Action(nameof(ResetPassword), "api/JwtAuth/", new { token }, Request.Scheme);

                string subject = "Your changed password";
                string body = "https://localhost:44355/Authentication/ResetPassword?token="+token;
                emailManager.SendEmail(_config.GetSection("MailSettings").GetSection("Mail").Value,
                    subject, body, forget.Email);

                return Ok("Token has been sent to your email");
            }
            return NotFound("this email does not exist in database");
        }

        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(ResetPassword reset)
        {
            try
            {
                string token = reset.Token;
                var jwtReader = new JwtSecurityTokenHandler();
                var jwt = jwtReader.ReadJwtToken(token);

                var email = jwt.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                var isExist = _context.Accounts.FirstOrDefault(u => u.Employee.Email == email);

                isExist.Password = BCrypt.Net.BCrypt.HashPassword(reset.Password);

                var result = accountRepository.Put(isExist);
                if (result > 0)
                {
                    return Ok(new { Status = "Success", Message = "Password has been reset" });
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
