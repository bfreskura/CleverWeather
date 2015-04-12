using cleverWeather2.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace cleverWeather2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TomorrowPage : Page
    {
        ViewModelTomorrow _viewModel;
        bool _isNewPageInstance = false;
        public Dictionary<string, Object> State = new Dictionary<string, Object>();
        UpdateViewTomorrow dataTomorrow;
        private bool isConnected, isLocationEnabled;


        public TomorrowPage()
        {

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            _isNewPageInstance = true;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            // If _isNewPageInstance is true, the page constuctor has been called, so
            // state may need to be restored.
            if (_isNewPageInstance)
            {

                if (_viewModel == null)
                {
                    if (State.Count > 0)
                    {
                        _viewModel = (ViewModelTomorrow)State["ViewModelTomorrow"];

                    }
                    else
                    {
                        _viewModel = new ViewModelTomorrow();

                    }


                }
                //ShowLoadingIndicator();

                StoryBoardTomorrow.Begin();
                dataTomorrow = new UpdateViewTomorrow(_viewModel);

                // HideLoadingIndicator();

                DataContext = _viewModel;


            }


            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;



        }




        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // If this is a back navigation, the page will be discarded, so there
            // is no need to save state.
            if (e.NavigationMode == NavigationMode.Back)
            {
                // Save the ViewModel variable in the page's State dictionary.
                State["ViewModelTomorrow"] = _viewModel;
            }
        }



        public async Task checkLocation()
        {
            Geolocator geolocator = new Geolocator();
            // Debug.WriteLine(geolocator.LocationStatus);
            if (geolocator.LocationStatus == PositionStatus.Disabled)
            {
                isLocationEnabled = false;
                await new MessageDialog("Location services are disabled. Enable them in Settings > Location").ShowAsync();
            }
            else
                isLocationEnabled = true;
        }

        private async Task CheckInternet()
        {
            isConnected = NetworkInterface.GetIsNetworkAvailable();
            if (!isConnected)
            {
                await new MessageDialog("No internet connection avaliable.").ShowAsync();
            }
            else
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                {
                    isConnected = false;
                }
            }
        }


        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await checkLocation();
            await CheckInternet();
            if (isConnected == true && isLocationEnabled == true)
            {


                dataTomorrow = new UpdateViewTomorrow(_viewModel);
                //StoryBoardTomorrow.Begin();

            }
        }

        private void AppBarButtonLegend_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IconLegend));
        }





    }

}

