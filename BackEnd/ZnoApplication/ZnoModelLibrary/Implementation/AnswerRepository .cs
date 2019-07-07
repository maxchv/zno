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
    public class AnswerRepository : IGenericRepository<Answer>
    {
        private ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext applicationContext)
        {
            this._context = applicationContext;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Answer with the specified ID not found!!!");

            _context.Answers.Remove(entity);
        }

        public async Task<IEnumerable<Answer>> Find(Expression<Func<Answer, bool>> predicate)
        {
            return await _context.Answers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Answer>> FindAll()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer> FindById(object id)
        {
            return await _context.Answers.Include(a=>a.ContentType).FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
        }

        public async Task Update(Answer entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Answer with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}