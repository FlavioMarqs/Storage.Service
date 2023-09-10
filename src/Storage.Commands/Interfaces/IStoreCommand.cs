namespace Storage.Commands.Interfaces
{
    public interface IStoreCommand<T>
    {
        T Identifier { get; set; }
    }
}
