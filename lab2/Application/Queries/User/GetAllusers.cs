using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Queries.User
{
    public class GetAllusers
    {
        public record Query() : IRequest<List<Domain.Models.User>>;


        public class Handler : IRequestHandler<Query, List<Domain.Models.User>>
        {
            private IUserRepository _repository;
            public Handler(IUserRepository repo)
            {
                _repository = repo;
            }
            public async Task<List<Domain.Models.User>> Handle(Query request, CancellationToken cancellationToken)
            {
               List<Domain.Models.User> users = await _repository.GetAllusersAsync();
               
               return users != null && users.Count > 0 ? users : null;
            }
        } 
    }
}