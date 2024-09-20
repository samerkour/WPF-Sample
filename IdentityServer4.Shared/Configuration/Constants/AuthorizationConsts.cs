using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Shared.Configuration.Constants
{
    public class AuthorizationConsts
    {
        public const string Policy = "RequireRole";
        public const string AdministrationPolicy = "RequireAdministratorRole";
        public const string DirectorPolicy = "RequireDirectorRole";
        public const string AdminPolicy = "RequireAdminRole";
        public const string UserPolicy = "RequireUserRole";
    }
}
