namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListMessagesRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
