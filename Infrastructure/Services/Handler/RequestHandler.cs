using RestSharp;

namespace Infrastructure.Services.Handler
{
    public class RequestHandler
    {
        public string Post(string clientUrl, string requestUrl)
        {
            
            var client = new RestClient(clientUrl);
            var request = new RestRequest(requestUrl, Method.GET);

            IRestResponse response = client.Execute(request);
            return response.Content;            
        }
    }
}
