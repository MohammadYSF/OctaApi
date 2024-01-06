namespace Query.Application.Repositories;
public interface IDistributedCacheService<T> where T : class
{
    T Read(Func<T, bool> predicate);
    List<T> FindBy(Func<T, bool> predicate);
    T FindById(long id);
    IList<T> FindByIds(long[] ids);
    void Create(T entity);
    void Creates(IList<T> entities);
    void Delete(T entity);
    long Next();
    IList<T> GetAll();
    void Update(Func<T, bool> predicate, T entity);
    bool ExpireAt(string keyName, int expireInSeconds);
    long GetTtl(string keyName);
    void Set(string keyName, string content);
    string Get(string keyName);
    long Exists(string key);
    bool Ping();
    IDictionary<string, string> GetInfo();
    void Dirty();
}

