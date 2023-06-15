using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using URLShortenerAPI.Common.Models.Responses;
using URLShortenerAPI.Common.Models.Responses.Interfaces;
using URLShortenerAPI.Data.DataManager.Interfaces;
using URLShortenerAPI.Data.Entities;
using URLShortenerAPI.Data.UnitOfWork;
using URLShortenerAPI.Models;
using URLShortenerAPI.Models.DTO;
using URLShortenerAPI.Repositories;

namespace URLShortenerAPI.Data.DataManager
{
    public class URLDataManager : IURLDataManager
    {
        private readonly IURLShortenerUnitOfWork _urlShortenerUnitOfWork;
        private readonly IRepository<URLShortener> _urlRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Dictionary<string, string> urlDictionary = new Dictionary<string, string>();

        public URLDataManager(IURLShortenerUnitOfWork urlShortenerUnitOfWork, IRepository<URLShortener> urlRepository, IHttpContextAccessor httpContextAccessor)
        {
            _urlShortenerUnitOfWork = urlShortenerUnitOfWork ?? throw new ArgumentNullException(nameof(urlShortenerUnitOfWork));
            _urlRepository = urlRepository ?? throw new ArgumentNullException(nameof(urlRepository));
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IResponse<URLResponseDTO>> AddURLShortenerAsync(URLRequestDTO uRLRequestDTO)
        {
            if (!Uri.TryCreate(uRLRequestDTO.Url, UriKind.Absolute, out var intputUrl))
            {
                return new ErrorResponseModel<URLResponseDTO>("Invalid url");
            }
            string encoded = string.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(uRLRequestDTO.Url));
                encoded = Convert.ToBase64String(hashBytes)
                    .Replace("/", "_")
                    .Replace("+", "-")
                    .Substring(0, 6);

            }
            var shortUrl = new URLShortener()
            {
                Url = uRLRequestDTO.Url,
                ShortUrl = encoded
            };
            _urlRepository.Add(shortUrl);
            await _urlShortenerUnitOfWork.SaveChangesAsync(false);
            var result = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{shortUrl.ShortUrl}";
            return new SuccessResponseModel<URLResponseDTO>(new URLResponseDTO()
            {
                Url = result
            });
        }
        public async Task<IResponse<URLResponseDTO>> GetCustomUrl(URLRequestDTO uRLRequestDTO)
        {
            List<string> getAllUrl = await _urlRepository.GetAll().Select(x => x.Url).ToListAsync();
            var getUrl = await _urlRepository.GetAll().FirstOrDefaultAsync(x => x.ShortUrl.ToLower().Trim() == uRLRequestDTO.Url.ToLower().Trim());
            // Check the custom URL
            if (getAllUrl.Contains(uRLRequestDTO.Url))
            {
                return new SuccessResponseModel<URLResponseDTO>(new URLResponseDTO()
                {
                    Url = getUrl.ShortUrl
                });
            }

            string shortenedUrl;
            do
            {
                shortenedUrl = Guid.NewGuid().ToString().Substring(0, 6);
            }
            while (getAllUrl.Contains(shortenedUrl));

            getAllUrl.Add(shortenedUrl);

            return new SuccessResponseModel<URLResponseDTO>(new URLResponseDTO()
            {
                Url = shortenedUrl
            });

        }

    }
}
