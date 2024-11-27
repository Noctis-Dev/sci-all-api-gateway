namespace sci_all_api_gateway.model.responses;

public class PublicationResourceResponse
{
    public string? publication_uuid { get; set; }
    public string? body { get; set; }
    public string? type { get; set; }
    public string? created_at { get; set; }
    public string? user_uuid { get; set; }
    public string? secondary_item_uuid { get; set; }
    public string resource_url { get; set; }
}