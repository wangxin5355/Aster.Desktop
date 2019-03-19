using Aster.ApiInvoker.HttpClients;

namespace Aster.ApiInvoker.Invoker
{
    public class UserInvoker : BaseHttpInvoker
    {
        public UserInvoker( UserClient httpClient) : base(httpClient.Client)
        {
        }
    }
}
