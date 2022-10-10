using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IO.Swagger.Services;

public interface IHttpHandler
{
    Task<Stream> GetAsyncStream(string url);
    Task<HttpResponseMessage> GetAsync(string url);
}

public class HttpClientHandler : IHttpHandler
{
    private HttpClient _client = new HttpClient();


    public async Task<Stream> GetAsyncStream(string url)
    {
        //using (var response = await _client.GetAsync(url)){ 
        var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new FileNotFoundException(url);
            return await response.Content.ReadAsStreamAsync();
        //}
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await _client.GetAsync(url);
    }
    
}