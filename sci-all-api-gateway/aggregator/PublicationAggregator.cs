using System.Net.Http.Headers;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using sci_all_api_gateway.model.responses;

namespace sci_all_api_gateway.aggregator;

public class PublicationAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var resourcesResponse = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
        var publicationsResponse = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
        
        var resources = JsonConvert.DeserializeObject<List<Resource>>(resourcesResponse);
        var publications = JsonConvert.DeserializeObject<List<Publication>>(publicationsResponse);
        
        var publicationDic = publications?.ToDictionary(r => r.publication_uuid!);
        var result = new List<PublicationResourceResponse>();

        foreach (var resource in resources!)
        {
            if (publicationDic!.TryGetValue(resource.owner_uuid!, out var pub))
            {
                var publicationResource = new PublicationResourceResponse()
                {
                    publication_uuid = pub.publication_uuid,
                    body = pub.body,
                    type = pub.type,
                    created_at = pub.created_at,
                    user_uuid = pub.user_uuid,
                    resource_url = resource.url!
                };
                result.Add(publicationResource);
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