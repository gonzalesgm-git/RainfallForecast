using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace RainfallForecast.API.Services.Http
{
    public abstract class RestApiClient
    {

        private HttpClient _httpClient;
        protected HttpClient HttpClient => _httpClient ?? (_httpClient = CreateHttpClient());



        protected virtual HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }


        protected async Task<ApiResult<T>> GetAsync<T>(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await HandleRequestAsync<T>(HttpMethod.Get, url, null, null, cancellationToken).ConfigureAwait(false);
        }

        protected virtual async Task<ApiResult<T>> HandleRequestAsync<T>(HttpMethod httpMethod, string url, object content, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = BuildHttpRequestMessage(httpMethod, url, content, headers);
            var requestInfo = BuildApiRequest(request, url, content);
            try
            {
                var webResponse = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
                return await HandleResponseAsync<T>(webResponse, requestInfo).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                return HandleError<T>(requestInfo, exception);
            }
        }




        protected virtual HttpRequestMessage BuildHttpRequestMessage(HttpMethod httpMethod, string url, object content, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            AddHeaders(request, headers);

            if (content is HttpContent)
            {
                request.Content = content as HttpContent;
            }
            else
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (content != null)
                {
                    request.Content = GetContent(content);
                }
            }

            return request;
        }

        protected virtual ApiRequest BuildApiRequest(HttpRequestMessage request, string url, object content)
        {
            return new ApiRequest
            {
                Url = url,
                Method = request.Method.Method,
                Headers = request.Headers.ToDictionary(x => x.Key, x => x.Value),
                Content = JsonConvert.SerializeObject(content)
            };
        }



        protected virtual async Task<ApiResult<T>> HandleResponseAsync<T>(HttpResponseMessage response, ApiRequest requestInfo)
        {
            var responseResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = new ApiResult<T>
            {
                Request = requestInfo,
                StatusReasonPhrase = response.ReasonPhrase,
                ResponseContent = responseResult,
                Error = BuildErrorMessage(response, requestInfo, responseResult)
            };

            if (result.IsSuccess)
            {
                result.Content = JsonConvert.DeserializeObject<T>(responseResult,
                    new JsonSerializerSettings
                    {
                        Error = (sender, args) =>
                        {
                            result.Error = args.ErrorContext.Error.Message + ". Response content: " + responseResult;
                            args.ErrorContext.Handled = true;
                        }
                    });
            }

            return result;
        }

        protected virtual string BuildErrorMessage(HttpResponseMessage response, ApiRequest requestInfo, string responseResult)
        {
            return !response.IsSuccessStatusCode
                ? $"Request returned Unsuccessful status: {response.StatusCode} for url: {response.RequestMessage.RequestUri}. Content: {responseResult}."
                : null;
        }

        protected StreamContent GetBufferedContent(string filename, object content)
        {
            using (var fs = File.Open(filename, FileMode.CreateNew))
            using (var sw = new StreamWriter(fs))
            using (var jw = new JsonTextWriter(sw))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(jw, content);
            }

            return new StreamContent(new FileStream(filename, FileMode.Open));
        }

        protected virtual StringContent GetContent(object content)
        {
            var stringContent = JsonConvert.SerializeObject(content);
            return new StringContent(stringContent, Encoding.UTF8, "application/json");
        }

        protected virtual ApiResult HandleError(ApiRequest requestInfo, Exception ex)
        {
            return new ApiResult
            {
                Request = requestInfo,
                Error = $"An exception occurred for url {requestInfo.Url} - {ex}"
            };
        }

        protected virtual ApiResult<T> HandleError<T>(ApiRequest requestInfo, Exception ex)
        {
            return new ApiResult<T>
            {
                Request = requestInfo,
                Error = $"An exception occurred for url {requestInfo.Url} - {ex}"
            };
        }

        protected void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }
    }

    public class ApiRequest
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public Dictionary<string, IEnumerable<string>> Headers { get; set; }
    }
}
