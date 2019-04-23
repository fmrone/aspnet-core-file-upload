using FMR.Image.Data.Base;
using FMR.Image.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMR.Image.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private ImageDbContext _context;

        //declaração de um dataset genérico
        private DbSet<T> _dataset;

        public Repository(ImageDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            var entity = _dataset.SingleOrDefault(q => q.Id == id);

            try
            {
                if (entity == null)
                    _dataset.Remove(entity);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(string id)
        {
            return _dataset.SingleOrDefault(q => q.Id == id);
        }
    }
}
