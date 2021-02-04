using SVPP_Lab_9_DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVPP_Lab_9_DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cartel> Cartels { get; }
        IRepository<Railway> Railways { get; }
        void Save();
    }
}
