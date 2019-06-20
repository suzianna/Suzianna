using System;
using System.Collections.Generic;
using System.Text;

namespace Suzianna.Rest.OAuth
{
    public static class GetAccessToken
    {
        public static RopcFlowTask UsingResourceOwnerPasswordCredentialFlow()=> new RopcFlowTask();
    }
}
