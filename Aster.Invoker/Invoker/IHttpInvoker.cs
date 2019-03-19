using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aster.ApiInvoker.Invoker
    /// <summary>
    /// asp.net core api子系统调用服务（管理后台、代理后台、用户系统）
    /// </summary>
public interface IHttpInvoker
    {
        Task<T> Get<T>(string apiUrl, IDictionary<string, string> paras, string lang = "cn", int packType = 6);

        Task<ResponseT> Post<RequestT, ResponseT>(string apiUrl, RequestT request, IDictionary<string, string> urlParas = null, string lang = "cn", int packType = 6);
    }
}
