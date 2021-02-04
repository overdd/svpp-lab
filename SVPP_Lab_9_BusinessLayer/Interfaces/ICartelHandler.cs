using SVPP_Lab_9_BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SVPP_Lab_9_BusinessLayer.Interfaces
{
    public interface ICartelHandler
    {
        ObservableCollection<CartelViewModel> GetAll();
        CartelViewModel Get(int id);
        void AddRailwayToCartel(int droupId, RailwayViewModel railway);
        void RemoveRailwayFromCartel(int cartelId, int railwayId);
        void CreateCartel(CartelViewModel cartel);
        void DeleteCartel(int cartelId);
        void UpdateCartel(CartelViewModel cartel);
    }
}
