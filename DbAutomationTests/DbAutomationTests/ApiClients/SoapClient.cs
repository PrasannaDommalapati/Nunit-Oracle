using RestSharp;

namespace DbAutomationTests.ApiClients;

public class SoapClient
{
    private readonly RestClient _client;

    public SoapClient(string baseUrl) => _client = new RestClient(baseUrl);

    public async Task<string> PostSoapServiceAsync(string resourceUrl, string soapAction, string soapBody)
    {
        var request = CreateSoapRequest(resourceUrl, Method.Post, soapAction, soapBody);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> GetSoapServiceAsync(string resourceUrl, string soapAction)
    {
        var request = CreateSoapRequest(resourceUrl, Method.Get, soapAction);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> PutSoapServiceAsync(string resourceUrl, string soapAction, string soapBody)
    {
        var request = CreateSoapRequest(resourceUrl, Method.Put, soapAction, soapBody);
        return await ExecuteRequestAsync(request);
    }

    public async Task<string> DeleteSoapServiceAsync(string resourceUrl, string soapAction)
    {
        var request = CreateSoapRequest(resourceUrl, Method.Delete, soapAction);
        return await ExecuteRequestAsync(request);
    }

    private RestRequest CreateSoapRequest(string resourceUrl, Method method, string soapAction, string? soapBody = null)
    {
        var request = new RestRequest(resourceUrl, method);
        request.AddHeader("Content-Type", "text/xml");
        request.AddHeader("SOAPAction", soapAction);
        if (soapBody != null)
        {
            request.AddStringBody(soapBody, DataFormat.Xml);
        }
        return request;
    }

    private async Task<string> ExecuteRequestAsync(RestRequest request)
    {
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"SOAP API Error: {response.StatusCode} - {response.ErrorMessage}");
        }

        return response.Content!;
    }
}
