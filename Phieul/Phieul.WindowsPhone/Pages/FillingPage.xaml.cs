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
        private enum InputFields { Price, Volume, Odometer }
        private InputFields ActiveField;

        public FillingPage() {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            SetActiveField(InputFields.Price);
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
            } else if(tag == "Up") {
                MoveUp();
            } else if(tag == "Down") {
                MoveDown();
            } else if(tag == "Save") {
                //TODO: Implement save logic
            }
        }

        private void SetActiveField(InputFields toField) {
            var accentColor = (Color)App.Current.Resources.ThemeDictionaries["SystemColorControlAccentColor"];
            var upscaled = 1.5d;
            var regular = 1d;

            PriceBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
            VolumeBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
            OdometerBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
            ScaleAnimate(regular, PriceScaleTransform);
            ScaleAnimate(regular, VolumeScaleTransform);
            ScaleAnimate(regular, OdometerScaleTransform);


            switch(toField) {
                case InputFields.Price:
                    PriceBorder.BorderBrush = new SolidColorBrush(accentColor);
                    ScaleAnimate(upscaled, PriceScaleTransform);
                    break;
                case InputFields.Volume:
                    VolumeBorder.BorderBrush = new SolidColorBrush(accentColor);
                    ScaleAnimate(upscaled, VolumeScaleTransform);
                    break;
                case InputFields.Odometer:
                    OdometerBorder.BorderBrush = new SolidColorBrush(accentColor);
                    ScaleAnimate(upscaled, OdometerScaleTransform);
                    break;
                default:
                    break;
            }

            ActiveField = toField;
        }

        private void ScaleAnimate(double to, DependencyObject obj) {
            var xAnimation = new DoubleAnimation() { To = to };
            var yAnimation = new DoubleAnimation() { To = to };
            Storyboard.SetTarget(xAnimation, obj);
            Storyboard.SetTargetProperty(xAnimation, "ScaleX");
            Storyboard.SetTarget(yAnimation, obj);
            Storyboard.SetTargetProperty(yAnimation, "ScaleY");
            var sb = new Storyboard() { Duration = TimeSpan.FromMilliseconds(200) };
            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);
            sb.Begin();
        }

        private void AddCharacter(string character) {
            TextBlock tb = null;
            switch(ActiveField) {
                case InputFields.Price:
                    tb = PriceInput;
                    break;
                case InputFields.Volume:
                    tb = VolumeInput;
                    break;
                case InputFields.Odometer:
                    tb = OdometerInput;
                    break;
                default:
                    break;
            }
            if(character == "." && tb.Text.Contains(character)) return;
            tb.Text += character;
        }

        private void RemoveCharacter() {
            TextBlock tb = null;
            switch(ActiveField) {
                case InputFields.Price:
                    tb = PriceInput;
                    break;
                case InputFields.Volume:
                    tb = VolumeInput;
                    break;
                case InputFields.Odometer:
                    tb = OdometerInput;
                    break;
                default:
                    break;
            }
            if(tb.Text == string.Empty) return;
            tb.Text = tb.Text.Remove(tb.Text.Count() - 1);
        }

        private void MoveUp() {
            switch(ActiveField) {
                case InputFields.Price:
                    break;
                case InputFields.Volume:
                    SetActiveField(InputFields.Price);
                    break;
                case InputFields.Odometer:
                    SetActiveField(InputFields.Volume);
                    break;
                default:
                    break;
            }
        }

        private void MoveDown() {
            switch(ActiveField) {
                case InputFields.Price:
                    SetActiveField(InputFields.Volume);
                    break;
                case InputFields.Volume:
                    SetActiveField(InputFields.Odometer);
                    break;
                case InputFields.Odometer:
                    break;
                default:
                    break;
            }
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e) {
            if(sender == PriceBorder) {
                SetActiveField(InputFields.Price);
            } else if(sender == VolumeBorder) {
                SetActiveField(InputFields.Volume);
            } else if(sender == OdometerBorder) {
                SetActiveField(InputFields.Odometer);
            }
        }
    }
}
