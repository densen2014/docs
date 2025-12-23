using System.Net;
using System.Net.NetworkInformation;

namespace MauiWebApi;

public static class NetworkHelper
{
    public static IPAddress? GetIpAddress()
    {
        // Up, Ethernet and IP4.
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(network => network.OperationalStatus == OperationalStatus.Up &&
                network.GetIPProperties().UnicastAddresses.Any(ai => ai.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                network.NetworkInterfaceType != NetworkInterfaceType.Loopback))
            .ToArray();
        if (networkInterfaces.Count() == 0)
        {
            return null;
        }
        List<UnicastIPAddressInformation> addressInfos = new();
        foreach (var network in networkInterfaces)
        {
            addressInfos.AddRange(network.GetIPProperties().UnicastAddresses.Where(ai => ai.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                !ai.Address.ToString().StartsWith("169") &&
                !ai.Address.ToString().StartsWith("172")));
        } 
        return addressInfos.Count == 0 ? null : addressInfos[0].Address;
    }
}
