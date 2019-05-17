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
    public class GeneratedTestRepository : IGenericRepository<GeneratedTest>
    {
        private ApplicationContext _context;

        public GeneratedTestRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            //_context.Remove(_context.GeneratedTests.Where(t => t.Id == id));
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GeneratedTest>> Find(Expression<Func<GeneratedTest, bool>> predicate)
        {
            return await _context.GeneratedTests.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<GeneratedTest>> FindAll()
        {
            return await _context.GeneratedTests.ToListAsync();
        }

        public async Task<GeneratedTest> FindById(int id)
        {
            return await _context.GeneratedTests.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(GeneratedTest entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(GeneratedTest entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
