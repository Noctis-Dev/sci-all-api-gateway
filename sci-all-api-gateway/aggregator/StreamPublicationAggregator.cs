using System.Net.Http.Headers;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using sci_all_api_gateway.model.responses;

namespace sci_all_api_gateway.aggregator;

public class StreamPublicationAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var streamsResponse = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
        var publicationResponse = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
        
        var streams = JsonConvert.DeserializeObject<List<Streams>>(streamsResponse);
        var publication = JsonConvert.DeserializeObject<List<Publication>>(publicationResponse);
        
        var result = new List<StreamPublicationResponse>();
        for (int i = 0; i < streams?.Count && i < publication?.Count; i++)
        {
            var streamPublication = new StreamPublicationResponse()
            {
                stream_uuid = streams[i].uuid,
                publication_uuid = publication[i].publication_uuid,
                user_uuid = streams[i].user_uuid,
                access_token = streams[i].access_token,
                created_at = streams[i].created_at,
                finalized_at = streams[i].finalized_at,
                body = publication[i].body,
                type = publication[i].type
            };
            
            result.Add(streamPublication);
        }
        
        var resultJson = JsonConvert.SerializeObject(result);
        var stringContent = new StringContent(resultJson)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
        
        return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
    }
}