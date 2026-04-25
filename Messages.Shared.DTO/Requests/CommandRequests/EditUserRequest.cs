namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class EditUserRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public string CellNumber { get; set; }
    }
}
