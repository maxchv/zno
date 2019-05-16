using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Entities;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
{
    public class TestRepository : IGenericRepository<Test>
    {
        private ApplicationContext _context;

        public TestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity != null)
            {
                _context.Test.Remove(entity);
            }
        }

        public async Task<IEnumerable<Test>> Find(Expression<Func<Test, bool>> predicate)
        {
            return await _context.Test.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Test>> FindAll()
        {
            return await _context.Test.ToListAsync();
        }

        public async Task<Test> FindById(object id)
        {
            return await _context.Test.FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(Test entity)
        {
            await _context.Test.AddAsync(entity);
        }

        public async Task Update(Test entityToUpdate)
        {
            var test = await FindById(entityToUpdate.Id);

            if (test is null)
                throw new ArgumentException("Test with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}