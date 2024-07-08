namespace VisiProject.Contracts.Transactions;

public interface IAtomicScope : IAsyncDisposable, IDisposable
{
    ValueTask CommitAsync();
}