using Newtonsoft.Json.Linq;
using SlackWeather.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SlackWeather.Controllers
{
    public class WeatherController
    {
        /// <summary>
        /// Method for getting the weather details
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public WeatherAC GetWeather(string location)
        {
            StringConstant _stringConstant = new StringConstant();
            string appId = _stringConstant.WeatherApiKey;
            string api = _stringConstant.WeatherApi;

            WeatherAC weather = new WeatherAC();

            // Take Input
            api = String.Format(api, location, appId);

            // Connect to http client
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(api);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Get weather details
            HttpResponseMessage response = client.GetAsync(api).Result;
            if (response.IsSuccessStatusCode)
            {
                var weatherObj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                // Get Coordinates
                Coordinates coord = GetCoOrdinates(weatherObj);
                weather.Latitude = coord.Latitude;
                weather.Longitude = coord.Longitude;

                // Get Humidity (%)
                weather.Humidity = GetHumidity(weatherObj);

                // Get Location Name
                var countryDetails = new RegionInfo(weatherObj.SelectToken("sys").SelectToken("country").ToString());
                weather.Location = weatherObj.SelectToken("name").ToString() + ", "
                    + countryDetails.EnglishName + " (" + countryDetails.NativeName + ")";
                
                // Get Wind Speed (km/h)
                weather.Wind = GetWindSpeed(weatherObj);

                // Get Temperature (°C)
                weather.Temperature = GetTemparature(weatherObj);

                weather.Status = GetStatus(weatherObj);
            }

            return weather;
        }

        /// <summary>
        /// Get Coordinates
        /// </summary>
        /// <param name="weatherObj"></param>
        /// <returns></returns>
        public static Coordinates GetCoOrdinates(JToken weatherObj)
        {
            var coord = weatherObj.SelectToken("coord");
            string latitude = JObject.Parse(coord.ToString()).SelectToken("lat").ToString();
            string longitude = JObject.Parse(coord.ToString()).SelectToken("lon").ToString();

            return new Coordinates()
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }

        /// <summary>
        /// Get Humidity
        /// </summary>
        /// <param name="weatherObj"></param>
        /// <returns></returns>
        public static string GetHumidity(JToken weatherObj)
        {
            var main = weatherObj.SelectToken("main");
            return JObject.Parse(main.ToString()).SelectToken("humidity").ToString() + "%";
        }

        /// <summary>
        /// Get Wind Speed 
        /// </summary>
        /// <param name="weatherObj"></param>
        /// <returns></returns>
        public static string GetWindSpeed(JToken weatherObj)
        {
            var wind = weatherObj.SelectToken("wind");
            double windSpeed = Convert.ToDouble(JObject.Parse(wind.ToString()).SelectToken("speed"));
            return (Math.Round(windSpeed * 3.6, 2)).ToString() + " km/h";
        }

        /// <summary>
        /// Get Temperature
        /// </summary>
        /// <param name="weatherObj"></param>
        /// <returns></returns>
        public static string GetTemparature(JToken weatherObj)
        {
            var main = weatherObj.SelectToken("main");
            double temp = Convert.ToDouble(JObject.Parse(main.ToString()).SelectToken("temp")) - 273.15;
            return Math.Round(temp, 2).ToString() + " °C";
        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <param name="weatherObj"></param>
        /// <returns></returns>
        public static string GetStatus(JToken weatherObj)
        {
            var weather = weatherObj.SelectToken("weather");

            return JObject.Parse(weather[0].ToString()).SelectToken("main").ToString() + '/'
                + JObject.Parse(weather[0].ToString()).SelectToken("description").ToString();
        }
    }
}