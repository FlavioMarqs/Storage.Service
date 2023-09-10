namespace Storage.Commands.Interfaces
{
    public interface IStoreCommand<T>
    {
        T Value { get; set; }
    }
}
