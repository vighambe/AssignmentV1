using ContactManagement.Lib.AbstractRepository;
using ContactManagement.Lib.Context;
using ContactManagement.Lib.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactManagement.Lib.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
            where T : class, IModelBase, new()
    {
        private ContactContext _context;

        #region Properties

        public BaseRepository(ContactContext context)
        {
            _context = context;
        }

        #endregion

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }


        public T GetSingle(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        //public virtual void Add(T entity)
        //{
        //    EntityEntry dbEntityEntry = _context.Entry<T>(entity);
        //    _context.Set<T>().Add(entity);
        //    _context.SaveChanges(true);
        //}

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
           
        }

        public void Add(Contact model)
        {
            model.Id = GetNextId();
            _context.Contacts.Add(model);
            _context.SaveChanges(true);

        }

        private int GetNextId()
        {
            return (_context.Contacts.Count() + 1);
            

        }
    }
}
