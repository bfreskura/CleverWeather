using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace cleverWeather2.Model
{
    /// <summary>
    /// Updates the View with latest data
    /// </summary>
    class UpdateViewTomorrow
    {
        ViewModelTomorrow _viewModel;
        private bool isConnected = NetworkInterface.GetIsNetworkAvailable();
        private bool isLocationEnabled;
        //Constructor
        public UpdateViewTomorrow(ViewModelTomorrow vmt)
        {
            this._viewModel = vmt;

            GetData();
        }


        /* Explained problem with async and await
         * http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
         * */


        /// <summary>
        /// Checks if locations services are enabled in settings
        /// </summary>
        /// <returns></returns>
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



        /// <summary>
        /// Checks for internet connection
        /// If there is none, sets variable isConnected to false, true otherwise
        /// </summary>
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
                if (connection == NetworkConnectivityLevel.InternetAccess)
                {
                    isConnected = true;

                }
                else
                {
                    isConnected = false;
                    await new MessageDialog("No internet connection avaliable.").ShowAsync();
                }


            }

        }

        /// <summary>
        /// Call to CheckInternet method
        /// If the connection is established, start refreshing 
        /// </summary>
        public async void GetData()
        {
            await checkLocation();
            await CheckInternet();
            if (isConnected == true && isLocationEnabled == true)
            {
                var myTask = await Calculation.posao();
                RefreshData(myTask);
            }
        }

        /// <summary>
        /// Refresh the UI based on other methods in this class (loads icons);
        /// 
        /// </summary>
        /// <param name="days">
        /// Day structure for the given days (today and tomorrow in this case)
        /// </param>
        public void RefreshData(Day days)
        {
            int counter = 0;
            /**
             * Find Top Clothes Main
             * */
            for (int i = 0; i < days.Tomorrow[0].Top.Length; i++)
            {
                if (days.Tomorrow[0].Top[i])
                {
                    switch (i)
                    {
                        case 0:
                            _viewModel.ClothesTorso = "Images/Clothes/1080_720p/tshirt_short.png";
                            break;
                        case 1:
                            _viewModel.ClothesTorso = "Images/Clothes/1080_720p/shirt_long_sleeves.png";
                            break;

                        case 2:
                            _viewModel.ClothesTorso = "Images/Clothes/1080_720p/tank_top.png";
                            break;
                    }

                }
            }

            /**
            * Find Legs Clothes Main
            * */

            for (int i = 0; i < days.Tomorrow[0].Bottom.Length; i++)
            {
                if (days.Tomorrow[0].Bottom[i])
                {
                    switch (i)
                    {
                        case 0:
                            _viewModel.ClothesLegs = "Images/Clothes/1080_720p/long_pants.png";
                            break;
                        case 1:
                            _viewModel.ClothesLegs = "Images/Clothes/1080_720p/short_pants.png";
                            break;

                        

                    }

                }
            }

            /**
             * Find Shoes (Accessori 3) Main
             * */
            for (int i = 0; i < days.Tomorrow[0].Shoes.Length; i++)
            {
                if (days.Tomorrow[0].Shoes[i])
                {
                    switch (i)
                    {
                        case 0:
                            _viewModel.ClothesAccessori3 = "Images/Clothes/1080_720p/winter_shoes.png";
                            break;
                        case 1:
                            _viewModel.ClothesAccessori3 = "Images/Clothes/1080_720p/summer_shoes.png";
                            break;


                    }

                }
            }

            /**
             * Find Morning Accessories 
             * */

            for (int i = 0; i < (days.Tomorrow[0].Accessories.Length) && (counter < 5); i++)
            {

                if (days.Tomorrow[0].Accessories[i])
                {
                    if (counter == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.MorningAccessori1 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }
                    else if (counter == 1)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.MorningAccessori2 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }

                    else if (counter == 2)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.MorningAccessori3 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }

                    else if (counter == 3)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.MorningAccessori4 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }


                    }

                    else
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.MorningAccessori5 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }


                    }
                    counter++;
                }

            }


            /**
             * Find Afternoon Accessories 
             * */
            counter = 0;
            for (int i = 0; i < (days.Tomorrow[1].Accessories.Length) && (counter < 5); i++)
            {

                if (days.Tomorrow[1].Accessories[i])
                {
                    if (counter == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.AfternoonAccessori1 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }
                    else if (counter == 1)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.AfternoonAccessori2 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }

                    else if (counter == 2)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.AfternoonAccessori3 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }

                    else if (counter == 3)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.AfternoonAccessori4 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }

                    else
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.AfternoonAccessori5 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }
                    counter++;
                }

            }


            /**
             * Find Night Accessories 
             * */
            counter = 0;
            for (int i = 0; i < (days.Tomorrow[2].Accessories.Length) && (counter < 5); i++)
            {

                if (days.Tomorrow[2].Accessories[i])
                {
                    if (counter == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.NightAccessori1 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }
                    else if (counter == 1)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.NightAccessori2 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }
                    }

                    else if (counter == 2)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.NightAccessori3 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }

                    else if (counter == 3)
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.NightAccessori4 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }



                    else
                    {
                        switch (i)
                        {
                            case 0:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/sunglasses.png";
                                break;
                            case 1:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/jacket_light.png";
                                break;

                            case 2:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/coat_winter.png";
                                break;
                            case 3:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/winter_hat.png";
                                break;
                            case 4:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/winter_gloves.png";
                                break;
                            case 5:
                                _viewModel.NightAccessori5 = "Images/Clothes/1080_720p/scarf.png";
                                break;

                        }

                    }
                    counter++;
                }

            }

            /*Morning Percentage
             * */
            _viewModel.MorningPercentage = days.Tomorrow[0].ChanceOfRain.ToString() + "%";

            /*Afternoon Percentage
            * */
            _viewModel.AfternoonPercentage = days.Tomorrow[1].ChanceOfRain.ToString() + "%";

            /*Night Percentage
            * */
            _viewModel.NightPercentage = days.Tomorrow[2].ChanceOfRain.ToString() + "%";



            /**
             * Morning Temperature
             */
            _viewModel.MorningTemperature = days.Tomorrow[0].Temperature;


            /**
             * Afternoon Temperature
             */
            _viewModel.AfternoonTemperature = days.Tomorrow[1].Temperature;


            /**
             * NIght Temperature
             */
            _viewModel.NightTemperature = days.Tomorrow[2].Temperature;

            //City
            //If the city in view matches the city from the other day, don't fetch
            
                _viewModel.City = Calculation.cityString;
        }


    }
}

