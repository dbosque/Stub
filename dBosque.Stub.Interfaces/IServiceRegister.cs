namespace dBosque.Stub.Interfaces
{
    public interface IServiceRegister
    {
        IStubService Service { get; }

        bool Enabled { get; }
    }
}
