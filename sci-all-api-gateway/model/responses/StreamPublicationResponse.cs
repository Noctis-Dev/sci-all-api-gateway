namespace sci_all_api_gateway.model.responses;

public class StreamPublicationResponse
{
    public string? stream_uuid { get; set; }
    public string? publication_uuid { get; set; }
    public string? user_uuid { get; set; }
    public string? access_token { get; set; }
    public string? created_at { get; set; }
    public string? finalized_at { get; set; }
    public string? body { get; set; }
    public string? type { get; set; }
}