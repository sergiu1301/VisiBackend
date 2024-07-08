using System.Data.Common;

namespace VisiProject.Contracts.Transactions;

public interface ISqlServerAtomicScope: IAtomicScope
{
    DbTransaction Transaction { get; }

    DbConnection Connection { get; }
}