using Microsoft.Extensions.Logging;


namespace RainfallForecast.API.Services.Http
{
    public abstract class ApiClient : RestApiClient
    {
        protected readonly ILogger Logger;

        protected ApiClient(ILogger logger)
        {
            Logger = logger;
        }

        protected virtual Result<T> HandleApiResult<T>(ApiResult<Result<T>> apiResult, bool throwException = false)
        {
            var result = apiResult.Content ?? new Result<T>();
            HandleApiResult(apiResult, result, throwException);
            return result;
        }

        protected virtual Result<T> HandleApiResult<T>(ApiResult<T> apiResult, bool throwException = false)
        {
            var result = new Result<T>(apiResult.Content);
            HandleApiResult(apiResult, result, throwException);
            return result;
        }

        private void HandleApiResult<T>(ApiResult<T> apiResult, Result result, bool throwException = false)
        {
            if (!apiResult.IsSuccess)
            {
                result.AddError(apiResult.StatusReasonPhrase);
                if (throwException)
                {
                    throw new Exception(apiResult.Error);
                }
                Logger.LogError(apiResult.Error);
            }
        }
    }
}
