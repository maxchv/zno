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
    public class QuestionTypeRepository : IGenericRepository<QuestionType>
    {
        private ApplicationDbContext _context;

        public QuestionTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.QuestionTypes.Remove(entity);
        }

        public async Task<IEnumerable<QuestionType>> Find(Expression<Func<QuestionType, bool>> predicate)
        {
            return await _context.QuestionTypes.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<QuestionType>> FindAll()
        {
            return await _context.QuestionTypes.ToListAsync();
        }

        public async Task<QuestionType> FindById(object id)
        {
            return await _context.QuestionTypes.FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(QuestionType entity)
        {
            await _context.QuestionTypes.AddAsync(entity);
        }

        public async Task Update(QuestionType entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}