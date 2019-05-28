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
    public class TestRepository : IGenericRepository<Test>
    {
        private ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Test with the specified ID not found!!!");

            _context.Tests.Remove(entity);
        }

        public async Task<IEnumerable<Test>> Find(Expression<Func<Test, bool>> predicate)
        {
            return await _context.Tests
                .Include(t => t.Type)
                .Include(t => t.Subject)
                .Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Test>> FindAll()
        {
            return await _context.Tests
                .Include(t => t.Type)
                .Include(t => t.Subject)
                .ToListAsync();
        }

        public async Task<Test> FindById(object id)
        {
            return await _context.Tests
                .Include(t => t.Type)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(Test entity)
        {
            await _context.Tests.AddAsync(entity);
        }

        public async Task Update(Test entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Test with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}