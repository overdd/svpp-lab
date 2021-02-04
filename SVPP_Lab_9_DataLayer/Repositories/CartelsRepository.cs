using System;
using System.Collections.Generic;
using System.Linq;
using SVPP_Lab_9_DataLayer.Interfaces;
using SVPP_Lab_9_DataLayer.Entities;
using SVPP_Lab_9_DataLayer.EFContext;
using System.Data.Entity;

namespace SVPP_Lab_9_DataLayer.Repositories
{
    class CartelsRepository : IRepository<Cartel>
    {
        CoursesContext context;

        public CartelsRepository(CoursesContext context)
        {
            this.context = context;
        }

        public void Create(Cartel t)
        {
            context.Cartels.Add(t);
        }

        public void Delete(int id)
        {
            var group = context.Cartels.Find(id);
            context.Cartels.Remove(cartel);
        }

        public IEnumerable<Cartel> Find(Func<Cartel, bool> predicate)
        {
            return context.Cartels
                    .Include(g => g.Students)
                    .Where(predicate)
                    .ToList();
        }

        public Cartel Get(int id)
        {
            return context.Cartels.Find(id);
        }

        public IEnumerable<Cartel> GetAll()
        {
            return context.Cartels.Include(g => g.Students);
        }

        public void Update(Cartel t)
        {
            context.Entry<Cartel>(t).State = EntityState.Modified;
        }
    }
}
