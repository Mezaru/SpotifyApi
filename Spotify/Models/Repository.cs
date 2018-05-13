using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Models
{
    public class Repository
    {
        //    private const string AuthenticationEndpoint = "https://accounts.spotify.com/api/token";
        //    private string _clientId;
        //    private string _clientSecret;

        //      public async Task<Result> GetAuthenticationTokenResponse()
        //    {
        //        _clientId = "996d0037680544c987287a9b0470fdbb";
        //        _clientSecret = "5a3c92099a324b8f9e45d77e919fec13";
        //        var client = new HttpClient();

        //        var content = new FormUrlEncodedContent(new[]
        //        {
        //            new KeyValuePair<string, string>("grant_type", "client_credentials")
        //        });

        //        var authHeader = BuildAuthHeader();

        //        var requestMessage = new HttpRequestMessage(HttpMethod.Post, AuthenticationEndpoint);
        //        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
        //        requestMessage.Content = content;

        //        var response = await client.SendAsync(requestMessage);
        //        var responseString = await response.Content.ReadAsStringAsync();

        //        var authenticationResponse = JsonConvert.DeserializeObject<Result>(responseString);
        //        return authenticationResponse;
        //    }
        //    private string BuildAuthHeader()
        //    {
        //        return Base64Encode(_clientId + ":" + _clientSecret);
        //    }

        //    private string Base64Encode(string plainText)
        //    {
        //        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //        return Convert.ToBase64String(plainTextBytes);
        //    }
    }
}
