using IResult = URLShortenerAPI.Common.Models.Responses.Interfaces.IResult;

namespace URLShortenerAPI.Common.Models.Responses
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string responseMessage) : this(success)
        {
            ResponseMessage = responseMessage;
        }

        public Result(bool success, string message, int responseCode) : this(success, message)
        {
            ResponseCode = responseCode;
        }

        public bool Success { get; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; }
    }
}
