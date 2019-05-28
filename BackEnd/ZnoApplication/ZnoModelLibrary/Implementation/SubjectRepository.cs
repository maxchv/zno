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
    public class SubjectRepository : IGenericRepository<Subject>
    {
        private ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Subject with the specified ID not found!!!");

            _context.Subjects.Remove(entity);
        }

        public async Task<IEnumerable<Subject>> Find(Expression<Func<Subject, bool>> predicate)
        {
            return await _context.Subjects.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Subject>> FindAll()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> FindById(object id)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(Subject entity)
        {
            await _context.Subjects.AddAsync(entity);
        }

        public async Task Update(Subject entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Subject with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}