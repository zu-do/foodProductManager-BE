namespace PVP_Projektas_API.SecretFakeApi;

public class DistanceApi
{
    public DistanceApi()
    {

    }
    private const double EarthRadiusKm = 6371.0;

    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Convert latitude and longitude from degrees to radians
        double radiansLat1 = ToRadians(lat1);
        double radiansLon1 = ToRadians(lon1);
        double radiansLat2 = ToRadians(lat2);
        double radiansLon2 = ToRadians(lon2);

        // Calculate the differences between the latitudes and longitudes
        double deltaLat = radiansLat2 - radiansLat1;
        double deltaLon = radiansLon2 - radiansLon1;

        // Calculate the distance using the Haversine formula
        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(radiansLat1) * Math.Cos(radiansLat2) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = EarthRadiusKm * c;

        return distance;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}