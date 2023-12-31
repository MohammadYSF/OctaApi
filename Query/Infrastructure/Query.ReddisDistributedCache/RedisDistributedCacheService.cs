using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Options;
using Query.Application.Repositories;
using ServiceStack.Data;
using ServiceStack.Redis;
using StackExchange.Redis;
namespace Query.Infrastructure.RedisDistributedCache;
public class RedisDistributedCacheService<T> : IDistributedCacheService<T> where T : class
{
    private readonly string _redisUri = "127.0.0.1:6379";

    private readonly PooledRedisClientManager _pooledRedisClientManager;

    private readonly IDictionary<Type, List<object>> _cache = (IDictionary<Type, List<object>>)(object)new ConcurrentDictionary<Type, List<object>>();

    public RedisDistributedCacheService(IOptions<RedisConfig> redisConfig)
    {
        _redisUri = redisConfig.Value.Uri;
        _pooledRedisClientManager = new PooledRedisClientManager(new string[1] { _redisUri });
        LoadIntoCache();
    }
    static RedisDistributedCacheService()
    {
    }

    private void LoadIntoCache()
    {
        _cache[typeof(T)] = Enumerable.ToList<object>(Enumerable.Cast<object>((System.Collections.IEnumerable)GetAll()));
    }

    public T Read(Func<T, bool> predicate)
    {
        if (_cache.TryGetValue(typeof(T), out var val))
        {
            return Enumerable.FirstOrDefault<T>(Enumerable.Where<T>(Enumerable.Cast<T>((System.Collections.IEnumerable)val), predicate));
        }

        throw new Exception("");
    }

    public List<T> FindBy(Func<T, bool> predicate)
    {
        if (_cache.TryGetValue(typeof(T), out var val))
        {
            return Enumerable.ToList<T>(Enumerable.Where<T>(Enumerable.Cast<T>((System.Collections.IEnumerable)val), predicate));
        }
        throw new Exception("");

    }

    public T FindById(long id)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            T byId = ((IEntityStore)client).GetById<T>((object)id);
            return byId;
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    public IList<T> FindByIds(long[] ids)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            IList<T> byIds = ((IEntityStore)client).GetByIds<T>((System.Collections.ICollection)(object)ids);
            return byIds;
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    public void Create(T entity)
    {
        if (!_cache.TryGetValue(typeof(T), out var val))
        {
            val = new List<object>();
        }

        val.Add((object)entity);
        _cache[typeof(T)] = val;
        Store(entity);
    }

    public void Creates(IList<T> entities)
    {
        if (!_cache.TryGetValue(typeof(T), out var val))
        {
            val = new List<object>();
        }

        val.AddRange((System.Collections.Generic.IEnumerable<object>)entities);
        _cache[typeof(T)] = val;
        StoreAll(entities);
    }

    public void Delete(T entity)
    {
        if (_cache.TryGetValue(typeof(T), out var val))
        {
            val.Remove((object)entity);
            _cache[typeof(T)] = val;
            RedisDelete(entity);
        }
    }
    public void Dirty()
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            client.DeleteAll<T>();
        }
        catch (Exception e)
        {

        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    public long Next()
    {
        long result = 1L;
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            result = client.As<T>().GetNextSequence();
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }

        return result;
    }

    public IList<T> GetAll()
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            return ((IEntityStore<T>)(object)client.As<T>()).GetAll();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return (IList<T>)new List<T>();
        }
        finally
        {
            ((IDisposable)client)?.Dispose();
        }
    }

    public void Update(Func<T, bool> predicate, T entity)
    {
        if (_cache.TryGetValue(typeof(T), out var val))
        {
            T val2 = Enumerable.FirstOrDefault<T>(Enumerable.Cast<T>((System.Collections.IEnumerable)val), predicate);
            if (val2 != null)
            {
                val.Remove((object)val2);
            }

            val.Add((object)entity);
            _cache[typeof(T)] = val;
            Store(entity);
        }
    }

    public bool ExpireAt(string keyName, int expireInSeconds)
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return val.Expire(keyName, expireInSeconds);
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public long GetTtl(string keyName)
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return val.Ttl(keyName);
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public void Set(string keyName, string content)
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            val.Set(keyName, Encoding.UTF8.GetBytes(content));
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public string Get(string keyName)
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return Encoding.UTF8.GetString(val.Get(keyName));
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public IDictionary<string, string> GetInfo()
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return (IDictionary<string, string>)(object)val.Info;
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public bool Ping()
    {
        //IL_0007: Unknown result type (might be due to invalid IL or missing references)
        //IL_000d: Expected O, but got Unknown
        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return val.Ping();
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    public long Exists(string key)
    {
        //IL_001a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0020: Expected O, but got Unknown
        //IL_000e: Unknown result type (might be due to invalid IL or missing references)
        if (key == null)
        {
            throw new ArgumentNullException("key");
        }

        RedisNativeClient val = new RedisNativeClient(_redisUri);
        try
        {
            return val.Exists(key);
        }
        finally
        {
            ((System.IDisposable)val)?.Dispose();
        }
    }

    private void Store(T entity)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            ((IEntityStore)client).Store<T>(entity);
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    private void StoreAll(IList<T> entities)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            ((IEntityStore)client).StoreAll<T>((IEnumerable<T>)entities);
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    private void RedisDelete(T entity)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            ((IEntityStore<T>)(object)client.As<T>()).Delete(entity);
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }

    private T Find(long id)
    {
        IRedisClient client = _pooledRedisClientManager.GetClient();
        try
        {
            return ((IEntityStore<T>)(object)client.As<T>()).GetById((object)id);
        }
        finally
        {
            ((System.IDisposable)client)?.Dispose();
        }
    }
}