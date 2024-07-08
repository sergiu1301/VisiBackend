namespace VisiProject.Contracts.Transactions;

public interface IAtomicScopeFactory
{
    IAtomicScope Create();
    IAtomicScope CreateWithoutTransaction();
}