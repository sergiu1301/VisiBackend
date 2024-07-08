using VisiProject.Contracts.Transactions;
using VisiProject.Infrastructure.Options;

namespace VisiProject.Infrastructure.Transactions;

public class AtomicScopeFactory : IAtomicScopeFactory
{
    private readonly SqlServerOptions _options;

    public AtomicScopeFactory(
        SqlServerOptions options)
    {
        _options = options;
    }
    
    public IAtomicScope Create()
    {
        return new TransactionSqlServerAtomicScope(_options.DefaultConnection);
    }

    public IAtomicScope CreateWithoutTransaction()
    {
        return new NoTransactionSqlServerAtomicScope(_options.DefaultConnection);
    }
}