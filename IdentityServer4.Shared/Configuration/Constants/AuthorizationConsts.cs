using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Shared.Configuration.Constants
{
    public class AuthorizationConsts
    {
        public const string FarmPolicy = "RequireFarmRole";
        public const string AdministrationPolicy = "RequireAdministratorRole";
        public const string FarmDirectorPolicy = "RequireFarmDirectorRole";
        public const string FarmAdminPolicy = "RequireFarmAdminRole";
        public const string FarmUserPolicy = "RequireFarmUserRole";
    }
}
