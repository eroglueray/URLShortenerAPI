namespace URLShortenerAPI.Common.Models.Responses.Interfaces
{
    public interface IResponse<out T> : IResult
    {
        T Data { get; }
    }
}
