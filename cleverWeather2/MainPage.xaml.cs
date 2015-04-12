using cleverWeather2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace cleverWeather2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ViewModelTomorrow _viewModel;
        bool _isNewPageInstance = false;
        public Dictionary<string, Object> State = new Dictionary<string, Object>();
        UpdateViewToday dataToday;
        public StatusBarProgressIndicator ProgressIndicator;
        private bool isConnected, isLocationEnabled;

        public MainPage()
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
        /// 





        //CHeck for Internet Access
        public async Task CheckInternet()
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {



            // If _isNewPageInstance is true, the page constuctor has been called, so
            // state may need to be restored.
            if (_isNewPageInstance)
            {
                if (_viewModel == null)
                {
                   
                    if (State.Count > 0)
                    {
                        _viewModel = (ViewModelTomorrow)State["ViewModelToday"];
                    }
                    else
                    {
                        _viewModel = new ViewModelTomorrow();
                    }
                }

                


                dataToday = new UpdateViewToday(_viewModel);

                tomorrow_button.IsEnabled = false;
                await Task.Delay(2000);
                tomorrow_button.IsEnabled = true;

                StoryBoardToday.Begin();


                DataContext = _viewModel;


            }
            //ShowPopup();

            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;


            //Call to Refresh class which refreshes view


        }





        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // If this is a back navigation, the page will be discarded, so there
            // is no need to save state.
            if (e.NavigationMode != NavigationMode.Back)
            {
                // Save the ViewModel variable in the page's State dictionary.
                State["ViewModelToday"] = _viewModel;
            }
        }


        private void tomorrow_button_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(TomorrowPage));
        }




        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await CheckInternet();
            await checkLocation();
            if (isConnected == true && isLocationEnabled == true)
            {





                dataToday = new UpdateViewToday(_viewModel);
                //  StoryBoardToday.Begin();




            }


        }

        private void AppBarButtonLegend_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IconLegend));

        }

        private  void tomorrow_button_Loaded(object sender, RoutedEventArgs e)
        {
            
        }



    }

}
