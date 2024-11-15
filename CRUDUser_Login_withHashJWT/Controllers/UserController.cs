using AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Data;
using Repository.DTO.FormModel;
using Repository.DTO.ViewModel;
using Service.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRUDUser_Login_withHashJWT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_configuration = configuration;
            _userService = userServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_userServices = userServices;
            //this.emailSender = emailSender;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUser()
        {
            try
            {
                var result = await _userService.GetAllUserAsync();
                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "User not found." });
                }
                result = result.ToList();
                var userViewModel = _mapper.Map<List<UserViewModel>>(result);
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUserPaginationOptions()
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _userService.GetAllUserAsync();
                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "User not found." });
                }
                result = result.ToList();
                var userViewModel = _mapper.Map<List<UserViewModel>>(result);
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPost("upload-avatar")]
        [SwaggerOperation(Summary = "Don't FromForm the IFormFile as it's already implied")]
        public async Task<IActionResult> UploadImageAvatar(string userName, UpdateProfileAvatarModel update)
        {
            try
            {
                var userNameFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userNameFromToken != userName)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "You are not allowed to update other people's profiles." });
                }

                _unitOfWork.BeginTransaction();

                if (update != null)
                {
                    var result = await _userService.UpdateImageAvatar(update.image);
                    return Ok();
                }

                else
                {
                    _unitOfWork.RollbackTransaction();
                    throw new ArgumentException("No image was given!");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("upload-image")]
        [SwaggerOperation(Summary = "Will prioritize update/image param before avatar param")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                if (image == null)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new ArgumentException("No image was given!");
                }
                var result = await _userService.UpdateImageAvatar(image);
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
