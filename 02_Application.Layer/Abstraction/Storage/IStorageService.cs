namespace _02_Application.Layer.Abstraction.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; set; }
    }
}
