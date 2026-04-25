using Messages.Domain.Entities;
using Messages.Domain.Queries;
using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class AddUserHandler : ICommandHandler<AddUserRequest>
    {
        private readonly IRepository<User> _repository;
        private readonly IQuery<GetUserRoleRequest, GetUserRoleResult> _getUserRoleQuery;
        private readonly IQuery<GetUserRequest, GetUserResult> _getUserQuery;

        public AddUserHandler(IRepository<User> repository,
            IQuery<GetUserRoleRequest, GetUserRoleResult> getUserRoleQuery,
            IQuery<GetUserRequest, GetUserResult> getUserQuery)
        {
            _repository = repository;
            _getUserRoleQuery = getUserRoleQuery;
            _getUserQuery = getUserQuery;
        }

        public async Task<GenericCommandResult> Execute(AddUserRequest model)
        {
            var result = new GenericCommandResult();

            try
            {
                model.RoleName = "User";

                if (model.Email == "ssankabi@gmail.com")
                {
                    model.RoleName = "Admin";
                }

                // Check if user role exists
                var userRoleResult = await _getUserRoleQuery.Query(new GetUserRoleRequest { RoleName = model.RoleName });

                // Check if user exists
                var userResult = await _getUserQuery.Query(new GetUserRequest { Email = model.Email, Password = model.Password });

                if (userResult.UserName != null)
                {
                    result.Success = false;
                    result.Errors.Add("User already exists.");
                    return result;
                }

                if (userRoleResult.RoleName == null)
                {
                    result.Success = false;
                    result.Errors.Add("User role does not exist.");
                    return result;
                }

                var entity = new User();

                entity.Name = model.Name;
                entity.Surname = model.Surname;
                entity.UserName = /*model.UserName;*/ model.Email;
                entity.Email = model.Email;
                entity.Identifier = model.Identifier;
                entity.Password = model.Password;
                entity.CellNumber = model.CellNumber;

                entity.DateOfBirth = model.DateOfBirth;

                entity.UserRoleID = userRoleResult.Id;

                entity.DateEntered = DateTime.Now;

                _repository.Add(entity);
                await _repository.SaveChangesAsync();

                result.Success = true;
                //Here
                //userResult = await _getUserQuery.Query(new GetUserRequest { Email = model.Email, Password = model.Password });
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }
    }
}
