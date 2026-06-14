using System.Net;
using System.Net.NetworkInformation;

namespace MauiWebApi;

public static class NetworkHelper
{
    static bool IsPrivateNetwork(IPAddress ipv4Address)
    {

        byte[] ipBytes = ipv4Address.GetAddressBytes();
        if (ipBytes[0] == 10)
        {
            return true;
        }
        else if (ipBytes[0] == 172 && ipBytes[1] >= 16 && ipBytes[1] <= 31)
        {
            return true;
        }
        else if (ipBytes[0] == 192 && ipBytes[1] == 168)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static bool IsPrivateNetwork(string ipv4Address)
    {
        if (IPAddress.TryParse(ipv4Address, out var ip))
        {
            return IsPrivateNetwork(ip);
        }
        return false;
    }

    /// <summary>
    /// 判断IP地址是否为有效的外部地址
    /// </summary>
    public static bool IsValidIpAddress(IPAddress address)
    {
        var ip = address.ToString();
        return !ip.StartsWith("169") &&
               !ip.StartsWith("127") &&
               !ip.StartsWith("10.7") &&
               !ip.StartsWith("172");
    }

    public static List<IPAddress>? GetIpAddress()
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

        List<IPAddress> addressInfos = new();
        foreach (var network in networkInterfaces)
        {
            addressInfos.AddRange(network.GetIPProperties().UnicastAddresses.Where(ai => ai.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                IsValidIpAddress(ai.Address)).Select(a => a.Address));
        }
        return addressInfos;
    }
}
