namespace URLShortenerAPI.Common.Models.Responses
{
    public class ErrorResponseModel<T> : BaseResponseModel<T>
    {
        public ErrorResponseModel(string responseMessage) : base(default, false, responseMessage)
        {
        }

    }
}
