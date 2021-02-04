using SVPP_Lab_9_DataLayer.EFContext;
using SVPP_Lab_9_DataLayer.Entities;
using SVPP_Lab_9_DataLayer.Interfaces;
using System;

namespace SVPP_Lab_9_DataLayer.Repositories
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        readonly CoursesContext context;
        CartelsRepository cartelsRepository;
        RailwaysRepository railwaysRepository;
        public EntityUnitOfWork(string name)
        {
            context = new CoursesContext(name);
        }
        public IRepository<Cartel> Groups
        {
            get
            {
                if (cartelsRepository == null)
                    cartelsRepository = new CartelsRepository(context);
                return cartelsRepository;
            }
        }
        public IRepository<Railway> Students
        {
            get
            {
                if (railwaysRepository == null)
                    railwaysRepository =
                    new RailwaysRepository(context);
                return railwaysRepository;
            }
        }

        public IRepository<Cartel> Cartels => throw new NotImplementedException();

        public IRepository<Railway> Railways => throw new NotImplementedException();

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
