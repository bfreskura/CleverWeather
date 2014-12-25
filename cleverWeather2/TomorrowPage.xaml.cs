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
        public Dictionary<string, Object> State = new Dictionary<string, object>();
        UpdateView dataTomorrow;


        public TomorrowPage()
        {

            this.InitializeComponent();
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
                DataContext = _viewModel;
            }


            // Set _isNewPageInstance to false. If the user navigates back to this page
            // and it has remained in memory, this value will continue to be false.
            _isNewPageInstance = false;

            //Call to Refresh class which refreshes view
          //  dataTomorrow = new UpdateView(_viewModel);

            
                 ShowLoadingIndicator();
            
            dataTomorrow = new UpdateView(_viewModel);
            HideLoadingIndicator();

        }


        public async void ShowLoadingIndicator() {
            await StatusBar.GetForCurrentView().ShowAsync();
            var progInd = StatusBar.GetForCurrentView().ProgressIndicator;
            progInd.Text= "Downloading Weather Data";
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
                State["ViewModelTomorrow"] = _viewModel;
            }
        }

        private void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }



    }

}

