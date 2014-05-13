using System;
using System.Collections.Generic;
using System.Text;

namespace Phieul {
    public class Units {
        public enum DistanceUnits { Kilometers, Miles, TenKilometers }
        public enum VolumeUnits { Litres, Gallons }

        const double MilesToKilometers = 1.60934;
        const double KilometersToMiles = 0.62137;
        const double KilometersToMetricMiles = 0.1;
        const double MetricMilesToKilometers = 10;
        const double GallonsToLitres = 3.78541;
        const double LitresToGallons = 0.26417;

        public static double DistanceCalculation(double value, DistanceUnits unit, bool toUniveral = false) {
            double distance = 0;
            if(unit == DistanceUnits.Kilometers) {
                distance = value;
            } else if(unit == DistanceUnits.Miles) {
                if(!toUniveral) distance = value * KilometersToMiles;
                else distance = value * MilesToKilometers;
            } else if(unit == DistanceUnits.TenKilometers) {
                if(!toUniveral) distance = value * KilometersToMetricMiles;
                else distance = value * MetricMilesToKilometers;
            }
            return distance;
        }

        public static double VolumeCalculation(double value, VolumeUnits unit, bool toUniveral = false) {
            double distance = 0;
            if(unit == VolumeUnits.Litres) {
                distance = value;
            } else if(unit == VolumeUnits.Gallons) {
                if(!toUniveral) distance = value * LitresToGallons;
                else distance = value * GallonsToLitres;
            }
            return distance;
        }
    }
}
