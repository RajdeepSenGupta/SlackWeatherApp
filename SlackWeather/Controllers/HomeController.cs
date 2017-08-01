﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SlackWeather.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Main Slack Controller for Receiving Messages
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task Slack(PayLoad payload)
        {
            StringConstant _stringConstant = new StringConstant();
            WeatherController _weather = new WeatherController();

            Uri webhookUrl = new Uri(_stringConstant.SlackIncomingWebhook);
            SlackController slackClient = new SlackController(webhookUrl);

            string location = payload.text.Remove(0, 8);
            if (location.StartsWith(":"))
            {
                string errorMessageJson = ShowErrorMessage("Please enter a valid location, zip code or an area id.");
                await slackClient.SendMessageAsync(errorMessageJson);
            }
            else
            {
                WeatherAC weather = _weather.GetWeather(location);
                if (weather.Location == null)
                {
                    string errorMessageJson = ShowErrorMessage("The location does not exist.");
                    await slackClient.SendMessageAsync(errorMessageJson);
                }
                else
                {
                    await slackClient.SendMessageAsync(GetOutputFormat(weather));
                }
            }
        }

        /// <summary>
        /// Method for formatting output
        /// </summary>
        /// <param name="weather"></param>
        /// <returns></returns>
        public string GetOutputFormat(WeatherAC weather)
        {
            var payload = new
            {
                attachments = new List<Object>()
                {
                    new
                    {
                        fallback = "Required plain-text summary of the attachment.",
                        color = "#36a64f",
                        title = weather.Location + " Weather",
                        fields = GetFields(weather)
                    }
                }
            };

            string result = JsonConvert.SerializeObject(payload);
            return result;
        }

        /// <summary>
        /// Method for getting particular fields of the weather object
        /// </summary>
        /// <param name="weather"></param>
        /// <returns></returns>
        public List<Object> GetFields(WeatherAC weather)
        {
            List<Object> weatherProperties = new List<Object>()
            {
                new
                {
                    title = "Weather",
                    value = weather.Status,
                    @short = true
                },
                new
                {
                    title = "Temperature",
                    value = weather.Temperature,
                    @short = true
                },
                new
                {
                    title = "Humidity",
                    value = weather.Humidity,
                    @short = true
                },
                new
                {
                    title = "Wind",
                    value = weather.Wind,
                    @short = true
                }
            };
            return weatherProperties;
        }

        /// <summary>
        /// Method for showing error message
        /// </summary>
        /// <param name="message"></param>
        public string ShowErrorMessage(string message)
        {
            var errorMessage = new
            {
                attachments = new List<Object>()
                    {
                        new
                        {
                            fallback = "Required plain-text summary of the attachment.",
                            color = "#FF370D",
                            title = message
                        }
                    }
            };
            string errorMessageJson = JsonConvert.SerializeObject(errorMessage);
            return errorMessageJson;
        }
    }
}