using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Phieul {
    public class Filling : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public enum ConsumptionUnits { LitresPerTenKilometer, MilesPerGallon }
        public enum PriceUnits { PerKilometer, PerMile, PerTenKilometer }

        #region Unit Settings
        private Units.DistanceUnits _distanceUnit;
        public Units.DistanceUnits DistanceUnit {
            get { return _distanceUnit; }

            set {
                if(value != _distanceUnit) {
                    _distanceUnit = value;
                    OnPropertyChanged("DistanceUnit");
                    OnPropertyChanged("Odometer");
                }
            }
        }

        private Units.VolumeUnits _volumeUnit;
        public Units.VolumeUnits VolumeUnit {
            get { return _volumeUnit; }

            set {
                if(value != _volumeUnit) {
                    _volumeUnit = value;
                    OnPropertyChanged("VolumeUnit");
                    OnPropertyChanged("Volume");
                }
            }
        }

        private ConsumptionUnits _consumptionUnit;
        public ConsumptionUnits ConsumptionUnit {
            get { return _consumptionUnit; }

            set {
                if(value != _consumptionUnit) {
                    _consumptionUnit = value;
                    OnPropertyChanged("ConsumptionUnit");
                }
            }
        }

        private PriceUnits _priceUnit;
        public PriceUnits PriceUnit {
            get { return _priceUnit; }

            set {
                if(value != _priceUnit) {
                    _priceUnit = value;
                    OnPropertyChanged("PriceUnit");
                }
            }
        }
        #endregion Unit Settings

        #region Entered properties
        private bool _fullTank;
        public bool FullTank {
            get { return _fullTank; }

            set {
                if(value != _fullTank) {
                    _fullTank = value;
                    OnPropertyChanged("FullTank");
                }
            }
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; }

            set {
                if(value != _date) {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private double _price;
        public double Price {
            get { return _price; }

            set {
                if(value != _price) {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        private double _odometer;
        public double Odometer {
            get { return Units.DistanceCalculation(_odometer, DistanceUnit); }

            set {
                var val = Units.DistanceCalculation(value, DistanceUnit, true);
                if(val != _odometer) {
                    _odometer = val;
                    OnPropertyChanged("Odometer");
                }
            }
        }

        private double _volume;
        public double Volume {
            get { return Units.VolumeCalculation(_volume, VolumeUnit); }

            set {
                var val = Units.VolumeCalculation(value, VolumeUnit, true);
                if(val != _volume) {
                    _volume = val;
                    OnPropertyChanged("Volume");
                }
            }
        }
        #endregion Entered properties

        #region Calculated properties
        private bool _isFirstFilling;
        public bool IsFirstFilling {
            get { return _isFirstFilling; }

            set {
                if(value != _isFirstFilling) {
                    _isFirstFilling = value;
                    OnPropertyChanged("IsFirstFilling");
                }
            }
        }

        private double _distance;
        public double Distance {
            get {
                if(IsFirstFilling) return 0;
                return Units.DistanceCalculation(_distance, DistanceUnit);
            }

            set {
                var val = Units.DistanceCalculation(value, DistanceUnit, true);
                if(val != _distance) {
                    _distance = val;
                    OnPropertyChanged("Distance");
                }
            }
        }

        private double _calculatingVolume;
        public double CalculatingVolume {
            private get { return _calculatingVolume; }

            set {
                var val = Units.VolumeCalculation(value, VolumeUnit, true);
                if(val != _calculatingVolume) {
                    _calculatingVolume = val;
                    OnPropertyChanged("CalculatingVolume");
                }
            }
        }

        private double _calculatingDistance;
        public double CalculatingDistance {
            private get { return _calculatingDistance; }

            set {
                var val = Units.DistanceCalculation(value, DistanceUnit, true);
                if(val != _calculatingDistance) {
                    _calculatingDistance = val;
                    OnPropertyChanged("CalculatingDistance");
                }
            }
        }

        public double AverageConsumption {
            get {
                if(IsFirstFilling) return 0;
                double ac = 0;
                if(ConsumptionUnit == ConsumptionUnits.LitresPerTenKilometer) ac = Units.VolumeCalculation(CalculatingVolume, Units.VolumeUnits.Litres) / Units.DistanceCalculation(CalculatingDistance, Units.DistanceUnits.TenKilometers);
                else if(ConsumptionUnit == ConsumptionUnits.MilesPerGallon) ac = Units.DistanceCalculation(CalculatingDistance, Units.DistanceUnits.Miles) / Units.VolumeCalculation(CalculatingVolume, Units.VolumeUnits.Gallons);
                return ac;
            }
        }

        public double DistancePrice {
            get {
                if(IsFirstFilling) return 0;
                double dp = 0;
                if(PriceUnit == PriceUnits.PerKilometer) dp = (Price * Units.VolumeCalculation(CalculatingVolume, VolumeUnit)) / Units.DistanceCalculation(CalculatingDistance, Units.DistanceUnits.Kilometers);
                else if(PriceUnit == PriceUnits.PerMile) dp = (Price * Units.VolumeCalculation(CalculatingVolume, VolumeUnit)) / Units.DistanceCalculation(CalculatingDistance, Units.DistanceUnits.Miles);
                else if(PriceUnit == PriceUnits.PerTenKilometer) dp = (Price * Units.VolumeCalculation(CalculatingVolume, VolumeUnit)) / Units.DistanceCalculation(CalculatingDistance, Units.DistanceUnits.TenKilometers);
                return dp;
            }
        }
        #endregion Calculated properties

        private void OnPropertyChanged(string changedProperty) {
            PropertyChanged(this, new PropertyChangedEventArgs(changedProperty));
        }
    }
}
