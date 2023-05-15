namespace PVP_Projektas_API.Interfaces;

public interface IDistanceClient
{
    public double GetDistance(double latUser, double lonUser, double latProduct, double lonProduct);
}
