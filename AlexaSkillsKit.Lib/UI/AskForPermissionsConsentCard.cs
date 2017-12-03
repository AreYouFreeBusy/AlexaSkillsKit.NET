using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillsKit.UI
{
    public class AskForPermissionsConsentCard : Card
    {
        public override string Type
        {
            get { return "AskForPermissionsConsent"; }
        }

        protected virtual string[] Permissions { get; private set; }

        public virtual PermissionType PermissionType {

            set
            {
                Permissions = value == PermissionType.FullAddress
                    ? new string[] {"read::alexa:device:all:address"}
                    : new string[] {"read::alexa:device:all:address:country_and_postal_code"};
            }
        } 

        
    }

    public enum  PermissionType
    {
        FullAddress,
        CountryAndPostalCode
    }
}
