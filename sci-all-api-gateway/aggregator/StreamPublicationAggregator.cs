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
        
        var streamsDict = streams?.ToDictionary(s => s.uuid!);
        var result = new List<StreamPublicationResponse>();

        foreach (var pub in publication!)
        {
            if (streamsDict!.TryGetValue(pub.secondary_item_uuid!, out var stream))
            {
                var streamPublication = new StreamPublicationResponse()
                {
                    stream_uuid = stream.uuid,
                    publication_uuid = pub.publication_uuid,
                    user_uuid = stream.user_uuid,
                    access_token = stream.access_token,
                    created_at = stream.created_at,
                    finalized_at = stream.finalized_at,
                    body = pub.body,
                    type = pub.type
                };
                result.Add(streamPublication);
            }
        }
        
        var resultJson = JsonConvert.SerializeObject(result);
        var stringContent = new StringContent(resultJson)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
        
        return new DownstreamResponse(
            stringContent, 
            System.Net.HttpStatusCode.OK, 
            new List<KeyValuePair<string, 
                IEnumerable<string>>>(), 
            "OK"
        );
    }
}