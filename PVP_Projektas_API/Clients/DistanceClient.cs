using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.SecretFakeApi;

namespace PVP_Projektas_API.Clients;

public class DistanceClient : IDistanceClient
{
    DistanceApi distanceApi = new DistanceApi();
    public double GetDistance(double latUser, double lonUser, double latProduct, double lonProduct)
    {
       var distance = distanceApi.CalculateDistance(latUser, lonUser, latProduct, lonProduct);

        return distance;
    }
}
