using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        ViewModelToday _viewModel;
        bool _isNewPageInstance = false;
        public Dictionary<string, Object> State = new Dictionary<string,object>();
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
                        _viewModel = (ViewModelToday)State["ViewModelToday"];
                    }
                    else
                    {
                        _viewModel = new ViewModelToday();
                    }
                }
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
            if (e.NavigationMode != NavigationMode.Back)
            {
                // Save the ViewModel variable in the page's State dictionary.
                State["ViewModelToday"] = _viewModel;
            }
        }


    }
}
