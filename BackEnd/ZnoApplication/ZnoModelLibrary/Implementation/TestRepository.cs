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
    public class TestRepository : IGenericRepository<Test>
    {
        private ApplicationContext _context;

        public TestRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Test>> Find(Expression<Func<Test, bool>> predicate)
        {
            return await _context.Tests.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Test>> FindAll()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> FindById(int id)
        {
            return await _context.Tests.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(Test entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Test entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
