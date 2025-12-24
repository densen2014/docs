using System.Net;
using System.Net.NetworkInformation;

namespace MauiWebApi;

public static class NetworkHelper
{
    public static IPAddress? GetIpAddress()
    {
        // Up, Ethernet and IP4.
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(nic =>
                                    nic.OperationalStatus == OperationalStatus.Up &&
                                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                                    nic.GetIPProperties().GatewayAddresses.Count>0)
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
