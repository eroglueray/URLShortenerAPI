using Microsoft.AspNetCore.Mvc;
using URLShortenerAPI.Data.DataManager.Interfaces;
using URLShortenerAPI.Models.DTO;

namespace URLShortenerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLShortenerController : ControllerBase
    {
        private readonly IURLDataManager _urlDataManager;

        public URLShortenerController(IURLDataManager urlDataManager)
        {
            _urlDataManager= urlDataManager;
        }

        [HttpPost("/URLShortener")]   
        public async Task<IActionResult> AddURLShortener(URLRequestDTO uRLRequestDTO)
        {
            var response = await _urlDataManager.AddURLShortenerAsync(uRLRequestDTO);
            return Ok(response);
        }
        [HttpGet("/GetCustomURL")]
        public async Task<IActionResult> RedirectShortUrl([FromQuery]URLRequestDTO uRLRequestDTO)
        {
            var response = await _urlDataManager.GetCustomUrl(uRLRequestDTO);
            return Ok(response);
        }
    }
}