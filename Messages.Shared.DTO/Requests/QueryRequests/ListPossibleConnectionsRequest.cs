namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListPossibleConnectionsRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public int UserID { get; set; }
    }
}
