using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class GetUserDetailsQuery : IQuery<GetUserDetailsRequest, GetUserDetailsResult>
    {
        private readonly IMessagesDomain _context;

        public GetUserDetailsQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<GetUserDetailsResult> Query(GetUserDetailsRequest model)
        {
            var getUserResult = new GetUserDetailsResult();

            try
            {
                var query = _context.Users
                    .Where(u => u.Email.ToUpper().Contains(model.Email.ToUpper()) || u.UserName.ToUpper().Contains(model.UserName.ToUpper()))
                    .Select(u => new GetUserDetailsResult
                    {
                        Name = u.Name,
                        Surname = u.Surname,
                        UserID = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,
                        Identifier = u.Identifier,
                        DateOfBirth = u.DateOfBirth,
                        Password = u.Password,
                        CellNumber = u.CellNumber,
                        UserRoleID = u.UserRoleID
                    })
                    .FirstOrDefault();

                if (query == null || query.UserName == null)
                {
                    getUserResult.Error = "User does not exist";
                    return getUserResult;
                }

                getUserResult = query;
                //getUserResult.UserName = "Test";
                //getUserResult.Password = "Test";

                return getUserResult;
            }
            catch (Exception ex)
            {
                getUserResult.Error = ex.Message;

                return getUserResult;
            }
        }
    }
}
