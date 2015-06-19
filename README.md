# AlexaAppKit.NET
.NET library to write Alexa App "Speechlets" that's interface-compatible with Amazon's AlexaAppKit.
* handles the (de)serialization of Alexa requests & responses into easy-to-use object models
* verifies request signature and timestamp to ensure they are genuine Alexa requests
* performs automatic session management so you can easily build [conversational Alexa apps](https://freebusy.io/blog/building-conversational-alexa-apps-for-amazon-echo)
* vetted by Amazon (Alexa apps written using this library passed certification)

This library was originally developed for and is in use at https://freebusy.io

This library is available as a NuGet package at https://www.nuget.org/packages/AlexaAppKit.NET/

# How To Use

### 1. Set up your development environment

Read [Getting started with Alexa App development for Amazon Echo using .NET on Windows](https://freebusy.io/blog/getting-started-with-alexa-app-development-for-amazon-echo-using-dot-net)

### 2. Implement your app

If your app does any kind of I/O and assuming you're building on top of .NET Framework 4.5 it's recommended that you derive your "Speechlet" from the abstract SpeechletAsync and implement these methods as defined by ISpeechletAsync
  
```csharp
public interface ISpeechletAsync
{
    Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
    Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
    Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
    Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
}
```
  
Or derive your "Speechlet" from the abstract Speechlet and implement these methods as defined by ISpeechlet.
  
```csharp
public interface ISpeechlet
{
    SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
    SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
    void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
    void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
}
```
  
Take a look at https://github.com/AreYouFreeBusy/AlexaAppKit.NET/blob/master/AlexaAppKit.Sample/Speechlet/SampleSessionSpeechlet.cs for an example.

### 3. Wire-up "Speechlet" to HTTP hosting environment

The Sample app is using ASP.NET 4.5 WebApi 2 so wiring-up requests & responses from the HTTP hosting environment (i.e. ASP.NET 4.5) to the "Speechlet" is just a matter of writing a 2-line ApiController like this https://github.com/AreYouFreeBusy/AlexaAppKit.NET/blob/master/AlexaAppKit.Sample/Speechlet/AlexaController.cs 
  
*Note: sample project is generated from the ASP.NET 4.5 WebApi 2 template so it includes a lot of functionality that's not directly related to Alexa Speechlets, but it does make make for a complete Web API project.*

Alternatively you can host your app and the AlexaAppKit.NET library in any other web service framework like ServiceStack.
