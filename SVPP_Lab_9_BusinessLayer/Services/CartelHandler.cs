using System;
using System.Collections.ObjectModel;
using SVPP_Lab_9_BusinessLayer.Interfaces;
using SVPP_Lab_9_DataLayer.Entities;
using SVPP_Lab_9_BusinessLayer.Models;
using SVPP_Lab_9_DataLayer.Interfaces;
using SVPP_Lab_9_DataLayer.Repositories;
using AutoMapper;

namespace SVPP_Lab_9_BusinessLayer.Services
{
    public class CartelHandler : ICartelHandler
    {
        IUnitOfWork dataBase;
        public CartelHandler(string name)
        {
            dataBase = new EntityUnitOfWork(name);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Cartel, CartelViewModel>();
                cfg.CreateMap<Railway, RailwayViewModel>();
                cfg.CreateMap<RailwayViewModel, Railway>();
            });
        }

        public void AddRailwayToCartel(int cartelId, RailwayViewModel railway)
        {
            var cartel = dataBase.Cartels.Get(cartelId);
            var stud = Mapper.Map<Railway>(railway);
            stud.IndividualPrice = railway.IsForSale == true
                ? cartel.BasePrice * (decimal)0.8
                : cartel.BasePrice;
            cartel.Cartels.Add(stud);
            dataBase.Save();
        }

        public void CreateCartel(CartelViewModel cartel)
        {
            throw new NotImplementedException();
        }

        public void DeleteCartel(int cartelId)
        {
            throw new NotImplementedException();
        }

        public CartelViewModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<CartelViewModel> GetAll()
        {
            ObservableCollection<CartelViewModel> railways =
                Mapper.Map<ObservableCollection<CartelViewModel>>(dataBase.Cartels.GetAll());
            return railways;
        }

        public void RemoveRailwayFromCartel(int cartelId, int railwayId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCartel(CartelViewModel cartel)
        {
            throw new NotImplementedException();
        }
    }
}
