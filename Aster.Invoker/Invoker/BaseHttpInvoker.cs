using Aster.ApiInvoker.Exceptions;
using Aster.ApiInvoker.Models.Commons;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aster.ApiInvoker.Invoker
{
    public abstract class BaseHttpInvoker : IHttpInvoker
    {
        private readonly HttpClient _httpClient;

        public BaseHttpInvoker( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<T> Get<T>(string apiUrl, IDictionary<string, string> paras, string lang = "cn", int packType = 6)
        {
            if (string.IsNullOrWhiteSpace(apiUrl)) throw new ArgumentNullException(nameof(apiUrl));

            string api = apiUrl;
            paras = AddCommonParas(paras, lang);

            api = QueryHelpers.AddQueryString(apiUrl, paras);

           // _logger.LogInformation("get {Url}", new Uri(_httpClient.BaseAddress, api));

            var request = new HttpRequestMessage(HttpMethod.Get, api)
            {
                Headers = { { "packType", packType.ToString() } }
            };

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

               // _logger.LogInformation("url: {Url}, method: {HttpMethod}, response: content: {ReponseContent}", api, "get", content);

                var rps = JsonConvert.DeserializeObject<ApiResponseModel<T>>(content);

                if (rps.Ret != 0)
                {
                    throw new InvokerFailException(rps.ErrCode, rps.ErrStr);
                }

                return rps.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default(T);
            }
            else
            {
                throw new InvokerFailException(response.StatusCode.ToString());
            }
        }

        private IDictionary<string, string> AddCommonParas(IDictionary<string, string> paras, string lang)
        {
            paras = paras ?? new Dictionary<string, string>();
            paras.TryAdd("culture", lang);

            return paras;
        }

        public async Task<ResponseT> Post<RequestT, ResponseT>(string apiUrl,
            RequestT requestPara,
            IDictionary<string, string> urlParas = null, string lang = "cn", int packType = 6)
        {
            if (string.IsNullOrWhiteSpace(apiUrl)) throw new ArgumentNullException(nameof(apiUrl));

            urlParas = AddCommonParas(urlParas, lang);

            string api = QueryHelpers.AddQueryString(apiUrl, urlParas);

            string reqJson = JsonConvert.SerializeObject(requestPara);

           // _logger.LogInformation("post {Url}，参数：{Parameter}", new Uri(_httpClient.BaseAddress, api), reqJson);

            var request = new HttpRequestMessage(HttpMethod.Post, api)
            {
                Headers = { { "packType", packType.ToString() } },
                Content = new StringContent(reqJson, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            //_logger.LogInformation("url: {Url}, method: {HttpMethod}, response: status: {StatusCode}", api, "post", response.StatusCode);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();

               // _logger.LogInformation("url: {Url}, method: {HttpMethod}, response: content: {Content}", api, "post", content);

                var rps = JsonConvert.DeserializeObject<ApiResponseModel<ResponseT>>(content);

                if (rps.Ret != 0)
                {
                    throw new InvokerFailException(rps.ErrCode, rps.ErrStr);
                }

                return rps.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default(ResponseT);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
