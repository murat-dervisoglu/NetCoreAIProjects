﻿using NetCoreAI.Project3_RapidApi.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;


var client = new HttpClient();
List<ApiSeriesViewModel> apiSeriesViewModels = new List<ApiSeriesViewModel>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "b3dabe2513msh2606952b7184c9dp1e82f4jsnd0c4ace77a25" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var series in apiSeriesViewModels)
    {
        Console.WriteLine(series.rank + " - " + series.title + " -Film puanı: " + series.rating + " Yapım yılı: " + series.year);
    }
}


