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
    public class GeneratedTestRepository : IGenericRepository<GeneratedTest>
    {
        private ApplicationDbContext _context;

        public GeneratedTestRepository(ApplicationDbContext applicationContext)
        {
            this._context = applicationContext;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Generated Test with the specified ID not found!!!");

            _context.GeneratedTests.Remove(entity);
        }

        public async Task<IEnumerable<GeneratedTest>> Find(Expression<Func<GeneratedTest, bool>> predicate)
        {
            return await _context.GeneratedTests.Include(gt=>gt.Questions).Include(gt => gt.Answers).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<GeneratedTest>> FindAll()
        {
            return await _context.GeneratedTests.ToListAsync();
        }

        public async Task<GeneratedTest> FindById(object id)
        {
            return await _context.GeneratedTests.Include(gt => gt.Questions).Include(gt => gt.Answers).Include(gt=>gt.User).FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(GeneratedTest entity)
        {
            await _context.GeneratedTests.AddAsync(entity);
        }

        public async Task Update(GeneratedTest entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Generated Test with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}