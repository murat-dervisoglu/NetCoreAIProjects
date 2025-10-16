using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

class program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Çevirmek istediğiniz metni girin: ");
        string inputtext = Console.ReadLine();

        string apikey = "api key gelecek";
        string translatedText = await TranslateTextToEnglish(inputtext,apikey);

        if (!string.IsNullOrEmpty(translatedText))
        {
            Console.WriteLine($"Çeviri: {translatedText}");
        }
        else 
        {
            Console.WriteLine("Oğuz uyan Oğuz, hata var.");
        }

    }

    private static async Task<string> TranslateTextToEnglish(string text, string apikey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}");
            var requestbody = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new { role = "system", content = "Sen bir çevirmensin. Sadece verilen metni çevir, başka hiçbir açıklama ekleme." },
                    new { role = "user", content = "Translate this text to English" + text }
                }
            };

            string jsonBody = JsonConvert.SerializeObject(requestbody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", content); 
                string responseString = await response.Content.ReadAsStringAsync(); 

                dynamic responseObj = JsonConvert.DeserializeObject(responseString);
                string translation = responseObj.choices[0].message.content;

                return translation;
            }
            catch (Exception er)
            {
                Console.WriteLine("Bu Bir Hataydı: " + er.Message);
                return null;
            }

        }
    }
}