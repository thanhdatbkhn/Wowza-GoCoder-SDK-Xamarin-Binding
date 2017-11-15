
// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
// to the project by right-clicking (or Control-clicking) the folder containing this source
// file and clicking "Add files..." and then simply select the native library (or libraries)
// that you want to bind.
//
// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
// architectures that the native library supports and fills in that information for you,
// however, it cannot auto-detect any Frameworks or other system libraries that the
// native library may depend on, so you'll need to fill in that information yourself.
//
// Once you've done that, you're ready to move on to binding the API...
//
//
// Here is where you'd define your API definition for the native Objective-C library.
//
// For example, to bind the following Objective-C class:
//
//     @interface Widget : NSObject {
//     }
//
// The C# binding would look like this:
//
//     [BaseType (typeof (NSObject))]
//     interface Widget {
//     }
//
// To bind Objective-C properties, such as:
//
//     @property (nonatomic, readwrite, assign) CGPoint center;
//
// You would add a property definition in the C# interface like so:
//
//     [Export ("center")]
//     CGPoint Center { get; set; }
//
// To bind an Objective-C method, such as:
//
//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
//
// You would add a method definition to the C# interface like so:
//
//     [Export ("doSomething:atIndex:")]
//     void DoSomething (NSObject object, int index);
//
// Objective-C "constructors" such as:
//
//     -(id)initWithElmo:(ElmoMuppet *)elmo;
//
// Can be bound as:
//
//     [Export ("initWithElmo:")]
//     IntPtr Constructor (ElmoMuppet elmo);
//
// For more information, see http://developer.xamarin.com/guides/ios/advanced_topics/binding_objective-c/
//
using System;
using AVFoundation;
using AudioToolbox;

using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using UIKit;
//using WowzaGoCoderSDK;

namespace Wowza
{
    interface IWZMediaSink { }

    interface IWZBroadcastComponent { }

    interface IWZAudioSink { }

    interface IWZVideoSink { }

    interface IWZStatusCallback { }

    [Static]
    //[Verify(ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern NSString *const _Nonnull WZStatusNewBitrateKey;
        [Field("WZStatusNewBitrateKey", "__Internal")]
        NSString WZStatusNewBitrateKey { get; }

        // extern NSString *const _Nonnull WZStatusPreviousBitrateKey;
        [Field("WZStatusPreviousBitrateKey", "__Internal")]
        NSString WZStatusPreviousBitrateKey { get; }
    }

    // @interface WZStatus : NSObject <NSMutableCopying, NSCopying>
    [BaseType(typeof(NSObject))]
    interface WZStatus : INSMutableCopying, INSCopying
    {
        // @property (nonatomic) WZState state;
        [Export("state", ArgumentSemantic.Assign)]
        WZState State { get; set; }

        // @property (nonatomic) WZEvent event;
        [Export("event", ArgumentSemantic.Assign)]
        WZEvent Event { get; set; }

        // @property (nonatomic, strong) NSError * _Nullable error;
        [NullAllowed, Export("error", ArgumentSemantic.Strong)]
        NSError Error { get; set; }

        // @property (nonatomic, strong) NSDictionary * _Nullable data;
        [NullAllowed, Export("data", ArgumentSemantic.Strong)]
        NSDictionary Data { get; set; }

        // +(instancetype _Nonnull)statusWithState:(WZState)aState;
        [Static]
        [Export("statusWithState:")]
        WZStatus StatusWithState(WZState aState);

        // +(instancetype _Nonnull)statusWithStateAndError:(WZState)aState aError:(NSError * _Nonnull)aError;
        [Static]
        [Export("statusWithStateAndError:aError:")]
        WZStatus StatusWithStateAndError(WZState aState, NSError aError);

        // +(instancetype _Nonnull)statusWithEvent:(WZEvent)event;
        [Static]
        [Export("statusWithEvent:")]
        WZStatus StatusWithEvent(WZEvent @event);

        // +(instancetype _Nonnull)statusWithState:(WZState)aState event:(WZEvent)event;
        [Static]
        [Export("statusWithState:event:")]
        WZStatus StatusWithState(WZState aState, WZEvent @event);

        // -(instancetype _Nonnull)initWithState:(WZState)aState;
        [Export("initWithState:")]
        IntPtr Constructor(WZState aState);

        // -(instancetype _Nonnull)initWithStateAndError:(WZState)aState aError:(NSError * _Nonnull)aError;
        [Export("initWithStateAndError:aError:")]
        IntPtr Constructor(WZState aState, NSError aError);

        // -(instancetype _Nonnull)initWithEvent:(WZEvent)event;
        [Export("initWithEvent:")]
        IntPtr Constructor(WZEvent @event);

        // -(instancetype _Nonnull)initWithState:(WZState)aState event:(WZEvent)event;
        [Export("initWithState:event:")]
        IntPtr Constructor(WZState aState, WZEvent @event);

        // -(void)resetStatus;
        [Export("resetStatus")]
        void ResetStatus();

        // -(void)resetStatusWithState:(WZState)aState;
        [Export("resetStatusWithState:")]
        void ResetStatusWithState(WZState aState);

        // @property (readonly, nonatomic) BOOL isIdle;
        [Export("isIdle")]
        bool IsIdle { get; }

        // @property (readonly, nonatomic) BOOL isStarting;
        [Export("isStarting")]
        bool IsStarting { get; }

        // @property (readonly, nonatomic) BOOL isReady;
        [Export("isReady")]
        bool IsReady { get; }

        // @property (readonly, nonatomic) BOOL isRunning;
        [Export("isRunning")]
        bool IsRunning { get; }

        // @property (readonly, nonatomic) BOOL isStopping;
        [Export("isStopping")]
        bool IsStopping { get; }

        // @property (readonly, nonatomic) BOOL hasError;
        [Export("hasError")]
        bool HasError { get; }
    }

    // @interface WZMediaConfig : NSObject <NSMutableCopying, NSCopying, NSCoding>
    [BaseType(typeof(NSObject))]
    interface WZMediaConfig : INSMutableCopying, INSCopying, INSCoding
    {
        // +(NSString * _Nullable)AVCaptureSessionPresetFromPreset:(WZFrameSizePreset)wzPreset;
        [Static]
        [Export("AVCaptureSessionPresetFromPreset:")]
        [return: NullAllowed]
        string AVCaptureSessionPresetFromPreset(WZFrameSizePreset wzPreset);

        // +(CGSize)CGSizeFromPreset:(WZFrameSizePreset)preset;
        [Static]
        [Export("CGSizeFromPreset:")]
        CGSize CGSizeFromPreset(WZFrameSizePreset preset);

        // +(NSString * _Nullable)closestAVCaptureSessionPresetByWidth:(NSUInteger)width;
        [Static]
        [Export("closestAVCaptureSessionPresetByWidth:")]
        [return: NullAllowed]
        string ClosestAVCaptureSessionPresetByWidth(nuint width);

        // +(NSArray * _Nonnull)presets;
        [Static]
        [Export("presets")]
        ////[Verify(MethodToProperty), Verify(StronglyTypedNSArray)]
        NSObject[] Presets { get; }

        // +(NSArray * _Nonnull)presetConfigs;
        [Static]
        [Export("presetConfigs")]
        ////[Verify(MethodToProperty), Verify(StronglyTypedNSArray)]
        NSObject[] PresetConfigs { get; }

        // @property (assign, nonatomic) BOOL videoEnabled;
        [Export("videoEnabled")]
        bool VideoEnabled { get; set; }

        // @property (assign, nonatomic) BOOL audioEnabled;
        [Export("audioEnabled")]
        bool AudioEnabled { get; set; }

        // @property (assign, nonatomic) NSUInteger videoWidth;
        [Export("videoWidth")]
        nuint VideoWidth { get; set; }

        // @property (assign, nonatomic) NSUInteger videoHeight;
        [Export("videoHeight")]
        nuint VideoHeight { get; set; }

        // @property (assign, nonatomic) NSUInteger videoFrameRate;
        [Export("videoFrameRate")]
        nuint VideoFrameRate { get; set; }

        // @property (assign, nonatomic) NSUInteger videoKeyFrameInterval;
        [Export("videoKeyFrameInterval")]
        nuint VideoKeyFrameInterval { get; set; }

        // @property (assign, nonatomic) NSUInteger videoBitrate;
        [Export("videoBitrate")]
        nuint VideoBitrate { get; set; }

        // @property (assign, nonatomic) Float32 videoBitrateLowBandwidthScalingFactor;
        [Export("videoBitrateLowBandwidthScalingFactor")]
        float VideoBitrateLowBandwidthScalingFactor { get; set; }

        // @property (assign, nonatomic) NSUInteger videoFrameBufferSizeMultiplier;
        [Export("videoFrameBufferSizeMultiplier")]
        nuint VideoFrameBufferSizeMultiplier { get; set; }

        // @property (assign, nonatomic) NSUInteger videoFrameRateLowBandwidthSkipCount;
        [Export("videoFrameRateLowBandwidthSkipCount")]
        nuint VideoFrameRateLowBandwidthSkipCount { get; set; }

        // @property (assign, nonatomic) BOOL capturedVideoRotates;
        [Export("capturedVideoRotates")]
        bool CapturedVideoRotates { get; set; }

        // @property (assign, nonatomic) BOOL videoPreviewRotates;
        [Export("videoPreviewRotates")]
        bool VideoPreviewRotates { get; set; }

        // @property (assign, nonatomic) WZBroadcastOrientation broadcastVideoOrientation;
        [Export("broadcastVideoOrientation", ArgumentSemantic.Assign)]
        WZBroadcastOrientation BroadcastVideoOrientation { get; set; }

        // @property (assign, nonatomic) WZBroadcastScaleMode broadcastScaleMode;
        [Export("broadcastScaleMode", ArgumentSemantic.Assign)]
        WZBroadcastScaleMode BroadcastScaleMode { get; set; }

        // @property (assign, nonatomic) BOOL mirrorFrontCamera;
        [Export("mirrorFrontCamera")]
        bool MirrorFrontCamera { get; set; }

        // @property (assign, nonatomic) BOOL backgroundBroadcastEnabled;
        [Export("backgroundBroadcastEnabled")]
        bool BackgroundBroadcastEnabled { get; set; }

        // @property (assign, nonatomic) NSUInteger audioChannels;
        [Export("audioChannels")]
        nuint AudioChannels { get; set; }

        // @property (assign, nonatomic) NSUInteger audioSampleRate;
        [Export("audioSampleRate")]
        nuint AudioSampleRate { get; set; }

        // @property (assign, nonatomic) NSUInteger audioBitrate;
        [Export("audioBitrate")]
        nuint AudioBitrate { get; set; }

        // @property (readonly, nonatomic) CGSize frameSize;
        [Export("frameSize")]
        CGSize FrameSize { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull frameSizeLabel;
        [Export("frameSizeLabel")]
        string FrameSizeLabel { get; }

        // -(instancetype _Nonnull)initWithPreset:(WZFrameSizePreset)preset;
        [Export("initWithPreset:")]
        IntPtr Constructor(WZFrameSizePreset preset);

        // -(void)loadPreset:(WZFrameSizePreset)preset;
        [Export("loadPreset:")]
        void LoadPreset(WZFrameSizePreset preset);

        // -(NSString * _Nullable)toAVCaptureSessionPreset;
        [NullAllowed, Export("toAVCaptureSessionPreset")]
        //[Verify(MethodToProperty)]
        string ToAVCaptureSessionPreset { get; }

        // -(NSString * _Nonnull)toClosestAVCaptureSessionPreset;
        [Export("toClosestAVCaptureSessionPreset")]
        //[Verify(MethodToProperty)]
        string ToClosestAVCaptureSessionPreset { get; }

        // -(WZFrameSizePreset)toPreset;
        [Export("toPreset")]
        //[Verify(MethodToProperty)]
        WZFrameSizePreset ToPreset { get; }

        // -(WZFrameSizePreset)toClosestPreset;
        [Export("toClosestPreset")]
        //[Verify(MethodToProperty)]
        WZFrameSizePreset ToClosestPreset { get; }

        // -(BOOL)equals:(WZMediaConfig * _Nonnull)other;
        [Export("equals:")]
        bool Equals(WZMediaConfig other);

        // -(BOOL)isPortrait;
        [Export("isPortrait")]
        //[Verify(MethodToProperty)]
        bool IsPortrait { get; }

        // -(BOOL)isLandscape;
        [Export("isLandscape")]
        //[Verify(MethodToProperty)]
        bool IsLandscape { get; }

        // -(void)copyTo:(WZMediaConfig * _Nonnull)other;
        [Export("copyTo:")]
        void CopyTo(WZMediaConfig other);
    }

    // typedef void (^WZDataCallback)(WZDataMap * _Nullable, BOOL);
    delegate void WZDataCallback([NullAllowed] WZDataMap arg0, bool arg1);

    // @interface WZDataItem : NSObject <NSMutableCopying, NSCopying, NSCoding>
    [BaseType(typeof(NSObject))]
    interface WZDataItem : INSMutableCopying, INSCopying, INSCoding
    {
        // @property (readonly, assign, nonatomic) WZDataType type;
        [Export("type", ArgumentSemantic.Assign)]
        WZDataType Type { get; }

        // +(instancetype _Nonnull)itemWithInteger:(NSInteger)value;
        [Static]
        [Export("itemWithInteger:")]
        WZDataItem ItemWithInteger(nint value);

        // +(instancetype _Nonnull)itemWithDouble:(double)value;
        [Static]
        [Export("itemWithDouble:")]
        WZDataItem ItemWithDouble(double value);

        // +(instancetype _Nonnull)itemWithFloat:(float)value;
        [Static]
        [Export("itemWithFloat:")]
        WZDataItem ItemWithFloat(float value);

        // +(instancetype _Nonnull)itemWithBool:(BOOL)value;
        [Static]
        [Export("itemWithBool:")]
        WZDataItem ItemWithBool(bool value);

        // +(instancetype _Nonnull)itemWithString:(NSString * _Nonnull)value;
        [Static]
        [Export("itemWithString:")]
        WZDataItem ItemWithString(string value);

        // +(instancetype _Nonnull)itemWithDate:(NSDate * _Nonnull)value;
        [Static]
        [Export("itemWithDate:")]
        WZDataItem ItemWithDate(NSDate value);

        // -(NSInteger)integerValue;
        [Export("integerValue")]
        //[Verify(MethodToProperty)]
        nint IntegerValue { get; }

        // -(double)doubleValue;
        [Export("doubleValue")]
        //[Verify(MethodToProperty)]
        double DoubleValue { get; }

        // -(float)floatValue;
        [Export("floatValue")]
        //[Verify(MethodToProperty)]
        float FloatValue { get; }

        // -(BOOL)boolValue;
        [Export("boolValue")]
        //[Verify(MethodToProperty)]
        bool BoolValue { get; }

        // -(NSString * _Nullable)stringValue;
        [NullAllowed, Export("stringValue")]
        //[Verify(MethodToProperty)]
        string StringValue { get; }

        // -(NSDate * _Nullable)dateValue;
        [NullAllowed, Export("dateValue")]
        //[Verify(MethodToProperty)]
        NSDate DateValue { get; }

        // -(WZDataMap * _Nullable)mapValue;
        [NullAllowed, Export("mapValue")]
        //[Verify(MethodToProperty)]
        WZDataMap MapValue { get; }

        // -(WZDataList * _Nullable)listValue;
        [NullAllowed, Export("listValue")]
        //[Verify(MethodToProperty)]
        WZDataList ListValue { get; }
    }

    // @interface WZDataMap : WZDataItem
    [BaseType(typeof(WZDataItem))]
    interface WZDataMap
    {
        // @property (readonly, nonatomic, strong) NSMutableDictionary<NSString *,WZDataItem *> * _Nullable data;
        [NullAllowed, Export("data", ArgumentSemantic.Strong)]
        NSMutableDictionary<NSString, WZDataItem> Data { get; }

        // +(instancetype _Nonnull)dataMapWithDictionary:(NSDictionary<NSString *,WZDataItem *> * _Nonnull)dictionary;
        [Static]
        [Export("dataMapWithDictionary:")]
        WZDataMap DataMapWithDictionary(NSDictionary<NSString, WZDataItem> dictionary);

        // -(instancetype _Nonnull)initWithDictionary:(NSDictionary<NSString *,WZDataItem *> * _Nonnull)dictionary;
        [Export("initWithDictionary:")]
        IntPtr Constructor(NSDictionary<NSString, WZDataItem> dictionary);

        // -(void)setInteger:(NSInteger)value forKey:(NSString * _Nonnull)key;
        [Export("setInteger:forKey:")]
        void SetInteger(nint value, string key);

        // -(void)setDouble:(double)value forKey:(NSString * _Nonnull)key;
        [Export("setDouble:forKey:")]
        void SetDouble(double value, string key);

        // -(void)setFloat:(float)value forKey:(NSString * _Nonnull)key;
        [Export("setFloat:forKey:")]
        void SetFloat(float value, string key);

        // -(void)setBool:(BOOL)value forKey:(NSString * _Nonnull)key;
        [Export("setBool:forKey:")]
        void SetBool(bool value, string key);

        // -(void)setString:(NSString * _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setString:forKey:")]
        void SetString([NullAllowed] string value, string key);

        // -(void)setDate:(NSDate * _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setDate:forKey:")]
        void SetDate([NullAllowed] NSDate value, string key);

        // -(void)setItem:(WZDataItem * _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setItem:forKey:")]
        void SetItem([NullAllowed] WZDataItem value, string key);

        // -(void)setMap:(WZDataMap * _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setMap:forKey:")]
        void SetMap([NullAllowed] WZDataMap value, string key);

        // -(void)setList:(WZDataList * _Nullable)value forKey:(NSString * _Nonnull)key;
        [Export("setList:forKey:")]
        void SetList([NullAllowed] WZDataList value, string key);

        // -(void)remove:(NSString * _Nonnull)key;
        [Export("remove:")]
        void Remove(string key);
    }

    // @interface WZDataList : WZDataItem
    [BaseType(typeof(WZDataItem))]
    interface WZDataList
    {
        // @property (readonly, nonatomic, strong) NSMutableArray<WZDataItem *> * _Nullable elements;
        [NullAllowed, Export("elements", ArgumentSemantic.Strong)]
        NSMutableArray<WZDataItem> Elements { get; }

        // +(instancetype _Nullable)dataListWithArray:(NSArray<WZDataItem *> * _Nonnull)array;
        [Static]
        [Export("dataListWithArray:")]
        [return: NullAllowed]
        WZDataList DataListWithArray(WZDataItem[] array);

        // +(NSUInteger)maximumSize;
        [Static]
        [Export("maximumSize")]
        //[Verify(MethodToProperty)]
        nuint MaximumSize { get; }

        // -(instancetype _Nullable)initWithArray:(NSArray<WZDataItem *> * _Nonnull)array;
        [Export("initWithArray:")]
        IntPtr Constructor(WZDataItem[] array);

        // -(void)addInteger:(NSInteger)value;
        [Export("addInteger:")]
        void AddInteger(nint value);

        // -(void)addDouble:(double)value;
        [Export("addDouble:")]
        void AddDouble(double value);

        // -(void)addFloat:(float)value;
        [Export("addFloat:")]
        void AddFloat(float value);

        // -(void)addBool:(BOOL)value;
        [Export("addBool:")]
        void AddBool(bool value);

        // -(void)addString:(NSString * _Nonnull)value;
        [Export("addString:")]
        void AddString(string value);

        // -(void)addDate:(NSDate * _Nonnull)value;
        [Export("addDate:")]
        void AddDate(NSDate value);

        // -(void)addItem:(WZDataItem * _Nonnull)value;
        [Export("addItem:")]
        void AddItem(WZDataItem value);

        // -(void)addMap:(WZDataMap * _Nonnull)value;
        [Export("addMap:")]
        void AddMap(WZDataMap value);

        // -(void)addList:(WZDataList * _Nonnull)value;
        [Export("addList:")]
        void AddList(WZDataList value);
    }

    // @interface WZStreamConfig : WZMediaConfig <NSMutableCopying, NSCopying, NSCoding>
    [BaseType(typeof(WZMediaConfig))]
    interface WZStreamConfig : INSMutableCopying, INSCopying, INSCoding
    {
        // @property (nonatomic, strong) NSString * _Nullable hostAddress;
        [NullAllowed, Export("hostAddress", ArgumentSemantic.Strong)]
        string HostAddress { get; set; }

        // @property (assign, nonatomic) NSUInteger portNumber;
        [Export("portNumber")]
        nuint PortNumber { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable applicationName;
        [NullAllowed, Export("applicationName", ArgumentSemantic.Strong)]
        string ApplicationName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable streamName;
        [NullAllowed, Export("streamName", ArgumentSemantic.Strong)]
        string StreamName { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable username;
        [NullAllowed, Export("username", ArgumentSemantic.Strong)]
        string Username { get; set; }

        // @property (nonatomic, strong) NSString * _Nullable password;
        [NullAllowed, Export("password", ArgumentSemantic.Strong)]
        string Password { get; set; }

        // @property (nonatomic, strong) WZDataMap * _Nullable connectionParameters;
        [NullAllowed, Export("connectionParameters", ArgumentSemantic.Strong)]
        WZDataMap ConnectionParameters { get; set; }

        // -(instancetype _Nonnull)initWithPreset:(WZFrameSizePreset)preset;
        [Export("initWithPreset:")]
        IntPtr Constructor(WZFrameSizePreset preset);

        // -(NSError * _Nullable)validateForBroadcast;
        [NullAllowed, Export("validateForBroadcast")]
        //[Verify(MethodToProperty)]
        NSError ValidateForBroadcast { get; }

        // -(void)copyTo:(WZStreamConfig * _Nonnull)other;
        [Export("copyTo:")]
        void CopyTo(WZStreamConfig other);
    }

    // @protocol WZMediaSink <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface WZMediaSink
    {
    }

    // @protocol WZBroadcastComponent <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface WZBroadcastComponent
    {
        // @required -(WZStatus * _Nonnull)getStatus;
        [Abstract]
        [Export("getStatus")]
        //[Verify(MethodToProperty)]
        WZStatus Status { get; }

        // @required -(WZStatus * _Nonnull)prepareForBroadcast:(WZStreamConfig * _Nonnull)config;
        [Abstract]
        [Export("prepareForBroadcast:")]
        WZStatus PrepareForBroadcast(WZStreamConfig config);

        // @required -(WZStatus * _Nonnull)startBroadcasting;
        [Abstract]
        [Export("startBroadcasting")]
        //[Verify(MethodToProperty)]
        WZStatus StartBroadcasting { get; }

        // @required -(WZStatus * _Nonnull)stopBroadcasting;
        [Abstract]
        [Export("stopBroadcasting")]
        //[Verify(MethodToProperty)]
        WZStatus StopBroadcasting { get; }

        // @optional -(void)registerSink:(id<WZMediaSink> _Nonnull)sink;
        [Export("registerSink:")]
        void RegisterSink(WZMediaSink sink);

        // @optional -(void)unregisterSink:(id<WZMediaSink> _Nonnull)sink;
        [Export("unregisterSink:")]
        void UnregisterSink(WZMediaSink sink);
    }



    // @protocol WZAudioEncoderSink <WZMediaSink>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    interface WZAudioEncoderSink : WZMediaSink
    {
        // @optional -(void)audioSampleWasEncoded:(CMSampleBufferRef _Nullable)data;
        [Export("audioSampleWasEncoded:")]
        unsafe void AudioSampleWasEncoded([NullAllowed] CMSampleBuffer data);

        // @optional -(void)audioFrameWasEncoded:(void * _Nonnull)data size:(uint32_t)size time:(CMTime)time sampleRate:(Float64)sampleRate;
        //[Export("audioFrameWasEncoded:size:time:sampleRate:")]
        //unsafe void AudioFrameWasEncoded(void* data, uint size, CMTime time, double sampleRate);
    }

    // @protocol WZAudioSink <WZMediaSink>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    interface WZAudioSink : WZMediaSink
    {
        // @optional -(void)audioFrameWasCaptured:(void * _Nonnull)data size:(uint32_t)size time:(CMTime)time sampleRate:(Float64)sampleRate;
        //[Export("audioFrameWasCaptured:size:time:sampleRate:")]
        //unsafe void AudioFrameWasCaptured(void* data, uint size, CMTime time, double sampleRate);

        // @optional -(void)audioPCMFrameWasCaptured:(const AudioStreamBasicDescription * _Nonnull)pcmASBD bufferList:(const AudioBufferList * _Nonnull)bufferList time:(CMTime)time sampleRate:(Float64)sampleRate;
        [Export("audioPCMFrameWasCaptured:bufferList:time:sampleRate:")]
        unsafe void AudioPCMFrameWasCaptured(AudioStreamBasicDescription pcmASBD, AudioBuffers bufferList, CMTime time, double sampleRate);

        // @optional -(BOOL)canConvertStreamWithDescription:(const AudioStreamBasicDescription * _Nonnull)asbd;
        [Export("canConvertStreamWithDescription:")]
        unsafe bool CanConvertStreamWithDescription(AudioStreamBasicDescription asbd);

        // @optional -(void)audioLevelDidChange:(float)level;
        [Export("audioLevelDidChange:")]
        void AudioLevelDidChange(float level);
    }

    // @interface WZAACEncoder : NSObject <WZBroadcastComponent, WZAudioSink>
    [BaseType(typeof(NSObject))]
    interface WZAACEncoder : IWZBroadcastComponent, IWZAudioSink
    {
        // -(void)registerSink:(id<WZAudioEncoderSink> _Nonnull)sink;
        [Export("registerSink:")]
        void RegisterSink(WZAudioEncoderSink sink);

        // -(void)unregisterSink:(id<WZAudioEncoderSink> _Nonnull)sink;
        [Export("unregisterSink:")]
        void UnregisterSink(WZAudioEncoderSink sink);
    }

    // @interface WZAudioDevice : NSObject <WZBroadcastComponent>
    [BaseType(typeof(NSObject))]
    interface WZAudioDevice : IWZBroadcastComponent
    {
        // @property (assign, nonatomic) BOOL paused;
        [Export("paused")]
        bool Paused { get; set; }

        // -(instancetype _Nonnull)initWithOptions:(AVAudioSessionCategoryOptions)options;
        [Export("initWithOptions:")]
        IntPtr Constructor(AVAudioSessionCategoryOptions options);

        // -(void)registerSink:(id<WZAudioSink> _Nonnull)sink;
        [Export("registerSink:")]
        void RegisterSink(WZAudioSink sink);

        // -(void)unregisterSink:(id<WZAudioSink> _Nonnull)sink;
        [Export("unregisterSink:")]
        void UnregisterSink(WZAudioSink sink);

        // +(NSArray * _Nullable)supportedBitratesForSampleRateAndChannels:(Float64)sampleRate channels:(UInt32)numChannels;
        [Static]
        [Export("supportedBitratesForSampleRateAndChannels:channels:")]
        //[Verify(StronglyTypedNSArray)]
        [return: NullAllowed]
        NSObject[] SupportedBitratesForSampleRateAndChannels(double sampleRate, uint numChannels);
    }

    // @protocol WZStatusCallback <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface WZStatusCallback
    {
        // @required -(void)onWZStatus:(WZStatus *)status;
        [Abstract]
        [Export("onWZStatus:")]
        void OnWZStatus(WZStatus status);

        // @required -(void)onWZError:(WZStatus *)status;
        [Abstract]
        [Export("onWZError:")]
        void OnWZError(WZStatus status);

        // @optional -(void)onWZEvent:(WZStatus *)status;
        [Export("onWZEvent:")]
        void OnWZEvent(WZStatus status);
    }

    // @interface WZDataEvent : NSObject
    [BaseType(typeof(NSObject))]
    interface WZDataEvent
    {
        // @property (nonatomic, strong) NSString * _Nullable eventName;
        [NullAllowed, Export("eventName", ArgumentSemantic.Strong)]
        string EventName { get; set; }

        // @property (nonatomic, strong) WZDataMap * _Nullable eventMapParams;
        [NullAllowed, Export("eventMapParams", ArgumentSemantic.Strong)]
        WZDataMap EventMapParams { get; set; }

        // -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name mapParams:(WZDataMap * _Nonnull)mapParams;
        [Export("initWithName:mapParams:")]
        IntPtr Constructor(string name, WZDataMap mapParams);
    }

    // @protocol WZDataSink <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface WZDataSink
    {
        // @required -(void)onData:(WZDataEvent * _Nonnull)dataEvent;
        [Abstract]
        [Export("onData:")]
        void OnData(WZDataEvent dataEvent);
    }

    // @interface WZBroadcast : NSObject
    [BaseType(typeof(NSObject))]
    interface WZBroadcast
    {
        // @property (readonly, nonatomic) WZStatus * _Nonnull status;
        [Export("status")]
        WZStatus Status { get; }

        // @property (nonatomic, unsafe_unretained) id<WZStatusCallback> _Nullable statusCallback;
        [NullAllowed, Export("statusCallback", ArgumentSemantic.Assign)]
        WZStatusCallback StatusCallback { get; set; }

        // @property (nonatomic, strong) id<WZBroadcastComponent> _Nullable videoEncoder;
        [NullAllowed, Export("videoEncoder", ArgumentSemantic.Strong)]
        WZBroadcastComponent VideoEncoder { get; set; }

        // @property (nonatomic, strong) id<WZBroadcastComponent> _Nullable audioEncoder;
        [NullAllowed, Export("audioEncoder", ArgumentSemantic.Strong)]
        WZBroadcastComponent AudioEncoder { get; set; }

        // @property (nonatomic, strong) id<WZBroadcastComponent> _Nullable audioDevice;
        [NullAllowed, Export("audioDevice", ArgumentSemantic.Strong)]
        WZBroadcastComponent AudioDevice { get; set; }

        // @property (readonly, nonatomic) WZDataMap * _Nullable metaData;
        [NullAllowed, Export("metaData")]
        WZDataMap MetaData { get; }

        // -(WZStatus * _Nonnull)startBroadcast:(WZStreamConfig * _Nonnull)config statusCallback:(id<WZStatusCallback> _Nullable)statusCallback;
        [Export("startBroadcast:statusCallback:")]
        WZStatus StartBroadcast(WZStreamConfig config, [NullAllowed] WZStatusCallback statusCallback);

        // -(WZStatus * _Nonnull)endBroadcast:(id<WZStatusCallback> _Nullable)statusCallback;
        [Export("endBroadcast:")]
        WZStatus EndBroadcast([NullAllowed] WZStatusCallback statusCallback);

        // -(void)sendDataEvent:(WZDataScope)scope eventName:(NSString * _Nonnull)eventName params:(WZDataMap * _Nonnull)params callback:(WZDataCallback _Nullable)callback;
        [Export("sendDataEvent:eventName:params:callback:")]
        void SendDataEvent(WZDataScope scope, string eventName, WZDataMap @params, [NullAllowed] WZDataCallback callback);

        // -(void)registerDataSink:(id<WZDataSink> _Nonnull)sink eventName:(NSString * _Nonnull)eventName;
        [Export("registerDataSink:eventName:")]
        void RegisterDataSink(WZDataSink sink, string eventName);

        // -(void)unregisterDataSink:(id<WZDataSink> _Nonnull)sink eventName:(NSString * _Nonnull)eventName;
        [Export("unregisterDataSink:eventName:")]
        void UnregisterDataSink(WZDataSink sink, string eventName);
    }

    // @interface WowzaConfig : WZStreamConfig <NSMutableCopying, NSCopying, NSCoding>
    [BaseType(typeof(WZStreamConfig))]
    interface WowzaConfig : INSMutableCopying, INSCopying, INSCoding
    {
        // -(instancetype _Nonnull)initWithPreset:(WZFrameSizePreset)preset;
        [Export("initWithPreset:")]
        IntPtr Constructor(WZFrameSizePreset preset);

        // -(void)copyTo:(WowzaConfig * _Nonnull)other;
        [Export("copyTo:")]
        void CopyTo(WowzaConfig other);
    }

    // @interface WZCamera : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface WZCamera
    {
        // @property (readonly) NSUInteger cameraId;
        [Export("cameraId")]
        nuint CameraId { get; }

        // @property (readonly) WZCameraDirection direction;
        [Export("direction")]
        WZCameraDirection Direction { get; }

        // @property (readonly, nonatomic) AVCaptureDevice * _Nonnull iOSCaptureDevice;
        [Export("iOSCaptureDevice")]
        AVCaptureDevice IOSCaptureDevice { get; }

        // @property (readonly, nonatomic) NSArray<NSValue *> * _Nonnull frameSizes;
        [Export("frameSizes")]
        NSValue[] FrameSizes { get; }

        // @property (readonly, nonatomic) NSArray<WZMediaConfig *> * _Nonnull supportedPresetConfigs;
        [Export("supportedPresetConfigs")]
        WZMediaConfig[] SupportedPresetConfigs { get; }

        // @property (readonly, nonatomic) BOOL hasTorch;
        [Export("hasTorch")]
        bool HasTorch { get; }

        // @property (getter = isTorchOn, nonatomic) BOOL torchOn;
        [Export("torchOn")]
        bool TorchOn { [Bind("isTorchOn")] get; set; }

        // @property (nonatomic) WZCameraFocusMode focusMode;
        [Export("focusMode", ArgumentSemantic.Assign)]
        WZCameraFocusMode FocusMode { get; set; }

        // @property (nonatomic) WZCameraExposureMode exposureMode;
        [Export("exposureMode", ArgumentSemantic.Assign)]
        WZCameraExposureMode ExposureMode { get; set; }

        // -(instancetype _Nonnull)initWithCaptureDevice:(AVCaptureDevice * _Nonnull)captureDevice;
        [Export("initWithCaptureDevice:")]
        IntPtr Constructor(AVCaptureDevice captureDevice);

        // -(BOOL)supportsWidth:(NSUInteger)width;
        [Export("supportsWidth:")]
        bool SupportsWidth(nuint width);

        // -(BOOL)supportsFocusMode:(WZCameraFocusMode)mode;
        [Export("supportsFocusMode:")]
        bool SupportsFocusMode(WZCameraFocusMode mode);

        // -(BOOL)supportsExposureMode:(WZCameraExposureMode)mode;
        [Export("supportsExposureMode:")]
        bool SupportsExposureMode(WZCameraExposureMode mode);

        // -(BOOL)isBack;
        [Export("isBack")]
        //[Verify(MethodToProperty)]
        bool IsBack { get; }

        // -(BOOL)isFront;
        [Export("isFront")]
        //[Verify(MethodToProperty)]
        bool IsFront { get; }

        // -(void)setFocusMode:(WZCameraFocusMode)focusMode focusPoint:(CGPoint)point;
        [Export("setFocusMode:focusPoint:")]
        void SetFocusMode(WZCameraFocusMode focusMode, CGPoint point);

        // -(void)setExposureMode:(WZCameraExposureMode)exposureMode exposurePoint:(CGPoint)point;
        [Export("setExposureMode:exposurePoint:")]
        void SetExposureMode(WZCameraExposureMode exposureMode, CGPoint point);
    }

    // @interface WZCameraPreview : NSObject
    [BaseType(typeof(NSObject))]
    //[DisableDefaultCtor]
    interface WZCameraPreview
    {
        // +(NSInteger)numberOfDeviceCameras;
        [Static]
        [Export("numberOfDeviceCameras")]
        //[Verify(MethodToProperty)]
        nint NumberOfDeviceCameras { get; }

        // +(NSArray<WZCamera *> * _Nonnull)deviceCameras;
        [Static]
        [Export("deviceCameras")]
        //[Verify(MethodToProperty)]
        WZCamera[] DeviceCameras { get; }

        // @property (nonatomic) WZCamera * _Nullable camera;
        [NullAllowed, Export("camera", ArgumentSemantic.Assign)]
        WZCamera Camera { get; set; }

        // @property (nonatomic) WowzaConfig * _Nonnull config;
        [Export("config", ArgumentSemantic.Assign)]
        WowzaConfig Config { get; set; }

        // @property (readonly, nonatomic) AVCaptureVideoPreviewLayer * _Nullable previewLayer;
        [NullAllowed, Export("previewLayer")]
        AVCaptureVideoPreviewLayer PreviewLayer { get; }

        // @property (assign, nonatomic) WZCameraPreviewGravity previewGravity;
        [Export("previewGravity", ArgumentSemantic.Assign)]
        WZCameraPreviewGravity PreviewGravity { get; set; }

        // @property (readonly, nonatomic) NSArray<WZCamera *> * _Nullable cameras;
        [NullAllowed, Export("cameras")]
        WZCamera[] Cameras { get; }

        // @property (readonly, getter = isPreviewActive, nonatomic) BOOL previewActive;
        [Export("previewActive")]
        bool PreviewActive { [Bind("isPreviewActive")] get; }

        // -(instancetype _Nonnull)initWithViewAndConfig:(UIView * _Nonnull)containingView config:(WowzaConfig * _Nonnull)aConfig;
        [Export("initWithViewAndConfig:config:")]
        IntPtr Constructor(UIView containingView, WowzaConfig aConfig);

        // -(void)startPreview;
        [Export("startPreview")]
        void StartPreview();

        // -(void)stopPreview;
        [Export("stopPreview")]
        void StopPreview();

        // -(BOOL)isSwitchCameraAvailableForConfig:(WZMediaConfig * _Nonnull)config;
        [Export("isSwitchCameraAvailableForConfig:")]
        bool IsSwitchCameraAvailableForConfig(WZMediaConfig config);

        // -(WZCamera * _Nonnull)switchCamera;
        [Export("switchCamera")]
        //[Verify(MethodToProperty)]
        WZCamera SwitchCamera { get; }

        // -(WZCamera * _Nullable)otherCamera;
        [NullAllowed, Export("otherCamera")]
        //[Verify(MethodToProperty)]
        WZCamera OtherCamera { get; }
    }

    // @protocol WZVideoEncoderSink <WZMediaSink>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    interface WZVideoEncoderSink : WZMediaSink
    {
        // @required -(void)videoFrameWasEncoded:(CMSampleBufferRef _Nonnull)data;
        //[Abstract]
        [Export("videoFrameWasEncoded:")]
        unsafe void VideoFrameWasEncoded(CMSampleBuffer data);

        // @optional -(void)videoBitrateDidChange:(NSUInteger)newBitrate previousBitrate:(NSUInteger)previousBitrate __attribute__((deprecated("")));
        [Export("videoBitrateDidChange:previousBitrate:")]
        void VideoBitrateDidChange(nuint newBitrate, nuint previousBitrate);
    }

    // @protocol WZVideoSink <WZMediaSink>
    [BaseType(typeof(NSObject))]
    [Protocol, Model]
    interface WZVideoSink : WZMediaSink
    {
        // @required -(void)videoFrameWasCaptured:(CVImageBufferRef _Nonnull)imageBuffer framePresentationTime:(CMTime)framePresentationTime frameDuration:(CMTime)frameDuration;
        [Abstract]
        [Export("videoFrameWasCaptured:framePresentationTime:frameDuration:")]
        unsafe void VideoFrameWasCaptured(CVImageBuffer imageBuffer, CMTime framePresentationTime, CMTime frameDuration);

        // @optional -(void)videoCaptureInterruptionStarted;
        [Export("videoCaptureInterruptionStarted")]
        void VideoCaptureInterruptionStarted();

        // @optional -(void)videoCaptureInterruptionEnded;
        [Export("videoCaptureInterruptionEnded")]
        void VideoCaptureInterruptionEnded();

        // @optional -(void)videoCaptureUsingQueue:(dispatch_queue_t _Nullable)queue;
        [Export("videoCaptureUsingQueue:")]
        void VideoCaptureUsingQueue([NullAllowed] DispatchQueue queue);
    }

    // @interface WZH264Encoder : NSObject <WZBroadcastComponent, WZVideoSink>
    [BaseType(typeof(NSObject))]
    interface WZH264Encoder : IWZBroadcastComponent, IWZVideoSink
    {
        // @property (assign, nonatomic) OSType pixelFormat;
        [Export("pixelFormat")]
        uint PixelFormat { get; set; }

        // -(void)registerSink:(id<WZVideoEncoderSink> _Nonnull)sink;
        [Export("registerSink:")]
        void RegisterSink(WZVideoEncoderSink sink);

        // -(void)unregisterSink:(id<WZVideoEncoderSink> _Nonnull)sink;
        [Export("unregisterSink:")]
        void UnregisterSink(WZVideoEncoderSink sink);
    }

    // @interface WZImageUtilities : NSObject
    [BaseType(typeof(NSObject))]
    interface WZImageUtilities
    {
        // +(UIImage * _Nullable)imageFromCVPixelBuffer:(CVPixelBufferRef _Nonnull)pixelBuffer destinationImageSize:(CGSize)destinationImageSize;
        [Static]
        [Export("imageFromCVPixelBuffer:destinationImageSize:")]
        [return: NullAllowed]
        unsafe UIImage ImageFromCVPixelBuffer(CVPixelBuffer pixelBuffer, CGSize destinationImageSize);

        // +(UIImage * _Nullable)imageFromCVPixelBuffer:(CVPixelBufferRef _Nonnull)pixelBuffer destinationImageSize:(CGSize)destinationImageSize rotationAngle:(NSInteger)rotationAngle;
        [Static]
        [Export("imageFromCVPixelBuffer:destinationImageSize:rotationAngle:")]
        [return: NullAllowed]
        unsafe UIImage ImageFromCVPixelBuffer(CVPixelBuffer pixelBuffer, CGSize destinationImageSize, nint rotationAngle);
    }

    // @interface WZPlatformInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface WZPlatformInfo
    {
        // +(NSString * _Nonnull)model;
        [Static]
        [Export("model")]
        //[Verify(MethodToProperty)]
        string Model { get; }

        // +(NSString * _Nonnull)friendlyModel;
        [Static]
        [Export("friendlyModel")]
        //[Verify(MethodToProperty)]
        string FriendlyModel { get; }

        // +(NSString * _Nonnull)iOSVersion;
        [Static]
        [Export("iOSVersion")]
        //[Verify(MethodToProperty)]
        string IOSVersion { get; }

        // +(NSString * _Nonnull)string;
        [Static]
        [Export("string")]
        //[Verify(MethodToProperty)]
        string String { get; }
    }

    // @interface WZVersionInfo : NSObject
    [BaseType(typeof(NSObject))]
    interface WZVersionInfo
    {
        // +(NSUInteger)majorVersion;
        [Static]
        [Export("majorVersion")]
        //[Verify(MethodToProperty)]
        nuint MajorVersion { get; }

        // +(NSUInteger)minorVersion;
        [Static]
        [Export("minorVersion")]
        //[Verify(MethodToProperty)]
        nuint MinorVersion { get; }

        // +(NSUInteger)revision;
        [Static]
        [Export("revision")]
        //[Verify(MethodToProperty)]
        nuint Revision { get; }

        // +(NSUInteger)buildNumber;
        [Static]
        [Export("buildNumber")]
        //[Verify(MethodToProperty)]
        nuint BuildNumber { get; }

        // +(NSString * _Nonnull)string;
        [Static]
        [Export("string")]
        //[Verify(MethodToProperty)]
        string String { get; }

        // +(NSString * _Nonnull)verboseString;
        [Static]
        [Export("verboseString")]
        //[Verify(MethodToProperty)]
        string VerboseString { get; }
    }

    // @interface WowzaGoCoder : NSObject <WZStatusCallback>
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface WowzaGoCoder : IWZStatusCallback
    {
        // +(NSError * _Nullable)registerLicenseKey:(NSString * _Nonnull)licenseKey;
        [Static]
        [Export("registerLicenseKey:")]
        [return: NullAllowed]
        NSError RegisterLicenseKey(string licenseKey);

        // +(void)setLogLevel:(WowzaGoCoderLogLevel)level;
        [Static]
        [Export("setLogLevel:")]
        void SetLogLevel(WowzaGoCoderLogLevel level);

        // +(void)requestPermissionForType:(WowzaGoCoderPermissionType)type response:(WZPermissionBlock _Nullable)response;
        [Static]
        [Export("requestPermissionForType:response:")]
        void RequestPermissionForType(WowzaGoCoderPermissionType type, [NullAllowed] WZPermissionBlock response);

        // +(WowzaGoCoderCapturePermission)permissionForType:(WowzaGoCoderPermissionType)type;
        [Static]
        [Export("permissionForType:")]
        WowzaGoCoderCapturePermission PermissionForType(WowzaGoCoderPermissionType type);

        // +(instancetype _Nullable)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        //[return: NullAllowed]
        WowzaGoCoder SharedInstance();

        // @property (copy, nonatomic) WowzaConfig * _Nonnull config;
        [Export("config", ArgumentSemantic.Copy)]
        WowzaConfig Config { get; set; }

        // @property (nonatomic) UIView * _Nullable cameraView;
        [NullAllowed, Export("cameraView", ArgumentSemantic.Assign)]
        UIView CameraView { get; set; }

        // @property (readonly, nonatomic) WZCameraPreview * _Nullable cameraPreview;
        [Export("cameraPreview")]
        WZCameraPreview CameraPreview { get; }

        // @property (readonly, nonatomic) WZStatus * _Nonnull status;
        [Export("status")]
        WZStatus Status { get; }

        // @property (getter = isAudioMuted, assign, nonatomic) BOOL audioMuted;
        [Export("audioMuted")]
        bool AudioMuted { [Bind("isAudioMuted")] get; set; }

        // @property (assign, nonatomic) AVAudioSessionCategoryOptions audioSessionOptions;
        [Export("audioSessionOptions", ArgumentSemantic.Assign)]
        AVAudioSessionCategoryOptions AudioSessionOptions { get; set; }

        // @property (readonly, nonatomic) BOOL isStreaming;
        [Export("isStreaming")]
        bool IsStreaming { get; }

        // @property (readonly, nonatomic) WZDataMap * _Nullable metaData;
        [NullAllowed, Export("metaData")]
        WZDataMap MetaData { get; }

        // -(void)startStreaming:(id<WZStatusCallback> _Nullable)statusCallback;
        [Export("startStreaming:")]
        void StartStreaming([NullAllowed] WZStatusCallback statusCallback);

        // -(void)startStreamingWithConfig:(id<WZStatusCallback> _Nullable)statusCallback config:(WowzaConfig * _Nonnull)aConfig;
        [Export("startStreamingWithConfig:config:")]
        void StartStreamingWithConfig([NullAllowed] WZStatusCallback statusCallback, WowzaConfig aConfig);

        // -(void)startStreamingWithPreset:(id<WZStatusCallback> _Nullable)statusCallback preset:(WZFrameSizePreset)aPreset;
        [Export("startStreamingWithPreset:preset:")]
        void StartStreamingWithPreset([NullAllowed] WZStatusCallback statusCallback, WZFrameSizePreset aPreset);

        // -(void)endStreaming:(id<WZStatusCallback> _Nullable)statusCallback;
        [Export("endStreaming:")]
        void EndStreaming([NullAllowed] WZStatusCallback statusCallback);

        // -(void)sendDataEvent:(WZDataScope)scope eventName:(NSString * _Nonnull)eventName params:(WZDataMap * _Nonnull)params callback:(WZDataCallback _Nullable)callback;
        [Export("sendDataEvent:eventName:params:callback:")]
        void SendDataEvent(WZDataScope scope, string eventName, WZDataMap @params, [NullAllowed] WZDataCallback callback);

        // -(void)registerVideoSink:(id<WZVideoSink> _Nonnull)sink;
        [Export("registerVideoSink:")]
        void RegisterVideoSink(WZVideoSink sink);

        // -(void)unregisterVideoSink:(id<WZVideoSink> _Nonnull)sink;
        [Export("unregisterVideoSink:")]
        void UnregisterVideoSink(WZVideoSink sink);

        // -(void)registerAudioSink:(id<WZAudioSink> _Nonnull)sink;
        [Export("registerAudioSink:")]
        void RegisterAudioSink(WZAudioSink sink);

        // -(void)unregisterAudioSink:(id<WZAudioSink> _Nonnull)sink;
        [Export("unregisterAudioSink:")]
        void UnregisterAudioSink(WZAudioSink sink);

        // -(void)registerVideoEncoderSink:(id<WZVideoEncoderSink> _Nonnull)sink;
        [Export("registerVideoEncoderSink:")]
        void RegisterVideoEncoderSink(WZVideoEncoderSink sink);

        // -(void)unregisterVideoEncoderSink:(id<WZVideoEncoderSink> _Nonnull)sink;
        [Export("unregisterVideoEncoderSink:")]
        void UnregisterVideoEncoderSink(WZVideoEncoderSink sink);

        // -(void)registerAudioEncoderSink:(id<WZAudioEncoderSink> _Nonnull)sink;
        [Export("registerAudioEncoderSink:")]
        void RegisterAudioEncoderSink(WZAudioEncoderSink sink);

        // -(void)unregisterAudioEncoderSink:(id<WZAudioEncoderSink> _Nonnull)sink;
        [Export("unregisterAudioEncoderSink:")]
        void UnregisterAudioEncoderSink(WZAudioEncoderSink sink);

        // -(void)registerDataSink:(id<WZDataSink> _Nonnull)sink eventName:(NSString * _Nonnull)eventName;
        [Export("registerDataSink:eventName:")]
        void RegisterDataSink(WZDataSink sink, string eventName);

        // -(void)unregisterDataSink:(id<WZDataSink> _Nonnull)sink eventName:(NSString * _Nonnull)eventName;
        [Export("unregisterDataSink:eventName:")]
        void UnregisterDataSink(WZDataSink sink, string eventName);
    }

    // typedef void (^WZPermissionBlock)(WowzaGoCoderCapturePermission);
    delegate void WZPermissionBlock(WowzaGoCoderCapturePermission arg0);
}

