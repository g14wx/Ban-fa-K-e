using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.DTOs;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllusersAsync();
        public Task<User> FindUserByIdAsync(int Id);

        public Task<User> CreateUser(User user);
    }
}