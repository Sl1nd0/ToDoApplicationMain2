using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Microsoft.EntityFrameworkCore;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class AddCommentTodoHandler : ICommandHandler<AddCommentTodoRequest>
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMessagesDomain _context;

        public AddCommentTodoHandler(IRepository<Comment> repository, IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<GenericCommandResult> Execute(AddCommentTodoRequest model)
        {
            var result = new GenericCommandResult();

            var queryUser = await _context.Users.Where(w => w.Id.Equals(model.userID)
            || w.Email.ToUpper().Equals(model.UserName.ToUpper())
            ).FirstOrDefaultAsync();

            if (queryUser == null || queryUser.Email == null)
            {
                result.Success = false;
                result.Errors.Add($"User doesn't exist for Id {model.userID}");
            }

            var queryToDo = await _context.ToDos.Where(w => w.Id.Equals(model.ToDoID)).FirstOrDefaultAsync();

            if (queryToDo == null || queryToDo.ToDoTitle == null)
            {
                result.Success = false;
                result.Errors.Add($"A todo doesn't exist for todoID {model.ToDoID}");
            }

            var entity = new Comment();

            entity.UserID = model.userID;
            entity._Comment = model.Comment;
            entity.DateEntered = DateTime.Now;
            entity.ToDoID = model.ToDoID;

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            result.Success = true;
            
            return result;
        }
    }
}
