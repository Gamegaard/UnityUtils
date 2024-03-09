using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Networking;

namespace Gamegaard.Utils
{
    public static class InternetUtils
    {
        ///<summary>
        /// Returns whether the device is currently connected to the internet.
        ///</summary>
        public static bool IsConnectedToInternet()
        {
            bool isConnected = false;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback && ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    IPInterfaceProperties ipProps = ni.GetIPProperties();

                    if (ipProps.GatewayAddresses.Count > 0)
                    {
                        isConnected = true;
                        break;
                    }
                }
            }

            return isConnected;
        }

        ///<summary>
        /// Sends an email to the configured email address with a subject "App Review" and no body.
        ///</summary>
        public static void SendEmail(string email)
        {
            string subject = MyEscapeURL("App Review");
            Application.OpenURL("mailto:" + email + "?subject=" + subject + "&amp;body=");
        }

        ///<summary>
        /// Escapes the given URL string to be used as a parameter in a URL query.
        ///</summary>
        private static string MyEscapeURL(string URL)
        {
            return UnityWebRequest.EscapeURL(URL).Replace("+", "%20");
        }
    }
}