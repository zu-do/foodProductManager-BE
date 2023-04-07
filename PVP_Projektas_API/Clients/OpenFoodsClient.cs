using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using Newtonsoft.Json.Linq;

namespace PVP_Projektas_API.Clients;

public class OpenFoodsClient : IOpenFoodsClient
{
    private readonly HttpClient _httpClient;
    private readonly string _url = "https://world.openfoodfacts.org/api/v2/product/";

    public OpenFoodsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ProductInfo> GetProductInfo(string barcode)
    {
        var response = await _httpClient.GetAsync($"{_url}{barcode}");

        var json = await response.Content.ReadAsStringAsync();

        var product = JObject.Parse(json);
        Console.WriteLine(product);
        var name = product["product"]?["product_name"]?.ToString();
        var carbohydrates = product["product"]?["nutriments"]?["carbohydrates_100g"]?.ToString();
        var fat = product["product"]?["nutriments"]?["fat_100g"]?.ToString();
        var proteins = product["product"]?["nutriments"]?["proteins_100g"]?.ToString();
        var kcal = product["product"]?["nutriments"]?["energy-kcal_100g"]?.ToString();


        var productInfo = new ProductInfo()
        {
            ProductName = name,
            Carbohydrates = carbohydrates,
            Fat = fat,
            Proteins = proteins,
            Kcal = kcal
        };

        return productInfo;
    }
}
