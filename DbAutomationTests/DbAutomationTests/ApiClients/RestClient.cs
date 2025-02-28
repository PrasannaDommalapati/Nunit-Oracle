using RestSharp;

namespace DbAutomationTests.ApiClients;

public class RestApiClient
{
    private readonly RestClient _client;

    public RestApiClient(string baseUrl) => _client = new RestClient(baseUrl);

    public async Task<string> GetDataAsync(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Get);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> PostDataAsync(string endpoint, object data)
    {
        var request = new RestRequest(endpoint, Method.Post);
        request.AddJsonBody(data);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> PutDataAsync(string endpoint, object data)
    {
        var request = new RestRequest(endpoint, Method.Put);
        request.AddJsonBody(data);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> DeleteDataAsync(string endpoint)
    {
        var request = new RestRequest(endpoint, Method.Delete);
        return await ExecuteRequestAsync(request);
    }

    private async Task<string> ExecuteRequestAsync(RestRequest request)
    {
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"REST API Error: {response.StatusCode} - {response.ErrorMessage}");
        }

        return response.Content!;
    }
}