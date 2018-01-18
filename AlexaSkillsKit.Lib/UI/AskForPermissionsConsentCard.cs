// Copyright 2018 Stefan Negritoiu (FreeBusy) and contributors. See LICENSE file for more information.

using System.Collections.Generic;

namespace AlexaSkillsKit.UI
{
    /// <summary>
    /// https://developer.amazon.com/docs/custom-skills/device-address-api.html#permissions-card
    /// </summary>
    public class AskForPermissionsConsentCard : Card
    {
        public override string Type {
            get { return "AskForPermissionsConsent"; }
        }

        public virtual IEnumerable<string> Permissions {
            get;
            set;
        }
    }
}