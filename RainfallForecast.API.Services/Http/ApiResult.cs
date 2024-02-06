namespace RainfallForecast.API.Services.Http
{
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// Response Content
        /// </summary>
        public T Content { get; set; }
    }

    public class ApiResult
    {
        /// <summary>
        /// Details of the original Request
        /// </summary>
        public ApiRequest Request { get; set; }


        /// <summary>
        /// Http Status reason phrase
        /// </summary>
        public string StatusReasonPhrase { get; set; }

        /// <summary>
        /// Raw Response Content
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Check if Result is successful
        /// </summary>
        public bool IsSuccess => string.IsNullOrEmpty(Error);

        /// <summary>
        /// Headers of the response
        /// </summary>
        public Dictionary<string, IEnumerable<string>> ResponseHeaders { get; set; }
    }
}
