﻿using Microsoft.EntityFrameworkCore;
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
    public class TestSettingsRepository : IGenericRepository<TestSettings>
    {
        private ApplicationDbContext _context;

        public TestSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            var entity = await FindById(id);

            if (entity is null)
                throw new ArgumentException("Settings with the specified ID not found!!!");

            _context.TestSettings.Remove(entity);
        }

        public async Task<IEnumerable<TestSettings>> Find(Expression<Func<TestSettings, bool>> predicate)
        {
            return await _context.TestSettings.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TestSettings>> FindAll()
        {
            return await _context.TestSettings.ToListAsync();
        }

        public async Task<TestSettings> FindById(object id)
        {
            return await _context.TestSettings.FirstOrDefaultAsync(t => t.Id == (int)id);
        }

        public async Task Insert(TestSettings entity)
        {
            await _context.TestSettings.AddAsync(entity);
        }

        public async Task Update(TestSettings entityToUpdate)
        {
            var entity = await FindById(entityToUpdate.Id);

            if (entity is null)
                throw new ArgumentException("Settings with the specified ID not found!!!");

            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}