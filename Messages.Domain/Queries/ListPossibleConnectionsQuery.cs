using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class ListPossibleConnectionsQuery : IQuery<ListPossibleConnectionsRequest, List<ListPossibleConnectionsResult>>
    {
        private readonly IMessagesDomain _context;

        public ListPossibleConnectionsQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<List<ListPossibleConnectionsResult>> Query(ListPossibleConnectionsRequest model)

        {
            var min = DateTime.MinValue;

            var result = new List<ListPossibleConnectionsResult>();

            if (_context.Connections.Count() == 0)
            {
                result = await _context.Users.ActiveRows()
                    .Where(w => (w.DateDeleted <= min) && 
                    w.Id != model.UserID)
                    .Select(s => new ListPossibleConnectionsResult
                    {
                        ConnectionID = s.Id,
                        Id = s.Id,
                        Email = s.Email,
                        Name = s.Name,
                        Surname = s.Surname,
                        UserName = s.UserName,
                    })
                    .ToListAsync();

                return result;
            }

            var listone = await _context.Connections.ActiveRows()
                            .Where(w => (w.DateDeleted <= min) &&
                            w.ConnectionUserID == model.UserID ||
                             w.UserID == model.UserID)
                            .Join(_context.Users.ActiveRows(), c => c.ConnectionUserID, u => u.Id,
                            (c, u) => new
                            {
                                UserID = c.UserID,
                                ConnectionID = c.ConnectionUserID,
                                //Email = u.Email,
                                //Name = u.Name,
                                //Surname = u.Surname,
                                //UserName = u.UserName,
                            }).ToListAsync();

            var listConnectedOne = new List<int>();

            foreach (var connected in listone)
            {
                listConnectedOne.Add(connected.UserID);
                listConnectedOne.Add(connected.ConnectionID);
            }

            var listConnectedTwo = new List<int>();
            //List of possible connections with connected
            var listtwo = await _context.Users.ActiveRows()
                 .Where(w => (w.DateDeleted <= min) &&
                 w.Id != model.UserID)
                 .Select(s => new
                 {
                     ConnectionID = s.Id,
                 }).ToListAsync();

            foreach (var connected in listtwo)
            {
                listConnectedTwo.Add(connected.ConnectionID);
            }

            //Possible connections
            var listThree = listConnectedTwo.Except(listConnectedOne);

            //GeneratedCodeAttribute results for listthree
            //Email = u.Email,
            //Name = u.Name,
            //Surname = u.Surname,
            //UserName = u.UserName,
            var listPossibleConnectionsResult = new List<ListPossibleConnectionsResult>();

            //if (listThree.Count() > 0)
            //{
            //    foreach (var c in listThree)
            //    {
            //        var list3data = await _context.Users.ActiveRows()
            //                        .Where(u => u.Id.Equals(c)).Select(s => new ListPossibleConnectionsResult
            //                        {
            //                            Id = s.Id,
            //                            Email = s.Email,
            //                            Name = s.Name,
            //                            Surname = s.Surname,
            //                            UserName = s.UserName,
            //                            ConnectionID = s.Id,
            //                        }).FirstOrDefaultAsync();

            //        listPossibleConnectionsResult.Add(list3data);
            //    }

            //    return listPossibleConnectionsResult;
            //}

            foreach (var c in listThree)
            {
                var list3data = await _context.Users.ActiveRows()
                                .Where(u => u.Id.Equals(c)).Select(s => new ListPossibleConnectionsResult
                                {
                                    Id = s.Id,
                                    Email = s.Email,
                                    Name = s.Name,
                                    Surname = s.Surname,
                                    UserName = s.UserName,
                                    ConnectionID = s.Id,
                                }).FirstOrDefaultAsync();

                listPossibleConnectionsResult.Add(list3data);
            }
                      
            return listPossibleConnectionsResult;
        }

        public class ListConnected
        {
            public int Id { get; set; }
        }
    }
}
