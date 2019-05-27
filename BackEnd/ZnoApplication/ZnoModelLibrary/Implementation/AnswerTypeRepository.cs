using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZnoModelLibrary.Context;
using ZnoModelLibrary.Entities;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
{
    public class AnswerTypeRepository : IGenericRepository<AnswerType>
    {
        private ApplicationDbContext _context;

        public AnswerTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.AnswerTypes.Remove(entity);
        }

        public async Task<IEnumerable<AnswerType>> Find(Expression<Func<AnswerType, bool>> predicate)
        {
            return await _context.AnswerTypes.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<AnswerType>> FindAll()
        {
            return await _context.AnswerTypes.ToListAsync();
        }

        public async Task<AnswerType> FindById(object id)
        {
            return await _context.AnswerTypes.FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(AnswerType entity)
        {
            await _context.AnswerTypes.AddAsync(entity);
        }

        public async Task Update(AnswerType entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}