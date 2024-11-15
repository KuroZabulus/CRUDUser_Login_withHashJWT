﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.DTO.FormModel;
using Repository.DTO.ValidationModel;
using Service.Implement;
using Service.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace CRUDUser_Login_withHashJWT.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _service;

        public AuthController(IAuthService authServices, IUnitOfWork unitOfWork)
        {
            //_configuration = configuration;
            _service = authServices;
            _unitOfWork = unitOfWork;
            //_userServices = userServices;
            //this.emailSender = emailSender;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login return jwt token")]
        public async Task<IActionResult> Login([FromForm] LoginModel login)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _service.Login(login.Username, login.Password);

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new account with hashed password")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _service.Register(info);

                if (result.StartsWith("Internal server error"))
                {
                    _unitOfWork.RollbackTransaction();
                    return StatusCode(500, result);
                }
                else if (result == "Email already exists" || result == "UserName already exists" || result == "Phone number already exists")
                {
                    _unitOfWork.RollbackTransaction();
                    return BadRequest(result);
                }

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
