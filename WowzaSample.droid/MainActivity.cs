using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using Android.Support.V7.App;
using Com.Wowza.Gocoder.Sdk.Api;
using Com.Wowza.Gocoder.Sdk.Api.Devices;
using Com.Wowza.Gocoder.Sdk.Api.Broadcast;
using Com.Wowza.Gocoder.Sdk.Api.Errors;
using Android.Content;
using Android.Support.V4.App;
using Com.Wowza.Gocoder.Sdk.Api.Configuration;
using Com.Wowza.Gocoder.Sdk.Api.Status;

namespace WowzaSample.droid
{
    [Activity(Label = "WowzaSample.droid", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity, IWZStatusCallback
    {
        public const string GoCoderKey = "GOSK-5744-0103-B17A-6E6C-C8A3";
        // The top level GoCoder API interface
        private WowzaGoCoder goCoder;

        // The GoCoder SDK camera view
        private WZCameraView goCoderCameraView;

        // The GoCoder SDK audio device
        private WZAudioDevice goCoderAudioDevice;

        // The broadcast configuration settings
        private WZBroadcastConfig goCoderBroadcastConfig;

        WZBroadcast goCoderBroadcaster;

        // Properties needed for Android 6+ permissions handling
        private const int PERMISSIONS_REQUEST_CODE = 0x1;
        private bool mPermissionsGranted = true;
        private string[] mRequiredPermissions = new string[] {
            Android. Manifest.Permission.Camera,
            Android.Manifest.Permission.RecordAudio
        };

        IWowzaConfig wowzaConfig;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            wowzaConfig = new WowzaVTCConfig();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            goCoder = WowzaGoCoder.Init(ApplicationContext, GoCoderKey);

            if (goCoder == null)
            {
                // If initialization failed, retrieve the last error and display it
                WZError goCoderInitError = WowzaGoCoder.LastError;
                Toast.MakeText(this,
                               "GoCoder SDK error: " + goCoderInitError.ErrorDescription,
                               ToastLength.Long).Show();
                return;
            }

            // Associate the WZCameraView defined in the U/I layout with the corresponding class member
            goCoderCameraView = FindViewById<WZCameraView>(Resource.Id.camera_preview);

            // Create an audio device instance for capturing and broadcasting audio
            goCoderAudioDevice = new WZAudioDevice();

            // Create a broadcaster instance
            goCoderBroadcaster = new WZBroadcast();

            // Create a configuration instance for the broadcaster
            goCoderBroadcastConfig = new WZBroadcastConfig(WZMediaConfig.FRAMESIZE320x240);

            // Designate the camera preview as the video source
            goCoderBroadcastConfig.VideoBroadcaster = (goCoderCameraView);

            // Designate the audio device as the audio broadcaster
            goCoderBroadcastConfig.AudioBroadcaster = (goCoderAudioDevice);

            goCoderBroadcastConfig.HostAddress = wowzaConfig.HostAddress;
            goCoderBroadcastConfig.PortNumber = wowzaConfig.PortNumber;
            goCoderBroadcastConfig.ApplicationName = wowzaConfig.ApplicationName;
            goCoderBroadcastConfig.StreamName = wowzaConfig.StreamName;
            goCoderBroadcastConfig.Username = wowzaConfig.Username;
            goCoderBroadcastConfig.Password = wowzaConfig.Password;

            Button broadcastButton = FindViewById<Button>(Resource.Id.broadcast_button);
            broadcastButton.Click += BroadcastButton_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                mPermissionsGranted = hasPermissions(this, mRequiredPermissions);
                if (!mPermissionsGranted)
                    ActivityCompat.RequestPermissions(this, mRequiredPermissions, PERMISSIONS_REQUEST_CODE);
            }
            else
                mPermissionsGranted = true;

            // Start the camera preview display
            if (mPermissionsGranted && goCoderCameraView != null)
            {
                if (goCoderCameraView.IsPreviewPaused)
                    goCoderCameraView.OnResume();
                else
                    goCoderCameraView.StartPreview();
            }

        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            View rootView = Window.DecorView.FindViewById(Android.Resource.Id.Content);
            if (rootView != null)
            {
                rootView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutFullscreen
                                               | SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen | SystemUiFlags.ImmersiveSticky);
            }
        }

        void BroadcastButton_Click(object sender, System.EventArgs e)
        {
            if (!mPermissionsGranted) return;

            // Ensure the minimum set of configuration settings have been specified necessary to
            // initiate a broadcast streaming session
            WZStreamingError configValidationError = goCoderBroadcastConfig.ValidateForBroadcast();

            if (configValidationError != null)
            {
                Toast.MakeText(this, configValidationError.ErrorDescription, ToastLength.Short).Show();
            }
            else if (goCoderBroadcaster.Status.IsRunning)
            {
                // Stop the broadcast that is currently running
                goCoderBroadcaster.EndBroadcast(this);
            }
            else
            {
                // Start streaming
                goCoderBroadcaster.StartBroadcast(goCoderBroadcastConfig, this);
            }
        }

        private static bool hasPermissions(Context context, string[] permissions)
        {
            //for (string permission : permissions)
            //if (context.checkCallingOrSelfPermission(permission) != PackageManager.PERMISSION_GRANTED)
            //return false;
            foreach (var permission in permissions)
            {
                if (context.CheckCallingOrSelfPermission(permission) != Permission.Granted)
                    return false;
            }
            return true;
        }

        public void OnWZError(WZStatus p0)
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(this,
                                "Streaming error: " + p0.LastError.ErrorDescription,
                                ToastLength.Short).Show();
            });
        }

        public void OnWZStatus(WZStatus p0)
        {
            string statusMessage = ("Broadcast status: ");

            switch (p0.State)
            {
                case WZState.Starting:
                    statusMessage += ("Broadcast initialization");
                    break;

                case WZState.Ready:
                    statusMessage += ("Ready to begin streaming");
                    break;

                case WZState.Running:
                    statusMessage += ("Streaming is active");
                    break;

                case WZState.Stopping:
                    statusMessage += ("Broadcast shutting down");
                    break;

                case WZState.Idle:
                    statusMessage += ("The broadcast is stopped");
                    break;

                default:
                    return;
            }
            RunOnUiThread(() =>
            {
                Toast.MakeText(this, statusMessage, ToastLength.Short).Show();
            });
        }
    }
}

