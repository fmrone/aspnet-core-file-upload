using FMR.Image.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMR.Image.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(string id);
        List<T> FindAll();
        void Delete(string id);
    }
}
