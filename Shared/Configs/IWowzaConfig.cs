using System;
namespace WowzaSample
{
    public interface IWowzaConfig
    {
        string HostAddress { get; }
        int PortNumber { get; }
        string ApplicationName { get; }
        string StreamName { get; }
        string Username { get; }
        string Password { get; }
    }
}
