using Microsoft.Extensions.DependencyInjection;

namespace PHMIS.Infrastructure.RepositoryStores;


public partial class RepositoriesStore
{
    private readonly IServiceProvider _provider;
    private Dictionary<string, object> _repositoriesStore;

    public RepositoriesStore(IServiceProvider serviceProvider)
    {
        _provider = serviceProvider;
        _repositoriesStore = new Dictionary<string, object>();
    }

    public T GetInterface<T>()
    {
        return _provider.GetService<T>()!;
    }

    public T GetRepository<T>() where T : notnull
    {
        var name = typeof(T).Name;
        if (_repositoriesStore.TryGetValue(name, out var repository)) return (T)repository;
        repository = _provider.GetRequiredService<T>()!;
        _repositoriesStore.Add(name, repository);
        return (T)repository;
    }
}
