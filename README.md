#AlexaAppKit.NET
.NET library to write Alexa App "Speechlets" that's interface-compatible with Amazon's AlexaAppKit
* handles the (de)serialization of Alexa requests & responses into easy-to-use object models
* verifies request signatures to authenticate the request is a genuine Alexa request
* gives you an interface to help organize your logic in a manner consistent with the Alexa app model

This library was originally developed for and is in use at https://freebusy.io

This library is available as a NuGet package at https://www.nuget.org/packages/AlexaAppKit.NET/

# How To Use
Derive your "Speechlet" from the abstract SpeechletAsync (recommended) or Speechlet and implement the methods in ISpeechletAsync or ISpeechlet interface.

The Sample project is generated from the ASP.NET 4.5 WebApi 2 template so it includes a lot of functionality that's not directly related to Alexa Speechlets, but it does make make for a complete Web API project. See \Speechlet subfolder for the actual implementation of the sample speechlet.

