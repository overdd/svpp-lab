using System;
using System.Collections.Generic;
using System.Text;

namespace SVPP_Lab_9_DataLayer.EFContext
{
    class CoursesContext
    {
        private string name;

        public CoursesContext(string name)
        {
            this.name = name;
        }

        public object Cartels { get; internal set; }
        public object Railways { get; internal set; }

        internal object Entry<T>(T t)
        {
            throw new NotImplementedException();
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
