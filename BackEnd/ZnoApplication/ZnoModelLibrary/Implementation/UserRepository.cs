using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZnoModelLibrary.Abstraction;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Entities;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
{
    public class UserRepository : IGenericRepository<ApplicationUser>, IUserRepository<ApplicationUser>
    {
        private ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> Find(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<ApplicationUser> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(login) || u.PhoneNumber.Equals(login));
        }

        public Task Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationUser entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}