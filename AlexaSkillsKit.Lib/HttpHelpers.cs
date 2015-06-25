//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit
{
    public static class HttpHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        public static string ToLogString(this HttpRequestMessage httpRequest) {
            var serializedRequest = AsyncHelpers.RunSync<byte[]>(() => 
                new HttpMessageContent(httpRequest).ReadAsByteArrayAsync());
            return UTF8Encoding.UTF8.GetString(serializedRequest);
        }


        /// <summary>
        /// 
        /// </summary>
        public static string ToLogString(this HttpResponseMessage httpResponse) {
            var serializedRequest = AsyncHelpers.RunSync<byte[]>(() => 
                new HttpMessageContent(httpResponse).ReadAsByteArrayAsync());
            return UTF8Encoding.UTF8.GetString(serializedRequest);
        } 
    }
}