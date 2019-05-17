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
    public class QuestionRepository : IGenericRepository<Question>
    {
        private ApplicationContext _context;

        public QuestionRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Question>> Find(Expression<Func<Question, bool>> predicate)
        {
            return await _context.Questions.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Question>> FindAll()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> FindById(int id)
        {
            return await _context.Questions.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Question entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
