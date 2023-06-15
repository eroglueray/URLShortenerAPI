namespace URLShortenerAPI.Data.Entities.Interfaces
{
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }
    public interface IEntity
    {
    }
}
