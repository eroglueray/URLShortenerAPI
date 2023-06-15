using URLShortenerAPI.Common.Models.Responses.Interfaces;

namespace URLShortenerAPI.Common.Models.Responses
{
    public class BaseResponseModel<T> : Result, IResponse<T>
    {
        public BaseResponseModel(T data, bool success, string responseMessage) : base(success, responseMessage)
        {
            Data = data;
        }
        public BaseResponseModel(T data, bool success, string responseMessage, int responseCode) : base(success, responseMessage, responseCode)
        {
            Data = data;
        }

        public BaseResponseModel(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
