using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apikey = "api key gelecek";
        Console.WriteLine("Yapay Zekaya Mesaj Yazın: ");

        var prompt = Console.ReadLine();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}");

        var requestBody = new
        {
            model = "llama-3.1-8b-instant",
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt}
            },
            max_tokens = 600
        };
        
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                Console.WriteLine("Yapay Zekanın Cevabı: ");
                Console.WriteLine(answer);
            }

            else
            {
                Console.WriteLine("Bir hata oluştu: " + response.StatusCode);
                Console.WriteLine(responseString);  
            }
        }
        catch (Exception er)
        {
            Console.WriteLine("Hata: " + er.Message);
        }

    }
}
