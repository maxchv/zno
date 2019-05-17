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
    public class TestSettingsRepository : IGenericRepository<TestSettings>
    {
        private ApplicationContext _context;

        public TestSettingsRepository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestSettings>> Find(Expression<Func<TestSettings, bool>> predicate)
        {
            return await _context.TestSettings.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TestSettings>> FindAll()
        {
            return await _context.TestSettings.ToListAsync();
        }

        public async Task<TestSettings> FindById(int id)
        {
            return await _context.TestSettings.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task Insert(TestSettings entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(TestSettings entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
