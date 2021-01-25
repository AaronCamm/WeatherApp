using System;
using System.Net;
using System.Text.RegularExpressions;

namespace WeatherApp
{
    class Program
    {
        static readonly string API_ACCESS_KEY = "";
        static string currentTemp;
        static string weatherDesc;
        static string windSpeed;
        static string windDir;
        static string humidity;
        
        static void Main(string[] args)
        {
            Console.Write("Enter City and Country(Melbourne Australia): ");
            var location = Console.ReadLine();

            using (var client = new WebClient())
            {
                var url = $"http://api.weatherstack.com/current?access_key={API_ACCESS_KEY}&query={location}";
                var response = client.DownloadString(url);

                currentTemp = Regex.Match(response, "temperature\":(.+?),\"weather_code").Groups[1].Value;
                weatherDesc = Regex.Match(response, "weather_descriptions\":\\[\"(.+?)\"]").Groups[1].Value;
                windSpeed = Regex.Match(response, "wind_speed\":(.+?),\"wind_degree").Groups[1].Value;
                windDir = Regex.Match(response, "wind_dir\":\"(.+?)\",\"pressure").Groups[1].Value;
                humidity = Regex.Match(response, "humidity\":(.+?),\"cloudcover").Groups[1].Value;

                var syntax = $"Current Weather In {location}\nCurrent Temp: {currentTemp}\nWeather: {weatherDesc}\nWind Speed: {windSpeed}\nWind Direction: {windDir}\nHumidity: {humidity}";

                Console.Clear();
                Console.WriteLine(FirstLetterToUpper(syntax));
            }

            Console.ReadLine();
        }

        static string FirstLetterToUpper(string str)
        {
            for (var i = 1; i < str.Length; i++)
            {
                if (str[i - 1] == ' ')
                {
                    var temp = str[i].ToString().ToUpper();
                    str = str.Remove(i, 1);
                    str = str.Insert(i, temp);
                }
            }

            return str;
        }
    }
}
