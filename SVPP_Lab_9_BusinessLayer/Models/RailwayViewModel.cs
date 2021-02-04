using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVPP_Lab_9_BusinessLayer.Models
{
    public class RailwayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FoundationDate { get; set; }
        public int AssessedValue { get; set; }
        public string FileName { get; set; }
        public bool IsForSale { get; set; }
    }
}
