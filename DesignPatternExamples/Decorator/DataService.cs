using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

// Interface base
public interface IDataService
{
    Task<string> GetExpensiveDataAsync(string key);
    Task UpdateDataAsync(string key, string value);
}

// Implementação concreta
public class DatabaseDataService : IDataService
{
    private readonly ILogger<DatabaseDataService> _logger;

    public DatabaseDataService(ILogger<DatabaseDataService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetExpensiveDataAsync(string key)
    {
        _logger.LogInformation("Fetching data from database for key: {Key}", key);
        await Task.Delay(1000); // Simular operação demorada
        return $"Data for {key} from database";
    }

    public async Task UpdateDataAsync(string key, string value)
    {
        _logger.LogInformation("Updating data in database for key: {Key}", key);
        await Task.Delay(500);
    }
}

// Decorator de Cache
public class CachingDataServiceDecorator : IDataService
{
    private readonly IDataService _innerService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingDataServiceDecorator> _logger;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public CachingDataServiceDecorator(
        IDataService innerService,
        IMemoryCache cache,
        ILogger<CachingDataServiceDecorator> logger)
    {
        _innerService = innerService;
        _cache = cache;
        _logger = logger;
        _cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
    }

    public async Task<string> GetExpensiveDataAsync(string key)
    {
        if (_cache.TryGetValue<string>(key, out var cachedData))
        {
            _logger.LogInformation("Cache hit for key: {Key}", key);
            return cachedData!;
        }

        _logger.LogInformation("Cache miss for key: {Key}", key);
        var data = await _innerService.GetExpensiveDataAsync(key);
        
        _cache.Set(key, data, _cacheOptions);
        return data;
    }

    public async Task UpdateDataAsync(string key, string value)
    {
        await _innerService.UpdateDataAsync(key, value);
        _cache.Remove(key); // Invalidar cache
        _logger.LogInformation("Cache invalidated for key: {Key}", key);
    }
}

// Decorator de Logging
public class LoggingDataServiceDecorator : IDataService
{
    private readonly IDataService _innerService;
    private readonly ILogger<LoggingDataServiceDecorator> _logger;

    public LoggingDataServiceDecorator(IDataService innerService, ILogger<LoggingDataServiceDecorator> logger)
    {
        _innerService = innerService;
        _logger = logger;
    }

    public async Task<string> GetExpensiveDataAsync(string key)
    {
        _logger.LogInformation("Starting GetExpensiveDataAsync for key: {Key}", key);
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        try
        {
            var result = await _innerService.GetExpensiveDataAsync(key);
            stopwatch.Stop();
            
            _logger.LogInformation("Completed GetExpensiveDataAsync for key: {Key} in {ElapsedMs}ms", 
                key, stopwatch.ElapsedMilliseconds);
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetExpensiveDataAsync for key: {Key}", key);
            throw;
        }
    }

    public async Task UpdateDataAsync(string key, string value)
    {
        _logger.LogInformation("Starting UpdateDataAsync for key: {Key}", key);
        
        try
        {
            await _innerService.UpdateDataAsync(key, value);
            _logger.LogInformation("Completed UpdateDataAsync for key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateDataAsync for key: {Key}", key);
            throw;
        }
    }
}