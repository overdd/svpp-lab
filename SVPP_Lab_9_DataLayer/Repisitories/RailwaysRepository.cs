using System;
using System.Collections.Generic;
using System.Linq;
using SVPP_Lab_9_DataLayer.EFContext;
using SVPP_Lab_9_DataLayer.Entities;
using SVPP_Lab_9_DataLayer.Interfaces;



namespace SVPP_Lab_9_DataLayer.Repisitories
{
    class RailwaysRepository : IRepository<Railway>
    {
        readonly CoursesContext context;

        public RailwaysRepository(CoursesContext context)
        {
            this.context = context;
        }

        public void Create(Railway t)
        {
            context.Railways.Add(t);
        }

        public void Delete(int id)
        {
            var student = context.Railways.Find(id);
            context.Railways.Remove(student);
        }

        public IEnumerable<Railway> Find(Func<Railway, bool> predicate)
        {
            return context.Railways
                    .Include("Cartel")
                    .Where(predicate)
                    .ToList();
        }

        public Railway Get(int id)
        {
            return context.Railways.Find(id);
        }

        public IEnumerable<Railway> GetAll()
        {
            return context.Railways.Include("Cartel");
        }

        public void Update(Railway t)
        {
            context.Entry<Railway>(t).State = EntityState.Modified;
        }
    }
}
