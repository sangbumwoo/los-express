using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LosExpress.Utils
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class HttpClientWrapper
    {
        private readonly HttpClient httpClient;

        public HttpClientWrapper()
        {
            httpClient = new HttpClient();
        }

        public HttpClientWrapper(TimeSpan timeout)
        {
            httpClient = new HttpClient { Timeout = timeout };
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> getStringAsync(string requestString)
        {
            string clientResp = null;
            try
            {
                clientResp = await this.httpClient.GetStringAsync(requestString);
            }
            catch (Exception e)
            {
                throw e;
            }

            return clientResp;
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<HttpResponseMessage> getAsync(string requestString)
        {
            HttpResponseMessage clientResp = null;
            try
            {
                clientResp = await this.httpClient.GetAsync(requestString);
            }
            catch (Exception e)
            {
                throw e;
            }

            return clientResp;
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> postAsync(string request, string postBody, string contentType)
        {
            string clientResp = null;
            try
            {
                System.Diagnostics.Debug.WriteLine(request);
                System.Diagnostics.Debug.WriteLine(postBody);

                StringContent content = new StringContent(postBody, UnicodeEncoding.UTF8, contentType);

                HttpResponseMessage message = await this.httpClient.PostAsync(request, content);

                message.EnsureSuccessStatusCode();
                clientResp = await message.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                throw e;
            }
            return clientResp;
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<HttpResponseMessage> postAsync(string request, IEnumerable<KeyValuePair<string, string>> postBody)
        {
            HttpResponseMessage clientResp = null;
            try
            {
                var content = new FormUrlEncodedContent(postBody);
                clientResp = await this.httpClient.PostAsync(request, content);
            }
            catch (Exception e)
            {
                throw e;
            }
            return clientResp;
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task<string> getStringAsync(string requestString, Dictionary<string, string> requestHeaders)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
            string clientResp = null;

            foreach (KeyValuePair<string, string> entry in requestHeaders)
            {
                httpRequestMessage.Headers.Add(entry.Key, entry.Value);
            }

            HttpResponseMessage message = await this.httpClient.SendAsync(httpRequestMessage);
            message.EnsureSuccessStatusCode();
            clientResp = await message.Content.ReadAsStringAsync();

            return clientResp;
        }

        [ExcludeFromCodeCoverage]
        public void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> entry in headers)
            {
                this.httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
        }
    }
}