using System.Net;
using Newtonsoft.Json;

namespace Application.Infrastructure.External;

public interface IApiCaller
{
    Task<ApiCallerResponse<T>> GetAsync<T>(string url);
    Task<ApiCallerResponse<T>> PostAsync<T>(string url, dynamic data);
}

public class ApiCaller : IApiCaller
{
    private const int ATTEMPTS = 3;
    private const int DELAY_MILLISECONDS = 300;

    public async Task<ApiCallerResponse<T>> GetAsync<T>(string url)
        => await sendAsync<T>((client) => client.GetAsync(url));
    
    public async Task<ApiCallerResponse<T>> PostAsync<T>(string url, dynamic data)
        => await sendAsync<T>((client) => client.PostAsync(url, data));

    private async Task<ApiCallerResponse<T>> sendAsync<T>(Func<HttpClient, Task<HttpResponseMessage>> send)
    {
        var response = new ApiCallerResponse<T>();
        var retries = 0;

        while (retries < ATTEMPTS)
        {
            var client = new HttpClient();
            var apiResponse = await send(client);
            
            if(apiResponse.IsSuccessStatusCode)
            {
                var apiResponseValue = await apiResponse.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(apiResponseValue);
                response = new ApiCallerResponse<T> { Model = model };
                break;
            }
            
            if(!retryAllowed(apiResponse.StatusCode))
            {
                response = new ApiCallerResponse<T> { Errors = { $"External API Response: { (int) apiResponse.StatusCode } - { apiResponse.ReasonPhrase }" }};
                break;
            }
            
            retries++;
            await Task.Delay(DELAY_MILLISECONDS);
        }

        return response;
    }

    private bool retryAllowed(HttpStatusCode statusCode)
    {
        return statusCode == HttpStatusCode.InternalServerError;
    }
}