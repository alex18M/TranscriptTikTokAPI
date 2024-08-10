using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransTokAPI.Services
{
    public class TikTokTranscriber
    {
        private readonly HttpClient _httpClient;

        public TikTokTranscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranscribeTikTokAudio(string videoUrl)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://tiktok-transcript.p.rapidapi.com/transcribe-tiktok-audio"),
                Headers =
                {
                    { "x-rapidapi-key", "74778d3883msh401453335d133fep19254fjsn18230bd97d0e" },
                    { "x-rapidapi-host", "tiktok-transcript.p.rapidapi.com" },
                    { "x-rapidapi-ua", "RapidAPI-Playground" }
                },
                Content = new StringContent("{ \"url\": \"" + videoUrl + "\" }", Encoding.UTF8, "application/json")
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error: {response.StatusCode}, Content: {errorContent}");
                }
            }
        }
    }
}
