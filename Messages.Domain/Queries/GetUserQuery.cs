using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class GetUserQuery : IQuery<GetUserRequest, GetUserResult>
    {
        private readonly IMessagesDomain _context;

        public GetUserQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<GetUserResult> Query(GetUserRequest model)
        {
            var getUserResult = new GetUserResult();

            try
            {
                var query = _context.Users
                    .Where(u => u.Email.ToUpper().Contains(model.Email.ToUpper()) && u.Password == model.Password)
                    .Select(u => new GetUserResult
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
