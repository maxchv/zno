using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Entities;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
{
    public class UserAnswerRepository : IGenericRepository<UserAnswer>
    {
        private ApplicationContext _context;

        public UserAnswerRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserAnswer>> Find(Expression<Func<UserAnswer, bool>> predicate)
        {
            return await _context.UserAnswers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<UserAnswer>> FindAll()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        public async Task<UserAnswer> FindById(int id)
        {
            return await _context.UserAnswers.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(UserAnswer entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserAnswer entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
