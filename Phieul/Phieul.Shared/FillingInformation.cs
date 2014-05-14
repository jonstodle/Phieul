using System;
using System.Collections.Generic;
using System.Text;

namespace Phieul{
    public class FillingInformation{
        public FillingInformation(Filling origin) {
            Origin = origin;
        }

        public Filling Origin { get; set; }
        public bool FullTank { get { return Origin.FullTank; } }
        public DateTime Date { get { return Origin.Date; } }
        public double Price { get { return Origin.Price; } }
        public double Odometer { get { return Origin.Odometer; } }
        public double Volume { get { return Origin.FilledVolume; } }
        public double Distance { get; set; }
        public double AverageConsumption {
            get {
                //TODO: Add unit specific calculation
                if(true) {

                }
            }
        }
        public double DistancePrice { get; set; }
    }
}