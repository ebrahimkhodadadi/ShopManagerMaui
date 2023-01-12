using System.Net.Http;
using System.Threading;

namespace Common.API;

public interface IApiService //: IScopedDependency
{
    /// <summary>
    /// ارسال درخواست به سرویس
    /// </summary>
    /// <param name="uri">آدرس</param>
    /// <param name="payload">محتوا</param>
    /// <param name="method">متد</param>
    /// <returns>response httpRequest</returns>
    Task<T?> SendRequestAsync<T>(string uri, object? payload, HttpMethod method);
}

