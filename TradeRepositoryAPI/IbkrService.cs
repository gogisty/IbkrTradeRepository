using System.Xml.Serialization;
using TradeRepositoryAPI.IbkrResponses;

namespace TradeRepositoryAPI
{
    public class IbkrService
    {
        private readonly HttpClient _httpClient;

        public IbkrService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetReferenceCode(string queryNumber, string token)
        {
            var requestResponse = await _httpClient.GetAsync($"SendRequest?t={token}&q={queryNumber}&v=3");

            if (requestResponse.IsSuccessStatusCode)
            {
                var stream = await requestResponse.Content.ReadAsStreamAsync();
                var serializer = new XmlSerializer(typeof(FlexStatementResponse));
                var response = (FlexStatementResponse)serializer.Deserialize(stream);


                return response.ReferenceCode;
            }
            else
            {
                return null;
            }
        }

        public async Task<FlexQueryResponse?> GetStatements(string referenceNumber, string token)
        {
            var requestResponse = await _httpClient.GetAsync($"GetStatement?t={token}&q={referenceNumber}&v=3");

            if (requestResponse.IsSuccessStatusCode)
            {
                var stream = await requestResponse.Content.ReadAsStreamAsync();
                var serializer = new XmlSerializer(typeof(FlexQueryResponse));
                var response = (FlexQueryResponse)serializer.Deserialize(stream);


                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
