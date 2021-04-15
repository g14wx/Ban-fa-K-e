using System;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Queries.User
{
    public class GetuserById
    {
        public record Query(int Id) : IRequest<Response>;
        public record Response(int Id, String Name);
        
        public class Handler : IRequestHandler<GetuserById.Query, GetuserById.Response>
            {
                private IUserRepository _repository;
                public Handler(IUserRepository repository)
                {
                    _repository = repository;
                }
                public async Task<GetuserById.Response> Handle(GetuserById.Query request, CancellationToken cancellationToken)
                {
                    Domain.Models.User user = await _repository.FindUserByIdAsync(request.Id);
                    return user == null ? null : new GetuserById.Response(user.Id,user.Name);
                }
            }
    }
}