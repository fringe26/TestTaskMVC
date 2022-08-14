﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMVC.DAL.DataBase;
using TestTaskMVC.DAL.IRepository;

namespace TestTaskMVC.DAL.Repository
{

    public class EfCoreRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public EfCoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<T> DetailsById(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task<bool> AddAsync(T item)
        {
            try
            {
                await _context.Set<T>().AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(T item)
        {
            try
            {
                _context.Set<T>().Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

      
    }
}
