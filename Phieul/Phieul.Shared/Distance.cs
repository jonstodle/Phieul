using System;
using System.Collections.Generic;
using System.Text;

namespace Phieul {
    public struct Distance {
        public enum Units { Kilometers, Miles, MetricMiles }
        private readonly Units unit;
        private readonly double value;

        public Distance(double value, Units unit) {
            this.unit = unit;
           
            double d = value;
            switch(unit) {
                case Units.Kilometers:
                    break;
                case Units.Miles:
                    d *= 1.60934;
                    break;
                case Units.MetricMiles:
                    d *= 10;
                    break;
                default:
                    break;
            }
            this.value = d;
        }

        public Units Unit {
            get { return unit; }
        }

        public double Value {
            get { return value; }
        }

        public double CalculatedValue {
            get {
                double d = value;
                switch(unit) {
                    case Units.Kilometers:
                        break;
                    case Units.Miles:
                        d *= 0.62137;
                        break;
                    case Units.MetricMiles:
                        d *= 0.1;
                        break;
                    default:
                        break;
                }
                return d;
            }
        }

        public override string ToString() {
            return CalculatedValue.ToString();
        }

        public static implicit operator Distance(double d) {
            return new Distance(d, Units.Kilometers);
        }

        public static implicit operator double(Distance d){
            return d.CalculatedValue;
        }
    }
}
