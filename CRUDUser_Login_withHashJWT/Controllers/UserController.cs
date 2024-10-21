using AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Data;
using Repository.DTO.ViewModel;
using Service.Interface;

namespace CRUDUser_Login_withHashJWT.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService userServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_configuration = configuration;
            _service = userServices;
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
                var result = await _service.GetAllUserAsync();
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
                var result = await _service.GetAllUserAsync();
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
    }
}
