namespace RainfallForecast.API.Services.Patterns
{

    public interface IQuery { }

    public interface IQueryAsync<TResult, in TCriteria> : IQuery
    {
        Task<TResult> ExecuteAsync(TCriteria criteria, CancellationToken cancellationToken = default);
    }

}
