namespace URLShortenerAPI.Common.Models.Responses
{
    public class SuccessResponseModel<T> : BaseResponseModel<T>
    {

        public SuccessResponseModel(T data) : base(data, true)
        {
        }

      
    }
}
