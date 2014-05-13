using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

namespace Phieul {
    public class FillingCollection : ObservableCollection<Filling> {
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            //base.OnCollectionChanged(e);
            if(e.Action == NotifyCollectionChangedAction.Add) {
                foreach(Filling item in e.NewItems)item.PropertyChanged += ItemPropertyChanged;
            } else if(e.Action == NotifyCollectionChangedAction.Remove) {
                foreach(Filling item in e.OldItems)item.PropertyChanged -= ItemPropertyChanged;
            } else if(e.Action == NotifyCollectionChangedAction.Replace) {
                foreach(Filling item in e.OldItems)item.PropertyChanged -= ItemPropertyChanged;
                foreach(Filling item in e.NewItems)item.PropertyChanged += ItemPropertyChanged;
            }
            OrderAndCalculate();
        }

        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            OrderAndCalculate();
        }

        void OrderAndCalculate() {
            this.OrderBy(n => n.Date);
            CalculateDistances();
            CalculateCalculatingVariables();
        }

        void CalculateDistances() {
            if(Count > 0) {
                this[0].Distance = 0;
                for(int i = 1; i < Count; i++) {
                    this[i].Distance = this[i].Odometer - this[i - 1].Odometer;
                }
            }
        }

        void CalculateCalculatingVariables() {
            if(Count > 0) {
                var prevItem = this[0];
                var partials = new List<Filling>();
                prevItem.IsFirstFilling = true;
                for(int i = 1; i < Count; i++) {
                    var currentItem = this[i];
                    if(currentItem.FullTank) {
                        if(partials.Count > 0) {
                            var tv = partials.Sum(n => n.Volume) + currentItem.Volume;
                            var td = partials.Sum(n=> n.Distance) + currentItem.Distance;
                            currentItem.CalculatingVolume = tv;
                            currentItem.CalculatingDistance = td;
                            foreach(Filling f in partials) {
                                f.CalculatingVolume = tv;
                                f.CalculatingDistance = td;
                            }
                            partials.Clear();
                        } else {
                            currentItem.CalculatingVolume = currentItem.Volume;
                            currentItem.CalculatingDistance = currentItem.Distance;
                        }
                    } else {
                        partials.Add(currentItem);
                    }
                }
                if(partials.Count > 0) {
                    double tv = partials.Sum(n=>n.Volume);
                    double td = partials.Sum(n=>n.Distance);
                    foreach(Filling f in partials) {
                        f.CalculatingVolume = tv;
                        f.CalculatingDistance = td;
                    }
                }
            }
        }
    }
}
