namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class GetUserDetailsRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
    }
}
