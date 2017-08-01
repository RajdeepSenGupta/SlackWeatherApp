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
            get { return "https://hooks.slack.com/services/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"; }
        }

        public string WeatherApiKey
        {
            get { return "0ff0028b695d833b6817c6fdef55146c"; }
        }

        public string WeatherApi
        {
            get { return "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}"; }
        }
    }
}