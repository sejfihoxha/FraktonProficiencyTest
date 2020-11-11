using FraktonProficiencyTest.Services.CryptoCoins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraktonProficiencyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CryptoCoinsController : ControllerBase
    {
        #region Properties
        private readonly ICryptoCoinsService _cryptoCoinsService;
        #endregion

        #region Constructor
        public CryptoCoinsController(ICryptoCoinsService cryptoCoinsService)
        {
            _cryptoCoinsService = cryptoCoinsService;
        }
        #endregion

        #region Endpoints
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _cryptoCoinsService.GetAll();

            if (result.Data == null)
                return NoContent();

            return Ok(result);
        }
        #endregion
    }
}