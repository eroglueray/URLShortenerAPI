using URLShortenerAPI.Common.Models.Responses.Interfaces;
using URLShortenerAPI.Models.DTO;

namespace URLShortenerAPI.Data.DataManager.Interfaces
{
    public interface IURLDataManager
    {
        Task<IResponse<URLResponseDTO>> AddURLShortenerAsync(URLRequestDTO uRLRequestDTO);
        Task<IResponse<URLResponseDTO>> GetCustomUrl(URLRequestDTO uRLRequestDTO);
    }
}
