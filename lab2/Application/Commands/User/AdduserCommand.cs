using System;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Commands.User
{
    public static class AdduserCommand
    {
        public record Command(String Name) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private IUserRepository _repository;

            public Handler(IUserRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                Domain.Models.User user = new Domain.Models.User
                {
                    Name = request.Name
                };
                user = await _repository.CreateUser(user);

                return user.Id;
            }
        }
    }
}