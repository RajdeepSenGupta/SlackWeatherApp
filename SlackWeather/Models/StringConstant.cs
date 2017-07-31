using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackWeather.Models
{
    public class StringConstant
    {
        public string SlackIncomingWebhook
        {
            get { return "<Put your incoming webhook url here>"; }
        }

        public string WeatherApiKey
        {
            get { return "<Put your api key here>"; }
        }

        public string WeatherApi
        {
            get { return "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}"; }
        }
    }
}