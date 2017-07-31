using Newtonsoft.Json;
using SlackWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlackWeather.Controllers
{
    public class SlackController
    {
        private readonly Uri _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webhookUrl"></param>
        public SlackController(Uri webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        /// <summary>
        /// Method for sending message to the slack channel
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendMessageAsync(string message, string channel = null, string username = null)
        {
            var response = await _httpClient.PostAsync(_webhookUrl, new StringContent(message, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}