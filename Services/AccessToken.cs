// Services/AccessToken.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class GetAccessToken
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;
    private readonly string _code;

    public GetAccessToken(string code)
    {
        _clientId = "0oacs1087qDla7ZCP5d7";
        _clientSecret = "exXabw4jGKFQSDM70S86Pmxrt4wUQjefdeCcXnTDieh95-FiK2f7Jydin0SxyAOn";
        _redirectUri = "https://github.com/LopesZ3R4/WebApiBackend";
        _code = code;
    }

    public async Task<string> ExecuteAsync()
    {
        using (var client = new HttpClient())
        {
            var tokenEndpoint = new Uri("https://signin.johndeere.com/oauth/auz/token");
            var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", _code),
                new KeyValuePair<string, string>("redirect_uri", _redirectUri),
                new KeyValuePair<string, string>("client_id", _clientId)
            });

            var response = await client.PostAsync(tokenEndpoint, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}