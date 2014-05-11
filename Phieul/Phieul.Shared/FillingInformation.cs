using System;
using System.Collections.Generic;
using System.Text;

namespace Phieul{
    public class FillingInformation{
        public Filling Origin { get; set; }
        public bool FullTank { get { return Origin.FullTank; } }
        public DateTime Date { get { return Origin.Date; } }
        public double Price { get { return Origin.Price; } }
        public double Odometer { get { return Origin.Odometer; } }
        public double Volume { get { return Origin.Volume; } }
        public double Distance { get; set; }
        public double AverageConsumption { get; set; }
        public double DistancePrice { get; set; }
    }
}