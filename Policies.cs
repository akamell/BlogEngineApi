using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApi
{
    public class Policies
    {
        public const string Admin = "Admin";
        public const string Writer = "Writer";
        public const string Editor = "Editor";
        public static AuthorizationPolicy AdminPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();

        public static AuthorizationPolicy WriterPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Writer).Build();

        public static AuthorizationPolicy EditorPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Editor).Build();

    }
}