using Nancy.Authentication.Basic;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;
using Nancy.Security;
using System;

namespace WebApi.Auth
{
    // https://github.com/NancyFx/Nancy/blob/master/samples/Nancy.Demo.Authentication.Basic
    public class UserValidator : IUserValidator
    {
        public ClaimsPrincipal Validate(string username, string password)
        {
            if (username == "user" && password == "user")
            {
                Claim[] claims = new Claim[1];
                claims[0] = new Claim(ClaimTypes.Role, "user");

                return new ClaimsPrincipal(new GenericIdentity(username, "user"));
            }

            if (username == "admin" && password == "admin")
            {
                return new ClaimsPrincipal(new GenericIdentity(username, "admin"));
            }

            // Not recognised => anonymous.
            return null;
        }

        
    }

}
