﻿using Common.Exceptions;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using Services.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.ActionFilter;
using WebFramework.Api;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        public UserController(IUserRepository userRepository , IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<User>> Get(CancellationToken cancellationToken)
        {
            var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
            
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if(user==null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpGet("[action]")]
        public async Task<string> Token(string username , string password , CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserAndPassword(username, password,cancellationToken);
            if(user==null)
            {
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            }
            var jwt = jwtService.Generate(user);

            return jwt;
        }

        [HttpPost]
    
        public async Task<ApiResult<User>> Create( UserDto userDto, CancellationToken cancellationToken)
        {
          
            var user = new User
            {
                Age = userDto.Age,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                UserName = userDto.UserName
            };
            await userRepository.AddAsync(user,userDto.password, cancellationToken);
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
