using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace cleverWeather2.Model
{
    class UpdateView
    {
        ViewModelTomorrow _viewModel;
        private bool isConnected = NetworkInterface.GetIsNetworkAvailable();
        //Constructor
        public UpdateView(ViewModelTomorrow vmt)
        {
            this._viewModel = vmt;

            GetData();
        }


        /* Explained problem with async and await
         * http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
         * */


        private async void CheckInternet()
        {

            isConnected = NetworkInterface.GetIsNetworkAvailable();
            if (!isConnected)
            {
                await new MessageDialog("No internet connection is avaliable.").ShowAsync();
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
                    await new MessageDialog("No internet connection is avaliable.").ShowAsync();
                }


            }

        }

        public async void GetData()
        {
            CheckInternet();
            if (isConnected == true)
            {
                var myTask = await posao();
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

            for (int i = 0; i < (days.Tomorrow[0].Accessories.Length) && (counter < 3); i++)
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

                        }
                    }

                    else
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

                        }

                    }
                    counter++;
                }

            }


            /**
             * Find Afternoon Accessories 
             * */
            counter = 0;
            for (int i = 0; i < (days.Tomorrow[1].Accessories.Length) && (counter < 3); i++)
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

                        }
                    }

                    else
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

                        }

                    }
                    counter++;
                }

            }


            /**
             * Find Night Accessories 
             * */
            counter = 0;
            for (int i = 0; i < (days.Tomorrow[2].Accessories.Length) && (counter < 3); i++)
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

                        }
                    }

                    else
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

        }

        /// <summary>
        /// Method posao
        /// fetch api URI in json format and forward it to methods that convert and work with data
        /// </summary>
        /// <returns>Days structure for today and tomorrow</returns>
        public async Task<Day> posao()
        {
            var httpClient = new Windows.Web.Http.HttpClient();
            var uri = new Uri("http://api.worldweatheronline.com/free/v2/weather.ashx?key=d0f78d5a8ba6c2e26d2e1ba61d493&q=Zagreb&format=json&tp=1");
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            //var parirano = JsonArray.Parse(content);
            System.Diagnostics.Debug.WriteLine(content);
            var root1 = JsonConvert.DeserializeObject<RootObject>(content);
            var dan = Calculate(root1);

            //System.Diagnostics.Debug.WriteLine( dan.Today[0].Temperature);
            System.Diagnostics.Debug.WriteLine(root1.data.weather[0].hourly[2].tempC);

            return dan;

            //System.Diagnostics.Debug.WriteLine();

        }

        /// <summary>
        /// Toni's class (possible crap cause asshole didn't comment for shit)
        /// Based on downloaded data calculates whics clothes should be worn, temperature and weather conditions (calles other methods)
        /// </summary>
        /// <param name="Information"> Json Object from API</param>
        /// <returns> Day structure containing information about today and tomorrow</returns>
        public static Day Calculate(RootObject Information)
        {
            Day days = new Day();
            int currentTime = DateTime.Now.Hour;
            Data data = Information.data;
            days.Today[0] = new Struktura();
            days.Today[1] = new Struktura();
            days.Today[2] = new Struktura();

            days.Tomorrow[0] = new Struktura();
            days.Tomorrow[1] = new Struktura();
            days.Tomorrow[2] = new Struktura();

            CalculateMorning(data.weather.ElementAt(0), days.Today[0]);
            CalculateMorning(data.weather.ElementAt(1), days.Tomorrow[0]);

            CalculateAfternoon(data.weather.ElementAt(0), days.Today[1]);
            CalculateAfternoon(data.weather.ElementAt(1), days.Tomorrow[1]);

            CalculateNight(data.weather.ElementAt(0), days.Today[2]);
            CalculateNight(data.weather.ElementAt(1), days.Tomorrow[2]);

            CalculateShared(days);
            CalculateShared(days);


            return days;
        }
        static private void CalculateMorning(Weather weather, Struktura value)
        {
            List<Hourly> hourly = weather.hourly;
            int[] weatherConditions = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int chanceOfWindy = 0;
            int temperature = 0;

            for (int i = 2; i < 4; i++)
            {
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);


            }
            value.Temperature = temperature / 2;
            chanceOfWindy = chanceOfWindy / 2;
            value.ChanceOfRain = weatherConditions[0] / 2;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max);
        }

        static private void CalculateAfternoon(Weather weather, Struktura value)
        {
            List<Hourly> hourly = weather.hourly;
            int[] weatherConditions = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int chanceOfWindy = 0;
            int temperature = 0;

            for (int i = 4; i < 6; i++)
            {
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);
            }
            value.Temperature = temperature / 2;
            chanceOfWindy = chanceOfWindy / 2;
            value.ChanceOfRain = weatherConditions[0] / 2;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max);
        }

        static private void CalculateNight(Weather weather, Struktura value)
        {
            List<Hourly> hourly = weather.hourly;
            int[] weatherConditions = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int chanceOfWindy = 0;
            int temperature = 0;
            int i = 6;
            while (i != 2)
            {
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);
                i = (i + 1) % 8;
            };
            value.Temperature = temperature / 4;
            chanceOfWindy = chanceOfWindy / 4;
            value.ChanceOfRain = weatherConditions[0] / 4;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max);
        }

        static private void CalculateShared(Day day)
        {
            int[] averageToday = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int averageTemperatureToday = 0;
            int[] averageTomorrow = new int[3];
            int averageTemperatureTomorrow = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (day.Today[i].WeatherConditions[j])
                    {
                        averageToday[j]++;
                    }
                    if (day.Tomorrow[i].WeatherConditions[j])
                    {
                        averageTomorrow[j]++;
                    }
                }
                averageTemperatureToday += day.Today[j].Temperature;
                averageTemperatureTomorrow += day.Tomorrow[j].Temperature;
            }
            averageTemperatureToday = averageTemperatureToday / 3;
            averageTemperatureTomorrow = averageTemperatureTomorrow / 3;

            int maxToday = Array.IndexOf(averageToday, averageToday.Max());
            int maxTomorrow = Array.IndexOf(averageToday, averageTomorrow.Max());
            for (int i = 0; i < 3; i++)
            {
                Shared(day.Today[i], averageTemperatureToday, maxToday);
                Shared(day.Tomorrow[i], averageTemperatureTomorrow, maxTomorrow);
            }

        }

        static private void Shared(Struktura day, int temperature, int maxIndex)
        {
            if (temperature > 22)
            {
                day.Top[0] = true;
                day.Bottom[1] = true;
                day.Shoes[1] = true;
            }
            else if (temperature > 17)
            {
                day.Top[0] = true;
                day.Bottom[0] = true;
                day.Shoes[1] = true;
            }
            else if (temperature > 12)
            {
                day.Top[1] = true;
                day.Bottom[0] = true;
                day.Shoes[1] = true;
            }
            else
            {
                day.Top[1] = true;
                day.Bottom[0] = true;
                day.Shoes[0] = true;
            }
        }
        static private void CalculateAccessories(Struktura value, int[] weatherConditions, int chanceOfWindy, int max)
        {
            if (max == 0)
            {
                value.WeatherConditions[0] = true;
            }
            else if (max == 1)
            {
                value.WeatherConditions[1] = true;
            }
            else
            {
                value.WeatherConditions[2] = true;
            }

            if (chanceOfWindy >= 50)
            {
                value.Wind = true;
            }

            if (value.Temperature > 22)
            {
                if (value.WeatherConditions[2])
                {
                    value.Accessories[0] = true;
                }
            }
            else if (value.Temperature > 10)
            {
                value.Accessories[1] = true;
            }
            else
            {
                value.Accessories[2] = true;
                value.Accessories[3] = true;
                value.Accessories[4] = true;
            }
        }
    }
}

