using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;

public class ShowIpAdd : MonoBehaviour
{
    public TextMeshProUGUI ipText; // Reference to a UI TextMeshPro component

    void Start()
    {
        string ipAddress = GetLocalIPAddress();
        if (ipText != null)
        {
            ipText.text = "IP Address: " + ipAddress;
        }
        else
        {
            Debug.LogWarning("IP Address: " + ipAddress);
        }
    }

    // Improved method to get the local IP address
    string GetLocalIPAddress()
    {
        foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return unicastAddress.Address.ToString();
                    }
                }
            }
        }
        return "No IPv4 Address Found";
    }
}
