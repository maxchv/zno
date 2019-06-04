using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;

namespace Zno.DAL.Implementation
{
    public class UserAnswerRepository : IGenericRepository<UserAnswer>
    {
        private ApplicationDbContext _context;

        public UserAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("User Answer type with the specified ID not found!!!");

            _context.UserAnswers.Remove(entity);
        }

        public async Task<IEnumerable<UserAnswer>> Find(Expression<Func<UserAnswer, bool>> predicate)
        {
            return await _context.UserAnswers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<UserAnswer>> FindAll()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        public async Task<UserAnswer> FindById(object id)
        {
            return await _context.UserAnswers.FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(UserAnswer entity)
        {
            await _context.UserAnswers.AddAsync(entity);
        }

        public async Task Update(UserAnswer entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("User Answer with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}