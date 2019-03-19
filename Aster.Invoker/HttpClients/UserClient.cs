using System.Net.Http;

namespace Aster.ApiInvoker.HttpClients
{
    /// <summary>
    /// 用户系统 httpclient instance
    /// </summary>
    public class UserClient
    {
        public UserClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpClient Client { get; set; }
    }
}
