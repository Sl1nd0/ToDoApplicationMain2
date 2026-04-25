namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListMessagesResult
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
