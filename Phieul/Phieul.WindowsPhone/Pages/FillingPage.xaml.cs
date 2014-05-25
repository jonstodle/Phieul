using Phieul.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Phieul.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FillingPage : Page {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private RadioButton ActiveRadio;

        public FillingPage() {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            //Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e) {
            var filling = e.NavigationParameter as Filling;
            if(filling == null) {
                filling = new Filling();
            }
            this.DataContext = filling;
            DateField.Text = filling.Date.ToString();
            PriceField.Text = filling.Price.ToString();
            VolumeField.Text = filling.FilledVolume.ToString();
            OdometerField.Text = filling.Odometer.ToString();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e) {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e) {
            var tag = (string)((Button)sender).Tag;
            var content = (string)((Button)sender).Content;
            if(tag == "Character") {
                AddCharacter(content);
            } else if(tag == "Delete") {
                RemoveCharacter();
            } else if(tag == "Previous") {
                MoveToPrevious();
            } else if(tag == "Next") {
                MoveToNext();
            } else if(tag == "Save") {
                //TODO: implement save logic
            }
        }

        private void AddCharacter(string character) {
            TextBlock field = null;
            if(ActiveRadio == PriceRadio) {
                field = PriceField;
            } else if(ActiveRadio == VolumeRadio) {
                field = VolumeField;
            } else if(ActiveRadio == OdometerRadio) {
                field = OdometerField;
            }

            if(field == null) return;
            var txt = field.Text;

            if(character == "." && txt.Contains(character)) return;
            if(txt == "0") {
                if(character == ".") {
                    txt = "0";
                } else if(character != "0") {
                    txt = "";
                }
            }
            field.Text = txt + character;
        }

        private void RemoveCharacter() {
            TextBlock field = null;
            if(ActiveRadio == PriceRadio) {
                field = PriceField;
            } else if(ActiveRadio == VolumeRadio) {
                field = VolumeField;
            } else if(ActiveRadio == OdometerRadio) {
                field = OdometerField;
            }

            if(field == null) return;
            var txt = field.Text;
            var count = txt.Count();

            if(count > 1) {
                field.Text = txt.Remove(count - 1);
            } else {
                field.Text = "0";
            }
        }

        private void MoveToPrevious() {
            if(ActiveRadio == PriceRadio) {
                DateRadio.IsChecked = true;
            } else if(ActiveRadio == VolumeRadio) {
                PriceRadio.IsChecked = true;
            } else if(ActiveRadio == OdometerRadio) {
                VolumeRadio.IsChecked = true;
            }
        }

        private void MoveToNext() {
            if(ActiveRadio == DateRadio) {
                PriceRadio.IsChecked = true;
            } else if(ActiveRadio == PriceRadio) {
                VolumeRadio.IsChecked = true;
            } else if(ActiveRadio == VolumeRadio) {
                OdometerRadio.IsChecked = true;
            }
        }

        private async void Radio_Checked(object sender, RoutedEventArgs e) {
            ActiveRadio = (RadioButton)sender;
            if(ActiveRadio == DateRadio) {
                var dpfo = new DatePickerFlyout();
                var pickerOpen = dpfo.ShowAtAsync(ContentRoot);
                PriceRadio.IsChecked = true;
                var result = await pickerOpen;
                DateField.Text = result.Value.DateTime.ToString();
            }
        }
    }
}
