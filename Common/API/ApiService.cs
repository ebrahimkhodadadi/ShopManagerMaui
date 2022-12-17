using Newtonsoft.Json;
using System.Text;

namespace Common.API;
public class ApiService : IApiService
{
    public ApiService()
    {

    }

    public async Task<T?> SendRequestAsync<T>(string uri, string payload, HttpMethod method)
    {
        var startDate = DateTime.Now;

        //Send Request
        using (var httpclient = new HttpClient())
        using (var request = new HttpRequestMessage(method, uri))
        {
            // set query
            using (var stringContent = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                if (!string.IsNullOrWhiteSpace(payload))
                    request.Content = stringContent;
                else
                    request.Content = null;

                // Send Request
                using (var response = await httpclient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
        }
    }
}
