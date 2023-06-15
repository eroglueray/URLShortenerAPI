namespace URLShortenerAPI.Common.Models.Responses.Interfaces
{
    public interface IResult
    {
        bool Success { get; }
        int ResponseCode { get; }
        string ResponseMessage { get; set; }
    }
}
