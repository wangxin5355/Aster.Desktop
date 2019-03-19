using System;

namespace Aster.ApiInvoker.Extensions
{
    public static class DateTimeExtensions
    {
        public static long Unixtimestamp(this DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}
