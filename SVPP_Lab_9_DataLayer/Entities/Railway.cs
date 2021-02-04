using System;
using System.Collections.Generic;
using System.Text;

namespace SVPP_Lab_9_DataLayer.Entities
{
    public class Railway
    {
        public int RailwayId { get; set; }
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public int AssessedValue { get; set; }
        public string FileName { get; set; }
        public int CartelId { get; set; }
        public Cartel Group { get; set; }
    }
}
