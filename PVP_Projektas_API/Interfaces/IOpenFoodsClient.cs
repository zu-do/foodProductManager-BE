using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IOpenFoodsClient
{
    Task<ProductInfo> GetProductInfo(string barcode);
}
