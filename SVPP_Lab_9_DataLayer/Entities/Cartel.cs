using System;
using System.Collections.Generic;
using System.Text;

namespace SVPP_Lab_9_DataLayer.Entities
{
    public class Cartel
    {
        public int CartelId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCartelAgreement { get; set; }
        public decimal Bribe { get; set; }
        public List<Railway> Railways { get; set; }

        public Cartel()
        {
            Railways = new List<Railway>();
        }
    }
}
