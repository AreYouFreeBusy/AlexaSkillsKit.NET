# AlexaSkillsKit.NET
.NET library to write Alexa skills that's interface-compatible with [Amazon's AlexaSkillsKit for Java](https://github.com/amzn/alexa-skills-kit-java) and matches that functionality:
* handles the (de)serialization of Alexa requests & responses into easy-to-use object models
* verifies authenticity of the request by validating its signature and timestamp
* code-reviewed and vetted by Amazon (Alexa skills written using this library passed certification)
* :new: supports following interfaces:
  * [AudioPlayer](https://developer.amazon.com/docs/custom-skills/audioplayer-interface-reference.html)
  * [PlaybackController](https://developer.amazon.com/docs/custom-skills/playback-controller-interface-reference.html)
  * [Display](https://developer.amazon.com/docs/custom-skills/display-interface-reference.html)
  * [Dialog](https://developer.amazon.com/docs/custom-skills/dialog-interface-reference.html)
  * [VideoApp](https://developer.amazon.com/docs/custom-skills/videoapp-interface-reference.html)
  

Beyond the functionality in Amazon's AlexaSkillsKit for Java, AlexaSkillsKit.NET:
* performs automatic session management so you can easily [build conversational Alexa apps](https://freebusy.io/blog/building-conversational-alexa-apps-for-amazon-echo)
* supports asynchronous programming model
* :new: can be extended to support custom and new coming interfaces and request types
(see [Implement external interface](#implement-external-interface) section), such as:
  * [Skill Events](https://developer.amazon.com/docs/smapi/skill-events-in-alexa-skills.html)

This library was originally developed for and is in use at https://freebusy.io

This library is available as a NuGet package at https://www.nuget.org/packages/AlexaSkillsKit.NET/

# How To Use

### 1. Set up your development environment

Read [Getting started with Alexa App development for Amazon Echo using .NET on Windows](https://freebusy.io/blog/getting-started-with-alexa-app-development-for-amazon-echo-using-dot-net)

Note, that if you are hosting your API in Amazon Lambda, Azure Function, Azure Web App or other well-known cloud service, you can use parent domain certificate instead of providing your own.

### 2. Implement your skill as a "Speechlet"

If your Alexa skill does any kind of asynchronous I/O, it's recommended that you derive your app from the `SpeechletBase` class and implement these methods as defined by `ISpeechletWithContextAsync`:
  
```csharp
public interface ISpeechletWithContextAsync
{
    Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context);
    Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
    Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
}
```
  
Alternatively, you can implement synchronous `ISpeechletWithContext` interface:
  
```csharp
public interface ISpeechletWithContext
{
    SpeechletResponse OnIntent(IntentRequest intentRequest, Session session, Context context);
    SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
    void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
    void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
}
```

To handle external interface requests, your speechlet can implement additional interfaces either synchronous or asynchronous:
* `IAudioPlayerSpeechletAsync` or `IAudioPlayerSpeechlet` for "AudioPlayer.*" and "PlaybackController.*" requests.
* `IDisplaySpeechletAsync` or `IDisplaySpeechlet` for "Display.*" requests.

For backwards compatibility `Speechlet` and `SpeechletAsync` abstract classes are still available, along with deprecated `ISpeechlet` and `ISpeechletAsync` interfaces.
Note, that old interfaces don't support [Context object](https://developer.amazon.com/docs/custom-skills/request-and-response-json-reference.html#context-object).
  
Take a look at https://github.com/AreYouFreeBusy/AlexaSkillsKit.NET/blob/master/AlexaSkillsKit.Sample/Speechlet/SampleSessionSpeechlet.cs for an example.

### 3. Wire-up "Speechlet" to HTTP hosting environment

The Sample app is using ASP.NET 4.5 WebApi 2 so wiring-up requests & responses from the HTTP hosting environment (i.e. ASP.NET 4.5) to the "Speechlet" is just a matter of writing a 2-line ApiController like this https://github.com/AreYouFreeBusy/AlexaSkillsKit.NET/blob/master/AlexaSkillsKit.Sample/Speechlet/AlexaController.cs 
  
*Note: sample project is generated from the ASP.NET 4.5 WebApi 2 template so it includes a lot of functionality that's not directly related to Alexa Speechlets, but it does make make for a complete Web API project.*

Alternatively you can host your app and the AlexaSkillsKit.NET library in any other web service framework like ServiceStack.

# How it works

### SpeechletService class
To handle Alexa requests an instance of `SpeechletService` is used. This is the main entry point for all operations involved in handling incoming requests.

For convenience and backward compatibility both `Speechlet` and `SpeechletAsync` now derive from `SpeechletBase` base class,
which provides built-in `SpeechletService` instance and wraps most common operations with it.
Skill authors can access this internal `SpeechletService` through `Service` property, e.g. to register additional request handlers.

### Parsing request

When new request is recieved, it first needs to be parced from json string into object model represented by `SpeechletRequestEnvelope` class.
`Task<SpeechletRequestEnvelope> SpeechletService.GetRequestAsync(string content, string chainUrl, string signature)` method is used for this.
Request headers and body validation also takes place during this step. The `SpeechletValidationException` is produced in case of any validation error.

See [Override request validation policy](#override-request-validation-policy) for more details on request validation.
Request validation can be omitted by directly calling one of `SpeechletRequestEnvelope.FromJson` static methods.

Only version "1.0" of Alexa Skill API is supported. Otherwise `SpeechletValidationException` with `SpeechletRequestValidationResult.InvalidVersion` is thrown.
Same is true, when calling `SpeechletRequestEnvelope.FromJson` methods directly.

There are a lot of different request types available for Alexa Skills.
Standard requests have simple type names: "LaunchRequest", "IntentRequest", "SessionEndedRequest".
All other requests are related to specific interfaces and their request type name consists of interface name and request subtype name separated by "." sign,
e.g. "System.ExceptionEncountered", "Dialog.Delegate" and so on.
 
`SpeechletRequestParser` class is used to deserialize `request` json field to appropriate subclass of `SpeechletRequest` base class.
By default, it has no knowledge to which class each request type should be deserialized.
`SpeechletRequestParser` has `AddInterface` method to bind interface name, with specific deserializagion handler.

`SpeechletRequestEnvelope` currently uses it's static instance of `SpeechletRequestParser` for request deserialization,
provided as `SpeechletRequestEnvelope.RequestParser` static property.
Use it, if you need to register deserialization method for additional request type.

Deserialization methods for all natively supported requests are registered internally by default.

## Advanced

### Override request validation policy

By default, requests with missing or invalid signatures, or with missing or too old timestamps,
unsupported API version (only API v"1.0" is supported), are rejected.
For Application Id to be validated, your skill needs to set value for `SpeechletService.ApplicationId` property. 
You can override the request validation policy if you'd like not to reject the request in certain conditions and/or to log validation failures.

```csharp
/// <summary>
/// return true if you want request to be processed, otherwise false
/// </summary>
public override bool OnRequestValidation(
    SpeechletRequestValidationResult result, DateTime referenceTimeUtc, SpeechletRequestEnvelope requestEnvelope) 
{

    if (result != SpeechletRequestValidationResult.OK) 
    {
        if (result.HasFlag(SpeechletRequestValidationResult.NoSignatureHeader)) 
        {
            Debug.WriteLine("Alexa request is missing signature header, rejecting.");
            return false;
        }
        if (result.HasFlag(SpeechletRequestValidationResult.NoCertHeader)) 
        {
            Debug.WriteLine("Alexa request is missing certificate header, rejecting.");
            return false;
        }
        if (result.HasFlag(SpeechletRequestValidationResult.InvalidSignature)) 
        {
            Debug.WriteLine("Alexa request signature is invalid, rejecting.");
            return false;
        }
        else 
        {
            if (result.HasFlag(SpeechletRequestValidationResult.InvalidTimestamp)) 
            {
                var diff = referenceTimeUtc - requestEnvelope.Request.Timestamp;
                Debug.WriteLine("Alexa request timestamped '{0:0.00}' seconds ago making timestamp invalid, but continue processing.",
                    diff.TotalSeconds);
            }
            return true;
        }
    }
    else 
    {      
        var diff = referenceTimeUtc - requestEnvelope.Request.Timestamp;
        Debug.WriteLine("Alexa request timestamped '{0:0.00}' seconds ago.", diff.TotalSeconds);
        return true;
    }            
}
```

# Credits

The original author of AlexaSkillsKit.NET is:
* [Stefan Negritoiu (FreeBusy)](https://github.com/stefann42)

The current authors and maintainers are:
* [Stefan Negritoiu (FreeBusy)](https://github.com/stefann42)
* [Sergey Greenko](https://github.com/ElvenMonky)

**Thank You** to library contributors (in alphbetical order):
* [Ahmed Osman](https://github.com/q3blend)
* [Chris Pauly](https://github.com/chrispauly)
* [dg-racing](https://github.com/dg-racing)
* [Dustin Masters](https://github.com/dustinsoftware)
* [Eric Jernigan](https://github.com/jejernig)
* [Jasson Moya](https://github.com/Jasc01)
* [Jayson Helseth](https://github.com/jaysonhelseth)
* [Marcus Braun](https://github.com/teriansilva)
* [Matt Becker](https://github.com/MattBecker)
* [Robert Mroch](https://github.com/epsilon-rmroch)
* [ruppert92](https://github.com/ruppert92)
* [Sergey Greenko](https://github.com/ElvenMonky)
* [Stefan Negritoiu](https://github.com/stefann42)
* [vp123456](https://github.com/vp123456)

Contributor License Agreement:   
https://cla-assistant.io/AreYouFreeBusy/AlexaSkillsKit.NET
