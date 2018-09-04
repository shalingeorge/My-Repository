using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

using WeatherApp.Models;
using WeatherApp.Class;

namespace WeatherApp.Controllers
{
    public class OpenWeatherMapController: Controller
    {
        public ActionResult Index()
        {
            OpenWeatherMap openWeatherMap = FillCity(); //Fill the city to call the API
            return View(openWeatherMap);
        }

        [HttpPost]
        public ActionResult Index(string cities)
        {
            OpenWeatherMap openWeatherMap = FillCity();

            if (cities != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "de6d52c2ebb7b1398526329875a49c57";
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + cities + "&appid=" + apiKey ) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/

                /*http://json2csharp.com*/
                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>Location:</td><td>" + rootObject.name + "</td></tr>");           
                sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Visibility:</td><td>" + rootObject.visibility + "</td></tr>");
                sb.Append("<tr><td>Sky Conditions:</td><td>" + rootObject.weather[0].description + "</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp +"</td></tr>");
                sb.Append("<tr><td>Relative Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Dew Point:</td><td>" + rootObject.dt + "</td></tr>");
                sb.Append("<tr><td>Pressure:</td><td>" + rootObject.main.pressure + "</td></tr>");
                sb.Append("</table>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            else
            {
                if (Request.Form["submit"] != null)
                {
                    openWeatherMap.apiResponse = "Select City";
                }
            }
            return View(openWeatherMap);
        }

        public OpenWeatherMap FillCity()            //List of Cities for user selection
        {
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.cities = new Dictionary<string, string>();
            openWeatherMap.cities.Add("Melbourne", "Melbourne");
            openWeatherMap.cities.Add("Sydney", "Sydney");
            openWeatherMap.cities.Add("London", "London");
            openWeatherMap.cities.Add("Paris", "Paris");
            openWeatherMap.cities.Add("Perth", "Perth");
            return openWeatherMap;
        }
    }
}