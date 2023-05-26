﻿using first_asp_app.Contracts;
using first_asp_app.Data;
using Microsoft.EntityFrameworkCore;

namespace first_asp_app.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context) {
            this.context = context;
        }
        public async Task<T>  AddAsync(T entity)
        {
           await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
          await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(int id)
        {
           var entity = await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(entity); 
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if(id == null)
            {
                return null;
            }
            return await context.Set<T>().FindAsync(id);
        }

  
        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
           
        }
    }
}
