using Services.Core.DTO;

namespace Services.Core.Interfaces
{
    public interface ICommandHandler<T>
    {
        Task<GenericCommandResult> Execute(T model);
    }
}
