using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace OpenAIClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "<yourAPIkey>";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "image-alpha-001",
                prompt = "Slam dunk",
                num_images = 1,
                size = "1024x1024"
            };
            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/images/generations", requestBody);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                ImageResponse responseObj =  JsonConvert.DeserializeObject<ImageResponse>(jsonString);
                var imageUrl = responseObj.data[0].url;
            }
            else { 
                Console.WriteLine($"Error generating image: {response.StatusCode}"); }
        }
    }
}


public class ImageResponse
{
    public string created { get; set; }
    public List<Data> data { get; set; }
}
public class Data
{
    public string url { get; set; }
}