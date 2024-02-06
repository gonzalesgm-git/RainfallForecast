namespace RainfallForecast.API.Services.Queries.Patterns
{

    public interface IQuery { }

    public interface IQuery<out TResult, in TCriteria> : IQuery
    {
        TResult Execute(TCriteria criteria);
    }

    public interface IQuery<out TResult> : IQuery
    {
        TResult Execute();
    }

    public interface IQueryAsync<TResult> : IQuery
    {
        Task<TResult> ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface IQueryAsync<TResult, in TCriteria> : IQuery
    {
        Task<TResult> ExecuteAsync(TCriteria criteria, CancellationToken cancellationToken = default(CancellationToken));
    }

}
