using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Phieul{
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

        async void LoadData() {
            var rf = ApplicationData.Current.RoamingFolder;
            var file = await rf.CreateFileAsync("data.json", CreationCollisionOption.OpenIfExists);
            data = JsonConvert.DeserializeObject<FillingCollection>(await FileIO.ReadTextAsync(file));
        }

        public async void SaveData() {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            var rf = ApplicationData.Current.RoamingFolder;
            var file = await rf.CreateFileAsync("data.json", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, json);
        }

        void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
