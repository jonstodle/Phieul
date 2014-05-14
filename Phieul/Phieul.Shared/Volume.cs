using System;
using System.Collections.Generic;
using System.Text;

namespace Phieul {
    public struct Volume {
        public enum Units { Litres, Gallons }
        private readonly Units unit;
        private readonly double value;

        public Volume(double value, Units unit) {
            this.unit = unit;

            double v = value;
            switch(unit) {
                case Units.Litres:
                    break;
                case Units.Gallons:
                    v *= 3.78541;
                    break;
                default:
                    break;
            }
            this.value = v;
        }

        public Units Unit {
            get { return unit; }
        }

        public double Value {
            get { return value; }
        }

        public double CalculatedValue {
            get {
                double v = value;
                switch(unit) {
                    case Units.Litres:
                        break;
                    case Units.Gallons:
                        v *= 0.26417;
                        break;
                    default:
                        break;
                }
                return v;
            }
        }

        public override string ToString() {
            return CalculatedValue.ToString();
        }

        public static implicit operator Volume(double d) {
            return new Volume(d, Units.Litres);
        }

        public static implicit operator double(Volume v) {
            return v.CalculatedValue;
        }
    }
}
