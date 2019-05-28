using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;

namespace Zno.DAL.Implementation
{
    public class QuestionRepository : IGenericRepository<Question>
    {
        private ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Question with the specified ID not found!!!");

            _context.Questions.Remove(entity);
        }

        public async Task<IEnumerable<Question>> Find(Expression<Func<Question, bool>> predicate)
        {
            return await _context.Questions.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Question>> FindAll()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> FindById(object id)
        {
            return await _context.Questions.FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(Question entity)
        {
            await _context.Questions.AddAsync(entity);
        }

        public async Task Update(Question entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Question with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
