// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using AlexaSkillsKit.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.Helpers
{
    public class CustomerInformation
    {

        public class mobileNumber
        {
            public string countryCode { get; set; }
            public string phoneNumber { get; set; }
        }


        public void GETAccountFullName(SpeechletRequestEnvelope requestEnvelope, out string strAccountFullName, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/accounts/~current/settings/Profile.name";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            strResult = strResult.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(strResult))
                strResult = null;

            strAccountFullName = strResult;
        }


        public void GETAccountGivenName(SpeechletRequestEnvelope requestEnvelope, out string strAccountGivenName, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/accounts/~current/settings/Profile.givenName";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }

            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;

            }
            strResult = strResult.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(strResult))
                strResult = null;

            strAccountGivenName = strResult;
        }


        public void GETAccountEmail(SpeechletRequestEnvelope requestEnvelope, out string strAccountEmail, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/accounts/~current/settings/Profile.email";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            strResult = strResult.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(strResult))
                strResult = null;

            strAccountEmail = strResult;
        }


        public void GETAccountMobile(SpeechletRequestEnvelope requestEnvelope, out mobileNumber objMobileNumber, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/accounts/~current/settings/Profile.mobileNumber";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }

            objMobileNumber = (mobileNumber)Newtonsoft.Json.JsonConvert.DeserializeObject(strResult, typeof(mobileNumber));
        }


        public void GETProfileFullName(SpeechletRequestEnvelope requestEnvelope, out string strAccountFullName, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/persons/~current/profile/name";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            strResult = strResult.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(strResult))
                strResult = null;

            strAccountFullName = strResult;
        }


        public void GETProfileGivenName(SpeechletRequestEnvelope requestEnvelope, out string strAccountGivenName, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/persons/~current/profile/givenName";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }

            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }
            strResult = strResult.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(strResult))
                strResult = null;

            strAccountGivenName = strResult;
        }


        public void GETProfileMobile(SpeechletRequestEnvelope requestEnvelope, out mobileNumber objMobileNumber, out int nResponseCode)
        {
            string responseText;
            string strResult = "";
            nResponseCode = 200;
            string requestUri = @"" + requestEnvelope.Context.System.ApiEndpoint + "/v2/persons/~current/profile/mobileNumber";
            HttpWebRequest requestClient = WebRequest.Create(requestUri) as HttpWebRequest;
            requestClient.Method = "GET";
            requestClient.ContentType = "application/json";
            requestClient.Headers["Accept-Language"] = requestEnvelope.Request.Locale;
            requestClient.Headers["authorization"] = "Bearer " + requestEnvelope.Context.System.ApiAccessToken;

            HttpWebResponse response;
            try
            {
                using (response = requestClient.GetResponse() as HttpWebResponse)
                {
                    nResponseCode = (int)response.StatusCode;
                    StreamReader str = new StreamReader(response.GetResponseStream());
                    strResult += str.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responseText = "There was an error with the GET Request to Alexa Servers";
                responseText += " Exception: " + ex.Message;
                nResponseCode = (int)((HttpWebResponse)ex.Response).StatusCode;
            }

            objMobileNumber = (mobileNumber)Newtonsoft.Json.JsonConvert.DeserializeObject(strResult, typeof(mobileNumber));

        }
    }
}
