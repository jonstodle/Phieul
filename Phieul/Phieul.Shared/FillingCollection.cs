using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace Phieul{
    public class FillingCollection : ObservableCollection<Filling>{
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            //base.OnCollectionChanged(e);
            if(e.Action == NotifyCollectionChangedAction.Add) {
                foreach(Filling item in e.NewItems) {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            } else if(e.Action == NotifyCollectionChangedAction.Remove) {
                foreach(Filling item in e.OldItems) {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            } else if(e.Action == NotifyCollectionChangedAction.Replace) {
                foreach(Filling item in e.OldItems) {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
                foreach(Filling item in e.NewItems) {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
        }

        public List<FillingInformation> GetFillings() {
            List<FillingInformation> list = new List<FillingInformation>();

            return list;
        }

        void ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var s = (Filling)sender;
            if(!s.FullTank) {
                for(int i = this.IndexOf(s); i < this.Count; i++) {
                    if(this[i].FullTank) {

                    }
                }
            }
        }
    }
}
