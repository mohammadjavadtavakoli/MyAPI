using Common;
using Common.Exceptions;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using Services.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.ActionFilter;
using WebFramework.Api;

namespace MyApi.Controllers.V1
{
  
    public class UserController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        public UserController(IUserRepository userRepository, IJwtService jwtService, UserManager<User> userManager
            , RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> Get(CancellationToken cancellationToken)
        {
            var userName = HttpContext.User.Identity.GetUserName();
            userName = HttpContext.User.Identity.Name;
            var userId = HttpContext.User.Identity.GetUserId();
            var useridInt = HttpContext.User.Identity.GetUserId<int>();
            var phoneNumber = HttpContext.User.Identity.FindFirstValue("PhoneNumber");
            var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var userData = await userManager.FindByIdAsync(id.ToString());
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
            {
                return NotFound();
            }
            await userRepository.UpdateSecurityStampAsync(user, cancellationToken);
            return user;
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<string> Token(string username, string password, CancellationToken cancellationToken)
        {
            //var user = await userRepository.GetUserAndPassword(username, password, cancellationToken);

            var user = await userManager.FindByNameAsync(username);
            if(user == null)
            {
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            }
            var passwordvalidate = await userManager.CheckPasswordAsync(user, password);

            if (!passwordvalidate)
            {
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            }
            var jwt = await jwtService.Generate(user);

            return jwt;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {

            var user = new User
            {
                Age = userDto.Age,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
                Email=userDto.Email
            };

            var Result = await userManager.CreateAsync(user, userDto.password);

            var resultRole = await roleManager.CreateAsync(new Role { Name = "Admin", Description = "AdminRole" });

            var AddToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
            //await userRepository.AddAsync(user,userDto.password, cancellationToken);
            return user;
        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.UserName = user.UserName;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.FullName = user.FullName;
            updateUser.Age = user.Age;
            updateUser.Gender = user.Gender;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLogineDate = user.LastLogineDate;

            await userRepository.UpdateAsync(updateUser, cancellationToken);

            return Ok();

        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            await userRepository.DeleteAsync(user, cancellationToken);

            return Ok();

            //return new ApiResult<User>
            //{
            //    IsSuccess = true,
            //    StatusCode = ApiResualtStatusCode.Success,
            //    Message = "عملیات با موفقیت انجام شد",
            //    Data = user
            //};
        }
    }
}
