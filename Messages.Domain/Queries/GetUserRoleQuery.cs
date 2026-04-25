using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class GetUserRoleQuery : IQuery<GetUserRoleRequest, GetUserRoleResult>
    {
        private readonly IMessagesDomain _context;

        public GetUserRoleQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<GetUserRoleResult> Query(GetUserRoleRequest model)
        {
            var getUserRoleResult = new GetUserRoleResult();

            var userRoleResult = _context.UserRoles
                .Where(ur => ur.Role.ToUpper().Contains(model.RoleName.ToUpper()))
                .Select(ur => new GetUserRoleResult
                {
                    Id = ur.Id,
                    RoleName = ur.Role
                })
                .FirstOrDefault();

            if (userRoleResult.RoleName == null)
            {
                return getUserRoleResult;
            }

            return userRoleResult;
        }
    }
}
