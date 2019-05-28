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
    public class TestTypeRepository : IGenericRepository<TestType>
    {
        private ApplicationDbContext _context;

        public TestTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Test type with the specified ID not found!!!");

            _context.TestTypes.Remove(entity);
        }

        public async Task<IEnumerable<TestType>> Find(Expression<Func<TestType, bool>> predicate)
        {
            return await _context.TestTypes.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TestType>> FindAll()
        {
            return await _context.TestTypes.ToListAsync();
        }

        public async Task<TestType> FindById(object id)
        {
            return await _context.TestTypes.FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(TestType entity)
        {
            await _context.TestTypes.AddAsync(entity);
        }

        public async Task Update(TestType entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Test type with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}