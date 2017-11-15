using System;
using ObjCRuntime;
using Wowza;

namespace Wowza
{
    [Native]
    public enum WZState : long
    {
        Idle = 0,
        Starting,
        Running,
        Stopping,
        Buffering,
        Ready
    }

    [Native]
    public enum WZEvent : long
    {
        None = 0,
        LowBandwidth,
        BitrateReduced,
        BitrateIncreased,
        EncoderPaused,
        EncoderResumed
    }

    [Native]
    public enum WZFrameSizePreset : long
    {
        WZFrameSizePreset352x288,
        WZFrameSizePreset640x480,
        WZFrameSizePreset1280x720,
        WZFrameSizePreset1920x1080,
        WZFrameSizePreset3840x2160,
        Count
    }

    [Native]
    public enum WZBroadcastOrientation : long
    {
        SameAsDevice,
        AlwaysLandscape,
        AlwaysPortrait
    }

    [Native]
    public enum WZBroadcastScaleMode : long
    {
        t,
        ll
    }

    [Native]
    public enum WZAudioChannels : long
    {
        Mono = 1,
        Stereo = 2
    }

    [Native]
    public enum WZDataType : long
    {
        Null,
        String,
        Boolean,
        Date,
        Integer,
        Float,
        Double,
        Map,
        List
    }

    [Native]
    public enum WZDataScope : long
    {
        Module,
        Stream
    }

    [Native]
    public enum WZCameraDirection : long
    {
        Back = 0,
        Front
    }

    [Native]
    public enum WZCameraFocusMode : long
    {
        Locked = 0,
        Auto,
        Continuous
    }

    [Native]
    public enum WZCameraExposureMode : long
    {
        Locked = 0,
        Auto,
        Continuous
    }

    [Native]
    public enum WZCameraPreviewGravity : long
    {
        Aspect = 0,
        AspectFill,
        WZCameraPreviewGravityResize
    }

    [Native]
    public enum WZError : long
    {
        NoError = 0,
        InvalidHostAddress,
        InvalidPortNumber,
        InvalidApplicationName,
        InvalidStreamName,
        InvalidUsernameOrPassword,
        ConnectionError,
        InitializationError,
        InternalError,
        InvalidSDKLicense,
        ExpiredSDKLicense,
        CameraAccessDenied,
        MicrophoneAccessDenied,
        MicrophoneInsufficientPriority,
        InvalidAudioConfiguration,
        UnknownError
    }

    [Native]
    public enum WowzaGoCoderLogLevel : long
    {
        Off,
        Default,
        Verbose
    }

    [Native]
    public enum WowzaGoCoderCapturePermission : long
    {
        Authorized,
        Denied,
        NotDetermined
    }

    [Native]
    public enum WowzaGoCoderPermissionType : long
    {
        Camera,
        Microphone
    }
}
