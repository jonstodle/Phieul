using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Phieul {
    public sealed class Settings : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        static readonly Settings instance = new Settings();
        static ApplicationDataContainer settings = ApplicationData.Current.RoamingSettings;
        static FillingCollection data;

        static Settings() {
            Instance.LoadData();
        }

        public static Settings Instance {
            get { return instance; }
        }

        public Distance.Units DefaultDistanceUnit {
            get {
                if(settings.Values.ContainsKey("DefaultDistanceUnit")) {
                    settings.Values["DefaultDistanceUnit"] = Distance.Units.Kilometers;
                }
                return (Distance.Units)settings.Values["DefaultDistanceUnit"];
            }

            set {
                settings.Values["DefaultDistanceUnit"] = value;
                OnPropertyChanged("DefaultDistanceUnit");
            }
        }

        public Volume.Units DefaultVolumeUnit {
            get {
                if(settings.Values.ContainsKey("DefaultVolumeUnit")) {
                    settings.Values["DefaultVolumeUnit"] = Volume.Units.Litres;
                }
                return (Volume.Units)settings.Values["DefaultVolumeUnit"];
            }

            set {
                settings.Values["DefaultVolumeUnit"] = value;
                OnPropertyChanged("DefaultVolumeUnit");
            }
        }

        public Filling.ConsumptionUnits DefaultConsumptionUnit {
            get {
                if(settings.Values.ContainsKey("DefaultConsumptionUnit")) {
                    settings.Values["DefaultConsumptionUnit"] = Filling.ConsumptionUnits.LitresPerMetricMile;
                }
                return (Filling.ConsumptionUnits)settings.Values["DefaultConsumptionUnit"];
            }

            set {
                settings.Values["DefaultConsumptionUnit"] = value;
                OnPropertyChanged("DefaultConsumptionUnit");
            }
        }

        public Filling.PriceUnits DefaultPriceUnit {
            get {
                if(settings.Values.ContainsKey("DefaultPriceUnit")) {
                    settings.Values["DefaultPriceUnit"] = Filling.PriceUnits.PerMetricMile;
                }
                return (Filling.PriceUnits)settings.Values["DefaultPriceUnit"];
            }

            set {
                settings.Values["DefaultPriceUnit"] = value;
                OnPropertyChanged("DefaultPriceUnit");
            }
        }

        async void LoadData() {
            var rf = ApplicationData.Current.RoamingFolder;
            var file = await rf.CreateFileAsync("data.json", CreationCollisionOption.OpenIfExists);
            data = JsonConvert.DeserializeObject<FillingCollection>(await FileIO.ReadTextAsync(file));
            System.Diagnostics.Debug.WriteLine("Settings Loaded");
        }

        public async void SaveData() {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            var rf = ApplicationData.Current.RoamingFolder;
            var file = await rf.CreateFileAsync("data.json", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, json);
            System.Diagnostics.Debug.WriteLine("Settings Saved");
        }

        void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
