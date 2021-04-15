using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace lab2.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private MyDbContext _db;

        public UserRepository(IMapper mapper, MyDbContext context)
        {
            _mapper = mapper;
            _db = context;
        }

        public async Task<List<User>> GetAllusersAsync()
        {
            List<EfModels.Models.User> UsersModels = _db.Users.ToList();
            List<User> users = _mapper.Map<List<EfModels.Models.User>,List<User>>(UsersModels);
            return users;
        }

        public async Task<User> FindUserByIdAsync(int Id)
        {
            EfModels.Models.User userModel = _db.Users.FirstOrDefault(x => x.Id == Id);
            User user = _mapper.Map<User>(userModel);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            EfModels.Models.User UserEF = _mapper.Map<EfModels.Models.User>(user);
            await _db.AddAsync(UserEF);
            await _db.SaveChangesAsync();
            return _mapper.Map<User>(UserEF);
        }
    }
}