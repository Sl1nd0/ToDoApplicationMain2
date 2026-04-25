namespace Messages.Shared.DTO.Results.QueryResults
{
    public class GetUserDetailsResult
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Identifier { get; set; }
        public string Password { get; set; }
        public string CellNumber { get; set; }
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public string Error { get; set; }
    }
}
