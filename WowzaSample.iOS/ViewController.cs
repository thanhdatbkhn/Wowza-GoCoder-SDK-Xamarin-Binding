using System;
using Foundation;
using UIKit;
using Wowza;

namespace WowzaSample.iOS
{
    public partial class ViewController : UIViewController
    {
        const string SDKSampleSavedConfigKey = "SDKSampleSavedConfigKey";
        int count = 1;
        WowzaGoCoder GoCoder;
        WZCameraPreview CameraPreview;
        IWowzaConfig wowzaConfig;
        public ViewController(IntPtr handle) : base(handle)
        {
            wowzaConfig = new WowzaVTCConfig();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            //Button.Hidden = true;
            Button.AccessibilityIdentifier = "myButton";

            WowzaGoCoder.SetLogLevel(WowzaGoCoderLogLevel.Verbose);

            Console.WriteLine(string.Format("{0} {1} {2} {3}", WZVersionInfo.MajorVersion, WZVersionInfo.MinorVersion,
                                            WZVersionInfo.Revision, WZVersionInfo.BuildNumber));

            // Register the GoCoder SDK license key
            var goCoderLicensingError = WowzaGoCoder.RegisterLicenseKey("GOSK-3444-0103-6577-EC42-DE56");
            if (goCoderLicensingError != null)
            {
                // Log license key registration failure
                Console.WriteLine(goCoderLicensingError.LocalizedDescription);
            }
            else
            {
                // Initialize the GoCoder SDK
                GoCoder = WowzaGoCoder.SharedInstance();
            }
            if (GoCoder != null)
            {
                WowzaConfig goCoderBroadcastConfig = this.GoCoder.Config;

                //// Set the defaults for 720p video
                //[goCoderBroadcastConfig loadPreset:WZFrameSizePreset1280x720];
                goCoderBroadcastConfig.LoadPreset(WZFrameSizePreset.WZFrameSizePreset1280x720);

                goCoderBroadcastConfig.HostAddress = wowzaConfig.HostAddress;
                goCoderBroadcastConfig.PortNumber = (nuint)wowzaConfig.PortNumber;
                goCoderBroadcastConfig.ApplicationName = wowzaConfig.ApplicationName;
                goCoderBroadcastConfig.StreamName = wowzaConfig.StreamName;
                goCoderBroadcastConfig.Username = wowzaConfig.Username;
                goCoderBroadcastConfig.Password = wowzaConfig.Password;

                //// Update the active config
                this.GoCoder.Config = goCoderBroadcastConfig;

                GoCoder.CameraView = this.View;
                //GoCoder.

                // Start the camera preview
                CameraPreview = GoCoder.CameraPreview;
                if (CameraPreview != null)
                    CameraPreview.StartPreview();

                Button.TouchUpInside += (sender, e) =>
                {
                    StartStreaming();
                };
                View.BringSubviewToFront(Button);
            }
        }

        void StartStreaming()
        {
            if (GoCoder.Config.ValidateForBroadcast != null)
            {
                Console.WriteLine("Wowza streaming config incomplete!");
            }
            else if (GoCoder.Status.State != WZState.Running)
            {
                GoCoder.StartStreaming(new WowzaStreamCallback());
            }
            else
            {
                GoCoder.EndStreaming(new WowzaStreamCallback());
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        class WowzaStreamCallback : WZStatusCallback
        {
            public override void OnWZError(WZStatus status)
            {
                Console.WriteLine("Wowza streaming Error!");
            }

            public override void OnWZStatus(WZStatus status)
            {
                Console.WriteLine("WowzaStateCallback: " + status.State.ToString());
            }
        }
    }
}
