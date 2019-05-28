using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zno.DAL.Abstraction;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;

namespace Zno.DAL.Implementation
{
    public class UserRepository : IGenericRepository<ApplicationUser>, IUserRepository<ApplicationUser>
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("User with the specified ID not found!!!");

            _context.Users.Remove(entity);
        }

        public async Task<IEnumerable<ApplicationUser>> Find(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> FindById(object id)
        {
            return await _context.Users.FirstOrDefaultAsync(t => t.Id.Equals(id.ToString()));
        }

        public async Task<ApplicationUser> FindByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(login) || u.PhoneNumber.Equals(login));
        }

        public Task Insert(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ApplicationUser entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("User with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}