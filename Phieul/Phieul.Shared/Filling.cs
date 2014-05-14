using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Phieul {
    public class Filling : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public enum ConsumptionUnits { LitresPerMetricMile, MilesPerGallon }
        public enum PriceUnits { PerKilometer, PerMile, PerMetricMile }

        #region Unit Settings
        public Distance.Units DistanceUnit {
            get {
                return Odometer.Unit;
            }

            set {
                if(value != Odometer.Unit) {
                    Odometer = new Distance(Odometer.Value, value);
                    OnPropertyChanged("DistanceUnit");
                    OnPropertyChanged("Odometer");
                }
            }
        }

        private Volume.Units _volumeUnit;
        public Volume.Units VolumeUnit {
            get { return FilledVolume.Unit; }

            set {
                if(value != FilledVolume.Unit) {
                    FilledVolume = new Volume(FilledVolume.Value, value);
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

        private Distance _odometer;
        public Distance Odometer {
            get { return _odometer; }

            set {
                if(value != _odometer) {
                    _odometer = new Distance(value, DistanceUnit);
                    OnPropertyChanged("Odometer");
                }
            }
        }

        private Volume _filledVolume;
        public Volume FilledVolume {
            get { return _filledVolume; }

            set {
                if(value != _filledVolume) {
                    _filledVolume = new Volume(value, VolumeUnit);
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

        private Distance _traveledDistance;
        public Distance TraveledDistance {
            get {
                if(IsFirstFilling) return 0;
                return _traveledDistance;
            }

            set {
                if(value != _traveledDistance) {
                    _traveledDistance = new Distance(value, DistanceUnit); ;
                    OnPropertyChanged("Distance");
                }
            }
        }

        private Volume _calculatingVolume;
        public Volume CalculatingVolume {
            private get { return _calculatingVolume; }

            set {
                if(value != _calculatingVolume) {
                    _calculatingVolume = new Volume(value, VolumeUnit);
                    OnPropertyChanged("CalculatingVolume");
                }
            }
        }

        private Distance _calculatingDistance;
        public Distance CalculatingDistance {
            private get { return _calculatingDistance; }

            set {
                if(value != _calculatingDistance) {
                    _calculatingDistance = new Distance(value, DistanceUnit); ;
                    OnPropertyChanged("CalculatingDistance");
                }
            }
        }

        public double AverageConsumption {
            get {
                if(IsFirstFilling) return 0;
                double ac = 0;
                if(ConsumptionUnit == ConsumptionUnits.LitresPerMetricMile) ac = new Volume(CalculatingVolume.Value, Volume.Units.Litres) / new Distance(CalculatingDistance.Value, Distance.Units.MetricMiles);
                else if(ConsumptionUnit == ConsumptionUnits.MilesPerGallon) ac = new Distance(CalculatingDistance.Value, Distance.Units.Miles) / new Volume(CalculatingVolume.Value, Volume.Units.Gallons);
                return ac;
            }
        }

        public double DistancePrice {
            get {
                if(IsFirstFilling) return 0;
                double dp = 0;
                if(PriceUnit == PriceUnits.PerKilometer) dp = (Price * CalculatingVolume) / new Distance(CalculatingDistance.Value, Distance.Units.Kilometers);
                else if(PriceUnit == PriceUnits.PerMile) dp = (Price * CalculatingVolume) / new Distance(CalculatingDistance.Value, Distance.Units.Miles);
                else if(PriceUnit == PriceUnits.PerMetricMile) dp = (Price * CalculatingVolume) / new Distance(CalculatingDistance.Value, Distance.Units.MetricMiles);
                return dp;
            }
        }
        #endregion Calculated properties

        private void OnPropertyChanged(string changedProperty) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(changedProperty)); 
            }
        }
    }
}
