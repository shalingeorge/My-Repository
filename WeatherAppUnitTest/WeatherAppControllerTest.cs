using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using  WeatherApp.Controllers;
using WeatherApp.Models;

namespace WeatherAppUnitTest
{
   public class WeatherAppControllerTest
    {
        OpenWeatherMapController controller = new OpenWeatherMapController();
                                                                                                                                                                                                       

        public void OpenWeatherMap_NotNullInput()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Validate_FillCity()
        {
            // var sample = new OpenWeatherMapControlle.FillCity();
                
             OpenWeatherMap result = controller.FillCity() as OpenWeatherMap;
            
            Assert.IsNotNull(result);
        }

        public void Validate_CityInput()
        {
            // var sample = new OpenWeatherMapControlle.FillCity();

            String city = "Sydney";
            ViewResult respnse = controller.Index(city) as ViewResult;
            Assert.IsNotNull(respnse);
        }
        
    }
}
