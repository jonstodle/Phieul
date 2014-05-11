using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Phieul{
    public class Filling : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _fullTank;
        public bool FullTank {
            get { return _fullTank; }

            set {
                if(value != _fullTank) {
                    _fullTank = value;
                    OnProperyChanged("FullTank");
                }
            }
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; }

            set{
                if(value != _date) {
                    _date = value;
                    OnProperyChanged("Date");
                }
            }
        }

        private double _price;
        public double Price {
            get { return _price; }

            set {
                if(value != _price) {
                    _price = value;
                    OnProperyChanged("Price");
                }
            }
        }

        private double _odometer;
        public double Odometer {
            get { return _odometer; }

            set {
                if(value != _odometer) {
                    _odometer = value;
                    OnProperyChanged("Odometer");
                }
            }
        }

        private double _volume;
        public double Volume {
            get { return _volume; }

            set {
                if(value != _volume) {
                    _volume = value;
                    OnProperyChanged("Volume");
                }
            }
        }

        private void OnProperyChanged(string changedProperty) {
            PropertyChanged(this, new PropertyChangedEventArgs(changedProperty));
        }
    }
}
