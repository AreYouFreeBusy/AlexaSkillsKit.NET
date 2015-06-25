//  Copyright 2015 Stefan Negritoiu (FreeBusy). See LICENSE file for more information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using AlexaSkillsKit;

namespace Sample.Controllers
{
    public class AlexaController : ApiController
    {
        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession() {
            var speechlet = new SampleSessionSpeechlet();
            return speechlet.GetResponse(Request);
        }
    }
}
