using cleverWeather2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public Dictionary<string, Object> State = new Dictionary<string, object>();
        UpdateViewToday dataToday;
        public StatusBarProgressIndicator ProgressIndicator;

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
                        _viewModel = (ViewModelTomorrow)State["ViewModelToday"];
                    }
                    else
                    {
                        _viewModel = new ViewModelTomorrow();
                    }
                }
                DataContext = _viewModel;
            }


            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;


            //Call to Refresh class which refreshes view
            ShowLoadingIndicator();
            
            dataToday = new UpdateViewToday(_viewModel);
            HideLoadingIndicator();

        }


        public async void ShowLoadingIndicator() {
            await StatusBar.GetForCurrentView().ShowAsync();
            var progInd = StatusBar.GetForCurrentView().ProgressIndicator;
            progInd.Text= "Downlaoding Weather Data";
            await progInd.ShowAsync();
        }

        public async void HideLoadingIndicator() {
           var progInd = StatusBar.GetForCurrentView().ProgressIndicator;
           await  progInd.HideAsync();
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
    }




}
