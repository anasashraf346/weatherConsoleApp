
using Newtonsoft.Json.Linq;

public class Program
{
    static void Main(string[] args)
    {

        // Create an instance of the HttpClient Class called client
        var client = new HttpClient();

        // Ask the user for their city name and store that in a variable called "city_name"
        Console.Write("Please Enter the city: ");        
        var city_name = Console.ReadLine().ToLower();

        // Create a variable to store the URL (use String Interpolation for the {city_name} and {api_Key}  HINT: Make sure to use the "imperial" measurement endpoint
        //weatherstack.com was not working for free subscription users , so i am using different weather service 
        var userURL = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid=6e818e76332207b1216990927bad79d2&units=imperial";

        // Create a variable to store the response from your GET request to that URL from above  HINT: Don't forget to call .Result 
        var weatherResponse = client.GetStringAsync(userURL).Result;

        //Question # 1
        JObject jObject = JObject.Parse(weatherResponse);
        JToken mainWeather = jObject["weather"].First["main"];
        var checkMainWeather = mainWeather.ToString();
        Console.WriteLine("\nShould I go outside?");
        if (checkMainWeather == "Rain")
        {
            Console.WriteLine($"No! you can't go outside because its {checkMainWeather} right now");
        }
        else
        {
            Console.WriteLine($"Yes! you can go outside because its not raining, its {checkMainWeather} right now");
        }

        //Question # 2    //UV index data is not available, so I change the question little bit
        var Temperature = JObject.Parse(weatherResponse).GetValue("main").ToString();
        var checkTemperature = JObject.Parse(Temperature).GetValue("feels_like").ToString();

        Console.WriteLine("\nShould I wear jacket?");
        if (Convert.ToDecimal(checkTemperature) < 77)
        {
            Console.WriteLine($"Yes. Because temperature is {checkTemperature} F");
        }
        else
        {
            Console.WriteLine($"No");
        }


        //Question # 3
        var formattedResponseMain = JObject.Parse(weatherResponse).GetValue("wind").ToString();
        var checkWindSpeed = JObject.Parse(formattedResponseMain).GetValue("speed").ToString();

        Console.WriteLine("\nCan I fly my kite?");
        if (Convert.ToDecimal(checkWindSpeed) < 15 && checkMainWeather != "Rain")
        {
            Console.WriteLine($"Yes");
        }
        else
        {
            Console.WriteLine($"No");
        }



    }
}