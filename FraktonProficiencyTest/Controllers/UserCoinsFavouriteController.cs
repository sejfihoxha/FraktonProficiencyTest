using FraktonProficiencyTest.Helpers;
using FraktonProficiencyTest.Models;
using FraktonProficiencyTest.Services.UserCoinsFavourites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FraktonProficiencyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCoinsFavouriteController : ControllerBase
    {
        #region Properties
        private readonly IUserCoinsFavouriteService _userCoinsFavouriteService;
        #endregion

        #region Constructor
        public UserCoinsFavouriteController(IUserCoinsFavouriteService userCoinsFavouriteService)
        {
            _userCoinsFavouriteService = userCoinsFavouriteService;
        }
        #endregion

        #region Endpoints
        [HttpPost("addOrRemoveToFavourite")]
        public IActionResult AddOrRemoveToFavourite(UserCoinsFavouriteCreateModel userCoinsFavouriteModel)
        {
            var userId = HttpContext.GetUserId();

            var result= _userCoinsFavouriteService.AddOrRemoveFromFavourite(userId, userCoinsFavouriteModel);
           
            return Ok(result);
        }

        [HttpGet("getAllFavourite")]
        public IActionResult GetAllFavourite()
        {
            var userId = HttpContext.GetUserId();

            var result = _userCoinsFavouriteService.GetAllFavouriteByUserId(userId);

            return Ok(result);
        }
        #endregion
    }
}