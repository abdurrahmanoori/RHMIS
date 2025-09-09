
namespace RHMIS.Application.Repositories.Base
{
    public interface IUnitOfWork
    {

        //ITaxCentreRepository TaxCentreRepository { get; }

        Task StartTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);




        Task SaveChanges(CancellationToken cancellationToken);
        //public Task SaveChangesSequenceAutoGenerator(CancellationToken cancellationToken);
        //public Task<T?> SaveChangesSequenceAutoGenerator<T>(T entity, CancellationToken cancellationToken);


    }
}