using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Shared.Configuration
{
    public class AdminApiConfiguration
    {
        public string ApiName { get; set; }

        public ICollection<VersionModel> ApiVersions { get; set; }

        public string IdentityServerBaseUrl { get; set; }

        public string ApiBaseUrl { get; set; }

        public string OidcSwaggerUIClientId { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public string OidcApiName { get; set; }

        public ICollection<string> AdministrationRoles { get; set; }

        public bool CorsAllowAnyOrigin { get; set; }

        public string[] CorsAllowOrigins { get; set; }
    }

    public class VersionModel
    {
        public string Version { get; set; }
        public string Description { get; set; }

        //public Uri TermsOfService { get; set; }
    }

}
