using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class ListConnectionsByUserIdQuery : IQuery<ListConnectionsByUserIdRequest, List<ListConnectionsByUserIdResult>>
    {
        private readonly IMessagesDomain _context;

        public ListConnectionsByUserIdQuery(IMessagesDomain context)
        {
            _context = context;
        }
        public async Task<List<ListConnectionsByUserIdResult>> Query(ListConnectionsByUserIdRequest model)
        {

            var result = new List<ListConnectionsByUserIdResult>();
            var min = DateTime.MinValue;

            if (_context.Connections.Where(w => w.DateDeleted <= min && w.UserID == model.UserID).Count() > 0)
            {

                var query = await _context.Connections.ActiveRows()
                    .Where(w => w.UserID == model.UserID && w.DateDeleted <= min)
                    .Join(_context.Users, c => c.ConnectionUserID, u => u.Id,
                    (c, u) => new ListConnectionsByUserIdResult
                    {
                        Name = u.Name,
                        Surname = u.Surname,
                        UserName = u.UserName,
                        Email = u.Email,
                        Id = c.Id,
                        ConnectionID = c.ConnectionUserID == model.UserID ? c.UserID : c.ConnectionUserID
                    }).ToListAsync();

                result.AddRange(query);
            }

            //w.UserID == model.UserID 
            var test = _context.Connections.Where(w =>  w.DateDeleted <= min && w.ConnectionUserID == model.UserID).Count();

            if (_context.Connections.Where(w => w.DateDeleted <= min && w.ConnectionUserID == model.UserID).Count() > 0)
            {

                var query = await _context.Connections.ActiveRows()
                    .Where(w => w.ConnectionUserID == model.UserID && w.DateDeleted <= min)
                    .Join(_context.Users, c => c.UserID, u => u.Id,
                    (c, u) => new ListConnectionsByUserIdResult
                    {
                        Name = u.Name,
                        Surname = u.Surname,
                        UserName = u.UserName,
                        Email = u.Email,
                        Id = c.Id,
                        ConnectionID = c.ConnectionUserID == model.UserID ? c.UserID : c.ConnectionUserID
                    }).ToListAsync();

                result.AddRange(query);
            }

            return result;
        }
    }
}
