using System;
namespace WowzaSample
{
    public class WowzaVTCConfig : IWowzaConfig
    {
        public WowzaVTCConfig()
        {
        }

		public string HostAddress => "117.103.204.53";

		public int PortNumber => 1935;

		public string ApplicationName => "test";

		public string StreamName => "myStream";

		public string Username => "hoa.ngo";

		public string Password => "hoa.ngo123";
    }
}
