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
    public class ContentTypeRepository : IGenericRepository<ContentType>
    {
        private ApplicationDbContext _context;

        public ContentTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.ContentTypes.Remove(entity);
        }

        public async Task<IEnumerable<ContentType>> Find(Expression<Func<ContentType, bool>> predicate)
        {
            return await _context.ContentTypes.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<ContentType>> FindAll()
        {
            return await _context.ContentTypes.ToListAsync();
        }

        public async Task<ContentType> FindById(object id)
        {
            return await _context.ContentTypes.FirstOrDefaultAsync(s => s.Id == (int)id);
        }

        public async Task Insert(ContentType entity)
        {
            await _context.ContentTypes.AddAsync(entity);
        }

        public async Task Update(ContentType entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Answer Type with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
