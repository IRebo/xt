using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xtrance
{
    /*public class WebClientInternal : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();
        private Log.LogFunction _logFunction;

        public CookieContainer Cookies { get { return _cookieContainer; } }

        public int Timeout { get; set; }

        public WebClientInternal(Log.LogFunction logFunction)
        {
            _logFunction = logFunction;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            _logFunction($"Sending request '{address}'");

            WebRequest request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            if (request is HttpWebRequest myrequest)
            {
                myrequest.ReadWriteTimeout = Timeout;
                myrequest.CookieContainer = _cookieContainer;
                myrequest.AllowAutoRedirect = false;
            }
            return request;
        }
    }*/

    public class WebClientEx
    {
        private HttpClient _client2;
        private Log.LogFunction _logFunction;
        private CancellationToken _cancellationToken;
        public WebClientEx(Log.LogFunction logFunction, Encoding encoding, int timeout, CancellationToken token)
        {
            _client2 = new HttpClient();
            _client2.Timeout = TimeSpan.FromMilliseconds(timeout);
            _logFunction = logFunction;
            _cancellationToken = token;
        }
        public int Retries { get; set; } = 5;

        public HttpRequestHeaders Headers
        {
            get { return _client2.DefaultRequestHeaders; }
            //set { _client2.DefaultRequestHeaders = value; }
        }

        internal async Task<T> Retry<T>(Func<Task<T>> action)
        {
            int retryCount = 0;
            while (true)
            {
                _cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    return await action();
                }
                catch (TaskCanceledException ex)
                {
                    retryCount++;
                    if (retryCount > Retries)
                    {
                        throw;
                    }
                    else
                    {
                        _logFunction($"retrying after exception '{ex}'");
                    }
                }
            }
        }

        public Task<string> DownloadStringTaskAsync(string address)
        {
            _logFunction($"Sending DownloadStringTaskAsync request '{address}'");
            return Retry(async () => await (await _client2.GetAsync(address, _cancellationToken)).Content.ReadAsStringAsync());
        }

        public Task<byte[]> UploadValuesTaskAsync(string address, Dictionary<string, string> data)
        {
            _logFunction($"Sending UploadValuesTaskAsync request '{address}'");
            return Retry(async () => await (await _client2.PostAsync(address, new FormUrlEncodedContent(data), _cancellationToken)).Content.ReadAsByteArrayAsync());
        }

        public Task<byte[]> DownloadDataTaskAsync(string address)
        {
            _logFunction($"Sending DownloadDataTaskAsync request '{address}'");
            return Retry(async () => await (await _client2.GetAsync(address, _cancellationToken)).Content.ReadAsByteArrayAsync());
        }

    }
}