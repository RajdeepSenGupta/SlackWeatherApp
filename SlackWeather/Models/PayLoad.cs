using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackWeather.Models
{
    public class PayLoad
    {
        public string token { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string text { get; set; }
    }
}