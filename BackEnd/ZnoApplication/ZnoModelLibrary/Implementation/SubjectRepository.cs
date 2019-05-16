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
    public class SubjectRepository : IGenericRepository<Subject>
    {
        private ApplicationContext _context;

        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
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

        public Task Insert(Subject entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Subject entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}