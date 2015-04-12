using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

namespace cleverWeather2.Model
{
    static class Calculation
    {
        private static String longitude;
        private static String latitude;


        public static String cityString { get; set; }

        

        /// <summary>
        /// Shows Status bar loading indicator
        /// </summary>
        /// <returns></returns>
        public static async Task ShowLoadingIndicator()
        {
            await StatusBar.GetForCurrentView().ShowAsync();
            var progInd = StatusBar.GetForCurrentView().ProgressIndicator;
            progInd.Text = "Fetching weather and location data...";
            await progInd.ShowAsync();
        }

        /// <summary>
        /// Hides Status bar loading indicator
        /// </summary>
        /// <returns></returns>
        public static async Task HideLoadingIndicator()
        {
            var progInd = StatusBar.GetForCurrentView().ProgressIndicator;
            await progInd.HideAsync();
        }




        /// <summary>
        /// Returns current location
        /// </summary>
        /// <returns></returns>
        public static async Task GetSinglePositionAsync()
        {


            MapLocationFinderResult result = null;
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 500;
            Geoposition geoposition = null;
            Geopoint pointToReverseGeocode = null;
            while (geoposition == null && pointToReverseGeocode == null && result==null)
            {

                geoposition = await geolocator.GetGeopositionAsync();

                longitude = geoposition.Coordinate.Point.Position.Longitude.ToString();
                latitude = geoposition.Coordinate.Point.Position.Latitude.ToString();


                // reverse geocoding
                BasicGeoposition myLocation = new BasicGeoposition
                {
                    Longitude = Convert.ToDouble(geoposition.Coordinate.Point.Position.Longitude.ToString()),
                    Latitude = Convert.ToDouble(geoposition.Coordinate.Point.Position.Latitude.ToString())
                };
                pointToReverseGeocode = new Geopoint(myLocation);
                //await Task.Delay(100);
                result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);
                  Debug.WriteLine(geolocator.DesiredAccuracyInMeters);
                geolocator.DesiredAccuracyInMeters += 100;

            }


            // Try to get city from location
            try
            {
                cityString = result.Locations[0].Address.Town.ToString().ToLower();
            }
            catch (ArgumentOutOfRangeException e)
            {

                Debug.WriteLine("Array out of range " + e);
                //new MessageDialog("Could not fetch location. Restart the app and try again.").ShowAsync();
                throw;
            }

            await HideLoadingIndicator();


            //Debug.WriteLine(cityString + " " +geolocator.DesiredAccuracyInMeters);

        }
        /// <summary>
        /// Method posao
        /// fetch api URI in json format and forward it to methods that convert and work with data
        /// </summary>
        /// <returns>Days structure for today and tomorrow</returns>
        public static async Task<Day> posao()
        {
            await ShowLoadingIndicator();
            await GetSinglePositionAsync();  //latitude + longitude
            var httpClient = new Windows.Web.Http.HttpClient();
            var uri = new Uri("http://api.worldweatheronline.com/free/v2/weather.ashx?key=d0f78d5a8ba6c2e26d2e1ba61d493&q=" + latitude + longitude + "&format=json&tp=1");
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine(content);
            var root1 = JsonConvert.DeserializeObject<RootObject>(content);

            //Call to outer class for calculation
            var dan = Calculate(root1);

            return dan;



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
            int cloudy = 0;

            for (int i = 2; i < 4; i++)
            {
                cloudy += Convert.ToInt32(hourly.ElementAt(i).cloudcover);
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);
            }
            cloudy = (int)(cloudy / 2);
            value.Temperature = temperature / 2;
            value.ChanceOfRain = weatherConditions[0] / 2;
            chanceOfWindy = chanceOfWindy / 2;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max, cloudy);
        }

        static private void CalculateAfternoon(Weather weather, Struktura value)
        {
            List<Hourly> hourly = weather.hourly;
            int[] weatherConditions = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int chanceOfWindy = 0;
            int temperature = 0;
            int cloudy = 0;

            for (int i = 4; i < 6; i++)
            {
                cloudy += Convert.ToInt32(hourly.ElementAt(i).cloudcover);
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);
            }
            cloudy = (int)(cloudy / 2);
            value.Temperature = temperature / 2;
            value.ChanceOfRain = weatherConditions[0] / 2;
            chanceOfWindy = chanceOfWindy / 2;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max, cloudy);
        }

        static private void CalculateNight(Weather weather, Struktura value)
        {
            List<Hourly> hourly = weather.hourly;
            int[] weatherConditions = new int[3]; //0-chanceOfRain, 1-chanceOfSnow, 2-chanceOfSun
            int chanceOfWindy = 0;
            int temperature = 0;
            int i = 6;
            int cloudy = 0;
            while (i != 2)
            {
                cloudy += Convert.ToInt32(hourly.ElementAt(i).cloudcover);
                weatherConditions[0] += Convert.ToInt32(hourly.ElementAt(i).chanceofrain);
                weatherConditions[1] += Convert.ToInt32(hourly.ElementAt(i).chanceofsnow);
                weatherConditions[2] += Convert.ToInt32(hourly.ElementAt(i).chanceofsunshine);
                chanceOfWindy += Convert.ToInt32(hourly.ElementAt(i).chanceofwindy);
                temperature += Convert.ToInt32(hourly.ElementAt(i).tempC);
                i = (i + 1) % 8;
            };
            cloudy = (int)(cloudy / 4);
            value.Temperature = temperature / 4;
            value.ChanceOfRain = weatherConditions[0] / 4;
            chanceOfWindy = chanceOfWindy / 4;
            int max = Array.IndexOf(weatherConditions, weatherConditions.Max());
            CalculateAccessories(value, weatherConditions, chanceOfWindy, max, cloudy);
            value.Accessories[0] = false;
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
            if (temperature > 30) {
                day.Top[2] = true;
                day.Bottom[1] = true;
                day.Shoes[1] = true;
            }

            else if (temperature > 22)
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
        static private void CalculateAccessories(Struktura value, int[] weatherConditions, int chanceOfWindy, int max, int cloudy)
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

            if (value.Temperature > 15)
            {

            }
            else if (value.Temperature > 7 && value.Temperature < 16)
            {
                value.Accessories[1] = true;
            }

            else if(value.Temperature > -1 && value.Temperature < 7 ){
                value.Accessories[2] = true;
            }
            else
            {
                value.Accessories[2] = true;
                value.Accessories[3] = true;
                value.Accessories[4] = true;
            }


            if (value.WeatherConditions[2] && (cloudy <= 55))
            {
                value.Accessories[0] = true; //Sunglasses
            }

            if ((value.Temperature < 2) || (value.Temperature < 5 && chanceOfWindy >=75) ) {
                value.Accessories[5] = true; //Scarf
            }
        }
    }
}
