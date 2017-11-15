using System;
namespace WowzaSample
{
    public class WowzaCloudConfig : IWowzaConfig
    {
        public WowzaCloudConfig()
        {
        }

        public string HostAddress => "24e1c0.entrypoint.cloud.wowza.com";

        public int PortNumber => 1935;

        public string ApplicationName => "app-fbba";

        public string StreamName => "a6cd2f9e";

        public string Username => "client10777";

        public string Password => "123456";
    }
}
