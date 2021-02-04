using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SVPP_Lab_9_BusinessLayer.Models
{
    public class CartelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCartelAgreement { get; set; }
        public decimal Bribe { get; set; }
        public ObservableCollection<RailwayViewModel> Railways
        { get; set; }
    }
}
