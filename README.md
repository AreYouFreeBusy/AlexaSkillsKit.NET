# AlexaSkillsKit.NET
.NET library to write Alexa skills that's interface-compatible with [Amazon's AlexaSkillsKit for Java](https://github.com/amzn/alexa-skills-kit-java) and matches that functionality:
* handles the (de)serialization of Alexa requests & responses into easy-to-use object models
* verifies authenticity of the request by validating its signature and timestamp
* code-reviewed and vetted by Amazon (Alexa skills written using this library passed certification)

Beyond the functionality in Amazon's AlexaSkillsKit for Java, AlexaSkillsKit.NET:
* performs automatic session management so you can easily [build conversational Alexa apps](https://freebusy.io/blog/building-conversational-alexa-apps-for-amazon-echo)


This library was originally developed for and is in use at https://freebusy.io

This library is available as a NuGet package at https://www.nuget.org/packages/AlexaSkillsKit.NET/

# How To Use

### 1. Set up your development environment

Read [Getting started with Alexa App development for Amazon Echo using .NET on Windows](https://freebusy.io/blog/getting-started-with-alexa-app-development-for-amazon-echo-using-dot-net)

### 2. Implement your skill as a "Speechlet"

If your Alexa skill does any kind of I/O and assuming you're building on top of .NET Framework 4.5 it's recommended that you derive your app from the abstract SpeechletAsync and implement these methods as defined by ISpeechletAsync
  
```csharp
public interface ISpeechletAsync
{
    Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
    Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
    Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
}
```
  
Or derive your app from the abstract Speechlet and implement these methods as defined by ISpeechlet.
  
```csharp
public interface ISpeechlet
{
    SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
    SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
    void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
    void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
}
```
  
Take a look at https://github.com/AreYouFreeBusy/AlexaSkillsKit.NET/blob/master/AlexaSkillsKit.Sample/Speechlet/SampleSessionSpeechlet.cs for an example.

### 3. Wire-up "Speechlet" to HTTP hosting environment

The Sample app is using ASP.NET 4.5 WebApi 2 so wiring-up requests & responses from the HTTP hosting environment (i.e. ASP.NET 4.5) to the "Speechlet" is just a matter of writing a 2-line ApiController like this https://github.com/AreYouFreeBusy/AlexaSkillsKit.NET/blob/master/AlexaSkillsKit.Sample/Speechlet/AlexaController.cs 
  
*Note: sample project is generated from the ASP.NET 4.5 WebApi 2 template so it includes a lot of functionality that's not directly related to Alexa Speechlets, but it does make make for a complete Web API project.*

Alternatively you can host your app and the AlexaSkillsKit.NET library in any other web service framework like ServiceStack.

## Advanced

### Override request validation policy

By default, requests with missing or invalid signatures, or with missing or too old timestamps, are rejected. You can override the request validation policy if you'd like not to reject the request in certain conditions and/or to log validation failures.

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
