using Newtonsoft.Json;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        string apikey = "key";
        Console.WriteLine("Example Prompt: ");
        string prompt = Console.ReadLine();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Api-Key", $"Bearer {apikey}");
            var requestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.deepai.org/api/text2img", content);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
            else
            {
                Console.WriteLine("Hata: " + response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }
    }
}


