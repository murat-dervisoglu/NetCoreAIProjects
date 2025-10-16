﻿using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        string apikey = "key";
        string audiofilepath = "audio1.mp3";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);

            var form = new MultipartFormDataContent();
            var audioContent = new ByteArrayContent(File.ReadAllBytes(audiofilepath)); 
            audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");
            form.Add(audioContent, "file", Path.GetFileName(audiofilepath));
            form.Add(new StringContent("whisper-1"), "model");

            Console.WriteLine("Ses dosyası işleniyor...");

            var response = await client.PostAsync("https://api.openai.com/v1/audio/transcription", form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Transkript: ");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Hata: "+ response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}