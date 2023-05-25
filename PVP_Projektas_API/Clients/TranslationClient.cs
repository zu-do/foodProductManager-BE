using Newtonsoft.Json.Linq;
using PVP_Projektas_API.Interfaces;

namespace PVP_Projektas_API.Clients
{
    public class TranslationClient : ITranslationClient
    {
        private readonly HttpClient _httpClient;

        public TranslationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "aaa1d2bce5msh3839f44df64a869p1a0e4ejsnaad49780a74f");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "nlp-translation.p.rapidapi.com");
        }

        public async Task<string> GetTranslationFromLtToEn(string productsCSV)
        {
            var responseTranslation = await _httpClient.GetAsync($"https://nlp-translation.p.rapidapi.com/v1/translate?text={productsCSV}&to=en&from=lt");

            var jsonTranslation = await responseTranslation.Content.ReadAsStringAsync();

            var result = JObject.Parse(jsonTranslation)["translated_text"]!["en"].ToString();

            return result;
        }
        public async Task<string> TranslateFromEnToLt(string text)
        {
            var responseTranslation = await _httpClient.GetAsync($"https://nlp-translation.p.rapidapi.com/v1/translate?text={text}&to=lt&from=en");

            var jsonTranslation = await responseTranslation.Content.ReadAsStringAsync();

            var result = JObject.Parse(jsonTranslation)["translated_text"]!["lt"].ToString();

            return result;
        }
    }
}
