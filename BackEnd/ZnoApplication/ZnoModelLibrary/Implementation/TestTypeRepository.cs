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
    public class TestTypeRepository : IGenericRepository<TestType>
    {
        private ApplicationContext _context;

        public TestTypeRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestType>> Find(Expression<Func<TestType, bool>> predicate)
        {
            return await _context.TestTypes.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TestType>> FindAll()
        {
            return await _context.TestTypes.ToListAsync();
        }

        public async Task<TestType> FindById(int id)
        {
            return await _context.TestTypes.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(TestType entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(TestType entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
