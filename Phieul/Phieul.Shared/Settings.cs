using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Phieul{
    public class Settings{
        ApplicationDataContainer rs = ApplicationData.Current.RoamingSettings;

        public Units.DistanceUnits DistanceUnit {
            get {
                if(rs.Values.ContainsKey("DistanceUnit")) {
                    rs.Values["DistanceUnit"] = Units.DistanceUnits.Kilometers;
                }
                return (Units.DistanceUnits)rs.Values["DistanceUnit"];
            }

            set { rs.Values["DistanceUnit"] = value; }
        }

        public Units.VolumeUnits VolumeUnit {
            get {
                if(rs.Values.ContainsKey("VolumeUnit")) {
                    rs.Values["VolumeUnit"] = Units.VolumeUnits.Litres;
                }
                return (Units.VolumeUnits)rs.Values["VolumeUnit"];
            }

            set { rs.Values["VolumeUnit"] = value; }
        }

        public Filling.ConsumptionUnits ConsumptionUnit {
            get {
                if(rs.Values.ContainsKey("ConsumptionUnit")) {
                    rs.Values["ConsumptionUnit"] = Filling.ConsumptionUnits.LitresPerMetricMile;
                }
                return (Filling.ConsumptionUnits)rs.Values["ConsumptionUnit"];
            }

            set { rs.Values["ConsumptionUnit"] = value; }
        }

        public Filling.PriceUnits PriceUnit {
            get {
                if(rs.Values.ContainsKey("PriceUnit")) {
                    rs.Values["PriceUnit"] = Filling.PriceUnits.PerMetricMile;
                }
                return (Filling.PriceUnits)rs.Values["PriceUnit"];
            }

            set { rs.Values["PriceUnit"] = value; }
        }
    }
}
