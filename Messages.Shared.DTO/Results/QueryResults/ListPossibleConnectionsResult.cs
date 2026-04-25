using System.ComponentModel.DataAnnotations.Schema;

namespace Messages.Shared.DTO.Results.QueryResults
{
    public class ListPossibleConnectionsResult
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int ConnectionID { get; set; }
    }
}
